using Interop=System.Runtime.InteropServices;
using Gen=System.Collections.Generic;
using _=mwg.Win32.__global;

using CHAR=System.SByte;
using BYTE=System.Byte;
using WORD=System.UInt16;
using DWORD=System.UInt32;
using LONG=System.Int32;
using ULONGLONG=System.UInt64;

namespace mwg.Win32{
	/// <summary>
	/// __stdcall 関数ポインタを管理します。
	/// </summary>
	public struct FPtr{
		System.IntPtr p;
		public FPtr(System.Delegate d){
			if(d_cache.TryGetValue(d,out this.p))return;
			d_cache.Add(d,this.p=Interop::Marshal.GetFunctionPointerForDelegate(d));
		}
		public System.Delegate ToDelegate(System.Type t){
			return Interop::Marshal.GetDelegateForFunctionPointer(p,t);
		}

		/// <summary>
		/// 作成した System.Delegate が勝手に消えないようにするためのキャッシュです。
		/// </summary>
		static Gen::Dictionary<System.Delegate,System.IntPtr> d_cache=new Gen::Dictionary<System.Delegate,System.IntPtr>();
		//============================================================
		//		演算子の類
		//============================================================
		private static string ToString_format="X"+(System.IntPtr.Size*2).ToString();
		public override string ToString(){
			return "fptr "+p.ToString(ToString_format);
		}
		public override bool Equals(object obj){
			// check;
			//if(obj is void*)return this.p==(System.IntPtr)(void*)obj;
			if(obj is System.IntPtr)return this.p==(System.IntPtr)obj;
			if(obj is FPtr)return this.p==(System.IntPtr)(FPtr)obj;
			return false;
		}
		public override int GetHashCode() {
			return this.p.GetHashCode();
		}
		public static bool operator ==(FPtr l,FPtr r){
			return l.p==r.p;
		}
		public static bool operator !=(FPtr l,FPtr r){
			return l.p!=r.p;
		}
		public static implicit operator System.IntPtr(FPtr fptr){
			return fptr.p;
		}
		public static implicit operator FPtr(System.IntPtr ptr){
			FPtr ret=new FPtr();
			ret.p=ptr;
			return ret;
		}
		public static unsafe implicit operator void*(FPtr fptr){
			return (void*)(System.IntPtr)fptr;
		}
		public static unsafe implicit operator FPtr(void* ptr){
			return (FPtr)(System.IntPtr)ptr;
		}
	}

	/// <summary>
	/// 実行形式などのファイルイメージに関する操作を提供します。
	/// </summary>
	public static class IMAGE {

		#region IMAGE_DOS_HEADER など
		/// <summary>
		/// IMAGE_*_SIGNATURE
		/// </summary>
		public enum SIGNATURE:ushort{
			[afh.EnumDescription("DOS Header \"MZ\"")]
			DOS=0x5A4D,
			[afh.EnumDescription("OS2 Header \"NE\"")]
			OS2=0x454E,
			[afh.EnumDescription("VXD Header \"LE\"")]
			VXD=0x454C,
			[afh.EnumDescription("VXD Header \"LE\"")]
			OS2_LE=VXD,
			[afh.EnumDescription("PE Header \"PE\\0\\0\"")]
			NT=0x00004550
		}

		/// <summary>
		/// IMAGE_DOS_HEADER
		/// </summary>
		[Interop::StructLayout(Interop::LayoutKind.Sequential)]
		public unsafe struct DOS_HEADER{// DOS .EXE header
			public SIGNATURE magic;           // Magic number 0x5a4d
			public WORD cblp;                 // Bytes on last page of file
			public WORD cp;                   // Pages in file
			public WORD crlc;                 // Relocations
			public WORD cparhdr;              // Size of header in paragraphs
			public WORD minalloc;             // Minimum extra paragraphs needed
			public WORD maxalloc;             // Maximum extra paragraphs needed
			public WORD ss;                   // Initial (relative) SS value
			public WORD sp;                   // Initial SP value
			public WORD csum;                 // Checksum
			public WORD ip;                   // Initial IP value
			public WORD cs;                   // Initial (relative) CS value
			public WORD lfarlc;               // File address of relocation table
			public WORD ovno;                 // Overlay number

			fixed  WORD res_1[4];             // Reserved words
			
