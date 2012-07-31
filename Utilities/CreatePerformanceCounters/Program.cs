using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CreatePerformanceCounters
{
	class Program
	{
		static void Main(string[] args)
		{

			if (PerformanceCounterCategory.Exists("DontStayIn"))
				PerformanceCounterCategory.Delete("DontStayIn");
				
			// Create the collection container
			CounterCreationDataCollection counters = new CounterCreationDataCollection();

			// Create counter #1 and add it to the collection
			CounterCreationData dsiPages = new CounterCreationData();
			dsiPages.CounterName = "DsiPages per sec";
			dsiPages.CounterHelp = "Total number of dsi pages per second.";
			dsiPages.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
			counters.Add(dsiPages);

			// Create counter #3 and add it to the collection
			CounterCreationData genTime = new CounterCreationData();
			genTime.CounterName = "DsiPage generation time";
			genTime.CounterHelp = "Average time to generate a page.";
			genTime.CounterType = PerformanceCounterType.AverageTimer32;
			counters.Add(genTime);

			CounterCreationData genTimeBase = new CounterCreationData();
			genTimeBase.CounterName = "DsiPage generation time base";
			genTimeBase.CounterHelp = "Average time to generate a page base.";
			genTimeBase.CounterType = PerformanceCounterType.AverageBase;
			counters.Add(genTimeBase);

			// Create the category and all of the counters.
			PerformanceCounterCategory.Create("DontStayIn", "Performance counters for DontStayIn.", PerformanceCounterCategoryType.SingleInstance, counters);
			Console.WriteLine("Done!");
			Console.ReadLine();

		}
	}
}
