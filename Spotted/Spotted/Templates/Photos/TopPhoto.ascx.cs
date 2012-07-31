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

namespace Spotted.Templates.Photos
{
	public partial class TopPhoto : System.Web.UI.UserControl
	{
		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Photo.Columns.Icon,
					Photo.Columns.ContentDisabled,
					Photo.Columns.K,
					Photo.Columns.UrlFragment,
					Photo.Columns.EventK,
					Photo.Columns.ArticleK,
					Photo.Columns.PhotoOfWeekCaption,
					Photo.Columns.IsSonyC902);
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			NormalPanel.Visible = !CurrentPhoto.IsSonyC902;
			SonyPanel.Visible = CurrentPhoto.IsSonyC902;
		}

		protected Photo CurrentPhoto
		{
			get
			{
				if (currentPhoto == null)
					currentPhoto = ((Photo)((DataListItem)NamingContainer).DataItem);
				return currentPhoto;
			}
		}
		Photo currentPhoto;

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
