using CM=System.ComponentModel;
using Ref=System.Reflection;

namespace mwg.Windows{
	internal class WindowConverter:CM::TypeConverter{
		/// <summary>
		/// PropertyDescriptorCollection �����擾���܂��B
		/// </summary>
		/// <param name="context">�R���e�L�X�g�����w�肵�܂��B</param>
		/// <param name="value">�v���p�e�B��ێ�����I�u�W�F�N�g���w�肵�܂��B</param>
		/// <param name="attributes">�v���p�e�B��I�������ł̎w����s���������w�肵�܂��B</param>
		/// <returns>�v���p�e�B�Q�̏���ێ����� PropertyDescriptorCollection ��Ԃ��܂��B</returns>
		public override CM::PropertyDescriptorCollection GetProperties(CM::ITypeDescriptorContext context,object value,System.Attribute[] attributes){
			const Ref::BindingFlags BF_PublicInstance=Ref::BindingFlags.Public|Ref::BindingFlags.Instance;
			const Ref::BindingFlags BF_AnyStatic=Ref::BindingFlags.Public|Ref::BindingFlags.NonPublic|Ref::BindingFlags.Static;
			//-------------------------------------------------------
			CM::PropertyDescriptorCollection pdc=new CM::PropertyDescriptorCollection(new CM::PropertyDescriptor[0]);

			//int i,iM;
			System.Reflection.PropertyInfo[] p=typeof(mwg.Windows.Window).GetProperties(BF_PublicInstance);
			WndPropertyDescriptor wpd;
			for(int i=0,iM=p.Length;i<iM;i++){
				if(p[i].GetIndexParameters().Length>0) continue;
				wpd=new WndPropertyDescriptor(p[i]);
				if(!wpd.IsBrowsable) continue;
				pdc.Add(wpd);
			}

			//--�X�^�C��
			Ref::FieldInfo[] f=typeof(mwg.Win32.WS).GetFields(BF_AnyStatic);
			for(int i=0,iM=f.Length;i<iM;i++)
				pdc.Add(new WSPropertyDescriptor("WS_"+f[i].Name,(Win32.WS)f[i].GetValue(null)));

			//--�g���X�^�C��
			f=typeof(mwg.Win32.WS_EX).GetFields(BF_AnyStatic);
			for(int i=0,iM=f.Length;i<iM;i++)
				pdc.Add(new WSXPropertyDescriptor("WS_EX_"+f[i].Name,(Win32.WS_EX)f[i].GetValue(null)));

			return pdc;
		}

		/// <summary>
		/// GetProperties ���\�b�h���������Ă��邩�ǂ������擾���܂��B
		/// </summary>
		/// <param name="context">�R���e�L�X�g�����w�肵�܂��B</param>
		/// <returns>��� true ��Ԃ��܂��B</returns>
		public override bool GetPropertiesSupported(CM::ITypeDescriptorContext context){return true;}

