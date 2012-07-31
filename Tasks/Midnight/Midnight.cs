using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace Midnight
{
	class Midnight
	{
		static void Main(string[] args)
		{
			DateTime dt = DateTime.Now;

			Console.WriteLine("Midnight...");

			//try
			//{
			//    if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday || Vars.DevEnv)
			//    {
			//        Console.WriteLine("Emailing promoters listings reminder email...");

			//        Utilities.EmailPromotersListingsReminder();

			//        Console.WriteLine("Finished emailing promoters listings reminder email...");
			//    }

			//}
			//catch (Exception ex)
			//{
			//    Mailer admin = new Mailer();
			//    admin.TemplateType = Mailer.TemplateTypes.AdminNote;
			//    admin.Body = "<p>Exception in emailing promoters listings reminder email</p>";
			//    admin.Body += "<p>" + ex.ToString() + "</p>";
			//    admin.Subject = "Exception in emailing promoters listings reminder email";
			//    admin.To = "neil@dontstayin.com,d.brophy@dontstayin.com";
			//    admin.Send();
			//}
			//double EmailPromotersListingsReminder = (DateTime.Now - dt).TotalMinutes;
			//dt = DateTime.Now;

			if (Vars.DevEnv)
				return;

			try
			{
				// Run outstanding statements first because the GetBalance method called cant track what time a transfer status changed from Pending to Success. So best to run it as close to midnight as possible.  Chance of error occurring is very marginal.

				// if its the 1st of the month, then check to see which promoters need to be sent a monthly statement for previous month
				if (DateTime.Now.Day == 1)
				{
					Console.WriteLine("Emailing statements to promoters and to accounts...");
					// Send statements from previous month
					Utilities.EmailAllPromoterOutstandingStatements(DateTime.Now.AddDays(-1));
					Console.WriteLine("Finished emailing statements to promoters and to accounts...");
				}

			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception in emailing promoter statements</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";
				admin.Subject = "Exception in emailing promoter statements";
				admin.To = "neil@dontstayin.com,d.brophy@dontstayin.com";
				admin.Send();
			}
			double EmailAllPromoterOutstandingStatementsTime = (DateTime.Now - dt).TotalMinutes;
			dt = DateTime.Now;

			try
			{
				Console.WriteLine("Emailing outstanding invoices to promoters and to accounts...");
				Utilities.EmailAllPromoterOutstandingInvoices();
				Console.WriteLine("Finished emailing outstanding invoices to promoters and to accounts...");
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception in emailing outstanding invoices</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";
				admin.Subject = "Exception in emailing outstanding invoices";
				admin.To = "neil@dontstayin.com,d.brophy@dontstayin.com";
				admin.Send();
			}
			double EmailAllPromoterOutstandingInvoicesTime = (DateTime.Now - dt).TotalMinutes;
			dt = DateTime.Now;

			try
			{
				Console.WriteLine("Emailing overdue invoices to promoters and to accounts...");
				Utilities.EmailAllPromoterOverdueInvoices();
				Console.WriteLine("Finished emailing overdue invoices to promoters and to accounts...");
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception in emailing overdue invoices</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";
				admin.Subject = "Exception in emailing overdue invoices";
				admin.To = "neil@dontstayin.com,d.brophy@dontstayin.com";
				admin.Send();
			}
			double EmailAllPromoterOverdueInvoicesTime = (DateTime.Now - dt).TotalMinutes;
			dt = DateTime.Now;


			try
			{
				Console.WriteLine("RecomputeSpottingsMonth...");
				Usr.RecomputeSpottingsMonth();
				Console.WriteLine("Finished RecomputeSpottingsMonth...");
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception RecomputeSpottingsMonth</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";

				admin.Subject = "Exception RecomputeSpottingsMonth";
				admin.To = "d.brophy@dontstayin.com";
				admin.Send();
			}
			double RecomputeSpottingsMonthTime = (DateTime.Now - dt).TotalMinutes;
			dt = DateTime.Now;

			try
			{
				Console.WriteLine("RecomputeSpottingsMonthRank...");
				Usr.RecomputeSpottingsMonthRank();
				Console.WriteLine("Finished RecomputeSpottingsMonthRank...");
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception RecomputeSpottingsMonthRank</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";

				admin.Subject = "Exception RecomputeSpottingsMonthRank";
				admin.To = "d.brophy@dontstayin.com";
				admin.Send();
			}
			double RecomputeSpottingsMonthRankTime = (DateTime.Now - dt).TotalMinutes;
			dt = DateTime.Now;

			try
			{
				Console.WriteLine("RecomputeSpottingsTotal...");
				Usr.RecomputeSpottingsTotal();
				Console.WriteLine("Finished RecomputeSpottingsTotal...");
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception RecomputeSpottingsTotal</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";

				admin.Subject = "Exception RecomputeSpottingsTotal";
				admin.To = "d.brophy@dontstayin.com";
				admin.Send();
			}
			double RecomputeSpottingsTotalTime = (DateTime.Now - dt).TotalMinutes;
			dt = DateTime.Now;

			try
			{
				Console.WriteLine("Recomputing Promoter.FutureEvents...");
				Promoter.RecomputeFutureEvents();
				Console.WriteLine("Finished recomputing Promoter.FutureEvents...");
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception recomputing Promoter.FutureEvents</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";

				admin.Subject = "Exception recomputing Promoter.FutureEvents";
				admin.To = "d.brophy@dontstayin.com";
				admin.Send();
			}

			double RecomputeFutureEventsTime = (DateTime.Now - dt).TotalMinutes;
			dt = DateTime.Now;


			//try
			//{
			//    Console.WriteLine("Starting Banner.GeneratePositionStats()...");
			//    Banner.GeneratePositionStats();
			//    Console.WriteLine("Finished Banner.GeneratePositionStats()...");
			//}
			//catch (Exception ex)
			//{
			//    Mailer admin = new Mailer();
			//    admin.TemplateType = Mailer.TemplateTypes.AdminNote;
			//    admin.Body = "<p>Exception during Banner.GeneratePositionStats()</p>";
			//    admin.Body += "<p>" + ex.ToString() + "</p>";

			//    admin.Subject = "Exception during Banner.GeneratePositionStats()";
			//    admin.To = "d.brophy@dontstayin.com";
			//    admin.Send();
			//}

			try
			{
                Console.WriteLine("Starting TicketPromoterEvent.EvaluateRecentTicketEventsAndReleaseFunds()...");
                TicketPromoterEvent.EvaluateRecentTicketEventsAndReleaseFunds();
                Console.WriteLine("Finished TicketPromoterEvent.EvaluateRecentTicketEventsAndReleaseFunds()...");
			}
			catch (Exception ex)
			{
                Utilities.AdminEmailAlert("<p>Exception in Midnight task: TicketPromoterEvent.EvaluateRecentTicketEventsAndReleaseFunds()</p>", 
                                          "Exception in Midnight task: TicketPromoterEvent.EvaluateRecentTicketEventsAndReleaseFunds()", ex);
			}

			double EvaluateRecentTicketEventsAndReleaseFundsTime = (DateTime.Now - dt).TotalMinutes;
			dt = DateTime.Now;
            
            
            try
			{
				Console.WriteLine("Starting Banner.RefundFinishedBanners()...");
				Banner.RefundFinishedBanners();
				Console.WriteLine("Finished Banner.RefundFinishedBanners()...");
			}
			catch (Exception ex)
			{
				Utilities.AdminEmailAlert("<p>Exception in Midnight task: Banner.RefundFinishedBanners()</p>",
										  "Exception in Midnight task: Banner.RefundFinishedBanners()", ex);
			}

			double RefundFinishedBannersTime = (DateTime.Now - dt).TotalMinutes;
			dt = DateTime.Now;


//            Mailer admin1 = new Mailer();
//            admin1.TemplateType = Mailer.TemplateTypes.AdminNote;
//            admin1.Body = "<p>Midnight task complete. Times in minutes:</p>";
//            admin1.Body += @"<p>
//" + EmailAllPromoterOutstandingStatementsTime.ToString("0.00") + @" - EmailAllPromoterOutstandingStatementsTime<br>
//" + EmailAllPromoterOutstandingInvoicesTime.ToString("0.00") + @" - EmailAllPromoterOutstandingInvoicesTime<br>
//" + EmailAllPromoterOverdueInvoicesTime.ToString("0.00") + @" - EmailAllPromoterOverdueInvoicesTime<br>
//" + RecomputeSpottingsMonthTime.ToString("0.00") + @" - RecomputeSpottingsMonthTime<br>
//" + RecomputeSpottingsMonthRankTime.ToString("0.00") + @" - RecomputeSpottingsMonthRankTime<br>
//" + RecomputeSpottingsTotalTime.ToString("0.00") + @" - RecomputeSpottingsTotalTime<br>
//" + RecomputeFutureEventsTime.ToString("0.00") + @" - RecomputeFutureEventsTime<br>
//" + EvaluateRecentTicketEventsAndReleaseFundsTime.ToString("0.00") + @" - EvaluateRecentTicketEventsAndReleaseFundsTime<br>
//" + RefundFinishedBannersTime.ToString("0.00") + @" - RefundFinishedBannersTime</p>";
//            admin1.Subject = "Midnight task complete";
//            admin1.To = "dave@dontstayin.com";
//            admin1.Send();


			Console.WriteLine("Ended!!!");
			System.Environment.Exit(0);
		}		
	}
}
