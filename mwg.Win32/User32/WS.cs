namespace mwg.Win32{
	// Quote: http://msdn2.microsoft.com/ja-jp/library/xyfwf42d(VS.80).aspx
	// Quote: http://www.activebasic.com/help_center/Pages/API/Window/Window/CreateWindowEx.htm
	/// <summary>
	/// ウィンドウのスタイルを指定するのに使用します。
	/// </summary>
	[System.Flags]
	public enum WS:uint{
		/// <summary>
		/// オーバーラップウィンドウである事を示します。
		/// オーバーラップウィンドウには通常キャプションと境界線があります。
		/// </summary>
		OVERLAPPED=0x00000000,
		/// <summary>
		/// ポップアップウィンドウである事を示します。
		/// CHILD スタイルと共に使用する事は出来ません。
		/// </summary>
		POPUP		=0x80000000,
		/// <summary>
		/// 子ウィンドウを作成します。
		/// POPUP スタイルと共に使用する事は出来ません。 
		/// </summary>
		CHILD		=0x40000000,
		/// <summary>
		/// ウィンドウが最小化されている事を示します。
		/// OVERLAPPED と共に使用します。
		/// </summary>
		MINIMIZE	=0x20000000,
		/// <summary>
		/// ウィンドウが表示されている事を示します。
		/// </summary>
		VISIBLE		=0x10000000,
		/// <summary>
		/// ウィンドウが無効になっている事を示します。
		/// </summary>
		DISABLED	=0x08000000,
		/// <summary>
		/// 関連する子ウィンドウをクリップします。
		/// つまり、1 つの子ウィンドウが描画メッセージを受け取ると、 CLIPSIBLINGS スタイルが適用されている場合、
		/// 更新する子ウィンドウの領域から、そのウィンドウと重複しているほかの子ウィンドウをすべてクリップします。
		/// CLIPSIBLINGS が指定されていない場合に、子ウィンドウが重複していると、
		/// 1 つの子ウィンドウのクライアント領域で描画するときに、
		/// 隣接する子ウィンドウのクライアント領域に描画してしまう可能性があります。
		/// 必ず WS_CHILD スタイルと一緒に使います。
		/// </summary>
		CLIPSIBLINGS=0x04000000,
		/// <summary>
		/// 親ウィンドウの内部で描画するときに、子ウィンドウが占める領域を除外します。
		/// 親ウィンドウを作成するときに使用します。
		/// </summary>
		CLIPCHILDREN=0x02000000,
		/// <summary>
		/// ウィンドウが最大化している事を示します。
		/// </summary>
		MAXIMIZE	=0x01000000,
		/// <summary>
		/// ウィンドウがタイトルバーを持つ事を示します。
		/// </summary>
		CAPTION=0x00C00000,		/* WS_BORDER | WS_DLGFRAME  */
		/// <summary>
		/// ウィンドウが境界線を持つ事を示します。
		/// </summary>
		BORDER		=0x00800000,
		/// <summary>
		/// ウィンドウが二重の境界線を持つ事を示します。
		/// </summary>
		DLGFRAME	=0x00400000,
		/// <summary>
		/// ウィンドウが垂直スクロールバーを持つ事を示します。
		/// </summary>
		VSCROLL		=0x00200000,
		/// <summary>
		/// ウィンドウが水平スクロールバーを持つ事を示します。
		/// </summary>
		HSCROLL		=0x00100000,
		/// <summary>
		/// タイトルバーにコントロールメニューボックスを持つ事を示します。
		/// タイトルバーを持つウィンドウだけに指定する事が出来ます。
		/// </summary>
		SYSMENU		=0x00080000,
		/// <summary>
		/// ウィンドウがサイズ変更に使用する事の出来る太い枠を持つ事を示します。
		/// </summary>
		THICKFRAME	=0x00040000,
		/// <summary>
		/// コントロールグループの最初のウィンドウである事を示します。
		/// コントロールグループではユーザーは方向キーを使用して、或るコントロールから次のコントロールに移動出来ます。
		/// 最初のコントロールの後から GROUP スタイルを FALSE と指定して定義したコントロールは全て同じグループに属します。
		/// GROUP スタイルを持つ次のコントロールから次のグループに属す様になります。
		/// </summary>
		GROUP		=0x00020000,
		/// <summary>
		/// Tab キーを押してユーザーがフォーカスを移動できることを示します。
		/// ユーザーが Tab キーを押すと、TABSTOP スタイルが指定されている次のコントロールにフォーカスが移動します。
		/// </summary>
		TABSTOP		=0x00010000,

		/// <summary>
		/// ウィンドウが最小化ボタンを持つ事を示します。
		/// </summary>
		MINIMIZEBOX=0x00020000,
		/// <summary>
		/// ウィンドウが最大化ボタンを持つ事を示します。
		/// </summary>
		MAXIMIZEBOX=0x00010000,

		/// <summary>
		/// オーバーラップウィンドウである事を示します。
		/// オーバーラップウィンドウには通常キャプションと境界線があります。
		/// OVERLAPPED と同じです。
		/// </summary>
		TILED=OVERLAPPED,
		/// <summary>
		/// ウィンドウが最小化された状態である事を示します。
		/// MIMIZE スタイルと同じです。
		/// </summary>
		ICONIC=MINIMIZE,
		/// <summary>
		/// ウィンドウがサイズ変更境界を持つ事を示します。
		/// THICKFRAME と同じです。
		/// </summary>
		SIZEBOX=THICKFRAME,
		/// <summary>
		/// OVERLAPPED、CAPTION、SYSMENU、THICKFRAME、MINIMIZEBOX
		/// 及び MAXIMIZEBOX スタイルを持つオーバラップ ウィンドウで在る事を示します。
		/// OVERLAPPEDWINDOW と同じです。
		/// </summary>
		TILEDWINDOW=OVERLAPPEDWINDOW,

		/*
		 * Common Window Styles
		 */
		/// <summary>
		/// OVERLAPPED、CAPTION、SYSMENU、THICKFRAME、MINIMIZEBOX
		/// 及び MAXIMIZEBOX スタイルを持つオーバラップ ウィンドウで在る事を示します。
		/// </summary>
		OVERLAPPEDWINDOW=OVERLAPPED|CAPTION|SYSMENU|THICKFRAME|MINIMIZEBOX|MAXIMIZEBOX,
		/// <summary>
		/// BORDER, POPUP, SYSMENU スタイルを持つポップアップウィンドウである事を示します。
		/// </summary>
		POPUPWINDOW=POPUP|BORDER|SYSMENU,
		/// <summary>
		/// CHILD と同じです。
		/// </summary>
		CHILDWINDOW=CHILD
	}
	/// <summary>
	/// ボタンのスタイルを指定します。
	/// </summary>
	public static class BS{
		/// <summary>
		/// プッシュボタンである事を示します。 
		/// </summary>
		public const WS PUSHBUTTON=WS.OVERLAPPED;	// 0x00
		/// <summary>
		/// プッシュボタンである事を示します。ただし、黒色の太い境界も持ちます。
		/// このボタンがダイアログボックスにある場合は、ボタンが入力フォーカスを持っていなくても、エンターキーを押せばボタンを選択できます。 
		/// </summary>
		public const WS DEFPUSHBUTTON=(WS)0x00000001;
		/// <summary>
		/// チェックボックスである事を示します。
		/// </summary>
		public const WS CHECKBOX=(WS)0x00000002;
		/// <summary>
		/// チェックボックスである事を示します。
		/// ただし、ユーザーがチェックボックスを選択するとボックスの状態が自動的に変わります。
		/// </summary>
		public const WS AUTOCHECKBOX=(WS)0x00000003;
		/// <summary>
		/// ラジオボタンである事を示します。 
		/// </summary>
		public const WS RADIOBUTTON=(WS)0x00000004;
		/// <summary>
		/// 選択された状態、選択されていない状態、グレー表示の状態という3つの状態を持つチェックボックスである事を示します。
		/// グレーの状態は、チェックボックスの状態が決められていないことを示すときなどに使います。
		/// </summary>
		public const WS _3STATE=(WS)0x00000005;
		/// <summary>
		/// _3STATEと同じボタンです。ただし、ユーザーがチェックボックスを選択するとボックスの状態が自動的に変わります。 
		/// </summary>
		public const WS AUTO3STATE=(WS)0x00000006;
		/// <summary>
		/// グループボックスである事を示します。ほかのコントロールを、このコントロールの中にグループ化できます。 
		/// </summary>
		public const WS GROUPBOX=(WS)0x00000007;
		/// <summary>
		/// OWNERDRAW の古い形式です。
		/// </summary>
		[System.Obsolete("OWNERDRAW を使用して下さい。")]
		public const WS USERBUTTON=(WS)0x00000008;
		/// <summary>
		/// ラジオボタンである事を示します。
		/// ただし、ユーザーがボタンを選択すると、Windowsが自動的にボタンを選択状態にし、同じグループのほかのボタンを非選択状態にします。 
		/// </summary>
		public const WS AUTORADIOBUTTON=(WS)0x00000009;
		/// <summary>
		/// 対応されていません。
		/// </summary>
		[System.Obsolete("対応されていません。")]
		public const WS PUSHBOX=(WS)0x0000000A;
		/// <summary>
		/// オーナー描画ボタンを作成します。
		/// オーナーウィンドウは、ボタンが作成されると WM.MEASUREITEM メッセージを受け取り、ボタンの外観が変わると WM.DRAWITEM メッセージを受け取ります。
		/// 他のボタンスタイルと組み合わせることはできません。 
		/// </summary>
		public const WS OWNERDRAW=(WS)0x0000000B;
		/// <summary>
		/// ボタンの種類を指定している部分のビットマスクです。
		/// </summary>
		public const WS TYPEMASK=(WS)0x0000000F;
		/// <summary>
		/// ラジオボタンスタイルやチェックボックススタイルと組み合わせると、ラジオボタンの円やチェックボックスの四角の左側にテキストが置かれます。RIGHTBUTTON スタイルを同じです。 
		/// </summary>
		public const WS LEFTTEXT=(WS)0x00000020;

		// 以下 if(WINVER >= 0x0400)
		/// <summary>
		/// ボタンがテキストを表示するよう指定します。 
		/// </summary>
		public const WS TEXT=WS.OVERLAPPED;	//0x00
		/// <summary>
		/// アイコンを表示するボタンを作成します。 
		/// </summary>
		public const WS ICON=(WS)0x00000040;
		/// <summary>
		/// ビットマップを表示するボタンを作成します。 
		/// </summary>
		public const WS BITMAP=(WS)0x00000080;
		/// <summary>
		/// ボタンの中にテキストを左寄せします。ボタンが RIGHTBUTTON スタイルを持たないチェックボックス（またはラジオボタン）の場合は、テキストはチェックボックスやラジオボタンの右側に左寄せされます。 
		/// </summary>
		public const WS LEFT=(WS)0x00000100;
		/// <summary>
		/// ボタンの中にテキストを右寄せします。ボタンが RIGHTBUTTON スタイルを持たないチェックボックス（またはラジオボタン）の場合は、テキストはチェックボックスやラジオボタンの右側に右寄せされます。 
		/// </summary>
		public const WS RIGHT=(WS)0x00000200;
		/// <summary>
		/// ボタンの中央にテキスト置きます。 
		/// </summary>
		public const WS CENTER=(WS)0x00000300;
		/// <summary>
		/// ボタンの最上部にテキストを置きます。 
		/// </summary>
		public const WS TOP=(WS)0x00000400;
		/// <summary>
		/// ボタンの下部にテキストを置きます。 
		/// </summary>
		public const WS BOTTOM=(WS)0x00000800;
		/// <summary>
		/// ボタンの（垂直方向の）中央にテキストを置きます。 
		/// </summary>
		public const WS VCENTER=(WS)0x00000C00;
		/// <summary>
		/// プッシュボタンのような概観と機能を持つ、チェックボックスまたはラジオボタンを作ります。 
		/// </summary>
		public const WS PUSHLIKE=(WS)0x00001000;
		/// <summary>
		/// テキストが長すぎてボタンの中に一行で収まらないときは、テキストを複数行に折り返します。 
		/// </summary>
		public const WS MULTILINE=(WS)0x00002000;
		/// <summary>
		/// 親ウィンドウに、ボタンがBN_DBLCLK通知メッセージ、BN_KILLFOCUS通知メッセージ、BN_SETFOCUS通知メッセージを送ることを可能にします。このスタイルを持つかどうかに関わらず、ボタンはBN_CLICKED通知メッセージを送ります。 
		/// </summary>
		public const WS NOTIFY=(WS)0x00004000;
		/// <summary>
		/// ボタンがフラットに描画される事を示します。
		/// </summary>
		public const WS FLAT=(WS)0x00008000;
		/// <summary>
		/// BS_LEFTTEXT スタイルと同じです。 
		/// </summary>
		public const WS RIGHTBUTTON=(WS)0x00000020;//LEFTTEXT;
	}
	/// <summary>
	/// コンボボックスのスタイルを指定する為の値です。
	/// </summary>
	public static class CBS{
		/// <summary> 
		/// リスト ボックスを常に表示します。 
		/// リスト ボックス内で現在選択されている項目が、エディット コントロールに表示されます。 
		/// </summary> 
		public const WS SIMPLE=(WS)0x0001; 
		/// <summary> 
		/// ユーザーがエディット コントロールの横にあるアイコンを選択しないとリスト ボックスが表示されない点を除いて、CBS_SIMPLE と同じです。 
		/// </summary> 
		public const WS DROPDOWN=(WS)0x0002; 
		/// <summary> 
		/// エディット コントロールが、リスト ボックス内で現在選択されている項目を表示する静的なテキスト項目に置き換えられる点を除いて、CBS_DROPDOWN と同じです。 
		/// </summary> 
		public const WS DROPDOWNLIST=(WS)0x0003; 
		/// <summary> 
		/// リスト ボックスのオーナーが、そのリスト ボックスの内容を描画します。 
		/// リスト ボックス内の項目は、すべて同じ高さで描画されます。 
		/// </summary> 
		public const WS OWNERDRAWFIXED=(WS)0x0010; 
		/// <summary> 
		/// リスト ボックスのオーナーが、そのリスト ボックスの内容を描画します。 
		/// リスト ボックス内の項目の高さは固定されません。 
		/// </summary> 
		public const WS OWNERDRAWVARIABLE=(WS)0x0020; 
		/// <summary> 
		/// ユーザーが行末に文字を入力したときに、エディット コントロール内のテキストを自動的に右にスクロールします。 
		/// このスタイルが設定されていないと、四角形境界内に収まる長さのテキストしか入力できません。 
		/// </summary> 
		public const WS AUTOHSCROLL=(WS)0x0040; 
		/// <summary> 
		/// コンボ ボックス エディット コントロールに入力されたテキストを ANSI 文字セットから OEM 文字セットへ変換し、その後、ANSI 文字セットに戻します。 
		/// これにより、コンボ ボックス内の ANSI 文字列を OEM 文字に変換するためにアプリケーションが Windows 関数 AnsiToOem を呼び出したときに、文字変換が適切に処理されます。 
		/// このスタイルは、ファイル名を保持するコンボ ボックスで使用すると役に立ちます。 
		/// CBS_SIMPLE または CBS_DROPDOWN のスタイルが適用されているコンボ ボックスでだけ指定できます。 
		/// </summary> 
		public const WS OEMCONVERT=(WS)0x0080; 
		/// <summary> 
		/// リスト ボックスに入力された文字列を自動的に並べ替えます。 
		/// </summary> 
		public const WS SORT=(WS)0x0100; 
		/// <summary> 
		/// 文字列で構成される項目を格納するオーナー描画コンボ ボックスです。 
		/// コンボ ボックスは文字列に割り当てるメモリやポインタを維持するため、アプリケーションで、GetText メンバ関数を使用して特定の項目のテキストを取得できます。 
		/// </summary> 
		public const WS HASSTRINGS=(WS)0x0200; 
		/// <summary> 
		/// コンボ ボックスのサイズが、そのコンボ ボックスがアプリケーションによって作成されたときの指定サイズと同じになるように指定します。 
		/// 通常は、コンボ ボックスに表示される項目が一部だけにならないように、Windows によってコンボ ボックスのサイズが調整されます。 
		/// </summary> 
		public const WS NOINTEGRALHEIGHT=(WS)0x0400; 
		/// <summary> 
		/// リスト ボックス内の項目が少なく、スクロールする必要がない場合、垂直スクロール バーを使用できない状態で表示します。 
		/// このスタイルが設定されていないと、項目が少なくてスクロールする必要がない場合、スクロール バーは表示されません。 
		/// </summary> 
		public const WS DISABLENOSCROLL=(WS)0x0800; 
		/// <summary> 
		/// 選択フィールドとリスト両方のすべてのテキストを大文字に変換します。 
		/// </summary> 
		public const WS UPPERCASE=(WS)0x2000; 
		/// <summary> 
		/// 選択フィールドとリスト両方のすべてのテキストを小文字に変換します。 
		/// </summary> 
		public const WS LOWERCASE=(WS)0x4000; 
	}
	/// <summary>
	/// エディットボックスのスタイルを指定するのに使用します。
	/// </summary>
	public static class ES{
		/// <summary>
		/// 単一行または複数行のエディット コントロールで、テキストを左揃えで表示します。
		/// </summary>
		public const WS LEFT=WS.OVERLAPPED;	// 0x00
		/// <summary>
		/// 単一行または複数行のエディット コントロールで、テキストを中央揃えで表示します。
		/// </summary>
		public const WS CENTER=(WS)0x0001;
		/// <summary>
		/// 単一行または複数行のエディット コントロールで、テキストを右揃えで表示します。
		/// </summary>
		public const WS RIGHT=(WS)0x0002;
		/// <summary>
		/// 複数行エディット コントロールを指定します。
		/// 既定では単一行のエディット コントロールが指定されます。
		/// ES_AUTOVSCROLL スタイルが指定されていると、エディット コントロールは、できるだけ多くの行を表示し、ユーザーが Enter キーを押したときに垂直方向にスクロールします。
		/// ES_AUTOVSCROLL が指定されていない場合、エディット コントロールは、できるだけ多くの行を表示しますが、それ以上行を表示できなくなったときにユーザーが Enter キーを押すと、ビープ音を鳴らします。
		/// ES_AUTOHSCROLL スタイルが指定されている場合、複数行エディット コントロールは、カレットがコントロールの右端を越えたときに自動的に水平方向にスクロールします。
		/// ユーザーは、行を新しくする場合は Enter キーを押す必要があります。
		/// ES_AUTOHSCROLL が指定されていない場合、必要に応じて自動的に次の行の先頭にテキストが折り返して表示されます。
		/// Enter キーを押して新しい行を始めることもできます。
		/// 折り返す位置は、ウィンドウのサイズによって決まります。
		/// ウィンドウのサイズが変更されると、折り返す位置も変更され、テキストが再表示されます。
		/// 複数行エディット コントロールには、スクロール バーを表示できます。
		/// スクロール バーがあるエディット コントロールは、スクロール バー メッセージを独自に処理します。
		/// スクロール バーがないエディット コントロールは、上で説明したようにスクロールし、親ウィンドウから送信されたスクロール メッセージを処理します。
		/// </summary>
		public const WS MULTILINE=(WS)0x0004;
		/// <summary>
		/// エディット コントロールに入力されたすべての文字を大文字に変換します。
		/// </summary>
		public const WS UPPERCASE=(WS)0x0008;
		/// <summary>
		/// エディット コントロールに入力された文字をすべて小文字に変換します。
		/// </summary>
		public const WS LOWERCASE=(WS)0x0010;
		/// <summary>
		/// エディット コントロールに入力されたすべての文字をアスタリスク (*) で表示します。
		/// アプリケーション側では、SetPasswordChar メンバ関数を使用して、表示される文字を変更できます。
		/// </summary>
		public const WS PASSWORD=(WS)0x0020;
		/// <summary>
		/// ユーザーが最終行で Enter キーを押したときに、自動的に 1 ページ分のテキストをスクロールします。
		/// </summary>
		public const WS AUTOVSCROLL=(WS)0x0040;
		/// <summary>
		/// ユーザーが行末に文字を入力したときに、テキストを自動的に 10 文字分右へスクロールします。
		/// ユーザーが Enter キーを押すと、すべてのテキストがスクロールされ、カーソル位置が 0 に戻ります。
		/// </summary>
		public const WS AUTOHSCROLL=(WS)0x0080;
		/// <summary>
		/// 通常、エディット コントロールでは、入力フォーカスが別のコントロールに移ると選択内容が非表示になり、入力フォーカスが受け取ると選択内容が強調表示されます。
		/// ES_NOHIDESEL を指定すると、この既定の動作が行われなくなります。
		/// </summary>
		public const WS NOHIDESEL=(WS)0x0100;
		/// <summary>
		/// エディット コントロールに入力されたテキストを ANSI 文字セットから OEM 文字セットに変換し、その後、ANSI 文字セットに戻します。
		/// これにより、コンボ ボックス内の ANSI 文字列を OEM 文字列に変換するためにアプリケーションが Windows 関数 AnsiToOem を呼び出したときに、文字変換が適切に処理されます。
		/// このスタイルは、ファイル名を保持するエディット コントロールで使用すると役に立ちます。
		/// </summary>
		public const WS OEMCONVERT=(WS)0x0400;
		/// <summary>
		/// エディット コントロール内でテキストを入力および編集できないようにします。
		/// </summary>
		public const WS READONLY=(WS)0x0800;
		/// <summary>
		/// ユーザーがダイアログ ボックス内の複数行エディット コントロールへのテキストの入力中に Enter キーを押したときに、キャリッジ リターンが挿入されるように指定します。
		/// このスタイルが指定されていない場合、Enter キーを押すことは、ダイアログ ボックスの既定のプッシュ ボタンを押したことと同じになります。
		/// このスタイルは、単一行のエディット コントロールには影響しません。
		/// </summary>
		public const WS WANTRETURN=(WS)0x1000;
		/// <summary>
		/// エディット コントロールに数字だけを入力できるようにします。
		/// </summary>
		public const WS NUMBER=(WS)0x2000;
	}
	/// <summary>
	/// リストボックスのスタイルを指定するのに使用します。
	/// </summary>
	public static class LBS{
		/// <summary>
		/// ユーザーが文字列をクリックまたはダブルクリックするたびに、親ウィンドウが入力メッセージを受け取ります。
		/// </summary>
		public const WS NOTIFY=(WS)0x0001;
		/// <summary>
		/// リスト ボックス内の文字列をアルファベット順に並べ替えます。
		/// </summary>
		public const WS SORT=(WS)0x0002;
		/// <summary>
		/// リスト ボックスが変更されても表示内容を更新しません。
		/// このスタイルは、WM_SETREDRAW メッセージを送信することにより、いつでも変更できます。
		/// </summary>
		public const WS NOREDRAW=(WS)0x0004;
		/// <summary>
		/// ユーザーが文字列をクリックまたはダブルクリックするたびに、選択されている文字列が切り替わります。
		/// 文字列はいくつでも選択できます。
		/// </summary>
		public const WS MULTIPLESEL=(WS)0x0008;
		/// <summary>
		/// リスト ボックスのオーナーが、そのリスト ボックスの内容を描画します。
		/// リスト ボックス内の項目は、すべて同じ高さで描画されます。
		/// </summary>
		public const WS OWNERDRAWFIXED=(WS)0x0010;
		/// <summary>
		/// リスト ボックスのオーナーが、そのリスト ボックスの内容を描画します。
		/// リスト ボックス内の項目の高さは固定されません。
		/// </summary>
		public const WS OWNERDRAWVARIABLE=(WS)0x0020;
		/// <summary>
		/// 文字列で構成される項目を格納するオーナー描画リスト ボックスを指定します。
		/// リスト ボックスは文字列に割り当てるメモリやポインタを維持するため、アプリケーションで、GetText メンバ関数を使用して特定の項目のテキストを取得できます。
		/// </summary>
		public const WS HASSTRINGS=(WS)0x0040;
		/// <summary>
		/// リスト ボックスに文字列が描画されるときに、タブ文字が認識および展開されるようにします。
		/// 既定のタブ位置は、32 ダイアログ単位です。
		/// ダイアログ単位は、水平方向または垂直方向の距離を表します。
		/// 1 水平ダイアログ単位は、現在のダイアログの基本幅の 4 分 1 です。
		/// ダイアログの基本単位は、現在のシステム フォントの高さと幅を基準に計算されます。
		/// Windows 関数 GetDialogBaseUnits は、現在のダイアログの基本単位をピクセル数で返します。
		/// このスタイルは、LBS_OWNERDRAWFIXED と組み合わせて指定しないでください。
		/// </summary>
		public const WS USETABSTOPS=(WS)0x0080;
		/// <summary>
		/// リスト ボックスのサイズが、そのリスト ボックスがアプリケーションによって作成されたときの指定サイズと同じになります。
		/// 通常は、リスト ボックスに表示される項目が一部だけにならないように、Windows によってリスト ボックスのサイズが調整されます。
		/// </summary>
		public const WS NOINTEGRALHEIGHT=(WS)0x0100;
		/// <summary>
		/// 水平にスクロールする複数列のリスト ボックスを指定します。
		/// SetColumnWidth メンバ関数は、列の幅を設定します。
		/// </summary>
		public const WS MULTICOLUMN=(WS)0x0200;
		/// <summary>
		/// リスト ボックスに入力フォーカスがあるときにユーザーがキーを押すと、リスト ボックスのオーナーが WM_VKEYTOITEM または WM_CHARTOITEM メッセージを受け取ります。
		/// これにより、キーボード入力に対してアプリケーションが特別な処理を実行できるようになります。
		/// </summary>
		public const WS WANTKEYBOARDINPUT=(WS)0x0400;
		/// <summary>
		/// Shift キーとマウス、または特殊なキーを組み合わせて使用して、ユーザーが複数の項目を選択できるようにします。
		/// </summary>
		public const WS EXTENDEDSEL=(WS)0x0800;
		/// <summary>
		/// リスト ボックス内の項目が少なく、スクロールする必要がない場合、垂直スクロール バーを使用できない状態で表示します。
		/// このスタイルが設定されていないと、項目が少なくてスクロールする必要がない場合、スクロール バーは表示されません。
		/// </summary>
		public const WS DISABLENOSCROLL=(WS)0x1000;
		/// <summary>
		/// データを持たないリスト ボックスを指定します。
		/// このスタイルは、リスト ボックスに含まれる項目の数が 1,000 を超える場合に指定します。
		/// データを持たないリスト ボックスには、LBS_OWNERDRAWFIXED スタイルも設定する必要があります。
		/// ただし、LBS_SORT スタイルまたは LBS_HASSTRINGS スタイルは設定しないでください。
		/// データを持たないリスト ボックスは、リスト項目を示す文字列やビットマップなどのデータを含んでいない点を除けば、オーナー描画リスト ボックスと同じです。
		/// 項目を追加、挿入、または削除するコマンドを実行しても、指定した項目のデータは常に無視されます。
		/// また、リスト ボックス内の文字列を検索する要求も必ず失敗します。
		/// 項目を描画する必要がある場合、オーナー ウィンドウに WM_DRAWITEM メッセージが送信されます。
		/// WM_DRAWITEM メッセージで渡された DRAWITEMSTRUCT 構造体の itemID メンバにより、描画する項目の行番号が指定されます。
		/// データを持たないリスト ボックスは、WM_DELETEITEM メッセージを送信しません。
		/// </summary>
		public const WS NODATA=(WS)0x2000;
		/// <summary>
		/// リスト ボックスに、表示できるが選択できない項目が含まれるように指定します。
		/// </summary>
		public const WS NOSEL=(WS)0x4000;
		/// <summary>
		/// リストボックスがコンボボックスの一部である事を示します。
		/// </summary>
		public const WS COMBOBOX=(WS)0x8000;
		/// <summary>
		/// リスト ボックス内の文字列をアルファベット順に並べ替えます。
		/// また、ユーザーが文字列をクリックまたはダブルクリックするたびに、親ウィンドウが入力メッセージを受け取ります。
		/// リスト ボックスの四辺に境界が表示されます。
		/// </summary>
		public const WS STANDARD=(WS)0x00a00003;
	}
	/// <summary>
	/// スクロールバーのスタイルに関する指定に使用します。
	/// </summary>
	public struct SBS{
		/// <summary>
		/// 水平スクロール バーを指定します。
		/// SBS_BOTTOMALIGN と SBS_TOPALIGN スタイルがどちらも指定されていないときは、スクロール バーは Create メンバ関数で指定された幅、高さ、位置になります。
		/// </summary>
		public const WS HORZ=WS.OVERLAPPED;	// 0x00
		/// <summary>
		/// 垂直スクロール バーを指定します。
		/// SBS_RIGHTALIGN と SBS_LEFTALIGN スタイルがどちらも指定されていないときは、スクロール バーは Create メンバ関数で指定された幅、高さ、位置になります。
		/// </summary>
		public const WS VERT=(WS)0x0001;
		/// <summary>
		/// SBS_HORZ スタイルと一緒に使用します。
		/// スクロール バーの上端を Create メンバ関数で指定された四角形の上端に揃えます。
		/// スクロール バーの高さは、システム スクロール バーの既定の高さと同じになります。
		/// </summary>
		public const WS TOPALIGN=(WS)0x0002;
		/// <summary>
		/// SBS_VERT スタイルと一緒に使用します。
		/// スクロール バーの左端を Create メンバ関数で指定されている四角形の左端に揃えます。
		/// スクロール バーの幅は、システム スクロール バーの既定の幅と同じになります。
		/// </summary>
		public const WS LEFTALIGN=(WS)0x0002;
		/// <summary>
		/// SBS_HORZ スタイルと一緒に使用します。
		/// スクロール バーの下端を Create メンバ関数で指定されている四角形の下端に揃えます。
		/// スクロール バーの高さは、システム スクロール バーの既定の高さと同じになります。
		/// </summary>
		public const WS BOTTOMALIGN=(WS)0x0004;
		/// <summary>
		/// SBS_VERT スタイルと一緒に使用します。
		/// スクロール バーの右端を Create メンバ関数で指定されている四角形の右端に揃えます。
		/// スクロール バーの幅は、システム スクロール バーの既定の幅と同じになります。
		/// </summary>
		public const WS RIGHTALIGN=(WS)0x0004;
		/// <summary>
		/// SBS_SIZEBOX スタイルと一緒に使用します。
		/// サイズ ボックスの左上隅を Create メンバ関数で指定されている四角形の左上隅に揃えます。
		/// サイズ ボックスのサイズは、システム サイズ ボックスの既定のサイズと同じになります。
		/// </summary>
		public const WS SIZEBOXTOPLEFTALIGN=(WS)0x0002;
		/// <summary>
		/// SBS_SIZEBOX スタイルと一緒に使用します。
		/// サイズ ボックスの右下隅を Create メンバ関数で指定されている四角形の右下隅に揃えます。
		/// サイズ ボックスのサイズは、システム サイズ ボックスの既定のサイズと同じになります。
		/// </summary>
		public const WS SIZEBOXBOTTOMRIGHTALIGN=(WS)0x0004;
		/// <summary>
		/// サイズ ボックスを指定します。
		/// SBS_SIZEBOXBOTTOMRIGHTALIGN と SBS_SIZEBOXTOPLEFTALIGN スタイルがどちらも指定されていないときは、サイズ ボックスは Create メンバ関数で指定された高さ、幅、位置になります。
		/// </summary>
		public const WS SIZEBOX=(WS)0x0008;
		/// <summary>
		/// SBS_SIZEBOX と同じですが、境界線が浮き出して表示されます。
		/// </summary>
		public const WS SIZEGRIP=(WS)0x0010;
	}
	/// <summary>
	/// スタティックテキストのスタイルを指定するのに使用します。
	/// </summary>
	public static class SS{
		/// <summary>
		/// 単純な四角形を指定し、指定されたテキストをその中に左揃えで表示します。
		/// テキストは書式設定されてから表示されます。
		/// テキストの長さが 1 行の長さよりも長い場合は、自動的に次の行に折り返され、新しい行も左揃えで表示されます。
		/// </summary>
		public static readonly WS LEFT=WS.OVERLAPPED; // 0x00
		/// <summary>
		/// 単純な四角形を指定し、指定されたテキストをその中に中央揃えで表示します。
		/// テキストは書式設定されてから表示されます。
		/// テキストの長さが 1 行文よりも長い場合は、自動的に次の行に折り返され、新しい行も中央揃えで表示されます。
		/// </summary>
		public const WS CENTER=(WS)0x00000001;
		/// <summary>
		/// 単純な四角形を指定し、指定されたテキストをその中に右揃えで表示します。
		/// テキストは書式設定されてから表示されます。
		/// テキストの長さが 1 行の長さよりも長い場合は、自動的に次の行に折り返され、新しい行も右揃えで表示されます。
		/// </summary>
		public const WS RIGHT=(WS)0x00000002;
		/// <summary>
		/// ダイアログ ボックスに表示するアイコンを指定します。
		/// 指定されたテキストは、アイコン ファイルの名前ではなく、リソース ファイルで定義されているアイコン名です。
		/// パラメータ nWidth と nHeight は無視されます。
		/// アイコンのサイズは自動的に調整されます。
		/// </summary>
		public const WS ICON=(WS)0x00000003;
		/// <summary>
		/// ウィンドウの枠の色で塗りつぶされた四角形を指定します。
		/// 既定の色は黒です。
		/// </summary>
		public const WS BLACKRECT=(WS)0x00000004;
		/// <summary>
		/// 画面の背景色で塗りつぶされた四角形を指定します。
		/// 既定の色は灰色です。
		/// </summary>
		public const WS GRAYRECT=(WS)0x00000005;
		/// <summary>
		/// ウィンドウの背景色で塗りつぶされた四角形を指定します。
		/// 既定の色は白です。
		/// </summary>
		public const WS WHITERECT=(WS)0x00000006;
		/// <summary>
		/// ウィンドウの枠と同じ色の枠を持つボックスを指定します。
		/// 既定の色は黒です。
		/// </summary>
		public const WS BLACKFRAME=(WS)0x00000007;
		/// <summary>
		/// 画面の背景色 (デスクトップ) と同じ色の枠を持つボックスを指定します。
		/// 既定の色は灰色です。
		/// </summary>
		public const WS GRAYFRAME=(WS)0x00000008;
		/// <summary>
		/// ウィンドウの背景色と同じ色の枠を持つボックスを指定します。
		/// 既定の色は白です。
		/// </summary>
		public const WS WHITEFRAME=(WS)0x00000009;
		/// <summary>
		/// ユーザー定義の項目を指定します。
		/// </summary>
		public const WS USERITEM=(WS)0x0000000A;
		/// <summary>
		/// 単純な四角形を指定し、その中に単一行のテキストを左揃えで表示します。
		/// テキスト行を短くしたり変更したりすることはできません。
		/// コントロールの親ウィンドウまたはダイアログ ボックスでは、WM_CTLCOLOR メッセージを処理しないようにする必要があります。
		/// </summary>
		public const WS SIMPLE=(WS)0x0000000B;
		/// <summary>
		/// 単純な四角形を指定し、指定されたテキストをその中に左揃えで表示します。
		/// タブは展開されますが、テキストは折り返されません。
		/// 1 行の長さよりも長いテキストはクリップされます。
		/// </summary>
		public const WS LEFTNOWORDWRAP=(WS)0x0000000C;
		/// <summary>
		/// 静的コントロールのオーナーが、コントロールを描画することを指定します。
		/// コントロールを描画する必要があるときは、オーナー ウィンドウは WM_DRAWITEM メッセージを受け取ります。
		/// </summary>
		public const WS OWNERDRAW=(WS)0x0000000D;
		/// <summary>
		/// 静的コントロールにビットマップを表示することを指定します。
		/// ビットマップのファイル名ではなく、リソース ファイルで定義されているビットマップ名を指定します。
		/// このスタイルでは、パラメータ nWidth および nHeight は無視されます。
		/// ビットマップが収まるように、コントロールのサイズが自動的に調整されます。
		/// </summary>
		public const WS BITMAP=(WS)0x0000000E;
		/// <summary>
		/// 静的コントロールに拡張メタファイルを表示することを指定します。
		/// メタファイルの名前を指定します。
		/// 拡張メタファイルを表示する静的コントロールのサイズは固定です。
		/// メタファイルの表示サイズは、静的コントロールのクライアント領域に合わせて調整されます。
		/// </summary>
		public const WS ENHMETAFILE=(WS)0x0000000F;
		/// <summary>
		/// 静的コントロールの上端および下端を EDGE_ETCHED スタイルで描画します。
		/// </summary>
		public const WS ETCHEDHORZ=(WS)0x00000010;
		/// <summary>
		/// 静的コントロールの右端および左端を EDGE_ETCHED スタイルで描画します。
		/// </summary>
		public const WS ETCHEDVERT=(WS)0x00000011;
		/// <summary>
		/// 静的コントロールの枠を EDGE_ETCHED スタイルで描画します。
		/// </summary>
		public const WS ETCHEDFRAME=(WS)0x00000012;
		/// <summary>
		/// 静的コントロールの種別を指定する部分のビットマスクです。
		/// </summary>
		public const WS TYPEMASK=(WS)0x0000001F;
		/// <summary>
		/// コントロールのサイズに合わせてビットマップの大きさを調整します。
		/// </summary>
		public const WS REALSIZECONTROL=(WS)0x00000040;
		/// <summary>
		/// このスタイルを指定しないと、Windows では、コントロールのテキストに表示されるアンパサンド (&amp;) 文字がアクセラレータの先頭文字として解釈されます。
		/// この場合、アンパサンドは表示されず、文字列内でアンパサンドの次にある文字が下線付きで表示されます。
		/// この機能を必要としないテキストが静的コントロールに表示される場合は、SS_NOPREFIX を指定できます。
		/// 静的コントロールのこのスタイルは、定義されているすべての静的コントロールに適用できます。
		/// ビットごとの OR 演算子を使って、ほかのスタイルと SS_NOPREFIX を組み合わせて指定できます。
		/// このスタイルは、アンパサンドを含むファイル名やその他の文字列をダイアログ ボックスの静的コントロールに表示する必要がある場合によく使用されます。
		/// </summary>
		public const WS NOPREFIX=(WS)0x00000080;
		/// <summary>
		/// ユーザーがコントロールをクリックまたはダブルクリックしたときに、親ウィンドウに STN_CLICKED、STN_DBLCLK、STN_DISABLE、および STN_ENABLE 通知メッセージを送信します。
		/// </summary>
		public const WS NOTIFY=(WS)0x00000100;
		/// <summary>
		/// ビットマップまたはアイコンが静的コントロールのクライアント領域よりも小さい場合、クライアント領域の残りの部分をそのビットマップまたはアイコンの左上隅にあるピクセルの色で塗りつぶすことを指定します。
		/// 静的コントロールに 1 行のテキストが表示されている場合、そのテキストは、コントロールのクライアント領域で上下方向の中央に配置されます。
		/// </summary>
		public const WS CENTERIMAGE=(WS)0x00000200;
		/// <summary>
		/// SS_BITMAP または SS_ICON スタイルを持つ静的コントロールのサイズを変更した場合も、そのコントロールの右下隅の位置は固定されたままになるように指定します。
		/// 新しいビットマップまたはアイコンを表示する場合、コントロールの上辺と左辺だけが調整されます。
		/// </summary>
		public const WS RIGHTJUST=(WS)0x00000400;
		/// <summary>
		/// 静的なアイコンまたはビットマップ コントロール (SS_ICON または SS_BITMAP のスタイルを持つ静的コントロール) が読み込まれたり描画されたりするときに、そのサイズが変更されないようにします。
		/// アイコンまたはビットマップが描画先の領域よりも大きい場合、そのイメージはクリップされます。
		/// </summary>
		public const WS REALSIZEIMAGE=(WS)0x00000800;
		/// <summary>
		/// 静的コントロールの周囲に、半分くぼんだ境界線を描画します。
		/// </summary>
		public const WS SUNKEN=(WS)0x00001000;
		/// <summary>
		/// 静的コントロールの描画方法をエディットボックスのそれと同じ物にします。
		/// 特に、平均文字幅の計算法をエディットボックスと同じにし、また、領域をはみ出る行は描画しません。
		/// </summary>
		public const WS EDITCONTROL=(WS)0x00002000;
		/// <summary>
		/// または SS_PATHELLIPSIS   指定された文字列が指定された四角形に収まるように、文字列の一部を省略記号 (...) に置き換えます。
		/// </summary>
		public const WS ENDELLIPSIS=(WS)0x00004000;
		/// <summary>
		/// 文字列が長くて表示しきれない場合に、文字列の末端を "..." に置き換えます。
		/// 特定の文字にバックスラッシュ \ を指定する事によって、その文字を出来るだけ省略しない様に指定する事が出来ます。
		/// </summary>
		public const WS PATHELLIPSIS=(WS)0x00008000;
		/// <summary>
		/// 収まりきらないテキストを切り詰め、省略記号を追加します。
		/// </summary>
		public const WS WORDELLIPSIS=(WS)0x0000C000;
		/// <summary>
		/// 収まりきらないテキストの表示方法を指定する部分のビットマスクです。
		/// </summary>
		public const WS ELLIPSISMASK=(WS)0x0000C000;
	}
	/// <summary>
	/// ウィンドウの拡張スタイルを表します。
	/// </summary>
	[System.Flags]
	public enum WS_EX:uint{
		/// <summary>
		/// 二重の境界を持つウィンドウを指定します。
		/// パラメータ dwStyle に WS_CAPTION スタイル フラグを指定することにより、タイトル バーを追加することもできます。
		/// </summary>
		DLGMODALFRAME =0x00000001,
		/// <summary>
		/// このスタイルを指定されている子ウィンドウは、作成または破棄されるときに親ウィンドウに WM_PARENTNOTIFY メッセージを送りません。
		/// </summary>
		NOPARENTNOTIFY =0x00000004,
		/// <summary>
		/// このスタイルで作成されたウィンドウは、すべてのウィンドウの上に配置され、アクティブでなくなった場合でも、引き続きほかのウィンドウの上に表示されたままになります。
		/// アプリケーションは、SetWindowPos メンバ関数を使って、この属性を追加および削除できます。
		/// </summary>
		TOPMOST   =0x00000008,
		/// <summary>
		/// このスタイルで作成されたウィンドウでは、ファイルをドラッグ アンド ドロップできます。
		/// </summary>
		ACCEPTFILES  =0x00000010,
		/// <summary>
		/// このスタイルで作成されたウィンドウは透明になります。
		/// つまり、このウィンドウの下にあるウィンドウが見えなくなることはありません。
		/// このスタイルで作成されたウィンドウは、そのウィンドウの下にある兄弟ウィンドウがすべて更新された後にだけ、WM_PAINT メッセージを受け取ります。
		/// </summary>
		TRANSPARENT  =0x00000020,

		/// <summary>
		/// MDI 子ウィンドウを作成します。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		MDICHILD  =0x00000040,
		/// <summary>
		/// ツール ウィンドウを作成します。
		/// このウィンドウは、フローティング ツール バーとして使用します。
		/// ツール ウィンドウのタイトル バーは通常よりも短く、ウィンドウのタイトルはより小さいフォントで描画されます。
		/// タスク バーや、ユーザーが Alt キーを押しながら Tab キーを押して表示したウィンドウには、ツール ウィンドウは表示されません。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		TOOLWINDOW  =0x00000080,
		/// <summary>
		/// 縁の浮き出した境界線を持つウィンドウを指定します。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		WINDOWEDGE  =0x00000100,
		/// <summary>
		/// ウィンドウを 3 次元で表示することを指定します。
		/// つまり、ウィンドウには、くぼんだ境界線が付きます。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		CLIENTEDGE  =0x00000200,
		/// <summary>
		/// ウィンドウのタイトル バーに疑問符 (?) を追加します。
		/// ユーザーが疑問符 (?) をクリックすると、カーソルがポインタの付いた疑問符 (?) に変わります。
		/// 続いてユーザーが子ウィンドウをクリックすると、その子ウィンドウが WM_HELP メッセージを受け取ります。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		CONTEXTHELP  =0x00000400,

		/// <summary>
		/// ウィンドウに汎用右揃えプロパティを指定します。
		/// このスタイルは、ウィンドウ クラスに依存します。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		RIGHT   =0x00001000,
		/// <summary>
		/// ウィンドウに汎用左揃えプロパティを指定します。
		/// これは、既定の設定です。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		LEFT=0x00000000,
		/// <summary>
		/// 右から左への読み取り順序でウィンドウのテキストを表示します。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		RTLREADING  =0x00002000,
		/// <summary>
		/// 左から右への読み取り順序でウィンドウのテキストを表示します。
		/// これは、既定の設定です。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		LTRREADING=0x00000000,
		/// <summary>
		/// クライアント領域の左に垂直スクロール バーを配置します。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		LEFTSCROLLBAR =0x00004000,
		/// <summary>
		/// クライアント領域の右に垂直スクロール バー (存在する場合) を配置します。
		/// これは、既定の設定です。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		RIGHTSCROLLBAR=0x00000000,

		/// <summary>
		/// ユーザーが、Tab キーを使ってウィンドウ内の子ウィンドウ間を移動できるようにします。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		CONTROLPARENT =0x00010000,
		/// <summary>
		/// 3 次元の境界線スタイルを持つウィンドウを作成します。
		/// このウィンドウは、ユーザーの入力を受け付けない項目用に使用します。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		STATICEDGE  =0x00020000,
		/// <summary>
		/// 一番上にあるウィンドウを表示するときに、強制的にタスクバーに含みます。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		APPWINDOW  =0x00040000,

		/// <summary>
		/// WS_EX_CLIENTEDGE スタイルと WS_EX_WINDOWEDGE スタイルを組み合わせます。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		OVERLAPPEDWINDOW=WINDOWEDGE|CLIENTEDGE,
		/// <summary>
		/// WS_EX_WINDOWEDGE スタイルと WS_EX_TOPMOST スタイルを組み合わせます。
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		PALETTEWINDOW=WINDOWEDGE|TOOLWINDOW|TOPMOST,


		/// <summary>
		/// Windows 2000：レイヤーウィンドウ を作成します。
		/// </summary>
		/// <remarks>_WIN32_WINNT >= 0x0500</remarks>
		LAYERED   =0x00080000,

		/// <summary>
		/// 子ウィンドウへの反転レイアウトの継承を無効にします。
		/// </summary>
		/// <remarks>WINVER >= 0x0500</remarks>
		NOINHERITLAYOUT =0x00100000, // Disable inheritence of mirroring by children
		/// <summary>
		/// コントロール及び文字の配置を右から左に反転します。
		/// </summary>
		/// <remarks>WINVER >= 0x0500</remarks>
		LAYOUTRTL  =0x00400000, // Right to left mirroring

		/// <summary>
		/// ウィンドウの描画時にダブルバッファリングを使用します。
		/// </summary>
		/// <remarks>_WIN32_WINNT >= 0x0500</remarks>
		COMPOSITED  =0x02000000,
		/// <summary>
		/// Windows 2000：このスタイルで作成されたトップレベルウィンドウは、ユーザーがクリックしてもフォアグラウンドウィンドウになりません。ユーザーがフォアグラウンドウィンドウを最小化したり閉じたりしたときにも、システムがこのウィンドウをフォアグラウンドウィンドウにすることはありません。 
		/// このウィンドウをアクティブにするには、SetActiveWindow 関数または SetForegroundWindow 関数を使います。
		/// 既定では、このウィンドウはタスクバーには表示されません。ウィンドウがタスクバーに表示されるようにするには、WS_EX_APPWINDOW スタイルを指定します。
		/// </summary>
		/// <remarks>_WIN32_WINNT >= 0x0500</remarks>
		NOACTIVATE  =0x08000000
	}
}