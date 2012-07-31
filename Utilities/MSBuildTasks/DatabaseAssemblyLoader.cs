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
    public class DatabaseAssemblyLoader : Task
    {

        public string Server { get; set; }
        public string Database { get; set; }
        public string ConnectionString { get; set; }

        [Required]
        public string AssemblyPath { get; set; }
        
        public string PathToFolderOnRemoteServer {get;set;}

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
                    if (Server == null || Database == null)
                    {
                        connectionString = Common.Properties.ConnectionString;
                    }
                    else{
                        connectionString = String.Format("Server={0};Database={1};Trusted_Connection=True", Server, Database);
                    }
                }
                System.IO.DirectoryInfo pathToFolderOnRemoteServer = null;
                if (PathToFolderOnRemoteServer != null){
                    pathToFolderOnRemoteServer = new System.IO.DirectoryInfo(PathToFolderOnRemoteServer);
                }
                

                global::DatabaseAssemblyLoader.DatabaseAssemblyLoader loader 
                    = new global::DatabaseAssemblyLoader.DatabaseAssemblyLoader(new System.Data.SqlClient.SqlConnection(connectionString), 
                                                                                new System.IO.FileInfo(AssemblyPath),
                                                                                pathToFolderOnRemoteServer);
                loader.LoadAssemblyIntoDatabase();
                return true;
            }catch(Exception ex){
                this.Log.LogErrorFromException(ex);
                return false;
            }
        }





    }
}
