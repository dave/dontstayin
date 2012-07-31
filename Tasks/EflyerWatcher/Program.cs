using System;
using System.Diagnostics;
using Bobs;

namespace EflyerWatcher
{
	class Program
	{
		private const int maxNumberOfConcurrentEflyers = 2;
		static void Main(string[] args)
		{
			Console.WriteLine("EflyerWatcher v3...");

			if (args.Length != 1)
			{
				Console.Error.WriteLine("USAGE: EflyerWatcher.exe <path to EflyerSender>");
				Environment.Exit(1);
			}


			int sending = GetFlyersSendingCount();
			if (sending >= maxNumberOfConcurrentEflyers)
			{
				Console.WriteLine("Sending " + sending.ToString() + " flyers... so exiting...");
				Environment.Exit(0);
			}

			FlyerSet flyersToSend = GetFlyersToSend(maxNumberOfConcurrentEflyers - sending);

			Console.WriteLine(flyersToSend.Count.ToString() + " flyers to send...");

			foreach (Flyer f in flyersToSend)
			{
				Process sender = new Process { StartInfo = { FileName = args[0], Arguments = f.K.ToString() } };
				sender.Start();
			}
		}

		private static FlyerSet GetFlyersToSend(int max)
		{
			return new FlyerSet(new Query
			{
				QueryCondition = new And(new Q(Flyer.Columns.IsReadyToSend, true),
										 new Q(Flyer.Columns.IsSending, false),
										 new Q(Flyer.Columns.HasFinishedSending, false)),
				TopRecords = max
			});
		}

		private static int GetFlyersSendingCount()
		{
			return new FlyerSet(new Query
			{
				QueryCondition = new Q(Flyer.Columns.IsSending, true)
			}).Count;
		}
	}
}
