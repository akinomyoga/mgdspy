//#define DESIGN

using Frms=System.Windows.Forms;
using Gdi=System.Drawing;
using Gen=System.Collections.Generic;
using Ref=System.Reflection;

#if DESIGN
using BaseClass=System.Windows.Forms.UserControl;
#else
using BaseClass=System.Windows.Forms.TreeView;
#endif

namespace mwg.mgdspy{
	public class WindowTreeView:BaseClass{
		private System.Windows.Forms.ContextMenu menuWnd;
		private System.Windows.Forms.MenuItem mniIndicate;
		private System.Windows.Forms.MenuItem mniRefresh;
		private System.Windows.Forms.ContextMenu menuPrc;
		private System.Windows.Forms.MenuItem mniHook;
	
		public WindowTreeView(){
			InitializeComponent();
			this.ImageList=WindowIconList.list;
		}

		public void RefreshProcesses(){
			this.Nodes.Clear();

			Gen::List<Frms::TreeNode> nodes=new System.Collections.Generic.List<System.Windows.Forms.TreeNode>();
			foreach(Frms::TreeNode node in ProcNode.GetCurrentProcesses()){
				nodes.Add(node);
			}

			this.Nodes.AddRange(nodes.ToArray());
		}

		#region Designer's code
#if DESIGN
		private void InitializeComponent() {
			this.menuWnd=new System.Windows.Forms.ContextMenu();
			this.mniIndicate=new System.Windows.Forms.MenuItem();
			this.mniRefresh=new System.Windows.Forms.MenuItem();
			this.menuPrc=new System.Windows.Forms.ContextMenu();
			this.mniHook=new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// menuWnd
			// 
			this.menuWnd.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniIndicate,
            this.mniRefresh});
			// 
			// mniIndicate
			// 
			this.mniIndicate.Index=0;
			this.mniIndicate.Text="ã≠í≤ï\é¶(&I)";
			this.mniIndicate.Click+=new System.EventHandler(this.mniIndicate_Click);
			// 
			// mniRefresh
			// 
			this.mniRefresh.Index=1;
			this.mniRefresh.Text="éq Window ÇÃçXêV(&R)";
			this.mniRefresh.Click+=new System.EventHandler(this.mniRefresh_Click);
			// 
			// menuPrc
			// 
			this.menuPrc.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniHook});
			// 
			// mniHook
			// 
			this.mniHook.Index=0;
			this.mniHook.Text="ê⁄ë±(&H)";
			this.mniHook.Click+=new System.EventHandler(this.mniHook_Click);
			// 
			// WindowTreeView
			// 
			this.Font=new System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN",9F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(128)));
			this.Name="WindowTreeView";
			this.Size=new System.Drawing.Size(150,245);
			this.MouseUp+=new System.Windows.Forms.MouseEventHandler(this.WindowTreeView_MouseUp);
			this.ResumeLayout(false);

		}
#else
		private void InitializeComponent() {
			this.menuWnd=new System.Windows.Forms.ContextMenu();
			this.mniIndicate=new System.Windows.Forms.MenuItem();
			this.mniRefresh=new System.Windows.Forms.MenuItem();
			this.menuPrc=new System.Windows.Forms.ContextMenu();
			this.mniHook=new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// menuWnd
			// 
			this.menuWnd.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniIndicate,
            this.mniRefresh});
			// 
			// mniIndicate
			// 
			this.mniIndicate.Index=0;
			this.mniIndicate.Text="ã≠í≤ï\é¶(&I)";
			this.mniIndicate.Click+=new System.EventHandler(this.mniIndicate_Click);
			// 
			// mniRefresh
			// 
			this.mniRefresh.Index=1;
			this.mniRefresh.Text="éq Window ÇÃçXêV(&R)";
			this.mniRefresh.Click+=new System.EventHandler(this.mniRefresh_Click);
			// 
			// menuPrc
			// 
			this.menuPrc.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mniHook});
			// 
			// mniHook
			// 
			this.mniHook.Index=0;
			this.mniHook.Text="ê⁄ë±(&H)";
			this.mniHook.Click+=new System.EventHandler(this.mniHook_Click);
			// 
			// WindowTreeView
			// 
			this.Font=new System.Drawing.Font("ÇlÇr ÉSÉVÉbÉN",9F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(128)));
			this.HideSelection=false;
			this.BeforeExpand+=new System.Windows.Forms.TreeViewCancelEventHandler(this.WindowTreeView_BeforeExpand);
			this.MouseUp+=new System.Windows.Forms.MouseEventHandler(this.WindowTreeView_MouseUp);
			this.ResumeLayout(false);

		}
