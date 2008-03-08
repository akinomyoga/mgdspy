using System;
using Serial=System.Runtime.Serialization;
using BinaryFormatter=System.Runtime.Serialization.Formatters.Binary.BinaryFormatter;
using Ref=System.Reflection;

namespace SerialTest{
	//
	//	分かった事
	//    System.Type は Surrogate を追加してもその Surrogate を使わずにシリアル化される?
	//    →内部で継承されている為か?
	//    →System.RuntimeType を追加したら無事に成功した
	//
	public class SerializationSurrogate{
		public static void test(){
			Serial::SurrogateSelector ss=new System.Runtime.Serialization.SurrogateSelector();
			Serial::StreamingContext sc=new System.Runtime.Serialization.StreamingContext();
			ss.AddSurrogate(typeof(System.Type),sc,new CustomSurrogate());
			ss.AddSurrogate(typeof(System.Type).Assembly.GetType("System.RuntimeType"),sc,new CustomSurrogate());
			ss.AddSurrogate(typeof(XXX),sc,new MySurrogate());
			ss.AddSurrogate(MemberInfoSHSurrogate.holderType,sc,new MemberInfoSHSurrogate());

			BinaryFormatter binf=new BinaryFormatter(ss,sc);
			binf.Binder=new CustomBinder();

			// surrogate selector を確認
			/*
			Serial::ISurrogateSelector iss=binf.SurrogateSelector;
			if(iss==null)
				Console.WriteLine("<iss is null>");
			else 
				Console.WriteLine(iss.GetType().ToString());
			//*/

			System.Action<object> ser_des=delegate(object value){
				System.IO.Stream mstr=new System.IO.MemoryStream();
				Console.WriteLine("--------------------------------");
				Console.WriteLine("{0} を Serialize します",value.GetType());
				binf.Serialize(mstr,value);
				Console.WriteLine();

				// 逆シリアル化
				Console.WriteLine("Deserialize の結果:");
				mstr.Position=0;
				Console.WriteLine(binf.Deserialize(mstr).ToString());
				Console.WriteLine();
				mstr.Close();
			};

			//ser_des("今日はこれは実験です。");
			//ser_des(typeof(System.AppDomain));

			//XXX val=new XXX();val.name="あいう";val.age=100;
			//ser_des(val);

			//ser_des(new NestedBindTarget());

			ser_des(typeof(Math).GetMethod("IEEERemainder"));

			Console.ReadLine();
		}
	}
	//===============================================================
	//		SurrogateSelector
	//===============================================================
	public class CustomSurrogate:Serial::ISerializationSurrogate{
		public CustomSurrogate(){}
		#region ISerializationSurrogate メンバ

		public void GetObjectData(object obj,Serial::SerializationInfo info,Serial::StreamingContext context) {
			if(obj==null)throw new System.ArgumentNullException("obj");
			System.Type type=obj as System.Type;
			if(obj==null)throw new System.InvalidCastException("引数に指定した obj は System.Type 型ではありません。");

			Console.WriteLine("<CustomSurrogate が System.Type をシリアル化します。>");
			info.AddValue("asm-loc",type.Assembly.Location);
			//info.AddValue("name",type.FullName);
			info.AddValue("name","System.Guid");
		}

		public object SetObjectData(object obj,Serial::SerializationInfo info,Serial::StreamingContext context,Serial::ISurrogateSelector selector){
			Console.WriteLine("<CustomSurrogate が System.Type を逆シリアル化します。>");
			string asmLocation=info.GetString("asm-loc");
			string typename=info.GetString("name");
			return System.Reflection.Assembly.LoadFrom(asmLocation).GetType(typename);
		}

		#endregion
	}
	[System.Serializable]
	public struct XXX{
		public string name;
		public int age;

		public override string ToString() {
			return "私の名前は "+name+" です。年は "+age.ToString()+" です。";
		}
	}
	public class MySurrogate:Serial::ISerializationSurrogate{
		public MySurrogate(){}
		#region ISerializationSurrogate メンバ
		public void GetObjectData(object obj,Serial::SerializationInfo info,Serial::StreamingContext context) {
			if(obj==null)throw new System.ArgumentNullException("obj");
			if(!(obj is XXX)) throw new System.InvalidCastException("引数に指定した obj は XXX 型ではありません。");
			XXX value=(XXX)obj;

			Console.WriteLine("<CustomSurrogate が XXX をシリアル化します。>");
			info.AddValue("name",value.name);
			info.AddValue("age",value.age);
		}

