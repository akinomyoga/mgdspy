#pragma once
#include "stdafx.h"

#if USE_ATL
# pragma managed(push,off)
#   include <atlfile.h>
# pragma managed(pop)
#else
# include "atlmacro_def.inl"
# include "CAtlFileMapping.h"
# include "atlmacro_undef.inl"
# define CAtlFileMappingBase		mwg::ATL::CAtlFileMappingBase
#endif

using namespace System::Runtime::InteropServices;
//-----------------------------------------------
// namespace mwg::Interop
namespace mwg{
namespace Interop{
//-----------------------------------------------
	/// <summary>
	/// プロセス間で共有するメモリを提供します。
	/// </summary>
	public ref class SharedMemory{
		unsigned int size;
		void* pfile;
#if USE_ATL
		CAtlFileMappingBase* pmap;
#else
		CAtlFileMappingBase^ pmap;
#endif
	public:
		/// <summary>
		/// SharedMemory の初期化子です。
		/// 指定した名前で指定した大きさの共有メモリを確保します。
		/// </summary>
		SharedMemory(System::String^ memoryId,unsigned int size){
#if USE_ATL
			this->pmap=&CAtlFileMappingBase();
#else
			this->pmap=gcnew CAtlFileMappingBase();
#endif
			this->size=size;

			if(pmap->MapSharedMem(size,String2LPTSTR(memoryId))!=S_OK){
				this->pfile=NULL;
				return;
			}

			if(pmap->GetMappingSize()!=size){
				this->pfile=NULL;
				return;
			}

			this->pfile=this->pmap->GetData();
			//Frms::MessageBox::Show("Shared Memory Name is:"+memoryId);
			//Frms::MessageBox::Show(string_ct::Format("Shared Memory was Allocated: address == {0:X}",(System::IntPtr)this->pfile));
			//Frms::MessageBox::Show(string_ct::Format("Initial Content of first 4B: {0}",*(int*)this->pfile));
			//*(int*)this->pfile=12345;
		}
		~SharedMemory(){this->!SharedMemory();}
		!SharedMemory(){
#if USE_ATL
			this->pmap->~CAtlFileMappingBase();
#else
			delete this->pmap;
#endif
		}
		void Close(){
			this->~SharedMemory();
		}

		/// <summary>
		/// データの開始位置を示すポインタを取得します。
		/// </summary>
		property void* pData{
			void* get(){return this->pfile;}
		}

		/// <summary>
		/// 確保されているデータの長さを取得します。
		/// </summary>
		property unsigned int Size{
			unsigned int get(){
				return this->size;
			}
		}
	};
//-----------------------------------------------
// endof namespace mwg::Interop
}
}
//-----------------------------------------------
