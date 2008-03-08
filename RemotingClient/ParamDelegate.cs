using Console=System.Console;

namespace DelegateTest{
	public delegate void DVV();
	public delegate object DOV();
	public delegate R DRV<R>();
	public delegate object DTypedArg<T1>(T1 arg1);
	public delegate object DTypedArg<T1,T2>(T1 arg1,T2 arg2);
	public delegate R DRAA<R,A1,A2>(A1 arg1,A2 arg2);
	public static class Tester{
		public static void Test(){
			Invoke(VV);
			InvokeR(VoidArg1);

			Invoke<int,int,int>(RAA,10,10);

			Invoke(delegate(int x,int y){
				return x+y;
			},3,4);

			int a=9;
			int b=10;
			InvokeR(delegate(){return a+b;});
		}

		public static void Invoke(DVV x){x();}
		public static void InvokeR(DOV x){x();}
		//public static void Invoke<T>(DRV<T> x) {x();}

		public static void Invoke<T1>(DTypedArg<T1> d,T1 a1){
			d(a1);
		}
		public static void Invoke<T1,T2>(DTypedArg<T1,T2> d,T1 a1,T2 a2){
			d(a1,a2);
		}
		public static void Invoke<R,A1,A2>(DRAA<R,A1,A2> d,A1 a1,A2 a2){d(a1,a2);}

		static void VV(){
			Console.WriteLine("VoidArg");
		}
		static object VoidArg1(){
			Console.WriteLine("VoidArg");
			return 1;
		}
		static int RAA(int x,int y){
			Console.WriteLine("TypeArg2");
			return x+y;
		}
	}
}