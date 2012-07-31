using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace Midday
{
	class Midday
	{
		static void Main(string[] args)
		{
			

			DateTime dt = DateTime.Now;
			

			try
			{
				Console.WriteLine("Picking mixmagvote entries...");
				MixmagEntry.SendUpdateEmails();
				Console.WriteLine("Finished picking mixmagvote entries...");

			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception picking winners</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";
				admin.Subject = "Exception picking winners";
				admin.To = "d.brophy@dontstayin.com";
				admin.Send();
			}

			if (!Vars.DevEnv)
			{

				try
				{
					Console.WriteLine("Picking competitions...");
					Comp.PickAllWinners();
					Console.WriteLine("Finished picking competitions...");

				}
				catch (Exception ex)
				{
					Mailer admin = new Mailer();
					admin.TemplateType = Mailer.TemplateTypes.AdminNote;
					admin.Body = "<p>Exception picking winners</p>";
					admin.Body += "<p>" + ex.ToString() + "</p>";
					admin.Subject = "Exception picking winners";
					admin.To = "d.brophy@dontstayin.com";
					admin.Send();
				}
				double PickAllWinnersTime = (DateTime.Now - dt).TotalMinutes;
				dt = DateTime.Now;

				try
				{
					Console.WriteLine("Sending promoter competition reminders...");
					Comp.SendPromoterReminders();
					Console.WriteLine("Finished sending promoter competition reminders...");
				}
				catch (Exception ex)
				{
					Mailer admin = new Mailer();
					admin.TemplateType = Mailer.TemplateTypes.AdminNote;
					admin.Body = "<p>Exception sending promoter conpetition remintders</p>";
					admin.Body += "<p>" + ex.ToString() + "</p>";
					admin.Subject = "Exception sending promoter conpetition remintders";
					admin.To = "d.brophy@dontstayin.com";
					admin.Send();
				}
				double SendPromoterRemindersTime = (DateTime.Now - dt).TotalMinutes;
				dt = DateTime.Now;

				try
				{
					Console.WriteLine("Sending promoter guestlist reminders...");
					Event.SendGuestlistReminders();
					Console.WriteLine("Finished sending promoter guestlist reminders...");
				}
				catch (Exception ex)
				{
					Mailer admin = new Mailer();
					admin.TemplateType = Mailer.TemplateTypes.AdminNote;
					admin.Body = "<p>Exception sending guestlist reminders</p>";
					admin.Body += "<p>" + ex.ToString() + "</p>";
					admin.Subject = "Exception sending guestlist reminders";
					admin.To = "d.brophy@dontstayin.com";
					admin.Send();
				}

				double SendGuestlistRemindersTime = (DateTime.Now - dt).TotalMinutes;
			
				dt = DateTime.Now;
			

				try
				{
					Console.WriteLine("Sending after event user reminders...");
					Event.SendAfterEventReminders();
					Console.WriteLine("Finished sending after event user reminders...");
				}
				catch (Exception ex)
				{
					Mailer admin = new Mailer();
					admin.TemplateType = Mailer.TemplateTypes.AdminNote;
					admin.Body = "<p>Exception sending after event user reminders</p>";
					admin.Body += "<p>" + ex.ToString() + "</p>";
					admin.Subject = "Exception sending after event user reminders";
					admin.To = "d.brophy@dontstayin.com";
					admin.Send();
				}

				double SendAfterEventRemindersTime = (DateTime.Now - dt).TotalMinutes;
				dt = DateTime.Now;

			}
//            Mailer finished = new Mailer();
//            finished.TemplateType = Mailer.TemplateTypes.AdminNote;
//            finished.Subject = "Finished midday task";
//            finished.Body = "<p>Finished midday task. Times in minutes:</p>";
//            finished.Body += @"<p>
//" + SendAfterEventRemindersTime.ToString("0.00") + @" - SendAfterEventRemindersTime</p>";
//            finished.To = "d.brophy@dontstayin.com";
//            finished.Send();

			Console.WriteLine("Ended!!!");
			System.Environment.Exit(0);

		}
	}
}
