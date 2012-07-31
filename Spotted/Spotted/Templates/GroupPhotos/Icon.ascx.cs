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

namespace Spotted.Templates.GroupPhotos
{
	public partial class Icon : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{

		}

		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Photo.Columns.Icon,
					Photo.Columns.ContentDisabled,
					Photo.Columns.K,
					Photo.Columns.UrlFragment,
					Photo.Columns.ArticleK,
					Photo.Columns.EventK,
					Photo.Columns.PhotoOfWeekCaption,
					GroupPhoto.Columns.GroupK,
					GroupPhoto.Columns.PhotoK,
					GroupPhoto.Columns.Caption);
			}
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