			public WORD oemid;                // OEM identifier (for e_oeminfo)
			public WORD oeminfo;              // OEM information; e_oemid specific
			
			fixed  WORD res_2[10];            // Reserved words
			
			public int  lfanew;               // File address of new exe header
		}
		public struct OS2_HEADER {      // OS/2 .EXE header
			public SIGNATURE ne_magic;                    // Magic number
			public CHAR   ne_ver;                      // Version number
			public CHAR   ne_rev;                      // Revision number
			public WORD   ne_enttab;                   // Offset of Entry Table
			public WORD   ne_cbenttab;                 // Number of bytes in Entry Table
			public LONG   ne_crc;                      // Checksum of whole file
			public WORD   ne_flags;                    // Flag word
			public WORD   ne_autodata;                 // Automatic data segment number
			public WORD   ne_heap;                     // Initial heap allocation
			public WORD   ne_stack;                    // Initial stack allocation
			public LONG   ne_csip;                     // Initial CS:IP setting
			public LONG   ne_sssp;                     // Initial SS:SP setting
			public WORD   ne_cseg;                     // Count of file segments
			public WORD   ne_cmod;                     // Entries in Module Reference Table
			public WORD   ne_cbnrestab;                // Size of non-resident name table
			public WORD   ne_segtab;                   // Offset of Segment Table
			public WORD   ne_rsrctab;                  // Offset of Resource Table
			public WORD   ne_restab;                   // Offset of resident name table
			public WORD   ne_modtab;                   // Offset of Module Reference Table
			public WORD   ne_imptab;                   // Offset of Imported Names Table
			public LONG   ne_nrestab;                  // Offset of Non-resident Names Table
			public WORD   ne_cmovent;                  // Count of movable entries
			public WORD   ne_align;                    // Segment alignment shift count
			public WORD   ne_cres;                     // Count of resource segments
			public BYTE   ne_exetyp;                   // Target Operating system
			public BYTE   ne_flagsothers;              // Other .EXE flags
			public WORD   ne_pretthunks;               // offset to return thunks
			public WORD   ne_psegrefbytes;             // offset to segment ref. bytes
			public WORD   ne_swaparea;                 // Minimum code swap area size
			public WORD   ne_expver;                   // Expected Windows version number
		}

		public unsafe struct VXD_HEADER {      // Windows VXD header
			public SIGNATURE e32_magic;                   // Magic number
			public BYTE   e32_border;                  // The byte ordering for the VXD
			public BYTE   e32_worder;                  // The word ordering for the VXD
			public DWORD  e32_level;                   // The EXE format level for now = 0
			public WORD   e32_cpu;                     // The CPU type
			public WORD   e32_os;                      // The OS type
			public DWORD  e32_ver;                     // Module version
			public DWORD  e32_mflags;                  // Module flags
			public DWORD  e32_mpages;                  // Module # pages
			public DWORD  e32_startobj;                // Object # for instruction pointer
			public DWORD  e32_eip;                     // Extended instruction pointer
			public DWORD  e32_stackobj;                // Object # for stack pointer
			public DWORD  e32_esp;                     // Extended stack pointer
			public DWORD  e32_pagesize;                // VXD page size
			public DWORD  e32_lastpagesize;            // Last page size in VXD
			public DWORD  e32_fixupsize;               // Fixup section size
			public DWORD  e32_fixupsum;                // Fixup section checksum
			public DWORD  e32_ldrsize;                 // Loader section size
			public DWORD  e32_ldrsum;                  // Loader section checksum
			public DWORD  e32_objtab;                  // Object table offset
			public DWORD  e32_objcnt;                  // Number of objects in module
			public DWORD  e32_objmap;                  // Object page map offset
			public DWORD  e32_itermap;                 // Object iterated data map offset
			public DWORD  e32_rsrctab;                 // Offset of Resource Table
			public DWORD  e32_rsrccnt;                 // Number of resource entries
			public DWORD  e32_restab;                  // Offset of resident name table
			public DWORD  e32_enttab;                  // Offset of Entry Table
			public DWORD  e32_dirtab;                  // Offset of Module Directive Table
			public DWORD  e32_dircnt;                  // Number of module directives
			public DWORD  e32_fpagetab;                // Offset of Fixup Page Table
			public DWORD  e32_frectab;                 // Offset of Fixup Record Table
			public DWORD  e32_impmod;                  // Offset of Import Module Name Table
			public DWORD  e32_impmodcnt;               // Number of entries in Import Module Name Table
			public DWORD  e32_impproc;                 // Offset of Import Procedure Name Table
			public DWORD  e32_pagesum;                 // Offset of Per-Page Checksum Table
			public DWORD  e32_datapage;                // Offset of Enumerated Data Pages
			public DWORD  e32_preload;                 // Number of preload pages
			public DWORD  e32_nrestab;                 // Offset of Non-resident Names Table
			public DWORD  e32_cbnrestab;               // Size of Non-resident Name Table
			public DWORD  e32_nressum;                 // Non-resident Name Table Checksum
			public DWORD  e32_autodata;                // Object # for automatic data object
			public DWORD  e32_debuginfo;               // Offset of the debugging information
			public DWORD  e32_debuglen;                // The length of the debugging info. in bytes
			public DWORD  e32_instpreload;             // Number of instance pages in preload section of VXD file
			public DWORD  e32_instdemand;              // Number of instance pages in demand load section of VXD file
			public DWORD  e32_heapsize;                // Size of heap - for 16-bit apps
			fixed  BYTE   e32_res3[12];                // Reserved words
			public DWORD  e32_winresoff;
			public DWORD  e32_winreslen;
			public WORD   e32_devid;                   // Device ID for VxD
			public WORD   e32_ddkver;                  // DDK version for VxD
		}
		#endregion

