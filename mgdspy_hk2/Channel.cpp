#include "stdafx.h"
#include "Channel.h"

UINT WM_Channel=::RegisterWindowMessage(_T("mwg.Interop.WM_Channel"));

inline cl::string^ CreateChannelName(DWORD pIdSv,DWORD pIdCl){
	return cl::string::Format("mwg.Interop.Channel[sv:{0}->cl:{1}]",pIdSv,pIdCl);
}

using namespace mwg::Interop;

//===============================================================
//		ChannelMemory 初期化
//===============================================================
ChannelMemory::ChannelMemory(DWORD processIdParent,DWORD processIdChild){
	this->Initialize(::CreateChannelName(processIdParent,processIdChild));
}

void ChannelMemory::Initialize(cl::string^ name){
	this->name=name;
	this->shared=gcnew mwg::Interop::SharedMemory(this->name,ALLOC_SIZE);
	this->map=(MemoryMap*)this->shared->pData;
	this->pdata0=(void*)&this->map->data0;
}
//---------------------------------------------------------------
//		init_AssemblyLocation は現在使用されていません
//---------------------------------------------------------------
cl::string^ ChannelMemory::init_AssemblyLocation::get(){
	int size=this->init_sizeofAssemblyName;
	
	System::IO::Stream^ mstr=gcnew System::IO::MemoryStream(size);
	BYTE* p=&this->init_pdata->bAssemblyName;
	BYTE* pM=p+size;
	while(p<pM)mstr->WriteByte(*p++);
	mstr->Position=0;

	cl::string^ ret=(cl::string^)TransTypeSerializer::Deserialize(mstr);
	mstr->Close();
	return ret;
}

void ChannelMemory::init_AssemblyLocation::set(cl::string^ value){
	System::IO::Stream^ mstr=gcnew System::IO::MemoryStream();
	TransTypeSerializer::Serialize(mstr,value);
	
	if(mstr->Length>(__int64)SHARE_SIZE)throw gcnew System::Exception("assembly file 名が長すぎます");
	int size=(int)mstr->Length;
	this->init_sizeofAssemblyName=size;

	mstr->Position=0;
	BYTE* p=&this->init_pdata->bAssemblyName;
	BYTE* pM=p+size;
	while(p<pM)*p++=(BYTE)mstr->ReadByte();
	mstr->Close();
}

//===============================================================
//		Channel 初期化
//===============================================================
void Channel::Initialize(HWND hWnd){
	// Parent Id
	this->procidSrc=::GetCurrentProcessId();

	// Child Id
	DWORD procidDst;
	DWORD threadId=::GetWindowThreadProcessId(hWnd,&procidDst);
	this->procidDst=procidDst;

	// 共有メモリ名 (FileMapping)
	this->name=::CreateChannelName(this->procidSrc,this->procidDst);
	
	// 登録されている SharedMemory を取得
	ChannelInfo info;
	if(dic_info->TryGetValue(this->name,info)){
		this->memory=info.memory;
		this->receiver=info.receiver;
	}else{
		info.memory=this->memory=gcnew ChannelMemory(this->name);

		if(!CreateReceiver(hWnd,threadId)){
			throw gcnew System::Exception("Channel の確立に失敗しました。");
		}
		info.receiver=this->receiver;
		dic_info->Add(this->name,info);
	}
}

