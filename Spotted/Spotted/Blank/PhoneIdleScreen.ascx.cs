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

namespace Spotted.Blank
{
	public partial class PhoneIdleScreen : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Request.UserHostAddress.Equals("84.45.14.60") && !Vars.DevEnv)
				throw new Exception("");
			
			Phone p = Phone.GetFromMac(ContainerPage.Url[0].Raw);

			string menu = ContainerPage.Url[1].Raw;
			string xml = "";

			if (menu.Equals(""))
			{
				#region Default
				xml = @"
<?xml version=""1.0"" encoding=""UTF-8""?>
<SnomIPPhoneMenu>
<Title>Menu</Title>
<MenuItem>
<Name>Personal sales</Name>
<URL>http://" + Vars.DomainName + @"/popup/phoneidlescreen/" + p.Mac + @"/personal</URL>
</MenuItem>
<MenuItem>
<Name>Team sales</Name>
<URL>http://" + Vars.DomainName + @"/popup/phoneidlescreen/" + p.Mac + @"/team</URL>
</MenuItem>
</SnomIPPhoneMenu>
";
				#endregion
			}
			else if (menu.Equals("personal"))
			{
					#region Personal
					string salesToday = "0";
					string salesYesterday = "0";
					string salesThisMonth = "0";
					string salesLastMonth = "0";
					try
					{
						Query q = new Query();
						q.ExtraSelectElements.Add("sum", "sum([Invoice].[SalesUsrAmount])");
						q.QueryCondition = new And(
							new Q(Bobs.Invoice.Columns.SalesUsrK, p.UsrK),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Today),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.LessThan, DateTime.Today.AddDays(1))
						);
						q.Columns = new ColumnSet();
						InvoiceSet ins = new InvoiceSet(q);
						salesToday = ((double)ins[0].ExtraSelectElements["sum"]).ToString("#,##0");
					}
					catch { }

					try
					{
						Query q = new Query();
						q.ExtraSelectElements.Add("sum", "sum([Invoice].[SalesUsrAmount])");
						q.QueryCondition = new And(
							new Q(Bobs.Invoice.Columns.SalesUsrK, p.UsrK),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Today.AddDays(-1)),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.LessThan, DateTime.Today)
						);
						q.Columns = new ColumnSet();
						InvoiceSet ins = new InvoiceSet(q);
						salesYesterday = ((double)ins[0].ExtraSelectElements["sum"]).ToString("#,##0");
					}
					catch { }

					try
					{
						Query q = new Query();
						q.ExtraSelectElements.Add("sum", "sum([Invoice].[SalesUsrAmount])");
						q.QueryCondition = new And(
							new Q(Bobs.Invoice.Columns.SalesUsrK, p.UsrK),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.LessThan, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1))
						);
						q.Columns = new ColumnSet();
						InvoiceSet ins = new InvoiceSet(q);
						salesThisMonth = ((double)ins[0].ExtraSelectElements["sum"]).ToString("#,##0");
					}
					catch { }

					try
					{
						Query q = new Query();
						q.ExtraSelectElements.Add("sum", "sum([Invoice].[SalesUsrAmount])");
						q.QueryCondition = new And(
							new Q(Bobs.Invoice.Columns.SalesUsrK, p.UsrK),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1)),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.LessThan, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1))
						);
						q.Columns = new ColumnSet();
						InvoiceSet ins = new InvoiceSet(q);
						salesLastMonth = ((double)ins[0].ExtraSelectElements["sum"]).ToString("#,##0");
					}
					catch { }

					xml = @"
