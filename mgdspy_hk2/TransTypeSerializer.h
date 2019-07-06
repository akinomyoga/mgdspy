#pragma once
#include "stdafx.h"

//-------------------------------------------------------------------
// namespace mwg::Interop
namespace mwg{
namespace Interop{
//-------------------------------------------------------------------
	/// <summary>
	/// ����v���Z�X����͒��������鏊�� Assembly ���A�ʂ̃v���Z�X���炾�Ɖ����ɂ��邩������Ȃ������肵�܂��B
	/// ���� Serializer �� System.Type ��Assembly �̈ʒu���Ƌ��� Serialize ���āA
	/// Assembly �̏ꏊ������������悤�ɂ��� Serialize �����s���܂��B
	/// <para>���ł� nullptr �����鎖���o���܂��B</para>
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
//				::MessageBox(NULL,_T("IsNull ���܂܂�Ă�����ł�"),NULL,NULL);
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
		/// ������ Assembly �𗭂߂Ă����܂��B
		/// </summary>
		ref class AssemblyStore abstract sealed{
			static Gen::Dictionary<cl::string^,System::Reflection::Assembly^>^ loc2asm
				=gcnew Gen::Dictionary<cl::string^,System::Reflection::Assembly^>();
			static Gen::Dictionary<cl::string^,System::Reflection::Assembly^>^ name2asm
				=gcnew Gen::Dictionary<cl::string^,System::Reflection::Assembly^>();
			static AssemblyStore(){
				// mgdspy_hk ��o�^
				RegisterAssembly(AssemblyStore::typeid->Assembly);

				// mgdspy_hk ���܂߂đS�ēo�^
				//for each(Ref::Assembly^ assem in System::AppDomain::GetAssemblies()){
				//	RegisterAssembly(assem);
				//}
			}
		public:

			static void RegisterAssembly(Ref::Assembly^ assem){
				loc2asm[assem->Location]=assem;
				name2asm[assem->FullName]=assem;

				// �����ʖ���
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
				//Frms::MessageBox::Show("�A�Z���u����: "+assemblyName+"\r\n�^��: "+typeName);
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
		/// System.Type �ɓ��ʂ̃V���A�������s���ׂ� Surrogate �ł��B
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
				if(obj==nullptr)throw gcnew System::InvalidCastException("�����Ɏw�肵�� obj �� System.Type �^�ł͂���܂���B");

				cl::string^ location=type->Assembly->Location;
#if FALSE		
				// �����ʉ߂��đ��M���ꂽ�I�u�W�F�N�g���g�����Ƃ���ƁA
				// FatalExecutionEngineException ���o�ė�O��߂܂��鎖���o���Ȃ�...

				// 1. ��̃A�Z���u���̒��ɕ����̃��W���[�����������肷��ƋN����̂�?
				// 2. DotFuscator �̌��ʂ̃A�Z���u����G�낤�Ƃ���ƋN����̂�?
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
//					Frms::MessageBox::Show("�A�Z���u���̏ꏊ���ςł��B");
//					return System::Type::GetType(type_name);
//				}

				System::Reflection::Assembly^ assem=AssemblyStore::GetAssemblyFromLocation(asmLocation);
				return assem->GetType(type_name);
			}
		};
#pragma endregion

#pragma region ref class MemberInfoSHSurrogate // surrogate for System.Reflection.MemberInfo
		/// <summary>
		/// MemberInfoSerializationHolder (MemberInfo �̏��ێ���) �ɓ��ʂ̃V���A�������s���܂��B
		/// (���t���N�V�������g�p���ē��e���C�����܂��B)
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
				//	MemberInfoSerializationHolder �� Reflection �p�ϐ���������
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
				//	SerializationInfo �� Reflection �p�ϐ���������
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
				throw gcnew System::Exception("!!! MemberInfoSHSurrogate �� �������Ɏ��s���܂��� !!!");
			}

		public:
			/// <summary>
			/// �g�p����Ȃ��̂ŕ��u
			/// </summary>
			virtual void GetObjectData(
				cl::object^ obj,
				System::Runtime::Serialization::SerializationInfo^ info,
				System::Runtime::Serialization::StreamingContext context
			){
				throw gcnew System::NotImplementedException();
			}

			/// <summary>
			/// 1. SerializationInfo �̌^��񂩂�Ǝ��Ɍ^���擾
			/// 2. SerializationInfo �̌^���𖳓�ȕ��ɏ�������
			/// 3. MemberInfoSerializationHolder �𐶐����āA1. �Ŏ擾�����^����������
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

				// ���X�Ȃ������ꍇ
				info->AddValue(name,value,cl::string::typeid);
			}
		};
#pragma endregion

		/// <summary>
		/// ������ Assembly �̒��̌^�̃C���X�^���X���t�V���A�����o����悤�ɂ���ׂ� Binder �ł��B
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
