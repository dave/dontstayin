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
using Bobs;

namespace Spotted.Pages.Events
{
	public partial class Buy : EventUserControl
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			ContainerPage.SslPage = true;
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (CurrentEvent.K == 46716 || Vars.DevEnv)
			{
				TicketsPlaceholder.Controls.Clear();
				TicketsPlaceholder.Controls.Add(Page.LoadControl("~/Controls/Tickets/2006-07/HeatSW4PickUp.ascx"));
			}
		}
		//public Event CurrentEvent
		//{
		//    get
		//    {
		//        return ContainerPage.Url.ObjectFilterEvent;
		//    }
		//}
	}

}
