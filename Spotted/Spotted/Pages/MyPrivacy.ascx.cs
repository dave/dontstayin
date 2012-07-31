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

namespace Spotted.Pages
{
	public partial class MyPrivacy : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("");



			if (!Page.IsPostBack)
			{
				SendSpottedEmails.Checked = Usr.Current.SendSpottedEmails;
				SendInvites.Checked = Usr.Current.SendFlyers;
				EnhancedSecurity.Checked = Usr.Current.EnhancedSecurity;
				ExDirectory.Checked = Usr.Current.ExDirectory;
				InboxEmailsCheckBox.Checked = !Usr.Current.NoInboxEmails;

				FacebookStory.Checked = !Usr.Current.FacebookStory.HasValue || (Usr.Current.FacebookStory.HasValue && Usr.Current.FacebookStory.Value);
				

				FacebookStoryAttendEvent.Checked = Usr.Current.FacebookStoryAttendEvent || !FacebookStory.Checked;
				FacebookStoryBuyTicket.Checked = Usr.Current.FacebookStoryBuyTicket || !FacebookStory.Checked;
				FacebookStoryUploadPhoto.Checked = Usr.Current.FacebookStoryUploadPhoto || !FacebookStory.Checked;
				FacebookStorySpotted.Checked = Usr.Current.FacebookStorySpotted || !FacebookStory.Checked;
				FacebookStoryPhotoFeatured.Checked = Usr.Current.FacebookStoryPhotoFeatured || !FacebookStory.Checked;
				FacebookStoryNewBuddy.Checked = Usr.Current.FacebookStoryNewBuddy || !FacebookStory.Checked;
				FacebookStoryPublishArticle.Checked = Usr.Current.FacebookStoryPublishArticle || !FacebookStory.Checked;
				FacebookStoryJoinGroup.Checked = Usr.Current.FacebookStoryJoinGroup || !FacebookStory.Checked;
				FacebookStoryFavourite.Checked = Usr.Current.FacebookStoryFavourite || !FacebookStory.Checked;
				FacebookStoryNewTopic.Checked = Usr.Current.FacebookStoryNewTopic || !FacebookStory.Checked;
				FacebookStoryEventReview.Checked = Usr.Current.FacebookStoryEventReview || !FacebookStory.Checked;
				FacebookStoryPostNews.Checked = Usr.Current.FacebookStoryPostNews || !FacebookStory.Checked;
				FacebookStoryLaugh.Checked = Usr.Current.FacebookStoryLaugh || !FacebookStory.Checked;

				FacebookEventAdd.Checked = Usr.Current.FacebookEventAdd;
				FacebookEventAttend.Checked = Usr.Current.FacebookEventAttend;
			}

			
			FacebookStory.Attributes["onclick"] = "document.getElementById('" + FacebookStoryPanel.ClientID + "').style.display = document.getElementById('" + FacebookStory.ClientID + "').checked ? '' : 'none';";
			FacebookStoryPanel.Style["display"] = FacebookStory.Checked ? "" : "none";
		}
		#region PrefsUpdateClick
		protected void PrefsUpdateClick(object sender, EventArgs eventArgs)
		{
			if (UnsubscribeCheckBox.Checked)
			{
				Usr.Current.Unsubscribe();				
			}
			else
			{
				Usr.Current.FacebookStory = FacebookStory.Checked;
				Usr.Current.FacebookStory1 = FacebookStory.Checked;
				Usr.Current.FacebookStoryAttendEvent = FacebookStory.Checked && FacebookStoryAttendEvent.Checked;
				Usr.Current.FacebookStoryBuyTicket = FacebookStory.Checked && FacebookStoryBuyTicket.Checked;
				Usr.Current.FacebookStoryUploadPhoto = FacebookStory.Checked && FacebookStoryUploadPhoto.Checked;
				Usr.Current.FacebookStorySpotted = FacebookStory.Checked && FacebookStorySpotted.Checked;
				Usr.Current.FacebookStoryPhotoFeatured = FacebookStory.Checked && FacebookStoryPhotoFeatured.Checked;
				Usr.Current.FacebookStoryNewBuddy = FacebookStory.Checked && FacebookStoryNewBuddy.Checked;
				Usr.Current.FacebookStoryPublishArticle = FacebookStory.Checked && FacebookStoryPublishArticle.Checked;
				Usr.Current.FacebookStoryJoinGroup = FacebookStory.Checked && FacebookStoryJoinGroup.Checked;
				Usr.Current.FacebookStoryFavourite = FacebookStory.Checked && FacebookStoryFavourite.Checked;
				Usr.Current.FacebookStoryNewTopic = FacebookStory.Checked && FacebookStoryNewTopic.Checked;
				Usr.Current.FacebookStoryEventReview = FacebookStory.Checked && FacebookStoryEventReview.Checked;
				Usr.Current.FacebookStoryPostNews = FacebookStory.Checked && FacebookStoryPostNews.Checked;
				Usr.Current.FacebookStoryLaugh = FacebookStory.Checked && FacebookStoryLaugh.Checked;

				Usr.Current.FacebookEventAdd = FacebookEventAdd.Checked;
				Usr.Current.FacebookEventAttend = FacebookEventAttend.Checked;

				Usr.Current.SendSpottedEmails = SendSpottedEmails.Checked;
				Usr.Current.SendSpottedTexts = SendInvites.Checked;
				Usr.Current.SendFlyers = SendInvites.Checked;
				Usr.Current.SendInvites = SendInvites.Checked;
				Usr.Current.EnhancedSecurity = EnhancedSecurity.Checked;
				Usr.Current.ExDirectory = ExDirectory.Checked;
				Usr.Current.NoInboxEmails = !InboxEmailsCheckBox.Checked;
				Usr.Current.Update();
				SuccessLabel.Text = "Details updated";
			}			
		}
		#endregion
	}
}
