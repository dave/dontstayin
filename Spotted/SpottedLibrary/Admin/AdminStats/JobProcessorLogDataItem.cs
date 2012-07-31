using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedLibrary.Admin.AdminStats
{
	public class JobProcessorLogDataItem
	{
		public DateTime StartOfTimePeriod { get; set; }
		public int NumberOfItemsQueued { get; set; }
		public int NumberOfItemsServed { get; set; }
		public int NumberOfExceptions { get; set; }
	}
}
