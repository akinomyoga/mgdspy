using Thr=System.Threading;
using Gen=System.Collections.Generic;

namespace mwg.Remote.Utils{
	class ThreadPool{
		Gen::List<ThreadWorker> threads=new Gen::List<ThreadWorker>();

		public ThreadPool(){}

		public void Post(System.Action action){
			lock(threads){
				foreach(ThreadWorker th in threads){
					if(th.Charge(action))return;
				}

				ThreadWorker w=new ThreadWorker();
				w.Charge(action);
				threads.Add(w);
			}
		}

		class ThreadWorker:System.IDisposable{
			Thr::Thread thread;
			System.Action action=null;
			bool dispose=false;
			public ThreadWorker(){
				this.thread=new System.Threading.Thread(this.work);
				this.thread.IsBackground=true;
				this.thread.Start();
			}

			void work(){
				while(!dispose){
					if(this.action!=null){
						try{
							this.action();
						}catch{}
						this.action=null;
					}else{
						Thr::Thread.Sleep(50);
					}
				}
			}

			public void Dispose(){
				this.dispose=true;
			}

			bool IsBusy{
				get{return this.action!=null;}
			}
			public bool Charge(System.Action action){
				if(this.action!=null)return false;
				this.action=action;
				return true;
			}
		}
	}
}