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

namespace Spotted.Templates.Usrs
{
	public partial class Default : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		protected string Online
		{
			get
			{
				if (CurrentUsr.IsLoggedOn && CurrentUsr.DateTimeLastPageRequest > DateTime.Now.AddMinutes(-5))
				{
					//return "<img src=\"/gfx/icon-me-up.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><b>I'm online</b>";
					return "<b>(online)</b>";
				}
				else
				{
					return "<small>(offline)</small>";
					//return "<img src=\"/gfx/icon-me-dn.png\" width=\"26\" height=\"21\" align=\"absmiddle\" style=\"margin-right:3px;\"><small>I'm offline</small>";
				}
			}
		}
		protected string BoldStart
		{
			get
			{
				if (CurrentUsr.IsLoggedOn && CurrentUsr.DateTimeLastPageRequest > DateTime.Now.AddMinutes(-5))
					return "<b>";
				else
					return "<small>";
			}
		}
		protected string BoldEnd
		{
			get
			{
				if (CurrentUsr.IsLoggedOn && CurrentUsr.DateTimeLastPageRequest > DateTime.Now.AddMinutes(-5))
					return "</b>";
				else
					return "</small>";
			}
		}
		protected Usr CurrentUsr
		{
			get
			{
				if (currentUsr == null)
					currentUsr = ((Usr)((DataListItem)NamingContainer).DataItem);
				return currentUsr;
			}
		}
		Usr currentUsr;

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
