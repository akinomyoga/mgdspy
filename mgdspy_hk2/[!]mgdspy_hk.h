// mgdspy_hk2.h
#pragma once
/*
using namespace System;
namespace ManagedSpy{
	public delegate void ProcMessage(HWND hWnd,int code,WPARAM wParam,LPARAM lParam);
	struct MessageInfo{
		WPARAM wParam;
		LPARAM lParam;
		int msg;
		HWND hWnd;
	};
	/// <summary>
	/// Client
	/// </summary>
	public ref class Client{
	private:
		HHOOK hhk;
	public:
		int OnMessage(int nCode,WPARAM wParam,LPARAM lParam){
			try{
				if(nCode==HC_ACTION){
					MessageInfo* info=(MessageInfo*)lParam;
					System::Windows::Forms::Message msg=System::Windows::Forms::Message::Create(
						(System::IntPtr)(int)info->hWnd,
						info->msg,
						(System::IntPtr)(int)info->wParam,
						(System::IntPtr)(int)info->lParam
					);
				}
			}catch(...){}

			return (int)CallNextHookEx(hhk,nCode,wParam,lParam);
		}
	public:
		event ProcMessage^ Message;
	};

}//*/