		/// <summary>
		/// ManagedSpy.Window �� mwg.Windows.Window �̃v���p�e�B�����J����ׂ� PropertyDescriptor ���������܂��B
		/// </summary>
		protected class WndPropertyDescriptor:CM::TypeConverter.SimplePropertyDescriptor {
			private System.Reflection.PropertyInfo pi;
			private object[] param;
			/// <summary>
			/// mwg.Windows.Window �̎��v���p�e�B�����J����ׂ̃R���X�g���N�^�ł��B
			/// </summary>
			/// <param name="prop">�v���p�e�B��\�� PropertyInfo ���w�肵�܂��B</param>
			public WndPropertyDescriptor(Ref::PropertyInfo prop):this(prop,new object[]{}){}
			/// <summary>
			/// mwg.Windows.Window �̎��v���p�e�B�����J����ׂ̃R���X�g���N�^�ł��B
			/// </summary>
			/// <param name="prop">�v���p�e�B��\�� PropertyInfo ���w�肵�܂��B</param>
			/// <param name="param">�v���p�e�B�̒l�ɃA�N�Z�X����ۂɗp����������w�肵�܂��B</param>
			public WndPropertyDescriptor(System.Reflection.PropertyInfo prop,object[] param)
				:base(typeof(Window),prop.Name,prop.PropertyType,GetAttributes(prop)) {
				this.pi=prop; this.param=param;
			}
			public override object GetValue(object component) {
				if(!(component is Window)) return null;
				Window w=(Window)component;
				return this.pi.GetValue(w,this.param);
			}
			public override void SetValue(object component,object value) {
				if(!(component is Window)) return;
				Window w=(Window)component;
				this.pi.SetValue(w,value,this.param);
			}
			private static System.Attribute[] GetAttributes(System.Reflection.PropertyInfo prop) {
				System.Attribute[] r=(System.Attribute[])prop.GetCustomAttributes(typeof(System.Attribute),true);
				if(prop.CanWrite) return r;
				System.Attribute[] r2=new System.Attribute[r.Length+1];
				r2[0]=new CM::ReadOnlyAttribute(true);
				r.CopyTo(r2,1);
				return r2;
			}
			//public bool IsBrowsable{get{return !this.Attributes.Contains(CM::BrowsableAttribute.No);}}
		}

