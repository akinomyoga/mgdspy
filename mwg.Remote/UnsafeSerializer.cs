using System;
using System.Text;

using Ser=System.Runtime.Serialization;
using Ref=System.Reflection;
using Gen=System.Collections.Generic;

using Compilers=System.Runtime.CompilerServices;

namespace mwg.Remote{
	internal static class TypeUtils{
		//==========================================================================
		//	Utils
		//--------------------------------------------------------------------------
		internal static bool HasCustomAttribute(this System.Type type,System.Type attributeType){
			return 0!=type.GetCustomAttributes(attributeType,false).Length;
		}
	}

	public static class UnsafeSerializer{
		static Formatter instance=new Formatter();

		public static byte[] Serialize(object graph){
			lock(instance)
				return instance.Serialize(graph);
		}
		public static object Deserialize(byte[] data){
			lock(instance)
				return instance.Deserialize(data);
		}
		public static void Serialize(System.IO.Stream stream,object graph){
			lock(instance)
				instance.Serialize(stream,graph);
		}
		public static object Deserialize(System.IO.Stream stream){
			lock(instance)
				return instance.Deserialize(stream);
		}

		//==========================================================================
		public class Formatter{
			Ser::Formatters.Binary.BinaryFormatter binf;
			public Formatter(){
				Ser::SurrogateSelector sur_sel=new UnsafeSurrogateSelector();
				Ser::StreamingContext sc=new Ser::StreamingContext();
				this.binf=new Ser::Formatters.Binary.BinaryFormatter(sur_sel,sc);
				this.binf.Binder=new RemoteTypeBinder();
			}

			/// <summary>
			/// オブジェクトをバイト配列に変換します。
			/// </summary>
			/// <param name="graph">シリアル化するオブジェクトを指定します。</param>
			/// <returns>変換結果を格納したバイト配列を返します。</returns>
			public byte[] Serialize(object graph){
				using(System.IO.MemoryStream mstr=new System.IO.MemoryStream()){
					Serialize(mstr,graph);
					return mstr.ToArray();
				}
			}
			/// <summary>
			/// オブジェクトをストリームに書き込みます。
			/// </summary>
			/// <param name="stream">データの格納先のストリームを指定します。</param>
			/// <param name="graph">シリアル化するオブジェクトを指定します。</param>
			public void Serialize(System.IO.Stream stream,object graph){
				if(graph==null){
					graph=new IsNull();
				}
				this.binf.Serialize(stream,graph);
			}
			/// <summary>
			/// バイト配列からオブジェクトを復元します。
			/// </summary>
			/// <param name="data">オブジェクトをシリアル化した結果を格納してある配列を指定します。</param>
			/// <returns>配列のデータから復元したオブジェクトを返します。</returns>
			public object Deserialize(byte[] data){
				using(System.IO.MemoryStream mstr=new System.IO.MemoryStream(data))
					return Deserialize(mstr);
			}
			/// <summary>
			/// ストリームからオブジェクトを読み取ります。
			/// </summary>
			/// <param name="stream">シリアル化されたオブジェクトの情報が格納されているストリームを指定します。</param>
			/// <returns>復元されたオブジェクトを返します。</returns>
			public object Deserialize(System.IO.Stream stream){
				object ret=this.binf.Deserialize(stream);

//			if(ret->ToString()->IndexOf("IsNull")>=0){
//				::MessageBox(NULL,_T("IsNull が含まれている情報です"),NULL,NULL);
//				Frms::MessageBox::Show(ret->GetType()->FullName);
//				Frms::MessageBox::Show(IsNull::typeid->FullName);
//				Frms::MessageBox::Show((ret::typeid==IsNull::typeid).ToString());
//			}

				if(ret.GetType()==typeof(IsNull))ret=null;
				return ret;
			}
		}
		//--------------------------------------------------------------------------

		[System.Serializable]
		internal struct IsNull{};

		//==========================================================================
		//		class RemoteTypeBinder (System.Type Binder)
		//--------------------------------------------------------------------------
		/// <summary>
		/// 遠くの Assembly の中の型のインスタンスも逆シリアル化出来るようにする為の Binder です。
		/// </summary>
		internal sealed class RemoteTypeBinder:Ser::SerializationBinder{
			public RemoteTypeBinder(){}
			public override System.Type BindToType(string assemblyName,string typeName){
				return AssemblyStore.GetTypeFromName(assemblyName,typeName);
			}
		}
	}

