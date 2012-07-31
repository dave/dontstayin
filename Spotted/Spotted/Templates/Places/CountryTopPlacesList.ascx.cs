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

namespace Spotted.Templates.Places
{
	public partial class CountryTopPlacesList : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		protected Place CurrentPlace
		{
			get
			{
				if (currentPlace == null)
					currentPlace = ((Place)((DataListItem)NamingContainer).DataItem);
				return currentPlace;
			}
		}
		Place currentPlace;


		protected string Start
		{
			get
			{
				string html = "";
				if (!CurrentPlace.Enabled || CurrentPlace.TotalEvents == 0)
					html += "<small>";
				if (CurrentPlace.Enabled)
					html += "<a href=\"" + CurrentPlace.Url() + "\">";
				return html;
			}
		}
		protected string End
		{
			get
			{
				string html = "";
				if (CurrentPlace.Enabled)
					html += "</a>";
				if (!CurrentPlace.Enabled || CurrentPlace.TotalEvents == 0)
					html += "</small>";
				if (CurrentPlace.TotalEvents > 0)
					html += " <small>(" + CurrentPlace.TotalEvents.ToString("#,##0") + ")</small>";
				return html;
			}
		}

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
