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
	public partial class IncomeEarningDate : Spotted.AdminUserControl
	{
		protected Invoice.BuyerTypes BuyerType
		{
			get
			{
				try
				{
					return (Invoice.BuyerTypes)(int)ContainerPage.Url["BuyerType"];
				}
				catch
				{
					return Invoice.BuyerTypes.NonAgencyPromoter;
				}
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{

			Stats.Controls.Add(new LiteralControl("<table cellpadding=5 cellspacing=0 border=1 xstyle=\"border:1px solid #000000;\"><tr><td>Date</td><td>Invoiced</td><td>-6</td><td>-5</td><td>-4</td><td>-3</td><td>-2</td><td>-1</td><td>+0</td><td>+1</td><td>+2</td><td>+3</td><td>+4</td><td>+5</td><td>+6</td><td>+7</td><td>+8</td><td>+9</td><td>+10</td><td>+11</td><td>+12</td></tr>"));
			//for (DateTime dtMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); dtMonth <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); dtMonth = dtMonth.AddMonths(1))
			for (DateTime dtMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-12); dtMonth <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); dtMonth = dtMonth.AddMonths(1))
			{
				Stats.Controls.Add(new LiteralControl("<tr><td>" + dtMonth.Year.ToString() + "-" + dtMonth.Month.ToString("00") + "</td>"));

				decimal Total = 0.0m;
				{
					Query q = new Query();
					q.Columns = new ColumnSet();
					q.ExtraSelectElements.Add("sum", "SUM([Invoice].[Price])");
					q.QueryCondition = new And(
						new Q(Invoice.Columns.BuyerType, BuyerType),
						new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, dtMonth),
						new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, dtMonth.AddMonths(1)));
					InvoiceSet ins = new InvoiceSet(q);
					if (ins.Count > 0 && !string.IsNullOrEmpty(ins[0].ExtraSelectElements["sum"].ToString()))
						Total = (decimal) ins[0].ExtraSelectElements["sum"];
				}
