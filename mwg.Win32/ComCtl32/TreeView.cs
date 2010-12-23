using Frms=System.Windows.Forms;
using Gdi=System.Drawing;
using Gen=System.Collections.Generic;
using CM=System.ComponentModel;
using Interop=System.Runtime.InteropServices;
using Ref=System.Reflection;

namespace mwg.Win32{
	/// <summary>
	/// Tree View コントロールの Window Style を指定するのに使用します。
	/// </summary>
	public static class TVS{
		public const WS HASBUTTONS=(WS)0x0001;
		public const WS HASLINES=(WS)0x0002;
		public const WS LINESATROOT=(WS)0x0004;
		public const WS EDITLABELS=(WS)0x0008;
		public const WS DISABLEDRAGDROP=(WS)0x0010;
		public const WS SHOWSELALWAYS=(WS)0x0020;

		// 以降 _WIN32_IE >= 0x0300
		public const WS RTLREADING=(WS)0x0040;

		public const WS NOTOOLTIPS=(WS)0x0080;
		public const WS CHECKBOXES=(WS)0x0100;
		public const WS TRACKSELECT=(WS)0x0200;

		// 以降 _WIN32_IE >= 0x0400
		public const WS SINGLEEXPAND=(WS)0x0400;
		public const WS INFOTIP=(WS)0x0800;
		public const WS FULLROWSELECT=(WS)0x1000;
		public const WS NOSCROLL=(WS)0x2000;
		public const WS NONEVENHEIGHT=(WS)0x4000;

		// 以降 _WIN32_IE >= 0x500
		public const WS NOHSCROLL=(WS)0x8000;  // TVS_NOSCROLL overrides this
	}

	/// <summary>
	/// Tree View コントロールで使用される Window Message を定義します。
	/// </summary>
	public static class TVM{
		private const WM FIRST=(WM)0x1100;
		private static readonly bool AUTO_IS_W=Interop::Marshal.SystemDefaultCharSize==2;

		public static readonly WM INSERTITEM=AUTO_IS_W?INSERTITEMA:INSERTITEMW;
		public const WM INSERTITEMA=(WM)(FIRST+0);
		public const WM INSERTITEMW=(WM)(FIRST+50);
		public const WM DELETEITEM=(WM)(FIRST+1);
		public const WM EXPAND=(WM)(FIRST+2);
		public const WM GETITEMRECT=(WM)(FIRST+4);
		public const WM GETCOUNT=(WM)(FIRST+5);
		public const WM GETINDENT=(WM)(FIRST+6);
		public const WM SETINDENT=(WM)(FIRST+7);
		public const WM GETIMAGELIST=(WM)(FIRST+8);
		public const WM SETIMAGELIST=(WM)(FIRST+9);
		public const WM GETNEXTITEM=(WM)(FIRST+10);
		public const WM SELECTITEM=(WM)(FIRST+11);
		public const WM GETITEMA=(WM)(FIRST+12);
		public const WM GETITEMW=(WM)(FIRST+62);
		public const WM SETITEMA=(WM)(FIRST+13);
		public const WM SETITEMW=(WM)(FIRST+63);
		public const WM EDITLABELA=(WM)(FIRST+14);
		public const WM EDITLABELW=(WM)(FIRST+65);
		public const WM GETEDITCONTROL=(WM)(FIRST+15);
		public const WM GETVISIBLECOUNT=(WM)(FIRST+16);
		public const WM HITTEST=(WM)(FIRST+17);
		public const WM CREATEDRAGIMAGE=(WM)(FIRST+18);
		public const WM SORTCHILDREN=(WM)(FIRST+19);
		public const WM ENSUREVISIBLE=(WM)(FIRST+20);
		public const WM SORTCHILDRENCB=(WM)(FIRST+21);
		public const WM ENDEDITLABELNOW=(WM)(FIRST+22);
		public const WM GETISEARCHSTRINGA=(WM)(FIRST+23);
		public const WM GETISEARCHSTRINGW=(WM)(FIRST+64);

		// 以降 _WIN32_IE >= 0x0300

		public const WM SETTOOLTIPS=(WM)(FIRST+24);
		public const WM GETTOOLTIPS=(WM)(FIRST+25);
		public const WM SETINSERTMARK=(WM)(FIRST+26);
		public const WM SETITEMHEIGHT=(WM)(FIRST+27);
		public const WM GETITEMHEIGHT=(WM)(FIRST+28);
		public const WM SETBKCOLOR=(WM)(FIRST+29);
		public const WM SETTEXTCOLOR=(WM)(FIRST+30);
		public const WM GETBKCOLOR=(WM)(FIRST+31);
		public const WM GETTEXTCOLOR=(WM)(FIRST+32);
		public const WM SETSCROLLTIME=(WM)(FIRST+33);
		public const WM GETSCROLLTIME=(WM)(FIRST+34);
		public const WM SETINSERTMARKCOLOR=(WM)(FIRST+37);
		public const WM GETINSERTMARKCOLOR=(WM)(FIRST+38);

