using Interop=System.Runtime.InteropServices;

using DWORD=System.UInt32;
using BOOL=System.Int32;
using _=mwg.Win32.__global;
using HANDLE=System.IntPtr;

namespace mwg.Win32{
	/// <summary>
	/// kernel32.dll �̊֐������s���܂��B
	/// </summary>
	public static partial class Kernel32{
		[Interop.DllImport("kernel32.dll",CharSet=Interop.CharSet.Auto)]
		public extern static System.IntPtr LoadLibrary(string lpFileName);
		[Interop.DllImport("kernel32.dll")]
		public extern static bool FreeLibrary(System.IntPtr hModule);
		[Interop.DllImport("kernel32.dll",CharSet=Interop.CharSet.Ansi,ExactSpelling=true)]
		public static extern System.IntPtr GetProcAddress(System.IntPtr hModule, string procName);

		/// <summary>
		/// �Ăяo�����v���Z�X�̃v���Z�X���ʎq���擾���܂��B
		/// </summary>
		/// <returns>�Ăяo�����v���Z�X�̃v���Z�X���ʎq���Ԃ�܂��B</returns>
		[Interop::DllImport("kernel32.dll")]
		public static extern uint GetCurrentProcessId();
		/// <summary>
		/// ���݂̃v���Z�X�ɑΉ�����^���n���h�����擾���܂��B
		/// </summary>
		/// <returns>���݂̃v���Z�X�̋^���n���h�����Ԃ�܂��B</returns>
		[Interop::DllImport("kernel32.dll")]
		public static extern System.IntPtr GetCurrentProcess();
		//==========================================================================
		//		���z����������
		//==========================================================================
		/// <summary>
		/// �v���Z�X�̃n���h�����w�肵�܂��B���̃v���Z�X�̉��z�A�h���X��ԓ��Ƀ��������m�ۂ��܂��B
		/// </summary>
		/// <param name="hProcess"></param>
		/// <param name="lpAddress"></param>
		/// <param name="dwSize"></param>
		/// <param name="flAllocationType"></param>
		/// <param name="flProtect"></param>
		/// <returns></returns>
		[Interop::DllImport("kernel32.dll",SetLastError=true,ExactSpelling=true)]
		public static extern System.IntPtr VirtualAllocEx(
			System.IntPtr hProcess,System.IntPtr lpAddress,
			uint dwSize, MEM flAllocationType,PAGE flProtect);
		/// <summary>
		/// �w�肵�����z�������̈�̃A�N�Z�X�ی��ύX���܂��B
		/// </summary>
		/// <param name="lpAddress">�R�~�b�g�ς݃y�[�W�̈�̃A�h���X</param>
		/// <param name="dwSize">�̈�̃T�C�Y</param>
		/// <param name="flNewProtect">��]�̃A�N�Z�X�ی�</param>
		/// <param name="lpflOldProtect">�]���̃A�N�Z�X�ی���擾����ϐ��̃A�h���X</param>
		/// <returns>���������ꍇ�� true ��Ԃ��܂��B����ȊO�̏ꍇ�� false ��Ԃ��܂��B</returns>
		[Interop::DllImport("kernel32.dll",SetLastError=true)]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		public static unsafe extern bool VirtualProtect(
			void* lpAddress,
			uint dwSize,
			PAGE flNewProtect,
			out PAGE lpflOldProtect
			);

		[System.Flags]
		public enum PAGE:uint{
			NOACCESS=0x01,   
			READONLY=0x02,  
			READWRITE=0x04, 
			WRITECOPY=0x08,
			EXECUTE=0x10,
			EXECUTE_READ=0x20,
			EXECUTE_READWRITE=0x40,
			EXECUTE_WRITECOPY=0x80,
			GUARD=0x100,
			NOCACHE=0x200,
			WRITECOMBINE=0x400
		}
		[System.Flags]
		public enum MEM{
			COMMIT     =0x1000,
			RESERVE    =0x2000,
			DECOMMIT   =0x4000,
			RELEASE    =0x8000,
			RESET      =0x80000,
			PHYSICAL   =0x400000,
			TOP_DOWN   =0x100000,
			WRITE_WATCH=0x200000,
			LARGE_PAGES=0x20000000,
		}

