using Common;

namespace Model.Entities
{
	public partial class FacebookPost
	{
		public enum TypeEnum
		{
			NewConnect = 1,
			AttendEvent = 2,
			Spotted = 3,
			PhotoUpload = 4,
			FrontPagePhotoDsi = 5,
			FrontPagePhoto = 6,
			NewTopic = 7,
			Laugh = 8,
			FavouritePhoto = 9,
			FavouriteTopic = 10,
			NewTopicNews = 11,
			NewTopicReview = 12,
			ArticlePublish = 13,
			NewBuddy = 14,
			JoinGroup = 15,
			BuyTicket = 16,
			AddEvent = 17
		}
	}

	public enum TaggableType
	{
		Photo = ObjectType.Photo
	}
	public enum ArchiveObjectType
	{
		Gallery,
		News,
		Review,
		Comp,
		Article,
		Guestlist,
		PhotoOfTheWeek
	}
	

	public partial class Abuse
	{
		public enum StatusEnum
		{
			New = 1,
			Done = 2
		}
		public enum ResolveStatusEnum
		{
			/// <summary>
			/// Abuse was found - the object has been deleted and the abusing user will be penalised
			/// </summary>
			Abuse = 1,
			/// <summary>
			/// The report was helpful, but there was no abuse - the abusing user shouldn't be penalised
			/// </summary>
			NoAbuse = 2,
			/// <summary>
			/// Overturned - the reporting user should not have made the report
			/// </summary>
			Overturned = 3
		}
	}
	public partial class Admin
	{
		public enum AdminObjectType
		{
			Country = 1,
			Place = 2
		}
	}
	public partial class Article
	{
		public enum RelevanceEnum
		{
			Worldwide = 0,
			Country = 1,
			Region = 2,
			Place = 3,
			Venue = 4,
			Event = 5
		}
		public enum StatusEnum
		{
			New = 1,
			Edit = 2,
			Enabled = 3,
			Disabled = 4
		}
	}
	public partial class BankExport
	{
		public enum Statuses
		{
			Added = 1,
			AwaitingConfirmation = 2,
			Successful = 3,
			Failed = 4,
			Cancelled = 5
		}
		public enum Types
		{
			InternalTransferNonTicketFunds = 1,
			InternalTransferTicketFundsUsed = 2,
			InternalTransferTicketFundsToPromoter = 3,
			ExternalBACSTicketFundsToPromoter = 4,
			ExternalBACSRefundToPromoter = 5,
			InternalTransferRefundToPromoter = 6
		}
	}
	public partial class Banner
	{
		public enum ExposureLevels
		{
			None = 0,
			Light = 1,
			Medium = 2,
			Heavy = 3
		}
		public enum DesignTypes
		{
			None = 0,
			Jpg = 1,
			Gif = 2,
			Flash = 3
		}
		public enum StatusEnum
		{
			None = 0
			//	New = 1,
			//	Enabled = 2,
			//	Disabled = 3,
			//	Booked = 4
		}
		public enum Positions
		{
			None = 0,
			Leaderboard = 1,
			Hotbox = 2,
			EmailBanner = 3,
			PhotoBanner = 4,
			Skyscraper = 5
		}
		public enum DisplayTypes
		{
			AutoEventBanner = 1,
			CustomAutoEventBanner = 2,
			AnimatedGif = 3,
			Jpg = 4,
			FlashMovie = 5,
			CustomHtml = 6
		}
		public enum LinkTargets
		{
			Event = 1,
			Brand = 2,
			InternalUrl = 4,
			ExternalUrl = 5,
			Venue = 6,
			TicketsBrand = 7,
			TicketsVenue = 8
		}
		public enum AssignReturns
		{
			CanUseNow = 1,
			WaitingForCheck = 2,
			Failed = 3
		}
		public enum TargettingProperty
		{
			//ONLY INSERT NEW THINGS AT THE BOTTOM OF THIS LIST!!!!
			//IT'S IMPORTANT THAT THE EXISTING VALUES DO NOT CHANGE
			Gender_Unknown = 1,
			Gender_Male = 2,
			Gender_Female = 3,
			IsPromoter_False = 4,
			IsPromoter_True = 5,
			Salary_Unknown = 6,
			Salary_1 = 7,
			Salary_2 = 8,
			Salary_3 = 9,
			Salary_4 = 10,
			Salary_5 = 11,
			Salary_6 = 12,
			Salary_7 = 13,
			Employment_Unknown = 14,
			Employment_1 = 15,
			Employment_2 = 16,
			Employment_3 = 17,
			Employment_4 = 18,
			DrinkWater_Unknown = 19,
			DrinkWater_False = 20,
			DrinkWater_True = 21,
			DrinkSoft_Unknown = 22,
			DrinkSoft_False = 23,
			DrinkSoft_True = 24,
			DrinkEnergy_Unknown = 25,
			DrinkEnergy_False = 26,
			DrinkEnergy_True = 27,
			DrinkDraftBeer_Unknown = 28,
			DrinkDraftBeer_False = 29,
			DrinkDraftBeer_True = 30,
			DrinkBottledBeer_Unknown = 31,
			DrinkBottledBeer_False = 32,
			DrinkBottledBeer_True = 33,
			DrinkSpirits_Unknown = 34,
			DrinkSpirits_False = 35,
			DrinkSpirits_True = 36,
			DrinkWine_Unknown = 37,
			DrinkWine_False = 38,
			DrinkWine_True = 39,
			DrinkAlcopops_Unknown = 40,
			DrinkAlcopops_False = 41,
			DrinkAlcopops_True = 42,
			DrinkCider_Unknown = 43,
			DrinkCider_False = 44,
			DrinkCider_True = 45,
			CreditCard_Unknown = 46,
			CreditCard_False = 47,
			CreditCard_True = 48,
			Loan_Unknown = 49,
			Loan_False = 50,
			Loan_True = 51,
			Mortgage_Unknown = 52,
			Mortgage_False = 53,
			Mortgage_True = 54,
			AgeRange_Unknown = 55,
			AgeRange_Under_18 = 56,
			AgeRange_18_24 = 57,
			AgeRange_25_29 = 58,
			AgeRange_30_34 = 59,
			AgeRange_35_39 = 60,
			AgeRange_40_44 = 61,
			AgeRange_45_49 = 62,
			AgeRange_50_Plus = 63,
			SpendMusicCd_Unknown = 64,
			SpendMusicCd_Zero = 65,
			SpendMusicCd_MoreThanZero = 66,
			SpendMusicVinyl_Unknown = 67,
			SpendMusicVinyl_Zero = 68,
			SpendMusicVinyl_MoreThanZero = 69,
			SpendMusicDownload_Unknown = 70,
			SpendMusicDownload_Zero = 71,
			SpendMusicDownload_MoreThanZero = 72

