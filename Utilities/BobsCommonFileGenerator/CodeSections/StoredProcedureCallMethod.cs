using System;
using System.Collections.Generic;
using System.Text;

namespace BobsCommonFileGenerator.CodeSections
{
	abstract class StoredProcedureCallMethod : Method
	{
		internal StoredProcedureCallMethod(string accessor, string name, string storedProcedureName, string command, List<Common.Automation.Sql.Parameter> parameters, string returnType)
			: base(accessor, name, ConvertParameters(parameters), GetCode(name, storedProcedureName, command, parameters), returnType)
		{
		}
		static List<MethodParameter> ConvertParameters(List<Common.Automation.Sql.Parameter> sqlParameters)
		{
			List<MethodParameter> list = new List<MethodParameter>();
			foreach (var sqlParameter in sqlParameters)
			{
				
				switch (sqlParameter.Type.ToLower())
				{
					case "int": list.Add(new CodeSections.Int32MethodParameter(sqlParameter.Name.Substring(1))); break;
					case "datetime": list.Add(new CodeSections.DateTimeMethodParameter(sqlParameter.Name.Substring(1))); break;
					case "uniqueidentifier": list.Add(new CodeSections.GuidMethodParameter(sqlParameter.Name.Substring(1))); break;
					case "varchar": list.Add(new CodeSections.StringMethodParameter(sqlParameter.Name.Substring(1))); break;
					case "float": list.Add(new CodeSections.DoubleMethodParameter(sqlParameter.Name.Substring(1))); break;
					default: throw new NotImplementedException("Need to implement type '" + sqlParameter.Type.ToLower() + "'");
				}
			}
			return list;
		}
		private static string[] GetCode(string name, string storedProcedureName, string command, List<Common.Automation.Sql.Parameter> sqlParameters)
		{
			List<MethodParameter> parameters = ConvertParameters(sqlParameters);
			List<string> s = new List<string>();
			s.Add("System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(global::Bobs.Vars.DefaultConnectionString);");
			s.Add("System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(\"[" + storedProcedureName + "]\", conn);");
			s.Add("cmd.CommandType = System.Data.CommandType.StoredProcedure;");
			foreach(MethodParameter parameter in parameters){
				s.Add(String.Format("cmd.Parameters.AddWithValue(\"@{0}\", {1} != null ? (object){1} : DBNull.Value);", parameter.Name, MethodParameter.SetFirstLetterToLowerCase(parameter.Name)));
			}
			s.Add("try");
			s.Add("{");
			s.Add("\tconn.Open();");
			s.Add("\t" + command);
			s.Add("}");
			s.Add("finally");
			s.Add("{");
			s.Add("\tconn.Close();");
			s.Add("}");
			return s.ToArray();					
		}
		
	}
}
