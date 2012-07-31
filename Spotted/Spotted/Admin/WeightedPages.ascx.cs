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
	public partial class WeightedPages : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			gen(StatsInit, Log.Items.DsiPages);
			gen(StatsRender, Log.Items.DsiPageRender);
			gen(StatsRenderNoCrawlers, Log.Items.DsiPageRenderNoCrawlers);
		}

		void gen(PlaceHolder Stats, Log.Items item)
		{
			Query q = new Query();
			q.ExtraSelectElements.Add("Count", "SUM([Log].[Count])");
			q.ExtraSelectElements.Add("Day", "DATENAME(DW,[Log].[Date])");
			q.QueryCondition = new Q(Log.Columns.Item, item);
			q.GroupBy = new GroupBy("DATENAME(DW,[Log].[Date])");
			q.Columns = new ColumnSet();
			LogSet ls = new LogSet(q);
			Dictionary<DayOfWeek, double> weight = new Dictionary<DayOfWeek, double>();
			int total = 0;
			foreach (Log l in ls)
			{
				total += (int)l.ExtraSelectElements["Count"];
			}
			foreach (Log l in ls)
			{
				double fraction = (double)(int)l.ExtraSelectElements["Count"] / (double)total;
				switch ((string)l.ExtraSelectElements["Day"])
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
			Stats.Controls.Add(new LiteralControl("<table><tr><td>Month</td><td>Year</td><td>Weight</td><td>Actual pages</td><td>Weighted pages</td></tr>"));
			for (DateTime dtMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-12); dtMonth <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); dtMonth = dtMonth.AddMonths(1))
			{
				try
				{
					double monthWeight = 0.0;
					for (DateTime dtDay = dtMonth; dtDay < dtMonth.AddMonths(1) && dtDay < DateTime.Today; dtDay = dtDay.AddDays(1))
					{
						monthWeight += weight[dtDay.DayOfWeek];
					}
					Query qMonth = new Query();
					qMonth.ExtraSelectElements.Add("Count", "SUM([Log].[Count])");
					qMonth.QueryCondition = new And(
						new Q(Log.Columns.Item, item),
						new Q(Log.Columns.Date, QueryOperator.GreaterThanOrEqualTo, dtMonth),
						new Q(Log.Columns.Date, QueryOperator.LessThan, dtMonth.AddMonths(1)),
						new Q(Log.Columns.Date, QueryOperator.LessThan, DateTime.Today));
					qMonth.Columns = new ColumnSet();
					LogSet lsMonth = new LogSet(qMonth);
					int actualPages = (int)lsMonth[0].ExtraSelectElements["Count"];
					double pagesPerWeek = (double)actualPages / monthWeight;
					double pagesPerMonth = pagesPerWeek * 4.345238095;

					Stats.Controls.Add(new LiteralControl("<tr><td>" + dtMonth.ToString("MMM") + "</td><td>" + dtMonth.Year + "</td><td>" + monthWeight.ToString("0.00") + "</td><td>" + actualPages.ToString("0") + "</td><td>" + pagesPerMonth.ToString("0") + "</td></tr>"));
					//				Stats.Controls.Add(new LiteralControl( + " " +  + " is " +  + " weeks. " +  + " pages per week.<br>"));
				}
				catch { }
			}
			Stats.Controls.Add(new LiteralControl("</table>"));
		}
	}
}
