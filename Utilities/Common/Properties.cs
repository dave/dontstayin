using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Security.Principal;
using Common.General;
using System.Collections.Specialized;
using Common.Automation.Sql;
using System.Net;

namespace Common
{
	public static class Properties
	{

		//const string DatabaseServerIp = "192.168";
		#region DatabaseServerIp property
		public static IPAddress LiveDatabaseServerIp
		{
			get
			{
				if (MachineName.Equals("EXTRA") || MachineName.Equals("STATIC8"))
					return IpAddresses.Db2Ext;
				else
					return IpAddresses.Db2Int;
			}
		}
		#endregion

		public static string SqlServer { get; private set; }
		public static string SqlDatabase { get; private set; }

		public static class PhotoBrowser
		{
			////12*50
			//public static int IconSize { get { return 48; } }
			//public static int IconsPerRow { get { return 12; } }
			//public static int IconsPerPage { get { return 36; } }

			////10*60
			public static int IconSize { get { return 75; } }
			public static int IconsPerRow { get { return 8; } }
			public static int RowsPerPage { get { return 3; } }
			public static int IconsPerPage { get { return IconsPerRow * RowsPerPage; } }

			////8*75
			//public static int IconSize { get { return 73; } }
			//public static int IconsPerRow { get { return 8; } }
			//public static int IconsPerPage { get { return 16; } }

			//6*100
			//public static int IconSize { get { return 98; } }
			//public static int IconsPerRow { get { return 6; } }
			//public static int IconsPerPage { get { return 12; } }

		}

		#region ConnectionString

		public static void SetDatabaseNameAndServer(string connectionString)
		{
			SqlConnection conn = new SqlConnection(connectionString);
			char sep = '€';
			SqlCommand cmd = new SqlCommand(@"SELECT @@ServerName + '" + sep + "' + DB_NAME()", conn);
			try
			{
				conn.Open();
				string result = (string)cmd.ExecuteScalar();
				conn.Close();
				SqlServer = result.Split(sep)[0];
				SqlDatabase = result.Split(sep)[1];
			}
			catch (SqlException) // database doesn't exist
			{
				SqlServer = conn.DataSource;
				SqlDatabase = conn.Database;
			}
		}

		static string connectionString;
		static Object connectionStringLock = new Object();
		public static string ConnectionString
		{
			get
			{
				if (connectionString == null)
				{

					lock (connectionStringLock) //in case of threading issues
					{
						System.Data.SqlClient.SqlConnection conn = null;
						if (connectionString == null)
						{
							string source = null;
							try
							{
								conn = new System.Data.SqlClient.SqlConnection(@"context connection=true");
								connectionString = @"context connection=true";
								source = "Sql Clr";
							}
							catch { }
							if (connectionString == null)
							{
								connectionString = GetConnectionStringFromCommandLineArguments();
								source = "command line";
							}
							if (connectionString == null && IsDevelopmentEnvironment)
							{
								connectionString = GetConnectionStringFromConfigFile();
								source = "config file";
							}
							if (connectionString == null)
							{
								connectionString = GetAppropriateConnectionStringBasedOnMachineName();
								source = "generated";
							}
							Console.WriteLine("ConnectionString: " + ConnectionString + " (" + source + ")");
						}
						Common.Properties.SetDatabaseNameAndServer(connectionString);
					}

				}
				return connectionString;
			}
			set
			{
				connectionString = value;
				Common.Properties.SetDatabaseNameAndServer(connectionString);
			}
		}