	//==========================================================================
	//		class AssemblyStore
	//			Assembly 解決を実行します。
	//--------------------------------------------------------------------------
	/// <summary>
	/// 遠くの Assembly を溜めておきます。
	/// </summary>
	public static class AssemblyStore{
		static Gen::Dictionary<string,Ref::Assembly> loc2asm
			=new Gen::Dictionary<string,Ref::Assembly>();
		static Gen::Dictionary<string,Ref::Assembly> name2asm
			=new Gen::Dictionary<string,Ref::Assembly>();
		static AssemblyStore(){
			// mgdspy_hk を登録
			RegisterAssembly(typeof(AssemblyStore).Assembly);

			// mgdspy_hk も含めて全て登録
			//for each(Ref::Assembly^ assem in System::AppDomain::GetAssemblies()){
			//	RegisterAssembly(assem);
			//}

			System.AppDomain.CurrentDomain.AssemblyResolve+=delegate(object sender,ResolveEventArgs args){
				return GetAssemblyFromName(args.Name);
			};
		}

		public static void RegisterAssembly(Ref::Assembly assem){
			lock(loc2asm)
				loc2asm[assem.Location]=assem;
			lock(name2asm)
				name2asm[assem.FullName]=assem;

			// ↓効果無し
//				static Gen::List<string^>^ private_paths=new Gen::List<string^>();
//
//				string^ priv=System::IO::Path::GetDirectoryName(assem->Location);
//				if(!privPaths->Contains(priv)){
//					System::AppDomain::CurrentDomain->AppendPrivatePath(priv);
//					private_paths->Add(priv);
//				}

		}
		public static Ref::Assembly GetAssemblyFromLocation(string location){
			lock(loc2asm){
				Ref::Assembly assem;
				if(loc2asm.TryGetValue(location,out assem)){
					return assem;
				}else{
					assem=Ref::Assembly.LoadFrom(location);
					RegisterAssembly(assem);
					return assem;
				}
			}
		}

		public static Ref::Assembly GetAssemblyFromName(string assemblyName){
			lock(name2asm){
				Ref::Assembly assem;
				if(name2asm.TryGetValue(assemblyName,out assem)){
					return assem;
				}else{
					return null;
				}
			}
		}

		public static System.Type GetTypeFromName(string assemblyName,string typeName){
			Ref::Assembly assem=AssemblyStore.GetAssemblyFromName(assemblyName);
			//Frms::MessageBox::Show("アセンブリ名: "+assemblyName+"\r\n型名: "+typeName);
			if(assem!=null){
				return assem.GetType(typeName);
			}else{
				return System.Type.GetType(string.Format("{0}, {1}",typeName,assemblyName));
			}
		}
	}

	//==========================================================================
	//	UnsafeSurrogateSelector
	//		本来シリアル化出来ないオブジェクトをシリアル化する為に使用します。
	//--------------------------------------------------------------------------
	/// <summary>
	/// 本来シリアル化出来ないオブジェクトをシリアル化する為に使用します。
	/// </summary>
	/// <remarks>
	/// アセンブリの位置なども含めてシリアル化し、自動でアセンブリを読み込みます。
	/// 信用出来ないデータを逆シリアル化すると危険です。任意のコードを実行されます。
	/// </remarks>
	public class UnsafeSurrogateSelector:Ser::SurrogateSelector,Ser::ISurrogateSelector{
		private AnonymousDelegateSurrogate surr_anony=new AnonymousDelegateSurrogate();
		public UnsafeSurrogateSelector(){
			Ser::StreamingContext sc=new Ser::StreamingContext();
			Ref::Assembly asm_mscorlib=typeof(System.Type).Assembly;

			// Register Surrogate for System.Type
			RemoteTypeSurrogate surrogate=new RemoteTypeSurrogate();
			this.AddSurrogate(typeof(System.Type),sc,surrogate);
			this.AddSurrogate(asm_mscorlib.GetType("System.RuntimeType"),sc,surrogate);
			this.AddSurrogate(asm_mscorlib.GetType("System.ReflectionOnlyType"),sc,surrogate);

			// Register Surrogate for MemberInfoSerializationHolder
			this.AddSurrogate(asm_mscorlib.GetType("System.Reflection.MemberInfoSerializationHolder"),sc,new MemberInfoSHSurrogate());
		}

		public override Ser::ISerializationSurrogate GetSurrogate(
			Type type,Ser::StreamingContext context,out Ser::ISurrogateSelector selector
		){
			if(IsAnonymous(type)){
				selector=this;
				return this.surr_anony;
			}

			return base.GetSurrogate(type,context,out selector);
		}

