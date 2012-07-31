using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using Common.Clocks;
using Common;

namespace SqlScriptRunner.ScriptTypes
{
    public  abstract class Script
    {
        internal string _objectName;
        protected string content;
        internal FileInfo File { get; set; }
        internal string ScriptType { get; set; }
        internal Script()
        {

        }

        internal Script(string path, string scriptType)
        {
            File = new FileInfo(path);
            ScriptType = scriptType;
        }


        string GetContent()
        {
            StreamReader reader = new StreamReader(File.FullName, System.Text.Encoding.Default, true);
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append(reader.ReadToEnd());
            reader.Close();
            return builder.ToString();
        }


		
        

        internal  abstract bool ShouldBeApplied();

       


        internal  void ApplyToDatabase(bool withEncryption)
        {
            string[] scripts = SqlScriptParser.Parse(this.GetContent(), withEncryption);
            ExecuteProcs(scripts);
            LogUpdateInDatabase();
        }

        internal  abstract void LogUpdateInDatabase();

        //internal bool IsCheckedOut()
        //{
        //    return !((File.Attributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly);
        //}

        void ExecuteProcs(string[] scripts)
        {
            SqlTransaction trans = null;
            string commandText = null;
            SqlConnection conn = new SqlConnection(Global.ConnectionString + ";Connection Timeout=0");
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                
                for (int i=0;i<scripts.Count();i++)
                {
                    commandText = scripts[i];
                    if (commandText.Trim() != "")
                    {

						try
						{
							SqlCommand cmd = new SqlCommand(commandText, conn, trans);
							cmd.CommandTimeout = 0;
							cmd.ExecuteNonQuery();
						}
						catch (SqlException ex)
						{
							if (ex.Message.Contains("@WorkingDirectory"))
							{
								string workingDirectoryDef = "DECLARE @WorkingDirectory VARCHAR(MAX) SET @WorkingDirectory = '" + Directory.GetCurrentDirectory() + "' \r\n";
								var cmd2 = new SqlCommand(workingDirectoryDef + commandText, conn, trans);
								cmd2.CommandTimeout = 0;
								cmd2.ExecuteNonQuery();
							}
							else
							{
								throw ex;
							}
						}
                        
                    }
                }
                trans.Commit();
                
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                trans.Rollback();
                ScriptExecutionException scriptEx = new ScriptExecutionException(this, ex);
                ArrayList missingObjectNames = MissingObjectExceptionMessage.GetMissingObjectNames(ex.Message);
                if (missingObjectNames.Count == 0)
                {
                    LogTable.Write(this, Time.Now, "Failure: " + ex.ToString());
                    throw scriptEx;
                }
                else
                {
                    DependencyException depEx = new DependencyException(scriptEx);
                    foreach (string missingObjectName in missingObjectNames)
                    {
                        depEx.addDependency(missingObjectName);
                    }
                    throw depEx;
                }
            }
            finally
            {
                conn.Close();
            }
            LogTable.Write(this, Time.Now, "Success");
        }
        class NotAMissingObjectException : Exception
        {
        }
        class MissingObjectExceptionMessage
        {

            public static ArrayList GetMissingObjectNames(string message)
            {
                try
                {
                    ArrayList missingObjectNames = new ArrayList();
                    ArrayList someMissingObjectNames = GetMissingObjectNames(message, "Invalid object name ", '\'');
                    foreach (string objectName in someMissingObjectNames)
                    {
                        if (!(missingObjectNames.Contains(objectName)))
                        {
                            missingObjectNames.Add(objectName);
                        }
                    }
                    someMissingObjectNames.Clear();
                    someMissingObjectNames = GetMissingObjectNames(message, "the user-defined function or aggregate ", '"');
                    foreach (string objectName in someMissingObjectNames)
                    {
                        if (!(missingObjectNames.Contains(objectName)))
                        {
                            missingObjectNames.Add(objectName);
                        }
                    }
                    return missingObjectNames;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            private static ArrayList GetMissingObjectNames(string message, string searchString, char delimiter)
            {
                ArrayList missingObjectNames = new ArrayList();
                Int32 pos = 0;
                int indexOfInvalidObjectNameMessage = message.IndexOf(searchString, pos);
                while (indexOfInvalidObjectNameMessage > -1)
                {
                    pos = message.IndexOf(delimiter, indexOfInvalidObjectNameMessage + searchString.Length + 1);
                    string objectName = message.Substring(indexOfInvalidObjectNameMessage + searchString.Length + 1, pos - indexOfInvalidObjectNameMessage - (searchString.Length + 1));
                    if (objectName.Substring(0, 4).ToLower() != "dbo.")
                    {
                        objectName = "dbo." + objectName;
                    }
                    if (!(missingObjectNames.Contains(objectName)))
                    {
                        missingObjectNames.Add(objectName);
                    }
                    indexOfInvalidObjectNameMessage = message.IndexOf("Invalid object name", pos);
                }
                return missingObjectNames;
            }
        }

    }
}
