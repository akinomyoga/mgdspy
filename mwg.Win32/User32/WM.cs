namespace mwg.Win32{
	/// <summary>
	/// Window Message ウィンドウメッセージを表す列挙体です。
	/// </summary>
	public enum WM:uint{
		/// <summary>
		/// 何の動作もしません。受信側の Window に無視されるメッセージを送りたい時に使用します。
		/// </summary>
		/// <wparam>使用されません。</wparam>
		/// <lparam>使用されません。</lparam>
		/// <return>このメッセージが処理された時に 0 を返します。</return>
		/// <remarks>
		/// 例えば、アプリケーションが WH_GETMESSAGE フックを設置している時に、受信ウィンドウがメッセージを処理するのを妨げる事が出来ます。
		/// 具体的には GetMsgProc 関数内でメッセージを WM_NULL に変更する事によって、受信ウィンドウはメッセージを無視する様になります。
		/// <para>他の例としては、アプリケーションは WM_NULL を SendMessageTimeout 送信する事によって
		/// その Window が応答するかどうかを確認する事が出来ます。
		/// </para>
		/// </remarks>
		NULL = 0x00,
		/// <summary>
		/// このメッセージは CreateWindowEx 亦は CreateWindow 関数によって window を作成する時に送られます。
		/// (このメッセージは CreateWindowEx/CreateWindow 関数から返る前に送られます。)
		/// 作成された window の window procedure は window が表示される前にこのメッセージを受け取ります。
		/// <para>このメッセージは WindowProc 関数を通じて受信されます。</para>
		/// </summary>
		/// <wparam>使用されません。</wparam>
		/// <lparam>作成されたウィンドウに関する情報を保持する CREATESTRUCT 構造体へのポインタを指定します。</lparam>
		/// <return>
		/// このメッセージが正常に処理され、window の作成を継続する場合には 0 を返します。
		/// -1 を返した場合には window は破棄され、CreateWindowEx / CreateWindow 関数は NULL ハンドルを返します。
		/// </return>
		CREATE = 0x01,
		DESTROY = 0x02,
		MOVE = 0x03,
		SIZE = 0x05,
		ACTIVATE = 0x06,
		SETFOCUS = 0x07,
		/// <summary>
		/// キーボードフォーカスを失う寸前のウィンドウに送られます。
		/// </summary>
		/// <wparam>キーボードフォーカスの行き先のウィンドウハンドル。NULL の可能性もあります。</wparam>
		/// <lparam>使用されません。</lparam>
		/// <return>このメッセージを処理したら 0 を返します。</return>
		/// <remarks>
		/// キャレットを表示している場合にはこの時点でキャレットを破棄するべきです。
		/// このメッセージを処理している間にウィンドウを描画する関数を呼び出さないで下さい。
		/// メッセージデッドロックが発生します。
		/// </remarks>
		KILLFOCUS = 0x08,
		ENABLE = 0x0A,
		SETREDRAW = 0x0B,
		SETTEXT = 0x0C,
		GETTEXT = 0x0D,
		GETTEXTLENGTH = 0x0E,
		PAINT = 0x0F,
		CLOSE = 0x10,
		QUERYENDSESSION = 0x11,
		QUIT = 0x12,
		QUERYOPEN = 0x13,
		ERASEBKGND = 0x14,
		SYSCOLORCHANGE = 0x15,
		ENDSESSION = 0x16,
		SYSTEMERROR = 0x17,
		SHOWWINDOW = 0x18,
		CTLCOLOR = 0x19,
		WININICHANGE = 0x1A,
		SETTINGCHANGE = 0x1A,
		DEVMODECHANGE = 0x1B,
		ACTIVATEAPP = 0x1C,
		FONTCHANGE = 0x1D,
		TIMECHANGE = 0x1E,
		CANCELMODE = 0x1F,
		SETCURSOR = 0x20,
		MOUSEACTIVATE = 0x21,
		CHILDACTIVATE = 0x22,
		QUEUESYNC = 0x23,
		GETMINMAXINFO = 0x24,
		PAINTICON = 0x26,
		ICONERASEBKGND = 0x27,
		NEXTDLGCTL = 0x28,
		SPOOLERSTATUS = 0x2A,
		DRAWITEM = 0x2B,
		MEASUREITEM = 0x2C,
		DELETEITEM = 0x2D,
		VKEYTOITEM = 0x2E,
		CHARTOITEM = 0x2F,
		SETFONT = 0x30,
		GETFONT = 0x31,
		SETHOTKEY = 0x32,
		GETHOTKEY = 0x33,
		QUERYDRAGICON = 0x37,
		COMPAREITEM = 0x39,
		COMPACTING = 0x41,
		WINDOWPOSCHANGING = 0x46,
		WINDOWPOSCHANGED = 0x47,
		POWER = 0x48,
		COPYDATA = 0x4A,
		CANCELJOURNAL = 0x4B,
		NOTIFY = 0x4E,
		INPUTLANGCHANGEREQUEST = 0x50,
		INPUTLANGCHANGE = 0x51,
		TCARD = 0x52,
		HELP = 0x53,
		USERCHANGED = 0x54,
		NOTIFYFORMAT = 0x55,
		CONTEXTMENU = 0x7B,
		STYLECHANGING = 0x7C,
		STYLECHANGED = 0x7D,
		DISPLAYCHANGE = 0x7E,
		GETICON = 0x7F,
		SETICON = 0x80,
		NCCREATE = 0x81,
		NCDESTROY = 0x82,
		NCCALCSIZE = 0x83,
		NCHITTEST = 0x84,
		NCPAINT = 0x85,
		NCACTIVATE = 0x86,
		GETDLGCODE = 0x87,
		NCMOUSEMOVE = 0xA0,
		NCLBUTTONDOWN = 0xA1,
		NCLBUTTONUP = 0xA2,
		NCLBUTTONDBLCLK = 0xA3,
		NCRBUTTONDOWN = 0xA4,
		NCRBUTTONUP = 0xA5,
		NCRBUTTONDBLCLK = 0xA6,
		NCMBUTTONDOWN = 0xA7,
		NCMBUTTONUP = 0xA8,
		NCMBUTTONDBLCLK = 0xA9,
		KEYFIRST = 0x100,
		KEYDOWN = 0x100,
		KEYUP = 0x101,
		CHAR = 0x102,
		DEADCHAR = 0x103,
		SYSKEYDOWN = 0x104,
		SYSKEYUP = 0x105,
		SYSCHAR = 0x106,
		SYSDEADCHAR = 0x107,
		KEYLAST = 0x108,
		IME_STARTCOMPOSITION = 0x10D,
		IME_ENDCOMPOSITION = 0x10E,
		IME_COMPOSITION = 0x10F,
		IME_KEYLAST = 0x10F,
		INITDIALOG = 0x110,
		COMMAND = 0x111,
		SYSCOMMAND = 0x112,
		TIMER = 0x113,
		HSCROLL = 0x114,
		VSCROLL = 0x115,
		INITMENU = 0x116,
		INITMENUPOPUP = 0x117,
		MENUSELECT = 0x11F,
		MENUCHAR = 0x120,
		ENTERIDLE = 0x121,
		CTLCOLORMSGBOX = 0x132,
		CTLCOLOREDIT = 0x133,
		CTLCOLORLISTBOX = 0x134,
		CTLCOLORBTN = 0x135,
		CTLCOLORDLG = 0x136,
		CTLCOLORSCROLLBAR = 0x137,
		CTLCOLORSTATIC = 0x138,
		MOUSEFIRST = 0x200,
		MOUSEMOVE = 0x200,
		LBUTTONDOWN = 0x201,
		LBUTTONUP = 0x202,
		LBUTTONDBLCLK = 0x203,
		RBUTTONDOWN = 0x204,
		RBUTTONUP = 0x205,
		RBUTTONDBLCLK = 0x206,
		MBUTTONDOWN = 0x207,
		MBUTTONUP = 0x208,
		MBUTTONDBLCLK = 0x209,
		MOUSELAST = 0x20A,
		MOUSEWHEEL = 0x20A,
		PARENTNOTIFY = 0x210,
		ENTERMENULOOP = 0x211,
		EXITMENULOOP = 0x212,
		NEXTMENU = 0x213,
		SIZING = 0x214,
		CAPTURECHANGED = 0x215,
		MOVING = 0x216,
		POWERBROADCAST = 0x218,
		DEVICECHANGE = 0x219,
		MDICREATE = 0x220,
		MDIDESTROY = 0x221,
		MDIACTIVATE = 0x222,
		MDIRESTORE = 0x223,
		MDINEXT = 0x224,
		MDIMAXIMIZE = 0x225,
		MDITILE = 0x226,
		MDICASCADE = 0x227,
		MDIICONARRANGE = 0x228,
		MDIGETACTIVE = 0x229,
		MDISETMENU = 0x230,
		ENTERSIZEMOVE = 0x231,
		EXITSIZEMOVE = 0x232,
		DROPFILES = 0x233,
		MDIREFRESHMENU = 0x234,
		IME_SETCONTEXT = 0x281,
		IME_NOTIFY = 0x282,
		IME_CONTROL = 0x283,
		IME_COMPOSITIONFULL = 0x284,
		IME_SELECT = 0x285,
		IME_CHAR = 0x286,
		IME_KEYDOWN = 0x290,
		IME_KEYUP = 0x291,
		MOUSEHOVER = 0x2A1,
		NCMOUSELEAVE = 0x2A2,
		MOUSELEAVE = 0x2A3,
		CUT = 0x300,
		COPY = 0x301,
		PASTE = 0x302,
		CLEAR = 0x303,
		UNDO = 0x304,
		RENDERFORMAT = 0x305,
		RENDERALLFORMATS = 0x306,
		DESTROYCLIPBOARD = 0x307,
		DRAWCLIPBOARD = 0x308,
		PAINTCLIPBOARD = 0x309,
		VSCROLLCLIPBOARD = 0x30A,
		SIZECLIPBOARD = 0x30B,
		ASKCBFORMATNAME = 0x30C,
		CHANGECBCHAIN = 0x30D,
		HSCROLLCLIPBOARD = 0x30E,
		QUERYNEWPALETTE = 0x30F,
		PALETTEISCHANGING = 0x310,
		PALETTECHANGED = 0x311,
		HOTKEY = 0x312,
		PRINT = 0x317,
		PRINTCLIENT = 0x318,
		HANDHELDFIRST = 0x358,
		HANDHELDLAST = 0x35F,
		PENWINFIRST = 0x380,
		PENWINLAST = 0x38F,
		COALESCE_FIRST = 0x390,
		COALESCE_LAST = 0x39F,
		DDE_FIRST = 0x3E0,
		DDE_INITIATE = 0x3E0,
		DDE_TERMINATE = 0x3E1,
		DDE_ADVISE = 0x3E2,
		DDE_UNADVISE = 0x3E3,
		DDE_ACK = 0x3E4,
		DDE_DATA = 0x3E5,
		DDE_REQUEST = 0x3E6,
		DDE_POKE = 0x3E7,
		DDE_EXECUTE = 0x3E8,
		DDE_LAST = 0x3E8,
		USER = 0x400,
		APP = 0x8000
	}
	/// <summary>
	/// エディットコントロールに関するメッセージです。
	/// </summary>
	public static class EM{
		//===========================================================
		//		WinUser.h
		//===========================================================
		/// <summary>
		/// Edit コントロールの現在の選択範囲の始めと終わりの位置を取得します。
		/// Edit/RichEdit に送る事が出来ます。
		/// </summary>
		/// <wparam>選択範囲の初めの位置を受け取る為のバッファへのポインタを指定します。NULL を指定する事が可能です。</wparam>
		/// <lparam>選択範囲の末端の位置を受け取る為のバッファへのポインタを指定します。NULL を指定する事が可能です。</lparam>
		/// <return>
		/// 低位ワードに選択範囲の初めの位置を返します。
		/// 上位ワードに選択範囲の末端の位置を返します。
		/// どちらかの値が 0xFFFF を越える場合には -1 を返します。
		/// wParam 及び lParam に返される値を使用する事が推奨されます。
		/// </return>
		/// <remarks>
		/// [RichEdit]: 同等の情報を取得するのに EM_EXGETSEL を使用する事が出来ます。
		/// </remarks>
		public const WM GETSEL=(WM)0x00B0;
		/// <summary>
		/// Edit コントロール内の連続する文字列を選択します。
		/// Edit/RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <wparam>選択範囲の開始位置を指定します。</wparam>
		/// <lparam>選択範囲の終了位置を指定します。</lparam>
		/// <return>値は返しません。</return>
		/// <remarks>
		/// 初めの位置の値は末端の位置の値よりも大きい値を指定する事が可能です。
		/// より小さい方が選択範囲の初めの位置になり、より大きい方が選択範囲の末端になります。
		/// 開始位置は固定された端で、終了位置は現在アクティブになっている位置です。
		/// ユーザが SHIFT を使用して選択範囲を調節する際は、
		/// アクティブになっている側が動きますが、固定位置は動きません。
		/// 開始位置に 0 を指定し終了位置に -1 を指定すると Edit コントロール内の全ての文字列が選択されます。
		/// 開始位置が -1 の時には現在の選択範囲は全て解除されます。
		/// [Edit]: コントロールはキャレットを終了位置に表示します。
		/// </remarks>
		public const WM SETSEL=(WM)0x00B1;
		/// <summary>
		/// Edit コントロールの整形矩形を取得します。
		/// 整形矩形は文字列を描画する領域を示します。
		/// この矩形は Edit コントロールのウィンドウ自体の矩形とは別の物です。
		/// </summary>
		/// <wparam>常に 0 を指定します。</wparam>
		/// <lparam>矩形情報を受け取る為の RECT へのポインタを指定します。</lparam>
		/// <return>戻り値には特に意味はありません。</return>
		/// <remarks>
		/// Edit コントロールの整形矩形は EM_SETRECT/EM_SETRECTNP を使用して変更する事が出来ます。
		/// 特定の条件下では、EM_GETRECT によって得られる値は EM_SETRECT/EM_SETRECTNP で設定した値と厳密には一致しません。
		/// 2, 3 ピクセルの誤差が生じる事があります。
		/// [RichEdit]: 整形矩形に選択バーは含まれません。選択バーは整形矩形の左側にあり、そこをクリックする事により行を選択できる物です。
		/// </remarks>
		public const WM GETRECT=(WM)0x00B2;
		/// <summary>
		/// 複数行 Edit コントロールの整形矩形を設定します。
		/// 整形矩形は文字列を描画する領域を示します。
		/// この矩形は Edit コントロールのウィンドウ自体の矩形とは別の物です。
		/// </summary>
		/// <wparam>
		/// [RichEdit 2.0 以降]: lParam が絶対座標を表すか相対座標を表すかを指定します。
		/// 0 は絶対座標である事を示します。1 は現在の整形矩形に対する相対座標 (正負のどちらも可能) である事を示します。
		/// [Edit/RichEdit 1.0]: 常に 0 を指定します。
		/// </wparam>
		/// <lparam>新しい整形矩形の値を保持する RECT へのポインタを指定します。NULL を指定した場合、整形矩形を既定の値に戻します。</lparam>
		/// <return>値は返しません。</return>
		/// <remarks>
		/// このメッセージによって Edit コントロール内の文字列は再描画されます。再描画する事為しに文字列を変更したい場合には EM_SETRECTNP を使用して下さい。
		/// Edit コントロールが作成された時に整形矩形は既定の大きさに設定されます。
		/// 整形矩形の大きさをコントロールウィンドウの大きさより大きくしたり小さくしたりしたい場合には EM_SETRECT を使用します。
		/// Edit コントロールが水平スクロールバーを持たず整形矩形が Edit コントロールウィンドウよりも大きい場合には、
		/// コントロールウィンドウより長い行はワードラップされる代わりに先頭のみ切り抜かれて表示されます。
		/// Edit コントロールが境界を持つ場合は整形矩形はその分小さくなります。EM_GETRECT によって得られる値を以て整形矩形の大きさを調整する場合には EM_SETRECT を送る前にコントロールの境界を取り除く必要があります。
		/// [RichEdit]: 整形矩形に選択バーは含まれません。選択バーは整形矩形の左側にあり、そこをクリックする事により行を選択できる物です。
		/// </remarks>
		public const WM SETRECT=(WM)0x00B3;
		/// <summary>
		/// 複数行 Edit コントロールの整形矩形を設定します。これは、Edit コントロールウィンドウを再描画しないという点を除いては EM_SETRECT と同じです。
		/// 整形矩形は文字列を描画する領域を示します。この矩形は Edit コントロールのウィンドウ自体の矩形とは別の物です。
		/// Edit/RichEdit コントロールに送る事が出来ます。但し、単一行 Edit コントロールでは処理されません。
		/// </summary>
		/// <wparam>
		/// [RichEdit 3.0 以降]: lParam が絶対座標を表すか相対座標を表すかを指定します。
		/// 0 は絶対座標である事を示します。1 は現在の整形矩形に対する相対座標 (正負のどちらも可能) である事を示します。
		/// [Edit]: 常に 0 を指定します。
		/// </wparam>
		/// <lparam>新しい整形矩形の値を保持する RECT へのポインタを指定します。NULL を指定した場合、整形矩形を既定の値に戻します。</lparam>
		/// <return>値は返しません。</return>
		/// <remarks>
		/// [RichEdit]: 3.0 以降で対応しています。
		/// </remarks>
		public const WM SETRECTNP=(WM)0x00B4;
		public const WM SCROLL=(WM)0x00B5;
		public const WM LINESCROLL=(WM)0x00B6;
		public const WM SCROLLCARET=(WM)0x00B7;
		/// <summary>
		/// Edit コントロールの変更済フラグの状態を取得します。
		/// 変更済フラッグは Edit コントロール内の文字列が変更されたかどうかを指定します。
		/// Edit/RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <wparam>常に 0 を指定します。</wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>Edit コントロールの内容に変更が為されている時に 0 以外の値を返します。</return>
		/// <remarks>
		/// コントロール作成時にシステムは自動的に変更済フラッグを 0 にします。
		/// 亦、文字列がユーザによって変更された場合にはシステムは変更済フラッグを 0 でない値に変更します。
		/// </remarks>
		public const WM GETMODIFY=(WM)0x00B8;
		/// <summary>
		/// Edit コントロールに対する変更済フラグを設定します。
		/// 変更済フラッグは Edit コントロール内の文字列が変更されたかどうかを指定します。
		/// Edit/RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <wparam>変更済フラグの新しい値を指定します。
		/// TRUE の値は文字列が変更された事を示します。FALSE の値は文字列が変更されていない事を示します。</wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>値は返しません。</return>
		/// <remarks>
		/// コントロール作成時にシステムは自動的に変更済フラッグを 0 にします。
		/// 亦、文字列がユーザによって変更された場合にはシステムは変更済フラッグを 0 でない値に変更します。
		/// [RichEdit 1.0]: Objects created without the REO_DYNAMICSIZE flag will lock in their extents when the modify flag is set to FALSE.
		/// </remarks>
		public const WM SETMODIFY=(WM)0x00B9;
		/// <summary>
		/// 複数行 Edit コントロールの行数を取得します。
		/// Edit/RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <wparam>常に 0 を指定します。</wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>複数行 Edit コントロール亦は RichEdit コントロールの中の全行数を示す値を返します。</return>
		/// <remarks>
		/// 現在見えている行数ではなくて、コントロール内にある全ての行の数を取得します。
		/// ワードラップ機能が有効になっている場合には、Edit コントロールの大きさが変化した際に行数も変化する可能性があります。
		/// </remarks>
		public const WM GETLINECOUNT=(WM)0x00BA;
		public const WM LINEINDEX=(WM)0x00BB;
		/// <summary>
		/// 複数行 Edit コントロールが使用するメモリのハンドルを指定します。
		/// </summary>
		/// <wparam>現在表示されている文字列を格納する為に使用する新しいメモリバッファへのハンドルを指定します。
		/// 必要がある場合にはコントロールはこのメモリを再配置します。</wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>値は返しません。</return>
		public const WM SETHANDLE=(WM)0x00BC;
		/// <summary>
		/// 複数行 Edit コントロールの文字列に割り当てられているメモリのハンドルを取得します。
		/// </summary>
		/// <lparam>常に 0 を指定します。</lparam><wparam>常に 0 を指定します。</wparam>
		/// <return>Edit コントロールの内容を保持するバッファのメモリハンドルを返します。
		/// 単一行 Edit コントロールにメッセージを送るなどしてエラーが起こった場合には 0 を返します。</return>
		/// <remarks>
		/// 返ってきたハンドルを HLOCAL にキャストし、LocalLock に渡して得たポインタを使用して内容にアクセスする事が可能になります。
		/// このポインタは、コントロールを作成した関数の文字セットに応じて CHAR 亦は WCHAR の null 終末の配列を指します。
		/// 使用した後は LocalUnlock を使用して Edit コントロールが新しい処理を実行する事が出来る様にしなければなりません。
		/// 亦、アプリケーションはこの内容を変更する事は出来ません。内容を変更したい場合には、GetWindowText を使用して内容を自分で用意したバッファにコピーして下さい。
		/// [RichEdit]: RichEdit コントロールは文字列を単純な配列として格納している訳ではないのでこのメッセージを使用する事は出来ません。
		/// </remarks>
		public const WM GETHANDLE=(WM)0x00BD;
		/// <summary>
		/// 複数行 Edit ボックスの垂直スクロールバーのサム (スクロールボックス) の位置を取得します。
		/// </summary>
		/// <wparam>常に 0 を指定します。</wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>スクロールボックスの位置を返します。</return>
		/// <remarks>[RichEdit]: 2.0 以降で対応指定しています。</remarks>
		public const WM GETTHUMB=(WM)0x00BE;
		public const WM LINELENGTH=(WM)0x00C1;
		public const WM REPLACESEL=(WM)0x00C2;
		/// <summary>
		/// 指定した行の内容を指定したバッファにコピーします。
		/// </summary>
		/// <wparam>行のインデックスを指定します。単一行 Edit コントロールでは無視されます。</wparam>
		/// <lparam>
		/// 文字列を取得する為のバッファへのポインタを指定します。
		/// メッセージを送る前に、バッファのサイズを TCHAR 単位でバッファの 1 word 目に書き込んでおいて下さい。
		/// </lparam>
		/// <return>コピーされた文字の数を返します。wParam に Edit コントロールが現在持つ行数以上の値を設定した場合には 0 を返します。
		/// </return>
		/// <remarks>
		/// [Edit]: コピーされた行は null 終末ではありません。
		/// [RichEdit]: コピーされた行は null 終末ではありません。但し、何の文字列もコピーされなかった場合には 1 文字目に null を設定します。
		/// </remarks>
		public const WM GETLINE=(WM)0x00C4;
		/// <summary>
		/// EM_SETLIMITTEXT を参照して下さい。
		/// </summary>
		public const WM LIMITTEXT=(WM)0x00C5;
		/// <summary>
		/// 「元に戻す」操作を現在実行出来るかどうかを決定します。
		/// Edit コントロール亦は RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <wparam>常に 0 を指定します。</wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>「元に戻す」操作を実行可能の時に 0 以外の値が返ります。</return>
		public const WM CANUNDO=(WM)0x00C6;
		public const WM UNDO=(WM)0x00C7;
		/// <summary>
		/// 複数行 Edit コントロールでソフト改行を使用するかどうかを設定します。
		/// ソフト改行は二つの CR 及び一つの LF からなり(\r\r\n)、ワードラッピングによる改行の後に挿入されます。
		/// </summary>
		/// <wparam>TRUE を指定するとソフト改行を挿入する事を指定します。
		/// FALSE を指定するとソフト改行を挿入しない事を指定します。</wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>wParam に指定した値を返します。</return>
		/// <remarks>このメッセージは EM_GETHANDLE によって返されるバッファ、及び WM_GETTEXT によって返される文字列のみに影響します。
		/// エディットコントロールへの表示には何も効果はありません。
		/// EM_FMTLINES は通常の改行 (\r\n) には影響しません。
		/// 亦、このメッセージを処理する事により文字列の長さは変化します。
		/// [RichEdit]: このメッセージには対応していません。
		/// </remarks>
		public const WM FMTLINES=(WM)0x00C8;
		public const WM LINEFROMCHAR=(WM)0x00C9;
		/// <summary>
		/// 複数行 Edit コントロールのタブストップ位置を設定します。
		/// コントロールに入力されたタブは次のタブストップ位置まで空白を開ける働きを持ちます。
		/// Edit/RichEdit コントロールに送る事が出来ます。但し、このメッセージは単一行 Edit コントロールでは処理されません。
		/// タブストップ位置の指定には水平方向の dialog-template-units が使用されます。
		/// 1 horizontal dialog-template-unit== 1/4 average width of the font です。
		/// 茲の説明では dtu と略記します。
		/// </summary>
		/// <wparam>lParam で指定する配列の長さを指定します。
		/// 0 を指定した場合には 32dtu 置きにタブストップ位置が設定されます。
		/// 1 を指定した場合には lParam の参照先の unsigned int の長さ置きにタブストップ位置が設定されます。
		/// 1 より大きな値を設定した場合には lParam はタブストップ位置の配列へのポインタになります。
		/// </wparam>
		/// <lparam>
		/// タブストップを指定する unsigned int の配列へのポインタを指定します。
		/// wParam に 1 を指定した場合にはそれぞれのタブストップの間の間隔を指定する unsigned int へのポインタになります。
		/// [95/98/Me]: このメッセージによって配列が変更される事はありませんが、lParam によって指定されるバッファは書込可能なメモリに存在する必要があります。
		/// </lparam>
		/// <return>指定した全てのタブが設定された場合に TRUE を返します。</return>
		/// <remarks>
		/// EM_SETTABSTOPS メッセージは自動的には再描画を行いません。
		/// 既に文字列が Edit コントロールに設定されている状態でタブストップの位置を変更したい場合には、InvalidateRect 関数を呼び出して Edit コントロールウィンドウを再描画させなければなりません。
		/// 配列に指定される値は dtu 単位です。dtu 単位はダイアログボックスのテンプレートに使用されるデバイス依存の単位です。
		/// dtu 単位からスクリーン単位 (ピクセル) に変換するには MapDialogRect 関数を使用して下さい。
		/// [RichEdit]: 3.0 以降で対応しています。
		/// </remarks>
		public const WM SETTABSTOPS=(WM)0x00CB;
		/// <summary>
		/// ユーザが文字列を入力した時に、入力した文字の代わりに表示する「パスワード文字」を設定します。
		/// Edit/RichEdit コントロールに指定する事が出来ます。
		/// </summary>
		/// <wparam>
		/// ユーザの入力した文字の代わりに表示する文字を指定します。
		/// 0 を指定した場合にはパスワード文字は無効になり、ユーザの入力した文字列が直接表示されます。
		/// </wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>値は返しません。</return>
		/// <remarks>
		/// Edit コントロールが EM_SETPASSWORDCHAR を受け取ると、全ての見える位置にある文字を wParam で指定された文字で描画し直します。
		/// wParam が 0 の場合にはユーザによって入力された文字列をそのまま描画します。
		/// Edit コントロールが ES_PASSWORD スタイルを指定して作成された場合には、既定のパスワード文字はアスタリスク (*) になります。
		/// ES_PASSWORD を指定せずに作成された場合には「パスワード文字」は存在しません。
		/// wParam を 0 にして EM_SETPASSWORDCHAR メッセージを送られた場合には ES_PASSWORD スタイルは削除されます。
		/// [XP]: Edit コントロールが user32.dll から ES_PASSWORD を以て作成された場合には既定のパスワード文字はアスタリスクになります。
		/// 然し、comctl32.dll ver 6 から ES_PASSWORD を以て作成された場合には既定の文字は黒丸になります。
		/// comctl32.dll ver 6 は再頒布出来ませんが XP 以降に含まれている事に留意して下さい。
		/// comctl32.dll ver 6 を使用するにはマニフェストで指定して下さい。詳細は XP ビジュアルスタイルの使用で調べて下さい。
		/// [Edit]: 複数行 Edit コントロールはパスワード様式及びメッセージに対応しません。
		/// [RichEdit]: 2.0 以降で対応しています。複数行及び単一行、両方でパスワード様式及びメッセージに対応しています。
		/// </remarks>
		public const WM SETPASSWORDCHAR=(WM)0x00CC;
		/// <summary>
		/// 「元に戻す」・「やり直す」を行う為の操作の記録を消します。Edit/RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <wparam>常に 0 を指定します。</wparam><lparam>常に 0 を指定します。</lparam>
		/// <return>値は返しません。</return>
		/// <remarks>
		/// [EDIT]: 最近の操作のみ元に戻す事が出来ます。
		/// [RichEdit 1.0]: 最近の操作のみ元に戻す事が出来ます。
		/// [RichEdit 2.0 以降]: 複数の操作に関して元に戻す操作を提供します。このメッセージは全ての記録を消去します。
		/// </remarks>
		public const WM EMPTYUNDOBUFFER=(WM)0x00CD;
		/// <summary>
		/// 複数行 Edit コントロールに於いて一番上に表示されている行のインデックスを取得します。
		/// Edit/RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <lparam>常に 0 を指定します。</lparam><wparam>常に 0 を指定します。</wparam>
		/// <return>
		/// 一番上に表示されている行のインデックスを返します。
		/// EDIT: 単一行 Edit コントロールの場合、一番初めに表示されている文字のインデックスを返します。
		/// </return>
		public const WM GETFIRSTVISIBLELINE=(WM)0x00CE;
		/// <summary>
		/// Edit コントロールに読込専用スタイルを設定・解除します。
		/// Edit/RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <wparam>
		/// ES_READONLY スタイルを設定するか解除するかを指定します。
		/// TRUE を指定した場合 ES_READONLY スタイルを設定します。FALSE を指定すると ES_REAONLY スタイルを解除します。
		/// </wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>操作が成功した場合に 0 でない数を返します。</return>
		/// <remarks>
		/// Edit コントロールに ES_READONLY スタイルが設定されている場合ユーザは Edit コントロール内の文字列を変更する事は出来ません。
		/// Edit コントロールが ES_READONLY スタイルを持つかどうかを取得するには GWL_STYLE を用いて GetWindowLong 関数を使用して下さい。
		/// </remarks>
		public const WM SETREADONLY=(WM)0x00CF;
		/// <summary>
		/// Edit コントロールのワードラップを管理する関数を指定します。
		/// Edit/RichEdit コントロールに指定する事が出来ます。
		/// </summary>
		/// <wparam>常に 0 を指定します。</wparam>
		/// <lparam>アプリケーション定義のワードラップ関数を EditWordBreakProc で指定します。</lparam>
		/// <return>値は返しません。</return>
		/// <remarks>
		/// ワードラップ関数はスクリーン上に描画される文字列のバッファを読み、表示の際の改行位置を決定します。
		/// ワードラップ関数は、複数行 Edit コントロールの文字列に於いて改行しても良い点を定義します。通常はスペース文字などに定義されます。
		/// 単一行 Edit コントロールでも単語の境界を計算する為に使用されます。これは、ユーザが [Ctrl] と共に矢印キーを使用して単語境界の間を移動する際に使用されます。
		/// アプリケーション定義のワードラップ関数を使用する事により、スペース以外のハイフンなどの文字で改行させる事が可能になります。
		/// </remarks>
		public const WM SETWORDBREAKPROC=(WM)0x00D0;
		public const WM GETWORDBREAKPROC=(WM)0x00D1;
		/// <summary>
		/// ユーザが文字列を入力した時に、入力した文字の代わりに表示する「パスワード文字」を取得します。
		/// Edit/RichEdit コントロールに指定する事が出来ます。
		/// </summary>
		/// <wparam>常に 0 を指定します。</wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>
		/// ユーザが入力した文字の代わりに表示する文字を返します。
		/// 若し戻り値が NULL であった場合、Edit コントロールはユーザの入力した文字を直接表示します。
		/// </return>
		/// <remarks>
		/// Edit コントロールが ES_PASSWORD スタイルを指定して作成された場合には、既定のパスワード文字はアスタリスク (*) になります。
		/// ES_PASSWORD を指定せずに作成された場合には「パスワード文字」は存在しません。
		/// パスワード文字を変更するには EM_SETPASSWORDCHAR を送ります。
		/// [XP]: Edit コントロールが user32.dll から ES_PASSWORD を以て作成された場合には既定のパスワード文字はアスタリスクになります。
		/// 然し、comctl32.dll ver 6 から ES_PASSWORD を以て作成された場合には既定の文字は黒丸になります。
		/// comctl32.dll ver 6 は再頒布出来ませんが XP 以降に含まれている事に留意して下さい。
		/// comctl32.dll ver 6 を使用するにはマニフェストで指定して下さい。詳細は XP ビジュアルスタイルの使用で調べて下さい。
		/// [Edit]: 複数行 Edit コントロールはパスワード様式及びメッセージに対応しません。
		/// [RichEdit]: 2.0 以降で対応しています。複数行及び単一行、両方でパスワード様式及びメッセージに対応しています。
		/// </remarks>
		public const WM GETPASSWORDCHAR=(WM)0x00D2;
		//#if(WINVER >= 0x0400)
		/// <summary>
		/// Edit コントロールの左右の余白を設定します。
		/// 余白の大きさを反映する為に、このメッセージによって再描画が為されます。
		/// Edit/RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <wparam>
		/// 設定する余白の種類を EC で設定します。
		/// [Edit]: EC_USEFONTINFO は wParam に使用出来ません。lParam に指定して下さい。
		/// [RichEdit]: EC_USEFONTINFO を使用した時には lParam は無視されます。
		/// </wparam>
		/// <lparam>
		/// 下位ワードには新しい左余白を指定します。EC_LEFTMARGIN が設定されていない時には無視されます。
		/// 上位ワードには新しい右余白を指定します。EC_RIGHTMARGIN が設定されていない時には無視されます。
		/// </lparam>
		/// <return>値は返しません。</return>
		/// <remarks>
		/// [Edit]: EC_USEFONTINFO は wParam に使用出来ません。lParam に指定して下さい。
		/// [RichEdit]: EC_USEFONTINFO は wParam に指定する事が出来ます。
		/// [RichEdit 3.0 以降]: EC_USEFONTINFO は wParam 及び lParam のどちらにも使用する事が出来ます。
		/// </remarks>
		public const WM SETMARGINS=(WM)0x00D3;
		/// <summary>
		/// Edit コントロールの左右の余白を取得します。
		/// </summary>
		/// <wparam>常に 0 を指定します。</wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>下位ワードに左余白を返します。亦、上位ワードに右余白を返します。</return>
		/// <remarks>[RichEdit]: 対応していません。</remarks>
		public const WM GETMARGINS=(WM)0x00D4;
		/// <summary>
		/// Edit コントロールに文字数の限度を設定します。
		/// Edit/RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <wparam>
		/// TCHAR による文字数の制限を指定します。末端の null 文字は文字数に含みません。
		/// [RichEdit]: 0 を指定すると制限文字数は 64000 文字になります。
		/// [Edit on NT/2000/XP]: 0 を指定すると、単一行の場合制限文字数は 0x7FFFFFFE になります。複数行の場合は -1 になります。
		/// [Edit on 95/98/Me]:
		/// </wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>値は返しません。</return>
		/// <remarks>
		/// このメッセージによって制限を受けるのはユーザの入力する文字のみです。
		/// メッセージが送られた時に既に Edit コントロールの中に入っている文字には何の影響もありません。
		/// 亦、WM_SETTEXT によって設定される文字列にも影響しません。
		/// 若し WM_SETTEXT によって制限を超える文字列を Edit コントロールに設定した場合にはユーザは自由に文字列を編集する事が出来る様になります。
		/// 既定の Edit コントロールの入力制限数は 32767 文字です。
		/// [Edit on NT/2000/XP]: 単一行 Edit コントロールの場合、制限は 0x7FFFFFFE バイト及び wParam で指定した値の小さい方になります。
		/// 複数行 Edit コントロールの場合は制限値は -1 亦は wParam で指定した値の小さい方になります。
		/// [Edit on 95/98/Me]: 単一行 Edit コントロールの場合は、制限は 0x7FFE バイト及び wParam で指定した値の小さい方になります。
		/// 複数行 Edit コントロールの場合には 0xFFFF バイト亦は wParam で指定した値の小さい方になります。
		/// [RichEdit] 1.0 以降で対応しています。
		/// </remarks>
		public const WM SETLIMITTEXT=(WM)0x00C5; // EM_LIMITTEXT // win40 Name change
		/// <summary>
		/// 現在の入力文字数の制限を取得します。
		/// Edit/RichEdit コントロールに使用出来ます。
		/// </summary>
		/// <wparam>常に 0 を指定します。</wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>入力文字数の制限値を取得します。</return>
		/// <remarks>
		/// [Edit/RichEdit 2.0 以降]: 文字数の制限はコントロールが含む事が出来る TCHAR による最大の文字数です。
		/// [RichEdit 1.0]: 文字数の制限は、コントロールが含む事が出来るバイト数の最大値です。
		/// </remarks>
		public const WM GETLIMITTEXT=(WM)0x00D5;
		/// <summary>
		/// Edit コントロール内の指定した座標にある文字のクライアント座標を取得します。
		/// Edit/RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <wparam>
		/// [RichEdit 1.0/3.0]: 指定した文字のクライアント座標を受け取る為の POINTL 構造体へのポインタを指定します。
		/// 座標はコントロールのクライアント領域の左上隅の点を原点にしたピクセル単位の座標です。
		/// [Edit/RichEdit 2.0]: 文字のインデックスを指定します。
		/// </wparam>
		/// <lparam>
		/// [RichEdit 1.0/3.0]: 文字のインデックスを指定します。
		/// [Edit/RichEdit 2.0]: 常に 0 を指定します。
		/// </lparam>
		/// <return>
		/// [RichEdit 1.0/3.0]: 戻り値は使用されません。
		/// [Edit/RichEdit 2.0]: 指定した文字のクライアント座標を返します。
		/// 下位ワードに水平座標、上位ワードに垂直座標を指定します。
		/// </return>
		/// <remarks>
		/// 結果の値は、指定した文字が Edit コントロールに表示されていない時に負になる可能性もあります。亦、座標値は整数値に丸められます。
		/// 指定した文字が改行文字の場合は、その行で見えている最後の文字の座標を返します。指定したインデックスが Edit コントロール内の文字数異常の場合はコントロールは結果に -1 を返します。
		/// [RichEdit 3.0-]: 互換性の為、RichEdit 2.0 で使用されたメッセージ形式にも対応します。
		/// wParam が有効な POINTL 構造体でないと判断した場合には、RichEdit 2.0 で使用された指定の仕方であると見なして処理を行います。
		/// この場合には結果の座標を戻り値に返します。
		/// </remarks>
		public const WM POSFROMCHAR=(WM)0x00D6;
		/// <summary>
		/// 指定したクライアント座標に最も近い位置にある文字についての情報を取得します。
		/// Edit/RichEdit コントロールに送る事が出来ます。
		/// </summary>
		/// <wparam>常に 0 を指定します。</wparam>
		/// <lparam>
		/// [RichEdit]: POINTL 構造体へのポインタを指定します。
		/// [EDIT]: 下位ワードには水平座標を、高位ワードには垂直座標を指定します。
		/// </lparam>
		/// <return>
		/// 指定した点に最も近い位置にある文字のインデックスを返します。最後の文字よりも後ろにある領域を指定すると、最後の文字のインデックスを返します。
		/// [RichEdit]: 上記のインデックスを返します。
		/// [EDIT]: 下位ワードに上記のインデックスを返します。インデックスはその行の中のインデックスではなくてコントロールないでのインデックスです。
		/// 高位ワードには行の番号を返します。単一行の場合には常に高位ワードに 0 を返します。その行で最後の文字よりも後に座標が存在する場合には、文字のインデックスは改行文字を示します。
		/// </return>
		public const WM CHARFROMPOS=(WM)0x00D7;
		//#endif //WINVER >= 0x0400
		//#if(WINVER >= 0x0500)
		/// <summary>
		/// Edit コントロールと IME の間の通信方法を示すフラグを設定します。
		/// </summary>
		/// <wparam>
		/// 設定する状態の種類を指定します。
		/// EMSIS_COMPOSITIONSTRING を指定する事が出来ます。
		/// </wparam>
		/// <lparam>設定する値を指定します。wParam に指定する値によって内容は変わります。</lparam>
		/// <remarks>RichEdit は対応していません。</remarks>
		public const WM SETIMESTATUS=(WM)0x00D8;
		/// <summary>
		/// Edit コントロールと IME の間の通信方法を示すフラグを取得します。
		/// </summary>
		/// <wparam>
		/// 取得する状態の種類を指定します。
		/// EMSIS_COMPOSITIONSTRING を指定する事が出来ます。
		/// </wparam>
		/// <lparam>常に 0 を指定します。</lparam>
		/// <return>wParam に指定した値に応じた値が返されます。</return>
		public const WM GETIMESTATUS=(WM)0x00D9;
		//#endif //WINVER >= 0x0500
		//commctrl32.h にも宣言あり
		//EM_SETCUEBANNER
		//EM_EXGETSEL
		//EM_GETHILITE EM_SETHILITE//vista
	}
	/// <summary>
	/// 余白を設定する為のフラグです。
	/// </summary>
	[System.Flags]
	public enum EC{
		/// <summary>
		/// 左余白を設定します。
		/// </summary>
		LEFTMARGIN=0x0001,
		/// <summary>
		/// 右余白を設定します。
		/// </summary>
		RIGHTMARGIN=0x0002,
		/// <summary>
		/// これを指定した場合には現在のフォントを元にして設定されます。
		/// 左余白は A の幅に、右余白は C の幅に合わせられます。
		/// (http://homepage2.nifty.com/Mr_XRAY/Halbow/Chap12.html による)
		/// </summary>
		/// <remarks>
		/// [Edit]: EC_USEFONTINFO は EM_SETMARGINS wParam に使用出来ません。EM_SETMARGINS lParam に指定して下さい。
		/// [RichEdit]: EC_USEFONTINFO を使用した時には EM_SETMARGINS lParam は無視されます。
		/// </remarks>
		USEFONTINFO=0xffff
	}
	/// <summary>
	/// EM_GETIMESTATUS・EM_SETIMESTATUS で取得・設定する値の種類を指定します。
	/// </summary>
	public enum EMSIS{
		/// <summary>
		/// この値を指定した場合、戻り値亦は lparam に指定する値は EIMES の組合せで表されます。
		/// </summary>
		COMPOSITIONSTRING=0x0001
	}
	/// <summary>
	/// Edit コントロールに於ける IME の状態を指定します。
	/// </summary>
	[System.Flags]
	public enum EIMES{
		/// <summary>
		/// IME の結果を WM_IME_COMPOSITION (GCS_RESULTSTR) を受け取った際に一度に取得する事を指定します。
		/// この指定がない場合には WM_CHAR を使用して結果を一文字ずつ取得します。
		/// 既定では設定されていません。
		/// </summary>
		GETCOMPSTRATONCE=0x0001,
		/// <summary>
		/// これが設定されている時には、このコントロールが WM_SETFOCUS によって Focus を得た時に、編集中の文字列をクリアします。
		/// 設定されていない時には編集中の文字列を消去しません。
		/// 既定では設定されていません。
		/// </summary>
		CANCELCOMPSTRINFOCUS=0x0002,
		/// <summary>
		/// これが設定されている時には、WM_KILLFOCUS が来た時に編集中の文字列を確定させます。
		/// 設定されていない時にはフォーカスを失っても編集中の文字列は確定されません。
		/// 既定では設定されません。
		/// </summary>
		COMPLETECOMPSTRKILLFOCUS=0x0004
	}
}