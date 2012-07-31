using System;
using System.Html;
using Js.Controls.PhotoBrowser;
//using Spotted.WebServices.Controls.PhotoControl;
using BannersGeneratorController = Js.Controls.Banners.Generator.Controller;
using LoginPageImplementation = Js.Controls.Login.PageImplementation;
using jQueryApi;
using Js.Library;

namespace Js.Controls.PhotoControl
{
	public class Controller : PhotoBrowsingUsingKeysControl
	{
		private View view;
		private PhotoStub[] photoSet;
		private int selectedIndex;
		private bool disableBanner;
		private bool overlayEnabled;
		private int overlayHeight;
		private int overlayWidth;
		public PhotoStub CurrentPhoto
		{
			get
			{
				return photoSet[selectedIndex];
			}
		}
		private readonly bool displayMakeFrontPageOptions;
		public EventHandler OnRolloverMouseOverTextChanged;

		public Controller(View view)
			: base(new jQueryObject[] { view.uiPhotoJ, view.uiFlashHolderJ })
		{
			this.view = view;
			this.displayMakeFrontPageOptions = bool.Parse(view.uiDisplayMakeFrontPageOptions.Value);
			
			view.uiPhotoJ.Click(nextPhotoClick);
			view.uiPrevPhotoButtonJ.Click(prevPhotoClick);
			view.uiNextPhotoButtonJ.Click(nextPhotoClick);

			if (view.uiUsrSpottedToggleButton != null) // else user not logged in
				view.uiUsrSpottedToggleButtonJ.Click(usrSpottedToggleButtonClick);
			
			if (bool.Parse(view.uiUsrIsLoggedIn.Value))
				view.uiIsFavouritePhotoToggleButtonJ.Click(isFavouritePhotoToggleButtonClick);
			
			disableBanner = bool.Parse(view.uiDisableBanner.Value);
			overlayEnabled = bool.Parse(view.uiOverlayEnabled.Value);
			overlayWidth = int.Parse(view.uiOverlayWidth.Value);
			overlayHeight = int.Parse(view.uiOverlayHeight.Value);

			if (view.uiBuddySpottedButton != null) // else user not logged in
				view.uiBuddySpottedButtonJ.Click(buddySpottedButtonClick);

			if (view.uiUseAsProfilePictureButton != null) // else user not logged in
			{
				view.uiUseAsProfilePictureButton.SetAttribute("onclick", "");
				view.uiUseAsProfilePictureButtonJ.Click(useAsProfilePictureButtonClick);
			}

			//if (displayMakeFrontPageOptions)
			//	DomEvent.AddHandler(view.uiSavePhotoOfWeek, "click", new DomEventHandler(savePhotoOfWeekClick));

			if (view.uiAddToCompetitionGroup != null)
				view.uiAddToCompetitionGroupJ.Click(addToCompetitionGroupPhoto);

			
		}
		

		private void addToCompetitionGroupPhoto(jQueryEvent e)
		{
			e.PreventDefault();
			Service.SetAsCompetitionGroupPhoto(CurrentPhoto.k, !CurrentPhoto.isInCompetitionGroup, addToCompetitionGroupPhotoSuccess, null, !CurrentPhoto.isInCompetitionGroup, -1);
		}
		void addToCompetitionGroupPhotoSuccess(object nullObject, object isInCompetitionGroup, string methodName)
		{
			CurrentPhoto.isInCompetitionGroup = (bool)isInCompetitionGroup;
			setAddToCompetitionGroupImgSrc();
		}

		private void setAddToCompetitionGroupImgSrc()
		{
			if (CurrentPhoto.canEnterCompetition)
			{
				view.uiAddToCompetitionGroup.Style.Display = "";
				view.uiAddToCompetitionGroupImg.Src =
					CurrentPhoto.isInCompetitionGroup ? 
					view.uiAddToCompetitionGroupImgRemoveButtonUrl.Value : 
					view.uiAddToCompetitionGroupImgAddButtonUrl.Value;
			}
			else
			{
				view.uiAddToCompetitionGroup.Style.Display = "none";
			}
		}

