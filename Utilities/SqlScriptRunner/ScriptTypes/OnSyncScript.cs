using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlScriptRunner.ScriptTypes
{
	class OnSyncScript : Script 
	{

		internal OnSyncScript(string path)
			: base(path, SqlScriptRunner.ScriptType.OnSync)
		{

		}
		internal override bool ShouldBeApplied()
		{
			return true;
		}

		internal override void LogUpdateInDatabase()
		{
			
		}
	}
}
