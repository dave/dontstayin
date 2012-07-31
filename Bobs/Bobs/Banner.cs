using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Bobs.BannerServer;
using Common.Clocks;
using Common;
namespace Bobs
{

	#region RelevanceHolder
	public interface IRelevanceHolder
	{
		void RelevantPlacesAdd(int PlaceK);
		void RelevantMusicAdd(int MusicTypeK);
		List<int> RelevantPlaces { get; }
		List<int> RelevantMusic { get; }
	}
	public class RelevanceHolder : IRelevanceHolder
	{
		#region RelevantPlaces
		public List<int> RelevantPlaces
		{
			get { return relevantPlaces ?? (relevantPlaces = new List<int>()); }
		}
		private List<int> relevantPlaces;
		#endregion
		#region RelevantMusic
		public List<int> RelevantMusic
		{
			get { return relevantMusic ?? (relevantMusic = new List<int>()); }
		}
		private List<int> relevantMusic;
		#endregion
		public void RelevantPlacesAdd(int i)
		{
			if (!RelevantPlaces.Contains(i))
				RelevantPlaces.Add(i);
		}
		public void RelevantMusicAdd(int i)
		{
			if (!RelevantMusic.Contains(i))
				RelevantMusic.Add(i);
		}
	}
	#endregion
	
