#pragma once
#include "stdafx.h"
#include "SharedMem.h"
#include "TransTypeSerializer.h"

extern UINT WM_Channel;

using namespace System::Windows::Forms;
//-------------------------------------------------------------------
// namespace mwg::Interop
namespace mwg{
namespace Interop{
	ref class Channel;
	ref class Receiver;
//-------------------------------------------------------------------
	/// <summary>
	/// mwg.Interop.WM_CHANNEL �Ŏg�p����l���w�肵�܂��B
	/// </summary>
	public enum class MessageCode{
		ShowMessageBox,
		CreateReceiver,
		SendObject,
		GetObject,
		DestroyReceiver,
	};

	public interface class IChannelCommand{
		void Execute(Receiver^ receiver);
	};
//-------------------------------------------------------------------
	/// <summary>
	/// Channel �ł̏������ɗp���郁������񋟂��܂��B
	/// </summary>
	ref class ChannelMemory{
		static const unsigned int SHARE_SIZE=0x1000;			// 4 KB
		static const unsigned int ALLOC_SIZE=SHARE_SIZE+100;	// SHARE_SIZE + �w�b�_���̑����X
		cl::string^ name;
		SharedMemory^ shared;

		value struct MemoryMap{
			FOURCC id;
			MessageCode code;
			BYTE data0;
		} *map;

		void* pdata0;

	internal:
		ChannelMemory(DWORD processIdParent,DWORD processIdChild);
		ChannelMemory(cl::string^ name){
			this->Initialize(name);
		}
		~ChannelMemory(){this->!ChannelMemory();}
		!ChannelMemory(){
			delete this->shared;
		}
	private:
		void Initialize(cl::string^ name);
	public:
		property cl::string^ Name{
			cl::string^ get(){return this->name;}
		}

		property MessageCode Code{
			MessageCode get(){return this->map->code;}
			void set(MessageCode value){this->map->code=value;}
		}

		property void* Data0{
			void* get(){return this->pdata0;}
		}

	//===============================================================
	//		������
	//===============================================================
	public:
		property System::IntPtr init_ReceiverHWND{
			System::IntPtr get(){return this->init_pdata->receiverHWND;}
			void set(System::IntPtr value){this->init_pdata->receiverHWND=value;}
		}
		property cl::string^ init_AssemblyLocation{
			cl::string^ get();
			void set(cl::string^ value);
		}
	private:
		value struct init_Data{
			System::IntPtr receiverHWND;
			int szAssemblyName;
			BYTE bAssemblyName;
		};
		property int init_sizeofAssemblyName{
			int get(){return this->init_pdata->szAssemblyName;}
			void set(int value){this->init_pdata->szAssemblyName=value;}
		}
		property init_Data* init_pdata{
			init_Data* get(){return (init_Data*)this->pdata0;}
		}
	//===============================================================
	//		����M
	//===============================================================
	public:
		property bool trans_IsComplete{
			bool get(){return this->send_pdata->isComplete!=0;}
			void set(bool value){this->send_pdata->isComplete=value?1:0;}
		}
		property bool trans_IsInitialize{
			bool get(){return this->send_pdata->isInitialize!=0;}
			void set(bool value){this->send_pdata->isInitialize=value?1:0;}
		}
		property bool trans_IsFailed{
			bool get(){return this->send_pdata->isFailed!=0;}
			void set(bool value){this->send_pdata->isFailed=value?1:0;}
		}
		property DWORD trans_DataSize{
			DWORD get(){return this->send_pdata->dataSize;}
			void set(DWORD value){this->send_pdata->dataSize=value;}
		}
		void trans_ClearState(){
			this->trans_IsInitialize=false;
			this->trans_IsComplete=false;
			this->trans_IsFailed=false;
			this->trans_DataSize=0;
		}
		/// <summary>
		/// input �̓��e�����L�������ɏ������݂܂��B
		/// �������ގ����o�����ʂ� trans_DataSize �ɐݒ肵�܂��B
		/// input �̓��e��S�ď������������ɂ� trans_IsComplete �� true �ɐݒ肵�܂��B
		/// </summary>
		void trans_WriteData(System::IO::Stream^ input){
			const int BUFFLEN=0x400;
			array<BYTE>^ buffer=gcnew array<BYTE>(BUFFLEN);

			BYTE* p0=&this->send_pdata->bData;
			BYTE* pM=p0+SHARE_SIZE;
			BYTE* p=p0;

			pin_ptr<BYTE> psrc0=&buffer[0];
			while(p<pM){
				// Stream ����Ǎ�
				int readlen=System::Math::Min(BUFFLEN,pM-p);
				readlen=input->Read(buffer,0,readlen);

				// ���L�������ɏ���
				BYTE* psrcM=psrc0+readlen;
				BYTE* psrc=psrc0;
				while(psrc<psrcM)*p++=*psrc++;

				// �I���������
				if(input->Position==input->Length){
					this->trans_IsComplete=true;
					break;
				}
			}

			this->trans_DataSize=p-p0;
		}
		/// <summary>
		/// ���L�������̓��e�� output �ɒǉ����܂��B
		/// trans_DataSize �ɐݒ肳��Ă���ʂ����ǂݎ��܂��B
		/// </summary>
		void trans_ReadData(System::IO::Stream^ output){
			const int BUFFLEN=0x400;
			array<BYTE>^ buffer=gcnew array<BYTE>(BUFFLEN);

			BYTE* pdata0=&this->send_pdata->bData;
			BYTE* pdataM=pdata0+this->trans_DataSize;
			BYTE* pdata=pdata0;

			pin_ptr<BYTE> pbuff0=&buffer[0];
			BYTE* pbuffM=pbuff0+BUFFLEN;

			while(true){
				BYTE* pbuff=pbuff0;
				while(pdata<pdataM&&pbuff<pbuffM)*pbuff++=*pdata++;
				output->Write(buffer,0,(int)(pbuff-pbuff0));

				// �S�ēǂݐ؂��Ă���ꍇ
				if(pdata>=pdataM)break;
			}
		}
	private:
		value struct send_Data{
			BYTE isInitialize;
			BYTE isComplete;
			BYTE isFailed;
			DWORD dataSize;
			BYTE bData;
		};
		property send_Data* send_pdata{
			send_Data* get(){return (send_Data*)this->pdata0;}
		}
	};

//******************************************************************************
//  class Channel
//------------------------------------------------------------------------------
	/// <summary>
	/// ���̃v���Z�X�Ƃ̒ʐM���s���ׂ̒ʘH��񋟂��܂��B
	/// </summary>
	public ref class Channel{

		value struct ChannelInfo{
			ChannelMemory^ memory;
			HWND receiver;
		};

		DWORD procidSrc;
		DWORD procidDst;
		System::String^ name;
		ChannelMemory^ memory;
		HWND receiver;

	//===============================================================
	//		"HookPoint" �̏���
	//===============================================================
	private:
		static HMODULE hDll;
		static HOOKPROC hProc;
		static Channel(){
			// Hook ����֐��̏���
			hDll=::LoadLibrary(_T("mgdspy_hk.dll"));
			if(hDll!=NULL){
				hProc=(HOOKPROC)::GetProcAddress(hDll,"HookPoint");
			}
		}
	public:
		static void FinalizeChannel(){
			::FreeLibrary(hDll);
		}
		~Channel(){this->!Channel();}
		!Channel(){
			if(this->memory!=nullptr){
				try{
					this->SendChannelMessage(MessageCode::DestroyReceiver);
				}catch(...){}
				delete this->memory;
			}
		}
	//===============================================================
	//		������
	//===============================================================
	public:
		/// <summary>
		/// �w�肵�� Window Handle ���ȂđΏۂ̃v���Z�X�Ƃ̊Ԃ� Channel ���m�����܂��B
		/// </summary>
		Channel(System::IntPtr hWnd){
			this->Initialize((HWND)(void*)hWnd);
		}
		/// <summary>
		/// �w�肵�� Window Handle ���ȂđΏۂ̃v���Z�X�Ƃ̊Ԃ� Channel ���m�����܂��B
		/// </summary>
		Channel(HWND hWnd){
			this->Initialize(hWnd);
		}
	private:
		/// <summary>
		/// �Ώۂ̃v���Z�X�ɑ΂��Ċ��� ChannelMemory �� Receiver �𐶐����Ă����ꍇ�ɁA
		/// ����𗬗p����ׂ̃X�g���[�W�ł��B
		/// </summary>
		static Gen::Dictionary<System::String^,ChannelInfo>^ dic_info=gcnew Gen::Dictionary<System::String^,ChannelInfo>();
		/// <summary>
		/// �w�肵�� Window Handle ���g�p���ď��������s���܂��B
		/// </summary>
		void Initialize(HWND hWnd);
		/// <summary>
		/// �Ώۂ̃v���Z�X�� Receiver �C���X�^���X�𐶐����܂��B
		/// ���̌�ɂ��� HWND ���擾���܂��B
		/// </summary>
		/// <param name="hWnd">hook ��� Window ���w�肵�܂��B</param>
		/// <param name="threadId">hWnd �̑�����X���b�h���w�肵�܂��B</param>
		/// <returns>������ Receiver �𐶐����鎖���o�����ꍇ�� true ��Ԃ��܂��B
		/// ���s�����ꍇ�ɂ� false ��Ԃ��܂��B</returns>
		bool CreateReceiver(HWND hWnd,DWORD threadId);
	//===============================================================
	//		�I�u�W�F�N�g����M
	//===============================================================
	private:
		void SendChannelMessage(MessageCode code){
			this->memory->Code=code;
			LRESULT result=::SendMessage(this->receiver,WM_Channel,NULL,NULL);
			if(result!=WM_Channel)
				throw gcnew System::Exception("Channel Message ����M����܂���ł����B");
		}
		void PostChannelMessage(MessageCode code){
			this->memory->Code=code;
			::PostMessage(this->receiver,WM_Channel,NULL,NULL);
		}
	public:
		void ShowMessageBox(){
			this->PostChannelMessage(MessageCode::ShowMessageBox);
		}
    /// <summary>
    /// �w�肵���I�u�W�F�N�g�𑗐M���܂��B
    /// </summary>
		void SendObject(cl::object^ obj){
			System::IO::Stream^ mstr=gcnew System::IO::MemoryStream();
			TransTypeSerializer::Serialize(mstr,obj);
			mstr->Position=0;

			msclr::lock l(this->memory);
			this->memory->trans_ClearState();
			this->memory->trans_IsInitialize=true;
			while(true){
				this->memory->trans_WriteData(mstr);

				if(this->memory->trans_IsComplete){
					this->SendChannelMessage(MessageCode::SendObject);
					mstr->Close();
					break;
				}else{
					this->SendChannelMessage(MessageCode::SendObject);
					this->memory->trans_ClearState();
				}
			}
		}
    /// <summary>
    /// �����_�Ō��������p�ӂ��Ă���I�u�W�F�N�g���擾���܂��B
    /// </summary>
		cl::object^ GetObject(){
			System::IO::Stream^ mstr=gcnew System::IO::MemoryStream();

			{
				msclr::lock l(this->memory);
				this->memory->trans_ClearState();
				this->memory->trans_IsInitialize=true;
				while(true){
					this->SendChannelMessage(MessageCode::GetObject);
					if(this->memory->trans_IsFailed)
						throw gcnew System::Exception("�I�u�W�F�N�g�̎�M�Ɏ��s���܂���");

					this->memory->trans_ReadData(mstr);

					if(this->memory->trans_IsComplete){
						// �ǂݎ�芮���������āAStream ���������
						this->SendChannelMessage(MessageCode::GetObject);
						break;
					}else{
						this->memory->trans_ClearState();
					}
				}
			}

			mstr->Position=0;
			return TransTypeSerializer::Deserialize(mstr);

//			if(ret!=nullptr){
//				System::Type^ t=dynamic_cast<System::Type^>(ret);
//				try{
//					if(t!=nullptr)t->ToString();
//				}catch(...){
//					return nullptr;
//				}
//			}
//			return ret;
		}
	//===============================================================
	//		����
	//===============================================================
	public:
		void LoadAssembly(System::Reflection::Assembly^ assem){
			this->SendObject(cmdLoadAssembly(assem->Location));
		}
	internal:
		[System::Serializable]
		value class cmdLoadAssembly:IChannelCommand{
			cl::string^ assembly_path;
		public:
			cmdLoadAssembly(cl::string^ path){
				this->assembly_path=path;
			}
			virtual void Execute(Receiver^ receiver){
				TransTypeSerializer::AssemblyStore::GetAssemblyFromLocation(this->assembly_path);
			}
			virtual cl::string^ ToString() override{
				return "mwg.Interop.Channel.cmdLoadAssembly{path = "+this->assembly_path+"}";
			}
		};
	//===============================================================
	//		����: �֐��̎��s
	//===============================================================
	public:
		cl::object^ InvokeMethod(cl::object^ instance,Ref::MethodInfo^ method,array<cl::object^>^ args){
			cl::object^ ret;

			{
				msclr::lock l(this->memory);

				this->SendObject(cmdInvoke(instance,method,args));
				ret=this->GetObject();
			}

			cmdInvoke_Exception^ e=dynamic_cast<cmdInvoke_Exception^>(ret);
			if(e!=nullptr)throw e->exception;
			return ret;
		}
		cl::object^ InvokeMethod(System::Delegate^ deleg,array<cl::object^>^ args){
			if(cmdAnonymousInvoke_Holder::IsAnonymousMethod(deleg)){
				cl::object^ ret;

				{
					msclr::lock l(this->memory);

					this->SendObject(cmdAnonymousInvoke(deleg,args));
					ret=this->GetObject();
				}

				cmdInvoke_Exception^ e0=dynamic_cast<cmdInvoke_Exception^>(ret);
				if(e0!=nullptr)throw e0->exception;
				//System::Exception^ e=dynamic_cast<System::Exception^>(ret);
				//if(e!=nullptr)throw e;

				cmdAnonymousInvoke_Return ret2=safe_cast<cmdAnonymousInvoke_Return>(ret);
				ret2.instance.ApplyFields(deleg->Target);
				return ret2.retval;
			}else{
				return this->InvokeMethod(deleg->Target,deleg->Method,args);
			}
		}

#pragma region InvokeDelegateV/O/R // delegate �̐錾���ȗ�����ׂ̊֐�
		delegate void DelegateVV();
		void InvokeDelegateV(DelegateVV^ deleg){
			this->InvokeMethod(deleg,gcnew array<cl::object^>{});
		}

		generic<typename A1>
		delegate void DelegateVA1(A1 arg1);
		generic<typename A1>
		void InvokeDelegateV(DelegateVA1<A1>^ deleg,A1 arg1){
			this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1});
		}

