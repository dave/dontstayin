using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

namespace ToolRunner
{
	class Program
	{
		private const string DatabaseVersionExtendedPropertyName = "DatabaseVersion|ModifiedDate|UpdateTimestamp|MostRecentScriptName";
		static int Main(string[] args)
		{
			try
			{
				if (args.Contains("/runondbupdated"))
				{
					var server = new Server(Common.Properties.SqlServer);
					var database = server.Databases[Common.Properties.SqlDatabase];
					var dbVersion = database.ExtendedProperties[DatabaseVersionExtendedPropertyName];
					if (dbVersion == null) throw new Exception("DatabaseVersion was null! Probably needs ScriptRunner to execute against it");

					var extendedPropertyName = "DatabaseVersion(" + args[0] + "|" + Hash(args[1]) + ")";

					var toolVersion = database.ExtendedProperties[extendedPropertyName];
					var toolVersionParts = (toolVersion == null ? "0|" + DateTime.MinValue + "|" + DateTime.MinValue + "|" + "noscript" : (string) toolVersion.Value).Split('|');
					var dbVersionParts = ((string) dbVersion.Value).Split('|');
					if (int.Parse(toolVersionParts[0]) < int.Parse(dbVersionParts[0]) || DateTime.Parse(toolVersionParts[1]) < DateTime.Parse(dbVersionParts[1]))
					{
						Console.WriteLine("Db version changed. Running tool.");
						int returnValue = RunProcess(args);
						if (returnValue != 0)
						{
							return returnValue;
						}
						if (database.ExtendedProperties[extendedPropertyName] == null)
						{
							var ep = new ExtendedProperty() {Parent = database, Name = extendedPropertyName, Value = database.ExtendedProperties[DatabaseVersionExtendedPropertyName].Value};
							ep.Create();
						}
						else
						{
							database.ExtendedProperties[extendedPropertyName].Value = database.ExtendedProperties[DatabaseVersionExtendedPropertyName].Value;
							database.ExtendedProperties[extendedPropertyName].Alter();
						}
					}
					return 0;
				}
				else
				{
					return RunProcess(args);
				}
			}catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return 1;
			}
		}

		private static string Hash(string p)
		{
			System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] data = System.Text.Encoding.ASCII.GetBytes(p);
			data = x.ComputeHash(data);
			string ret = "";
			for (int i = 0; i < data.Length; i++)
				ret += data[i].ToString("x2").ToLower();
			return ret.Length > 12 ? ret : ret.Substring(0, 12); 

		}

		private static int RunProcess(string[] args)
		{
			var startInfo = new ProcessStartInfo(args[0], InsertCustomValues(args[1]))
			{
				RedirectStandardError = true,
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				UseShellExecute = false
			};
			Process p = new Process()
			{
				StartInfo = startInfo,
			};
			p.Start();
			p.WaitForExit();
			return p.ExitCode;
		}

		private static string InsertCustomValues(string s)
		{
			return s
				.Replace("*SERVER*", Common.Properties.SqlServer)
				.Replace("*CONNECTIONSTRING*", Common.Properties.ConnectionString)
				.Replace("*DATABASE*", Common.Properties.SqlDatabase);
		}
	}
}
