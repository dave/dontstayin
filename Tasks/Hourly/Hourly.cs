using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace Hourly
{
	class Hourly
	{
		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine("Email updates to all ticket promoter events with ticket runs that ended in the last hour...");
				Utilities.EmailAllEndedTicketRuns();
				Console.WriteLine("Finished emailing ticket promoter events...");
			}
			catch (Exception ex)
			{
				Utilities.AdminEmailAlert("Exception emailing ticket promoter events", "Exception emailing ticket promoter events", ex);
			}


			try
			{
				Console.WriteLine("Update TicketsAvailable flag on all events...");
				Ticket.UpdateTicketsStatusOfEvents();
				Console.WriteLine("Done update TicketsAvailable flag on all events...");
			}
			catch (Exception ex)
			{
				Utilities.AdminEmailAlert("Exception updating TicketsAvailable flag on all events", "Exception updating TicketsAvailable flag on all events", ex);
			}

			try
			{
				Console.WriteLine("Update TicketHeat float on all events...");
				Ticket.UpdateTicketHeatOfEvents();
				Console.WriteLine("Done update TicketHeat float on all events...");
			}
			catch (Exception ex)
			{
				Utilities.AdminEmailAlert("Exception updating TicketHeat float on all events", "Exception updating TicketHeat float on all events", ex);
			}

			//This code only needs to be run if the bannerserver is NOT logging hits to the database
			//try
			//{
			//    Console.WriteLine("Logging banner hits to database...");
			//    Bobs.BannerServer.Server server = new Bobs.BannerServer.Server();
			//    server.CopyCachedDataToDatabase();
			//    Console.WriteLine("Finished logging banner hits to database");
			//}
			//catch (Exception ex)
			//{
			//    Utilities.AdminEmailAlert("Exception logging banner hits to database", ex.ToString());
			//}

			Console.WriteLine("Ended!!!");
			System.Environment.Exit(0);
		}
	}
}
