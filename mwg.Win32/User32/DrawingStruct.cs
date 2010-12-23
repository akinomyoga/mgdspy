using Interop=System.Runtime.InteropServices;
using CM=System.ComponentModel;
namespace mwg.Win32{
	/// <summary>
	/// 画面上の座標などを指定するのに使用する構造体です。
	/// </summary>
	[System.Serializable]
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public struct POINT{
		/// <summary>
		/// X 座標の位置を表します。
		/// </summary>
		public int x;
		/// <summary>
		/// Y 座標の位置を表します。
		/// </summary>
		public int y;

		/// <summary>
		/// X 座標を設定亦は取得します。
		/// </summary>
		public int X{get{return this.x;}set{this.x=value;}}
		/// <summary>
		/// Y 座標を設定亦は取得します。
		/// </summary>
		public int Y{get{return this.y;}set{this.y=value;}}
		/// <summary>
		/// POINT のコンストラクタです。
		/// </summary>
		/// <param name="x">X 座標を指定して下さい。</param>
		/// <param name="y">Y 座標を指定して下さい。</param>
		public POINT(int x,int y){
			this.x = x;
			this.y = y;
		}
		/// <summary>
		/// POINT を <see cref="System.Drawing.Point"/> に変換します。
		/// </summary>
		/// <param name="p">変換元の座標を指定します。</param>
		/// <returns>変換後の座標を返します。</returns>
		public static implicit operator System.Drawing.Point(POINT p){
			return new System.Drawing.Point(p.X,p.Y);
		}
		/// <summary>
		/// <see cref="System.Drawing.Point"/> から POINT に変換します。
		/// </summary>
		/// <param name="p">変換元の座標を指定します。</param>
		/// <returns>変換後の座標を返します。</returns>
		public static implicit operator POINT(System.Drawing.Point p){
			return new POINT(p.X,p.Y);
		}
	}
	[System.Serializable]
	[Interop.StructLayout(Interop.LayoutKind.Sequential,CharSet=Interop.CharSet.Auto)]   
	public struct LOGFONT{   
		public int lfHeight;
		public int lfWidth;
		public int lfEscapement;
		public int lfOrientation;
		public int lfWeight;
		public byte lfItalic;
		public byte lfUnderline;
		public byte lfStrikeOut;
		public byte lfCharSet;
		public byte lfOutPrecision;
		public byte lfClipPrecision;
		public byte lfQuality;
		public byte lfPitchAndFamily;
		[Interop.MarshalAs(Interop.UnmanagedType.ByValArray, SizeConst=32/*LF_FACESIZE*/*2)]
		public byte[] lfFaceName;
		/// <summary>
		/// LOGFONT から System.Drawing.Font への暗黙の変換をサポートします。
		/// </summary>
		/// <param name="font">変換元のフォントを指定します。</param>
		/// <returns>変換後の System.Drawing.Font を返します。</returns>
		public static implicit operator System.Drawing.Font(LOGFONT font){
			return System.Drawing.Font.FromLogFont(font);
		}
	}
	/// <summary>
	/// 矩形領域を表現する構造体です。
	/// </summary>
	[System.Serializable]
	[CM.TypeConverter(typeof(RECTConverter))]
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public struct RECT{
		/// <summary>
		/// 矩形領域の左端の X 座標を保持します。
		/// </summary>
		public int left;
		/// <summary>
		/// 矩形領域の上端の Y 座標を保持します。
		/// </summary>
		public int top;
		/// <summary>
		/// 矩形領域の右端の X 座標を保持します。
		/// </summary>
		public int right;
		/// <summary>
		/// 矩形領域の下端の Y 座標を保持します。
		/// </summary>
		public int bottom;
		/// <summary>
		/// RECT のコンストラクタです。
		/// </summary>
		/// <param name="left">矩形領域の左端の X 座標を指定します。</param>
		/// <param name="top">矩形領域の上端の Y 座標を指定します。</param>
		/// <param name="right">矩形領域の右端の X 座標を指定します。</param>
		/// <param name="bottom">矩形領域の下端の Y 座標を指定します。</param>
		public RECT(int left,int top,int right,int bottom){
			this.left=left;
			this.top=top;
			this.right=right;
			this.bottom=bottom;
		}
		//--properties
		/// <summary>
		/// 矩形領域の左端の座標を取得亦は設定します。
		/// </summary>
		[CM.Description("矩形領域の左端の座標を取得亦は設定します。")]
		public int Left{get{return this.left;}set{this.left=value;}}
		/// <summary>
		/// 矩形領域の上端の座標を取得亦は設定します。
		/// </summary>
		[CM.Description("矩形領域の上端の座標を取得亦は設定します。")]
		public int Top{get{return this.top;}set{this.top=value;}}
		/// <summary>
		/// 矩形領域の右端の座標を取得亦は設定します。
		/// </summary>
		[CM.Description("矩形領域の右端の座標を取得亦は設定します。")]
		public int Right{get{return this.right;}set{this.right=value;}}
		/// <summary>
		/// 矩形領域の下端の座標を取得亦は設定します。
		/// </summary>
		[CM.Description("矩形領域の下端の座標を取得亦は設定します。")]
		public int Bottom{get{return this.bottom;}set{this.bottom=value;}}
		//--operators
		public static implicit operator RECT(System.Drawing.Rectangle rect){
			return new RECT(rect.Left,rect.Top,rect.Right,rect.Bottom);
		}
		public static implicit operator System.Drawing.Rectangle(RECT rect){
			return new System.Drawing.Rectangle(rect.Left,rect.Top,rect.Right-rect.Left,rect.Bottom-rect.Top);
		}
	}
	/// <summary>
	/// RECT 型と他の型の間の変換を実装します。
	/// </summary>
	public class RECTConverter:CM.TypeConverter{
		/// <summary>
		/// 矩形領域 RECT が他の型から変換して売る事が出来るか否かを取得します。
		/// </summary>
		/// <param name="context">変換の際のコンテクストを指定します。</param>
		/// <param name="sourceType">変換元の型を指定します。</param>
		/// <returns>指定した型から変換する事が出来る際に true を返します。</returns>
		public override bool CanConvertFrom(CM.ITypeDescriptorContext context,System.Type sourceType){
			return sourceType==typeof(string)||base.CanConvertFrom(context,sourceType);
		}
		public override bool CanConvertTo(CM.ITypeDescriptorContext context,System.Type destinationType){
			return destinationType==typeof(CM.Design.Serialization.InstanceDescriptor)||base.CanConvertTo(context,destinationType);
		}

