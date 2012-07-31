
using System.Collections.Generic;
using System;
using System.IO;
using SqlScriptRunner.ScriptTypes;
using SqlScriptRunner.ScriptSources;
using Common.Automation.Sql;
using Common.Clocks;
using Common;
namespace SqlScriptRunner
{
    public class ScriptProjectRunner
    {
        public delegate void MessageHandler(string message, Common.CommandLine.Log.SeverityLevel level);
        public static event MessageHandler Message;
		string scriptSourcePath;
        
        
        ScriptSource scriptSource;
        bool encryptScripts;
        bool applyDataDirectory;
        Global global = new Global();
		

        bool updateOrObjectScriptsWereRun = false;
        public bool UpdateOrObjectScriptsWereRun
        {
            get
            {
                return updateOrObjectScriptsWereRun;
            }
        }


        internal static void Log(string message, Common.CommandLine.Log.SeverityLevel level)
        {
            if (Message != null)
            {
                Message(message, level);
            }
        }

        public ScriptProjectRunner(string scriptSourcePath, string connectionstring, bool encryptScripts, bool applyDataDirectory)
        {

			if (Message != null) Message("Starting Sql sync", Common.CommandLine.Log.SeverityLevel.Message);
			this.scriptSourcePath = scriptSourcePath;
			if (Message != null) Message("Path:" + scriptSourcePath, Common.CommandLine.Log.SeverityLevel.Message);
            this.scriptSource = GetScriptSource(scriptSourcePath);
            Global.ConnectionString = connectionstring;
			if (Message != null) Message("ConnectionString: " + connectionstring, Common.CommandLine.Log.SeverityLevel.Message);
            Global.ToolStarted = Time.Now;
            this.encryptScripts = encryptScripts;
            this.applyDataDirectory = applyDataDirectory;
        }

		

        public void SynchroniseDatabaseWithScripts()
        {
			try
			{

				ApplyScripts(this.scriptSource.UpdateScripts.ConvertAll(s => (Script)s));



				ApplyScripts(this.scriptSource.ViewScripts.ConvertAll(s => (Script)s));
				ApplyScripts(this.scriptSource.FunctionScripts.ConvertAll(s => (Script)s));
				ApplyScripts(this.scriptSource.ProcedureScripts.ConvertAll(s => (Script)s));
				ApplyScripts(this.scriptSource.TriggerScripts.ConvertAll(s => (Script)s));
				ApplyScripts(this.scriptSource.OnSyncScripts.ConvertAll(s => (Script)s));
				if (this.applyDataDirectory && Global.ConnectionString.IndexOf(Common.Properties.LiveDatabaseServerIp.ToString()) == -1)
				{
					ApplyScripts(this.scriptSource.TableDataScripts.ConvertAll(s => (Script)s));
				}
				if (updateOrObjectScriptsWereRun)
				{
					DirectoryInfo directory = new DirectoryInfo(scriptSourcePath);
					string propertyName = "DatabaseChangeCounter(" + directory.Name + ")";
					try
					{
						int? version = Int32.Parse(SqlExtendedPropertyMethods.GetDatabaseLevelExtendedProperty(Global.GetConnection(), propertyName));
						SqlExtendedPropertyMethods.UpdateDatabaseLevelProperty(Global.GetConnection(), propertyName, (++version).ToString());
					}
					catch (SqlExtendedPropertyMethods.ObjectDoesNotExist)
					{
						SqlExtendedPropertyMethods.UpdateDatabaseLevelProperty(Global.GetConnection(), propertyName, "1");
					}
				}
			}
			catch (Exception ex)
			{
				Message(ex.ToString(), Common.CommandLine.Log.SeverityLevel.Error);
			}
        }


            
            
        

        

        private void ApplyScripts(List<Script> list)
        {
            foreach (Script s in list)
            {
                RunScript(s);
            }
        }
        


        void RunScript(Script script)
        {
            try
            {
                if (script.ShouldBeApplied())
                {
                    script.ApplyToDatabase(encryptScripts);
                    if (script.ScriptType != SqlScriptRunner.ScriptType.TableData)
                    {
						
                        this.updateOrObjectScriptsWereRun = true;
                    }
					if (Message != null) Message("Script applied:" + script.File.Name, Common.CommandLine.Log.SeverityLevel.Message);
                }
            }
            catch (DependencyException depEx)
            {
                foreach (string dependancy in depEx.Dependencies)
                {
                    string objectName = dependancy;
                    ObjectScript requiredScript = null;

                    if (objectName.Substring(0, 4) == "dbo.")
                    {
                        objectName = objectName.Substring(objectName.Length - 4);//strip dbo. from object name
                    }
                    if (requiredScript == null) { requiredScript = this.scriptSource.FunctionScripts.Find(s => s.ObjectName == objectName); };
                    if (requiredScript == null) { requiredScript = this.scriptSource.ProcedureScripts.Find(s => s.ObjectName == objectName); };
                    if (requiredScript == null) { requiredScript = this.scriptSource.ViewScripts.Find(s => s.ObjectName == objectName); };

                    if (requiredScript == null)
                    {
                        throw new CannotFindDependencyScriptException(script, objectName);
                    }
                    else
                    {
                        RunScript(requiredScript);
                    }

                }
                RunScript(script);	//reapply original script after dependencies have been created
            }
        }

      


        private static ScriptSource GetScriptSource(string path)
        {
            ScriptSource scriptSource;
            FileInfo fi = new FileInfo(path);

            if (!fi.Exists)
            {
                DirectoryInfo di = new DirectoryInfo(path);
                if (di.Exists)
                {
                    FileInfo[] projectFiles = di.GetFiles("*.dbp");
                    if (projectFiles.Length == 1)
                    {
                        scriptSource = new DatabaseProjectScriptSource(projectFiles[0].FullName);
                    }
                    else
                    {
                        projectFiles = di.GetFiles("*.csproj");
                        if (projectFiles.Length == 1)
                        {
                            scriptSource = new CSharpProjectScriptSource(projectFiles[0].FullName);
                        }
                        else
                        {
                            scriptSource = new FolderStructureScriptSource(path);
                        }
                    }

                }
                else
                {
                    throw new Exception(String.Format("Path {0} was not found", path));
                }
            }else{
            
                switch (fi.Extension)
                {
                    case "dbp": scriptSource = new DatabaseProjectScriptSource(path); break;
                    case "csproj": scriptSource = new CSharpProjectScriptSource(path); break;
                    default: throw new Exception(String.Format("Unknown project type '{0}'", fi.Extension));
                }
            }
            return scriptSource;
            
        }

 

        

    }





}