bool Channel::CreateReceiver(HWND hWnd,DWORD threadId){
	// Hook する関数の確認
	if(hDll==NULL){
		Frms::MessageBox::Show("mgdspy_hk.dll が見つかりません...");
		return false;
	}
	if(hProc==NULL){
		Frms::MessageBox::Show("mgdspy_hk.dll にプロシージャ HookPoint が見つかりません...");
		return false;
	}
	//Frms::MessageBox::Show(cl::string::Format("hDll {0:X}; hProc {0:X}",(int)hDll,(int)hProc));

	// Hook 先の環境の準備
	if(hWnd==NULL){
		Frms::MessageBox::Show("指定した Window Handle は NULL です...");
		return false;
	}

	// Hook
	this->memory->Code=MessageCode::CreateReceiver;
	HHOOK hhook=::SetWindowsHookEx(WH_CALLWNDPROC,Channel::hProc,Channel::hDll,threadId);
	if(hhook==NULL){
		Frms::MessageBox::Show("相手の Window に対する Hook に失敗しました");
		return false;
	}

	::SendMessage(hWnd,WM_CHANNEL,(WPARAM)hhook,(LPARAM)this->procidSrc);
	// ↑ 茲で向こうのプロセスにて Receiver が生成されて memory->init_ReceiverHWND に Handle を格納する
	::UnhookWindowsHookEx(hhook);

	//Frms::MessageBox::Show(cl::string::Format("Code: {0}",this->memory->Code));
	//Frms::MessageBox::Show(cl::string::Format("Memory Position: 0x{0:X}",(System::IntPtr)this->memory->Data0));
	//Frms::MessageBox::Show(cl::string::Format("Receiver Handle を取得しました: {0}",this->memory->init_ReceiverHWND));
	this->receiver=(HWND)(void*)this->memory->init_ReceiverHWND;
	if(this->receiver==NULL){
		Frms::MessageBox::Show("相手プロセスでの Receiver 生成に失敗しました");
		return false;
	}
	return true;
}

//===============================================================
//		Channel 送受信
//===============================================================
void Receiver::Cwm_SendObject(){
	if(this->memory->trans_IsInitialize){
		if(this->send_recvstr!=nullptr)this->send_recvstr->Close();
		this->send_recvstr=gcnew System::IO::MemoryStream();
	}

	this->memory->trans_ReadData(this->send_recvstr);

	if(!this->memory->trans_IsComplete)return;

	// 以下は受信完了後の処理

	this->send_recvstr->Position=0;
	cl::object^ obj=TransTypeSerializer::Deserialize(this->send_recvstr);
	this->send_recvstr->Close();
	this->send_recvstr=nullptr;

	IChannelCommand^ cmd=dynamic_cast<IChannelCommand^>(obj);
	if(cmd!=nullptr){
		cmd->Execute(this);
		return;
	}
	this->OnReceivedObject(obj);
}

void Receiver::Cwm_GetObject(){
	// GetObject の方法
	// 1. 先ず何らかの方法で向こうに送りたいオブジェクトが存在する事を知らせる
	// 2. 向こうから GetObject (IsInitialize) が SendMessage されて来る。
	// 3. こっちで適当にデータを書き込んで制御を戻す。
	// 4. 向こうから GetObject (通常) が SendMessage されて来る。
	// 5. 3. 4. を繰り返し、最後のデータを書き込んだら IsComplete を設定して返す。 
	// 6. 向こうから GetObject (IsComplete) が SendMessage されて来たら、stream を閉じて終わり。
	//-------------------------------------------------------------
	try{
		// 6.
		if(this->memory->trans_IsComplete){
			if(this->trans_sendstr!=nullptr)this->trans_sendstr->Close();
			return;
		}

		// 2.
		if(this->memory->trans_IsInitialize){
			if(this->trans_sendstr!=nullptr)this->trans_sendstr->Close();
			this->trans_sendstr=gcnew System::IO::MemoryStream();
			TransTypeSerializer::Serialize(this->trans_sendstr,this->trans_sendobj);
			this->trans_sendstr->Position=0;
		}

		// 2->3, 4->5.
		this->memory->trans_WriteData(this->trans_sendstr);
	}catch(...){
		this->memory->trans_IsFailed=true;
	}
}

//===============================================================
//		各種命令
//===============================================================
void Channel::cmdInvoke::Execute(Receiver^ receiver){
	try{
		receiver->TransferTarget=this->minfo->Invoke(this->instance,this->params);
	}catch(System::Exception^ e){
		receiver->TransferTarget=cmdInvoke_Exception(e);
	}
}
void Channel::cmdAnonymousInvoke::Execute(Receiver^ receiver){
	try{
		cl::object^ instance=this->instance.GetRealObject();
		cl::object^ ret=this->minfo->Invoke(instance,this->params);
		receiver->TransferTarget=cmdAnonymousInvoke_Return(
			cmdAnonymousInvoke_Holder(instance),
			ret
			);
	}catch(System::Exception^ e){
		receiver->TransferTarget=cmdInvoke_Exception(e);
	}
}
