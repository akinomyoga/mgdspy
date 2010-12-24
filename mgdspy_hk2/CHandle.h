#pragma once
#include "stdatl.h"

//
//	from <atlbase.h>
//
//-----------------------------------------------
// namespace mwg::ATL
namespace mwg{
namespace ATL{
//-----------------------------------------------
	public ref class CHandle{
	public:
		HANDLE m_h;
	public:
		CHandle():m_h(NULL){}

		CHandle(CHandle^ h):m_h(NULL){
			this->Attach(h->Detach());
		}

		explicit CHandle(HANDLE h):m_h(h){}

		~CHandle(){
			if(this->m_h!=NULL)this->Close();
		}

		CHandle^ operator=(CHandle^ h){
			if(this!=h){
				this->Close();
				this->Attach(h->Detach());
			}
			return this;
		}

		operator HANDLE(){return this->m_h;}

		// Attach to an existing handle (takes ownership).
		void Attach(HANDLE h){
			ATLASSUME(m_h==NULL);
			this->m_h=h;
		}

		// Detach the handle from the object (releases ownership).
		HANDLE Detach(){
			HANDLE h=m_h;
			this->m_h=NULL;
			return h;
		}

		// Close the handle.
		void Close(){
			if(this->m_h!=NULL){
				::CloseHandle(this->m_h);
				this->m_h=NULL;
			}
		}
	};
//-----------------------------------------------
// end of namespace mwg::ATL
}
}
//-----------------------------------------------
