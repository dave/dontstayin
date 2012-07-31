using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedLibrary.Admin.AdminStats
{
	public class PhotoUploaderSuccessFailureDataItem
	{
		public string HostName { get; set; }
		public int Successes { get; set; }
		public int Failures { get; set; }
	}
}
