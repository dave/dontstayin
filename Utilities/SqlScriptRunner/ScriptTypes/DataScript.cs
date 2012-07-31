using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Automation.Sql;
namespace SqlScriptRunner.ScriptTypes
{
    internal  class DataScript : Script
    {

		string DataScriptLastRun_ExtendedPropertyName = "DataScriptLastRun";

        internal DataScript(string path): base(path, SqlScriptRunner.ScriptType.TableData)
        {
			this._objectName = this.File.Name.Split('.')[0];
        }

        internal  override void LogUpdateInDatabase()
        {
			SqlExtendedPropertyMethods.Update(Global.GetConnection(), DataScriptLastRun_ExtendedPropertyName, this.File.LastWriteTime.ToString(), this._objectName, this.ScriptType);
        }

        internal  override bool ShouldBeApplied()
        {
			return IsNewerThanItWasLastTimeItWasRun();
        }

		private bool IsNewerThanItWasLastTimeItWasRun()
		{
			try
			{
				DateTime dbDate = DateTime.Parse(SqlExtendedPropertyMethods.GetObjectLevelExtendedProperty(Global.GetConnection(), DataScriptLastRun_ExtendedPropertyName, this._objectName, this.ScriptType));
				return Math.Abs(this.File.LastWriteTime.Subtract(((DateTime)(dbDate))).TotalSeconds) > 1;
			}
			catch (SqlExtendedPropertyMethods.ObjectDoesNotExist)
			{
				return true;
			}
		}
    }
}
