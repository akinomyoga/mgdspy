#include "stdafx.h"
#include "Channel.h"

UINT WM_Channel=::RegisterWindowMessage(_T("mwg.Interop.WM_Channel"));

inline cl::string^ CreateChannelName(DWORD pIdSv,DWORD pIdCl){
	return cl::string::Format("mwg.Interop.Channel[sv:{0}->cl:{1}]",pIdSv,pIdCl);
}

using namespace mwg::Interop;

//===============================================================
//		ChannelMemory ������
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
//		init_AssemblyLocation �͌��ݎg�p����Ă��܂���
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
	
	if(mstr->Length>(__int64)SHARE_SIZE)throw gcnew System::Exception("assembly file �����������܂�");
	int size=(int)mstr->Length;
	this->init_sizeofAssemblyName=size;

	mstr->Position=0;
	BYTE* p=&this->init_pdata->bAssemblyName;
	BYTE* pM=p+size;
	while(p<pM)*p++=(BYTE)mstr->ReadByte();
	mstr->Close();
}

//===============================================================
//		Channel ������
//===============================================================
void Channel::Initialize(HWND hWnd){
	// Parent Id
	this->procidSrc=::GetCurrentProcessId();

	// Child Id
	DWORD procidDst;
	DWORD threadId=::GetWindowThreadProcessId(hWnd,&procidDst);
	this->procidDst=procidDst;

	// ���L�������� (FileMapping)
	this->name=::CreateChannelName(this->procidSrc,this->procidDst);
	
	// �o�^����Ă��� SharedMemory ���擾
	ChannelInfo info;
	if(dic_info->TryGetValue(this->name,info)){
		this->memory=info.memory;
		this->receiver=info.receiver;
	}else{
		info.memory=this->memory=gcnew ChannelMemory(this->name);

		if(!CreateReceiver(hWnd,threadId)){
			throw gcnew System::Exception("Channel �̊m���Ɏ��s���܂����B");
		}
		info.receiver=this->receiver;
		dic_info->Add(this->name,info);
	}
}

bool Channel::CreateReceiver(HWND hWnd,DWORD threadId){
	// Hook ����֐��̊m�F
	if(hDll==NULL){
		Frms::MessageBox::Show("mgdspy_hk.dll ��������܂���...");
		return false;
	}
	if(hProc==NULL){
		Frms::MessageBox::Show("mgdspy_hk.dll �Ƀv���V�[�W�� HookPoint ��������܂���...");
		return false;
	}
	//Frms::MessageBox::Show(cl::string::Format("hDll {0:X}; hProc {0:X}",(int)hDll,(int)hProc));

	// Hook ��̊��̏���
	if(hWnd==NULL){
		Frms::MessageBox::Show("�w�肵�� Window Handle �� NULL �ł�...");
		return false;
	}

	// Hook
	this->memory->Code=MessageCode::CreateReceiver;
	HHOOK hhook=::SetWindowsHookEx(WH_CALLWNDPROC,Channel::hProc,Channel::hDll,threadId);
	if(hhook==NULL){
		Frms::MessageBox::Show("����� Window �ɑ΂��� Hook �Ɏ��s���܂���");
		return false;
	}

	::SendMessage(hWnd,WM_CHANNEL,(WPARAM)hhook,(LPARAM)this->procidSrc);
	// �� 䢂Ō������̃v���Z�X�ɂ� Receiver ����������� memory->init_ReceiverHWND �� Handle ���i�[����
	::UnhookWindowsHookEx(hhook);

	//Frms::MessageBox::Show(cl::string::Format("Code: {0}",this->memory->Code));
	//Frms::MessageBox::Show(cl::string::Format("Memory Position: 0x{0:X}",(System::IntPtr)this->memory->Data0));
	//Frms::MessageBox::Show(cl::string::Format("Receiver Handle ���擾���܂���: {0}",this->memory->init_ReceiverHWND));
	this->receiver=(HWND)(void*)this->memory->init_ReceiverHWND;
	if(this->receiver==NULL){
		Frms::MessageBox::Show("����v���Z�X�ł� Receiver �����Ɏ��s���܂���");
		return false;
	}
	return true;
}

//===============================================================
//		Channel ����M
//===============================================================
void Receiver::Cwm_SendObject(){
	if(this->memory->trans_IsInitialize){
		if(this->send_recvstr!=nullptr)this->send_recvstr->Close();
		this->send_recvstr=gcnew System::IO::MemoryStream();
	}

	this->memory->trans_ReadData(this->send_recvstr);

	if(!this->memory->trans_IsComplete)return;

	// �ȉ��͎�M������̏���

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
	// GetObject �̕��@
	// 1. �悸���炩�̕��@�Ō������ɑ��肽���I�u�W�F�N�g�����݂��鎖��m�点��
	// 2. ���������� GetObject (IsInitialize) �� SendMessage ����ė���B
	// 3. �������œK���Ƀf�[�^����������Ő����߂��B
	// 4. ���������� GetObject (�ʏ�) �� SendMessage ����ė���B
	// 5. 3. 4. ���J��Ԃ��A�Ō�̃f�[�^���������񂾂� IsComplete ��ݒ肵�ĕԂ��B 
	// 6. ���������� GetObject (IsComplete) �� SendMessage ����ė�����Astream ����ďI���B
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
//		�e�햽��
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
