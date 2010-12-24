using Interop=System.Runtime.InteropServices;
using Diag=System.Diagnostics;
using Ref=System.Reflection;
using Forms=System.Windows.Forms;
using Kernel32=mwg.Win32.Kernel32;
using User32=mwg.Win32.User32;

namespace mwg.Remote{
	static class SerializationTest{
		public delegate int test1delegate();
		public static int Test1(){
			int a=11;

			System.IO.MemoryStream mstr=new System.IO.MemoryStream();
			UnsafeSerializer.Serialize(mstr,(test1delegate)delegate(){
			  for(int i=0;i<123;i++){
			    a++;
			  }
				return a;
			});
			//UnsafeSerializer.Serialize(mstr,null);
			mstr.Position=0;
			test1delegate d=(test1delegate)UnsafeSerializer.Deserialize(mstr);
			d();
			return d();
		}

		public const string IDSTR_WM_CHANNEL="mwg.Interop.WM_Channel";

		public static string test2(){
			IpcSessionSv session=IpcManager.CreateSessionSv();
			ChannelStation station=new mwg.Remote.ChannelStation(session);

			// Get HookPoint3
			System.IntPtr hMod=Kernel32.LoadLibrary(test2_GetHkPath());
			if(hMod==System.IntPtr.Zero)return "<loadlibrary fail>";
			System.IntPtr hHP3=Kernel32.GetProcAddress(hMod,"HookPoint3");
			if(hHP3==System.IntPtr.Zero)return "<GetProcAddress fail>";

			// Hook
			System.IntPtr hWnd=test2_GetHwnd();
			if(hWnd==System.IntPtr.Zero)return "<getting hwnd fail>";
			uint procidDst;
			uint threadId=User32.GetWindowThreadProcessId(hWnd,out procidDst);
			System.IntPtr hHook=User32.SetWindowsHookEx(mwg.Win32.WH.CALLWNDPROC,hHP3,hMod,threadId);
			if(hHook==System.IntPtr.Zero)return "<SetWindowsHookEx fail>";

			// Send WM_Channel
			WMChannel.WParamCode.InitNotifyOriginalHook.SendMessage(hWnd,hHook);
			WMChannel.WParamCode.InitNotifyProcessId.SendMessage(hWnd,(System.IntPtr)Kernel32.GetCurrentProcessId());
			WMChannel.WParamCode.InitNotifySessionId.SendMessage(hWnd,(System.IntPtr)session.SessionId);

			if(!User32.UnhookWindowsHookEx(hHook))return "<UnhookWindowsHookEx fail>";

			Kernel32.FreeLibrary(hMod);

			int x=100;
			station.RemoteInvoke(delegate(){
				x+=10;
				Forms::MessageBox.Show("RemoteInvoke! "+x);
			});

			return "<comp>"+x;
		}
		static string test2_GetHkPath(){
			string dllloc=typeof(SerializationTest).Assembly.Location;
			dllloc=System.IO.Path.GetDirectoryName(dllloc);
			dllloc=System.IO.Path.GetDirectoryName(dllloc);
			dllloc=System.IO.Path.GetDirectoryName(dllloc);
			dllloc=System.IO.Path.GetDirectoryName(dllloc);
			dllloc=System.IO.Path.Combine(dllloc,"debug\\mgdspy_hk.dll");
			return dllloc;
		}
		static System.IntPtr test2_GetHwnd(){
			System.IntPtr ret=System.IntPtr.Zero;
			User32.EnumWindows(delegate(System.IntPtr hWnd,System.IntPtr lparam){
				if(User32.GetWindowText(hWnd)=="電卓"){
					ret=hWnd;
					return false;
				}
				return true;
			},System.IntPtr.Zero);
			return ret;

			//System.IntPtr hWnd=(System.IntPtr)0x4c0c8a;
			//return hWnd;
		}
		static void test2_hoge(){
			System.Diagnostics.Process proc=new System.Diagnostics.Process();
			try{
				foreach(Diag::ProcessModule module in proc.Modules){
					try{
						string name=module.ModuleName.ToLower();
						// ※ nicotool を実行したら勝手に .ni.dll で ngen してくれた為、
						// このコンピュータではどの実行ファイルも debug 時以外は .ni.dll で実行される
						if(name!="mwg.remote.dll")continue;

						Ref::AssemblyName asmname=Ref::AssemblyName.GetAssemblyName(module.FileName);
						if(asmname==null)continue;

						//asmname
					}catch{}
				}
			}catch{}

		}

	}
}