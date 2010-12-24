#pragma once
#include "stdatl.h"

//
//	from atlfile.h
//
//-----------------------------------------------
// namespace mwg::ATL
namespace mwg{
namespace ATL{
//-----------------------------------------------
	public ref class CAtlFileMappingBase{
	private:
		void* m_pData;
		SIZE_T m_nMappingSize;
		HANDLE m_hMapping;
		Win32::ULARGE_INTEGER m_nOffset; // ULARGE_INTEGER
		DWORD m_dwViewDesiredAccess;
	public: // 初期化・削除
		CAtlFileMappingBase(){ //throw()
			m_pData = NULL;
			m_hMapping = NULL;
		}
		~CAtlFileMappingBase(){this->!CAtlFileMappingBase();}
		!CAtlFileMappingBase(){ //throw()
			Unmap();
		}
		HRESULT Unmap(){ //throw()
			HRESULT hr = S_OK;

			if(m_pData!=NULL){
				if(!::UnmapViewOfFile(m_pData))
					hr=AtlHresultFromLastError();
				m_pData=NULL;
			}

			if(m_hMapping!=NULL){
				if(!::CloseHandle(m_hMapping)&&SUCCEEDED(hr))
					hr=AtlHresultFromLastError();
				m_hMapping=NULL;
			}

			return hr;
		}
	public: // 複製
		CAtlFileMappingBase(__in CAtlFileMappingBase^ orig){
			if(orig->m_hMapping==NULL){
				m_pData=NULL;
				m_hMapping=NULL;
				return;
			}

			m_dwViewDesiredAccess=orig->m_dwViewDesiredAccess;
			m_nOffset=orig->m_nOffset;
			m_nMappingSize=orig->m_nMappingSize;

			// create Handle
			HANDLE hMapping=this->m_hMapping;
			if(!::DuplicateHandle(
				::GetCurrentProcess(),orig->m_hMapping,
				::GetCurrentProcess(),&hMapping,
				NULL,TRUE,DUPLICATE_SAME_ACCESS
				)
			){
				HRESULT hr=AtlHresultFromLastError();
				if(FAILED(hr))AtlThrow(hr);
				return; // 茲には到達しない筈
			}
			this->m_hMapping=hMapping;

			// get MapView of file
			m_pData=::MapViewOfFileEx(
				m_hMapping,m_dwViewDesiredAccess,
				this->m_nOffset.HighPart,this->m_nOffset.LowPart,
				m_nMappingSize,NULL
				);
			if(m_pData==NULL){
				HRESULT hr=AtlHresultFromLastError();
				::CloseHandle(m_hMapping);
				m_hMapping=NULL;
				if(FAILED(hr))AtlThrow(hr);
				return; // 茲には到達しない筈
			}
		}

		// original
		CAtlFileMappingBase^ Clone(){
			return gcnew CAtlFileMappingBase(this);
		}
#if FALSE
		HRESULT CopyFrom(__in CAtlFileMappingBase^ orig){ //throw()
			if(this == &orig)
				return S_OK;
			ATLASSUME(m_pData == NULL);
			ATLASSUME(m_hMapping == NULL);
			ATLASSERT(orig.m_pData != NULL);
			ATLASSERT(orig.m_hMapping != NULL);

			m_dwViewDesiredAccess=orig.m_dwViewDesiredAccess;
			m_nOffset.QuadPart=orig.m_nOffset.QuadPart;
			m_nMappingSize=orig.m_nMappingSize;

			if(!::DuplicateHandle(GetCurrentProcess(),orig.m_hMapping,GetCurrentProcess(),&m_hMapping,NULL,TRUE,DUPLICATE_SAME_ACCESS))
				return AtlHresultFromLastError();

			m_pData=::MapViewOfFileEx(m_hMapping, m_dwViewDesiredAccess, m_nOffset.HighPart, m_nOffset.LowPart, m_nMappingSize, NULL);
			if(m_pData == NULL){
				HRESULT hr;

				hr = AtlHresultFromLastError();
				::CloseHandle(m_hMapping);
				m_hMapping=NULL;
				return hr;
			}

			return S_OK;
		}
		CAtlFileMappingBase(/*__in*/ CAtlFileMappingBase^ orig){
			m_pData = NULL;
			m_hMapping = NULL;

			HRESULT hr = CopyFrom(orig);
			if (FAILED(hr))
				AtlThrow(hr);
		}
		CAtlFileMappingBase& operator=(/*__in*/ CAtlFileMappingBase^ orig){
			HRESULT hr = CopyFrom(orig);
			if (FAILED(hr))
				AtlThrow(hr);

			return *this;
		}
#endif
	public: // メモリ確保
		HRESULT MapFile(
			__in HANDLE hFile
		){
			return this->MapFile(hFile,0,0,PAGE_READONLY,FILE_MAP_READ);
		}
		HRESULT MapFile(
			__in HANDLE hFile,
			__in SIZE_T nMappingSize
		){
			return this->MapFile(hFile,nMappingSize,0,PAGE_READONLY,FILE_MAP_READ);
		}
		HRESULT MapFile(
			__in HANDLE hFile,
			__in SIZE_T nMappingSize,
			__in ULONGLONG nOffset
		){
			return this->MapFile(hFile,nMappingSize,nOffset,PAGE_READONLY,FILE_MAP_READ);
		}
		HRESULT MapFile(
			__in HANDLE hFile,
			__in SIZE_T nMappingSize,
			__in ULONGLONG nOffset,
			__in DWORD dwMappingProtection
		){
			return this->MapFile(hFile,nMappingSize,nOffset,dwMappingProtection,FILE_MAP_READ);
		}
		HRESULT MapFile(
			__in HANDLE hFile,
			__in SIZE_T nMappingSize,// = 0,
			__in ULONGLONG nOffset,// = 0,
			__in DWORD dwMappingProtection,// = PAGE_READONLY,
			__in DWORD dwViewDesiredAccess// = FILE_MAP_READ
		)/*throw()*/{
			ATLASSUME(m_pData == NULL);
			ATLASSUME(m_hMapping == NULL);
			ATLASSERT(hFile != INVALID_HANDLE_VALUE && hFile != NULL);

			ULARGE_INTEGER liFileSize;
			liFileSize.LowPart=::GetFileSize(hFile,&liFileSize.HighPart);

			if(liFileSize.QuadPart<nMappingSize)
				liFileSize.QuadPart=nMappingSize;

			m_hMapping=::CreateFileMapping(hFile,NULL,dwMappingProtection,liFileSize.HighPart,liFileSize.LowPart,0);
			if(m_hMapping==NULL)
				return AtlHresultFromLastError();

			if(nMappingSize==0)
				m_nMappingSize=(SIZE_T)(liFileSize.QuadPart - nOffset);
			else
				m_nMappingSize=nMappingSize;

			m_dwViewDesiredAccess=dwViewDesiredAccess;
			m_nOffset.QuadPart=nOffset;

			m_pData=::MapViewOfFileEx(m_hMapping, m_dwViewDesiredAccess, m_nOffset.HighPart, m_nOffset.LowPart, m_nMappingSize, NULL);
			if(m_pData == NULL){
				HRESULT hr=AtlHresultFromLastError();
				::CloseHandle(m_hMapping);
				m_hMapping=NULL;
				return hr;
			}

			return S_OK;
		}
		HRESULT MapSharedMem(
			__in SIZE_T nMappingSize,
			__in LPCTSTR szName
		){
			return this->MapSharedMem(nMappingSize,szName,NULL,NULL,PAGE_READWRITE,FILE_MAP_ALL_ACCESS);
		}
		HRESULT MapSharedMem(
			__in SIZE_T nMappingSize,
			__in LPCTSTR szName,
			__out_opt BOOL* pbAlreadyExisted
		){
			return this->MapSharedMem(nMappingSize,szName,pbAlreadyExisted,NULL,PAGE_READWRITE,FILE_MAP_ALL_ACCESS);
		}
		HRESULT MapSharedMem(
			__in SIZE_T nMappingSize,
			__in LPCTSTR szName,
			__out_opt BOOL* pbAlreadyExisted,
			__in_opt LPSECURITY_ATTRIBUTES lpsa
		){
			return this->MapSharedMem(nMappingSize,szName,pbAlreadyExisted,lpsa,PAGE_READWRITE,FILE_MAP_ALL_ACCESS);
		}
		HRESULT MapSharedMem(
			__in SIZE_T nMappingSize,
			__in LPCTSTR szName,
			__out_opt BOOL* pbAlreadyExisted,
			__in_opt LPSECURITY_ATTRIBUTES lpsa,
			__in DWORD dwMappingProtection
		){
			return this->MapSharedMem(nMappingSize,szName,pbAlreadyExisted,lpsa,dwMappingProtection,FILE_MAP_ALL_ACCESS);
		}
		HRESULT MapSharedMem(
			__in SIZE_T nMappingSize,
			__in LPCTSTR szName,
			__out_opt BOOL* pbAlreadyExisted,// = NULL,
			__in_opt LPSECURITY_ATTRIBUTES lpsa,// = NULL,
			__in DWORD dwMappingProtection,// = PAGE_READWRITE,
			__in DWORD dwViewDesiredAccess// = FILE_MAP_ALL_ACCESS
		)/*throw()*/{
			ATLASSUME(m_pData == NULL);
			ATLASSUME(m_hMapping == NULL);
			ATLASSERT(nMappingSize > 0);
			ATLASSERT(szName != NULL); // if you just want a regular chunk of memory, use a heap allocator

			m_nMappingSize = nMappingSize;

			ULARGE_INTEGER nSize;
			nSize.QuadPart = nMappingSize;
			m_hMapping = ::CreateFileMapping(INVALID_HANDLE_VALUE, lpsa, dwMappingProtection, nSize.HighPart, nSize.LowPart, szName);
			if (m_hMapping == NULL)
				return AtlHresultFromLastError();

			if (pbAlreadyExisted != NULL)
				*pbAlreadyExisted = (::GetLastError() == ERROR_ALREADY_EXISTS);

			m_dwViewDesiredAccess = dwViewDesiredAccess;
			m_nOffset.QuadPart = 0;

			m_pData = ::MapViewOfFileEx(m_hMapping, m_dwViewDesiredAccess, m_nOffset.HighPart, m_nOffset.LowPart, m_nMappingSize, NULL);
			if(m_pData==NULL){
				HRESULT hr=AtlHresultFromLastError();
				::CloseHandle(m_hMapping);
				return hr;
			}

			return S_OK;
		}
		HRESULT OpenMapping(
			__in LPCTSTR szName,
			__in SIZE_T nMappingSize
		){
			return this->OpenMapping(szName,nMappingSize,0,FILE_MAP_ALL_ACCESS);
		}
		HRESULT OpenMapping(
			__in LPCTSTR szName,
			__in SIZE_T nMappingSize,
			__in ULONGLONG nOffset
		){
			return this->OpenMapping(szName,nMappingSize,nOffset,FILE_MAP_ALL_ACCESS);
		}
		HRESULT OpenMapping(
			__in LPCTSTR szName,
			__in SIZE_T nMappingSize,
			__in ULONGLONG nOffset,// = 0,
			__in DWORD dwViewDesiredAccess// = FILE_MAP_ALL_ACCESS
		)/*throw()*/{
			ATLASSUME(m_pData == NULL);
			ATLASSUME(m_hMapping == NULL);
			ATLASSERT(szName != NULL); // if you just want a regular chunk of memory, use a heap allocator

			m_nMappingSize = nMappingSize;
			m_dwViewDesiredAccess = dwViewDesiredAccess;

			m_hMapping = ::OpenFileMapping(m_dwViewDesiredAccess, FALSE, szName);
			if (m_hMapping == NULL)
				return AtlHresultFromLastError();

			m_dwViewDesiredAccess=dwViewDesiredAccess;
			m_nOffset.QuadPart=nOffset;

			m_pData=::MapViewOfFileEx(m_hMapping, m_dwViewDesiredAccess, m_nOffset.HighPart, m_nOffset.LowPart, m_nMappingSize, NULL);
			if(m_pData==NULL){
				HRESULT hr=AtlHresultFromLastError();
				::CloseHandle(m_hMapping);
				return hr;
			}

			return S_OK;
		}
	public: // 状態
		void* GetData(){ //const throw()
			return m_pData;
		}
		HANDLE GetHandle(){ //const throw()
			return m_hMapping;
		}
		SIZE_T GetMappingSize(){ //throw()
			return m_nMappingSize;
		}
	};
//-----------------------------------------------
template<typename T = char>
public ref class CAtlFileMapping:public CAtlFileMappingBase{
public:
	static operator T*(CAtlFileMapping<T>^ value){
		return reinterpret_cast<T*>(value->GetData());
	}
};
//-----------------------------------------------
// end of namespace mwg::ATL
}
}
//-----------------------------------------------
