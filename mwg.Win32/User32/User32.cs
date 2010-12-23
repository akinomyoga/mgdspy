using Frms=System.Windows.Forms;
using Interop=System.Runtime.InteropServices;
using CM=System.ComponentModel;

namespace mwg.Win32{
	/// <summary>
	/// ウィンドウなどのユーザーインターフェイスに関する関数群を公開するクラスです。
	/// </summary>
	public partial class User32{
		/// <summary>
		/// 画面上のすべてのトップレベルウィンドウを列挙します。
		/// この関数を呼び出すと、各ウィンドウのハンドルが順々にアプリケーション定義のコールバック関数に渡されます。
		/// EnumWindows 関数は、すべてのトップレベルリンドウを列挙し終えるか、
		/// またはアプリケーション定義のコールバック関数から 0（FALSE）が返されるまで処理を続けます。
		/// </summary>
		/// <param name="lpEnumFunc">アプリケーション定義のコールバック関数へのポインタを指定します。
		/// 詳細については、EnumWindowsProc 関数の説明を参照してください。</param>
		/// <param name="lParam">コールバック関数に渡すアプリケーション定義の値を指定します。</param>
		/// <returns>関数が成功すると true を返します。拡張エラー情報は GetLastError で取得出来ます。</returns>
		[Interop.DllImport("user32.dll",SetLastError=true)]
		[return: Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc,System.IntPtr lParam);
		/// <summary>
		/// 子ウィンドウのハンドルを取得します。
		/// </summary>
		/// <param name="hwndParent">親ウィンドウのハンドルを指定します。</param>
		/// <param name="lpEnumFunc">
		/// 子ウィンドウのハンドルを処理する関数を指定します。
		/// 処理に成功した場合には true を返します。
		/// 処理に失敗した場合には false を返します。
		/// </param>
		/// <param name="lParam">lpEnumFunc で指定した関数に渡す引数を指定します。</param>
		/// <returns>関数が成功したか否かを返します。拡張エラー情報は GetLastError で取得出来ます。</returns>
		[Interop.DllImport("user32.dll",SetLastError=true)]
		[return: Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		public static extern bool EnumChildWindows(System.IntPtr hwndParent,EnumWindowsProc lpEnumFunc,System.IntPtr lParam);

		[return: Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		public delegate bool EnumWindowsProc(System.IntPtr hWnd,System.IntPtr lParam);
		//[Interop.DllImport("user32.dll",EntryPoint="EnumWindows")]
		//[return: Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		//public static extern bool EnumWindowsRf(EnumWindowsProcRf lpEnumFunc,ref System.IntPtr lParam);
		//public delegate bool EnumWindowsProcRf(System.IntPtr hWnd,ref System.IntPtr lParam);

		[Interop::DllImport("user32.dll",SetLastError=true,CharSet=Interop::CharSet.Auto)]
		public static extern WM RegisterWindowMessage(string lpString);


		#region WindowLongPtr
		private static Interop::HandleRef Window2Handle(Frms::IWin32Window wnd){
			return new Interop::HandleRef(wnd,wnd.Handle);
		}
		/// <summary>
		/// ウィンドウの拡張ウィンドウメモリ内の指定されたオフセット位置にある値を取得します。
		/// この関数は、GetWindowLong 関数の改訂版です。32 ビット版 Windows と 64 ビット版 Windows の両方ともと互換性のあるコードを記述するには、GetWindowLongPtr 関数を使ってください。
		/// </summary>
		/// <param name="wnd">ウィンドウを指定します。</param>
		/// <param name="nIndex">取得する値の 0 から始まるオフセットを指定します。有効なオフセットは、0 から拡張ウィンドウメモリのバイト数 -8 までです。たとえば、拡張メモリが 24 バイト以上ある場合、16 を指定すると、3 番目の整数値が取得できます。</param>
		/// <returns>関数が成功すると、要求した値が返ります。関数が失敗すると、0 が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。</returns>
		/// <remarks>拡張ウィンドウメモリは、RegisterClassEx 関数に渡す WNDCLASSEX 構造体の cbWndExtra メンバに 0 以外の値を設定することによって確保します。</remarks>
		public static System.IntPtr GetWindowLongPtr(Frms::IWin32Window wnd,int nIndex){
			Interop::HandleRef hWnd=Window2Handle(wnd);
			return System.IntPtr.Size==8?GetWindowLongPtr64(hWnd,nIndex):(System.IntPtr)GetWindowLongPtr32(hWnd,nIndex);
		}
		/// <summary>
		/// 指定されたウィンドウに関する情報を取得します。
		/// </summary>
		/// <param name="wnd">ウィンドウを指定します。</param>
		/// <param name="nIndex">取得する情報に対応する値を指定します。</param>
		/// <returns>関数が成功すると、要求した値が返ります。関数が失敗すると、0 が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。</returns>
		/// <remarks>拡張ウィンドウメモリは、RegisterClassEx 関数に渡す WNDCLASSEX 構造体の cbWndExtra メンバに 0 以外の値を設定することによって確保します。</remarks>
		public static System.IntPtr GetWindowLongPtr(Frms::IWin32Window wnd,GWLP nIndex) {
			Interop::HandleRef hWnd=Window2Handle(wnd);
			if(System.IntPtr.Size==8) {
				int index=nIndex==GWLP.USER?16:nIndex==GWLP.DLGPROC?8:(int)nIndex;
				return GetWindowLongPtr64(hWnd,index);
			}else{
				return (System.IntPtr)GetWindowLongPtr32(hWnd,(int)nIndex);
			}
		}
		/// <summary>
		/// ウィンドウの拡張ウィンドウメモリ内の指定されたオフセット位置にある値を取得します。
		/// この関数は、GetWindowLong 関数の改訂版です。32 ビット版 Windows と 64 ビット版 Windows の両方ともと互換性のあるコードを記述するには、GetWindowLongPtr 関数を使ってください。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドルを指定します。</param>
		/// <param name="nIndex">取得する値の 0 から始まるオフセットを指定します。有効なオフセットは、0 から拡張ウィンドウメモリのバイト数 -8 までです。たとえば、拡張メモリが 24 バイト以上ある場合、16 を指定すると、3 番目の整数値が取得できます。</param>
		/// <returns>関数が成功すると、要求した値が返ります。関数が失敗すると、0 が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。</returns>
		/// <remarks>拡張ウィンドウメモリは、RegisterClassEx 関数に渡す WNDCLASSEX 構造体の cbWndExtra メンバに 0 以外の値を設定することによって確保します。</remarks>
		public static System.IntPtr GetWindowLongPtr(System.IntPtr hWnd,int nIndex){
			return System.IntPtr.Size==8?GetWindowLongPtr64(hWnd,nIndex):(System.IntPtr)GetWindowLongPtr32(hWnd,nIndex);
		}
		/// <summary>
		/// 指定されたウィンドウに関する情報を取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドルを指定します。</param>
		/// <param name="nIndex">取得する情報に対応する値を指定します。</param>
		/// <returns>関数が成功すると、要求した値が返ります。関数が失敗すると、0 が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。</returns>
		/// <remarks>拡張ウィンドウメモリは、RegisterClassEx 関数に渡す WNDCLASSEX 構造体の cbWndExtra メンバに 0 以外の値を設定することによって確保します。</remarks>
		public static System.IntPtr GetWindowLongPtr(System.IntPtr hWnd,GWLP nIndex){
			if(System.IntPtr.Size==8){
				int index=nIndex==GWLP.USER?16:nIndex==GWLP.DLGPROC?8:(int)nIndex;
				return GetWindowLongPtr64(hWnd,index);
			}else{
				return (System.IntPtr)GetWindowLongPtr32(hWnd,(int)nIndex);
			}
		}
		[Interop.DllImport("user32.dll",EntryPoint="GetWindowLong")]
		private static extern int GetWindowLongPtr32(System.IntPtr hWnd,int nIndex);
		[Interop.DllImport("user32.dll",EntryPoint="GetWindowLong")]
		private static extern int GetWindowLongPtr32(Interop::HandleRef hWnd,int nIndex);
		[Interop.DllImport("user32.dll",EntryPoint="GetWindowLongPtr")]
		private static extern System.IntPtr GetWindowLongPtr64(System.IntPtr hWnd, int nIndex);
		[Interop.DllImport("user32.dll",EntryPoint="GetWindowLongPtr")]
		private static extern System.IntPtr GetWindowLongPtr64(Interop::HandleRef hWnd,int nIndex);
		/// <summary>
		/// ウィンドウの拡張ウィンドウメモリ内の指定されたオフセット位置にある値を取得します。
		/// この関数は、GetWindowLong 関数の改訂版です。32 ビット版 Windows と 64 ビット版 Windows の両方ともと互換性のあるコードを記述するには、GetWindowLongPtr 関数を使ってください。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドルを指定します。</param>
		/// <param name="nIndex">拡張ウィンドウメモリ内の値を変更するには、0 から始まるオフセットを指定します。有効なオフセットは、0 から拡張ウィンドウメモリのバイト数 -8 までです。たとえば、拡張メモリが 24 バイト以上ある場合、16 を指定すると、3 番目の整数値を変更できます。</param>
		/// <param name="dwNewLong">新しい値を指定します。</param>
		/// <returns>関数が成功すると、変更した情報の変更前の値が返ります。関数が失敗すると、0 が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。</returns>
		/// <remarks>拡張ウィンドウメモリは、RegisterClassEx 関数に渡す WNDCLASSEX 構造体の cbWndExtra メンバに 0 以外の値を設定することによって確保します。</remarks>
		public static System.IntPtr SetWindowLongPtr(System.IntPtr hWnd,int nIndex,System.IntPtr dwNewLong){
			return System.IntPtr.Size==8?SetWindowLongPtr64(hWnd,nIndex,dwNewLong):(System.IntPtr)SetWindowLongPtr32(hWnd,nIndex,dwNewLong);
		}
		/// <summary>
		/// 指定されたウィンドウに関する情報を取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドルを指定します。</param>
		/// <param name="nIndex">設定する情報に対応する値を指定します。</param>
		/// <param name="dwNewLong">新しい値を指定します。</param>
		/// <returns>関数が成功すると、変更した情報の変更前の値が返ります。関数が失敗すると、0 が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。</returns>
		/// <remarks>拡張ウィンドウメモリは、RegisterClassEx 関数に渡す WNDCLASSEX 構造体の cbWndExtra メンバに 0 以外の値を設定することによって確保します。</remarks>
		public static System.IntPtr SetWindowLongPtr(System.IntPtr hWnd,GWLP nIndex,System.IntPtr dwNewLong){
			if(System.IntPtr.Size==8){
				int index=nIndex==GWLP.USER?16:nIndex==GWLP.DLGPROC?8:(int)nIndex;
				return SetWindowLongPtr64(hWnd,index,dwNewLong);
			}else{
				return (System.IntPtr)SetWindowLongPtr32(hWnd,(int)nIndex,dwNewLong);
			}
		}
		[Interop.DllImport("user32.dll",EntryPoint="SetWindowLong",SetLastError=true)]
		private static extern int SetWindowLongPtr32(System.IntPtr hWnd,int nIndex,System.IntPtr dwNewLong);
		[Interop.DllImport("user32.dll",EntryPoint="SetWindowLongPtr",SetLastError=true)]
		private static extern System.IntPtr SetWindowLongPtr64(System.IntPtr hWnd, int nIndex,System.IntPtr dwNewLong);
		/// <summary>
		/// この関数は、GetWindowLongPtr 関数の古い版です。32 ビット版 Windows と 64 ビット版 Windows の両方ともと互換性のあるコードを記述するには、GetWindowLongPtr 関数を使ってください。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドルを指定します。</param>
		/// <param name="nIndex">取得する値の 0 から始まるオフセットを指定します。</param>
		/// <returns>関数が成功すると、要求した値が返ります。関数が失敗すると、0 が返ります。</returns>
		[Interop.DllImport("user32.dll"),System.Obsolete("GetWindowLongPtr を使って下さい。")]
		public static extern int GetWindowLong(System.IntPtr hWnd,int nIndex);
		/// <summary>
		/// この関数は、SetWindowLongPtr 関数の古い版です。32 ビット版 Windows と 64 ビット版 Windows の両方ともと互換性のあるコードを記述するには、GetWindowLongPtr 関数を使ってください。
		/// </summary>
		/// <param name="hWnd">ウィンドウのハンドルを指定します。</param>
		/// <param name="nIndex">設定する値の 0 から始まるオフセットを指定します。</param>
		/// <param name="dwNewLong">新しい値を設定します。</param>
		/// <returns>関数が成功すると、要求した値が返ります。関数が失敗すると、0 が返ります。</returns>
		[Interop.DllImport("user32.dll"),System.Obsolete("SetWindowLongPtr を使って下さい。")]
		public static extern int SetWindowLong(System.IntPtr hWnd,int nIndex,System.IntPtr dwNewLong);
		/// <summary>
		/// GetWindowLongPtr 及び SetWindowLongPtr を使用して、
		/// ウィンドウに関する情報を取得亦は設定する際に指定する値です。
		/// </summary>
		public enum GWLP:int{
			/// <summary>
			/// 拡張ウィンドウスタイルを取得・設定します。 
			/// </summary>
			EXSTYLE=-20,
			/// <summary>
			/// ウィンドウスタイルを取得・設定します。 
			/// </summary>
			STYLE=-16,
			/// <summary>
			/// ウィンドウプロシージャへのポインタ、またはウィンドウプロシージャへのポインタを表すハンドルを取得・設定します。ウィンドウプロシージャを呼び出すには、CallWindowProc 関数を使わなければなりません。 
			/// </summary>
			/// <remarks>
			/// SetWindowLongPtr 関数によりウィンドウプロシージャを変更する場合、新しいウィンドウプロシージャは、WindowProc コールバック関数の説明に示されているガイドラインに従っていなければなりません。
			/// この値を指定して SetWindowLongPtr 関数を呼び出すと、ウィンドウクラスのサブクラスが作成され、それがウィンドウの作成に使われるようになります。
			/// システムクラスのサブクラスは作成してもかまいませんが、他のプロセスで生成されたウィンドウクラスのサブクラスは作成しないようにしてください。
			/// SetWindowLongPtr 関数は、ウィンドウクラスに関連付けられたウィンドウプロシージャを変更することによりウィンドウクラスをサブクラス化するため、それ以降システムは変更前のウィンドウプロシージャではなく新しいウィンドウプロシージャを呼び出すようになります。
			/// 新しいウィンドウプロシージャでは処理されないようなメッセージは、CallWindowProc 関数を呼び出すことによって変更前のウィンドウプロシージャへ渡さなければなりません。これは、アプリケーションがウィンドウプロシージャのチェインを作ることを可能にします。
			/// </remarks>
			WNDPROC=-4,
			/// <summary>
			/// アプリケーションインスタンスのハンドルを取得・設定します。 
			/// </summary>
			HINSTANCE=-6,
			/// <summary>
			/// 親ウィンドウがある場合、そのハンドルを取得します。
			/// </summary>
			/// <remarks>
			/// この値を指定して SetWindowLongPtr 関数を呼び出すことにより子ウィンドウの親を変更してはなりません。
			/// 親ウィンドウの変更には、SetParent 関数を使ってください。
			/// </remarks>
			HWNDPARENT=-8,
			/// <summary>
			/// ウィンドウ ID を取得・設定します。 
			/// </summary>
			ID=-12,
			/// <summary>
			/// ウィンドウに関連付けられた 32 ビット値を取得・設定します。この 32 ビット値は、ウィンドウを作成したアプリケーションで使用する目的で各ウィンドウが持っているものです。この値の初期値は 0 です。 
			/// </summary>
			USERDATA=-21,
			/// <summary>
			/// hWnd パラメータでダイアログボックスを指定しているとき指定できます。 
			/// ダイアログボックスプロシージャへのポインタ、またはダイアログボックスプロシージャへのポインタを表すハンドルを取得・設定します。ダイアログボックスプロシージャを呼び出すには、CallWindowProc 関数を使わなければなりません。 
			/// </summary>
			DLGPROC=4,
			/// <summary>
			/// hWnd パラメータでダイアログボックスを指定しているとき指定できます。 
			/// ダイアログボックスプロシージャ内で処理されたメッセージの戻り値を取得・設定します。 
			/// </summary>
			MSGRESULT=0,
			/// <summary>
			/// hWnd パラメータでダイアログボックスを指定しているとき指定できます。 
			/// ハンドルやポインタなどのアプリケーション固有の拡張情報を取得・設定します。 
			/// </summary>
			USER=8
		}
		#endregion

		#region WindowInfo
		[Interop.DllImport("user32.dll")]
		public static extern bool GetWindowInfo(System.IntPtr hwnd, ref WINDOWINFO pwi);
		#endregion

		#region Parent
		/// <summary>
		/// 指定された子ウィンドウの親ウィンドウを変更します。
		/// </summary>
		/// <param name="hWndChild">子ウィンドウのハンドルを指定します。</param>
		/// <param name="hWndNewParent">新しい親ウィンドウを指定します。NULL を指定すると、デスクトップウィンドウが新しい親ウィンドウになります。
		/// Windows 2000：このパラメータに HWND_MESSAGE を指定すると、子ウィンドウはメッセージ専用ウィンドウ になります。</param>
		/// <returns>関数が成功すると、直前の親ウィンドウのハンドルが返ります。
		/// 関数が失敗すると、NULL が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。</returns>
		/// <remarks>
		/// SetParent 関数を使うと、ポップアップウィンドウ、オーバーラップウィンドウ、子ウィンドウの親ウィンドウを設定できます。
		/// 新しい親ウィンドウと子ウィンドウは、同一アプリケーションに属していなければなりません。
		/// hWndChild パラメータで指定したウィンドウが画面に表示されている場合は、システムにより適切な再描画が実行されます。
		/// <para>
		/// SetParent 関数は、互換性の理由から、親ウィンドウが変更されたウィンドウのウィンドウスタイル WS_CHILD と WS_POPUP の変更は行いません。
		/// このため、hWndNewParent パラメータに NULL を指定する場合は、SetParent 関数を呼び出した後に WS_CHILD フラグをクリアし、WS_POPUP フラグをセットしなければなりません。
		/// 逆に、デスクトップの子ウィンドウに対して hWndNewParent パラメータに NULL 以外の値を指定して SetParent 関数を呼び出す場合は、関数を呼び出す前に WS_POPUP フラグをクリアし、WS_CHILD フラグをセットしておかなければなりません。
		/// </para>
		/// <para>
		/// Windows 2000：ウィンドウの親を変更する場合は、両方のウィンドウの UISTATE を同期させなければなりません。
		/// 詳細については、WM_CHANGEUISTATE メッセージおよび WM_UPDATEUISTATE メッセージの説明を参照してください。
		/// </para>
		/// </remarks>
		[Interop.DllImport("user32.dll")]
		static extern System.IntPtr SetParent(System.IntPtr hWndChild,System.IntPtr hWndNewParent);
		/// <summary>
		/// 指定された子ウィンドウの親ウィンドウまたはオーナーウィンドウのハンドルを返します。
		/// </summary>
		/// <param name="hWnd">子ウィンドウのハンドルを指定します。</param>
		/// <returns>指定したウィンドウが子ウィンドウの場合は、親ウィンドウのハンドルが返ります。
		/// 指定したウィンドウがトップレベルウィンドウの場合は、オーナーウィンドウのハンドルが返ります。
		/// 指定したウィンドウがトップレベルのオーナーを持たないウィンドウだった場合、および関数が失敗した場合は NULL が返ります。
		/// 拡張エラー情報を取得するには、GetLastError 関数を使います。</returns>
		/// <remarks>この関数の名前は GetParent ですが、親ウィンドウではなくオーナーウィンドウが返される場合もあります。
		/// 親ウィンドウだけを取得したい場合は、GA_PARENT フラグを指定して GetAncestor 関数を呼び出してください。</remarks>
		[Interop.DllImport("user32.dll",ExactSpelling=true,CharSet=Interop.CharSet.Auto)]
		public static extern System.IntPtr GetParent(System.IntPtr hWnd);
		/// <summary>
		/// 指定したウィンドウの祖先のハンドルを取得します。
		/// </summary>
		/// <param name="hwnd">祖先を取得するウィンドウのハンドルを指定します。デスクトップウィンドウのハンドルを指定すると、NULL が返ります。</param>
		/// <param name="gaFlags">取得する祖先を指定します。</param>
		/// <returns>祖先のウィンドウのハンドルが返ります。</returns>
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr GetAncestor(System.IntPtr hwnd,GA gaFlags);
		/// <summary>
		/// 取得したい先祖の種類を指定するのに使用します。
		/// </summary>
		public enum GA{
			///<summary>
			///親ウィンドウを取得します。これには、GetParent 関数で取得されるような、オーナーウィンドウは含みません。 
			///</summary>
			PARENT=1,
			///<summary>
			///親ウィンドウのチェーンをたどってルートウィンドウを取得します。 
			///</summary>
			ROOT=2,
			///<summary>
			///GetParent 関数が返す親ウィンドウのチェーンをたどって所有されているルートウィンドウを取得します。 
			///</summary>
			ROOTOWNER=3
		}
		#endregion

		#region func:Is*
		/// <summary>
		/// 指定したウィンドウが最小化されているかどうかを取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウをハンドルで指定します。</param>
		/// <returns>指定されたウィンドウが最小化されている場合に true を返します。</returns>
		[Interop.DllImport("user32.dll")]
		[return:Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		public static extern bool IsIconic(System.IntPtr hWnd);
		/// <summary>
		/// 指定されたウィンドウが最大化されているかどうかを取得します。
		/// </summary>
		/// <param name="hWnd">ウィンドウをハンドルで指定します。</param>
		/// <returns>指定されたウィンドウが最大化されている場合に true を返します。</returns>
		[Interop.DllImport("user32.dll")]
		[return:Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		public static extern bool IsZoomed(System.IntPtr hWnd);
		/// <summary>
		/// 指定されたウィンドウハンドルを持つウィンドウが存在しているかどうかを調べます。
		/// </summary>
		/// <param name="hWnd">調査するウィンドウハンドルを指定します。 </param>
		/// <returns>指定したウィンドウハンドルを持つウィンドウが存在している場合に true を返します。</returns>
		[Interop.DllImport("user32.dll")]
		public static extern bool IsWindow(System.IntPtr hWnd);
		//[Interop.DllImport("user32.dll")]
		//public static extern bool IsWindowEnabled(System.IntPtr hWnd);
		/// <summary>
		/// 指定したウィンドウの表示状態を取得します。
		/// </summary>
		/// <param name="hWnd">調査するウィンドウのハンドルを指定します。</param>
		/// <returns>
		/// 指定したウィンドウと、その先祖に当たるウィンドウの全てが WS_VISIBLE スタイルを持つ場合に true を返します。
		/// 戻り値が true であっても、ウィンドウ自体は別のウィンドウに隠されて見えない場合もあります。
		/// </returns>
		[Interop.DllImport("user32.dll")]
		public static extern bool IsWindowVisible(System.IntPtr hWnd);
		/// <summary>
		/// 指定したウィンドウが Unicode のネイティブウィンドウであるかどうかを調べます。
		/// </summary>
		/// <param name="hWnd">調査するウィンドウのハンドルを指定します。</param>
		/// <returns>
		/// 指定したウィンドウが Unicode のネイティブウィンドウである場合に true を返します。
		/// RegisterClassA を使ってウィンドウクラスを登録した場合には ANSI ネイティブになります。
		/// RegisterClassW を使ってウィンドウクラスを登録した場合には Unicode ネイティブになります。
		/// </returns>
		[Interop.DllImport("user32.dll")]
		public static extern bool IsWindowUnicode(System.IntPtr hWnd);
		/// <summary>
		/// 指定されたウィンドウが、別に指定されたウィンドウの子ウィンドウ（または子孫のウィンドウ）であるかどうかを調べます。
		/// 親ウィンドウが子ウィンドウの親ウィンドウチェインに入っている場合、その子ウィンドウは指定された親ウィンドウの直接の子孫です。
		/// 親ウィンドウチェインは、子ウィンドウの元のオーバーラップウィンドウまたはポップアップウィンドウからつながっています。
		/// </summary>
		/// <param name="hWndParent">親ウィンドウのハンドルを指定します。 </param>
		/// <param name="hWnd">調査するウィンドウのハンドルを指定します。</param>
		/// <returns>調査対象ウィンドウが指定した親ウィンドウの子ウィンドウまたは子孫ウィンドウの場合に true を返します。</returns>
		[Interop.DllImport("user32.dll")]
		public static extern bool IsChild(System.IntPtr hWndParent,System.IntPtr hWnd);
		#endregion

		#region 文字列処理
		/// <summary>
		/// 指定された文字が英字かどうかを判断します。
		/// ユーザーがセットアップの際に選択した言語、またはコントロールパネルで選択した言語に基づいて、この決定を行います。
		/// </summary>
		/// <param name="c">テストの対象の文字を指定します。</param>
		/// <returns>指定した文字が英字である場合に true を返します。</returns>
		public static bool IsCharAlpha(char c){return char.IsLetter(c);}
		/// <summary>
		/// 指定した文字が英字亦は数字かどうかを判断します。
		/// </summary>
		/// <param name="c">テストの対象の文字を指定します。</param>
		/// <returns>指定した文字が英字亦は数字である場合に true を返します。</returns>
		public static bool IsCharAlphaNumeric(char c){return char.IsLetterOrDigit(c);}
		/// <summary>
		/// 指定した文字が大文字かどうかを判断します。
		/// </summary>
		/// <param name="c">テストの対象の文字を指定します。</param>
		/// <returns>指定した文字が大文字である場合に true を返します。</returns>
		public static bool IsCharUpper(char c){return char.IsUpper(c);}
		/// <summary>
		/// 指定した文字が小文字かどうかを判断します。
		/// </summary>
		/// <param name="c">テストの対象の文字を指定します。</param>
		/// <returns>指定した文字が小文字である場合に true を返します。</returns>
		public static bool IsCharLower(char c){return char.IsLower(c);}
		/// <summary>
		/// 小文字を大文字に変換します。
		/// </summary>
		/// <param name="c">変換前の文字を指定します。変換の結果がここに格納されます。</param>
		/// <returns>変換後の文字を返します。</returns>
		public static char CharUpper(ref char c){return c=char.ToUpper(c);}
		/// <summary>
		/// 指定した文字列に含まれる小文字を大文字に変換します。
		/// </summary>
		/// <param name="lpsz">変換前の文字列を指定します。変換の結果がここに格納されます。</param>
		/// <returns>変換後の文字列を返します。</returns>
		public static string CharUpper(ref string lpsz){return lpsz=lpsz.ToUpper();}
		/// <summary>
		/// 指定した文字列の指定した数の文字を小文字から大文字に変換します。
		/// </summary>
		/// <param name="lpsz">変換前の文字列を指定します。変換後の文字列が格納されます。</param>
		/// <param name="cchLength">変換する文字数を指定します。</param>
		/// <returns>実際に処理された文字数を返します。(小文字でなかった文字も含みます。)</returns>
		public static uint CharUpperBuff(ref string lpsz,uint cchLength){
			int len=lpsz.Length;
			if(len>cchLength){
				lpsz=lpsz.Substring(0,len).ToUpper()+lpsz.Substring(len);
				return cchLength;
			}else{
				lpsz=lpsz.ToUpper();
				return (uint)len;
			}
		}
		/// <summary>
		/// 大文字を小文字に変換します。
		/// </summary>
		/// <param name="c">変換前の文字を指定します。変換の結果がここに格納されます。</param>
		/// <returns>変換後の文字を返します。</returns>
		public static char CharLower(ref char c){return c=char.ToLower(c);}
		/// <summary>
		/// 指定した文字列に含まれる大文字を小文字に変換します。
		/// </summary>
		/// <param name="lpsz">変換前の文字列を指定します。変換の結果がここに格納されます。</param>
		/// <returns>変換後の文字列を返します。</returns>
		public static string CharLower(ref string lpsz){return lpsz=lpsz.ToLower();}
		/// <summary>
		/// 指定した文字列の指定した数の文字を大文字から小文字に変換します。
		/// </summary>
		/// <param name="lpsz">変換前の文字列を指定します。変換後の文字列が格納されます。</param>
		/// <param name="cchLength">変換する文字数を指定します。</param>
		/// <returns>実際に処理された文字数を返します。(大文字でなかった文字も含みます。)</returns>
		public static uint CharLowerBuff(ref string lpsz,uint cchLength){
			int len=lpsz.Length;
			if(len>cchLength){
				lpsz=lpsz.Substring(0,len).ToLower()+lpsz.Substring(len);
				return cchLength;
			}else{
				lpsz=lpsz.ToLower();
				return (uint)len;
			}
		}
		/// <summary>
		/// 文字列の中の次の文字へのポインタを取得します。
		/// 文字列内の文字の列挙を行いたいのなら str[index] (String string) を使用するか、
		/// enm.MoveNext (Enumerator enm=str.GetEnumerator) を使用して下さい。
		/// </summary>
		/// <param name="lpsz">NULL で終わる文字列の中の 1 つの文字へのポインタを指定します。 </param>
		/// <returns>
		/// 指定した文字列の中の次の文字へのポインタが返ります。lpsz パラメータで、文字列の中の最後の文字を指定した場合、NULL 文字へのポインタが返ります。
		/// lpsz パラメータで、文字列の最後の NULL 文字を指定した場合、lpsz パラメータと同じ値が返ります。
		/// </returns>
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr CharNext(System.IntPtr lpsz);
		/// <summary>
		/// 文字列の中の次の文字へのポインタを取得します。
		/// 文字列内の文字の列挙を行いたいのなら str[index] (String string) を使用して下さい。
		/// </summary>
		/// <param name="lpszStart">NULL で終わる文字列の中の最初の文字へのポインタを指定します。</param>
		/// <param name="lpszCurrent">NULL で終わる文字列の中の 1 つの文字へのポインタを指定します。この文字の 1 つ前の文字へのポインタが返ります。</param>
		/// <returns>
		/// 指定した文字列の中の次の文字へのポインタが返ります。lpsz パラメータで、文字列の中の最後の文字を指定した場合、NULL 文字へのポインタが返ります。
		/// lpsz パラメータで、文字列の最後の NULL 文字を指定した場合、lpsz パラメータと同じ値が返ります。
		/// </returns>
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr CharPrev(System.IntPtr lpszStart,System.IntPtr lpszCurrent);
		/// <summary>
		/// 指定された文字列を、OEM 定義の文字セットへ変換します。.NET では意味ありません。
		/// </summary>
		/// <param name="lpszSrc">変換するべき、NULL 文字で終わる文字列へのポインタを指定します。 </param>
		/// <param name="lpszDst">
		/// 1 個のバッファへのポインタを指定します。関数から制御が返ると、このバッファに、変換後の文字列が格納されます。ANSI 版の CharToOem 関数を使う場合、lpszDst パラメータで lpszSrc パラメータと同じアドレスを指定すると、その場で変換を行い、元の文字列を上書きします。
		/// Unicode 版の CharToOem 関数を使う場合、lpszSrc パラメータと同じアドレスを指定することはできません。</param>
		/// <returns>常に true が返ります。</returns>
		[Interop.DllImport("user32.dll"),System.Obsolete(".NET の文字列に対してこの操作をする事は全く意味を為しません。")]
		public static extern bool CharToOem(
			[Interop.MarshalAs(Interop.UnmanagedType.LPTStr)]string lpszSrc,
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]string lpszDst
			);
		/// <summary>
		/// 指定された文字列を、OEM 定義の文字セットへ変換します。.NET では意味ありません。
		/// </summary>
		/// <param name="lpszSrc">変換するべき、NULL 文字で終わる文字列へのポインタを指定します。 </param>
		/// <param name="lpszDst">
		/// 1 個のバッファへのポインタを指定します。関数から制御が返ると、このバッファに、変換後の文字列が格納されます。ANSI 版の CharToOemBuff 関数を使う場合、lpszDst パラメータで lpszSrc パラメータと同じアドレスを指定すると、その場で変換を行い、元の文字列を上書きします。
		/// Unicode 版の CharToOemBuff 関数を使う場合、lpszSrc パラメータと同じアドレスを指定することはできません。
		/// </param>
		/// <param name="cchDstLength">lpszSrc パラメータで指定された文字列のうち、変換するべき文字の数を TCHAR 単位で指定します。</param>
		/// <returns>常に true が返ります。</returns>
		[Interop.DllImport("user32.dll"),System.Obsolete(".NET の文字列に対してこの操作をする事は全く意味を為しません。")]
		public static extern bool CharToOemBuff(
			[Interop.MarshalAs(Interop.UnmanagedType.LPTStr)]string lpszSrc,
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]ref string lpszDst,
			uint cchDstLength
			);
		/// <summary>
		/// 指定されたモジュールに関連付けられている実行可能ファイルから文字列リソースをロードし、バッファへコピーし、最後に 1 つの NULL 文字を追加します。
		/// </summary>
		/// <param name="hInstance">モジュールインスタンスのハンドルを指定します。このモジュールの実行可能ファイルは、ロードするべき文字列のリソースを保持しています。</param>
		/// <param name="uID">ロードするべき文字列の整数の識別子を指定します。</param>
		/// <param name="lpBuffer">バッファへのポインタを指定します。関数から制御が返ると、このバッファに、NULL で終わる文字列が格納されます。</param>
		/// <param name="nBufferMax">バッファのサイズを TCHAR 単位で指定します。バッファのサイズが不足して、指定された文字列の一部を格納できない場合、文字列は途中で切り捨てられます。</param>
		/// <returns>関数が成功すると、バッファにコピーされた文字の数が TCHAR 単位で返ります（終端の NULL は含まない）。文字列リソースが存在しない場合、0 が返ります。拡張エラー情報を取得するには、GetLastError 関数を使います。</returns>
		[Interop.DllImport("user32.dll",SetLastError=true)]
		public static extern int LoadString(System.IntPtr hInstance,uint uID,ref string lpBuffer,int nBufferMax);
		//TODO:CharNextExA CharPrevExA CompareString FoldString GetStringTypeEx GetStringType wsprintf
		//http://msdn.microsoft.com/library/ja/default.asp?url=/library/ja/vclib/html/_crt_va_arg.2c_.va_end.2c_.va_start.asp
		//http://www.microsoft.com/japan/msdn/net/general/win32map.aspx
#if obsolete16
		public static string AnsiUpper(ref char c){return CharUpper(c);}
		public static string AnsiUpper(ref string lpsz){return CharUpper(lpsz);}
		public static uint AnsiUpperBuff(ref string lpsz,uint cchLength){return CharUpperBuff(lpsz,cchLength);}
		public static string AnsiLower(ref char c){return CharUpper(c);}
		public static string AnsiLower(ref string lpsz){return CharUpper(lpsz);}
		public static uint AnsiLowerBuff(ref string lpsz,uint cchLength){return CharUpperBuff(lpsz,cchLength);}
		public static System.IntPtr AnsiNext(System.IntPtr lpsz){return CharNext(lpsz);}
		public static System.IntPtr AnsiPrev(System.IntPtr lpszStart,System.IntPtr lpszCurrent){return CharPrev(lpszStart,lpszCurrent);}
		public static extern bool AnsiToOem(string lpszSrc,string lpszDst){return CharToOemBuff(lpszSrc,lpszDst);}
		public static extern bool AnsiToOemBuff(string lpszSrc,string lpszDst,uint cchDstLength){return CharToOemBuff(lpszSrc,lpszDst,cchDstLength);}
#endif
		#endregion

		/*


		*/

		#region func:GetWindow*
		[Interop.DllImport("user32.dll", CharSet=Interop.CharSet.Auto)]
		public static extern int GetClassName(System.IntPtr hWnd,System.Text.StringBuilder lpClassName,int nMaxCount);
		//--Window Text
		/// <summary>
		/// 指定されたウィンドウのタイトルバーのテキストをバッファへコピーします。
		/// 指定されたウィンドウがコントロールの場合は、コントロールのテキストをコピーします。
		/// ただし、他のアプリケーションのコントロールのテキストを取得することはできません。
		/// </summary>
		/// <param name="hWnd">ウィンドウ（ またはテキストを持つコントロール）のハンドルを指定します。</param>
		/// <param name="lpString">バッファへのポインタを指定します。このバッファにテキストが格納されます。</param>
		/// <param name="nMaxCount">バッファにコピーする文字の最大数を指定します。テキストのこのサイズを超える部分は、切り捨てられます。NULL 文字も数に含められます。</param>
		/// <returns>関数が成功すると、コピーされた文字列の文字数が返ります（ 終端の NULL 文字は含められません）。
		/// それ以外の場合、0 が返ります。拡張エラー情報を取得するには、関数を使います。</returns>
		[Interop.DllImport("user32.dll",SetLastError=true,CharSet=Interop.CharSet.Auto)]
		public static extern int GetWindowText(System.IntPtr hWnd,[Interop.Out]System.Text.StringBuilder lpString,int nMaxCount);
		[Interop.DllImport("user32.dll",SetLastError=true,CharSet=Interop.CharSet.Auto)]
		public static extern int GetWindowTextLength(System.IntPtr hWnd);
		/// <summary>
		/// 指定されたウィンドウのタイトルバーのテキストを取得します。
		/// 指定されたウィンドウがコントロールの場合は、コントロールのテキストを取得します。
		/// ただし、他のアプリケーションのコントロールのテキストを取得することはできません。
		/// </summary>
		/// <param name="hWnd">ウィンドウ（ またはテキストを持つコントロール）のハンドルを指定します。</param>
		/// <param name="lpString">ウィンドウテキストの文字列を返します。</param>
		/// <returns>取得したウィンドウテキストの文字数が返ります。それ以外の場合、0 が返り、拡張エラー情報が設定されます。</returns>
		public static int GetWindowText(System.IntPtr hWnd,out string lpString){
			int len=GetWindowTextLength(hWnd);
			System.Text.StringBuilder buffer=new System.Text.StringBuilder(len+1);
			int ret=GetWindowText(hWnd,buffer,buffer.Capacity);
			lpString=buffer.ToString();
			return ret;
		}
		/// <summary>
		/// 指定されたウィンドウのタイトルバーのテキストを取得します。
		/// 指定されたウィンドウがコントロールの場合は、コントロールのテキストを取得します。
		/// ただし、他のアプリケーションのコントロールのテキストを取得することはできません。
		/// </summary>
		/// <param name="hWnd">ウィンドウ（ またはテキストを持つコントロール）のハンドルを指定します。</param>
		/// <returns>バウィンドウテキストの文字列を返します。取得に失敗した場合、拡張エラー情報を設定して null を返します。</returns>
		public static string GetWindowText(System.IntPtr hWnd){
			int len=GetWindowTextLength(hWnd);
			System.Text.StringBuilder buffer=new System.Text.StringBuilder(len+1);
			int ret=GetWindowText(hWnd,buffer,buffer.Capacity);
			return ret>0?buffer.ToString():null;
		}
		[Interop.DllImport("user32.dll")]
		public static extern bool SetWindowText(System.IntPtr hWnd,string lpString);//[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]
		[Interop.DllImport("user32.dll",SetLastError=true)]
		public static extern uint GetWindowThreadProcessId(System.IntPtr hWnd, out uint lpdwProcessId);
		public static uint GetWindowThreadProcessId(System.IntPtr hWnd){
			uint d;return GetWindowThreadProcessId(hWnd,out d);
		}
		#endregion

		#region func:WindowsHookEx
		[Interop.DllImport("user32.dll")]
		public static extern bool UnhookWindowsHookEx(System.IntPtr hhk);
		[Interop.DllImport("user32.dll")]
		public static extern int CallNextHookEx(System.IntPtr hhk,int nCode,System.IntPtr wParam,System.IntPtr lParam);
		[Interop.DllImport("user32.dll")]
		public static extern int CallNextHookEx(System.IntPtr hhk,int nCode,WM wParam,[Interop.In]KBDLLHOOKSTRUCT lParam);
		[Interop.DllImport("user32.dll")]
		public static extern int CallNextHookEx(System.IntPtr hhk,int nCode,WM wParam,[Interop.In]MSLLHOOKSTRUCT lParam);
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr SetWindowsHookEx(WH idHook,System.IntPtr procAddr,System.IntPtr hMod,uint dwThreadId);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="idHook">フックするプロシージャの種類を <see cref="WH"/> で指定します。</param>
		/// <param name="callback"></param>
		/// <param name="hMod"></param>
		/// <param name="dwThreadId"></param>
		/// <returns></returns>
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr SetWindowsHookEx(WH idHook,HookProc callback,System.IntPtr hMod,uint dwThreadId);
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr SetWindowsHookEx(WH hook,LowLevelKeyboardProc callback,System.IntPtr hMod,uint dwThreadId);
		[Interop.DllImport("user32.dll",SetLastError=true)]
		public static extern System.IntPtr SetWindowsHookEx(WH code,LowLevelMouseProc func,System.IntPtr hInstance,int threadID);
		#endregion
	}

	/// <summary>
	/// ウィンドウに関連付けられている情報を取得するのに使用する構造体です。
	/// </summary>
	[System.Serializable]
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public struct WINDOWINFO{
		/// <summary>
		/// この構造体の大きさを表します。呼び出し元はこれに sizeof(WINDOWINFO) を設定する必要があります。
		/// </summary>
		public uint cbSize;
		/// <summary>
		/// ウィンドウの領域を取得します。
		/// </summary>
		public RECT rcWindow;
		/// <summary>
		/// クライアント領域を取得します。
		/// </summary>
		public RECT rcClient;
		/// <summary>
		/// ウィンドウのスタイルを保持します。
		/// </summary>
		public WS		dwStyle;
		/// <summary>
		/// ウィンドウの拡張スタイルを保持します。
		/// </summary>
		public WS_EX	dwExStyle;
		/// <summary>
		/// ウィンドウの状態を保持します。
		/// </summary>
		public WStatus	dwWindowStatus;
		/// <summary>
		/// ウィンドウの垂直境界の幅を保持します。
		/// </summary>
		public uint cxWindowBorders;
		/// <summary>
		/// ウィンドウの水平境界の高さを保持します。
		/// </summary>
		public uint cyWindowBorders;
		/// <summary>
		/// ウィンドウクラスのアトムを保持します。
		/// </summary>
		public ushort atomWindowType;
		/// <summary>
		/// ウィンドウを作成したプログラムのバージョンを保持します。
		/// </summary>
		public ushort wCreatorVersion;
		/// <summary>
		/// ウィンドウの領域を取得亦は設定します。
		/// </summary>
		[CM.Description("ウィンドウの領域を取得亦は設定します。")]
		public RECT Region{get{return this.rcWindow;}set{this.rcWindow=value;}}
		/// <summary>
		/// クライアント領域を取得亦は設定します。
		/// </summary>
		[CM.Description("クライアント領域を取得亦は設定します。")]
		public RECT ClientRegion{get{return this.rcClient;}set{this.rcClient=value;}}
	}
	public enum WStatus:uint{
		ZERO			=0x0000,
		ACTIVECAPTION	=0x0001
	}

	#region enum:VK
	/// <summary>
	/// Virtual Key 仮想キーコードを表す列挙体です。
	/// </summary>
	public enum VK{
		//winuser.h より
		/**<summary>マウスの左ボタンを示します。</summary>*/		LBUTTON=0x01,
		/**<summary>マウスの左ボタンを示します。</summary>*/		RBUTTON=0x02,
		/**<summary>キャンセルボタンを示します。</summary>*/		CANCEL=0x03,
		/**<summary>マウスの左ボタンを示します。</summary>*/		MBUTTON=0x04,    /* NOT contiguous with L & RBUTTON */

		//if(_WIN32_WINNT >= 0x0500)
		/**<summary>マウスの拡張ボタン 1 を示します。</summary>*/	XBUTTON1=0x05,    /* NOT contiguous with L & RBUTTON */
		/**<summary>マウスの拡張ボタン 2 を示します。</summary>*/	XBUTTON2=0x06,    /* NOT contiguous with L & RBUTTON */
		//endif /* _WIN32_WINNT >= 0x0500 */

		/*
			* 0x07 : unassigned
			*/

		BACK=0x08,
		TAB=0x09,

		/*
			* 0x0A - 0x0B : reserved
			*/

		CLEAR=0x0C,
		RETURN=0x0D,

		SHIFT=0x10,
		CONTROL=0x11,
		MENU=0x12,
		PAUSE=0x13,
		CAPITAL=0x14,
	
		//IME-MODE
		KANA=0x15,
		HANGEUL=0x15,  /* old name - should be here for compatibility */
		HANGUL=0x15,
		JUNJA=0x17,
		FINAL=0x18,
		HANJA=0x19,
		KANJI=0x19,

		ESCAPE=0x1B,
	
		//IME
		CONVERT=0x1C,
		NONCONVERT=0x1D,
		ACCEPT=0x1E,
		MODECHANGE=0x1F,

		/**<summary>Space キーを示します。</summary>*/			SPACE=0x20,
		/**<summary>PageUp キーを示します。</summary>*/			PRIOR=0x21,
		/**<summary>PageDown キーを示します。</summary>*/		NEXT=0x22,
		/**<summary>End キーを示します。</summary>*/			END=0x23,
		/**<summary>Home キーを示します。</summary>*/			HOME=0x24,
		/**<summary>← キーを示します。</summary>*/				LEFT=0x25,
		/**<summary>↑ キーを示します。</summary>*/				UP=0x26,
		/**<summary>→ キーを示します。</summary>*/				RIGHT=0x27,
		/**<summary>↓ キーを示します。</summary>*/				DOWN=0x28,
		/**<summary>Select キーを示します。</summary>*/			SELECT=0x29,
		/**<summary>Print キーを示します。</summary>*/			PRINT=0x2A,
		/**<summary>Execute キーを示します。</summary>*/		EXECUTE=0x2B,
		/**<summary>PrintScreen キーを示します。</summary>*/	SNAPSHOT=0x2C,
		/**<summary>Insert キーを示します。</summary>*/			INSERT=0x2D,
		/**<summary>Delete キーを示します。</summary>*/			DELETE=0x2E,
		/**<summary>Help キーを示します。</summary>*/			HELP=0x2F,

		/*
			* VK_0 - VK_9 are the same as ASCII '0' - '9' (0x30 - 0x39)
			* 0x40 : unassigned
			* VK_A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A)
			* 
			* '0' - '9' は以下では D を冠して記す事にする
			*/
		D0=0x30,		D1=0x31,		D2=0x32,		D3=0x33,		D4=0x34,
		D5=0x35,		D6=0x36,		D7=0x37,		D8=0x38,		D9=0x39,

		/**<summary>A キーを示します。</summary>*/A=0x41,
		/**<summary>B キーを示します。</summary>*/B=0x42,
		/**<summary>C キーを示します。</summary>*/C=0x43,
		/**<summary>D キーを示します。</summary>*/D=0x44,
		/**<summary>E キーを示します。</summary>*/E=0x45,
		/**<summary>F キーを示します。</summary>*/F=0x46,
		/**<summary>G キーを示します。</summary>*/G=0x47,
		/**<summary>H キーを示します。</summary>*/H=0x48,
		/**<summary>I キーを示します。</summary>*/I=0x49,
		/**<summary>J キーを示します。</summary>*/J=0x4a,
		/**<summary>K キーを示します。</summary>*/K=0x4b,
		/**<summary>L キーを示します。</summary>*/L=0x4c,
		/**<summary>M キーを示します。</summary>*/M=0x4d,
		/**<summary>N キーを示します。</summary>*/N=0x4e,
		/**<summary>O キーを示します。</summary>*/O=0x4f,
		/**<summary>P キーを示します。</summary>*/P=0x50,
		/**<summary>Q キーを示します。</summary>*/Q=0x51,
		/**<summary>R キーを示します。</summary>*/R=0x52,
		/**<summary>S キーを示します。</summary>*/S=0x53,
		/**<summary>T キーを示します。</summary>*/T=0x54,
		/**<summary>U キーを示します。</summary>*/U=0x55,
		/**<summary>V キーを示します。</summary>*/V=0x56,
		/**<summary>W キーを示します。</summary>*/W=0x57,
		/**<summary>X キーを示します。</summary>*/X=0x58,
		/**<summary>Y キーを示します。</summary>*/Y=0x59,
		/**<summary>Z キーを示します。</summary>*/Z=0x5a,
	
		LWIN=0x5B,
		RWIN=0x5C,
		APPS=0x5D,

		/*
			* 0x5E : reserved
			*/

		SLEEP=0x5F,//不明

		NUMPAD0=0x60,		NUMPAD1=0x61,		NUMPAD2=0x62,		NUMPAD3=0x63,		NUMPAD4=0x64,
		NUMPAD5=0x65,		NUMPAD6=0x66,		NUMPAD7=0x67,		NUMPAD8=0x68,		NUMPAD9=0x69,
		MULTIPLY=0x6A,
		ADD=0x6B,
		SEPARATOR=0x6C,
		SUBTRACT=0x6D,
		DECIMAL=0x6E,
		DIVIDE=0x6F,
		F1=0x70,		F2=0x71,		F3=0x72,		F4=0x73,		F5=0x74,
		F6=0x75,		F7=0x76,		F8=0x77,		F9=0x78,		F10=0x79,
		F11=0x7A,		F12=0x7B,		F13=0x7C,		F14=0x7D,		F15=0x7E,
		F16=0x7F,		F17=0x80,		F18=0x81,		F19=0x82,		F20=0x83,
		F21=0x84,		F22=0x85,		F23=0x86,		F24=0x87,

		/*
			* 0x88 - 0x8F : unassigned
			*/

		NUMLOCK=0x90,
		SCROLL=0x91,

		/*
			* NEC PC-9800 kbd definitions
			*/
		OEM_NEC_EQUAL=0x92,   // '=' key on numpad

		/*
			* Fujitsu/OASYS kbd definitions
			*/
		OEM_FJ_JISHO=0x92,   // 'Dictionary' key
		OEM_FJ_MASSHOU=0x93,   // 'Unregister word' key
		OEM_FJ_TOUROKU=0x94,   // 'Register word' key
		OEM_FJ_LOYA=0x95,   // 'Left OYAYUBI' key
		OEM_FJ_ROYA=0x96,   // 'Right OYAYUBI' key

		/*
			* 0x97 - 0x9F : unassigned
			*/

		/*
			* VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys.
			* Used only as parameters to GetAsyncKeyState() and GetKeyState().
			* No other API or message will distinguish left and right keys in this way.
			*/
		LSHIFT=0xA0,
		RSHIFT=0xA1,
		LCONTROL=0xA2,
		RCONTROL=0xA3,
		LMENU=0xA4,
		RMENU=0xA5,

		//if(_WIN32_WINNT >= 0x0500)
		BROWSER_BACK=0xA6,
		BROWSER_FORWARD=0xA7,
		BROWSER_REFRESH=0xA8,
		BROWSER_STOP=0xA9,
		BROWSER_SEARCH=0xAA,
		BROWSER_FAVORITES=0xAB,
		BROWSER_HOME=0xAC,

		VOLUME_MUTE=0xAD,
		VOLUME_DOWN=0xAE,
		VOLUME_UP=0xAF,
		MEDIA_NEXT_TRACK=0xB0,
		MEDIA_PREV_TRACK=0xB1,
		MEDIA_STOP=0xB2,
		MEDIA_PLAY_PAUSE=0xB3,
		LAUNCH_MAIL=0xB4,
		LAUNCH_MEDIA_SELECT=0xB5,
		LAUNCH_APP1=0xB6,
		LAUNCH_APP2=0xB7,

		//endif /* _WIN32_WINNT >= 0x0500 */

		/*
			* 0xB8 - 0xB9 : reserved
			*/

		OEM_1=0xBA,   // ';:' for US
		OEM_PLUS=0xBB,   // '+' any country
		OEM_COMMA=0xBC,   // ',' any country
		OEM_MINUS=0xBD,   // '-' any country
		OEM_PERIOD=0xBE,   // '.' any country
		OEM_2=0xBF,   // '/?' for US
		OEM_3=0xC0,   // '`~' for US

		/*
			* 0xC1 - 0xD7 : reserved
			*/

		/*
			* 0xD8 - 0xDA : unassigned
			*/

		OEM_4=0xDB,  //  '[{' for US
		OEM_5=0xDC,  //  '\|' for US
		OEM_6=0xDD,  //  ']}' for US
		OEM_7=0xDE,  //  ''"' for US
		OEM_8=0xDF,

		/*
			* 0xE0 : reserved
			*/

		/*
			* Various extended or enhanced keyboards
			*/
		//以下不明
		OEM_AX=0xE1,  //  'AX' key on Japanese AX kbd
		OEM_102=0xE2,  //  "<>" or "\|" on RT 102-key kbd.
		ICO_HELP=0xE3,  //  Help key on ICO
		ICO_00=0xE4,  //  00 key on ICO

		//if(WINVER >= 0x0400)
		PROCESSKEY=0xE5,
		//endif /* WINVER >= 0x0400 */

		ICO_CLEAR=0xE6,


		//if(_WIN32_WINNT >= 0x0500)
		PACKET=0xE7,
		//endif /* _WIN32_WINNT >= 0x0500 */

		/*
			* 0xE8 : unassigned
			*/

		/*
			* Nokia/Ericsson definitions
			*/
		OEM_RESET=0xE9,
		OEM_JUMP=0xEA,
		OEM_PA1=0xEB,
		OEM_PA2=0xEC,
		OEM_PA3=0xED,
		OEM_WSCTRL=0xEE,
		OEM_CUSEL=0xEF,
		OEM_ATTN=0xF0,
		OEM_FINISH=0xF1,
		OEM_COPY=0xF2,
		OEM_AUTO=0xF3,
		OEM_ENLW=0xF4,
		OEM_BACKTAB=0xF5,

		ATTN=0xF6,
		CRSEL=0xF7,
		EXSEL=0xF8,
		EREOF=0xF9,
		PLAY=0xFA,
		ZOOM=0xFB,
		NONAME=0xFC,
		PA1=0xFD,
		OEM_CLEAR=0xFE

		/*
			* 0xFF : reserved
			*/
	}
	#endregion

	/// <summary>
	/// ウィンドウの配置に関する情報を保持します。
	/// </summary>
	[System.Serializable,Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public struct WINDOWPLACEMENT{
		/// <summary>
		/// この構造体のバイト単位のサイズを指定します。
		/// </summary>
		public uint length;
		/// <summary>
		/// 最小化されたウィンドウの位置と、ウィンドウを元に戻す方法を制御するフラグを指定します。
		/// </summary>
		public WPF flags;
		/// <summary>
		/// ウィンドウの現在の表示状態を指定します。このメンバには、次のいずれかの値を指定できます。
		/// </summary>
		public SW showCmd;
		/// <summary>
		/// ウィンドウが最小化されるときの、ウィンドウの左上隅の位置を指定します。
		/// </summary>
		public POINT ptMinPosition;
		/// <summary>
		/// ウィンドウが最大化されるときの、ウィンドウの左上隅の位置を指定します。
		/// </summary>
		public POINT ptMaxPosition;
		/// <summary>
		/// ウィンドウが通常 (元に戻された) の位置に表示されるときの、ウィンドウの座標を指定します。
		/// </summary>
		public RECT rcNormalPosition;
	}
	/// <summary>
	/// 最小化されたウィンドウの位置と、ウィンドウを元に戻す方法を指定するのに使用します。
	/// </summary>
	[System.Flags]
	public enum WPF{
		/// <summary>
		/// 最小化されたウィンドウの x 位置と y 位置が指定できることを示します。
		/// 座標を WINDOWPLACEMENT.ptMinPosition メンバに設定するときは、このフラグを指定します。
		/// </summary>
		SETMINPOSITION=0x0001,
		/// <summary>
		/// 最小化される前に最大化されていたかどうかに関係なく、復元されるウィンドウが最大化されることを示します。
		/// この設定は、次にウィンドウが元に戻されるときにのみ有効です。既定の復元動作は変更しません。
		/// このフラグは、WINDOWPLACEMENT.showCmd メンバに SW_SHOWMINIMIZED の値が指定されたときにだけ有効です。
		/// </summary>
		RESTORETOMAXIMIZED=0x0002,
		/// <summary>
		/// Windows 2000/XP: 呼び出し元のスレッドとウィンドウを処理しているスレッドが異なる場合、
		/// システムがウィンドウを処理するスレッドに入力を与える事を指定します。
		/// この事によって、呼び出し元の処理が、ウィンドウの処理の実行の間停止する事を避ける事が出来ます。
		/// </summary>
		ASYNCWINDOWPLACEMENT=0x0004
	}
	/// <summary>
	/// ウィンドウの表示状態を指定するのに使用します。
	/// ShowWindow 関数、STARTUPINFO 構造体、WINDOWPLACEMENT 構造体などで使用されます。
	/// </summary>
	public enum SW{
		/// <summary>
		/// ウィンドウを非表示にし、他のウィンドウをアクティブにします。
		/// </summary>
		HIDE=0,
		/// <summary>
		/// ウィンドウをアクティブにして表示します。ウィンドウが最小化または最大化されていた場合は、その位置とサイズを元に戻します。
		/// 初めてウィンドウを表示するときには、このフラグを指定してください。
		/// </summary>
		SHOWNORMAL=1,
		/// <summary>
		/// ウィンドウをアクティブにして表示します。ウィンドウが最小化または最大化されていた場合は、その位置とサイズを元に戻します。
		/// 初めてウィンドウを表示するときには、このフラグを指定してください。
		/// </summary>
		NORMAL=1,
		/// <summary>
		/// ウィンドウをアクティブにして、最小化します。
		/// </summary>
		SHOWMINIMIZED=2,
		/// <summary>
		/// ウィンドウをアクティブにして、最大化します。
		/// </summary>
		SHOWMAXIMIZED=3,
		/// <summary>
		/// ウィンドウを最大化します。
		/// </summary>
		MAXIMIZE=3,
		/// <summary>
		/// ウィンドウを直前の位置とサイズで表示します。
		/// SW_SHOWNORMAL と似ていますが、この値を指定した場合は、ウィンドウはアクティブ化されません。
		/// </summary>
		SHOWNOACTIVATE=4,
		/// <summary>
		/// ウィンドウをアクティブにして、現在の位置とサイズで表示します。
		/// </summary>
		SHOW=5,
		/// <summary>
		/// ウィンドウを最小化し、Z オーダーが次のトップレベルウィンドウをアクティブにします。
		/// </summary>
		MINIMIZE=6,
		/// <summary>
		/// ウィンドウを最小化します。
		/// SW_SHOWMINIMIZED と似ていますが、この値を指定した場合は、ウィンドウはアクティブ化されません。
		/// </summary>
		SHOWMINNOACTIVATE=7,
		/// <summary>
		/// ウィンドウを現在のサイズと位置で表示します。
		/// SW_SHOW と似ていますが、この値を指定した場合は、ウィンドウはアクティブ化されません。
		/// </summary>
		SHOWNA=8,
		/// <summary>
		/// ウィンドウをアクティブにして表示します。最小化または最大化されていたウィンドウは、元の位置とサイズに戻ります。
		/// 最小化されているウィンドウを元に戻す場合は、このフラグをセットします。
		/// </summary>
		RESTORE=9,
		/// <summary>
		/// アプリケーションを起動したプログラムが CreateProcess 関数に渡した STARTUPINFO 構造体で指定された SW_ フラグに従って表示状態を設定します。
		/// </summary>
		SHOWDEFAULT=10,
		/// <summary>
		/// Windows 2000：たとえウィンドウを所有するスレッドがハングしていても、ウィンドウを最小化します。
		/// このフラグは、ほかのスレッドのウィンドウを最小化する場合にだけ使用してください。
		/// </summary>
		FORCEMINIMIZE=11,
		/// <summary>
		/// 不明。FORCEMINIMIZE と同じです。
		/// </summary>
		MAX=11
	}
	/// <summary>
	/// ScrollWindow/ScrollWindowEx 関数の引数です。
	/// </summary>
	[System.Flags]
	public enum SW_{
		/// <summary>
		/// prcScroll によって指定される矩形領域と交わる全ての子ウィンドウをスクロールします。
		/// </summary>
		SCROLLCHILDREN=0x0001,
		/// <summary>
		/// スクロール後に hrgnUpdate で指定した領域を無効にします。
		/// </summary>
		INVALIDATE=0x0002,
		/// <summary>
		/// SW_INVALIDATE と共に指定した場合、WM_ERASEBKGND を送って無効になった領域を消去します。
		/// </summary>
		ERASE=0x0004,
		/// <summary>
		/// Windows 98/Me Windows 2000/XP: スクロール時に滑らかなスクロール処理を行う事を指定します。
		/// flags の高位ワードを使用して滑らかなスクロールの処理を何回行うかを指定する事が出来ます。
		/// </summary>
		SMOOTHSCROLL=0x0010
	}
	/// <summary>
	/// メッセージ WM_SHOWWINDOW の lParam です。表示するウィンドウの状態を指定するのに指定します。
	/// </summary>
	public enum SW__{
		/// <summary>
		/// 親ウィンドウが最小化する事を示します。
		/// </summary>
		PARENTCLOSING=1,
		/// <summary>
		/// 他のウィンドウが最大化した為、このウィンドウが隠される事を示します。
		/// </summary>
		OTHERZOOM=2,
		/// <summary>
		/// 親ウィンドウが開かれる事を示します。
		/// </summary>
		PARENTOPENING=3,
		/// <summary>
		/// 他のウィンドウの最大化が解消され、このウィンドウが露わになる事を示します。
		/// </summary>
		OTHERUNZOOM=4
	}
	/// <summary>
	/// WindowsHook の種類を指定する為に使用します。
	/// <see cref="User32.SetWindowsHookEx(mwg.Win32.WH,System.IntPtr,System.IntPtr,uint)"/> 関数に使用します。
	/// <a href="http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp">MSDN 頁</a>
	/// </summary>
	public enum WH:int{
		//
		// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar.
		// For more information, see the MessageProc hook procedure.
		// MSGFILTER
		//
		/// <summary>
		/// Installs a hook procedure that records input messages posted to the system message queue. This hook is useful for recording macros.
		/// For more information, see the JournalRecordProc hook procedure.
		/// </summary>
		JOURNALRECORD = 0,
		/// <summary>
		/// Installs a hook procedure that posts messages previously recorded by a WH_JOURNALRECORD hook procedure.
		/// For more information, see the JournalPlaybackProc hook procedure.
		/// </summary>
		JOURNALPLAYBACK = 1,
		/// <summary>
		/// Installs a hook procedure that monitors keystroke messages.
		/// For more information, see the KeyboardProc hook procedure.
		/// </summary>
		KEYBOARD = 2,
		/// <summary>
		/// Installs a hook procedure that monitors messages posted to a message queue.
		/// For more information, see the GetMsgProc hook procedure.
		/// </summary>
		GETMESSAGE = 3,
		/// <summary>
		/// 目標のウィンドウの処理の前にメッセージを監視する事を示します。
		/// For more information, see the CallWndProc hook procedure.
		/// </summary>
		CALLWNDPROC = 4,
		/// <summary>
		/// Installs a hook procedure that receives notifications useful to a computer-based training (CBT) application.
		/// For more information, see the CBTProc hook procedure.
		/// </summary>
		CBT = 5,
		/// <summary>
		/// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. The hook procedure monitors these messages for all applications in the same desktop as the calling thread.
		/// For more information, see the SysMsgProc hook procedure.
		/// </summary>
		SYSMSGFILTER = 6,
		/// <summary>
		/// Installs a hook procedure that monitors mouse messages.
		/// For more information, see the MouseProc hook procedure.
		/// </summary>
		MOUSE = 7,
		/// <summary>
		/// ?
		/// </summary>
		HARDWARE = 8,
		/// <summary>
		/// Installs a hook procedure useful for debugging other hook procedures.
		/// For more information, see the DebugProc hook procedure.
		/// </summary>
		DEBUG = 9,
		/// <summary>
		/// Installs a hook procedure that receives notifications useful to shell applications.
		/// For more information, see the ShellProc hook procedure.
		/// </summary>
		SHELL = 10,
		/// <summary>
		/// Installs a hook procedure that will be called when the application's foreground thread is about to become idle. This hook is useful for performing low priority tasks during idle time.
		/// For more information, see the ForegroundIdleProc hook procedure. 
		/// </summary>
		FOREGROUNDIDLE = 11,
		/// <summary>
		/// Installs a hook procedure that monitors messages after they have been processed by the destination window procedure.
		/// For more information, see the CallWndRetProc hook procedure.
		/// </summary>
		CALLWNDPROCRET = 12,
		/// <summary>
		/// Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard input events.
		/// For more information, see the LowLevelKeyboardProc hook procedure.
		/// </summary>
		KEYBOARD_LL = 13,
		/// <summary>
		/// Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events.
		/// For more information, see the LowLevelMouseProc hook procedure.
		/// </summary>
		MOUSE_LL=14
	}
	/// <summary>
	/// フックする関数を表すデリゲートクラスです。
	/// </summary>
	public delegate int HookProc(int code,System.IntPtr wParam,System.IntPtr lParam);
	/// <summary>
	/// 低レベルのキーボードのイベントを処理するプロシージャを表すデリゲートです。
	/// </summary>
	public delegate int LowLevelKeyboardProc(int nCode,WM wParam, [Interop.In]KBDLLHOOKSTRUCT lParam);
	/// <summary>
	/// 低レベルのマウスのイベントを処理するプロシージャを表すデリゲートです。
	/// </summary>
	public delegate int LowLevelMouseProc(int code,WM wParam, [Interop.In]MSLLHOOKSTRUCT lParam);
	/// <summary>
	/// キーボードのイベントの情報を保持する構造体です。
	/// </summary>
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public class KBDLLHOOKSTRUCT{
		/// <summary>
		/// キーの情報を保持します。
		/// </summary>
		public VK vkCode;
		public int scanCode;
		public int flags;
		/// <summary>
		/// このメッセージの発生した時刻の情報を保持します。
		/// </summary>
		public int time;
		/// <summary>
		/// このメッセージに関する追加の情報を保持します。
		/// </summary>
		public System.IntPtr dwExtraInfo;
	}
	/// <summary>
	/// マウスのイベントの情報を保持する構造体です。
	/// </summary>
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public class MSLLHOOKSTRUCT{
		/// <summary>
		/// マウスカーソルの位置情報を保持します。
		/// </summary>
		public POINT pt;
		public int mouseData;
		public int flags;
		/// <summary>
		/// このメッセージの発生した時刻の情報を保持します。
		/// </summary>
		public int time;
		/// <summary>
		/// このメッセージに関する追加の情報を保持します。
		/// </summary>
		public System.IntPtr dwExtraInfo;
	}
	/// <summary>
	/// CallWndProc で使用する構造体です。
	/// </summary>
	[Interop::StructLayout(Interop::LayoutKind.Sequential)]
	public struct CWPSTRUCT{
		/// <summary>
		/// メッセージの lParam を保持します。
		/// </summary>
		public System.IntPtr lParam;
		/// <summary>
		/// メッセージの wParam を保持します。
		/// </summary>
		public System.IntPtr wParam;
		/// <summary>
		/// メッセージの種類を保持します。
		/// </summary>
		public WM message;
		/// <summary>
		/// メッセージの送り先の Window の Handle を保持します。
		/// </summary>
		public System.IntPtr hwnd;
	} 
}