<?xml version=""1.0"" encoding=""UTF-8""?>
<SnomIPPhoneMenu>
<Title>Menu</Title>
<MenuItem>
<Name>Today: " + salesToday + @"</Name>
<URL>http://" + Vars.DomainName + @"/popup/phoneidlescreen/" + p.Mac + @"</URL>
</MenuItem>
<MenuItem>
<Name>Yesterday: " + salesYesterday + @"</Name>
<URL>http://" + Vars.DomainName + @"/popup/phoneidlescreen/" + p.Mac + @"</URL>
</MenuItem>
<MenuItem>
<Name>" + DateTime.Now.ToString("MMM") + @": " + salesThisMonth + @"</Name>
<URL>http://" + Vars.DomainName + @"/popup/phoneidlescreen/" + p.Mac + @"</URL>
</MenuItem>
<MenuItem>
<Name>" + DateTime.Now.AddMonths(-1).ToString("MMM") + @": " + salesLastMonth + @"</Name>
<URL>http://" + Vars.DomainName + @"/popup/phoneidlescreen/" + p.Mac + @"</URL>
</MenuItem>
</SnomIPPhoneMenu>
";
					#endregion
			}
			else if (menu.Equals("team"))
			{
					#region Team
					string salesToday = "0";
					string salesYesterday = "0";
					string salesThisMonth = "0";
					string salesLastMonth = "0";
					try
					{
						Query q = new Query();
						q.ExtraSelectElements.Add("sum", "sum([Invoice].[SalesUsrAmount])");
						q.QueryCondition = new And(
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Today),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.LessThan, DateTime.Today.AddDays(1))
						);
						q.Columns = new ColumnSet();
						InvoiceSet ins = new InvoiceSet(q);
						salesToday = ((double)ins[0].ExtraSelectElements["sum"]).ToString("#,##0");
					}
					catch { }

					try
					{
						Query q = new Query();
						q.ExtraSelectElements.Add("sum", "sum([Invoice].[SalesUsrAmount])");
						q.QueryCondition = new And(
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Today.AddDays(-1)),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.LessThan, DateTime.Today)
						);
						q.Columns = new ColumnSet();
						InvoiceSet ins = new InvoiceSet(q);
						salesYesterday = ((double)ins[0].ExtraSelectElements["sum"]).ToString("#,##0");
					}
					catch { }

					try
					{
						Query q = new Query();
						q.ExtraSelectElements.Add("sum", "sum([Invoice].[SalesUsrAmount])");
						q.QueryCondition = new And(
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.LessThan, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1))
						);
						q.Columns = new ColumnSet();
						InvoiceSet ins = new InvoiceSet(q);
						salesThisMonth = ((double)ins[0].ExtraSelectElements["sum"]).ToString("#,##0");
					}
					catch { }

					try
					{
						Query q = new Query();
						q.ExtraSelectElements.Add("sum", "sum([Invoice].[SalesUsrAmount])");
						q.QueryCondition = new And(
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1)),
							new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.LessThan, new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1))
						);
						q.Columns = new ColumnSet();
						InvoiceSet ins = new InvoiceSet(q);
						salesLastMonth = ((double)ins[0].ExtraSelectElements["sum"]).ToString("#,##0");
					}
					catch { }

					xml = @"
<?xml version=""1.0"" encoding=""UTF-8""?>
<SnomIPPhoneMenu>
<Title>Menu</Title>
<MenuItem>
<Name>Today: " + salesToday + @"</Name>
<URL>http://" + Vars.DomainName + @"/popup/phoneidlescreen/" + p.Mac + @"</URL>
</MenuItem>
<MenuItem>
<Name>Yesterday: " + salesYesterday + @"</Name>
<URL>http://" + Vars.DomainName + @"/popup/phoneidlescreen/" + p.Mac + @"</URL>
</MenuItem>
<MenuItem>
<Name>" + DateTime.Now.ToString("MMM") + @": " + salesThisMonth + @"</Name>
<URL>http://" + Vars.DomainName + @"/popup/phoneidlescreen/" + p.Mac + @"</URL>
</MenuItem>
<MenuItem>
<Name>" + DateTime.Now.AddMonths(-1).ToString("MMM") + @": " + salesLastMonth + @"</Name>
<URL>http://" + Vars.DomainName + @"/popup/phoneidlescreen/" + p.Mac + @"</URL>
</MenuItem>
</SnomIPPhoneMenu>
";
#endregion

			}
			
			Response.ContentType = "text/xml";
			Response.Write(xml);
			Response.End();
		}
	}
}
