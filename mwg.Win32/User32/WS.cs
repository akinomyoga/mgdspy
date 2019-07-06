namespace mwg.Win32{
	// Quote: http://msdn2.microsoft.com/ja-jp/library/xyfwf42d(VS.80).aspx
	// Quote: http://www.activebasic.com/help_center/Pages/API/Window/Window/CreateWindowEx.htm
	/// <summary>
	/// �E�B���h�E�̃X�^�C�����w�肷��̂Ɏg�p���܂��B
	/// </summary>
	[System.Flags]
	public enum WS:uint{
		/// <summary>
		/// �I�[�o�[���b�v�E�B���h�E�ł��鎖�������܂��B
		/// �I�[�o�[���b�v�E�B���h�E�ɂ͒ʏ�L���v�V�����Ƌ��E��������܂��B
		/// </summary>
		OVERLAPPED=0x00000000,
		/// <summary>
		/// �|�b�v�A�b�v�E�B���h�E�ł��鎖�������܂��B
		/// CHILD �X�^�C���Ƌ��Ɏg�p���鎖�͏o���܂���B
		/// </summary>
		POPUP		=0x80000000,
		/// <summary>
		/// �q�E�B���h�E���쐬���܂��B
		/// POPUP �X�^�C���Ƌ��Ɏg�p���鎖�͏o���܂���B 
		/// </summary>
		CHILD		=0x40000000,
		/// <summary>
		/// �E�B���h�E���ŏ�������Ă��鎖�������܂��B
		/// OVERLAPPED �Ƌ��Ɏg�p���܂��B
		/// </summary>
		MINIMIZE	=0x20000000,
		/// <summary>
		/// �E�B���h�E���\������Ă��鎖�������܂��B
		/// </summary>
		VISIBLE		=0x10000000,
		/// <summary>
		/// �E�B���h�E�������ɂȂ��Ă��鎖�������܂��B
		/// </summary>
		DISABLED	=0x08000000,
		/// <summary>
		/// �֘A����q�E�B���h�E���N���b�v���܂��B
		/// �܂�A1 �̎q�E�B���h�E���`�惁�b�Z�[�W���󂯎��ƁA CLIPSIBLINGS �X�^�C�����K�p����Ă���ꍇ�A
		/// �X�V����q�E�B���h�E�̗̈悩��A���̃E�B���h�E�Əd�����Ă���ق��̎q�E�B���h�E�����ׂăN���b�v���܂��B
		/// CLIPSIBLINGS ���w�肳��Ă��Ȃ��ꍇ�ɁA�q�E�B���h�E���d�����Ă���ƁA
		/// 1 �̎q�E�B���h�E�̃N���C�A���g�̈�ŕ`�悷��Ƃ��ɁA
		/// �אڂ���q�E�B���h�E�̃N���C�A���g�̈�ɕ`�悵�Ă��܂��\��������܂��B
		/// �K�� WS_CHILD �X�^�C���ƈꏏ�Ɏg���܂��B
		/// </summary>
		CLIPSIBLINGS=0x04000000,
		/// <summary>
		/// �e�E�B���h�E�̓����ŕ`�悷��Ƃ��ɁA�q�E�B���h�E����߂�̈�����O���܂��B
		/// �e�E�B���h�E���쐬����Ƃ��Ɏg�p���܂��B
		/// </summary>
		CLIPCHILDREN=0x02000000,
		/// <summary>
		/// �E�B���h�E���ő剻���Ă��鎖�������܂��B
		/// </summary>
		MAXIMIZE	=0x01000000,
		/// <summary>
		/// �E�B���h�E���^�C�g���o�[�������������܂��B
		/// </summary>
		CAPTION=0x00C00000,		/* WS_BORDER | WS_DLGFRAME  */
		/// <summary>
		/// �E�B���h�E�����E���������������܂��B
		/// </summary>
		BORDER		=0x00800000,
		/// <summary>
		/// �E�B���h�E����d�̋��E���������������܂��B
		/// </summary>
		DLGFRAME	=0x00400000,
		/// <summary>
		/// �E�B���h�E�������X�N���[���o�[�������������܂��B
		/// </summary>
		VSCROLL		=0x00200000,
		/// <summary>
		/// �E�B���h�E�������X�N���[���o�[�������������܂��B
		/// </summary>
		HSCROLL		=0x00100000,
		/// <summary>
		/// �^�C�g���o�[�ɃR���g���[�����j���[�{�b�N�X�������������܂��B
		/// �^�C�g���o�[�����E�B���h�E�����Ɏw�肷�鎖���o���܂��B
		/// </summary>
		SYSMENU		=0x00080000,
		/// <summary>
		/// �E�B���h�E���T�C�Y�ύX�Ɏg�p���鎖�̏o���鑾���g�������������܂��B
		/// </summary>
		THICKFRAME	=0x00040000,
		/// <summary>
		/// �R���g���[���O���[�v�̍ŏ��̃E�B���h�E�ł��鎖�������܂��B
		/// �R���g���[���O���[�v�ł̓��[�U�[�͕����L�[���g�p���āA����R���g���[�����玟�̃R���g���[���Ɉړ��o���܂��B
		/// �ŏ��̃R���g���[���̌ォ�� GROUP �X�^�C���� FALSE �Ǝw�肵�Ē�`�����R���g���[���͑S�ē����O���[�v�ɑ����܂��B
		/// GROUP �X�^�C���������̃R���g���[�����玟�̃O���[�v�ɑ����l�ɂȂ�܂��B
		/// </summary>
		GROUP		=0x00020000,
		/// <summary>
		/// Tab �L�[�������ă��[�U�[���t�H�[�J�X���ړ��ł��邱�Ƃ������܂��B
		/// ���[�U�[�� Tab �L�[�������ƁATABSTOP �X�^�C�����w�肳��Ă��鎟�̃R���g���[���Ƀt�H�[�J�X���ړ����܂��B
		/// </summary>
		TABSTOP		=0x00010000,

		/// <summary>
		/// �E�B���h�E���ŏ����{�^���������������܂��B
		/// </summary>
		MINIMIZEBOX=0x00020000,
		/// <summary>
		/// �E�B���h�E���ő剻�{�^���������������܂��B
		/// </summary>
		MAXIMIZEBOX=0x00010000,

		/// <summary>
		/// �I�[�o�[���b�v�E�B���h�E�ł��鎖�������܂��B
		/// �I�[�o�[���b�v�E�B���h�E�ɂ͒ʏ�L���v�V�����Ƌ��E��������܂��B
		/// OVERLAPPED �Ɠ����ł��B
		/// </summary>
		TILED=OVERLAPPED,
		/// <summary>
		/// �E�B���h�E���ŏ������ꂽ��Ԃł��鎖�������܂��B
		/// MIMIZE �X�^�C���Ɠ����ł��B
		/// </summary>
		ICONIC=MINIMIZE,
		/// <summary>
		/// �E�B���h�E���T�C�Y�ύX���E�������������܂��B
		/// THICKFRAME �Ɠ����ł��B
		/// </summary>
		SIZEBOX=THICKFRAME,
		/// <summary>
		/// OVERLAPPED�ACAPTION�ASYSMENU�ATHICKFRAME�AMINIMIZEBOX
		/// �y�� MAXIMIZEBOX �X�^�C�������I�[�o���b�v �E�B���h�E�ō݂鎖�������܂��B
		/// OVERLAPPEDWINDOW �Ɠ����ł��B
		/// </summary>
		TILEDWINDOW=OVERLAPPEDWINDOW,

		/*
		 * Common Window Styles
		 */
		/// <summary>
		/// OVERLAPPED�ACAPTION�ASYSMENU�ATHICKFRAME�AMINIMIZEBOX
		/// �y�� MAXIMIZEBOX �X�^�C�������I�[�o���b�v �E�B���h�E�ō݂鎖�������܂��B
		/// </summary>
		OVERLAPPEDWINDOW=OVERLAPPED|CAPTION|SYSMENU|THICKFRAME|MINIMIZEBOX|MAXIMIZEBOX,
		/// <summary>
		/// BORDER, POPUP, SYSMENU �X�^�C�������|�b�v�A�b�v�E�B���h�E�ł��鎖�������܂��B
		/// </summary>
		POPUPWINDOW=POPUP|BORDER|SYSMENU,
		/// <summary>
		/// CHILD �Ɠ����ł��B
		/// </summary>
		CHILDWINDOW=CHILD
	}
	/// <summary>
	/// �{�^���̃X�^�C�����w�肵�܂��B
	/// </summary>
	public static class BS{
		/// <summary>
		/// �v�b�V���{�^���ł��鎖�������܂��B 
		/// </summary>
		public const WS PUSHBUTTON=WS.OVERLAPPED;	// 0x00
		/// <summary>
		/// �v�b�V���{�^���ł��鎖�������܂��B�������A���F�̑������E�������܂��B
		/// ���̃{�^�����_�C�A���O�{�b�N�X�ɂ���ꍇ�́A�{�^�������̓t�H�[�J�X�������Ă��Ȃ��Ă��A�G���^�[�L�[�������΃{�^����I���ł��܂��B 
		/// </summary>
		public const WS DEFPUSHBUTTON=(WS)0x00000001;
		/// <summary>
		/// �`�F�b�N�{�b�N�X�ł��鎖�������܂��B
		/// </summary>
		public const WS CHECKBOX=(WS)0x00000002;
		/// <summary>
		/// �`�F�b�N�{�b�N�X�ł��鎖�������܂��B
		/// �������A���[�U�[���`�F�b�N�{�b�N�X��I������ƃ{�b�N�X�̏�Ԃ������I�ɕς��܂��B
		/// </summary>
		public const WS AUTOCHECKBOX=(WS)0x00000003;
		/// <summary>
		/// ���W�I�{�^���ł��鎖�������܂��B 
		/// </summary>
		public const WS RADIOBUTTON=(WS)0x00000004;
		/// <summary>
		/// �I�����ꂽ��ԁA�I������Ă��Ȃ���ԁA�O���[�\���̏�ԂƂ���3�̏�Ԃ����`�F�b�N�{�b�N�X�ł��鎖�������܂��B
		/// �O���[�̏�Ԃ́A�`�F�b�N�{�b�N�X�̏�Ԃ����߂��Ă��Ȃ����Ƃ������Ƃ��ȂǂɎg���܂��B
		/// </summary>
		public const WS _3STATE=(WS)0x00000005;
		/// <summary>
		/// _3STATE�Ɠ����{�^���ł��B�������A���[�U�[���`�F�b�N�{�b�N�X��I������ƃ{�b�N�X�̏�Ԃ������I�ɕς��܂��B 
		/// </summary>
		public const WS AUTO3STATE=(WS)0x00000006;
		/// <summary>
		/// �O���[�v�{�b�N�X�ł��鎖�������܂��B�ق��̃R���g���[�����A���̃R���g���[���̒��ɃO���[�v���ł��܂��B 
		/// </summary>
		public const WS GROUPBOX=(WS)0x00000007;
		/// <summary>
		/// OWNERDRAW �̌Â��`���ł��B
		/// </summary>
		[System.Obsolete("OWNERDRAW ���g�p���ĉ������B")]
		public const WS USERBUTTON=(WS)0x00000008;
		/// <summary>
		/// ���W�I�{�^���ł��鎖�������܂��B
		/// �������A���[�U�[���{�^����I������ƁAWindows�������I�Ƀ{�^����I����Ԃɂ��A�����O���[�v�̂ق��̃{�^�����I����Ԃɂ��܂��B 
		/// </summary>
		public const WS AUTORADIOBUTTON=(WS)0x00000009;
		/// <summary>
		/// �Ή�����Ă��܂���B
		/// </summary>
		[System.Obsolete("�Ή�����Ă��܂���B")]
		public const WS PUSHBOX=(WS)0x0000000A;
		/// <summary>
		/// �I�[�i�[�`��{�^�����쐬���܂��B
		/// �I�[�i�[�E�B���h�E�́A�{�^�����쐬������ WM.MEASUREITEM ���b�Z�[�W���󂯎��A�{�^���̊O�ς��ς��� WM.DRAWITEM ���b�Z�[�W���󂯎��܂��B
		/// ���̃{�^���X�^�C���Ƒg�ݍ��킹�邱�Ƃ͂ł��܂���B 
		/// </summary>
		public const WS OWNERDRAW=(WS)0x0000000B;
		/// <summary>
		/// �{�^���̎�ނ��w�肵�Ă��镔���̃r�b�g�}�X�N�ł��B
		/// </summary>
		public const WS TYPEMASK=(WS)0x0000000F;
		/// <summary>
		/// ���W�I�{�^���X�^�C����`�F�b�N�{�b�N�X�X�^�C���Ƒg�ݍ��킹��ƁA���W�I�{�^���̉~��`�F�b�N�{�b�N�X�̎l�p�̍����Ƀe�L�X�g���u����܂��BRIGHTBUTTON �X�^�C���𓯂��ł��B 
		/// </summary>
		public const WS LEFTTEXT=(WS)0x00000020;

		// �ȉ� if(WINVER >= 0x0400)
		/// <summary>
		/// �{�^�����e�L�X�g��\������悤�w�肵�܂��B 
		/// </summary>
		public const WS TEXT=WS.OVERLAPPED;	//0x00
		/// <summary>
		/// �A�C�R����\������{�^�����쐬���܂��B 
		/// </summary>
		public const WS ICON=(WS)0x00000040;
		/// <summary>
		/// �r�b�g�}�b�v��\������{�^�����쐬���܂��B 
		/// </summary>
		public const WS BITMAP=(WS)0x00000080;
		/// <summary>
		/// �{�^���̒��Ƀe�L�X�g�����񂹂��܂��B�{�^���� RIGHTBUTTON �X�^�C���������Ȃ��`�F�b�N�{�b�N�X�i�܂��̓��W�I�{�^���j�̏ꍇ�́A�e�L�X�g�̓`�F�b�N�{�b�N�X�⃉�W�I�{�^���̉E���ɍ��񂹂���܂��B 
		/// </summary>
		public const WS LEFT=(WS)0x00000100;
		/// <summary>
		/// �{�^���̒��Ƀe�L�X�g���E�񂹂��܂��B�{�^���� RIGHTBUTTON �X�^�C���������Ȃ��`�F�b�N�{�b�N�X�i�܂��̓��W�I�{�^���j�̏ꍇ�́A�e�L�X�g�̓`�F�b�N�{�b�N�X�⃉�W�I�{�^���̉E���ɉE�񂹂���܂��B 
		/// </summary>
		public const WS RIGHT=(WS)0x00000200;
		/// <summary>
		/// �{�^���̒����Ƀe�L�X�g�u���܂��B 
		/// </summary>
		public const WS CENTER=(WS)0x00000300;
		/// <summary>
		/// �{�^���̍ŏ㕔�Ƀe�L�X�g��u���܂��B 
		/// </summary>
		public const WS TOP=(WS)0x00000400;
		/// <summary>
		/// �{�^���̉����Ƀe�L�X�g��u���܂��B 
		/// </summary>
		public const WS BOTTOM=(WS)0x00000800;
		/// <summary>
		/// �{�^���́i���������́j�����Ƀe�L�X�g��u���܂��B 
		/// </summary>
		public const WS VCENTER=(WS)0x00000C00;
		/// <summary>
		/// �v�b�V���{�^���̂悤�ȊT�ςƋ@�\�����A�`�F�b�N�{�b�N�X�܂��̓��W�I�{�^�������܂��B 
		/// </summary>
		public const WS PUSHLIKE=(WS)0x00001000;
		/// <summary>
		/// �e�L�X�g���������ă{�^���̒��Ɉ�s�Ŏ��܂�Ȃ��Ƃ��́A�e�L�X�g�𕡐��s�ɐ܂�Ԃ��܂��B 
		/// </summary>
		public const WS MULTILINE=(WS)0x00002000;
		/// <summary>
		/// �e�E�B���h�E�ɁA�{�^����BN_DBLCLK�ʒm���b�Z�[�W�ABN_KILLFOCUS�ʒm���b�Z�[�W�ABN_SETFOCUS�ʒm���b�Z�[�W�𑗂邱�Ƃ��\�ɂ��܂��B���̃X�^�C���������ǂ����Ɋւ�炸�A�{�^����BN_CLICKED�ʒm���b�Z�[�W�𑗂�܂��B 
		/// </summary>
		public const WS NOTIFY=(WS)0x00004000;
		/// <summary>
		/// �{�^�����t���b�g�ɕ`�悳��鎖�������܂��B
		/// </summary>
		public const WS FLAT=(WS)0x00008000;
		/// <summary>
		/// BS_LEFTTEXT �X�^�C���Ɠ����ł��B 
		/// </summary>
		public const WS RIGHTBUTTON=(WS)0x00000020;//LEFTTEXT;
	}
	/// <summary>
	/// �R���{�{�b�N�X�̃X�^�C�����w�肷��ׂ̒l�ł��B
	/// </summary>
	public static class CBS{
		/// <summary> 
		/// ���X�g �{�b�N�X����ɕ\�����܂��B 
		/// ���X�g �{�b�N�X���Ō��ݑI������Ă��鍀�ڂ��A�G�f�B�b�g �R���g���[���ɕ\������܂��B 
		/// </summary> 
		public const WS SIMPLE=(WS)0x0001; 
		/// <summary> 
		/// ���[�U�[���G�f�B�b�g �R���g���[���̉��ɂ���A�C�R����I�����Ȃ��ƃ��X�g �{�b�N�X���\������Ȃ��_�������āACBS_SIMPLE �Ɠ����ł��B 
		/// </summary> 
		public const WS DROPDOWN=(WS)0x0002; 
		/// <summary> 
		/// �G�f�B�b�g �R���g���[�����A���X�g �{�b�N�X���Ō��ݑI������Ă��鍀�ڂ�\������ÓI�ȃe�L�X�g���ڂɒu����������_�������āACBS_DROPDOWN �Ɠ����ł��B 
		/// </summary> 
		public const WS DROPDOWNLIST=(WS)0x0003; 
		/// <summary> 
		/// ���X�g �{�b�N�X�̃I�[�i�[���A���̃��X�g �{�b�N�X�̓��e��`�悵�܂��B 
		/// ���X�g �{�b�N�X���̍��ڂ́A���ׂē��������ŕ`�悳��܂��B 
		/// </summary> 
		public const WS OWNERDRAWFIXED=(WS)0x0010; 
		/// <summary> 
		/// ���X�g �{�b�N�X�̃I�[�i�[���A���̃��X�g �{�b�N�X�̓��e��`�悵�܂��B 
		/// ���X�g �{�b�N�X���̍��ڂ̍����͌Œ肳��܂���B 
		/// </summary> 
		public const WS OWNERDRAWVARIABLE=(WS)0x0020; 
		/// <summary> 
		/// ���[�U�[���s���ɕ�������͂����Ƃ��ɁA�G�f�B�b�g �R���g���[�����̃e�L�X�g�������I�ɉE�ɃX�N���[�����܂��B 
		/// ���̃X�^�C�����ݒ肳��Ă��Ȃ��ƁA�l�p�`���E���Ɏ��܂钷���̃e�L�X�g�������͂ł��܂���B 
		/// </summary> 
		public const WS AUTOHSCROLL=(WS)0x0040; 
		/// <summary> 
		/// �R���{ �{�b�N�X �G�f�B�b�g �R���g���[���ɓ��͂��ꂽ�e�L�X�g�� ANSI �����Z�b�g���� OEM �����Z�b�g�֕ϊ����A���̌�AANSI �����Z�b�g�ɖ߂��܂��B 
		/// ����ɂ��A�R���{ �{�b�N�X���� ANSI ������� OEM �����ɕϊ����邽�߂ɃA�v���P�[�V������ Windows �֐� AnsiToOem ���Ăяo�����Ƃ��ɁA�����ϊ����K�؂ɏ�������܂��B 
		/// ���̃X�^�C���́A�t�@�C������ێ�����R���{ �{�b�N�X�Ŏg�p����Ɩ��ɗ����܂��B 
		/// CBS_SIMPLE �܂��� CBS_DROPDOWN �̃X�^�C�����K�p����Ă���R���{ �{�b�N�X�ł����w��ł��܂��B 
		/// </summary> 
		public const WS OEMCONVERT=(WS)0x0080; 
		/// <summary> 
		/// ���X�g �{�b�N�X�ɓ��͂��ꂽ������������I�ɕ��בւ��܂��B 
		/// </summary> 
		public const WS SORT=(WS)0x0100; 
		/// <summary> 
		/// ������ō\������鍀�ڂ��i�[����I�[�i�[�`��R���{ �{�b�N�X�ł��B 
		/// �R���{ �{�b�N�X�͕�����Ɋ��蓖�Ă郁������|�C���^���ێ����邽�߁A�A�v���P�[�V�����ŁAGetText �����o�֐����g�p���ē���̍��ڂ̃e�L�X�g���擾�ł��܂��B 
		/// </summary> 
		public const WS HASSTRINGS=(WS)0x0200; 
		/// <summary> 
		/// �R���{ �{�b�N�X�̃T�C�Y���A���̃R���{ �{�b�N�X���A�v���P�[�V�����ɂ���č쐬���ꂽ�Ƃ��̎w��T�C�Y�Ɠ����ɂȂ�悤�Ɏw�肵�܂��B 
		/// �ʏ�́A�R���{ �{�b�N�X�ɕ\������鍀�ڂ��ꕔ�����ɂȂ�Ȃ��悤�ɁAWindows �ɂ���ăR���{ �{�b�N�X�̃T�C�Y����������܂��B 
		/// </summary> 
		public const WS NOINTEGRALHEIGHT=(WS)0x0400; 
		/// <summary> 
		/// ���X�g �{�b�N�X���̍��ڂ����Ȃ��A�X�N���[������K�v���Ȃ��ꍇ�A�����X�N���[�� �o�[���g�p�ł��Ȃ���Ԃŕ\�����܂��B 
		/// ���̃X�^�C�����ݒ肳��Ă��Ȃ��ƁA���ڂ����Ȃ��ăX�N���[������K�v���Ȃ��ꍇ�A�X�N���[�� �o�[�͕\������܂���B 
		/// </summary> 
		public const WS DISABLENOSCROLL=(WS)0x0800; 
		/// <summary> 
		/// �I���t�B�[���h�ƃ��X�g�����̂��ׂẴe�L�X�g��啶���ɕϊ����܂��B 
		/// </summary> 
		public const WS UPPERCASE=(WS)0x2000; 
		/// <summary> 
		/// �I���t�B�[���h�ƃ��X�g�����̂��ׂẴe�L�X�g���������ɕϊ����܂��B 
		/// </summary> 
		public const WS LOWERCASE=(WS)0x4000; 
	}
	/// <summary>
	/// �G�f�B�b�g�{�b�N�X�̃X�^�C�����w�肷��̂Ɏg�p���܂��B
	/// </summary>
	public static class ES{
		/// <summary>
		/// �P��s�܂��͕����s�̃G�f�B�b�g �R���g���[���ŁA�e�L�X�g���������ŕ\�����܂��B
		/// </summary>
		public const WS LEFT=WS.OVERLAPPED;	// 0x00
		/// <summary>
		/// �P��s�܂��͕����s�̃G�f�B�b�g �R���g���[���ŁA�e�L�X�g�𒆉������ŕ\�����܂��B
		/// </summary>
		public const WS CENTER=(WS)0x0001;
		/// <summary>
		/// �P��s�܂��͕����s�̃G�f�B�b�g �R���g���[���ŁA�e�L�X�g���E�����ŕ\�����܂��B
		/// </summary>
		public const WS RIGHT=(WS)0x0002;
		/// <summary>
		/// �����s�G�f�B�b�g �R���g���[�����w�肵�܂��B
		/// ����ł͒P��s�̃G�f�B�b�g �R���g���[�����w�肳��܂��B
		/// ES_AUTOVSCROLL �X�^�C�����w�肳��Ă���ƁA�G�f�B�b�g �R���g���[���́A�ł��邾�������̍s��\�����A���[�U�[�� Enter �L�[���������Ƃ��ɐ��������ɃX�N���[�����܂��B
		/// ES_AUTOVSCROLL ���w�肳��Ă��Ȃ��ꍇ�A�G�f�B�b�g �R���g���[���́A�ł��邾�������̍s��\�����܂����A����ȏ�s��\���ł��Ȃ��Ȃ����Ƃ��Ƀ��[�U�[�� Enter �L�[�������ƁA�r�[�v����炵�܂��B
		/// ES_AUTOHSCROLL �X�^�C�����w�肳��Ă���ꍇ�A�����s�G�f�B�b�g �R���g���[���́A�J���b�g���R���g���[���̉E�[���z�����Ƃ��Ɏ����I�ɐ��������ɃX�N���[�����܂��B
		/// ���[�U�[�́A�s��V��������ꍇ�� Enter �L�[�������K�v������܂��B
		/// ES_AUTOHSCROLL ���w�肳��Ă��Ȃ��ꍇ�A�K�v�ɉ����Ď����I�Ɏ��̍s�̐擪�Ƀe�L�X�g���܂�Ԃ��ĕ\������܂��B
		/// Enter �L�[�������ĐV�����s���n�߂邱�Ƃ��ł��܂��B
		/// �܂�Ԃ��ʒu�́A�E�B���h�E�̃T�C�Y�ɂ���Č��܂�܂��B
		/// �E�B���h�E�̃T�C�Y���ύX�����ƁA�܂�Ԃ��ʒu���ύX����A�e�L�X�g���ĕ\������܂��B
		/// �����s�G�f�B�b�g �R���g���[���ɂ́A�X�N���[�� �o�[��\���ł��܂��B
		/// �X�N���[�� �o�[������G�f�B�b�g �R���g���[���́A�X�N���[�� �o�[ ���b�Z�[�W��Ǝ��ɏ������܂��B
		/// �X�N���[�� �o�[���Ȃ��G�f�B�b�g �R���g���[���́A��Ő��������悤�ɃX�N���[�����A�e�E�B���h�E���瑗�M���ꂽ�X�N���[�� ���b�Z�[�W���������܂��B
		/// </summary>
		public const WS MULTILINE=(WS)0x0004;
		/// <summary>
		/// �G�f�B�b�g �R���g���[���ɓ��͂��ꂽ���ׂĂ̕�����啶���ɕϊ����܂��B
		/// </summary>
		public const WS UPPERCASE=(WS)0x0008;
		/// <summary>
		/// �G�f�B�b�g �R���g���[���ɓ��͂��ꂽ���������ׂď������ɕϊ����܂��B
		/// </summary>
		public const WS LOWERCASE=(WS)0x0010;
		/// <summary>
		/// �G�f�B�b�g �R���g���[���ɓ��͂��ꂽ���ׂĂ̕������A�X�^���X�N (*) �ŕ\�����܂��B
		/// �A�v���P�[�V�������ł́ASetPasswordChar �����o�֐����g�p���āA�\������镶����ύX�ł��܂��B
		/// </summary>
		public const WS PASSWORD=(WS)0x0020;
		/// <summary>
		/// ���[�U�[���ŏI�s�� Enter �L�[���������Ƃ��ɁA�����I�� 1 �y�[�W���̃e�L�X�g���X�N���[�����܂��B
		/// </summary>
		public const WS AUTOVSCROLL=(WS)0x0040;
		/// <summary>
		/// ���[�U�[���s���ɕ�������͂����Ƃ��ɁA�e�L�X�g�������I�� 10 �������E�փX�N���[�����܂��B
		/// ���[�U�[�� Enter �L�[�������ƁA���ׂẴe�L�X�g���X�N���[������A�J�[�\���ʒu�� 0 �ɖ߂�܂��B
		/// </summary>
		public const WS AUTOHSCROLL=(WS)0x0080;
		/// <summary>
		/// �ʏ�A�G�f�B�b�g �R���g���[���ł́A���̓t�H�[�J�X���ʂ̃R���g���[���Ɉڂ�ƑI����e����\���ɂȂ�A���̓t�H�[�J�X���󂯎��ƑI����e�������\������܂��B
		/// ES_NOHIDESEL ���w�肷��ƁA���̊���̓��삪�s���Ȃ��Ȃ�܂��B
		/// </summary>
		public const WS NOHIDESEL=(WS)0x0100;
		/// <summary>
		/// �G�f�B�b�g �R���g���[���ɓ��͂��ꂽ�e�L�X�g�� ANSI �����Z�b�g���� OEM �����Z�b�g�ɕϊ����A���̌�AANSI �����Z�b�g�ɖ߂��܂��B
		/// ����ɂ��A�R���{ �{�b�N�X���� ANSI ������� OEM ������ɕϊ����邽�߂ɃA�v���P�[�V������ Windows �֐� AnsiToOem ���Ăяo�����Ƃ��ɁA�����ϊ����K�؂ɏ�������܂��B
		/// ���̃X�^�C���́A�t�@�C������ێ�����G�f�B�b�g �R���g���[���Ŏg�p����Ɩ��ɗ����܂��B
		/// </summary>
		public const WS OEMCONVERT=(WS)0x0400;
		/// <summary>
		/// �G�f�B�b�g �R���g���[�����Ńe�L�X�g����͂���ѕҏW�ł��Ȃ��悤�ɂ��܂��B
		/// </summary>
		public const WS READONLY=(WS)0x0800;
		/// <summary>
		/// ���[�U�[���_�C�A���O �{�b�N�X���̕����s�G�f�B�b�g �R���g���[���ւ̃e�L�X�g�̓��͒��� Enter �L�[���������Ƃ��ɁA�L�����b�W ���^�[�����}�������悤�Ɏw�肵�܂��B
		/// ���̃X�^�C�����w�肳��Ă��Ȃ��ꍇ�AEnter �L�[���������Ƃ́A�_�C�A���O �{�b�N�X�̊���̃v�b�V�� �{�^�������������ƂƓ����ɂȂ�܂��B
		/// ���̃X�^�C���́A�P��s�̃G�f�B�b�g �R���g���[���ɂ͉e�����܂���B
		/// </summary>
		public const WS WANTRETURN=(WS)0x1000;
		/// <summary>
		/// �G�f�B�b�g �R���g���[���ɐ�����������͂ł���悤�ɂ��܂��B
		/// </summary>
		public const WS NUMBER=(WS)0x2000;
	}
	/// <summary>
	/// ���X�g�{�b�N�X�̃X�^�C�����w�肷��̂Ɏg�p���܂��B
	/// </summary>
	public static class LBS{
		/// <summary>
		/// ���[�U�[����������N���b�N�܂��̓_�u���N���b�N���邽�тɁA�e�E�B���h�E�����̓��b�Z�[�W���󂯎��܂��B
		/// </summary>
		public const WS NOTIFY=(WS)0x0001;
		/// <summary>
		/// ���X�g �{�b�N�X���̕�������A���t�@�x�b�g���ɕ��בւ��܂��B
		/// </summary>
		public const WS SORT=(WS)0x0002;
		/// <summary>
		/// ���X�g �{�b�N�X���ύX����Ă��\�����e���X�V���܂���B
		/// ���̃X�^�C���́AWM_SETREDRAW ���b�Z�[�W�𑗐M���邱�Ƃɂ��A���ł��ύX�ł��܂��B
		/// </summary>
		public const WS NOREDRAW=(WS)0x0004;
		/// <summary>
		/// ���[�U�[����������N���b�N�܂��̓_�u���N���b�N���邽�тɁA�I������Ă��镶���񂪐؂�ւ��܂��B
		/// ������͂����ł��I���ł��܂��B
		/// </summary>
		public const WS MULTIPLESEL=(WS)0x0008;
		/// <summary>
		/// ���X�g �{�b�N�X�̃I�[�i�[���A���̃��X�g �{�b�N�X�̓��e��`�悵�܂��B
		/// ���X�g �{�b�N�X���̍��ڂ́A���ׂē��������ŕ`�悳��܂��B
		/// </summary>
		public const WS OWNERDRAWFIXED=(WS)0x0010;
		/// <summary>
		/// ���X�g �{�b�N�X�̃I�[�i�[���A���̃��X�g �{�b�N�X�̓��e��`�悵�܂��B
		/// ���X�g �{�b�N�X���̍��ڂ̍����͌Œ肳��܂���B
		/// </summary>
		public const WS OWNERDRAWVARIABLE=(WS)0x0020;
		/// <summary>
		/// ������ō\������鍀�ڂ��i�[����I�[�i�[�`�惊�X�g �{�b�N�X���w�肵�܂��B
		/// ���X�g �{�b�N�X�͕�����Ɋ��蓖�Ă郁������|�C���^���ێ����邽�߁A�A�v���P�[�V�����ŁAGetText �����o�֐����g�p���ē���̍��ڂ̃e�L�X�g���擾�ł��܂��B
		/// </summary>
		public const WS HASSTRINGS=(WS)0x0040;
		/// <summary>
		/// ���X�g �{�b�N�X�ɕ����񂪕`�悳���Ƃ��ɁA�^�u�������F������ѓW�J�����悤�ɂ��܂��B
		/// ����̃^�u�ʒu�́A32 �_�C�A���O�P�ʂł��B
		/// �_�C�A���O�P�ʂ́A���������܂��͐��������̋�����\���܂��B
		/// 1 �����_�C�A���O�P�ʂ́A���݂̃_�C�A���O�̊�{���� 4 �� 1 �ł��B
		/// �_�C�A���O�̊�{�P�ʂ́A���݂̃V�X�e�� �t�H���g�̍����ƕ�����Ɍv�Z����܂��B
		/// Windows �֐� GetDialogBaseUnits �́A���݂̃_�C�A���O�̊�{�P�ʂ��s�N�Z�����ŕԂ��܂��B
		/// ���̃X�^�C���́ALBS_OWNERDRAWFIXED �Ƒg�ݍ��킹�Ďw�肵�Ȃ��ł��������B
		/// </summary>
		public const WS USETABSTOPS=(WS)0x0080;
		/// <summary>
		/// ���X�g �{�b�N�X�̃T�C�Y���A���̃��X�g �{�b�N�X���A�v���P�[�V�����ɂ���č쐬���ꂽ�Ƃ��̎w��T�C�Y�Ɠ����ɂȂ�܂��B
		/// �ʏ�́A���X�g �{�b�N�X�ɕ\������鍀�ڂ��ꕔ�����ɂȂ�Ȃ��悤�ɁAWindows �ɂ���ă��X�g �{�b�N�X�̃T�C�Y����������܂��B
		/// </summary>
		public const WS NOINTEGRALHEIGHT=(WS)0x0100;
		/// <summary>
		/// �����ɃX�N���[�����镡����̃��X�g �{�b�N�X���w�肵�܂��B
		/// SetColumnWidth �����o�֐��́A��̕���ݒ肵�܂��B
		/// </summary>
		public const WS MULTICOLUMN=(WS)0x0200;
		/// <summary>
		/// ���X�g �{�b�N�X�ɓ��̓t�H�[�J�X������Ƃ��Ƀ��[�U�[���L�[�������ƁA���X�g �{�b�N�X�̃I�[�i�[�� WM_VKEYTOITEM �܂��� WM_CHARTOITEM ���b�Z�[�W���󂯎��܂��B
		/// ����ɂ��A�L�[�{�[�h���͂ɑ΂��ăA�v���P�[�V���������ʂȏ��������s�ł���悤�ɂȂ�܂��B
		/// </summary>
		public const WS WANTKEYBOARDINPUT=(WS)0x0400;
		/// <summary>
		/// Shift �L�[�ƃ}�E�X�A�܂��͓���ȃL�[��g�ݍ��킹�Ďg�p���āA���[�U�[�������̍��ڂ�I���ł���悤�ɂ��܂��B
		/// </summary>
		public const WS EXTENDEDSEL=(WS)0x0800;
		/// <summary>
		/// ���X�g �{�b�N�X���̍��ڂ����Ȃ��A�X�N���[������K�v���Ȃ��ꍇ�A�����X�N���[�� �o�[���g�p�ł��Ȃ���Ԃŕ\�����܂��B
		/// ���̃X�^�C�����ݒ肳��Ă��Ȃ��ƁA���ڂ����Ȃ��ăX�N���[������K�v���Ȃ��ꍇ�A�X�N���[�� �o�[�͕\������܂���B
		/// </summary>
		public const WS DISABLENOSCROLL=(WS)0x1000;
		/// <summary>
		/// �f�[�^�������Ȃ����X�g �{�b�N�X���w�肵�܂��B
		/// ���̃X�^�C���́A���X�g �{�b�N�X�Ɋ܂܂�鍀�ڂ̐��� 1,000 �𒴂���ꍇ�Ɏw�肵�܂��B
		/// �f�[�^�������Ȃ����X�g �{�b�N�X�ɂ́ALBS_OWNERDRAWFIXED �X�^�C�����ݒ肷��K�v������܂��B
		/// �������ALBS_SORT �X�^�C���܂��� LBS_HASSTRINGS �X�^�C���͐ݒ肵�Ȃ��ł��������B
		/// �f�[�^�������Ȃ����X�g �{�b�N�X�́A���X�g���ڂ������������r�b�g�}�b�v�Ȃǂ̃f�[�^���܂�ł��Ȃ��_�������΁A�I�[�i�[�`�惊�X�g �{�b�N�X�Ɠ����ł��B
		/// ���ڂ�ǉ��A�}���A�܂��͍폜����R�}���h�����s���Ă��A�w�肵�����ڂ̃f�[�^�͏�ɖ�������܂��B
		/// �܂��A���X�g �{�b�N�X���̕��������������v�����K�����s���܂��B
		/// ���ڂ�`�悷��K�v������ꍇ�A�I�[�i�[ �E�B���h�E�� WM_DRAWITEM ���b�Z�[�W�����M����܂��B
		/// WM_DRAWITEM ���b�Z�[�W�œn���ꂽ DRAWITEMSTRUCT �\���̂� itemID �����o�ɂ��A�`�悷�鍀�ڂ̍s�ԍ����w�肳��܂��B
		/// �f�[�^�������Ȃ����X�g �{�b�N�X�́AWM_DELETEITEM ���b�Z�[�W�𑗐M���܂���B
		/// </summary>
		public const WS NODATA=(WS)0x2000;
		/// <summary>
		/// ���X�g �{�b�N�X�ɁA�\���ł��邪�I���ł��Ȃ����ڂ��܂܂��悤�Ɏw�肵�܂��B
		/// </summary>
		public const WS NOSEL=(WS)0x4000;
		/// <summary>
		/// ���X�g�{�b�N�X���R���{�{�b�N�X�̈ꕔ�ł��鎖�������܂��B
		/// </summary>
		public const WS COMBOBOX=(WS)0x8000;
		/// <summary>
		/// ���X�g �{�b�N�X���̕�������A���t�@�x�b�g���ɕ��בւ��܂��B
		/// �܂��A���[�U�[����������N���b�N�܂��̓_�u���N���b�N���邽�тɁA�e�E�B���h�E�����̓��b�Z�[�W���󂯎��܂��B
		/// ���X�g �{�b�N�X�̎l�ӂɋ��E���\������܂��B
		/// </summary>
		public const WS STANDARD=(WS)0x00a00003;
	}
	/// <summary>
	/// �X�N���[���o�[�̃X�^�C���Ɋւ���w��Ɏg�p���܂��B
	/// </summary>
	public struct SBS{
		/// <summary>
		/// �����X�N���[�� �o�[���w�肵�܂��B
		/// SBS_BOTTOMALIGN �� SBS_TOPALIGN �X�^�C�����ǂ�����w�肳��Ă��Ȃ��Ƃ��́A�X�N���[�� �o�[�� Create �����o�֐��Ŏw�肳�ꂽ���A�����A�ʒu�ɂȂ�܂��B
		/// </summary>
		public const WS HORZ=WS.OVERLAPPED;	// 0x00
		/// <summary>
		/// �����X�N���[�� �o�[���w�肵�܂��B
		/// SBS_RIGHTALIGN �� SBS_LEFTALIGN �X�^�C�����ǂ�����w�肳��Ă��Ȃ��Ƃ��́A�X�N���[�� �o�[�� Create �����o�֐��Ŏw�肳�ꂽ���A�����A�ʒu�ɂȂ�܂��B
		/// </summary>
		public const WS VERT=(WS)0x0001;
		/// <summary>
		/// SBS_HORZ �X�^�C���ƈꏏ�Ɏg�p���܂��B
		/// �X�N���[�� �o�[�̏�[�� Create �����o�֐��Ŏw�肳�ꂽ�l�p�`�̏�[�ɑ����܂��B
		/// �X�N���[�� �o�[�̍����́A�V�X�e�� �X�N���[�� �o�[�̊���̍����Ɠ����ɂȂ�܂��B
		/// </summary>
		public const WS TOPALIGN=(WS)0x0002;
		/// <summary>
		/// SBS_VERT �X�^�C���ƈꏏ�Ɏg�p���܂��B
		/// �X�N���[�� �o�[�̍��[�� Create �����o�֐��Ŏw�肳��Ă���l�p�`�̍��[�ɑ����܂��B
		/// �X�N���[�� �o�[�̕��́A�V�X�e�� �X�N���[�� �o�[�̊���̕��Ɠ����ɂȂ�܂��B
		/// </summary>
		public const WS LEFTALIGN=(WS)0x0002;
		/// <summary>
		/// SBS_HORZ �X�^�C���ƈꏏ�Ɏg�p���܂��B
		/// �X�N���[�� �o�[�̉��[�� Create �����o�֐��Ŏw�肳��Ă���l�p�`�̉��[�ɑ����܂��B
		/// �X�N���[�� �o�[�̍����́A�V�X�e�� �X�N���[�� �o�[�̊���̍����Ɠ����ɂȂ�܂��B
		/// </summary>
		public const WS BOTTOMALIGN=(WS)0x0004;
		/// <summary>
		/// SBS_VERT �X�^�C���ƈꏏ�Ɏg�p���܂��B
		/// �X�N���[�� �o�[�̉E�[�� Create �����o�֐��Ŏw�肳��Ă���l�p�`�̉E�[�ɑ����܂��B
		/// �X�N���[�� �o�[�̕��́A�V�X�e�� �X�N���[�� �o�[�̊���̕��Ɠ����ɂȂ�܂��B
		/// </summary>
		public const WS RIGHTALIGN=(WS)0x0004;
		/// <summary>
		/// SBS_SIZEBOX �X�^�C���ƈꏏ�Ɏg�p���܂��B
		/// �T�C�Y �{�b�N�X�̍������ Create �����o�֐��Ŏw�肳��Ă���l�p�`�̍�����ɑ����܂��B
		/// �T�C�Y �{�b�N�X�̃T�C�Y�́A�V�X�e�� �T�C�Y �{�b�N�X�̊���̃T�C�Y�Ɠ����ɂȂ�܂��B
		/// </summary>
		public const WS SIZEBOXTOPLEFTALIGN=(WS)0x0002;
		/// <summary>
		/// SBS_SIZEBOX �X�^�C���ƈꏏ�Ɏg�p���܂��B
		/// �T�C�Y �{�b�N�X�̉E������ Create �����o�֐��Ŏw�肳��Ă���l�p�`�̉E�����ɑ����܂��B
		/// �T�C�Y �{�b�N�X�̃T�C�Y�́A�V�X�e�� �T�C�Y �{�b�N�X�̊���̃T�C�Y�Ɠ����ɂȂ�܂��B
		/// </summary>
		public const WS SIZEBOXBOTTOMRIGHTALIGN=(WS)0x0004;
		/// <summary>
		/// �T�C�Y �{�b�N�X���w�肵�܂��B
		/// SBS_SIZEBOXBOTTOMRIGHTALIGN �� SBS_SIZEBOXTOPLEFTALIGN �X�^�C�����ǂ�����w�肳��Ă��Ȃ��Ƃ��́A�T�C�Y �{�b�N�X�� Create �����o�֐��Ŏw�肳�ꂽ�����A���A�ʒu�ɂȂ�܂��B
		/// </summary>
		public const WS SIZEBOX=(WS)0x0008;
		/// <summary>
		/// SBS_SIZEBOX �Ɠ����ł����A���E���������o���ĕ\������܂��B
		/// </summary>
		public const WS SIZEGRIP=(WS)0x0010;
	}
	/// <summary>
	/// �X�^�e�B�b�N�e�L�X�g�̃X�^�C�����w�肷��̂Ɏg�p���܂��B
	/// </summary>
	public static class SS{
		/// <summary>
		/// �P���Ȏl�p�`���w�肵�A�w�肳�ꂽ�e�L�X�g�����̒��ɍ������ŕ\�����܂��B
		/// �e�L�X�g�͏����ݒ肳��Ă���\������܂��B
		/// �e�L�X�g�̒����� 1 �s�̒������������ꍇ�́A�����I�Ɏ��̍s�ɐ܂�Ԃ���A�V�����s���������ŕ\������܂��B
		/// </summary>
		public static readonly WS LEFT=WS.OVERLAPPED; // 0x00
		/// <summary>
		/// �P���Ȏl�p�`���w�肵�A�w�肳�ꂽ�e�L�X�g�����̒��ɒ��������ŕ\�����܂��B
		/// �e�L�X�g�͏����ݒ肳��Ă���\������܂��B
		/// �e�L�X�g�̒����� 1 �s�����������ꍇ�́A�����I�Ɏ��̍s�ɐ܂�Ԃ���A�V�����s�����������ŕ\������܂��B
		/// </summary>
		public const WS CENTER=(WS)0x00000001;
		/// <summary>
		/// �P���Ȏl�p�`���w�肵�A�w�肳�ꂽ�e�L�X�g�����̒��ɉE�����ŕ\�����܂��B
		/// �e�L�X�g�͏����ݒ肳��Ă���\������܂��B
		/// �e�L�X�g�̒����� 1 �s�̒������������ꍇ�́A�����I�Ɏ��̍s�ɐ܂�Ԃ���A�V�����s���E�����ŕ\������܂��B
		/// </summary>
		public const WS RIGHT=(WS)0x00000002;
		/// <summary>
		/// �_�C�A���O �{�b�N�X�ɕ\������A�C�R�����w�肵�܂��B
		/// �w�肳�ꂽ�e�L�X�g�́A�A�C�R�� �t�@�C���̖��O�ł͂Ȃ��A���\�[�X �t�@�C���Œ�`����Ă���A�C�R�����ł��B
		/// �p�����[�^ nWidth �� nHeight �͖�������܂��B
		/// �A�C�R���̃T�C�Y�͎����I�ɒ�������܂��B
		/// </summary>
		public const WS ICON=(WS)0x00000003;
		/// <summary>
		/// �E�B���h�E�̘g�̐F�œh��Ԃ��ꂽ�l�p�`���w�肵�܂��B
		/// ����̐F�͍��ł��B
		/// </summary>
		public const WS BLACKRECT=(WS)0x00000004;
		/// <summary>
		/// ��ʂ̔w�i�F�œh��Ԃ��ꂽ�l�p�`���w�肵�܂��B
		/// ����̐F�͊D�F�ł��B
		/// </summary>
		public const WS GRAYRECT=(WS)0x00000005;
		/// <summary>
		/// �E�B���h�E�̔w�i�F�œh��Ԃ��ꂽ�l�p�`���w�肵�܂��B
		/// ����̐F�͔��ł��B
		/// </summary>
		public const WS WHITERECT=(WS)0x00000006;
		/// <summary>
		/// �E�B���h�E�̘g�Ɠ����F�̘g�����{�b�N�X���w�肵�܂��B
		/// ����̐F�͍��ł��B
		/// </summary>
		public const WS BLACKFRAME=(WS)0x00000007;
		/// <summary>
		/// ��ʂ̔w�i�F (�f�X�N�g�b�v) �Ɠ����F�̘g�����{�b�N�X���w�肵�܂��B
		/// ����̐F�͊D�F�ł��B
		/// </summary>
		public const WS GRAYFRAME=(WS)0x00000008;
		/// <summary>
		/// �E�B���h�E�̔w�i�F�Ɠ����F�̘g�����{�b�N�X���w�肵�܂��B
		/// ����̐F�͔��ł��B
		/// </summary>
		public const WS WHITEFRAME=(WS)0x00000009;
		/// <summary>
		/// ���[�U�[��`�̍��ڂ��w�肵�܂��B
		/// </summary>
		public const WS USERITEM=(WS)0x0000000A;
		/// <summary>
		/// �P���Ȏl�p�`���w�肵�A���̒��ɒP��s�̃e�L�X�g���������ŕ\�����܂��B
		/// �e�L�X�g�s��Z��������ύX�����肷�邱�Ƃ͂ł��܂���B
		/// �R���g���[���̐e�E�B���h�E�܂��̓_�C�A���O �{�b�N�X�ł́AWM_CTLCOLOR ���b�Z�[�W���������Ȃ��悤�ɂ���K�v������܂��B
		/// </summary>
		public const WS SIMPLE=(WS)0x0000000B;
		/// <summary>
		/// �P���Ȏl�p�`���w�肵�A�w�肳�ꂽ�e�L�X�g�����̒��ɍ������ŕ\�����܂��B
		/// �^�u�͓W�J����܂����A�e�L�X�g�͐܂�Ԃ���܂���B
		/// 1 �s�̒������������e�L�X�g�̓N���b�v����܂��B
		/// </summary>
		public const WS LEFTNOWORDWRAP=(WS)0x0000000C;
		/// <summary>
		/// �ÓI�R���g���[���̃I�[�i�[���A�R���g���[����`�悷�邱�Ƃ��w�肵�܂��B
		/// �R���g���[����`�悷��K�v������Ƃ��́A�I�[�i�[ �E�B���h�E�� WM_DRAWITEM ���b�Z�[�W���󂯎��܂��B
		/// </summary>
		public const WS OWNERDRAW=(WS)0x0000000D;
		/// <summary>
		/// �ÓI�R���g���[���Ƀr�b�g�}�b�v��\�����邱�Ƃ��w�肵�܂��B
		/// �r�b�g�}�b�v�̃t�@�C�����ł͂Ȃ��A���\�[�X �t�@�C���Œ�`����Ă���r�b�g�}�b�v�����w�肵�܂��B
		/// ���̃X�^�C���ł́A�p�����[�^ nWidth ����� nHeight �͖�������܂��B
		/// �r�b�g�}�b�v�����܂�悤�ɁA�R���g���[���̃T�C�Y�������I�ɒ�������܂��B
		/// </summary>
		public const WS BITMAP=(WS)0x0000000E;
		/// <summary>
		/// �ÓI�R���g���[���Ɋg�����^�t�@�C����\�����邱�Ƃ��w�肵�܂��B
		/// ���^�t�@�C���̖��O���w�肵�܂��B
		/// �g�����^�t�@�C����\������ÓI�R���g���[���̃T�C�Y�͌Œ�ł��B
		/// ���^�t�@�C���̕\���T�C�Y�́A�ÓI�R���g���[���̃N���C�A���g�̈�ɍ��킹�Ē�������܂��B
		/// </summary>
		public const WS ENHMETAFILE=(WS)0x0000000F;
		/// <summary>
		/// �ÓI�R���g���[���̏�[����щ��[�� EDGE_ETCHED �X�^�C���ŕ`�悵�܂��B
		/// </summary>
		public const WS ETCHEDHORZ=(WS)0x00000010;
		/// <summary>
		/// �ÓI�R���g���[���̉E�[����э��[�� EDGE_ETCHED �X�^�C���ŕ`�悵�܂��B
		/// </summary>
		public const WS ETCHEDVERT=(WS)0x00000011;
		/// <summary>
		/// �ÓI�R���g���[���̘g�� EDGE_ETCHED �X�^�C���ŕ`�悵�܂��B
		/// </summary>
		public const WS ETCHEDFRAME=(WS)0x00000012;
		/// <summary>
		/// �ÓI�R���g���[���̎�ʂ��w�肷�镔���̃r�b�g�}�X�N�ł��B
		/// </summary>
		public const WS TYPEMASK=(WS)0x0000001F;
		/// <summary>
		/// �R���g���[���̃T�C�Y�ɍ��킹�ăr�b�g�}�b�v�̑傫���𒲐����܂��B
		/// </summary>
		public const WS REALSIZECONTROL=(WS)0x00000040;
		/// <summary>
		/// ���̃X�^�C�����w�肵�Ȃ��ƁAWindows �ł́A�R���g���[���̃e�L�X�g�ɕ\�������A���p�T���h (&amp;) �������A�N�Z�����[�^�̐擪�����Ƃ��ĉ��߂���܂��B
		/// ���̏ꍇ�A�A���p�T���h�͕\�����ꂸ�A��������ŃA���p�T���h�̎��ɂ��镶���������t���ŕ\������܂��B
		/// ���̋@�\��K�v�Ƃ��Ȃ��e�L�X�g���ÓI�R���g���[���ɕ\�������ꍇ�́ASS_NOPREFIX ���w��ł��܂��B
		/// �ÓI�R���g���[���̂��̃X�^�C���́A��`����Ă��邷�ׂĂ̐ÓI�R���g���[���ɓK�p�ł��܂��B
		/// �r�b�g���Ƃ� OR ���Z�q���g���āA�ق��̃X�^�C���� SS_NOPREFIX ��g�ݍ��킹�Ďw��ł��܂��B
		/// ���̃X�^�C���́A�A���p�T���h���܂ރt�@�C�����₻�̑��̕�������_�C�A���O �{�b�N�X�̐ÓI�R���g���[���ɕ\������K�v������ꍇ�ɂ悭�g�p����܂��B
		/// </summary>
		public const WS NOPREFIX=(WS)0x00000080;
		/// <summary>
		/// ���[�U�[���R���g���[�����N���b�N�܂��̓_�u���N���b�N�����Ƃ��ɁA�e�E�B���h�E�� STN_CLICKED�ASTN_DBLCLK�ASTN_DISABLE�A����� STN_ENABLE �ʒm���b�Z�[�W�𑗐M���܂��B
		/// </summary>
		public const WS NOTIFY=(WS)0x00000100;
		/// <summary>
		/// �r�b�g�}�b�v�܂��̓A�C�R�����ÓI�R���g���[���̃N���C�A���g�̈�����������ꍇ�A�N���C�A���g�̈�̎c��̕��������̃r�b�g�}�b�v�܂��̓A�C�R���̍�����ɂ���s�N�Z���̐F�œh��Ԃ����Ƃ��w�肵�܂��B
		/// �ÓI�R���g���[���� 1 �s�̃e�L�X�g���\������Ă���ꍇ�A���̃e�L�X�g�́A�R���g���[���̃N���C�A���g�̈�ŏ㉺�����̒����ɔz�u����܂��B
		/// </summary>
		public const WS CENTERIMAGE=(WS)0x00000200;
		/// <summary>
		/// SS_BITMAP �܂��� SS_ICON �X�^�C�������ÓI�R���g���[���̃T�C�Y��ύX�����ꍇ���A���̃R���g���[���̉E�����̈ʒu�͌Œ肳�ꂽ�܂܂ɂȂ�悤�Ɏw�肵�܂��B
		/// �V�����r�b�g�}�b�v�܂��̓A�C�R����\������ꍇ�A�R���g���[���̏�ӂƍ��ӂ�������������܂��B
		/// </summary>
		public const WS RIGHTJUST=(WS)0x00000400;
		/// <summary>
		/// �ÓI�ȃA�C�R���܂��̓r�b�g�}�b�v �R���g���[�� (SS_ICON �܂��� SS_BITMAP �̃X�^�C�������ÓI�R���g���[��) ���ǂݍ��܂ꂽ��`�悳�ꂽ�肷��Ƃ��ɁA���̃T�C�Y���ύX����Ȃ��悤�ɂ��܂��B
		/// �A�C�R���܂��̓r�b�g�}�b�v���`���̗̈�����傫���ꍇ�A���̃C���[�W�̓N���b�v����܂��B
		/// </summary>
		public const WS REALSIZEIMAGE=(WS)0x00000800;
		/// <summary>
		/// �ÓI�R���g���[���̎��͂ɁA�������ڂ񂾋��E����`�悵�܂��B
		/// </summary>
		public const WS SUNKEN=(WS)0x00001000;
		/// <summary>
		/// �ÓI�R���g���[���̕`����@���G�f�B�b�g�{�b�N�X�̂���Ɠ������ɂ��܂��B
		/// ���ɁA���ϕ������̌v�Z�@���G�f�B�b�g�{�b�N�X�Ɠ����ɂ��A�܂��A�̈���͂ݏo��s�͕`�悵�܂���B
		/// </summary>
		public const WS EDITCONTROL=(WS)0x00002000;
		/// <summary>
		/// �܂��� SS_PATHELLIPSIS   �w�肳�ꂽ�����񂪎w�肳�ꂽ�l�p�`�Ɏ��܂�悤�ɁA������̈ꕔ���ȗ��L�� (...) �ɒu�������܂��B
		/// </summary>
		public const WS ENDELLIPSIS=(WS)0x00004000;
		/// <summary>
		/// �����񂪒����ĕ\��������Ȃ��ꍇ�ɁA������̖��[�� "..." �ɒu�������܂��B
		/// ����̕����Ƀo�b�N�X���b�V�� \ ���w�肷�鎖�ɂ���āA���̕������o���邾���ȗ����Ȃ��l�Ɏw�肷�鎖���o���܂��B
		/// </summary>
		public const WS PATHELLIPSIS=(WS)0x00008000;
		/// <summary>
		/// ���܂肫��Ȃ��e�L�X�g��؂�l�߁A�ȗ��L����ǉ����܂��B
		/// </summary>
		public const WS WORDELLIPSIS=(WS)0x0000C000;
		/// <summary>
		/// ���܂肫��Ȃ��e�L�X�g�̕\�����@���w�肷�镔���̃r�b�g�}�X�N�ł��B
		/// </summary>
		public const WS ELLIPSISMASK=(WS)0x0000C000;
	}
	/// <summary>
	/// �E�B���h�E�̊g���X�^�C����\���܂��B
	/// </summary>
	[System.Flags]
	public enum WS_EX:uint{
		/// <summary>
		/// ��d�̋��E�����E�B���h�E���w�肵�܂��B
		/// �p�����[�^ dwStyle �� WS_CAPTION �X�^�C�� �t���O���w�肷�邱�Ƃɂ��A�^�C�g�� �o�[��ǉ����邱�Ƃ��ł��܂��B
		/// </summary>
		DLGMODALFRAME =0x00000001,
		/// <summary>
		/// ���̃X�^�C�����w�肳��Ă���q�E�B���h�E�́A�쐬�܂��͔j�������Ƃ��ɐe�E�B���h�E�� WM_PARENTNOTIFY ���b�Z�[�W�𑗂�܂���B
		/// </summary>
		NOPARENTNOTIFY =0x00000004,
		/// <summary>
		/// ���̃X�^�C���ō쐬���ꂽ�E�B���h�E�́A���ׂẴE�B���h�E�̏�ɔz�u����A�A�N�e�B�u�łȂ��Ȃ����ꍇ�ł��A���������ق��̃E�B���h�E�̏�ɕ\�����ꂽ�܂܂ɂȂ�܂��B
		/// �A�v���P�[�V�����́ASetWindowPos �����o�֐����g���āA���̑�����ǉ�����э폜�ł��܂��B
		/// </summary>
		TOPMOST   =0x00000008,
		/// <summary>
		/// ���̃X�^�C���ō쐬���ꂽ�E�B���h�E�ł́A�t�@�C�����h���b�O �A���h �h���b�v�ł��܂��B
		/// </summary>
		ACCEPTFILES  =0x00000010,
		/// <summary>
		/// ���̃X�^�C���ō쐬���ꂽ�E�B���h�E�͓����ɂȂ�܂��B
		/// �܂�A���̃E�B���h�E�̉��ɂ���E�B���h�E�������Ȃ��Ȃ邱�Ƃ͂���܂���B
		/// ���̃X�^�C���ō쐬���ꂽ�E�B���h�E�́A���̃E�B���h�E�̉��ɂ���Z��E�B���h�E�����ׂčX�V���ꂽ��ɂ����AWM_PAINT ���b�Z�[�W���󂯎��܂��B
		/// </summary>
		TRANSPARENT  =0x00000020,

		/// <summary>
		/// MDI �q�E�B���h�E���쐬���܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		MDICHILD  =0x00000040,
		/// <summary>
		/// �c�[�� �E�B���h�E���쐬���܂��B
		/// ���̃E�B���h�E�́A�t���[�e�B���O �c�[�� �o�[�Ƃ��Ďg�p���܂��B
		/// �c�[�� �E�B���h�E�̃^�C�g�� �o�[�͒ʏ�����Z���A�E�B���h�E�̃^�C�g���͂�菬�����t�H���g�ŕ`�悳��܂��B
		/// �^�X�N �o�[��A���[�U�[�� Alt �L�[�������Ȃ��� Tab �L�[�������ĕ\�������E�B���h�E�ɂ́A�c�[�� �E�B���h�E�͕\������܂���B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		TOOLWINDOW  =0x00000080,
		/// <summary>
		/// ���̕����o�������E�������E�B���h�E���w�肵�܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		WINDOWEDGE  =0x00000100,
		/// <summary>
		/// �E�B���h�E�� 3 �����ŕ\�����邱�Ƃ��w�肵�܂��B
		/// �܂�A�E�B���h�E�ɂ́A���ڂ񂾋��E�����t���܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		CLIENTEDGE  =0x00000200,
		/// <summary>
		/// �E�B���h�E�̃^�C�g�� �o�[�ɋ^�╄ (?) ��ǉ����܂��B
		/// ���[�U�[���^�╄ (?) ���N���b�N����ƁA�J�[�\�����|�C���^�̕t�����^�╄ (?) �ɕς��܂��B
		/// �����ă��[�U�[���q�E�B���h�E���N���b�N����ƁA���̎q�E�B���h�E�� WM_HELP ���b�Z�[�W���󂯎��܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		CONTEXTHELP  =0x00000400,

		/// <summary>
		/// �E�B���h�E�ɔėp�E�����v���p�e�B���w�肵�܂��B
		/// ���̃X�^�C���́A�E�B���h�E �N���X�Ɉˑ����܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		RIGHT   =0x00001000,
		/// <summary>
		/// �E�B���h�E�ɔėp�������v���p�e�B���w�肵�܂��B
		/// ����́A����̐ݒ�ł��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		LEFT=0x00000000,
		/// <summary>
		/// �E���獶�ւ̓ǂݎ�菇���ŃE�B���h�E�̃e�L�X�g��\�����܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		RTLREADING  =0x00002000,
		/// <summary>
		/// ������E�ւ̓ǂݎ�菇���ŃE�B���h�E�̃e�L�X�g��\�����܂��B
		/// ����́A����̐ݒ�ł��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		LTRREADING=0x00000000,
		/// <summary>
		/// �N���C�A���g�̈�̍��ɐ����X�N���[�� �o�[��z�u���܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		LEFTSCROLLBAR =0x00004000,
		/// <summary>
		/// �N���C�A���g�̈�̉E�ɐ����X�N���[�� �o�[ (���݂���ꍇ) ��z�u���܂��B
		/// ����́A����̐ݒ�ł��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		RIGHTSCROLLBAR=0x00000000,

		/// <summary>
		/// ���[�U�[���ATab �L�[���g���ăE�B���h�E���̎q�E�B���h�E�Ԃ��ړ��ł���悤�ɂ��܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		CONTROLPARENT =0x00010000,
		/// <summary>
		/// 3 �����̋��E���X�^�C�������E�B���h�E���쐬���܂��B
		/// ���̃E�B���h�E�́A���[�U�[�̓��͂��󂯕t���Ȃ����ڗp�Ɏg�p���܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		STATICEDGE  =0x00020000,
		/// <summary>
		/// ��ԏ�ɂ���E�B���h�E��\������Ƃ��ɁA�����I�Ƀ^�X�N�o�[�Ɋ܂݂܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		APPWINDOW  =0x00040000,

		/// <summary>
		/// WS_EX_CLIENTEDGE �X�^�C���� WS_EX_WINDOWEDGE �X�^�C����g�ݍ��킹�܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		OVERLAPPEDWINDOW=WINDOWEDGE|CLIENTEDGE,
		/// <summary>
		/// WS_EX_WINDOWEDGE �X�^�C���� WS_EX_TOPMOST �X�^�C����g�ݍ��킹�܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0400</remarks>
		PALETTEWINDOW=WINDOWEDGE|TOOLWINDOW|TOPMOST,


		/// <summary>
		/// Windows 2000�F���C���[�E�B���h�E ���쐬���܂��B
		/// </summary>
		/// <remarks>_WIN32_WINNT >= 0x0500</remarks>
		LAYERED   =0x00080000,

		/// <summary>
		/// �q�E�B���h�E�ւ̔��]���C�A�E�g�̌p���𖳌��ɂ��܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0500</remarks>
		NOINHERITLAYOUT =0x00100000, // Disable inheritence of mirroring by children
		/// <summary>
		/// �R���g���[���y�ѕ����̔z�u���E���獶�ɔ��]���܂��B
		/// </summary>
		/// <remarks>WINVER >= 0x0500</remarks>
		LAYOUTRTL  =0x00400000, // Right to left mirroring

		/// <summary>
		/// �E�B���h�E�̕`�掞�Ƀ_�u���o�b�t�@�����O���g�p���܂��B
		/// </summary>
		/// <remarks>_WIN32_WINNT >= 0x0500</remarks>
		COMPOSITED  =0x02000000,
		/// <summary>
		/// Windows 2000�F���̃X�^�C���ō쐬���ꂽ�g�b�v���x���E�B���h�E�́A���[�U�[���N���b�N���Ă��t�H�A�O���E���h�E�B���h�E�ɂȂ�܂���B���[�U�[���t�H�A�O���E���h�E�B���h�E���ŏ�������������肵���Ƃ��ɂ��A�V�X�e�������̃E�B���h�E���t�H�A�O���E���h�E�B���h�E�ɂ��邱�Ƃ͂���܂���B 
		/// ���̃E�B���h�E���A�N�e�B�u�ɂ���ɂ́ASetActiveWindow �֐��܂��� SetForegroundWindow �֐����g���܂��B
		/// ����ł́A���̃E�B���h�E�̓^�X�N�o�[�ɂ͕\������܂���B�E�B���h�E���^�X�N�o�[�ɕ\�������悤�ɂ���ɂ́AWS_EX_APPWINDOW �X�^�C�����w�肵�܂��B
		/// </summary>
		/// <remarks>_WIN32_WINNT >= 0x0500</remarks>
		NOACTIVATE  =0x08000000
	}
}