		// 以降 _WIN32_IE >= 0x0500

		public const WM GETITEMSTATE=(WM)(FIRST+39);
		public const WM SETLINECOLOR=(WM)(FIRST+40);
		public const WM GETLINECOLOR=(WM)(FIRST+41);
		public const WM MAPACCIDTOHTREEITEM=(WM)(FIRST+42);
		public const WM MAPHTREEITEMTOACCID=(WM)(FIRST+43);
	}

	public static unsafe class TreeView{
		public struct _TREEITEM{}
		[Interop::StructLayout(Interop::LayoutKind.Sequential)]
		public struct HTREEITEM{
			System.IntPtr item;
			internal HTREEITEM(System.IntPtr ptr){
				this.item=ptr;
			}
			public static explicit operator HTREEITEM(System.IntPtr ptr){
				return new HTREEITEM(ptr);
			}
			public static implicit operator _TREEITEM*(HTREEITEM hTreeItem){
				return (_TREEITEM*)hTreeItem.item;
			}
			public static implicit operator System.IntPtr(HTREEITEM hTreeItem){
				return hTreeItem.item;
			}
		}

		[Interop::StructLayout(Interop::LayoutKind.Sequential,CharSet=Interop::CharSet.Auto)]
		public struct ITEM{
			uint mask;
			HTREEITEM hItem;
			uint state;
			uint stateMask;
			[Interop::MarshalAs(Interop::UnmanagedType.LPTStr)]
			string pszText;
			int cchTextMax;
			int iImage;
			int iSelectedImage;
			int cChildren;
			System.IntPtr lParam;
		}
		[Interop::StructLayout(Interop::LayoutKind.Sequential,CharSet=Interop::CharSet.Auto)]
		public struct ITEMEX{
			uint mask;
			HTREEITEM hItem;
			uint state;
			uint stateMask;
			[Interop::MarshalAs(Interop::UnmanagedType.LPTStr)]
			string pszText;
			int cchTextMax;
			int iImage;
			int iSelectedImage;
			int cChildren;
			System.IntPtr lParam;
			int iIntegral;
		}

		[Interop::StructLayout(Interop::LayoutKind.Sequential,CharSet=Interop::CharSet.Auto)]
		public struct INSERTSTRUCT{
			public HTREEITEM hParent;
			public HTREEITEM hInsertAfter;
		//#if _WIN32_IE >= 0x0400)
			public ITEMEX itemex;
		//#else
			//ITEM item;
		//#endif
		}

		public static readonly HTREEITEM TVI_ROOT	=new HTREEITEM((System.IntPtr)(-0x10000));
		public static readonly HTREEITEM TVI_FIRST	=new HTREEITEM((System.IntPtr)(-0x0FFFF));
		public static readonly HTREEITEM TVI_LAST	=new HTREEITEM((System.IntPtr)(-0x0FFFE));
		public static readonly HTREEITEM TVI_SORT	=new HTREEITEM((System.IntPtr)(-0x0FFFD));

		[Interop::DllImport("user32.dll",CharSet=Interop::CharSet.Auto,EntryPoint="SendMessage")]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		private static extern bool bool_SNDMSG(Interop::HandleRef hWnd,WM msg,System.IntPtr wParam,System.IntPtr lParam);
		private static bool bool_SNDMSG(Frms::IWin32Window wnd,WM msg,System.IntPtr wParam,System.IntPtr lParam){
			return bool_SNDMSG(new Interop::HandleRef(wnd,wnd.Handle),msg,wParam,lParam);
		}
		[Interop::DllImport("user32.dll",CharSet=Interop::CharSet.Auto,EntryPoint="SendMessage")]
		private static extern uint uint_SNDMSG(Interop::HandleRef hWnd,WM msg,System.IntPtr wParam,System.IntPtr lParam);
		private static uint uint_SNDMSG(Frms::IWin32Window wnd,WM msg,System.IntPtr wParam,System.IntPtr lParam){
			return uint_SNDMSG(new Interop::HandleRef(wnd,wnd.Handle),msg,wParam,lParam);
		}

		//
		//	InsertItem
		//
		public static HTREEITEM InsertItem(Frms::IWin32Window wnd,ref INSERTSTRUCT lpis){
			return SendMessage(new Interop::HandleRef(wnd,wnd.Handle),TVM.INSERTITEM,System.IntPtr.Zero,ref lpis);
		}
		[Interop::DllImport("user32.dll",CharSet=Interop::CharSet.Auto)]
		private static extern HTREEITEM SendMessage(Interop::HandleRef hWnd,WM msg,System.IntPtr wParam,ref INSERTSTRUCT lParam);

