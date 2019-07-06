using Interop=System.Runtime.InteropServices;
using CM=System.ComponentModel;
namespace mwg.Win32{
	/// <summary>
	/// ��ʏ�̍��W�Ȃǂ��w�肷��̂Ɏg�p����\���̂ł��B
	/// </summary>
	[System.Serializable]
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public struct POINT{
		/// <summary>
		/// X ���W�̈ʒu��\���܂��B
		/// </summary>
		public int x;
		/// <summary>
		/// Y ���W�̈ʒu��\���܂��B
		/// </summary>
		public int y;

		/// <summary>
		/// X ���W��ݒ薒�͎擾���܂��B
		/// </summary>
		public int X{get{return this.x;}set{this.x=value;}}
		/// <summary>
		/// Y ���W��ݒ薒�͎擾���܂��B
		/// </summary>
		public int Y{get{return this.y;}set{this.y=value;}}
		/// <summary>
		/// POINT �̃R���X�g���N�^�ł��B
		/// </summary>
		/// <param name="x">X ���W���w�肵�ĉ������B</param>
		/// <param name="y">Y ���W���w�肵�ĉ������B</param>
		public POINT(int x,int y){
			this.x = x;
			this.y = y;
		}
		/// <summary>
		/// POINT �� <see cref="System.Drawing.Point"/> �ɕϊ����܂��B
		/// </summary>
		/// <param name="p">�ϊ����̍��W���w�肵�܂��B</param>
		/// <returns>�ϊ���̍��W��Ԃ��܂��B</returns>
		public static implicit operator System.Drawing.Point(POINT p){
			return new System.Drawing.Point(p.X,p.Y);
		}
		/// <summary>
		/// <see cref="System.Drawing.Point"/> ���� POINT �ɕϊ����܂��B
		/// </summary>
		/// <param name="p">�ϊ����̍��W���w�肵�܂��B</param>
		/// <returns>�ϊ���̍��W��Ԃ��܂��B</returns>
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
		/// LOGFONT ���� System.Drawing.Font �ւ̈Öق̕ϊ����T�|�[�g���܂��B
		/// </summary>
		/// <param name="font">�ϊ����̃t�H���g���w�肵�܂��B</param>
		/// <returns>�ϊ���� System.Drawing.Font ��Ԃ��܂��B</returns>
		public static implicit operator System.Drawing.Font(LOGFONT font){
			return System.Drawing.Font.FromLogFont(font);
		}
	}
	/// <summary>
	/// ��`�̈��\������\���̂ł��B
	/// </summary>
	[System.Serializable]
	[CM.TypeConverter(typeof(RECTConverter))]
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public struct RECT{
		/// <summary>
		/// ��`�̈�̍��[�� X ���W��ێ����܂��B
		/// </summary>
		public int left;
		/// <summary>
		/// ��`�̈�̏�[�� Y ���W��ێ����܂��B
		/// </summary>
		public int top;
		/// <summary>
		/// ��`�̈�̉E�[�� X ���W��ێ����܂��B
		/// </summary>
		public int right;
		/// <summary>
		/// ��`�̈�̉��[�� Y ���W��ێ����܂��B
		/// </summary>
		public int bottom;
		/// <summary>
		/// RECT �̃R���X�g���N�^�ł��B
		/// </summary>
		/// <param name="left">��`�̈�̍��[�� X ���W���w�肵�܂��B</param>
		/// <param name="top">��`�̈�̏�[�� Y ���W���w�肵�܂��B</param>
		/// <param name="right">��`�̈�̉E�[�� X ���W���w�肵�܂��B</param>
		/// <param name="bottom">��`�̈�̉��[�� Y ���W���w�肵�܂��B</param>
		public RECT(int left,int top,int right,int bottom){
			this.left=left;
			this.top=top;
			this.right=right;
			this.bottom=bottom;
		}
		//--properties
		/// <summary>
		/// ��`�̈�̍��[�̍��W���擾���͐ݒ肵�܂��B
		/// </summary>
		[CM.Description("��`�̈�̍��[�̍��W���擾���͐ݒ肵�܂��B")]
		public int Left{get{return this.left;}set{this.left=value;}}
		/// <summary>
		/// ��`�̈�̏�[�̍��W���擾���͐ݒ肵�܂��B
		/// </summary>
		[CM.Description("��`�̈�̏�[�̍��W���擾���͐ݒ肵�܂��B")]
		public int Top{get{return this.top;}set{this.top=value;}}
		/// <summary>
		/// ��`�̈�̉E�[�̍��W���擾���͐ݒ肵�܂��B
		/// </summary>
		[CM.Description("��`�̈�̉E�[�̍��W���擾���͐ݒ肵�܂��B")]
		public int Right{get{return this.right;}set{this.right=value;}}
		/// <summary>
		/// ��`�̈�̉��[�̍��W���擾���͐ݒ肵�܂��B
		/// </summary>
		[CM.Description("��`�̈�̉��[�̍��W���擾���͐ݒ肵�܂��B")]
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
	/// RECT �^�Ƒ��̌^�̊Ԃ̕ϊ����������܂��B
	/// </summary>
	public class RECTConverter:CM.TypeConverter{
		/// <summary>
		/// ��`�̈� RECT �����̌^����ϊ����Ĕ��鎖���o���邩�ۂ����擾���܂��B
		/// </summary>
		/// <param name="context">�ϊ��̍ۂ̃R���e�N�X�g���w�肵�܂��B</param>
		/// <param name="sourceType">�ϊ����̌^���w�肵�܂��B</param>
		/// <returns>�w�肵���^����ϊ����鎖���o����ۂ� true ��Ԃ��܂��B</returns>
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
		/// PropertyDescriptorCollection �����擾���܂��B
		/// </summary>
		/// <param name="context">�R���e�L�X�g�����w�肵�܂��B</param>
		/// <param name="value">�v���p�e�B��ێ�����I�u�W�F�N�g���w�肵�܂��B</param>
		/// <param name="attributes">�v���p�e�B��I�������ł̎w����s���������w�肵�܂��B</param>
		/// <returns>�v���p�e�B�Q�̏���ێ����� PropertyDescriptorCollection ��Ԃ��܂��B</returns>
		public override CM.PropertyDescriptorCollection GetProperties(CM.ITypeDescriptorContext context,object value,System.Attribute[] attributes){
			return CM.TypeDescriptor.GetProperties(typeof(RECT),attributes).Sort(new string[]{"Left","Top","Right","Bottom"});
		}
		/// <summary>
		/// GetProperties ���\�b�h���������Ă��邩�ǂ������擾���܂��B
		/// </summary>
		/// <param name="context">�R���e�L�X�g�����w�肵�܂��B</param>
		/// <returns>��� true ��Ԃ��܂��B</returns>
		public override bool GetPropertiesSupported(CM.ITypeDescriptorContext context){return true;}
	}
}