		private static string GetConnectionStringFromCommandLineArguments()
		{
			List<string> args = new List<string>(Environment.GetCommandLineArgs());
			if (args.Find(s => s.StartsWith("/cs:")) != null)
			{
				return args.Find(s => s.StartsWith("/cs:")).Substring(4);
			}
			if (connectionString == null && args.Find(s => s.StartsWith("/s:")) != null && args.Find(s => s.StartsWith("/d:")) != null)
			{
				return BuildConnectionString(args.Find(s => s.StartsWith("/s:")).Substring(3), args.Find(s => s.StartsWith("/d:")).Substring(3));
			}
			return null;
		}
		public static string GetConnectionStringFromConfigFile()
		{
			string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
			if (connectionString != null && connectionString.Trim() != "" && connectionString != "default")
			{
				return connectionString;
			}
			else
			{
				return null;
			}
		}
		public static string GetAppropriateConnectionStringBasedOnMachineName()
		{
			if (IsDevelopmentEnvironment)
			{
				return BuildConnectionString(SqlInstanceAddressBasedOnMachineName, DatabaseNameBasedOnMachineName);
			}
			else
			{
				return BuildConnectionString(SqlInstanceAddressBasedOnMachineName, DatabaseNameBasedOnMachineName, "DSIUSR", "toobusy319hello");
			}
		}
		//public static string JQueryPath { get { return @"/misc/jQuery/jquery-1.2.6.min.js"; } }
		////public static string JQueryPath { get { return @"/misc/jQuery/jquery-ui-1-6-b/jquery-1.2.6.js"; } }
		//public static string JQueryUIBasePath { get { return @"/Misc/jQuery/jquery-ui-1-6-b/ui/minified/ui.core.min.js"; } }
		//public static string JQueryUIDialogPath { get { return @"/Misc/jQuery/jquery-ui-1-6-b/ui/minified/ui.dialog.min.js"; } }
		//public static string JQueryUIDraggablePath { get { return @"/Misc/jQuery/jquery-ui-1-6-b/ui/minified/ui.draggable.min.js"; } }
		//public static string JQueryUISortablePath { get { return @"/Misc/jQuery/jquery-ui-1-6-b/ui/minified/ui.sortable.min.js"; } }
		//public static string JQueryUIResizablePath { get { return @"/Misc/jQuery/jquery-ui-1-6-b/ui/minified/ui.resizable.min.js"; } }
		//public static string JQueryUITabsPath { get { return @"/Misc/jQuery/jquery-ui-1-6-b/ui/minified/ui.tabs.min.js"; } }

		//public static string JQueryEffectsCorePath { get { return @"/Misc/jQuery/jquery-ui-1-6-b/ui/minified/effects.core.min.js"; } }
		//public static string JQueryEffectsDropPath { get { return @"/Misc/jQuery/jquery-ui-1-6-b/ui/minified/effects.drop.min.js"; } }
		//public static string JQueryEffectsTransferPath { get { return @"/Misc/jQuery/jquery-ui-1-6-b/ui/minified/effects.transfer.min.js"; } }

		//public static string JQuerySelectboxesPath { get { return @"/Misc/jQuery/selectboxes/jquery.selectboxes.min.js?a=1"; } }
		////public static string JQuerySelectboxesPath { get { return @"/Misc/jQuery/selectboxes/jquery.selectboxes.js"; } }

		public static string SqlInstanceAddressBasedOnMachineName
		{
			get
			{
				switch (MachineName)
				{
					case "ENDOR": return @"ENDOR";
					case "NABOO": return @"NABOO\SQLEXPRESS";
					case "KAMINO": return @"KAMINO\SQLEXPRESS";
					case "SOLO": return @"SOLO\SQLEXPRESS";
                    case "DEV1": return @"DEV1\SQLEXPRESS";
                    case "DEV2": return @"DEV2\SQLEXPRESS";
					default:
						if (IsDevelopmentEnvironment)
						{
							return "DB1";
						}
						else
						{
							return Common.Properties.LiveDatabaseServerIp.ToString();
						}
				}
			}
		}
		public static string DatabaseNameBasedOnMachineName
		{
			get
			{
				if (IsDevelopmentEnvironment)
				{
					return "db_spotted_" + CurrentUserNameWithoutSpecialCharacters + "_" + CurrentBranchName;
				}
				else
				{
					return "db_spotted";
				}
			}
		}
		public static string BuildConnectionString(string server, string database)
		{
			return String.Format("Trusted_Connection=True;Server={0};Database={1}", server, database);
		}
		private static string BuildConnectionString(string server, string database, string username, string password)
		{
			return String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", server, database, username, password);
		}
		private static string CurrentUserNameWithoutSpecialCharacters
		{
			get
			{
				return WindowsIdentity.GetCurrent().Name.Split('\\')[1].Replace(".", "");
			}
		}
		#region CurrentBranchNameProperty
		public static string CurrentBranchName
		{
			get
			{
				try
				{
					return System.Reflection.Assembly.GetExecutingAssembly().CodeBase.ToString().Split('/')[5];
				}
				catch
				{
					return "unknown";
				}
			}
		}
		#endregion
		#endregion

		#region MachineName property
		static string machineName;
		static Object machineNameLock = new Object();
		public static string MachineName
		{
			get
			{
				if (machineName == null)
				{
					lock (machineNameLock) //in case of threading issues
					{
						if (machineName == null)
						{
							machineName = System.Environment.MachineName;
						}
					}
				}
				return machineName;
			}
		}
		#endregion

		#region SystemUsrK
		public static int SystemUsrK { get { return 8; } }
		#endregion

