using System;
using System.Collections.Generic;
using System.Text;
using Bobs.BannerServer;

namespace Bobs.DataHolders
{
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct DataHolder class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.
	#region BannerDataHolder
	/// <summary>
	/// Advertising - includes banners, hotboxes etc.DataHolder
	/// </summary>
	[Serializable]
	public partial class BannerDataHolder : DataHolder<Banner>, IBannerDesiredHitsRequiredInformation
	{
		[NonSerializedAttribute]
		Banner bob;

		public BannerDataHolder()
		{
			this.dataHolder = new Banner();
		}
		void CopyValues(Banner source, Banner destination)
		{
			destination[Bobs.Banner.Columns.K] = source[Bobs.Banner.Columns.K];
			//destination[Bobs.Banner.Columns.Name] = source[Bobs.Banner.Columns.Name];
			destination[Bobs.Banner.Columns.FirstDay] = source[Bobs.Banner.Columns.FirstDay];
			destination[Bobs.Banner.Columns.LastDay] = source[Bobs.Banner.Columns.LastDay];
			//destination[Bobs.Banner.Columns.EventK] = source[Bobs.Banner.Columns.EventK];
			//destination[Bobs.Banner.Columns.DisplayType] = source[Bobs.Banner.Columns.DisplayType];
			//destination[Bobs.Banner.Columns.MiscK] = source[Bobs.Banner.Columns.MiscK];
			//destination[Bobs.Banner.Columns.MiscGuid] = source[Bobs.Banner.Columns.MiscGuid];
			//destination[Bobs.Banner.Columns.CustomHtml] = source[Bobs.Banner.Columns.CustomHtml];
			//destination[Bobs.Banner.Columns.CustomXml] = source[Bobs.Banner.Columns.CustomXml];
			destination[Bobs.Banner.Columns.Position] = source[Bobs.Banner.Columns.Position];
			//destination[Bobs.Banner.Columns.Status] = source[Bobs.Banner.Columns.Status];
			//destination[Bobs.Banner.Columns.AdminNote] = source[Bobs.Banner.Columns.AdminNote];
			//destination[Bobs.Banner.Columns.UsrK] = source[Bobs.Banner.Columns.UsrK];
			//destination[Bobs.Banner.Columns.PromoterK] = source[Bobs.Banner.Columns.PromoterK];
			//destination[Bobs.Banner.Columns.BrandK] = source[Bobs.Banner.Columns.BrandK];
			//destination[Bobs.Banner.Columns.LinkUrl] = source[Bobs.Banner.Columns.LinkUrl];
			//destination[Bobs.Banner.Columns.IsMusicTargetted] = source[Bobs.Banner.Columns.IsMusicTargetted];
			//destination[Bobs.Banner.Columns.IsPlaceTargetted] = source[Bobs.Banner.Columns.IsPlaceTargetted];
			//destination[Bobs.Banner.Columns.LinkTarget] = source[Bobs.Banner.Columns.LinkTarget];
			//destination[Bobs.Banner.Columns.IsPriceFixed] = source[Bobs.Banner.Columns.IsPriceFixed];
			//destination[Bobs.Banner.Columns.PriceStored] = source[Bobs.Banner.Columns.PriceStored];
			//destination[Bobs.Banner.Columns.NewMiscK] = source[Bobs.Banner.Columns.NewMiscK];
			//destination[Bobs.Banner.Columns.CustomiseFirstLine] = source[Bobs.Banner.Columns.CustomiseFirstLine];
			//destination[Bobs.Banner.Columns.CustomiseFirstLineSize] = source[Bobs.Banner.Columns.CustomiseFirstLineSize];
			//destination[Bobs.Banner.Columns.CustomiseSecondLine] = source[Bobs.Banner.Columns.CustomiseSecondLine];
			//destination[Bobs.Banner.Columns.CustomiseThirdLine] = source[Bobs.Banner.Columns.CustomiseThirdLine];
			//destination[Bobs.Banner.Columns.FailedMiscK] = source[Bobs.Banner.Columns.FailedMiscK];
			//destination[Bobs.Banner.Columns.DesignType] = source[Bobs.Banner.Columns.DesignType];
			//destination[Bobs.Banner.Columns.BuyableLockDateTime] = source[Bobs.Banner.Columns.BuyableLockDateTime];
			//destination[Bobs.Banner.Columns.DesignProcessed] = source[Bobs.Banner.Columns.DesignProcessed];
			destination[Bobs.Banner.Columns.FrequencyCapPerIdentifierPerDay] = source[Bobs.Banner.Columns.FrequencyCapPerIdentifierPerDay];
			//destination[Bobs.Banner.Columns.TargettingProperties0] = source[Bobs.Banner.Columns.TargettingProperties0];
			//destination[Bobs.Banner.Columns.TargettingProperties1] = source[Bobs.Banner.Columns.TargettingProperties1];
			//destination[Bobs.Banner.Columns.LastTimestamp] = source[Bobs.Banner.Columns.LastTimestamp];
			destination[Bobs.Banner.Columns.TotalRequiredImpressions] = source[Bobs.Banner.Columns.TotalRequiredImpressions];
			//destination[Bobs.Banner.Columns.BannerFolderK] = source[Bobs.Banner.Columns.BannerFolderK];
			//destination[Bobs.Banner.Columns.VenueK] = source[Bobs.Banner.Columns.VenueK];
			//destination[Bobs.Banner.Columns.AutomaticDates] = source[Bobs.Banner.Columns.AutomaticDates];
			//destination[Bobs.Banner.Columns.AutomaticDatesWeeks] = source[Bobs.Banner.Columns.AutomaticDatesWeeks];
			//destination[Bobs.Banner.Columns.AutomaticTargetting] = source[Bobs.Banner.Columns.AutomaticTargetting];
			//destination[Bobs.Banner.Columns.AutomaticExposure] = source[Bobs.Banner.Columns.AutomaticExposure];
			//destination[Bobs.Banner.Columns.AutomaticExposureLevel] = source[Bobs.Banner.Columns.AutomaticExposureLevel];
			//destination[Bobs.Banner.Columns.StatusEnabled] = source[Bobs.Banner.Columns.StatusEnabled];
			//destination[Bobs.Banner.Columns.StatusBooked] = source[Bobs.Banner.Columns.StatusBooked];
			//destination[Bobs.Banner.Columns.StatusArtwork] = source[Bobs.Banner.Columns.StatusArtwork];
			//destination[Bobs.Banner.Columns.Refunded] = source[Bobs.Banner.Columns.Refunded];
			//destination[Bobs.Banner.Columns.RefundedCredits] = source[Bobs.Banner.Columns.RefundedCredits];
			//destination[Bobs.Banner.Columns.RefundCampaignCreditK] = source[Bobs.Banner.Columns.RefundCampaignCreditK];
			//destination[Bobs.Banner.Columns.DuplicateGuid] = source[Bobs.Banner.Columns.DuplicateGuid];
			//destination[Bobs.Banner.Columns.PriceCreditsStored] = source[Bobs.Banner.Columns.PriceCreditsStored];
			//destination[Bobs.Banner.Columns.FixedDiscount] = source[Bobs.Banner.Columns.FixedDiscount];
			destination[Bobs.Banner.Columns.Priority] = source[Bobs.Banner.Columns.Priority];
			destination[Bobs.Banner.Columns.AlwaysShow] = source[Bobs.Banner.Columns.AlwaysShow];
		}
		public BannerDataHolder(Banner bob)
			: this()
		{
			CopyValues(bob, this.dataHolder);
		}

