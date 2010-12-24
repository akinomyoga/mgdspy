#include "stdafx.h"
#include "Channel.h"

using namespace mwg::Interop;

LRESULT __stdcall HookPoint(int nCode,WPARAM wParam,LPARAM lParam){
	static HHOOK hhook=NULL;

	CWPSTRUCT cwp=*(CWPSTRUCT*)lParam;
	if(cwp.message==WM_Channel){
		DWORD sv_procid=(DWORD)cwp.lParam;			// e‚Ì process-id
		DWORD cl_procid=::GetCurrentProcessId();	// Ž©•ª‚Ì process-id

		msclr::lock l(global::receivers);
		if(sv_procid!=NULL&&!global::receivers->ContainsKey(sv_procid)){
			try{
				global::receivers->Add(sv_procid,gcnew Receiver(sv_procid,cl_procid));
			}catch(System::Exception^ e){
				Frms::MessageBox::Show("Receiver ‚Ì¶¬‚ÉŽ¸”s‚µ‚Ü‚µ‚½...\r\n\r\n"+e->ToString());
			}catch(...){}

			if(cwp.wParam!=NULL)
				hhook=(HHOOK)cwp.wParam;
		}
	}
	
	return ::CallNextHookEx(hhook,nCode,wParam,lParam);
}