		public void IsGallerySelectedChanged(bool gallerySelected)
		{
			view.uiPhotoGalleryLinkHolder.Style.Display = gallerySelected ? "none" : "";
		}

		private void setDetails()
		{
			//Script.Literal("debugger");

			PhotoStub photo = CurrentPhoto;

			if (photo.isPhoto)
			{
				hideVideo();
				showPhoto();
			}
			else if (photo.isVideo)
			{
				showVideo();
				hidePhoto();
			}

			view.uiPhotoGalleryLink.Href = photo.url;

			view.uiTakenByDetailsSpan.InnerHTML = photo.takenByDetailsHtml;
			view.uiCopyrightUsrLinkSpan.InnerHTML = photo.usrLink;

			setUsrsInPhoto();

			view.uiPhotoVideoLabel1.InnerHTML = photo.photoVideoLabel;
			view.uiPhotoVideoLabel2.InnerHTML = photo.photoVideoLabel;
			view.uiLinkToThis.Value = photo.linkToPhotoUrl;

			if (view.uiAddToGroupLink != null) // else not logged in or not mod
				view.uiAddToGroupLink.Href = photo.linkToPhotoUrl + "/addtogroup";
			
			view.uiSendLink.Href = photo.linkToPhotoUrl + "/send";
			view.uiReportLink.Href = photo.linkToPhotoUrl + "/report";
			
			view.uiEmbedThis.Value = photo.embedThisPhotoHtml;

			//view.uiQuickBrowserUrl.Href = photo.quickBrowserUrl;
			view.uiDownloadPhotoLinkHtml.InnerHTML = photo.downloadPhotoLinkHtml;

			setSpottedToggleButtonText();
			setIsFavouritePhotoToggleButtonText();
			setAddToCompetitionGroupImgSrc();

			if (view.uiBuddyValidator != null) // else user is not logged in
				view.uiBuddyValidator.Style.Display = "none";

			//if (displayMakeFrontPageOptions)
			//{
			//    view.uiPhotoOfWeekCaptionTextBox.Value = photo.photoOfWeekCaption;
			//    view.uiPhotoOfWeekCheckBox.Checked = photo.photoOfWeek;
			//}

			try
			{
				view.uiPhotoUsage.InnerHTML = photo.photoUsageAdminString;
			}
			catch { }

			Service.IncrementViews(photo.k, null, null, null, -1);

			AnchorElement uiModerateThisPhotosTagsLink = (AnchorElement)Document.GetElementById("NavAdmin_uiModerateTagsAnchor");
			if (uiModerateThisPhotosTagsLink != null)
			{
				uiModerateThisPhotosTagsLink.Href = "/pages/moderatephototags/photo-" + photo.k;
			}
			AnchorElement uiAdministrateChatAnchor = (AnchorElement)Document.GetElementById("NavAdmin_uiAdministrateChatAnchor");
			if (uiAdministrateChatAnchor != null)
			{
				if (photo.threadK > 0)
				{
					uiAdministrateChatAnchor.Href = "/pages/chatadmin/k-" + photo.threadK;
					uiAdministrateChatAnchor.Style.Display = "";
				}
				else
				{
					uiAdministrateChatAnchor.Style.Display = "none";
				}
			}

		}

		private void setSpottedToggleButtonText()
		{
			if (view.uiUsrSpottedToggleButton != null) // else not logged in
				view.uiUsrSpottedToggleButton.InnerHTML = CurrentPhoto.usrIsInPhoto ?
					"I'm not in this photo" : "I've been spotted!";
		}

		public void PhotoSetChanged(object source, EventArgs e)
		{
			if (!bool.Parse(view.uiFirstTimeLoading.Value))
				BannersGeneratorController.RefreshAllBanners();
			setPhotoSet(((PhotoSetEventArgs)e).PhotoSet, ((PhotoSetEventArgs)e).SelectedIndex);
		}

