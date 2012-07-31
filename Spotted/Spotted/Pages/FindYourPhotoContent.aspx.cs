using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Pages
{
	public partial class FindYourPhotoContent : System.Web.UI.Page
	{
		protected HtmlAnchor SpotterLink1, SpotterLink2;
		protected HtmlImage SpotterImg;
		protected HtmlContainerControl SpotterCodePanel, EventSearchPanel;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["UsrK"] != null && Request.QueryString["UsrK"].Length > 0)
			{
				if (CurrentUsr != null)
				{
					SpotterLink1.HRef = CurrentUsr.Url();
					SpotterImg.Src = CurrentUsr.PicPath;
					SpotterLink2.HRef = CurrentUsr.Url();
					SpotterLink2.InnerText = CurrentUsr.NickName;
					CurrentUsr.MakeRolloverNoPic(SpotterLink1);
					CurrentUsr.MakeRolloverNoPic(SpotterLink2);
					SpotterCodePanel.Visible = true;
					EventSearchPanel.Visible = false;

					uiGalleriesBySpotter.SpotterUsr = CurrentUsr;
					uiGalleriesBySpotter.DataBind();

				}
				else
				{
					Response.Write("Can't find this spotter. The spotter code should be 8 digits, like 1234-5678.");
					Response.Flush();
					Response.End();
				}
			}
			else if (Request.QueryString["EventK"] != null && Request.QueryString["EventK"].Length > 0)
			{
				SpotterCodePanel.Visible = false;
				EventSearchPanel.Visible = true;

				uiGalleriesByEvent.ThisEvent = CurrentEvent;
				uiGalleriesByEvent.DataBind();
			}
		}
		#region CurrentUsr
		public Usr CurrentUsr
		{
			get
			{
				try
				{
					if (currentUsr == null && Request.QueryString["UsrK"] != null && Request.QueryString["UsrK"].Length > 0)
						currentUsr = new Usr(int.Parse(Request.QueryString["UsrK"]));
				}
				catch { }
				return currentUsr;
			}
			set
			{
				currentUsr = value;
			}
		}
		public Usr currentUsr;
		#endregion

		#region CurrentEvent
		public Event CurrentEvent
		{
			get
			{
				try
				{
					if (currentEvent == null && Request.QueryString["EventK"] != null && Request.QueryString["EventK"].Length > 0)
						currentEvent = new Event(int.Parse(Request.QueryString["EventK"]));
				}
				catch { }
				return currentEvent;
			}
			set
			{
				currentEvent = value;
			}
		}
		public Event currentEvent;
		#endregion

	}
}
