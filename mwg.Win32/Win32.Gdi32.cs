using Frms=System.Windows.Forms;
using Gdi=System.Drawing;
using Gen=System.Collections.Generic;
using CM=System.ComponentModel;
using Interop=System.Runtime.InteropServices;
using Ref=System.Reflection;

namespace mwg.Win32{
	public static class Gdi32 {
		[Interop::DllImport("gdi32.dll",CharSet=Interop::CharSet.Auto,SetLastError=true,ExactSpelling=true)]
		public static extern System.IntPtr SelectObject(Interop::HandleRef hDC,Interop::HandleRef hObject);
		public static System.IntPtr SelectObject(NMCUSTOMDRAW customDraw,Frms::OwnerDrawPropertyBag ownerdrawProps) {
			return SelectObject(
				new Interop::HandleRef(customDraw,customDraw.hdc),
				new Interop::HandleRef(ownerdrawProps,(System.IntPtr)get_FontHandle.Invoke(ownerdrawProps,null))
				);
		}
		private static Ref::MethodInfo get_FontHandle
			=typeof(Frms::OwnerDrawPropertyBag).GetMethod("get_FontHandle",Ref::BindingFlags.Instance|Ref::BindingFlags.NonPublic);
	}

	[Interop::StructLayout(Interop::LayoutKind.Sequential)]
	public struct NMHDR{
		public System.IntPtr hwndFrom;
		public System.IntPtr idFrom;
		public int code;
	}

	[Interop::StructLayout(Interop::LayoutKind.Sequential)]
	public struct NMCUSTOMDRAW {
		public NMHDR nmcd;

		public int dwDrawStage;
		public System.IntPtr hdc;

		public RECT rc;

		public System.IntPtr dwItemSpec;
		public int uItemState;
		public System.IntPtr lItemlParam;
	}

}