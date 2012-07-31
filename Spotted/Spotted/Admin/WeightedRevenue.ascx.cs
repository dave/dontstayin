using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Admin
{
	public partial class WeightedRevenue : AdminUserControl
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

			Query q = new Query();
			q.ExtraSelectElements.Add("Sum", "SUM([Invoice].[Price])");
			q.ExtraSelectElements.Add("Day", "DATENAME(DW,[Invoice].[CreatedDateTime])");
			q.QueryCondition = new And(
								   new Q(Invoice.Columns.Type, Invoice.Types.Invoice), 
								   new Q(Invoice.Columns.Price, QueryOperator.LessThan, 1000.0));
			q.GroupBy = new GroupBy("DATENAME(DW,[Invoice].[CreatedDateTime])");
			q.Columns = new ColumnSet();
			InvoiceSet ins = new InvoiceSet(q);
			Dictionary<DayOfWeek, decimal> weight = new Dictionary<DayOfWeek, decimal>();
			decimal total = 0.0m;
			foreach (Invoice i in ins)
			{
				total += (decimal)i.ExtraSelectElements["Sum"];
			}
			foreach (Invoice i in ins)
			{
				decimal fraction = (decimal)i.ExtraSelectElements["Sum"] / total;
				switch ((string)i.ExtraSelectElements["Day"])
				{
					case "Monday": weight[DayOfWeek.Monday] = fraction; break;
					case "Tuesday": weight[DayOfWeek.Tuesday] = fraction; break;
					case "Wednesday": weight[DayOfWeek.Wednesday] = fraction; break;
					case "Thursday": weight[DayOfWeek.Thursday] = fraction; break;
					case "Friday": weight[DayOfWeek.Friday] = fraction; break;
					case "Saturday": weight[DayOfWeek.Saturday] = fraction; break;
					case "Sunday": weight[DayOfWeek.Sunday] = fraction; break;
					default: break;
				}
			}
			Stats.Controls.Add(new LiteralControl("<table><tr><td>Month</td><td>Year</td><td>Weight</td><td>Actual revenue</td><td>Weighted revenue</td></tr>"));
			for (DateTime dtMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-12); dtMonth <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); dtMonth = dtMonth.AddMonths(1))
			{
				try
				{
					decimal monthWeight = 0.0m;
					for (DateTime dtDay = dtMonth; dtDay < dtMonth.AddMonths(1) && dtDay < DateTime.Today; dtDay = dtDay.AddDays(1))
					{
						monthWeight += weight[dtDay.DayOfWeek];
					}
					Query qMonth = new Query();
					qMonth.ExtraSelectElements.Add("Count", "SUM([Invoice].[Price])");
					qMonth.QueryCondition = new And(
						new Q(Invoice.Columns.BuyerType, BuyerType),
						new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, dtMonth),
						new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, dtMonth.AddMonths(1)),
						new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, DateTime.Today));
					qMonth.Columns = new ColumnSet();
					InvoiceSet isMonth = new InvoiceSet(qMonth);
					decimal actualRevenue = (decimal)isMonth[0].ExtraSelectElements["Count"];
					decimal revenuePerWeek = actualRevenue / monthWeight;
					decimal revenuePerMonth = revenuePerWeek * 4.345238095m;

					Stats.Controls.Add(new LiteralControl("<tr><td>" + dtMonth.ToString("MMM") + "</td><td>" + dtMonth.Year + "</td><td>" + monthWeight.ToString("0.00") + "</td><td>" + actualRevenue.ToString("£#,##0") + "</td><td>" + revenuePerMonth.ToString("£#,##0") + "</td></tr>"));
					//				Stats.Controls.Add(new LiteralControl( + " " +  + " is " +  + " weeks. " +  + " pages per week.<br>"));
				}
				catch { }
			}
			Stats.Controls.Add(new LiteralControl("</table>"));
		}
	}
}