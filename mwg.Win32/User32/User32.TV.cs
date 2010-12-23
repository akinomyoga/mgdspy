using Frms=System.Windows.Forms;
using Gdi=System.Drawing;
using Gen=System.Collections.Generic;
using CM=System.ComponentModel;
using Interop=System.Runtime.InteropServices;
using Ref=System.Reflection;

namespace mwg.Win32 {
	public static partial class User32{
		[Interop::DllImport("user32.dll",CharSet=Interop::CharSet.Auto,ExactSpelling=true)]
		public static extern bool GetScrollInfo(Interop::HandleRef hWnd,int fnBar,ref SCROLLINFO si);
		public static bool GetScrollInfo(Frms::IWin32Window wnd,int fnBar,ref SCROLLINFO si){
			return GetScrollInfo(new Interop::HandleRef(wnd,wnd.Handle),fnBar,ref si);
		}

		[Interop::DllImport("user32.dll",CharSet=Interop::CharSet.Auto)]
		public static extern bool PostMessage(Interop::HandleRef hwnd,WM msg,System.IntPtr wparam,System.IntPtr lparam);
		[Interop::DllImport("user32.dll",CharSet=Interop::CharSet.Auto)]
		public static extern bool PostMessage(System.IntPtr hwnd,WM msg,System.IntPtr wparam,System.IntPtr lparam);
		public static bool PostMessage(Frms::IWin32Window wnd,WM msg,System.IntPtr wparam,System.IntPtr lparam){
			return PostMessage(new Interop::HandleRef(wnd,wnd.Handle),msg,wparam,lparam);
		}

		[Interop::DllImport("user32.dll",CharSet=Interop::CharSet.Auto)]
		public static extern System.IntPtr SendMessage(Interop::HandleRef hWnd,WM msg,System.IntPtr wParam,System.IntPtr lParam);
		[Interop::DllImport("user32.dll",CharSet=Interop::CharSet.Auto)]
		public static extern System.IntPtr SendMessage(System.IntPtr hWnd,WM msg,System.IntPtr wParam,System.IntPtr lParam);
		public static System.IntPtr SendMessage(Frms::IWin32Window wnd,WM msg,System.IntPtr wParam,System.IntPtr lParam) {
			return SendMessage(new Interop::HandleRef(wnd,wnd.Handle),msg,wParam,lParam);
		}
	}

	[Interop::StructLayout(Interop::LayoutKind.Sequential)]
	public struct SCROLLINFO {
		public int cbSize;
		public int fMask;
		public int nMin;
		public int nMax;
		public int nPage;
		public int nPos;
		public int nTrackPos;
	}


	[Interop::StructLayout(Interop::LayoutKind.Sequential)]
	public struct NMTVCUSTOMDRAW {
		public NMCUSTOMDRAW nmcd;
		public int clrText;
		public int clrTextBk;
		public int iLevel;
	}

}