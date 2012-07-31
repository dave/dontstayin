using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Bobs;

namespace Spotted.Admin
{
	public partial class Sql : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Usr.Current.IsSuperAdmin)
				throw new Exception();
		}
		protected void Run(object o, EventArgs e)
		{
			if (!Usr.Current.IsSuperAdmin)
				throw new Exception();

			if (Password.Text != "sylvia782sql")
				throw new Exception("wrong password!");

			DataSet dataSet = new DataSet();
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			conn.Open();
			SqlCommand command = new SqlCommand(Query.Text, conn);
			SqlDataAdapter Adapter = new SqlDataAdapter();
			Adapter.SelectCommand = command;
			Adapter.Fill(dataSet, "Table");

			Response.Clear();
			Response.ContentType = "application/ms-excel";
			Response.AddHeader("Content-Disposition", "attachment;filename=Query.csv");

			bool firstCol = true;
			foreach (DataColumn c in dataSet.Tables[0].Columns)
			{
				if (!firstCol)
					Response.Write(",");
				firstCol = false;

				Response.Write("\"");
				Response.Write(c.ColumnName.ToString());
				Response.Write("\"");

			}
			Response.Write("\n");
			
			foreach (DataRow r in dataSet.Tables[0].Rows)
			{
				bool first = true;
				foreach (DataColumn c in dataSet.Tables[0].Columns)
				{
					if (!first)
						Response.Write(",");
					first = false;

					Response.Write("\"");
					Response.Write(r[c].ToString().Replace("\"", "\\\"").Replace("\n", "").Replace("\r", "").TruncateWithDots(256));
					Response.Write("\"");
					
				}
				Response.Write("\n");
			}
			Response.Flush();
			Response.End();
				
				//.Tables[0].Rows .Rows[Query.Paging.RecordsPerPage].
		}
	}
}