		private void setPhotoSet(PhotoStub[] photoSet, int selectedIndex)
		{
			this.photoSet = photoSet;
			PhotoChanged(this, new IntEventArgs(selectedIndex));

			int[] photoKs = new int[photoSet.Length];
			for (int i = 0; i < photoKs.Length; i++)
			{
				photoKs[i] = photoSet[i].k;
			}
			// must happen after setting photoK - refactor to make this encapsulated.


			//view.uiTaggingControl.LoadTagsForPhotoKs(photoKs);

			// do this last
			Window.SetTimeout(preloadAllImages, 1000);
		}

		#region onPhotoChanged
		public EventHandler OnPhotoChanged;
		public void PhotoChanged(object source, EventArgs e)
		{
			

			selectedIndex = ((IntEventArgs)e).value;

			// do this regardless of if it's first load or not
			//view.uiTaggingControl.CurrentPhotoK = photoSet[selectedIndex].k;

			if (bool.Parse(view.uiFirstTimeLoading.Value) && !photoSet[selectedIndex].isVideo)
			{
				//Script.Literal("debugger");
			    view.uiFirstTimeLoading.Value = false.ToString();
			    doDelayEvents();
			}
			else
			{
				//Script.Literal("debugger");
				view.uiFirstTimeLoading.Value = false.ToString();
				setDetails(); // show the new photo...
				showNewBanner();
				triggerDelayEvents();

				if (OnPhotoChanged != null)
					OnPhotoChanged(this, new PhotoEventArgs(CurrentPhoto));
			}

			view.uiPhoto.Focus();

			//reposition the overlay?

		}
		#endregion

		#region onPhotoChangedAfterDelay
		public EventHandler OnPhotoChangedAfterDelay;
		int delayCount = 0;
		int delayMilliseconds = 500;
		private void triggerDelayEvents()
		{
			delayCount++;
			Window.SetTimeout(testDelayCount, delayMilliseconds);
		}
		private void testDelayCount()
		{
			delayCount--;
			if (delayCount <= 0)
			{
				doDelayEvents();
			}
		}

		private void doDelayEvents()
		{
			if (OnPhotoChangedAfterDelay != null)
				OnPhotoChangedAfterDelay(this, new PhotoEventArgs(CurrentPhoto));
		}
		#endregion

		#region Next / Prev photo click
		private void nextPhotoClick(jQueryEvent e)
		{
			e.PreventDefault();
			if (OnPhotoNextClick != null)
				OnPhotoNextClick(this, EventArgs.Empty);

			// for Chrome - Window.Document.ActiveElement is null otherwise
			view.uiPhoto.Focus();
		}
		private void prevPhotoClick(jQueryEvent e)
		{
			e.PreventDefault();
			if (OnPhotoPrevClick != null)
				OnPhotoPrevClick(this, EventArgs.Empty);
		}
		#endregion

		private void preloadAllImages()
		{
			// download all images
			for (int i = 0; i < photoSet.Length; i++)
			{
				if (photoSet[i] != null)
				{
					ImageElement img = (ImageElement)Document.CreateElement("img");
					img.Src = photoSet[i].webPath;
				}
			}
		}
		private void useAsProfilePictureButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			LoginPageImplementation.WhenLoggedIn(
				new Action(
					delegate()
					{
						useAsProfilePictureButtonClickInner();
					}
				)
			);
		}
		private void useAsProfilePictureButtonClickInner()
		{
	
			if (!CurrentPhoto.usrIsInPhoto)
			{
				if (Script.Confirm("Are you sure you are in this photo?"))
				{
					setUsrSpottedInPhoto(-1, CurrentPhoto.k, true);
					redirectToProfilePhotoPage();
				}
			}
			else
			{
				redirectToProfilePhotoPage();
			}
		}

