using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Automation.Sql;
namespace SqlScriptRunner.ScriptTypes
{
    internal  class UpdateScript : Script, IComparable
    {
        internal const string ExtendedPropertyForVersionInfoName = "DatabaseVersion|ModifiedDate|UpdateTimestamp|MostRecentScriptName";
		internal int VersionNumber
		{
			get
			{
				return Int32.Parse(File.Name.Substring(0, File.Name.IndexOf('-')).Trim());
			}
		}
        internal UpdateScript(string path)
            : base(path, SqlScriptRunner.ScriptType.UpdateScript)
        {

            string versionNumberString = this.File.Name.Substring(0, this.File.Name.IndexOf("-")).Trim();
            this.ScriptType = SqlScriptRunner.ScriptType.UpdateScript;
        }

        internal  override bool ShouldBeApplied()
        {
            try
            {
                string valueOfExtendedPropertyForVersionInfoName = SqlExtendedPropertyMethods.GetDatabaseLevelExtendedProperty(Global.GetConnection(), UpdateScript.ExtendedPropertyForVersionInfoName);
                int databaseVersionNumber = Convert.ToInt32(valueOfExtendedPropertyForVersionInfoName.Split('|')[0]);
                
                if (this.VersionNumber < databaseVersionNumber)
                {
                    return false;
                }
                else if (this.VersionNumber == databaseVersionNumber)
                {
                    DateTime modifiedDateOfMostRecentScript = DateTime.Parse(valueOfExtendedPropertyForVersionInfoName.Split('|')[1]);
                    return Math.Abs(this.File.LastWriteTime.Subtract(modifiedDateOfMostRecentScript).TotalSeconds) > 1;
                }
                else
                {
                    return true;
                }
            }
            catch (SqlExtendedPropertyMethods.ObjectDoesNotExist ex)
            {
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        internal  override void LogUpdateInDatabase()
        {
			SqlExtendedPropertyMethods.UpdateDatabaseLevelProperty(Global.GetConnection(), UpdateScript.ExtendedPropertyForVersionInfoName, this.VersionNumber + "|" + this.File.LastWriteTime.ToString() + "|" + DateTime.Now + "|" + this.File.Name);
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return this.VersionNumber.CompareTo(((UpdateScript)(obj)).VersionNumber);
        }

        #endregion
    }
}
