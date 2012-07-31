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
using System.Text;

namespace Spotted.Blank
{
	public partial class Accounts : DsiUserControl
	{
		protected Repeater SpotterLetterRepeater;
		protected CheckBox MarkAllSent;
		protected Label SuccessLabel, FailLabel;
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotAdmin("Must be admin");

			string[] dateAry = Request.QueryString["date"].Split('/');

			DateTime d1, d2;
			if (dateAry.Length == 1)
			{
				d1 = new DateTime(int.Parse(dateAry[0]), 1, 1);
				d2 = d1.AddYears(1);
			}
			else if (dateAry.Length == 2)
			{
				d1 = new DateTime(int.Parse(dateAry[0]), int.Parse(dateAry[1]), 1);
				d2 = d1.AddMonths(1);
			}
			else
			{
				d1 = new DateTime(int.Parse(dateAry[0]), int.Parse(dateAry[1]), int.Parse(dateAry[2]));
				d2 = d1.AddDays(1);
			}

			StringBuilder s = new StringBuilder();
			for (DateTime d = d1; d < d2; d = d.AddDays(1))
			{
				Query q = new Query();
				q.Columns = new ColumnSet(
					InvoiceItem.Columns.K,
					InvoiceItem.Columns.InvoiceK,
					InvoiceItem.Columns.KeyData,
					InvoiceItem.Columns.Price,
					InvoiceItem.Columns.Type,
					InvoiceItem.Columns.Vat,
					Banner.Columns.Position);
				q.TableElement = new Join(
					new Join(InvoiceItem.Columns.KeyData, Banner.Columns.K, QueryJoinType.Left, new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.Banner)),
					Bobs.Invoice.Columns.K,
					InvoiceItem.Columns.InvoiceK);
				q.QueryCondition = new And(
					new Q(Bobs.Invoice.Columns.Paid, true),
					new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, d),
					new Q(Bobs.Invoice.Columns.PaidDateTime, QueryOperator.LessThan, d.AddDays(1)));
				InvoiceItemSet iis = new InvoiceItemSet(q);

				Hashtable total = new Hashtable();
				Hashtable vat = new Hashtable();
				Hashtable taxCodes = new Hashtable();

				foreach (InvoiceItem ii in iis)
				{
					if (taxCodes[ii.NominalCode] == null)
						taxCodes[ii.NominalCode] = ii.TaxCode;

					if (total[ii.NominalCode] == null)
					{
						total[ii.NominalCode] = ii.Price;
						vat[ii.NominalCode] = ii.Vat;
					}
					else
					{
						total[ii.NominalCode] = (decimal)total[ii.NominalCode] + ii.Price;
						vat[ii.NominalCode] = (decimal)vat[ii.NominalCode] + ii.Vat;
					}
				}


				foreach (object ob in total.Keys)
				{
					int nominal = (int)ob;
					decimal vatTot = (decimal)vat[ob];
					decimal totalTot = (decimal)total[ob];
					int taxCode = (int)taxCodes[ob];

					s.Append("BR");                      // Bank Receipt
					s.Append(",");
					s.Append("1220");                    // Cardnet account
					s.Append(",");
					s.Append(nominal.ToString());        // Nominal code
					s.Append(",");
					s.Append("0");                       // Department number
					s.Append(",");
					s.Append(d.ToString("dd/MM/yyyy"));  // Date
					s.Append(",");
					s.Append("ref");                     // Transaction reference
					s.Append(",");
					s.Append("Web sales summary - ");    // Transaction details
					s.Append(d.ToString("yyyy-MM-dd"));
					s.Append(" - n/c ");
					s.Append(nominal.ToString());
					s.Append(",");
					s.Append(totalTot.ToString("0.00")); // Total
					s.Append(",");
					s.Append("T");
					s.Append(taxCode.ToString());        // Tax code
					s.Append(",");
					s.Append(vatTot.ToString("0.00"));   // Vat
					s.Append("\n");
				}
			}


			Response.Clear();
			Response.Write(s.ToString());
			Response.Flush();
			Response.End();


			//SpotterLetterRepeater.ItemTemplate=this.LoadTemplate("~/ucAdm/ucSpotterLetter.ascx");
			//SpotterLetterRepeater.DataSource=us;
			//SpotterLetterRepeater.DataBind();
		}
		
	}
}
