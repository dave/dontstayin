using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Pages.Promoters
{
	public partial class BannerEdit : PromoterUserControl
	{

		#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.SetPageTitle("Banner administration");

			Usr.KickUserIfNotLoggedIn();
			if (!Usr.Current.IsPromoter && !Usr.Current.IsAdmin)
			{
				throw new DsiUserFriendlyException("You must be a promoter to view this page");
			}
			if (Edit)
			{
				if (!Usr.Current.CanEdit(CurrentBanner))
					throw new DsiUserFriendlyException("You can't edit this banner!");
			}
			if (Copy)
			{
				if (!Usr.Current.CanEdit(CopyBanner))
					throw new DsiUserFriendlyException("You can't copy this banner!");
			}
			if (Add)
			{
				if (!Usr.Current.IsEnabledPromoter(CurrentPromoter.K) && !Usr.Current.IsAdmin)
					throw new DsiUserFriendlyException("You can't add a banner here!");
			}
			if (Mode.Equals(Modes.Delete))
			{
				if (!Usr.Current.CanDelete(CurrentBanner))
					throw new DsiUserFriendlyException("You can't delete this banner!");
			}

			if (!Page.IsPostBack)
			{
				if (Edit)
				{
					LoadFormFromBanner(CurrentBanner);
				}
				else if (Copy)
				{
					LoadFormFromBanner(CopyBanner);
				}

				ViewState["BannerFolderDuplicateGuid"] = Guid.NewGuid();
				ViewState["BannerDuplicateGuid"] = Guid.NewGuid();
			}

			UpdateLockedItems();
		}
		#endregion

		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs e)
		{
			Update();
		}
		#endregion

		#region LoadFormFromBanner(Banner b)
		void LoadFormFromBanner(Banner b)
		{
			LinkEventRadio.Checked = b.LinkTarget.Equals(Banner.LinkTargets.Event);
			LinkBrandRadio.Checked = b.LinkTarget.Equals(Banner.LinkTargets.Brand);
			LinkVenueRadio.Checked = b.LinkTarget.Equals(Banner.LinkTargets.Venue);
			LinkTicketsRadio.Checked = b.LinkTarget.Equals(Banner.LinkTargets.TicketsBrand) || b.LinkTarget.Equals(Banner.LinkTargets.TicketsVenue);
			LinkCustomRadio.Checked = b.LinkTarget.Equals(Banner.LinkTargets.InternalUrl) || b.LinkTarget.Equals(Banner.LinkTargets.ExternalUrl);

			if (b.LinkTarget.Equals(Banner.LinkTargets.Event) && HasLinkEventDropDown)
				LinkEventDropDown.SelectedValue = GetLinkEventDropDownValue(b.Event);

			if (b.LinkTarget.Equals(Banner.LinkTargets.Brand) && HasLinkBrandDropDown)
				LinkBrandDropDown.SelectedValue = b.BrandK.ToString();

			if (b.LinkTarget.Equals(Banner.LinkTargets.Venue) && HasLinkVenueDropDown)
				LinkBrandDropDown.SelectedValue = b.VenueK.ToString();

			if (b.LinkTarget.Equals(Banner.LinkTargets.TicketsBrand) && HasLinkTicketsDropDown)
				LinkTicketsDropDown.SelectedValue = "Brand-" + b.BrandK.ToString();

			if (b.LinkTarget.Equals(Banner.LinkTargets.TicketsVenue) && HasLinkTicketsDropDown)
				LinkTicketsDropDown.SelectedValue = "Venue-" + b.VenueK.ToString();

			if (b.LinkTarget.Equals(Banner.LinkTargets.InternalUrl) || b.LinkTarget.Equals(Banner.LinkTargets.ExternalUrl))
				LinkCustomTextBox.Text = b.LinkUrl;

			PositionHotboxRadio.Checked = b.Position.Equals(Banner.Positions.Hotbox);
			PositionLeaderboardRadio.Checked = b.Position.Equals(Banner.Positions.Leaderboard);
			PositionSkyscraperRadio.Checked = b.Position.Equals(Banner.Positions.Skyscraper);
			PositionPhotoBannerRadio.Checked = b.Position.Equals(Banner.Positions.PhotoBanner);
			PositionEmailBannerRadio.Checked = b.Position.Equals(Banner.Positions.EmailBanner);

			Dates1WeekRadio.Checked = b.AutomaticDates && b.AutomaticDatesWeeks == 1 && DisplayDates1WeekSpan();
			Dates2WeekRadio.Checked = b.AutomaticDates && b.AutomaticDatesWeeks == 2 && DisplayDates2WeekSpan();
			Dates3WeekRadio.Checked = b.AutomaticDates && b.AutomaticDatesWeeks == 3 && DisplayDates3WeekSpan();
			Dates4WeekRadio.Checked = b.AutomaticDates && b.AutomaticDatesWeeks == 4 && DisplayDates4WeekSpan();
			DatesCustomRadio.Checked = !b.AutomaticDates;
			if (DatesCustomRadio.Checked)
			{
				DatesStartCal.Date = b.FirstDay;
				DatesEndCal.Date = b.LastDay;
			}

			ExposureLightRadio.Checked = b.AutomaticExposure && b.AutomaticExposureLevel.Equals(Banner.ExposureLevels.Light);
			ExposureMediumRadio.Checked = b.AutomaticExposure && b.AutomaticExposureLevel.Equals(Banner.ExposureLevels.Medium);
			ExposureHeavyRadio.Checked = b.AutomaticExposure && b.AutomaticExposureLevel.Equals(Banner.ExposureLevels.Heavy);
			ExposureCustomRadio.Checked = !b.AutomaticExposure;
			ImpressionsTextBox.Text = b.TotalRequiredImpressions.ToString("#,##0");

			

			bool placeTargetting = b.IsPlaceTargetted && b.Places.Count > 0;
			bool musicTargetting = b.IsMusicTargetted && b.MusicTypesChosen.Count > 0 && !(b.MusicTypesChosen.Count == 1 && b.MusicTypesChosen[0].K == 1);

			TargettingAutomaticRadio.Checked = b.AutomaticTargetting;
			TargettingNoneRadio.Checked = !b.AutomaticTargetting && !musicTargetting && !placeTargetting;
			TargettingCustomRadio.Checked = !b.AutomaticTargetting && (musicTargetting || placeTargetting);

			if (!b.AutomaticTargetting && !musicTargetting && !placeTargetting)
			{
				LocationTargettingHidden.Value = "";
				LocationTargettingTextBox.Text = "no towns";
				MusicTargettingHidden.Value = "";
				MusicTargettingTextBox.Text = "no music types";
			}
			else
			{
				LocationTargettingHidden.Value = placeTargetting ? string.Join(", ", b.Places.ToList().ConvertAll(p => p.K.ToString()).ToArray()) : "0";
				LocationTargettingTextBox.Text = (placeTargetting ? b.Places.Count.ToString() : "all") + " town" + (b.Places.Count == 1 ? "" : "s");

				MusicTargettingHidden.Value = musicTargetting ? String.Join(",", b.MusicTypesChosen.ToList().ConvertAll(mt => mt.K.ToString()).ToArray()) : "1";
				MusicTargettingTextBox.Text = (musicTargetting ? b.MusicTypesChosen.Count.ToString() : "all") + " music type" + (musicTargetting && b.MusicTypesChosen.Count == 1 ? "" : "s");
			}

			ArtworkUploadRadio.Checked = b.DesignType.Equals(Banner.DesignTypes.None) && (b.DisplayType.Equals(Banner.DisplayTypes.Jpg) || b.DisplayType.Equals(Banner.DisplayTypes.AnimatedGif) || b.DisplayType.Equals(Banner.DisplayTypes.FlashMovie));
			ArtworkGifRadio.Checked = b.DesignType.Equals(Banner.DesignTypes.Gif);
			ArtworkJpgRadio.Checked = b.DesignType.Equals(Banner.DesignTypes.Jpg);
			ArtworkFlashRadio.Checked = b.DesignType.Equals(Banner.DesignTypes.Flash);
			ArtworkAutomaticRadio.Checked = b.DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner) || b.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner);
			if (ArtworkAutomaticRadio.Checked)
			{
				AutomaticEventBannerHidden.Value = b.GetAutomaticBannerTextXml();
				if (b.DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner))
					AutomaticEventBannerTextBox.Text = "automatic text from event";
				else
					AutomaticEventBannerTextBox.Text = "customised text";
			}

			NameTextBox.Text = b.Name;

			FolderActionEventRadio.Checked = b.BannerFolder.EventK == b.EventK;
			
		}
		#endregion
		#region UpdateLockedItems()
		void UpdateLockedItems()
		{
			
			if (LockedPosition)
				PositionLockedLabel.Text = CurrentBanner.PositionString(true);

			if (LockedDates)
				DatesLockedLabel.Text = Cambro.Misc.Utility.FriendlyDate(CurrentBanner.FirstDay, true) + " - " + Cambro.Misc.Utility.FriendlyDate(CurrentBanner.LastDay, false);

			if (LockedExposure)
			{
				ExposureLockedLabel.Text = CurrentBanner.ExposureDescription;
			}
			
			if (LockedArtwork)
				ArtworkLockedLabel.Text = CurrentBanner.ArtworkString(true);

			if (LockedToEventLink)
			{
				if (UpcomingEvents.Count > 1)
				{
					LinkEventLockedSpan.InnerHtml = @"<img src=""/gfx/icon-lock.png"" alt=""Locked"" align=""absmiddle"" />My upcoming event...";
				}
				else if (UpcomingEvents.Count == 1)
				{
					LinkEventLockedSpan.InnerHtml = @"<img src=""/gfx/icon-lock.png"" alt=""Locked"" align=""absmiddle"" />My upcoming event: <a href=""" + UpcomingEvents[0].Url() + "\" target=\"_blank\">" + Cambro.Misc.Utility.Snip(UpcomingEvents[0].Name, 20) + "</a>"
						+ " @ <a href=\"" + UpcomingEvents[0].Venue.Url() + "\" target=\"_blank\">" + Cambro.Misc.Utility.Snip(UpcomingEvents[0].Venue.Name, 15) + "</a>"
						+ ", " + UpcomingEvents[0].FriendlyDate(false);
				}
				
			}
			
		}
		#endregion

		#region SaveToDatabase()
		protected void SaveToDatabase()
		{
			if (Page.IsValid)
			{

				Banner b;
				if (CurrentBanner == null)
				{
					Query q = new Query();
					q.QueryCondition = new Q(Banner.Columns.DuplicateGuid, (Guid)ViewState["BannerDuplicateGuid"]);
					BannerSet bs = new BannerSet(q);
					if (bs.Count == 0)
					{
						b = new Banner();
						b.UsrK = Usr.Current.K;
						b.StatusEnabled = true;
						//b.DateLastHit = DateTime.Today;
						b.PromoterK = CurrentPromoter.K;
						b.DuplicateGuid = (Guid)ViewState["BannerDuplicateGuid"];
					}
					else
					{
						Response.Redirect(bs[0].Url());
						return;
					}
				}
				else
				{
					b = CurrentBanner;
				}

				if (((IBuyableCredits)b).IsLocked)
					throw new DsiUserFriendlyException("It looks we're processing a payment for this banner... If not - wait a minute before trying again.");

				#region Link
				b.LinkTarget = GetSelectedLinkTarget();
				b.EventK = b.LinkTarget.Equals(Banner.LinkTargets.Event) ? CurrentEvent.K : 0;
				b.BrandK = b.LinkTarget.Equals(Banner.LinkTargets.Brand) || b.LinkTarget.Equals(Banner.LinkTargets.TicketsBrand) ? CurrentBrand.K : 0;
				b.VenueK = b.LinkTarget.Equals(Banner.LinkTargets.Venue) || b.LinkTarget.Equals(Banner.LinkTargets.TicketsVenue) ? CurrentVenue.K : 0;
				b.LinkUrl = b.LinkTarget.Equals(Banner.LinkTargets.InternalUrl) || b.LinkTarget.Equals(Banner.LinkTargets.ExternalUrl) ? LinkCustomTextBox.Text : "";
				#endregion
				#region Position
				if (!LockedPosition)
				{
					b.Position = GetSelectedBannerPosition();
				}
				#endregion
				#region Dates
				if (!LockedDates)
				{
					if (CustomDatesSelected)
					{
						b.AutomaticDates = false;
						b.AutomaticDatesWeeks = 0;
					}
					else
					{
						b.AutomaticDates = true;
						b.AutomaticDatesWeeks = GetSelectedAutomaticDatesWeeks();
					}
					b.FirstDay = GetSelectedFirstDay();
					b.LastDay = GetSelectedLastDay();
				}
				#endregion
				#region Exposure
				if (!LockedExposure)
				{
					int impressions;
					int credits;
					if (ExposureCustomRadio.Checked)
					{
						b.AutomaticExposure = false;
						b.AutomaticExposureLevel = Banner.ExposureLevels.None;

						impressions = int.Parse(ImpressionsTextBox.Text.Replace(",", ""));
						//credits = (int)Math.Ceiling((double)impressions / (double)GetImpressionsPerCredit());
					}
					else
					{
						b.AutomaticExposure = true;
						b.AutomaticExposureLevel = GetSelectedExposureLevel();

						credits = GetCreditsPerDay(b.AutomaticExposureLevel) * GetTotalDays();
						impressions = credits * GetImpressionsPerCredit();
					}
					b.TotalRequiredImpressions = impressions;
					//b.PriceStored = credits;
					//Should we be storing credits somewhere?
				}
				#endregion
				#region Targetting
				if (DisplayTargettingAutomaticSpan() && TargettingAutomaticRadio.Checked)
				{
					b.AutomaticTargetting = true;
					//we update the music / place targetting below (we need a banner k to do it)
				}
				else if (TargettingCustomRadio.Checked)
				{
					b.AutomaticTargetting = false;
					//we update the music / place targetting below (we need a banner k to do it)
				}
				else
				{
					b.AutomaticTargetting = false;
					b.IsMusicTargetted = false;
					b.IsPlaceTargetted = false;
				}
				#endregion
				#region Artwork
				if (!LockedArtwork)
				{
					b.DisplayType = GetSelectedDisplayType();
					b.DesignType = GetSelectedDesignType();

					if (DisplayArtworkAutomaticSpan() && ArtworkAutomaticRadio.Checked)
					{
						b.SetAutomaticBannerText(AutomaticEventBannerHidden.Value);
						if (!b.StatusArtwork && b.FirstDay < DateTime.Today) b.FirstDay = DateTime.Today;
						b.StatusArtwork = true;
					}

					if (Edit)
					{
						//Remove any non-relevant assigned files
						if (b.DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner) || b.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
						{
							b.MiscK = 0;
							b.NewMiscK = 0;
						}
						else if (b.Misc != null && !b.Misc.DisplayType.Equals(CurrentBanner.DisplayType))
						{
							b.MiscK = 0;
						}
					}
				}
				#endregion
				#region Name
				b.Name = Cambro.Web.Helpers.Strip(NameTextBox.Text);
				#endregion
				#region Folder
				if (DisplayFolderActionEventSpan() && FolderActionEventRadio.Checked)
				{
					Query q = new Query();
					q.QueryCondition = new And(
						new Q(BannerFolder.Columns.EventK, CurrentEvent.K),
						new Q(BannerFolder.Columns.PromoterK, CurrentPromoter.K));
					BannerFolderSet bfs = new BannerFolderSet(q);
					if (bfs.Count == 0)
					{
						BannerFolder bf = new BannerFolder();
						bf.PromoterK = CurrentPromoter.K;
						bf.EventK = CurrentEvent.K;
						bf.Name = "Event: " + Cambro.Misc.Utility.Snip(CurrentEvent.Name, 30) + " @ " + Cambro.Misc.Utility.Snip(CurrentEvent.Venue.Name, 20) + ", " + CurrentEvent.DateTime.ToString("MMM dd yy");
						bf.DuplicateGuid = (Guid)ViewState["BannerFolderDuplicateGuid"];
						bf.DateTimeCreated = DateTime.Now;
						bf.Update();
						b.BannerFolderK = bf.K;
					}
					else
						b.BannerFolderK = bfs[0].K;
				}
				else if (FolderActionExistingRadio.Checked)
				{
					BannerFolder bf = new BannerFolder(int.Parse(FolderExistingDropDown.SelectedValue));
					if (bf.PromoterK != CurrentPromoter.K)
						throw new DsiUserFriendlyException("Selected banner folder isn't in your promoter account!");
					b.BannerFolderK = bf.K;
				}
				else if (FolderActionNewRadio.Checked)
				{
					Query q = new Query();
					q.QueryCondition = new And(
						new Q(BannerFolder.Columns.DuplicateGuid, (Guid)ViewState["BannerFolderDuplicateGuid"]));
					BannerFolderSet bfs = new BannerFolderSet(q);
					if (bfs.Count == 0)
					{
						BannerFolder bf = new BannerFolder();
						bf.PromoterK = CurrentPromoter.K;
						bf.Name = Cambro.Web.Helpers.Strip(FolderNewTextBox.Text);
						bf.DuplicateGuid = (Guid)ViewState["BannerFolderDuplicateGuid"];
						bf.DateTimeCreated = DateTime.Now;
						bf.Update();
						b.BannerFolderK = bf.K;	
					}
					else
					{
						if (bfs[0].PromoterK != CurrentPromoter.K)
							throw new DsiUserFriendlyException("Selected banner folder isn't in your promoter account!");
						b.BannerFolderK = bfs[0].K;
					}
				}
				#endregion

				#region if Copy - extra Properties to Copy from existing Banner
				if (Copy)
				{
					b.TargettingProperties0 = CopyBanner.TargettingProperties0;
					b.TargettingProperties1 = CopyBanner.TargettingProperties1;

					if (b.DesignType == Banner.DesignTypes.None)
					{
						if (!b.StatusArtwork && CopyBanner.StatusArtwork && b.FirstDay < DateTime.Today) b.FirstDay = DateTime.Today;
						b.StatusArtwork = CopyBanner.StatusArtwork;
						b.MiscK = CopyBanner.MiscK;
					}
				}
				#endregion

				b.Update();

				if (b.AutomaticTargetting)
				{
					b.IsPlaceTargetted = b.SavePlaceTargetting(CurrentEvent);
					b.IsMusicTargetted = b.SaveMusicTargetting(CurrentEvent);
					b.Update();
				}
				else if (TargettingCustomRadio.Checked)
				{
					b.IsPlaceTargetted = b.SavePlaceTargetting(new List<string>(LocationTargettingHidden.Value.Split(',')).ConvertAll(s => int.Parse(s.Trim())));
					b.IsMusicTargetted = b.SaveMusicTargetting(new List<string>(MusicTargettingHidden.Value.Split(',')).ConvertAll(s => int.Parse(s.Trim())));
					b.Update();
				}
				else
				{
					b.IsPlaceTargetted = b.SavePlaceTargetting(new List<int>(0));
					b.IsMusicTargetted = b.SaveMusicTargetting(new List<int>(1) { 1 });
					b.Update();
				}

                CurrentBanner = b;
                //if (Add || Copy)
                //{
                //    if (HasFixedCurrentEvent)
                //        Response.Redirect(CurrentPromoter.UrlEventOptions(CurrentEvent));
                //    else
                //        Response.Redirect(CurrentPromoter.UrlApp("banners"));
                //}
                //else
                //{
                //    if (HasFixedCurrentEvent)
                //        Response.Redirect(CurrentBanner.OptionsUrl("eventk", CurrentEvent.K.ToString()));
                //    else
						Response.Redirect(CurrentBanner.Url());
                //}
			}
		}
		#endregion

		#region Intro
		public void Intro_Load(object o, System.EventArgs e)
		{
			if (HasFixedCurrentEvent)
			{
				IntroBannerListLink.InnerText = "Back to the promoter event page";
				IntroBannerListLink.HRef = CurrentPromoter.UrlEventOptions(CurrentEvent);
			}
			else
				IntroBannerListLink.HRef = CurrentPromoter.UrlApp("banners");
		}
		#endregion
		#region Mode
		#region GetCurrentPanelIndex()
		int GetCurrentPanelIndex()
		{
			return int.Parse(CurrentPanelIndexHidden.Value);
		}
		#endregion
		#region Mode_Init
		protected void Mode_Init(object sender, EventArgs eventArgs)
		{
			if (!Page.IsPostBack)
			{
				CurrentPanelIndexHidden.Value = HasFixedCurrentEvent ? "1" : "0";

				ModeBeginnerRadio.Checked = false;
				ModeExpertRadio.Checked = true;
			//	ModeBeginnerRadio.Checked = !HideModeControls && (Prefs.Current["BannerWizardExpertMode"].IsNull || Prefs.Current["BannerWizardExpertMode"] == 0);
			//	ModeExpertRadio.Checked = !ModeBeginnerRadio.Checked;

				
			}
		}
		#endregion
		#region BeginnerMode
		bool BeginnerMode
		{
			get
			{
				return ModeBeginnerRadio.Checked;
			}
		}
		#endregion
		#region HideModeControls
		bool HideModeControls
		{
			get
			{
				return true;// Edit;
			}
		}
		#endregion
		#endregion
		#region Link
		#region Link_Init
		protected void Link_Init(object sender, EventArgs eventArgs)
		{
			if (!Page.IsPostBack)
			{
				if (!HasFixedCurrentEvent)
				{
					#region LinkEvent
					if (UpcomingEvents.Count > 1)
					{
						LinkEventRadio.Text = "My upcoming event...";
						LinkEventDropDownBind();
					}
					else if (UpcomingEvents.Count == 1)
					{
						LinkEventRadio.Text = "My upcoming event: <a href=\"" + UpcomingEvents[0].Url() + "\" target=\"_blank\">" + Cambro.Misc.Utility.Snip(UpcomingEvents[0].Name, 20) + "</a>"
							+ " @ <a href=\"" + UpcomingEvents[0].Venue.Url() + "\" target=\"_blank\">" + Cambro.Misc.Utility.Snip(UpcomingEvents[0].Venue.Name, 15) + "</a>"
							+ ", " + UpcomingEvents[0].FriendlyDate(false);
					}
					#endregion
					#region LinkBrand
					if (CurrentPromoter.AllBrands.Count > 1)
					{
						LinkBrandRadio.Text = "My brand page...";
						LinkBrandDropDownBind();
					}
					else if (CurrentPromoter.AllBrands.Count == 1)
					{
						LinkBrandRadio.Text = "My brand page: <a href=\"" + CurrentPromoter.AllBrands[0].Url() + "\" target=\"_blank\">" + Cambro.Misc.Utility.Snip(CurrentPromoter.AllBrands[0].Name, 20) + "</a>";
					}
					#endregion
					#region LinkVenue
					if (CurrentPromoter.AllVenues.Count > 1)
					{
						LinkVenueRadio.Text = "My venue page...";
						LinkVenueDropDownBind();
					}
					else if (CurrentPromoter.AllVenues.Count == 1)
					{
						LinkVenueRadio.Text = "My venue page: <a href=\"" + CurrentPromoter.AllVenues[0].Url() + "\" target=\"_blank\">" + Cambro.Misc.Utility.Snip(CurrentPromoter.AllVenues[0].Name, 20) + "</a>";
					}
					#endregion
					#region LinkTickets
					if ((CurrentPromoter.AllBrands.Count + CurrentPromoter.AllVenues.Count) > 1)
					{
						LinkTicketsRadio.Text = "My tickets page...";
						LinkTicketsDropDownBind();
					}
					else if ((CurrentPromoter.AllBrands.Count + CurrentPromoter.AllVenues.Count) == 1)
					{
						if (CurrentPromoter.AllVenues.Count == 1)
							LinkTicketsRadio.Text = "My tickets page: <a href=\"" + CurrentPromoter.AllVenues[0].UrlApp("tickets") + "\" target=\"_blank\">" + Cambro.Misc.Utility.Snip(CurrentPromoter.AllVenues[0].Name, 20) + "</a>";
						else
							LinkTicketsRadio.Text = "My tickets page: <a href=\"" + CurrentPromoter.AllBrands[0].UrlApp("tickets") + "\" target=\"_blank\">" + Cambro.Misc.Utility.Snip(CurrentPromoter.AllBrands[0].Name, 20) + "</a>";
					}
					#endregion
				}
			}
		}
		#endregion

		

		#region UpcomingEvents
		public EventSet UpcomingEvents
		{
			get
			{
				if (upcomingEvents == null)
				{
					if (Edit && CurrentBanner.EventK > 0)
						upcomingEvents = CurrentPromoter.GetUpcomingEvents(CurrentBanner.EventK, false);
					else
						upcomingEvents = CurrentPromoter.GetUpcomingEvents(false);
				}
				return upcomingEvents;
			}
			set
			{
				upcomingEvents = value;
			}
		}
		private EventSet upcomingEvents;
		#endregion

		#region GetLinkEventDropDownValue(Event e)
		string GetLinkEventDropDownValue(Event e)
		{
			return e.K.ToString() + "," + e.DateTime.Year.ToString() + "," + (e.DateTime.Month - 1).ToString() + "," + e.DateTime.Day.ToString();
		}
		#endregion

		#region LinkEventDropDownBind()
		void LinkEventDropDownBind()
		{
			UpcomingEvents = null;
			LinkEventDropDown.Items.Clear();
			LinkEventDropDown.Items.Add(new ListItem("", "0"));
			foreach (Event e in UpcomingEvents)
			{
				LinkEventDropDown.Items.Add(new ListItem(Cambro.Misc.Utility.Snip(e.Name, 20) + " @ " + Cambro.Misc.Utility.Snip(e.Venue.Name, 10) + ", " + e.FriendlyDate(false), GetLinkEventDropDownValue(e)));
			}
		}
		#endregion
		#region LinkBrandDropDownBind()
		void LinkBrandDropDownBind()
		{
			LinkBrandDropDown.DataSource = CurrentPromoter.AllBrands;
			LinkBrandDropDown.DataValueField = "K";
			LinkBrandDropDown.DataTextField = "Name";
			LinkBrandDropDown.DataBind();
			LinkBrandDropDown.Items.Insert(0,new ListItem("", "0"));
		}
		#endregion
		#region LinkVenueDropDownBind()
		void LinkVenueDropDownBind()
		{
			LinkVenueDropDown.DataSource = CurrentPromoter.AllVenues;
			LinkVenueDropDown.DataValueField = "K";
			LinkVenueDropDown.DataTextField = "Name";
			LinkVenueDropDown.DataBind();
			LinkVenueDropDown.Items.Insert(0,new ListItem("", "0"));
		}
		#endregion
		#region LinkTicketsDropDownBind()
		void LinkTicketsDropDownBind()
		{
			LinkTicketsDropDown.Items.Clear();
			LinkTicketsDropDown.Items.Add(new ListItem("", "0"));
			foreach (Venue v in CurrentPromoter.AllVenues)
			{
				LinkTicketsDropDown.Items.Add(new ListItem(Cambro.Misc.Utility.Snip(v.Name, 20) + " (venue)", "Venue-" + v.K.ToString()));
			}
			foreach (Brand b in CurrentPromoter.AllBrands)
			{
				LinkTicketsDropDown.Items.Add(new ListItem(Cambro.Misc.Utility.Snip(b.Name, 20) + " (brand)", "Brand-" + b.K.ToString()));
			}
		}
		#endregion

		#endregion
		#region Position
		#region Position_Init
		protected void Position_Init(object sender, EventArgs eventArgs)
		{
			
		}
		#endregion
		#endregion
		#region Dates
		#region Dates_Init
		protected void Dates_Init(object sender, EventArgs eventArgs)
		{
			
			//if (CurrentEvent == null || CurrentEvent.DateTime < DateTime.Today)
			//{
			//    DatesAutoRow.Style["display"] = "none";
			//}
			//else if (CurrentEvent.DateTime.AddDays(-7) < DateTime.Today)
			//{
			//    Dates2WeekSpan.Style["display"] = "none";
			//    Dates4WeekSpan.Style["display"] = "none";
			//}
			//else if (CurrentEvent.DateTime.AddDays(-14) < DateTime.Today)
			//{
			//    Dates4WeekSpan.Style["display"] = "none";
			//}
		}
		#endregion
		#endregion
		#region Exposure
		#region Exposure_Init
		protected void Exposure_Init(object sender, EventArgs eventArgs)
		{
			
		}
		#endregion
		#endregion
		#region Targetting
		#region Targetting_Init
		protected void Targetting_Init(object sender, EventArgs eventArgs)
		{
			LocationTargettingButton.Attributes["onclick"] = "openPopupFocusSize('/popup/bannereditlocation?placek=' + escape(document.getElementById('" + LocationTargettingHidden.ClientID + "').value), 620, 680);return false;";
			MusicTargettingButton.Attributes["onclick"] = "openPopupFocusSize('/popup/bannereditmusic?musictypek=' + escape(document.getElementById('" + MusicTargettingHidden.ClientID + "').value), 500, 600);return false;";
		}
		#endregion
		#endregion
		#region Artwork
		#region Artwork_Init
		protected void Artwork_Init(object sender, EventArgs eventArgs)
		{
			AutomaticEventBannerButton.Attributes["onclick"] = "openPopupFocusSize('/popup/bannereditautomatic?eventk=' + CurrentEventK + '&position=' + GetSelectedBannerPosition() + '&text=' + escape(document.getElementById('" + AutomaticEventBannerHidden.ClientID + "').value), 770, 450);return false;";
		}
		#endregion
		#endregion
		#region Name
		#region Name_Init
		protected void Name_Init(object sender, EventArgs eventArgs)
		{
			
		}
		#endregion
		#endregion
		#region Folder
		#region Folder_Init
		protected void Folder_Init(object sender, EventArgs eventArgs)
		{
			if (!Page.IsPostBack)
			{
				FolderExistingDropDown.Items.Clear();
				FolderExistingDropDown.Items.Add(new ListItem("", "0"));

				Query q = new Query();
				q.TopRecords = 50;
				q.QueryCondition = new Q(BannerFolder.Columns.PromoterK, CurrentPromoter.K);
				q.OrderBy = new OrderBy(BannerFolder.Columns.DateTimeCreated, OrderBy.OrderDirection.Descending);
				BannerFolderSet bfs = new BannerFolderSet(q);
				
				bool found = false;
				foreach (BannerFolder bf in bfs)
				{
					if (Edit && bf.K == CurrentBanner.BannerFolderK)
						found = true;
					FolderExistingDropDown.Items.Add(new ListItem(Cambro.Misc.Utility.Snip(bf.Name, 40), bf.K.ToString()));
				}
				if (Edit && !found)
					FolderExistingDropDown.Items.Add(new ListItem(Cambro.Misc.Utility.Snip(CurrentBanner.BannerFolder.Name, 40), CurrentBanner.BannerFolderK.ToString()));

				if (Edit)
				{
					if (CurrentBanner.EventK > 0 && CurrentBanner.BannerFolder.EventK == CurrentBanner.EventK)
					{
						FolderActionEventRadio.Checked = true;
					}
					else
					{
						FolderActionExistingRadio.Checked = true;
						FolderExistingDropDown.SelectedValue = CurrentBanner.BannerFolderK.ToString();
					}
				}
			}
		}
		#endregion
		#endregion
		#region Book
		#region Book_Init
		protected void Book_Init(object sender, EventArgs eventArgs)
		{
			
		}
		#endregion
		#endregion

		#region Validators
		#region LinkVal
		protected void LinkVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				HasFixedCurrentEvent ||
				(HasLinkEvents && (LockedToEventLink || LinkEventRadio.Checked) && (!HasLinkEventDropDown || LinkEventDropDown.SelectedValue != "0")) ||
				(HasLinkBrands && LinkBrandRadio.Checked && (!HasLinkBrandDropDown || LinkBrandDropDown.SelectedValue != "0")) ||
				(HasLinkVenues && LinkVenueRadio.Checked && (!HasLinkVenueDropDown || LinkVenueDropDown.SelectedValue != "0")) ||
				(HasLinkTickets && LinkTicketsRadio.Checked && (!HasLinkTicketsDropDown || LinkTicketsDropDown.SelectedValue != "0")) ||
				LinkCustomRadio.Checked;
		}
		#endregion
		#region LinkCustomVal
		protected void LinkCustomVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				!LinkCustomRadio.Checked ||
				(LinkCustomTextBox.Text.StartsWith("http://") || LinkCustomTextBox.Text.StartsWith("https://"));
		}
		#endregion
		#region PositionVal
		protected void PositionVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				LockedPosition ||
				PositionHotboxRadio.Checked ||
				PositionLeaderboardRadio.Checked ||
				PositionSkyscraperRadio.Checked ||
				PositionPhotoBannerRadio.Checked ||
				PositionEmailBannerRadio.Checked;
		}
		#endregion
		#region DatesVal
		protected void DatesVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				LockedDates ||
				!DisplayDatesAutoRow() ||
				(DisplayDates1WeekSpan() && Dates1WeekRadio.Checked) ||
				(DisplayDates2WeekSpan() && Dates2WeekRadio.Checked) ||
				(DisplayDates3WeekSpan() && Dates3WeekRadio.Checked) ||
				(DisplayDates4WeekSpan() && Dates4WeekRadio.Checked) ||
				DatesCustomRadio.Checked;
		}
		#endregion
		#region DatesCustomVal
		protected void DatesCustomVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				LockedDates ||
				(DisplayDatesAutoRow() && !DatesCustomRadio.Checked) ||
				CustomDatesAreComplete();
		}
		#endregion
		#region CustomDatesAreComplete()
		protected bool CustomDatesAreComplete()
		{
			return DatesStartCal.DateValid && DatesEndCal.DateValid;
		}
		#endregion
		#region DatesCustomEndDateVal
		protected void DatesCustomEndDateVal(object s, ServerValidateEventArgs args)
		{
			if (!LockedDates && (!DisplayDatesAutoRow() || DatesCustomRadio.Checked) && CustomDatesAreComplete())
				args.IsValid = DatesEndCal.Date >= DateTime.Today;
			else
				args.IsValid = true;
		}
		#endregion
		#region ExposureVal
		protected void ExposureVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				LockedExposure ||
				ExposureLightRadio.Checked ||
				ExposureMediumRadio.Checked ||
				ExposureHeavyRadio.Checked ||
				ExposureCustomRadio.Checked;
		}
		#endregion
		#region ExposureCustomVal
		protected void ExposureCustomVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				LockedExposure ||
				!ExposureCustomRadio.Checked ||
				CustomExposureIsOk();
		}
		#endregion
		#region CustomExposureIsOk()
		protected bool CustomExposureIsOk()
		{
			try
			{
				int impressions = int.Parse(ImpressionsTextBox.Text.Replace(",", string.Empty));
				int days = GetTotalDays();
				if (days == 0)
					return true;
				double impressionsPerDay = (double)impressions / (double)days;
				return impressionsPerDay > 900.0;
			}
			catch
			{
				return false;
			}
		}
		#endregion
		#region ArtworkVal
		protected void ArtworkVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				LockedArtwork ||
				ArtworkUploadRadio.Checked ||
				ArtworkJpgRadio.Checked ||
				ArtworkGifRadio.Checked ||
				(DisplayArtworkFlashSpan() && ArtworkFlashRadio.Checked) ||
				(DisplayArtworkAutomaticSpan() && ArtworkAutomaticRadio.Checked);
		}
		#endregion
		#region TargettingVal
		protected void TargettingVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				(DisplayTargettingAutomaticSpan() && TargettingAutomaticRadio.Checked) ||
				TargettingNoneRadio.Checked ||
				TargettingCustomRadio.Checked;
		}
		#endregion
		#region TargettingCustomVal
		protected void TargettingCustomVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				!TargettingCustomRadio.Checked ||
				(LocationTargettingHidden.Value != "" && MusicTargettingHidden.Value != "");
		}
		#endregion
		#region FolderVal
		protected void FolderVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				(DisplayFolderActionEventSpan() && FolderActionEventRadio.Checked) ||
				(FolderActionExistingRadio.Checked && FolderExistingDropDown.SelectedValue != "0") ||
				(FolderActionNewRadio.Checked && FolderNewTextBox.Text != "");
		}
		#endregion
		#region BookVal
		protected void BookVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				BookNowCreditsRadio.Checked ||
				BookNowCashRadio.Checked ||
				BookLaterRadio.Checked;
		}
		#endregion
		#endregion

		#region DoneClick
		protected void DoneClick(object sender, EventArgs eventArgs)
		{
			SaveToDatabase();
		}
		#endregion

        protected void CancelButton_Click(object sender, EventArgs eventArgs)
        {
			if (Mode == Modes.Add)
			{
				Response.Redirect(CurrentPromoter.UrlApp("banners"));
			}
			else if (Mode == Modes.Copy)
			{
				Response.Redirect(CopyBanner.Url());
			}
			else
			{
				Response.Redirect(CurrentBanner.Url());
			}
        }

		#region Javascript helpers

		#region CurrentDate
		DateTime CurrentDate
		{
			get
			{
				return DateTime.Today;
			}
		}
		#endregion

		#region CurrentDateString
		protected string CurrentDateString
		{
			get
			{
				return CurrentDate.Year + ", " + (CurrentDate.Month - 1).ToString() + ", " + CurrentDate.Day;
			}
		}
		#endregion

		#region CurrentEventDate
		DateTime CurrentEventDate
		{
			get
			{
				return CurrentEvent != null ? CurrentEvent.DateTime.Date : new DateTime(1900,1,1);
			}
		}
		#endregion

		#region CurrentEventDateString
		protected string CurrentEventDateString
		{
			get
			{
				return CurrentEventDate.Year + ", " + (CurrentEventDate.Month - 1).ToString() + ", " + CurrentEventDate.Day;
			}
		}
		#endregion

		#region CurrentEventK
		protected int CurrentEventK
		{
			get
			{
				return CurrentEvent != null ? CurrentEvent.K : 0;
			}
		}
		#endregion

		#region HasFixedCurrentEvent
		protected bool HasFixedCurrentEvent
		{
			get
			{
				return ContainerPage.Url["EventK"].IsInt;
			}
		}
		#endregion

		#region HasCurrentEvent
		protected bool HasCurrentEvent
		{
			get
			{
				return CurrentEvent != null;
			}
		}
		#endregion

		#region HasLinkXXX
		protected bool HasLinkEventDropDown{ get{ return UpcomingEvents.Count > 1; } }
		protected bool HasLinkBrandDropDown { get { return CurrentPromoter.AllBrands.Count > 1; } }
		protected bool HasLinkVenueDropDown { get { return CurrentPromoter.AllVenues.Count > 1; } }
		protected bool HasLinkTicketsDropDown { get { return CurrentPromoter.AllBrands.Count + CurrentPromoter.AllVenues.Count > 1; } }
		protected bool HasLinkEvents { get { return UpcomingEvents.Count > 0; } }
		protected bool HasLinkBrands { get { return CurrentPromoter.AllBrands.Count > 0; } }
		protected bool HasLinkVenues { get { return CurrentPromoter.AllVenues.Count > 0; } }
		protected bool HasLinkTickets { get { return CurrentPromoter.AllBrands.Count + CurrentPromoter.AllVenues.Count > 0; } }
		#endregion

		#region SingleUpcomingEvent
		protected bool HasSingleUpcomingEvent { get { return UpcomingEvents.Count == 1; } }
		protected int SingleUpcomingEventK { get { return UpcomingEvents.Count == 1 ? UpcomingEvents[0].K : 0; } }
		protected string SingleUpcomingEventDateString { get { return UpcomingEvents.Count == 1 ? (UpcomingEvents[0].DateTime.Year + ", " + (UpcomingEvents[0].DateTime.Month - 1).ToString() + ", " + UpcomingEvents[0].DateTime.Day) : "2007,0,1"; } }
		#endregion

		#region LockedXXX
		protected bool LockedToEventLink { get { return CurrentBanner != null && CurrentBanner.IsPriceLocked && (CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner) || CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner)); } }
		protected bool LockedPosition { get { return CurrentBanner != null && CurrentBanner.IsPriceLocked; } }
		protected bool LockedDates { get { return CurrentBanner != null && CurrentBanner.IsPriceLocked; } }
		protected bool LockedExposure { get { return CurrentBanner != null && CurrentBanner.IsPriceLocked; } }
		protected bool LockedArtwork { get { return CurrentBanner != null && (CurrentBanner.IsPriceLocked || CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.CustomHtml)); } }
		#endregion

		#region DiscountCreditsString
		protected string DiscountCreditsString
		{
			get
			{
				return string.Join(", ", new List<int>(CampaignCredit.DiscountCredits).ConvertAll(a => a.ToString()).ToArray());
			}
		}
		#endregion

		#region DiscountLevelsString
		protected string DiscountLevelsString
		{
			get
			{
				return string.Join(", ", new List<double>(CampaignCredit.DiscountLevels).ConvertAll(a => (a * 100.0).ToString("0.00")).ToArray());
			}
		}
		#endregion

		#endregion

		#region Functions to maintain the state accross postbacks

		#region DisplayXXX functions
		bool DisplayModePanel() { return !HideModeControls; }

		bool DisplayBackNextPanel() { return BeginnerMode; }
		bool DisplayLinkPanel() { return ((!BeginnerMode && !HasFixedCurrentEvent) || (BeginnerMode && GetCurrentPanelIndex() == 0)); }
		bool DisplayPositionPanel() { return (!BeginnerMode || (BeginnerMode && GetCurrentPanelIndex() == 1)); }
		bool DisplayDatesPanel() { return !BeginnerMode || GetCurrentPanelIndex() == 2; }
		bool DisplayExposurePanel() { return !BeginnerMode || GetCurrentPanelIndex() == 3; }
		bool DisplayTargettingPanel() { return !BeginnerMode || GetCurrentPanelIndex() == 4; }
		bool DisplayArtworkPanel() { return !BeginnerMode || GetCurrentPanelIndex() == 5; }
		bool DisplayNamePanel() { return !BeginnerMode || GetCurrentPanelIndex() == 6; }
		bool DisplayFolderPanel() { return !BeginnerMode || GetCurrentPanelIndex() == 7; }
		bool DisplayBookPanel() { return false; }// !BeginnerMode || GetCurrentPanelIndex() == 8; }

		bool DisplayLinkEventLockedSpan() { return LockedToEventLink; }
		bool DisplayLinkEventDropDown() { return !HasFixedCurrentEvent && HasLinkEventDropDown && LinkEventRadio.Checked; }
		bool DisplayLinkBrandDropDown() { return !HasFixedCurrentEvent && HasLinkBrandDropDown && LinkBrandRadio.Checked; }
		bool DisplayLinkVenueDropDown() { return !HasFixedCurrentEvent && HasLinkVenueDropDown && LinkVenueRadio.Checked; }
		bool DisplayLinkTicketsDropDown() { return !HasFixedCurrentEvent && HasLinkTicketsDropDown && LinkTicketsRadio.Checked; }
		bool DisplayLinkCustomTextBox() { return !HasFixedCurrentEvent && LinkCustomRadio.Checked; }

		bool DisplayLinkEventRadio() { return !LockedToEventLink && HasLinkEvents; }
		
		bool DisplayLinkEventSpan() { return HasLinkEvents; }
		bool DisplayLinkBrandSpan() { return !LockedToEventLink && HasLinkBrands; }
		bool DisplayLinkVenueSpan() { return !LockedToEventLink && HasLinkVenues; }
		bool DisplayLinkTicketsSpan() { return !LockedToEventLink && HasLinkTickets; }
		bool DisplayLinkCustomSpan() { return !LockedToEventLink; }

		bool DisplayPositionLockedRow() { return LockedPosition; }
		bool DisplayPositionRow() { return !LockedPosition; }

		bool DisplayDatesLockedRow() { return LockedDates; }
		bool DisplayDatesAutoRow() { return !LockedDates && HasCurrentEvent && CurrentEventDate >= CurrentDate; }
		bool DisplayDates1WeekSpan() { return !LockedDates && DisplayDatesAutoRow(); }
		bool DisplayDates2WeekSpan() { return !LockedDates && CurrentEventDate.AddDays(-7) >= CurrentDate; }
		bool DisplayDates3WeekSpan() { return !LockedDates && CurrentEventDate.AddDays(-14) >= CurrentDate; }
		bool DisplayDates4WeekSpan() { return !LockedDates && CurrentEventDate.AddDays(-21) >= CurrentDate; }
		bool DisplayCustomDatesRows() { return !LockedDates && ((DisplayDatesAutoRow() && DatesCustomRadio.Checked) || !DisplayDatesAutoRow()); }

		bool DisplayExposureLockedRow() { return LockedExposure; }
		bool DisplayExposureRow() { return !LockedExposure; }
		bool DisplayImpressionsRow() { return !LockedExposure && ExposureCustomRadio.Checked; }
		bool DisplayExposureDetailsRow() { return !LockedExposure; }

		bool DisplayTargettingAutomaticSpan() { return HasCurrentEvent; }
		bool DisplayTargettingRows() { return TargettingCustomRadio.Checked; }

		bool DisplayArtworkLockedRow() { return LockedArtwork; }
		bool DisplayArtworkRow() { return !LockedArtwork; }

		bool DisplayArtworkFlashSpan() { return !LockedArtwork && !PositionEmailBannerRadio.Checked; }
		bool DisplayArtworkAutomaticSpan() { return !LockedArtwork && HasCurrentEvent && (PositionLeaderboardRadio.Checked || PositionPhotoBannerRadio.Checked); }
		bool DisplayAutomaticArtworkRow() {return (DisplayArtworkAutomaticSpan() || LockedArtwork) && ArtworkAutomaticRadio.Checked; }
		bool DisplayFolderExistingDropDown() { return FolderActionExistingRadio.Checked; }
		bool DisplayFolderNewTextBox() { return FolderActionNewRadio.Checked; }
		bool DisplayFolderActionEventSpan() { return HasCurrentEvent; }

		#endregion

		#region Update
		void Update()
		{
			ModePanel.Style["display"] = DisplayModePanel() ? null : "none";

			BackNextPanel.Style["display"] = DisplayBackNextPanel() ? null : "none";
			LinkPanel.Style["display"] = DisplayLinkPanel() ? null : "none";
			PositionPanel.Style["display"] = DisplayPositionPanel() ? null : "none";
			DatesPanel.Style["display"] = DisplayDatesPanel() ? null : "none";
			ExposurePanel.Style["display"] = DisplayExposurePanel() ? null : "none";
			TargettingPanel.Style["display"] = DisplayTargettingPanel() ? null : "none";
			ArtworkPanel.Style["display"] = DisplayArtworkPanel() ? null : "none";
			NamePanel.Style["display"] = DisplayNamePanel() ? null : "none";
			FolderPanel.Style["display"] = DisplayFolderPanel() ? null : "none";
			BookPanel.Style["display"] = DisplayBookPanel() ? null : "none";

			LinkEventLockedSpan.Style["display"] = DisplayLinkEventLockedSpan() ? null : "none";
			LinkEventDropDown.Style["display"] = DisplayLinkEventDropDown() ? null : "none";
			LinkBrandDropDown.Style["display"] = DisplayLinkBrandDropDown() ? null : "none";
			LinkVenueDropDown.Style["display"] = DisplayLinkVenueDropDown() ? null : "none";
			LinkTicketsDropDown.Style["display"] = DisplayLinkTicketsDropDown() ? null : "none";
			LinkCustomTextBox.Style["display"] = DisplayLinkCustomTextBox() ? null : "none";

			LinkEventRadio.Style["display"] = DisplayLinkEventRadio() ? null : "none";

			LinkEventSpan.Style["display"] = DisplayLinkEventSpan() ? null : "none";
			LinkBrandSpan.Style["display"] = DisplayLinkBrandSpan() ? null : "none";
			LinkVenueSpan.Style["display"] = DisplayLinkVenueSpan() ? null : "none";
			LinkTicketsSpan.Style["display"] = DisplayLinkTicketsSpan() ? null : "none";
			LinkCustomSpan.Style["display"] = DisplayLinkCustomSpan() ? null : "none";

			PositionLockedRow.Style["display"] = DisplayPositionLockedRow() ? null : "none";
			PositionRow.Style["display"] = DisplayPositionRow() ? null : "none";

			DatesLockedRow.Style["display"] = DisplayDatesLockedRow() ? null : "none";
			DatesAutoRow.Style["display"] = DisplayDatesAutoRow() ? null : "none";
			Dates1WeekSpan.Style["display"] = DisplayDates1WeekSpan() ? null : "none";
			Dates2WeekSpan.Style["display"] = DisplayDates2WeekSpan() ? null : "none";
			Dates3WeekSpan.Style["display"] = DisplayDates3WeekSpan() ? null : "none";
			Dates4WeekSpan.Style["display"] = DisplayDates4WeekSpan() ? null : "none";
			DatesStartDateRow.Style["display"] = DisplayCustomDatesRows() ? null : "none";
			DatesEndDateRow.Style["display"] = DisplayCustomDatesRows() ? null : "none";

			ExposureLockedRow.Style["display"] = DisplayExposureLockedRow() ? null : "none";
			ExposureRow.Style["display"] = DisplayExposureRow() ? null : "none";
			ExposureDetailsRow.Style["display"] = DisplayExposureDetailsRow() ? null : "none";
			ImpressionsRow.Style["display"] = DisplayImpressionsRow() ? null : "none";

			TargettingAutomaticSpan.Style["display"] = DisplayTargettingAutomaticSpan() ? null : "none";
			LocationTargettingRow.Style["display"] = DisplayTargettingRows() ? null : "none";
			MusicTargettingRow.Style["display"] = DisplayTargettingRows() ? null : "none";
			
			ArtworkLockedRow.Style["display"] = DisplayArtworkLockedRow() ? null : "none";
			ArtworkRow.Style["display"] = DisplayArtworkRow() ? null : "none";
			ArtworkFlashSpan.Style["display"] = DisplayArtworkFlashSpan() ? null : "none";
			ArtworkAutomaticSpan.Style["display"] = DisplayArtworkAutomaticSpan() ? null : "none";
			AutomaticArtworkRow.Style["display"] = DisplayAutomaticArtworkRow() ? null : "none";
			
			FolderExistingDropDown.Style["display"] = DisplayFolderExistingDropDown() ? null : "none";
			FolderNewTextBox.Style["display"] = DisplayFolderNewTextBox() ? null : "none";
			FolderActionEventSpan.Style["display"] = DisplayFolderActionEventSpan() ? null : "none";

			UpdatePrices();
		}
		#endregion

		#region UpdatePrices()
		void UpdatePrices()
		{

			int totalDays = GetTotalDays();
			int impressionsPerCredit = GetImpressionsPerCredit();
			Banner.Positions selectedBannerPosition = GetSelectedBannerPosition();

			if (totalDays == 0)
			{
				int dateError = GetDateError();

				if (dateError == 1)
				{
					ExposureLevelP.InnerHtml = "Error when calculating price - please check the dates.";
					ExposureCostCampaignCreditsP.InnerHtml = "Error when calculating price - please check the dates.";
					ExposureCostCashP.InnerHtml = "Error when calculating price - please check the dates.";
					return;
				}
				else if (dateError == 2)
				{
					ExposureLevelP.InnerHtml = "Please check your dates - the end date seems to be before the start date!";
					ExposureCostCampaignCreditsP.InnerHtml = "Please check your dates - the end date seems to be before the start date!";
					ExposureCostCashP.InnerHtml = "Please check your dates - the end date seems to be before the start date!";
					return;
				}
				else
				{
					ExposureLevelP.InnerHtml = "Choose some dates to work out your exposure.";
					ExposureCostCampaignCreditsP.InnerHtml = "Choose some dates to work out a price.";
					ExposureCostCashP.InnerHtml = "Choose some dates to work out a price.";
					return;
				}
			}

			if (ExposureCustomRadio.Checked)
			{
				int impressions = int.Parse(ImpressionsTextBox.Text.Replace(",", ""));
				int credits = (int)Math.Ceiling((double)impressions / (double)impressionsPerCredit);

				ExposureLevelP.InnerHtml = "Exposure: " + GetExposure(credits, totalDays);
				ExposureCostCampaignCreditsP.InnerHtml = selectedBannerPosition.Equals(Banner.Positions.None) ? "Select a banner position to work out a price." : "Price in campaign credits: " + credits.ToString("#,##0") + " credits";
				ExposureCostCashP.InnerHtml = selectedBannerPosition.Equals(Banner.Positions.None) ? "Select a banner position to work out a price." : "Cash price: £" + GetCashPrice(credits).ToString("#,##0.00") + (GetDiscount(credits) > 0 ? " (this includes a " + GetDiscount(credits) + "% discount)" : "");
			}
			else
			{
				Banner.ExposureLevels exposureLevel = GetSelectedExposureLevel();
				if (!exposureLevel.Equals(Banner.ExposureLevels.None))
				{
					int creditsPerDay = GetCreditsPerDay(exposureLevel);

					if (!selectedBannerPosition.Equals(Banner.Positions.None))
					{
						int credits = creditsPerDay * totalDays;
						int totalImpressions = credits * impressionsPerCredit;

						ImpressionsTextBox.Text = totalImpressions.ToString("#,##0");

						ExposureLevelP.InnerHtml = "Actual impressions: " + totalImpressions.ToString("#,##0");
						ExposureCostCampaignCreditsP.InnerHtml = "Price in campaign credits: " + credits.ToString("#,##0") + " credits";
						ExposureCostCashP.InnerHtml = "Cash price: £" + GetCashPrice(credits).ToString("#,##0.00") + (GetDiscount(credits) > 0 ? " (this includes a " + GetDiscount(credits) + "% discount)" : "");
					}
					else
					{
						ExposureLevelP.InnerHtml = "Select a banner position to work out your impressions.";
						ExposureCostCampaignCreditsP.InnerHtml = "Select a banner position to work out a price.";
						ExposureCostCashP.InnerHtml = "Select a banner position to work out a price.";
					}
				}
				else
				{
					ExposureLevelP.InnerHtml = "Choose an exposure level...";
					ExposureCostCampaignCreditsP.InnerHtml = "Choose an exposure level to work out a price.";
					ExposureCostCashP.InnerHtml = "Choose an exposure level to work out a price.";
				}
			}
		}
		#endregion

		#region GetSelectedLinkTarget()
		Banner.LinkTargets GetSelectedLinkTarget()
		{
			if (HasFixedCurrentEvent || LockedToEventLink || LinkEventRadio.Checked)
				return Banner.LinkTargets.Event;
			else if (LinkBrandRadio.Checked)
				return Banner.LinkTargets.Brand;
			else if (LinkVenueRadio.Checked)
				return Banner.LinkTargets.Venue;
			else if (LinkTicketsRadio.Checked && (LinkTicketsDropDown.SelectedValue.StartsWith("Brand-") || (CurrentPromoter.AllBrands.Count == 1 && CurrentPromoter.AllVenues.Count == 0)))
				return Banner.LinkTargets.TicketsBrand;
			else if (LinkTicketsRadio.Checked && (LinkTicketsDropDown.SelectedValue.StartsWith("Venue-") || (CurrentPromoter.AllBrands.Count == 0 && CurrentPromoter.AllVenues.Count == 1)))
				return Banner.LinkTargets.TicketsVenue;
			else if (LinkCustomRadio.Checked && (LinkCustomTextBox.Text.StartsWith("http://www.dontstayin.com") || LinkCustomTextBox.Text.StartsWith("https://www.dontstayin.com") || LinkCustomTextBox.Text.StartsWith("http://dontstayin.com") || LinkCustomTextBox.Text.StartsWith("https://dontstayin.com")))
				return Banner.LinkTargets.InternalUrl;
			else if (LinkCustomRadio.Checked && (LinkCustomTextBox.Text.StartsWith("http://") || LinkCustomTextBox.Text.StartsWith("https://")))
				return Banner.LinkTargets.ExternalUrl;
			else
				throw new DsiUserFriendlyException("Invalid LinkTarget selected");

		}
		#endregion

		#region GetSelectedDisplayType()
		Banner.DisplayTypes GetSelectedDisplayType()
		{
			if (ArtworkUploadRadio.Checked)
			{
				if (Add)
					return Banner.DisplayTypes.Jpg;
				else if (Copy)
				{
					if (CopyBanner.DisplayType.Equals(Banner.DisplayTypes.Jpg))
						return Banner.DisplayTypes.Jpg;
					else if (CopyBanner.DisplayType.Equals(Banner.DisplayTypes.AnimatedGif))
						return Banner.DisplayTypes.AnimatedGif;
					else if (CopyBanner.DisplayType.Equals(Banner.DisplayTypes.FlashMovie))
						return Banner.DisplayTypes.FlashMovie;
					else
						return Banner.DisplayTypes.Jpg;
				}
				else
				{
					if (CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.Jpg))
						return Banner.DisplayTypes.Jpg;
					else if (CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.AnimatedGif))
						return Banner.DisplayTypes.AnimatedGif;
					else if (CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.FlashMovie))
						return Banner.DisplayTypes.FlashMovie;
					else
						return Banner.DisplayTypes.Jpg;
				}
			}
			else if (DisplayArtworkAutomaticSpan() && ArtworkAutomaticRadio.Checked)
			{
				if (Add)
					return Banner.DisplayTypes.AutoEventBanner;
				if (Copy)
				{
					if (CopyBanner.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
						return Banner.DisplayTypes.CustomAutoEventBanner;
					else
						return Banner.DisplayTypes.AutoEventBanner;
				}
				else
				{
					if (CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
						return Banner.DisplayTypes.CustomAutoEventBanner;
					else
						return Banner.DisplayTypes.AutoEventBanner;
				}
			}
			else if (ArtworkJpgRadio.Checked)
				return Banner.DisplayTypes.Jpg;
			else if (ArtworkGifRadio.Checked)
				return Banner.DisplayTypes.AnimatedGif;
			else if (DisplayArtworkFlashSpan() && ArtworkFlashRadio.Checked)
				return Banner.DisplayTypes.FlashMovie;
			else
				throw new DsiUserFriendlyException("Invalid artwork selected");
		}
		#endregion

		#region GetSelectedDesignType()
		Banner.DesignTypes GetSelectedDesignType()
		{
			if (ArtworkJpgRadio.Checked)
				return Banner.DesignTypes.Jpg;
			else if (ArtworkGifRadio.Checked)
				return Banner.DesignTypes.Gif;
			else if (DisplayArtworkFlashSpan() && ArtworkFlashRadio.Checked)
				return Banner.DesignTypes.Flash;
			else
				return Banner.DesignTypes.None;
		}
		#endregion

		#region GetSelectedExposureLevel()
		Banner.ExposureLevels GetSelectedExposureLevel()
		{
			if (ExposureLightRadio.Checked)
				return Banner.ExposureLevels.Light;
			else if (ExposureMediumRadio.Checked)
				return Banner.ExposureLevels.Medium;
			else if (ExposureHeavyRadio.Checked)
				return Banner.ExposureLevels.Heavy;
			else
				return Banner.ExposureLevels.None;
		}
		#endregion

		#region GetSelectedBannerPosition()
		Banner.Positions GetSelectedBannerPosition()
		{
			if (PositionHotboxRadio.Checked)
				return Banner.Positions.Hotbox;
			else if (PositionLeaderboardRadio.Checked)
				return Banner.Positions.Leaderboard;
			else if (PositionSkyscraperRadio.Checked)
				return Banner.Positions.Skyscraper;
			else if (PositionPhotoBannerRadio.Checked)
				return Banner.Positions.PhotoBanner;
			else if (PositionEmailBannerRadio.Checked)
				return Banner.Positions.EmailBanner;
			else
				return Banner.Positions.None;
		}
		#endregion

		#region GetCashPrice(int credits)
		decimal GetCashPrice(int credits)
		{
			return credits * CurrentPromoter.CostPerCampaignCredit * (decimal)(1 - (GetDiscount(credits) / 100.0));
		}
		#endregion

		#region GetDiscount(int credits)
		int GetDiscount(int credits)
		{
			return (int)Math.Round(CampaignCredit.GetDiscountForCredits(credits, CurrentPromoter) * 100.0);
		}
		#endregion

		#region GetCreditsPerDay(Banner.ExposureLevels exposureLevel)
		int GetCreditsPerDay(Banner.ExposureLevels exposureLevel)
		{
			return Banner.GetCreditsPerDay(exposureLevel);
		}
		#endregion

		#region GetExposure(int credits, int days)
		//light: 15 credits/day (display 'light' when custom impressions makes credits < 20/day)
		//medium: 30 credits/day (display 'medium' when custom impressions makes credits < 40/day)
		//heavy: 50 credits/day
		string GetExposure(int credits, int days)
		{
			return Banner.GetEquivalentExposureLevelStatic(credits, days).ToString().ToLower();
			//var creditsPerDay = credits / days;
			//if (creditsPerDay < 20)
			//    return "light";
			//else if (creditsPerDay < 40)
			//    return "medium";
			//else
			//    return "heavy";
		}
		#endregion

		#region GetImpressionsPerCredit()
		int GetImpressionsPerCredit()
		{
			try
			{
				return Banner.GetImpressionsPerCampaignCredit(GetSelectedBannerPosition());
			}
			catch
			{
				return 0;
			}
		}
		#endregion

		#region GetTotalDays()
		bool CustomDatesSelected
		{
			get
			{
				return !DisplayDatesAutoRow() || DatesCustomRadio.Checked;
			}
		}
		int GetSelectedAutomaticDatesWeeks()
		{
			if (DisplayDates1WeekSpan() && Dates1WeekRadio.Checked) 
				return 1;
			else if (DisplayDates2WeekSpan() && Dates2WeekRadio.Checked)
				return 2;
			else if (DisplayDates3WeekSpan() && Dates3WeekRadio.Checked)
				return 3;
			else if (DisplayDates4WeekSpan() && Dates4WeekRadio.Checked)
				return 4;
			else
				return 0;
		}

		int GetDateError()
		{
			if (!DisplayDatesAutoRow() || DatesCustomRadio.Checked)
			{

				try
				{
					if (!DatesStartCal.DateValid || !DatesEndCal.DateValid)
						return 1;

					DateTime endDate = GetSelectedLastDay();
					DateTime startDate = GetSelectedFirstDay();

					if (startDate.Equals(DateTime.MinValue) || endDate.Equals(DateTime.MinValue))
						return 0;

					TimeSpan ts = endDate - startDate;
					int days = ts.Days;

					if (days < 0)
						return 2;
					else
						return 0;
				}
				catch
				{
					return 1;
				}
			}
			else
				return 0;
		}


		int GetTotalDays()
		{
			if (DisplayDatesAutoRow() && ((DisplayDates1WeekSpan() && Dates1WeekRadio.Checked) || (DisplayDates2WeekSpan() && Dates2WeekRadio.Checked) || (DisplayDates3WeekSpan() && Dates3WeekRadio.Checked) || (DisplayDates4WeekSpan() && Dates4WeekRadio.Checked)))
			{
				DateTime endDate = GetSelectedLastDay();
				DateTime startDate = GetSelectedFirstDay();

				TimeSpan ts = endDate - startDate;
				int days = ts.Days;

				if (days < 0)
					return 1;
				else
					return days + 1;
			}
			else if (!DisplayDatesAutoRow() || DatesCustomRadio.Checked)
			{
				try
				{
					if (!DatesStartCal.DateValid || !DatesEndCal.DateValid)
						return 0;

					DateTime endDate = GetSelectedLastDay();
					DateTime startDate = GetSelectedFirstDay();

					if (startDate.Equals(DateTime.MinValue) || endDate.Equals(DateTime.MinValue))
						return 0;

					TimeSpan ts = endDate - startDate;
					int days = ts.Days;

					if (days < 0)
						return 1;
					else
						return days + 1;
				}
				catch
				{
					return 0;
				}
			}
			else
				return 0;
		}
		DateTime GetSelectedFirstDay()
		{
			if (DisplayDatesAutoRow() && ((DisplayDates1WeekSpan() && Dates1WeekRadio.Checked) || (DisplayDates2WeekSpan() && Dates2WeekRadio.Checked) || (DisplayDates3WeekSpan() && Dates3WeekRadio.Checked) || (DisplayDates4WeekSpan() && Dates4WeekRadio.Checked)))
			{
				DateTime startDate = CurrentEventDate.AddDays(-7 * GetSelectedAutomaticDatesWeeks());
				
				if (startDate < CurrentDate)
					startDate = CurrentDate;

				return startDate;
			}
			else if (!DisplayDatesAutoRow() || DatesCustomRadio.Checked)
				return DatesStartCal.Date;
			else
				return DateTime.MinValue;
		}
		DateTime GetSelectedLastDay()
		{
			if (DisplayDatesAutoRow() && ((DisplayDates1WeekSpan() && Dates1WeekRadio.Checked) || (DisplayDates2WeekSpan() && Dates2WeekRadio.Checked) || (DisplayDates3WeekSpan() && Dates3WeekRadio.Checked) || (DisplayDates4WeekSpan() && Dates4WeekRadio.Checked)))
				return CurrentEventDate;
			else if (!DisplayDatesAutoRow() || DatesCustomRadio.Checked)
				return DatesEndCal.Date;
			else
				return DateTime.MinValue;
		}
		#endregion

		#endregion

		#region Wizard framework

		#region Mode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url["Mode"].IsNull)
					return Modes.Add;
				else if (ContainerPage.Url["Mode"].Equals("Add"))
					return Modes.Add;
				else if (ContainerPage.Url["Mode"].Equals("Edit"))
					return Modes.Edit;
				else if (ContainerPage.Url["Mode"].Equals("Copy"))
					return Modes.Copy;
				else if (ContainerPage.Url["Mode"].Equals("Delete"))
					return Modes.Delete;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			Add,
			Edit,
			Delete,
			Copy
		}
		#endregion

		#region Add
		protected bool Add
		{
			get
			{
				return Mode.Equals(Modes.Add);
			}
		}
		#endregion
		#region Edit
		protected bool Edit
		{
			get
			{
				return Mode.Equals(Modes.Edit) && CurrentBanner != null;
			}
		}
		#endregion
		#region Copy
		protected bool Copy
		{
			get
			{
				return Mode.Equals(Modes.Copy) && CopyBannerK > 0;
			}
		}
		#endregion

		#region BannerK
		int BannerK
		{
			get
			{
				return ContainerPage.Url["BannerK"];
			}
		}
		#endregion
		#region CurrentBanner
		public Banner CurrentBanner
		{
			get
			{
				if (currentBanner == null && BannerK > 0)
					currentBanner = new Banner(BannerK);

				return currentBanner;
			}
			set
			{
				currentBanner = value;
			}
		}
		Banner currentBanner;
		#endregion

		#region CopyBannerK
		int CopyBannerK
		{
			get
			{
				return ContainerPage.Url["CopyBannerK"];
			}
		}
		#endregion
		#region CopyBanner
		/// <summary>
		/// CopyBanner - purely a data holder for the banner info.
		/// Do NOT update this, it will update the original banner!
		/// </summary>
		public Banner CopyBanner
		{
			get
			{
				if (copyBanner == null && CopyBannerK > 0 && Copy)
				{
					copyBanner = new Banner(CopyBannerK);
					copyBanner.Name = "Copy of " + copyBanner.Name;
				}

				return copyBanner;
			}
			set
			{
				copyBanner = value;
			}
		}
		Banner copyBanner;
		#endregion

		#region CurrentVenue
		public Venue CurrentVenue
		{
			get
			{
				if (currentVenue == null)
				{
					if (LinkVenueRadio.Checked && CurrentPromoter.AllVenues.Count == 1)
						currentVenue = CurrentPromoter.AllVenues[0];
					else if (LinkVenueRadio.Checked && !LinkVenueDropDown.SelectedValue.Equals("0"))
						currentVenue = new Venue(int.Parse(LinkVenueDropDown.SelectedValue));
					else if (LinkTicketsRadio.Checked && CurrentPromoter.AllBrands.Count == 0 && CurrentPromoter.AllVenues.Count == 1)
						currentVenue = CurrentPromoter.AllVenues[0];
					else if (LinkTicketsRadio.Checked && LinkTicketsDropDown.SelectedValue.StartsWith("Venue-"))
						currentVenue = new Venue(int.Parse(LinkTicketsDropDown.SelectedValue.Split('-')[1]));

					if (currentVenue != null && currentVenue.PromoterK != CurrentPromoter.K)
						throw new DsiUserFriendlyException("Error: This venue is not in your promoter account.");
				}
				return currentVenue;
			}
		}
		Venue currentVenue;
		#endregion

		#region CurrentBrand
		public Brand CurrentBrand
		{
			get
			{
				if (currentBrand == null)
				{
					if (LinkBrandRadio.Checked && CurrentPromoter.AllBrands.Count == 1)
						currentBrand = CurrentPromoter.AllBrands[0];
					else if (LinkBrandRadio.Checked && !LinkBrandDropDown.SelectedValue.Equals("0"))
						currentBrand = new Brand(int.Parse(LinkBrandDropDown.SelectedValue));
					else if (LinkTicketsRadio.Checked && CurrentPromoter.AllBrands.Count == 1 && CurrentPromoter.AllVenues.Count == 0)
						currentBrand = CurrentPromoter.AllBrands[0];
					else if (LinkTicketsRadio.Checked && LinkTicketsDropDown.SelectedValue.StartsWith("Brand-"))
						currentBrand = new Brand(int.Parse(LinkTicketsDropDown.SelectedValue.Split('-')[1]));

					if (currentBrand != null && currentBrand.PromoterK != CurrentPromoter.K)
						throw new DsiUserFriendlyException("Error: This brand is not in your promoter account.");
				}
				return currentBrand;
			}
		}
		Brand currentBrand;
		#endregion

		#region CurrentEvent
		public Event CurrentEvent
		{
			get
			{
				if (currentEvent == null)
				{
					if (ContainerPage.Url["EventK"].IsInt)
						currentEvent = new Event(ContainerPage.Url["EventK"]);
					else if ((LockedToEventLink || LinkEventRadio.Checked) && UpcomingEvents.Count == 1)
						currentEvent = UpcomingEvents[0];
					else if ((LockedToEventLink || LinkEventRadio.Checked) && !LinkEventDropDown.SelectedValue.Equals("0"))
						currentEvent = new Event(int.Parse(LinkEventDropDown.SelectedValue.Split(',')[0]));

					if (currentEvent!=null && !currentEvent.IsPromoter(CurrentPromoter.K))
						throw new DsiUserFriendlyException("Error: This event is not in your promoter account.");
				}
				return currentEvent;
			}
		}
		Event currentEvent;
		#endregion

		#endregion

		#region OnInit(EventArgs e)
		protected override void OnInit(EventArgs e)
		{
			this.Init += new EventHandler(Mode_Init);
			this.Init += new EventHandler(Link_Init);
			this.Init += new EventHandler(Dates_Init);
			this.Init += new EventHandler(Position_Init);
			this.Init += new EventHandler(Exposure_Init);
			this.Init += new EventHandler(Targetting_Init);
			this.Init += new EventHandler(Artwork_Init);
			this.Init += new EventHandler(Name_Init);
			this.Init += new EventHandler(Folder_Init);
			this.Init += new EventHandler(Book_Init);

			this.Load += new EventHandler(Intro_Load);

		//	this.PreRender += new EventHandler(Page_PreRender);

			base.OnInit(e);
		}
		#endregion

	}
}