		/// <summary>
		/// IMAGE_FILE_HEADER
		/// </summary>
		public unsafe struct FILE_HEADER{
			public FILE_HEADER_MACHINE machine;
			public string MachineDescription{
				get{return afh.Enum.GetDescription(this.machine);}
			}

			public WORD NumberOfSections;
			
			public DWORD timeDateStamp;
			private static System.DateTime dt0=new System.DateTime(1970,1,1,0,0,0);
			public System.DateTime TimeDateStamp{
				get{return dt0.AddSeconds(this.timeDateStamp);}
			}
			private System.DateTime TimeDateStamp2{
				get{
					long ft=(timeDateStamp+11644473600L)*10000000;
					return System.DateTime.FromFileTime(ft);
				}
			}
			private System.DateTime TimeDateStamp3{
				get{
					long time=timeDateStamp;
					return mwg.Crt.Time.localtime(&time)->ToLocalTime();
				}
			}

			public DWORD PointerToSymbolTable;
			public DWORD NumberOfSymbols;
			public WORD SizeOfOptionalHeader;
			public FILE_CHARACTER Characteristics;
		}

		/// <summary>
		/// IMAGE_FILE_MACHINE_*
		/// </summary>
		public enum FILE_HEADER_MACHINE:ushort{
			[afh.EnumDescription("不明な/任意のプロセッサ")]
			UNKNOWN		=0x0,
			[afh.EnumDescription("Intel 386 互換")]
			I386		=0x14c, 	//Intel 386 以降の processor 及び互換 processor Intel 386 or later processors and compatible processors
			[afh.EnumDescription("MIPS (BE) R3000")]
			R3000BE		=0x160,
			[afh.EnumDescription("MIPS (LE) R3000")]
			R3000		=0x162,
			[afh.EnumDescription("MIPS (LE) R4000")]
			R4000		=0x166, 	//MIPS little endian
			[afh.EnumDescription("MIPS (LE) R10000")]
			R10000		=0x168,
			[afh.EnumDescription("MIPS (LE) WCE v2")]
			WCEMIPSV2	=0x169, 	//MIPS little-endian WCE v2
			[afh.EnumDescription("Alpha_AXP")]
			ALPHA		=0x184,
			[afh.EnumDescription("日立 SH3")]
			SH3			=0x1a2, 	//日立 SH3 Hitachi SH3
			[afh.EnumDescription("日立 SH3 DSP")]
			SH3DSP		=0x1a3, 	//日立 SH3 DSP Hitachi SH3 DSP
			[afh.EnumDescription("日立 SH3E")]
			SH3E		=0x1a4,
			[afh.EnumDescription("日立 SH4")]
			SH4			=0x1a6, 	//日立 SH4 Hitachi SH4
			[afh.EnumDescription("日立 SH5")]
			SH5			=0x1a8, 	//日立 SH5 Hitachi SH5
			[afh.EnumDescription("ARM (LE)")]
			ARM			=0x1c0, 	//ARM little endian
			[afh.EnumDescription("Thumb")]
			THUMB		=0x1c2, 	//Thumb
			[afh.EnumDescription("松下 AM33")]
			AM33		=0x1d3, 	//松下 AM33 Matsushita AM33
			[afh.EnumDescription("IBM Power PC (LE)")]
			POWERPC		=0x1f0, 	//Power PC little endian
			[afh.EnumDescription("IBM Power PC with FPU")]
			POWERPCFP	=0x1f1, 	//Power PC with floating point support
			[afh.EnumDescription("Intel IA64 (Intel Itanium 系)")]
			IA64		=0x200, 	//Intel Itanium 系 processor Intel Itanium processor family
			[afh.EnumDescription("MIPS16")]
			MIPS16		=0x266,		//MIPS16
			[afh.EnumDescription("ALPHA AXP64")]
			ALPHA64		=0x284,
			[afh.EnumDescription("ALPHA AXP64")]
			AXP64		=0x284,
			[afh.EnumDescription("MIPS with FPU")]
			MIPSFPU		=0x366, 	//MIPS with FPU
			[afh.EnumDescription("MIPS16 with FPU")]
			MIPSFPU16	=0x466, 	//MIPS16 with FPU
			[afh.EnumDescription("Infineon Tricore")]
			TRICORE		=0x520,
			CEF			=0x0CEF,
			[afh.EnumDescription("EFI Byte Code")]
			EBC			=0x0ebc, 	//EFI byte code
			[afh.EnumDescription("AMD64 K8")]
			AMD64		=0x8664, 	//x64
			[afh.EnumDescription("三菱 M32R (LE)")]
			M32R		=0x9041,	//三菱 M32R little endian Mitsubishi M32R little endian
			[afh.EnumDescription("Common Language Runtime")]
			CEE			=0xC0EE,
		}

