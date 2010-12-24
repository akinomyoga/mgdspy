using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;

using Rem=System.Runtime.Remoting;
using Diag=System.Diagnostics;

namespace mwg.Remote{

	static class WMChannel{
		public const string IDSTR_WM_CHANNEL="mwg.Interop.WM_Channel";
		public static readonly mwg.Win32.WM WM_CHANNEL
			=mwg.Win32.User32.RegisterWindowMessage(IDSTR_WM_CHANNEL);

		public enum WParamCode{
			InitNotifyOriginalHook =1,
			InitNotifyProcessId    =2,
			InitNotifySessionId    =3,

			RecProcessDownData,
			RecDeleteReceiver,
		}

		public static System.IntPtr SendMessage(this WParamCode code,System.IntPtr hWnd,System.IntPtr lParam){
			return mwg.Win32.User32.SendMessage(hWnd,WM_CHANNEL,(System.IntPtr)(int)code,lParam);
		}
	}
	interface IBucket{
		// [Cl] → Sv
		//----------------------------------------------------------------
		/// <summary>
		/// データをクライアントからサーバに送ります。
		/// </summary>
		/// <param name="code"></param>
		/// <param name="data"></param>
		void Upload(int code,byte[] data);

		// [Sv] → Cl
		//----------------------------------------------------------------
		/// <summary>
		/// サーバからクライアントに送るデータを取得します。
		/// サーバからクライアントの Receiver インスタンスへ、
		/// WindowMessage を送る事によって同期します。
		/// </summary>
		byte[] DownData{get;}
		/// <summary>
		/// サーバからクライアントに送るデータの種類を取得します。
		/// </summary>
		int DownCode{get;}
		System.IntPtr ReceiverHandle{get;set;}
	}


	public class IpcManager{
		static int self_pid;
		static IpcManager(){
			self_pid=Diag::Process.GetCurrentProcess().Id;
		}

		static string ipcch_name;
		static Rem::Channels.Ipc.IpcServerChannel ipcch;
		static readonly object sync=new object();
		public static bool Initialize(){
			lock(sync){
				if(ipcch!=null)return true;
				try{
					ipcch_name=string.Format("pid{0}.interproc.mwg",(uint)self_pid);
					ipcch=new Rem::Channels.Ipc.IpcServerChannel(ipcch_name);
					Rem::Channels.ChannelServices.RegisterChannel(ipcch,false); // true
				}catch{
					return false;
				}
				return true;
			}
		}

		internal static string GetBucketUri(int sv_pid,int sid){
			return string.Format("ipc://pid{0}.interproc.mwg/sess{1}.bsk",(uint)sv_pid,sid);
		}
		internal static string GetBucketName(int sid){
			return string.Format("sess{0}.bsk",sid);
		}
		static Gen::Dictionary<string,IpcSessionSv> dicSessSv
			=new Gen::Dictionary<string,IpcSessionSv>();
		static int dicSessSvCount=0;
		/// <summary>
		/// 新しいセッションを作成します。
		/// </summary>
		/// <returns>新しく作成したセッションを返します。</returns>
		public static IpcSessionSv CreateSessionSv(){
			Initialize();
			
			lock(dicSessSv){
				int sid=dicSessSvCount++;
				IpcSessionSv ret=new IpcSessionSv(sid);
				dicSessSv[ret.BucketUri]=ret;
				return ret;
			}
		}
		/// <summary>
		/// 自身の開いたセッションを取得します。
		/// </summary>
		/// <param name="sid">セッションの識別番号を指定します。</param>
		/// <returns>指定した識別番号に対応するセッションが見付かった場合には、それを返します。
		/// 見付からなかった場合には null を返します。</returns>
		public static IpcSessionSv OpenSessionSv(int sid){
			string k=GetBucketUri(self_pid,sid);
			lock(dicSessSv){
				IpcSessionSv ret;
				if(dicSessSv.TryGetValue(k,out ret)){
					return ret;
				}else{
					return null;
				}
			}
		}
		static Gen::Dictionary<string,IpcSessionCl> dicSessCl
			=new Gen::Dictionary<string,IpcSessionCl>();
		/// <summary>
		/// 既存のセッションにクライアントとして接続します。
		/// </summary>
		/// <param name="sv_pid"></param>
		/// <param name="sid"></param>
		/// <returns></returns>
		public static IpcSessionCl OpenSessionCl(int sv_pid,int sid){
			string k=GetBucketUri(sv_pid,sid);
			lock(dicSessCl){
				IpcSessionCl ret;
				if(dicSessCl.TryGetValue(k,out ret)){
					return ret;
				}else{
					ret=new IpcSessionCl(sv_pid,sid);
					dicSessCl[k]=ret;
					return ret;
				}
			}
		}
	}
	public class IpcSessionSv:IDataGate{
		readonly int sid;
		public int SessionId{
			get{return this.sid;}
		}

		readonly string bucket_uri;
		public string BucketUri{
			get{return this.bucket_uri;}
		}

