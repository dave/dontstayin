using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.CommandLine;
using System.Data.SqlClient;
using System.IO;
using System.Security.Principal;
using Common.Automation.Sql;
namespace DatabaseAssemblyLoader
{
    public class DatabaseAssemblyLoader
    {

        SqlConnection conn;
		ClrAssemblyFile clrAssemblyFile;
        DirectoryInfo directoryToCopyAssemlyTo;

        
        public DatabaseAssemblyLoader(SqlConnection conn, FileInfo fi, DirectoryInfo directoryToCopyAssemlyTo)
        {
            this.conn = conn;
            this.clrAssemblyFile = new ClrAssemblyFile(fi);
            this.directoryToCopyAssemlyTo = directoryToCopyAssemlyTo;
        }

        public void LoadAssemblyIntoDatabase()
        {
			SqlInstanceInfo sqlInstanceInfo = new SqlInstanceInfo(conn);
			Database database = new Database(conn);
			database.CreateDatabaseIfDoesNotExist();
			if (System.Environment.MachineName != sqlInstanceInfo.ServerName)
			{
				ClrAssemblyFile remoteClrAssemblyFile = null;
				if (directoryToCopyAssemlyTo == null)
				{
					directoryToCopyAssemlyTo = GetTempDirectoryInfo(sqlInstanceInfo.ServerName, CurrentUserNameWithoutDomain);
				}
				if (!directoryToCopyAssemlyTo.Exists)
				{
					directoryToCopyAssemlyTo.Create();
				}
				CopyFilesOverToRemoteServer(clrAssemblyFile.FileInfo.Directory, directoryToCopyAssemlyTo);
				string path = Path.Combine(directoryToCopyAssemlyTo.FullName, clrAssemblyFile.FileInfo.Name);
				remoteClrAssemblyFile = new ClrAssemblyFile(new FileInfo(path));
				UpdateAssembly(database, remoteClrAssemblyFile);
			}
			else
			{
				UpdateAssembly(database, clrAssemblyFile);
			}
			
        }

    	private void UpdateAssembly(Database database, ClrAssemblyFile assembly)
    	{
    		try
    		{
				database.UpdateAssembly(assembly);
    		}
    		catch (Exception ex)
    		{
    			if (ex.Message.Contains("was not found with the same signature in the updated assembly"))
    			{
    				DropTriggers(database);
					database.RemoveAssembly(assembly);
					database.UpdateAssembly(assembly);
    			}
    			else
    			{
    				throw;
    			}
    		}
    	}

    	private void DropTriggers(Database database)
		{
    		database.Execute(
    			@"DECLARE @Triggers TABLE(TriggerName VARCHAR(MAX))
				INSERT INTO
				@Triggers
				SELECT Name FROM Sys.triggers


				DECLARE @Commands TABLE (Id INT, Sql VARCHAR(MAX))
				INSERT INTO @Commands
				SELECT ROW_NUMBER() OVER(ORDER BY TriggerName), Replace('IF EXISTS (SELECT * FROM Sys.triggers WHERE Name = ''{TriggerName}'' ) DROP TRIGGER {TriggerName}', '{TriggerName}', TriggerName) FROM @Triggers



				DECLARE @Counter INT SELECT @Counter = COUNT(*) FROM @Commands
				WHILE (@Counter > 0) BEGIN
					DECLARE @Command VARCHAR(MAX)
					SELECT @Command = Sql FROM @Commands WHERE Id = @Counter
					PRINT (@Command)
					EXEC (@Command)
					SET @Counter = @Counter - 1
				END
			");
		}

		 
        private DirectoryInfo GetTempDirectoryInfo(string server, string username)
        {
			return new DirectoryInfo(String.Format(@"\\{0}\c$\TriggerTemp", server));
			//if (server.ToString().ToLower() == "db2")
			//	return new DirectoryInfo(String.Format(@"\\{0}\c$\Users\{1}\Local Settings\Temp", server, username));
			//else
			//	return new DirectoryInfo(String.Format(@"\\{0}\c$\Documents and Settings\{1}\Local Settings\Temp", server, username));
        }


		private string CurrentUserNameWithoutDomain
		{
			get
			{
				string nameWithDomain = WindowsIdentity.GetCurrent().Name;
				return nameWithDomain.Substring(nameWithDomain.IndexOf("\\") + 1);
			}
		}
		
        

        private void CopyFilesOverToRemoteServer(DirectoryInfo directory, DirectoryInfo directoryToCopyAssemlyTo)
        {
			foreach (FileInfo file in directory.GetFiles("*.dll"))
            {
                string destination = Path.Combine(directoryToCopyAssemlyTo.FullName, file.Name);
                file.CopyTo(destination, true);
            }
        }


        static int Main(string[] args)
        {
            try
            {
                Console.WriteLine("Updating assembly in database");

                FilePathArgument assemblyFileArg = new FilePathArgument(new string[] { "a" }, null, "parameter /a:pathToAssemblyToBeAddedToDatabase was not supplied correctly");
                DirectoryPathArgument directoryToCopyAssemlyToArg = new DirectoryPathArgument(new string[] { "p" }, null);

                DatabaseAssemblyLoader program = new DatabaseAssemblyLoader(new SqlConnection(Common.Properties.ConnectionString), assemblyFileArg.File, directoryToCopyAssemlyToArg.Directory);
                program.LoadAssemblyIntoDatabase();
                Console.WriteLine("Success");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseAssemblyLoader.exe : error 1 : " + ex.ToString());
                return 1;
            }
        }
    }
}