			//OwnCar_Unknown = 55,
			//OwnCar_False = 56,
			//OwnCar_True = 57,
			//OwnBike_Unknown = 58,
			//OwnBike_False = 59,
			//OwnBike_True = 60,
			//OwnMp3_Unknown = 61,
			//OwnMp3_False = 62,
			//OwnMp3_True = 63,
			//OwnPc_Unknown = 64,
			//OwnPc_False = 65,
			//OwnPc_True = 66,
			//OwnLaptop_Unknown = 67,
			//OwnLaptop_False = 68,
			//OwnLaptop_True = 69,
			//OwnMac_Unknown = 70,
			//OwnMac_False = 71,
			//OwnMac_True = 72,
			//OwnBroadBand_Unknown = 73,
			//OwnBroadBand_False = 74,
			//OwnBroadBand_True = 75,
			//OwnConsole_Unknown = 76,
			//OwnConsole_False = 77,
			//OwnConsole_True = 78,
			//OwnCamera_Unknown = 79,
			//OwnCamera_False = 80,
			//OwnCamera_True = 81,
			//OwnDvd_Unknown = 82,
			//OwnDvd_False = 83,
			//OwnDvd_True = 84,
			//OwnDvdRec_Unknown = 85,
			//OwnDvdRec_False = 86,
			//OwnDvdRec_True = 87,
			//BuyCar_Unknown = 88,
			//BuyCar_False = 89,
			//BuyCar_True = 90,
			//BuyBike_Unknown = 91,
			//BuyBike_False = 92,
			//BuyBike_True = 93,
			//BuyMp3_Unknown = 94,
			//BuyMp3_False = 95,
			//BuyMp3_True = 96,
			//BuyPc_Unknown = 97,
			//BuyPc_False = 98,
			//BuyPc_True = 99,
			//BuyLaptop_Unknown = 100,
			//BuyLaptop_False = 101,
			//BuyLaptop_True = 102,
			//BuyMac_Unknown = 103,
			//BuyMac_False = 104,
			//BuyMac_True = 105,
			//BuyBroadBand_Unknown = 106,
			//BuyBroadBand_False = 107,
			//BuyBroadBand_True = 108,
			//BuyConsole_Unknown = 109,
			//BuyConsole_False = 110,
			//BuyConsole_True = 111,
			//BuyCamera_Unknown = 112,
			//BuyCamera_False = 113,
			//BuyCamera_True = 114,
			//BuyDvd_Unknown = 115,
			//BuyDvd_False = 116,
			//BuyDvd_True = 117,
			//BuyDvdRec_Unknown = 118,
			//BuyDvdRec_False = 119,
			//BuyDvdRec_True = 120,

		}
	}
	public partial class BinRange
	{
		public enum Types
		{
			None = 0,
			Delta = 1,
			Electron = 2,
			VisaPurchasing = 3,
			Visa = 4,
			MasterCard = 5,
			Switch = 6,
			Solo = 7,
			JCB = 8,		// DontStayIn does not accept JCB
			Maestro = 9,
			AmericanExpress = 10
		}
	}
	public partial class Brand
	{
		public enum PromoterStatusEnum
		{
			Unconfirmed = 1,
			Confirmed = 2
		}
	}
	public partial class Buddy
	{
		public enum BuddyFindingMethod
		{
			Nickname = 0,
			RealName = 1,
			EmailAddress = 2,
			SpotterCode = 3
		}
	}
	public partial class Chat
	{
		public enum IconShade
		{
			Selected,
			Shaded30,
			Shaded40,
			Shaded50
		}
	}
	public partial class Comp
	{
		public enum DisplayTypes
		{
			Old = 1,
			New = 2
		}
		public enum LinkTypes
		{
			None = 1,
			Event = 2,
			Brand = 3
		}
		public enum StatusEnum
		{
			New = 1,
			Published = 2,
			Enabled = 3,
			Disabled = 4
		}
	}
	public partial class Country
	{
		public enum PostageZones
		{
			UK = 0,
			World1 = 1,
			World2 = 2,
			RestOfEurope = 3,
			WesternEurope = 4
		}
		public enum BillingRegionEnum
		{
			UnitedKingdom = 1,
			EuropeanUnion = 2,
			RestOfTheWorld = 3
		}
	}
	public partial class DonationIcon
	{
		public enum Control
		{
			Basic = 0,
			Default = 1,
			Monkey = 2
		}
	}
	public partial class Event
	{
		public enum StartTimes
		{
			Morning = 2,
			Daytime = 3,
			Evening = 4
		}
		public enum DeleteReturnStatus
		{
			Success,
			FailComments,
			FailPhotos,
			FailNoPermission,
			FailException,
			FailPromoter
		}
		public enum BannerTypes
		{
			None = 0,
			Auto = 1,
			Image = 2,
			Flash = 3
		}

