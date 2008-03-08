using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Security.Permissions;

namespace RemotingTest{
	class Program {
		static void Main(string[] args) {
			//SerialTest.SerializationSurrogate.test();
			SerialDelegate.Tester.Test();

			Dictionary<string,string> a=new Dictionary<string,string>();
			a["dhasjk"]="jdlka";
			a["djsak"]="uiouio";
			System.Console.WriteLine(a);

			System.Console.ReadLine();
		}
	}

	public static class ClientTest{
		const string OBJECT_URL="http://localhost:8709/RemoteObject.rem";
		public static void Test(){
			// Create the channel.
			HttpClientChannel clientChannel=new HttpClientChannel();

			// Register the channel.
			ChannelServices.RegisterChannel(clientChannel,false);

			// Register as client for remote object.
			WellKnownClientTypeEntry remoteType=new WellKnownClientTypeEntry(typeof(RemoteObject),OBJECT_URL);
			RemotingConfiguration.RegisterWellKnownClientType(remoteType);

			// Create a message sink.
			string objectUri;
			System.Runtime.Remoting.Messaging.IMessageSink messageSink=clientChannel.CreateMessageSink(OBJECT_URL,null,out objectUri);
			Console.WriteLine("The URI of the message sink is {0}.",objectUri);

			if(messageSink!=null){
				Console.WriteLine("The type of the message sink is {0}.",messageSink.GetType().ToString());
			}

			// Display the channel's properties using Keys and Item.
			foreach(string key in clientChannel.Keys){
				Console.WriteLine("clientChannel[{0}] = <{1}>",key,clientChannel[key]);
			}

			// Parse the channel's URI.
			string channelUri=clientChannel.Parse(OBJECT_URL,out objectUri);
			Console.WriteLine("The object URL is {0}.",OBJECT_URL);
			Console.WriteLine("The object URI is {0}.",objectUri);
			Console.WriteLine("The channel URI is {0}.",channelUri);

			// Create an instance of the remote object.
			RemoteObject service=new RemoteObject();

			// Invoke a method on the remote object.
			Console.WriteLine("The client is invoking the remote object.");
			Console.WriteLine("The remote object has been called {0} times.",service.GetCount());

			Console.WriteLine(service.SecretType);
			Console.ReadLine();
		}
	}
}
