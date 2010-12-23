using CM=System.ComponentModel;
using Ref=System.Reflection;

namespace mwg.Windows{
	internal class WindowConverter:CM::TypeConverter{
		/// <summary>
		/// PropertyDescriptorCollection 情報を取得します。
		/// </summary>
		/// <param name="context">コンテキスト情報を指定します。</param>
		/// <param name="value">プロパティを保持するオブジェクトを指定します。</param>
		/// <param name="attributes">プロパティを選択する上での指定を行う属性を指定します。</param>
		/// <returns>プロパティ群の情報を保持する PropertyDescriptorCollection を返します。</returns>
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

			//--スタイル
			Ref::FieldInfo[] f=typeof(mwg.Win32.WS).GetFields(BF_AnyStatic);
			for(int i=0,iM=f.Length;i<iM;i++)
				pdc.Add(new WSPropertyDescriptor("WS_"+f[i].Name,(Win32.WS)f[i].GetValue(null)));

			//--拡張スタイル
			f=typeof(mwg.Win32.WS_EX).GetFields(BF_AnyStatic);
			for(int i=0,iM=f.Length;i<iM;i++)
				pdc.Add(new WSXPropertyDescriptor("WS_EX_"+f[i].Name,(Win32.WS_EX)f[i].GetValue(null)));

			return pdc;
		}

		/// <summary>
		/// GetProperties メソッドを実装しているかどうかを取得します。
		/// </summary>
		/// <param name="context">コンテキスト情報を指定します。</param>
		/// <returns>常に true を返します。</returns>
		public override bool GetPropertiesSupported(CM::ITypeDescriptorContext context){return true;}