		public enum HotelLinkSources
		{
			Icon,
			Banner
		}
	}
	public partial class Global
	{
		public enum Records
		{
			LastComment = 1,
			LastChatMessage = 2,
			PriceWeekLeaderboard = 3,
			PriceWeekHotBox = 4,
			PriceWeekPhotoBanner = 5,
			PriceWeekEmailBanner = 6,
			MaxUsers5Min = 7,
			MaxUsers30Min = 8,
			LastPhotoUpload = 9,
			LiveChatRestricted = 11,
			PendingPhotoAbuseReports = 12,
			VatRate = 13,
			PriceWeekSkyscraper = 14,
			CampDsiTickets = 15,
			RoyalMailEproPassword = 16,
			BannerPositionData = 17,
			DnsConfigBatchFilesLocations = 18
		}
		public enum QueryTypes { Select, Update, Insert, Delete }
		public enum RequestCounter
		{
			BannerServerTimeout,
			CacheStore,
			CacheHit,
			CacheMiss,
			CacheException
		}
	}
	public partial class Group
	{
		public enum StatusEnum
		{
			Enabled = 1,
			New = 2,
			Disabled = 3
		}
		public enum RestrictionEnum
		{
			None = 1,
			Member = 2,
			Moderator = 3,
			Custom = 4
		}
		public enum CustomRestrictionTypes
		{
			Admins = 1,
			EventModerators = 2,
			PhotoModerators = 3,
			ChatModerators = 4,
			Promoters = 5
		}
	}
	public partial class GroupUsr
	{
		public enum StatusEnum
		{
			/// <summary>
			/// This person is a member of the group
			/// </summary>
			Member = 1,
			/// <summary>
			/// This person has requested membership
			/// </summary>
			Request = 2,
			/// <summary>
			/// The membership request was denied
			/// </summary>
			RequestRejected = 3,
			/// <summary>
			/// The person has been officially invited to the group - if they accept they will become a 
			/// member instantly
			/// </summary>
			Invite = 4,
			/// <summary>
			/// This person was invited to the group, but decided they didn't want to be a member
			/// </summary>
			InviteRejected = 5,
			/// <summary>
			/// The person exited the group themselves
			/// </summary>
			Exited = 6,
			/// <summary>
			/// The person has been barred by a moderator
			/// </summary>
			Barred = 7,
			/// <summary>
			/// A member has recommended this person as a group member. The membership admins will either 
			/// change the status to Invited, or change it to RecommendRejected. This status level is 
			/// only used in groups with restricted membership
			/// </summary>
			Recommend = 8,
			/// <summary>
			/// A member has recommended this person as a member of the group, but they were rejected by a 
			/// membership moderator. This status level is only used in groups with restricted membership
			/// </summary>
			RecommendRejected = 9
		}
	}
	public partial class IncomingSms
	{
		public enum ServiceTypes
		{
			FrontPagePhoto = 1
		}
		//public enum ServiceTypes
		//{
		//    Tonight = 1,
		//    Pllay = 2,
		//    TextGuest = 3,
		//    Mms = 4
		//}
	}
	public partial class InsertionOrder
	{
		public enum InsertionOrderStatus
		{
			Proforma = 1, Raised = 2, Disabled = 3
		}
	}
	public partial class Invoice
	{
		public enum PaymentTypes
		{
			CreditCard = 1,
			Invoiced = 2
		}
		public enum Statuses
		{
			Paid = 1,
			Outstanding = 2,
			Overdue = 3
		};
		public enum Types
		{
			Invoice = 1,
			Credit = 2
		}
		public enum BuyerTypes
		{
			None = 0, 
			AgencyPromoter = 1, 
			NonAgencyPromoter = 2, 
			TicketUsr = 3, 
			NonTicketUsr = 4
		}
		public enum VATCodes
		{
			T1 = 1,
			T3 = 3,
			T4 = 4
		}
	}
	public partial class InvoiceItem
	{
		public enum VATCodes
		{
			/// <summary>
			/// T0: 0% - zero rated VAT items (UK)
			/// </summary>
			T0 = 10,
			/// <summary>
			/// T1: 17.5% - standard rated VAT items (UK)
			/// </summary>
			T1 = 1,
			/// <summary>
			/// T2: 0% - exempt VAT items (UK)
			/// </summary>
			T2 = 2,
			/// <summary>
			/// T9: 0% - nothing to do with VAT and dont show o
			/// </summary>
			T9 = 9
		}
		public enum Types
		{
			Misc = 0,
			Banner = 2,
			UsrDonate = 3,
			EventDonate = 4,
			GuestlistCredit = 5,
			BannerTop = 6,
			BannerHotbox = 7,
			BannerPhoto = 8,
			BannerEmail = 9,
			Design = 10,
			OtherWebAdvertising = 11,
			NonWebAdvertising = 12,
			Broadcast = 13,
			BannerSkyscraper = 14,
			EventTickets = 15,
			DsiEventTickets = 16,
			EventTicketsBookingFee = 17,
			EventTicketsDelivery = 18,
			CharityDonation = 19,
			DesignBannerJpg = 20,
			DesignBannerAnimatedGif = 21,
			DesignBannerFlash = 22,
			CampaignCredits = 23,
			Eflyer = 24
		}
	}
	public partial class Log
	{
		public enum Items
		{
			PhotoImpressions = 1,
			EmailsSent = 2,
			WelcomeDelete = 3,
			WelcomeLogOff = 4,
			WelcomeSignUp = 5,
			DsiPages = 6,
			BlankPages = 7,
			AdminPages = 8,
			LegacyPageHits = 9,
			BannerAdminStart = 10,
			BannerAdminNext1 = 11,
			BannerAdminNext2 = 12,
			BannerAdminNext3 = 13,
			BannerAdminNext4 = 14,
			BannerAdminNext5 = 15,
			DsiPagesLoggedIn = 16,
			DsiPagesLoggedOut = 17,
			VisitWriteFailures = 18,
			ResizeImageStart = 19,
			ResizeImageEnd = 20,
			SkipQuestionairre = 21,
			CompleteQuestionairre = 22,
			DefaultSkyscraper = 23,
			DefaultLeaderboard = 24,
			PassthroughSkyscraper = 25,
			PassthroughLeaderboard = 26,
			AddvantageSkyscraper = 27,
			AddvantageLeaderboard = 28,
			CamerasPage = 29,
			OriginalDynamicGenerate = 30,
			OriginalRenderPersist = 31,
			DbQueriesSelect = 32,
			DbQueriesUpdate = 33,
			DbQueriesInsert = 34,
			DbQueriesDelete = 35,
			CaptionGroupJoin = 36,
			CaptionsAdded = 37,
			BounceUnverify = 38,
			BounceUnsubscribe = 39,
			MailUkSmall = 40,
			MailUkLarge = 41,
			MailWestEurope = 42,
			MailRestEurope = 43,
			MailRestWorld = 44,
			PromoterOfferStart = 45,
			PromoterOfferSuccess = 46,
			MailPromoterPacks = 47,
			PhotoPageImpressions = 48,
			DoAlertsStart = 49,
			DoAlertsEnd = 50,
			LondonPage = 51,
			ThreadLog0 = 52,
			ThreadLog1 = 53,
			ThreadLog2 = 54,
			ThreadLog3 = 55,
			ThreadLog4 = 56,
			ThreadLog5 = 57,
			ThreadLog6 = 58,
			ThreadLog7Plus = 59,
			GoogleSkyscraper = 60,
			GoogleLeaderboard = 61,
			GoogleHotbox = 62,
			DefaultHotbox = 63,
			PassthroughHotbox = 64,
			AddvantageHotbox = 65,
			SkipInviteFriends = 66,
			MailPromoterLetters = 67,
			FindYourPhotoClicked = 68,
			UploadPhotosClicked = 69,
			FindEventsClicked = 70,
			FindYourFriendsClicked = 71,
			FindTicketsClicked = 72,
			CompsUrlShortcut = 73,
			IbizaRocksUrlShortcut = 74,
			DsiPageInits = 75,
			DsiPageRender = 76,
			DsiPageRenderNoCrawlers = 77,
			HotBoxRender = 78,
			SkyScraperRender = 79,
			LeaderboardRender = 80,
			PhotoBannerRender = 81,
			PhotoBannerAjaxRender = 82,
			HotboxRotation = 83,
			LeaderboardRotation = 84,
			SkyScraperRotation = 85,
			HotBoxHit = 86,
			LeaderboardHit = 87,
			SkyScraperHit = 88,
			PhotoBannerHit = 98,
			PhotoBannerRotation = 99,
			HotelLinkIconClicked = 100,
			HotelLinkBannerClicked = 101,
			FacebookUrlShortcut = 102,
			TwitterUrlShortcut = 103,
			AddEventClicked = 104,
			FreeGuestlistClicked = 105,
			PhotoImpressionsNoCrawlers = 106,
			EmailBouncesDisabled = 107,
			EmailSkippedBecauseBrokenAddresses = 108,
			MixmagBouncesDisabled = 109,
			SchmucksUrlShortcut = 110,
			WhosGoingOutPage = 111,
			WhosGoingOutPageNoCrawlers = 112,
			MixmagVotePages = 113,
			MixmagVotePagesNoCrawlers = 114,
			MixmagGreatestPages = 115,
			MixmagGreatestPagesNoCrawlers = 116,
			MobilePages = 117,
			MobilePagesNoCrawlers = 118,
		}
	}

