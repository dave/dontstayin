using System;
using System.Collections.Generic;

using System.Text;

namespace Common.Automation.Sql
{
	class DatabaseClrAssembly : ClrAssembly 
	{
		Database databaseInfo = null;
		internal DatabaseClrAssembly(Database databaseInfo, string assemblyName){
			this.name = assemblyName;
			this.databaseInfo = databaseInfo;
		}
		public override string Mvid
		{
			get
			{
				return databaseInfo.ExecuteScalar(
				"SELECT UPPER(CONVERT(VARCHAR(MAX), assemblyproperty('" + this.Name + "','mvid'))")
				as string;
			}
		}
	}
}
