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
		//TODO:対応していない物
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
			/// 候補リスト識別子。0 から 31 までの数値で指定する事。 
			/// </summary>
			int dwIndex;
			/// <summary>
			/// CFS.CANDIDATEPOS, CFS.EXCLUDE を指定
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
		/// 入力モード conversion mode を表します
		/// </summary>
		[System.Flags()]public enum CMODE:uint{
			ALPHANUMERIC=0x0000,
			NATIVE=0x0001,		CHINESE=0x0001,		HANGUL=0x0001,		JAPANESE=0x0001,
			[System.Obsolete("HANGUL を使用して下さい")]
			[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
			HANGEUL=0x0001,
			/// <summary>
			/// 片仮名。only effect under IME_CMODE_NATIVE
			/// </summary>
			KATAKANA=0x0002,
			LANGUAGE=0x0003,
			/// <summary>
			/// 全角
			/// </summary>
			FULLSHAPE=0x0008,
			/// <summary>
			/// ローマ字変換
			/// </summary>
			ROMAN=0x0010,
			CHARCODE=0x0020,
			HANJACONVERT=0x0040,
			/// <summary>
			/// ソフトキーボード
			/// </summary>
			SOFTKBD=0x0080,
			/// <summary>
			/// 無変換
			/// </summary>
			NOCONVERSION=0x0100,
			EUDC=0x0200,
			SYMBOL=0x0400,
			FIXED=0x0800,
			RESERVED=0xF0000000
		}
		/// <summary>
		/// 変換モード sentence mode を表します
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
		/// WM_IME_COMPOSITION の lParam として渡された場合、どの情報が更新されたかを示します。
		/// ImmGetCompositionString の 引数として渡す場合、どの情報を取得するか指定します。
		/// </summary>
		[System.Flags()]public enum GCS{
			/// <summary>
			/// 現在の編集文字列の読み
			/// </summary>
			COMPREADSTR=0x0001,
			/// <summary>
			/// 現在の編集文字列の読みの属性
			/// </summary>
			COMPREADATTR=0x0002,
			/// <summary>
			/// 現在の編集文字列の文節情報
			/// </summary>
			COMPREADCLAUSE=0x0004,
			/// <summary>
			/// 現在の編集文字列
			/// </summary>
			COMPSTR=0x0008,
			/// <summary>
			/// 編集文字列の属性
			/// </summary>
			COMPATTR=0x0010,
			/// <summary>
			/// 編集文字列の文節情報
			/// </summary>
			COMPCLAUSE=0x0020,
			/// <summary>
			/// 編集文字列中ののカーソルの位置
			/// </summary>
			CURSORPOS=0x0080,
			/// <summary>
			/// 編集文字列が変化した際の差分開始位置?
			/// </summary>
			DELTASTART=0x0100,
			/// <summary>
			/// 確定文字列の読み
			/// </summary>
			RESULTREADSTR=0x0200,
			/// <summary>
			/// 確定文字列の読みの文節情報
			/// </summary>
			RESULTREADCLAUSE=0x0400,
			/// <summary>
			/// 確定文字列
			/// </summary>
			RESULTSTR=0x0800,
			/// <summary>
			/// 確定文字列の文節情報
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
			//byte[] 内容;
		}//*/
		public struct CandidateList{
			/// <summary>
			/// 候補のスタイル。候補を抽出した方法
			/// </summary>
			public IME.CAND Style;
			/// <summary>
			/// 現在選択されている候補
			/// </summary>
			public int Selection;
			/// <summary>
			/// 候補ウィンドウに表示されている物で一番初めの候補の index
			/// </summary>
			public int PageStart;
			/// <summary>
			/// 候補ウィンドウに表示される候補の数
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
						throw new System.ArgumentOutOfRangeException("i",i,"指定したインデックスの要素はありません");
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
		/// 変換候補のスタイルを示します。
		/// </summary>
		public enum CAND{
			/// <summary>
			/// 不明の候補スタイル
			/// </summary>
			UNKNOWN=0x0000,
			/// <summary>
			/// 候補は同一の読みです
			/// </summary>
			READ=0x0001,
			/// <summary>
			/// 候補は同一の文字コード範囲にあります
			/// </summary>
			CODE=0x0002,
			/// <summary>
			/// 候補は同じ意味です
			/// </summary>
			MEANING=0x0003,
			/// <summary>
			/// 候補は同一の部首を使っています
			/// </summary>
			RADICAL=0x0004,
			/// <summary>
			/// 候補は同一の画数です
			/// </summary>
			STROKE=0x0005
		}
		/// <summary>
		/// WM_IME_NOTIFY の wParam
		/// </summary>
		public enum IMN{
			/// <summary>
			/// ステータスウィンドウを閉じようとしています。lParamは使われません。
			/// </summary>
			CLOSESTATUSWINDOW=0x0001,
			/// <summary>
			/// ステータスウィンドウを作ろうとしています。lParamは使われません。
			/// </summary>
			OPENSTATUSWINDOW=0x0002,
			/// <summary>
			/// IMEが候補ウィンドウの内容を変えようとしたときに送られてきます。
			/// lParamは候補リストフラグです。それぞれのビットがリストに対応しています。ビット０が最初のリストです。
			/// </summary>
			CHANGECANDIDATE=0x0003,
			/// <summary>
			/// 候補ウィンドウを閉じようとしています。lParamは候補リストフラグです。
			/// </summary>
			CLOSECANDIDATE=0x0004,
			/// <summary>
			/// 候補ウィンドウを開こうとしています。lParamは候補リストフラグです。
			/// </summary>
			OPENCANDIDATE=0x0005,
			/// <summary>
			/// 入力文字列モードが変化したときに送られてきます。lParamは使いません。
			/// </summary>
			SETCONVERSIONMODE=0x0006,
			/// <summary>
			/// 変換モードが変化したときに送られてきます。lParamは使いません。
			/// </summary>
			SETSENTENCEMODE=0x0007,
			/// <summary>
			/// IMEのON,OFF状態が変化したときに送られます。lParamは使いません。
			/// </summary>
			SETOPENSTATUS=0x0008,
			/// <summary>
			/// 候補処理が終了して候補ウィンドウを移動しようとしています。
			/// lParamは候補リストフラグです。
			/// </summary>
			SETCANDIDATEPOS=0x0009,
			/// <summary>
			/// 入力コンテキストのフォントが最新化されたときに送られてきます。
			/// lParamは使われません。
			/// </summary>
			SETCOMPOSITIONFONT=0x000A,
			/// <summary>
			/// 編集ウィンドウのスタイルや位置が変化したときに送られてきます。
			/// lParamは使われません。
			/// </summary>
			SETCOMPOSITIONWINDOW=0x000B,
			/// <summary>
			/// ステータスウィンドウの位置が変化したときに送られます。lParamは使いません。
			/// </summary>
			SETSTATUSWINDOWPOS=0x000C,
			/// <summary>
			/// エラーメッセージを表示しようとしています。lParamは使われません。
			/// </summary>
			GUIDELINE=0x000D,
			PRIVATE=0x000E
		}
		/// <summary>
		/// ImmNotifyIME の dwAction。
		/// [引用:http://msdn.microsoft.com/library/ja/default.asp?url=/library/ja/jpintl/html/_win32_ImmGetConversionStatus.asp]
		/// </summary>
		public enum NI:int{
			/// <summary>
			/// IME に、候補一覧を開くように指示します。
			/// dwIndex パラメータには、開く一覧のインデックスを指定します。DwValue パラメータには、何も指定しません。
			/// IME は、一覧を開いたらアプリケーションに IMN_OPENCANDIDATE メッセージを送信します。
			/// </summary>
			OPENCANDIDATE=0x0010,
			/// <summary>
			/// IME に、候補一覧を閉じるように指示します。
			/// dwIndex パラメータには、閉じる一覧のインデックスを指定し、dwValue パラメータには何も指定しません。
			/// IME は、一覧を閉じたらアプリケーションに IMN_CLOSECANDIDATE メッセージを送信します。
			/// </summary>
			CLOSECANDIDATE=0x0011,
			/// <summary>
			/// 変換候補の 1 つを選択します。dwIndex パラメータには、対象とする候補一覧のインデックスを指定します。
			/// dwValue パラメータには、その候補一覧での候補文字列のインデックスを指定します。
			/// </summary>
			SELECTCANDIDATESTR=0x0012,
			/// <summary>
			/// 選択されている候補一覧を変更します。
			/// dwIndex パラメータには、選択する候補一覧のインデックスを指定し、dwValue パラメータには何も指定しません。
			/// </summary>
			CHANGECANDIDATELIST=0x0013,
			FINALIZECONVERSIONRESULT=0x0014,
			/// <summary>
			/// IME に、変換文字列に対する処理を実行するように指示します。dwValue パラメータには何も指定しません。
			/// dwIndex パラメータには、enum IME.CPS のいずれかを指定します。
			/// </summary>
			COMPOSITIONSTR=0x0015,
			/// <summary>
			/// dwIndex パラメータには、変更する候補一覧を指定します。0～3 の範囲内の値を指定しなければなりません。
			/// </summary>
			SETCANDIDATE_PAGESTART=0x0016,
			/// <summary>
			/// dwIndex パラメータには、変更する候補一覧を指定します。0～3 の範囲内の値を指定しなければなりません。
			/// </summary>
			SETCANDIDATE_PAGESIZE=0x0017,
			/// <summary>
			/// IME に、指定したメニューを処理することをアプリケーションに許可するよう指示します。
			/// dwIndex パラメータにはメニューの ID を指定し、
			/// dwValue パラメータにはそのメニュー項目用のアプリケーション定義の値を指定します。
			/// </summary>
			IMEMENUSELECTED=0x0018		
		}
		/// <summary>
		/// dwIndex for ImmNotifyIME/NI_COMPOSITIONSTR
		/// </summary>
		public enum CPS:int{
			/// <summary>
			/// 現在の変換文字列を変換結果として確定します。
			/// </summary>
			COMPLETE=0x0001,
			/// <summary>
			/// 変換文字列を変換します。
			/// </summary>
			CONVERT=0x0002,
			/// <summary>
			/// 現在の変換文字列を取り消し、未変換文字列に戻します。
			/// </summary>
			REVERT=0x0003,
			/// <summary>
			/// 変換文字列を消去し、状態を変換文字列なしに設定します。
			/// </summary>
			CANCEL=0x0004
		}
		/// <summary>
		/// IME の登録単語の情報を保持します。
		/// </summary>
		[Interop.StructLayout(Interop.LayoutKind.Sequential)]
		public struct REGISTERWORD{
			/// <summary>
			/// 単語の読み
			/// </summary>
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]
			string lpReading;
			/// <summary>
			/// 単語の表記
			/// </summary>
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]
			string lpWord;
			/// <summary>
			/// REGISTERWORD の新しいインスタンスを作成します
			/// </summary>
			/// <param name="reading">単語の読みを指定します</param>
			/// <param name="word">単語の表記を指定します</param>
			public REGISTERWORD(string reading,string word){
				this.lpReading=reading;
				this.lpWord=word;
			}
		}
		/// <summary>
		/// ImmConfigureIME() の Dialog mode を表します
		/// </summary>
		public enum CONFIG{
			/// <summary>
			/// プロパティダイアログボックスを表示します。 
			/// </summary>
			GENERAL=1,
			/// <summary>
			/// 単語登録用のダイアログボックスを表示します。
			/// </summary>
			REGISTERWORD=2,
			/// <summary>
			/// 辞書選択用のダイアログボックスを表示します。
			/// </summary>
			SELECTDICTIONARY=3
		}
	}
}