	public partial class OutgoingSms
	{
		public enum Types
		{
			FrontPagePhoto = 1

		}
		public enum ChargeTypes
		{
			FreeBudget = 1,
			Premium025p = 2,
			Premium050p = 3,
			Premium100p = 4,
			Premium150p = 5,
			FreeAdvanced = 6,
			FreePremier = 7,
			FreePremierPorted = 8,
			FreePremierPlus = 9,
			Premium012p = 10,
			Premium300p = 11,
			Premium500p = 12,
			Premium035p = 10

		}
	}
	public partial class PageTime
	{
		public enum LogItems
		{
			PhotoImpressions = 1,
			EmailsSent = 2,
			WelcomeDelete = 3,
			WelcomeLogOff = 4,
			WelcomeSignUp = 5,
			DsiPages = 6,
			BlankPages = 7,
			AdminPages = 8,
			LegacyPageHits = 9,
			BannerAdminStart = 10,
			BannerAdminNext1 = 11,
			BannerAdminNext2 = 12,
			BannerAdminNext3 = 13,
			BannerAdminNext4 = 14,
			BannerAdminNext5 = 15,
			DsiPagesLoggedIn = 16,
			DsiPagesLoggedOut = 17,
			VisitWriteFailures = 18,
			ResizeImageStart = 19,
			ResizeImageEnd = 20,
			SkipQuestionairre = 21,
			CompleteQuestionairre = 22,
			DefaultSkyscraper = 23,
			DefaultLeaderboard = 24,
			PassthroughSkyscraper = 25,
			PassthroughLeaderboard = 26,
			AddvantageSkyscraper = 27,
			AddvantageLeaderboard = 28,
			CamerasPage = 29,
			OriginalDynamicGenerate = 30,
			OriginalRenderPersist = 31,
			DbQueriesSelect = 32,
			DbQueriesUpdate = 33,
			DbQueriesInsert = 34,
			DbQueriesDelete = 35//,
			//SonyGroupJoin = 36,
		}
	}
	public partial class Para
	{
		public enum PhotoTypes
		{
			None = 0,
			Icon = 1,
			Thumb = 2,
			Web = 3,
			Custom = 4,
			VideoLo = 5,
			VideoMed = 6,
			VideoHi = 7
		}
		public enum TypeEnum
		{
			Title = 1,
			Para = 2,
			Photo = 3
		}
		public enum PhotoAlignEnum
		{
			Left = 1,
			Right = 2,
			Center = 3,
			Top = 4,
			Bottom = 5
		}
	}
	public partial class Photo
	{
		public enum MediaTypes
		{
			Unknown = 0,
			Image = 1,
			Video = 2,
			Audio = 3
		}
		public enum SaveWebFileSourceLocations
		{
			UploadTemporary,
			Master
		}
		public enum OperationType
		{
			MaxSide,
			FixedSize,
			Crop,
		}
		public enum Overlays
		{
			None = 0,
			DsiLogoBottomRight = 1,
			DsiLogoBottomRightSonyBottomLeft = 2,
			DsiLogoBottomRightThinkBottomLeft = 3,
			DsiLogoBottomRightThinkTextBottomLeft = 4,
			PlayButtonLarge = 5,
			PlayButtonSmall = 6,
			DsiLogoBottomRightSonyC902BottomLeft = 7,

		}
		public enum StatusEnum
		{
			Moderate = 1,
			Enabled = 2,
			Processing = 3
		}
	}
	public partial class PhotoReview
	{
		public enum RatingTypes
		{
			Cool = 1,
			Sexy = 2
		}
	}
	public partial class Place
	{
		public enum MixmagZone
		{
			Euro = 1,
			Ibiza = 2,
			London = 3,
			Midlands = 4,
			North = 5,
			Ireland = 6,
			Scotland = 7,
			SouthAndEast = 8,
			West = 9
		}
	}
	public partial class Prefs
	{
		public enum Sources
		{
			BrowserGuid,
			CurrentUsr
		}
	}
	public partial class Promoter
	{
		public enum AddedMedhods
		{
			EndUser = 1,
			SalesUser = 2,
			Batch = 3
		}
		public enum OfferTypes
		{
			None = 1,
			DoubleSlots = 2
		}
		public enum LetterTypes
		{
			CurrentNewPromoter = 1,
			CurrentIdlePromoter = 2,
			CurrentActivePromoter = 3,
			AutoVenue = 4
		}
		public enum LetterStatusEnum
		{
			New = 1,
			Printing = 2,
			Posted = 3
		}
		public enum SalesStatusEnum
		{
			New = 1,
			Idle = 2,
			Proactive = 3,
			Active = 4,
			Sleeping = 5
		}
		public enum StatusEnum
		{
			Enabled = 1,
			Active = 3,
			Disabled = 4
		}
		public enum ClientSectorEnum
		{
			None = 0,
			Promoter = 1,
			Accomodation = 2,
			Alcohol = 3,
			Apparel = 4,
			Cameras = 5,
			COIGov = 6,
			FilmIndustry = 7,
			Finance = 8,
			Food = 9,
			HomeEntertainment = 10,
			Insurance = 11,
			MajorEvents = 12,
			MediaAgency = 13,
			MobileComms = 14,
			MultipleVenues = 15,
			MusicIndustry = 16,
			PersonalHealth = 17,
			SoftDrink = 18,
			TravelTransport = 19,
			Miscellaneous = 20,
			Retailer = 21,
			Manufacturer = 22
		}
		public enum VatStatusEnum
		{
			Unknown = 0,
			NotRegistered = 1,
			Registered = 2
		}
		public enum SalesEstimateEnum
		{
			None = 0,
			Rubbish = 1,
			Poor = 2,
			Average = 3,
			Good = 4,
			Excellent = 5
		}
		public enum AccountStatus
		{
			InCredit = 1,
			ZeroBalance = 2,
			Outstanding = 3,
			Overdue = 4
		}
	}
	public partial class SalesCall
	{
		public enum Types
		{
			Cold = 1,
			ProactiveFollowUp = 2,
			Active = 3
		}
		public enum Directions
		{
			Outgoing = 1,
			Incoming = 2
		}
	}
	public partial class SalesStatusChange
	{
		public enum Types
		{
			NewProactiveClient = 1,
			NewActiveClient = 2
		}
	}
	public partial class TagPhotoHistory
	{
		public enum TagPhotoHistoryAction
		{

