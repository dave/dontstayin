//		<!--img ID="uiPhoto" runat="server" style="cursor: pointer;" class="BorderBlack All" src="<%# WebPath %>" 
//			width="<%# Photo == null ? 0 : Photo.WebWidth %>" height="<%# Photo == null ? 0 : Photo.WebHeight %>" /-->

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
using SpottedLibrary.Controls.PhotoControl;
using Bobs;
using System.Collections.Generic;
using Common;
using SpottedLibrary.Controls.TaggingControl;
using SpottedLibrary.Controls.BuddyChooser;
using Bobs.DataHolders;
using Spotted.Master;

namespace Spotted.Controls
{
	[ClientScript]
	public partial class PhotoControl : EnhancedUserControl, IPhotoControlView, IPhotoControl, IIncludesJs
	{
		PhotoControlController controller;

		public PhotoControl()
		{
			controller = new PhotoControlController(this);
			this.PreRender += new EventHandler(PhotoControl_PreRender);
		}

		public void IncludeJsInternal() { IncludeJs(this.Page); }
		public static void IncludeJs(Page page)
		{
			ScriptSharp.RegisterInclude(page, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		public void Page_Load(object o, EventArgs e)
		{
			
			if (!UsrSpotted)
			{
				uiUseAsProfilePictureButton.Attributes["onclick"] = "return confirm('Are you sure you are in this photo?');";
			}
			this.uiDisplayMakeFrontPageOptions.Value = DisplayMakeFrontPageOptions.ToString();
			this.uiUsrIsLoggedIn.Value = (Usr.Current != null).ToString();
			this.uiDisableBanner.Value = (Prefs.HideBanners).ToString();
			this.uiFirstTimeLoading.Value = bool.TrueString;

			this.uiCompetitionPanel.Style["display"] = Photo != null && Photo.CanEnterCompetition ? "" : "none";
			//this.uiPhotoOfWeekUserP.Style["display"] = Photo != null && !Photo.BlockedFromPhotoOfWeekUser && Common.Settings.FrontPagePhotos == Settings.FrontPagePhotosOption.On ? "" : "none";
			this.uiPhoto.Style["display"] = IsPhoto ? "" : "none";
			this.uiFlashHolder.Style["display"] = IsVideo ? "" : "none";
			this.uiPhotoGalleryLinkHolder.Style["display"] = GallerySelected ? "none" : "";

			uiPhotoOverlay.Visible = false;
			uiOverlayEnabled.Value = "false";
			if (Photo != null && !ContainerPage.SslPage && Vars.UrlScheme != "https" && Common.Settings.TakeoverStatus == Common.Settings.TakeoverStatusOption.On || (Usr.Current != null && Usr.Current.IsAdmin && Common.Settings.TakeoverStatus == Common.Settings.TakeoverStatusOption.Test))
			{
				if (Common.Settings.TakeoverPhotoOverlayStatus == Settings.TakeoverPhotoOverlayStatusOption.On)
				{
					uiPhotoOverlay.Visible = true;
					uiPhotoOverlay.Style["width"] = Common.Settings.TakeoverPhotoOverlayWidth.ToString() + "px";
					uiPhotoOverlay.Style["height"] = Common.Settings.TakeoverPhotoOverlayHeight.ToString() + "px";
					uiPhotoOverlay.Style["top"] = "-" + (Common.Settings.TakeoverPhotoOverlayHeight + (Vars.IE ? 2 : 6)).ToString() + "px";
					if (Photo.WebWidth < 500 && Photo.WebHeight < 500)
						uiPhotoOverlay.Style["left"] = (((600 - Photo.WebWidth) / 2)).ToString() + "px";
					else
						uiPhotoOverlay.Style["right"] = (((600 - Photo.WebWidth) / 2)).ToString() + "px";
					uiPhotoOverlay.InnerHtml = Common.Settings.TakeoverPhotoOverlayHtml;
					uiOverlayEnabled.Value = "true";
					uiOverlayWidth.Value = Common.Settings.TakeoverPhotoOverlayWidth.ToString();
					uiOverlayHeight.Value = Common.Settings.TakeoverPhotoOverlayHeight.ToString();
				}
			}

			if (Vars.IsCompetitionActive)
			{
				Group g = new Group(Vars.CompetitionGroupK);
				uiCompetitionGroupLink.InnerHtml = g.Name;
				uiCompetitionGroupLink.HRef = g.Url();

				uiAddToCompetitionGroupImg.Width = Common.Settings.CompetitionButtonWidth;
				uiAddToCompetitionGroupImg.Height = Common.Settings.CompetitionButtonHeight;
				uiAddToCompetitionGroupImg.Src = Photo != null && Photo.IsInCompetitionGroup ? Common.Settings.CompetitionButtonRemoveUrl : Common.Settings.CompetitionButtonAddUrl;

				uiAddToCompetitionGroupImgAddButtonUrl.Value = Common.Settings.CompetitionButtonAddUrl;
				uiAddToCompetitionGroupImgRemoveButtonUrl.Value = Common.Settings.CompetitionButtonRemoveUrl;
			}

		}
		DsiPage ContainerPage
		{
			get
			{
				return (DsiPage)Page;
			}
		}

		public bool GallerySelected { get; set; }

		public bool IncludeEventInfoInAboutHtml { private get; set; }

		void PhotoControl_PreRender(object sender, EventArgs e)
		{
			if (this.Photo != null)
			{
				Photo.IncrementViews();
				Photo.Gallery.SetGalleryUsr(Photo.Gallery.LivePhotos);
				


				//width:<%#  Photo == null ? 0 : Photo.VideoMedWidth %>px;height:<%# Photo == null ? 0 : (Photo.VideoMedHeight + 20) %>px;
				uiFlashHolder.Style["width"] = (Photo == null ? "0" : Photo.VideoMedWidth.ToString()) + "px";
				uiFlashHolder.Style["height"] = (Photo == null ? "0" : (Photo.VideoMedHeight + 20).ToString()) + "px";
			}

			uiBannerHolder.Style["display"] =
				BannerPhoto.BannerServerBanner != null && !Prefs.HideBanners ? "" : "none";

			uiAddToGroupLink.HRef = PhotoUrl + "/addtogroup";
			uiSendLink.HRef = PhotoUrl + "/send";
			uiReportLink.HRef = PhotoUrl + "/report";

			if (Photo != null)
			{
				if (Page is DsiPage)
				{
					DsiPage p = (DsiPage)Page;

					if (Photo.ArticleK > 0 && Photo.Article != null)
					{
						p.MetaTitle.Content = Photo.Article.Title;
						p.MetaDescription.Content = Photo.Article.Summary;
					}
					else
					{
						p.MetaTitle.Content = Photo.Event.Name;
						p.MetaDescription.Content = "From " + Cambro.Web.Helpers.StripHtml(Photo.Event.FriendlyHtml());
					}
					
					
					p.MetaMedium.Content = Photo.MediaType == Model.Entities.Photo.MediaTypes.Image ? "image" : "video";
					p.LinkImageSrc.Href = Photo.WebPath;
					if (Photo.MediaType == Model.Entities.Photo.MediaTypes.Video)
					{
						p.LinkVideoSrc.Href = "http://www.dontstayin.com/misc/flvplayer.swf?file=" + HttpUtility.UrlEncode(Photo.VideoMedPath);
						p.MetaVideoHeight.Content = Photo.VideoMedHeight.ToString();
						p.MetaVideoWidth.Content = Photo.VideoMedWidth.ToString();
						p.MetaVideoType.Content = "application/x-shockwave-flash";
					}
					
				}
			}
		}

		public event EventHandler<EventArgs<Photo>> PhotoSet;

		protected override void OnInit(EventArgs initArgs)
		{
			base.OnInit(initArgs);
			//this.uiUsrSpottedToggleButton.Click += (s, e) => { if (this.UsrSpottedSet != null) this.UsrSpottedSet(this, new EventArgs<bool>(!this.UsrSpotted)); };
			//this.uiIsFavouritePhotoToggleButton.Click += (s, e) => { if (this.IsFavouritePhotoSet != null) this.IsFavouritePhotoSet(this, new EventArgs<bool>(!this.IsFavouritePhoto)); };
			//this.uiBuddySpottedButton.Click += (s, e) => { if (uiBuddyValidator.IsValid) { if (this.BuddySpottedInPhoto != null) { this.BuddySpottedInPhoto(this, null); }; } };
			//this.uiSavePhotoOfWeek.Click += (s, e) => { if (this.PhotoOfWeekToggled != null) this.PhotoOfWeekToggled(this, new EventArgs<bool>(this.uiPhotoOfWeekCheckBox.Checked)); };

			// these events aren't fired if javascript library intercepts action
			//this.uiPrevPhotoButton.Click += (s, e) => { if (this.PrevButtonClick != null) PrevButtonClick(this, null); };
			//this.uiPhoto.Click += (s, e) => { if (this.PhotoClick != null) this.PhotoClick(this, null); };
			//this.uiNextPhotoButton.Click += (s, e) => { if (this.NextButtonClick != null) this.NextButtonClick(this, null); };
		}


		public bool IsValid { get { return true; } }


		//public ITaggingControl TaggingControl { get { return this.uiTaggingControl; } }


		#region original state data, for non-scripting browsers / google cache
		public string EventInfoHtml { get { return Photo != null ? Photo.EventInfoHtml : ""; } }
		public string ArticleInfoHtml { get { return Photo != null ? Photo.ArticleInfoHtml : ""; } }
		protected string TakenByDetailsHtml { get { return Photo != null ? Photo.TakenByDetailsHtml(IncludeEventInfoInAboutHtml) : ""; } }
		protected string UsrLink { get { return Photo == null ? "" : Photo.Usr.Link(); } }
		protected string Link { get { return Photo == null ? "" : Photo.Link; } }

		protected string PhotoVideoLabel { get { return Photo == null ? "" : Photo.PhotoVideoLabel; } }
		protected string EmbedHtml { get { return Photo == null ? "" : Photo.EmbedHtml; } }
		protected bool IsPhoto { get { return Photo == null ? false : Photo.IsPhoto; } }
		protected bool IsVideo { get { return Photo == null ? false : Photo.IsVideo; } }

		protected string DownloadPhotoLinkHtml { get { return Photo == null ? "" : Photo.DownloadPhotoLinkHtml; } }
		protected string QuickBrowserUrl { get { return Photo == null ? "" : Photo.QuickBrowserUrl; } }
		public string UsrsInPhotoHtml
		{
			set
			{
				uiUsrsInPhotoSpan.Style["display"] = value.Length > 0 ? "" : "none";
				uiUsrsInPhotoSpan.InnerHtml = value;
			}
		}

		public int? CurrentUsrK { get { return (Usr.Current == null) ? null : (int?)Usr.Current.K; } }

		public string WebPath { get { return Photo == null ? "" : Photo.WebPath; } }
		protected bool ShowAddToGroupTopPhotos { get { return CurrentUsrK != null && Usr.Current.IsGroupModerator; } }
		protected string PhotoUrl { get { return Photo == null ? "" : Photo.Url(); } }
		protected string PhotoMasterGuid { get { return Photo == null ? "" : Photo.Master.ToString(); } }
		protected int PhotoUsrK { get { return Photo == null ? 0 : Photo.Usr.K; } }

		#endregion


		Photo photo;
		public Photo Photo
		{
			get
			{
				if (photo == null)
				{
					int? photoK = ViewState["PhotoK"] as int?;
					if (photoK != null) { photo = new Photo(photoK.Value); }
				}
				return photo;
			}
			set
			{
				photo = value;
				ViewState["PhotoK"] = value == null ? (int?) null : value.K;
				if (this.PhotoSet != null) { PhotoSet(this, new EventArgs<Photo>(value)); }
			}
		}
		public bool UsrSpotted
		{
			get
			{
				return ViewState["UsrSpotted"] as bool? ?? false;
			}
			set { ViewState["UsrSpotted"] = value; }
		}

		public bool IsFavouritePhoto
		{
			get
			{
				return ViewState["IsFavouritePhoto"] as bool? ?? false;
			}
			set { ViewState["IsFavouritePhoto"] = value; }
		}




		public string Title { protected get { return ViewState["Title"] as string; } set { ViewState["Title"] = value; } }
 

		public IBuddyChooser BuddyChooser
		{
			get { return this.uiBuddyChooser; }
		}
		public string Url
		{
			get { return Request.Url.AbsoluteUri; }
		}
		public void Redirect(string url)
		{
			Response.Redirect(url);
		}
		//public event EventHandler PhotoClick;
		//public event EventHandler NextButtonClick;
		//public event EventHandler PrevButtonClick;
		public event EventHandler BuddySpottedInPhoto;
		public event EventHandler<EventArgs<bool>> IsFavouritePhotoSet;
		public event EventHandler<EventArgs<bool>> UsrSpottedSet;

		protected void uiBuddyValidator_ServerValidate1(object source, ServerValidateEventArgs args)
		{
			args.IsValid = uiBuddyChooser.SelectedBuddyK > 0; 
		}



		public bool DisplayMakeFrontPageOptions { protected get; set; }
	


		//public event EventHandler<EventArgs<bool>>  PhotoOfWeekToggled;


		//public string PhotoOfWeekCaption
		//{
		//    get { return this.uiPhotoOfWeekCaptionTextBox.Text; }
		//}

		#region UseAsProfilePictureClick
		public void UseAsProfilePictureClick(object o, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to do this!");
			Usr.Current.PhotoMe(Photo, true, null);
			Response.Redirect(UrlInfo.PageUrl("type", "pic", "mypicture", "k", Photo.K.ToString()));
		}
		#endregion

		public void DisplayAdminOptions()
		{
			var ContainerPage = (DsiPage)this.Page;
			ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"" + Photo.Gallery.UrlApp("edit") + "\">Edit gallery</a></p>"));
			if (Photo.ThreadK > 0)
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/thread?ID=" + Photo.ThreadK + "\">Edit thread</a></p>"));
			ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a onclick=\"return confirm('Are you sure?');\" href=\"/admin/disablecontent?PhotoK=" + Photo.K + "\">Disable content</a></p>"));
			ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a onclick=\"return confirm('This will delete ALL attached objects.\\nARE YOU SURE?');\" href=\"/admin/multidelete?ObjectType=Photo&ObjectK=" + Photo.K + "\">Delete this photo</a></p>"));
			ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"" + Photo.MasterPath + "\" target=\"_blank\">Master</a>, "));
			ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<a href=\"" + Photo.WebPath + "\" target=\"_blank\">Web</a>, "));
			ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<a href=\"" + Photo.ThumbPath + "\" target=\"_blank\">Thumb</a>, "));
			ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<a href=\"" + Photo.IconPath + "\" target=\"_blank\">Icon</a></p>"));
			if (Photo.EnabledByUsrK > 0)
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p>Photo enabled by <a href=\"" + Photo.EnabledByUsr.Url() + "\">" + Photo.EnabledByUsr.NickNameSafe + "</a>, " + Photo.EnabledDateTime.ToString() + "</p>"));
		}
	}
}
