using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using Common.Automation.Sql;
namespace BobsCommonFileGenerator
{


    public class DatabaseExecute
    {
		//private string ConnextionString = "Data Source=JABBA; user id=DSIUSRDEV; password=dont47transmit; Initial Catalog=db_spotted;";

		string connectionString;

        public DatabaseExecute(string connectionString )
        {
			this.connectionString = connectionString;
        }

        public DataTable SQLExec(string sqlString)
        {
            //ColumnDef PrimaryKeyColumn = Table[Table.SinglePrimaryKey];

            SqlConnection conn;
            //if (transaction == null)
			conn = new SqlConnection(connectionString);
            //else
            //    conn = transaction.SqlConnection;
            try
            {
                //string sqlString = "SELECT TOP 1 * FROM [" + Table.TableName + "] WHERE [" + Table.TableName + "].[" + Table.ColumnName(Table.SinglePrimaryKey) + "] = @PrimaryKey";
                SqlCommand command = new SqlCommand(sqlString, conn);


                //command.Parameters.Add("@PrimaryKey", PrimaryKeyColumn.SqlDbType);
                //command.Parameters[0].Value = PrimaryKey;

                //if (transaction != null)
                //    command.Transaction = transaction.SqlTransaction;

                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;


                //if (Vars.TraceQueries && HttpContext.Current != null)
                //{
                //    HttpContext.Current.Trace.Write(command.CommandText.Replace("@PrimaryKey", PrimaryKey.ToString()));
                //}

                if (!conn.State.Equals(ConnectionState.Open))
                    conn.Open();

                adapter.Fill(dataset);

                //DataView dv = dataset.Tables[0].DefaultView;
                DataTable dt = dataset.Tables[0];

                //return dv;
                return dt;

                //if (HttpContext.Current != null)
                //{
                //    if (HttpContext.Current.Items["DbQueries"] == null)
                //        HttpContext.Current.Items["DbQueries"] = 1;
                //    else
                //        HttpContext.Current.Items["DbQueries"] = ((int)HttpContext.Current.Items["DbQueries"]) + 1;
                //}

                //if (dv.Count == 1)
                //{
                //    Initialise(dv[0].Row);
                //}
                //else
                //{
                //    throw new BobNotFound("You've requested something that's not in our database. It may have been deleted. (Technical details: " + Table.TableEnum.ToString() + " #" + PrimaryKey + ")");
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (transaction == null)
                //{
                conn.Close();
                conn.Dispose();
                //}
            }
        }

        public DataSet SQLExecDataSet(string sqlString)
        {
            //ColumnDef PrimaryKeyColumn = Table[Table.SinglePrimaryKey];

            SqlConnection conn;
            //if (transaction == null)
            conn = new SqlConnection(this.connectionString );
            //else
            //    conn = transaction.SqlConnection;
            try
            {
                //string sqlString = "SELECT TOP 1 * FROM [" + Table.TableName + "] WHERE [" + Table.TableName + "].[" + Table.ColumnName(Table.SinglePrimaryKey) + "] = @PrimaryKey";
                SqlCommand command = new SqlCommand(sqlString, conn);


                //command.Parameters.Add("@PrimaryKey", PrimaryKeyColumn.SqlDbType);
                //command.Parameters[0].Value = PrimaryKey;

                //if (transaction != null)
                //    command.Transaction = transaction.SqlTransaction;

                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;


                //if (Vars.TraceQueries && HttpContext.Current != null)
                //{
                //    HttpContext.Current.Trace.Write(command.CommandText.Replace("@PrimaryKey", PrimaryKey.ToString()));
                //}

                if (!conn.State.Equals(ConnectionState.Open))
                    conn.Open();

                adapter.Fill(dataset);


                return dataset;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (transaction == null)
                //{
                conn.Close();
                conn.Dispose();
                //}
            }
        }
        
        
        
        
		//public List<Table> GetTables()
		//{
		//    List<Table> TableData = new List<Table>();
		//    DataTable TableList = this.SQLExec("sp_tables");
		//    string TableName;
		//    Table NewTable;
		//    foreach (DataRow TableDetails in TableList.Rows)
		//    {
		//        if (TableDetails["TABLE_OWNER"] as string == "dbo")
		//        {
		//            TableName = TableDetails["TABLE_NAME"] as string;
		//            NewTable = new Table(this.connectionString, TableName);
		//            if (NewTable.Description != null)
		//            {
		//                NewTable.ColumnData = GetColumnData(TableName);
		//                NewTable.SetKeys();
		//                TableData.Add(NewTable);
		//            }
		//        }
		//    }
		//    return TableData;
		//}