		/// <summary>
		/// IMAGE_FILE_*
		/// </summary>
		[System.Flags]
		public enum FILE_CHARACTER:ushort{
			RELOCS_STRIPPED           =0x0001,  // Relocation info stripped from file.
			EXECUTABLE_IMAGE          =0x0002,  // File is executable  (i.e. no unresolved externel references).
			LINE_NUMS_STRIPPED        =0x0004,  // Line nunbers stripped from file.
			LOCAL_SYMS_STRIPPED       =0x0008,  // Local symbols stripped from file.
			AGGRESIVE_WS_TRIM         =0x0010,  // Agressively trim working set
			LARGE_ADDRESS_AWARE       =0x0020,  // App can handle >2gb addresses
			BYTES_REVERSED_LO         =0x0080,  // Bytes of machine word are reversed.
			_32BIT_MACHINE            =0x0100,  // 32 bit word machine.
			DEBUG_STRIPPED            =0x0200,  // Debugging info stripped from file in .DBG file
			REMOVABLE_RUN_FROM_SWAP   =0x0400,  // If Image is on removable media, copy and run from the swap file.
			NET_RUN_FROM_SWAP         =0x0800,  // If Image is on Net, copy and run from the swap file.
			SYSTEM                    =0x1000,  // System File.
			DLL                       =0x2000,  // File is a DLL.
			UP_SYSTEM_ONLY            =0x4000,  // File should only be run on a UP machine
			BYTES_REVERSED_HI         =0x8000,  // Bytes of machine word are reversed.
		}

		#region IMAGE_OPTIONAL_HEADER など
		/// <summary>
		/// IMAGE_*_OPTIONAL_*_MAGIC
		/// </summary>
		public enum OPTIONAL_MAGIC:ushort{
			[afh.EnumDescription("NT PE32")]
			NT_HDR32	=0x10b,
			[afh.EnumDescription("NT PE32+")]
			NT_HDR64	=0x20b,
			[afh.EnumDescription("ROM Image HDR")]
			ROM_HDR		=0x107,
		}
		/// <summary>
		/// 拡張ヘッダの標準フィールドを保持します。
		/// </summary>
		public struct STD_OPTIONAL_HEADER{
			public OPTIONAL_MAGIC    Magic;
			public BYTE    majorLinkerVersion;
			public BYTE    minorLinkerVersion;
			public string LinkerVersion{
				get{return majorLinkerVersion.ToString()+"."+minorLinkerVersion.ToString();}
			}

