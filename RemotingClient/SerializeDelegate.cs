using Ref=System.Reflection;
using Gen=System.Collections.Generic;
using Compiler=System.Runtime.CompilerServices;
using Serial=System.Runtime.Serialization;
using BinaryFormatter=System.Runtime.Serialization.Formatters.Binary.BinaryFormatter;

namespace SerialDelegate{
	public class Tester{
		static BinaryFormatter binf=new BinaryFormatter();

		public static void Test(){
			int x=4;
			int y=7;

			// test : attempt a direct serialization
			Invoke3(delegate(){
				x+=y;
				System.Console.WriteLine("x+=y");
				y+=x;
				System.Console.WriteLine("y+=x");
			});

			System.Console.WriteLine("x == {0}",x);
			System.Console.WriteLine("y == {0}",y);
		}

		public delegate void DVoid();
		public static void Invoke1(DVoid func){
			func();
			
			System.IO.Stream str=new System.IO.MemoryStream();
			System.Console.WriteLine("指定した delegate をシリアル化します。");
			try{
				System.Console.WriteLine("-> 先ず関数をシリアル化します。");
				binf.Serialize(str,func.Method);
				System.Console.WriteLine("-> 次にインスタンスをシリアル化します。");
				if(func.Target!=null)
					binf.Serialize(str,func.Target);
				System.Console.WriteLine("シリアル化しました。");
			}catch{
				System.Console.WriteLine("シリアル化に失敗しました。");
			}
		}

		public static void Invoke2(DVoid func){
			func();
			System.IO.Stream str=new System.IO.MemoryStream();
			System.Console.WriteLine("指定した delegate をシリアル化します。");
			try{
				System.Console.WriteLine("-> 先ず関数をシリアル化します。");
				binf.Serialize(str,func.Method);

				System.Console.WriteLine("-> 次にインスタンスをシリアル化します。");
				if(!SpecialSerialize(str,func.Target)){
					binf.Serialize(str,func.Target);
				}
					
				System.Console.WriteLine("シリアル化しました。");
			}catch{
				System.Console.WriteLine("シリアル化に失敗しました。");
			}
		}
		private static bool SpecialSerialize(System.IO.Stream str,object graph){
			if(graph==null)return true;

			System.Type type=graph.GetType();
			if(!type.Name.StartsWith("<>"))return false;

			System.Console.WriteLine("  ※ 特別のシリアル化を行います。");
			binf.Serialize(str,new AnonymousInstanceHolder(graph));
			return true;
		}

		public static void Invoke3(DVoid func){
			if(AnonymousInstanceHolder.IsAnonymousMethod(func)){
				System.IO.Stream str=new System.IO.MemoryStream();
				binf.Serialize(str,new AnonymousInstanceHolder(func));
				str.Position=0;
				
				AnonymousInstanceHolder holder=(AnonymousInstanceHolder)binf.Deserialize(str);
				object o=holder.GetRealObject();
				holder.method.Invoke(o,new object[0]);

				new AnonymousInstanceHolder(o).ApplyFields(func.Target);
			}
		}

		[System.Serializable]
		class AnonymousInstanceHolder{
			private const Ref::BindingFlags BF=Ref::BindingFlags.Instance|Ref::BindingFlags.Public|Ref::BindingFlags.NonPublic;
			private static readonly System.Type[] ctor_params=new System.Type[0];
			private static readonly object[] ctor_args=new object[0];

			public System.Type type;
			public Gen::Dictionary<string,object> fields=new System.Collections.Generic.Dictionary<string,object>();
			public Ref::MethodInfo method;

			public AnonymousInstanceHolder(System.Delegate deleg):this(deleg.Target){
				this.method=deleg.Method;
			}
			internal AnonymousInstanceHolder(object target){
				this.type=target.GetType();
				foreach(Ref::FieldInfo finfo in type.GetFields(BF)){
					this.fields[finfo.Name]=finfo.GetValue(target);
				}
			}

			internal object GetRealObject(){
				Ref::ConstructorInfo ctor=type.GetConstructor(BF,null,ctor_params,null);
				if(ctor==null)
					throw new Serial::SerializationException("AnonymousMethodHolder.GetRealObject: 既定のコンストラクタがないので元に戻せません。");

				object ret=ctor.Invoke(ctor_args);
				this.ApplyFields(ret);
				return ret;
			}

			public void ApplyFields(object target){
				foreach(Ref::FieldInfo finfo in type.GetFields(BF)){
					finfo.SetValue(target,this.fields[finfo.Name]);
				}
			}

			public static bool IsAnonymousMethod(System.Delegate deleg){
				object graph;
				return (graph=deleg.Target)!=null&&graph.GetType().Name.StartsWith("<>");
			}
		}

	}
}