		//private List<Column> GetColumnData(string tableName)
		//{
		//    List<Column> ColumnInfo = new List<Column>();

		//    //Get the table containing this table's Column Details
		//    DataTable ColumnDetails = this.SQLExec("sp_columns [" + tableName +"]");

		//    //Get column descriptions
		//    string GetDescriptionsSQL = "SELECT objName, cast(value as varchar(100)) as description   FROM ::FN_LISTEXTENDEDPROPERTY('MS_Description',     'User',  'dbo',     'table', '"+tableName+"',     'column', null)";

		//    DataTable DescriptionDetails = this.SQLExec(GetDescriptionsSQL);

		//    Column NewColumn;

		//    //For each row which repesents a column's attributes, create a ColumnDataContainer
		//    //and add it to the ColumnInfo ArrayList
		//    foreach (DataRow ColumnDetail in ColumnDetails.Rows)
		//    {
		//        //get the details
		//        NewColumn = new Column(ColumnDetail);

		//        //only add to the list if it has a description
		//        if (NewColumn.AddDescription(DescriptionDetails))
		//            ColumnInfo.Add(NewColumn);
                
		//    }

		//    //As the stored proc doesn't return the SQLDBType for use in .NET, run this query against each column in the list
		//    foreach (Column ColumnData in ColumnInfo)
		//    {
		//        ColumnData.SqlDbType = this.GetSQLDBType(tableName, ColumnData);
		//        ColumnData.NativeType = GetNativeType(ColumnData.SqlDbType);
		//    }

		//    return ColumnInfo;

		//}


       

		//public static string GetNativeType(SqlDbType sqlDbType)
		//{
		//    switch (sqlDbType)
		//    {
		//        case SqlDbType.BigInt: return "Int64";
		//        case SqlDbType.Binary: System.Diagnostics.Debugger.Break();return  "???";
		//        case SqlDbType.Bit: return "bool";
		//        case SqlDbType.Char: return "string";
		//        case SqlDbType.DateTime: return "DateTime";
		//        case SqlDbType.Decimal: return "System.Data.SqlTypes.SqlDecimal";
		//        case SqlDbType.Float: return "double";
		//        case SqlDbType.Image: System.Diagnostics.Debugger.Break();return  "???";
		//        case SqlDbType.Int: return "int";
		//        case SqlDbType.Money: System.Diagnostics.Debugger.Break();return  "???";
		//        case SqlDbType.NChar: return "string";
		//        case SqlDbType.NText: return "string";
		//        case SqlDbType.NVarChar: return "string";
		//        case SqlDbType.Real: System.Diagnostics.Debugger.Break();return  "???";
		//        case SqlDbType.SmallDateTime: return "DateTime";
		//        case SqlDbType.SmallInt: return "int";
		//        case SqlDbType.SmallMoney: System.Diagnostics.Debugger.Break();return  "???";
		//        case SqlDbType.Text: return "string";
		//        case SqlDbType.Timestamp: return "byte[]";
		//        case SqlDbType.TinyInt: return "byte";
		//        case SqlDbType.Udt: System.Diagnostics.Debugger.Break();return  "???";
		//        case SqlDbType.UniqueIdentifier: return "Guid";
		//        case SqlDbType.VarBinary: return "System.Data.SqlTypes.SqlBinary";
		//        case SqlDbType.VarChar: return "string";
		//        case SqlDbType.Variant: return "object";
		//        case SqlDbType.Xml: return "string";
		//        default: return "";
		//    }
		//}


    }

}
