using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace Spotted.Support
{
	public partial class DbComboTest : System.Web.UI.Page
	{
		[Cambro.Web.DbCombo.ResultsMethodAttribute(true)]
		public static object DbComboMethod(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataSet dataset = new DataSet();
			SqlConnection conn = new SqlConnection("Data Source=SOLO; Initial Catalog=db_spotted; Integrated Security=SSPI;");
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.SelectCommand = new SqlCommand("SELECT TOP " + args.Top + " NickName+NickName+NickName+NickName+NickName AS DbComboText, K AS DbComboValue FROM Usr WHERE IsSkeleton=0 AND NickName LIKE @Query ORDER BY NickName", conn);
			adapter.SelectCommand.Parameters.AddWithValue("@Query", "%" + args.Query + "%");
			adapter.Fill(dataset);
			conn.Close();
			return dataset;


		}
		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}
