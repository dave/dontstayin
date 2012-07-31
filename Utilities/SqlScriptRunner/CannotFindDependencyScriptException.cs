using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlScriptRunner
{
    internal class CannotFindDependencyScriptException : Exception 
    {
        string message;
		public string ExecutingScriptFileName { get; private set; }
        internal CannotFindDependencyScriptException(ScriptTypes.Script executingScript, string dependencyScriptName)
        {
			ExecutingScriptFileName = executingScript.File.FullName;
            message = String.Format("{0} is dependant on script '{1}' which could not be found", executingScript.File.FullName, dependencyScriptName);
        }
        public override string Message
        {
            get
            {
                return message;
            }
        }
    }
}
