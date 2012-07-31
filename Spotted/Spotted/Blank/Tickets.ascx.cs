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
	public partial class Tickets : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must log in to view this page...");
			TicketRun tr = new TicketRun(ContainerPage.Url[0]);
			if (!Usr.Current.IsPromoterK(tr.PromoterK) && !Usr.Current.IsAdmin)
			{
				throw new Exception("You must be a member of the " + tr.Promoter.Name + " promoter account to view this page.");
			}
			H1.InnerText = tr.Description;
			Query q = new Query();
			q.QueryCondition=new Q(Ticket.Columns.TicketRunK, (int)ContainerPage.Url[0]);
			q.OrderBy = new OrderBy(Ticket.Columns.K);
			TicketSet ts = new TicketSet(q);
			foreach (Ticket t in ts)
			{
				HtmlTableRow tRow = new HtmlTableRow();

				

				HtmlTableCell refCell = new HtmlTableCell();
				refCell.InnerText = t.InvoiceItemK.ToString();
				tRow.Cells.Add(refCell);

				HtmlTableCell dateCell = new HtmlTableCell();
				dateCell.InnerText = t.BuyDateTime.ToString();
				tRow.Cells.Add(dateCell);

				HtmlTableCell nameCell = new HtmlTableCell();
				nameCell.InnerText = t.FirstName + " " + t.LastName;
				tRow.Cells.Add(nameCell);

				HtmlTableCell mobileCell = new HtmlTableCell();
				mobileCell.InnerText = "0"+t.MobileNumber;
				tRow.Cells.Add(mobileCell);
				
				HtmlTableCell postcodeCell = new HtmlTableCell();
				postcodeCell.InnerText = t.AddressPostcode;
				tRow.Cells.Add(postcodeCell);

				HtmlTableCell qtyCell = new HtmlTableCell();
				qtyCell.InnerText = t.Quantity.ToString();
				tRow.Cells.Add(qtyCell);

			//	HtmlTableCell cardCell = new HtmlTableCell();
			//	cardCell.InnerText = t.CardDigits;
			//	tRow.Cells.Add(cardCell);

				Tab.Rows.Add(tRow);


			}
		}
	}
}