		private static bool IsAnonymous(System.Type type){
			return type.Name.Contains("<>")&&type.HasCustomAttribute(typeof(Compilers::CompilerGeneratedAttribute));
		}
		private static bool IsAnonymousObject(object obj){
			return obj!=null&&IsAnonymous(obj.GetType());
		}

		#region Surrogates
		//==========================================================================
		//	surrogate for 匿名メソッド
		//--------------------------------------------------------------------------
		internal sealed class AnonymousDelegateSurrogate:Ser::ISerializationSurrogate{
			const Ref::BindingFlags BF_ALLINSTANCE=Ref::BindingFlags.Instance|Ref::BindingFlags.Public|Ref::BindingFlags.NonPublic;
			static readonly System.Type[] CTOR_PARAMS=new System.Type[0];
			static readonly object[] CTOR_ARGS=new object[0];

			public void GetObjectData(object target,Ser::SerializationInfo info,Ser::StreamingContext context) {
				System.Type type=target.GetType();
				info.AddValue("type",type);
				foreach(Ref::FieldInfo finfo in type.GetFields(BF_ALLINSTANCE)){
					object f=finfo.GetValue(target);
					info.AddValue("ftype:"+finfo.Name,f.GetType());
					info.AddValue("field:"+finfo.Name,f);
				}
			}

			public object SetObjectData(object obj,Ser::SerializationInfo info,Ser::StreamingContext context,Ser::ISurrogateSelector selector) {
				System.Type type=(System.Type)info.GetValue("type",typeof(System.Type));

				Ref::ConstructorInfo ctor=type.GetConstructor(BF_ALLINSTANCE,null,CTOR_PARAMS,null);
				if(ctor==null)
					throw new Ser::SerializationException(
						"AnonymousDelegateSurrogate.SetObjectData: 既定のコンストラクタがないので元に戻せません。"
						);

				object ret=ctor.Invoke(CTOR_ARGS);
				foreach(Ref::FieldInfo finfo in type.GetFields(BF_ALLINSTANCE)){
					System.Type ftype=(System.Type)info.GetValue("ftype:"+finfo.Name,typeof(System.Type));
					finfo.SetValue(ret,info.GetValue("field:"+finfo.Name,ftype));
				}
				
				return ret;
			}
		}
		//==========================================================================
		//	surrogate for System.Type
		//--------------------------------------------------------------------------
		/// <summary>
		/// System.Type に特別のシリアル化を行う為の Surrogate です。
		/// </summary>
		internal sealed class RemoteTypeSurrogate:Ser::ISerializationSurrogate{
			public RemoteTypeSurrogate(){}

			public void GetObjectData(
				object obj,
				Ser::SerializationInfo info,
				Ser::StreamingContext context
			){
				if(obj==null)throw new System.ArgumentNullException("obj");
				System.Type type=obj as System.Type;
				if(obj==null)throw new System.InvalidCastException("引数に指定した obj は System.Type 型ではありません。");

				String location=type.Assembly.Location;
#if FALSE
				// これを通過して送信されたオブジェクトを使おうとすると、
				// FatalExecutionEngineException が出て例外を捕まえる事も出来ない...

				// 1. 一つのアセンブリの中に複数のモジュールがあったりすると起こるのか?
				// 2. DotFuscator の結果のアセンブリを触ろうとすると起こるのか?
				if(location.Length==0){
					location=type.Assembly.CodeBase;
					if(location.StartsWith("file:///"))
						location=location.Substring(8).Replace(L'/',L'\\');
				}

				// for debug
				if(location.Length==0){
					Ref::Assembly assem=type.Assembly;
					System.Text.StringBuilder^ build=new System::Text::StringBuilder();
					build.Append("<Assembly> ");
					build.Append(assem.ToString());
					build.Append("\r\n");
					build.Append("CodeBase: ");
					build.Append(assem.CodeBase);
					build.Append("\r\n");
					build.Append("EscapedCodeBase: ");
					build.Append(assem.EscapedCodeBase);
					build.Append("\r\n");
					build.Append("FullName: ");
					build.Append(assem.FullName);
					build.Append("\r\n");
					build.Append("ManifestModule: ");
					build.Append(assem.ManifestModule.Name);
					build.Append("\r\n");
					build.Append("Modified Location: ");
					build.Append(location);
					build.Append("\r\n");
					System.Windows.Forms.MessageBox.Show(build.ToString());
				}
#endif

				info.AddValue("asm-loc",location);
				info.AddValue("name",type.FullName);
			}

