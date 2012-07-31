using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace SqlScriptRunner
{

    public class ScriptExecutionException : Exception
    {
        ScriptTypes.Script script;
        public ScriptExecutionException(ScriptTypes.Script script, Exception ex):base("Error in " + script.File.Name + "\n" + ex.Message, ex)
        {
            this.script = script;
        }
		public String ScriptFileName
		{
			get
			{
				return script.File.FullName;
			}
		}
    
    }
}
