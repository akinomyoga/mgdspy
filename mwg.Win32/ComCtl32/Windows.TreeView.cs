using mwg.Win32;
using TV=mwg.Win32.TreeView;
using Interop=System.Runtime.InteropServices;

namespace mwg.Windows{
	public class TreeView:mwg.Windows.Window{
		public TreeView(System.IntPtr hWnd):base(hWnd){
		}

		public TV.HTREEITEM InsertItem(ref TV.INSERTSTRUCT insert){
			return TV.InsertItem(this,ref insert);
		}
		public bool DeleteItem(TV.HTREEITEM item){
			return TV.DeleteItem(this,item);
		}
		public bool DeleteAllItems(){
			return TV.DeleteAllItems(this);
		}
		public uint GetCount(){
			return TV.GetCount(this);
		}
		public bool Expand(TV.HTREEITEM item,TV.TVE code){
			return TV.Expand(this,item,code);
		}
	}
}