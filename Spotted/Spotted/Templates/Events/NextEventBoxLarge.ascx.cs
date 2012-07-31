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

namespace Spotted.Templates.Events
{
	public partial class NextEventBoxLarge : System.Web.UI.UserControl
	{
		protected HtmlAnchor PicLink;
		protected Panel SmallItem;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			PicLink.Visible = CurrentEvent.HasAnyPic;

		}


		protected Event CurrentEvent
		{
			get
			{
				if (currentEvent == null)
					currentEvent = ((Event)((DataListItem)NamingContainer).DataItem);
				return currentEvent;
			}
		}
		Event currentEvent;



		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			
		}
		#endregion
	}
}
