// これは メイン DLL ファイルです。

#include "stdafx.h"
#include "mgdspy_hk.h"


static CAtlFileMappingBase myFileMap;

const int FILENAME_SIZE=50;
static TCHAR filename[FILENAME_SIZE];
const int FILE_SIZE=0x80000; // 500 KB

static UINT spy_msg=::RegisterWindowMessage(_T("mwg.mgdspy_hk.Message"));

UINT GetSpyWindowMessage(void);
void* GetData(__int64 procid);
void* InitializeFile(__int64);
void* InitializeFile2(); // 旧形式

//===================================================================
//		以下: 関数の実装
//===================================================================
UINT GetSpyWindowMessage(void){
	return spy_msg;
}

void* GetData(__int64 procid){
	static bool initialized=false;
	static void* pdata=NULL;

	if(!initialized){
		initialized=true;
		pdata=InitializeFile(procid);
	}

	return pdata;
}

void* InitializeFile(__int64 procid){
	::_stprintf_s(filename,FILENAME_SIZE,_T("mwg.mgdspy_hk[ProcId=0x%X]"),procid);

	if(myFileMap.MapSharedMem(FILE_SIZE,filename)!=S_OK){
		::MessageBox(NULL,_T("ファイルをマップする事が出来ませんでした。"),_T("エラー"),0);
		return NULL;
	}

	// Confirm the size of the mapping file.
	if(myFileMap.GetMappingSize()!=FILE_SIZE){
		::MessageBox(NULL,_T("指定したサイズを確保する事が出来ませんでした"),_T("エラー"),0);
		return NULL;
	}

	void* ret=myFileMap.GetData();
	//Frms::MessageBox::Show(string_ct::Format("Memory Position: {0}",(System::IntPtr)ret));

#ifdef DEBUG_OLD
	*(int*)ret=2345;
	::MessageBox(NULL,_T("整数 2345 を shared mem に書き込みました"),_T("エラー"),0);
#endif

	return ret;
}

//
// こちらを使うと何故かアクセス出来ない
//
void* InitializeFile2(){
	// Create a file.
	CAtlFileMappingBase myFileMap;
	CAtlFile	myFile;

	myFile.Create(
		_T("myMapTestFile"),
		GENERIC_READ|GENERIC_WRITE|STANDARD_RIGHTS_ALL,
		FILE_SHARE_READ|FILE_SHARE_WRITE,
		OPEN_ALWAYS
		);

	// The file handle.
	HANDLE hFile = (HANDLE)myFile;

	// ちゃんとファイルが開いたか確認
	if(hFile==INVALID_HANDLE_VALUE){
		::MessageBox(NULL,_T("ファイルを開く事が出来ませんでした。"),_T("エラー"),0);
		return NULL;
	}

	// Open the file for file-mapping.
	// Must give a size as the file is zero by default.
	if(myFileMap.MapFile(hFile,1024,0,PAGE_READWRITE,FILE_MAP_READ)!=S_OK){
		CloseHandle(hFile);
		::MessageBox(NULL,_T("ファイルをマップする事が出来ませんでした。"),_T("エラー"),0);
		return NULL;
	}

	// Confirm the size of the mapping file.
	if(myFileMap.GetMappingSize()!=1024){
		::MessageBox(NULL,_T("指定したサイズを確保する事が出来ませんでした"),_T("エラー"),0);
		return NULL;
	}

	//::MessageBox(NULL,_T("1024 確保しました"),_T("エラー"),0);

	void* ret=myFileMap.GetData();
	return ret;
}


extern "C"{

__declspec(dllexport)
int __stdcall MessageHookProc(int nCode,unsigned int wparam,unsigned int lparam){
	/*
	static bool first=true;
	if(first){
		first=false;
		::MessageBox(NULL,_T("hooked"),_T("hooked"),0);
	}
	//*/
	return mwg::ManagedSpy::Channel::Client::OnMessage(
		(mwg::ManagedSpy::Channel::HC)nCode,
		(System::IntPtr)(int)wparam,
		(System::IntPtr)(int)lparam
	);
}

}
