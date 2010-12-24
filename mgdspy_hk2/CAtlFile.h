#pragma once
#include "stdatl.h"
#include "CHandle.h"
//
//	from atlbase.h
//
//-----------------------------------------------
// namespace mwg::ATL
namespace mwg{
namespace ATL{
//-----------------------------------------------
	public ref class CAtlFile:public CHandle{
	public:
		CAtlFile(){}
		
		/// Transfers ownership
		CAtlFile(__in CAtlFile^ file):CHandle(file){}

		/// Takes ownership
		explicit CAtlFile( __in HANDLE hFile ):CHandle(hFile){}

		//-----------------------------------------------------------------
		//		Create
		//-----------------------------------------------------------------
		HRESULT Create(
			__in LPCTSTR szFilename,
			__in DWORD dwDesiredAccess,
			__in DWORD dwShareMode,
			__in DWORD dwCreationDisposition
		){
			return Create(szFilename,dwDesiredAccess,dwShareMode,dwCreationDisposition,FILE_ATTRIBUTE_NORMAL,NULL,NULL);
		}
		HRESULT Create(
			__in LPCTSTR szFilename,
			__in DWORD dwDesiredAccess,
			__in DWORD dwShareMode,
			__in DWORD dwCreationDisposition,
			__in DWORD dwFlagsAndAttributes
		){
			return Create(szFilename,dwDesiredAccess,dwShareMode,dwCreationDisposition,dwFlagsAndAttributes,NULL,NULL);
		}
		HRESULT Create(
			__in LPCTSTR szFilename,
			__in DWORD dwDesiredAccess,
			__in DWORD dwShareMode,
			__in DWORD dwCreationDisposition,
			__in DWORD dwFlagsAndAttributes,
			__in_opt LPSECURITY_ATTRIBUTES lpsa
		){
			return Create(szFilename,dwDesiredAccess,dwShareMode,dwCreationDisposition,dwFlagsAndAttributes,lpsa,NULL);
		}
		HRESULT Create(
			__in LPCTSTR szFilename,
			__in DWORD dwDesiredAccess,
			__in DWORD dwShareMode,
			__in DWORD dwCreationDisposition,
			__in DWORD dwFlagsAndAttributes,// = FILE_ATTRIBUTE_NORMAL,
			__in_opt LPSECURITY_ATTRIBUTES lpsa,// = NULL,
			__in_opt HANDLE hTemplateFile// = NULL
		){
			ATLASSUME(m_h == NULL);

			HANDLE hFile=::CreateFile(
				szFilename,
				dwDesiredAccess,
				dwShareMode,
				lpsa,
				dwCreationDisposition,
				dwFlagsAndAttributes,
				hTemplateFile
				);

			if(hFile==INVALID_HANDLE_VALUE)
				return AtlHresultFromLastError();

			this->Attach(hFile);
			return S_OK;
		}

		//-----------------------------------------------------------------
		//		Read
		//-----------------------------------------------------------------
		HRESULT Read(
			__out_bcount(nBufSize) LPVOID pBuffer,
			DWORD nBufSize
		){
			ATLASSUME(m_h != NULL);

			DWORD nBytesRead=0;
			if(!::ReadFile(m_h,pBuffer,nBufSize,&nBytesRead,NULL))
				return AtlHresultFromLastError();
			if(nBytesRead != nBufSize)
				return HRESULT_FROM_WIN32( ERROR_HANDLE_EOF );

			return S_OK;
		}
		HRESULT Read(
			__out_bcount(nBufSize) LPVOID pBuffer,
			__in DWORD nBufSize,
			__out DWORD &nBytesRead
		){
			ATLASSUME(m_h != NULL);

			if(!::ReadFile(m_h,pBuffer,nBufSize,&nBytesRead,NULL))
				return AtlHresultFromLastError();

			return S_OK;
		}
		/// this function will usually return HRESULT_FROM_WIN32(ERROR_IO_PENDING)
		/// indicating succesful queueing of the operation
		HRESULT Read(
			__out_bcount(nBufSize) LPVOID pBuffer,
			__in DWORD nBufSize,
			__in_opt LPOVERLAPPED pOverlapped
		){
			ATLASSUME(m_h != NULL);

			if(!::ReadFile(m_h, pBuffer, nBufSize, NULL, pOverlapped))
				return AtlHresultFromLastError();

			return S_OK;
		}

		HRESULT Read(
			__in_bcount(nBufSize) LPVOID pBuffer,
			__in DWORD nBufSize,
			__in_opt LPOVERLAPPED pOverlapped,
			__in LPOVERLAPPED_COMPLETION_ROUTINE pfnCompletionRoutine
		){
			ATLASSUME(m_h != NULL);

			if(!::ReadFileEx(m_h, pBuffer, nBufSize, pOverlapped, pfnCompletionRoutine))
				return AtlHresultFromLastError();

			return S_OK;
		}

		//-----------------------------------------------------------------
		//		Write
		//-----------------------------------------------------------------
		HRESULT Write(
			__in_bcount(nBufSize) LPCVOID pBuffer,
			__in DWORD nBufSize
		){
			return this->Write(pBuffer,nBufSize,(DWORD*)NULL);
		}
		HRESULT Write(
			__in_bcount(nBufSize) LPCVOID pBuffer,
			__in DWORD nBufSize,
			__out_opt DWORD* pnBytesWritten// = NULL
		){
			ATLASSUME(m_h != NULL);

			DWORD nBytesWritten;
			if (pnBytesWritten == NULL)
				pnBytesWritten = &nBytesWritten;

			if(!::WriteFile(m_h, pBuffer, nBufSize, pnBytesWritten, NULL))
				return AtlHresultFromLastError();

			return S_OK;
		}
		/// this function will usually return HRESULT_FROM_WIN32(ERROR_IO_PENDING)
		/// indicating succesful queueing of the operation
		HRESULT Write(
			__in_bcount(nBufSize) LPCVOID pBuffer,
			__in DWORD nBufSize,
			__in_opt LPOVERLAPPED pOverlapped
		){
			ATLASSUME(m_h != NULL);

			if (!::WriteFile(m_h, pBuffer, nBufSize, NULL, pOverlapped))
				return AtlHresultFromLastError();

			return S_OK;
		}
		HRESULT Write(
			__in_bcount(nBufSize) LPCVOID pBuffer,
			__in DWORD nBufSize,
			__in_opt LPOVERLAPPED pOverlapped,
			__in LPOVERLAPPED_COMPLETION_ROUTINE pfnCompletionRoutine
		){
			ATLASSUME(m_h != NULL);

			if (!::WriteFileEx(m_h, pBuffer, nBufSize, pOverlapped, pfnCompletionRoutine))
				return AtlHresultFromLastError();

			return S_OK;
		}

		//-----------------------------------------------------------------
		//		‘¼
		//-----------------------------------------------------------------
		/// this function returns HRESULT_FROM_WIN32(ERROR_IO_INCOMPLETE)
		/// if bWait is false and the operation is still pending
		HRESULT GetOverlappedResult(
			__in LPOVERLAPPED pOverlapped,
			__out DWORD& dwBytesTransferred,
			__in BOOL bWait
		){
			if (!::GetOverlappedResult(m_h, pOverlapped, &dwBytesTransferred, bWait))
				return AtlHresultFromLastError();

			return S_OK;
		}

		HRESULT Seek(__in LONGLONG nOffset){
			return this->Seek(nOffset,FILE_CURRENT);
		}
		HRESULT Seek(__in LONGLONG nOffset, __in DWORD dwFrom/* = FILE_CURRENT*/){
			ATLASSUME(m_h != NULL);
			ATLASSERT(dwFrom == FILE_BEGIN || dwFrom == FILE_END || dwFrom == FILE_CURRENT);

			LARGE_INTEGER liOffset;
			liOffset.QuadPart = nOffset;
			DWORD nNewPos = ::SetFilePointer(m_h, liOffset.LowPart, &liOffset.HighPart, dwFrom);
			if(nNewPos==INVALID_SET_FILE_POINTER){
				HRESULT hr=AtlHresultFromLastError();
				if(FAILED(hr))return hr;
			}

			return S_OK;
		}

		HRESULT GetPosition(__out ULONGLONG& nPos){
			ATLASSUME(m_h != NULL);

			LARGE_INTEGER liOffset;
			liOffset.QuadPart = 0;
			liOffset.LowPart = ::SetFilePointer(m_h, 0, &liOffset.HighPart, FILE_CURRENT);
			if(liOffset.LowPart == INVALID_SET_FILE_POINTER){
				HRESULT hr= AtlHresultFromLastError();
				if(FAILED(hr))return hr;
			}
			nPos = liOffset.QuadPart;

			return S_OK;
		}

		HRESULT Flush(){
			ATLASSUME(m_h != NULL);

			if (!::FlushFileBuffers(m_h))
				return AtlHresultFromLastError();

			return S_OK;
		}

		HRESULT LockRange(__in ULONGLONG nPos, __in ULONGLONG nCount){
			ATLASSUME(m_h != NULL);

			LARGE_INTEGER liPos;
			liPos.QuadPart = nPos;

			LARGE_INTEGER liCount;
			liCount.QuadPart = nCount;

			if (!::LockFile(m_h, liPos.LowPart, liPos.HighPart, liCount.LowPart, liCount.HighPart))
				return AtlHresultFromLastError();

			return S_OK;
		}

		HRESULT UnlockRange(__in ULONGLONG nPos, __in ULONGLONG nCount){
			ATLASSUME(m_h != NULL);

			LARGE_INTEGER liPos;
			liPos.QuadPart = nPos;

			LARGE_INTEGER liCount;
			liCount.QuadPart = nCount;

			if (!::UnlockFile(m_h, liPos.LowPart, liPos.HighPart, liCount.LowPart, liCount.HighPart))
				return AtlHresultFromLastError();

			return S_OK;
		}

		HRESULT SetSize(__in ULONGLONG nNewLen){
			ATLASSUME(m_h != NULL);

			HRESULT hr = Seek(nNewLen, FILE_BEGIN);
			if (FAILED(hr))
				return hr;

			if (!::SetEndOfFile(m_h))
				return AtlHresultFromLastError();

			return S_OK;
		}

		HRESULT GetSize(__out ULONGLONG& nLen){
			ATLASSUME(m_h != NULL);

			ULARGE_INTEGER liFileSize;
			liFileSize.LowPart = ::GetFileSize(m_h, &liFileSize.HighPart);
			if (liFileSize.LowPart == INVALID_FILE_SIZE){
				HRESULT hr;

				hr = AtlHresultFromLastError();
				if (FAILED(hr))
					return hr;
			}

			nLen = liFileSize.QuadPart;

			return S_OK;
		}
	};
//-----------------------------------------------
	// Checked <atlchecked.h> ‚ð®—‚µ‚Ä‚©‚ç
#if FALSE
	/// This class allows the creation of a temporary file that is written to.
	/// When the entire file has been successfully written it will be closed and given
	/// it's proper file name if required.
	public ref class CAtlTemporaryFile{
	public:
		CAtlTemporaryFile(){}

		~CAtlTemporaryFile(){
			// Ensure that the temporary file is closed and deleted,
			// if necessary.
			if(m_file->m_h != NULL){
				this->Close();
			}
		}

		HRESULT Create(){
			return Create(NULL,GENERIC_WRITE);
		}
		HRESULT Create(__in_opt LPCTSTR pszDir){
			return Create(pszDir,GENERIC_WRITE);
		}
		HRESULT Create(__in_opt LPCTSTR pszDir/* = NULL*/, __in DWORD dwDesiredAccess/* = GENERIC_WRITE*/){
			TCHAR szPath[_MAX_PATH]; 
			TCHAR tmpFileName[_MAX_PATH]; 

			ATLASSUME(m_file->m_h==NULL);

			if (pszDir==NULL){
				DWORD dwRet = GetTempPath(_MAX_DIR, szPath);
				if(dwRet == 0){
					// Couldn't find temporary path;
					return AtlHresultFromLastError();
				}else if (dwRet > _MAX_DIR){
					return DISP_E_BUFFERTOOSMALL;
				}
			}else{
				if(Checked::tcsncpy_s(szPath, _countof(szPath), pszDir, _TRUNCATE)==STRUNCATE){
					return DISP_E_BUFFERTOOSMALL;
				}
			}

			if(!GetTempFileName(szPath, _T("TFR"), 0, tmpFileName)){
				// Couldn't create temporary filename;
				return AtlHresultFromLastError();
			}
			tmpFileName[_countof(tmpFileName)-1]='\0';

			Checked::tcsncpy_s(m_szTempFileName,_countof(m_szTempFileName),tmpFileName,_TRUNCATE);
			SECURITY_ATTRIBUTES secatt;
			secatt.nLength = sizeof(secatt);
			secatt.lpSecurityDescriptor = NULL;
			secatt.bInheritHandle = TRUE;

			m_dwAccess = dwDesiredAccess;

			return m_file->Create(
				m_szTempFileName,
				m_dwAccess,
				0,
				CREATE_ALWAYS,
				FILE_ATTRIBUTE_NOT_CONTENT_INDEXED|FILE_ATTRIBUTE_TEMPORARY,
				&secatt
				);
		}
		HRESULT Close(){
			this->Close(NULL);
		}
		HRESULT Close(__in_opt LPCTSTR szNewName/* = NULL*/){
			ATLASSUME(m_file.m_h != NULL);

			// This routine is called when we are finished writing to the 
			// temporary file, so we now just want to close it and copy
			// it to the actual filename we want it to be called.

			// So let's close it first.
			m_file->Close();

			// no new name so delete it
			if(szNewName==NULL){
				::DeleteFile(m_szTempFileName);
				return S_OK;
			}

			// delete any existing file and move our temp file into it's place
			if(!::DeleteFile(szNewName)){
				DWORD dwError = GetLastError();
				if(dwError != ERROR_FILE_NOT_FOUND)
					return AtlHresultFromWin32(dwError);
			}

			if(!::MoveFile(m_szTempFileName, szNewName))
				return AtlHresultFromLastError();

			return S_OK;
		}

		HRESULT HandsOff(){
			m_file->Flush();
			m_file->Close();

			return S_OK;
		}

		HRESULT HandsOn(){
			HRESULT hr = m_file->Create(
				m_szTempFileName,
				m_dwAccess,
				0,
				OPEN_EXISTING);
			if(FAILED(hr))return hr;

			return m_file->Seek(0, FILE_END);
		}

		HRESULT Read(
			__out_bcount(nBufSize) LPVOID pBuffer,
			__in DWORD nBufSize,
			__out DWORD& nBytesRead
		){
			return m_file->Read(pBuffer, nBufSize, nBytesRead);
		}

		HRESULT Write(
			__in_bcount(nBufSize) LPCVOID pBuffer,
			__in DWORD nBufSize
		){
			return m_file->Write(pBuffer, nBufSize, NULL);
		}
		HRESULT Write(
			__in_bcount(nBufSize) LPCVOID pBuffer,
			__in DWORD nBufSize,
			__out_opt DWORD* pnBytesWritten/* = NULL*/
		){
			return m_file->Write(pBuffer, nBufSize, pnBytesWritten);
		}

		HRESULT Seek(__in LONGLONG nOffset){
			return m_file->Seek(nOffset, FILE_CURRENT);
		}
		HRESULT Seek(__in LONGLONG nOffset, __in DWORD dwFrom/* = FILE_CURRENT*/){
			return m_file->Seek(nOffset, dwFrom);
		}

		HRESULT GetPosition(__out ULONGLONG& nPos){
			return m_file->GetPosition(nPos);
		}

		HRESULT Flush(){
			return m_file->Flush();
		}

		HRESULT LockRange(__in ULONGLONG nPos, __in ULONGLONG nCount){
			return m_file->LockRange(nPos, nCount);
		}

		HRESULT UnlockRange(__in ULONGLONG nPos, __in ULONGLONG nCount){
			return m_file->UnlockRange(nPos, nCount);
		}

		HRESULT SetSize(__in ULONGLONG nNewLen){
			return m_file->SetSize(nNewLen);
		}

		HRESULT GetSize(__out ULONGLONG& nLen){
			return m_file->GetSize(nLen);
		}

		operator HANDLE(){
			return m_file;
		}

		LPCTSTR TempFileName(){
			return m_szTempFileName;
		}

	private:
		CAtlFile^ m_file;

		[System::Runtime::InteropServices::StructLayout(System::Runtime::InteropServices::LayoutKind::Sequential,Size=30)]
		[System::Runtime::CompilerServices::UnsafeValueType]
		value struct TCHARArray{};

		[System::Runtime::CompilerServices::FixedBufferAttribute(TCHAR::typeid,_MAX_FNAME+1)]
		TCHARArray m_szTempFileName;

		DWORD m_dwAccess;

	};
#endif

//-----------------------------------------------
// end of namespace mwg::ATL
}
}
//-----------------------------------------------
