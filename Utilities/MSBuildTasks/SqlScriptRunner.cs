using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using SqlScriptRunner;
using System.Security.Principal;
namespace MSBuildTasks
{
    public class SqlScriptRunner : Task
    {
      

        public string ProjectPath { get; set; }
        public string ConnectionString {get; set;}
        public string Server {get; set;}
        public string Database {get; set;}
        public bool ApplyDataScripts {get;set;}
        public Common.CommandLine.Log.SeverityLevel? MessageLevel { get; set; }

        public override bool Execute()
        {
            try
            {
                string connectionString;
                if (ConnectionString != null)
                {
                    connectionString = ConnectionString;
                }
                else
                {
                    if (Server == null)
                    {
                        Server = Common.Properties.SqlInstanceAddressBasedOnMachineName;
                    }
                    if (Database == null){
                        Database = Common.Properties.DatabaseNameBasedOnMachineName;
                    }
                    connectionString = String.Format("Server={0};Database={1};Trusted_Connection=True", Server, Database);
                }
                if (MessageLevel == null)
                {
                    MessageLevel = Common.CommandLine.Log.SeverityLevel.Message;
                }
                ScriptProjectRunner runner = new ScriptProjectRunner(ProjectPath, connectionString, false, ApplyDataScripts);
                ScriptProjectRunner.Message += new ScriptProjectRunner.MessageHandler(ScriptProjectRunner_Message);
                runner.SynchroniseDatabaseWithScripts();
                return true;
            }
            catch(Exception ex)
            {
                this.Log.LogErrorFromException(ex);
                return false;
            }
        }

        void ScriptProjectRunner_Message(string message, Common.CommandLine.Log.SeverityLevel level)
        {
            if (level >= MessageLevel){
				Common.CommandLine.Log.Write(message, "SqlScriptRunner.exe", level);
			}
        }




    }
}
