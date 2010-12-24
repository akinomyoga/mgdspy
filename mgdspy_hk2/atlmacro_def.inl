
#include "stdatl.h"
	#pragma push_macro("_ASSERTE")
	#pragma push_macro("ATLASSERT")
	#pragma push_macro("ATLASSUME")
#ifdef _DEBUG
	/*
	inline void __AssertExp(bool value,cli_string^ msg1,cli_string^ msg2){
		System::Diagnostics::Debug::Assert(value,msg1,msg2);
	}
	#define _ASSERTE(expr)  __AssertExp((expr)?0:1,"_ASSERTE: ğŒ‚ª–‚½‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ" , _CRT_WIDE(#expr))
	*/
#define _ASSERTE(expr)		System::Diagnostics::Debug::Assert((expr)?1:0,"_ASSERTE: ğŒ‚ª–‚½‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ" , _CRT_WIDE(#expr))
#else
	#define _ASSERTE(expr)
#endif
	#define ATLASSERT(expr) _ASSERTE(expr)
	#define ATLASSUME(expr) {ATLASSERT(expr); __analysis_assume(!!(expr));}
