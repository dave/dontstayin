using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlScriptRunner
{
	internal class ScriptType
	{
		internal const string TableData = "data";
		internal const string UpdateScript = "update";
		internal const string StoredProcedure = "procedure";
		internal const string UserDefinedFunction = "function";
		internal const string Trigger = "trigger";
		internal const string View = "view";
		internal const string OnSync = "onsync";
	}
}
