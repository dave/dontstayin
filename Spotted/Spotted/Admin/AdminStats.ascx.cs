using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SpottedLibrary.Admin.AdminStats;

namespace Spotted.Admin
{

	public partial class AdminStats : AdminUserControl , IAdminStatsView
	{
		AdminStatsController controller;
		AdminStatsService service;
		public AdminStats()
		{
			service = new AdminStatsService();
			controller = new AdminStatsController(this, service);
		}


		public int JobProcessorCurrentQueueSize { set { this.uiCurrentJobProcessorQueueSize.Text = value.ToString(); } }
		public System.Collections.Generic.List<JobProcessorLogDataItem> JobProcessorDataItems
		{
			set
			{
				this.JobProcessorDataItemsGridView.DataSource = value;
			}
		}

		public bool IsValid
		{
			get { return Page.IsValid; }
		}



		public System.Collections.Generic.List<PhotoUploaderTriesDataItem> PhotoUploaderTriesDataItems
		{
			set { this.uiPhotoUploaderTriesDataItemsGridView.DataSource = value; ; }
		}

		public System.Collections.Generic.List<PhotoUploaderSuccessFailureDataItem> PhotoUploaderSuccessFailureDataItems
		{
			set { uiPhotoUploaderSuccessFailureDataItemsGridView.DataSource = value; }
		}

	}
}
