using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedLibrary.Admin.AdminStats
{
	public class AdminStatsController
	{
		IAdminStatsView view;
		AdminStatsService service;
		public AdminStatsController(IAdminStatsView view, AdminStatsService service)
		{
			this.view = view;
			this.service = service;
			this.view.Load += new EventHandler(view_Load);
		}

		void view_Load(object sender, EventArgs e)
		{
			view.JobProcessorDataItems = service.GetRecentJobProcessorLogInfo();
			view.JobProcessorCurrentQueueSize = service.GetCurrentQueueSize();
			view.PhotoUploaderTriesDataItems = service.GetPhotoUploaderTriesDataItems();
			view.PhotoUploaderSuccessFailureDataItems = service.GetPhotoUploaderSuccessFailureDataItems();
			view.DataBind();
		}
	}
}