#endif
		#endregion

		private void WindowTreeView_BeforeExpand(object sender,System.Windows.Forms.TreeViewCancelEventArgs e) {
			foreach(System.Windows.Forms.TreeNode node in e.Node.Nodes){
				if(node is WndNode){
					WndNode w=(WndNode)node;
					w.RefreshChildWindows();
				}
			}
		}

		Frms::TreeNode contextNode=null;
		private void WindowTreeView_MouseUp(object sender,System.Windows.Forms.MouseEventArgs e) {
			if(e.Button==System.Windows.Forms.MouseButtons.Right){
				Gdi::Point p=new Gdi::Point(e.X,e.Y);
				this.contextNode=this.GetNodeAt(p);
				if(this.contextNode is WndNode){
					this.menuWnd.Show(this,p);
				}else if(this.contextNode is ProcNode){
					this.menuPrc.Show(this,p);
				}
			}
		}

		private void mniIndicate_Click(object sender,System.EventArgs e) {
			WndNode w=this.contextNode as WndNode;
			if(w!=null){
				Gdi::Rectangle rect=w.Win32.Rect;
				Gdi::Color col=System.Drawing.Color.Black;
				for(int i=0;i<7;i++){
					Frms::ControlPaint.DrawReversibleFrame(rect,col,Frms::FrameStyle.Thick);
					System.Threading.Thread.Sleep(100);
				}
				System.Threading.Thread.Sleep(250);
				Frms::ControlPaint.DrawReversibleFrame(rect,col,Frms::FrameStyle.Thick);
			}

		}

		private void mniRefresh_Click(object sender,System.EventArgs e){
			WndNode w=this.contextNode as WndNode;
			if(w!=null){
				w.Collapse();
				w.RefreshChildWindows();
			}
		}

		private void mniHook_Click(object sender,System.EventArgs e){
			ProcNode p=this.contextNode as ProcNode;
			if(p!=null){
				p.CreateChannel();
			}
		}
	}

	internal static class WindowIconList{
		public static Frms::ImageList list=new Frms::ImageList();
		private static int processIconIndex=0;
		private static int windowIconIndex=0;

		private static Gen::Dictionary<System.Type,int> type2index
			=new System.Collections.Generic.Dictionary<System.Type,int>();
		private static Gen::Dictionary<string,int> cls2index
			=new System.Collections.Generic.Dictionary<string,int>();
		private static Gen::Dictionary<string,int> clsStart2index
			=new System.Collections.Generic.Dictionary<string,int>();
		//===========================================================
		//		èâä˙âªãyÇ— Image ÇÃìoò^
		//===========================================================
		private static Ref::Assembly thisasm=typeof(WindowIconList).Assembly;
		static WindowIconList(){
			const string IMAGE_BASE="mwg.ManagedSpy.Icons.";

			list.ColorDepth=System.Windows.Forms.ColorDepth.Depth32Bit;
			list.ImageSize=new System.Drawing.Size(16,16);

			// äÓñ{
			processIconIndex=AddBitmapFromResource(IMAGE_BASE+"Prc.bmp");
			windowIconIndex=AddBitmapFromResource(IMAGE_BASE+"Wnd.bmp");
			type2index[typeof(System.ComponentModel.Component)]=AddBitmapFromResource(IMAGE_BASE+"Component.bmp");
			type2index[typeof(object)]=windowIconIndex;

			// ìoò^çœ Window Class
			System.Xml.XmlDocument doc=new System.Xml.XmlDocument();
			using(System.IO.Stream stream=thisasm.GetManifestResourceStream(IMAGE_BASE+"Icons.xml")){
				doc.Load(stream);
			}

			Gen::Dictionary<string,int> resname2index=new System.Collections.Generic.Dictionary<string,int>();
			foreach(System.Xml.XmlNode node in doc.GetElementsByTagName("class")){
				System.Xml.XmlElement elem=node as System.Xml.XmlElement;
				if(elem==null)continue;

				string name=elem.GetAttribute("name");
				string image=elem.GetAttribute("image");
				bool isStart="true"==elem.GetAttribute("startsWith").Trim().ToLower();
				if(name==""||image=="")continue;

				if(resname2index.ContainsKey(image)){
					(isStart?clsStart2index:cls2index)[name]=resname2index[image];
					continue;
				}

				int val=AddBitmapFromResource(IMAGE_BASE+image);
				if(val!=windowIconIndex){
					resname2index[image]=val;
					(isStart?clsStart2index:cls2index)[name]=val;
				}
			}
		}
		private static int AddBitmapFromResource(string resname){
			System.IO.Stream stream=thisasm.GetManifestResourceStream(resname);
			Gdi::Bitmap bmp=new System.Drawing.Bitmap(stream);
			// ‰¢Ç≈ï¬Ç∂ÇƒÇÕçsÇØÇ»Ç¢
			//stream.Close();

			int ret=list.Images.Count;
			list.Images.Add(bmp,Gdi::Color.Teal);
			return ret;
		}
		private static int AddBitmapFromType(System.Type t){
			int ret=list.Images.Count;

			object[] attrs=t.GetCustomAttributes(typeof(System.Drawing.ToolboxBitmapAttribute),false);
			if(attrs.Length>0){
				System.Drawing.ToolboxBitmapAttribute attr=(System.Drawing.ToolboxBitmapAttribute)attrs[0];
				list.Images.Add(attr.GetImage(t));
				return ret;
			}

			try{
				string resName=System.IO.Path.GetFileName(t.FullName.Replace(".","\\"))+".bmp";
				System.IO.Stream stream=t.Assembly.GetManifestResourceStream(t,resName);
				if(stream!=null){
					Gdi::Image img=new System.Drawing.ToolboxBitmapAttribute(t).GetImage(null);
					list.Images.Add(img);
					return ret;
				}
			}catch{}

			return windowIconIndex;
		}
		//===========================================================
		//		TreeNode ÇÃ ImageIndex ÇÃê›íË
		//===========================================================
		public static void UpdateImage(Frms::TreeNode node){
			int index;
			if(node is WndNode){
				WndNode wndnode=(WndNode)node;
				index=GetImageIndex(wndnode.CtsType)??GetImageIndex(wndnode.Win32.ClassName)??windowIconIndex;
			}else if(node is ProcNode){
				index=processIconIndex;
			}else{
				index=windowIconIndex;
			}
			node.ImageIndex=index;
			node.SelectedImageIndex=index;
		}
		private static int? GetImageIndex(System.Type t){
			if(t==null)return null;

			// ä˘Ç…ìoò^ÇµÇƒÇ†ÇÈèÍçá
			int ret;
			if(type2index.TryGetValue(t,out ret)){
				return ret;
			}

			// ÉäÉ\Å[ÉXÇ©ÇÁéÊìæèoóàÇΩèÍçá
			ret=AddBitmapFromType(t);
			if(ret!=windowIconIndex){
				type2index[t]=ret;
				return ret;
			}

			// åpè≥å≥Ç©ÇÁåüçı
			ret=(int)GetImageIndex(t.BaseType??typeof(object));
			type2index[t]=ret;
			return ret;
		}
		private static int? GetImageIndex(string clsname){
			int ret;
			if(cls2index.TryGetValue(clsname,out ret)){
				return ret;
			}
			
			foreach(Gen::KeyValuePair<string,int> pair in clsStart2index){
				if(clsname.StartsWith(pair.Key))return pair.Value;
			}

			return null;
		}
	}
}