			public DWORD   SizeOfCode;
			public DWORD   SizeOfInitializedData;
			public DWORD   SizeOfUninitializedData;
			public DWORD   AddressOfEntryPoint;
			public DWORD   BaseOfCode;
		}

		/// <summary>
		/// IMAGE_OPTIONAL_HEADER。PE32 の拡張ヘッダです。
		/// </summary>
		public struct NT32_OPTIONAL_HEADER{
			//
			// Standard fields.
			//
			public STD_OPTIONAL_HEADER STD;
			public DWORD   BaseOfData;

			//
			// NT additional fields.
			//
			public DWORD   ImageBase;
			public DWORD   SectionAlignment;
			public DWORD   FileAlignment;
			
			public WORD    majorOperatingSystemVersion;
			public WORD    minorOperatingSystemVersion;
			public string OSVersion{
				get{return majorOperatingSystemVersion.ToString()+"."+minorOperatingSystemVersion.ToString();}
			}
			public WORD    majorImageVersion;
			public WORD    minorImageVersion;
			public string ImageVersion{
				get{return majorImageVersion.ToString()+"."+minorImageVersion.ToString();}
			}
			public WORD    majorSubsystemVersion;
			public WORD    minorSubsystemVersion;
			public string SubsystemVersion{
				get{return majorSubsystemVersion.ToString()+"."+minorSubsystemVersion.ToString();}
			}

			public DWORD   Win32VersionValue;
			public DWORD   SizeOfImage;
			public DWORD   SizeOfHeaders;
			public DWORD   CheckSum;
			public SUBSYSTEM Subsystem;
			public DLLCHARACTERISTICS DllCharacteristics;
			public DWORD   SizeOfStackReserve;
			public DWORD   SizeOfStackCommit;
			public DWORD   SizeOfHeapReserve;
			public DWORD   SizeOfHeapCommit;
			public DWORD   LoaderFlags;  // Reserved. must be 0
			public DWORD   NumberOfRvaAndSizes;
			public DATA_DIRECTORY_Array DataDirectory;
		}

		/// <summary>
		/// IMAGE_ROM_OPTIONAL_HEADER
		/// </summary>
		public struct ROM_OPTIONAL_HEADER {
			public STD_OPTIONAL_HEADER STD;

			public DWORD  BaseOfData;
			public DWORD  BaseOfBss;
			public DWORD  GprMask;
			public DWORD  CprMask0;
			public DWORD  CprMask1;
			public DWORD  CprMask2;
			public DWORD  CprMask3;
			public DWORD  GpValue;
		}

		/// <summary>
		/// PE64 の拡張ヘッダです。
		/// IMAGE_OPTIONAL_HEADER64
		/// </summary>
		public struct NT64_OPTIONAL_HEADER {
			public STD_OPTIONAL_HEADER STD;

			public ULONGLONG   ImageBase;
			public DWORD       SectionAlignment;
			public DWORD       FileAlignment;
			public WORD        MajorOperatingSystemVersion;
			public WORD        MinorOperatingSystemVersion;
			public WORD        MajorImageVersion;
			public WORD        MinorImageVersion;
			public WORD        MajorSubsystemVersion;
			public WORD        MinorSubsystemVersion;
			public DWORD       Win32VersionValue;
			public DWORD       SizeOfImage;
			public DWORD       SizeOfHeaders;
			public DWORD       CheckSum;
			public SUBSYSTEM   Subsystem;
			public DLLCHARACTERISTICS DllCharacteristics;
			public ULONGLONG   SizeOfStackReserve;
			public ULONGLONG   SizeOfStackCommit;
			public ULONGLONG   SizeOfHeapReserve;
			public ULONGLONG   SizeOfHeapCommit;
			public DWORD       LoaderFlags;
			public DWORD       NumberOfRvaAndSizes;
			public DATA_DIRECTORY_Array DataDirectory;
		}

