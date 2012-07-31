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
				Stats.Controls.Add(new LiteralControl("<td>£ " + Total.ToString("#,##0") + "</td>"));

				Hashtable revenueBins = new Hashtable();

				{
					Query q = new Query();
					q.TableElement = new Join(InvoiceItem.Columns.InvoiceK, Invoice.Columns.K);
					q.QueryCondition = new And(
						new Q(Invoice.Columns.BuyerType, BuyerType),
						new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, dtMonth),
						new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, dtMonth.AddMonths(1)));
					InvoiceItemSet iis = new InvoiceItemSet(q);

					foreach (InvoiceItem ii in iis)
					{
						int totalDays = ((TimeSpan)(ii.RevenueEndDate - ii.RevenueStartDate)).Days;
						if (totalDays > 0)
						{
							for (DateTime month = new DateTime(ii.RevenueStartDate.Year, ii.RevenueStartDate.Month, 1); month < ii.RevenueEndDate; month = month.AddMonths(1))
							{
								DateTime start = ii.RevenueStartDate > month ? ii.RevenueStartDate : month;
								DateTime end = ii.RevenueEndDate < month.AddMonths(1) ? ii.RevenueEndDate : month.AddMonths(1);
								int thisBinDays = ((TimeSpan)(end - start)).Days;
								decimal revenueInThisBin = ii.Price * (decimal)thisBinDays / (decimal)totalDays;
								AddToBin(revenueBins, month, dtMonth, revenueInThisBin);
							}
						}
						else
						{
							AddToBin(revenueBins, ii.RevenueStartDate, dtMonth, ii.Price);
						}
					}
				}

				for (int bin = -6; bin <= 12; bin++)
				{
					try
					{
						//Stats.Controls.Add(new LiteralControl("<td>" + ((double)revenueBins[bin]).ToString() + "</td>"));
						Stats.Controls.Add(new LiteralControl("<td>" + (bin == 0 ? "<b>" : "") + Math.Round(100 * (decimal)revenueBins[bin] / Total).ToString("0") + "%" + (bin == 0 ? "</b>" : "") + "</td>"));
					}
					catch
					{
						Stats.Controls.Add(new LiteralControl("<td>&nbsp;&nbsp;</td>"));
					}
				}

				Stats.Controls.Add(new LiteralControl("</tr>"));

			}
			Stats.Controls.Add(new LiteralControl("</table>"));
		}
		void AddToBin(Hashtable revenueBins, DateTime binMonth, DateTime thisMonth, decimal revenue)
		{
			int binMonths = (binMonth.Year * 12) + binMonth.Month;
			int thisMonths = (thisMonth.Year * 12) + thisMonth.Month;
			int bin = binMonths - thisMonths;

			if (revenueBins[bin] == null)
				revenueBins[bin] = revenue;
			else
				revenueBins[bin] = (decimal)revenueBins[bin] + revenue;
		}
	}
}
