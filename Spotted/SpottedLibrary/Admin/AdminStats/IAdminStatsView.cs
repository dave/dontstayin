using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedLibrary.Admin.AdminStats
{
	public interface IAdminStatsView :  IView
	{
		int JobProcessorCurrentQueueSize { set; }
		List<JobProcessorLogDataItem> JobProcessorDataItems { set; }
		List<PhotoUploaderTriesDataItem> PhotoUploaderTriesDataItems { set; }
		List<PhotoUploaderSuccessFailureDataItem> PhotoUploaderSuccessFailureDataItems { set; }
	}
}
