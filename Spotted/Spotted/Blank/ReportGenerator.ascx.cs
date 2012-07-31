using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Blank
{
	public partial class ReportGenerator : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string type = "";
			int k = 0;
			int pk = 0;

			if (ContainerPage.Url["type"].Exists)
				type = ContainerPage.Url["type"].Value;

			if (ContainerPage.Url["K"].Exists && ContainerPage.Url["K"].IsInt)
				k = Convert.ToInt32(ContainerPage.Url["K"].Value);

			if (ContainerPage.Url["PK"].Exists && ContainerPage.Url["PK"].IsInt)
				pk = Convert.ToInt32(ContainerPage.Url["PK"].Value);

			//Promoter CurrentPromoter = new Promoter(pk);

			Usr.KickUserIfNotLoggedIn();

			//if (!Usr.Current.IsPromoter && !Usr.Current.IsAdmin)
			//{
			//    throw new Exception("You must be a promoter to view this page");
			//}
			//if (CurrentPromoter != null)
			//{
			//    if (!Usr.Current.IsPromoterK(CurrentPromoter.K) && !Usr.Current.IsAdmin)
			//        throw new Exception("You can't view these details.");
			//}

			if (type.ToUpper() == "STATEMENT")
			{
				Promoter promoter = new Promoter(pk);

				if (!promoter.IsUsrAllowedAccess(Usr.Current))
					throw new Exception(Vars.CANT_VIEW_DETAILS);

				int month = DateTime.Now.Month;
				int year = DateTime.Now.Year;

				if (ContainerPage.Url["M"].Exists && ContainerPage.Url["M"].IsInt)
					month = Convert.ToInt32(ContainerPage.Url["M"].Value);
				if (ContainerPage.Url["Y"].Exists && ContainerPage.Url["Y"].IsInt)
					year = Convert.ToInt32(ContainerPage.Url["Y"].Value);

				Response.Write(promoter.GenerateMonthlyStatementStringBuilder(month, year, true).ToString());
			}
            else if(type.ToUpper().Equals("TICKETFUNDSINVOICE"))
            {
                TicketPromoterEvent tpe = new TicketPromoterEvent(pk, k);
                if (!tpe.IsUsrAllowedAccess(Usr.Current))
                    throw new Exception(Vars.CANT_VIEW_DETAILS);

                Response.Write(tpe.GenerateReportStringBuilder(true).ToString());
            }
			else
			{
				IBobReport bobReport;
				switch (type.ToUpper())
				{
					case "TRANSFER": bobReport = new Transfer(k); break;
					case "INVOICE": // goto credit
					case "CREDIT": bobReport = new Bobs.Invoice(k); break;
					case "TICKET": bobReport = new Ticket(k); break;
					case "TICKETFORPRINTING": bobReport = new TicketForPrinting(k); break;
					case "INSERTIONORDER": bobReport = new InsertionOrder(k); break;
					default: bobReport = null; break;
				}

				if (bobReport != null)
				{
					if (!bobReport.IsUsrAllowedAccess(Usr.Current))
						throw new Exception(Vars.CANT_VIEW_DETAILS);

					Response.Write(bobReport.GenerateReportStringBuilder(true).ToString());
				}
			}

		}
	}
}
