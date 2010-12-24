using Gen=System.Collections.Generic;
using Forms=System.Windows.Forms;

using Rem=System.Runtime.Remoting;
using Diag=System.Diagnostics;

namespace mwg.Remote{
	#region // クライアントの PID は要らない気がするので書き換え
	class IpcCenter{
		static int self_pid;
		static IpcCenter(){
			self_pid=Diag::Process.GetCurrentProcess().Id;
		}

		static string ipcch_name;
		static Rem::Channels.Ipc.IpcServerChannel ipcch;
		static readonly object sync=new object();
		public static bool CreateServer(){
			lock(sync){
				if(ipcch!=null)return true;
				try{
					ipcch_name=string.Format("pid{0}.interproc.mwg",(uint)self_pid);
					ipcch=new Rem::Channels.Ipc.IpcServerChannel(ipcch_name);
					Rem::Channels.ChannelServices.RegisterChannel(ipcch,true);
				}catch{
					return false;
				}
				return true;
			}
		}

		public static string GetBucketUri(int sv_pid,int cl_pid,int sid){
			return string.Format(
				"ipc://pid{0}.interproc.mwg/for{1}/sess{2}.bsk",
				(uint)sv_pid,(uint)cl_pid,sid
				);
		}
		static Gen::Dictionary<string,IpcSessionCl> dicSessCl
			=new Gen::Dictionary<string,IpcSessionCl>();
		static Gen::Dictionary<int,int> dicSessClCount
			=new Gen::Dictionary<int,int>();
		public static IpcSessionSv CreateSessionSv(int cl_pid){
			int count;
			if(!dicSessClCount.TryGetValue(cl_pid,out count))count=0;

			int sid=count++;
			dicSessClCount[cl_pid]=count;

			IpcSessionSv ret=new IpcSessionSv(cl_pid,sid);
			dicSessSv[ret.BucketUri]=ret;
			return ret;
		}
		public static IpcSessionSv OpenSessionSv(int cl_pid,int sid){
			string k=GetBucketUri(self_pid,cl_pid,sid);
			IpcSessionSv ret;
			if(dicSessSv.TryGetValue(k,out ret)){
				return ret;
			}else{
				return null;
			}
		}
		static Gen::Dictionary<string,IpcSessionSv> dicSessSv
			=new Gen::Dictionary<string,IpcSessionSv>();
		public static IpcSessionCl OpenSessionCl(int sv_pid,int sid){
			string k=GetBucketUri(sv_pid,self_pid,sid);
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
	class IpcSessionSv{
		int cl_pid;
		public int TargetProcessId{
			get{return this.cl_pid;}
		}
		int sid;
		public int SessionId{
			get{return this.sid;}
		}

		string bucket_uri;
		public string BucketUri{
			get{return this.bucket_uri;}
		}

		BucketForCl bucket;
		internal IpcSessionSv(int cl_pid,int sid){
			this.sid=sid;
			this.cl_pid=cl_pid;
			this.bucket=new BucketForCl();

			int sv_pid=Diag::Process.GetCurrentProcess().Id;
			this.bucket_uri=IpcCenter.GetBucketUri(sv_pid,cl_pid,sid);

			Rem::RemotingServices.Marshal(this.bucket,this.bucket_uri,typeof(IBucketForCl));
		}
	}
	class IpcSessionCl{
		int cl_pid;
		public int TargetProcessId{
			get{return this.cl_pid;}
		}
		int sid;
		public int SessionId{
			get{return this.sid;}
		}

		string bucket_uri;
		IBucketForCl bucket;
		internal IpcSessionCl(int sv_pid,int sid){
			int cl_pid=Diag::Process.GetCurrentProcess().Id;
			this.bucket_uri=IpcCenter.GetBucketUri(sv_pid,cl_pid,sid);
			this.bucket=(IBucketForCl)System.Activator.GetObject(
				typeof(IBucketForCl),this.bucket_uri);
		}

		public void Send(int id,byte[] data){
			this.bucket.Ring(id,data);
		}
	}
	#endregion

	#region ちょっと書いてみる
	class IpcConnection:System.IDisposable{
		/// <summary>
		/// Key: Client の ProcessId
		/// Value: 接続
		/// </summary>
		static readonly Gen::Dictionary<uint,IpcConnection> connections
			=new Gen::Dictionary<uint,IpcConnection>();
		static readonly uint self_procid=(uint)Diag::Process.GetCurrentProcess().Id;

		uint cl_procid;
		string ch_name;
		Rem::Channels.Ipc.IpcServerChannel sv_ch;

		internal IpcConnection(uint cl_procid){
			this.cl_procid=cl_procid;
			this.ch_name=string.Format("{0}.{1}.remote.mwg",cl_procid,self_procid);
			this.sv_ch=new Rem::Channels.Ipc.IpcServerChannel(this.ch_name);
			Rem::Channels.ChannelServices.RegisterChannel(this.sv_ch,true);
			// 初期化終了後、登録
			connections.Add(cl_procid,this);
		}

		public void Dispose(){
			if(this.sv_ch!=null){
				Rem::Channels.ChannelServices.UnregisterChannel(sv_ch);
				this.sv_ch=null;
			}
		}
	}
	#endregion
}
