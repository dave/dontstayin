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
	public partial class Logs : AdminUserControl
	{
		protected DataGrid Times;
		protected Calendar Cal;
		DateTime Date
		{
			get
			{
				return Cal.SelectedDate;
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
				Cal.SelectedDate = DateTime.Today;
			bind(Date);
		}
		protected void bind(DateTime d)
		{
			Query q = new Query();
			q.NoLock = false;
			q.QueryCondition = new Q(Log.Columns.Date, d.Date);
			q.OrderBy = new OrderBy(Log.Columns.Item);
			LogSet ls = new LogSet(q);
			Times.DataSource = ls;
			Times.DataBind();
		}
		public void ReBind(object o, System.EventArgs e)
		{
			bind(Date);
		}
	}
}
