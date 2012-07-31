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
using Common;
using System.Linq;

namespace Spotted.Pages
{
	public partial class HomeContent : System.Web.UI.UserControl
	{
		protected Label NewUsersLabel;

		private void Page_Load(object sender, System.EventArgs e)
		{



			
			
		}

		#region PhotoOfWeek
		private void PhotoOfWeek_Load(object sender, System.EventArgs e)
		{
			TopPhotosUc.Holder = PhotoOfWeekAllPanel;
			//TopPhotosUc.Bind();
		}
		#endregion

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
			this.Load += new System.EventHandler(this.PhotoOfWeek_Load);
		}
		#endregion
	}
}
