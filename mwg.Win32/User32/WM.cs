namespace mwg.Win32{
	/// <summary>
	/// Window Message �E�B���h�E���b�Z�[�W��\���񋓑̂ł��B
	/// </summary>
	public enum WM:uint{
		/// <summary>
		/// ���̓�������܂���B��M���� Window �ɖ�������郁�b�Z�[�W�𑗂肽�����Ɏg�p���܂��B
		/// </summary>
		/// <wparam>�g�p����܂���B</wparam>
		/// <lparam>�g�p����܂���B</lparam>
		/// <return>���̃��b�Z�[�W���������ꂽ���� 0 ��Ԃ��܂��B</return>
		/// <remarks>
		/// �Ⴆ�΁A�A�v���P�[�V������ WH_GETMESSAGE �t�b�N��ݒu���Ă��鎞�ɁA��M�E�B���h�E�����b�Z�[�W����������̂�W���鎖���o���܂��B
		/// ��̓I�ɂ� GetMsgProc �֐����Ń��b�Z�[�W�� WM_NULL �ɕύX���鎖�ɂ���āA��M�E�B���h�E�̓��b�Z�[�W�𖳎�����l�ɂȂ�܂��B
		/// <para>���̗�Ƃ��ẮA�A�v���P�[�V������ WM_NULL �� SendMessageTimeout ���M���鎖�ɂ����
		/// ���� Window ���������邩�ǂ������m�F���鎖���o���܂��B
		/// </para>
		/// </remarks>
		NULL = 0x00,
		/// <summary>
		/// ���̃��b�Z�[�W�� CreateWindowEx ���� CreateWindow �֐��ɂ���� window ���쐬���鎞�ɑ����܂��B
		/// (���̃��b�Z�[�W�� CreateWindowEx/CreateWindow �֐�����Ԃ�O�ɑ����܂��B)
		/// �쐬���ꂽ window �� window procedure �� window ���\�������O�ɂ��̃��b�Z�[�W���󂯎��܂��B
		/// <para>���̃��b�Z�[�W�� WindowProc �֐���ʂ��Ď�M����܂��B</para>
		/// </summary>
		/// <wparam>�g�p����܂���B</wparam>
		/// <lparam>�쐬���ꂽ�E�B���h�E�Ɋւ������ێ����� CREATESTRUCT �\���̂ւ̃|�C���^���w�肵�܂��B</lparam>
		/// <return>
		/// ���̃��b�Z�[�W������ɏ�������Awindow �̍쐬���p������ꍇ�ɂ� 0 ��Ԃ��܂��B
		/// -1 ��Ԃ����ꍇ�ɂ� window �͔j������ACreateWindowEx / CreateWindow �֐��� NULL �n���h����Ԃ��܂��B
		/// </return>
		CREATE = 0x01,
		DESTROY = 0x02,
		MOVE = 0x03,
		SIZE = 0x05,
		ACTIVATE = 0x06,
		SETFOCUS = 0x07,
		/// <summary>
		/// �L�[�{�[�h�t�H�[�J�X���������O�̃E�B���h�E�ɑ����܂��B
		/// </summary>
		/// <wparam>�L�[�{�[�h�t�H�[�J�X�̍s����̃E�B���h�E�n���h���BNULL �̉\��������܂��B</wparam>
		/// <lparam>�g�p����܂���B</lparam>
		/// <return>���̃��b�Z�[�W������������ 0 ��Ԃ��܂��B</return>
		/// <remarks>
		/// �L�����b�g��\�����Ă���ꍇ�ɂ͂��̎��_�ŃL�����b�g��j������ׂ��ł��B
		/// ���̃��b�Z�[�W���������Ă���ԂɃE�B���h�E��`�悷��֐����Ăяo���Ȃ��ŉ������B
		/// ���b�Z�[�W�f�b�h���b�N���������܂��B
		/// </remarks>
		KILLFOCUS = 0x08,
		ENABLE = 0x0A,
		SETREDRAW = 0x0B,
		SETTEXT = 0x0C,
		GETTEXT = 0x0D,
		GETTEXTLENGTH = 0x0E,
		PAINT = 0x0F,
		CLOSE = 0x10,
		QUERYENDSESSION = 0x11,
		QUIT = 0x12,
		QUERYOPEN = 0x13,
		ERASEBKGND = 0x14,
		SYSCOLORCHANGE = 0x15,
		ENDSESSION = 0x16,
		SYSTEMERROR = 0x17,
		SHOWWINDOW = 0x18,
		CTLCOLOR = 0x19,
		WININICHANGE = 0x1A,
		SETTINGCHANGE = 0x1A,
		DEVMODECHANGE = 0x1B,
		ACTIVATEAPP = 0x1C,
		FONTCHANGE = 0x1D,
		TIMECHANGE = 0x1E,
		CANCELMODE = 0x1F,
		SETCURSOR = 0x20,
		MOUSEACTIVATE = 0x21,
		CHILDACTIVATE = 0x22,
		QUEUESYNC = 0x23,
		GETMINMAXINFO = 0x24,
		PAINTICON = 0x26,
		ICONERASEBKGND = 0x27,
		NEXTDLGCTL = 0x28,
		SPOOLERSTATUS = 0x2A,
		DRAWITEM = 0x2B,
		MEASUREITEM = 0x2C,
		DELETEITEM = 0x2D,
		VKEYTOITEM = 0x2E,
		CHARTOITEM = 0x2F,
		SETFONT = 0x30,
		GETFONT = 0x31,
		SETHOTKEY = 0x32,
		GETHOTKEY = 0x33,
		QUERYDRAGICON = 0x37,
		COMPAREITEM = 0x39,
		COMPACTING = 0x41,
		WINDOWPOSCHANGING = 0x46,
		WINDOWPOSCHANGED = 0x47,
		POWER = 0x48,
		COPYDATA = 0x4A,
		CANCELJOURNAL = 0x4B,
		NOTIFY = 0x4E,
		INPUTLANGCHANGEREQUEST = 0x50,
		INPUTLANGCHANGE = 0x51,
		TCARD = 0x52,
		HELP = 0x53,
		USERCHANGED = 0x54,
		NOTIFYFORMAT = 0x55,
		CONTEXTMENU = 0x7B,
		STYLECHANGING = 0x7C,
		STYLECHANGED = 0x7D,
		DISPLAYCHANGE = 0x7E,
		GETICON = 0x7F,
		SETICON = 0x80,
		NCCREATE = 0x81,
		NCDESTROY = 0x82,
		NCCALCSIZE = 0x83,
		NCHITTEST = 0x84,
		NCPAINT = 0x85,
		NCACTIVATE = 0x86,
		GETDLGCODE = 0x87,
		NCMOUSEMOVE = 0xA0,
		NCLBUTTONDOWN = 0xA1,
		NCLBUTTONUP = 0xA2,
		NCLBUTTONDBLCLK = 0xA3,
		NCRBUTTONDOWN = 0xA4,
		NCRBUTTONUP = 0xA5,
		NCRBUTTONDBLCLK = 0xA6,
		NCMBUTTONDOWN = 0xA7,
		NCMBUTTONUP = 0xA8,
		NCMBUTTONDBLCLK = 0xA9,
		KEYFIRST = 0x100,
		KEYDOWN = 0x100,
		KEYUP = 0x101,
		CHAR = 0x102,
		DEADCHAR = 0x103,
		SYSKEYDOWN = 0x104,
		SYSKEYUP = 0x105,
		SYSCHAR = 0x106,
		SYSDEADCHAR = 0x107,
		KEYLAST = 0x108,
		IME_STARTCOMPOSITION = 0x10D,
		IME_ENDCOMPOSITION = 0x10E,
		IME_COMPOSITION = 0x10F,
		IME_KEYLAST = 0x10F,
		INITDIALOG = 0x110,
		COMMAND = 0x111,
		SYSCOMMAND = 0x112,
		TIMER = 0x113,
		HSCROLL = 0x114,
		VSCROLL = 0x115,
		INITMENU = 0x116,
		INITMENUPOPUP = 0x117,
		MENUSELECT = 0x11F,
		MENUCHAR = 0x120,
		ENTERIDLE = 0x121,
		CTLCOLORMSGBOX = 0x132,
		CTLCOLOREDIT = 0x133,
		CTLCOLORLISTBOX = 0x134,
		CTLCOLORBTN = 0x135,
		CTLCOLORDLG = 0x136,
		CTLCOLORSCROLLBAR = 0x137,
		CTLCOLORSTATIC = 0x138,
		MOUSEFIRST = 0x200,
		MOUSEMOVE = 0x200,
		LBUTTONDOWN = 0x201,
		LBUTTONUP = 0x202,
		LBUTTONDBLCLK = 0x203,
		RBUTTONDOWN = 0x204,
		RBUTTONUP = 0x205,
		RBUTTONDBLCLK = 0x206,
		MBUTTONDOWN = 0x207,
		MBUTTONUP = 0x208,
		MBUTTONDBLCLK = 0x209,
		MOUSELAST = 0x20A,
		MOUSEWHEEL = 0x20A,
		PARENTNOTIFY = 0x210,
		ENTERMENULOOP = 0x211,
		EXITMENULOOP = 0x212,
		NEXTMENU = 0x213,
		SIZING = 0x214,
		CAPTURECHANGED = 0x215,
		MOVING = 0x216,
		POWERBROADCAST = 0x218,
		DEVICECHANGE = 0x219,
		MDICREATE = 0x220,
		MDIDESTROY = 0x221,
		MDIACTIVATE = 0x222,
		MDIRESTORE = 0x223,
		MDINEXT = 0x224,
		MDIMAXIMIZE = 0x225,
		MDITILE = 0x226,
		MDICASCADE = 0x227,
		MDIICONARRANGE = 0x228,
		MDIGETACTIVE = 0x229,
		MDISETMENU = 0x230,
		ENTERSIZEMOVE = 0x231,
		EXITSIZEMOVE = 0x232,
		DROPFILES = 0x233,
		MDIREFRESHMENU = 0x234,
		IME_SETCONTEXT = 0x281,
		IME_NOTIFY = 0x282,
		IME_CONTROL = 0x283,
		IME_COMPOSITIONFULL = 0x284,
		IME_SELECT = 0x285,
		IME_CHAR = 0x286,
		IME_KEYDOWN = 0x290,
		IME_KEYUP = 0x291,
		MOUSEHOVER = 0x2A1,
		NCMOUSELEAVE = 0x2A2,
		MOUSELEAVE = 0x2A3,
		CUT = 0x300,
		COPY = 0x301,
		PASTE = 0x302,
		CLEAR = 0x303,
		UNDO = 0x304,
		RENDERFORMAT = 0x305,
		RENDERALLFORMATS = 0x306,
		DESTROYCLIPBOARD = 0x307,
		DRAWCLIPBOARD = 0x308,
		PAINTCLIPBOARD = 0x309,
		VSCROLLCLIPBOARD = 0x30A,
		SIZECLIPBOARD = 0x30B,
		ASKCBFORMATNAME = 0x30C,
		CHANGECBCHAIN = 0x30D,
		HSCROLLCLIPBOARD = 0x30E,
		QUERYNEWPALETTE = 0x30F,
		PALETTEISCHANGING = 0x310,
		PALETTECHANGED = 0x311,
		HOTKEY = 0x312,
		PRINT = 0x317,
		PRINTCLIENT = 0x318,
		HANDHELDFIRST = 0x358,
		HANDHELDLAST = 0x35F,
		PENWINFIRST = 0x380,
		PENWINLAST = 0x38F,
		COALESCE_FIRST = 0x390,
		COALESCE_LAST = 0x39F,
		DDE_FIRST = 0x3E0,
		DDE_INITIATE = 0x3E0,
		DDE_TERMINATE = 0x3E1,
		DDE_ADVISE = 0x3E2,
		DDE_UNADVISE = 0x3E3,
		DDE_ACK = 0x3E4,
		DDE_DATA = 0x3E5,
		DDE_REQUEST = 0x3E6,
		DDE_POKE = 0x3E7,
		DDE_EXECUTE = 0x3E8,
		DDE_LAST = 0x3E8,
		USER = 0x400,
		APP = 0x8000
	}
	/// <summary>
	/// �G�f�B�b�g�R���g���[���Ɋւ��郁�b�Z�[�W�ł��B
	/// </summary>
	public static class EM{
		//===========================================================
		//		WinUser.h
		//===========================================================
		/// <summary>
		/// Edit �R���g���[���̌��݂̑I��͈͂̎n�߂ƏI���̈ʒu���擾���܂��B
		/// Edit/RichEdit �ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>�I��͈͂̏��߂̈ʒu���󂯎��ׂ̃o�b�t�@�ւ̃|�C���^���w�肵�܂��BNULL ���w�肷�鎖���\�ł��B</wparam>
		/// <lparam>�I��͈̖͂��[�̈ʒu���󂯎��ׂ̃o�b�t�@�ւ̃|�C���^���w�肵�܂��BNULL ���w�肷�鎖���\�ł��B</lparam>
		/// <return>
		/// ��ʃ��[�h�ɑI��͈͂̏��߂̈ʒu��Ԃ��܂��B
		/// ��ʃ��[�h�ɑI��͈̖͂��[�̈ʒu��Ԃ��܂��B
		/// �ǂ��炩�̒l�� 0xFFFF ���z����ꍇ�ɂ� -1 ��Ԃ��܂��B
		/// wParam �y�� lParam �ɕԂ����l���g�p���鎖����������܂��B
		/// </return>
		/// <remarks>
		/// [RichEdit]: �����̏����擾����̂� EM_EXGETSEL ���g�p���鎖���o���܂��B
		/// </remarks>
		public const WM GETSEL=(WM)0x00B0;
		/// <summary>
		/// Edit �R���g���[�����̘A�����镶�����I�����܂��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>�I��͈͂̊J�n�ʒu���w�肵�܂��B</wparam>
		/// <lparam>�I��͈͂̏I���ʒu���w�肵�܂��B</lparam>
		/// <return>�l�͕Ԃ��܂���B</return>
		/// <remarks>
		/// ���߂̈ʒu�̒l�͖��[�̈ʒu�̒l�����傫���l���w�肷�鎖���\�ł��B
		/// ��菬���������I��͈͂̏��߂̈ʒu�ɂȂ�A���傫�������I��͈̖͂��[�ɂȂ�܂��B
		/// �J�n�ʒu�͌Œ肳�ꂽ�[�ŁA�I���ʒu�͌��݃A�N�e�B�u�ɂȂ��Ă���ʒu�ł��B
		/// ���[�U�� SHIFT ���g�p���đI��͈͂𒲐߂���ۂ́A
		/// �A�N�e�B�u�ɂȂ��Ă��鑤�������܂����A�Œ�ʒu�͓����܂���B
		/// �J�n�ʒu�� 0 ���w�肵�I���ʒu�� -1 ���w�肷��� Edit �R���g���[�����̑S�Ă̕����񂪑I������܂��B
		/// �J�n�ʒu�� -1 �̎��ɂ͌��݂̑I��͈͂͑S�ĉ�������܂��B
		/// [Edit]: �R���g���[���̓L�����b�g���I���ʒu�ɕ\�����܂��B
		/// </remarks>
		public const WM SETSEL=(WM)0x00B1;
		/// <summary>
		/// Edit �R���g���[���̐��`��`���擾���܂��B
		/// ���`��`�͕������`�悷��̈�������܂��B
		/// ���̋�`�� Edit �R���g���[���̃E�B���h�E���̂̋�`�Ƃ͕ʂ̕��ł��B
		/// </summary>
		/// <wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <lparam>��`�����󂯎��ׂ� RECT �ւ̃|�C���^���w�肵�܂��B</lparam>
		/// <return>�߂�l�ɂ͓��ɈӖ��͂���܂���B</return>
		/// <remarks>
		/// Edit �R���g���[���̐��`��`�� EM_SETRECT/EM_SETRECTNP ���g�p���ĕύX���鎖���o���܂��B
		/// ����̏������ł́AEM_GETRECT �ɂ���ē�����l�� EM_SETRECT/EM_SETRECTNP �Őݒ肵���l�ƌ����ɂ͈�v���܂���B
		/// 2, 3 �s�N�Z���̌덷�������鎖������܂��B
		/// [RichEdit]: ���`��`�ɑI���o�[�͊܂܂�܂���B�I���o�[�͐��`��`�̍����ɂ���A�������N���b�N���鎖�ɂ��s��I���ł��镨�ł��B
		/// </remarks>
		public const WM GETRECT=(WM)0x00B2;
		/// <summary>
		/// �����s Edit �R���g���[���̐��`��`��ݒ肵�܂��B
		/// ���`��`�͕������`�悷��̈�������܂��B
		/// ���̋�`�� Edit �R���g���[���̃E�B���h�E���̂̋�`�Ƃ͕ʂ̕��ł��B
		/// </summary>
		/// <wparam>
		/// [RichEdit 2.0 �ȍ~]: lParam ����΍��W��\�������΍��W��\�������w�肵�܂��B
		/// 0 �͐�΍��W�ł��鎖�������܂��B1 �͌��݂̐��`��`�ɑ΂��鑊�΍��W (�����̂ǂ�����\) �ł��鎖�������܂��B
		/// [Edit/RichEdit 1.0]: ��� 0 ���w�肵�܂��B
		/// </wparam>
		/// <lparam>�V�������`��`�̒l��ێ����� RECT �ւ̃|�C���^���w�肵�܂��BNULL ���w�肵���ꍇ�A���`��`������̒l�ɖ߂��܂��B</lparam>
		/// <return>�l�͕Ԃ��܂���B</return>
		/// <remarks>
		/// ���̃��b�Z�[�W�ɂ���� Edit �R���g���[�����̕�����͍ĕ`�悳��܂��B�ĕ`�悷�鎖�ׂ��ɕ������ύX�������ꍇ�ɂ� EM_SETRECTNP ���g�p���ĉ������B
		/// Edit �R���g���[�����쐬���ꂽ���ɐ��`��`�͊���̑傫���ɐݒ肳��܂��B
		/// ���`��`�̑傫�����R���g���[���E�B���h�E�̑傫�����傫�������菬���������肵�����ꍇ�ɂ� EM_SETRECT ���g�p���܂��B
		/// Edit �R���g���[���������X�N���[���o�[�����������`��`�� Edit �R���g���[���E�B���h�E�����傫���ꍇ�ɂ́A
		/// �R���g���[���E�B���h�E��蒷���s�̓��[�h���b�v��������ɐ擪�̂ݐ؂蔲����ĕ\������܂��B
		/// Edit �R���g���[�������E�����ꍇ�͐��`��`�͂��̕��������Ȃ�܂��BEM_GETRECT �ɂ���ē�����l���ȂĐ��`��`�̑傫���𒲐�����ꍇ�ɂ� EM_SETRECT �𑗂�O�ɃR���g���[���̋��E����菜���K�v������܂��B
		/// [RichEdit]: ���`��`�ɑI���o�[�͊܂܂�܂���B�I���o�[�͐��`��`�̍����ɂ���A�������N���b�N���鎖�ɂ��s��I���ł��镨�ł��B
		/// </remarks>
		public const WM SETRECT=(WM)0x00B3;
		/// <summary>
		/// �����s Edit �R���g���[���̐��`��`��ݒ肵�܂��B����́AEdit �R���g���[���E�B���h�E���ĕ`�悵�Ȃ��Ƃ����_�������Ă� EM_SETRECT �Ɠ����ł��B
		/// ���`��`�͕������`�悷��̈�������܂��B���̋�`�� Edit �R���g���[���̃E�B���h�E���̂̋�`�Ƃ͕ʂ̕��ł��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B�A���A�P��s Edit �R���g���[���ł͏�������܂���B
		/// </summary>
		/// <wparam>
		/// [RichEdit 3.0 �ȍ~]: lParam ����΍��W��\�������΍��W��\�������w�肵�܂��B
		/// 0 �͐�΍��W�ł��鎖�������܂��B1 �͌��݂̐��`��`�ɑ΂��鑊�΍��W (�����̂ǂ�����\) �ł��鎖�������܂��B
		/// [Edit]: ��� 0 ���w�肵�܂��B
		/// </wparam>
		/// <lparam>�V�������`��`�̒l��ێ����� RECT �ւ̃|�C���^���w�肵�܂��BNULL ���w�肵���ꍇ�A���`��`������̒l�ɖ߂��܂��B</lparam>
		/// <return>�l�͕Ԃ��܂���B</return>
		/// <remarks>
		/// [RichEdit]: 3.0 �ȍ~�őΉ����Ă��܂��B
		/// </remarks>
		public const WM SETRECTNP=(WM)0x00B4;
		public const WM SCROLL=(WM)0x00B5;
		public const WM LINESCROLL=(WM)0x00B6;
		public const WM SCROLLCARET=(WM)0x00B7;
		/// <summary>
		/// Edit �R���g���[���̕ύX�σt���O�̏�Ԃ��擾���܂��B
		/// �ύX�σt���b�O�� Edit �R���g���[�����̕����񂪕ύX���ꂽ���ǂ������w�肵�܂��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>Edit �R���g���[���̓��e�ɕύX���ׂ���Ă��鎞�� 0 �ȊO�̒l��Ԃ��܂��B</return>
		/// <remarks>
		/// �R���g���[���쐬���ɃV�X�e���͎����I�ɕύX�σt���b�O�� 0 �ɂ��܂��B
		/// ���A�����񂪃��[�U�ɂ���ĕύX���ꂽ�ꍇ�ɂ̓V�X�e���͕ύX�σt���b�O�� 0 �łȂ��l�ɕύX���܂��B
		/// </remarks>
		public const WM GETMODIFY=(WM)0x00B8;
		/// <summary>
		/// Edit �R���g���[���ɑ΂���ύX�σt���O��ݒ肵�܂��B
		/// �ύX�σt���b�O�� Edit �R���g���[�����̕����񂪕ύX���ꂽ���ǂ������w�肵�܂��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>�ύX�σt���O�̐V�����l���w�肵�܂��B
		/// TRUE �̒l�͕����񂪕ύX���ꂽ���������܂��BFALSE �̒l�͕����񂪕ύX����Ă��Ȃ����������܂��B</wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>�l�͕Ԃ��܂���B</return>
		/// <remarks>
		/// �R���g���[���쐬���ɃV�X�e���͎����I�ɕύX�σt���b�O�� 0 �ɂ��܂��B
		/// ���A�����񂪃��[�U�ɂ���ĕύX���ꂽ�ꍇ�ɂ̓V�X�e���͕ύX�σt���b�O�� 0 �łȂ��l�ɕύX���܂��B
		/// [RichEdit 1.0]: Objects created without the REO_DYNAMICSIZE flag will lock in their extents when the modify flag is set to FALSE.
		/// </remarks>
		public const WM SETMODIFY=(WM)0x00B9;
		/// <summary>
		/// �����s Edit �R���g���[���̍s�����擾���܂��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>�����s Edit �R���g���[������ RichEdit �R���g���[���̒��̑S�s���������l��Ԃ��܂��B</return>
		/// <remarks>
		/// ���݌����Ă���s���ł͂Ȃ��āA�R���g���[�����ɂ���S�Ă̍s�̐����擾���܂��B
		/// ���[�h���b�v�@�\���L���ɂȂ��Ă���ꍇ�ɂ́AEdit �R���g���[���̑傫�����ω������ۂɍs�����ω�����\��������܂��B
		/// </remarks>
		public const WM GETLINECOUNT=(WM)0x00BA;
		public const WM LINEINDEX=(WM)0x00BB;
		/// <summary>
		/// �����s Edit �R���g���[�����g�p���郁�����̃n���h�����w�肵�܂��B
		/// </summary>
		/// <wparam>���ݕ\������Ă��镶������i�[����ׂɎg�p����V�����������o�b�t�@�ւ̃n���h�����w�肵�܂��B
		/// �K�v������ꍇ�ɂ̓R���g���[���͂��̃��������Ĕz�u���܂��B</wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>�l�͕Ԃ��܂���B</return>
		public const WM SETHANDLE=(WM)0x00BC;
		/// <summary>
		/// �����s Edit �R���g���[���̕�����Ɋ��蓖�Ă��Ă��郁�����̃n���h�����擾���܂��B
		/// </summary>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam><wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <return>Edit �R���g���[���̓��e��ێ�����o�b�t�@�̃������n���h����Ԃ��܂��B
		/// �P��s Edit �R���g���[���Ƀ��b�Z�[�W�𑗂�Ȃǂ��ăG���[���N�������ꍇ�ɂ� 0 ��Ԃ��܂��B</return>
		/// <remarks>
		/// �Ԃ��Ă����n���h���� HLOCAL �ɃL���X�g���ALocalLock �ɓn���ē����|�C���^���g�p���ē��e�ɃA�N�Z�X���鎖���\�ɂȂ�܂��B
		/// ���̃|�C���^�́A�R���g���[�����쐬�����֐��̕����Z�b�g�ɉ����� CHAR ���� WCHAR �� null �I���̔z����w���܂��B
		/// �g�p������� LocalUnlock ���g�p���� Edit �R���g���[�����V�������������s���鎖���o����l�ɂ��Ȃ���΂Ȃ�܂���B
		/// ���A�A�v���P�[�V�����͂��̓��e��ύX���鎖�͏o���܂���B���e��ύX�������ꍇ�ɂ́AGetWindowText ���g�p���ē��e�������ŗp�ӂ����o�b�t�@�ɃR�s�[���ĉ������B
		/// [RichEdit]: RichEdit �R���g���[���͕������P���Ȕz��Ƃ��Ċi�[���Ă����ł͂Ȃ��̂ł��̃��b�Z�[�W���g�p���鎖�͏o���܂���B
		/// </remarks>
		public const WM GETHANDLE=(WM)0x00BD;
		/// <summary>
		/// �����s Edit �{�b�N�X�̐����X�N���[���o�[�̃T�� (�X�N���[���{�b�N�X) �̈ʒu���擾���܂��B
		/// </summary>
		/// <wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>�X�N���[���{�b�N�X�̈ʒu��Ԃ��܂��B</return>
		/// <remarks>[RichEdit]: 2.0 �ȍ~�őΉ��w�肵�Ă��܂��B</remarks>
		public const WM GETTHUMB=(WM)0x00BE;
		public const WM LINELENGTH=(WM)0x00C1;
		public const WM REPLACESEL=(WM)0x00C2;
		/// <summary>
		/// �w�肵���s�̓��e���w�肵���o�b�t�@�ɃR�s�[���܂��B
		/// </summary>
		/// <wparam>�s�̃C���f�b�N�X���w�肵�܂��B�P��s Edit �R���g���[���ł͖�������܂��B</wparam>
		/// <lparam>
		/// ��������擾����ׂ̃o�b�t�@�ւ̃|�C���^���w�肵�܂��B
		/// ���b�Z�[�W�𑗂�O�ɁA�o�b�t�@�̃T�C�Y�� TCHAR �P�ʂŃo�b�t�@�� 1 word �ڂɏ�������ł����ĉ������B
		/// </lparam>
		/// <return>�R�s�[���ꂽ�����̐���Ԃ��܂��BwParam �� Edit �R���g���[�������ݎ��s���ȏ�̒l��ݒ肵���ꍇ�ɂ� 0 ��Ԃ��܂��B
		/// </return>
		/// <remarks>
		/// [Edit]: �R�s�[���ꂽ�s�� null �I���ł͂���܂���B
		/// [RichEdit]: �R�s�[���ꂽ�s�� null �I���ł͂���܂���B�A���A���̕�������R�s�[����Ȃ������ꍇ�ɂ� 1 �����ڂ� null ��ݒ肵�܂��B
		/// </remarks>
		public const WM GETLINE=(WM)0x00C4;
		/// <summary>
		/// EM_SETLIMITTEXT ���Q�Ƃ��ĉ������B
		/// </summary>
		public const WM LIMITTEXT=(WM)0x00C5;
		/// <summary>
		/// �u���ɖ߂��v��������ݎ��s�o���邩�ǂ��������肵�܂��B
		/// Edit �R���g���[������ RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>�u���ɖ߂��v��������s�\�̎��� 0 �ȊO�̒l���Ԃ�܂��B</return>
		public const WM CANUNDO=(WM)0x00C6;
		public const WM UNDO=(WM)0x00C7;
		/// <summary>
		/// �����s Edit �R���g���[���Ń\�t�g���s���g�p���邩�ǂ�����ݒ肵�܂��B
		/// �\�t�g���s�͓�� CR �y�ш�� LF ����Ȃ�(\r\r\n)�A���[�h���b�s���O�ɂ����s�̌�ɑ}������܂��B
		/// </summary>
		/// <wparam>TRUE ���w�肷��ƃ\�t�g���s��}�����鎖���w�肵�܂��B
		/// FALSE ���w�肷��ƃ\�t�g���s��}�����Ȃ������w�肵�܂��B</wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>wParam �Ɏw�肵���l��Ԃ��܂��B</return>
		/// <remarks>���̃��b�Z�[�W�� EM_GETHANDLE �ɂ���ĕԂ����o�b�t�@�A�y�� WM_GETTEXT �ɂ���ĕԂ���镶����݂̂ɉe�����܂��B
		/// �G�f�B�b�g�R���g���[���ւ̕\���ɂ͉������ʂ͂���܂���B
		/// EM_FMTLINES �͒ʏ�̉��s (\r\n) �ɂ͉e�����܂���B
		/// ���A���̃��b�Z�[�W���������鎖�ɂ�蕶����̒����͕ω����܂��B
		/// [RichEdit]: ���̃��b�Z�[�W�ɂ͑Ή����Ă��܂���B
		/// </remarks>
		public const WM FMTLINES=(WM)0x00C8;
		public const WM LINEFROMCHAR=(WM)0x00C9;
		/// <summary>
		/// �����s Edit �R���g���[���̃^�u�X�g�b�v�ʒu��ݒ肵�܂��B
		/// �R���g���[���ɓ��͂��ꂽ�^�u�͎��̃^�u�X�g�b�v�ʒu�܂ŋ󔒂��J���铭���������܂��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B�A���A���̃��b�Z�[�W�͒P��s Edit �R���g���[���ł͏�������܂���B
		/// �^�u�X�g�b�v�ʒu�̎w��ɂ͐��������� dialog-template-units ���g�p����܂��B
		/// 1 horizontal dialog-template-unit== 1/4 average width of the font �ł��B
		/// 䢂̐����ł� dtu �Ɨ��L���܂��B
		/// </summary>
		/// <wparam>lParam �Ŏw�肷��z��̒������w�肵�܂��B
		/// 0 ���w�肵���ꍇ�ɂ� 32dtu �u���Ƀ^�u�X�g�b�v�ʒu���ݒ肳��܂��B
		/// 1 ���w�肵���ꍇ�ɂ� lParam �̎Q�Ɛ�� unsigned int �̒����u���Ƀ^�u�X�g�b�v�ʒu���ݒ肳��܂��B
		/// 1 ���傫�Ȓl��ݒ肵���ꍇ�ɂ� lParam �̓^�u�X�g�b�v�ʒu�̔z��ւ̃|�C���^�ɂȂ�܂��B
		/// </wparam>
		/// <lparam>
		/// �^�u�X�g�b�v���w�肷�� unsigned int �̔z��ւ̃|�C���^���w�肵�܂��B
		/// wParam �� 1 ���w�肵���ꍇ�ɂ͂��ꂼ��̃^�u�X�g�b�v�̊Ԃ̊Ԋu���w�肷�� unsigned int �ւ̃|�C���^�ɂȂ�܂��B
		/// [95/98/Me]: ���̃��b�Z�[�W�ɂ���Ĕz�񂪕ύX����鎖�͂���܂��񂪁AlParam �ɂ���Ďw�肳���o�b�t�@�͏����\�ȃ������ɑ��݂���K�v������܂��B
		/// </lparam>
		/// <return>�w�肵���S�Ẵ^�u���ݒ肳�ꂽ�ꍇ�� TRUE ��Ԃ��܂��B</return>
		/// <remarks>
		/// EM_SETTABSTOPS ���b�Z�[�W�͎����I�ɂ͍ĕ`����s���܂���B
		/// ���ɕ����� Edit �R���g���[���ɐݒ肳��Ă����ԂŃ^�u�X�g�b�v�̈ʒu��ύX�������ꍇ�ɂ́AInvalidateRect �֐����Ăяo���� Edit �R���g���[���E�B���h�E���ĕ`�悳���Ȃ���΂Ȃ�܂���B
		/// �z��Ɏw�肳���l�� dtu �P�ʂł��Bdtu �P�ʂ̓_�C�A���O�{�b�N�X�̃e���v���[�g�Ɏg�p�����f�o�C�X�ˑ��̒P�ʂł��B
		/// dtu �P�ʂ���X�N���[���P�� (�s�N�Z��) �ɕϊ�����ɂ� MapDialogRect �֐����g�p���ĉ������B
		/// [RichEdit]: 3.0 �ȍ~�őΉ����Ă��܂��B
		/// </remarks>
		public const WM SETTABSTOPS=(WM)0x00CB;
		/// <summary>
		/// ���[�U�����������͂������ɁA���͂��������̑���ɕ\������u�p�X���[�h�����v��ݒ肵�܂��B
		/// Edit/RichEdit �R���g���[���Ɏw�肷�鎖���o���܂��B
		/// </summary>
		/// <wparam>
		/// ���[�U�̓��͂��������̑���ɕ\�����镶�����w�肵�܂��B
		/// 0 ���w�肵���ꍇ�ɂ̓p�X���[�h�����͖����ɂȂ�A���[�U�̓��͂��������񂪒��ڕ\������܂��B
		/// </wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>�l�͕Ԃ��܂���B</return>
		/// <remarks>
		/// Edit �R���g���[���� EM_SETPASSWORDCHAR ���󂯎��ƁA�S�Ă̌�����ʒu�ɂ��镶���� wParam �Ŏw�肳�ꂽ�����ŕ`�悵�����܂��B
		/// wParam �� 0 �̏ꍇ�ɂ̓��[�U�ɂ���ē��͂��ꂽ����������̂܂ܕ`�悵�܂��B
		/// Edit �R���g���[���� ES_PASSWORD �X�^�C�����w�肵�č쐬���ꂽ�ꍇ�ɂ́A����̃p�X���[�h�����̓A�X�^���X�N (*) �ɂȂ�܂��B
		/// ES_PASSWORD ���w�肹���ɍ쐬���ꂽ�ꍇ�ɂ́u�p�X���[�h�����v�͑��݂��܂���B
		/// wParam �� 0 �ɂ��� EM_SETPASSWORDCHAR ���b�Z�[�W�𑗂�ꂽ�ꍇ�ɂ� ES_PASSWORD �X�^�C���͍폜����܂��B
		/// [XP]: Edit �R���g���[���� user32.dll ���� ES_PASSWORD ���Ȃč쐬���ꂽ�ꍇ�ɂ͊���̃p�X���[�h�����̓A�X�^���X�N�ɂȂ�܂��B
		/// �R���Acomctl32.dll ver 6 ���� ES_PASSWORD ���Ȃč쐬���ꂽ�ꍇ�ɂ͊���̕����͍��ۂɂȂ�܂��B
		/// comctl32.dll ver 6 �͍ĔЕz�o���܂��� XP �ȍ~�Ɋ܂܂�Ă��鎖�ɗ��ӂ��ĉ������B
		/// comctl32.dll ver 6 ���g�p����ɂ̓}�j�t�F�X�g�Ŏw�肵�ĉ������B�ڍׂ� XP �r�W���A���X�^�C���̎g�p�Œ��ׂĉ������B
		/// [Edit]: �����s Edit �R���g���[���̓p�X���[�h�l���y�у��b�Z�[�W�ɑΉ����܂���B
		/// [RichEdit]: 2.0 �ȍ~�őΉ����Ă��܂��B�����s�y�ђP��s�A�����Ńp�X���[�h�l���y�у��b�Z�[�W�ɑΉ����Ă��܂��B
		/// </remarks>
		public const WM SETPASSWORDCHAR=(WM)0x00CC;
		/// <summary>
		/// �u���ɖ߂��v�E�u��蒼���v���s���ׂ̑���̋L�^�������܂��BEdit/RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>��� 0 ���w�肵�܂��B</wparam><lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>�l�͕Ԃ��܂���B</return>
		/// <remarks>
		/// [EDIT]: �ŋ߂̑���̂݌��ɖ߂������o���܂��B
		/// [RichEdit 1.0]: �ŋ߂̑���̂݌��ɖ߂������o���܂��B
		/// [RichEdit 2.0 �ȍ~]: �����̑���Ɋւ��Č��ɖ߂������񋟂��܂��B���̃��b�Z�[�W�͑S�Ă̋L�^���������܂��B
		/// </remarks>
		public const WM EMPTYUNDOBUFFER=(WM)0x00CD;
		/// <summary>
		/// �����s Edit �R���g���[���ɉ����Ĉ�ԏ�ɕ\������Ă���s�̃C���f�b�N�X���擾���܂��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam><wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <return>
		/// ��ԏ�ɕ\������Ă���s�̃C���f�b�N�X��Ԃ��܂��B
		/// EDIT: �P��s Edit �R���g���[���̏ꍇ�A��ԏ��߂ɕ\������Ă��镶���̃C���f�b�N�X��Ԃ��܂��B
		/// </return>
		public const WM GETFIRSTVISIBLELINE=(WM)0x00CE;
		/// <summary>
		/// Edit �R���g���[���ɓǍ���p�X�^�C����ݒ�E�������܂��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>
		/// ES_READONLY �X�^�C����ݒ肷�邩�������邩���w�肵�܂��B
		/// TRUE ���w�肵���ꍇ ES_READONLY �X�^�C����ݒ肵�܂��BFALSE ���w�肷��� ES_REAONLY �X�^�C�����������܂��B
		/// </wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>���삪���������ꍇ�� 0 �łȂ�����Ԃ��܂��B</return>
		/// <remarks>
		/// Edit �R���g���[���� ES_READONLY �X�^�C�����ݒ肳��Ă���ꍇ���[�U�� Edit �R���g���[�����̕������ύX���鎖�͏o���܂���B
		/// Edit �R���g���[���� ES_READONLY �X�^�C���������ǂ������擾����ɂ� GWL_STYLE ��p���� GetWindowLong �֐����g�p���ĉ������B
		/// </remarks>
		public const WM SETREADONLY=(WM)0x00CF;
		/// <summary>
		/// Edit �R���g���[���̃��[�h���b�v���Ǘ�����֐����w�肵�܂��B
		/// Edit/RichEdit �R���g���[���Ɏw�肷�鎖���o���܂��B
		/// </summary>
		/// <wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <lparam>�A�v���P�[�V������`�̃��[�h���b�v�֐��� EditWordBreakProc �Ŏw�肵�܂��B</lparam>
		/// <return>�l�͕Ԃ��܂���B</return>
		/// <remarks>
		/// ���[�h���b�v�֐��̓X�N���[����ɕ`�悳��镶����̃o�b�t�@��ǂ݁A�\���̍ۂ̉��s�ʒu�����肵�܂��B
		/// ���[�h���b�v�֐��́A�����s Edit �R���g���[���̕�����ɉ����ĉ��s���Ă��ǂ��_���`���܂��B�ʏ�̓X�y�[�X�����Ȃǂɒ�`����܂��B
		/// �P��s Edit �R���g���[���ł��P��̋��E���v�Z����ׂɎg�p����܂��B����́A���[�U�� [Ctrl] �Ƌ��ɖ��L�[���g�p���ĒP�ꋫ�E�̊Ԃ��ړ�����ۂɎg�p����܂��B
		/// �A�v���P�[�V������`�̃��[�h���b�v�֐����g�p���鎖�ɂ��A�X�y�[�X�ȊO�̃n�C�t���Ȃǂ̕����ŉ��s�����鎖���\�ɂȂ�܂��B
		/// </remarks>
		public const WM SETWORDBREAKPROC=(WM)0x00D0;
		public const WM GETWORDBREAKPROC=(WM)0x00D1;
		/// <summary>
		/// ���[�U�����������͂������ɁA���͂��������̑���ɕ\������u�p�X���[�h�����v���擾���܂��B
		/// Edit/RichEdit �R���g���[���Ɏw�肷�鎖���o���܂��B
		/// </summary>
		/// <wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>
		/// ���[�U�����͂��������̑���ɕ\�����镶����Ԃ��܂��B
		/// �Ⴕ�߂�l�� NULL �ł������ꍇ�AEdit �R���g���[���̓��[�U�̓��͂��������𒼐ڕ\�����܂��B
		/// </return>
		/// <remarks>
		/// Edit �R���g���[���� ES_PASSWORD �X�^�C�����w�肵�č쐬���ꂽ�ꍇ�ɂ́A����̃p�X���[�h�����̓A�X�^���X�N (*) �ɂȂ�܂��B
		/// ES_PASSWORD ���w�肹���ɍ쐬���ꂽ�ꍇ�ɂ́u�p�X���[�h�����v�͑��݂��܂���B
		/// �p�X���[�h������ύX����ɂ� EM_SETPASSWORDCHAR �𑗂�܂��B
		/// [XP]: Edit �R���g���[���� user32.dll ���� ES_PASSWORD ���Ȃč쐬���ꂽ�ꍇ�ɂ͊���̃p�X���[�h�����̓A�X�^���X�N�ɂȂ�܂��B
		/// �R���Acomctl32.dll ver 6 ���� ES_PASSWORD ���Ȃč쐬���ꂽ�ꍇ�ɂ͊���̕����͍��ۂɂȂ�܂��B
		/// comctl32.dll ver 6 �͍ĔЕz�o���܂��� XP �ȍ~�Ɋ܂܂�Ă��鎖�ɗ��ӂ��ĉ������B
		/// comctl32.dll ver 6 ���g�p����ɂ̓}�j�t�F�X�g�Ŏw�肵�ĉ������B�ڍׂ� XP �r�W���A���X�^�C���̎g�p�Œ��ׂĉ������B
		/// [Edit]: �����s Edit �R���g���[���̓p�X���[�h�l���y�у��b�Z�[�W�ɑΉ����܂���B
		/// [RichEdit]: 2.0 �ȍ~�őΉ����Ă��܂��B�����s�y�ђP��s�A�����Ńp�X���[�h�l���y�у��b�Z�[�W�ɑΉ����Ă��܂��B
		/// </remarks>
		public const WM GETPASSWORDCHAR=(WM)0x00D2;
		//#if(WINVER >= 0x0400)
		/// <summary>
		/// Edit �R���g���[���̍��E�̗]����ݒ肵�܂��B
		/// �]���̑傫���𔽉f����ׂɁA���̃��b�Z�[�W�ɂ���čĕ`�悪�ׂ���܂��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>
		/// �ݒ肷��]���̎�ނ� EC �Őݒ肵�܂��B
		/// [Edit]: EC_USEFONTINFO �� wParam �Ɏg�p�o���܂���BlParam �Ɏw�肵�ĉ������B
		/// [RichEdit]: EC_USEFONTINFO ���g�p�������ɂ� lParam �͖�������܂��B
		/// </wparam>
		/// <lparam>
		/// ���ʃ��[�h�ɂ͐V�������]�����w�肵�܂��BEC_LEFTMARGIN ���ݒ肳��Ă��Ȃ����ɂ͖�������܂��B
		/// ��ʃ��[�h�ɂ͐V�����E�]�����w�肵�܂��BEC_RIGHTMARGIN ���ݒ肳��Ă��Ȃ����ɂ͖�������܂��B
		/// </lparam>
		/// <return>�l�͕Ԃ��܂���B</return>
		/// <remarks>
		/// [Edit]: EC_USEFONTINFO �� wParam �Ɏg�p�o���܂���BlParam �Ɏw�肵�ĉ������B
		/// [RichEdit]: EC_USEFONTINFO �� wParam �Ɏw�肷�鎖���o���܂��B
		/// [RichEdit 3.0 �ȍ~]: EC_USEFONTINFO �� wParam �y�� lParam �̂ǂ���ɂ��g�p���鎖���o���܂��B
		/// </remarks>
		public const WM SETMARGINS=(WM)0x00D3;
		/// <summary>
		/// Edit �R���g���[���̍��E�̗]�����擾���܂��B
		/// </summary>
		/// <wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>���ʃ��[�h�ɍ��]����Ԃ��܂��B���A��ʃ��[�h�ɉE�]����Ԃ��܂��B</return>
		/// <remarks>[RichEdit]: �Ή����Ă��܂���B</remarks>
		public const WM GETMARGINS=(WM)0x00D4;
		/// <summary>
		/// Edit �R���g���[���ɕ������̌��x��ݒ肵�܂��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>
		/// TCHAR �ɂ�镶�����̐������w�肵�܂��B���[�� null �����͕������Ɋ܂݂܂���B
		/// [RichEdit]: 0 ���w�肷��Ɛ����������� 64000 �����ɂȂ�܂��B
		/// [Edit on NT/2000/XP]: 0 ���w�肷��ƁA�P��s�̏ꍇ������������ 0x7FFFFFFE �ɂȂ�܂��B�����s�̏ꍇ�� -1 �ɂȂ�܂��B
		/// [Edit on 95/98/Me]:
		/// </wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>�l�͕Ԃ��܂���B</return>
		/// <remarks>
		/// ���̃��b�Z�[�W�ɂ���Đ������󂯂�̂̓��[�U�̓��͂��镶���݂̂ł��B
		/// ���b�Z�[�W������ꂽ���Ɋ��� Edit �R���g���[���̒��ɓ����Ă��镶���ɂ͉��̉e��������܂���B
		/// ���AWM_SETTEXT �ɂ���Đݒ肳��镶����ɂ��e�����܂���B
		/// �Ⴕ WM_SETTEXT �ɂ���Đ����𒴂��镶����� Edit �R���g���[���ɐݒ肵���ꍇ�ɂ̓��[�U�͎��R�ɕ������ҏW���鎖���o����l�ɂȂ�܂��B
		/// ����� Edit �R���g���[���̓��͐������� 32767 �����ł��B
		/// [Edit on NT/2000/XP]: �P��s Edit �R���g���[���̏ꍇ�A������ 0x7FFFFFFE �o�C�g�y�� wParam �Ŏw�肵���l�̏��������ɂȂ�܂��B
		/// �����s Edit �R���g���[���̏ꍇ�͐����l�� -1 ���� wParam �Ŏw�肵���l�̏��������ɂȂ�܂��B
		/// [Edit on 95/98/Me]: �P��s Edit �R���g���[���̏ꍇ�́A������ 0x7FFE �o�C�g�y�� wParam �Ŏw�肵���l�̏��������ɂȂ�܂��B
		/// �����s Edit �R���g���[���̏ꍇ�ɂ� 0xFFFF �o�C�g���� wParam �Ŏw�肵���l�̏��������ɂȂ�܂��B
		/// [RichEdit] 1.0 �ȍ~�őΉ����Ă��܂��B
		/// </remarks>
		public const WM SETLIMITTEXT=(WM)0x00C5; // EM_LIMITTEXT // win40 Name change
		/// <summary>
		/// ���݂̓��͕������̐������擾���܂��B
		/// Edit/RichEdit �R���g���[���Ɏg�p�o���܂��B
		/// </summary>
		/// <wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>���͕������̐����l���擾���܂��B</return>
		/// <remarks>
		/// [Edit/RichEdit 2.0 �ȍ~]: �������̐����̓R���g���[�����܂ގ����o���� TCHAR �ɂ��ő�̕������ł��B
		/// [RichEdit 1.0]: �������̐����́A�R���g���[�����܂ގ����o����o�C�g���̍ő�l�ł��B
		/// </remarks>
		public const WM GETLIMITTEXT=(WM)0x00D5;
		/// <summary>
		/// Edit �R���g���[�����̎w�肵�����W�ɂ��镶���̃N���C�A���g���W���擾���܂��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>
		/// [RichEdit 1.0/3.0]: �w�肵�������̃N���C�A���g���W���󂯎��ׂ� POINTL �\���̂ւ̃|�C���^���w�肵�܂��B
		/// ���W�̓R���g���[���̃N���C�A���g�̈�̍�����̓_�����_�ɂ����s�N�Z���P�ʂ̍��W�ł��B
		/// [Edit/RichEdit 2.0]: �����̃C���f�b�N�X���w�肵�܂��B
		/// </wparam>
		/// <lparam>
		/// [RichEdit 1.0/3.0]: �����̃C���f�b�N�X���w�肵�܂��B
		/// [Edit/RichEdit 2.0]: ��� 0 ���w�肵�܂��B
		/// </lparam>
		/// <return>
		/// [RichEdit 1.0/3.0]: �߂�l�͎g�p����܂���B
		/// [Edit/RichEdit 2.0]: �w�肵�������̃N���C�A���g���W��Ԃ��܂��B
		/// ���ʃ��[�h�ɐ������W�A��ʃ��[�h�ɐ������W���w�肵�܂��B
		/// </return>
		/// <remarks>
		/// ���ʂ̒l�́A�w�肵�������� Edit �R���g���[���ɕ\������Ă��Ȃ����ɕ��ɂȂ�\��������܂��B���A���W�l�͐����l�Ɋۂ߂��܂��B
		/// �w�肵�����������s�����̏ꍇ�́A���̍s�Ō����Ă���Ō�̕����̍��W��Ԃ��܂��B�w�肵���C���f�b�N�X�� Edit �R���g���[�����̕������ُ�̏ꍇ�̓R���g���[���͌��ʂ� -1 ��Ԃ��܂��B
		/// [RichEdit 3.0-]: �݊����ׁ̈ARichEdit 2.0 �Ŏg�p���ꂽ���b�Z�[�W�`���ɂ��Ή����܂��B
		/// wParam ���L���� POINTL �\���̂łȂ��Ɣ��f�����ꍇ�ɂ́ARichEdit 2.0 �Ŏg�p���ꂽ�w��̎d���ł���ƌ��Ȃ��ď������s���܂��B
		/// ���̏ꍇ�ɂ͌��ʂ̍��W��߂�l�ɕԂ��܂��B
		/// </remarks>
		public const WM POSFROMCHAR=(WM)0x00D6;
		/// <summary>
		/// �w�肵���N���C�A���g���W�ɍł��߂��ʒu�ɂ��镶���ɂ��Ă̏����擾���܂��B
		/// Edit/RichEdit �R���g���[���ɑ��鎖���o���܂��B
		/// </summary>
		/// <wparam>��� 0 ���w�肵�܂��B</wparam>
		/// <lparam>
		/// [RichEdit]: POINTL �\���̂ւ̃|�C���^���w�肵�܂��B
		/// [EDIT]: ���ʃ��[�h�ɂ͐������W���A���ʃ��[�h�ɂ͐������W���w�肵�܂��B
		/// </lparam>
		/// <return>
		/// �w�肵���_�ɍł��߂��ʒu�ɂ��镶���̃C���f�b�N�X��Ԃ��܂��B�Ō�̕����������ɂ���̈���w�肷��ƁA�Ō�̕����̃C���f�b�N�X��Ԃ��܂��B
		/// [RichEdit]: ��L�̃C���f�b�N�X��Ԃ��܂��B
		/// [EDIT]: ���ʃ��[�h�ɏ�L�̃C���f�b�N�X��Ԃ��܂��B�C���f�b�N�X�͂��̍s�̒��̃C���f�b�N�X�ł͂Ȃ��ăR���g���[���Ȃ��ł̃C���f�b�N�X�ł��B
		/// ���ʃ��[�h�ɂ͍s�̔ԍ���Ԃ��܂��B�P��s�̏ꍇ�ɂ͏�ɍ��ʃ��[�h�� 0 ��Ԃ��܂��B���̍s�ōŌ�̕���������ɍ��W�����݂���ꍇ�ɂ́A�����̃C���f�b�N�X�͉��s�����������܂��B
		/// </return>
		public const WM CHARFROMPOS=(WM)0x00D7;
		//#endif //WINVER >= 0x0400
		//#if(WINVER >= 0x0500)
		/// <summary>
		/// Edit �R���g���[���� IME �̊Ԃ̒ʐM���@�������t���O��ݒ肵�܂��B
		/// </summary>
		/// <wparam>
		/// �ݒ肷���Ԃ̎�ނ��w�肵�܂��B
		/// EMSIS_COMPOSITIONSTRING ���w�肷�鎖���o���܂��B
		/// </wparam>
		/// <lparam>�ݒ肷��l���w�肵�܂��BwParam �Ɏw�肷��l�ɂ���ē��e�͕ς��܂��B</lparam>
		/// <remarks>RichEdit �͑Ή����Ă��܂���B</remarks>
		public const WM SETIMESTATUS=(WM)0x00D8;
		/// <summary>
		/// Edit �R���g���[���� IME �̊Ԃ̒ʐM���@�������t���O���擾���܂��B
		/// </summary>
		/// <wparam>
		/// �擾�����Ԃ̎�ނ��w�肵�܂��B
		/// EMSIS_COMPOSITIONSTRING ���w�肷�鎖���o���܂��B
		/// </wparam>
		/// <lparam>��� 0 ���w�肵�܂��B</lparam>
		/// <return>wParam �Ɏw�肵���l�ɉ������l���Ԃ���܂��B</return>
		public const WM GETIMESTATUS=(WM)0x00D9;
		//#endif //WINVER >= 0x0500
		//commctrl32.h �ɂ��錾����
		//EM_SETCUEBANNER
		//EM_EXGETSEL
		//EM_GETHILITE EM_SETHILITE//vista
	}
	/// <summary>
	/// �]����ݒ肷��ׂ̃t���O�ł��B
	/// </summary>
	[System.Flags]
	public enum EC{
		/// <summary>
		/// ���]����ݒ肵�܂��B
		/// </summary>
		LEFTMARGIN=0x0001,
		/// <summary>
		/// �E�]����ݒ肵�܂��B
		/// </summary>
		RIGHTMARGIN=0x0002,
		/// <summary>
		/// ������w�肵���ꍇ�ɂ͌��݂̃t�H���g�����ɂ��Đݒ肳��܂��B
		/// ���]���� A �̕��ɁA�E�]���� C �̕��ɍ��킹���܂��B
		/// (http://homepage2.nifty.com/Mr_XRAY/Halbow/Chap12.html �ɂ��)
		/// </summary>
		/// <remarks>
		/// [Edit]: EC_USEFONTINFO �� EM_SETMARGINS wParam �Ɏg�p�o���܂���BEM_SETMARGINS lParam �Ɏw�肵�ĉ������B
		/// [RichEdit]: EC_USEFONTINFO ���g�p�������ɂ� EM_SETMARGINS lParam �͖�������܂��B
		/// </remarks>
		USEFONTINFO=0xffff
	}
	/// <summary>
	/// EM_GETIMESTATUS�EEM_SETIMESTATUS �Ŏ擾�E�ݒ肷��l�̎�ނ��w�肵�܂��B
	/// </summary>
	public enum EMSIS{
		/// <summary>
		/// ���̒l���w�肵���ꍇ�A�߂�l���� lparam �Ɏw�肷��l�� EIMES �̑g�����ŕ\����܂��B
		/// </summary>
		COMPOSITIONSTRING=0x0001
	}
	/// <summary>
	/// Edit �R���g���[���ɉ����� IME �̏�Ԃ��w�肵�܂��B
	/// </summary>
	[System.Flags]
	public enum EIMES{
		/// <summary>
		/// IME �̌��ʂ� WM_IME_COMPOSITION (GCS_RESULTSTR) ���󂯎�����ۂɈ�x�Ɏ擾���鎖���w�肵�܂��B
		/// ���̎w�肪�Ȃ��ꍇ�ɂ� WM_CHAR ���g�p���Č��ʂ��ꕶ�����擾���܂��B
		/// ����ł͐ݒ肳��Ă��܂���B
		/// </summary>
		GETCOMPSTRATONCE=0x0001,
		/// <summary>
		/// ���ꂪ�ݒ肳��Ă��鎞�ɂ́A���̃R���g���[���� WM_SETFOCUS �ɂ���� Focus �𓾂����ɁA�ҏW���̕�������N���A���܂��B
		/// �ݒ肳��Ă��Ȃ����ɂ͕ҏW���̕�������������܂���B
		/// ����ł͐ݒ肳��Ă��܂���B
		/// </summary>
		CANCELCOMPSTRINFOCUS=0x0002,
		/// <summary>
		/// ���ꂪ�ݒ肳��Ă��鎞�ɂ́AWM_KILLFOCUS ���������ɕҏW���̕�������m�肳���܂��B
		/// �ݒ肳��Ă��Ȃ����ɂ̓t�H�[�J�X�������Ă��ҏW���̕�����͊m�肳��܂���B
		/// ����ł͐ݒ肳��܂���B
		/// </summary>
		COMPLETECOMPSTRKILLFOCUS=0x0004
	}
}