	#region Banner
	[Serializable]
	public partial class Banner : IDeleteAll, IBuyableCredits, IBannerDesiredHitsRequiredInformation, IHasObjectType, IHasSinglePrimaryKey, IBobType, IPage, ILinkable
	{
		//Banner can be shown if: Today>=FirstDay && Today<=LastDay && StatusEnabled && StatusBooked && StatusArtwork

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Banner.Columns.K] as int? ?? 0; }
			set { this[Banner.Columns.K] = value; }
		}
		/// <summary>
		/// Description of the banner for admin etc.
		/// </summary>
		public override string Name
		{
			get { return (string)this[Banner.Columns.Name]; }
			set { this[Banner.Columns.Name] = value; }
		}
		/// <summary>
		/// Date that the banners start to be displayed
		/// </summary>
		public override DateTime FirstDay
		{
			get { return (DateTime)this[Banner.Columns.FirstDay]; }
			set { this[Banner.Columns.FirstDay] = value; }
		}
		/// <summary>
		/// Last day that the banners are displayed
		/// </summary>
		public override DateTime LastDay
		{
			get { return (DateTime)this[Banner.Columns.LastDay]; }
			set { this[Banner.Columns.LastDay] = value; }
		}
		/// <summary>
		/// Links to an event if the advert is advertising an event
		/// </summary>
		public override int EventK
		{
			get { return (int)this[Banner.Columns.EventK]; }
			set { this[Banner.Columns.EventK] = value; }
		}
		/// <summary>
		/// How the banner is rendered - 1=Auto event banner, 2=Custom auto event banner, 3=Animated gif, 4=Jpg, 5=Flash movie, 6=Custom HTML
		/// </summary>
		public override DisplayTypes DisplayType
		{
			get { return (DisplayTypes)this[Banner.Columns.DisplayType]; }
			set { this[Banner.Columns.DisplayType] = value; }
		}
		/// <summary>
		/// The misc of the animated gif / jpg / flash movie
		/// </summary>
		public override int MiscK
		{
			get { return (int)this[Banner.Columns.MiscK]; }
			set { misc = null; this[Banner.Columns.MiscK] = value; }
		}
		/// <summary>
		/// The guid of the animated gif / jpg / flash movie
		/// </summary>
		public override Guid MiscGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Banner.Columns.MiscGuid]); }
			set { this[Banner.Columns.MiscGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Html to render if DisplayType = CustomHtml
		/// </summary>
		public override string CustomHtml
		{
			get { return (string)this[Banner.Columns.CustomHtml]; }
			set { this[Banner.Columns.CustomHtml] = value; }
		}
		/// <summary>
		/// Xml used to customise the text of the auto event banners
		/// </summary>
		public override string CustomXml
		{
			get { return (string)this[Banner.Columns.CustomXml]; }
			set { this[Banner.Columns.CustomXml] = value; }
		}
		/// <summary>
		/// Position - Leaderboard=1, Hotbox=2
		/// </summary>
		public override Positions Position
		{
			get { return (Positions)this[Banner.Columns.Position]; }
			set { this[Banner.Columns.Position] = value; }
		}
		///// <summary>
		///// Number of hits today
		///// </summary>
		//public override int HitsToday
		//{
		//    get { return (int)this[Banner.Columns.HitsToday]; }
		//    set { this[Banner.Columns.HitsToday] = value; }
		//}
		///// <summary>
		///// The date that the last hit happened
		///// </summary>
		//public override DateTime DateLastHit
		//{
		//    get { return (DateTime)this[Banner.Columns.DateLastHit]; }
		//    set { this[Banner.Columns.DateLastHit] = value; }
		//}
		///// <summary>
		///// Number of slots occupied by this banner. Value of 2 indicates double the normal number of hits.
		///// </summary>
		//public override double Weight
		//{
		//    get { return (double)this[Banner.Columns.Weight]; }
		//    set { this[Banner.Columns.Weight] = value; }
		//}
		///// <summary>
		///// The normalised hits today - e.g. as if Weight=1. Calculated as HitsToday / Weight
		///// </summary>
		//public override double HitsTodayNormalised
		//{
		//    get { return (double)this[Banner.Columns.HitsTodayNormalised]; }
		//    set { this[Banner.Columns.HitsTodayNormalised] = value; }
		//}
		/// <summary>
		/// Status of the banner: New (awaiting payment etc.) = 1, Enabled (booked, live on the site, or finished) = 2, Disabled = 3
		/// </summary>
		public override StatusEnum Status
		{
			get { return (StatusEnum)this[Banner.Columns.Status]; }
			set { this[Banner.Columns.Status] = value; }
		}
		/// <summary>
		/// Note visible only to admins
		/// </summary>
		public override string AdminNote
		{
			get { return (string)this[Banner.Columns.AdminNote]; }
			set { this[Banner.Columns.AdminNote] = value; }
		}
		/// <summary>
		/// The user that added this banner
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Banner.Columns.UsrK]; }
			set { usr = null; this[Banner.Columns.UsrK] = value; }
		}
		/// <summary>
		/// The promoter that added this banner (if it's a promoter banner)
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[Banner.Columns.PromoterK]; }
			set { promoter = null; this[Banner.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// The brand that this banner advertises
		/// </summary>
		public override int BrandK
		{
			get { return (int)this[Banner.Columns.BrandK]; }
			set { brand = null; this[Banner.Columns.BrandK] = value; }
		}
		/// <summary>
		/// The URL to link to if it's a custom URL
		/// </summary>
		public override string LinkUrl
		{
			get { return (string)this[Banner.Columns.LinkUrl]; }
			set { this[Banner.Columns.LinkUrl] = value; }
		}
		///// <summary>
		///// Total banner impressions on a page that has matched the banners targetting
		///// </summary>
		//public override int TotalHitsTargetMatch
		//{
		//    get { return (int)this[Banner.Columns.TotalHitsTargetMatch]; }
		//    set { this[Banner.Columns.TotalHitsTargetMatch] = value; }
		//}
		///// <summary>
		///// Total banner impressions
		///// </summary>
		//public override int TotalHits
		//{
		//    get { return (int)this[Banner.Columns.TotalHits]; }
		//    //set { this[Banner.Columns.TotalHits] = value; }
		//}
		///// <summary>
		///// Total banner clicks
		///// </summary>
		//public override int TotalClicks
		//{
		//    get { return (int)this[Banner.Columns.TotalClicks]; }
		//    //set { this[Banner.Columns.TotalClicks] = value; }
		//}
		/// <summary>
		/// Does this banner have target MusicTypes selected?
		/// </summary>
		public override bool IsMusicTargetted
		{
			get { return (bool)this[Banner.Columns.IsMusicTargetted]; }
			set { this[Banner.Columns.IsMusicTargetted] = value; }
		}
		/// <summary>
		/// Does this banner have target Places selected?
		/// </summary>
		public override bool IsPlaceTargetted
		{
			get { return (bool)this[Banner.Columns.IsPlaceTargetted]; }
			set { this[Banner.Columns.IsPlaceTargetted] = value; }
		}
		/// <summary>
		/// Where does the banner link to?
		/// </summary>
		public override LinkTargets LinkTarget
		{
			get { return (LinkTargets)this[Banner.Columns.LinkTarget]; }
			set { this[Banner.Columns.LinkTarget] = value; }
		}
		/// <summary>
		/// Has admin fixed a special price on this banner?
		/// </summary>
		public override bool IsPriceFixed
		{
			get { return (bool)this[Banner.Columns.IsPriceFixed]; }
			set { this[Banner.Columns.IsPriceFixed] = value; }
		}
		/// <summary>
		/// If IsPriceFixed=true, this is the price that will be charged. If IsBooked=true, this the price that was paid.
		/// </summary>
		public override double PriceStored
		{
			get { return (double)this[Banner.Columns.PriceStored]; }
			set { this[Banner.Columns.PriceStored] = value; }
		}
		/// <summary>
		/// File waiting for authorisation. When it's authorised, it'll be swapped in.
		/// </summary>
		public override int NewMiscK
		{
			get { return (int)this[Banner.Columns.NewMiscK]; }
			set { newMisc = null; this[Banner.Columns.NewMiscK] = value; }
		}
		/// <summary>
		/// Customised banner - first line
		/// </summary>
		public override string CustomiseFirstLine
		{
			get { return (string)this[Banner.Columns.CustomiseFirstLine]; }
			set { this[Banner.Columns.CustomiseFirstLine] = value; }
		}
		/// <summary>
		/// Customised banner - font size of first line
		/// </summary>
		public override int CustomiseFirstLineSize
		{
			get { return (int)this[Banner.Columns.CustomiseFirstLineSize]; }
			set { this[Banner.Columns.CustomiseFirstLineSize] = value; }
		}
		/// <summary>
		/// Customised banner - second line
		/// </summary>
		public override string CustomiseSecondLine
		{
			get { return (string)this[Banner.Columns.CustomiseSecondLine]; }
			set { this[Banner.Columns.CustomiseSecondLine] = value; }
		}
		/// <summary>
		/// Customised banner - third line
		/// </summary>
		public override string CustomiseThirdLine
		{
			get { return (string)this[Banner.Columns.CustomiseThirdLine]; }
			set { this[Banner.Columns.CustomiseThirdLine] = value; }
		}
		/// <summary>
		/// When uploading a file for a banner - this is the K of the misc if it FAILS.
		/// </summary>
		public override int FailedMiscK
		{
			get { return (int)this[Banner.Columns.FailedMiscK]; }
			set { failedMisc = null; this[Banner.Columns.FailedMiscK] = value; }
		}
		///// <summary>
		///// Total hits with place matching
		///// </summary>
		//public override int TotalHitsPlaceMatch
		//{
		//    get { return (int)this[Banner.Columns.TotalHitsPlaceMatch]; }
		//    set { this[Banner.Columns.TotalHitsPlaceMatch] = value; }
		//}
		///// <summary>
		///// Total hits with music matching
		///// </summary>
		//public override int TotalHitsMusicMatch
		//{
		//    get { return (int)this[Banner.Columns.TotalHitsMusicMatch]; }
		//    set { this[Banner.Columns.TotalHitsMusicMatch] = value; }
		//}
		///// <summary>
		///// Total clicks with place matching
		///// </summary>
		//public override int TotalClicksPlaceMatch
		//{
		//    get { return (int)this[Banner.Columns.TotalClicksPlaceMatch]; }
		//    set { this[Banner.Columns.TotalClicksPlaceMatch] = value; }
		//}
		///// <summary>
		///// Total clicks with music matching
		///// </summary>
		//public override int TotalClicksMusicMatch
		//{
		//    get { return (int)this[Banner.Columns.TotalClicksMusicMatch]; }
		//    set { this[Banner.Columns.TotalClicksMusicMatch] = value; }
		//}
		/// <summary>
		/// How will the artwork be created?
		/// </summary>
		public override DesignTypes DesignType
		{
			get { return (DesignTypes)this[Banner.Columns.DesignType]; }
			set { this[Banner.Columns.DesignType] = value; }
		}
		/// <summary>
		/// Time stamp to record when someone is trying to purchase an IBuyable item that is linked to this Bob.
		/// </summary>
		public override DateTime BuyableLockDateTime
		{
			get { return (DateTime)this[Banner.Columns.BuyableLockDateTime]; }
			set { this[Banner.Columns.BuyableLockDateTime] = value; }
		}
		/// <summary>
		/// Has the design been processed (for the invoice system)
		/// </summary>
		public override bool DesignProcessed
		{
			get { return (bool)this[Banner.Columns.DesignProcessed]; }
			set { this[Banner.Columns.DesignProcessed] = value; }
		}
		/// <summary>
		/// The maximum number of times this banner should be served to a particular website user per day. -1 me
		/// </summary>
		public override int FrequencyCapPerIdentifierPerDay
		{
			get { return (int)this[Banner.Columns.FrequencyCapPerIdentifierPerDay]; }
			set { this[Banner.Columns.FrequencyCapPerIdentifierPerDay] = value; }
		}
		/// <summary>
		/// A total of all the targetting bit 0 = no targetting, otherwise is a bitwise total from BannerServer.
		/// </summary>
		public override Int64 TargettingProperties0
		{
			get { return (Int64)this[Banner.Columns.TargettingProperties0]; }
			set { this[Banner.Columns.TargettingProperties0] = value; }
		}
		/// <summary>
		/// A total of all the targetting bit 0 = no targetting, otherwise is a bitwise total from BannerServer.
		/// </summary>
		public override Int64 TargettingProperties1
		{
			get { return (Int64)this[Banner.Columns.TargettingProperties1]; }
			set { this[Banner.Columns.TargettingProperties1] = value; }
		}
		
		/// <summary>
		/// Total impressions required for this banner campaign
		/// </summary>
		public override int TotalRequiredImpressions
		{
			get { return (int)this[Banner.Columns.TotalRequiredImpressions]; }
			set { this[Banner.Columns.TotalRequiredImpressions] = value; }
		}

		/// <summary>
		/// The K of the campaign to which the banner belongs
		/// </summary>
		public override int BannerFolderK
		{
			get { return (int)this[Banner.Columns.BannerFolderK]; }
			set { this[Banner.Columns.BannerFolderK] = value; }
		}
		/// <summary>
		/// The venue to link to
		/// </summary>
		public override int VenueK
		{
			get { return (int)this[Banner.Columns.VenueK]; }
			set { this[Banner.Columns.VenueK] = value; }
		}
		/// <summary>
		/// Are automatic dates selected in the banner wizard?
		/// </summary>
		public override bool AutomaticDates
		{
			get { return (bool)this[Banner.Columns.AutomaticDates]; }
			set { this[Banner.Columns.AutomaticDates] = value; }
		}
		/// <summary>
		/// The number of weeks selected in the automatic dates section of the banner wizard
		/// </summary>
		public override int AutomaticDatesWeeks
		{
			get { return (int)this[Banner.Columns.AutomaticDatesWeeks]; }
			set { this[Banner.Columns.AutomaticDatesWeeks] = value; }
		}
		/// <summary>
		/// Is automatic targetting selected in the banner wizard?
		/// </summary>
		public override bool AutomaticTargetting
		{
			get { return (bool)this[Banner.Columns.AutomaticTargetting]; }
			set { this[Banner.Columns.AutomaticTargetting] = value; }
		}
		/// <summary>
		/// Is one of the automatic exposure levels selected in the banner wizard?
		/// </summary>
		public override bool AutomaticExposure
		{
			get { return (bool)this[Banner.Columns.AutomaticExposure]; }
			set { this[Banner.Columns.AutomaticExposure] = value; }
		}
		/// <summary>
		/// The automatic exposure level that is selected in the banner wizard
		/// </summary>
		public override ExposureLevels AutomaticExposureLevel
		{
			get { return (ExposureLevels)this[Banner.Columns.AutomaticExposureLevel]; }
			set { this[Banner.Columns.AutomaticExposureLevel] = value; }
		}
		/// <summary>
		/// Usually true, only false if the banner has been paused or cancelled (cancelled when IsRefunded = tru
		/// </summary>
		public override bool StatusEnabled
		{
			get { return (bool)this[Banner.Columns.StatusEnabled]; }
			set { this[Banner.Columns.StatusEnabled] = value; }
		}
		/// <summary>
		/// false if the banner is new, true if it has been paid for
		/// </summary>
		public override bool StatusBooked
		{
			get { return (bool)this[Banner.Columns.StatusBooked]; }
			set { this[Banner.Columns.StatusBooked] = value; }
		}
		/// <summary>
		/// true if the artwork is ready, false if not
		/// </summary>
		public override bool StatusArtwork
		{
			get { return (bool)this[Banner.Columns.StatusArtwork]; }
			set { this[Banner.Columns.StatusArtwork] = value; }
		}
		/// <summary>
		/// true if campaign credits have been successfully refunded to the promoter account
		/// </summary>
		public override bool Refunded
		{
			get { return (bool)this[Banner.Columns.Refunded]; }
			set { this[Banner.Columns.Refunded] = value; }
		}
		/// <summary>
		/// How many credits were refunded?
		/// </summary>
		public override int RefundedCredits
		{
			get { return (int)this[Banner.Columns.RefundedCredits]; }
			set { this[Banner.Columns.RefundedCredits] = value; }
		}
		/// <summary>
		/// Link to the CampaignCredit table for the refund
		/// </summary>
		public override int RefundCampaignCreditK
		{
			get { return (int)this[Banner.Columns.RefundCampaignCreditK]; }
			set { this[Banner.Columns.RefundCampaignCreditK] = value; }
		}
		/// <summary>
		/// Duplicate guid used to prevent duplicates while adding
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Banner.Columns.DuplicateGuid]); }
			set { this[Banner.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Price in credits (either for fixed price banners, or for after banner is booked)
		/// </summary>
		public override int PriceCreditsStored
		{
			get { return (int)this[Banner.Columns.PriceCreditsStored]; }
			set { this[Banner.Columns.PriceCreditsStored] = value; }
		}
		/// <summary>
		/// Admin override to fix discount level for price of credits for this banner
		/// </summary>
		public override double FixedDiscount
		{
			get { return (double)this[Banner.Columns.FixedDiscount]; }
			set { this[Banner.Columns.FixedDiscount] = value; }
		}
		/// <summary>
		/// Higher priority banners will always be shown before those with lower priorities
		/// </summary>
		public override int Priority
		{
			get { return (int)this[Banner.Columns.Priority]; }
			set { this[Banner.Columns.Priority] = value; }
		}
		/// <summary>
		/// Setting this bit ensures that this banner will be shown if suitable for request
		/// </summary>
		public override bool AlwaysShow
		{
			get { return (bool)this[Banner.Columns.AlwaysShow]; }
			set { this[Banner.Columns.AlwaysShow] = value; }
		}
		/// <summary>
		/// Has this banner been cancelled?
		/// </summary>
		public override bool IsCancelled
		{
			get { return (bool)this[Banner.Columns.IsCancelled]; }
			set { this[Banner.Columns.IsCancelled] = value; }
		}

		/// <summary>
		/// Number of seconds to display banner for when rotating banners. null indicates that the default should be used
		/// </summary>
		public override int? DisplayDuration
		{
			get { return (int?)this[Banner.Columns.DisplayDuration]; }
			set { this[Banner.Columns.DisplayDuration] = value; }
		}
		#endregion

		#region ILinkable Members

		public string Link(params string[] par)
		{
			return ILinkableExtentions.Link(this, par);
		}
		public string LinkNewWindow(params string[] par)
		{
			return ILinkableExtentions.LinkNewWindow(this, par);
		}

		#endregion

		#region InfoText
		public string InfoText
		{
			get
			{
				if (this.StatusEnabled && this.StatusArtwork && this.StatusBooked && this.FirstDay <= DateTime.Today && this.LastDay >= DateTime.Today)
					return "Running";
				else if (!this.IsCancelled && !this.StatusEnabled && this.StatusArtwork && this.StatusBooked && this.FirstDay <= DateTime.Today && this.LastDay >= DateTime.Today)
					return "Paused";
				else if (this.IsCancelled && this.Refunded)
					return "Cancelled,<br>refunded";
				else if (this.IsCancelled)
					return "Cencelled";
				else if (this.Refunded)
					return "Ended,<br>refunded";
				else if (this.StatusBooked && this.LastDay < DateTime.Today)
					return "Ended";
				else
					return "Waiting";
			}
		}
		#endregion

		#region IBobAsHTML methods
		public string AsHTML()
		{
			string lineReturn = Vars.HTML_LINE_RETURN;
			StringBuilder sb = new StringBuilder();

			sb.Append(lineReturn);
			sb.Append(lineReturn);
			sb.Append("<u>Banner details</u>");
			sb.Append(lineReturn);
			sb.Append(this.PositionString(true));
			sb.Append(" K: ");
			sb.Append(this.K.ToString());
			sb.Append(lineReturn);
			if (this.Promoter != null)
			{
				sb.Append("Promoter: ");
				sb.Append(this.Promoter.Name);
				sb.Append(" (K: ");
				sb.Append(this.PromoterK.ToString());
				sb.Append(")");
				sb.Append(lineReturn);
			}
			if (this.Usr != null)
			{
				sb.Append("Usr: ");
				sb.Append(this.Usr.NickName);
				sb.Append(" (K: ");
				sb.Append(this.UsrK.ToString());
				sb.Append(")");
				sb.Append(lineReturn);
			}
			sb.Append("First day: ");
			sb.Append(this.FirstDay.ToString("ddd dd/MM/yyyy HH:mm"));
			sb.Append(lineReturn);
			sb.Append("Last day: ");
			sb.Append(this.LastDay.ToString("ddd dd/MM/yyyy HH:mm"));
			sb.Append(lineReturn);
			sb.Append("Price: ");
			sb.Append(this.PriceString);
			sb.Append(lineReturn);
			sb.Append("Status booked: ");
			sb.Append(this.StatusBooked.ToString());
			sb.Append(lineReturn);
			sb.Append("Status enabled: ");
			sb.Append(this.StatusEnabled.ToString());
			sb.Append(lineReturn);
			sb.Append("Display type: ");
			sb.Append(Utilities.CamelCaseToString(this.DisplayType.ToString()));
			sb.Append(lineReturn);
			if (this.Refunded)
			{
				sb.Append("Refunded: ");
				sb.Append(this.RefundedCredits.ToString());
				sb.Append(" credits");
				sb.Append(lineReturn);
			}
			return sb.ToString();
		}
		#endregion

		#region Hit/Clicks properties 
		#region TotalClicks
		[NonSerialized]
		Caching.Counter totalClicksCounter;
		public Caching.Counter TotalClicksCounter
		{
			get
			{
				return totalClicksCounter ?? (totalClicksCounter = new Caching.Counter(() => (uint)BannerStatTotals.Clicks, "Banner(K=" + this.K.ToString() + ").TotalClicks", Time.Hours(1)));
			}
		}
		public long TotalClicks { get { return TotalClicksCounter.Value; } }

		#endregion
		#region TotalHits
		[NonSerialized]
		Caching.Counter totalHitsCounter;
		public Caching.Counter TotalHitsCounter
		{
			get
			{
				return totalHitsCounter ?? (totalHitsCounter = new Caching.Counter(() => (uint)BannerStatTotals.Hits, "Banner(K=" + this.K.ToString() + ").TotalHits", Time.Hours(1)));
			}
		}
		public long TotalHits { get { return TotalHitsCounter.Value; } }
		#endregion

		#region BannerStatTotals

		[NonSerialized]
		BannerTotalStat bannerStatTotals = null;
		/// <summary>
		/// This property is used to store banner stats when they are got from the database.
		/// They should only be got from the database when a property that uses them is not 
		/// able to get a value from the cache
		/// </summary>
		private BannerTotalStat BannerStatTotals
        {
			get
			{
				if (bannerStatTotals == null)
				{
					bannerStatTotals = BannerStat.GetBannerStatTotals(this.K)[0];
				}
				return bannerStatTotals;
			}
		}
		#endregion
		#endregion

		#region GetEquivilentExposureLevelStatic(int credits, int days)
		//light: 15 credits/day (display 'light' when custom impressions makes credits < 20/day)
		//medium: 30 credits/day (display 'medium' when custom impressions makes credits < 40/day)
		//heavy: 50 credits/day
		public static Banner.ExposureLevels GetEquivalentExposureLevelStatic(int credits, int days)
		{
			int creditsPerDay = (int)((double)credits / (double)days);
			if (creditsPerDay < 20)
				return Banner.ExposureLevels.Light;
			else if (creditsPerDay < 40)
				return Banner.ExposureLevels.Medium;
			else
				return Banner.ExposureLevels.Heavy;
		}
		#endregion
		#region CurrentEquivalentExposureLevel
		public Banner.ExposureLevels CurrentEquivalentExposureLevel
		{
			get
			{
				return GetEquivalentExposureLevelStatic(CurrentCampaignCredits, GetTotalDays());

			}
		}
		#endregion

		#region ExposureDescription
		public string ExposureDescription
		{
			get
			{
				if (this.AutomaticExposure)
					return this.AutomaticExposureLevel.ToString() + " exposure (" + this.TotalRequiredImpressions.ToString("#,##0") + " impressions)";
				else
					return this.TotalRequiredImpressions.ToString("#,##0") + " impressions (" + this.CurrentEquivalentExposureLevel.ToString().ToLower() + " exposure)";
			}
		}
		#endregion

		#region CurrentCampaignCredits
		public int CurrentCampaignCredits
		{
			get
			{
				return (int)Math.Ceiling((double)this.TotalRequiredImpressions / (double)GetImpressionsPerCampaignCredit(this.Position));
			}
		}

        public int CurrentDesignCampaignCredits
		{
			get
			{
                if (this.DesignType == DesignTypes.None)
                    return 0;
                else
    				return Vars.BannerDesignPriceCredits(this.DesignType);
			}
		}
		#endregion

		#region ToCampaignCredits()
		public List<CampaignCredit> ToCampaignCredits(Usr usr, int promoterK, bool saveToDatabase)
		{
			List<CampaignCredit> campaignCredits = new List<CampaignCredit>();
			CampaignCredit bannerCampaignCredit = new CampaignCredit()
			{
				Description = this.Name + " - " + this.Position + " @ " + this.TotalRequiredImpressions.ToString("N0") + " impressions",
				BuyableObjectK = this.K,
				BuyableObjectType = Model.Entities.ObjectType.Banner,
				Credits = -this.PriceCredits,
				ActionDateTime = Time.Now,
				PromoterK = this.PromoterK,
				InvoiceItemType = Banner.GetInvoiceItemType(this.Position),
                Enabled = false,
				FixedDiscount = this.FixedDiscount,
				IsPriceFixed = this.IsPriceFixed
			};
			bannerCampaignCredit.SetUsrAndActionUsr(usr);
			if(saveToDatabase)
			bannerCampaignCredit.Update();
			campaignCredits.Add(bannerCampaignCredit);

			if (this.DesignType.Equals(Banner.DesignTypes.Jpg) || this.DesignType.Equals(Banner.DesignTypes.Gif) || this.DesignType.Equals(Banner.DesignTypes.Flash))
			{
				CampaignCredit bannerDesignCampaignCredit = new CampaignCredit()
				{
					Description = DesignToString(true),
					BuyableObjectK = this.K,
					BuyableObjectType = Model.Entities.ObjectType.Banner,
					Credits = -Vars.BannerDesignPriceCredits(this.DesignType),
					ActionDateTime = Time.Now,
					PromoterK = this.PromoterK,
					InvoiceItemType = Banner.GetInvoiceItemType(this.DesignType),
                    Enabled = false,
					FixedDiscount = this.FixedDiscount,
					IsPriceFixed = this.IsPriceFixed
				};
				bannerDesignCampaignCredit.SetUsrAndActionUsr(usr);
				if (saveToDatabase)
				bannerDesignCampaignCredit.Update();
				campaignCredits.Add(bannerDesignCampaignCredit);
			}

			return campaignCredits;
		}
		#endregion

		#region GetTotalDays()
		public int GetTotalDays()
		{
			TimeSpan ts = LastDay - FirstDay;
			return ts.Days + 1;
		}
		#endregion

		#region GetImpressionsPerCampaignCredit(Positions position)
        public int CampaignCredits
        {
            get
            {
                return Convert.ToInt32(Banner.GetCampaignCreditsPerImpression(this.Position) * this.TotalRequiredImpressions);
            }
        }
		public static int GetImpressionsPerCampaignCredit(Positions position)
		{
			switch (position)
			{
				case Positions.Hotbox: return Settings.HotboxCredits;
				case Positions.Leaderboard: return Settings.LeaderboardCredits;
				case Positions.Skyscraper: return Settings.SkyscaperCredits;
				case Positions.PhotoBanner: return Settings.PhotoBannerCredits;
				case Banner.Positions.EmailBanner: return Settings.EmailBannerCredits;
				default: throw new Exception("Unexpected banner position: " + position.ToString());
			}
		}
		#endregion

		#region GetCreditsPerDay(Banner.ExposureLevels exposureLevel)
		public static int GetCreditsPerDay(Banner.ExposureLevels exposureLevel)
		{
			if (exposureLevel.Equals(Banner.ExposureLevels.Light))
				return 15;
			else if (exposureLevel.Equals(Banner.ExposureLevels.Medium))
				return 30;
			else if (exposureLevel.Equals(Banner.ExposureLevels.Heavy))
				return 50;
			else
				return 0;
		}
		#endregion

		#region GetCampaignCreditsPerImpression(Positions position)
		internal static double GetCampaignCreditsPerImpression(Positions position)
		{
			return 1.0 / GetImpressionsPerCampaignCredit(position);
		}
		#endregion

		#region RemainingImpressions
		public int RemainingImpressions
		{
			get
			{
				return this.RemainingImpressionsWithMultiplier(1);
			}
		}
		#endregion

		#region ImpressionsValueInCampaignCredits
		public static int GetImpressionsValueInCampaignCredits(int impressions, Positions position)
		{
			return (int)Math.Floor(impressions * GetCampaignCreditsPerImpression(position));
		}
		#endregion

		#region SaveMusicTargetting
		public bool SaveMusicTargetting(Event e)
		{
			return SaveMusicTargetting(e.MusicTypes.ToList().ConvertAll(mt => mt.K));
		}
		public bool SaveMusicTargetting(IList<int> musicTypeKs)
		{
			this.musicTypesChosen = null;
			this.musicTypesAll = null;

			Delete delete = new Delete(TablesEnum.BannerMusicType, new Q(BannerMusicType.Columns.BannerK, this.K));
			delete.Run();

			if (musicTypeKs.Count == 0 || musicTypeKs.Contains(1))
			{
				return false;
			}

			List<int> done = new List<int>(musicTypeKs.Count);
			foreach (int mtK in musicTypeKs)
			{
				MusicType mt = new MusicType(mtK);
				if (!done.Contains(mt.K))
				{
					BannerMusicType newBmt = new BannerMusicType()
					{
						BannerK = this.K,
						MusicTypeK = mt.K,
						Chosen = true
					};
					newBmt.Update();
					done.Add(mt.K);
				}
				if (mt.ParentK > 1 && !done.Contains(mt.ParentK))
				{
					BannerMusicType newParentBmt = new BannerMusicType()
					{
						BannerK = this.K,
						MusicTypeK = mt.ParentK,
						Chosen = false
					};
					newParentBmt.Update();
					done.Add(mt.ParentK);
				}
				if (mt.ParentK == 1)
				{
					foreach (MusicType child in mt.Children)
					{
						if (!done.Contains(child.K))
						{
							BannerMusicType newChildBmt = new BannerMusicType()
							{
								BannerK = this.K,
								MusicTypeK = child.K,
								Chosen = false
							};
							newChildBmt.Update();
							done.Add(child.K);
						}
					}
				}
			}
			return true;
		}
		#endregion
		#region SavePlaceTargetting
		public bool SavePlaceTargetting(Event e)
		{
			Query qPlace = new Query();
			qPlace.TopRecords = 10;
			qPlace.QueryCondition = new Q(Place.Columns.Enabled, true);
			qPlace.OrderBy = e.Venue.Place.NearestPlacesOrderBy;
			PlaceSet ps = new PlaceSet(qPlace);

			return SavePlaceTargetting(ps.ToList().ConvertAll(p => p.K));
		}
		public bool SavePlaceTargetting(IList<int> placeKs)
		{
			this.places = null;

			Delete delete = new Delete(TablesEnum.BannerPlace, new Q(BannerPlace.Columns.BannerK, this.K));
			delete.Run();

			bool done = false;

			foreach (int k in placeKs)
			{
				if (k > 0)
				{
					BannerPlace b = new BannerPlace()
					{
						BannerK = this.K,
						PlaceK = k
					};
					b.Update();
					done = true;
				}
			}
			return done;
		}
		#endregion

		#region AutomaticBannerText
		public string GetAutomaticBannerTextXml()
		{
			return string.Format("<Banner><CustomiseFirstLine>{0}</CustomiseFirstLine><CustomiseFirstLineSize>{1}</CustomiseFirstLineSize><CustomiseSecondLine>{2}</CustomiseSecondLine><CustomiseThirdLine>{3}</CustomiseThirdLine><DisplayType>{4}</DisplayType></Banner>",
				CustomiseFirstLine,
				CustomiseFirstLineSize,
				CustomiseSecondLine,
				CustomiseThirdLine,
				((int)DisplayType).ToString());
		}
		public void SetAutomaticBannerText(string xml)
		{
			if (xml.Trim().Length == 0)
			{
				DisplayType = DisplayTypes.AutoEventBanner;
			}
			else
			{
				try
				{
					DisplayType = (DisplayTypes)int.Parse(GetValueFromXml("DisplayType", xml));
					if (DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
					{
						CustomiseFirstLine = Cambro.Web.Helpers.StripHtmlDoubleSpacesLineFeeds(GetValueFromXml("CustomiseFirstLine", xml));
						CustomiseFirstLineSize = int.Parse(GetValueFromXml("CustomiseFirstLineSize", xml));
						CustomiseSecondLine = Cambro.Web.Helpers.StripHtmlDoubleSpacesLineFeeds(GetValueFromXml("CustomiseSecondLine", xml));
						CustomiseThirdLine = Cambro.Web.Helpers.StripHtmlDoubleSpacesLineFeeds(GetValueFromXml("CustomiseThirdLine", xml));
					}
					else
					{
						CustomiseFirstLine = "";
						CustomiseFirstLineSize = 0;
						CustomiseSecondLine = "";
						CustomiseThirdLine = "";
					}
				}
				catch
				{
					DisplayType = DisplayTypes.AutoEventBanner;
				}
			}
		}
		private static string GetValueFromXml(string name, string xml)
		{
			string openTag = "<" + name + ">";
			string closeTag = "</" + name + ">";
			int indexOfOpenTag = xml.IndexOf(openTag);
			if (indexOfOpenTag == -1) return "";
			return xml.Substring(indexOfOpenTag + openTag.Length, xml.IndexOf(closeTag) - indexOfOpenTag - openTag.Length);
		}
		#endregion

		#region BannerFolder
		public BannerFolder BannerFolder
		{
			get
			{
				if (bannerFolder == null && BannerFolderK > 0)
					bannerFolder = new BannerFolder(BannerFolderK);
				return bannerFolder;
			}
			set
			{
				bannerFolder = value;
			}
		}
		BannerFolder bannerFolder;
		#endregion

		#region ExposureLevels
		#endregion

		#region Height
		public int Height
		{
			get
			{
				//XMAS
				if (this.Position.Equals(Positions.Skyscraper))
					return 250;//600;
				else if (this.Position.Equals(Positions.Hotbox))
					return 250;
				else if (this.Position.Equals(Positions.Leaderboard))
					return 90;
				else if (this.Position.Equals(Positions.PhotoBanner))
					return 50;
				else if (this.Position.Equals(Positions.EmailBanner))
					return 51;
				else
					return 0;
			}
		}
		#endregion

		#region Width
		public int Width
		{
			get
			{
				if (this.Position.Equals(Positions.Skyscraper))
					return 300;
				else if (this.Position.Equals(Positions.Hotbox))
					return 300;
				else if (this.Position.Equals(Positions.Leaderboard))
					return 728;
				else if (this.Position.Equals(Positions.PhotoBanner))
					return 450;
				else if (this.Position.Equals(Positions.EmailBanner))
					return 331;
				else
					return 0;
			}
		}
		#endregion

		#region ClickRate
		public double ClickRate
		{
			get
			{
				if (TotalHits == 0) return 0.0d;
				return (double)this.TotalClicks / (double)this.TotalHits;
			}
		}
		#endregion
		#region DesignTypes
		#endregion

		#region GetDesignTypeFromInvoiceItemType
		public static DesignTypes GetDesignTypeFromInvoiceItemType(InvoiceItem.Types invoiceItemType)
		{
			switch (invoiceItemType)
			{
				case InvoiceItem.Types.DesignBannerJpg:
					return DesignTypes.Jpg;
				case InvoiceItem.Types.DesignBannerAnimatedGif:
					return DesignTypes.Gif;
				case InvoiceItem.Types.DesignBannerFlash:
					return DesignTypes.Flash;
				default:
					return DesignTypes.None;
			}
		}
		#endregion

		#region PositionStats (removed)
		//#region PositionStats
		//[Serializable]
		//public class PositionStats
		//{
		//    public Cambro.Misc.SerializableDictionary<Banner.Positions, double> PercentageFull = new Cambro.Misc.SerializableDictionary<Banner.Positions, double>();
		//    public Cambro.Misc.SerializableDictionary<Banner.Positions, double> ClicksPerSlot = new Cambro.Misc.SerializableDictionary<Banner.Positions, double>();
		//    public Cambro.Misc.SerializableDictionary<Banner.Positions, double> HitsPerSlot = new Cambro.Misc.SerializableDictionary<Banner.Positions, double>();
		//}
		//#endregion
		//#region GeneratePositionStats()
		//public static void GeneratePositionStats()
		//{
		//    PositionStats ps = new PositionStats();

		//    foreach (Banner.Positions position in Enum.GetValues(typeof(Banner.Positions)))
		//    {
		//        double HitsPerSlot = 0.0;
		//        double ClicksPerSlot = 0.0;
		//        double SlotDays = 0.0;
		//        int TotalSlots = 100;// Banner.Slots(position);

		//        for (DateTime date = DateTime.Today.AddDays(-7); date < DateTime.Today; date = date.AddDays(1))
		//        {
		//            //count total hits / clicks...
		//            Query q = new Query();
		//            q.TableElement = new Join(BannerStat.Columns.BannerK, Banner.Columns.K);
		//            q.QueryCondition = new And(
		//                new Q(BannerStat.Columns.Date, date),
		//                Banner.IsBookedQ,
		//                new Q(Banner.Columns.FirstDay, QueryOperator.LessThanOrEqualTo, date),
		//                new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, date),
		//                new Q(Banner.Columns.Position, position));
		//            q.Columns = new ColumnSet();
		//            q.ExtraSelectElements.Add("hits", "sum([BannerStat].[Hits])");
		//            q.ExtraSelectElements.Add("clicks", "sum([BannerStat].[Clicks])");
		//            q.ExtraSelectElements.Add("slots", "sum([Banner].[Weight])");
		//            BannerStatSet bss = new BannerStatSet(q);

		//            int hits = 0;
		//            int clicks = 0;
		//            double slots = 0.0;

		//            try
		//            {
		//                hits = (int)bss[0].ExtraSelectElements["hits"];
		//            }
		//            catch { }

		//            try
		//            {
		//                clicks = (int)bss[0].ExtraSelectElements["clicks"];
		//            }
		//            catch { }

		//            try
		//            {
		//                slots = (double)bss[0].ExtraSelectElements["slots"];
		//            }
		//            catch { }

		//            HitsPerSlot += hits / slots;
		//            ClicksPerSlot += clicks / slots;
		//            SlotDays += slots;
		//        }



		//        ps.HitsPerSlot[position] = HitsPerSlot;
		//        ps.ClicksPerSlot[position] = ClicksPerSlot;
		//        ps.PercentageFull[position] = SlotDays * 100.0 / 7.0 / TotalSlots;
		//    }

		//    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(PositionStats));
		//    StringBuilder sb = new StringBuilder();
		//    StringWriter sw = new System.IO.StringWriter(sb);
		//    x.Serialize(sw, ps);
		//    Global g = new Global(Global.Records.BannerPositionData);
		//    g.ValueText = sb.ToString();
		//    g.Update();
		//}
		//#endregion
		//#region GetPositionStats()
		//public static PositionStats GetPositionStats()
		//{
		//    Global g = new Global(Global.Records.BannerPositionData);
		//    StringReader sr = new StringReader(g.ValueText);
		//    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(PositionStats));
		//    return (PositionStats)x.Deserialize(sr);
		//}
		//#endregion
		#endregion

		#region InvoiceItemDescription
		public string InvoiceItemDescription
		{
			get
			{
                return Cambro.Misc.Utility.Snip(this.Name, 150) + " " + this.FirstDay.ToString("MMM-dd") + " > " + this.LastDay.ToString("MMM-dd") + " [" + this.K + "]";
			}
		}
		#endregion

		#region CustomReplacements
		public string CustomReplacements(string s)
		{
			return Cambro.Web.Helpers.StripHtml(s).Replace("[Date]", Event.FriendlyDate(false)).Replace("[CapsDate]", Event.FriendlyDate(true)).Replace("[DateShort]", Cambro.Misc.Utility.FriendlyDate(Event.DateTime, false, false)).Replace("[CapsDateShort]", Cambro.Misc.Utility.FriendlyDate(Event.DateTime, true, false));
		}
		#endregion

		#region InvoiceItemType
		public InvoiceItem.Types InvoiceItemType
		{
			get
			{
				if (this.Position.Equals(Banner.Positions.Hotbox))
					return InvoiceItem.Types.BannerHotbox;
				else if (this.Position.Equals(Banner.Positions.Leaderboard))
					return InvoiceItem.Types.BannerTop;
				else if (this.Position.Equals(Banner.Positions.PhotoBanner))
					return InvoiceItem.Types.BannerPhoto;
				else if (this.Position.Equals(Banner.Positions.EmailBanner))
					return InvoiceItem.Types.BannerEmail;
				else if (this.Position.Equals(Banner.Positions.Skyscraper))
					return InvoiceItem.Types.BannerSkyscraper;
				else
					return InvoiceItem.Types.Banner;
			}
		}
		#endregion

		#region DeleteAll(Transaction transaction)
		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			try
			{
				//BannerMusicType
				Delete BannerMusicTypeDelete = new Delete(
					TablesEnum.BannerMusicType,
					new Q(BannerMusicType.Columns.BannerK, this.K)
					);
				BannerMusicTypeDelete.Run(transaction);
			}
			catch { }

			try
			{
				//BannerPlace
				Delete BannerPlaceDelete = new Delete(
					TablesEnum.BannerPlace,
					new Q(BannerPlace.Columns.BannerK, this.K)
					);
				BannerPlaceDelete.Run(transaction);
			}
			catch { }

			try
			{
				//BannerStat
				Delete BannerStatDelete = new Delete(
					TablesEnum.BannerStat,
					new Q(BannerStat.Columns.BannerK, this.K)
					);
				BannerStatDelete.Run(transaction);
			}
			catch { }

			this.Delete(transaction);
		}
		#endregion

		#region Misc
		public Misc Misc
		{
			get
			{
				if (misc == null && MiscK > 0)
					misc = new Misc(MiscK);
				return misc;
			}
			set
			{
				misc = value;
			}
		}
		private Misc misc;
		#endregion

		#region NewMisc
		public Misc NewMisc
		{
			get
			{
				if (newMisc == null && NewMiscK > 0)
					newMisc = new Misc(NewMiscK);
				return newMisc;
			}
			set
			{
				newMisc = value;
			}
		}
		private Misc newMisc;
		#endregion

		#region FailedMisc
		public Misc FailedMisc
		{
			get
			{
				if (failedMisc == null && FailedMiscK > 0)
					failedMisc = new Misc(FailedMiscK);
				return failedMisc;
			}
			set
			{
				failedMisc = value;
			}
		}
		private Misc failedMisc;
		#endregion

		#region StatusEnum (removed contents)
		#endregion

		#region Positions
		#endregion

		#region DisplayTypes
		#endregion

		#region LinkTargets
		#endregion

		#region InternalLink
		public bool InternalLink
		{
			get
			{
				return !LinkTarget.Equals(Banner.LinkTargets.ExternalUrl);
			}
		}
		#endregion
		#region PositionString
		public string PositionString(bool Capital)
		{
			string position = (Capital ? "L" : "l") + "eaderboard";
			if (Position.Equals(Positions.Hotbox))
				position = (Capital ? "H" : "h") + "otbox";
			else if (Position.Equals(Positions.Skyscraper))
				position = (Capital ? "S" : "s") + "kyscraper";
			else if (Position.Equals(Positions.PhotoBanner))
				position = (Capital ? "P" : "p") + "hoto banner";
			else if (Position.Equals(Positions.EmailBanner))
				position = (Capital ? "E" : "e") + "mail banner";
			return position;
		}
		#endregion
		#region DisplayTypeString
		public string DisplayTypeString(bool Capital)
		{
			string displayType = (Capital ? "A" : "a") + "utomatic event banner";
			if (DisplayType.Equals(DisplayTypes.CustomAutoEventBanner))
				displayType = (Capital ? "C" : "c") + "ustomised automatic event banner";
			else if (DisplayType.Equals(DisplayTypes.FlashMovie))
				displayType = (Capital ? "F" : "f") + "lash movie banner";
			else if (DisplayType.Equals(DisplayTypes.AnimatedGif))
				displayType = (Capital ? "A" : "a") + "nimated gif banner";
			else if (DisplayType.Equals(DisplayTypes.Jpg))
				displayType = (Capital ? "J" : "j") + "pg banner";
			else if (DisplayType.Equals(DisplayTypes.CustomHtml))
				displayType = (Capital ? "C" : "c") + "ustom HTML banner";
			return displayType;
		}
		#endregion

		#region ArtworkString
		public string ArtworkString(bool Capital)
		{
			string displayType = "";

            if (DisplayType.Equals(DisplayTypes.AutoEventBanner) || DisplayType.Equals(DisplayTypes.CustomAutoEventBanner))
                displayType = (Capital ? "A" : "a") + "utomatic event banner (no artwork needed)...";
            else if (this.DesignType.Equals(Banner.DesignTypes.Jpg) || this.DesignType.Equals(Banner.DesignTypes.Gif) || this.DesignType.Equals(Banner.DesignTypes.Flash))
                displayType = this.DesignToString(Capital);
            else if (DisplayType.Equals(DisplayTypes.FlashMovie) || DisplayType.Equals(DisplayTypes.AnimatedGif) || DisplayType.Equals(DisplayTypes.Jpg))
                displayType = (Capital ? "U" : "u") + "pload my own artwork";
            else if (DisplayType.Equals(DisplayTypes.CustomHtml))
                displayType = (Capital ? "C" : "c") + "ustom HTML banner";

			return displayType;
		}
		#endregion

		#region LinkTargetString
		public string LinkTargetString(bool Capital)
		{
			string linkTarget = (Capital ? "E" : "e") + "vent page";
			if (LinkTarget.Equals(LinkTargets.Brand))
				linkTarget = (Capital ? "B" : "b") + "rand page";
			else if (LinkTarget.Equals(LinkTargets.InternalUrl))
				linkTarget = (Capital ? "I" : "i") + "nternal URL";
			else if (LinkTarget.Equals(LinkTargets.ExternalUrl))
				linkTarget = (Capital ? "E" : "e") + "xternal URL";
			else if (LinkTarget.Equals(LinkTargets.Venue))
				linkTarget = (Capital ? "V" : "v") + "enue page";
			else if (LinkTarget.Equals(LinkTargets.TicketsVenue) || LinkTarget.Equals(LinkTargets.TicketsBrand))
				linkTarget = (Capital ? "T" : "t") + "ickets page";
			return linkTarget;
		}
		#endregion
		#region LinkTargetHtml
		public string LinkTargetHtml
		{
			get
			{
				try
				{
					if (LinkTarget.Equals(LinkTargets.Event))
						return "<a href=\"" + this.Event.Url() + "\" target=\"_blank\">" + this.Event.Name + "</a>";
					else if (LinkTarget.Equals(LinkTargets.Brand))
						return "<a href=\"" + this.Brand.Url() + "\" target=\"_blank\">" + this.Brand.Name + "</a>";
					else if (LinkTarget.Equals(LinkTargets.Venue))
						return "<a href=\"" + this.Venue.Url() + "\" target=\"_blank\">" + this.Venue.Name + "</a>";
					else if (LinkTarget.Equals(LinkTargets.TicketsBrand))
						return "<a href=\"" + this.Brand.UrlApp("tickets") + "\" target=\"_blank\">" + this.Brand.Name + " tickets</a>";
					else if (LinkTarget.Equals(LinkTargets.TicketsVenue))
						return "<a href=\"" + this.Venue.UrlApp("tickets") + "\" target=\"_blank\">" + this.Venue.Name + " tickets</a>";
					else if (LinkTarget.Equals(LinkTargets.InternalUrl))
						return "<a href=\"" + this.LinkUrl + "\" target=\"_blank\">internal</a>";
					else
						return "<a href=\"" + this.LinkUrl + "\" target=\"_blank\">external</a>";
				}
				catch
				{
					return "ERROR";
				}
			}
		}
		#endregion
		#region SummaryString
		public string SummaryString(bool Capital)
		{
			string displayType = (Capital ? "A" : "a") + "utomatic ";
			if (DisplayType.Equals(DisplayTypes.FlashMovie))
				displayType = (Capital ? "F" : "f") + "lash ";
			else if (DisplayType.Equals(DisplayTypes.AnimatedGif))
				displayType = (Capital ? "A" : "a") + "nimated gif ";
			else if (DisplayType.Equals(DisplayTypes.Jpg))
				displayType = (Capital ? "J" : "j") + "pg ";
			else if (DisplayType.Equals(DisplayTypes.CustomHtml))
				displayType = (Capital ? "C" : "c") + "ustom HTML ";

			if (Position.Equals(Positions.Hotbox))
				displayType += "hotbox";
			else if (Position.Equals(Positions.Skyscraper))
				displayType += "skyscraper";
			else if (Position.Equals(Positions.PhotoBanner))
				displayType += "photo banner";
			else if (Position.Equals(Positions.EmailBanner))
				displayType += "email banner";
			else
				displayType += "leaderboard";

			return displayType;
		}
		#endregion
		#region Description (removed)
		//public string Description(bool Capital)
		//{
		//    string displayTypeSrting = (Capital ? "A" : "a") + " custom banner";
		//    if (DisplayType.Equals(DisplayTypes.AutoEventBanner) || DisplayType.Equals(DisplayTypes.CustomAutoEventBanner))
		//        displayTypeSrting = (Capital ? "A" : "a") + "n automatic event banner";
		//    string position = "leaderboard";
		//    if (Position.Equals(Positions.Hotbox))
		//        position = "hotbox";
		//    else if (Position.Equals(Positions.Skyscraper))
		//        position = "skyscraper";
		//    else if (Position.Equals(Positions.PhotoBanner))
		//        position = "photo banner";
		//    else if (Position.Equals(Positions.EmailBanner))
		//        position = "email banner";

		//    return
		//        displayTypeSrting + " in the " + position + " position, " +
		//        "from " + FirstDay.ToString("dd MMMMM") + " until " + LastDay.ToString("dd MMMMM") + ", " +
		//        "occupying " + Weight.ToString() + " slot" + (Weight == 1.0 ? "" : "s") + ".";
		//}
		#endregion
		#region PriceString
		public string PriceString
		{
			get
			{
				if (this.StatusBooked || this.IsPriceFixed)
				{
					if (this.PriceCreditsStored > 0) //This is for new fixed-price banners
						return this.PriceCreditsStored.ToString() + " credits" + (this.PriceDesignCredits > 0 ? " + " + this.PriceDesignCredits.ToString() + " credits (design)" : "");
					if (this.PriceStored > 0.0) //This is for old banners that were paid for with cash.
						return "�" + this.PriceStored.ToString("#,##0.00");
				}
				
				return this.PriceCredits.ToString() + " credits" + (this.PriceDesignCredits > 0 ? " + " + this.PriceDesignCredits.ToString() + " credits (design)" : "");
			}
		}
		#endregion

		#region FixDiscountAndUpdate
		public void FixDiscountAndUpdate(double? discount)
		{
			if (discount != null)
			{
				this.FixedDiscount = Math.Round(discount.Value, 4);
				if (this.FixedDiscount > 1)
					this.FixedDiscount = 1;
				this.PriceCreditsStored = this.CampaignCredits;
				this.IsPriceFixed = true;
			}
			else
			{
				this.FixedDiscount = 0;
				this.PriceCreditsStored = 0;
				this.IsPriceFixed = false;
			}
			this.Update();
		}
		#endregion

		#region FixPriceCreditsAndUpdate
		public void FixPriceExVatCreditsAndUpdate(decimal? price)
		{
			if (price != null)
				this.FixDiscountAndUpdate((double?)(1 - (price / (this.CampaignCredits + this.PriceDesignCredits))));
			else
				FixDiscountAndUpdate(null);
		}
		public void FixPriceIncVatCreditsAndUpdate(decimal? price)
		{
			if (price != null)
			{
				price = price / (1 + (decimal)Invoice.VATRate(Invoice.VATCodes.T1, DateTime.Now));
				this.FixDiscountAndUpdate((double?)(1 - (price / (this.CampaignCredits + this.PriceDesignCredits))));
			}
			else
				FixDiscountAndUpdate(null);
		}
		#endregion

		#region RegisterClick (removed)
		//this should go too???
		public void RegisterClick()
		{
			if (HttpContext.Current != null)
			{
				if (this.Position.Equals(Banner.Positions.Leaderboard))
					HttpContext.Current.Items["VisitLeaderboardClicks"] = 1;
				else if (this.Position.Equals(Banner.Positions.Hotbox))
					HttpContext.Current.Items["VisitHotboxClicks"] = 1;
				else if (this.Position.Equals(Banner.Positions.PhotoBanner))
					HttpContext.Current.Items["VisitPhotoBannerClicks"] = 1;
				else if (this.Position.Equals(Banner.Positions.Skyscraper))
					HttpContext.Current.Items["VisitSkyscraperClicks"] = 1;
			}

			this.TotalClicksCounter.Increment();
			Bobs.BannerStat.Log(this.K, this.Position, DateTime.Today, 0, 0, 1);
		}
		#endregion

		#region RegisterHit
		public void RegisterHit()
		{
			RegisterHit(Identity.Current);
		}
		public void RegisterHit(Identity id)
		{
			Timeslot currentTimeslot = Timeslots.GetCurrentTimeslot();
			BannerTimeslotInfo bannerTimeslotInfo = new BannerTimeslotInfo(K, currentTimeslot);
			bannerTimeslotInfo.ActualHits.Increment();
			bool isUniqueVisitorHit = Timeslots.Today.BannerHitsForIdentity(K, id.Guid).Increment() == 1;
			this.TotalHitsCounter.Increment();
			Bobs.BannerStat.Log(this.K, this.Position, DateTime.Today, 1, isUniqueVisitorHit ? 1 : 0, 0);
			
		}
		#endregion

		#region LinkTargetUrl
		public string LinkTargetUrl
		{
			get
			{
				try
				{
					if (LinkTarget.Equals(LinkTargets.Event))
						return this.Event.Url();
					else if (LinkTarget.Equals(LinkTargets.Brand))
						return this.Brand.Url();
					else if (LinkTarget.Equals(LinkTargets.Venue))
						return this.Venue.Url();
					else if (LinkTarget.Equals(LinkTargets.TicketsBrand))
						return this.Brand.UrlApp("tickets");
					else if (LinkTarget.Equals(LinkTargets.TicketsVenue))
						return this.Venue.UrlApp("tickets");
					else
						return this.LinkUrl;
				}
				catch
				{
					return "http://" + Vars.DomainName;
				}
			}
		}
		#endregion
		#region GetCurrentPrice() (removed)
		//public double GetCurrentPrice()
		//{
		//    return PriceStatic(Position, DisplayType, Promoter.PricingMultiplier, FirstDay, LastDay, Weight);
		//}
		#endregion

		#region Old trafic shape functions - removed
		//#region HourOfDayModifier
		//public static double HourOfDayModifier(int hour)
		//{
		//    switch (hour)
		//    {
		//        case 0:
		//            return 0.027630254;
		//        case 1:
		//            return 0.01514573;
		//        case 2:
		//            return 0.00959191;
		//        case 3:
		//            return 0.003771969;
		//        case 4:
		//            return 0.004940586;
		//        case 5:
		//            return 0.003089312;
		//        case 6:
		//            return 0.001828132;
		//        case 7:
		//            return 0.003876104;
		//        case 8:
		//            return 0.020768973;
		//        case 9:
		//            return 0.058349821;
		//        case 10:
		//            return 0.057956426;
		//        case 11:
		//            return 0.0759832;
		//        case 12:
		//            return 0.067062376;
		//        case 13:
		//            return 0.079720458;
		//        case 14:
		//            return 0.088699133;
		//        case 15:
		//            return 0.079384914;
		//        case 16:
		//            return 0.083029609;
		//        case 17:
		//            return 0.069596307;
		//        case 18:
		//            return 0.049822393;
		//        case 19:
		//            return 0.04897775;
		//        case 20:
		//            return 0.044095017;
		//        case 21:
		//            return 0.045205781;
		//        case 22:
		//            return 0.035151052;
		//        case 23:
		//            return 0.026322793;
		//        default:
		//            return 0;
		//    }
		//}
		//#endregion
		//#region DayOfWeekModifier
		//public static double DayOfWeekModifier(Banner.Positions Position, DayOfWeek Day)
		//{
		//    if (Position.Equals(Positions.EmailBanner))
		//    {
		//        if (Day.Equals(DayOfWeek.Monday))
		//            return 0.16;
		//        else if (Day.Equals(DayOfWeek.Tuesday))
		//            return 0.17;
		//        else if (Day.Equals(DayOfWeek.Wednesday))
		//            return 0.36;
		//        else if (Day.Equals(DayOfWeek.Thursday))
		//            return 0.14;
		//        else if (Day.Equals(DayOfWeek.Friday))
		//            return 0.1;
		//        else if (Day.Equals(DayOfWeek.Saturday))
		//            return 0.03;
		//        else
		//            return 0.04;
		//    }
		//    else
		//    {
		//        if (Day.Equals(DayOfWeek.Monday))
		//            return 0.19;
		//        else if (Day.Equals(DayOfWeek.Tuesday))
		//            return 0.17;
		//        else if (Day.Equals(DayOfWeek.Wednesday))
		//            return 0.18;
		//        else if (Day.Equals(DayOfWeek.Thursday))
		//            return 0.16;
		//        else if (Day.Equals(DayOfWeek.Friday))
		//            return 0.14;
		//        else if (Day.Equals(DayOfWeek.Saturday))
		//            return 0.07;
		//        else
		//            return 0.09;
		//    }
		//}
		//#endregion
		//#region EstimateFullDay
		//public static double EstimateFullDay(DateTime timeNow, int impressionsNow)
		//{
		//    double fraction = EstimateFullDayFraction(timeNow);
		//    double calc = ((double)impressionsNow) / fraction;
		//    return calc;
		//}
		//#endregion
		//#region EstimateDaySoFarFractionOfWeek
		//public static double EstimateDaySoFarFractionOfWeek(DateTime timeNow)
		//{
		//    double fractionOfDay = EstimateFullDayFraction(timeNow);
		//    double fractionOfWeek = DayOfWeekModifier(Banner.Positions.Leaderboard, timeNow.DayOfWeek);
		//    return fractionOfDay * fractionOfWeek;
		//}
		//#endregion
		//#region EstimateFullDayFraction
		//public static double EstimateFullDayFraction(DateTime timeNow)
		//{
		//    double fraction = 0;
		//    for (int hour = 0; hour < timeNow.Hour; hour++)
		//    {
		//        fraction += Banner.HourOfDayModifier(hour);
		//    }
		//    double thisHourFraction = Banner.HourOfDayModifier(timeNow.Hour);
		//    thisHourFraction = thisHourFraction / 60.0;
		//    thisHourFraction = thisHourFraction * timeNow.Minute;
		//    fraction += thisHourFraction;
		//    return fraction;
		//}
		//#endregion
		#endregion

		#region FirstLine
		public string FirstLine
		{
			get
			{
				if (this.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
				{
					return this.CustomReplacements(this.CustomiseFirstLine);
				}
				else
				{
					if (this.Event != null)
						return this.Event.Name;
					else
						return "";
				}
			}
		}
		#endregion
		#region SecondLine
		public string SecondLine
		{
			get
			{
				if (this.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
				{
					return this.CustomReplacements(this.CustomiseSecondLine);
				}
				else
				{
					if (this.Event != null)
						return "@ " + HttpUtility.HtmlEncode(this.Event.Venue.Name) + " in " +
							HttpUtility.HtmlEncode(this.Event.Venue.Place.Name) + ", " +
							this.Event.FriendlyDate(false);
					else
						return "";
				}
			}
		}
		#endregion
		#region ThirdLine
		public string ThirdLine
		{
			get
			{
				if (this.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
				{
					return this.CustomReplacements(this.CustomiseThirdLine);
				}
				else
				{
					if (this.Event != null)
					{
						string musicTypesString = this.Event.MusicTypesString;
						if (musicTypesString.Length > 0)
						{
							return "Music : " + musicTypesString;
						}
						else
							return "";
					}
					else
						return "";

				}
			}
		}
		#endregion
		#region FontSize
		public int FontSize
		{
			get
			{
				if (CustomiseFirstLineSize == 0)
				{
					if (FirstLine.Length <= 30)
						return 18;
					else if (FirstLine.Length <= 33)
						return 17;
					else if (FirstLine.Length <= 36)
						return 16;
					else if (FirstLine.Length <= 38)
						return 15;
					else if (FirstLine.Length <= 40)
						return 14;
					else if (FirstLine.Length <= 50)
						return 12;
					else
						return 10;
				}
				else
					return CustomiseFirstLineSize;
			}
		}
		#endregion

		#region Price (removed)
		//public static double PriceStatic(
		//    Banner.Positions Position,
		//    Banner.DisplayTypes DisplayType,
		//    double PromoterPriceMultiplier)
		//{
		//    double price = 0.0;
		//    Bobs.Global prices = null;
		//    if (Position.Equals(Banner.Positions.Leaderboard))
		//        prices = new Bobs.Global(Bobs.Global.Records.PriceWeekLeaderboard);
		//    else if (Position.Equals(Banner.Positions.Hotbox))
		//        prices = new Bobs.Global(Bobs.Global.Records.PriceWeekHotBox);
		//    else if (Position.Equals(Banner.Positions.Skyscraper))
		//        prices = new Bobs.Global(Bobs.Global.Records.PriceWeekSkyscraper);
		//    else if (Position.Equals(Banner.Positions.PhotoBanner))
		//        prices = new Bobs.Global(Bobs.Global.Records.PriceWeekPhotoBanner);
		//    else if (Position.Equals(Banner.Positions.EmailBanner))
		//        prices = new Bobs.Global(Bobs.Global.Records.PriceWeekEmailBanner);
		//    else
		//        throw new Exception("funny banner position?");

		//    price = prices.ValueDouble;

		//    if (DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner) || DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
		//        price = price * 0.75;

		//    price = Math.Round(price * PromoterPriceMultiplier, 2);

		//    if (price > 0.0)
		//        return price;
		//    else
		//        return 0.0;

		//}
		//public static double PriceStatic(
		//    Banner.Positions Position,
		//    Banner.DisplayTypes DisplayType,
		//    double PromoterPriceMultiplier,
		//    DateTime StartDate,
		//    DateTime EndDate,
		//    double Slots)
		//{
		//    if (EndDate < StartDate)
		//        return 0.0;

		//    TimeSpan daysSpan = EndDate - StartDate;
		//    int daysTotal = daysSpan.Days + 1;
		//    int weeks = daysTotal / 7;
		//    int days = daysTotal - (weeks * 7);

		//    double weeklyPrice = Banner.PriceStatic(Position, DisplayType, PromoterPriceMultiplier);

		//    double total = 0.0;
		//    total += weeks * weeklyPrice;
		//    for (DateTime date = StartDate; date < StartDate.AddDays(days); date = date.AddDays(1))
		//    {
		//        total += Banner.DayOfWeekModifier(Position, date.DayOfWeek) * weeklyPrice;
		//    }
		//    return Math.Round(total * Slots, 2);
		//}
		#endregion

		#region PaymentReceived() (removed - It's not really "payment received" now...)
		//#region PaymentReceived()
		//public void PaymentReceived()
		//{
		//    if (!this.StatusBooked)
		//    {
		//        this.PriceCreditsStored = this.CurrentCampaignCredits;
		//        this.StatusBooked = true;
		//        this.Update();
		//    }
		//}
		//#endregion
		#endregion

		//#region BookBannerNow
		//public void BookBannerNow(int campaignCreditsDebited)
		//{
		//    if (!this.StatusBooked)
		//    {
		//        this.PriceCreditsStored = campaignCreditsDebited;
		//        this.StatusBooked = true;
		//        this.Update();
		//    }
		//}
		//#endregion

        #region GetInvoiceItemType
        public static InvoiceItem.Types GetInvoiceItemType(Banner.Positions position)
        {
            switch (position)
            {
                case Positions.EmailBanner: return InvoiceItem.Types.BannerEmail;
                case Positions.Hotbox: return InvoiceItem.Types.BannerHotbox;
                case Positions.Leaderboard: return InvoiceItem.Types.BannerTop;
                case Positions.PhotoBanner: return InvoiceItem.Types.BannerPhoto;
                case Positions.Skyscraper: return InvoiceItem.Types.BannerSkyscraper;
                default: return InvoiceItem.Types.Banner;
            }
        }

        public static InvoiceItem.Types GetInvoiceItemType(Banner.DesignTypes designType)
        {
            switch (designType)
            {
                case DesignTypes.Jpg: return InvoiceItem.Types.DesignBannerJpg;
                case DesignTypes.Gif: return InvoiceItem.Types.DesignBannerAnimatedGif;
                case DesignTypes.Flash: return InvoiceItem.Types.DesignBannerFlash;
                default: return InvoiceItem.Types.Banner;
            }
        }

        #endregion

		#region DesignToString
		public string DesignToString(bool Capital)
        {
            string output = (Capital ? "B" : "b") + "anner design service - ";
            switch (this.DesignType)
            {
                case DesignTypes.Jpg: return output + "static jpg";
                case DesignTypes.Gif: return output + "animated gif";
                case DesignTypes.Flash: return output + "flash movie";
                default: return "";
            }

        }
        #endregion

        //This all needs to be updated to use campaign credits...
		#region IBuyableCredits Methods + Properties
		/// <summary>
		/// Queries database to retrieve the latest BuyableLockDateTime. Only returns if there is a lock within the Vars.IBUYABLE_LOCK_SECONDS
		/// </summary>
		public bool IsLocked
		{
			get
			{
                if (K == 0)
                    return false;

				Query iBuyableLockDateTimeQuery = new Query(new And(new Q(Banner.Columns.K, this.K),
																	new Q(Banner.Columns.BuyableLockDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.IBUYABLE_LOCK_SECONDS))));
				iBuyableLockDateTimeQuery.Columns = new ColumnSet(Banner.Columns.BuyableLockDateTime);

				BannerSet lockedBannerSet = new BannerSet(iBuyableLockDateTimeQuery);
				if (lockedBannerSet.Count > 0)
				{
					this.BuyableLockDateTime = lockedBannerSet[0].BuyableLockDateTime;
					return true;
				}
				else
					return false;
			}
		}

        /// <summary>
		/// Checks the price entered against the calculated price.  This checks if the figures have been adjusted during the payment processing.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <param name="price">InvoiceItem.Price</param>
		/// <returns></returns>
		public bool VerifyPriceCredits(InvoiceItem.Types invoiceItemType, int priceCredits)
		{
			if (InvoiceItem.BaseType(invoiceItemType).Equals(InvoiceItem.Types.Banner))
			{
				return priceCredits >= this.PriceCredits;
			}
			else if (InvoiceItem.BaseType(invoiceItemType).Equals(InvoiceItem.Types.Design))
			{
				return priceCredits >= Vars.BannerDesignPriceCredits(Banner.GetDesignTypeFromInvoiceItemType(invoiceItemType));
			}
			else
				throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		}

        
		/// <summary>
		/// Checks if the IBuyableCredits Bob is ready to be processed. This is used as a pre-purchasing check.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <param name="price">InvoiceItem.Price</param>
		/// <returns></returns>
		public bool IsReadyForProcessingCredits(InvoiceItem.Types invoiceItemType, int priceCredits)
		{
			if (InvoiceItem.BaseType(invoiceItemType).Equals(InvoiceItem.Types.Banner))
			{
				if (!StatusBooked)
				{
					if (CheckDates())
					{
						if (VerifyPriceCredits(invoiceItemType, priceCredits))
						{
							return true;
						}
						else
						{
							throw new DsiUserFriendlyException("price wrong!");
						}
					}
					else
					{
						throw new DsiUserFriendlyException("dates wrong!");
					}
				}
				else
					return false;
			}
			else if (InvoiceItem.BaseType(invoiceItemType).Equals(InvoiceItem.Types.Design))
			{
				if (VerifyPriceCredits(invoiceItemType, priceCredits))
				{
					return true;
				}
				else
				{
					throw new DsiUserFriendlyException("price wrong!");
				}
			}
			else
				throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		}

        
		/// <summary>
		/// Processes the IBuyableCredits Bob. For banners, it verifies that the banner IsReadyForProcessing. If yes, then it sets banner status to Booked, stores the price, and updates the banner.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <param name="price">InvoiceItem.Price</param>
		/// <returns></returns>
		public bool ProcessCredits(InvoiceItem.Types invoiceItemType, int priceCredits)
		{
			if (InvoiceItem.BaseType(invoiceItemType).Equals(InvoiceItem.Types.Banner))
			{
				if (IsReadyForProcessingCredits(invoiceItemType, priceCredits))
				{
					this.PriceCreditsStored = priceCredits;
					if (!this.StatusBooked && this.FirstDay < DateTime.Today) this.FirstDay = DateTime.Today;
					this.StatusBooked = true;
					this.Update();
				}
			}
			else if (InvoiceItem.BaseType(invoiceItemType).Equals(InvoiceItem.Types.Design))
			{
				if (IsReadyForProcessingCredits(invoiceItemType, priceCredits))
				{
					//send email to John?

					Mailer m = new Mailer();
					m.Subject = this.DesignType.ToString() + " design service processed, banner: " + this.K + ", promoter: " + this.Promoter.Name;
					m.Body = "<p>Banner design service processed - please contact the promoter to get artwork.</p>";
					m.Body += "<p>Banner: <b><a href=\"[LOGIN(" + this.Url() + ")]\">" + this.K.ToString() + "</a></b></p>";
					m.Body += "<p>Promoter: <b><a href=\"[LOGIN(" + this.Promoter.Url() + ")]\">" + this.Promoter.Name + "</a> (K=" + this.Promoter.K.ToString() + ")</b></p>";
					m.Body += "<p>Service type: <b>" + this.DesignType.ToString() + "</b></p>";
					m.Body += "<p>Banner position: <b>" + this.Position.ToString() + "</b></p>";
					m.Body += "<p>Banner goes live: <b>" + this.FirstDay.ToString("dddd dd/MM/yy") + "</b></p>";
					m.RedirectUrl = this.Url();

					m.UsrRecipient = new Usr(1);
					m.Send();

					m.UsrRecipient = new Usr(2);
					m.Send();


					this.DesignProcessed = true;
					this.Update();
				}
			}

			return IsProcessed(invoiceItemType);
		}

		/// <summary>
		/// Unprocesses the IBuyable Bob. For banners, it sets the event donation off, and updates the event.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <returns></returns>
		public bool Unprocess(InvoiceItem.Types invoiceItemType)
		{
			if (InvoiceItem.BaseType(invoiceItemType).Equals(InvoiceItem.Types.Banner))
			{
				this.StatusBooked = false;
				this.Update();

				return !IsProcessed(invoiceItemType);
			}
			else if (InvoiceItem.BaseType(invoiceItemType).Equals(InvoiceItem.Types.Design))
			{

				Mailer m = new Mailer();
				m.Subject = this.DesignType.ToString() + " design service UNPROCESSED, banner: " + this.K + ", promoter: " + this.Promoter.Name;
				m.Body = "<p><b>Invoice UNPROCESSED (stop work! invoice was refunded or error happened after processing!)</b></p>";
				m.Body += "<p>Banner: <b><a href=\"[LOGIN(" + this.Url() + ")]\">" + this.K.ToString() + "</a></b></p>";
				m.Body += "<p>Promoter: <b><a href=\"[LOGIN(" + this.Promoter.Url() + ")]\">" + this.Promoter.Name + "</a> (K=" + this.Promoter.K.ToString() + ")</b></p>";
				m.Body += "<p>Service type: <b>" + this.DesignType.ToString() + "</b></p>";
				m.Body += "<p>Banner position: <b>" + this.Position.ToString() + "</b></p>";
				m.Body += "<p>Banner goes live: <b>" + this.FirstDay.ToString("dddd dd/MM/yy") + "</b></p>";
				m.RedirectUrl = this.Url();

				m.UsrRecipient = new Usr(1);
				m.Send();

				m.UsrRecipient = new Usr(2);
				m.Send();

				//send unprocess email to John?
				this.DesignProcessed = false;
				this.Update();

				return !IsProcessed(invoiceItemType);
			}
			else
				throw new Exception("Invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		}

		/// <summary>
		/// Verifies if the IBuyable Bob has already been processed successfully.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <returns></returns>
		public bool IsProcessed(InvoiceItem.Types invoiceItemType)
		{
			if (InvoiceItem.BaseType(invoiceItemType).Equals(InvoiceItem.Types.Banner))
			{
				return this.StatusBooked;
			}
			else if (InvoiceItem.BaseType(invoiceItemType).Equals(InvoiceItem.Types.Design))
			{
				return this.DesignProcessed;
			}
			else
				throw new Exception("Invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		}

		/// <summary>
		/// Sets the IBuyable Bob field BuyableLockDateTime to DateTime.Now and updates the Bob
		/// </summary>
		public void Lock()
		{
			this.BuyableLockDateTime = DateTime.Now;
			this.Update();
		}

		/// <summary>
		/// Sets the IBuyable Bob field BuyableLockDateTime to DateTime.MinValue and updates the Bob
		/// </summary>
		public void Unlock()
		{
			this.BuyableLockDateTime = DateTime.MinValue;
			this.Update();
		}

		#endregion

		#region LinkUrlLive
		public string LinkUrlLive
		{
			get
			{
				return GetLinkUrlLive(this.K);
			}
		}
		public static string GetLinkUrlLive(int bannerK)
		{
			return "/popup/bannerclick/bannerk-" + bannerK.ToString();
		}
		#endregion

		#region RequiresFile
		public bool RequiresFile
		{
			get
			{
				return !(DisplayType.Equals(DisplayTypes.AutoEventBanner) || DisplayType.Equals(DisplayTypes.CustomAutoEventBanner) || DisplayType.Equals(DisplayTypes.CustomHtml));
			}
		}
		#endregion

		#region AssignMisc
		public AssignReturns AssignMisc(Misc m)
		{
			if (this.DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner) || this.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
				return AssignReturns.Failed;

			if (this.Position.Equals(Banner.Positions.EmailBanner) && m.Extention.Equals("swf"))
				return AssignReturns.Failed;

			Misc.CanUseAsBannerReturn ret = m.CanUseAsBanner(this.Position);

			if (ret.CanUseNow)
			{
				this.FailedMiscK = 0;
				this.MiscK = m.K;
				if (m.Extention.Equals("swf"))
					this.DisplayType = Banner.DisplayTypes.FlashMovie;
				else if (m.Extention.Equals("gif"))
					this.DisplayType = Banner.DisplayTypes.AnimatedGif;
				else if (m.Extention.Equals("jpg"))
					this.DisplayType = Banner.DisplayTypes.Jpg;

				this.NewMiscK = 0;
				if (!this.StatusArtwork && this.FirstDay < DateTime.Today) this.FirstDay = DateTime.Today;
				this.StatusArtwork = true;
				this.Update();
				return AssignReturns.CanUseNow;

			}
			else if (ret.CanUseAfterAdminCheck)
			{
				this.FailedMiscK = 0;
				this.NewMiscK = m.K;
				this.Update();
				return AssignReturns.WaitingForCheck;
			}
			else
			{
				this.FailedMiscK = m.K;
				this.Update();
				return AssignReturns.Failed;
			}

		}
		#endregion
		#region AssignReturns
		#endregion

		#region Price (removed)
		//public double Price
		//{
		//    get
		//    {
		//        if (StatusBooked || IsPriceFixed)
		//            return Math.Round(this.PriceStored, 2);
		//        else
		//            return this.GetCurrentPrice();
		//    }
		//}
		#endregion

		#region PriceCredits
		public int PriceCredits
		{
			get
			{
				if (StatusBooked || IsPriceFixed)
					return this.PriceCreditsStored;
				else
					return this.CurrentCampaignCredits;
			}
		}

        public int PriceDesignCredits
        {
            get
            {
                return this.CurrentDesignCampaignCredits;
            }
        }
		#endregion

		#region IsLive
		public static Q IsLiveQ
		{
			get
			{
				return IsLiveOnDateQ(Time.Today);
			}
		}
		public static Q IsLiveOnDateQ(DateTime date)
		{
			return new And(
				new Q(Banner.Columns.StatusArtwork, true),
				new Q(Banner.Columns.StatusBooked, true),
				new Q(Banner.Columns.StatusEnabled, true),
				new Q(Banner.Columns.FirstDay, QueryOperator.LessThanOrEqualTo, date.Date),
				new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, date.Date)
			);
		}
		public bool IsLive
		{
			get
			{
				return this.StatusArtwork && this.StatusBooked && this.StatusEnabled && this.FirstDay <= Time.Now.Date && this.LastDay >= Time.Now.Date;
			}
		}
		public bool IsPaused
		{
			get
			{
				return this.StatusArtwork && this.StatusBooked && !this.StatusEnabled && this.FirstDay <= Time.Now.Date && this.LastDay >= Time.Now.Date;
			}
		}
		#endregion
		#region IsReady
		public bool IsReady
		{
			get
			{
				return this.StatusArtwork && this.StatusEnabled;
			}
		}
		public static Q IsReadyQ
		{
			get
			{
				return new And(new Q(Banner.Columns.StatusEnabled, true),
							   new Q(Banner.Columns.StatusArtwork, true));
			}
		}
		#endregion
		#region IsBooked
		//public bool IsBooked
		//{
		//    get
		//    {
		//        return this.Status.Equals(StatusEnum.Enabled) || this.Status.Equals(StatusEnum.Booked);
		//    }
		//}
		public static Q IsBookedQ
		{
			get
			{
				return new Q(Banner.Columns.StatusBooked, true);
			}
		}
		//public static Q IsNotBookedQ
		//{
		//    get
		//    {
		//        return new And(
		//            new Q(Banner.Columns.Status, QueryOperator.NotEqualTo, Banner.StatusEnum.Enabled),
		//            new Q(Banner.Columns.Status, QueryOperator.NotEqualTo, Banner.StatusEnum.Booked)
		//        );
		//    }
		//}
		#endregion

		#region OptionsUrl
		public string OptionsUrl(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "mode", "edit", "bannerk", this.K.ToString() }, par);
			return UrlInfo.MakeUrl(this.Promoter.UrlFilterPart, "banneroptions", fullParams);
		}

		#endregion

		public string Url(params string[] par)
		{
			return OptionsUrl(par);
		}

        #region EditUrl
        public string EditUrl(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "mode", "edit", "bannerk", this.K.ToString() }, par);
			return UrlInfo.MakeUrl(this.Promoter.UrlFilterPart, "banneredit", fullParams);
		}
		#endregion

		#region IsPriceLocked
		/// <summary>
		/// Should we lock editing of things that will change the price?
		/// </summary>
		public bool IsPriceLocked
		{
			get
			{
				return StatusBooked || IsPriceFixed;
			}
		}
		#endregion

		#region CheckDates()
		public bool CheckDates()
		{
			if (StatusBooked)
				return true;
			else
			{
				if (LastDay < Time.Now.Date)
					return false;
				else
					return true;
				}
			}
		#endregion

		#region Links to Bobs
		#region Event
		public Event Event
		{
			get
			{
				if (_event == null && EventK > 0)
					_event = new Event(EventK);
				return _event;
			}
			set
			{
				_event = value;
			}
		}
		private Event _event;
		#endregion
		#region Brand
		public Brand Brand
		{
			get
			{
				if (brand == null && BrandK > 0)
					brand = new Brand(BrandK);
				return brand;
			}
			set
			{
				brand = value;
			}
		}
		private Brand brand;
		#endregion
		#region Venue
		public Venue Venue
		{
			get
			{
				if (venue == null && VenueK > 0)
					venue = new Venue(VenueK);
				return venue;
			}
			set
			{
				venue = value;
			}
		}
		private Venue venue;
		#endregion
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null && UsrK > 0)
					usr = new Usr(UsrK);
				return usr;
			}
			set
			{
				usr = value;
			}
		}
		private Usr usr;
		#endregion
		#region Promoter
		public Promoter Promoter
		{
			get
			{
				if (promoter == null && PromoterK > 0)
					promoter = new Promoter(PromoterK);
				return promoter;
			}
			set
			{
				promoter = value;
			}
		}
		private Promoter promoter;
		#endregion
		#endregion

		#region Links to BobSets
		#region MusicTypesAll
		public MusicTypeSet MusicTypesAll
		{
			get
			{
				if (musicTypesAll == null)
				{
					Query q = new Query();
					q.TableElement = MusicType.BannerJoin;
					q.QueryCondition = new Q(Banner.Columns.K, this.K);
					q.NoLock = true;
					q.OrderBy = MusicType.OrderBy;
					musicTypesAll = new MusicTypeSet(q);
				}
				return musicTypesAll;
			}
			set
			{
				musicTypesAll = value;
			}
		}
		private MusicTypeSet musicTypesAll;
		#endregion
		#region MusicTypesChosen
		public MusicTypeSet MusicTypesChosen
		{
			get
			{
				if (musicTypesChosen == null)
				{
					Query q = new Query();
					q.TableElement = MusicType.BannerJoin;
					q.QueryCondition = new And(new Q(Banner.Columns.K, this.K), new Q(BannerMusicType.Columns.Chosen, true));
					q.NoLock = true;
					q.OrderBy = MusicType.OrderBy;
					musicTypesChosen = new MusicTypeSet(q);
				}
				return musicTypesChosen;
			}
			set
			{
				musicTypesChosen = value;
			}
		}
		private MusicTypeSet musicTypesChosen;
		#endregion
		#region Places
		public PlaceSet Places
		{
			get
			{
				if (places == null)
				{
					Query q = new Query();
					q.TableElement = Place.BannerJoin;
					q.QueryCondition = new Q(Banner.Columns.K, this.K);
					q.NoLock = true;
					q.OrderBy = new OrderBy(Place.Columns.Name);
					places = new PlaceSet(q);
				}
				return places;
			}
			set
			{
				places = value;
			}
		}
		private PlaceSet places;
		#endregion
		#endregion

		#region Joins
		public static Join MusicTypeJoin
		{
			get
			{
				return new Join(new Join(Banner.Columns.K, BannerMusicType.Columns.BannerK), MusicType.Columns.K, BannerMusicType.Columns.MusicTypeK);
			}
		}
		public static Join PlaceJoin
		{
			get
			{
				return new Join(new Join(Banner.Columns.K, BannerPlace.Columns.BannerK), Place.Columns.K, BannerPlace.Columns.PlaceK);
			}
		}
		#endregion

		#region TargettingProperties

		#region TargettingProperty
		#endregion

		#region TargettingPropertiesToExclude
		public bool[] TargettingPropertiesToExclude
		{
			get
			{
				bool[] result = new bool[126];
				for (int i = 0; i < 63; i++)
				{
					result[i] = (this.TargettingProperties0 & (long)(1L << i)) != 0;
					result[i + 63] = (this.TargettingProperties1 & (long)(1L << i)) != 0;
				}
				return result;
			}
			set
			{
				long[] targettingProperties = new long[2];
				for (int i = 0; i < value.Length; i++)
				{
					if (value[i]) targettingProperties[i / 63] += 1L << (i % 63);
				}
				this.TargettingProperties0 = targettingProperties[0];
				this.TargettingProperties1 = targettingProperties[1];
			}
		}
		#endregion

		#region SetTargettingProperty
		public void SetTargettingProperty(TargettingProperty property, bool exclude)
		{
			switch ((int)property / 63)
			{
				case 0:
					{
						int propertyValue = (int)property;
						// if it is already set the same as the exclude then ok.
						if (((this.TargettingProperties0 & (long)(1L << propertyValue)) != 0) == exclude)
						{
							break;
						}
						else
						{
							if (exclude == true)
							{
								this.TargettingProperties0 += 1L << propertyValue; break;
							}
							else
							{
								this.TargettingProperties0 -= 1L << propertyValue; break;
							}
						}
					}
				case 1:
					{
						int propertyValue = (int)property - 63;
						if (((this.TargettingProperties1 & (long)(1L << propertyValue)) != 0) == exclude)
						{
							break;
						}
						else
						{
							if (exclude == true)
							{
								this.TargettingProperties1 += 1L << propertyValue; break;
							}
							else
							{
								this.TargettingProperties1 -= 1L << propertyValue; break;
							}
						}
					}
				default: throw new Exception("Property value out of range: " + property.ToString() + " (" + (int)property + ")");
			}
		}
		#endregion

		#endregion

		#region CreateNewBannerForAdditionOrExtensionToThis()
		/// <summary>
		/// Note: does not create the MusicTypes and Places, must use SaveMusicTargetting and SavePlaceTargetting later.
		/// </summary>
		/// <returns></returns>
		public Banner CreateNewBannerForAdditionOrExtensionToThis()
		{
			return new Banner()
			{
				AutomaticExposure = this.AutomaticExposure,
				AutomaticExposureLevel = this.AutomaticExposureLevel,
				AutomaticTargetting = this.AutomaticTargetting,
				BannerFolderK = this.BannerFolderK,
				BrandK = this.BrandK,
				CustomHtml = this.CustomHtml,
				CustomiseFirstLine = this.CustomiseFirstLine,
				CustomiseFirstLineSize = this.CustomiseFirstLineSize,
				CustomiseSecondLine = this.CustomiseSecondLine,
				CustomiseThirdLine = this.CustomiseThirdLine,
				CustomXml = this.CustomXml,
				DesignProcessed = this.DesignProcessed,
				DesignType = this.DesignType,
				DisplayType = this.DisplayType,
				EventK = this.EventK,
				FailedMiscK = this.FailedMiscK,
				FirstDay = this.FirstDay,
				FrequencyCapPerIdentifierPerDay = this.FrequencyCapPerIdentifierPerDay,
				IsMusicTargetted = this.IsMusicTargetted,
				IsPlaceTargetted = this.IsPlaceTargetted,
				LastDay = this.LastDay,
				LinkTarget = this.LinkTarget,
				LinkUrl = this.LinkUrl,
				MiscGuid = this.MiscGuid,
				MiscK = this.MiscK,
				Name = this.Name,
				NewMiscK = this.NewMiscK,
				Position = this.Position,
				PromoterK = this.PromoterK,
				StatusArtwork = this.StatusArtwork,
				StatusEnabled = true, // new banners are always Enabled by default
				TargettingProperties0 = this.TargettingProperties0,
				TargettingProperties1 = this.TargettingProperties1,
				UsrK = this.UsrK,
				VenueK = this.VenueK
			};
		}
		#endregion

		#region Refund Banner
		public void Refund(int actionUsrK)
		{
			// get latest hits stats
			Query q = new Query();
			q.Columns = new ColumnSet(BannerStat.Columns.BannerK);
			string totalHitsExtraSelectElementName = "TotalHits";
			q.ExtraSelectElements.Add(totalHitsExtraSelectElementName, "SUM([Hits])");
			q.QueryCondition = new Q(BannerStat.Columns.BannerK, this.K);
			q.GroupBy = new GroupBy(BannerStat.Columns.BannerK);

			int totalImpressionsSoFar;
			BannerStatSet bss = new BannerStatSet(q);
			if (bss.Count == 0)
			{
				totalImpressionsSoFar = 0;
			}
			else
			{
				totalImpressionsSoFar = (int)bss[0].ExtraSelectElements[totalHitsExtraSelectElementName];
			}

			Refund(this.TotalRequiredImpressions - totalImpressionsSoFar, actionUsrK, 0);
		}

		public void Refund(int remainingImpressions, int actionUsrK, int displayOrder)
		{
			if (this.Refunded)
			{
				throw new DsiUserFriendlyException("This banner has already been refunded.");
			}

			int refundCredits;
			if (this.PriceCreditsStored > 0 && this.TotalRequiredImpressions - remainingImpressions == 0)
			{
				refundCredits = this.PriceCreditsStored;
			}
			else
			{
				refundCredits = Banner.GetImpressionsValueInCampaignCredits(remainingImpressions, this.Position);
			}

			if (refundCredits > 0)
			{
				CampaignCredit cc = new CampaignCredit()
				{
					Credits = refundCredits,
					PromoterK = this.PromoterK,
					ActionDateTime = Time.Now,
					Description = string.Format("Refund for Banner: {0} (banner-{1})", this.Name, this.K),
					DisplayOrder = displayOrder,
					Enabled = true,
					BuyableObjectK = this.K,
					BuyableObjectType = Model.Entities.ObjectType.Banner
				};
				Usr actionUsr;
				try
				{
					actionUsr = new Usr(actionUsrK);
				}
				catch
				{
					actionUsr = Usr.SystemUsr;
				}
				
				cc.SetUsrAndActionUsr(actionUsr);
				cc.UpdateWithRecalculateBalance();
				this.RefundCampaignCreditK = cc.K;

				this.Refunded = true;
				this.RefundedCredits = refundCredits;
				this.Update();
			}
		}

		public static void RefundFinishedBanners()
		{
			Query q = new Query();
			q.Columns = new ColumnSet(
							Banner.Columns.K, 
							Banner.Columns.Name, 
							Banner.Columns.PromoterK, 
							Banner.Columns.Position, 
							Banner.Columns.Refunded, 
							Banner.Columns.RefundCampaignCreditK, 
							Banner.Columns.RefundedCredits, 
							Banner.Columns.TotalRequiredImpressions,
							Banner.Columns.PriceCreditsStored);

			q.TableElement = new JoinLeft(Banner.Columns.K, BannerStat.Columns.BannerK);
			string totalHitsExtraSelectElementName = "TotalHits";
			q.ExtraSelectElements.Add(totalHitsExtraSelectElementName, "ISNULL(SUM([Hits]),0)");

			q.QueryCondition = new And(
				new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, new DateTime(2007, 9, 10)), // date new BannerServer went live
				new Q(Banner.Columns.LastDay, QueryOperator.LessThan, Time.Today),
				new Bobs.Or(new Q(Banner.Columns.Refunded, QueryOperator.IsNull, null), new Q(Banner.Columns.Refunded, false)),
				new Q(Banner.Columns.StatusBooked, true));

			q.GroupBy = new GroupBy(
							new GroupBy(Banner.Columns.K),
							new GroupBy(Banner.Columns.Name),
							new GroupBy(Banner.Columns.PromoterK),
							new GroupBy(Banner.Columns.Refunded),
							new GroupBy(Banner.Columns.RefundCampaignCreditK),
							new GroupBy(Banner.Columns.RefundedCredits),
							new GroupBy(Banner.Columns.TotalRequiredImpressions),
							new GroupBy(Banner.Columns.LastDay),
							new GroupBy(Banner.Columns.Position),
							new GroupBy(Banner.Columns.Refunded),
							new GroupBy(Banner.Columns.PriceCreditsStored));

			q.Having = new Q("ISNULL(SUM([Hits]),0) < {0}", Banner.Columns.TotalRequiredImpressions, null);
			q.OrderBy = new OrderBy(Banner.Columns.PromoterK);

			BannerSet bs = new BannerSet(q);

			List<string> exceptions = new List<string>();
			int promoterK = 0;
			int displayOrder = 0;
			foreach (Banner b in bs)
			{
				try
				{
					// Fix to ensure different displayOrder for CampaignCredits with the same promoter, which is needed for proper calculation of CampaignCredit.BalanceToDate
					if (promoterK == b.PromoterK)
						displayOrder++;
					else
						displayOrder = 0;
					b.Refund(b.TotalRequiredImpressions - (int)b.ExtraSelectElements[totalHitsExtraSelectElementName], 0, displayOrder);
					promoterK = b.PromoterK;
				}
				catch (Exception ex)
				{
					exceptions.Add("BannerK: " + b.K + " Exception: " + ex.ToString());
				}
			}

			if (exceptions.Count > 0)
			{
				throw new Exception(string.Join("<br><br>", exceptions.ToArray()));
			}
		}
		#endregion

		public static Log.Items GetLogItemTypeFromPositionType(Positions position)
		{
			switch (position)
			{
				case Positions.Hotbox: return Common.Settings.UsePerBannerTypeStats ? Log.Items.HotBoxHit : Log.Items.DsiPageRenderNoCrawlers;
				case Positions.Leaderboard: return Common.Settings.UsePerBannerTypeStats ? Log.Items.LeaderboardHit : Log.Items.DsiPageRenderNoCrawlers; 
				case Positions.Skyscraper: return Common.Settings.UsePerBannerTypeStats ? Log.Items.SkyScraperHit : Log.Items.DsiPageRenderNoCrawlers;
				case Positions.PhotoBanner: return Log.Items.PhotoImpressions;
				case Positions.EmailBanner: return Log.Items.EmailsSent;
				default: throw new Exception("Unexpected banner position.");
			}
		}

		#region GetPredictedRequiredImpressions
		/// <summary>
		/// Total predicted number of impressions to serve by date and banner position
		/// </summary>
		/// <param name="date"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		public static long GetPredictedRequiredImpressions(DateTime date, Banner.Positions position)
		{
			bool isPast = date.Date < Time.Today;
			// if it is in the past, allow caching for a couple of months. otherwise we need to recalculate fairly often.
			TimeSpan localCacheLifespan = isPast ? new TimeSpan(24, 0, 0) : new TimeSpan(0, 15, 0);
			TimeSpan distributedCacheLifespan = isPast ? new TimeSpan(61, 0, 0, 0) : new TimeSpan(0, 15, 0);

			return Caching.Instances.Main.GetWithLocalCaching(
				String.Format("GetPredictedRequiredImpressions(date={0}, position={1})", date, position),
				() => { return getPredictedRequiredImpressions(date, position); },
				localCacheLifespan,
				distributedCacheLifespan
			);
		}
		private static long getPredictedRequiredImpressions(DateTime date, Banner.Positions position)
		{
			BannerSet bs;
			Query q = new Query();
			q.Columns = new ColumnSet(Banner.Columns.K, Banner.Columns.FirstDay, Banner.Columns.LastDay, Banner.Columns.TotalRequiredImpressions);
			q.QueryCondition = new And(Banner.IsLiveOnDateQ(date), new Q(Banner.Columns.Position, position), new Q(Banner.Columns.TotalRequiredImpressions, QueryOperator.GreaterThan, 0));
			bs = new BannerSet(q);

			// if we're inspecting the past, predict as if today is the date in the past.
			// otherwise future predictions need to use current data.
			DateTime fixedDate;
			if (date < Time.Today)
			{
				fixedDate = date;
			}
			else
			{
				fixedDate = Time.Today;
			}

			Bobs.BannerServer.Traffic.DataDrivenTrafficShape ts = new Bobs.BannerServer.Traffic.DataDrivenTrafficShape();
			Log.Items logItem = Banner.GetLogItemTypeFromPositionType(position);

			long requiredImpressions = 0;

			foreach (Banner b in bs)
			{
				double thisDayCount = ts.GetPredictedCountOfLogItemBetweenDates(logItem, date, date.AddDays(1), fixedDate);
				double totalWeekCount = thisDayCount;

				for (DateTime d = date.AddDays(1); d <= date.AddDays(6); d = d.AddDays(1))
				{
					totalWeekCount += ts.GetPredictedCountOfLogItemBetweenDates(logItem, d, d.AddDays(1), fixedDate);
				}

				requiredImpressions += (int)Math.Round(b.TotalRequiredImpressions * (thisDayCount / totalWeekCount) * (7.0 / ((b.LastDay - b.FirstDay).TotalDays + 1)));
			}

			return requiredImpressions;
		}
		#endregion


		#region IHasObjectType Members

		public Model.Entities.ObjectType ObjectType
		{
			get { return Model.Entities.ObjectType.Banner; }
		}

		#endregion

		#region IBobType Members

		public string TypeName
		{
			get { return "Banner"; }
		}

		#endregion

		#region IReadableReference Members

		public string ReadableReference
		{
			get { return "Banner-" + K.ToString(); }
		}

		#endregion


		#region RenderAsHtml
		public string RenderAsHtml()
		{
			if (this.Position != Positions.PhotoBanner)
			{
				throw new NotImplementedException();
			}

			switch (this.DisplayType)
			{
				case DisplayTypes.CustomHtml: return this.CustomHtml;
				case DisplayTypes.Jpg:
				case DisplayTypes.AnimatedGif:
					return string.Format(@"<a href='/popup/bannerclick/bannerk-{0}' target=''><img src='{1}' width='450' height='50' vspace='0' hspace='0' border='0'></a>",
						this.K, this.Misc.Url());
				case DisplayTypes.FlashMovie:
					return "";
				default: throw new NotImplementedException();
			}
		}

		public string GetEmbedScript(string placeholderClientID)
		{
			if (this.Position != Positions.PhotoBanner)
			{
				throw new NotImplementedException();
			}
/*
 * 
<div id="Banner<%= Guid  %>_BannerDiv" style="width:<%= WidthString %>px;height:<%= HeightString %>px;" class=\"BorderBlack\"></div>
<dsi:InlineScript runat="server">
<script>
	var Banner<%=  Guid  %>_so = new SWFObject("<%= BannerUrl %>", "Banner<%=  Guid  %>_mymovie", <%= WidthString %>, <%= HeightString %>, "<%= FlashVersionString %>", "#ffffff");
	Banner<%=  Guid  %>_so.addParam("wmode", "transparent");
	Banner<%=  Guid  %>_so.addParam("AllowScriptAccess", "always");
	Banner<%=  Guid  %>_so.addVariable("targetTag", "<%= TargetTag %>");
	Banner<%=  Guid  %>_so.addVariable("linkTag", "<%= LinkTag %>");
	Banner<%=  Guid  %>_so.addParam("loop", "true");
	Banner<%=  Guid  %>_so.addParam("menu", "false");
	Banner<%=  Guid  %>_so.write("Banner<%=  Guid  %>_BannerDiv");
</script>
</dsi:InlineScript>
*/

			switch (this.DisplayType)
			{
				case DisplayTypes.CustomHtml:
				case DisplayTypes.Jpg:
				case DisplayTypes.AnimatedGif:
					return "";
				case DisplayTypes.FlashMovie:
					return string.Format(
@"var {0} = new SWFObject('{1}', '{0}_mymovie', {2}, {3}, {4}, '#ffffff');
{0}.addParam('wmode', 'transparent');
{0}.addParam('AllowScriptAccess', 'always');
{0}.addVariable('targetTag', '{5}');
{0}.addVariable('linkTag', '{6}');
{0}.addParam('loop', 'true');
{0}.addParam('menu', 'false');
{0}.write('{7}');",
				  "banner" + Guid.NewGuid().ToString().Replace("-", ""),
				  Misc.Url(),
				  Width,
				  Height,
				  string.IsNullOrEmpty(Misc.RequiredFlashVersion) ? "7" : Misc.RequiredFlashVersion, 
				  InternalLink ? "_self" : "_blank",
				  
				  LinkUrlLive.StartsWith("http://") ? LinkUrlLive : "http://" + Vars.DomainName + LinkUrlLive,
				  
				  placeholderClientID);
				default: throw new NotImplementedException();
			}
		}
		#endregion

	}
	#endregion

	
	#region BannerMusicType

	#endregion

	#region BannerPlace

	#endregion

	

}
