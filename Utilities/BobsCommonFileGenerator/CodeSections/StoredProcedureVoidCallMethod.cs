using System;
using System.Collections.Generic;
using System.Text;

namespace BobsCommonFileGenerator.CodeSections
{
	class StoredProcedureVoidCallMethod : StoredProcedureCallMethod
	{
		internal StoredProcedureVoidCallMethod(string name, string storedProcedureName, List<Common.Automation.Sql.Parameter> parameters)
			: base("private", name, storedProcedureName, "cmd.ExecuteNonQuery();", parameters, "void")
		{
		}
 	}
}