		//
		//	DeleteItem
		//
		public static bool DeleteItem(Frms::IWin32Window wnd,HTREEITEM hitem){
			return bool_SNDMSG(wnd,TVM.DELETEITEM,System.IntPtr.Zero,hitem);
		}

		//
		//	DeleteAllItems
		//
		public static bool DeleteAllItems(Frms::IWin32Window wnd){
			return bool_SNDMSG(wnd,TVM.DELETEITEM,System.IntPtr.Zero,TVI_ROOT);
		}

		//
		//	Expand
		//
		public static bool Expand(Frms::IWin32Window wnd,HTREEITEM hitem,TVE code){
			return bool_SNDMSG(wnd,TVM.EXPAND,(System.IntPtr)(int)code,hitem);
		}
		public enum TVE{
			COLLAPSE	=0x0001,
			EXPAND		=0x0002,
			TOGGLE		=0x0003,
//#if (_WIN32_IE >= 0x0300)
			EXPANDPARTIAL=0x4000,
//#endif
			COLLAPSERESET=0x8000,
		}

		//
		//	GetItemRect
		//
		public static bool GetItemRect(Frms::IWin32Window wnd,HTREEITEM hitem,out RECT prc,System.IntPtr code){
			RECT rc=new RECT();
			System.IntPtr lparam=(System.IntPtr)(&rc);
			
			*(HTREEITEM*)lparam=hitem;
			bool ret=bool_SNDMSG(wnd,TVM.GETITEMRECT,code,lparam);
			prc=rc;
			
			return ret;
		}

		//
		//	GetCount
		//
		public static uint GetCount(Frms::IWin32Window wnd){
			return uint_SNDMSG(wnd,TVM.GETCOUNT,System.IntPtr.Zero,System.IntPtr.Zero);
		}

		//	GetIndent
		//	SetIndent
		//	GetImageList
		//	SetImageList
		//	GetNextItem
		//	GetChild
		//	GetNextSibling
		//	GetPrevSibling
		//	GetParent
		//	GetFirstVisible
		//	GetNextVisible
		//	GetPrevVisible
		//	GetSelection
		//	GetDropHilight
		//	GetRoot
		//	GetLastVisible
		//	SelectItem
		//	SelectDropTarget
		//	SelectSetFirstVisible
		//	GetItem
		//	SetItem
		//	EditLabel
		//	GetEditControl
		//	GetVisibleCount
		//
		//	HitTest
		//
		public static HTREEITEM HitTest(Frms::IWin32Window wnd,HITTESTINFO* lpht){
			return (HTREEITEM)User32.SendMessage(wnd,TVM.HITTEST,System.IntPtr.Zero,(System.IntPtr)lpht);
		}
		[Interop::StructLayout(Interop::LayoutKind.Sequential,CharSet=Interop::CharSet.Auto)]
		public struct HITTESTINFO{
			public int pt_x;
			public int pt_y;
			public TVHT flags;
			public System.IntPtr hItem;
		}
		[System.Flags]
		public enum TVHT:int{
			NOWHERE=0x0001,
			ONITEMICON=0x0002,
			ONITEMLABEL=0x0004,
			ONITEM=(ONITEMICON | ONITEMLABEL | ONITEMSTATEICON),
			ONITEMINDENT=0x0008,
			ONITEMBUTTON=0x0010,
			ONITEMRIGHT=0x0020,
			ONITEMSTATEICON=0x0040,

			ABOVE=0x0100,
			BELOW=0x0200,
			TORIGHT=0x0400,
			TOLEFT=0x0800,
		}
		//	CreateDragImage
		//	SortChildren
		//	EnsureVisible
		//	SortChildrenCB
		//	EndEditLabelNow
		//	GetISearchString
		//	SetToolTips
		//	GetToolTips
		//	SetInsertMark
		//	SetUnicodeFormat
		//	GetUnicodeFormat
		//	SetItemHilight
		//	GetItemHilight
		//	GetBkColor
		//	SetBkColor
		//	GetTextColor
		//	SetTextColor
		//	SetScrollTime
		//	GetScrollTime
		//	SetInsertMarkColor
		//	GetInsertMarkColor
		//	SetItemState
		//	GetItemState
		//	SetCheckState
		//	GetCheckState
		//	SetLineColor
		//	GetLineColor
		//	MapAccIDToHTREEITEM
		//	MapHTREEITEMToAccID
		//	
	}
}