		/// <summary>
		/// IMAGE_SUBSYSTEM_*
		/// </summary>
		public enum SUBSYSTEM:ushort{
			UNKNOWN              =0,   // Unknown subsystem.
			NATIVE               =1,   // Image doesn't require a subsystem.
			WINDOWS_GUI          =2,   // Image runs in the Windows GUI subsystem.
			WINDOWS_CUI          =3,   // Image runs in the Windows character subsystem.
			OS2_CUI              =5,   // image runs in the OS/2 character subsystem.
			POSIX_CUI            =7,   // image runs in the Posix character subsystem.
			NATIVE_WINDOWS       =8,   // image is a native Win9x driver.
			WINDOWS_CE_GUI       =9,   // Image runs in the Windows CE subsystem.
			EFI_APPLICATION      =10,  //
			EFI_BOOT_SERVICE_DRIVER  =11,   //
			EFI_RUNTIME_DRIVER   =12,  //
			EFI_ROM              =13,
			XBOX                 =14,
		}

		[System.Flags]
		public enum DLLCHARACTERISTICS:ushort{
			PROCESS_INIT             =0x0001,     // Reserved. Must be 0.
			PROCESS_TERM             =0x0002,     // Reserved. Must be 0.
			THREAD_INIT              =0x0004,     // Reserved. Must be 0.
			THREAD_TERM              =0x0008,     // Reserved. Must be 0.
			DYNAMIC_BASE             =0x0040,
			FORCE_INTEGRITY          =0x0080,
			NX_COMPAT                =0x0100,
			NO_ISOLATION             =0x0200,     // Image understands isolation and doesn't want it
			NO_SEH                   =0x0400,     // Image does not use SEH.  No SE handler may reside in this image
			NO_BIND                  =0x0800,     // Do not bind this image.
			WDM_DRIVER               =0x2000,     // Driver uses WDM model
			TERMINAL_SERVER_AWARE    =0x8000,
		}
		#endregion

		#region IMAGE_DATA_DIRECTORY 配列
		/// <summary>
		/// IMAGE_DATA_DIRECTORY
		/// </summary>
		public struct DATA_DIRECTORY{
			public DWORD VirtualAddress;
			public DWORD Size;
		}

		/// <summary>
		/// IMAGE_DIRECTORY_ENTRY_*
		/// </summary>
		public enum DIRECTORY_ENTRY{
			[afh.EnumDescription("Export Directory")]
			EXPORT          =0,   // Export Directory
			[afh.EnumDescription("Import Directory")]
			IMPORT          =1,   // Import Directory
			[afh.EnumDescription("Resource Directory")]
			RESOURCE        =2,   // 
			[afh.EnumDescription("Exception Directory")]
			EXCEPTION       =3,   // Exception Directory
			[afh.EnumDescription("Security Directory")]
			SECURITY        =4,   // Security Directory
			[afh.EnumDescription("Base Relocation Table")]
			BASERELOC       =5,   // Base Relocation Table
			[afh.EnumDescription("Debug Directory")]
			DEBUG           =6,   // Debug Directory
			[afh.EnumDescription("Architecture Specific Data")]
			ARCHITECTURE    =7,   // Architecture Specific Data
			[afh.EnumDescription("RVA of GP")]
			GLOBALPTR       =8,   // RVA of GP
			[afh.EnumDescription("TLS Directory")]
			TLS             =9,   // TLS Directory
			[afh.EnumDescription("Load Configuration Directory")]
			LOAD_CONFIG    =10,   // Load Configuration Directory
			[afh.EnumDescription("Bound Import Directory in headers")]
			BOUND_IMPORT   =11,   // Bound Import Directory in headers
			[afh.EnumDescription("Import Address Table")]
			IAT            =12,   // Import Address Table
			[afh.EnumDescription("Delay Load Import Descriptors")]
			DELAY_IMPORT   =13,   // Delay Load Import Descriptors
			[afh.EnumDescription("COM/CLR Runtime Descriptor")]
			COM_DESCRIPTOR =14,   // COM Runtime descriptor
			[afh.EnumDescription("(Reserved. Must be 0)")]
			RESERVED       =15,
		}

