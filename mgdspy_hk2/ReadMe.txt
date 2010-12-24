===============================================================================
    ダイナミック リンク ライブラリ: mgdspy_hk2 プロジェクトの概要
===============================================================================

この mgdspy_hk2 DLL は、AppWizard によって作成されました。  

このファイルには、mgdspy_hk2 アプリケーションを構成する各ファイルの
内容の概略が記述されています。

mgdspy_hk2.vcproj
    これは、アプリケーション ウィザードで生成される VC++ プロジェクトのメインの
    プロジェクト ファイルです。 
    ファイルを生成した Visual C++ のバージョン情報と、アプリケーション ウィザー
    ドで選択したプラットフォーム、構成、およびプロジェクトの機能に関する情報が
    記述されています。

mgdspy_hk2.cpp
    これは、メインの DLL ソース ファイルです。

mgdspy_hk2.h
    このファイルには、クラス宣言が含まれています。

AssemblyInfo.cpp
	アセンブリ メタデータを変更するためのカスタム属性が含まれています。

///////////////////////////////////////////////////////////////////////////////
その他のメモ :

AppWizard では、"TODO:" を使用して、ユーザーが追加またはカスタマイズするソース
部分を示します。

///////////////////////////////////////////////////////////////////////////////





Just-In-Time (JIT) デバッグを呼び出すための詳細については、
ダイアログ ボックスではなく、このメッセージの最後を参照してください。

************** 例外テキスト **************
System.Reflection.TargetInvocationException: 呼び出しのターゲットが例外をスローしました。 ---> System.IO.FileNotFoundException: ファイルまたはアセンブリ 'ManagedSpy, Version=1.0.2981.23426, Culture=neutral, PublicKeyToken=null'、またはその依存関係の 1 つが読み込めませんでした。指定されたファイルが見つかりません。
ファイル名 'ManagedSpy, Version=1.0.2981.23426, Culture=neutral, PublicKeyToken=null' です。
   場所 System.Reflection.Assembly.nLoad(AssemblyName fileName, String codeBase, Evidence assemblySecurity, Assembly locationHint, StackCrawlMark& stackMark, Boolean throwOnFileNotFound, Boolean forIntrospection)
   場所 System.Reflection.Assembly.InternalLoad(AssemblyName assemblyRef, Evidence assemblySecurity, StackCrawlMark& stackMark, Boolean forIntrospection)
   場所 System.Reflection.Assembly.InternalLoad(String assemblyString, Evidence assemblySecurity, StackCrawlMark& stackMark, Boolean forIntrospection)
   場所 System.Reflection.Assembly.Load(String assemblyString)
   場所 System.Reflection.MemberInfoSerializationHolder..ctor(SerializationInfo info, StreamingContext context)

警告: アセンブリ バインドのログ記録がオフにされています。
アセンブリ バインドのエラー ログを有効にするには、レジストリ値 [HKLM\Software\Microsoft\Fusion!EnableLog] (DWORD) を 1 に設定してください。
注意: アセンブリ バインドのエラー ログに関連するパフォーマンス ペナルティがあります。
この機能をオフにするには、レジストリ値 [HKLM\Software\Microsoft\Fusion!EnableLog] を削除します。

   --- 内部例外スタック トレースの終わり ---
   場所 System.RuntimeMethodHandle._SerializationInvoke(Object target, SignatureStruct& declaringTypeSig, SerializationInfo info, StreamingContext context)
   場所 System.RuntimeMethodHandle.SerializationInvoke(Object target, SignatureStruct declaringTypeSig, SerializationInfo info, StreamingContext context)
   場所 System.Reflection.RuntimeConstructorInfo.SerializationInvoke(Object target, SerializationInfo info, StreamingContext context)
   場所 System.Runtime.Serialization.ObjectManager.CompleteISerializableObject(Object obj, SerializationInfo info, StreamingContext context)
   場所 System.Runtime.Serialization.ObjectManager.FixupSpecialObject(ObjectHolder holder)
   場所 System.Runtime.Serialization.ObjectManager.DoFixups()
   場所 System.Runtime.Serialization.Formatters.Binary.ObjectReader.Deserialize(HeaderHandler handler, __BinaryParser serParser, Boolean fCheck, Boolean isCrossAppDomain, IMethodCallMessage methodCallMessage)
   場所 System.Runtime.Serialization.Formatters.Binary.BinaryFormatter.Deserialize(Stream serializationStream, HeaderHandler handler, Boolean fCheck, Boolean isCrossAppDomain, IMethodCallMessage methodCallMessage)
   場所 System.Runtime.Serialization.Formatters.Binary.BinaryFormatter.Deserialize(Stream serializationStream)
   場所 mwg.Interop.TransTypeSerializer.Deserialize(Stream stream) 場所 c:\documents and settings\koichi\my documents\visual studio 2005\2003 projects\mgdspy\mgdspy_hk2\transtypeserializer.h:行 44
   場所 mwg.Interop.Receiver.Cwm_SendObject() 場所 c:\documents and settings\koichi\my documents\visual studio 2005\2003 projects\mgdspy\mgdspy_hk2\channel.cpp:行 147
   場所 mwg.Interop.Receiver.WndProc(Message& m) 場所 c:\documents and settings\koichi\my documents\visual studio 2005\2003 projects\mgdspy\mgdspy_hk2\channel.h:行 400
   場所 System.Windows.Forms.Control.ControlNativeWindow.OnMessage(Message& m)
   場所 System.Windows.Forms.Control.ControlNativeWindow.WndProc(Message& m)
   場所 System.Windows.Forms.NativeWindow.Callback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)


