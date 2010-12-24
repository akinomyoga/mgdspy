using Forms=System.Windows.Forms;

namespace mwg.Remote{
	public delegate void DataReceiver(int code,byte[] data);
	public interface IDataGate{
		void Send(int code,byte[] data);
		event DataReceiver DataReceived;
	}

	public interface IRequest{
		object Exec();
	}
	public delegate object DRequest();
	public delegate T DRequest<T>();
	public delegate void DRequestV();

	public class ChannelStation{
		enum SendCode{
			SendObject,
			Execute,
			ExecuteAction,
			ExecuteResult,
		}
		[System.Serializable]
		class ExecuteException{
			public ExecuteException(System.Exception e){
				this.exception=e;
			}

			readonly System.Exception exception;
			public System.Exception Exception{
				get{return this.exception;}
			}
		}
		[System.Serializable]
		class ExecuteResult{
			readonly object value;

			public ExecuteResult(object value){
				this.value=value;
			}

			public object Result{
				get{return this.value;}
			}
		}
		[System.Serializable]
		class ExceptionNotSerializable:System.Exception{
			string detail;
			public ExceptionNotSerializable(System.Exception e):base(e.Message){
				this.detail=e.ToString();
			}
			public string Detail{
				get{return this.detail;}
			}
		}

		IDataGate gate;

		public ChannelStation(IDataGate gate){
			this.gate=gate;
			this.gate.DataReceived+=new DataReceiver(gate_DataReceived);
		}

		object execResult=null;
		void gate_DataReceived(int code,byte[] data){
			switch((SendCode)code){
				case SendCode.SendObject:
					object graph=UnsafeSerializer.Deserialize(data);
					// TODO
					break;
				case SendCode.Execute:
					IRequest exec=UnsafeSerializer.Deserialize(data) as IRequest;
					if(exec!=null){
						this.SendExecuteException(new System.Exception("IExecutable でないオブジェクトが Execute コマンドで送信されました。"));
					}

					try{
						this.SendExecuteResult(exec.Exec());
					}catch(System.Exception e){
						this.SendExecuteException(e);
					}
					break;
				case SendCode.ExecuteResult:
					this.execResult=UnsafeSerializer.Deserialize(data);
					break;
			}
		}

		public void SendObject(object graph){
			gate.Send((int)SendCode.SendObject,UnsafeSerializer.Serialize(graph));
		}
		public void RemoteInvoke(DRequestV action){
			gate.Send((int)SendCode.Execute,UnsafeSerializer.Serialize(new VoidRequest(action)));
		}
		[System.Serializable]
		class VoidRequest:IRequest{
			DRequestV deleg;
			public VoidRequest(DRequestV deleg){
				this.deleg=deleg;
			}
			public object Exec(){
				this.deleg();
				return null;
			}
		}

		internal void SendExecuteResult(object result){
			ExecuteResult graph=new ExecuteResult(result);
			gate.Send((int)SendCode.ExecuteResult,UnsafeSerializer.Serialize(graph));
		}
		internal void SendExecuteException(System.Exception e){
			try{
				ExecuteException graph=new ExecuteException(e);
				gate.Send((int)SendCode.ExecuteResult,UnsafeSerializer.Serialize(graph));
			}catch{
				// 例外自体が Serializable でない場合→例外のメッセージだけ送る
				e=new ExceptionNotSerializable(e);
				ExecuteException graph=new ExecuteException(e);
				gate.Send((int)SendCode.ExecuteResult,UnsafeSerializer.Serialize(graph));
			}
		}

	}
}
