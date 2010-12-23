using mwg.Win32;
using CM=System.ComponentModel;
using Interop=System.Runtime.InteropServices;

namespace mwg.Windows{
	/// <summary>
	/// Win32API の Window を表現するクラスです。
	/// </summary>
	[CM::TypeConverter(typeof(WindowConverter))]
	public class Window:System.Windows.Forms.IWin32Window{
		/// <summary>
		/// ウィンドウのハンドルを保持します。
		/// </summary>
		protected System.IntPtr handle;
		/// <summary>
		/// ウィンドウのハンドルを取得します。
		/// </summary>
		[CM::Browsable(false)]
		public System.IntPtr Handle{
			get{return this.handle;}
		}
		/// <summary>
		/// Window のコンストラクタです。
		/// </summary>
		/// <param name="hWnd">Window のウィンドウハンドルを指定します。</param>
		public Window(System.IntPtr hWnd){
			this.handle=hWnd;
			this.wi.cbSize=(uint)Interop.Marshal.SizeOf(typeof(WINDOWINFO));
		}
		//===========================================================
		//		Properties
		//===========================================================
		/// <summary>
		/// Window handle を取得します。
		/// </summary>
		[CM::Category("Native"),CM::Description("Window handle を取得します。"),CM::TypeConverter(typeof(HexConverter))]
		public System.IntPtr hWnd{get{return this.handle;}}
		/// <summary>
		/// Window のキャプション文字列を取得します。
		/// </summary>
		[CM::Category("Native"),CM::Description("Window のキャプション文字列を取得します。")]
		public string Caption{
			get{
				try{
					int max=User32.GetWindowTextLength(this.handle)*2;
					byte[] buff=new byte[max];
					GetWindowText(this.handle,buff,max);
					return System.Text.Encoding.Unicode.GetString(buff);
				}catch{return "<error>";}
			}
			set{User32.SetWindowText(this.handle,value);}
		}
		[Interop.DllImport("user32.dll",SetLastError=true,CharSet=Interop.CharSet.Unicode)]
		private static extern int GetWindowText(System.IntPtr hWnd,[Interop::Out]byte[] lpString,int nMaxCount);
		/// <summary>
		/// このウィンドウが所属しているウィンドウクラスを取得します。
		/// </summary>
		[CM::Category("Native"),CM::Description("所属している window class です。")]
		public string ClassName{
			get{
				try{
					int max=200;
					System.Text.StringBuilder buff=new System.Text.StringBuilder(max);
					User32.GetClassName(this.handle,buff,max);
					return buff.ToString();
				}catch{return "<error>";}
			}
		}
		/// <summary>
		/// 親ウィンドウのウィンドウハンドルを取得亦は設定します。
		/// </summary>
		public System.IntPtr Parent{
			get{
				Kernel32.@try();
				System.IntPtr r=User32.GetParent(this.handle);
				if(Kernel32.@catch())Kernel32.@throw();
				return r;
			}
		//	set{}
		}
		//public System.IntPtr ParentNotOwner{}
		/// <summary>
		/// ウィンドウが最小化されているか否かを取得します。
		/// </summary>
		public bool IsIconic{get{return User32.IsIconic(this.handle);}}
		/// <summary>
		/// 現在のインスタンスに対応するウィンドウが存在しているか否かを取得します。
		/// </summary>
		public bool IsExists{get{return User32.IsWindow(this.handle);}}
		/// <summary>
		/// ウィンドウが表示されているか否かを取得します。
		/// </summary>
		public bool IsVisible{get{return User32.IsWindowVisible(this.handle);}}
		/// <summary>
		/// 指定したウィンドウが、このウィンドウの子孫ウィンドウであるか否かを取得します。
		/// </summary>
		/// <param name="hWndChild">子孫ウィンドウであるかどうかを確かめるウィンドウをハンドルで指定します。</param>
		/// <returns>指定したウィンドウを子孫ウィンドウに持つ場合に true を返します。</returns>
		public bool IsChild(System.IntPtr hWndChild){return User32.IsChild(this.handle,hWndChild);}
		//===========================================================
		//		WINDOWINFO
		//===========================================================
		private WINDOWINFO wi=new mwg.Win32.WINDOWINFO();
		/// <summary>
		/// Window の情報を取得します。
		/// </summary>
		[CM::Browsable(false)]
		public WINDOWINFO WindowInfo{
			get{
				User32.GetWindowInfo(this.handle,ref wi);
				return wi;
			}
		}
		/// <summary>
		/// Window の入る矩形領域を取得します。
		/// </summary>
		[CM::Category("Native"),CM::Description("Window の領域を表します。")]
		public RECT Rect{get{return this.WindowInfo.rcWindow;}}
		/// <summary>
		/// Window のクライアント領域を取得します。
		/// </summary>
		[CM::Category("Native"),CM::Description("クライアント領域を表します。")]
		public RECT ClientRect{get{return this.WindowInfo.rcWindow;}}
		[CM::Category("Native"),CM::Description("Active 状態を表します。")]public bool Status{get{return this.wi.dwWindowStatus==WStatus.ACTIVECAPTION;}}
		/// <summary>
		/// 境界線の幅を取得します。
		/// </summary>
		[CM::Category("Native")]public uint BorderX{get{return this.WindowInfo.cxWindowBorders;}}
		/// <summary>
		/// 境界線の高さを取得します。
		/// </summary>
		[CM::Category("Native")]public uint BorderY{get{return this.WindowInfo.cyWindowBorders;}}
		/// <summary>
		/// この Window の Class の Atom を取得します。
		/// </summary>
		[CM::Category("Native"),CM::TypeConverter(typeof(HexConverter))]
		public ushort AtomWindowType{get{return this.WindowInfo.atomWindowType;}}
		/// <summary>
		/// このウィンドウを作成したプログラムのバージョンを取得します。
		/// </summary>
		[CM::Category("Native"),CM::TypeConverter(typeof(HexConverter))]
		public ushort CreatorVersion{get{return this.WindowInfo.wCreatorVersion;}}
		/// <summary>
		/// このウィンドウを保持しているプロセスの ID を取得します。
		/// </summary>
		[CM::Category("Native"),CM::TypeConverter(typeof(HexConverter))]
		public int ProcessID{
			get{
				uint id;
				User32.GetWindowThreadProcessId(this.handle,out id);
				return (int)id;
			}
		}
		/// <summary>
		/// このウィンドウのメッセージループを動かしているスレッドの ID を取得します。
		/// </summary>
		[CM::Category("Native"),CM::TypeConverter(typeof(HexConverter))]
		public int ThreadID{get{return (int)User32.GetWindowThreadProcessId(this.handle);}}
		/// <summary>
		/// Window の拡張スタイルを取得亦は設定します。
		/// </summary>
		[CM::Category("Window 拡張スタイル"),CM::ReadOnly(true)]
		public WS_EX ExStyle{
			get{
				Kernel32.@try();
				WS_EX r=(WS_EX)(int)User32.GetWindowLongPtr(this.handle,User32.GWLP.EXSTYLE);
				if(Kernel32.@catch())Kernel32.@throw();
				return r;
			}
			set{
				Kernel32.@try();
				User32.SetWindowLongPtr(this.handle,User32.GWLP.EXSTYLE,(System.IntPtr)value);
				if(Kernel32.@catch())Kernel32.@throw();
			}
		}

