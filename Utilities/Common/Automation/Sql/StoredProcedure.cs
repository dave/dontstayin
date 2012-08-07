using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Common.Automation.Sql
{
	/// <summary>
	/// A class which represents a stored procedure in a database
	/// </summary>

	public class StoredProcedure
	{
		private Database database;
		private string name;

		public StoredProcedure(Database database, string name)
		{
			this.database = database;
			this.name = name;
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

		public List<Parameter> Parameters
		{
			get
			{
				string sql = @"
SELECT param.name AS [ParameterName], ISNULL(baset.name, N'') AS [SystemType], 
CAST(CASE WHEN baset.name IN (N'nchar', N'nvarchar') AND param.max_length <> -1 THEN 
param.max_length/2 ELSE param.max_length END AS int) AS [Length],
CAST(param.precision AS int) AS [NumericPrecision],
CAST(param.scale AS int) AS [NumericScale],
null AS [DefaultValue],
param.is_output AS [IsOutputParameter]
FROM
sys.all_objects AS sp
INNER JOIN sys.all_parameters AS param ON param.object_id=sp.object_id
LEFT OUTER JOIN sys.types AS baset ON baset.user_type_id = param.system_type_id and 
baset.user_type_id = baset.system_type_id
WHERE
(sp.type = N'P')and(sp.name=N'" + name + @"' and 
SCHEMA_NAME(sp.schema_id)=N'dbo')
ORDER BY param.parameter_id ASC";
				DataTable dt = database.ExecuteAdapter(sql).Tables[0];

				List<Parameter> parameters = new List<Parameter>();
				foreach (System.Data.DataRow dr in dt.Rows)
				{
					parameters.Add(new Parameter() { Name = (string) dr["ParameterName"], Type = (string) dr["SystemType"] });
				}
				return parameters;
				/*

				

				return list.ToArray();
				 * */
			}
		}
	}
}