		private void redirectToProfilePhotoPage()
		{
			// is there no way to redirect in script#?
			Script.Eval("window.location = '/pages/mypicture/type-pic/k-" + CurrentPhoto.k + "'");
		}

		
		private void buddySpottedButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			LoginPageImplementation.WhenLoggedIn(
				new Action(
					delegate()
					{
						buddySpottedButtonClickInner();
					}
				)
			);
		}
		private void buddySpottedButtonClickInner()
		{
			int usrK = 0;
			try { usrK = int.Parse(view.uiBuddyChooser.Value); }
			catch { }

			if (usrK > 0)
			{
				view.uiBuddyValidator.Style.Display = "none";
				view.uiBuddyChooser.Text = "";
				view.uiBuddyChooser.Value = "";
				setUsrSpottedInPhoto(usrK, CurrentPhoto.k, true);
			}
			else
			{
				view.uiBuddyValidator.Style.Display = "";
			}
		}

		private void usrSpottedToggleButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			LoginPageImplementation.WhenLoggedIn(
				new Action(
					delegate()
					{
						usrSpottedToggleButtonClickInner();
					}
				)
			);
		}
		private void usrSpottedToggleButtonClickInner()
		{
			setUsrSpottedInPhoto(-1, CurrentPhoto.k, !CurrentPhoto.usrIsInPhoto);
		}


		#region is favourite photo toggle
		private void isFavouritePhotoToggleButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			LoginPageImplementation.WhenLoggedIn(
				new Action(
					delegate()
					{
						isFavouritePhotoToggleButtonClickInner();
					}
				)
			);
		}
		private void isFavouritePhotoToggleButtonClickInner()
		{
			Service.SetIsFavouritePhoto(
				CurrentPhoto.k, 
				!CurrentPhoto.isFavourite,
				isFavouritePhotoToggleButtonClickSuccessCallback, 
				Trace.WebServiceFailure, 
				!CurrentPhoto.isFavourite, 
				-1);
		}
		private void isFavouritePhotoToggleButtonClickSuccessCallback(object nullObject, object isFavourite, string methodName)
		{
			CurrentPhoto.isFavourite = (bool)isFavourite;
			setIsFavouritePhotoToggleButtonText();
		}
		private void setIsFavouritePhotoToggleButtonText()
		{
			if (view.uiIsFavouritePhotoToggleButton != null) // else not logged in
				view.uiIsFavouritePhotoToggleButton.InnerHTML = CurrentPhoto.isFavourite ?
					"Remove from my favourites" : "Add to my favourites";
		}
		#endregion

		#region set usr spotted in photo
		private void setUsrSpottedInPhoto(int spottedUsrK, int photoK, bool isInPhoto)
		{
			if (spottedUsrK > 0)
				Service.SetUsrSpottedInPhoto(spottedUsrK, photoK, isInPhoto, setUsrSpottedInPhotoSuccessCallback, Trace.WebServiceFailure, false, -1);
			else
				Service.SetCurrentUsrSpottedInPhoto(photoK, isInPhoto, setCurrentUsrSpottedInPhotoSuccessCallback, Trace.WebServiceFailure, isInPhoto, -1);
		}
		private void setUsrSpottedInPhotoSuccessCallback(string[] newTexts, object userContext, string methodName)
		{
			setUsrInPhotoDetails(newTexts[0], newTexts[1]);
		}
		private void setCurrentUsrSpottedInPhotoSuccessCallback(string[] newTexts, object usrIsInPhoto, string methodName)
		{
			CurrentPhoto.usrIsInPhoto = (bool)usrIsInPhoto;
			setSpottedToggleButtonText();

			setUsrInPhotoDetails(newTexts[0], newTexts[1]);
		}
		private void setUsrInPhotoDetails(string usrsInPhotoHtml, string rolloverMouseOverText)
		{
			CurrentPhoto.usrsInPhotoHtml = usrsInPhotoHtml;
			CurrentPhoto.rolloverMouseOverText = rolloverMouseOverText;
			setUsrsInPhoto();
			if (OnRolloverMouseOverTextChanged != null)
				OnRolloverMouseOverTextChanged(this, new IntEventArgs(selectedIndex));
		}
		private void setUsrsInPhoto()
		{
			view.uiUsrsInPhotoSpan.Style.Display = CurrentPhoto.usrsInPhotoHtml.Length > 0 ? "" : "none";
			view.uiUsrsInPhotoSpan.InnerHTML = CurrentPhoto.usrsInPhotoHtml;
		}
		#endregion

		//#region set as photo of the week
		//private void savePhotoOfWeekClick(DomEvent e)
		//{
		//    Service.SetAsPhotoOfWeek(CurrentPhoto.k, view.uiPhotoOfWeekCheckBox.Checked, view.uiPhotoOfWeekCaptionTextBox.Value,
		//        setAsPhotoOfWeekSuccessCallback, Trace.WebServiceFailure, view.uiPhotoOfWeekCheckBox.Checked, -1);
		//    e.PreventDefault();
		//}
		//private void setAsPhotoOfWeekSuccessCallback(object nullObject, object isPhotoOfWeek, string methodName)
		//{
		//    //TODO nicer alert
		//    Script.Alert("Front page photo updated!");
		//}
		//#endregion

		#region show/hide video
		private void showVideo()
		{
			view.uiFlashHolder.Style.Display = "";
			PhotoStub p = CurrentPhoto;

			Script.Eval(
				string.Format(
@"var PhotoBrowser_so = new SWFObject('/misc/flvplayer.swf', 'PhotoBrowser_mymovie', {0}, {1}, 7, '#FFFFFF');
PhotoBrowser_so.addParam('wmode', 'transparent');
PhotoBrowser_so.addVariable('file', '{2}');
PhotoBrowser_so.addVariable('jpg', '{3}');
PhotoBrowser_so.addVariable('autoStart', '0');
PhotoBrowser_so.write('{4}');",
				p.videoMedWidth, p.videoMedHeight + 20, p.videoMedPath, p.webPath, view.uiFlashHolder.ID)
				);
		}
		private void hideVideo()
		{
			view.uiFlashHolder.Style.Display = "none";
		}
		#endregion

		#region show/hide photo
		private void hidePhoto()
		{
			view.uiPhotoHolder.Style.Display = "none";
		}

		private void showPhoto()
		{
			view.uiPhoto.Src = "/gfx/1pix-black.gif";
			Window.SetTimeout(showPhoto2, 0);
		}
		private void showPhoto2()
		{
			//Script.Literal("debugger");
			PhotoStub photo = CurrentPhoto;
			view.uiPhoto.Src = photo.webPath;
			view.uiPhoto.Style.Height = photo.height + "px";
			view.uiPhoto.Style.Width = photo.width + "px";
			view.uiPhoto.Style.Display = "";
			view.uiPhotoHolder.Style.Display = "";
			if (overlayEnabled)
			{
				if (photo.width < 500 && photo.height < 500)
					view.uiPhotoOverlay.Style.Left = (((600 - photo.width) / 2)).ToString() + "px";
				else
					view.uiPhotoOverlay.Style.Right = (((600 - photo.width) / 2)).ToString() + "px";
			}
		}
		#endregion

		#region photo banner
		private BannerStub[] banners;
		private int currentBannerIndex;

		private void showNewBanner()
		{
			if (!disableBanner)
			{
				if (banners == null)
				{
					getBannersAndSetCurrentBanner();
				}
				else
				{
					currentBannerIndex++;
					if (currentBannerIndex >= banners.Length)
						currentBannerIndex = 0;
					showBanner();
				}
			}
		}
		private void showBanner()
		{
			if (banners[currentBannerIndex] == null)
			{
				view.uiBannerHolder.Style.Display = "none";
				return;
			}
			view.uiBannerHolder.Style.Display = "";
			if (banners[currentBannerIndex].html.Length > 0)
			{
				view.uiBannerPlaceHolder.InnerHTML = banners[currentBannerIndex].html;
			}
			else
			{
				view.uiBannerPlaceHolder.InnerHTML = "";
				Script.Eval(banners[currentBannerIndex].script);
			}

			Service.RegisterBannerHit(banners[currentBannerIndex].k, null, null, null, -1);
		}
		private void getBannersAndSetCurrentBanner()
		{
			Service.GetBanners(view.uiBannerPlaceHolder.ID, getNewBannerHtmlAndRegisterHitSuccessCallback, Trace.WebServiceFailure, null, -1);
		}
		private void getNewBannerHtmlAndRegisterHitSuccessCallback(BannerStub[] banners, object context, string methodName)
		{
			this.banners = banners;
			this.currentBannerIndex = 0;
			this.showBanner();
		}
		#endregion

	}

}
