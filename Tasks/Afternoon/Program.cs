using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace Afternoon
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine("Sending ticket money in resserve...");
				Utilities.EmailAccountsTicketFundsReserveAmount();
				Console.WriteLine("Finished sending ticket money in resserve...");
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception sending ticket money in resserve</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";
				admin.Subject = "Exception sending ticket money in resserve";
				admin.To = Vars.EMAIL_ADDRESS_TIMI;
				admin.Send();
			}
			try
			{
				Console.WriteLine("Sending gallery update emails...");
				Gallery.DailySendNewGalleryEmails();
				Console.WriteLine("Finished sending gallery update emails...");
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception sending gallery emails</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";
				admin.Subject = "Exception sending gallery emails";
				admin.To = "d.brophy@dontstayin.com";
				admin.Send();
			}

			//Mailer admin1 = new Mailer();
			//admin1.TemplateType = Mailer.TemplateTypes.AdminNote;
			//admin1.Body = "<p>Finished sending afternoon email</p>";
			//admin1.Subject = "Finished sending afternoon email";
			//admin1.To = "d.brophy@dontstayin.com";
			//admin1.Send();

			Console.WriteLine("Ended!!!");
			System.Environment.Exit(0);
		}
	}
}
