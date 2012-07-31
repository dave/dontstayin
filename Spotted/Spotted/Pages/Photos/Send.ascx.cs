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
using System.Text.RegularExpressions;
using System.Linq;
namespace Spotted.Pages.Photos
{
	public partial class Send : DsiUserControl
	{
		public void Page_Init(object o, System.EventArgs e)
		{
			if (CurrentPhoto != null)
				CurrentPhoto.AddRelevant(ContainerPage);
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			ContainerPage.SetPageTitle("Send photo by email");
			
			PhotoImg.Src = CurrentPhoto.WebPath;
			PhotoImg.Width = CurrentPhoto.WebWidth;
			PhotoImg.Height = CurrentPhoto.WebHeight;
			PhotoAnchor.HRef = CurrentPhoto.Url();

		}
		#region Page_PreRender
		public void Page_PreRender(object o, System.EventArgs e)
		{
			ContainerPage.ViewStatePublic["CommentDuplicateGuid"] = Guid.NewGuid();
		}
		#endregion

		#region CurrentPhoto
		public Photo CurrentPhoto
		{
			get
			{
				return ContainerPage.Url.ObjectFilterPhoto;
			}
		}
		#endregion

		#region SendEmails
		public void SendEmails(object o, System.EventArgs e)
		{
			

			if (Page.IsValid && this.MultiBuddyChooser.SelectedUsrKs.Any())
			{
				if (!CurrentPhoto.Validate())
					throw new DsiUserFriendlyException("Photo not enabled!");

				
				
				Thread.Maker m = new Thread.Maker();
				m.DuplicateGuid = ContainerPage.ViewStatePublic["CommentDuplicateGuid"];

				string stripped = MessageHtml.GetPlainText();
				
				m.Subject = stripped.TruncateWithDots(50);

				m.Body = MessageHtml.GetHtml();
				
				m.ParentType = Model.Entities.ObjectType.Photo;
				m.ParentK = CurrentPhoto.K;
				m.Private = true;

				m.InviteKs = this.MultiBuddyChooser.SelectedUsrKs.ToList();
				
				m.PostingUsr = Usr.Current;

				Thread.MakerReturn r = m.Post();
				if (!r.Success && !r.Duplicate)
					throw new Exception(r.MessageHtml);

				Response.Redirect(r.Thread.Url());

				//SendPhotoByEmail(u, name);
				
			}
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
		}
		#endregion
	}
}
