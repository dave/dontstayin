//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.ExDirectoryPrivacyOption", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.MyPrivacy
{
	public partial class View
		 : Js.DsiUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public DivElement PanelChange {get {if (_PanelChange == null) {_PanelChange = (DivElement)Document.GetElementById(clientId + "_PanelChange");}; return _PanelChange;}} private DivElement _PanelChange;
		public jQueryObject PanelChangeJ {get {if (_PanelChangeJ == null) {_PanelChangeJ = jQuery.Select("#" + clientId + "_PanelChange");}; return _PanelChangeJ;}} private jQueryObject _PanelChangeJ;
		public CheckBoxElement SendSpottedEmails {get {if (_SendSpottedEmails == null) {_SendSpottedEmails = (CheckBoxElement)Document.GetElementById(clientId + "_SendSpottedEmails");}; return _SendSpottedEmails;}} private CheckBoxElement _SendSpottedEmails;
		public jQueryObject SendSpottedEmailsJ {get {if (_SendSpottedEmailsJ == null) {_SendSpottedEmailsJ = jQuery.Select("#" + clientId + "_SendSpottedEmails");}; return _SendSpottedEmailsJ;}} private jQueryObject _SendSpottedEmailsJ;
		public CheckBoxElement SendInvites {get {if (_SendInvites == null) {_SendInvites = (CheckBoxElement)Document.GetElementById(clientId + "_SendInvites");}; return _SendInvites;}} private CheckBoxElement _SendInvites;
		public jQueryObject SendInvitesJ {get {if (_SendInvitesJ == null) {_SendInvitesJ = jQuery.Select("#" + clientId + "_SendInvites");}; return _SendInvitesJ;}} private jQueryObject _SendInvitesJ;
		public Element H17a {get {if (_H17a == null) {_H17a = (Element)Document.GetElementById(clientId + "_H17a");}; return _H17a;}} private Element _H17a;
		public jQueryObject H17aJ {get {if (_H17aJ == null) {_H17aJ = jQuery.Select("#" + clientId + "_H17a");}; return _H17aJ;}} private jQueryObject _H17aJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public CheckBoxElement FacebookStory {get {if (_FacebookStory == null) {_FacebookStory = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStory");}; return _FacebookStory;}} private CheckBoxElement _FacebookStory;
		public jQueryObject FacebookStoryJ {get {if (_FacebookStoryJ == null) {_FacebookStoryJ = jQuery.Select("#" + clientId + "_FacebookStory");}; return _FacebookStoryJ;}} private jQueryObject _FacebookStoryJ;
		public Element FacebookStoryPanel {get {if (_FacebookStoryPanel == null) {_FacebookStoryPanel = (Element)Document.GetElementById(clientId + "_FacebookStoryPanel");}; return _FacebookStoryPanel;}} private Element _FacebookStoryPanel;
		public jQueryObject FacebookStoryPanelJ {get {if (_FacebookStoryPanelJ == null) {_FacebookStoryPanelJ = jQuery.Select("#" + clientId + "_FacebookStoryPanel");}; return _FacebookStoryPanelJ;}} private jQueryObject _FacebookStoryPanelJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public CheckBoxElement FacebookStoryAttendEvent {get {if (_FacebookStoryAttendEvent == null) {_FacebookStoryAttendEvent = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryAttendEvent");}; return _FacebookStoryAttendEvent;}} private CheckBoxElement _FacebookStoryAttendEvent;
		public jQueryObject FacebookStoryAttendEventJ {get {if (_FacebookStoryAttendEventJ == null) {_FacebookStoryAttendEventJ = jQuery.Select("#" + clientId + "_FacebookStoryAttendEvent");}; return _FacebookStoryAttendEventJ;}} private jQueryObject _FacebookStoryAttendEventJ;
		public CheckBoxElement FacebookStorySpotted {get {if (_FacebookStorySpotted == null) {_FacebookStorySpotted = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStorySpotted");}; return _FacebookStorySpotted;}} private CheckBoxElement _FacebookStorySpotted;
		public jQueryObject FacebookStorySpottedJ {get {if (_FacebookStorySpottedJ == null) {_FacebookStorySpottedJ = jQuery.Select("#" + clientId + "_FacebookStorySpotted");}; return _FacebookStorySpottedJ;}} private jQueryObject _FacebookStorySpottedJ;
		public CheckBoxElement FacebookStoryUploadPhoto {get {if (_FacebookStoryUploadPhoto == null) {_FacebookStoryUploadPhoto = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryUploadPhoto");}; return _FacebookStoryUploadPhoto;}} private CheckBoxElement _FacebookStoryUploadPhoto;
		public jQueryObject FacebookStoryUploadPhotoJ {get {if (_FacebookStoryUploadPhotoJ == null) {_FacebookStoryUploadPhotoJ = jQuery.Select("#" + clientId + "_FacebookStoryUploadPhoto");}; return _FacebookStoryUploadPhotoJ;}} private jQueryObject _FacebookStoryUploadPhotoJ;
		public CheckBoxElement FacebookStoryPhotoFeatured {get {if (_FacebookStoryPhotoFeatured == null) {_FacebookStoryPhotoFeatured = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryPhotoFeatured");}; return _FacebookStoryPhotoFeatured;}} private CheckBoxElement _FacebookStoryPhotoFeatured;
		public jQueryObject FacebookStoryPhotoFeaturedJ {get {if (_FacebookStoryPhotoFeaturedJ == null) {_FacebookStoryPhotoFeaturedJ = jQuery.Select("#" + clientId + "_FacebookStoryPhotoFeatured");}; return _FacebookStoryPhotoFeaturedJ;}} private jQueryObject _FacebookStoryPhotoFeaturedJ;
		public CheckBoxElement FacebookStoryNewTopic {get {if (_FacebookStoryNewTopic == null) {_FacebookStoryNewTopic = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryNewTopic");}; return _FacebookStoryNewTopic;}} private CheckBoxElement _FacebookStoryNewTopic;
		public jQueryObject FacebookStoryNewTopicJ {get {if (_FacebookStoryNewTopicJ == null) {_FacebookStoryNewTopicJ = jQuery.Select("#" + clientId + "_FacebookStoryNewTopic");}; return _FacebookStoryNewTopicJ;}} private jQueryObject _FacebookStoryNewTopicJ;
		public CheckBoxElement FacebookStoryLaugh {get {if (_FacebookStoryLaugh == null) {_FacebookStoryLaugh = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryLaugh");}; return _FacebookStoryLaugh;}} private CheckBoxElement _FacebookStoryLaugh;
		public jQueryObject FacebookStoryLaughJ {get {if (_FacebookStoryLaughJ == null) {_FacebookStoryLaughJ = jQuery.Select("#" + clientId + "_FacebookStoryLaugh");}; return _FacebookStoryLaughJ;}} private jQueryObject _FacebookStoryLaughJ;
		public CheckBoxElement FacebookStoryFavourite {get {if (_FacebookStoryFavourite == null) {_FacebookStoryFavourite = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryFavourite");}; return _FacebookStoryFavourite;}} private CheckBoxElement _FacebookStoryFavourite;
		public jQueryObject FacebookStoryFavouriteJ {get {if (_FacebookStoryFavouriteJ == null) {_FacebookStoryFavouriteJ = jQuery.Select("#" + clientId + "_FacebookStoryFavourite");}; return _FacebookStoryFavouriteJ;}} private jQueryObject _FacebookStoryFavouriteJ;
		public CheckBoxElement FacebookStoryFavouriteTopic {get {if (_FacebookStoryFavouriteTopic == null) {_FacebookStoryFavouriteTopic = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryFavouriteTopic");}; return _FacebookStoryFavouriteTopic;}} private CheckBoxElement _FacebookStoryFavouriteTopic;
		public jQueryObject FacebookStoryFavouriteTopicJ {get {if (_FacebookStoryFavouriteTopicJ == null) {_FacebookStoryFavouriteTopicJ = jQuery.Select("#" + clientId + "_FacebookStoryFavouriteTopic");}; return _FacebookStoryFavouriteTopicJ;}} private jQueryObject _FacebookStoryFavouriteTopicJ;
		public CheckBoxElement FacebookStoryPostNews {get {if (_FacebookStoryPostNews == null) {_FacebookStoryPostNews = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryPostNews");}; return _FacebookStoryPostNews;}} private CheckBoxElement _FacebookStoryPostNews;
		public jQueryObject FacebookStoryPostNewsJ {get {if (_FacebookStoryPostNewsJ == null) {_FacebookStoryPostNewsJ = jQuery.Select("#" + clientId + "_FacebookStoryPostNews");}; return _FacebookStoryPostNewsJ;}} private jQueryObject _FacebookStoryPostNewsJ;
		public CheckBoxElement FacebookStoryEventReview {get {if (_FacebookStoryEventReview == null) {_FacebookStoryEventReview = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryEventReview");}; return _FacebookStoryEventReview;}} private CheckBoxElement _FacebookStoryEventReview;
		public jQueryObject FacebookStoryEventReviewJ {get {if (_FacebookStoryEventReviewJ == null) {_FacebookStoryEventReviewJ = jQuery.Select("#" + clientId + "_FacebookStoryEventReview");}; return _FacebookStoryEventReviewJ;}} private jQueryObject _FacebookStoryEventReviewJ;
		public CheckBoxElement FacebookStoryPublishArticle {get {if (_FacebookStoryPublishArticle == null) {_FacebookStoryPublishArticle = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryPublishArticle");}; return _FacebookStoryPublishArticle;}} private CheckBoxElement _FacebookStoryPublishArticle;
		public jQueryObject FacebookStoryPublishArticleJ {get {if (_FacebookStoryPublishArticleJ == null) {_FacebookStoryPublishArticleJ = jQuery.Select("#" + clientId + "_FacebookStoryPublishArticle");}; return _FacebookStoryPublishArticleJ;}} private jQueryObject _FacebookStoryPublishArticleJ;
		public CheckBoxElement FacebookStoryNewBuddy {get {if (_FacebookStoryNewBuddy == null) {_FacebookStoryNewBuddy = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryNewBuddy");}; return _FacebookStoryNewBuddy;}} private CheckBoxElement _FacebookStoryNewBuddy;
		public jQueryObject FacebookStoryNewBuddyJ {get {if (_FacebookStoryNewBuddyJ == null) {_FacebookStoryNewBuddyJ = jQuery.Select("#" + clientId + "_FacebookStoryNewBuddy");}; return _FacebookStoryNewBuddyJ;}} private jQueryObject _FacebookStoryNewBuddyJ;
		public CheckBoxElement FacebookStoryJoinGroup {get {if (_FacebookStoryJoinGroup == null) {_FacebookStoryJoinGroup = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryJoinGroup");}; return _FacebookStoryJoinGroup;}} private CheckBoxElement _FacebookStoryJoinGroup;
		public jQueryObject FacebookStoryJoinGroupJ {get {if (_FacebookStoryJoinGroupJ == null) {_FacebookStoryJoinGroupJ = jQuery.Select("#" + clientId + "_FacebookStoryJoinGroup");}; return _FacebookStoryJoinGroupJ;}} private jQueryObject _FacebookStoryJoinGroupJ;
		public CheckBoxElement FacebookStoryBuyTicket {get {if (_FacebookStoryBuyTicket == null) {_FacebookStoryBuyTicket = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookStoryBuyTicket");}; return _FacebookStoryBuyTicket;}} private CheckBoxElement _FacebookStoryBuyTicket;
		public jQueryObject FacebookStoryBuyTicketJ {get {if (_FacebookStoryBuyTicketJ == null) {_FacebookStoryBuyTicketJ = jQuery.Select("#" + clientId + "_FacebookStoryBuyTicket");}; return _FacebookStoryBuyTicketJ;}} private jQueryObject _FacebookStoryBuyTicketJ;
		public CheckBoxElement FacebookEventAdd {get {if (_FacebookEventAdd == null) {_FacebookEventAdd = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookEventAdd");}; return _FacebookEventAdd;}} private CheckBoxElement _FacebookEventAdd;
		public jQueryObject FacebookEventAddJ {get {if (_FacebookEventAddJ == null) {_FacebookEventAddJ = jQuery.Select("#" + clientId + "_FacebookEventAdd");}; return _FacebookEventAddJ;}} private jQueryObject _FacebookEventAddJ;
		public CheckBoxElement FacebookEventAttend {get {if (_FacebookEventAttend == null) {_FacebookEventAttend = (CheckBoxElement)Document.GetElementById(clientId + "_FacebookEventAttend");}; return _FacebookEventAttend;}} private CheckBoxElement _FacebookEventAttend;
		public jQueryObject FacebookEventAttendJ {get {if (_FacebookEventAttendJ == null) {_FacebookEventAttendJ = jQuery.Select("#" + clientId + "_FacebookEventAttend");}; return _FacebookEventAttendJ;}} private jQueryObject _FacebookEventAttendJ;
		public Element H17 {get {if (_H17 == null) {_H17 = (Element)Document.GetElementById(clientId + "_H17");}; return _H17;}} private Element _H17;
		public jQueryObject H17J {get {if (_H17J == null) {_H17J = jQuery.Select("#" + clientId + "_H17");}; return _H17J;}} private jQueryObject _H17J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public CheckBoxElement InboxEmailsCheckBox {get {if (_InboxEmailsCheckBox == null) {_InboxEmailsCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_InboxEmailsCheckBox");}; return _InboxEmailsCheckBox;}} private CheckBoxElement _InboxEmailsCheckBox;
		public jQueryObject InboxEmailsCheckBoxJ {get {if (_InboxEmailsCheckBoxJ == null) {_InboxEmailsCheckBoxJ = jQuery.Select("#" + clientId + "_InboxEmailsCheckBox");}; return _InboxEmailsCheckBoxJ;}} private jQueryObject _InboxEmailsCheckBoxJ;
		public Element H18 {get {if (_H18 == null) {_H18 = (Element)Document.GetElementById(clientId + "_H18");}; return _H18;}} private Element _H18;
		public jQueryObject H18J {get {if (_H18J == null) {_H18J = jQuery.Select("#" + clientId + "_H18");}; return _H18J;}} private jQueryObject _H18J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public CheckBoxElement UnsubscribeCheckBox {get {if (_UnsubscribeCheckBox == null) {_UnsubscribeCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_UnsubscribeCheckBox");}; return _UnsubscribeCheckBox;}} private CheckBoxElement _UnsubscribeCheckBox;
		public jQueryObject UnsubscribeCheckBoxJ {get {if (_UnsubscribeCheckBoxJ == null) {_UnsubscribeCheckBoxJ = jQuery.Select("#" + clientId + "_UnsubscribeCheckBox");}; return _UnsubscribeCheckBoxJ;}} private jQueryObject _UnsubscribeCheckBoxJ;
		public Element H19 {get {if (_H19 == null) {_H19 = (Element)Document.GetElementById(clientId + "_H19");}; return _H19;}} private Element _H19;
		public jQueryObject H19J {get {if (_H19J == null) {_H19J = jQuery.Select("#" + clientId + "_H19");}; return _H19J;}} private jQueryObject _H19J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public CheckBoxElement EnhancedSecurity {get {if (_EnhancedSecurity == null) {_EnhancedSecurity = (CheckBoxElement)Document.GetElementById(clientId + "_EnhancedSecurity");}; return _EnhancedSecurity;}} private CheckBoxElement _EnhancedSecurity;
		public jQueryObject EnhancedSecurityJ {get {if (_EnhancedSecurityJ == null) {_EnhancedSecurityJ = jQuery.Select("#" + clientId + "_EnhancedSecurity");}; return _EnhancedSecurityJ;}} private jQueryObject _EnhancedSecurityJ;
		public Element ExDirectory {get {if (_ExDirectory == null) {_ExDirectory = (Element)Document.GetElementById(clientId + "_ExDirectory");}; return _ExDirectory;}} private Element _ExDirectory;
		public jQueryObject ExDirectoryJ {get {if (_ExDirectoryJ == null) {_ExDirectoryJ = jQuery.Select("#" + clientId + "_ExDirectory");}; return _ExDirectoryJ;}} private jQueryObject _ExDirectoryJ;//mappings.Add("Spotted.Controls.ExDirectoryPrivacyOption", ElementGetter("Element"));
		public Element PrefsUpdateButton {get {if (_PrefsUpdateButton == null) {_PrefsUpdateButton = (Element)Document.GetElementById(clientId + "_PrefsUpdateButton");}; return _PrefsUpdateButton;}} private Element _PrefsUpdateButton;
		public jQueryObject PrefsUpdateButtonJ {get {if (_PrefsUpdateButtonJ == null) {_PrefsUpdateButtonJ = jQuery.Select("#" + clientId + "_PrefsUpdateButton");}; return _PrefsUpdateButtonJ;}} private jQueryObject _PrefsUpdateButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element SuccessLabel {get {if (_SuccessLabel == null) {_SuccessLabel = (Element)Document.GetElementById(clientId + "_SuccessLabel");}; return _SuccessLabel;}} private Element _SuccessLabel;
		public jQueryObject SuccessLabelJ {get {if (_SuccessLabelJ == null) {_SuccessLabelJ = jQuery.Select("#" + clientId + "_SuccessLabel");}; return _SuccessLabelJ;}} private jQueryObject _SuccessLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
