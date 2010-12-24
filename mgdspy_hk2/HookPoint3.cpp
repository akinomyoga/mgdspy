#include "stdafx.h"

extern UINT WM_Channel;

namespace Rem=System::Runtime::Remoting;

ref class IpcKicker abstract sealed{
  static mwg::Remote::ChannelStation^ station;
public:
  [System::Runtime::CompilerServices::MethodImpl(System::Runtime::CompilerServices::MethodImplOptions::NoInlining)]
  static void Test(){
    System::IO::MemoryStream memstr;
    mwg::Remote::UnsafeSerializer::Serialize(%memstr,1);
  }
  [System::Runtime::CompilerServices::MethodImpl(System::Runtime::CompilerServices::MethodImplOptions::NoInlining)]
  static void CreateIpcHub(DWORD sv_procid,int sv_sessid){
    try{
      station=gcnew mwg::Remote::ChannelStation(
        mwg::Remote::IpcManager::OpenSessionCl((int)sv_procid,sv_sessid)
        );
    }catch(System::Exception^ e){
      Frms::MessageBox::Show(e->ToString());
    }
  }
};

ref class AssemblyLoader{
  static cl::string^ pathMwgRemoteDll=nullptr;
  static Ref::Assembly^ ResolveAssembly(cl::object^ sender,System::ResolveEventArgs^ e){
    cl::string^ name=e->Name->ToLower();

    if(name->StartsWith("mwg.remote, ")){
      return Ref::Assembly::LoadFrom(pathMwgRemoteDll);
    }
    return nullptr;
  }
public:
  static void Load(DWORD parentProcessId){
    if(pathMwgRemoteDll!=nullptr)return;

    Diag::Process^ process=Diag::Process::GetProcessById(parentProcessId);

    try{
      for each(Diag::ProcessModule^ module in process->Modules){
        try{
          cl::string^ name=module->ModuleName->ToLower();
          if(name!="mwg.remote.dll")continue;

          // is .NET Assembly?
          Ref::AssemblyName^ asmname=Ref::AssemblyName::GetAssemblyName(module->FileName);
          if(asmname==nullptr)continue;

          pathMwgRemoteDll=module->FileName;
        }catch(System::Exception^){}
      }
    }catch(System::Exception^){}

    System::AppDomain::CurrentDomain->AssemblyResolve
      +=gcnew System::ResolveEventHandler(&AssemblyLoader::ResolveAssembly);
  }
};

enum WMCC{
  WMCC_NOTIFY_HOOK  =1,
  WMCC_NOTIFY_SVPID =2,
  WMCC_NOTIFY_SVSID =3,
};

LRESULT __stdcall HookPoint3(int nCode,WPARAM wParam,LPARAM lParam){
  static HHOOK hhook=NULL;
  static DWORD sv_procid=0; // êeÇÃ process id
  static DWORD sv_sessid=0; // êeÇÃópà”ÇµÇΩ ipc session id

  static bool sv_sessid_set=false;
  static bool sv_procid_set=false;

  CWPSTRUCT cwp=*(CWPSTRUCT*)lParam;
  if(cwp.message==WM_Channel){
    switch((int)cwp.wParam){
      case WMCC_NOTIFY_HOOK:
        // original hook
        hhook=(HHOOK)cwp.lParam;

        // init (ìÒâÒñ⁄à»ç~ÇÃ Hook ÇÃà◊)
        sv_procid=0;
        sv_sessid=0;
        sv_sessid_set=false;
        sv_procid_set=false;
        break;
      case WMCC_NOTIFY_SVPID:
        sv_procid=(DWORD)cwp.lParam;
        if(sv_procid!=0){
          sv_procid_set=true;
          AssemblyLoader::Load(sv_procid);
        }

        if(sv_procid_set&&sv_sessid_set)
          IpcKicker::CreateIpcHub(sv_procid,sv_sessid);

        //DWORD cl_procid=::GetCurrentProcessId();
        //IpcKicker::Test();
        //Frms::MessageBox::Show("World!");

        break;
      case WMCC_NOTIFY_SVSID:
        sv_sessid=(DWORD)cwp.lParam;
        sv_sessid_set=true;
        if(sv_procid_set&&sv_sessid_set)
          IpcKicker::CreateIpcHub(sv_procid,sv_sessid);
        break;
    }
  }
  
  return ::CallNextHookEx(hhook,nCode,wParam,lParam);
}
