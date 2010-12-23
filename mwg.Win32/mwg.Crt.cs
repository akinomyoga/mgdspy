using Gen=System.Collections.Generic;
using Interop=System.Runtime.InteropServices;

namespace mwg.Crt{
	public struct tm{
		int tm_sec;     /* seconds after the minute - [0,59] */
		int tm_min;     /* minutes after the hour - [0,59] */
		int tm_hour;    /* hours since midnight - [0,23] */
		int tm_mday;    /* day of the month - [1,31] */
		int tm_mon;     /* months since January - [0,11] */
		int tm_year;    /* years since 1900 */
		int tm_wday;    /* days since Sunday - [0,6] */
		int tm_yday;    /* days since January 1 - [0,365] */
		int tm_isdst;   /* daylight savings time flag */

		/// <summary>
		/// このインスタンスの表現している時刻を現知事国として DateTime に変換します。
		/// </summary>
		/// <returns></returns>
		public System.DateTime ToLocalTime(){
			return new System.DateTime(1900+tm_year,tm_mon,tm_mday,tm_hour,tm_min,tm_sec,System.DateTimeKind.Local);
		}
		/// <summary>
		/// このインスタンスの表現している時刻を UTC 時刻として DateTime に変換します。
		/// </summary>
		/// <returns></returns>
		public System.DateTime ToUtcTime(){
			return new System.DateTime(1900+tm_year,tm_mon,tm_mday,tm_hour,tm_min,tm_sec,System.DateTimeKind.Utc);
		}
	};
	public static unsafe class Time{
		[Interop::DllImport("msvcrt",EntryPoint="localtime")]
		public static extern tm* localtime(int* time);
		[Interop::DllImport("msvcrt",EntryPoint="_localtime64")]
		public static extern tm* localtime(long* time);
	}
}