		#region IsDevelopmentEnvironment property
		public static bool IsDevelopmentEnvironment = GetIsDevelopmentEnvironment(MachineName);
		static bool GetIsDevelopmentEnvironment(string machineName)
		{
			return machineName.Equals("CORE") ||
					machineName.Equals("YODA") ||
                    machineName.Equals("DEV1") ||
                    machineName.Equals("DEV2") ||
					machineName.Equals("BOBA") ||
					machineName.Equals("DSI") ||
					machineName.Equals("HOTH") ||
					machineName.Equals("ENDOR") ||
					machineName.Equals("NABOO") ||
					machineName.Equals("SOLO") ||
					machineName.Equals("CORELLIA") ||
					machineName.Equals("ANOTH") ||
					machineName.Equals("DB1") ||
					machineName.Equals("KAMINO");
		}
		#endregion
		#region string[] FrontEndInternalWebServers
		public static string[] FrontEndInternalWebServers = GetFrontEndInternalWebServers();
		static string[] GetFrontEndInternalWebServers()
		{
			if (IsDevelopmentEnvironment)
			{
				return new string[] { };
			}
			else
			{
				return new string[] 
					{ 
					   //"server1i.dontstayin.com",
					   "server2i.dontstayin.com",
					   "server3i.dontstayin.com",
					   "server4i.dontstayin.com",
					   "server5i.dontstayin.com",
					   "server6i.dontstayin.com",
					   "server7i.dontstayin.com",
					   "server8i.dontstayin.com"
				   };
			}
		}
		#endregion
		#region Memcached server group definitions
	//	static readonly IPEndPoint DellaltCacheInstance0 = new IPEndPoint(IpAddresses.Dellalt, 11211);
	//	static readonly IPEndPoint DellaltCacheInstance1 = new IPEndPoint(IpAddresses.Dellalt, 11212);
	//	static readonly IPEndPoint Cache1Instance0 = new IPEndPoint(IpAddresses.Cache1Eth0, 11211);
		//static readonly IPEndPoint Cache1Instance1 = new IPEndPoint(IpAddresses.Cache1Eth0, 11212);
	//	static readonly IPEndPoint Cache1Instance2 = new IPEndPoint(IpAddresses.Cache1Eth1, 11211);
		//static readonly IPEndPoint Cache1Instance3 = new IPEndPoint(IpAddresses.Cache1Eth1, 11212);

		static readonly IPEndPoint[] LocalCacheServer = new IPEndPoint[] { new IPEndPoint(IpAddresses.Local, 11211) };
		#endregion

		#region server Ip Addresses
		static class IpAddresses
		{
			internal static readonly IPAddress Local = IPAddress.Parse("127.0.0.1");
			//internal static readonly IPAddress Db1Int = IPAddress.Parse("192.168.3.71");
			//internal static readonly IPAddress Db1Ext = IPAddress.Parse("84.45.14.71");

			internal static readonly IPAddress Db2Int = IPAddress.Parse("192.168.3.71");
			internal static readonly IPAddress Db2Ext = IPAddress.Parse("84.45.14.71");

		//	internal static readonly IPAddress Cache1Eth0 = IPAddress.Parse("192.168.3.14");
		//	internal static readonly IPAddress Cache1Eth1 = IPAddress.Parse("192.168.3.15");
		//	internal static readonly IPAddress Dellalt = IPAddress.Parse("192.168.113.27");
		}
		#endregion

		public static int TrafficUsrK
		{
			get
			{
				return 1;
			}
		}

		#region IPEndPoint[] CacheServers
		//LIN1:
		//public static IPEndPoint[] MainCacheServerIPEndPoints = new IPEndPoint[] { IsDevelopmentEnvironment ? new IPEndPoint(IPAddress.Parse("192.168.3.24"), 11211) : new IPEndPoint(IPAddress.Parse("192.168.3.14"), 11211) };
		//public static IPEndPoint[] ViewStateCacheServerIPEndPoints = new IPEndPoint[] { IsDevelopmentEnvironment ? new IPEndPoint(IPAddress.Parse("192.168.3.25"), 11211) : new IPEndPoint(IPAddress.Parse("192.168.3.15"), 11211) };

		//LIN2:
		//public static IPEndPoint[] MainCacheServerIPEndPoints = new IPEndPoint[] { IsDevelopmentEnvironment ? new IPEndPoint(IPAddress.Parse("192.168.3.22"), 11211) : new IPEndPoint(IPAddress.Parse("192.168.3.20"), 11211) };
		//public static IPEndPoint[] ViewStateCacheServerIPEndPoints = new IPEndPoint[] { IsDevelopmentEnvironment ? new IPEndPoint(IPAddress.Parse("192.168.3.23"), 11211) : new IPEndPoint(IPAddress.Parse("192.168.3.21"), 11211) };

		public static IPEndPoint[] MainCacheServerIPEndPoints = new IPEndPoint[] { IsDevelopmentEnvironment ? new IPEndPoint(IPAddress.Parse("192.168.3.20"), 11211) : new IPEndPoint(IPAddress.Parse("192.168.3.20"), 11211) };
		public static IPEndPoint[] ViewStateCacheServerIPEndPoints = new IPEndPoint[] { IsDevelopmentEnvironment ? new IPEndPoint(IPAddress.Parse("192.168.3.21"), 11211) : new IPEndPoint(IPAddress.Parse("192.168.3.21"), 11211) };


