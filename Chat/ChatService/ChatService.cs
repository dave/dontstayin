using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using System.IO;
using ChatLibrary;
using System.Runtime.Remoting.Lifetime;

namespace ChatService
{
    public partial class ChatService : ServiceBase
    {
        #region Constructor
        public ChatService()
        {
			RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;

            InitializeComponent();
        }
        #endregion

        #region Fields
        private ChatServer _server;
        private int _port;
        private Thread _backgroundThread;
        #endregion

        #region Start/Stop methods
        protected override void OnStart(string[] args)
        {
			Status("", true);
			Status("The ChatServer is initializing...", true);

            //log service initializing
            _port = SelectServerPort();
			Status("Done SelectServerPort...", true);
			//_server = ChatServer.Factory(_port);
            
			//_backgroundThread = new Thread(new ThreadStart(this.RunServer));
			//_backgroundThread.Start();
			//LogApplicationEvent("Start", "The ChatServer has successfully started up on port " + _port);
			
			LifetimeServices.LeaseTime = TimeSpan.FromDays(365);
			LifetimeServices.RenewOnCallTime = TimeSpan.FromDays(365);


			//Set up the channel details
			BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
			provider.TypeFilterLevel = TypeFilterLevel.Full;
			Status("Done BinaryServerFormatterSinkProvider...", true);

			string address = ChatServer.ChatServerIp;// ChatServer.GetMyIP();
			Hashtable props = new Hashtable();
			props.Add("port", _port);
			props.Add("bindTo", address);
			props.Add("name", "Address:" + address + ":" + _port);//this needs to be unique
			Status("Init on address:" + address + ":" + _port, true);
			
			//Create the channel
			TcpServerChannel chan = new TcpServerChannel(props, provider);
			Status("Done TcpServerChannel...", true);

			//register it
			ChannelServices.RegisterChannel(chan, false);
			Status("Done RegisterChannel", true);

			//register the object as a service
			RemotingConfiguration.RegisterWellKnownServiceType(typeof(ChatServer), "Reg", WellKnownObjectMode.Singleton);
			Status("Done RegisterWellKnownType", true);

			_server = new ChatServer(new ChatServer.StatusDelegate(Status));
			Status("Done ChatServer...", true);
			
			RemotingServices.Marshal(_server, "Reg");
			Status("Done RemotingServices.Marshal...", true);
			
			//_server.Setup(_port);
			Status("Done Setup...", true);

			_backgroundThread = new Thread(new ThreadStart(this.RunServer));
			_backgroundThread.Start();
			Status("Done Start...", true);

			//LogApplicationEvent("Start", "The ChatServer has successfully started up on port " + _port);

            
        }

        protected override void OnStop()
        {
			Status("Stopping...", true);
            LogApplicationEvent("Closing", "The ChatServer is shutting down");
            
            //Save state to DB
            //Set ChatServer as inactive
            //Remove entry from database
            //_server.UnRegisterSelfOnDatabase();
            //stop running
			try
			{
				_backgroundThread.Abort();
			}
			catch(Exception ex)
			{
				Status("Problem aborting the garbage collector background thread\n" + ex.ToString(), true);
				LogApplicationEvent("Stop", "Problem aborting the garbage collector background thread\n"+ex.ToString());
			}
            _server.Active = false;
			_server = null;
            LogApplicationEvent("Stop", "The ChatServer has successfully stopped");
			Status("Stopped...", true);
			Status("", true);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int SelectServerPort()
        {
			return ChatLibrary.ChatServer.ChatServerPort;

			//SqlConnection conn = null;
			//DataSet usedPorts = new DataSet();

			//try
			//{
			//    //Get a connection
			//    //conn = new SqlConnection(Properties.Settings.Default.DBConn);
			//    conn = new SqlConnection("Data Source=192.168.113.150; user id=DSIUSRdev; password=dont47transmit; Initial Catalog=db_spotted;");

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
			//        try
			//        {
			//            int maxPort = int.Parse(usedPorts.Tables[0].Rows[0]["MaxPort"].ToString());
			//            maxPort++;
			//            return maxPort++;
			//        }
			//        catch
			//        {

			//        }
			//    }
			//    return 9000;
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

        private void RunServer()
        {
        //    while (_server.IsActive())
        //  {
        //        Thread.Sleep(5000);
         //       _server.Update();
          //  }
        }

        private void LogApplicationEvent(string log, string message)
        {
            string source = "ChatService";

            EventLog.WriteEntry(source, message);
            
            
            //if (!EventLog.SourceExists(source))
            //{
            //    EventLog.CreateEventSource(source, log);
            //}

            //EventLog aLog = new EventLog();

            //aLog.Source = source;
            //if (String.Compare(aLog.Log, log, true, CultureInfo.InvariantCulture) != 0)
            //{
            //    //Console.WriteLine("Some other application is using the source!");
            //    return;
            //}

            //aLog.WriteEntry(message, EventLogEntryType.Information);
            ////Console.WriteLine("Entry written successfuly!");
        
        }
        #endregion

		#region Status
		public void Status(string message, bool saveToFile)
		{
			if (saveToFile)
			{
				try
				{
					using (StreamWriter sw = File.AppendText("c:\\chat-" + DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString("00") + "-" + DateTime.Today.Day.ToString("00") + ".txt"))
					{
						if (message.Length == 0)
							sw.WriteLine("");
						else
							sw.WriteLine(DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00") + " " + message);
						sw.Flush();
						sw.Close();
						sw.Dispose();
					}
				}
				catch { }

			}
		}
		#endregion

	}
}