		/// <summary>
		/// ManagedSpy.Window が mwg.Windows.Window のプロパティを公開する為の PropertyDescriptor を実装します。
		/// </summary>
		protected class WndPropertyDescriptor:CM::TypeConverter.SimplePropertyDescriptor {
			private System.Reflection.PropertyInfo pi;
			private object[] param;
			/// <summary>
			/// mwg.Windows.Window の持つプロパティを公開する為のコンストラクタです。
			/// </summary>
			/// <param name="prop">プロパティを表す PropertyInfo を指定します。</param>
			public WndPropertyDescriptor(Ref::PropertyInfo prop):this(prop,new object[]{}){}
			/// <summary>
			/// mwg.Windows.Window の持つプロパティを公開する為のコンストラクタです。
			/// </summary>
			/// <param name="prop">プロパティを表す PropertyInfo を指定します。</param>
			/// <param name="param">プロパティの値にアクセスする際に用いる引数を指定します。</param>
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
		/// ウィンドウスタイルに関するプロパティを表現します。
		/// </summary>
		protected sealed class WSPropertyDescriptor:CM::TypeConverter.SimplePropertyDescriptor {
			private Win32.WS style;
			/// <summary>
			/// 読込専用プロパティであるかどうかを保持します。
			/// </summary>
			private bool rop=true;
			/// <summary>
			/// WSPropertyDescriptor のコンストラクタです。
			/// </summary>
			/// <param name="name">プロパティの名前を指定します。</param>
			/// <param name="style">プロパティに関連付けられるスタイルの値を指定します。</param>
			public WSPropertyDescriptor(string name,Win32.WS style)
				:base(typeof(Window),name,typeof(bool),new System.Attribute[]{new CM::RefreshPropertiesAttribute(CM::RefreshProperties.All)}){
				this.style=style;
				for(int i=0;i<32;i++)if(0==(~(1<<i)&(int)style)){
					this.rop=false;
					break;
				}
			}
			//===========================================================
			//		抽象メンバの実装
			//===========================================================
			/// <summary>
			/// 指定したウィンドウのスタイルが設定されているか否かを取得します。
			/// </summary>
			/// <param name="component">取得する対象であるウィンドウを指定します。</param>
			/// <returns>
			/// この PropertyDescriptor が表現するスタイルを指定したウィンドウが持っていれば true を、そうでなければ false を返します。
			/// component にウィンドウでない物を指定した場合には null を返します。
			/// </returns>
			public override object GetValue(object component) {
				if(component is Window) {
					Window w=(Window)component;
					return 0!=(w.Style&this.style);
				}
				return null;
			}
			/// <summary>
			/// 指定したウィンドウに、このインスタンスが表すスタイルを適用・解除します。
			/// </summary>
			/// <param name="component">スタイルを変更するウィンドウを指定します。</param>
			/// <param name="value">適用する場合に true を、解除する場合に false を指定します。</param>
			public override void SetValue(object component,object value) {
				if(!(value is bool)||this.rop) return;
				if(component is mwg.Windows.Window) {
					Window wnd=(mwg.Windows.Window)component;
					if((bool)value) wnd.Style|=this.style; else wnd.Style&=this.style;
				}
			}
			/// <summary>
			/// 読込専用プロパティかどうかを取得します。
			/// </summary>
			public override bool IsReadOnly{get{return this.rop;}}
			//===========================================================
			//		仮想メンバの上書き
			//===========================================================
			/// <summary>
			/// カテゴリを表す文字列を取得します。
			/// </summary>
			public override string Category{get{return "Window スタイル";}}
			//TODO: Description を xml から読み出す。
		}
		/// <summary>
		/// ウィンドウ拡張スタイルに関するプロパティを表現します。
		/// </summary>
		protected sealed class WSXPropertyDescriptor:CM::TypeConverter.SimplePropertyDescriptor {
			private Win32.WS_EX style;
			private bool rop=true;
			/// <summary>
			/// WSPropertyDescriptor のコンストラクタです。
			/// </summary>
			/// <param name="name">プロパティの名前を指定します。</param>
			/// <param name="style">プロパティに関連付けられるスタイルの値を指定します。</param>
			public WSXPropertyDescriptor(string name,Win32.WS_EX style)
				: base(typeof(Window),name,typeof(bool),new System.Attribute[] { new CM::RefreshPropertiesAttribute(CM::RefreshProperties.All) }) {
				this.style=style;
				for(int i=0;i<32;i++) if(0==(~(1<<i)&(int)style)) {
						this.rop=false;
						break;
					}
			}
			//===========================================================
			//		抽象メンバの実装
			//===========================================================
			/// <summary>
			/// 指定したウィンドウのスタイルが設定されているか否かを取得します。
			/// </summary>
			/// <param name="component">取得する対象であるウィンドウを指定します。</param>
			/// <returns>
			/// この PropertyDescriptor が表現するスタイルを指定したウィンドウが持っていれば true を、そうでなければ false を返します。
			/// component にウィンドウでない物を指定した場合には null を返します。
			/// </returns>
			public override object GetValue(object component) {
				if(component is mwg.Windows.Window) {
					mwg.Windows.Window wnd=(mwg.Windows.Window)component;
					return 0!=(wnd.ExStyle&this.style);
				}
				return null;
			}
			/// <summary>
			/// 指定したウィンドウに、このインスタンスが表すスタイルを適用・解除します。
			/// </summary>
			/// <param name="component">スタイルを変更するウィンドウを指定します。</param>
			/// <param name="value">適用する場合に true を、解除する場合に false を指定します。</param>
			public override void SetValue(object component,object value) {
				if(!(value is bool)||this.rop) return;
				if(component is mwg.Windows.Window) {
					mwg.Windows.Window wnd=(mwg.Windows.Window)component;
					if((bool)value) wnd.ExStyle|=this.style; else wnd.ExStyle&=this.style;
				}
			}
			/// <summary>
			/// 読込専用プロパティかどうかを取得します。
			/// </summary>
			public override bool IsReadOnly { get { return this.rop; } }
			//===========================================================
			//		仮想メンバの上書き
			//===========================================================
			/// <summary>
			/// カテゴリを表す文字列を取得します。
			/// </summary>
			public override string Category { get { return "Window 拡張スタイル"; } }
			//TODO: Description を xml から読み出す。
		}
	}
}