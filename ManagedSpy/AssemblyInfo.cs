using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("ManagedSpy")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("K. Murase")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]		


[assembly: AssemblyVersion("1.0.*")]

[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]

namespace mwg.ManagedSpy{
	/// <summary>
	/// �v���O�����̎��s���s���܂��B
	/// </summary>
	public class App{
		/// <summary>
		/// �G���g���|�C���g�ł��B
		/// </summary>
		[System.STAThread]
		public static void Main(){
			/*
			ManagedSpy.Window.test();
			/*/
			System.Windows.Forms.Application.Run(new Form1());//*/
		}
	}
}