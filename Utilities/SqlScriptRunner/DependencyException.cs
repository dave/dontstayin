using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlScriptRunner
{

    internal class DependencyException : Exception
    {
        List<string> _dependencies = new List<string>();
        ScriptExecutionException ex;
        public DependencyException(ScriptExecutionException ex)
        {
            this.ex = ex;
        }
        internal void addDependency(string objectName)
        {
            this._dependencies.Add(objectName);
        }
        internal string[] Dependencies
        {
            get
            {
                return this._dependencies.ToArray();
            }
        }
        public  override string Message
        {
            get
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("Missing dependencies:");
                foreach (string dependency in this._dependencies)
                {
                    sb.AppendLine("  " + dependency);
                }
                sb.AppendLine(ex.Message);
                return sb.ToString();
            }

        }
    }
}