			public object SetObjectData(
				object obj,
				Ser::SerializationInfo info,
				Ser::StreamingContext context,
				Ser::ISurrogateSelector selector
			){
				string asmLocation=info.GetString("asm-loc");
				string type_name=info.GetString("name");

//				if(asmLocation==""){
//					Frms::MessageBox::Show("アセンブリの場所が変です。");
//					return System::Type::GetType(type_name);
//				}

				Ref::Assembly assem=AssemblyStore.GetAssemblyFromLocation(asmLocation);
				return assem.GetType(type_name);
			}
		}
		//==========================================================================
		//	surrogate for System.Reflection.MemberInfo
		//--------------------------------------------------------------------------
		/// <summary>
		/// MemberInfoSerializationHolder (mscorlib 内で定義されている MemberInfo の情報保持者)
		/// に特別のシリアル化を行います。
		/// (リフレクションを使用して内容を修正します。)
		/// </summary>
		internal sealed class MemberInfoSHSurrogate:Ser::ISerializationSurrogate{
			public static System.Type holderType;

			static Ref::ConstructorInfo holderCtor;
			static Ref::FieldInfo m_reflectedType;		// System.Type

			static Ref::FieldInfo info_m_members;		// string[]
			static Ref::FieldInfo info_m_currMember;	// int
			static Ref::MethodInfo info_AddValue;		// void		SerializationInfo.AddValue(string,object,Type,int);

			static MemberInfoSHSurrogate(){
				const Ref::BindingFlags BF=Ref::BindingFlags.NonPublic|Ref::BindingFlags.Instance|Ref::BindingFlags.Static;

				//
				//	MemberInfoSerializationHolder の Reflection 用変数を初期化
				//
				holderType=typeof(System.Math).Assembly.GetType("System.Reflection.MemberInfoSerializationHolder");
				if(holderType==null)goto fail;

				holderCtor=holderType.GetConstructor(
					BF,
					null,
					new System.Type[]{
						typeof(Ser::SerializationInfo),
						typeof(Ser::StreamingContext)
					},
					null
				);
				m_reflectedType=holderType.GetField("m_reflectedType",BF);
				if(holderCtor==null||m_reflectedType==null)goto fail;

				
				//
				//	SerializationInfo の Reflection 用変数を初期化
				//
				System.Type tSerializationInfo=typeof(Ser::SerializationInfo);
				info_m_currMember=tSerializationInfo.GetField("m_currMember",BF);
				info_m_members=tSerializationInfo.GetField("m_members",BF);
				info_AddValue=tSerializationInfo.GetMethod(
					"AddValue",
					BF,
					null,
					new System.Type[]{typeof(string),typeof(object),typeof(System.Type),typeof(int)},
					null
				);
				if(info_m_currMember==null||info_m_members==null||info_AddValue==null)goto fail;

				return;
			fail:
				throw new System.Exception("!!! MemberInfoSHSurrogate の 初期化に失敗しました !!!");
			}

			/// <summary>
			/// 使用されないので放置
			/// </summary>
			public void GetObjectData(
				object obj,
				Ser::SerializationInfo info,
				Ser::StreamingContext context
			){
				throw new System.NotImplementedException();
			}

			/// <summary>
			/// 1. SerializationInfo の型情報から独自に型を取得
			/// 2. SerializationInfo の型情報を無難な物に書き換え
			/// 3. MemberInfoSerializationHolder を生成して、1. で取得した型を書き込む
			/// </summary>
			public object SetObjectData(
				object obj,
				Ser::SerializationInfo info,
				Ser::StreamingContext context,
				Ser::ISurrogateSelector selector
			){
				// 1.
				string assemblyName=info.GetString("AssemblyName");
				string className=info.GetString("ClassName");
				System.Type reflectedType=AssemblyStore.GetTypeFromName(assemblyName,className);

				// 2.
				InfoDataOverwrite(info,"AssemblyName",typeof(System.Math).Assembly.FullName);
				InfoDataOverwrite(info,"ClassName",typeof(System.Math).FullName);

				// 3.
				object holder=holderCtor.Invoke(new object[]{info,context});
				m_reflectedType.SetValue(holder,reflectedType);

				return holder;
			}
			private static void InfoDataOverwrite(
				Ser::SerializationInfo info,
				string name,string value
			){
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
		}
		#endregion
	}

}