		public override object ConvertFrom(CM.ITypeDescriptorContext context,System.Globalization.CultureInfo culture, object value){
			if(!(value is string)){
				return base.ConvertFrom(context,culture,value);
			}
			string text = ((string)value).Trim();
			if(text.Length==0)return null;
			if(culture==null)culture=System.Globalization.CultureInfo.CurrentCulture;
			char ch=culture.TextInfo.ListSeparator[0];
			string[] textArray=text.Split(new char[]{ch});
			int[] numArray = new int[textArray.Length];
			CM.TypeConverter converter = CM.TypeDescriptor.GetConverter(typeof(int));
			for (int i=0;i<numArray.Length;i++){
				numArray[i]=(int)converter.ConvertFromString(context, culture, textArray[i]);
			}
			if(numArray.Length!=4)throw new System.ArgumentException("value");
			return new RECT(numArray[0],numArray[1],numArray[2],numArray[3]);
		}

		public override object ConvertTo(CM.ITypeDescriptorContext context,System.Globalization.CultureInfo culture, object value,System.Type destinationType){
			if(destinationType==null){
				throw new System.ArgumentNullException("destinationType");
			}
			if(destinationType==typeof(string)&&value is RECT){
				RECT rect =(RECT)value;
				if(culture==null){
					culture=System.Globalization.CultureInfo.CurrentCulture;
				}
				string separator = culture.TextInfo.ListSeparator + " ";
				CM.TypeConverter converter = CM.TypeDescriptor.GetConverter(typeof(int));
				string[] textArray = new string[4];
				int num=0;
				textArray[num++]=converter.ConvertToString(context, culture, rect.Left);
				textArray[num++]=converter.ConvertToString(context, culture, rect.Top);
				textArray[num++]=converter.ConvertToString(context, culture, rect.Right);
				textArray[num++]=converter.ConvertToString(context, culture, rect.Bottom);
				return string.Join(separator,textArray);
			}
			if(destinationType==typeof(CM.Design.Serialization.InstanceDescriptor)&&value is RECT){
				RECT rect2=(RECT)value;
				System.Reflection.ConstructorInfo member
					=typeof(RECT).GetConstructor(new System.Type[]{typeof(int),typeof(int),typeof(int),typeof(int)});
				if(member!=null){
					return new CM.Design.Serialization.InstanceDescriptor(
						member,new object[]{rect2.Left,rect2.Top,rect2.Right,rect2.Bottom}
					);
				}
			}
			return base.ConvertTo(context,culture,value,destinationType);
		}

		public override object CreateInstance(CM.ITypeDescriptorContext context,System.Collections.IDictionary propertyValues){
			return new RECT((int)propertyValues["Left"],(int)propertyValues["Top"],(int)propertyValues["Right"],(int)propertyValues["Bottom"]);
		}
		public override bool GetCreateInstanceSupported(CM.ITypeDescriptorContext context){return true;}
		/// <summary>
		/// PropertyDescriptorCollection 情報を取得します。
		/// </summary>
		/// <param name="context">コンテキスト情報を指定します。</param>
		/// <param name="value">プロパティを保持するオブジェクトを指定します。</param>
		/// <param name="attributes">プロパティを選択する上での指定を行う属性を指定します。</param>
		/// <returns>プロパティ群の情報を保持する PropertyDescriptorCollection を返します。</returns>
		public override CM.PropertyDescriptorCollection GetProperties(CM.ITypeDescriptorContext context,object value,System.Attribute[] attributes){
			return CM.TypeDescriptor.GetProperties(typeof(RECT),attributes).Sort(new string[]{"Left","Top","Right","Bottom"});
		}
		/// <summary>
		/// GetProperties メソッドを実装しているかどうかを取得します。
		/// </summary>
		/// <param name="context">コンテキスト情報を指定します。</param>
		/// <returns>常に true を返します。</returns>
		public override bool GetPropertiesSupported(CM.ITypeDescriptorContext context){return true;}
	}
}