		public object SetObjectData(object obj,Serial::SerializationInfo info,Serial::StreamingContext context,Serial::ISurrogateSelector selector){
			Console.WriteLine("<CustomSurrogate が XXX を逆シリアル化します。>");
			XXX ret=new XXX();
			ret.name=info.GetString("name");
			ret.age=info.GetInt32("age")-1;
			return ret;
		}

		#endregion
	}
	public class MemberInfoSHSurrogate:Serial::ISerializationSurrogate{
		public static System.Type holderType;
		static Ref::ConstructorInfo holderCtor;
		static Ref::FieldInfo m_reflectedType;		// System.Type

		static Ref::FieldInfo info_m_members;		// string[]
		static Ref::FieldInfo info_m_currMember;	// int
		static Ref::MethodInfo info_AddValue;		// void		SerializationInfo.AddValue(string,object,Type,int);

		static MemberInfoSHSurrogate(){
			const Ref::BindingFlags BF=Ref::BindingFlags.NonPublic|Ref::BindingFlags.Instance|Ref::BindingFlags.Static;
			holderType=typeof(System.Math).Assembly.GetType("System.Reflection.MemberInfoSerializationHolder");
			if(holderType==null)goto fail;

			holderCtor=holderType.GetConstructor(BF,null,new System.Type[]{typeof(Serial::SerializationInfo),typeof(Serial::StreamingContext)},null);
			if(holderCtor==null)goto fail;

			m_reflectedType=holderType.GetField("m_reflectedType",BF);
			if(m_reflectedType==null)goto fail;
			
			// serialization infos
			System.Type tSerializationInfo=typeof(Serial::SerializationInfo);
			info_m_currMember=tSerializationInfo.GetField("m_currMember",BF);
			info_m_members=tSerializationInfo.GetField("m_members",BF);
			info_AddValue=tSerializationInfo.GetMethod(
				"AddValue",BF,null,
				new System.Type[]{typeof(string),typeof(object),typeof(Type),typeof(int)},
				null);
			if(info_m_currMember==null||info_m_members==null||info_AddValue==null)goto fail;

			return;
		fail:
			throw new System.Exception("!!! Surrogate_MemberInfoHolder の 初期化に失敗しました !!!");
		}

		#region ISerializationSurrogate メンバ
		public void GetObjectData(object obj,System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
			throw new System.NotImplementedException();
		}

		public object SetObjectData(object obj,System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context,System.Runtime.Serialization.ISurrogateSelector selector) {
			string assemblyName=info.GetString("AssemblyName");
			string className=info.GetString("ClassName");
			System.Type reflectedType=System.Type.GetType(String.Format("{0}, {1}",className,assemblyName));

			// SetDummy
			InfoDataOverwrite(info,"AssemblyName",typeof(System.Math).Assembly.FullName);
			InfoDataOverwrite(info,"ClassName",typeof(System.Math).FullName);

			// construct
    		object holder=holderCtor.Invoke(new object[]{info,context});
			m_reflectedType.SetValue(holder,reflectedType);

			Console.WriteLine("<Surrogate_MemberInfoHolder が Holder を構築しました>");
			return holder;
		}

		public static void InfoDataOverwrite(Serial::SerializationInfo info,string name,string value){
			if(info==null||name==null||value==null)
				throw new System.ArgumentNullException();

			int m_currMember=(int)info_m_currMember.GetValue(info);
			string[] m_members=(string[])info_m_members.GetValue(info);
			for(int i=0;i<m_currMember;i++){
				if(m_members[i]!=name)continue;

				info_AddValue.Invoke(info,new object[]{name,value,typeof(string),i});
				return;
			}

			// 元々なかった場合
			info.AddValue(name,value,typeof(string));
		}
		#endregion
	}
	//===============================================================
	//		Binder
	//===============================================================
	[System.Serializable]
	public class NestedBindTarget{
		public System.Type t1=typeof(System.Guid);
		public System.Type t2=typeof(System.IntPtr);
	}
	public sealed class CustomBinder:Serial::SerializationBinder {
		public override Type BindToType(string assemblyName,string typeName) {
			Type typeToDeserialize=Type.GetType(String.Format("{0}, {1}",typeName,assemblyName));
			Console.WriteLine("<CustomBinder が型 {0} に決定しました>",typeToDeserialize);
			return typeToDeserialize;
		}
	}

}