		//LIN1 live, LIN2 test
		//public static IPEndPoint[] MainCacheServerIPEndPoints = new IPEndPoint[] { IsDevelopmentEnvironment ? new IPEndPoint(IPAddress.Parse("192.168.3.22"), 11211) : new IPEndPoint(IPAddress.Parse("192.168.3.14"), 11211) };
		//public static IPEndPoint[] ViewStateCacheServerIPEndPoints = new IPEndPoint[] { IsDevelopmentEnvironment ? new IPEndPoint(IPAddress.Parse("192.168.3.23"), 11211) : new IPEndPoint(IPAddress.Parse("192.168.3.15"), 11211) };

		#endregion

		#region NameValueCollection QuartzSchedulerProperties
		public static NameValueCollection QuartzSchedulerProperties = GetQuartzSchedulerProperties();
		public static NameValueCollection GetQuartzSchedulerProperties()
		{
			NameValueCollection properties = new NameValueCollection();
			properties["quartz.scheduler.instanceName"] = "SchedulerOn" + MachineName;
			properties["quartz.scheduler.instanceId"] = "AUTO";
			properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
			properties["quartz.threadPool.threadCount"] = "9";
			properties["quartz.threadPool.threadPriority"] = "Normal";
			properties["quartz.jobStore.misfireThreshold"] = "60000";
			properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
			properties["quartz.jobStore.useProperties"] = "false";
			properties["quartz.jobStore.dataSource"] = "default";
			properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
			properties["quartz.dataSource.default.connectionString"] = ConnectionString;
			properties["quartz.dataSource.default.provider"] = "SqlServer-11";
			properties["quartz.scheduler.idleWaitTime"] = "1000";
			//properties["quartz.jobStore.lockHandler.type"] = "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz";
			//properties["quartz.jobStore.isClustered"] = "true";
			return properties;
		}
		#endregion

		#region bool UseJobProcessorService
		public static bool UseJobProcessorService = GetUseJobProcessorService();
		private static bool GetUseJobProcessorService()
		{
			return (!IsDevelopmentEnvironment || MachineName == "ENDOR" || MachineName == "HOTH");
		}
		#endregion

		#region public static string DefaultSmtpServer - Use this for sending mail.
		/// <summary>
		/// Use this for sending mail.
		/// </summary>
		public static string GetDefaultSmtpServer()
		{
			return GetDefaultSmtpServer(new Random());
		}
		public static string GetDefaultSmtpServer(Random r)
		{
			if (IsDevelopmentEnvironment)
			{
				// Kamino currently does not have an external IP address to send external mail
				if (Common.Properties.MachineName.Equals("DEV1") || Common.Properties.MachineName.Equals("DEV2"))
					return GetMailServerIp(r);
				else if (Common.Properties.MachineName.Equals("KAMINO"))
					return "HOTH";
				else if (Common.Properties.MachineName.Equals("SOLO"))
					return "KAMINO";
				else
					return Common.Properties.MachineName;
			}
			else
			{
				if (Common.Properties.MachineName.Equals("DSI"))
					return "DSI";
				else
					return GetMailServerIp(r);
			}
		}
		#endregion
		#region GetMailServerIp
		public static string GetMailServerIp(Random r)
		{
			//return "84.45.14.32";
		//	if (IsDevelopmentEnvironment)
		//	/{
		//		return "84.45.14.34";
		//	}
		//	else
				return "84.45.14.32";
			//return "84.45.14.14" + r.Next(4).ToString();
		}
		#endregion


		static bool? isDevelopmentDatabase = null;
		public static bool IsDevelopmentDatabase
		{
			get
			{
				if (isDevelopmentDatabase == null)
				{
					isDevelopmentDatabase = GetIsDevelopmentDatabase();
				}
				return isDevelopmentDatabase.Value;
			}
		}

		static bool GetIsDevelopmentDatabase()
		{
			Database db = new Database(Common.Properties.ConnectionString);
			string isDevelopmentDatabase = db.ExtendedProperties["IsDevelopmentDatabase"] as string;
			if (isDevelopmentDatabase != null)
			{
				bool dev;
				if (bool.TryParse(isDevelopmentDatabase, out dev)) return dev;
			}
			return false;
		}

		public static string TfsServerName { get { return IsDevelopmentEnvironment ? "http:\\\\chandrila:8080" : "http:\\\\192.168.3.18:8080"; } }
		public static string TfsBranchName { get { return "development"; } }

	}
}
