#pragma once
#pragma managed(push,off)
#include <windows.h>
#include <tchar.h>
#include <sal.h>
#pragma managed(pop)

//-----------------------------------------------
// namespace mwg::Win32
namespace mwg{
namespace Win32{
//-----------------------------------------------
	[System::Serializable]
	public ref class HresultException:public System::Exception{
		HRESULT hr;
	public:
		HresultException(HRESULT hr):System::Exception(),hr(hr){}
	};
	public value struct ULARGE_INTEGER{
		ULONGLONG value;
		property ULONGLONG QuadPart{
			ULONGLONG get(){return this->value;}
			void set(ULONGLONG value){this->value=value;}
		}
		property DWORD LowPart{
			DWORD get(){
				ULONGLONG v=this->value;
				return reinterpret_cast<DWORD*>(&v)[0];
			}
			void set(DWORD value){
				this->value=this->value&HIGHPART|(ULONGLONG)value;
			}
		}
		property DWORD HighPart{
			DWORD get(){
				ULONGLONG v=this->value;
				return reinterpret_cast<DWORD*>(&v)[1];
			}
			void set(DWORD value){
				this->value=this->value&LOWPART|(ULONGLONG)value<<DWORD_WIDTH;
			}
		}
		ULARGE_INTEGER(::ULARGE_INTEGER val){
			this->value=val.QuadPart;
		}
		operator ::ULARGE_INTEGER(){
			ULONGLONG v=this->value;
			return *reinterpret_cast<::ULARGE_INTEGER*>(&v);
		}
	private:
		literal int DWORD_WIDTH=8*sizeof(DWORD);
		literal ULONGLONG LOWPART=(ULONGLONG)((DWORD)(-1));
		literal ULONGLONG HIGHPART=(ULONGLONG)((DWORD)(-1))<<DWORD_WIDTH;
	};
//-----------------------------------------------
// end of namespace mwg::Win32
}
}
//-----------------------------------------------

//-----------------------------------------------
// namespace mwg::ATL
namespace mwg{
namespace ATL{
//-----------------------------------------------
	inline HRESULT AtlHresultFromLastError() throw(){
		DWORD dwErr = ::GetLastError();
		return HRESULT_FROM_WIN32(dwErr);
	}
	inline void AtlThrow(HRESULT hr){
		if(FAILED(hr))throw gcnew mwg::Win32::HresultException(hr);
	}
//-----------------------------------------------
// end of namespace mwg::ATL
}
}
//-----------------------------------------------
