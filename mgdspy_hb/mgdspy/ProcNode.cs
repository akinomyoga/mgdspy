using Diag=System.Diagnostics;
using Ref=System.Reflection;
using Gen=System.Collections.Generic;
using Gdi=System.Drawing;

using mwg.Win32;
using mwg.Interop;

namespace mwg.mgdspy{
	public enum ClrVersion{
		Unmanaged,
		v1_0,
		v1_1,
		v2_0,
		v4_0,
		UnknownClr,
	}

	/// <summary>
	/// プロセスを表す <see cref="System.Windows.Forms.TreeNode"/> です。
	/// </summary>
	public class ProcNode:System.Windows.Forms.TreeNode{
		private Diag::Process process;
		private int id;
		private ClrVersion clrver;
		private Channel channel=null;
		private Gen::List<WndNode> wnds=new Gen::List<WndNode>();
		internal void AddWindowNode(WndNode node){
			this.wnds.Add(node);
		}

		/// <summary>
		/// プロセスに関連付けられた ID を取得します。
		/// </summary>
		public int Id{get{return this.Id;}}
		/// <summary>
		/// このプロセスの ClrVersion を取得します。
		/// </summary>
		public ClrVersion ClrVersion{
			get{return this.clrver;}
		}
		internal Channel Channel{
			get{return this.channel;}
		}
		//===========================================================
		//		Hook
		//===========================================================
		public void CreateChannel(){
			const string ERR_NOT_NET2_0=".NET Framework 2.0 が Load されていない process に channel を繋ぐ事は出来ません。";
			const string ERR_NO_WINDOWS="Window を一つも保持していない process に channel を繋ぐ事は出来ません。";
			//---------------------------------------------
			if(this.channel!=null)return;
			
			if(this.ClrVersion!=ClrVersion.v2_0)
				throw new System.InvalidOperationException(ERR_NOT_NET2_0);
			if(this.wnds.Count==0)
				throw new System.InvalidOperationException(ERR_NO_WINDOWS);

			this.channel=new mwg.Interop.Channel(this.wnds[0].HWnd);

			// 向こうに Assembly を覚えさせる
			this.channel.SendObject(typeof(WndNode));
			
			foreach(WndNode w in this.wnds){
				w.UpdateChannel(this.channel);
			}
		}
		//===========================================================
		//		インスタンスの生成
		//===========================================================
		/// <summary>
		/// Process のコンストラクタです。
		/// </summary>
		/// <param name="id">プロセスの ID を指定します。</param>
		private ProcNode(int id):this(Diag::Process.GetProcessById(id)){}
		/// <summary>
		/// Process のコンストラクタです。
		/// </summary>
		/// <param name="proc">この Process が参照する System.Diagnostics.Pocess を指定します。</param>
		/// <remarks>
		/// 対応するプロセスが Managed かどうかの確認はこの際に行われます。
		/// </remarks>
		private ProcNode(System.Diagnostics.Process proc):base(proc.ProcessName) {
			this.process=proc;
			this.id=proc.Id;
			this.clrver=DetermineClrVersion(proc);
			
			// clr version を色で表示
			switch(this.clrver){
				case ClrVersion.v2_0:
					this.ForeColor=Gdi::Color.DarkGreen;
					break;
				case ClrVersion.v1_0:
				case ClrVersion.v1_1:
					this.ForeColor=Gdi::Color.Olive;
					break;
				case ClrVersion.UnknownClr:
					this.ForeColor=Gdi::Color.Red;
					break;
			}

			WindowIconList.UpdateImage(this);
		}
		/// <summary>
		/// 指定した process の ClrVersion を取得します。
		/// </summary>
		/// <param name="process">ClrVersion を知りたい process を指定します。</param>
		/// <returns>現時点での指定した process の ClrVersion を返します。</returns>
		private static ClrVersion DetermineClrVersion(Diag::Process process){
			try{
				foreach(Diag::ProcessModule module in process.Modules){
					try{
						string name=module.ModuleName.ToLower();
						// ※ nicotool を実行したら勝手に .ni.dll で ngen してくれた為、
						// このコンピュータではどの実行ファイルも debug 時以外は .ni.dll で実行される
						if(name!="mscorlib.dll"&&name!="mscorlib.ni.dll")continue;

						Ref::AssemblyName asmname=Ref::AssemblyName.GetAssemblyName(module.FileName);
						if(asmname==null)continue;

						if(asmname.Version.Major==1){
							if(asmname.Version.Minor==1)
								return ClrVersion.v1_1;
							else if(asmname.Version.Minor==0)
								return ClrVersion.v1_0;
						}else if(asmname.Version.Major==2&&asmname.Version.Minor==0){
							return ClrVersion.v2_0;
						}else if(asmname.Version.Major==4&&asmname.Version.Minor==0){
							return ClrVersion.v4_0;
						}

						return ClrVersion.UnknownClr;
					}catch{}
				}
			}catch{}
			return ClrVersion.Unmanaged;
		}
		//===========================================================
		//		インスタンスの管理
		//===========================================================
		private static Gen::Dictionary<int,ProcNode> id2node=new Gen::Dictionary<int,ProcNode>();
		/// <summary>
		/// 現在 Window を保持している process を列挙します。
		/// </summary>
		/// <returns>取得した process の集合を返します。</returns>
		public static Gen::ICollection<ProcNode> GetCurrentProcesses(){
			id2node.Clear();
			User32.EnumWindows(GetCurrentProcesses_enumWndProc,System.IntPtr.Zero);
			return id2node.Values;
		}
		private static bool GetCurrentProcesses_enumWndProc(System.IntPtr hWnd,System.IntPtr arg) {
			GetProcessNodeFromHWND(hWnd).Nodes.Add(WndNode.GetWindowNode(hWnd));
			return true;
		}
		/// <summary>
		/// Process Id から ProcNode を取得します。
		/// </summary>
		/// <param name="id">Process の Id を指定します。</param>
		/// <returns>取得した ProcNode を返します。</returns>
		public static ProcNode GetProcessNodeFromId(int id){
			ProcNode node;
			if(id2node.TryGetValue(id,out node)){
				return node;
			}else{
				node=new ProcNode(id);
				id2node[id]=node;
				return node;
			}
		}
		/// <summary>
		/// Window handle から ProcNode を取得します。
		/// </summary>
		/// <param name="id">Process の所持する window の handle を指定します。</param>
		/// <returns>取得した ProcNode を返します。</returns>
		public static ProcNode GetProcessNodeFromHWND(System.IntPtr hWnd){
			uint id;
			User32.GetWindowThreadProcessId(hWnd,out id);
			return GetProcessNodeFromId((int)id);
		}
		/// <summary>
		/// Process から ProcNode を取得します。
		/// </summary>
		/// <param name="id">Process を指定します。</param>
		/// <returns>取得した ProcNode を返します。</returns>
		public static ProcNode GetProcessNodeFromProcess(Diag::Process process){
			ProcNode node;
			if(id2node.TryGetValue(process.Id,out node)){
				return node;
			}else{
				node=new ProcNode(process);
				id2node[process.Id]=node;
				return node;
			}
		}
	}
}