		generic<typename A1,typename A2>
		delegate void DelegateVA2(A1 arg1,A2 arg2);
		generic<typename A1,typename A2>
		void InvokeDelegateV(DelegateVA2<A1,A2>^ deleg,A1 arg1,A2 arg2){
			this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1,arg2});
		}

		generic<typename A1,typename A2,typename A3>
		delegate void DelegateVA3(A1 arg1,A2 arg2,A3 arg3);
		generic<typename A1,typename A2,typename A3>
		void InvokeDelegateV(DelegateVA3<A1,A2,A3>^ deleg,A1 arg1,A2 arg2,A3 arg3){
			this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1,arg2,arg3});
		}

		generic<typename A1,typename A2,typename A3,typename A4>
		delegate void DelegateVA4(A1 arg1,A2 arg2,A3 arg3,A4 arg4);
		generic<typename A1,typename A2,typename A3,typename A4>
		void InvokeDelegateV(DelegateVA4<A1,A2,A3,A4>^ deleg,A1 arg1,A2 arg2,A3 arg3,A4 arg4){
			this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1,arg2,arg3,arg4});
		}

		delegate cl::object^ DelegateOV();
		cl::object^ InvokeDelegateO(DelegateOV^ deleg){
			return this->InvokeMethod(deleg,gcnew array<cl::object^>{});
		}

		generic<typename A1>
		delegate cl::object^ DelegateOA1(A1 arg1);
		generic<typename A1>
		cl::object^ InvokeDelegateO(DelegateOA1<A1>^ deleg,A1 arg1){
			return this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1});
		}

		generic<typename A1,typename A2>
		delegate cl::object^ DelegateOA2(A1 arg1,A2 arg2);
		generic<typename A1,typename A2>
		cl::object^ InvokeDelegateO(DelegateOA2<A1,A2>^ deleg,A1 arg1,A2 arg2){
			return this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1,arg2});
		}

		generic<typename A1,typename A2,typename A3>
		delegate cl::object^ DelegateOA3(A1 arg1,A2 arg2,A3 arg3);
		generic<typename A1,typename A2,typename A3>
		cl::object^ InvokeDelegateO(DelegateOA3<A1,A2,A3>^ deleg,A1 arg1,A2 arg2,A3 arg3){
			return this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1,arg2,arg3});
		}

		generic<typename A1,typename A2,typename A3,typename A4>
		delegate cl::object^ DelegateOA4(A1 arg1,A2 arg2,A3 arg3,A4 arg4);
		generic<typename A1,typename A2,typename A3,typename A4>
		cl::object^ InvokeDelegateO(DelegateOA4<A1,A2,A3,A4>^ deleg,A1 arg1,A2 arg2,A3 arg3,A4 arg4){
			return this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1,arg2,arg3,arg4});
		}

		generic<typename R>
		delegate R DelegateRV();
		generic<typename R>
		R InvokeDelegateR(DelegateRV<R>^ deleg){
			return (R)this->InvokeMethod(deleg,gcnew array<cl::object^>{});
		}

		generic<typename R,typename A1>
		delegate R DelegateRA1(A1 arg1);
		generic<typename R,typename A1>
		R InvokeDelegateR(DelegateRA1<R,A1>^ deleg,A1 arg1){
			return (R)this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1});
		}

		generic<typename R,typename A1,typename A2>
		delegate R DelegateRA2(A1 arg1,A2 arg2);
		generic<typename R,typename A1,typename A2>
		R InvokeDelegateR(DelegateRA2<R,A1,A2>^ deleg,A1 arg1,A2 arg2){
			return (R)this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1,arg2});
		}

		generic<typename R,typename A1,typename A2,typename A3>
		delegate R DelegateRA3(A1 arg1,A2 arg2,A3 arg3);
		generic<typename R,typename A1,typename A2,typename A3>
		R InvokeDelegateR(DelegateRA3<R,A1,A2,A3>^ deleg,A1 arg1,A2 arg2,A3 arg3){
			return (R)this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1,arg2,arg3});
		}

		generic<typename R,typename A1,typename A2,typename A3,typename A4>
		delegate R DelegateRA4(A1 arg1,A2 arg2,A3 arg3,A4 arg4);
		generic<typename R,typename A1,typename A2,typename A3,typename A4>
		R InvokeDelegateR(DelegateRA4<R,A1,A2,A3,A4>^ deleg,A1 arg1,A2 arg2,A3 arg3,A4 arg4){
			return (R)this->InvokeMethod(deleg,gcnew array<cl::object^>{arg1,arg2,arg3,arg4});
		}
