#pragma once
#include "stdafx.h"

//-------------------------------------------------------------------
// namespace mwg::Interop
namespace mwg{
namespace Interop{
//-------------------------------------------------------------------
	/// <summary>
	/// 或るプロセスからは直ぐ分かる所の Assembly も、別のプロセスからだと何処にあるか分からなかったりします。
	/// この Serializer は System.Type をAssembly の位置情報と共に Serialize して、
	/// Assembly の場所が直ぐ分かるようにして Serialize を実行します。
	/// <para>序でに nullptr も送る事が出来ます。</para>
	/// </summary>
	ref class TransTypeSerializer abstract sealed{
	private:
		static System::Runtime::Serialization::Formatters::Binary::BinaryFormatter^ binf;
		static TransTypeSerializer(){
			System::Runtime::Serialization::SurrogateSelector^ sur_sel
				=gcnew System::Runtime::Serialization::SurrogateSelector();
			System::Runtime::Serialization::StreamingContext sc;

			/* setup sur_sel */{
				RemoteTypeSurrogate^ surrogate=gcnew RemoteTypeSurrogate();
				Ref::Assembly^ asm_mscorlib=cl::type::typeid->Assembly;

				sur_sel->AddSurrogate(cl::type::typeid,sc,surrogate);
				sur_sel->AddSurrogate(asm_mscorlib->GetType("System.RuntimeType"),sc,surrogate);
				sur_sel->AddSurrogate(asm_mscorlib->GetType("System.ReflectionOnlyType"),sc,surrogate);
				sur_sel->AddSurrogate(asm_mscorlib->GetType("System.Reflection.MemberInfoSerializationHolder"),sc,gcnew MemberInfoSHSurrogate());
			}

			binf=gcnew System::Runtime::Serialization::Formatters::Binary::BinaryFormatter(sur_sel,sc);
			binf->Binder=gcnew RemoteTypeBinder();
		}

	public:
		static void Serialize(System::IO::Stream^ stream,cl::object^ graph){
			if(graph==nullptr){
				graph=IsNull();
			}
			binf->Serialize(stream,graph);
		}
		static cl::object^ Deserialize(System::IO::Stream^ stream){
			cl::object^ ret=binf->Deserialize(stream);

//			if(ret->ToString()->IndexOf("IsNull")>=0){
//				::MessageBox(NULL,_T("IsNull が含まれている情報です"),NULL,NULL);
//				Frms::MessageBox::Show(ret->GetType()->FullName);
//				Frms::MessageBox::Show(IsNull::typeid->FullName);
//				Frms::MessageBox::Show((ret::typeid==IsNull::typeid).ToString());
//			}

			if(ret->GetType()==IsNull::typeid){
				ret=nullptr;
			}
			return ret;
		}
		/// <summary>
		/// 遠くの Assembly を溜めておきます。
		/// </summary>
		ref class AssemblyStore abstract sealed{
			static Gen::Dictionary<cl::string^,System::Reflection::Assembly^>^ loc2asm
				=gcnew Gen::Dictionary<cl::string^,System::Reflection::Assembly^>();
			static Gen::Dictionary<cl::string^,System::Reflection::Assembly^>^ name2asm
				=gcnew Gen::Dictionary<cl::string^,System::Reflection::Assembly^>();
			static AssemblyStore(){
				// mgdspy_hk を登録
				RegisterAssembly(AssemblyStore::typeid->Assembly);

				// mgdspy_hk も含めて全て登録
				//for each(Ref::Assembly^ assem in System::AppDomain::GetAssemblies()){
				//	RegisterAssembly(assem);
				//}
			}
		public:

			static void RegisterAssembly(Ref::Assembly^ assem){
				loc2asm[assem->Location]=assem;
				name2asm[assem->FullName]=assem;

				// ↓効果無し
//				static Gen::List<cl::string^>^ private_paths=gcnew Gen::List<cl::string^>();
//
//				cl::string^ priv=System::IO::Path::GetDirectoryName(assem->Location);
//				if(!privPaths->Contains(priv)){
//					System::AppDomain::CurrentDomain->AppendPrivatePath(priv);
//					private_paths->Add(priv);
//				}

			}
			static Ref::Assembly^ GetAssemblyFromLocation(cl::string^ location){
				Ref::Assembly^ assem;
				if(loc2asm->TryGetValue(location,assem)){
					return assem;
				}else{
					assem=Ref::Assembly::LoadFrom(location);
					RegisterAssembly(assem);
					return assem;
				}
			}

			static Ref::Assembly^ GetAssemblyFromName(cl::string^ assemblyName){
				Ref::Assembly^ assem;
				if(name2asm->TryGetValue(assemblyName,assem)){
					return assem;
				}else{
					return nullptr;
				}
			}

			static cl::type^ GetTypeFromName(cl::string^ assemblyName,cl::string^ typeName){
				System::Reflection::Assembly^ assem=AssemblyStore::GetAssemblyFromName(assemblyName);
				//Frms::MessageBox::Show("アセンブリ名: "+assemblyName+"\r\n型名: "+typeName);
				if(assem!=nullptr){
					return assem->GetType(typeName);
				}else{
					return cl::type::GetType(cl::string::Format("{0}, {1}",typeName,assemblyName));
				}
			}
		};
	internal:
		[System::Serializable]
		value struct IsNull{};

#pragma region ref class RemoteTypeSurrogate //surrogate of System.Type
		/// <summary>
		/// System.Type に特別のシリアル化を行う為の Surrogate です。
		/// </summary>
		ref class RemoteTypeSurrogate sealed:public System::Runtime::Serialization::ISerializationSurrogate{
		public:
			RemoteTypeSurrogate(){}

			virtual void GetObjectData(
				cl::object^ obj,
				System::Runtime::Serialization::SerializationInfo^ info,
				System::Runtime::Serialization::StreamingContext context
			){
				if(obj==nullptr)throw gcnew System::ArgumentNullException("obj");
				cl::type^ type=dynamic_cast<cl::type^>(obj);
				if(obj==nullptr)throw gcnew System::InvalidCastException("引数に指定した obj は System.Type 型ではありません。");

				cl::string^ location=type->Assembly->Location;
#if FALSE		
				// これを通過して送信されたオブジェクトを使おうとすると、
				// FatalExecutionEngineException が出て例外を捕まえる事も出来ない...

				// 1. 一つのアセンブリの中に複数のモジュールがあったりすると起こるのか?
				// 2. DotFuscator の結果のアセンブリを触ろうとすると起こるのか?
				if(location->Length==0){
					location=type->Assembly->CodeBase;
					if(location->StartsWith("file:///"))
						location=location->Substring(8)->Replace(L'/',L'\\');
				}

				// for debug
				if(location->Length==0){
					Ref::Assembly^ assem=type->Assembly;
					System::Text::StringBuilder^ build=gcnew System::Text::StringBuilder();
					build->Append("<Assembly> ");
					build->Append(assem->ToString());
					build->Append("\r\n");
					build->Append("CodeBase: ");
					build->Append(assem->CodeBase);
					build->Append("\r\n");
					build->Append("EscapedCodeBase: ");
					build->Append(assem->EscapedCodeBase);
					build->Append("\r\n");
					build->Append("FullName: ");
					build->Append(assem->FullName);
					build->Append("\r\n");
					build->Append("ManifestModule: ");
					build->Append(assem->ManifestModule->Name);
					build->Append("\r\n");
					build->Append("Modified Location: ");
					build->Append(location);
					build->Append("\r\n");
					Frms::MessageBox::Show(build->ToString());
				}
#endif

				info->AddValue("asm-loc",location);
				info->AddValue("name",type->FullName);
			}

			virtual cl::object^ SetObjectData(
				cl::object^ obj,
				System::Runtime::Serialization::SerializationInfo^ info,
				System::Runtime::Serialization::StreamingContext context,
				System::Runtime::Serialization::ISurrogateSelector^ selector
			){
				cl::string^ asmLocation=info->GetString("asm-loc");
				cl::string^ type_name=info->GetString("name");

//				if(asmLocation==""){
//					Frms::MessageBox::Show("アセンブリの場所が変です。");
//					return System::Type::GetType(type_name);
//				}

				System::Reflection::Assembly^ assem=AssemblyStore::GetAssemblyFromLocation(asmLocation);
				return assem->GetType(type_name);
			}
		};
#pragma endregion

#pragma region ref class MemberInfoSHSurrogate // surrogate for System.Reflection.MemberInfo
		/// <summary>
		/// MemberInfoSerializationHolder (MemberInfo の情報保持者) に特別のシリアル化を行います。
		/// (リフレクションを使用して内容を修正します。)
		/// </summary>
		ref class MemberInfoSHSurrogate:public System::Runtime::Serialization::ISerializationSurrogate{
		public:
			static cl::type^ holderType;
		private:
			static Ref::ConstructorInfo^ holderCtor;
			static Ref::FieldInfo^ m_reflectedType;		// System.Type

			static Ref::FieldInfo^ info_m_members;		// string[]
			static Ref::FieldInfo^ info_m_currMember;	// int
			static Ref::MethodInfo^ info_AddValue;		// void		SerializationInfo.AddValue(string,object,Type,int);

			static MemberInfoSHSurrogate(){
				const Ref::BindingFlags BF=Ref::BindingFlags::NonPublic|Ref::BindingFlags::Instance|Ref::BindingFlags::Static;

				//
				//	MemberInfoSerializationHolder の Reflection 用変数を初期化
				//
				holderType=System::Math::typeid->Assembly->GetType("System.Reflection.MemberInfoSerializationHolder");
				if(holderType==nullptr)goto fail;

				holderCtor=holderType->GetConstructor(
					BF,
					nullptr,
					gcnew array<cl::type^>{
						System::Runtime::Serialization::SerializationInfo::typeid,
						System::Runtime::Serialization::StreamingContext::typeid
					},
					nullptr
				);
				m_reflectedType=holderType->GetField("m_reflectedType",BF);
				if(holderCtor==nullptr||m_reflectedType==nullptr)goto fail;

				
				//
				//	SerializationInfo の Reflection 用変数を初期化
				//
				cl::type^ tSerializationInfo=System::Runtime::Serialization::SerializationInfo::typeid;
				info_m_currMember=tSerializationInfo->GetField("m_currMember",BF);
				info_m_members=tSerializationInfo->GetField("m_members",BF);
				info_AddValue=tSerializationInfo->GetMethod(
					"AddValue",
					BF,
					nullptr,
					gcnew array<cl::type^>{cl::string::typeid,cl::object::typeid,cl::type::typeid,int::typeid},
					nullptr
				);
				if(info_m_currMember==nullptr||info_m_members==nullptr||info_AddValue==nullptr)goto fail;

				return;
			fail:
				throw gcnew System::Exception("!!! MemberInfoSHSurrogate の 初期化に失敗しました !!!");
			}

		public:
			/// <summary>
			/// 使用されないので放置
			/// </summary>
			virtual void GetObjectData(
				cl::object^ obj,
				System::Runtime::Serialization::SerializationInfo^ info,
				System::Runtime::Serialization::StreamingContext context
			){
				throw gcnew System::NotImplementedException();
			}

			/// <summary>
			/// 1. SerializationInfo の型情報から独自に型を取得
			/// 2. SerializationInfo の型情報を無難な物に書き換え
			/// 3. MemberInfoSerializationHolder を生成して、1. で取得した型を書き込む
			/// </summary>
			virtual cl::object^ SetObjectData(
				cl::object^ obj,
				System::Runtime::Serialization::SerializationInfo^ info,
				System::Runtime::Serialization::StreamingContext context,
				System::Runtime::Serialization::ISurrogateSelector^ selector
			){
				// 1.
				cl::string^ assemblyName=info->GetString("AssemblyName");
				cl::string^ className=info->GetString("ClassName");
				cl::type^ reflectedType=AssemblyStore::GetTypeFromName(assemblyName,className);

				// 2.
				InfoDataOverwrite(info,"AssemblyName",System::Math::typeid->Assembly->FullName);
				InfoDataOverwrite(info,"ClassName",System::Math::typeid->FullName);

				// 3.
				cl::object^ holder=holderCtor->Invoke(gcnew array<cl::object^>{info,context});
				m_reflectedType->SetValue(holder,reflectedType);

				return holder;
			}
		private:
			static void InfoDataOverwrite(
				System::Runtime::Serialization::SerializationInfo^ info,
				cl::string^ name,cl::string^ value
			){
				if(info==nullptr||name==nullptr||value==nullptr)
					throw gcnew System::ArgumentNullException();

				int m_currMember=(int)info_m_currMember->GetValue(info);
				array<cl::string^>^ m_members=(array<cl::string^>^)info_m_members->GetValue(info);
				for(int i=0;i<m_currMember;i++){
					if(m_members[i]!=name)continue;

					info_AddValue->Invoke(info,gcnew array<cl::object^>{name,value,cl::string::typeid,i});
					return;
				}

				// 元々なかった場合
				info->AddValue(name,value,cl::string::typeid);
			}
		};
#pragma endregion

		/// <summary>
		/// 遠くの Assembly の中の型のインスタンスも逆シリアル化出来るようにする為の Binder です。
		/// </summary>
		ref class RemoteTypeBinder sealed:public System::Runtime::Serialization::SerializationBinder{
		public:
			RemoteTypeBinder(){}
			virtual cl::type^ BindToType(cl::string^ assemblyName,cl::string^ typeName) override{
				return AssemblyStore::GetTypeFromName(assemblyName,typeName);
			}
		};

	};
//-----------------------------------------------
// endof namespace mwg::Interop
}
}
//-----------------------------------------------