		/// <summary>
		/// IMAGE_NUMBEROF_DIRECTORY_ENTRIES
		/// </summary>
		public const int NUMBEROF_DIRECTORY_ENTRIES=16;
		public struct DATA_DIRECTORY_Array{
			public DATA_DIRECTORY this[int index]{
				get{return this[(DIRECTORY_ENTRY)index];}
				set{this[(DIRECTORY_ENTRY)index]=value;}
			}
			public DATA_DIRECTORY this[DIRECTORY_ENTRY index]{
				get{
					switch(index){
						case DIRECTORY_ENTRY.EXPORT:return this.exportTable;
						case DIRECTORY_ENTRY.IMPORT:return this.importTable;
						case DIRECTORY_ENTRY.RESOURCE:return this.resourceTable;
						case DIRECTORY_ENTRY.EXCEPTION:return this.exceptionTable;
						case DIRECTORY_ENTRY.SECURITY:return this.certificateTable;
						case DIRECTORY_ENTRY.BASERELOC:return this.baseRelocationTable;
						case DIRECTORY_ENTRY.DEBUG:return this.debug;
						case DIRECTORY_ENTRY.ARCHITECTURE:return this.architecture;
						case DIRECTORY_ENTRY.GLOBALPTR:return this.globalPtr;
						case DIRECTORY_ENTRY.TLS:return this.TLSTable;
						case DIRECTORY_ENTRY.LOAD_CONFIG:return this.loadConfigTable;
						case DIRECTORY_ENTRY.BOUND_IMPORT:return this.boundImport;
						case DIRECTORY_ENTRY.IAT:return this.IAT;
						case DIRECTORY_ENTRY.DELAY_IMPORT:return this.delayImportDescriptor;
						case DIRECTORY_ENTRY.COM_DESCRIPTOR:return this.CLRRuntimeHeader;
						case DIRECTORY_ENTRY.RESERVED:return this.RESERVED;
						default:
							throw new System.ArgumentOutOfRangeException("index","enum DIRECTORY_ENTRY の範囲で指定して下さい。");
					}
				}
				set{
					switch(index){
						case DIRECTORY_ENTRY.EXPORT:this.exportTable=value;break;
						case DIRECTORY_ENTRY.IMPORT:this.importTable=value;break;
						case DIRECTORY_ENTRY.RESOURCE:this.resourceTable=value;break;
						case DIRECTORY_ENTRY.EXCEPTION:this.exceptionTable=value;break;
						case DIRECTORY_ENTRY.SECURITY:this.certificateTable=value;break;
						case DIRECTORY_ENTRY.BASERELOC:this.baseRelocationTable=value;break;
						case DIRECTORY_ENTRY.DEBUG:this.debug=value;break;
						case DIRECTORY_ENTRY.ARCHITECTURE:this.architecture=value;break;
						case DIRECTORY_ENTRY.GLOBALPTR:this.globalPtr=value;break;
						case DIRECTORY_ENTRY.TLS:this.TLSTable=value;break;
						case DIRECTORY_ENTRY.LOAD_CONFIG:this.loadConfigTable=value;break;
						case DIRECTORY_ENTRY.BOUND_IMPORT:this.boundImport=value;break;
						case DIRECTORY_ENTRY.IAT:this.IAT=value;break;
						case DIRECTORY_ENTRY.DELAY_IMPORT:this.delayImportDescriptor=value;break;
						case DIRECTORY_ENTRY.COM_DESCRIPTOR:this.CLRRuntimeHeader=value;break;
						case DIRECTORY_ENTRY.RESERVED:this.RESERVED=value;break;
						default:
							throw new System.ArgumentOutOfRangeException("index","enum DIRECTORY_ENTRY の範囲で指定して下さい。");
					}
				}
			}
			public DATA_DIRECTORY exportTable;
			public DATA_DIRECTORY importTable;
			public DATA_DIRECTORY resourceTable;
			public DATA_DIRECTORY exceptionTable;
			public DATA_DIRECTORY certificateTable;
			public DATA_DIRECTORY baseRelocationTable;
			public DATA_DIRECTORY debug;
			public DATA_DIRECTORY architecture;
			public DATA_DIRECTORY globalPtr;
			public DATA_DIRECTORY TLSTable;
			public DATA_DIRECTORY loadConfigTable;
			public DATA_DIRECTORY boundImport;
			public DATA_DIRECTORY IAT;
			public DATA_DIRECTORY delayImportDescriptor;
			public DATA_DIRECTORY CLRRuntimeHeader;
			public DATA_DIRECTORY RESERVED;
		}
		#endregion

