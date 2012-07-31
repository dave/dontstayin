using System;
using System.Collections.Generic;
using System.Text;

namespace BobsCommonFileGenerator.CodeSections
{
	class StoredProcedureDataTableCallMethod : StoredProcedureCallMethod
	{
		internal StoredProcedureDataTableCallMethod(string name, string storedProcedureName, List<Common.Automation.Sql.Parameter> parameters)
			: base("private", name, storedProcedureName, "System.Data.DataTable dt = new DataTable();\r\n\t\t\t\t\t\tdt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));\r\n\t\t\t\t\t\treturn dt;", parameters, "System.Data.DataTable")
		{

		}
 
		
 
	}
}
