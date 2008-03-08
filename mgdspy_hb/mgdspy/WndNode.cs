using Frms=System.Windows.Forms;
using Gen=System.Collections.Generic;
using mwg.Win32;
using mwg.Interop;

namespace mwg.mgdspy{
	public sealed class WndNode:Frms::TreeNode{
		private readonly System.IntPtr handle;
		public System.IntPtr HWnd{
			get{return this.handle;}
		}
		private readonly mwg.Windows.Window wnd32;
		private System.Type ctstype;

		private Channel channel;
		private ProcessLocal<Frms::Control> kCtrl=new ProcessLocal<Frms::Control>();

		/// <summary>
		/// 子ウィンドウ一覧を更新します。
		/// </summary>
		public void RefreshChildWindows(){
			this.Collapse();
			base.Nodes.Clear();
			User32.EnumChildWindows(this.handle,this.enum_childwnd,System.IntPtr.Zero);
		}
		private bool enum_childwnd(System.IntPtr hWnd,System.IntPtr lParam){
			WndNode w=GetWindowNode(hWnd);
			if(w.Parent!=null)w.Parent.Nodes.Remove(w);
			base.Nodes.Add(w);
			return true;
		}
		/// <summary>
		/// プロセスを表現する Node を取得します。
		/// </summary>
		public ProcNode ProcessNode{
			get{return ProcNode.GetProcessNodeFromHWND(this.handle);}
		}
		public mwg.Windows.Window Win32{
			get{return this.wnd32;}
		}
		/// <summary>
		/// この Window を管理している System.Windows.Forms.Control の型を取得します。
		/// </summary>
		public System.Type CtsType{
			get{return this.ctstype;}
		}

		//===========================================================
		//		状態の更新
		//===========================================================
		internal void UpdateChannel(mwg.Interop.Channel channel){
			this.channel=channel;
			if(channel==null)return;

			WindowInfo winfo=new WindowInfo();
			System.IntPtr handle=this.handle;
			ProcessLocal<Frms::Control> kCtrl=this.kCtrl;
			try{
				channel.InvokeDelegateV(delegate(){
					Frms::Control ctrl=Frms::Control.FromHandle(handle);
					if(ctrl==null)return;

					if(kCtrl.IsEmpty)kCtrl=ctrl;
					winfo=new WindowInfo(ctrl);
				});
			}catch{
				// 失敗したら BaseType で挑戦
				try{
					channel.InvokeDelegateV(delegate(){
						Frms::Control ctrl=Frms::Control.FromHandle(handle);
						if(ctrl==null)return;

						if(kCtrl.IsEmpty)kCtrl=ctrl;
						winfo=new WindowInfo(ctrl.GetType().BaseType,ctrl.Name);
					});
					winfo.name="<!BaseType> "+winfo.name;
				}catch{
					//(System.Exception e)
					//Frms::MessageBox.Show(e.ToString());
					return;
				}
			}
			
			if(winfo.type!=null){
				this.ctstype=winfo.type;
				this.Text=string.Format("{0:X8}# {1} [{2}]",(int)handle,winfo.name,winfo.type.FullName);
				this.BackColor=System.Drawing.Color.FromArgb(0xEE,0xFF,0xEE);
				WindowIconList.UpdateImage(this);
			}
			if(this.kCtrl.IsEmpty){
				this.kCtrl=kCtrl;
			}
		}
		[System.Serializable]
		struct WindowInfo{
			public System.Type type;
			public string name;

			public WindowInfo(Frms::Control ctrl):this(ctrl.GetType(),ctrl.Name){}
			public WindowInfo(System.Type type,string name){
				this.type=type;
				this.name=name;
				if(name==null||name=="")this.name="<noname>";
			}
		}
		//===========================================================
		//		インスタンスの管理
		//===========================================================
		internal WndNode(System.IntPtr hWnd):base("<初期化中...>"){
			this.handle=hWnd;
			this.wnd32=new mwg.Windows.Window(hWnd);
			string cap=this.wnd32.Caption;
			base.Text=string.Format("{0:X8}: {1} [{2}]",(int)handle,cap!=""?cap:"<noname>",this.wnd32.ClassName);

			if(!this.wnd32.IsVisible)
				this.ForeColor=System.Drawing.Color.Gray;

			// Process
			ProcNode proc=ProcNode.GetProcessNodeFromHWND(hWnd);
			proc.AddWindowNode(this);
			if(proc.Channel!=null)
				this.UpdateChannel(proc.Channel);

			// ImageIndex
			WindowIconList.UpdateImage(this);
		}
		private static Gen::Dictionary<System.IntPtr,WndNode> dic_nodes=new Gen::Dictionary<System.IntPtr,WndNode>();
		public static WndNode GetWindowNode(System.IntPtr hWnd){
			WndNode node;
			if(dic_nodes.TryGetValue(hWnd,out node)){
				return node;
			}else{
				node=new WndNode(hWnd);
				dic_nodes.Add(hWnd,node);
				return node;
			}
		}
	}
}