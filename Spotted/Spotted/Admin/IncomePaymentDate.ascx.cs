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
	public partial class IncomePaymentDate : Spotted.AdminUserControl
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
			Stats.Controls.Add(new LiteralControl("<table cellpadding=5 cellspacing=0 border=1 xstyle=\"border:1px solid #000000;\"><tr><td>Date</td><td>Invoiced</td><td>+0</td><td>+1</td><td>+2</td><td>+3</td><td>+4</td><td>Unpaid</td></tr>"));
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
						Total = (decimal)ins[0].ExtraSelectElements["sum"];
				}

				Stats.Controls.Add(new LiteralControl("<td>£ " + Total.ToString("#,##0") + "</td>"));

				for (int offset = 0; offset <= 4; offset++)
				{
					if (dtMonth.AddMonths(offset) > DateTime.Now)
					{
						Stats.Controls.Add(new LiteralControl("<td>&nbsp;</td>"));
					}
					else
					{
						try
						{
							Query q = new Query();
							q.Columns = new ColumnSet();
							q.ExtraSelectElements.Add("sum", "SUM([Invoice].[Price])");
							q.QueryCondition = new And(
								new Q(Invoice.Columns.BuyerType, BuyerType),
								new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, dtMonth),
								new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, dtMonth.AddMonths(1)),
								new Q(Invoice.Columns.Price, QueryOperator.GreaterThan, 0),
								new Q(Invoice.Columns.Paid, true),
								new Q(Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, dtMonth.AddMonths(offset)),
								new Q(Invoice.Columns.PaidDateTime, QueryOperator.LessThan, dtMonth.AddMonths(offset + 1))
							);
							InvoiceSet ins = new InvoiceSet(q);
							decimal subTotal = (decimal)ins[0].ExtraSelectElements["sum"];
							decimal subTotalCredits = 0.0m;

							//credits are always paid when they are created
							if (offset == 0)
							{
								Query qCredits = new Query();
								qCredits.Columns = new ColumnSet();
								qCredits.ExtraSelectElements.Add("sum", "SUM([Invoice].[Price])");
								qCredits.QueryCondition = new And(
									new Q(Invoice.Columns.BuyerType, BuyerType),
									new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, dtMonth),
									new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, dtMonth.AddMonths(1)),
									new Q(Invoice.Columns.Price, QueryOperator.LessThan, 0)
								);
								InvoiceSet insCredits = new InvoiceSet(qCredits);
								try
								{
									subTotalCredits = (decimal)insCredits[0].ExtraSelectElements["sum"];
								}
								catch { }
							}

							subTotal += subTotalCredits;

							decimal fraction = subTotal / Total;
							if (dtMonth.AddMonths(offset + 1) > DateTime.Now)
								Stats.Controls.Add(new LiteralControl("<td><span style=color:#999999;>" + Math.Round(fraction * 100).ToString("0") + "%</span></td>"));
							else
								Stats.Controls.Add(new LiteralControl("<td>" + Math.Round(fraction * 100).ToString("0") + "%</td>"));


						}
						catch
						{
							if (dtMonth.AddMonths(offset + 1) > DateTime.Now)
								Stats.Controls.Add(new LiteralControl("<td><span style=color:#999999;>0%</span></td>"));
							else
								Stats.Controls.Add(new LiteralControl("<td>0%</td>"));
						}
					}
				}

				try
				{


					Query q = new Query();
					q.Columns = new ColumnSet();
					q.ExtraSelectElements.Add("sum", "SUM([Invoice].[Price])");
					q.QueryCondition = new And(
						new Q(Invoice.Columns.BuyerType, BuyerType),
						new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, dtMonth),
						new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, dtMonth.AddMonths(1)),
						new Q(Invoice.Columns.Price, QueryOperator.GreaterThan, 0),
						new Q(Invoice.Columns.Paid, false)
					);
					InvoiceSet ins = new InvoiceSet(q);
					decimal subTotal = (decimal)ins[0].ExtraSelectElements["sum"];
					decimal fraction = subTotal / Total;
					Stats.Controls.Add(new LiteralControl("<td>" + Math.Round(fraction * 100).ToString("0") + "%</td>"));


				}
				catch
				{
					Stats.Controls.Add(new LiteralControl("<td>0%</td>"));
				}

				Stats.Controls.Add(new LiteralControl("</tr>"));

			}
			Stats.Controls.Add(new LiteralControl("</table>"));
		}
	}
}
