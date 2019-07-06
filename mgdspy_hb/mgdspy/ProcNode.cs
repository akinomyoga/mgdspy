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
	/// �v���Z�X��\�� <see cref="System.Windows.Forms.TreeNode"/> �ł��B
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
		/// �v���Z�X�Ɋ֘A�t����ꂽ ID ���擾���܂��B
		/// </summary>
		public int Id{get{return this.Id;}}
		/// <summary>
		/// ���̃v���Z�X�� ClrVersion ���擾���܂��B
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
			const string ERR_NOT_NET2_0=".NET Framework 2.0 �� Load ����Ă��Ȃ� process �� channel ���q�����͏o���܂���B";
			const string ERR_NO_WINDOWS="Window ������ێ����Ă��Ȃ� process �� channel ���q�����͏o���܂���B";
			//---------------------------------------------
			if(this.channel!=null)return;
			
			if(this.ClrVersion!=ClrVersion.v2_0)
				throw new System.InvalidOperationException(ERR_NOT_NET2_0);
			if(this.wnds.Count==0)
				throw new System.InvalidOperationException(ERR_NO_WINDOWS);

			this.channel=new mwg.Interop.Channel(this.wnds[0].HWnd);

			// �������� Assembly ���o��������
			this.channel.SendObject(typeof(WndNode));
			
			foreach(WndNode w in this.wnds){
				w.UpdateChannel(this.channel);
			}
		}
		//===========================================================
		//		�C���X�^���X�̐���
		//===========================================================
		/// <summary>
		/// Process �̃R���X�g���N�^�ł��B
		/// </summary>
		/// <param name="id">�v���Z�X�� ID ���w�肵�܂��B</param>
		private ProcNode(int id):this(Diag::Process.GetProcessById(id)){}
		/// <summary>
		/// Process �̃R���X�g���N�^�ł��B
		/// </summary>
		/// <param name="proc">���� Process ���Q�Ƃ��� System.Diagnostics.Pocess ���w�肵�܂��B</param>
		/// <remarks>
		/// �Ή�����v���Z�X�� Managed ���ǂ����̊m�F�͂��̍ۂɍs���܂��B
		/// </remarks>
		private ProcNode(System.Diagnostics.Process proc):base(proc.ProcessName) {
			this.process=proc;
			this.id=proc.Id;
			this.clrver=DetermineClrVersion(proc);
			
			// clr version ��F�ŕ\��
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
		/// �w�肵�� process �� ClrVersion ���擾���܂��B
		/// </summary>
		/// <param name="process">ClrVersion ��m�肽�� process ���w�肵�܂��B</param>
		/// <returns>�����_�ł̎w�肵�� process �� ClrVersion ��Ԃ��܂��B</returns>
		private static ClrVersion DetermineClrVersion(Diag::Process process){
			try{
				foreach(Diag::ProcessModule module in process.Modules){
					try{
						string name=module.ModuleName.ToLower();
						// �� nicotool �����s�����珟��� .ni.dll �� ngen ���Ă��ꂽ�ׁA
						// ���̃R���s���[�^�ł͂ǂ̎��s�t�@�C���� debug ���ȊO�� .ni.dll �Ŏ��s�����
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
		//		�C���X�^���X�̊Ǘ�
		//===========================================================
		private static Gen::Dictionary<int,ProcNode> id2node=new Gen::Dictionary<int,ProcNode>();
		/// <summary>
		/// ���� Window ��ێ����Ă��� process ��񋓂��܂��B
		/// </summary>
		/// <returns>�擾���� process �̏W����Ԃ��܂��B</returns>
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
		/// Process Id ���� ProcNode ���擾���܂��B
		/// </summary>
		/// <param name="id">Process �� Id ���w�肵�܂��B</param>
		/// <returns>�擾���� ProcNode ��Ԃ��܂��B</returns>
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
		/// Window handle ���� ProcNode ���擾���܂��B
		/// </summary>
		/// <param name="id">Process �̏������� window �� handle ���w�肵�܂��B</param>
		/// <returns>�擾���� ProcNode ��Ԃ��܂��B</returns>
		public static ProcNode GetProcessNodeFromHWND(System.IntPtr hWnd){
			uint id;
			User32.GetWindowThreadProcessId(hWnd,out id);
			return GetProcessNodeFromId((int)id);
		}
		/// <summary>
		/// Process ���� ProcNode ���擾���܂��B
		/// </summary>
		/// <param name="id">Process ���w�肵�܂��B</param>
		/// <returns>�擾���� ProcNode ��Ԃ��܂��B</returns>
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