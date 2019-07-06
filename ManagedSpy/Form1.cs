//#define VERSION_1

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace mwg.ManagedSpy{
	/// <summary>
	/// Form1 �̊T�v�̐����ł��B
	/// </summary>
	public class Form1 : System.Windows.Forms.Form{
		/// <summary>
		/// �K�v�ȃf�U�C�i�ϐ��ł��B
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.PropertyGrid propGrid;

#if VERSION_1
		private WindowTree treeView;
#endif
		private mgdspy.WindowTreeView treeView2;
		/// <summary>
		/// Form1 �̃R���X�g���N�^�ł��B
		/// </summary>
		public Form1(){
			InitializeComponent();
#if VERSION_1
			//
			//	treeView
			//
			this.treeView=new WindowTree();
			this.treeView.Name="treeView";
			this.treeView.Dock=System.Windows.Forms.DockStyle.Fill;
			this.treeView.AfterSelect+=new TreeViewEventHandler(treeView_AfterSelect);
			this.Controls.SetChildIndex(this.treeView,0);
			this.Controls.Add(this.treeView);
#endif
			//
			//	treeView2
			//
			this.treeView2=new mwg.mgdspy.WindowTreeView();
			this.treeView2.Name="treeView";
			this.treeView2.Dock=System.Windows.Forms.DockStyle.Fill;
			this.treeView2.AfterSelect+=new TreeViewEventHandler(treeView2_AfterSelect);
			this.Controls.Add(this.treeView2);
			this.Controls.SetChildIndex(this.treeView2,0);
		}

		void treeView2_AfterSelect(object sender,TreeViewEventArgs e) {
			mwg.mgdspy.WndNode w=e.Node as mwg.mgdspy.WndNode;
			if(w!=null){
				this.propGrid.SelectedObject=w.Win32;
				return;
			}

			//ProcNode p=e.Node as ProcNode;
			//if(p!=null){
			//	return;
			//}
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent(){
			this.propGrid = new System.Windows.Forms.PropertyGrid();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.SuspendLayout();
			// 
			// propGrid
			// 
			this.propGrid.CommandsVisibleIfAvailable = true;
			this.propGrid.Dock = System.Windows.Forms.DockStyle.Right;
			this.propGrid.LargeButtons = false;
			this.propGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propGrid.Location = new System.Drawing.Point(440, 0);
			this.propGrid.Name = "propGrid";
			this.propGrid.Size = new System.Drawing.Size(240, 493);
			this.propGrid.TabIndex = 0;
			this.propGrid.Text = "propertyGrid1";
			this.propGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitter1.Location = new System.Drawing.Point(437, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 493);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(680, 493);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.propGrid);
			this.Name = "Form1";
			this.Text = "Spy#";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Form1_Load(object sender, System.EventArgs e){
#if VERSION_1
			mwg.ManagedSpy.Window.ReadTopLevelWindows();
			foreach(mwg.ManagedSpy.Process p in mwg.ManagedSpy.Process.List){
				//if(p.ManagedVersion==ManagedVersion.V1_0)
					this.treeView.Nodes.Add(p);
			}
			this.treeView.RefreshImageIndices();
#endif
			this.treeView2.RefreshProcesses();
		}

#if VERSION_1
		private void treeView_AfterSelect(object sender, TreeViewEventArgs e){
			this.propGrid.SelectedObject=this.treeView.SelectedNode;
		}
#endif
	}
}