************** 読み込まれたアセンブリ **************
mscorlib
    アセンブリ バージョン: 2.0.0.0
    Win32 バージョン: 2.0.50727.832 (QFE.050727-8300)
    コードベース: file:///C:/WINDOWS/Microsoft.NET/Framework/v2.0.50727/mscorlib.dll
----------------------------------------
mgdspy_hk
    アセンブリ バージョン: 1.0.2981.23346
    Win32 バージョン: 
    コードベース: file:///C:/Documents%20and%20Settings/koichi/My%20Documents/Visual%20Studio%202005/2003%20Projects/mgdspy/ManagedSpy/bin/Debug/mgdspy_hk.dll
----------------------------------------
msvcm80d
    アセンブリ バージョン: 8.0.50727.762
    Win32 バージョン: 8.00.50727.762
    コードベース: file:///C:/WINDOWS/WinSxS/x86_Microsoft.VC80.DebugCRT_1fc8b3b9a1e18e3b_8.0.50727.762_x-ww_5490cd9f/msvcm80d.dll
----------------------------------------
System
    アセンブリ バージョン: 2.0.0.0
    Win32 バージョン: 2.0.50727.832 (QFE.050727-8300)
    コードベース: file:///C:/WINDOWS/assembly/GAC_MSIL/System/2.0.0.0__b77a5c561934e089/System.dll
----------------------------------------
System.Windows.Forms
    アセンブリ バージョン: 2.0.0.0
    Win32 バージョン: 2.0.50727.832 (QFE.050727-8300)
    コードベース: file:///C:/WINDOWS/assembly/GAC_MSIL/System.Windows.Forms/2.0.0.0__b77a5c561934e089/System.Windows.Forms.dll
----------------------------------------
System.Drawing
    アセンブリ バージョン: 2.0.0.0
    Win32 バージョン: 2.0.50727.832 (QFE.050727-8300)
    コードベース: file:///C:/WINDOWS/assembly/GAC_MSIL/System.Drawing/2.0.0.0__b03f5f7f11d50a3a/System.Drawing.dll
----------------------------------------
ManagedSpy
    アセンブリ バージョン: 1.0.2981.23426
    Win32 バージョン: 1.0.2981.23426
    コードベース: file:///C:/Documents%20and%20Settings/koichi/My%20Documents/Visual%20Studio%202005/2003%20Projects/mgdspy/ManagedSpy/bin/Debug/ManagedSpy.exe
----------------------------------------
mscorlib.resources
    アセンブリ バージョン: 2.0.0.0
    Win32 バージョン: 2.0.50727.832 (QFE.050727-8300)
    コードベース: file:///C:/WINDOWS/Microsoft.NET/Framework/v2.0.50727/mscorlib.dll
----------------------------------------
System.Windows.Forms.resources
    アセンブリ バージョン: 2.0.0.0
    Win32 バージョン: 2.0.50727.42 (RTM.050727-4200)
    コードベース: file:///C:/WINDOWS/assembly/GAC_MSIL/System.Windows.Forms.resources/2.0.0.0_ja_b77a5c561934e089/System.Windows.Forms.resources.dll
----------------------------------------

************** JIT デバッグ **************
Just-In-Time (JIT) デバッグを有効にするには、このアプリケーション、
またはコンピュータ (machine.config) の構成ファイルの jitDebugging 
値を system.windows.forms セクションで設定しなければなりません。
アプリケーションはまた、デバッグを有効にしてコンパイルされなければ
なりません。

例:

<configuration>
    <system.windows.forms jitDebugging="true" />
</configuration>

JIT デバッグが有効なときは、このダイアログ ボックスで処理するよりも、
ハンドルされていない例外はすべてコンピュータに登録された
JIT デバッガに設定されなければなりません。