		[Interop::DllImport("kernel32.dll", SetLastError=true)]
		public static extern System.IntPtr LocalFree(System.IntPtr hMem);
		[Interop::DllImport("kernel32.dll", SetLastError=true)]
		public static unsafe extern System.IntPtr LocalFree(void* hMem);
		//============================================================
		//		Event
		//============================================================
		[Interop::DllImport("kernel32.dll",SetLastError=true)]
		public static unsafe extern HANDLE CreateEvent(
			SECURITY_ATTRIBUTES* Security,
			[Interop::MarshalAs(Interop::UnmanagedType.Bool)]bool ManualReset,
			[Interop::MarshalAs(Interop::UnmanagedType.Bool)]bool InitialState,
			[Interop::MarshalAs(Interop::UnmanagedType.LPTStr)]string EventName
			);
		public unsafe struct SECURITY_ATTRIBUTES{
			public DWORD nLength;
			public void* lpSecurityDescriptor;
			BOOL bInheritHandle;
			public bool InheritHandle{
				get{return bInheritHandle!=0;}
				set{bInheritHandle=value?_.TRUE:_.FALSE;}
			}
		}
		[Interop::DllImport("kernel32.dll",SetLastError=true)]
		public static extern HANDLE OpenEvent(
			EVENT_ACCESS dwDesiredAccess,  // �A�N�Z�X��
			[Interop::MarshalAs(Interop::UnmanagedType.Bool)]bool bInheritHandle,    // �p���I�v�V����
			[Interop::MarshalAs(Interop::UnmanagedType.LPTStr)]string lpName  // �C�x���g�I�u�W�F�N�g�̖��O
			);
		/// <summary>
		/// EVENT_* @ WinNT.h
		/// </summary>
		public enum EVENT_ACCESS:uint{
			SYNCHRONIZE					=0x00100000,
			STANDARD_RIGHTS_REQUIRED	=0x000F0000,
			MODIFY_STATE				=0x0002,
			ALL_ACCESS					=STANDARD_RIGHTS_REQUIRED|SYNCHRONIZE|0x3
		}
		[Interop::DllImport("kernel32.dll",SetLastError=true)]
		[return: Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		public static extern bool SetEvent(
			HANDLE hEvent   // �C�x���g�I�u�W�F�N�g�̃n���h��
		);
		[Interop::DllImport("kernel32.dll",SetLastError=true)]
		[return: Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		public static extern bool ResetEvent(
			HANDLE hEvent   // �C�x���g�I�u�W�F�N�g�̃n���h��
		);
		[Interop::DllImport("kernel32.dll",SetLastError=true)]
		[return: Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		public static extern bool PulseEvent(
			HANDLE hEvent   // �C�x���g�I�u�W�F�N�g�̃n���h��
		);
		//============================================================
		//		WaitFunctions
		//============================================================
		[Interop::DllImport("kernel32.dll",SetLastError=true)]
		public static extern WAIT WaitForSingleObject(
			HANDLE hHandle,
			DWORD dwMilliseconds
		);
		public enum WAIT:uint{
			ABANDONED=0x080,	// The specified object is a mutex object that was not released by some thread.
			OBJECT_0=0x000,		// The state of the specified object is signaled.
			TIMEOUT=0x102,		// The time-out interval elapsed, and the object's state is nonsignaled.
		}
		//============================================================
		//		FormatMessage
		//============================================================
		public unsafe static string FormatMessage(
			FORMAT_MESSAGE dwFlags,
			System.IntPtr lpSource,
			uint dwMessageId,
			uint dwLanguageId,
			string[] pArguments
		){
			sbyte* pBuff=(sbyte*)0;
			if(0==FormatMessage(
				dwFlags|FORMAT_MESSAGE.ALLOCATE_BUFFER,
				lpSource,
				dwMessageId,dwLanguageId,
				out pBuff,0,
				pArguments
			))return null;

			string ret=new string(pBuff);
			LocalFree(pBuff);
			return ret;
		}
		// FORMAT_MESSAGE_ALLOCATE_BUFFER ��p
		[Interop::DllImport("Kernel32.dll",SetLastError=true)]
		private unsafe static extern uint FormatMessage(
			FORMAT_MESSAGE dwFlags,
			System.IntPtr lpSource,
			uint dwMessageId,
			uint dwLanguageId,
			out sbyte* lpBuffer,
			uint nSize,
			string[] pArguments
			);
		/// <summary>
		/// FORMAT_MESSAGE_* from WinBase.h.
		/// </summary>
		public enum FORMAT_MESSAGE:uint{
			ALLOCATE_BUFFER =0x00000100,
			IGNORE_INSERTS  =0x00000200,
			FROM_STRING     =0x00000400,
			FROM_HMODULE    =0x00000800,
			FROM_SYSTEM     =0x00001000,
			ARGUMENT_ARRAY  =0x00002000,
			MAX_WIDTH_MASK  =0x000000FF,
		}
		//============================================================
		//		GetLastError �֘A
		//============================================================
		/// <summary>
		/// �Ō�̃G���[�̃R�[�h���擾���܂��B
		/// </summary>
		/// <returns>�G���[�R�[�h��Ԃ��܂��B</returns>
		/// <remarks>
		/// ����� CLR �̓s���ׁ̈A Kernel32 �� GetLastError ���Ăяo���܂���B
		/// ����� System.Runtime.InteropServices.Marshal.GetLastWin32Error ���g�p���܂��B
		/// �G���[�����o�������ꍇ�ɂ́ADllImportAttribute.SetLastError=true ��ݒ肵�ĉ������B
		/// </remarks>
		public static int GetLastError(){return Interop.Marshal.GetLastWin32Error();}
		/// <summary>
		/// �G���[�R�[�h��ݒ肵�܂��B
		/// </summary>
		/// <param name="dwErrCode">�ݒ肷��G���[�R�[�h���w�肵�܂��B</param>
		[Interop.DllImport("kernel32.dll",SetLastError=true)]
		public static extern void SetLastError(int dwErrCode);
		/// <summary>
		/// �G���[�R�[�h���N���A���ȍ~�̃G���[�����o���鏀�������܂��B
		/// </summary>
		public static void @try(){SetLastError(0);}
		/// <summary>
		/// @try() �����Ĉȍ~�ɃG���[�������������ǂ������擾���܂��B
		/// </summary>
		/// <returns>�G���[�����������ꍇ�� true ��Ԃ��܂��B</returns>
		public static bool @catch(){return GetLastError()!=0;}
		/// <summary>
		/// @try() �����Ĉȍ~�ɃG���[�������������ǂ������擾���܂��B
		/// </summary>
		/// <param name="code">�G���[�R�[�h��Ԃ�������w�肵�܂��B</param>
		/// <returns>�G���[�����������ꍇ�� true ��Ԃ��܂��B</returns>
		public static bool @catch(out int code){return (code=GetLastError())!=0;}
		/// <summary>
		/// �Ō�̃G���[�R�[�h�ɏ]���ăG���[�𔭐������܂��B
		/// </summary>
		/// <param name="msg">�G���[���b�Z�[�W</param>
		public static void @throw(string msg){
			int code=GetLastError();
			if(0!=(code&0x20000000)){
				string desc=FormatMessage(
					FORMAT_MESSAGE.IGNORE_INSERTS|FORMAT_MESSAGE.FROM_HMODULE,
					System.IntPtr.Zero,(uint)code,0,null);
				msg="ERROR "+(desc??code.ToString("X8"))+": "+msg;
				throw new System.ApplicationException(msg);
			}else{
				System.Exception inner=new System.ComponentModel.Win32Exception(code);
				if(msg==null)throw inner;
				throw new System.Exception(msg,inner);
			}
		}
		/// <summary>
		/// �Ō�̃G���[�R�[�h�̕\���G���[���b�Z�[�W���擾���܂��B
		/// </summary>
		/// <returns>�G���[�̐��������镶�����Ԃ��܂��B</returns>
		public static string GetLastErrorMessage(){
			int code=GetLastError();
			if(0==code)return null;
			if(0!=(code&0x20000000)){
				return FormatMessage(
					FORMAT_MESSAGE.IGNORE_INSERTS|FORMAT_MESSAGE.FROM_HMODULE,
					System.IntPtr.Zero,(uint)code,0,null);
			}else{
				return new System.ComponentModel.Win32Exception(code).Message;
			}
		}
		/// <summary>
		/// �Ō�̃G���[�R�[�h�ɏ]���ăG���[�𔭐������܂��B
		/// </summary>
		public static void @throw(){@throw("�\�����ʃG���[");}
	}
}
