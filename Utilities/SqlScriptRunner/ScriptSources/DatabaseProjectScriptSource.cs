using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
namespace SqlScriptRunner.ScriptSources
{
    internal class DatabaseProjectScriptSource : ScriptSource
    {

        internal string path;

        internal int current;

        internal string[] fileContent;

        internal string currentPath;

        internal int depth = 0;

        internal DatabaseProjectScriptSource(string path)
        {
            this.path = path;
            currentPath = new FileInfo(path).Directory.FullName + @"\";
            fileContent = File.ReadAllLines(path);
            current = 0;
            while ((current < fileContent.Length))
            {
                if (IsStartOfFolderDefinition())
                {
                    switch (GetBitInQuotes())
                    {
                        case ScriptSource.UpdateScriptDir:
                            AddScriptsFromDirectory(ScriptType.UpdateScript);
                            break;
                        case ScriptSource.CreateScriptDir:
                            AddCreateScripts();
                            break;
                        case ScriptSource.DataScriptDir:
                            AddScriptsFromDirectory(ScriptType.TableData);
							break;
						case ScriptSource.OnSyncScriptDir :
							AddScriptsFromDirectory(ScriptType.OnSync);
                            break;
                    }
                }
                MoveToNextLine();
            }
        }
        string Line
        {
            get
            {
                if ((current < fileContent.Length))
                {
                    return this.fileContent[current].Trim();
                }
                else
                {
                    return "";
                }
            }
        }

        

        void AddCreateScripts()
        {
            MoveToNextLine();
            while (!IsEndOfFolderDefinition())
            {
                if (IsStartOfFolderDefinition())
                {
                    switch (GetBitInQuotes())
                    {
                        case ScriptSource.ProcedureSubDir :
                            AddScriptsFromDirectory(ScriptType.StoredProcedure);
                            break;
                        case ScriptSource.ViewSubDir :
                            AddScriptsFromDirectory(ScriptType.View);
                            break;
                        case ScriptSource.FunctionSubDir :
                            AddScriptsFromDirectory(ScriptType.UserDefinedFunction);
                            break;
                        case ScriptSource.TriggerSubDir:
                            AddScriptsFromDirectory(ScriptType.Trigger);
                            break;
                    }
                }
                else
                {
                    throw new Exception(("Should only be folder definitions in Create Script folder. Found : " + Line));
                }
            }
        }

        void AddScriptsFromDirectory(string type)
        {
            MoveToNextLine();
            while (!IsEndOfFolderDefinition())
            {
                if (IsStartOfFolderDefinition())
                {
                    AddScriptsFromDirectory(type);
                }
                else if (this.IsScriptDefinition())
                {
                    AddScript(type, (currentPath + GetBitInQuotes()));
                }
                else
                {
                    throw new Exception(("Should only be script or folder definitions in "
                                    + (currentPath + (". Found : " + Line))));
                }
                MoveToNextLine();
            }
        }

        void AddFolderPathToCurrentPath()
        {
            this.currentPath = (this.currentPath
                        + (GetBitInQuotes() + "\\"));
            this.depth++;
        }

        void MoveToNextLine()
        {
            if (this.IsStartOfFolderDefinition())
            {
                AddFolderPathToCurrentPath();
            }
            current++;
            if (this.IsEndOfFolderDefinition())
            {
                GoUpALevel();
            }
        }

        internal void GoUpALevel()
        {
            if ((this.depth >= 0))
            {
                this.currentPath = this.currentPath.Substring(0, (this.currentPath.Length - 1));
                // remove trailing \
                this.currentPath = this.currentPath.Substring(0, (this.currentPath.LastIndexOf("\\") + 1));
                this.depth--;
            }
        }

        string GetBitInQuotes()
        {
            return Line.Substring((Line.IndexOf("\"") + 1), (Line.LastIndexOf("\"")
                            - (Line.IndexOf("\"") - 1))).Replace("\"\"", "\"");
        }

        bool IsEndOfFolderDefinition()
        {
            return Line == "End";
        }

        bool IsStartOfFolderDefinition()
        {
        if (Line.Length >= "Begin Folder".Length ){
            return Line.Substring(0, "Begin Folder".Length) == "Begin Folder";
		}else{
			return false;
		}
        }

        bool IsScriptDefinition()
        {
            try
            {
                return Line.Substring(0, "Script =".Length) == "Script =";
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                return false;
            }
        }
    }

}
