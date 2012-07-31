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

namespace Spotted.Support
{
	public partial class PhoneUtility : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["type"].Equals("hangup"))
			{
				Phone p = Phone.GetFromMac(Request.QueryString["mac"]);
				SalesCallSet scs = new SalesCallSet(new Query(new And(new Q(SalesCall.Columns.UsrK,p.UsrK), new Q(SalesCall.Columns.InProgress, true))));
				foreach (SalesCall sc in scs)
				{
					sc.InProgress = false;
					sc.DateTimeEnd = DateTime.Now;

					if (sc.DateTimeEnd < sc.DateTimeStart)
						sc.DateTimeEnd = sc.DateTimeStart;

					TimeSpan ts = sc.DateTimeEnd - sc.DateTimeStart;
					sc.Duration = ts.TotalMinutes;

					sc.Update();

				}
			}
			else if (Request.QueryString["type"].Equals("register"))
			{
				try
				{
					Phone p = Phone.GetFromMac(Request.QueryString["mac"]);
					p.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
					p.Update();
					Response.Write("Done - " + Request.ServerVariables["REMOTE_ADDR"]);
				}
				catch
				{
					Response.Write("Can't find phone with mac: " + Request.QueryString["mac"]);
				}
			}
		}
	}
}