#pragma endregion

	internal:
		[System::Serializable]
		value class cmdInvoke:IChannelCommand{
			cl::object^ instance;
			Ref::MethodInfo^ minfo;
			array<cl::object^>^ params;
		public:
			cmdInvoke(cl::object^ instance,Ref::MethodInfo^ minfo,array<cl::object^>^ params){
				this->instance=instance;
				this->minfo=minfo;
				this->params=params;
			}
			virtual void Execute(Receiver^ receiver);
		};

		[System::Serializable]
		value struct cmdInvoke_Exception{
			System::Exception^ exception;
			cmdInvoke_Exception(System::Exception^ e){
				this->exception=e;
			}
		};

		[System::Serializable]
		value class cmdAnonymousInvoke_Holder{
		private:
			static const Ref::BindingFlags BF=Ref::BindingFlags::Instance|Ref::BindingFlags::Public|Ref::BindingFlags::NonPublic;
			static initonly array<cl::type^>^ ctor_params=gcnew array<cl::type^>(0);
			static initonly array<cl::object^>^ ctor_args=gcnew array<cl::object^>(0);

		public:
			cl::type^ type;
			Gen::Dictionary<cl::string^,cl::object^>^ fields;

			cmdAnonymousInvoke_Holder(System::Delegate^ deleg){
				this->Initialize(deleg->Target);
			}
			cmdAnonymousInvoke_Holder(cl::object^ target){
				this->Initialize(target);
			}
		private:
			void Initialize(cl::object^ target){
				this->type=target->GetType();
				this->fields=gcnew Gen::Dictionary<cl::string^,cl::object^>();
				for each(Ref::FieldInfo^ finfo in this->type->GetFields(BF)){
					cl::object^ obj=finfo->GetValue(target);
					
					// ����q�ɂ��Ή� (����)
					if(IsAnonymousObject(obj))
						obj=cmdAnonymousInvoke_Holder(obj);

					this->fields[finfo->Name]=obj;
				}
			}
		public:
			cl::object^ GetRealObject(){
				Ref::ConstructorInfo^ ctor=type->GetConstructor(BF,nullptr,ctor_params,nullptr);
				if(ctor==nullptr)
					throw gcnew System::Runtime::Serialization::SerializationException(
						"AnonymousMethodHolder.GetRealObject: ����̃R���X�g���N�^���Ȃ��̂Ō��ɖ߂��܂���B"
						);

				cl::object^ ret=ctor->Invoke(ctor_args);
				this->ApplyFields(ret);
				return ret;
			}

			void ApplyFields(cl::object^ target){
				for each(Ref::FieldInfo^ finfo in type->GetFields(BF)){
					cl::object^ obj=this->fields[finfo->Name];

					// ����q�ɂ��Ή� (����)
					cmdAnonymousInvoke_Holder^ holder=dynamic_cast<cmdAnonymousInvoke_Holder^>(obj);
					if(holder!=nullptr){
//						Frms::MessageBox::Show("����q�Ȃ̂ŕ������܂��B");
						obj=holder->GetRealObject();
					}
					
//					if(obj->GetType()==cmdAnonymousInvoke_Holder::typeid){
//						Frms::MessageBox::Show("�����A����q�Ȃ̂ŕ������܂��B");
//						obj=safe_cast<cmdAnonymousInvoke_Holder>(obj).GetRealObject();
//					}
					
//					try{
						finfo->SetValue(target,obj);
//					}catch(System::Exception^ e){
//						Frms::MessageBox::Show("������ɃG���[������\r\nType: "+finfo->FieldType->ToString()+"\r\nValue: \r\n"+obj->ToString());
//					}
				}
			}

		private:
			static bool IsAnonymousObject(cl::object^ graph){
				return graph!=nullptr&&graph->GetType()->Name->Contains("<>");
			}
		public:
			static bool IsAnonymousMethod(System::Delegate^ deleg){
				return IsAnonymousObject(deleg->Target);
			}

			virtual cl::string^ ToString() override{
				System::Text::StringBuilder^ build=gcnew System::Text::StringBuilder();
				build->Append("{<instance type>\r\n");
				build->Append(this->type);
				build->Append("\r\n<instance fields>\r\n");
				for each(Gen::KeyValuePair<cl::string^,cl::object^> pair in this->fields){
					build->Append(pair.Key);
					build->Append(" : ");
					build->Append(pair.Value);
					build->Append("\r\n");
				}
				build->Append("}\r\n");
				return build->ToString();
			}
		};
		[System::Serializable]
		value class cmdAnonymousInvoke:IChannelCommand{
			cmdAnonymousInvoke_Holder instance;
			Ref::MethodInfo^ minfo;
			array<cl::object^>^ params;
		public:
			cmdAnonymousInvoke(System::Delegate^ deleg,array<cl::object^>^ params){
				this->instance=cmdAnonymousInvoke_Holder(deleg);
				this->minfo=deleg->Method;
				this->params=params;
			}
			virtual void Execute(Receiver^ receiver);
		};
		[System::Serializable]
		value struct cmdAnonymousInvoke_Return{
			cmdAnonymousInvoke_Holder instance;
			cl::object^ retval;
		public:
			cmdAnonymousInvoke_Return(cmdAnonymousInvoke_Holder instance,cl::object^ retval)
				:instance(instance),retval(retval){}
		};
	//===============================================================
	//		��
	//===============================================================
	public:
		static property UINT WM_CHANNEL{
			UINT get(){return WM_Channel;}
		}
	};

