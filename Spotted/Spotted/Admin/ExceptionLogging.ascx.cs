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
using Bobs;

namespace Spotted.Admin
{
	public partial class ExceptionLogging : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Query q = new Query();
			q.Columns = new ColumnSet(SpottedException.Columns.Message, SpottedException.Columns.MasterPath, SpottedException.Columns.PagePath);
			q.ExtraSelectElements.Add("Count", "count(distinct case when dsiguid is null then newid() else dsiguid end)");
			q.GroupBy = new GroupBy(new GroupBy(SpottedException.Columns.Message), new GroupBy(SpottedException.Columns.MasterPath), new GroupBy(SpottedException.Columns.PagePath));

			Q notInQ = new Q(true);
			foreach (string s in new string[] { "Bobs.BobNotFound", "Bobs.DSIUserFriendlyException", "MalformedUrlException" })
			{
				notInQ = new And(notInQ, new Q(SpottedException.Columns.ExceptionType, QueryOperator.NotEqualTo, s));
			}
			q.QueryCondition = new And(new Q(SpottedException.Columns.ExceptionDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddDays(-1)),
				notInQ);
			q.OrderBy = new OrderBy("Count DESC");
			q.TopRecords = 50;

			GridView.DataSource = new Bobs.SpottedExceptionSet(q);
			GridView.DataBind();
		}
	}
}
