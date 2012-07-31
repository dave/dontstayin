using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace SqlScriptRunner.ScriptSources
{
    using System.Collections.Generic;
    internal class FolderStructureScriptSource : ScriptSource
    {
        DirectoryInfo dir;

        internal FolderStructureScriptSource(string path)
        {
            
            dir = new DirectoryInfo(path);
			AddScriptsFromFolder(Path.Combine(dir.FullName, ScriptSource.OnSyncScriptDir), "*.sql", true, ScriptType.OnSync);
            AddScriptsFromFolder(Path.Combine(dir.FullName, ScriptSource.UpdateScriptDir), "*.sql", true, ScriptType.UpdateScript);
            AddScriptsFromFolder(Path.Combine(dir.FullName, ScriptSource.DataScriptDir) , "*.sql", true, ScriptType.TableData);
            string createScriptDir = Path.Combine(dir.FullName, ScriptSource.CreateScriptDir);
            AddScriptsFromFolder(Path.Combine(createScriptDir, ScriptSource.ViewSubDir ), "*.sql", true, ScriptType.View);
            AddScriptsFromFolder(Path.Combine(createScriptDir, ScriptSource.FunctionSubDir) , "*.sql", true, ScriptType.UserDefinedFunction);
            AddScriptsFromFolder(Path.Combine(createScriptDir, ScriptSource.ProcedureSubDir) , "*.sql", true, ScriptType.StoredProcedure);
            AddScriptsFromFolder(Path.Combine(createScriptDir, ScriptSource.TriggerSubDir), "*.sql", true, ScriptType.Trigger);
        }

        private void AddScriptsFromFolder(string path, string extensionMask, bool recurse, string type)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!(dir.Exists))
            {
                return;
            }
            foreach (FileInfo file in dir.GetFiles(extensionMask))
            {
                AddScript(type, file.FullName);
            }
            if (recurse)
            {
                foreach (DirectoryInfo directory in dir.GetDirectories())
                {
                    AddScriptsFromFolder(directory.FullName, extensionMask, recurse, type);
                }
            }
        }
    }
}