		/// <summary>
		/// IMAGE_IMPORT_DESCRIPTOR
		/// </summary>
		public struct IMPORT_DESCRIPTOR{
			DWORD   u;
			public DWORD Characteristics{           // 0 for terminating null import descriptor
				get{return u;}
				set{this.u=value;}
			}
			public DWORD OriginalFirstThunk{        // RVA to original unbound IAT (PIMAGE_THUNK_DATA)
				get{return u;}
				set{this.u=value;}
			}
		    
			public DWORD   TimeDateStamp;           // 0 if not bound,
													// -1 if bound, and real date\time stamp
													//     in IMAGE_DIRECTORY_ENTRY_BOUND_IMPORT (new BIND)
													// O.W. date/time stamp of DLL bound to (Old BIND)

			public DWORD   ForwarderChain;          // -1 if no forwarders
			public DWORD   pstrName;
			public DWORD   FirstThunk;              // RVA to IAT (if bound this IAT has actual addresses)
		}

		/// <summary>
		/// IMAGE_THUNK_DATA64
		/// </summary>
		public struct THUNK_DATA64 {
			ULONGLONG u1;
			public ULONGLONG ForwarderString{  // PBYTE 
				get{return this.u1;}
				set{this.u1=value;}
			}
			public ULONGLONG Function{         // PDWORD
 				get{return this.u1;}
				set{this.u1=value;}
			}
			public ULONGLONG Ordinal{
 				get{return this.u1;}
				set{this.u1=value;}
			}
			public ULONGLONG AddressOfData{    // PIMAGE_IMPORT_BY_NAME
 				get{return this.u1;}
				set{this.u1=value;}
			}

			public ushort OrdinalValue{
				get{return (ushort)this.u1;}
			}
			public bool IsSnapByOrdinal{
				get{return (this.u1&(1uL<<63))!=0;}
			}
		}

		/// <summary>
		/// IMAGE_THUNK_DATA32
		/// </summary>
		public struct THUNK_DATA32 {
			DWORD u1;
			public DWORD ForwarderString{      // PBYTE
				get{return this.u1;}
				set{this.u1=value;}
			}
			public DWORD Function{             // PDWORD
				get{return this.u1;}
				set{this.u1=value;}
			}
			public DWORD Ordinal{
				get{return this.u1;}
				set{this.u1=value;}
			}
			public DWORD AddressOfData{        // PIMAGE_IMPORT_BY_NAME
				get{return this.u1;}
				set{this.u1=value;}
			}

			public ushort OrdinalValue{
				get{return (ushort)this.u1;}
			}
			public bool IsSnapByOrdinal{
				get{return (this.u1&0x80000000)!=0;}
			}
		}

		public static bool SNAP_BY_ORDINAL(uint value){
			return (value&1U<<31)!=0;
		}
		public static bool SNAP_BY_ORDINAL(ulong value){
			return (value&1UL<<63)!=0;
		}
		public static ushort ORDINAL(uint value){
			return (ushort)value;
		}
		public static ushort ORDINAL(ulong value){
			return (ushort)value;
		}

		/// <summary>
		/// IMAGE_IMPORT_BY_NAME
		/// </summary>
		public unsafe struct IMPORT_BY_NAME{
			public WORD    Hint;
			//public BYTE    Name; // 初めの一文字 ?
			public fixed byte Name[8];
		}
	}

	public static unsafe class APIHook{
		public static void ReplaceIATEntry(string exporter,FPtr currentProc,FPtr newProc){
			void* hModule=_.NULL;
			foreach(DbgHelp.ImageImportDescriptor desc in DbgHelp.ImageDirectoryEntryImport(hModule,true)){
				if(desc.Name!=exporter)continue;

				foreach(void* thunk in desc.ThunkData){
					FPtr* ppfn=(FPtr*)thunk;
					if(currentProc==*ppfn){
						Kernel32.PAGE def;
						Kernel32.VirtualProtect(ppfn,(uint)sizeof(FPtr),Kernel32.PAGE.READWRITE,out def);
						*ppfn=newProc;
						Kernel32.VirtualProtect(ppfn,(uint)sizeof(FPtr),def,out def);
						return;
					}
				}
			}
		}
	}
}