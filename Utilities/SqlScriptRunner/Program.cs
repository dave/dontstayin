using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Automation.Sql;
using Common.CommandLine;
namespace SqlScriptRunner
{
	class Program
	{
		DirectoryPathArgument pathToSqlProject = new DirectoryPathArgument(new string[] { "p" }, ".");
		BooleanArgument encryptScriptsInDatabase = new BooleanArgument(new string[] { "e" }, false);
		BooleanArgument applyDataScripts = new BooleanArgument(new string[] { "data" }, false);
		IntegerArgument logLevelArg = new IntegerArgument(new string[] { "l" }, 4);
		Common.CommandLine.Log.SeverityLevel logLevel;
		public static void Main(string[] args)
		{
			Program p = new Program(args);
		}

		Program(string[] args)
		{
			try
			{
				Common.CommandLine.Log.Write("Daves mods 1.1: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name, "", Log.SeverityLevel.Message);
				Console.WriteLine("Daves mods 1.1: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name);


				logLevel = (Common.CommandLine.Log.SeverityLevel)logLevelArg.Value;
				Database database = new Database(Common.Properties.ConnectionString);
				database.CreateDatabaseIfDoesNotExist();
				Console.WriteLine("CreateDatabaseIfDoesNotExist done OK");
				SqlScriptRunner.ScriptProjectRunner runner = new SqlScriptRunner.ScriptProjectRunner(
					pathToSqlProject.Directory.FullName,
					Common.Properties.ConnectionString,
					encryptScriptsInDatabase.Value,
					applyDataScripts.Value
				);
				ScriptProjectRunner.Message += new SqlScriptRunner.ScriptProjectRunner.MessageHandler(runner_Message);
				runner.SynchroniseDatabaseWithScripts();
				Console.WriteLine("SynchroniseDatabaseWithScripts done OK");
				Console.WriteLine("Finished OK");
				Environment.Exit(0);
			}
			catch (ScriptExecutionException scriptEx)
			{
				Console.WriteLine(scriptEx.ToString());
				Common.CommandLine.Log.Error(scriptEx.Message, scriptEx.ScriptFileName);
			}
			catch (CannotFindDependencyScriptException depEx)
			{
				Console.WriteLine(depEx.ToString());
				Common.CommandLine.Log.Error(depEx.Message, depEx.ExecutingScriptFileName);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Common.CommandLine.Log.Error(ex.ToString());
			}
			Environment.Exit(1);
		}


		void cg_Progress()
		{
			Console.Write(".");
		}


		void runner_Message(string message, Common.CommandLine.Log.SeverityLevel messageLevel)
		{

			if (logLevel >= messageLevel)
			{
				Common.CommandLine.Log.Write(message, "SqlScriptRunner.exe", messageLevel);

			}
		}


	}
}