		/// <summary>
		/// �E�B���h�E�X�^�C���Ɋւ���v���p�e�B��\�����܂��B
		/// </summary>
		protected sealed class WSPropertyDescriptor:CM::TypeConverter.SimplePropertyDescriptor {
			private Win32.WS style;
			/// <summary>
			/// �Ǎ���p�v���p�e�B�ł��邩�ǂ�����ێ����܂��B
			/// </summary>
			private bool rop=true;
			/// <summary>
			/// WSPropertyDescriptor �̃R���X�g���N�^�ł��B
			/// </summary>
			/// <param name="name">�v���p�e�B�̖��O���w�肵�܂��B</param>
			/// <param name="style">�v���p�e�B�Ɋ֘A�t������X�^�C���̒l���w�肵�܂��B</param>
			public WSPropertyDescriptor(string name,Win32.WS style)
				:base(typeof(Window),name,typeof(bool),new System.Attribute[]{new CM::RefreshPropertiesAttribute(CM::RefreshProperties.All)}){
				this.style=style;
				for(int i=0;i<32;i++)if(0==(~(1<<i)&(int)style)){
					this.rop=false;
					break;
				}
			}
			//===========================================================
			//		���ۃ����o�̎���
			//===========================================================
			/// <summary>
			/// �w�肵���E�B���h�E�̃X�^�C�����ݒ肳��Ă��邩�ۂ����擾���܂��B
			/// </summary>
			/// <param name="component">�擾����Ώۂł���E�B���h�E���w�肵�܂��B</param>
			/// <returns>
			/// ���� PropertyDescriptor ���\������X�^�C�����w�肵���E�B���h�E�������Ă���� true ���A�����łȂ���� false ��Ԃ��܂��B
			/// component �ɃE�B���h�E�łȂ������w�肵���ꍇ�ɂ� null ��Ԃ��܂��B
			/// </returns>
			public override object GetValue(object component) {
				if(component is Window) {
					Window w=(Window)component;
					return 0!=(w.Style&this.style);
				}
				return null;
			}
			/// <summary>
			/// �w�肵���E�B���h�E�ɁA���̃C���X�^���X���\���X�^�C����K�p�E�������܂��B
			/// </summary>
			/// <param name="component">�X�^�C����ύX����E�B���h�E���w�肵�܂��B</param>
			/// <param name="value">�K�p����ꍇ�� true ���A��������ꍇ�� false ���w�肵�܂��B</param>
			public override void SetValue(object component,object value) {
				if(!(value is bool)||this.rop) return;
				if(component is mwg.Windows.Window) {
					Window wnd=(mwg.Windows.Window)component;
					if((bool)value) wnd.Style|=this.style; else wnd.Style&=this.style;
				}
			}
			/// <summary>
			/// �Ǎ���p�v���p�e�B���ǂ������擾���܂��B
			/// </summary>
			public override bool IsReadOnly{get{return this.rop;}}
			//===========================================================
			//		���z�����o�̏㏑��
			//===========================================================
			/// <summary>
			/// �J�e�S����\����������擾���܂��B
			/// </summary>
			public override string Category{get{return "Window �X�^�C��";}}
			//TODO: Description �� xml ����ǂݏo���B
		}
		/// <summary>
		/// �E�B���h�E�g���X�^�C���Ɋւ���v���p�e�B��\�����܂��B
		/// </summary>
		protected sealed class WSXPropertyDescriptor:CM::TypeConverter.SimplePropertyDescriptor {
			private Win32.WS_EX style;
			private bool rop=true;
			/// <summary>
			/// WSPropertyDescriptor �̃R���X�g���N�^�ł��B
			/// </summary>
			/// <param name="name">�v���p�e�B�̖��O���w�肵�܂��B</param>
			/// <param name="style">�v���p�e�B�Ɋ֘A�t������X�^�C���̒l���w�肵�܂��B</param>
			public WSXPropertyDescriptor(string name,Win32.WS_EX style)
				: base(typeof(Window),name,typeof(bool),new System.Attribute[] { new CM::RefreshPropertiesAttribute(CM::RefreshProperties.All) }) {
				this.style=style;
				for(int i=0;i<32;i++) if(0==(~(1<<i)&(int)style)) {
						this.rop=false;
						break;
					}
			}
			//===========================================================
			//		���ۃ����o�̎���
			//===========================================================
			/// <summary>
			/// �w�肵���E�B���h�E�̃X�^�C�����ݒ肳��Ă��邩�ۂ����擾���܂��B
			/// </summary>
			/// <param name="component">�擾����Ώۂł���E�B���h�E���w�肵�܂��B</param>
			/// <returns>
			/// ���� PropertyDescriptor ���\������X�^�C�����w�肵���E�B���h�E�������Ă���� true ���A�����łȂ���� false ��Ԃ��܂��B
			/// component �ɃE�B���h�E�łȂ������w�肵���ꍇ�ɂ� null ��Ԃ��܂��B
			/// </returns>
			public override object GetValue(object component) {
				if(component is mwg.Windows.Window) {
					mwg.Windows.Window wnd=(mwg.Windows.Window)component;
					return 0!=(wnd.ExStyle&this.style);
				}
				return null;
			}
			/// <summary>
			/// �w�肵���E�B���h�E�ɁA���̃C���X�^���X���\���X�^�C����K�p�E�������܂��B
			/// </summary>
			/// <param name="component">�X�^�C����ύX����E�B���h�E���w�肵�܂��B</param>
			/// <param name="value">�K�p����ꍇ�� true ���A��������ꍇ�� false ���w�肵�܂��B</param>
			public override void SetValue(object component,object value) {
				if(!(value is bool)||this.rop) return;
				if(component is mwg.Windows.Window) {
					mwg.Windows.Window wnd=(mwg.Windows.Window)component;
					if((bool)value) wnd.ExStyle|=this.style; else wnd.ExStyle&=this.style;
				}
			}
			/// <summary>
			/// �Ǎ���p�v���p�e�B���ǂ������擾���܂��B
			/// </summary>
			public override bool IsReadOnly { get { return this.rop; } }
			//===========================================================
			//		���z�����o�̏㏑��
			//===========================================================
			/// <summary>
			/// �J�e�S����\����������擾���܂��B
			/// </summary>
			public override string Category { get { return "Window �g���X�^�C��"; } }
			//TODO: Description �� xml ����ǂݏo���B
		}
	}
}