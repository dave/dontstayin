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
	public partial class AdminProcessing : System.Web.UI.UserControl
	{

		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Photo.Columns.K, 
					Photo.Columns.DateTime, 
					Photo.Columns.MediaType, 
					Photo.Columns.Status, 
					Photo.Columns.ProcessingProgress, 
					Photo.Columns.ProcessingLastChange, 
					Photo.Columns.ProcessingStartDateTime, 
					Photo.Columns.IsProcessing, 
					Photo.Columns.ProcessingAttempts, 
					Photo.Columns.UploadTemporary, 
					Photo.Columns.UploadTemporaryExtention);
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			DeleteLinkButton.Attributes["onclick"] = "return confirm('Are you sure?');";
			RetryLinkButton.Visible = CurrentPhoto.HasGivenUpProcessing;

			this.ID = "ucPhotoAdminK" + CurrentPhoto.K;

		}

		public void Command(object o, CommandEventArgs e)
		{
			if (CurrentPhoto.K == int.Parse(e.CommandArgument.ToString()))
			{
				if (e.CommandName.Equals("Delete"))
				{
					CurrentPhoto = new Photo(CurrentPhoto.K);
					if (Usr.Current.K == CurrentPhoto.UsrK || Usr.Current.IsSenior)
					{
						Delete.DeleteAll(CurrentPhoto);
					}
				}
				else if (e.CommandName.Equals("Retry"))
				{
					CurrentPhoto = new Photo(CurrentPhoto.K);
					if (Usr.Current.K == CurrentPhoto.UsrK || Usr.Current.IsSenior)
					{
						CurrentPhoto.ProcessingAttempts = 0;
						CurrentPhoto.Update();
					}
				}
				PhotoAdminPage.Refresh();
			}
		}

		IRefreshable PhotoAdminPage
		{
			get
			{
				return (IRefreshable)NamingContainer.NamingContainer.NamingContainer;
			}
		}

		protected Gallery CurrentGallery
		{
			get
			{
				return ((Spotted.Master.DsiPage)Page).Url.ObjectFilterGallery;
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
			set
			{
				currentPhoto = value;
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
