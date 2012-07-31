using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlScriptRunner.ScriptSources
{

    internal class CSharpProjectScriptSource : ScriptSource
    {
        internal System.IO.FileInfo projectFile;
        internal int current;
        internal string[] fileContent;

        internal CSharpProjectScriptSource(string path)
        {
            this.projectFile = new System.IO.FileInfo(path);
            fileContent = System.IO.File.ReadAllLines(projectFile.FullName);
            current = 0;
            while (current < fileContent.Length)
            {
                if (IsScriptDefinition(Line))
                {
                    string relativePathToScript = GetBitInQuotes();

                    string absolutePathToScript = System.IO.Path.Combine(projectFile.DirectoryName, relativePathToScript);
                    string scriptType = null;
                    if (absolutePathToScript.IndexOf("\\" + ScriptSource.UpdateScriptDir  + "\\") > -1) {scriptType = ScriptType.UpdateScript ;}
                    if (absolutePathToScript.IndexOf("\\" + ScriptSource.CreateScriptDir  + "\\" + ScriptSource.ProcedureSubDir + "\\") > -1) { scriptType = ScriptType.StoredProcedure; }
                    if (absolutePathToScript.IndexOf("\\" + ScriptSource.CreateScriptDir  + "\\" + ScriptSource.FunctionSubDir + "\\") > -1) { scriptType = ScriptType.UserDefinedFunction; }
                    if (absolutePathToScript.IndexOf("\\" + ScriptSource.CreateScriptDir  + "\\" + ScriptSource.ViewSubDir + "\\") > -1) { scriptType = ScriptType.View; }
                    if (absolutePathToScript.IndexOf("\\" + ScriptSource.CreateScriptDir  + "\\" + ScriptSource.TriggerSubDir + "\\") > -1) { scriptType = ScriptType.Trigger; }
                    if (absolutePathToScript.IndexOf("\\" + ScriptSource.DataScriptDir    + "\\") > -1) { scriptType = ScriptType.TableData; }
					if (absolutePathToScript.IndexOf("\\" + ScriptSource.OnSyncScriptDir + "\\") > -1) { scriptType = ScriptType.OnSync; }
                  
                    AddScript(scriptType, absolutePathToScript);
                }
                MoveToNextLine();
            }
        }

        internal  static string GetFirstBit(string path)
        {
            return path.Substring(0, path.IndexOf("\\"));
        }

        internal  static string GetSecondBit(string path)
        {
            int indexOfFirstSlash = path.IndexOf("\\");
            return path.Substring(indexOfFirstSlash + 1, path.IndexOf("\\", indexOfFirstSlash + 1));
        }

        void MoveToNextLine()
        {
            current += 1;
        }

        string Line
        {
            get
            {
                if (current < fileContent.Length)
                {
                    return this.fileContent[current].Trim();
                }
                else
                {
                    return "";
                }
            }
        }

        string GetBitInQuotes()
        {
            return Line.Substring(Line.IndexOf("\"") + 1, Line.LastIndexOf("\"") - Line.IndexOf("\"") - 1).Replace("\"\"", "\"");
        }

        static bool IsScriptDefinition(string line)
        {
            try
            {
                return line.IndexOf("<None Include=\"") > -1 & line.IndexOf(".sql\" />") > -1;
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                return false;
            }
        }
    }
}
