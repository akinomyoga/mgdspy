#pragma once
#include "stdafx.h"

using namespace System::Windows::Forms;
//-------------------------------------------------------------------
// namespace mwg::Interop
namespace mwg{
namespace Interop{
	ref class Channel;
	ref class Receiver;
//-------------------------------------------------------------------

	ref class ProcessLocals sealed abstract{
		static int next_key=1;
    static initonly Gen::Dictionary<int,cl::object^>^ vals
      =gcnew Gen::Dictionary<int,cl::object^>();
    static initonly Gen::Dictionary<cl::object^,int>^ keys
      =gcnew Gen::Dictionary<cl::object^,int>();
	public:
		generic<typename T>
		static T GetObject(int key){
      cl::object^ ret;
			if(vals->TryGetValue(key,ret)){
				return safe_cast<T>(ret);
			}else{
				throw gcnew System::Exception("Žw’è‚µ‚½ key ‚ðŽ‚Â•Ï”‚Í“o˜^‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB");
				//return nullptr;
			}
		}
		static cl::object^ GetObject(int key){
			cl::object^ ret;
			if(vals->TryGetValue(key,ret)){
				return ret;
			}else{
				throw gcnew System::Exception("Žw’è‚µ‚½ key ‚ðŽ‚Â•Ï”‚Í“o˜^‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB");
				//return nullptr;
			}
		}
    static int SetObject(cl::object^ value){
			int key;
			if(keys->TryGetValue(value,key)){
				return key;
			}else{
				key=next_key++;
				vals->Add(key,value);
				keys->Add(value,key);
				return key;
			}
		}
	};

	generic<typename T>
	[System::Serializable]
	public value class ProcessLocal{
		int key;
	public:
		property bool IsEmpty{
			bool get(){
				return this->key==0;
			}
		}
		static operator T(ProcessLocal<T> value){
			return ProcessLocals::GetObject<T>(value.key);
		}
		static operator ProcessLocal<T> (T value){
			ProcessLocal<T> ret;
			ret.key=ProcessLocals::SetObject(value);
			return ret;
		}
	};
//-------------------------------------------------------------------
// end of namespace mwg::Interop
}
}
//-------------------------------------------------------------------
