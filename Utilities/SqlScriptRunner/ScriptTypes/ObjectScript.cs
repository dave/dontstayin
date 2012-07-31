using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Automation.Sql;

namespace SqlScriptRunner.ScriptTypes
{
    internal  class ObjectScript :  Script
    {
        const string ExtendedPropertyForLastModifiedName = "LastModified";

        internal ObjectScript(string path, string scriptType)
            : base(path, scriptType)
        {
            this._objectName = this.File.Name.Substring(0, this.File.Name.LastIndexOf("."));
        }

        internal  override bool ShouldBeApplied()
        {
            return IsDifferentToTheOneInTheDatabase();
        }

        private bool IsDifferentToTheOneInTheDatabase()
        {
            try
            {
				DateTime dbDate = DateTime.Parse(SqlExtendedPropertyMethods.GetObjectLevelExtendedProperty(Global.GetConnection(), ExtendedPropertyForLastModifiedName, this._objectName, this.ScriptType));
                return Math.Abs(this.File.LastWriteTime.Subtract(((DateTime)(dbDate))).TotalSeconds) > 1;
            }
            catch (SqlExtendedPropertyMethods.ObjectDoesNotExist)
            {
                return true;
            }
        }

        internal  override void LogUpdateInDatabase()
        {
			SqlExtendedPropertyMethods.Update(Global.GetConnection(), ExtendedPropertyForLastModifiedName, this.File.LastWriteTime.ToString(), this._objectName, this.ScriptType);
        }

        internal string ObjectName
        {
            get
            {
                return this._objectName;
            }
        }
    }
}
