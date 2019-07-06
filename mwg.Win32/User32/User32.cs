using Frms=System.Windows.Forms;
using Interop=System.Runtime.InteropServices;
using CM=System.ComponentModel;

namespace mwg.Win32{
	/// <summary>
	/// �E�B���h�E�Ȃǂ̃��[�U�[�C���^�[�t�F�C�X�Ɋւ���֐��Q�����J����N���X�ł��B
	/// </summary>
	public partial class User32{
		/// <summary>
		/// ��ʏ�̂��ׂẴg�b�v���x���E�B���h�E��񋓂��܂��B
		/// ���̊֐����Ăяo���ƁA�e�E�B���h�E�̃n���h�������X�ɃA�v���P�[�V������`�̃R�[���o�b�N�֐��ɓn����܂��B
		/// EnumWindows �֐��́A���ׂẴg�b�v���x�������h�E��񋓂��I���邩�A
		/// �܂��̓A�v���P�[�V������`�̃R�[���o�b�N�֐����� 0�iFALSE�j���Ԃ����܂ŏ����𑱂��܂��B
		/// </summary>
		/// <param name="lpEnumFunc">�A�v���P�[�V������`�̃R�[���o�b�N�֐��ւ̃|�C���^���w�肵�܂��B
		/// �ڍׂɂ��ẮAEnumWindowsProc �֐��̐������Q�Ƃ��Ă��������B</param>
		/// <param name="lParam">�R�[���o�b�N�֐��ɓn���A�v���P�[�V������`�̒l���w�肵�܂��B</param>
		/// <returns>�֐������������ true ��Ԃ��܂��B�g���G���[���� GetLastError �Ŏ擾�o���܂��B</returns>
		[Interop.DllImport("user32.dll",SetLastError=true)]
		[return: Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc,System.IntPtr lParam);
		/// <summary>
		/// �q�E�B���h�E�̃n���h�����擾���܂��B
		/// </summary>
		/// <param name="hwndParent">�e�E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <param name="lpEnumFunc">
		/// �q�E�B���h�E�̃n���h������������֐����w�肵�܂��B
		/// �����ɐ��������ꍇ�ɂ� true ��Ԃ��܂��B
		/// �����Ɏ��s�����ꍇ�ɂ� false ��Ԃ��܂��B
		/// </param>
		/// <param name="lParam">lpEnumFunc �Ŏw�肵���֐��ɓn���������w�肵�܂��B</param>
		/// <returns>�֐��������������ۂ���Ԃ��܂��B�g���G���[���� GetLastError �Ŏ擾�o���܂��B</returns>
		[Interop.DllImport("user32.dll",SetLastError=true)]
		[return: Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		public static extern bool EnumChildWindows(System.IntPtr hwndParent,EnumWindowsProc lpEnumFunc,System.IntPtr lParam);

		[return: Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		public delegate bool EnumWindowsProc(System.IntPtr hWnd,System.IntPtr lParam);
		//[Interop.DllImport("user32.dll",EntryPoint="EnumWindows")]
		//[return: Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		//public static extern bool EnumWindowsRf(EnumWindowsProcRf lpEnumFunc,ref System.IntPtr lParam);
		//public delegate bool EnumWindowsProcRf(System.IntPtr hWnd,ref System.IntPtr lParam);

		[Interop::DllImport("user32.dll",SetLastError=true,CharSet=Interop::CharSet.Auto)]
		public static extern WM RegisterWindowMessage(string lpString);


		#region WindowLongPtr
		private static Interop::HandleRef Window2Handle(Frms::IWin32Window wnd){
			return new Interop::HandleRef(wnd,wnd.Handle);
		}
		/// <summary>
		/// �E�B���h�E�̊g���E�B���h�E���������̎w�肳�ꂽ�I�t�Z�b�g�ʒu�ɂ���l���擾���܂��B
		/// ���̊֐��́AGetWindowLong �֐��̉����łł��B32 �r�b�g�� Windows �� 64 �r�b�g�� Windows �̗����Ƃ��ƌ݊����̂���R�[�h���L�q����ɂ́AGetWindowLongPtr �֐����g���Ă��������B
		/// </summary>
		/// <param name="wnd">�E�B���h�E���w�肵�܂��B</param>
		/// <param name="nIndex">�擾����l�� 0 ����n�܂�I�t�Z�b�g���w�肵�܂��B�L���ȃI�t�Z�b�g�́A0 ����g���E�B���h�E�������̃o�C�g�� -8 �܂łł��B���Ƃ��΁A�g���������� 24 �o�C�g�ȏ゠��ꍇ�A16 ���w�肷��ƁA3 �Ԗڂ̐����l���擾�ł��܂��B</param>
		/// <returns>�֐�����������ƁA�v�������l���Ԃ�܂��B�֐������s����ƁA0 ���Ԃ�܂��B�g���G���[�����擾����ɂ́AGetLastError �֐����g���܂��B</returns>
		/// <remarks>�g���E�B���h�E�������́ARegisterClassEx �֐��ɓn�� WNDCLASSEX �\���̂� cbWndExtra �����o�� 0 �ȊO�̒l��ݒ肷�邱�Ƃɂ���Ċm�ۂ��܂��B</remarks>
		public static System.IntPtr GetWindowLongPtr(Frms::IWin32Window wnd,int nIndex){
			Interop::HandleRef hWnd=Window2Handle(wnd);
			return System.IntPtr.Size==8?GetWindowLongPtr64(hWnd,nIndex):(System.IntPtr)GetWindowLongPtr32(hWnd,nIndex);
		}
		/// <summary>
		/// �w�肳�ꂽ�E�B���h�E�Ɋւ�������擾���܂��B
		/// </summary>
		/// <param name="wnd">�E�B���h�E���w�肵�܂��B</param>
		/// <param name="nIndex">�擾������ɑΉ�����l���w�肵�܂��B</param>
		/// <returns>�֐�����������ƁA�v�������l���Ԃ�܂��B�֐������s����ƁA0 ���Ԃ�܂��B�g���G���[�����擾����ɂ́AGetLastError �֐����g���܂��B</returns>
		/// <remarks>�g���E�B���h�E�������́ARegisterClassEx �֐��ɓn�� WNDCLASSEX �\���̂� cbWndExtra �����o�� 0 �ȊO�̒l��ݒ肷�邱�Ƃɂ���Ċm�ۂ��܂��B</remarks>
		public static System.IntPtr GetWindowLongPtr(Frms::IWin32Window wnd,GWLP nIndex) {
			Interop::HandleRef hWnd=Window2Handle(wnd);
			if(System.IntPtr.Size==8) {
				int index=nIndex==GWLP.USER?16:nIndex==GWLP.DLGPROC?8:(int)nIndex;
				return GetWindowLongPtr64(hWnd,index);
			}else{
				return (System.IntPtr)GetWindowLongPtr32(hWnd,(int)nIndex);
			}
		}
		/// <summary>
		/// �E�B���h�E�̊g���E�B���h�E���������̎w�肳�ꂽ�I�t�Z�b�g�ʒu�ɂ���l���擾���܂��B
		/// ���̊֐��́AGetWindowLong �֐��̉����łł��B32 �r�b�g�� Windows �� 64 �r�b�g�� Windows �̗����Ƃ��ƌ݊����̂���R�[�h���L�q����ɂ́AGetWindowLongPtr �֐����g���Ă��������B
		/// </summary>
		/// <param name="hWnd">�E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <param name="nIndex">�擾����l�� 0 ����n�܂�I�t�Z�b�g���w�肵�܂��B�L���ȃI�t�Z�b�g�́A0 ����g���E�B���h�E�������̃o�C�g�� -8 �܂łł��B���Ƃ��΁A�g���������� 24 �o�C�g�ȏ゠��ꍇ�A16 ���w�肷��ƁA3 �Ԗڂ̐����l���擾�ł��܂��B</param>
		/// <returns>�֐�����������ƁA�v�������l���Ԃ�܂��B�֐������s����ƁA0 ���Ԃ�܂��B�g���G���[�����擾����ɂ́AGetLastError �֐����g���܂��B</returns>
		/// <remarks>�g���E�B���h�E�������́ARegisterClassEx �֐��ɓn�� WNDCLASSEX �\���̂� cbWndExtra �����o�� 0 �ȊO�̒l��ݒ肷�邱�Ƃɂ���Ċm�ۂ��܂��B</remarks>
		public static System.IntPtr GetWindowLongPtr(System.IntPtr hWnd,int nIndex){
			return System.IntPtr.Size==8?GetWindowLongPtr64(hWnd,nIndex):(System.IntPtr)GetWindowLongPtr32(hWnd,nIndex);
		}
		/// <summary>
		/// �w�肳�ꂽ�E�B���h�E�Ɋւ�������擾���܂��B
		/// </summary>
		/// <param name="hWnd">�E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <param name="nIndex">�擾������ɑΉ�����l���w�肵�܂��B</param>
		/// <returns>�֐�����������ƁA�v�������l���Ԃ�܂��B�֐������s����ƁA0 ���Ԃ�܂��B�g���G���[�����擾����ɂ́AGetLastError �֐����g���܂��B</returns>
		/// <remarks>�g���E�B���h�E�������́ARegisterClassEx �֐��ɓn�� WNDCLASSEX �\���̂� cbWndExtra �����o�� 0 �ȊO�̒l��ݒ肷�邱�Ƃɂ���Ċm�ۂ��܂��B</remarks>
		public static System.IntPtr GetWindowLongPtr(System.IntPtr hWnd,GWLP nIndex){
			if(System.IntPtr.Size==8){
				int index=nIndex==GWLP.USER?16:nIndex==GWLP.DLGPROC?8:(int)nIndex;
				return GetWindowLongPtr64(hWnd,index);
			}else{
				return (System.IntPtr)GetWindowLongPtr32(hWnd,(int)nIndex);
			}
		}
		[Interop.DllImport("user32.dll",EntryPoint="GetWindowLong")]
		private static extern int GetWindowLongPtr32(System.IntPtr hWnd,int nIndex);
		[Interop.DllImport("user32.dll",EntryPoint="GetWindowLong")]
		private static extern int GetWindowLongPtr32(Interop::HandleRef hWnd,int nIndex);
		[Interop.DllImport("user32.dll",EntryPoint="GetWindowLongPtr")]
		private static extern System.IntPtr GetWindowLongPtr64(System.IntPtr hWnd, int nIndex);
		[Interop.DllImport("user32.dll",EntryPoint="GetWindowLongPtr")]
		private static extern System.IntPtr GetWindowLongPtr64(Interop::HandleRef hWnd,int nIndex);
		/// <summary>
		/// �E�B���h�E�̊g���E�B���h�E���������̎w�肳�ꂽ�I�t�Z�b�g�ʒu�ɂ���l���擾���܂��B
		/// ���̊֐��́AGetWindowLong �֐��̉����łł��B32 �r�b�g�� Windows �� 64 �r�b�g�� Windows �̗����Ƃ��ƌ݊����̂���R�[�h���L�q����ɂ́AGetWindowLongPtr �֐����g���Ă��������B
		/// </summary>
		/// <param name="hWnd">�E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <param name="nIndex">�g���E�B���h�E���������̒l��ύX����ɂ́A0 ����n�܂�I�t�Z�b�g���w�肵�܂��B�L���ȃI�t�Z�b�g�́A0 ����g���E�B���h�E�������̃o�C�g�� -8 �܂łł��B���Ƃ��΁A�g���������� 24 �o�C�g�ȏ゠��ꍇ�A16 ���w�肷��ƁA3 �Ԗڂ̐����l��ύX�ł��܂��B</param>
		/// <param name="dwNewLong">�V�����l���w�肵�܂��B</param>
		/// <returns>�֐�����������ƁA�ύX�������̕ύX�O�̒l���Ԃ�܂��B�֐������s����ƁA0 ���Ԃ�܂��B�g���G���[�����擾����ɂ́AGetLastError �֐����g���܂��B</returns>
		/// <remarks>�g���E�B���h�E�������́ARegisterClassEx �֐��ɓn�� WNDCLASSEX �\���̂� cbWndExtra �����o�� 0 �ȊO�̒l��ݒ肷�邱�Ƃɂ���Ċm�ۂ��܂��B</remarks>
		public static System.IntPtr SetWindowLongPtr(System.IntPtr hWnd,int nIndex,System.IntPtr dwNewLong){
			return System.IntPtr.Size==8?SetWindowLongPtr64(hWnd,nIndex,dwNewLong):(System.IntPtr)SetWindowLongPtr32(hWnd,nIndex,dwNewLong);
		}
		/// <summary>
		/// �w�肳�ꂽ�E�B���h�E�Ɋւ�������擾���܂��B
		/// </summary>
		/// <param name="hWnd">�E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <param name="nIndex">�ݒ肷����ɑΉ�����l���w�肵�܂��B</param>
		/// <param name="dwNewLong">�V�����l���w�肵�܂��B</param>
		/// <returns>�֐�����������ƁA�ύX�������̕ύX�O�̒l���Ԃ�܂��B�֐������s����ƁA0 ���Ԃ�܂��B�g���G���[�����擾����ɂ́AGetLastError �֐����g���܂��B</returns>
		/// <remarks>�g���E�B���h�E�������́ARegisterClassEx �֐��ɓn�� WNDCLASSEX �\���̂� cbWndExtra �����o�� 0 �ȊO�̒l��ݒ肷�邱�Ƃɂ���Ċm�ۂ��܂��B</remarks>
		public static System.IntPtr SetWindowLongPtr(System.IntPtr hWnd,GWLP nIndex,System.IntPtr dwNewLong){
			if(System.IntPtr.Size==8){
				int index=nIndex==GWLP.USER?16:nIndex==GWLP.DLGPROC?8:(int)nIndex;
				return SetWindowLongPtr64(hWnd,index,dwNewLong);
			}else{
				return (System.IntPtr)SetWindowLongPtr32(hWnd,(int)nIndex,dwNewLong);
			}
		}
		[Interop.DllImport("user32.dll",EntryPoint="SetWindowLong",SetLastError=true)]
		private static extern int SetWindowLongPtr32(System.IntPtr hWnd,int nIndex,System.IntPtr dwNewLong);
		[Interop.DllImport("user32.dll",EntryPoint="SetWindowLongPtr",SetLastError=true)]
		private static extern System.IntPtr SetWindowLongPtr64(System.IntPtr hWnd, int nIndex,System.IntPtr dwNewLong);
		/// <summary>
		/// ���̊֐��́AGetWindowLongPtr �֐��̌Â��łł��B32 �r�b�g�� Windows �� 64 �r�b�g�� Windows �̗����Ƃ��ƌ݊����̂���R�[�h���L�q����ɂ́AGetWindowLongPtr �֐����g���Ă��������B
		/// </summary>
		/// <param name="hWnd">�E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <param name="nIndex">�擾����l�� 0 ����n�܂�I�t�Z�b�g���w�肵�܂��B</param>
		/// <returns>�֐�����������ƁA�v�������l���Ԃ�܂��B�֐������s����ƁA0 ���Ԃ�܂��B</returns>
		[Interop.DllImport("user32.dll"),System.Obsolete("GetWindowLongPtr ���g���ĉ������B")]
		public static extern int GetWindowLong(System.IntPtr hWnd,int nIndex);
		/// <summary>
		/// ���̊֐��́ASetWindowLongPtr �֐��̌Â��łł��B32 �r�b�g�� Windows �� 64 �r�b�g�� Windows �̗����Ƃ��ƌ݊����̂���R�[�h���L�q����ɂ́AGetWindowLongPtr �֐����g���Ă��������B
		/// </summary>
		/// <param name="hWnd">�E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <param name="nIndex">�ݒ肷��l�� 0 ����n�܂�I�t�Z�b�g���w�肵�܂��B</param>
		/// <param name="dwNewLong">�V�����l��ݒ肵�܂��B</param>
		/// <returns>�֐�����������ƁA�v�������l���Ԃ�܂��B�֐������s����ƁA0 ���Ԃ�܂��B</returns>
		[Interop.DllImport("user32.dll"),System.Obsolete("SetWindowLongPtr ���g���ĉ������B")]
		public static extern int SetWindowLong(System.IntPtr hWnd,int nIndex,System.IntPtr dwNewLong);
		/// <summary>
		/// GetWindowLongPtr �y�� SetWindowLongPtr ���g�p���āA
		/// �E�B���h�E�Ɋւ�������擾���͐ݒ肷��ۂɎw�肷��l�ł��B
		/// </summary>
		public enum GWLP:int{
			/// <summary>
			/// �g���E�B���h�E�X�^�C�����擾�E�ݒ肵�܂��B 
			/// </summary>
			EXSTYLE=-20,
			/// <summary>
			/// �E�B���h�E�X�^�C�����擾�E�ݒ肵�܂��B 
			/// </summary>
			STYLE=-16,
			/// <summary>
			/// �E�B���h�E�v���V�[�W���ւ̃|�C���^�A�܂��̓E�B���h�E�v���V�[�W���ւ̃|�C���^��\���n���h�����擾�E�ݒ肵�܂��B�E�B���h�E�v���V�[�W�����Ăяo���ɂ́ACallWindowProc �֐����g��Ȃ���΂Ȃ�܂���B 
			/// </summary>
			/// <remarks>
			/// SetWindowLongPtr �֐��ɂ��E�B���h�E�v���V�[�W����ύX����ꍇ�A�V�����E�B���h�E�v���V�[�W���́AWindowProc �R�[���o�b�N�֐��̐����Ɏ�����Ă���K�C�h���C���ɏ]���Ă��Ȃ���΂Ȃ�܂���B
			/// ���̒l���w�肵�� SetWindowLongPtr �֐����Ăяo���ƁA�E�B���h�E�N���X�̃T�u�N���X���쐬����A���ꂪ�E�B���h�E�̍쐬�Ɏg����悤�ɂȂ�܂��B
			/// �V�X�e���N���X�̃T�u�N���X�͍쐬���Ă����܂��܂��񂪁A���̃v���Z�X�Ő������ꂽ�E�B���h�E�N���X�̃T�u�N���X�͍쐬���Ȃ��悤�ɂ��Ă��������B
			/// SetWindowLongPtr �֐��́A�E�B���h�E�N���X�Ɋ֘A�t����ꂽ�E�B���h�E�v���V�[�W����ύX���邱�Ƃɂ��E�B���h�E�N���X���T�u�N���X�����邽�߁A����ȍ~�V�X�e���͕ύX�O�̃E�B���h�E�v���V�[�W���ł͂Ȃ��V�����E�B���h�E�v���V�[�W�����Ăяo���悤�ɂȂ�܂��B
			/// �V�����E�B���h�E�v���V�[�W���ł͏�������Ȃ��悤�ȃ��b�Z�[�W�́ACallWindowProc �֐����Ăяo�����Ƃɂ���ĕύX�O�̃E�B���h�E�v���V�[�W���֓n���Ȃ���΂Ȃ�܂���B����́A�A�v���P�[�V�������E�B���h�E�v���V�[�W���̃`�F�C������邱�Ƃ��\�ɂ��܂��B
			/// </remarks>
			WNDPROC=-4,
			/// <summary>
			/// �A�v���P�[�V�����C���X�^���X�̃n���h�����擾�E�ݒ肵�܂��B 
			/// </summary>
			HINSTANCE=-6,
			/// <summary>
			/// �e�E�B���h�E������ꍇ�A���̃n���h�����擾���܂��B
			/// </summary>
			/// <remarks>
			/// ���̒l���w�肵�� SetWindowLongPtr �֐����Ăяo�����Ƃɂ��q�E�B���h�E�̐e��ύX���Ă͂Ȃ�܂���B
			/// �e�E�B���h�E�̕ύX�ɂ́ASetParent �֐����g���Ă��������B
			/// </remarks>
			HWNDPARENT=-8,
			/// <summary>
			/// �E�B���h�E ID ���擾�E�ݒ肵�܂��B 
			/// </summary>
			ID=-12,
			/// <summary>
			/// �E�B���h�E�Ɋ֘A�t����ꂽ 32 �r�b�g�l���擾�E�ݒ肵�܂��B���� 32 �r�b�g�l�́A�E�B���h�E���쐬�����A�v���P�[�V�����Ŏg�p����ړI�Ŋe�E�B���h�E�������Ă�����̂ł��B���̒l�̏����l�� 0 �ł��B 
			/// </summary>
			USERDATA=-21,
			/// <summary>
			/// hWnd �p�����[�^�Ń_�C�A���O�{�b�N�X���w�肵�Ă���Ƃ��w��ł��܂��B 
			/// �_�C�A���O�{�b�N�X�v���V�[�W���ւ̃|�C���^�A�܂��̓_�C�A���O�{�b�N�X�v���V�[�W���ւ̃|�C���^��\���n���h�����擾�E�ݒ肵�܂��B�_�C�A���O�{�b�N�X�v���V�[�W�����Ăяo���ɂ́ACallWindowProc �֐����g��Ȃ���΂Ȃ�܂���B 
			/// </summary>
			DLGPROC=4,
			/// <summary>
			/// hWnd �p�����[�^�Ń_�C�A���O�{�b�N�X���w�肵�Ă���Ƃ��w��ł��܂��B 
			/// �_�C�A���O�{�b�N�X�v���V�[�W�����ŏ������ꂽ���b�Z�[�W�̖߂�l���擾�E�ݒ肵�܂��B 
			/// </summary>
			MSGRESULT=0,
			/// <summary>
			/// hWnd �p�����[�^�Ń_�C�A���O�{�b�N�X���w�肵�Ă���Ƃ��w��ł��܂��B 
			/// �n���h����|�C���^�Ȃǂ̃A�v���P�[�V�����ŗL�̊g�������擾�E�ݒ肵�܂��B 
			/// </summary>
			USER=8
		}
		#endregion

		#region WindowInfo
		[Interop.DllImport("user32.dll")]
		public static extern bool GetWindowInfo(System.IntPtr hwnd, ref WINDOWINFO pwi);
		#endregion

		#region Parent
		/// <summary>
		/// �w�肳�ꂽ�q�E�B���h�E�̐e�E�B���h�E��ύX���܂��B
		/// </summary>
		/// <param name="hWndChild">�q�E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <param name="hWndNewParent">�V�����e�E�B���h�E���w�肵�܂��BNULL ���w�肷��ƁA�f�X�N�g�b�v�E�B���h�E���V�����e�E�B���h�E�ɂȂ�܂��B
		/// Windows 2000�F���̃p�����[�^�� HWND_MESSAGE ���w�肷��ƁA�q�E�B���h�E�̓��b�Z�[�W��p�E�B���h�E �ɂȂ�܂��B</param>
		/// <returns>�֐�����������ƁA���O�̐e�E�B���h�E�̃n���h�����Ԃ�܂��B
		/// �֐������s����ƁANULL ���Ԃ�܂��B�g���G���[�����擾����ɂ́AGetLastError �֐����g���܂��B</returns>
		/// <remarks>
		/// SetParent �֐����g���ƁA�|�b�v�A�b�v�E�B���h�E�A�I�[�o�[���b�v�E�B���h�E�A�q�E�B���h�E�̐e�E�B���h�E��ݒ�ł��܂��B
		/// �V�����e�E�B���h�E�Ǝq�E�B���h�E�́A����A�v���P�[�V�����ɑ����Ă��Ȃ���΂Ȃ�܂���B
		/// hWndChild �p�����[�^�Ŏw�肵���E�B���h�E����ʂɕ\������Ă���ꍇ�́A�V�X�e���ɂ��K�؂ȍĕ`�悪���s����܂��B
		/// <para>
		/// SetParent �֐��́A�݊����̗��R����A�e�E�B���h�E���ύX���ꂽ�E�B���h�E�̃E�B���h�E�X�^�C�� WS_CHILD �� WS_POPUP �̕ύX�͍s���܂���B
		/// ���̂��߁AhWndNewParent �p�����[�^�� NULL ���w�肷��ꍇ�́ASetParent �֐����Ăяo������� WS_CHILD �t���O���N���A���AWS_POPUP �t���O���Z�b�g���Ȃ���΂Ȃ�܂���B
		/// �t�ɁA�f�X�N�g�b�v�̎q�E�B���h�E�ɑ΂��� hWndNewParent �p�����[�^�� NULL �ȊO�̒l���w�肵�� SetParent �֐����Ăяo���ꍇ�́A�֐����Ăяo���O�� WS_POPUP �t���O���N���A���AWS_CHILD �t���O���Z�b�g���Ă����Ȃ���΂Ȃ�܂���B
		/// </para>
		/// <para>
		/// Windows 2000�F�E�B���h�E�̐e��ύX����ꍇ�́A�����̃E�B���h�E�� UISTATE �𓯊������Ȃ���΂Ȃ�܂���B
		/// �ڍׂɂ��ẮAWM_CHANGEUISTATE ���b�Z�[�W����� WM_UPDATEUISTATE ���b�Z�[�W�̐������Q�Ƃ��Ă��������B
		/// </para>
		/// </remarks>
		[Interop.DllImport("user32.dll")]
		static extern System.IntPtr SetParent(System.IntPtr hWndChild,System.IntPtr hWndNewParent);
		/// <summary>
		/// �w�肳�ꂽ�q�E�B���h�E�̐e�E�B���h�E�܂��̓I�[�i�[�E�B���h�E�̃n���h����Ԃ��܂��B
		/// </summary>
		/// <param name="hWnd">�q�E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <returns>�w�肵���E�B���h�E���q�E�B���h�E�̏ꍇ�́A�e�E�B���h�E�̃n���h�����Ԃ�܂��B
		/// �w�肵���E�B���h�E���g�b�v���x���E�B���h�E�̏ꍇ�́A�I�[�i�[�E�B���h�E�̃n���h�����Ԃ�܂��B
		/// �w�肵���E�B���h�E���g�b�v���x���̃I�[�i�[�������Ȃ��E�B���h�E�������ꍇ�A����ъ֐������s�����ꍇ�� NULL ���Ԃ�܂��B
		/// �g���G���[�����擾����ɂ́AGetLastError �֐����g���܂��B</returns>
		/// <remarks>���̊֐��̖��O�� GetParent �ł����A�e�E�B���h�E�ł͂Ȃ��I�[�i�[�E�B���h�E���Ԃ����ꍇ������܂��B
		/// �e�E�B���h�E�������擾�������ꍇ�́AGA_PARENT �t���O���w�肵�� GetAncestor �֐����Ăяo���Ă��������B</remarks>
		[Interop.DllImport("user32.dll",ExactSpelling=true,CharSet=Interop.CharSet.Auto)]
		public static extern System.IntPtr GetParent(System.IntPtr hWnd);
		/// <summary>
		/// �w�肵���E�B���h�E�̑c��̃n���h�����擾���܂��B
		/// </summary>
		/// <param name="hwnd">�c����擾����E�B���h�E�̃n���h�����w�肵�܂��B�f�X�N�g�b�v�E�B���h�E�̃n���h�����w�肷��ƁANULL ���Ԃ�܂��B</param>
		/// <param name="gaFlags">�擾����c����w�肵�܂��B</param>
		/// <returns>�c��̃E�B���h�E�̃n���h�����Ԃ�܂��B</returns>
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr GetAncestor(System.IntPtr hwnd,GA gaFlags);
		/// <summary>
		/// �擾��������c�̎�ނ��w�肷��̂Ɏg�p���܂��B
		/// </summary>
		public enum GA{
			///<summary>
			///�e�E�B���h�E���擾���܂��B����ɂ́AGetParent �֐��Ŏ擾�����悤�ȁA�I�[�i�[�E�B���h�E�͊܂݂܂���B 
			///</summary>
			PARENT=1,
			///<summary>
			///�e�E�B���h�E�̃`�F�[�������ǂ��ă��[�g�E�B���h�E���擾���܂��B 
			///</summary>
			ROOT=2,
			///<summary>
			///GetParent �֐����Ԃ��e�E�B���h�E�̃`�F�[�������ǂ��ď��L����Ă��郋�[�g�E�B���h�E���擾���܂��B 
			///</summary>
			ROOTOWNER=3
		}
		#endregion

		#region func:Is*
		/// <summary>
		/// �w�肵���E�B���h�E���ŏ�������Ă��邩�ǂ������擾���܂��B
		/// </summary>
		/// <param name="hWnd">�E�B���h�E���n���h���Ŏw�肵�܂��B</param>
		/// <returns>�w�肳�ꂽ�E�B���h�E���ŏ�������Ă���ꍇ�� true ��Ԃ��܂��B</returns>
		[Interop.DllImport("user32.dll")]
		[return:Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		public static extern bool IsIconic(System.IntPtr hWnd);
		/// <summary>
		/// �w�肳�ꂽ�E�B���h�E���ő剻����Ă��邩�ǂ������擾���܂��B
		/// </summary>
		/// <param name="hWnd">�E�B���h�E���n���h���Ŏw�肵�܂��B</param>
		/// <returns>�w�肳�ꂽ�E�B���h�E���ő剻����Ă���ꍇ�� true ��Ԃ��܂��B</returns>
		[Interop.DllImport("user32.dll")]
		[return:Interop.MarshalAs(Interop.UnmanagedType.Bool)]
		public static extern bool IsZoomed(System.IntPtr hWnd);
		/// <summary>
		/// �w�肳�ꂽ�E�B���h�E�n���h�������E�B���h�E�����݂��Ă��邩�ǂ����𒲂ׂ܂��B
		/// </summary>
		/// <param name="hWnd">��������E�B���h�E�n���h�����w�肵�܂��B </param>
		/// <returns>�w�肵���E�B���h�E�n���h�������E�B���h�E�����݂��Ă���ꍇ�� true ��Ԃ��܂��B</returns>
		[Interop.DllImport("user32.dll")]
		public static extern bool IsWindow(System.IntPtr hWnd);
		//[Interop.DllImport("user32.dll")]
		//public static extern bool IsWindowEnabled(System.IntPtr hWnd);
		/// <summary>
		/// �w�肵���E�B���h�E�̕\����Ԃ��擾���܂��B
		/// </summary>
		/// <param name="hWnd">��������E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <returns>
		/// �w�肵���E�B���h�E�ƁA���̐�c�ɓ�����E�B���h�E�̑S�Ă� WS_VISIBLE �X�^�C�������ꍇ�� true ��Ԃ��܂��B
		/// �߂�l�� true �ł����Ă��A�E�B���h�E���͕̂ʂ̃E�B���h�E�ɉB����Č����Ȃ��ꍇ������܂��B
		/// </returns>
		[Interop.DllImport("user32.dll")]
		public static extern bool IsWindowVisible(System.IntPtr hWnd);
		/// <summary>
		/// �w�肵���E�B���h�E�� Unicode �̃l�C�e�B�u�E�B���h�E�ł��邩�ǂ����𒲂ׂ܂��B
		/// </summary>
		/// <param name="hWnd">��������E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <returns>
		/// �w�肵���E�B���h�E�� Unicode �̃l�C�e�B�u�E�B���h�E�ł���ꍇ�� true ��Ԃ��܂��B
		/// RegisterClassA ���g���ăE�B���h�E�N���X��o�^�����ꍇ�ɂ� ANSI �l�C�e�B�u�ɂȂ�܂��B
		/// RegisterClassW ���g���ăE�B���h�E�N���X��o�^�����ꍇ�ɂ� Unicode �l�C�e�B�u�ɂȂ�܂��B
		/// </returns>
		[Interop.DllImport("user32.dll")]
		public static extern bool IsWindowUnicode(System.IntPtr hWnd);
		/// <summary>
		/// �w�肳�ꂽ�E�B���h�E���A�ʂɎw�肳�ꂽ�E�B���h�E�̎q�E�B���h�E�i�܂��͎q���̃E�B���h�E�j�ł��邩�ǂ����𒲂ׂ܂��B
		/// �e�E�B���h�E���q�E�B���h�E�̐e�E�B���h�E�`�F�C���ɓ����Ă���ꍇ�A���̎q�E�B���h�E�͎w�肳�ꂽ�e�E�B���h�E�̒��ڂ̎q���ł��B
		/// �e�E�B���h�E�`�F�C���́A�q�E�B���h�E�̌��̃I�[�o�[���b�v�E�B���h�E�܂��̓|�b�v�A�b�v�E�B���h�E����Ȃ����Ă��܂��B
		/// </summary>
		/// <param name="hWndParent">�e�E�B���h�E�̃n���h�����w�肵�܂��B </param>
		/// <param name="hWnd">��������E�B���h�E�̃n���h�����w�肵�܂��B</param>
		/// <returns>�����ΏۃE�B���h�E���w�肵���e�E�B���h�E�̎q�E�B���h�E�܂��͎q���E�B���h�E�̏ꍇ�� true ��Ԃ��܂��B</returns>
		[Interop.DllImport("user32.dll")]
		public static extern bool IsChild(System.IntPtr hWndParent,System.IntPtr hWnd);
		#endregion

		#region �����񏈗�
		/// <summary>
		/// �w�肳�ꂽ�������p�����ǂ����𔻒f���܂��B
		/// ���[�U�[���Z�b�g�A�b�v�̍ۂɑI����������A�܂��̓R���g���[���p�l���őI����������Ɋ�Â��āA���̌�����s���܂��B
		/// </summary>
		/// <param name="c">�e�X�g�̑Ώۂ̕������w�肵�܂��B</param>
		/// <returns>�w�肵���������p���ł���ꍇ�� true ��Ԃ��܂��B</returns>
		public static bool IsCharAlpha(char c){return char.IsLetter(c);}
		/// <summary>
		/// �w�肵���������p�����͐������ǂ����𔻒f���܂��B
		/// </summary>
		/// <param name="c">�e�X�g�̑Ώۂ̕������w�肵�܂��B</param>
		/// <returns>�w�肵���������p�����͐����ł���ꍇ�� true ��Ԃ��܂��B</returns>
		public static bool IsCharAlphaNumeric(char c){return char.IsLetterOrDigit(c);}
		/// <summary>
		/// �w�肵���������啶�����ǂ����𔻒f���܂��B
		/// </summary>
		/// <param name="c">�e�X�g�̑Ώۂ̕������w�肵�܂��B</param>
		/// <returns>�w�肵���������啶���ł���ꍇ�� true ��Ԃ��܂��B</returns>
		public static bool IsCharUpper(char c){return char.IsUpper(c);}
		/// <summary>
		/// �w�肵�����������������ǂ����𔻒f���܂��B
		/// </summary>
		/// <param name="c">�e�X�g�̑Ώۂ̕������w�肵�܂��B</param>
		/// <returns>�w�肵���������������ł���ꍇ�� true ��Ԃ��܂��B</returns>
		public static bool IsCharLower(char c){return char.IsLower(c);}
		/// <summary>
		/// ��������啶���ɕϊ����܂��B
		/// </summary>
		/// <param name="c">�ϊ��O�̕������w�肵�܂��B�ϊ��̌��ʂ������Ɋi�[����܂��B</param>
		/// <returns>�ϊ���̕�����Ԃ��܂��B</returns>
		public static char CharUpper(ref char c){return c=char.ToUpper(c);}
		/// <summary>
		/// �w�肵��������Ɋ܂܂�鏬������啶���ɕϊ����܂��B
		/// </summary>
		/// <param name="lpsz">�ϊ��O�̕�������w�肵�܂��B�ϊ��̌��ʂ������Ɋi�[����܂��B</param>
		/// <returns>�ϊ���̕������Ԃ��܂��B</returns>
		public static string CharUpper(ref string lpsz){return lpsz=lpsz.ToUpper();}
		/// <summary>
		/// �w�肵��������̎w�肵�����̕���������������啶���ɕϊ����܂��B
		/// </summary>
		/// <param name="lpsz">�ϊ��O�̕�������w�肵�܂��B�ϊ���̕����񂪊i�[����܂��B</param>
		/// <param name="cchLength">�ϊ����镶�������w�肵�܂��B</param>
		/// <returns>���ۂɏ������ꂽ��������Ԃ��܂��B(�������łȂ������������܂݂܂��B)</returns>
		public static uint CharUpperBuff(ref string lpsz,uint cchLength){
			int len=lpsz.Length;
			if(len>cchLength){
				lpsz=lpsz.Substring(0,len).ToUpper()+lpsz.Substring(len);
				return cchLength;
			}else{
				lpsz=lpsz.ToUpper();
				return (uint)len;
			}
		}
		/// <summary>
		/// �啶�����������ɕϊ����܂��B
		/// </summary>
		/// <param name="c">�ϊ��O�̕������w�肵�܂��B�ϊ��̌��ʂ������Ɋi�[����܂��B</param>
		/// <returns>�ϊ���̕�����Ԃ��܂��B</returns>
		public static char CharLower(ref char c){return c=char.ToLower(c);}
		/// <summary>
		/// �w�肵��������Ɋ܂܂��啶�����������ɕϊ����܂��B
		/// </summary>
		/// <param name="lpsz">�ϊ��O�̕�������w�肵�܂��B�ϊ��̌��ʂ������Ɋi�[����܂��B</param>
		/// <returns>�ϊ���̕������Ԃ��܂��B</returns>
		public static string CharLower(ref string lpsz){return lpsz=lpsz.ToLower();}
		/// <summary>
		/// �w�肵��������̎w�肵�����̕�����啶�����珬�����ɕϊ����܂��B
		/// </summary>
		/// <param name="lpsz">�ϊ��O�̕�������w�肵�܂��B�ϊ���̕����񂪊i�[����܂��B</param>
		/// <param name="cchLength">�ϊ����镶�������w�肵�܂��B</param>
		/// <returns>���ۂɏ������ꂽ��������Ԃ��܂��B(�啶���łȂ������������܂݂܂��B)</returns>
		public static uint CharLowerBuff(ref string lpsz,uint cchLength){
			int len=lpsz.Length;
			if(len>cchLength){
				lpsz=lpsz.Substring(0,len).ToLower()+lpsz.Substring(len);
				return cchLength;
			}else{
				lpsz=lpsz.ToLower();
				return (uint)len;
			}
		}
		/// <summary>
		/// ������̒��̎��̕����ւ̃|�C���^���擾���܂��B
		/// ��������̕����̗񋓂��s�������̂Ȃ� str[index] (String string) ���g�p���邩�A
		/// enm.MoveNext (Enumerator enm=str.GetEnumerator) ���g�p���ĉ������B
		/// </summary>
		/// <param name="lpsz">NULL �ŏI��镶����̒��� 1 �̕����ւ̃|�C���^���w�肵�܂��B </param>
		/// <returns>
		/// �w�肵��������̒��̎��̕����ւ̃|�C���^���Ԃ�܂��Blpsz �p�����[�^�ŁA������̒��̍Ō�̕������w�肵���ꍇ�ANULL �����ւ̃|�C���^���Ԃ�܂��B
		/// lpsz �p�����[�^�ŁA������̍Ō�� NULL �������w�肵���ꍇ�Alpsz �p�����[�^�Ɠ����l���Ԃ�܂��B
		/// </returns>
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr CharNext(System.IntPtr lpsz);
		/// <summary>
		/// ������̒��̎��̕����ւ̃|�C���^���擾���܂��B
		/// ��������̕����̗񋓂��s�������̂Ȃ� str[index] (String string) ���g�p���ĉ������B
		/// </summary>
		/// <param name="lpszStart">NULL �ŏI��镶����̒��̍ŏ��̕����ւ̃|�C���^���w�肵�܂��B</param>
		/// <param name="lpszCurrent">NULL �ŏI��镶����̒��� 1 �̕����ւ̃|�C���^���w�肵�܂��B���̕����� 1 �O�̕����ւ̃|�C���^���Ԃ�܂��B</param>
		/// <returns>
		/// �w�肵��������̒��̎��̕����ւ̃|�C���^���Ԃ�܂��Blpsz �p�����[�^�ŁA������̒��̍Ō�̕������w�肵���ꍇ�ANULL �����ւ̃|�C���^���Ԃ�܂��B
		/// lpsz �p�����[�^�ŁA������̍Ō�� NULL �������w�肵���ꍇ�Alpsz �p�����[�^�Ɠ����l���Ԃ�܂��B
		/// </returns>
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr CharPrev(System.IntPtr lpszStart,System.IntPtr lpszCurrent);
		/// <summary>
		/// �w�肳�ꂽ��������AOEM ��`�̕����Z�b�g�֕ϊ����܂��B.NET �ł͈Ӗ�����܂���B
		/// </summary>
		/// <param name="lpszSrc">�ϊ�����ׂ��ANULL �����ŏI��镶����ւ̃|�C���^���w�肵�܂��B </param>
		/// <param name="lpszDst">
		/// 1 �̃o�b�t�@�ւ̃|�C���^���w�肵�܂��B�֐����琧�䂪�Ԃ�ƁA���̃o�b�t�@�ɁA�ϊ���̕����񂪊i�[����܂��BANSI �ł� CharToOem �֐����g���ꍇ�AlpszDst �p�����[�^�� lpszSrc �p�����[�^�Ɠ����A�h���X���w�肷��ƁA���̏�ŕϊ����s���A���̕�������㏑�����܂��B
		/// Unicode �ł� CharToOem �֐����g���ꍇ�AlpszSrc �p�����[�^�Ɠ����A�h���X���w�肷�邱�Ƃ͂ł��܂���B</param>
		/// <returns>��� true ���Ԃ�܂��B</returns>
		[Interop.DllImport("user32.dll"),System.Obsolete(".NET �̕�����ɑ΂��Ă��̑�������鎖�͑S���Ӗ����ׂ��܂���B")]
		public static extern bool CharToOem(
			[Interop.MarshalAs(Interop.UnmanagedType.LPTStr)]string lpszSrc,
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]string lpszDst
			);
		/// <summary>
		/// �w�肳�ꂽ��������AOEM ��`�̕����Z�b�g�֕ϊ����܂��B.NET �ł͈Ӗ�����܂���B
		/// </summary>
		/// <param name="lpszSrc">�ϊ�����ׂ��ANULL �����ŏI��镶����ւ̃|�C���^���w�肵�܂��B </param>
		/// <param name="lpszDst">
		/// 1 �̃o�b�t�@�ւ̃|�C���^���w�肵�܂��B�֐����琧�䂪�Ԃ�ƁA���̃o�b�t�@�ɁA�ϊ���̕����񂪊i�[����܂��BANSI �ł� CharToOemBuff �֐����g���ꍇ�AlpszDst �p�����[�^�� lpszSrc �p�����[�^�Ɠ����A�h���X���w�肷��ƁA���̏�ŕϊ����s���A���̕�������㏑�����܂��B
		/// Unicode �ł� CharToOemBuff �֐����g���ꍇ�AlpszSrc �p�����[�^�Ɠ����A�h���X���w�肷�邱�Ƃ͂ł��܂���B
		/// </param>
		/// <param name="cchDstLength">lpszSrc �p�����[�^�Ŏw�肳�ꂽ������̂����A�ϊ�����ׂ������̐��� TCHAR �P�ʂŎw�肵�܂��B</param>
		/// <returns>��� true ���Ԃ�܂��B</returns>
		[Interop.DllImport("user32.dll"),System.Obsolete(".NET �̕�����ɑ΂��Ă��̑�������鎖�͑S���Ӗ����ׂ��܂���B")]
		public static extern bool CharToOemBuff(
			[Interop.MarshalAs(Interop.UnmanagedType.LPTStr)]string lpszSrc,
			[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]ref string lpszDst,
			uint cchDstLength
			);
		/// <summary>
		/// �w�肳�ꂽ���W���[���Ɋ֘A�t�����Ă�����s�\�t�@�C�����當���񃊃\�[�X�����[�h���A�o�b�t�@�փR�s�[���A�Ō�� 1 �� NULL ������ǉ����܂��B
		/// </summary>
		/// <param name="hInstance">���W���[���C���X�^���X�̃n���h�����w�肵�܂��B���̃��W���[���̎��s�\�t�@�C���́A���[�h����ׂ�������̃��\�[�X��ێ����Ă��܂��B</param>
		/// <param name="uID">���[�h����ׂ�������̐����̎��ʎq���w�肵�܂��B</param>
		/// <param name="lpBuffer">�o�b�t�@�ւ̃|�C���^���w�肵�܂��B�֐����琧�䂪�Ԃ�ƁA���̃o�b�t�@�ɁANULL �ŏI��镶���񂪊i�[����܂��B</param>
		/// <param name="nBufferMax">�o�b�t�@�̃T�C�Y�� TCHAR �P�ʂŎw�肵�܂��B�o�b�t�@�̃T�C�Y���s�����āA�w�肳�ꂽ������̈ꕔ���i�[�ł��Ȃ��ꍇ�A������͓r���Ő؂�̂Ă��܂��B</param>
		/// <returns>�֐�����������ƁA�o�b�t�@�ɃR�s�[���ꂽ�����̐��� TCHAR �P�ʂŕԂ�܂��i�I�[�� NULL �͊܂܂Ȃ��j�B�����񃊃\�[�X�����݂��Ȃ��ꍇ�A0 ���Ԃ�܂��B�g���G���[�����擾����ɂ́AGetLastError �֐����g���܂��B</returns>
		[Interop.DllImport("user32.dll",SetLastError=true)]
		public static extern int LoadString(System.IntPtr hInstance,uint uID,ref string lpBuffer,int nBufferMax);
		//TODO:CharNextExA CharPrevExA CompareString FoldString GetStringTypeEx GetStringType wsprintf
		//http://msdn.microsoft.com/library/ja/default.asp?url=/library/ja/vclib/html/_crt_va_arg.2c_.va_end.2c_.va_start.asp
		//http://www.microsoft.com/japan/msdn/net/general/win32map.aspx
#if obsolete16
		public static string AnsiUpper(ref char c){return CharUpper(c);}
		public static string AnsiUpper(ref string lpsz){return CharUpper(lpsz);}
		public static uint AnsiUpperBuff(ref string lpsz,uint cchLength){return CharUpperBuff(lpsz,cchLength);}
		public static string AnsiLower(ref char c){return CharUpper(c);}
		public static string AnsiLower(ref string lpsz){return CharUpper(lpsz);}
		public static uint AnsiLowerBuff(ref string lpsz,uint cchLength){return CharUpperBuff(lpsz,cchLength);}
		public static System.IntPtr AnsiNext(System.IntPtr lpsz){return CharNext(lpsz);}
		public static System.IntPtr AnsiPrev(System.IntPtr lpszStart,System.IntPtr lpszCurrent){return CharPrev(lpszStart,lpszCurrent);}
		public static extern bool AnsiToOem(string lpszSrc,string lpszDst){return CharToOemBuff(lpszSrc,lpszDst);}
		public static extern bool AnsiToOemBuff(string lpszSrc,string lpszDst,uint cchDstLength){return CharToOemBuff(lpszSrc,lpszDst,cchDstLength);}
#endif
		#endregion

		/*


		*/

		#region func:GetWindow*
		[Interop.DllImport("user32.dll", CharSet=Interop.CharSet.Auto)]
		public static extern int GetClassName(System.IntPtr hWnd,System.Text.StringBuilder lpClassName,int nMaxCount);
		//--Window Text
		/// <summary>
		/// �w�肳�ꂽ�E�B���h�E�̃^�C�g���o�[�̃e�L�X�g���o�b�t�@�փR�s�[���܂��B
		/// �w�肳�ꂽ�E�B���h�E���R���g���[���̏ꍇ�́A�R���g���[���̃e�L�X�g���R�s�[���܂��B
		/// �������A���̃A�v���P�[�V�����̃R���g���[���̃e�L�X�g���擾���邱�Ƃ͂ł��܂���B
		/// </summary>
		/// <param name="hWnd">�E�B���h�E�i �܂��̓e�L�X�g�����R���g���[���j�̃n���h�����w�肵�܂��B</param>
		/// <param name="lpString">�o�b�t�@�ւ̃|�C���^���w�肵�܂��B���̃o�b�t�@�Ƀe�L�X�g���i�[����܂��B</param>
		/// <param name="nMaxCount">�o�b�t�@�ɃR�s�[���镶���̍ő吔���w�肵�܂��B�e�L�X�g�̂��̃T�C�Y�𒴂��镔���́A�؂�̂Ă��܂��BNULL ���������Ɋ܂߂��܂��B</param>
		/// <returns>�֐�����������ƁA�R�s�[���ꂽ������̕��������Ԃ�܂��i �I�[�� NULL �����͊܂߂��܂���j�B
		/// ����ȊO�̏ꍇ�A0 ���Ԃ�܂��B�g���G���[�����擾����ɂ́A�֐����g���܂��B</returns>
		[Interop.DllImport("user32.dll",SetLastError=true,CharSet=Interop.CharSet.Auto)]
		public static extern int GetWindowText(System.IntPtr hWnd,[Interop.Out]System.Text.StringBuilder lpString,int nMaxCount);
		[Interop.DllImport("user32.dll",SetLastError=true,CharSet=Interop.CharSet.Auto)]
		public static extern int GetWindowTextLength(System.IntPtr hWnd);
		/// <summary>
		/// �w�肳�ꂽ�E�B���h�E�̃^�C�g���o�[�̃e�L�X�g���擾���܂��B
		/// �w�肳�ꂽ�E�B���h�E���R���g���[���̏ꍇ�́A�R���g���[���̃e�L�X�g���擾���܂��B
		/// �������A���̃A�v���P�[�V�����̃R���g���[���̃e�L�X�g���擾���邱�Ƃ͂ł��܂���B
		/// </summary>
		/// <param name="hWnd">�E�B���h�E�i �܂��̓e�L�X�g�����R���g���[���j�̃n���h�����w�肵�܂��B</param>
		/// <param name="lpString">�E�B���h�E�e�L�X�g�̕������Ԃ��܂��B</param>
		/// <returns>�擾�����E�B���h�E�e�L�X�g�̕��������Ԃ�܂��B����ȊO�̏ꍇ�A0 ���Ԃ�A�g���G���[��񂪐ݒ肳��܂��B</returns>
		public static int GetWindowText(System.IntPtr hWnd,out string lpString){
			int len=GetWindowTextLength(hWnd);
			System.Text.StringBuilder buffer=new System.Text.StringBuilder(len+1);
			int ret=GetWindowText(hWnd,buffer,buffer.Capacity);
			lpString=buffer.ToString();
			return ret;
		}
		/// <summary>
		/// �w�肳�ꂽ�E�B���h�E�̃^�C�g���o�[�̃e�L�X�g���擾���܂��B
		/// �w�肳�ꂽ�E�B���h�E���R���g���[���̏ꍇ�́A�R���g���[���̃e�L�X�g���擾���܂��B
		/// �������A���̃A�v���P�[�V�����̃R���g���[���̃e�L�X�g���擾���邱�Ƃ͂ł��܂���B
		/// </summary>
		/// <param name="hWnd">�E�B���h�E�i �܂��̓e�L�X�g�����R���g���[���j�̃n���h�����w�肵�܂��B</param>
		/// <returns>�o�E�B���h�E�e�L�X�g�̕������Ԃ��܂��B�擾�Ɏ��s�����ꍇ�A�g���G���[����ݒ肵�� null ��Ԃ��܂��B</returns>
		public static string GetWindowText(System.IntPtr hWnd){
			int len=GetWindowTextLength(hWnd);
			System.Text.StringBuilder buffer=new System.Text.StringBuilder(len+1);
			int ret=GetWindowText(hWnd,buffer,buffer.Capacity);
			return ret>0?buffer.ToString():null;
		}
		[Interop.DllImport("user32.dll")]
		public static extern bool SetWindowText(System.IntPtr hWnd,string lpString);//[Interop.MarshalAs(Interop.UnmanagedType.LPStr)]
		[Interop.DllImport("user32.dll",SetLastError=true)]
		public static extern uint GetWindowThreadProcessId(System.IntPtr hWnd, out uint lpdwProcessId);
		public static uint GetWindowThreadProcessId(System.IntPtr hWnd){
			uint d;return GetWindowThreadProcessId(hWnd,out d);
		}
		#endregion

		#region func:WindowsHookEx
		[Interop.DllImport("user32.dll")]
		public static extern bool UnhookWindowsHookEx(System.IntPtr hhk);
		[Interop.DllImport("user32.dll")]
		public static extern int CallNextHookEx(System.IntPtr hhk,int nCode,System.IntPtr wParam,System.IntPtr lParam);
		[Interop.DllImport("user32.dll")]
		public static extern int CallNextHookEx(System.IntPtr hhk,int nCode,WM wParam,[Interop.In]KBDLLHOOKSTRUCT lParam);
		[Interop.DllImport("user32.dll")]
		public static extern int CallNextHookEx(System.IntPtr hhk,int nCode,WM wParam,[Interop.In]MSLLHOOKSTRUCT lParam);
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr SetWindowsHookEx(WH idHook,System.IntPtr procAddr,System.IntPtr hMod,uint dwThreadId);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="idHook">�t�b�N����v���V�[�W���̎�ނ� <see cref="WH"/> �Ŏw�肵�܂��B</param>
		/// <param name="callback"></param>
		/// <param name="hMod"></param>
		/// <param name="dwThreadId"></param>
		/// <returns></returns>
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr SetWindowsHookEx(WH idHook,HookProc callback,System.IntPtr hMod,uint dwThreadId);
		[Interop.DllImport("user32.dll")]
		public static extern System.IntPtr SetWindowsHookEx(WH hook,LowLevelKeyboardProc callback,System.IntPtr hMod,uint dwThreadId);
		[Interop.DllImport("user32.dll",SetLastError=true)]
		public static extern System.IntPtr SetWindowsHookEx(WH code,LowLevelMouseProc func,System.IntPtr hInstance,int threadID);
		#endregion
	}

	/// <summary>
	/// �E�B���h�E�Ɋ֘A�t�����Ă�������擾����̂Ɏg�p����\���̂ł��B
	/// </summary>
	[System.Serializable]
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public struct WINDOWINFO{
		/// <summary>
		/// ���̍\���̂̑傫����\���܂��B�Ăяo�����͂���� sizeof(WINDOWINFO) ��ݒ肷��K�v������܂��B
		/// </summary>
		public uint cbSize;
		/// <summary>
		/// �E�B���h�E�̗̈���擾���܂��B
		/// </summary>
		public RECT rcWindow;
		/// <summary>
		/// �N���C�A���g�̈���擾���܂��B
		/// </summary>
		public RECT rcClient;
		/// <summary>
		/// �E�B���h�E�̃X�^�C����ێ����܂��B
		/// </summary>
		public WS		dwStyle;
		/// <summary>
		/// �E�B���h�E�̊g���X�^�C����ێ����܂��B
		/// </summary>
		public WS_EX	dwExStyle;
		/// <summary>
		/// �E�B���h�E�̏�Ԃ�ێ����܂��B
		/// </summary>
		public WStatus	dwWindowStatus;
		/// <summary>
		/// �E�B���h�E�̐������E�̕���ێ����܂��B
		/// </summary>
		public uint cxWindowBorders;
		/// <summary>
		/// �E�B���h�E�̐������E�̍�����ێ����܂��B
		/// </summary>
		public uint cyWindowBorders;
		/// <summary>
		/// �E�B���h�E�N���X�̃A�g����ێ����܂��B
		/// </summary>
		public ushort atomWindowType;
		/// <summary>
		/// �E�B���h�E���쐬�����v���O�����̃o�[�W������ێ����܂��B
		/// </summary>
		public ushort wCreatorVersion;
		/// <summary>
		/// �E�B���h�E�̗̈���擾���͐ݒ肵�܂��B
		/// </summary>
		[CM.Description("�E�B���h�E�̗̈���擾���͐ݒ肵�܂��B")]
		public RECT Region{get{return this.rcWindow;}set{this.rcWindow=value;}}
		/// <summary>
		/// �N���C�A���g�̈���擾���͐ݒ肵�܂��B
		/// </summary>
		[CM.Description("�N���C�A���g�̈���擾���͐ݒ肵�܂��B")]
		public RECT ClientRegion{get{return this.rcClient;}set{this.rcClient=value;}}
	}
	public enum WStatus:uint{
		ZERO			=0x0000,
		ACTIVECAPTION	=0x0001
	}

	#region enum:VK
	/// <summary>
	/// Virtual Key ���z�L�[�R�[�h��\���񋓑̂ł��B
	/// </summary>
	public enum VK{
		//winuser.h ���
		/**<summary>�}�E�X�̍��{�^���������܂��B</summary>*/		LBUTTON=0x01,
		/**<summary>�}�E�X�̍��{�^���������܂��B</summary>*/		RBUTTON=0x02,
		/**<summary>�L�����Z���{�^���������܂��B</summary>*/		CANCEL=0x03,
		/**<summary>�}�E�X�̍��{�^���������܂��B</summary>*/		MBUTTON=0x04,    /* NOT contiguous with L & RBUTTON */

		//if(_WIN32_WINNT >= 0x0500)
		/**<summary>�}�E�X�̊g���{�^�� 1 �������܂��B</summary>*/	XBUTTON1=0x05,    /* NOT contiguous with L & RBUTTON */
		/**<summary>�}�E�X�̊g���{�^�� 2 �������܂��B</summary>*/	XBUTTON2=0x06,    /* NOT contiguous with L & RBUTTON */
		//endif /* _WIN32_WINNT >= 0x0500 */

		/*
			* 0x07 : unassigned
			*/

		BACK=0x08,
		TAB=0x09,

		/*
			* 0x0A - 0x0B : reserved
			*/

		CLEAR=0x0C,
		RETURN=0x0D,

		SHIFT=0x10,
		CONTROL=0x11,
		MENU=0x12,
		PAUSE=0x13,
		CAPITAL=0x14,
	
		//IME-MODE
		KANA=0x15,
		HANGEUL=0x15,  /* old name - should be here for compatibility */
		HANGUL=0x15,
		JUNJA=0x17,
		FINAL=0x18,
		HANJA=0x19,
		KANJI=0x19,

		ESCAPE=0x1B,
	
		//IME
		CONVERT=0x1C,
		NONCONVERT=0x1D,
		ACCEPT=0x1E,
		MODECHANGE=0x1F,

		/**<summary>Space �L�[�������܂��B</summary>*/			SPACE=0x20,
		/**<summary>PageUp �L�[�������܂��B</summary>*/			PRIOR=0x21,
		/**<summary>PageDown �L�[�������܂��B</summary>*/		NEXT=0x22,
		/**<summary>End �L�[�������܂��B</summary>*/			END=0x23,
		/**<summary>Home �L�[�������܂��B</summary>*/			HOME=0x24,
		/**<summary>�� �L�[�������܂��B</summary>*/				LEFT=0x25,
		/**<summary>�� �L�[�������܂��B</summary>*/				UP=0x26,
		/**<summary>�� �L�[�������܂��B</summary>*/				RIGHT=0x27,
		/**<summary>�� �L�[�������܂��B</summary>*/				DOWN=0x28,
		/**<summary>Select �L�[�������܂��B</summary>*/			SELECT=0x29,
		/**<summary>Print �L�[�������܂��B</summary>*/			PRINT=0x2A,
		/**<summary>Execute �L�[�������܂��B</summary>*/		EXECUTE=0x2B,
		/**<summary>PrintScreen �L�[�������܂��B</summary>*/	SNAPSHOT=0x2C,
		/**<summary>Insert �L�[�������܂��B</summary>*/			INSERT=0x2D,
		/**<summary>Delete �L�[�������܂��B</summary>*/			DELETE=0x2E,
		/**<summary>Help �L�[�������܂��B</summary>*/			HELP=0x2F,

		/*
			* VK_0 - VK_9 are the same as ASCII '0' - '9' (0x30 - 0x39)
			* 0x40 : unassigned
			* VK_A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A)
			* 
			* '0' - '9' �͈ȉ��ł� D �������ċL�����ɂ���
			*/
		D0=0x30,		D1=0x31,		D2=0x32,		D3=0x33,		D4=0x34,
		D5=0x35,		D6=0x36,		D7=0x37,		D8=0x38,		D9=0x39,

		/**<summary>A �L�[�������܂��B</summary>*/A=0x41,
		/**<summary>B �L�[�������܂��B</summary>*/B=0x42,
		/**<summary>C �L�[�������܂��B</summary>*/C=0x43,
		/**<summary>D �L�[�������܂��B</summary>*/D=0x44,
		/**<summary>E �L�[�������܂��B</summary>*/E=0x45,
		/**<summary>F �L�[�������܂��B</summary>*/F=0x46,
		/**<summary>G �L�[�������܂��B</summary>*/G=0x47,
		/**<summary>H �L�[�������܂��B</summary>*/H=0x48,
		/**<summary>I �L�[�������܂��B</summary>*/I=0x49,
		/**<summary>J �L�[�������܂��B</summary>*/J=0x4a,
		/**<summary>K �L�[�������܂��B</summary>*/K=0x4b,
		/**<summary>L �L�[�������܂��B</summary>*/L=0x4c,
		/**<summary>M �L�[�������܂��B</summary>*/M=0x4d,
		/**<summary>N �L�[�������܂��B</summary>*/N=0x4e,
		/**<summary>O �L�[�������܂��B</summary>*/O=0x4f,
		/**<summary>P �L�[�������܂��B</summary>*/P=0x50,
		/**<summary>Q �L�[�������܂��B</summary>*/Q=0x51,
		/**<summary>R �L�[�������܂��B</summary>*/R=0x52,
		/**<summary>S �L�[�������܂��B</summary>*/S=0x53,
		/**<summary>T �L�[�������܂��B</summary>*/T=0x54,
		/**<summary>U �L�[�������܂��B</summary>*/U=0x55,
		/**<summary>V �L�[�������܂��B</summary>*/V=0x56,
		/**<summary>W �L�[�������܂��B</summary>*/W=0x57,
		/**<summary>X �L�[�������܂��B</summary>*/X=0x58,
		/**<summary>Y �L�[�������܂��B</summary>*/Y=0x59,
		/**<summary>Z �L�[�������܂��B</summary>*/Z=0x5a,
	
		LWIN=0x5B,
		RWIN=0x5C,
		APPS=0x5D,

		/*
			* 0x5E : reserved
			*/

		SLEEP=0x5F,//�s��

		NUMPAD0=0x60,		NUMPAD1=0x61,		NUMPAD2=0x62,		NUMPAD3=0x63,		NUMPAD4=0x64,
		NUMPAD5=0x65,		NUMPAD6=0x66,		NUMPAD7=0x67,		NUMPAD8=0x68,		NUMPAD9=0x69,
		MULTIPLY=0x6A,
		ADD=0x6B,
		SEPARATOR=0x6C,
		SUBTRACT=0x6D,
		DECIMAL=0x6E,
		DIVIDE=0x6F,
		F1=0x70,		F2=0x71,		F3=0x72,		F4=0x73,		F5=0x74,
		F6=0x75,		F7=0x76,		F8=0x77,		F9=0x78,		F10=0x79,
		F11=0x7A,		F12=0x7B,		F13=0x7C,		F14=0x7D,		F15=0x7E,
		F16=0x7F,		F17=0x80,		F18=0x81,		F19=0x82,		F20=0x83,
		F21=0x84,		F22=0x85,		F23=0x86,		F24=0x87,

		/*
			* 0x88 - 0x8F : unassigned
			*/

		NUMLOCK=0x90,
		SCROLL=0x91,

		/*
			* NEC PC-9800 kbd definitions
			*/
		OEM_NEC_EQUAL=0x92,   // '=' key on numpad

		/*
			* Fujitsu/OASYS kbd definitions
			*/
		OEM_FJ_JISHO=0x92,   // 'Dictionary' key
		OEM_FJ_MASSHOU=0x93,   // 'Unregister word' key
		OEM_FJ_TOUROKU=0x94,   // 'Register word' key
		OEM_FJ_LOYA=0x95,   // 'Left OYAYUBI' key
		OEM_FJ_ROYA=0x96,   // 'Right OYAYUBI' key

		/*
			* 0x97 - 0x9F : unassigned
			*/

		/*
			* VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys.
			* Used only as parameters to GetAsyncKeyState() and GetKeyState().
			* No other API or message will distinguish left and right keys in this way.
			*/
		LSHIFT=0xA0,
		RSHIFT=0xA1,
		LCONTROL=0xA2,
		RCONTROL=0xA3,
		LMENU=0xA4,
		RMENU=0xA5,

		//if(_WIN32_WINNT >= 0x0500)
		BROWSER_BACK=0xA6,
		BROWSER_FORWARD=0xA7,
		BROWSER_REFRESH=0xA8,
		BROWSER_STOP=0xA9,
		BROWSER_SEARCH=0xAA,
		BROWSER_FAVORITES=0xAB,
		BROWSER_HOME=0xAC,

		VOLUME_MUTE=0xAD,
		VOLUME_DOWN=0xAE,
		VOLUME_UP=0xAF,
		MEDIA_NEXT_TRACK=0xB0,
		MEDIA_PREV_TRACK=0xB1,
		MEDIA_STOP=0xB2,
		MEDIA_PLAY_PAUSE=0xB3,
		LAUNCH_MAIL=0xB4,
		LAUNCH_MEDIA_SELECT=0xB5,
		LAUNCH_APP1=0xB6,
		LAUNCH_APP2=0xB7,

		//endif /* _WIN32_WINNT >= 0x0500 */

		/*
			* 0xB8 - 0xB9 : reserved
			*/

		OEM_1=0xBA,   // ';:' for US
		OEM_PLUS=0xBB,   // '+' any country
		OEM_COMMA=0xBC,   // ',' any country
		OEM_MINUS=0xBD,   // '-' any country
		OEM_PERIOD=0xBE,   // '.' any country
		OEM_2=0xBF,   // '/?' for US
		OEM_3=0xC0,   // '`~' for US

		/*
			* 0xC1 - 0xD7 : reserved
			*/

		/*
			* 0xD8 - 0xDA : unassigned
			*/

		OEM_4=0xDB,  //  '[{' for US
		OEM_5=0xDC,  //  '\|' for US
		OEM_6=0xDD,  //  ']}' for US
		OEM_7=0xDE,  //  ''"' for US
		OEM_8=0xDF,

		/*
			* 0xE0 : reserved
			*/

		/*
			* Various extended or enhanced keyboards
			*/
		//�ȉ��s��
		OEM_AX=0xE1,  //  'AX' key on Japanese AX kbd
		OEM_102=0xE2,  //  "<>" or "\|" on RT 102-key kbd.
		ICO_HELP=0xE3,  //  Help key on ICO
		ICO_00=0xE4,  //  00 key on ICO

		//if(WINVER >= 0x0400)
		PROCESSKEY=0xE5,
		//endif /* WINVER >= 0x0400 */

		ICO_CLEAR=0xE6,


		//if(_WIN32_WINNT >= 0x0500)
		PACKET=0xE7,
		//endif /* _WIN32_WINNT >= 0x0500 */

		/*
			* 0xE8 : unassigned
			*/

		/*
			* Nokia/Ericsson definitions
			*/
		OEM_RESET=0xE9,
		OEM_JUMP=0xEA,
		OEM_PA1=0xEB,
		OEM_PA2=0xEC,
		OEM_PA3=0xED,
		OEM_WSCTRL=0xEE,
		OEM_CUSEL=0xEF,
		OEM_ATTN=0xF0,
		OEM_FINISH=0xF1,
		OEM_COPY=0xF2,
		OEM_AUTO=0xF3,
		OEM_ENLW=0xF4,
		OEM_BACKTAB=0xF5,

		ATTN=0xF6,
		CRSEL=0xF7,
		EXSEL=0xF8,
		EREOF=0xF9,
		PLAY=0xFA,
		ZOOM=0xFB,
		NONAME=0xFC,
		PA1=0xFD,
		OEM_CLEAR=0xFE

		/*
			* 0xFF : reserved
			*/
	}
	#endregion

	/// <summary>
	/// �E�B���h�E�̔z�u�Ɋւ������ێ����܂��B
	/// </summary>
	[System.Serializable,Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public struct WINDOWPLACEMENT{
		/// <summary>
		/// ���̍\���̂̃o�C�g�P�ʂ̃T�C�Y���w�肵�܂��B
		/// </summary>
		public uint length;
		/// <summary>
		/// �ŏ������ꂽ�E�B���h�E�̈ʒu�ƁA�E�B���h�E�����ɖ߂����@�𐧌䂷��t���O���w�肵�܂��B
		/// </summary>
		public WPF flags;
		/// <summary>
		/// �E�B���h�E�̌��݂̕\����Ԃ��w�肵�܂��B���̃����o�ɂ́A���̂����ꂩ�̒l���w��ł��܂��B
		/// </summary>
		public SW showCmd;
		/// <summary>
		/// �E�B���h�E���ŏ��������Ƃ��́A�E�B���h�E�̍�����̈ʒu���w�肵�܂��B
		/// </summary>
		public POINT ptMinPosition;
		/// <summary>
		/// �E�B���h�E���ő剻�����Ƃ��́A�E�B���h�E�̍�����̈ʒu���w�肵�܂��B
		/// </summary>
		public POINT ptMaxPosition;
		/// <summary>
		/// �E�B���h�E���ʏ� (���ɖ߂��ꂽ) �̈ʒu�ɕ\�������Ƃ��́A�E�B���h�E�̍��W���w�肵�܂��B
		/// </summary>
		public RECT rcNormalPosition;
	}
	/// <summary>
	/// �ŏ������ꂽ�E�B���h�E�̈ʒu�ƁA�E�B���h�E�����ɖ߂����@���w�肷��̂Ɏg�p���܂��B
	/// </summary>
	[System.Flags]
	public enum WPF{
		/// <summary>
		/// �ŏ������ꂽ�E�B���h�E�� x �ʒu�� y �ʒu���w��ł��邱�Ƃ������܂��B
		/// ���W�� WINDOWPLACEMENT.ptMinPosition �����o�ɐݒ肷��Ƃ��́A���̃t���O���w�肵�܂��B
		/// </summary>
		SETMINPOSITION=0x0001,
		/// <summary>
		/// �ŏ��������O�ɍő剻����Ă������ǂ����Ɋ֌W�Ȃ��A���������E�B���h�E���ő剻����邱�Ƃ������܂��B
		/// ���̐ݒ�́A���ɃE�B���h�E�����ɖ߂����Ƃ��ɂ̂ݗL���ł��B����̕�������͕ύX���܂���B
		/// ���̃t���O�́AWINDOWPLACEMENT.showCmd �����o�� SW_SHOWMINIMIZED �̒l���w�肳�ꂽ�Ƃ��ɂ����L���ł��B
		/// </summary>
		RESTORETOMAXIMIZED=0x0002,
		/// <summary>
		/// Windows 2000/XP: �Ăяo�����̃X���b�h�ƃE�B���h�E���������Ă���X���b�h���قȂ�ꍇ�A
		/// �V�X�e�����E�B���h�E����������X���b�h�ɓ��͂�^���鎖���w�肵�܂��B
		/// ���̎��ɂ���āA�Ăяo�����̏������A�E�B���h�E�̏����̎��s�̊Ԓ�~���鎖������鎖���o���܂��B
		/// </summary>
		ASYNCWINDOWPLACEMENT=0x0004
	}
	/// <summary>
	/// �E�B���h�E�̕\����Ԃ��w�肷��̂Ɏg�p���܂��B
	/// ShowWindow �֐��ASTARTUPINFO �\���́AWINDOWPLACEMENT �\���̂ȂǂŎg�p����܂��B
	/// </summary>
	public enum SW{
		/// <summary>
		/// �E�B���h�E���\���ɂ��A���̃E�B���h�E���A�N�e�B�u�ɂ��܂��B
		/// </summary>
		HIDE=0,
		/// <summary>
		/// �E�B���h�E���A�N�e�B�u�ɂ��ĕ\�����܂��B�E�B���h�E���ŏ����܂��͍ő剻����Ă����ꍇ�́A���̈ʒu�ƃT�C�Y�����ɖ߂��܂��B
		/// ���߂ăE�B���h�E��\������Ƃ��ɂ́A���̃t���O���w�肵�Ă��������B
		/// </summary>
		SHOWNORMAL=1,
		/// <summary>
		/// �E�B���h�E���A�N�e�B�u�ɂ��ĕ\�����܂��B�E�B���h�E���ŏ����܂��͍ő剻����Ă����ꍇ�́A���̈ʒu�ƃT�C�Y�����ɖ߂��܂��B
		/// ���߂ăE�B���h�E��\������Ƃ��ɂ́A���̃t���O���w�肵�Ă��������B
		/// </summary>
		NORMAL=1,
		/// <summary>
		/// �E�B���h�E���A�N�e�B�u�ɂ��āA�ŏ������܂��B
		/// </summary>
		SHOWMINIMIZED=2,
		/// <summary>
		/// �E�B���h�E���A�N�e�B�u�ɂ��āA�ő剻���܂��B
		/// </summary>
		SHOWMAXIMIZED=3,
		/// <summary>
		/// �E�B���h�E���ő剻���܂��B
		/// </summary>
		MAXIMIZE=3,
		/// <summary>
		/// �E�B���h�E�𒼑O�̈ʒu�ƃT�C�Y�ŕ\�����܂��B
		/// SW_SHOWNORMAL �Ǝ��Ă��܂����A���̒l���w�肵���ꍇ�́A�E�B���h�E�̓A�N�e�B�u������܂���B
		/// </summary>
		SHOWNOACTIVATE=4,
		/// <summary>
		/// �E�B���h�E���A�N�e�B�u�ɂ��āA���݂̈ʒu�ƃT�C�Y�ŕ\�����܂��B
		/// </summary>
		SHOW=5,
		/// <summary>
		/// �E�B���h�E���ŏ������AZ �I�[�_�[�����̃g�b�v���x���E�B���h�E���A�N�e�B�u�ɂ��܂��B
		/// </summary>
		MINIMIZE=6,
		/// <summary>
		/// �E�B���h�E���ŏ������܂��B
		/// SW_SHOWMINIMIZED �Ǝ��Ă��܂����A���̒l���w�肵���ꍇ�́A�E�B���h�E�̓A�N�e�B�u������܂���B
		/// </summary>
		SHOWMINNOACTIVATE=7,
		/// <summary>
		/// �E�B���h�E�����݂̃T�C�Y�ƈʒu�ŕ\�����܂��B
		/// SW_SHOW �Ǝ��Ă��܂����A���̒l���w�肵���ꍇ�́A�E�B���h�E�̓A�N�e�B�u������܂���B
		/// </summary>
		SHOWNA=8,
		/// <summary>
		/// �E�B���h�E���A�N�e�B�u�ɂ��ĕ\�����܂��B�ŏ����܂��͍ő剻����Ă����E�B���h�E�́A���̈ʒu�ƃT�C�Y�ɖ߂�܂��B
		/// �ŏ�������Ă���E�B���h�E�����ɖ߂��ꍇ�́A���̃t���O���Z�b�g���܂��B
		/// </summary>
		RESTORE=9,
		/// <summary>
		/// �A�v���P�[�V�������N�������v���O������ CreateProcess �֐��ɓn���� STARTUPINFO �\���̂Ŏw�肳�ꂽ SW_ �t���O�ɏ]���ĕ\����Ԃ�ݒ肵�܂��B
		/// </summary>
		SHOWDEFAULT=10,
		/// <summary>
		/// Windows 2000�F���Ƃ��E�B���h�E�����L����X���b�h���n���O���Ă��Ă��A�E�B���h�E���ŏ������܂��B
		/// ���̃t���O�́A�ق��̃X���b�h�̃E�B���h�E���ŏ�������ꍇ�ɂ����g�p���Ă��������B
		/// </summary>
		FORCEMINIMIZE=11,
		/// <summary>
		/// �s���BFORCEMINIMIZE �Ɠ����ł��B
		/// </summary>
		MAX=11
	}
	/// <summary>
	/// ScrollWindow/ScrollWindowEx �֐��̈����ł��B
	/// </summary>
	[System.Flags]
	public enum SW_{
		/// <summary>
		/// prcScroll �ɂ���Ďw�肳����`�̈�ƌ����S�Ă̎q�E�B���h�E���X�N���[�����܂��B
		/// </summary>
		SCROLLCHILDREN=0x0001,
		/// <summary>
		/// �X�N���[����� hrgnUpdate �Ŏw�肵���̈�𖳌��ɂ��܂��B
		/// </summary>
		INVALIDATE=0x0002,
		/// <summary>
		/// SW_INVALIDATE �Ƌ��Ɏw�肵���ꍇ�AWM_ERASEBKGND �𑗂��Ė����ɂȂ����̈���������܂��B
		/// </summary>
		ERASE=0x0004,
		/// <summary>
		/// Windows 98/Me Windows 2000/XP: �X�N���[�����Ɋ��炩�ȃX�N���[���������s�������w�肵�܂��B
		/// flags �̍��ʃ��[�h���g�p���Ċ��炩�ȃX�N���[���̏���������s�������w�肷�鎖���o���܂��B
		/// </summary>
		SMOOTHSCROLL=0x0010
	}
	/// <summary>
	/// ���b�Z�[�W WM_SHOWWINDOW �� lParam �ł��B�\������E�B���h�E�̏�Ԃ��w�肷��̂Ɏw�肵�܂��B
	/// </summary>
	public enum SW__{
		/// <summary>
		/// �e�E�B���h�E���ŏ������鎖�������܂��B
		/// </summary>
		PARENTCLOSING=1,
		/// <summary>
		/// ���̃E�B���h�E���ő剻�����ׁA���̃E�B���h�E���B����鎖�������܂��B
		/// </summary>
		OTHERZOOM=2,
		/// <summary>
		/// �e�E�B���h�E���J����鎖�������܂��B
		/// </summary>
		PARENTOPENING=3,
		/// <summary>
		/// ���̃E�B���h�E�̍ő剻����������A���̃E�B���h�E���I��ɂȂ鎖�������܂��B
		/// </summary>
		OTHERUNZOOM=4
	}
	/// <summary>
	/// WindowsHook �̎�ނ��w�肷��ׂɎg�p���܂��B
	/// <see cref="User32.SetWindowsHookEx(mwg.Win32.WH,System.IntPtr,System.IntPtr,uint)"/> �֐��Ɏg�p���܂��B
	/// <a href="http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp">MSDN ��</a>
	/// </summary>
	public enum WH:int{
		//
		// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar.
		// For more information, see the MessageProc hook procedure.
		// MSGFILTER
		//
		/// <summary>
		/// Installs a hook procedure that records input messages posted to the system message queue. This hook is useful for recording macros.
		/// For more information, see the JournalRecordProc hook procedure.
		/// </summary>
		JOURNALRECORD = 0,
		/// <summary>
		/// Installs a hook procedure that posts messages previously recorded by a WH_JOURNALRECORD hook procedure.
		/// For more information, see the JournalPlaybackProc hook procedure.
		/// </summary>
		JOURNALPLAYBACK = 1,
		/// <summary>
		/// Installs a hook procedure that monitors keystroke messages.
		/// For more information, see the KeyboardProc hook procedure.
		/// </summary>
		KEYBOARD = 2,
		/// <summary>
		/// Installs a hook procedure that monitors messages posted to a message queue.
		/// For more information, see the GetMsgProc hook procedure.
		/// </summary>
		GETMESSAGE = 3,
		/// <summary>
		/// �ڕW�̃E�B���h�E�̏����̑O�Ƀ��b�Z�[�W���Ď����鎖�������܂��B
		/// For more information, see the CallWndProc hook procedure.
		/// </summary>
		CALLWNDPROC = 4,
		/// <summary>
		/// Installs a hook procedure that receives notifications useful to a computer-based training (CBT) application.
		/// For more information, see the CBTProc hook procedure.
		/// </summary>
		CBT = 5,
		/// <summary>
		/// Installs a hook procedure that monitors messages generated as a result of an input event in a dialog box, message box, menu, or scroll bar. The hook procedure monitors these messages for all applications in the same desktop as the calling thread.
		/// For more information, see the SysMsgProc hook procedure.
		/// </summary>
		SYSMSGFILTER = 6,
		/// <summary>
		/// Installs a hook procedure that monitors mouse messages.
		/// For more information, see the MouseProc hook procedure.
		/// </summary>
		MOUSE = 7,
		/// <summary>
		/// ?
		/// </summary>
		HARDWARE = 8,
		/// <summary>
		/// Installs a hook procedure useful for debugging other hook procedures.
		/// For more information, see the DebugProc hook procedure.
		/// </summary>
		DEBUG = 9,
		/// <summary>
		/// Installs a hook procedure that receives notifications useful to shell applications.
		/// For more information, see the ShellProc hook procedure.
		/// </summary>
		SHELL = 10,
		/// <summary>
		/// Installs a hook procedure that will be called when the application's foreground thread is about to become idle. This hook is useful for performing low priority tasks during idle time.
		/// For more information, see the ForegroundIdleProc hook procedure. 
		/// </summary>
		FOREGROUNDIDLE = 11,
		/// <summary>
		/// Installs a hook procedure that monitors messages after they have been processed by the destination window procedure.
		/// For more information, see the CallWndRetProc hook procedure.
		/// </summary>
		CALLWNDPROCRET = 12,
		/// <summary>
		/// Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard input events.
		/// For more information, see the LowLevelKeyboardProc hook procedure.
		/// </summary>
		KEYBOARD_LL = 13,
		/// <summary>
		/// Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events.
		/// For more information, see the LowLevelMouseProc hook procedure.
		/// </summary>
		MOUSE_LL=14
	}
	/// <summary>
	/// �t�b�N����֐���\���f���Q�[�g�N���X�ł��B
	/// </summary>
	public delegate int HookProc(int code,System.IntPtr wParam,System.IntPtr lParam);
	/// <summary>
	/// �჌�x���̃L�[�{�[�h�̃C�x���g����������v���V�[�W����\���f���Q�[�g�ł��B
	/// </summary>
	public delegate int LowLevelKeyboardProc(int nCode,WM wParam, [Interop.In]KBDLLHOOKSTRUCT lParam);
	/// <summary>
	/// �჌�x���̃}�E�X�̃C�x���g����������v���V�[�W����\���f���Q�[�g�ł��B
	/// </summary>
	public delegate int LowLevelMouseProc(int code,WM wParam, [Interop.In]MSLLHOOKSTRUCT lParam);
	/// <summary>
	/// �L�[�{�[�h�̃C�x���g�̏���ێ�����\���̂ł��B
	/// </summary>
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public class KBDLLHOOKSTRUCT{
		/// <summary>
		/// �L�[�̏���ێ����܂��B
		/// </summary>
		public VK vkCode;
		public int scanCode;
		public int flags;
		/// <summary>
		/// ���̃��b�Z�[�W�̔������������̏���ێ����܂��B
		/// </summary>
		public int time;
		/// <summary>
		/// ���̃��b�Z�[�W�Ɋւ���ǉ��̏���ێ����܂��B
		/// </summary>
		public System.IntPtr dwExtraInfo;
	}
	/// <summary>
	/// �}�E�X�̃C�x���g�̏���ێ�����\���̂ł��B
	/// </summary>
	[Interop.StructLayout(Interop.LayoutKind.Sequential)]
	public class MSLLHOOKSTRUCT{
		/// <summary>
		/// �}�E�X�J�[�\���̈ʒu����ێ����܂��B
		/// </summary>
		public POINT pt;
		public int mouseData;
		public int flags;
		/// <summary>
		/// ���̃��b�Z�[�W�̔������������̏���ێ����܂��B
		/// </summary>
		public int time;
		/// <summary>
		/// ���̃��b�Z�[�W�Ɋւ���ǉ��̏���ێ����܂��B
		/// </summary>
		public System.IntPtr dwExtraInfo;
	}
	/// <summary>
	/// CallWndProc �Ŏg�p����\���̂ł��B
	/// </summary>
	[Interop::StructLayout(Interop::LayoutKind.Sequential)]
	public struct CWPSTRUCT{
		/// <summary>
		/// ���b�Z�[�W�� lParam ��ێ����܂��B
		/// </summary>
		public System.IntPtr lParam;
		/// <summary>
		/// ���b�Z�[�W�� wParam ��ێ����܂��B
		/// </summary>
		public System.IntPtr wParam;
		/// <summary>
		/// ���b�Z�[�W�̎�ނ�ێ����܂��B
		/// </summary>
		public WM message;
		/// <summary>
		/// ���b�Z�[�W�̑����� Window �� Handle ��ێ����܂��B
		/// </summary>
		public System.IntPtr hwnd;
	} 
}