		#region Simple members

		/// <summary>
		/// The primary key
		/// </summary>
		public int K
		{
			get { return dataHolder.K; }
			set { this.dataHolder.K = value; }
		}
		///// <summary>
		///// Description of the banner for admin etc.
		///// </summary>
		//public string Name
		//{
		//    get { return dataHolder.Name; }
		//    set { this.dataHolder.Name = value; }
		//}
		/// <summary>
		/// Date that the banners start to be displayed
		/// </summary>
		public DateTime FirstDay
		{
			get { return dataHolder.FirstDay; }
			set { this.dataHolder.FirstDay = value; }
		}
		/// <summary>
		/// Last day that the banners are displayed
		/// </summary>
		public DateTime LastDay
		{
			get { return dataHolder.LastDay; }
			set { this.dataHolder.LastDay = value; }
		}
		///// <summary>
		///// Links to an event if the advert is advertising an event
		///// </summary>
		//public int EventK
		//{
		//    get { return dataHolder.EventK; }
		//    set { this.dataHolder.EventK = value; }
		//}
		///// <summary>
		///// How the banner is rendered - 1=Auto event banner, 2=Custom auto event banner, 3=Animated gif, 4=Jpg,
		///// </summary>
		//public Banner.DisplayTypes DisplayType
		//{
		//    get { return dataHolder.DisplayType; }
		//    set { this.dataHolder.DisplayType = value; }
		//}
		///// <summary>
		///// The misc of the animated gif / jpg / flash movie
		///// </summary>
		//public int MiscK
		//{
		//    get { return dataHolder.MiscK; }
		//    set { this.dataHolder.MiscK = value; }
		//}
		///// <summary>
		///// The guid of the animated gif / jpg / flash movie
		///// </summary>
		//public Guid MiscGuid
		//{
		//    get { return dataHolder.MiscGuid; }
		//    set { this.dataHolder.MiscGuid = value; }
		//}
		///// <summary>
		///// Html to render if DisplayType = CustomHtml
		///// </summary>
		//public string CustomHtml
		//{
		//    get { return dataHolder.CustomHtml; }
		//    set { this.dataHolder.CustomHtml = value; }
		//}
		///// <summary>
		///// Xml used to customise the text of the auto event banners
		///// </summary>
		//public string CustomXml
		//{
		//    get { return dataHolder.CustomXml; }
		//    set { this.dataHolder.CustomXml = value; }
		//}
		/// <summary>
		/// Position - TopBanner=1, Hotbox=2
		/// </summary>
		public Banner.Positions Position
		{
			get { return dataHolder.Position; }
			set { this.dataHolder.Position = value; }
		}
		///// <summary>
		///// Number of hits today
		///// </summary>
		//public int HitsToday
		//{
		//    get { return dataHolder.HitsToday; }
		//    set { this.dataHolder.HitsToday = value; }
		//}
		///// <summary>
		///// The date that the last hit happened
		///// </summary>
		//public DateTime DateLastHit
		//{
		//    get { return dataHolder.DateLastHit; }
		//    set { this.dataHolder.DateLastHit = value; }
		//}
		///// <summary>
		///// Number of slots occupied by this banner. Value of 2 indicates double the normal number of hits.
		///// </summary>
		//public double Weight
		//{
		//    get { return dataHolder.Weight; }
		//    set { this.dataHolder.Weight = value; }
		//}
		///// <summary>
		///// The normalised hits today - e.g. as if Weight=1. Calculated as HitsToday / Weight
		///// </summary>
		//public double HitsTodayNormalised
		//{
		//    get { return dataHolder.HitsTodayNormalised; }
		//    set { this.dataHolder.HitsTodayNormalised = value; }
		//}
		///// <summary>
		///// Status of the banner: New (awaiting payment etc.) = 1, Enabled (booked, live on the site, or finishe
		///// </summary>
		//public Banner.StatusEnum Status
		//{
		//    get { return dataHolder.Status; }
		//    set { this.dataHolder.Status = value; }
		//}
		///// <summary>
		///// Note visible only to admins
		///// </summary>
		//public string AdminNote
		//{
		//    get { return dataHolder.AdminNote; }
		//    set { this.dataHolder.AdminNote = value; }
		//}
		///// <summary>
		///// The user that added this banner
		///// </summary>
		//public int UsrK
		//{
		//    get { return dataHolder.UsrK; }
		//    set { this.dataHolder.UsrK = value; }
		//}
		///// <summary>
		///// The promoter that added this banner (if it's a promoter banner)
		///// </summary>
		//public int PromoterK
		//{
		//    get { return dataHolder.PromoterK; }
		//    set { this.dataHolder.PromoterK = value; }
		//}
		///// <summary>
		///// The brand that this banner advertises
		///// </summary>
		//public int BrandK
		//{
		//    get { return dataHolder.BrandK; }
		//    set { this.dataHolder.BrandK = value; }
		//}
		///// <summary>
		///// The URL to link to if it's a custom URL
		///// </summary>
		//public string LinkUrl
		//{
		//    get { return dataHolder.LinkUrl; }
		//    set { this.dataHolder.LinkUrl = value; }
		//}
		///// <summary>
		///// Total banner impressions on a page that has matched the banners targetting
		///// </summary>
		//public int TotalHitsTargetMatch
		//{
		//    get { return dataHolder.TotalHitsTargetMatch; }
		//    set { this.dataHolder.TotalHitsTargetMatch = value; }
		//}
		/// <summary>
		/// Total banner impressions
		/// </summary>
		public long TotalHits
		{
			get { return dataHolder.TotalHits; }
			//set { this.dataHolder.TotalHits = value; }
		}
		///// <summary>
		///// Total banner clicks
		///// </summary>
		//public int TotalClicks
		//{
		//    get { return dataHolder.TotalClicks; }
		//    set { this.dataHolder.TotalClicks = value; }
		//}
		///// <summary>
		///// Does this banner have target MusicTypes selected?
		///// </summary>
		//public bool IsMusicTargetted
		//{
		//    get { return dataHolder.IsMusicTargetted; }
		//    set { this.dataHolder.IsMusicTargetted = value; }
		//}
		///// <summary>
		///// Does this banner have target Places selected?
		///// </summary>
		//public bool IsPlaceTargetted
		//{
		//    get { return dataHolder.IsPlaceTargetted; }
		//    set { this.dataHolder.IsPlaceTargetted = value; }
		//}
		///// <summary>
		///// Where does the banner link to?
		///// </summary>
		//public Banner.LinkTargets LinkTarget
		//{
		//    get { return dataHolder.LinkTarget; }
		//    set { this.dataHolder.LinkTarget = value; }
		//}
		///// <summary>
		///// Has admin fixed a special price on this banner?
		///// </summary>
		//public bool IsPriceFixed
		//{
		//    get { return dataHolder.IsPriceFixed; }
		//    set { this.dataHolder.IsPriceFixed = value; }
		//}
		///// <summary>
		///// If IsPriceFixed=true, this is the price that will be charged. If IsBooked=true, this the price that 
		///// </summary>
		//public double PriceStored
		//{
		//    get { return dataHolder.PriceStored; }
		//    set { this.dataHolder.PriceStored = value; }
		//}
		///// <summary>
		///// File waiting for authorisation. When it's authorised, it'll be swapped in.
		///// </summary>
		//public int NewMiscK
		//{
		//    get { return dataHolder.NewMiscK; }
		//    set { this.dataHolder.NewMiscK = value; }
		//}
		///// <summary>
		///// Customised banner - first line
		///// </summary>
		//public string CustomiseFirstLine
		//{
		//    get { return dataHolder.CustomiseFirstLine; }
		//    set { this.dataHolder.CustomiseFirstLine = value; }
		//}
		///// <summary>
		///// Customised banner - font size of first line
		///// </summary>
		//public int CustomiseFirstLineSize
		//{
		//    get { return dataHolder.CustomiseFirstLineSize; }
		//    set { this.dataHolder.CustomiseFirstLineSize = value; }
		//}
		///// <summary>
		///// Customised banner - second line
		///// </summary>
		//public string CustomiseSecondLine
		//{
		//    get { return dataHolder.CustomiseSecondLine; }
		//    set { this.dataHolder.CustomiseSecondLine = value; }
		//}
		///// <summary>
		///// Customised banner - third line
		///// </summary>
		//public string CustomiseThirdLine
		//{
		//    get { return dataHolder.CustomiseThirdLine; }
		//    set { this.dataHolder.CustomiseThirdLine = value; }
		//}
		///// <summary>
		///// When uploading a file for a banner - this is the K of the misc if it FAILS.
		///// </summary>
		//public int FailedMiscK
		//{
		//    get { return dataHolder.FailedMiscK; }
		//    set { this.dataHolder.FailedMiscK = value; }
		//}
		///// <summary>
		///// Total hits with place matching
		///// </summary>
		//public int TotalHitsPlaceMatch
		//{
		//    get { return dataHolder.TotalHitsPlaceMatch; }
		//    set { this.dataHolder.TotalHitsPlaceMatch = value; }
		//}
		///// <summary>
		///// Total hits with music matching
		///// </summary>
		//public int TotalHitsMusicMatch
		//{
		//    get { return dataHolder.TotalHitsMusicMatch; }
		//    set { this.dataHolder.TotalHitsMusicMatch = value; }
		//}
		///// <summary>
		///// Total clicks with place matching
		///// </summary>
		//public int TotalClicksPlaceMatch
		//{
		//    get { return dataHolder.TotalClicksPlaceMatch; }
		//    set { this.dataHolder.TotalClicksPlaceMatch = value; }
		//}
		///// <summary>
		///// Total clicks with music matching
		///// </summary>
		//public int TotalClicksMusicMatch
		//{
		//    get { return dataHolder.TotalClicksMusicMatch; }
		//    set { this.dataHolder.TotalClicksMusicMatch = value; }
		//}
		///// <summary>
		///// How will the artwork be created?
		///// </summary>
		//public Banner.DesignTypes DesignType
		//{
		//    get { return dataHolder.DesignType; }
		//    set { this.dataHolder.DesignType = value; }
		//}
		///// <summary>
		///// Time stamp to record when someone is trying to purchase an IBuyable item that is linked to this Bob.
		///// </summary>
		//public DateTime BuyableLockDateTime
		//{
		//    get { return dataHolder.BuyableLockDateTime; }
		//    set { this.dataHolder.BuyableLockDateTime = value; }
		//}
		///// <summary>
		///// Has the design been processed (for the invoice system)
		///// </summary>
		//public bool DesignProcessed
		//{
		//    get { return dataHolder.DesignProcessed; }
		//    set { this.dataHolder.DesignProcessed = value; }
		//}
		/// <summary>
		/// The maximum number of times this banner should be served to a particular website user per day. -1 me
		/// </summary>
		public int FrequencyCapPerIdentifierPerDay
		{
			get { return dataHolder.FrequencyCapPerIdentifierPerDay; }
			set { this.dataHolder.FrequencyCapPerIdentifierPerDay = value; }
		}
		///// <summary>
		///// A total of all the targetting bit 0 = no targetting, otherwise is a bitwise total from BannerServer.
		///// </summary>
		//public Int64 TargettingProperties0
		//{
		//    get { return dataHolder.TargettingProperties0; }
		//    set { this.dataHolder.TargettingProperties0 = value; }
		//}
		///// <summary>
		///// A total of all the targetting bit 0 = no targetting, otherwise is a bitwise total from BannerServer.
		///// </summary>
		//public Int64 TargettingProperties1
		//{
		//    get { return dataHolder.TargettingProperties1; }
		//    set { this.dataHolder.TargettingProperties1 = value; }
		//}
		/////// <summary>
		/////// Last updated timestamp for cache testing
		/////// </summary>
		////public byte[] LastTimestamp
		////{
		////    get { return dataHolder.LastTimestamp; }
		////    set { this.dataHolder.LastTimestamp = value; }
		////}
		/// <summary>
		/// Total impressions required for this banner campaign
		/// </summary>
		public int TotalRequiredImpressions
		{
		    get { return dataHolder.TotalRequiredImpressions; }
		    set { this.dataHolder.TotalRequiredImpressions = value; }
		}
		///// <summary>
		///// The K of the BannerFolder to which the banner belongs
		///// </summary>
		//public int BannerFolderK
		//{
		//    get { return dataHolder.BannerFolderK; }
		//    set { this.dataHolder.BannerFolderK = value; }
		//}
		///// <summary>
		///// The venue to link to
		///// </summary>
		//public int VenueK
		//{
		//    get { return dataHolder.VenueK; }
		//    set { this.dataHolder.VenueK = value; }
		//}
		///// <summary>
		///// Are automatic dates selected in the banner wizard?
		///// </summary>
		//public bool AutomaticDates
		//{
		//    get { return dataHolder.AutomaticDates; }
		//    set { this.dataHolder.AutomaticDates = value; }
		//}
		///// <summary>
		///// The number of weeks selected in the automatic dates section of the banner wizard
		///// </summary>
		//public int AutomaticDatesWeeks
		//{
		//    get { return dataHolder.AutomaticDatesWeeks; }
		//    set { this.dataHolder.AutomaticDatesWeeks = value; }
		//}
		///// <summary>
		///// Is automatic targetting selected in the banner wizard?
		///// </summary>
		//public bool AutomaticTargetting
		//{
		//    get { return dataHolder.AutomaticTargetting; }
		//    set { this.dataHolder.AutomaticTargetting = value; }
		//}
		///// <summary>
		///// Is one of the automatic exposure levels selected in the banner wizard?
		///// </summary>
		//public bool AutomaticExposure
		//{
		//    get { return dataHolder.AutomaticExposure; }
		//    set { this.dataHolder.AutomaticExposure = value; }
		//}
		///// <summary>
		///// The automatic exposure level that is selected in the banner wizard
		///// </summary>
		//public Banner.ExposureLevels AutomaticExposureLevel
		//{
		//    get { return dataHolder.AutomaticExposureLevel; }
		//    set { this.dataHolder.AutomaticExposureLevel = value; }
		//}
		///// <summary>
		///// Usually true, only false if the banner has been paused or cancelled (cancelled when IsRefunded = tru
		///// </summary>
		//public bool StatusEnabled
		//{
		//    get { return dataHolder.StatusEnabled; }
		//    set { this.dataHolder.StatusEnabled = value; }
		//}
		///// <summary>
		///// false if the banner is new, true if it has been paid for
		///// </summary>
		//public bool StatusBooked
		//{
		//    get { return dataHolder.StatusBooked; }
		//    set { this.dataHolder.StatusBooked = value; }
		//}
		///// <summary>
		///// true if the artwork is ready, false if not
		///// </summary>
		//public bool StatusArtwork
		//{
		//    get { return dataHolder.StatusArtwork; }
		//    set { this.dataHolder.StatusArtwork = value; }
		//}
		///// <summary>
		///// true if campaign credits have been successfully refunded to the promoter account
		///// </summary>
		//public bool Refunded
		//{
		//    get { return dataHolder.Refunded; }
		//    set { this.dataHolder.Refunded = value; }
		//}
		///// <summary>
		///// How many credits were refunded?
		///// </summary>
		//public int RefundedCredits
		//{
		//    get { return dataHolder.RefundedCredits; }
		//    set { this.dataHolder.RefundedCredits = value; }
		//}
		///// <summary>
		///// Link to the CampaignCredit table for the refund
		///// </summary>
		//public int RefundCampaignCreditK
		//{
		//    get { return dataHolder.RefundCampaignCreditK; }
		//    set { this.dataHolder.RefundCampaignCreditK = value; }
		//}
		///// <summary>
		///// Duplicate guid used to prevent duplicates while adding
		///// </summary>
		//public Guid DuplicateGuid
		//{
		//    get { return dataHolder.DuplicateGuid; }
		//    set { this.dataHolder.DuplicateGuid = value; }
		//}
		///// <summary>
		///// Price in credits (either for fixed price banners, or for after banner is booked)
		///// </summary>
		//public int PriceCreditsStored
		//{
		//    get { return dataHolder.PriceCreditsStored; }
		//    set { this.dataHolder.PriceCreditsStored = value; }
		//}
		///// <summary>
		///// The start time of the most recent timeslot which had its data copied into total hits
		///// </summary>
		//public DateTime MostRecentlySavedTimeslotStart
		//{
		//    get { return dataHolder.MostRecentlySavedTimeslotStart; }
		//    set { this.dataHolder.MostRecentlySavedTimeslotStart = value; }
		//}
		/// <summary>
		/// Higher priority banners will always be shown before those with lower priorities
		/// </summary>
		public int Priority
		{
			get { return dataHolder.Priority; }
			set { this.dataHolder.Priority = value; }
		}
		/// <summary>
		/// Setting this bit ensures that this banner will be shown if suitable for request
		/// </summary>
		public bool AlwaysShow
		{
			get { return dataHolder.AlwaysShow; }
			set { this.dataHolder.AlwaysShow = value; }
		}
		#endregion
		#endregion
		public Banner Banner
		{
			get
			{
				if (bob == null || dataHolder.IsDirty())
				{
					if (K > 0)
					{
						bob = new Banner(K);
					}
					else
					{
						bob = new Banner();
					}
					CopyValues(this.dataHolder, bob);
				}
				return bob;
			}
		}


		#region RemainingImpressions
		public int RemainingImpressions
		{
			get
			{
				return this.RemainingImpressionsWithMultiplier(1);
			}
		}
		#endregion

	
	}
}
