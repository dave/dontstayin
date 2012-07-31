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

namespace Spotted.Master
{
	public partial class AdminPage : GenericPage
	{
		public HtmlForm TemplateForm;
		//protected HtmlGenericControl H1Tag;

		private void Page_Load(object sender, System.EventArgs e)
		{
			ScriptManager.RegisterStartupScript(Page, typeof(Page), "Tip", "mig_hand();", true);

			HttpContext.Current.Items["PageAdmin"] = true;

			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				BindCalls();
			}

			// The following line of code [DataBind();] breaks all data bound web controls (Repeater, GridView, DataList, etc)
			// Therefore, as discussed by Neil and Dave on Sep 12, 06, this line will be commented out until further notice.
			// Initial trial of DataBing() when !IsPostBack has been successful. (Oct 31, 06)
			if (!this.IsPostBack)
				this.DataBind();
		}

		public void BindCalls()
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(SalesCall.Columns.UsrK, Usr.Current.K), 
				new Q(SalesCall.Columns.IsCall, true), 
				new Or(new Q(SalesCall.Columns.InProgress, true), new Q(SalesCall.Columns.Dismissed, false)));
			SalesCallSet scs = new SalesCallSet(q);
			if (scs.Count > 0)
			{
				foreach (SalesCall sc in scs)
				{
					Spotted.Controls.Admin.SalesCallControl c = (Spotted.Controls.Admin.SalesCallControl)this.LoadControl("~/Controls/Admin/SalesCallControl.ascx");
					c.CurrentSalesCall = sc;
					SalesCallPlaceHolder.Controls.Add(c);
				}
			}
		}

		private void Page_Init(object sender, System.EventArgs e)
		{
			//H1Tag.InnerText = Url.PageName;
			Log.Increment(Log.Items.AdminPages);
		}
		public AdminUserControl ContentUserControl
		{
			get
			{
				return (AdminUserControl)GenericUserControl;
			}
		}
	}
}
