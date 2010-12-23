using Interop=System.Runtime.InteropServices;
using Gen=System.Collections.Generic;
using _=mwg.Win32.__global;

using DWORD=System.UInt32;

namespace mwg.Win32{
	internal unsafe static class __global{
		public static void* NULL=(void*)0;
		public static System.IntPtr INVALID_HANDLE_VALUE=(System.IntPtr)(-1);
		public const int TRUE=1;
		public const int FALSE=0;

		public const DWORD INFINITE=0xFFFFFFFFu;
	}

	public static unsafe class DbgHelp{
		[System.Obsolete("System::Diagnostics::Process->Modules を使用して下さい。")]
		[Interop::DllImport("dbghelp",SetLastError=true)]
		[return:Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		public static extern bool EnumerateLoadedModules(
			[Interop::In]System.IntPtr hProcess,
			[Interop::In]PENUMLOADED_MODULES_CALLBACK EnumLoadedModulesCallback,
			[Interop::In]System.IntPtr UserContext
			);

		[return: Interop::MarshalAs(Interop::UnmanagedType.Bool)]
		public delegate bool PENUMLOADED_MODULES_CALLBACK(
			[Interop::MarshalAs(Interop::UnmanagedType.LPStr)]string  ModuleName,
			System.IntPtr ModuleBase,
			uint ModuleSize,
			System.IntPtr UserContext
			);



		[Interop::DllImport("dbghelp",SetLastError=true)]
		public static unsafe extern void* ImageDirectoryEntryToData(
			[Interop::In]void* pBase,
			[Interop::In,Interop::MarshalAs(Interop::UnmanagedType.I1)]bool mappedAsImage,
			[Interop::In]IMAGE.DIRECTORY_ENTRY directoryEntry,
			[Interop::Out]uint* size
			);

		//============================================================
		//		IMAGE_DIRECTORY_ENTRY_IMPORT
		//============================================================
		public static ImageImportDescriptor[] ImageDirectoryEntryImport(void* pbase,bool mappedAsImage){
			uint size=0;
			IMAGE_IMPORT_DESCRIPTOR* ptr
				=(IMAGE_IMPORT_DESCRIPTOR*)ImageDirectoryEntryToData(pbase,mappedAsImage,IMAGE.DIRECTORY_ENTRY.IMPORT,&size);
			if(ptr==_.NULL)return null;

			Gen::List<ImageImportDescriptor> list=new Gen::List<ImageImportDescriptor>();
			while(ptr->Name!=0)
				list.Add(new ImageImportDescriptor(pbase,ptr++));
			return list.ToArray();
		}
		public struct IMAGE_IMPORT_DESCRIPTOR{
			/// <summary>
			/// 0 for terminating null import descriptor
			/// </summary>
			public uint Characteristics;
			/// <summary>
			/// RVA to original unbound IAT (PIMAGE_THUNK_DATA)
			/// </summary>
			public uint OriginalFirstThunk{
				get{return Characteristics;}
				set{Characteristics=value;}
			} 
			/// <summary>
			/// 0 if not bound,
			/// -1 if bound, and real date/time stamp
			///     in IMAGE_DIRECTORY_ENTRY_BOUND_IMPORT (new BIND)
			/// O.W. date/time stamp of DLL bound to (Old BIND)
			/// </summary>
			public uint TimeDateStamp;

			/// <summary>
			/// -1 if no forwarders
			/// </summary>
			public uint ForwarderChain;
			public uint Name;
		
			/// <summary>
			/// RVA to IAT (if bound this IAT has actual addresses)
			/// </summary>
			public uint FirstThunk; 
		}
		public class ImageImportDescriptor{
			internal ImageImportDescriptor(void* pbase,IMAGE_IMPORT_DESCRIPTOR* desc){
				this.base_addr=pbase;
				this.desc=desc;
			}

			IMAGE_IMPORT_DESCRIPTOR* desc;
			void* base_addr;

			public string Name{
				get{return Interop::Marshal.PtrToStringAnsi((System.IntPtr)((byte*)base_addr+desc->Name));}
			}

			void*[] thunkData=null;
			public void*[] ThunkData{
				get{
					if(thunkData==null){
						void** pth=(void**)((byte*)base_addr+desc->FirstThunk);
						void** pre=pth;while(*pre++!=_.NULL);
						thunkData=new void*[pre-pth];
						fixed(void** dstB=thunkData){
							void** dst=dstB;
							while((*dst++=*pth++)!=_.NULL);
						}
					}
					return thunkData;
				}
			}
		}
		//============================================================
		//		UNDNAME
		//============================================================
		/// <summary>
		/// UNDNAME_*
		/// </summary>
		public enum UNDNAME:uint{
			COMPLETE                 =0x0000,  // Enable full undecoration
			NO_LEADING_UNDERSCORES   =0x0001,  // Remove leading underscores from MS extended keywords
			NO_MS_KEYWORDS           =0x0002,  // Disable expansion of MS extended keywords
			NO_FUNCTION_RETURNS      =0x0004,  // Disable expansion of return type for primary declaration
			NO_ALLOCATION_MODEL      =0x0008,  // Disable expansion of the declaration model
			NO_ALLOCATION_LANGUAGE   =0x0010,  // Disable expansion of the declaration language specifier
			NO_MS_THISTYPE           =0x0020,  // NYI Disable expansion of MS keywords on the 'this' type for primary declaration
			NO_CV_THISTYPE           =0x0040,  // NYI Disable expansion of CV modifiers on the 'this' type for primary declaration
			NO_THISTYPE              =0x0060,  // Disable all modifiers on the 'this' type
			NO_ACCESS_SPECIFIERS     =0x0080,  // Disable expansion of access specifiers for members
			NO_THROW_SIGNATURES      =0x0100,  // Disable expansion of 'throw-signatures' for functions and pointers to functions
			NO_MEMBER_TYPE           =0x0200,  // Disable expansion of 'static' or 'virtual'ness of members
			NO_RETURN_UDT_MODEL      =0x0400,  // Disable expansion of MS model for UDT returns
			_32_BIT_DECODE           =0x0800,  // Undecorate 32-bit decorated names
			NAME_ONLY                =0x1000,  // Crack only the name for primary declaration;
											   //  return just [scope::]name.  Does expand template params
			NO_ARGUMENTS             =0x2000,  // Don't undecorate arguments to function
			NO_SPECIAL_SYMS          =0x4000,  // Don't undecorate special names (v-table, vcall, vector xxx, metatype, etc)
		}
		/// <summary>
		/// C++ の修飾名を普通の名前に戻します。
		/// </summary>
		/// <param name="DecoratedName">修飾名を指定します。</param>
		/// <param name="Flags">戻す際の動作についての指定を行います。</param>
		/// <returns>関数が失敗した場合には null を返します。</returns>
		public static string UnDecorateSymbolName(string DecoratedName,UNDNAME Flags){
			const int szBuff=0x400;
			sbyte* pch=stackalloc sbyte[szBuff+1];pch[szBuff]=0;
			if(0==UnDecorateSymbolName(DecoratedName,pch,(uint)szBuff,Flags))return null;
			return new string(pch);
		}

		[Interop::DllImport("dbghelp",SetLastError=true)]
		private static extern uint UnDecorateSymbolName(
			[Interop::MarshalAs(Interop::UnmanagedType.LPStr)]
			string  DecoratedName,         // Name to undecorate
			sbyte*  UnDecoratedName,       // If NULL, it will be allocated
			uint    UndecoratedLength,     // The maximym length
			UNDNAME Flags                  // See above.
			);
	}
}
