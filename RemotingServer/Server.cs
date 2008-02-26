using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Security.Permissions;

namespace RemotingTest {
	class Program {
		static void Main(string[] args){
			// Create the server channel.
			HttpServerChannel serverChannel=new HttpServerChannel(8709);

			// Register the server channel.
			ChannelServices.RegisterChannel(serverChannel,false);

			// Expose an object for remote calls.
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemoteObject),"RemoteObject.rem",WellKnownObjectMode.Singleton);

			// Wait for the user prompt.
			Console.WriteLine("Press ENTER to exit the server.");
			Console.ReadLine();
			Console.WriteLine("The server is exiting.");
		}
	}

	// Remote object.
	public class RemoteObject:MarshalByRefObject{
		private int callCount = 0;

		public int GetCount(){
			Console.WriteLine("GetCount was called.");
			callCount++;
			return callCount;
		}

		public System.Type SecretType{
			get{
				Console.WriteLine("îÈñßÇÃå^ÇêuÇ©ÇÍÇ‹ÇµÇΩ");
				return typeof(System.Windows.Forms.TreeView);
			}
		}
	}

}