		/// <summary>
		/// ウィンドウのスタイルを取得亦は設定します。
		/// </summary>
		[CM::Category("Window スタイル"),CM::ReadOnly(true)]
		public WS Style{
			get{
				Kernel32.@try();
				WS r=(WS)(int)User32.GetWindowLongPtr(this.handle,User32.GWLP.STYLE);
				if(Kernel32.@catch())Kernel32.@throw();
				return r;
			}
			set{
				Kernel32.@try();
				User32.SetWindowLongPtr(this.handle,User32.GWLP.STYLE,(System.IntPtr)value);
				if(Kernel32.@catch())Kernel32.@throw();
			}
		}
#if old

		#region WS
		/**<summary>WS_CHILD スタイルが設定されているか取得します。</summary>*/
		[CM.Category("Window スタイル")]public bool WS_CAPTION{get{return 0<(uint)(Style&WS.CAPTION);}}
		/**<summary>WS_OVERLAPPEDWINDOW スタイルが設定されているか取得します。</summary>*/
		[CM.Category("Window スタイル")]public bool WS_OVERLAPPEDWINDOW{get{return 0<(uint)(Style&WS.OVERLAPPEDWINDOW);}}
		/**<summary>WS_POPUPWINDOW スタイルが設定されているか取得します。</summary>*/
		[CM.Category("Window スタイル")]public bool WS_POPUPWINDOW{get{return 0<(uint)(Style&WS.POPUPWINDOW);}}
		/**<summary>WS_CHILDWINDOW スタイルが設定されているか取得します。</summary>*/
		[CM.Category("Window スタイル")]public bool WS_CHILDWINDOW{get{return 0<(uint)(Style&WS.CHILDWINDOW);}}
		/*
		<pre id="WS">
		/**&lt;summary&gt;WS_XXX スタイルの設定を取得・設定します。&lt;/summary&gt;&#042;/<br/>
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_XXX{
		get{return 0<(uint)(Style&WS.XXX);}
		set{if(value)Style|=WS.XXX;else Style&=~WS.XXX;}
		}
		</pre>
		<script>
		var a=("CHILD,MINIMIZE,VISIBLE,DISABLED,CLIPSIBLINGS,MAXIMIZE,BORDER,DLGFRAME,VSCROLL,HSCROLL,"
			+"SYSMENU,THICKFRAME,GROUP,TABSTOP,MINIMIZEBOX,MAXIMIZEBOX,TILED,ICONIC,SIZEBOX,TILEDWINDOW").split(",");
		var str=WS.innerHTML.replace(/\r\n/g,"")+"</br>";
		for(var i=0;i<a.length;i++)document.write(str.replace(/XXX/g,a[i]));
		</script>
		*/
		/**<summary>WS_CHILD スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_CHILD{get{return 0<(uint)(Style&WS.CHILD);}set{if(value)Style|=WS.CHILD;else Style&=~WS.CHILD;}}
		/**<summary>WS_MINIMIZE スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_MINIMIZE{get{return 0<(uint)(Style&WS.MINIMIZE);}set{if(value)Style|=WS.MINIMIZE;else Style&=~WS.MINIMIZE;}}
		/**<summary>WS_VISIBLE スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_VISIBLE{get{return 0<(uint)(Style&WS.VISIBLE);}set{if(value)Style|=WS.VISIBLE;else Style&=~WS.VISIBLE;}}
		/**<summary>WS_DISABLED スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_DISABLED{get{return 0<(uint)(Style&WS.DISABLED);}set{if(value)Style|=WS.DISABLED;else Style&=~WS.DISABLED;}}
		/**<summary>WS_CLIPSIBLINGS スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_CLIPSIBLINGS{get{return 0<(uint)(Style&WS.CLIPSIBLINGS);}set{if(value)Style|=WS.CLIPSIBLINGS;else Style&=~WS.CLIPSIBLINGS;}}
		/**<summary>WS_MAXIMIZE スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_MAXIMIZE{get{return 0<(uint)(Style&WS.MAXIMIZE);}set{if(value)Style|=WS.MAXIMIZE;else Style&=~WS.MAXIMIZE;}}
		/**<summary>WS_BORDER スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_BORDER{get{return 0<(uint)(Style&WS.BORDER);}set{if(value)Style|=WS.BORDER;else Style&=~WS.BORDER;}}
		/**<summary>WS_DLGFRAME スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_DLGFRAME{get{return 0<(uint)(Style&WS.DLGFRAME);}set{if(value)Style|=WS.DLGFRAME;else Style&=~WS.DLGFRAME;}}
		/**<summary>WS_VSCROLL スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_VSCROLL{get{return 0<(uint)(Style&WS.VSCROLL);}set{if(value)Style|=WS.VSCROLL;else Style&=~WS.VSCROLL;}}
		/**<summary>WS_HSCROLL スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_HSCROLL{get{return 0<(uint)(Style&WS.HSCROLL);}set{if(value)Style|=WS.HSCROLL;else Style&=~WS.HSCROLL;}}
		/**<summary>WS_SYSMENU スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_SYSMENU{get{return 0<(uint)(Style&WS.SYSMENU);}set{if(value)Style|=WS.SYSMENU;else Style&=~WS.SYSMENU;}}
		/**<summary>WS_THICKFRAME スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_THICKFRAME{get{return 0<(uint)(Style&WS.THICKFRAME);}set{if(value)Style|=WS.THICKFRAME;else Style&=~WS.THICKFRAME;}}
		/**<summary>WS_GROUP スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_GROUP{get{return 0<(uint)(Style&WS.GROUP);}set{if(value)Style|=WS.GROUP;else Style&=~WS.GROUP;}}
		/**<summary>WS_TABSTOP スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_TABSTOP{get{return 0<(uint)(Style&WS.TABSTOP);}set{if(value)Style|=WS.TABSTOP;else Style&=~WS.TABSTOP;}}
		/**<summary>WS_MINIMIZEBOX スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_MINIMIZEBOX{get{return 0<(uint)(Style&WS.MINIMIZEBOX);}set{if(value)Style|=WS.MINIMIZEBOX;else Style&=~WS.MINIMIZEBOX;}}
		/**<summary>WS_MAXIMIZEBOX スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_MAXIMIZEBOX{get{return 0<(uint)(Style&WS.MAXIMIZEBOX);}set{if(value)Style|=WS.MAXIMIZEBOX;else Style&=~WS.MAXIMIZEBOX;}}
		/**<summary>WS_TILED スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_TILED{get{return 0<(uint)(Style&WS.TILED);}set{if(value)Style|=WS.TILED;else Style&=~WS.TILED;}}
		/**<summary>WS_ICONIC スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_ICONIC{get{return 0<(uint)(Style&WS.ICONIC);}set{if(value)Style|=WS.ICONIC;else Style&=~WS.ICONIC;}}
		/**<summary>WS_SIZEBOX スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_SIZEBOX{get{return 0<(uint)(Style&WS.SIZEBOX);}set{if(value)Style|=WS.SIZEBOX;else Style&=~WS.SIZEBOX;}}
		/**<summary>WS_TILEDWINDOW スタイルの設定を取得・設定します。</summary>*/
		[CM.Category("Window スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_TILEDWINDOW{get{return 0<(uint)(Style&WS.TILEDWINDOW);}set{if(value)Style|=WS.TILEDWINDOW;else Style&=~WS.TILEDWINDOW;}}
		#endregion

		#region WS_EX
		/*
		<pre id="WS_EX1">
		/**&lt;summary&gt;WS_EX_XXX スタイルを取得・設定します。&lt;/summary&gt;&#42;/<br/>
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_XXX{
		get{return 0<(uint)(ExStyle&WS_EX.XXX);}
		set{if(value)ExStyle|=WS_EX.XXX;else ExStyle&=~WS_EX.XXX;}
		}
		</pre>
		<pre id="WS_EX2">
		/**&lt;summary&gt;WS_EX_XXX スタイルを取得します。&lt;/summary&gt;&#42;/<br/>
		[CM.Category("Window 拡張スタイル")]public bool WS_EX_XXX{
		get{return 0<(uint)(ExStyle&WS_EX.XXX);}
		}
		</pre>
		<script>
		var a=("DLGMODALFRAME,NOPARENTNOTIFY,TOPMOST,ACCEPTFILES,TRANSPARENT,MDICHILD,TOOLWINDOW,WINDOWEDGE,CLIENTEDGE,CONTEXTHELP,"
			+"RIGHT,RTLREADING,LEFTSCROLLBAR,CONTROLPARENT,STATICEDGE,APPWINDOW,LAYERED,NOINHERITLAYOUT,LAYOUTRTL,COMPOSITED,"
			+"NOACTIVATE").split(",");
		var str=WS_EX1.innerHTML.replace(/\r\n/g,"")+"</br>";
		for(var i=0;i<a.length;i++)document.write(str.replace(/XXX/g,a[i]));
		a="OVERLAPPEDWINDOW,PALETTEWINDOW,LEFT,LTRREADING,RIGHTSCROLLBAR".split(",");
		str=WS_EX2.innerHTML.replace(/\r\n/g,"")+"</br>";
		for(var i=0;i<a.length;i++)document.write(str.replace(/XXX/g,a[i]));
		</script>
		*/
		/**<summary>WS_EX_DLGMODALFRAME スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_DLGMODALFRAME{get{return 0<(uint)(ExStyle&WS_EX.DLGMODALFRAME);}set{if(value)ExStyle|=WS_EX.DLGMODALFRAME;else ExStyle&=~WS_EX.DLGMODALFRAME;}}
		/**<summary>WS_EX_NOPARENTNOTIFY スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_NOPARENTNOTIFY{get{return 0<(uint)(ExStyle&WS_EX.NOPARENTNOTIFY);}set{if(value)ExStyle|=WS_EX.NOPARENTNOTIFY;else ExStyle&=~WS_EX.NOPARENTNOTIFY;}}
		/**<summary>WS_EX_TOPMOST スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_TOPMOST{get{return 0<(uint)(ExStyle&WS_EX.TOPMOST);}set{if(value)ExStyle|=WS_EX.TOPMOST;else ExStyle&=~WS_EX.TOPMOST;}}
		/**<summary>WS_EX_ACCEPTFILES スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_ACCEPTFILES{get{return 0<(uint)(ExStyle&WS_EX.ACCEPTFILES);}set{if(value)ExStyle|=WS_EX.ACCEPTFILES;else ExStyle&=~WS_EX.ACCEPTFILES;}}
		/**<summary>WS_EX_TRANSPARENT スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_TRANSPARENT{get{return 0<(uint)(ExStyle&WS_EX.TRANSPARENT);}set{if(value)ExStyle|=WS_EX.TRANSPARENT;else ExStyle&=~WS_EX.TRANSPARENT;}}
		/**<summary>WS_EX_MDICHILD スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_MDICHILD{get{return 0<(uint)(ExStyle&WS_EX.MDICHILD);}set{if(value)ExStyle|=WS_EX.MDICHILD;else ExStyle&=~WS_EX.MDICHILD;}}
		/**<summary>WS_EX_TOOLWINDOW スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_TOOLWINDOW{get{return 0<(uint)(ExStyle&WS_EX.TOOLWINDOW);}set{if(value)ExStyle|=WS_EX.TOOLWINDOW;else ExStyle&=~WS_EX.TOOLWINDOW;}}
		/**<summary>WS_EX_WINDOWEDGE スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_WINDOWEDGE{get{return 0<(uint)(ExStyle&WS_EX.WINDOWEDGE);}set{if(value)ExStyle|=WS_EX.WINDOWEDGE;else ExStyle&=~WS_EX.WINDOWEDGE;}}
		/**<summary>WS_EX_CLIENTEDGE スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_CLIENTEDGE{get{return 0<(uint)(ExStyle&WS_EX.CLIENTEDGE);}set{if(value)ExStyle|=WS_EX.CLIENTEDGE;else ExStyle&=~WS_EX.CLIENTEDGE;}}
		/**<summary>WS_EX_CONTEXTHELP スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_CONTEXTHELP{get{return 0<(uint)(ExStyle&WS_EX.CONTEXTHELP);}set{if(value)ExStyle|=WS_EX.CONTEXTHELP;else ExStyle&=~WS_EX.CONTEXTHELP;}}
		/**<summary>WS_EX_RIGHT スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_RIGHT{get{return 0<(uint)(ExStyle&WS_EX.RIGHT);}set{if(value)ExStyle|=WS_EX.RIGHT;else ExStyle&=~WS_EX.RIGHT;}}
		/**<summary>WS_EX_RTLREADING スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_RTLREADING{get{return 0<(uint)(ExStyle&WS_EX.RTLREADING);}set{if(value)ExStyle|=WS_EX.RTLREADING;else ExStyle&=~WS_EX.RTLREADING;}}
		/**<summary>WS_EX_LEFTSCROLLBAR スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_LEFTSCROLLBAR{get{return 0<(uint)(ExStyle&WS_EX.LEFTSCROLLBAR);}set{if(value)ExStyle|=WS_EX.LEFTSCROLLBAR;else ExStyle&=~WS_EX.LEFTSCROLLBAR;}}
		/**<summary>WS_EX_CONTROLPARENT スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_CONTROLPARENT{get{return 0<(uint)(ExStyle&WS_EX.CONTROLPARENT);}set{if(value)ExStyle|=WS_EX.CONTROLPARENT;else ExStyle&=~WS_EX.CONTROLPARENT;}}
		/**<summary>WS_EX_STATICEDGE スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_STATICEDGE{get{return 0<(uint)(ExStyle&WS_EX.STATICEDGE);}set{if(value)ExStyle|=WS_EX.STATICEDGE;else ExStyle&=~WS_EX.STATICEDGE;}}
		/**<summary>WS_EX_APPWINDOW スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_APPWINDOW{get{return 0<(uint)(ExStyle&WS_EX.APPWINDOW);}set{if(value)ExStyle|=WS_EX.APPWINDOW;else ExStyle&=~WS_EX.APPWINDOW;}}
		/**<summary>WS_EX_LAYERED スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_LAYERED{get{return 0<(uint)(ExStyle&WS_EX.LAYERED);}set{if(value)ExStyle|=WS_EX.LAYERED;else ExStyle&=~WS_EX.LAYERED;}}
		/**<summary>WS_EX_NOINHERITLAYOUT スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_NOINHERITLAYOUT{get{return 0<(uint)(ExStyle&WS_EX.NOINHERITLAYOUT);}set{if(value)ExStyle|=WS_EX.NOINHERITLAYOUT;else ExStyle&=~WS_EX.NOINHERITLAYOUT;}}
		/**<summary>WS_EX_LAYOUTRTL スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_LAYOUTRTL{get{return 0<(uint)(ExStyle&WS_EX.LAYOUTRTL);}set{if(value)ExStyle|=WS_EX.LAYOUTRTL;else ExStyle&=~WS_EX.LAYOUTRTL;}}
		/**<summary>WS_EX_COMPOSITED スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_COMPOSITED{get{return 0<(uint)(ExStyle&WS_EX.COMPOSITED);}set{if(value)ExStyle|=WS_EX.COMPOSITED;else ExStyle&=~WS_EX.COMPOSITED;}}
		/**<summary>WS_EX_NOACTIVATE スタイルを取得・設定します。</summary>*/
		[CM.Category("Window 拡張スタイル"),CM.RefreshProperties(CM.RefreshProperties.All)]public bool WS_EX_NOACTIVATE{get{return 0<(uint)(ExStyle&WS_EX.NOACTIVATE);}set{if(value)ExStyle|=WS_EX.NOACTIVATE;else ExStyle&=~WS_EX.NOACTIVATE;}}
		/**<summary>WS_EX_OVERLAPPEDWINDOW スタイルを取得します。</summary>*/
		[CM.Category("Window 拡張スタイル")]public bool WS_EX_OVERLAPPEDWINDOW{get{return 0<(uint)(ExStyle&WS_EX.OVERLAPPEDWINDOW);}}
		/**<summary>WS_EX_PALETTEWINDOW スタイルを取得します。</summary>*/
		[CM.Category("Window 拡張スタイル")]public bool WS_EX_PALETTEWINDOW{get{return 0<(uint)(ExStyle&WS_EX.PALETTEWINDOW);}}
		/**<summary>WS_EX_LEFT スタイルを取得します。</summary>*/
		[CM.Category("Window 拡張スタイル")]public bool WS_EX_LEFT{get{return 0<(uint)(ExStyle&WS_EX.LEFT);}}
		/**<summary>WS_EX_LTRREADING スタイルを取得します。</summary>*/
		[CM.Category("Window 拡張スタイル")]public bool WS_EX_LTRREADING{get{return 0<(uint)(ExStyle&WS_EX.LTRREADING);}}
		/**<summary>WS_EX_RIGHTSCROLLBAR スタイルを取得します。</summary>*/
		[CM.Category("Window 拡張スタイル")]public bool WS_EX_RIGHTSCROLLBAR{get{return 0<(uint)(ExStyle&WS_EX.RIGHTSCROLLBAR);}}
		#endregion
#endif
	}
	/// <summary>
	/// 16 進数表現した文字列への変換を提供します。
	/// </summary>
	internal class HexConverter:CM::TypeConverter{
		public override bool CanConvertTo(CM::ITypeDescriptorContext context,System.Type destinationType){
			return destinationType==typeof(string)||base.CanConvertTo(context,destinationType);
		}
		public override object ConvertTo(CM::ITypeDescriptorContext context,System.Globalization.CultureInfo culture,object value,System.Type destinationType){
			if(destinationType!=typeof(string))return base.ConvertTo (context, culture, value, destinationType);
			System.Type t=value.GetType();
			if(t==typeof(int))return "0x"+((int)value).ToString("X8");
			if(t==typeof(uint))return "0x"+((uint)value).ToString("X8");
			if(t==typeof(long))return "0x"+((int)value).ToString("X16");
			if(t==typeof(ulong))return "0x"+((uint)value).ToString("X16");
			if(t==typeof(System.IntPtr)){
				if(System.IntPtr.Size==8){
					return "0x"+((long)(System.IntPtr)value).ToString("X16");
				}else{
					return "0x"+((int)(System.IntPtr)value).ToString("X8");
				}
			}
			if(t==typeof(sbyte))return "0x"+((short)value).ToString("X2");
			if(t==typeof(byte))return "0x"+((ushort)value).ToString("X2");
			if(t==typeof(short))return "0x"+((short)value).ToString("X4");
			if(t==typeof(ushort))return "0x"+((ushort)value).ToString("X4");
			if(t==typeof(char))return "0x"+((short)(char)value).ToString("X4");
			return value.ToString();
		}
		public override bool CanConvertFrom(CM::ITypeDescriptorContext context, System.Type sourceType){
			return sourceType==typeof(string)||base.CanConvertFrom(context, sourceType);
		}
		public override object ConvertFrom(CM::ITypeDescriptorContext context, System.Globalization.CultureInfo culture,object value){
			string str=value as string;
			if(str==null)return base.ConvertFrom(context,culture,value);
			if(str.StartsWith("0x"))str=str.Substring(2);
			try{
				return long.Parse(str,System.Globalization.NumberStyles.HexNumber);
			}catch(System.Exception e){
				throw new System.NotSupportedException("指定した文字列を適当な値に変換する事が出来ませんでした。",e);
			}
		}
	}
}