		readonly Bucket bucket;
		internal IpcSessionSv(int sid){
			this.sid=sid;
			this.bucket=new Bucket(this);

			int sv_pid=Diag::Process.GetCurrentProcess().Id;
			this.bucket_uri=IpcManager.GetBucketUri(sv_pid,sid);

			Rem::RemotingServices.Marshal(
			  this.bucket,IpcManager.GetBucketName(sid),typeof(IBucket));
			//Rem::RemotingServices.Marshal(
			//  this.bucket,this.bucket_uri,typeof(IBucket));
		}

		//=========================================================================
		// IDataGate 実装
		//-------------------------------------------------------------------------
		public void Send(int code,byte[] data){
			if(!bucket.IsReady)return;
			lock(bucket){
				bucket.DownCode=code;
				bucket.DownData=data;
				mwg.Win32.User32.SendMessage(
					bucket.ReceiverHandle,
					WMChannel.WM_CHANNEL,
					(System.IntPtr)(int)WMChannel.WParamCode.RecProcessDownData,
					System.IntPtr.Zero
					);
				bucket.DownData=null;
			}
		}

		public event DataReceiver DataReceived;
		protected void OnDataReceived(int code,byte[] data){
			if(this.DataReceived!=null)
				this.DataReceived(code,data);
		}
		//=========================================================================
		// IBucket 実装
		//-------------------------------------------------------------------------
		/// <summary>
		/// クライアントからサーバへデータを送る為のインスタンスです。
		/// サーバからクライアントへデータを送る時のバッファにもなります。
		/// </summary>
		class Bucket:System.MarshalByRefObject,IBucket{
			readonly IpcSessionSv parent;
			internal Bucket(IpcSessionSv parent){
				this.parent=parent;
			}
			//--------------------------------------------------------------
			//	Properties
			byte[] data=null;
			public byte[] DownData{
				get{return this.data;}
				set{this.data=value;}
			}

			int downid;
			public int DownCode{
				get{return this.downid;}
				set{this.downid=value;}
			}

			System.IntPtr handle=System.IntPtr.Zero;
			public System.IntPtr ReceiverHandle{
				get{return this.handle;}
				set{this.handle=value;}
			}
			public bool IsReady{
				get{return this.handle!=System.IntPtr.Zero;}
			}
			//--------------------------------------------------------------
			//	DataSend
			public void Upload(int code,byte[] data){
				this.parent.OnDataReceived(code,data);
			}
		}

	}

	public class IpcSessionCl:IDataGate{
		readonly int sv_pid;
		public int ServerProcessId{
			get{return this.sv_pid;}
		}
		readonly int sid;
		public int SessionId{
			get{return this.sid;}
		}

		readonly string bucket_uri;
		readonly IBucket bucket;
		readonly Receiver receiver;
		internal IpcSessionCl(int sv_pid,int sid){
			this.sv_pid=sv_pid;
			this.sid=sid;

			this.bucket_uri=IpcManager.GetBucketUri(sv_pid,sid);
			this.bucket=(IBucket)System.Activator.GetObject(
				typeof(IBucket),this.bucket_uri);
			this.receiver=new Receiver(this);
			this.bucket.ReceiverHandle=this.receiver.Handle;
		}

		//=========================================================================
		// IDataGate 実装
		//-------------------------------------------------------------------------
		public void Send(int code,byte[] data){
			this.bucket.Upload(code,data);
		}

		public event DataReceiver DataReceived;
		protected void OnDataReceived(){
			if(this.DataReceived!=null)
				this.DataReceived(bucket.DownCode,bucket.DownData);
		}
		//=========================================================================
		// Receiver Window
		//-------------------------------------------------------------------------
		/// <summary>
		/// サーバからのメッセージを受け取る為のウィンドウです。
		/// </summary>
		class Receiver:Forms::Control{
			readonly IpcSessionCl parent;
			internal Receiver(IpcSessionCl parent){
				this.parent=parent;
				this.CreateControl();
			}

			static readonly Utils.ThreadPool thpool=new mwg.Remote.Utils.ThreadPool();
			class State{public bool completed;}
			protected override void WndProc(ref Forms::Message m){
				if((mwg.Win32.WM)m.Msg==WMChannel.WM_CHANNEL){
					m.Result=(System.IntPtr)(int)WMChannel.WM_CHANNEL;
					switch((WMChannel.WParamCode)m.WParam){
						case WMChannel.WParamCode.RecProcessDownData:
							State stat=new State();
							thpool.Post(delegate(){
								try{
									this.parent.OnDataReceived();
								}finally{
									stat.completed=true;
								}
							});

							while(!stat.completed)
								Forms::Application.DoEvents();

							break;
						case WMChannel.WParamCode.RecDeleteReceiver:
							this.Dispose();
							break;
					}
				}else{
					base.WndProc(ref m);
				}
			}
		}
		//=========================================================================
	}
}