			Created = 0,
			Blocked = 1,
			Unblocked = 2,
			Disabled = 3,
			Enabled = 4
		}
	}
	public partial class Thread
	{
		public enum NewsStatusEnum
		{
			None = 0,
			Recommended = 1,
			Done = 2
		}
	}
	public partial class ThreadUsr
	{
		public enum StatusEnum
		{
			NewInvite = 0,
			Archived = 1,
			Deleted = 2,
			Ignore = 3,
			NewWatchedForumAlert = 4,
			NewGroupNewsAlert = 5,
			NewComment = 6,
			UnArchived = 8,
			None = 7
		}
		public enum PrivateChatTypes
		{
			Null = 0,
			None = 1,
			PublicChatAlert = 2,
			Popup = 3
		}
	}
	public partial class Ticket
	{
		public enum FeedbackEnum
		{
			None = 0,
			Good = 1,
			Bad = 2
		}
	}
	public partial class TicketRun
	{
		public enum TicketRunStatus
		{
			WaitingStartDate = 1,
			WaitingToFollowOtherTicketRun = 2,
			Running = 3,
			Paused = 4,
			SoldOut = 5,
			Ended = 6,
			Refunded = 7
		}
		public enum DeliveryMethodType
		{
			[EnumDescription("E-ticket")]
			E_Ticket = 0,
			[EnumDescription("Royal Mail Special Delivery")]
			SpecialDelivery = 1
		}
	}
	public partial class Transfer
	{
		public enum StatusEnum
		{
			Pending = 1,
			Success = 2,
			Cancelled = 3,
			Failed = 4
		}
		public enum TransferTypes
		{
			Payment = 1,
			Refund = 2
		}
		public enum Methods
		{
			Card = 1,
			BankTransfer = 2,
			Cheque = 3,
			Cash = 4,
			TicketSales = 5
		}
		public enum RefundStatuses
		{
			None = 0,
			PartialRefund = 1,
			FullRefund = 2
		}
		public enum FraudCheckEnum
		{
			Strict,
			Relaxed
		}
		public enum DSIBankAccounts
		{
			None = 0,
			Current = 1,
			Client = 2
		}
		public enum CompanyEnum
		{
			Unknown = 0,
			DSI = 1,
			DH = 2
		}
	}
	public partial class Usr
	{
		public enum PhotoUsageEnum
		{
			Use = 0,
			Contact = 1,
			DoNotUse = 2
		}
		public enum SpamBotDefeaterCounter
		{
			Comments,
			PrivateLiveChats,
			BuddyRequests,
			GroupInvites
		}
		public enum Permissions
		{
			/// <summary>
			/// Chat moderation - delete comments
			/// </summary>
			ChatModeration,
			/// <summary>
			/// Delete photos, moderate new galleries
			/// </summary>
			PhotoModeration,
			/// <summary>
			/// Delete and edit events
			/// </summary>
			EventModeration,
			/// <summary>
			/// Enable and edit articles
			/// </summary>
			ArticleModeration,
			/// <summary>
			/// Enable and moderate competitions
			/// </summary>
			CompetitionModeration,
			/// <summary>
			/// Enable or disable suggested news
			/// </summary>
			NewsModeration,
			/// <summary>
			/// Add plain html
			/// </summary>
			PlainHtml,
			/// <summary>
			/// Ban members
			/// </summary>
			Ban,
			/// <summary>
			/// Add top photos
			/// </summary>
			TopPhotos,
			/// <summary>
			/// Change these permissions
			/// </summary>
			ChangePermissions,
			/// <summary>
			/// Enable a disabled place
			/// </summary>
			EnablePlace,
			/// <summary>
			/// Delete any object with the multidelete page
			/// </summary>
			MultiDeleteObject,
			/// <summary>
			/// Act as any group owner / moderator
			/// </summary>
			GroupAdmin,
			/// <summary>
			/// Act as any promoter / edit promoter objects / use sales features
			/// </summary>
			PromoterAdmin,
			/// <summary>
			/// Can login as other people
			/// </summary>
			LoginAsUser,
			/// <summary>
			/// Delete peoples profile pictures
			/// </summary>
			DeleteProfilePicture,
			/// <summary>
			/// Sage accounts outputs etc.
			/// </summary>
			AccountsBackend,
			/// <summary>
			/// View sales figures
			/// </summary>
			ViewSalesStats,
			/// <summary>
			/// Enable the "Add buddies" checkbox when inviting by email
			/// </summary>
			ChatInviteEmailAddBuddiesCheckBox,
			/// <summary>
			/// Use sales features - promoter lists etc.
			/// </summary>
			SalesExecutive,
			/// <summary>
			/// Use accounts features - read only
			/// </summary>
			AccountsView,
			/// <summary>
			/// Accounts features - create
			/// </summary>
			AccountsCreateInvoice,
			/// <summary>
			/// Accounts features - issue credit notes and refunds
			/// </summary>
			AccountsRefund,
		}
		public enum CardStatusEnum
		{
			New = 3, WelcomePackInPost = 4, HaveCards = 0, NeedCards = 1, CardsInPost = 2, PrintingWelcomePack = 5, PrintingRefill = 6
		}
		public enum AdminLevels
		{
			None = 0,
			Junior = 1,
			Senior = 2,
			Super = 3
		}
		public enum AddBuddySource
		{
			InvitePanelEmails,
			BuddyButtonClick,
			WelcomePage,
			SpamPage,
			PhotoPageSpotByEmail,
			UsrPageSendPrivateMessage,
			BuddyImporter,
			BuddyAutoComplete
		}
		public enum SalesTeams
		{
			None = 0,
			DirectorSalesTeam = 1,
			PromoterSalesTeam = 2,
			CorporateSalesTeam = 3,
			SupportSalesTeam = 4,
			SmallBusinessSalesTeam = 5
		}
	}
	public partial class UsrDate
	{
		public enum StatusEnum
		{
			Yes = 1,
			No = 2,
			Maybe = 3
		}
		public enum MatchEnum
		{
			No = 0,
			Yes = 1,
			WasButNotAnyMore = 2
		}
	}
	public partial class Venue
	{
		public enum PromoterStatusEnum
		{
			Unconfirmed = 1,
			Confirmed = 2
		}
		public enum DeleteReturnStatus
		{
			Success,
			FailComments,
			FailPhotos,
			FailEvents,
			FailNoPermission,
			FailException,
			FailPromoter
		}
	}
}
