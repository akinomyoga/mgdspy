// stdafx.h : �W���̃V�X�e�� �C���N���[�h �t�@�C���̃C���N���[�h �t�@�C���A�܂���
// �Q�Ɖ񐔂������A�����܂�ύX����Ȃ��A�v���W�F�N�g��p�̃C���N���[�h �t�@�C��
// ���L�q���܂��B

#pragma once

#pragma managed(push,off)
# include <windows.h>
# include <tchar.h>
#pragma managed(pop)

#pragma warning(push)
# pragma warning(disable:4091)
# include <msclr/lock.h>
#pragma warning(pop)

#ifdef _UNICODE
# define String2LPTSTR(str1)	(wchar_t*)(void*)Marshal::StringToHGlobalUni(str1)
#else
# define String2LPTSTR(str1)	(char*)(void*)Marshal::StringToHGlobalAnsi(str1)
#endif

//#define cli_string	System::String
//#define cli_object	System::Object
//#define cli_type	System::Type
//#define Gen		System::Collections::Generic
//#define Frms	System::Windows::Forms
//#define Ref		System::Reflection
//#define Diag	System::Diagnostics

//typedef System::String cli_string;
//typedef System::Object cli_object;
//typedef System::Type cli_type;

namespace Gen=System::Collections::Generic;
namespace Frms=System::Windows::Forms;
namespace Ref=System::Reflection;
namespace Diag=System::Diagnostics;

namespace cl{
  typedef System::String  string;
  typedef System::Object  object;
  typedef System::Type    type;
}

//-------------------------------------------------------------------
// namespace mwg::Interop
namespace mwg{
namespace Interop{
	ref class Channel;
	ref class Receiver;
//-------------------------------------------------------------------

ref class global abstract sealed{
public:
	/// <summary>
	/// HookPoint ���� lock �Ώۂł��B����� lock ���Ȃ��ŉ������B
	/// </summary>
	static Gen::Dictionary<DWORD,Receiver^>^ receivers=gcnew Gen::Dictionary<DWORD,Receiver^>();
};

//-----------------------------------------------
// endof namespace mwg::Interop
}
}
//-----------------------------------------------
