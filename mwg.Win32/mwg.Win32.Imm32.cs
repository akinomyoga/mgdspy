using Interop=System.Runtime.InteropServices;
using IME=mwg.Win32.Imm32;
namespace mwg.Win32{
	public class Imm32{
		[Interop.DllImport("imm32.dll")]
		public static extern System.IntPtr ImmGetContext(System.IntPtr hWnd);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmReleaseContext(System.IntPtr hWnd,System.IntPtr hIMC);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetCompositionString(System.IntPtr hIMC, uint dwIndex, System.Text.StringBuilder lpBuf, uint dwBufLen);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetCompositionString(System.IntPtr hIMC, uint dwIndex,[Interop.Out()]byte[] lpBuf, uint dwBufLen);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmSetCompositionFont(System.IntPtr hIMC, ref LOGFONT lplf);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetCompositionFont(System.IntPtr hIMC, ref LOGFONT lplf);
		[Interop.DllImport("imm32.dll")]
		public static extern bool ImmSetCompositionWindow(System.IntPtr hIMC,ref COMPOSITIONFORM lpCompForm);
		[Interop.DllImport("imm32.dll")]
		public static extern bool ImmGetCompositionWindow(System.IntPtr hIMC,ref COMPOSITIONFORM lpCompForm);
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr GetKeyboardLayout(int idThread);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmIsIME(System.IntPtr hKL);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetDescription(System.IntPtr hKL,System.Text.StringBuilder lpszDescription,uint uBufLen);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmSetOpenStatus(System.IntPtr hIMC,
			[Interop.MarshalAs(Interop.UnmanagedType.Bool)]
			bool fOpen
			);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetOpenStatus(System.IntPtr hIMC);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetConversionStatus(System.IntPtr hIMC,IME.CMODE lpfdwConversion,IME.SMODE lpfdwSentence);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmSetConversionStatus(System.IntPtr hIMC,IME.CMODE lpfdwConversion,IME.SMODE lpfdwSentence);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmGetCandidateList(System.IntPtr hIMC,int dwIndex,IME.CANDIDATELIST lpCandList,int dwBufLen);
		[Interop.DllImport("imm32.dll",EntryPoint="ImmGetCandidateList")]
		private static extern int ImmGetCandidateList(System.IntPtr hIMC,int dwIndex,[Interop.Out()]byte[] lpCandList,int dwBufLen);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmNotifyIME(System.IntPtr hIMC,IME.NI dwAction,int dwIndex,int dwValue);
		[Interop.DllImport("imm32.dll")]
		public static extern int ImmConfigureIME(System.IntPtr hKL,System.IntPtr hWnd,IME.CONFIG dwMode,ref IME.REGISTERWORD lpData);
		//TODO:�Ή����Ă��Ȃ���
		//ImmAssociateContext
		//ImmAssociateContextEx
		//ImmCreateContxet
		//ImmDestroyContext
		//ImmDisableIME
		//ImmEnumInputContext
		//ImmEnumRegisterWord
		//ImmEscape c.f. http://msdn.microsoft.com/library/default.asp?url=/library/en-us/intl/ime_396b.asp
		//ImmGetCandidateWindow
		//ImmGetCandidateListCount
		//ImmGetConversionList
		//ImmGetDefaultIMEWnd
		//ImmGetGuideLine
		//ImmGetIMEFileName
		//ImmGetImeMenuItems
		//ImmGetProperty
		//ImmGetRegisterWordStyle
		//ImmGetStatusWindowPos
		//ImmGetVirtualKey
		//ImmInstallIME
		//ImmIsUIMessage
		//ImmRegisterWord
		//ImmSetCandidateWindow
		//ImmSetCompositionString
		//ImmSetStatusWindowPos
		//ImmSimulateHotkey
		//ImmUnregisterWord
		[Interop.StructLayout(Interop.LayoutKind.Sequential)]
		public struct COMPOSITIONFORM{
			public CFS dwStyle;
			public POINT ptCurrentPos;
			public RECT rcArea;
		}
		[Interop.StructLayout(Interop.LayoutKind.Sequential)]
		public struct CANDIDATEFORM{
			/// <summary>
			/// ��⃊�X�g���ʎq�B0 ���� 31 �܂ł̐��l�Ŏw�肷�鎖�B 
			/// </summary>
			int dwIndex;
			/// <summary>
			/// CFS.CANDIDATEPOS, CFS.EXCLUDE ���w��
			/// </summary>
			IME.CFS dwStyle;
			POINT ptCurrentPos;
			RECT  rcArea;
			public CANDIDATEFORM(int index,IME.CFS style,System.Drawing.Point currentPos,System.Drawing.Rectangle area){
				this.dwIndex=(0<=index&&index<32)?index:0;
				this.dwStyle=style&(IME.CFS.EXCLUDE|IME.CFS.CANDIDATEPOS);
				this.ptCurrentPos=currentPos;
				this.rcArea=area;
			}
		}
		/// <summary>
		/// ���̓��[�h conversion mode ��\���܂�
		/// </summary>
		[System.Flags()]public enum CMODE:uint{
			ALPHANUMERIC=0x0000,
			NATIVE=0x0001,		CHINESE=0x0001,		HANGUL=0x0001,		JAPANESE=0x0001,
			[System.Obsolete("HANGUL ���g�p���ĉ�����")]
			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
			HANGEUL=0x0001,
			/// <summary>
			/// �Љ����Bonly effect under IME_CMODE_NATIVE
			/// </summary>
			KATAKANA=0x0002,
			LANGUAGE=0x0003,
			/// <summary>
			/// �S�p
			/// </summary>
			FULLSHAPE=0x0008,
			/// <summary>
			/// ���[�}���ϊ�
			/// </summary>
			ROMAN=0x0010,
			CHARCODE=0x0020,
			HANJACONVERT=0x0040,
			/// <summary>
			/// �\�t�g�L�[�{�[�h
			/// </summary>
			SOFTKBD=0x0080,
			/// <summary>
			/// ���ϊ�
			/// </summary>
			NOCONVERSION=0x0100,
			EUDC=0x0200,
			SYMBOL=0x0400,
			FIXED=0x0800,
			RESERVED=0xF0000000
		}
		/// <summary>
		/// �ϊ����[�h sentence mode ��\���܂�
		/// </summary>
		[System.Flags()]public enum SMODE:uint{
			NONE=0x0000,
			PLAURALCLAUSE=0x0001,
			SINGLECONVERT=0x0002,
			AUTOMATIC=0x0004,
			PHRASEPREDICT=0x0008,
			CONVERSATION=0x0010,
			RESERVED=0xF000
		}
		/// <summary>
		/// WM_IME_COMPOSITION �� lParam �Ƃ��ēn���ꂽ�ꍇ�A�ǂ̏�񂪍X�V���ꂽ���������܂��B
		/// ImmGetCompositionString �� �����Ƃ��ēn���ꍇ�A�ǂ̏����擾���邩�w�肵�܂��B
		/// </summary>
		[System.Flags()]public enum GCS{
			/// <summary>
			/// ���݂̕ҏW������̓ǂ�
			/// </summary>
			COMPREADSTR=0x0001,
			/// <summary>
			/// ���݂̕ҏW������̓ǂ݂̑���
			/// </summary>
			COMPREADATTR=0x0002,
			/// <summary>
			/// ���݂̕ҏW������̕��ߏ��
			/// </summary>
			COMPREADCLAUSE=0x0004,
			/// <summary>
			/// ���݂̕ҏW������
			/// </summary>
			COMPSTR=0x0008,
			/// <summary>
			/// �ҏW������̑���
			/// </summary>
			COMPATTR=0x0010,
			/// <summary>
			/// �ҏW������̕��ߏ��
			/// </summary>
			COMPCLAUSE=0x0020,
			/// <summary>
			/// �ҏW�����񒆂̂̃J�[�\���̈ʒu
			/// </summary>
			CURSORPOS=0x0080,
			/// <summary>
			/// �ҏW�����񂪕ω������ۂ̍����J�n�ʒu?
			/// </summary>
			DELTASTART=0x0100,
			/// <summary>
			/// �m�蕶����̓ǂ�
			/// </summary>
			RESULTREADSTR=0x0200,
			/// <summary>
			/// �m�蕶����̓ǂ݂̕��ߏ��
			/// </summary>
			RESULTREADCLAUSE=0x0400,
			/// <summary>
			/// �m�蕶����
			/// </summary>
			RESULTSTR=0x0800,
			/// <summary>
			/// �m�蕶����̕��ߏ��
			/// </summary>
			RESULTCLAUSE=0x1000
		}
		[Interop.StructLayout(Interop.LayoutKind.Sequential)]
		public struct CANDIDATELIST{
			int dwSize;
			int dwStyle;
			int dwCount;
			int dwSelection;
			int dwPageStart;
			int dwPageSize;
			[Interop.MarshalAs(Interop.UnmanagedType.ByValArray,SizeConst=1)]
			int[] dwOffset;
			//byte[] ���e;
		}//*/
		public struct CandidateList{
			/// <summary>
			/// ���̃X�^�C���B���𒊏o�������@
			/// </summary>
			public IME.CAND Style;
			/// <summary>
			/// ���ݑI������Ă�����
			/// </summary>
			public int Selection;
			/// <summary>
			/// ���E�B���h�E�ɕ\������Ă��镨�ň�ԏ��߂̌��� index
			/// </summary>
			public int PageStart;
			/// <summary>
			/// ���E�B���h�E�ɕ\���������̐�
			/// </summary>
			public int PageSize;
			private string[] item;
			public CandidateList(byte[] lpcdl){
				int dwSize=0,dwCount=0;
				int[] dwOffset;
				using(System.IO.MemoryStream str=new System.IO.MemoryStream(lpcdl))
				using(System.IO.BinaryReader reader=new System.IO.BinaryReader(str)){
					dwSize=reader.ReadInt32();
					this.Style=(IME.CAND)reader.ReadInt32();
					dwCount=reader.ReadInt32();
					this.Selection=reader.ReadInt32();
					this.PageStart=reader.ReadInt32();
					this.PageSize=reader.ReadInt32();
					dwOffset=new int[dwCount+1];
					for(int i=0;i<dwCount;i++)dwOffset[i]=reader.ReadInt32();
					dwOffset[dwCount]=dwSize;
				}	
				this.item=new string[dwCount];
				//string str0="";
				for(int i=0;i<dwCount;i++){
					//ATOK16 Encoding.GetEncoding("shift-jis")
					this.item[i]=System.Text.Encoding.GetEncoding("shift-jis").GetString(lpcdl,dwOffset[i],dwOffset[i+1]-dwOffset[i]-1);
				}
			}
			public string this[int i]{
				get{
					if(i<0||i>=this.item.Length){
						throw new System.ArgumentOutOfRangeException("i",i,"�w�肵���C���f�b�N�X�̗v�f�͂���܂���");
					}
					return this.item[i];
				}
			}
			public int Count{get{return this.item.Length;}}
		}
		[System.Flags()]
		public enum CFS:uint{
			DEFAULT=0x0,
			RECT=0x1,
			POINT=0x2,
			FORCE_POSITION=0x20,
			CANDIDATEPOS=0x40,
			EXCLUDE=0x80
		}
		/// <summary>
		/// �ϊ����̃X�^�C���������܂��B
		/// </summary>
		public enum CAND{
			/// <summary>
			/// �s���̌��X�^�C��
			/// </summary>
			UNKNOWN=0x0000,
			/// <summary>
			/// ���͓���̓ǂ݂ł�
			/// </summary>
			READ=0x0001,
			/// <summary>
			/// ���͓���̕����R�[�h�͈͂ɂ���܂�
			/// </summary>
			CODE=0x0002,
			/// <summary>
			/// ���͓����Ӗ��ł�
			/// </summary>
			MEANING=0x0003,
			/// <summary>
			/// ���͓���̕�����g���Ă��܂�
			/// </summary>
			RADICAL=0x0004,
			/// <summary>
			/// ���͓���̉搔�ł�
			/// </summary>
			STROKE=0x0005
		}
		/// <summary>
		/// WM_IME_NOTIFY �� wParam
		/// </summary>
		public enum IMN{
			/// <summary>
			/// �X�e�[�^�X�E�B���h�E����悤�Ƃ��Ă��܂��BlParam�͎g���܂���B
			/// </summary>
			CLOSESTATUSWINDOW=0x0001,
			/// <summary>
			/// �X�e�[�^�X�E�B���h�E����낤�Ƃ��Ă��܂��BlParam�͎g���܂���B
			/// </summary>
			OPENSTATUSWINDOW=0x0002,
			/// <summary>
			/// IME�����E�B���h�E�̓��e��ς��悤�Ƃ����Ƃ��ɑ����Ă��܂��B
			/// lParam�͌�⃊�X�g�t���O�ł��B���ꂼ��̃r�b�g�����X�g�ɑΉ����Ă��܂��B�r�b�g�O���ŏ��̃��X�g�ł��B
			/// </summary>
			CHANGECANDIDATE=0x0003,
			/// <summary>
			/// ���E�B���h�E����悤�Ƃ��Ă��܂��BlParam�͌�⃊�X�g�t���O�ł��B
			/// </summary>
			CLOSECANDIDATE=0x0004,
			/// <summary>
			/// ���E�B���h�E���J�����Ƃ��Ă��܂��BlParam�͌�⃊�X�g�t���O�ł��B
			/// </summary>
			OPENCANDIDATE=0x0005,
			/// <summary>
			/// ���͕����񃂁[�h���ω������Ƃ��ɑ����Ă��܂��BlParam�͎g���܂���B
			/// </summary>
			SETCONVERSIONMODE=0x0006,
			/// <summary>
			/// �ϊ����[�h���ω������Ƃ��ɑ����Ă��܂��BlParam�͎g���܂���B
			/// </summary>
			SETSENTENCEMODE=0x0007,
			/// <summary>
			/// IME��ON,OFF��Ԃ��ω������Ƃ��ɑ����܂��BlParam�͎g���܂���B
			/// </summary>
			SETOPENSTATUS=0x0008,
			/// <summary>
			/// ��⏈�����I�����Č��E�B���h�E���ړ����悤�Ƃ��Ă��܂��B
			/// lParam�͌�⃊�X�g�t���O�ł��B
			/// </summary>
			SETCANDIDATEPOS=0x0009,
			/// <summary>
			/// ���̓R���e�L�X�g�̃t�H���g���ŐV�����ꂽ�Ƃ��ɑ����Ă��܂��B
			/// lParam�͎g���܂���B
			/// </summary>
			SETCOMPOSITIONFONT=0x000A,
			/// <summary>
			/// �ҏW�E�B���h�E�̃X�^�C����ʒu���ω������Ƃ��ɑ����Ă��܂��B
			/// lParam�͎g���܂���B
			/// </summary>
			SETCOMPOSITIONWINDOW=0x000B,
			/// <summary>
			/// �X�e�[�^�X�E�B���h�E�̈ʒu���ω������Ƃ��ɑ����܂��BlParam�͎g���܂���B
			/// </summary>
			SETSTATUSWINDOWPOS=0x000C,
			/// <summary>
			/// �G���[���b�Z�[�W��\�����悤�Ƃ��Ă��܂��BlParam�͎g���܂���B
			/// </summary>
			GUIDELINE=0x000D,
			PRIVATE=0x000E
		}
		/// <summary>
		/// ImmNotifyIME �� dwAction�B
		/// [���p:http://msdn.microsoft.com/library/ja/default.asp?url=/library/ja/jpintl/html/_win32_ImmGetConversionStatus.asp]
		/// </summary>
		public enum NI:int{
			/// <summary>
			/// IME �ɁA���ꗗ���J���悤�Ɏw�����܂��B
			/// dwIndex �p�����[�^�ɂ́A�J���ꗗ�̃C���f�b�N�X���w�肵�܂��BDwValue �p�����[�^�ɂ́A�����w�肵�܂���B
			/// IME �́A�ꗗ���J������A�v���P�[�V������ IMN_OPENCANDIDATE ���b�Z�[�W�𑗐M���܂��B
			/// </summary>
			OPENCANDIDATE=0x0010,
			/// <summary>
			/// IME �ɁA���ꗗ�����悤�Ɏw�����܂��B
			/// dwIndex �p�����[�^�ɂ́A����ꗗ�̃C���f�b�N�X���w�肵�AdwValue �p�����[�^�ɂ͉����w�肵�܂���B
			/// IME �́A�ꗗ�������A�v���P�[�V������ IMN_CLOSECANDIDATE ���b�Z�[�W�𑗐M���܂��B
			/// </summary>
			CLOSECANDIDATE=0x0011,
			/// <summary>
			/// �ϊ����� 1 ��I�����܂��BdwIndex �p�����[�^�ɂ́A�ΏۂƂ�����ꗗ�̃C���f�b�N�X���w�肵�܂��B
			/// dwValue �p�����[�^�ɂ́A���̌��ꗗ�ł̌�╶����̃C���f�b�N�X���w�肵�܂��B
			/// </summary>
			SELECTCANDIDATESTR=0x0012,
			/// <summary>
			/// �I������Ă�����ꗗ��ύX���܂��B
			/// dwIndex �p�����[�^�ɂ́A�I��������ꗗ�̃C���f�b�N�X���w�肵�AdwValue �p�����[�^�ɂ͉����w�肵�܂���B
			/// </summary>
			CHANGECANDIDATELIST=0x0013,
			FINALIZECONVERSIONRESULT=0x0014,
			/// <summary>
			/// IME �ɁA�ϊ�������ɑ΂��鏈�������s����悤�Ɏw�����܂��BdwValue �p�����[�^�ɂ͉����w�肵�܂���B
			/// dwIndex �p�����[�^�ɂ́Aenum IME.CPS �̂����ꂩ���w�肵�܂��B
			/// </summary>
			COMPOSITIONSTR=0x0015,
			/// <summary>
			/// dwIndex �p�����[�^�ɂ́A�ύX������ꗗ���w�肵�܂��B0�`3 �͈͓̔��̒l���w�肵�Ȃ���΂Ȃ�܂���B
			/// </summary>
			SETCANDIDATE_PAGESTART=0x0016,
			/// <summary>
			/// dwIndex �p�����[�^�ɂ́A�ύX������ꗗ���w�肵�܂��B0�`3 �͈͓̔��̒l���w�肵�Ȃ���΂Ȃ�܂���B
			/// </summary>
			SETCANDIDATE_PAGESIZE=0x0017,
			/// <summary>
			/// IME �ɁA�w�肵�����j���[���������邱�Ƃ��A�v���P�[�V�����ɋ�����悤�w�����܂��B
			/// dwIndex �p�����[�^�ɂ̓��j���[�� ID ���w�肵�A
			/// dwValue �p�����[�^�ɂ͂��̃��j���[���ڗp�̃A�v���P�[�V������`�̒l���w�肵�܂��B
			/// </summary>
			IMEMENUSELECTED=0x0018		
		}
		/// <summary>
		/// dwIndex for ImmNotifyIME/NI_COMPOSITIONSTR
		/// </summary>
		public enum CPS:int{
			/// <summary>
			/// ���݂̕ϊ��������ϊ����ʂƂ��Ċm�肵�܂��B
			/// </summary>
			COMPLETE=0x0001,
			/// <summary>
			/// �ϊ��������ϊ����܂��B
			/// </summary>
			CONVERT=0x0002,
			/// <summary>
			/// ���݂̕ϊ���������������A���ϊ�������ɖ߂��܂��B
			/// </summary>
			REVERT=0x0003,
			/// <summary>
			/// �ϊ���������������A��Ԃ�ϊ�������Ȃ��ɐݒ肵�܂��B
			/// </summary>
			CANCEL=0x0004
		}
		/// <summary>
		/// IME �̓o�^�P��̏���ێ����܂��B
		/// </summary>
		[Interop.StructLayout(Interop.LayoutKind.Sequential)]
		public struct REGISTERWORD{
			/// <summary>
			/// �P��̓ǂ�
			/// </summary>
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]
			string lpReading;
			/// <summary>
			/// �P��̕\�L
			/// </summary>
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]
			string lpWord;
			/// <summary>
			/// REGISTERWORD �̐V�����C���X�^���X���쐬���܂�
			/// </summary>
			/// <param name="reading">�P��̓ǂ݂��w�肵�܂�</param>
			/// <param name="word">�P��̕\�L���w�肵�܂�</param>
			public REGISTERWORD(string reading,string word){
				this.lpReading=reading;
				this.lpWord=word;
			}
		}
		/// <summary>
		/// ImmConfigureIME() �� Dialog mode ��\���܂�
		/// </summary>
		public enum CONFIG{
			/// <summary>
			/// �v���p�e�B�_�C�A���O�{�b�N�X��\�����܂��B 
			/// </summary>
			GENERAL=1,
			/// <summary>
			/// �P��o�^�p�̃_�C�A���O�{�b�N�X��\�����܂��B
			/// </summary>
			REGISTERWORD=2,
			/// <summary>
			/// �����I��p�̃_�C�A���O�{�b�N�X��\�����܂��B
			/// </summary>
			SELECTDICTIONARY=3
		}
	}
}