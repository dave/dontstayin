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

namespace Spotted.Controls.Banners
{
	public partial class ImageBanner : FileBanner
	{
		private void Page_Load(object sender, System.EventArgs eargs)
		{

		}

		protected string LinkTargetString
		{
			get
			{
				return LinkTargetBlank ? "_blank" : "";
			}
		}

		protected string WidthString
		{
			get
			{
				return Width.ToString();
			}
		}

		protected string HeightString
		{
			get
			{
				return Height.ToString();
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