//-------------------------------------------------------------------
	public delegate void ReceivedObjectEventHandler(Receiver^ receiver,cl::object^ obj);

	public ref class Receiver:public Control{
		DWORD procParent;
		DWORD procChild;
		ChannelMemory^ memory;

	internal:
		Receiver(DWORD processIdParent,DWORD processIdChild){
			// initfld
			this->procParent=processIdParent;
			this->procChild=processIdChild;
			this->memory=gcnew ChannelMemory(processIdParent,processIdChild);
			this->send_recvstr=nullptr;
			//this->trans_sendobj=(cl::string^)"����͔閧�̕��͂Ȃ̂ŁA�e�ɂ���������Ȃ��l�ɂȂ��Ă���̂����m��Ȃ�";

			// create handle
			this->Name=this->memory->Name;
			this->CreateControl();

			// �����̏���Ԃ�
			//Frms::MessageBox::Show(this->memory->Name);
			//Frms::MessageBox::Show(cl::string::Format("Code: {0}",this->memory->Code));
			//Frms::MessageBox::Show(cl::string::Format("Receiver Window was Created: handle == {0}",this->Handle));
			//Frms::MessageBox::Show(cl::string::Format("Memory Position: 0x{0:X}",(System::IntPtr)this->memory->Data0));
			this->memory->init_ReceiverHWND=this->Handle;
		}
	private:
		System::IO::Stream^ send_recvstr;
		System::IO::Stream^ trans_sendstr;
		cl::object^	trans_sendobj;
	public:
		property cl::object^ TransferTarget{
			cl::object^ get(){return this->trans_sendobj;}
			void set(cl::object^ value){this->trans_sendobj=value;}
		}
	//===============================================================
	//		WindowProc
	//===============================================================
	protected:
		virtual void WndProc(Message% m) override{
			if(m.Msg==WM_Channel){
				m.Result=(System::IntPtr)(int)WM_Channel;
				switch(this->memory->Code){
					case MessageCode::CreateReceiver:
						break;
					case MessageCode::ShowMessageBox:
						//::MessageBox(NULL,_T("MessageBox ��\�����܂���"),_T("MessageBox �̕\��"),0);
						Frms::MessageBox::Show(Receiver::typeid->Assembly->Location);
						Frms::MessageBox::Show(Receiver::typeid->Assembly->FullName);
						break;
					case MessageCode::SendObject:
						this->Cwm_SendObject();
						break;
					case MessageCode::GetObject:
						this->Cwm_GetObject();
						break;
					case MessageCode::DestroyReceiver:
						global::receivers->Remove(this->procParent);
						delete this;
						break;
				}
			}else{
				Control::WndProc(m);
			}
		}
	private:
		void Cwm_SendObject(void);
		void Cwm_GetObject(void);
	public:
		event ReceivedObjectEventHandler^ ReceivedObject;
	protected:
		/// <summary>
		/// SendObject �ŃI�u�W�F�N�g���󂯎�����ۂɌĂяo����܂��B
		/// </summary>
		virtual void OnReceivedObject(cl::object^ obj){
			//Frms::MessageBox::Show(obj->ToString());
			this->ReceivedObject(this,obj);
		}
	};
//-----------------------------------------------
// endof namespace mwg::Interop
}
}
//-----------------------------------------------
