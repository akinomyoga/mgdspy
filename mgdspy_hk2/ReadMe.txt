===============================================================================
    �_�C�i�~�b�N �����N ���C�u����: mgdspy_hk2 �v���W�F�N�g�̊T�v
===============================================================================

���� mgdspy_hk2 DLL �́AAppWizard �ɂ���č쐬����܂����B  

���̃t�@�C���ɂ́Amgdspy_hk2 �A�v���P�[�V�������\������e�t�@�C����
���e�̊T�����L�q����Ă��܂��B

mgdspy_hk2.vcproj
    ����́A�A�v���P�[�V���� �E�B�U�[�h�Ő�������� VC++ �v���W�F�N�g�̃��C����
    �v���W�F�N�g �t�@�C���ł��B 
    �t�@�C���𐶐����� Visual C++ �̃o�[�W�������ƁA�A�v���P�[�V���� �E�B�U�[
    �h�őI�������v���b�g�t�H�[���A�\���A����уv���W�F�N�g�̋@�\�Ɋւ�����
    �L�q����Ă��܂��B

mgdspy_hk2.cpp
    ����́A���C���� DLL �\�[�X �t�@�C���ł��B

mgdspy_hk2.h
    ���̃t�@�C���ɂ́A�N���X�錾���܂܂�Ă��܂��B

AssemblyInfo.cpp
	�A�Z���u�� ���^�f�[�^��ύX���邽�߂̃J�X�^���������܂܂�Ă��܂��B

///////////////////////////////////////////////////////////////////////////////
���̑��̃��� :

AppWizard �ł́A"TODO:" ���g�p���āA���[�U�[���ǉ��܂��̓J�X�^�}�C�Y����\�[�X
�����������܂��B

///////////////////////////////////////////////////////////////////////////////





Just-In-Time (JIT) �f�o�b�O���Ăяo�����߂̏ڍׂɂ��ẮA
�_�C�A���O �{�b�N�X�ł͂Ȃ��A���̃��b�Z�[�W�̍Ō���Q�Ƃ��Ă��������B

************** ��O�e�L�X�g **************
System.Reflection.TargetInvocationException: �Ăяo���̃^�[�Q�b�g����O���X���[���܂����B ---> System.IO.FileNotFoundException: �t�@�C���܂��̓A�Z���u�� 'ManagedSpy, Version=1.0.2981.23426, Culture=neutral, PublicKeyToken=null'�A�܂��͂��̈ˑ��֌W�� 1 ���ǂݍ��߂܂���ł����B�w�肳�ꂽ�t�@�C����������܂���B
�t�@�C���� 'ManagedSpy, Version=1.0.2981.23426, Culture=neutral, PublicKeyToken=null' �ł��B
   �ꏊ System.Reflection.Assembly.nLoad(AssemblyName fileName, String codeBase, Evidence assemblySecurity, Assembly locationHint, StackCrawlMark& stackMark, Boolean throwOnFileNotFound, Boolean forIntrospection)
   �ꏊ System.Reflection.Assembly.InternalLoad(AssemblyName assemblyRef, Evidence assemblySecurity, StackCrawlMark& stackMark, Boolean forIntrospection)
   �ꏊ System.Reflection.Assembly.InternalLoad(String assemblyString, Evidence assemblySecurity, StackCrawlMark& stackMark, Boolean forIntrospection)
   �ꏊ System.Reflection.Assembly.Load(String assemblyString)
   �ꏊ System.Reflection.MemberInfoSerializationHolder..ctor(SerializationInfo info, StreamingContext context)

�x��: �A�Z���u�� �o�C���h�̃��O�L�^���I�t�ɂ���Ă��܂��B
�A�Z���u�� �o�C���h�̃G���[ ���O��L���ɂ���ɂ́A���W�X�g���l [HKLM\Software\Microsoft\Fusion!EnableLog] (DWORD) �� 1 �ɐݒ肵�Ă��������B
����: �A�Z���u�� �o�C���h�̃G���[ ���O�Ɋ֘A����p�t�H�[�}���X �y�i���e�B������܂��B
���̋@�\���I�t�ɂ���ɂ́A���W�X�g���l [HKLM\Software\Microsoft\Fusion!EnableLog] ���폜���܂��B

   --- ������O�X�^�b�N �g���[�X�̏I��� ---
   �ꏊ System.RuntimeMethodHandle._SerializationInvoke(Object target, SignatureStruct& declaringTypeSig, SerializationInfo info, StreamingContext context)
   �ꏊ System.RuntimeMethodHandle.SerializationInvoke(Object target, SignatureStruct declaringTypeSig, SerializationInfo info, StreamingContext context)
   �ꏊ System.Reflection.RuntimeConstructorInfo.SerializationInvoke(Object target, SerializationInfo info, StreamingContext context)
   �ꏊ System.Runtime.Serialization.ObjectManager.CompleteISerializableObject(Object obj, SerializationInfo info, StreamingContext context)
   �ꏊ System.Runtime.Serialization.ObjectManager.FixupSpecialObject(ObjectHolder holder)
   �ꏊ System.Runtime.Serialization.ObjectManager.DoFixups()
   �ꏊ System.Runtime.Serialization.Formatters.Binary.ObjectReader.Deserialize(HeaderHandler handler, __BinaryParser serParser, Boolean fCheck, Boolean isCrossAppDomain, IMethodCallMessage methodCallMessage)
   �ꏊ System.Runtime.Serialization.Formatters.Binary.BinaryFormatter.Deserialize(Stream serializationStream, HeaderHandler handler, Boolean fCheck, Boolean isCrossAppDomain, IMethodCallMessage methodCallMessage)
   �ꏊ System.Runtime.Serialization.Formatters.Binary.BinaryFormatter.Deserialize(Stream serializationStream)
   �ꏊ mwg.Interop.TransTypeSerializer.Deserialize(Stream stream) �ꏊ c:\documents and settings\koichi\my documents\visual studio 2005\2003 projects\mgdspy\mgdspy_hk2\transtypeserializer.h:�s 44
   �ꏊ mwg.Interop.Receiver.Cwm_SendObject() �ꏊ c:\documents and settings\koichi\my documents\visual studio 2005\2003 projects\mgdspy\mgdspy_hk2\channel.cpp:�s 147
   �ꏊ mwg.Interop.Receiver.WndProc(Message& m) �ꏊ c:\documents and settings\koichi\my documents\visual studio 2005\2003 projects\mgdspy\mgdspy_hk2\channel.h:�s 400
   �ꏊ System.Windows.Forms.Control.ControlNativeWindow.OnMessage(Message& m)
   �ꏊ System.Windows.Forms.Control.ControlNativeWindow.WndProc(Message& m)
   �ꏊ System.Windows.Forms.NativeWindow.Callback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)


************** �ǂݍ��܂ꂽ�A�Z���u�� **************
mscorlib
    �A�Z���u�� �o�[�W����: 2.0.0.0
    Win32 �o�[�W����: 2.0.50727.832 (QFE.050727-8300)
    �R�[�h�x�[�X: file:///C:/WINDOWS/Microsoft.NET/Framework/v2.0.50727/mscorlib.dll
----------------------------------------
mgdspy_hk
    �A�Z���u�� �o�[�W����: 1.0.2981.23346
    Win32 �o�[�W����: 
    �R�[�h�x�[�X: file:///C:/Documents%20and%20Settings/koichi/My%20Documents/Visual%20Studio%202005/2003%20Projects/mgdspy/ManagedSpy/bin/Debug/mgdspy_hk.dll
----------------------------------------
msvcm80d
    �A�Z���u�� �o�[�W����: 8.0.50727.762
    Win32 �o�[�W����: 8.00.50727.762
    �R�[�h�x�[�X: file:///C:/WINDOWS/WinSxS/x86_Microsoft.VC80.DebugCRT_1fc8b3b9a1e18e3b_8.0.50727.762_x-ww_5490cd9f/msvcm80d.dll
----------------------------------------
System
    �A�Z���u�� �o�[�W����: 2.0.0.0
    Win32 �o�[�W����: 2.0.50727.832 (QFE.050727-8300)
    �R�[�h�x�[�X: file:///C:/WINDOWS/assembly/GAC_MSIL/System/2.0.0.0__b77a5c561934e089/System.dll
----------------------------------------
System.Windows.Forms
    �A�Z���u�� �o�[�W����: 2.0.0.0
    Win32 �o�[�W����: 2.0.50727.832 (QFE.050727-8300)
    �R�[�h�x�[�X: file:///C:/WINDOWS/assembly/GAC_MSIL/System.Windows.Forms/2.0.0.0__b77a5c561934e089/System.Windows.Forms.dll
----------------------------------------
System.Drawing
    �A�Z���u�� �o�[�W����: 2.0.0.0
    Win32 �o�[�W����: 2.0.50727.832 (QFE.050727-8300)
    �R�[�h�x�[�X: file:///C:/WINDOWS/assembly/GAC_MSIL/System.Drawing/2.0.0.0__b03f5f7f11d50a3a/System.Drawing.dll
----------------------------------------
ManagedSpy
    �A�Z���u�� �o�[�W����: 1.0.2981.23426
    Win32 �o�[�W����: 1.0.2981.23426
    �R�[�h�x�[�X: file:///C:/Documents%20and%20Settings/koichi/My%20Documents/Visual%20Studio%202005/2003%20Projects/mgdspy/ManagedSpy/bin/Debug/ManagedSpy.exe
----------------------------------------
mscorlib.resources
    �A�Z���u�� �o�[�W����: 2.0.0.0
    Win32 �o�[�W����: 2.0.50727.832 (QFE.050727-8300)
    �R�[�h�x�[�X: file:///C:/WINDOWS/Microsoft.NET/Framework/v2.0.50727/mscorlib.dll
----------------------------------------
System.Windows.Forms.resources
    �A�Z���u�� �o�[�W����: 2.0.0.0
    Win32 �o�[�W����: 2.0.50727.42 (RTM.050727-4200)
    �R�[�h�x�[�X: file:///C:/WINDOWS/assembly/GAC_MSIL/System.Windows.Forms.resources/2.0.0.0_ja_b77a5c561934e089/System.Windows.Forms.resources.dll
----------------------------------------

************** JIT �f�o�b�O **************
Just-In-Time (JIT) �f�o�b�O��L���ɂ���ɂ́A���̃A�v���P�[�V�����A
�܂��̓R���s���[�^ (machine.config) �̍\���t�@�C���� jitDebugging 
�l�� system.windows.forms �Z�N�V�����Őݒ肵�Ȃ���΂Ȃ�܂���B
�A�v���P�[�V�����͂܂��A�f�o�b�O��L���ɂ��ăR���p�C������Ȃ����
�Ȃ�܂���B

��:

<configuration>
    <system.windows.forms jitDebugging="true" />
</configuration>

JIT �f�o�b�O���L���ȂƂ��́A���̃_�C�A���O �{�b�N�X�ŏ�����������A
�n���h������Ă��Ȃ���O�͂��ׂăR���s���[�^�ɓo�^���ꂽ
JIT �f�o�b�K�ɐݒ肳��Ȃ���΂Ȃ�܂���B



