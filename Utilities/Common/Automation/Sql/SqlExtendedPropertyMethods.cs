using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
namespace Common.Automation.Sql
{
    public  class SqlExtendedPropertyMethods
    {
       
        #region GetStoredProcParameters
        static SqlParameter[] GetStoredProcParameters(string objectName, string objectType)
        {
            SqlParameter[] storedProcParameters = null;
            switch (objectType)
            {
                case ScriptType.UpdateScript:
                    storedProcParameters = GetStoredProcParameters(null, null, null, null, null, null);
                    break;
                case ScriptType.Trigger:
                    storedProcParameters = GetStoredProcParameters("user", "dbo", "table", objectName.Split('_')[0], "trigger", objectName);
                    break;
				case ScriptType.TableData:
					storedProcParameters = GetStoredProcParameters("user", "dbo", "table", objectName, null, null);
					break;
                default:
                    storedProcParameters = GetStoredProcParameters("user", "dbo", objectType, objectName, null, null);
                    break;
            }
            return storedProcParameters;
        }
        static SqlParameter[] GetStoredProcParameters(string level0type, string level0name, string level1type, string level1name, string level2type, string level2name)
        {
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@level0type", level0type != null ? (object) level0type : DBNull.Value);
            p[1] = new SqlParameter("@level0name", level0name != null ? (object) level0name : DBNull.Value);
            p[2] = new SqlParameter("@level1type", level1type != null ? (object) level1type : DBNull.Value);
            p[3] = new SqlParameter("@level1name", level1name != null ? (object) level1name : DBNull.Value);
            p[4] = new SqlParameter("@level2type", level2type != null ? (object) level2type : DBNull.Value);
            p[5] = new SqlParameter("@level2name", level2name != null ? (object) level2name : DBNull.Value);
            return p;
        }
        #endregion
        #region run stored procedure methods
        static string Get(SqlConnection conn, string name, SqlParameter[] storedProcParameters)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT value FROM ::fn_listextendedproperty (@name,@level0type,@level0name,@level1type,@level1name,@level2type,@level2name) WHERE NAME = @name", conn);
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 128).Value = name;
                cmd.Parameters.AddRange(storedProcParameters);
                object value = cmd.ExecuteScalar();
                if (value == null)
                {
                    throw new ObjectDoesNotExist();
                }
                return value.ToString();
            }
            finally
            {
                conn.Close();
            }
        }
        static void Add(System.Data.SqlClient.SqlConnection conn, string propertyName, string value, SqlParameter[] storedProcParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_addextendedproperty", conn);
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 128).Value = propertyName;
                cmd.Parameters.AddWithValue("@value", SqlDbType.DateTime).Value = value;
                cmd.Parameters.AddRange(storedProcParameters);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
        static void Delete(SqlConnection conn, string propertyName, SqlParameter[] storedProcParameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_dropextendedproperty", conn);
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 128).Value = propertyName;
                cmd.Parameters.AddRange(storedProcParameters);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
		#endregion
        #region Update
        public static void Update(SqlConnection conn, string propertyName, string value)
        {
            Update(conn, propertyName, value, "", ScriptType.UpdateScript);
        }

		public static void Update(SqlConnection conn, string propertyName, string value, string objectName, string objectType)
        {
             
            try
            {
                Get(conn, propertyName, GetStoredProcParameters(objectName, objectType));
                Delete(conn, propertyName, GetStoredProcParameters(objectName, objectType));
            }
            catch (ObjectDoesNotExist)
            {
            }
            Add(conn, propertyName, value, GetStoredProcParameters(objectName, objectType));
        }
        #endregion
        public class ObjectDoesNotExist : System.Exception
        {

        }
		
        public static string GetDatabaseLevelExtendedProperty(SqlConnection conn, string p)
        {
            return Get(conn, p, GetStoredProcParameters("", ScriptType.UpdateScript));
        }

        public static void UpdateDatabaseLevelProperty(SqlConnection conn, string name, string value)
        {
            Update(conn, name, value);
        }

        public static string GetObjectLevelExtendedProperty(SqlConnection conn, string name, string objectName, string objectType)
        {
            return Get(conn, name, GetStoredProcParameters(objectName, objectType));
        }


		public static void SetDatabaseLevelExtendedProperty()
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
