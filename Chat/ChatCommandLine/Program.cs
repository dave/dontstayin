using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using ChatLibrary;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Collections;

namespace ChatCommandLine
{
    class ServerInstance
    {
        static void Main(string[] args)
        {
            int newPort = SelectServerPort();
            //if (args.Length == 0)
            //{
            //    //throw new Exception("Supply a port number");
            //    //get the last port number and add 1
            //    string ports = System.IO.File.ReadAllText(@"c:\servers.dat");
            //    string[] port = ports.Split(Environment.NewLine.ToCharArray());

            //    int biggestPort = 0;
            //    int thisPort;
            //    for (int i = 0; i < port.Length; i++)
            //    {
            //        try
            //        {
            //            thisPort = int.Parse(port[i]);
            //            biggestPort = (thisPort > biggestPort) ? thisPort : biggestPort;
            //        }
            //        catch
            //        {
            //            //ignore bad parses
            //        }
                    
            //    }

            //    if (biggestPort == 0)
            //        biggestPort = 9000;

            //    biggestPort++;
            //    newPort = (biggestPort.ToString());

            //}
            //else
            //{
            //    newPort = args[0];
                
            //}


            ServerInstance s = new ServerInstance(newPort);

        }

        private static int SelectServerPort()
        {
            //SqlConnection conn = null;
            //DataSet usedPorts = new DataSet();

			return ChatLibrary.ChatServer.ChatServerPort;
			//try
			//{
			//    //Get a connection
			//    //conn = new SqlConnection(Properties.Settings.Default.DBConn);
			//    conn = new SqlConnection("Data Source=192.168.113.1; user id=DSIUSRdev; password=dont47transmit; Initial Catalog=db_spotted;");

			//    //Get all the registered servers
			//    //string sqlString = "SELECT Port FROM ActiveChatServers ORDER BY Port ASC";
			//    string sqlString = "SELECT MAX(Port) AS MaxPort FROM ActiveChatServers";
			//    SqlCommand command = new SqlCommand(sqlString, conn);
			//    SqlDataAdapter adapter = new SqlDataAdapter();
			//    adapter.SelectCommand = command;

			//    if (!conn.State.Equals(ConnectionState.Open))
			//        conn.Open();

			//    adapter.Fill(usedPorts);

			//    if (usedPorts.Tables.Count != 0 && usedPorts.Tables[0].Rows.Count != 0)
			//    {
			//        if (usedPorts.Tables[0].Rows[0]["MaxPort"].ToString() != "NULL")
			//        {
			//            try
			//            {
			//                int maxPort = (int)usedPorts.Tables[0].Rows[0]["MaxPort"];
			//                maxPort++;
			//                return maxPort;
			//            }
			//            catch (Exception ex)
			//            {
			//                return 9000;
			//            }
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    //ignore
			//}
			//finally
			//{
			//    if (conn != null)
			//    {
			//        conn.Close();
			//        conn = null;
			//    }
			//}
			//return 9000;

        }

        private static ChatServer _server;

        public ServerInstance(int port)
        {

            //_server = ChatServer.Factory(int.Parse(port));

			

			//Set up the channel details
			System.Runtime.Remoting.Channels.BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
			provider.TypeFilterLevel = TypeFilterLevel.Full;

			string address = ChatServer.ChatServerIp;// ChatServer.GetMyIP();
			Hashtable props = new Hashtable();
			props.Add("port", port);
			props.Add("bindTo", address);
			props.Add("name", "Address:" + address + ":" + port);//this needs to be unique

			Console.WriteLine("IP: " + address);
			Console.WriteLine("Port: " + port.ToString());

			//Create the channel
			//TcpChannel chan = new TcpChannel(props, null, provider);
			//TcpChannel chan = new TcpChannel(props, null, null);
			//TcpServerChannel chan = new TcpServerChannel(props, null, null);
			TcpServerChannel chan = new TcpServerChannel(props, provider);


			//register it
			ChannelServices.RegisterChannel(chan, false);
			Status("Registered channel", true);

			//register the object as a service
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(ChatServer), "Reg", WellKnownObjectMode.Singleton);
			Status("RegisterWellKnownType - Done", true);

			//Don't need this now
			//ChatServer newChatServer = (ChatServer)Activator.GetObject(typeof(ChatServer), "tcp://" + address + ":" + port + "/Reg");
			
			//move this to setup()
			//newChatServer.Setup(port, address);
			//newChatServer.RegisterMyself();
			//newChatServer.RegisterSelfOnDatabase();
			//newChatServer.RegisterOnOtherChatServers();


			_server = new ChatServer(new ChatServer.StatusDelegate(Status));
			RemotingServices.Marshal(_server, "Reg");
			//_server.Setup(port);

            while (true)
            {
                //just stay alive
				//do garbage collection in new thread
                Thread.Sleep(5000);
            }
        }
		public static void Status(string message, bool writeToFile)
		{
			if(writeToFile)
				Console.WriteLine(message);
		}
    }
}
