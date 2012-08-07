 
 
 
 
 
using System.Linq;
using System.Reflection;
namespace Model.Entities
{
	using System;
 		public abstract partial class Abuse : IAbuse
		{
			public abstract int K { get; set; }
			public abstract int ReportUsrK { get; set; }
			public abstract int AbuseUsrK { get; set; }
			public abstract Model.Entities.ObjectType ObjectType { get; set; }
			public abstract int ObjectK { get; set; }
			public abstract string ObjectString { get; set; }
			public abstract string ReportDescription { get; set; }
			public abstract DateTime ReportDateTime { get; set; }
			public abstract Model.Entities.Abuse.StatusEnum Status { get; set; }
			public abstract DateTime ResolveDateTime { get; set; }
			public abstract Model.Entities.Abuse.ResolveStatusEnum ResolveStatus { get; set; }
			public abstract string ResolveDescription { get; set; }
			public abstract int ResolveUsrK { get; set; }
		}
		public partial interface IAbuse
		{
			int K { get; set; }
			int ReportUsrK { get; set; }
			int AbuseUsrK { get; set; }
			Model.Entities.ObjectType ObjectType { get; set; }
			int ObjectK { get; set; }
			string ObjectString { get; set; }
			string ReportDescription { get; set; }
			DateTime ReportDateTime { get; set; }
			Model.Entities.Abuse.StatusEnum Status { get; set; }
			DateTime ResolveDateTime { get; set; }
			Model.Entities.Abuse.ResolveStatusEnum ResolveStatus { get; set; }
			string ResolveDescription { get; set; }
			int ResolveUsrK { get; set; }
		}
		
 		public abstract partial class Admin : IAdmin
		{
			public abstract int K { get; set; }
			public abstract int UsrK { get; set; }
			public abstract Model.Entities.Admin.AdminObjectType ObjectType { get; set; }
			public abstract int ObjectK { get; set; }
		}
		public partial interface IAdmin
		{
			int K { get; set; }
			int UsrK { get; set; }
			Model.Entities.Admin.AdminObjectType ObjectType { get; set; }
			int ObjectK { get; set; }
		}
		
 		public abstract partial class Article : IArticle
		{
			public abstract int K { get; set; }
			public abstract string Title { get; set; }
			public abstract string Summary { get; set; }
			public abstract Guid Pic { get; set; }
			public abstract int OwnerUsrK { get; set; }
			public abstract DateTime AddedDateTime { get; set; }
			public abstract Model.Entities.Article.StatusEnum Status { get; set; }
			public abstract DateTime EnabledDateTime { get; set; }
			public abstract int EnabledUsrK { get; set; }
			public abstract Model.Entities.Article.RelevanceEnum Relevance { get; set; }
			public abstract Model.Entities.ObjectType ParentObjectType { get; set; }
			public abstract int ParentObjectK { get; set; }
			public abstract int EventK { get; set; }
			public abstract int VenueK { get; set; }
			public abstract int PlaceK { get; set; }
			public abstract int CountryK { get; set; }
			public abstract bool HasSingleThread { get; set; }
			public abstract string AdminNote { get; set; }
			public abstract int Views { get; set; }
			public abstract bool IsWorldwide { get; set; }
			public abstract int TotalComments { get; set; }
			public abstract DateTime LastPost { get; set; }
			public abstract DateTime AverageCommentDateTime { get; set; }
			public abstract string OverrideContents { get; set; }
			public abstract bool HideOwner { get; set; }
			public abstract int PicMiscK { get; set; }
			public abstract string PicState { get; set; }
			public abstract int PicPhotoK { get; set; }
			public abstract string UrlFragment { get; set; }
			public abstract int? ThreadK { get; set; }
			public abstract string TwitterHeadline { get; set; }
			public abstract bool IsMixmagNews { get; set; }
			public abstract bool IsExtendedDisplay { get; set; }
			public abstract int MixmagSections { get; set; }
			public abstract bool ShowAboveFoldOnFrontPage { get; set; }
		}
		public partial interface IArticle
		{
			int K { get; set; }
			string Title { get; set; }
			string Summary { get; set; }
			Guid Pic { get; set; }
			int OwnerUsrK { get; set; }
			DateTime AddedDateTime { get; set; }
			Model.Entities.Article.StatusEnum Status { get; set; }
			DateTime EnabledDateTime { get; set; }
			int EnabledUsrK { get; set; }
			Model.Entities.Article.RelevanceEnum Relevance { get; set; }
			Model.Entities.ObjectType ParentObjectType { get; set; }
			int ParentObjectK { get; set; }
			int EventK { get; set; }
			int VenueK { get; set; }
			int PlaceK { get; set; }
			int CountryK { get; set; }
			bool HasSingleThread { get; set; }
			string AdminNote { get; set; }
			int Views { get; set; }
			bool IsWorldwide { get; set; }
			int TotalComments { get; set; }
			DateTime LastPost { get; set; }
			DateTime AverageCommentDateTime { get; set; }
			string OverrideContents { get; set; }
			bool HideOwner { get; set; }
			int PicMiscK { get; set; }
			string PicState { get; set; }
			int PicPhotoK { get; set; }
			string UrlFragment { get; set; }
			int? ThreadK { get; set; }
			string TwitterHeadline { get; set; }
			bool IsMixmagNews { get; set; }
			bool IsExtendedDisplay { get; set; }
			int MixmagSections { get; set; }
			bool ShowAboveFoldOnFrontPage { get; set; }
		}
		
 		public abstract partial class BacardiEmail : IBacardiEmail
		{
			public abstract int K { get; set; }
			public abstract string Email { get; set; }
		}
		public partial interface IBacardiEmail
		{
			int K { get; set; }
			string Email { get; set; }
		}
		
 		public abstract partial class BankExport : IBankExport
		{
			public abstract int K { get; set; }
			public abstract DateTime AddedDateTime { get; set; }
			public abstract DateTime OutputDateTime { get; set; }
			public abstract DateTime ProcessingDateTime { get; set; }
			public abstract int TransferK { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract Model.Entities.BankExport.Types Type { get; set; }
			public abstract decimal Amount { get; set; }
			public abstract string BatchRef { get; set; }
			public abstract Model.Entities.BankExport.Statuses Status { get; set; }
			public abstract string BankName { get; set; }
			public abstract string BankAccountSortCode { get; set; }
			public abstract string BankAccountNumber { get; set; }
			public abstract string Details { get; set; }
			public abstract DateTime ReferenceDateTime { get; set; }
		}
		public partial interface IBankExport
		{
			int K { get; set; }
			DateTime AddedDateTime { get; set; }
			DateTime OutputDateTime { get; set; }
			DateTime ProcessingDateTime { get; set; }
			int TransferK { get; set; }
			int PromoterK { get; set; }
			Model.Entities.BankExport.Types Type { get; set; }
			decimal Amount { get; set; }
			string BatchRef { get; set; }
			Model.Entities.BankExport.Statuses Status { get; set; }
			string BankName { get; set; }
			string BankAccountSortCode { get; set; }
			string BankAccountNumber { get; set; }
			string Details { get; set; }
			DateTime ReferenceDateTime { get; set; }
		}
		
 		public abstract partial class Banner : IBanner
		{
			public abstract int K { get; set; }
			public abstract string Name { get; set; }
			public abstract DateTime FirstDay { get; set; }
			public abstract DateTime LastDay { get; set; }
			public abstract int EventK { get; set; }
			public abstract Model.Entities.Banner.DisplayTypes DisplayType { get; set; }
			public abstract int MiscK { get; set; }
			public abstract Guid MiscGuid { get; set; }
			public abstract string CustomHtml { get; set; }
			public abstract string CustomXml { get; set; }
			public abstract Model.Entities.Banner.Positions Position { get; set; }
			public abstract Model.Entities.Banner.StatusEnum Status { get; set; }
			public abstract string AdminNote { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract int BrandK { get; set; }
			public abstract string LinkUrl { get; set; }
			public abstract bool IsMusicTargetted { get; set; }
			public abstract bool IsPlaceTargetted { get; set; }
			public abstract Model.Entities.Banner.LinkTargets LinkTarget { get; set; }
			public abstract bool IsPriceFixed { get; set; }
			public abstract double PriceStored { get; set; }
			public abstract int NewMiscK { get; set; }
			public abstract string CustomiseFirstLine { get; set; }
			public abstract int CustomiseFirstLineSize { get; set; }
			public abstract string CustomiseSecondLine { get; set; }
			public abstract string CustomiseThirdLine { get; set; }
			public abstract int FailedMiscK { get; set; }
			public abstract Model.Entities.Banner.DesignTypes DesignType { get; set; }
			public abstract DateTime BuyableLockDateTime { get; set; }
			public abstract bool DesignProcessed { get; set; }
			public abstract int FrequencyCapPerIdentifierPerDay { get; set; }
			public abstract long TargettingProperties0 { get; set; }
			public abstract long TargettingProperties1 { get; set; }
			public abstract int TotalRequiredImpressions { get; set; }
			public abstract int BannerFolderK { get; set; }
			public abstract int VenueK { get; set; }
			public abstract bool AutomaticDates { get; set; }
			public abstract int AutomaticDatesWeeks { get; set; }
			public abstract bool AutomaticTargetting { get; set; }
			public abstract bool AutomaticExposure { get; set; }
			public abstract Model.Entities.Banner.ExposureLevels AutomaticExposureLevel { get; set; }
			public abstract bool StatusEnabled { get; set; }
			public abstract bool StatusBooked { get; set; }
			public abstract bool StatusArtwork { get; set; }
			public abstract bool Refunded { get; set; }
			public abstract int RefundedCredits { get; set; }
			public abstract int RefundCampaignCreditK { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract int PriceCreditsStored { get; set; }
			public abstract double FixedDiscount { get; set; }
			public abstract int Priority { get; set; }
			public abstract bool AlwaysShow { get; set; }
			public abstract bool IsCancelled { get; set; }
			public abstract int? DisplayDuration { get; set; }
		}
		public partial interface IBanner
		{
			int K { get; set; }
			string Name { get; set; }
			DateTime FirstDay { get; set; }
			DateTime LastDay { get; set; }
			int EventK { get; set; }
			Model.Entities.Banner.DisplayTypes DisplayType { get; set; }
			int MiscK { get; set; }
			Guid MiscGuid { get; set; }
			string CustomHtml { get; set; }
			string CustomXml { get; set; }
			Model.Entities.Banner.Positions Position { get; set; }
			Model.Entities.Banner.StatusEnum Status { get; set; }
			string AdminNote { get; set; }
			int UsrK { get; set; }
			int PromoterK { get; set; }
			int BrandK { get; set; }
			string LinkUrl { get; set; }
			bool IsMusicTargetted { get; set; }
			bool IsPlaceTargetted { get; set; }
			Model.Entities.Banner.LinkTargets LinkTarget { get; set; }
			bool IsPriceFixed { get; set; }
			double PriceStored { get; set; }
			int NewMiscK { get; set; }
			string CustomiseFirstLine { get; set; }
			int CustomiseFirstLineSize { get; set; }
			string CustomiseSecondLine { get; set; }
			string CustomiseThirdLine { get; set; }
			int FailedMiscK { get; set; }
			Model.Entities.Banner.DesignTypes DesignType { get; set; }
			DateTime BuyableLockDateTime { get; set; }
			bool DesignProcessed { get; set; }
			int FrequencyCapPerIdentifierPerDay { get; set; }
			long TargettingProperties0 { get; set; }
			long TargettingProperties1 { get; set; }
			int TotalRequiredImpressions { get; set; }
			int BannerFolderK { get; set; }
			int VenueK { get; set; }
			bool AutomaticDates { get; set; }
			int AutomaticDatesWeeks { get; set; }
			bool AutomaticTargetting { get; set; }
			bool AutomaticExposure { get; set; }
			Model.Entities.Banner.ExposureLevels AutomaticExposureLevel { get; set; }
			bool StatusEnabled { get; set; }
			bool StatusBooked { get; set; }
			bool StatusArtwork { get; set; }
			bool Refunded { get; set; }
			int RefundedCredits { get; set; }
			int RefundCampaignCreditK { get; set; }
			Guid DuplicateGuid { get; set; }
			int PriceCreditsStored { get; set; }
			double FixedDiscount { get; set; }
			int Priority { get; set; }
			bool AlwaysShow { get; set; }
			bool IsCancelled { get; set; }
			int? DisplayDuration { get; set; }
		}
		
 		public abstract partial class BannerFolder : IBannerFolder
		{
			public abstract int K { get; set; }
			public abstract string Name { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract DateTime DateTimeCreated { get; set; }
			public abstract int EventK { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
		}
		public partial interface IBannerFolder
		{
			int K { get; set; }
			string Name { get; set; }
			int PromoterK { get; set; }
			DateTime DateTimeCreated { get; set; }
			int EventK { get; set; }
			Guid DuplicateGuid { get; set; }
		}
		
 		public abstract partial class BannerMusicType : IBannerMusicType
		{
			public abstract int BannerK { get; set; }
			public abstract int MusicTypeK { get; set; }
			public abstract bool Chosen { get; set; }
		}
		public partial interface IBannerMusicType
		{
			int BannerK { get; set; }
			int MusicTypeK { get; set; }
			bool Chosen { get; set; }
		}
		
 		public abstract partial class BannerPlace : IBannerPlace
		{
			public abstract int BannerK { get; set; }
			public abstract int PlaceK { get; set; }
		}
		public partial interface IBannerPlace
		{
			int BannerK { get; set; }
			int PlaceK { get; set; }
		}
		
 		public abstract partial class BannerStat : IBannerStat
		{
			public abstract int BannerK { get; set; }
			public abstract DateTime Date { get; set; }
			public abstract int Hits { get; set; }
			public abstract int Clicks { get; set; }
			public abstract int HitsTargetted { get; set; }
			public abstract int HitsPlaceTargetted { get; set; }
			public abstract int HitsMusicTargetted { get; set; }
			public abstract int ClicksPlaceTargetted { get; set; }
			public abstract int ClicksMusicTargetted { get; set; }
		}
		public partial interface IBannerStat
		{
			int BannerK { get; set; }
			DateTime Date { get; set; }
			int Hits { get; set; }
			int Clicks { get; set; }
			int HitsTargetted { get; set; }
			int HitsPlaceTargetted { get; set; }
			int HitsMusicTargetted { get; set; }
			int ClicksPlaceTargetted { get; set; }
			int ClicksMusicTargetted { get; set; }
		}
		
 		public abstract partial class BinRange : IBinRange
		{
			public abstract int Low { get; set; }
			public abstract int High { get; set; }
			public abstract Model.Entities.BinRange.Types Type { get; set; }
			public abstract int Order { get; set; }
		}
		public partial interface IBinRange
		{
			int Low { get; set; }
			int High { get; set; }
			Model.Entities.BinRange.Types Type { get; set; }
			int Order { get; set; }
		}
		
 		public abstract partial class Brand : IBrand
		{
			public abstract int K { get; set; }
			public abstract string Name { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract Guid Pic { get; set; }
			public abstract int OwnerUsrK { get; set; }
			public abstract bool IsNew { get; set; }
			public abstract bool IsEdited { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract Model.Entities.Brand.PromoterStatusEnum PromoterStatus { get; set; }
			public abstract string UrlName { get; set; }
			public abstract string PicState { get; set; }
			public abstract int PicPhotoK { get; set; }
			public abstract int PicMiscK { get; set; }
			public abstract int GroupK { get; set; }
			public abstract int TotalComments { get; set; }
			public abstract DateTime LastPost { get; set; }
			public abstract DateTime AverageCommentDateTime { get; set; }
			public abstract DateTime DateTimeCreated { get; set; }
			public abstract bool NoPhotos { get; set; }
			public abstract bool AddedRegulars { get; set; }
			public abstract string StyledCss { get; set; }
			public abstract string StyledXml { get; set; }
		}
		public partial interface IBrand
		{
			int K { get; set; }
			string Name { get; set; }
			int PromoterK { get; set; }
			Guid Pic { get; set; }
			int OwnerUsrK { get; set; }
			bool IsNew { get; set; }
			bool IsEdited { get; set; }
			Guid DuplicateGuid { get; set; }
			Model.Entities.Brand.PromoterStatusEnum PromoterStatus { get; set; }
			string UrlName { get; set; }
			string PicState { get; set; }
			int PicPhotoK { get; set; }
			int PicMiscK { get; set; }
			int GroupK { get; set; }
			int TotalComments { get; set; }
			DateTime LastPost { get; set; }
			DateTime AverageCommentDateTime { get; set; }
			DateTime DateTimeCreated { get; set; }
			bool NoPhotos { get; set; }
			bool AddedRegulars { get; set; }
			string StyledCss { get; set; }
			string StyledXml { get; set; }
		}
		
 		public abstract partial class Buddy : IBuddy
		{
			public abstract int K { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int BuddyUsrK { get; set; }
			public abstract bool FullBuddy { get; set; }
			public abstract DateTime LastPopupHoldOff { get; set; }
			public abstract bool CanInvite { get; set; }
			public abstract bool CanBuddyInvite { get; set; }
			public abstract bool Denied { get; set; }
			public abstract Model.Entities.Buddy.BuddyFindingMethod BuddyFoundByMethod { get; set; }
			public abstract string SkeletonName { get; set; }
		}
		public partial interface IBuddy
		{
			int K { get; set; }
			int UsrK { get; set; }
			int BuddyUsrK { get; set; }
			bool FullBuddy { get; set; }
			DateTime LastPopupHoldOff { get; set; }
			bool CanInvite { get; set; }
			bool CanBuddyInvite { get; set; }
			bool Denied { get; set; }
			Model.Entities.Buddy.BuddyFindingMethod BuddyFoundByMethod { get; set; }
			string SkeletonName { get; set; }
		}
		
 		public abstract partial class CampaignCredit : ICampaignCredit
		{
			public abstract int K { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract DateTime ActionDateTime { get; set; }
			public abstract Model.Entities.ObjectType BuyableObjectType { get; set; }
			public abstract int BuyableObjectK { get; set; }
			public abstract DateTime BuyableLockDateTime { get; set; }
			public abstract Model.Entities.InvoiceItem.Types InvoiceItemType { get; set; }
			public abstract string Description { get; set; }
			public abstract int Credits { get; set; }
			public abstract bool Enabled { get; set; }
			public abstract int BalanceToDate { get; set; }
			public abstract int DisplayOrder { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int ActionUsrK { get; set; }
			public abstract string Notes { get; set; }
			public abstract double FixedDiscount { get; set; }
			public abstract bool IsPriceFixed { get; set; }
		}
		public partial interface ICampaignCredit
		{
			int K { get; set; }
			int PromoterK { get; set; }
			DateTime ActionDateTime { get; set; }
			Model.Entities.ObjectType BuyableObjectType { get; set; }
			int BuyableObjectK { get; set; }
			DateTime BuyableLockDateTime { get; set; }
			Model.Entities.InvoiceItem.Types InvoiceItemType { get; set; }
			string Description { get; set; }
			int Credits { get; set; }
			bool Enabled { get; set; }
			int BalanceToDate { get; set; }
			int DisplayOrder { get; set; }
			int UsrK { get; set; }
			int ActionUsrK { get; set; }
			string Notes { get; set; }
			double FixedDiscount { get; set; }
			bool IsPriceFixed { get; set; }
		}
		
 		public abstract partial class Chat : IChat
		{
			public abstract int UsrK { get; set; }
			public abstract string ChatItems { get; set; }
			public abstract long LastChatItem { get; set; }
			public abstract int SessionId { get; set; }
		}
		public partial interface IChat
		{
			int UsrK { get; set; }
			string ChatItems { get; set; }
			long LastChatItem { get; set; }
			int SessionId { get; set; }
		}
		
 		public abstract partial class ChatMessage : IChatMessage
		{
			public abstract int K { get; set; }
			public abstract string Text { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract int UsrK { get; set; }
			public abstract Guid RoomGuid { get; set; }
			public abstract Guid? ChatItemGuid { get; set; }
			public abstract bool? Deleted { get; set; }
		}
		public partial interface IChatMessage
		{
			int K { get; set; }
			string Text { get; set; }
			DateTime DateTime { get; set; }
			int UsrK { get; set; }
			Guid RoomGuid { get; set; }
			Guid? ChatItemGuid { get; set; }
			bool? Deleted { get; set; }
		}
		
 		public abstract partial class ClubDetails : IClubDetails
		{
			public abstract int K { get; set; }
			public abstract string Company { get; set; }
			public abstract string WebLink { get; set; }
			public abstract string Telephone { get; set; }
			public abstract string Address { get; set; }
			public abstract string PostCode { get; set; }
			public abstract string ExtraInfo { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract int VenueK { get; set; }
			public abstract DateTime DoneDate { get; set; }
			public abstract int Dead { get; set; }
		}
		public partial interface IClubDetails
		{
			int K { get; set; }
			string Company { get; set; }
			string WebLink { get; set; }
			string Telephone { get; set; }
			string Address { get; set; }
			string PostCode { get; set; }
			string ExtraInfo { get; set; }
			int PromoterK { get; set; }
			int VenueK { get; set; }
			DateTime DoneDate { get; set; }
			int Dead { get; set; }
		}
		
 		public abstract partial class Comment : IComment
		{
			public abstract int K { get; set; }
			public abstract string Text { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract int ThreadK { get; set; }
			public abstract int UsrK { get; set; }
			public abstract bool Enabled { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract bool IsEdited { get; set; }
			public abstract DateTime EditDateTime { get; set; }
			public abstract int LolCount { get; set; }
			public abstract int IndexInThread { get; set; }
			public abstract string Ip { get; set; }
			public abstract Guid? ChatItemGuid { get; set; }
		}
		public partial interface IComment
		{
			int K { get; set; }
			string Text { get; set; }
			DateTime DateTime { get; set; }
			int ThreadK { get; set; }
			int UsrK { get; set; }
			bool Enabled { get; set; }
			Guid DuplicateGuid { get; set; }
			bool IsEdited { get; set; }
			DateTime EditDateTime { get; set; }
			int LolCount { get; set; }
			int IndexInThread { get; set; }
			string Ip { get; set; }
			Guid? ChatItemGuid { get; set; }
		}
		
 		public abstract partial class CommentAlert : ICommentAlert
		{
			public abstract int UsrK { get; set; }
			public abstract Model.Entities.ObjectType ParentObjectType { get; set; }
			public abstract int ParentObjectK { get; set; }
		}
		public partial interface ICommentAlert
		{
			int UsrK { get; set; }
			Model.Entities.ObjectType ParentObjectType { get; set; }
			int ParentObjectK { get; set; }
		}
		
 		public abstract partial class Comp : IComp
		{
			public abstract int K { get; set; }
			public abstract DateTime DateTimeAdded { get; set; }
			public abstract DateTime DateTimeStart { get; set; }
			public abstract DateTime DateTimeClose { get; set; }
			public abstract string Question { get; set; }
			public abstract string Answer1 { get; set; }
			public abstract string Answer2 { get; set; }
			public abstract string Answer3 { get; set; }
			public abstract int CorrectAnswer { get; set; }
			public abstract string Prize { get; set; }
			public abstract string Prize2 { get; set; }
			public abstract string Prize3 { get; set; }
			public abstract string SponsorDetails { get; set; }
			public abstract int Winners { get; set; }
			public abstract int Winners2 { get; set; }
			public abstract int Winners3 { get; set; }
			public abstract bool WinnersPicked { get; set; }
			public abstract int OwnerUsrK { get; set; }
			public abstract string IconFilename { get; set; }
			public abstract int PrizeValueRange { get; set; }
			public abstract int Entries { get; set; }
			public abstract Guid Pic { get; set; }
			public abstract Guid PicOriginal { get; set; }
			public abstract Model.Entities.Comp.DisplayTypes DisplayType { get; set; }
			public abstract Model.Entities.Comp.StatusEnum Status { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract int BrandK { get; set; }
			public abstract int EventK { get; set; }
			public abstract Model.Entities.Comp.LinkTypes LinkType { get; set; }
			public abstract string PicState { get; set; }
			public abstract int PicPhotoK { get; set; }
			public abstract int PicMiscK { get; set; }
			public abstract bool IsHtmlOverride { get; set; }
		}
		public partial interface IComp
		{
			int K { get; set; }
			DateTime DateTimeAdded { get; set; }
			DateTime DateTimeStart { get; set; }
			DateTime DateTimeClose { get; set; }
			string Question { get; set; }
			string Answer1 { get; set; }
			string Answer2 { get; set; }
			string Answer3 { get; set; }
			int CorrectAnswer { get; set; }
			string Prize { get; set; }
			string Prize2 { get; set; }
			string Prize3 { get; set; }
			string SponsorDetails { get; set; }
			int Winners { get; set; }
			int Winners2 { get; set; }
			int Winners3 { get; set; }
			bool WinnersPicked { get; set; }
			int OwnerUsrK { get; set; }
			string IconFilename { get; set; }
			int PrizeValueRange { get; set; }
			int Entries { get; set; }
			Guid Pic { get; set; }
			Guid PicOriginal { get; set; }
			Model.Entities.Comp.DisplayTypes DisplayType { get; set; }
			Model.Entities.Comp.StatusEnum Status { get; set; }
			int PromoterK { get; set; }
			int BrandK { get; set; }
			int EventK { get; set; }
			Model.Entities.Comp.LinkTypes LinkType { get; set; }
			string PicState { get; set; }
			int PicPhotoK { get; set; }
			int PicMiscK { get; set; }
			bool IsHtmlOverride { get; set; }
		}
		
 		public abstract partial class CompEntry : ICompEntry
		{
			public abstract int CompK { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int Answer { get; set; }
			public abstract bool Winner { get; set; }
			public abstract int Prize { get; set; }
			public abstract int WinnerThreadK { get; set; }
		}
		public partial interface ICompEntry
		{
			int CompK { get; set; }
			int UsrK { get; set; }
			int Answer { get; set; }
			bool Winner { get; set; }
			int Prize { get; set; }
			int WinnerThreadK { get; set; }
		}
		
 		public abstract partial class Country : ICountry
		{
			public abstract int K { get; set; }
			public abstract string Name { get; set; }
			public abstract string CurrencyCode { get; set; }
			public abstract string CurrencyName { get; set; }
			public abstract int CurrencyDecimals { get; set; }
			public abstract Model.Entities.Country.BillingRegionEnum Region { get; set; }
			public abstract string Code2Letter { get; set; }
			public abstract string Code3Letter { get; set; }
			public abstract string Code3Number { get; set; }
			public abstract string EuVatCodePrefix { get; set; }
			public abstract int PlacePopulationMinimum { get; set; }
			public abstract string FriendlyName { get; set; }
			public abstract int PostcodeType { get; set; }
			public abstract bool Mature { get; set; }
			public abstract bool UseRegion { get; set; }
			public abstract string RegionName { get; set; }
			public abstract bool Enabled { get; set; }
			public abstract int MinEventsForPlaceMenu { get; set; }
			public abstract int DialingCode { get; set; }
			public abstract int TotalEvents { get; set; }
			public abstract string UrlName { get; set; }
			public abstract string CustomHtml { get; set; }
			public abstract Model.Entities.Country.PostageZones PostageZone { get; set; }
		}
		public partial interface ICountry
		{
			int K { get; set; }
			string Name { get; set; }
			string CurrencyCode { get; set; }
			string CurrencyName { get; set; }
			int CurrencyDecimals { get; set; }
			Model.Entities.Country.BillingRegionEnum Region { get; set; }
			string Code2Letter { get; set; }
			string Code3Letter { get; set; }
			string Code3Number { get; set; }
			string EuVatCodePrefix { get; set; }
			int PlacePopulationMinimum { get; set; }
			string FriendlyName { get; set; }
			int PostcodeType { get; set; }
			bool Mature { get; set; }
			bool UseRegion { get; set; }
			string RegionName { get; set; }
			bool Enabled { get; set; }
			int MinEventsForPlaceMenu { get; set; }
			int DialingCode { get; set; }
			int TotalEvents { get; set; }
			string UrlName { get; set; }
			string CustomHtml { get; set; }
			Model.Entities.Country.PostageZones PostageZone { get; set; }
		}
		
 		public abstract partial class Demographics : IDemographics
		{
			public abstract Guid Guid { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract bool DrinkWater { get; set; }
			public abstract bool DrinkSoft { get; set; }
			public abstract bool DrinkEnergy { get; set; }
			public abstract bool DrinkDraftBeer { get; set; }
			public abstract bool DrinkBottledBeer { get; set; }
			public abstract bool DrinkSpirits { get; set; }
			public abstract bool DrinkWine { get; set; }
			public abstract bool DrinkAlcopops { get; set; }
			public abstract bool DrinkCider { get; set; }
			public abstract int Smoke { get; set; }
			public abstract double EveningAllNight { get; set; }
			public abstract double EveningLateNight { get; set; }
			public abstract double EveningCoupleDrinks { get; set; }
			public abstract double EveningOther { get; set; }
			public abstract double EveningStayIn { get; set; }
			public abstract int Employment { get; set; }
			public abstract int Salary { get; set; }
			public abstract bool CreditCard { get; set; }
			public abstract bool Loan { get; set; }
			public abstract bool Mortgage { get; set; }
			public abstract bool OwnCar { get; set; }
			public abstract bool OwnBike { get; set; }
			public abstract bool OwnMp3 { get; set; }
			public abstract bool OwnPc { get; set; }
			public abstract bool OwnLaptop { get; set; }
			public abstract bool OwnMac { get; set; }
			public abstract bool OwnBroadband { get; set; }
			public abstract bool OwnConsole { get; set; }
			public abstract bool OwnCamera { get; set; }
			public abstract bool OwnDvd { get; set; }
			public abstract bool OwnDvdRec { get; set; }
			public abstract bool BuyCar { get; set; }
			public abstract bool BuyBike { get; set; }
			public abstract bool BuyMp3 { get; set; }
			public abstract bool BuyPc { get; set; }
			public abstract bool BuyLaptop { get; set; }
			public abstract bool BuyMac { get; set; }
			public abstract bool BuyBroadband { get; set; }
			public abstract bool BuyConsole { get; set; }
			public abstract bool BuyCamera { get; set; }
			public abstract bool BuyDvd { get; set; }
			public abstract bool BuyDvdRec { get; set; }
			public abstract int SpendDesignerClothes { get; set; }
			public abstract int SpendHighStreetClothes { get; set; }
			public abstract int SpendMusicCd { get; set; }
			public abstract int SpendMusicVinyl { get; set; }
			public abstract int SpendMusicDownload { get; set; }
			public abstract int SpendDvd { get; set; }
			public abstract int SpendGames { get; set; }
			public abstract int SpendMobile { get; set; }
			public abstract int SpendSms { get; set; }
			public abstract int SpendCar { get; set; }
			public abstract int SpendTravel { get; set; }
			public abstract int Holidays { get; set; }
			public abstract string ImagingManufacturer { get; set; }
			public abstract int ImagingImportant { get; set; }
			public abstract int ImagingOpinionSony { get; set; }
			public abstract int ImagingOpinionNokia { get; set; }
			public abstract int ImagingOpinionMotorola { get; set; }
			public abstract int ImagingOpinionSiemens { get; set; }
			public abstract int ImagingOpinionLg { get; set; }
			public abstract int ImagingOpinionSamsung { get; set; }
			public abstract int ImagingCapabilitySony { get; set; }
			public abstract int ImagingCapabilityNokia { get; set; }
			public abstract int ImagingCapabilityMotorola { get; set; }
			public abstract int ImagingCapabilitySiemens { get; set; }
			public abstract int ImagingCapabilityLg { get; set; }
			public abstract int ImagingCapabilitySamsung { get; set; }
			public abstract int ImagingBuySony { get; set; }
			public abstract int ImagingBuyNokia { get; set; }
			public abstract int ImagingBuyMotorola { get; set; }
			public abstract int ImagingBuySiemens { get; set; }
			public abstract int ImagingBuyLg { get; set; }
			public abstract int ImagingBuySamsung { get; set; }
		}
		public partial interface IDemographics
		{
			Guid Guid { get; set; }
			DateTime DateTime { get; set; }
			bool DrinkWater { get; set; }
			bool DrinkSoft { get; set; }
			bool DrinkEnergy { get; set; }
			bool DrinkDraftBeer { get; set; }
			bool DrinkBottledBeer { get; set; }
			bool DrinkSpirits { get; set; }
			bool DrinkWine { get; set; }
			bool DrinkAlcopops { get; set; }
			bool DrinkCider { get; set; }
			int Smoke { get; set; }
			double EveningAllNight { get; set; }
			double EveningLateNight { get; set; }
			double EveningCoupleDrinks { get; set; }
			double EveningOther { get; set; }
			double EveningStayIn { get; set; }
			int Employment { get; set; }
			int Salary { get; set; }
			bool CreditCard { get; set; }
			bool Loan { get; set; }
			bool Mortgage { get; set; }
			bool OwnCar { get; set; }
			bool OwnBike { get; set; }
			bool OwnMp3 { get; set; }
			bool OwnPc { get; set; }
			bool OwnLaptop { get; set; }
			bool OwnMac { get; set; }
			bool OwnBroadband { get; set; }
			bool OwnConsole { get; set; }
			bool OwnCamera { get; set; }
			bool OwnDvd { get; set; }
			bool OwnDvdRec { get; set; }
			bool BuyCar { get; set; }
			bool BuyBike { get; set; }
			bool BuyMp3 { get; set; }
			bool BuyPc { get; set; }
			bool BuyLaptop { get; set; }
			bool BuyMac { get; set; }
			bool BuyBroadband { get; set; }
			bool BuyConsole { get; set; }
			bool BuyCamera { get; set; }
			bool BuyDvd { get; set; }
			bool BuyDvdRec { get; set; }
			int SpendDesignerClothes { get; set; }
			int SpendHighStreetClothes { get; set; }
			int SpendMusicCd { get; set; }
			int SpendMusicVinyl { get; set; }
			int SpendMusicDownload { get; set; }
			int SpendDvd { get; set; }
			int SpendGames { get; set; }
			int SpendMobile { get; set; }
			int SpendSms { get; set; }
			int SpendCar { get; set; }
			int SpendTravel { get; set; }
			int Holidays { get; set; }
			string ImagingManufacturer { get; set; }
			int ImagingImportant { get; set; }
			int ImagingOpinionSony { get; set; }
			int ImagingOpinionNokia { get; set; }
			int ImagingOpinionMotorola { get; set; }
			int ImagingOpinionSiemens { get; set; }
			int ImagingOpinionLg { get; set; }
			int ImagingOpinionSamsung { get; set; }
			int ImagingCapabilitySony { get; set; }
			int ImagingCapabilityNokia { get; set; }
			int ImagingCapabilityMotorola { get; set; }
			int ImagingCapabilitySiemens { get; set; }
			int ImagingCapabilityLg { get; set; }
			int ImagingCapabilitySamsung { get; set; }
			int ImagingBuySony { get; set; }
			int ImagingBuyNokia { get; set; }
			int ImagingBuyMotorola { get; set; }
			int ImagingBuySiemens { get; set; }
			int ImagingBuyLg { get; set; }
			int ImagingBuySamsung { get; set; }
		}
		
 		public abstract partial class Domain : IDomain
		{
			public abstract int K { get; set; }
			public abstract string DomainName { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract Model.Entities.ObjectType RedirectObjectType { get; set; }
			public abstract int RedirectObjectK { get; set; }
			public abstract string RedirectUrl { get; set; }
			public abstract string RedirectApp { get; set; }
			public abstract string WwdResourceID { get; set; }
			public abstract bool RegisteredPrimary { get; set; }
			public abstract bool RegisteredSecondary { get; set; }
		}
		public partial interface IDomain
		{
			int K { get; set; }
			string DomainName { get; set; }
			int PromoterK { get; set; }
			Model.Entities.ObjectType RedirectObjectType { get; set; }
			int RedirectObjectK { get; set; }
			string RedirectUrl { get; set; }
			string RedirectApp { get; set; }
			string WwdResourceID { get; set; }
			bool RegisteredPrimary { get; set; }
			bool RegisteredSecondary { get; set; }
		}
		
 		public abstract partial class DomainStats : IDomainStats
		{
			public abstract int DomainK { get; set; }
			public abstract DateTime Date { get; set; }
			public abstract int Hits { get; set; }
		}
		public partial interface IDomainStats
		{
			int DomainK { get; set; }
			DateTime Date { get; set; }
			int Hits { get; set; }
		}
		
 		public abstract partial class DonationIcon : IDonationIcon
		{
			public abstract int K { get; set; }
			public abstract string IconName { get; set; }
			public abstract string IconText { get; set; }
			public abstract string ImgUrl { get; set; }
			public abstract int ThreadK { get; set; }
			public abstract DateTime StartDateTime { get; set; }
			public abstract decimal PriceWhenActive { get; set; }
			public abstract decimal PriceWhenRetroactive { get; set; }
			public abstract Model.Entities.DonationIcon.Control DonatePageControl { get; set; }
			public abstract string DonatePageHeader { get; set; }
			public abstract string DonatePageCenterText { get; set; }
			public abstract string DonatePageLine1Text { get; set; }
			public abstract string DonatePageLine2Text { get; set; }
			public abstract bool Enabled { get; set; }
			public abstract Guid? ImgGuid { get; set; }
			public abstract string ImgExtension { get; set; }
			public abstract bool? Vatable { get; set; }
			public abstract string Description { get; set; }
			public abstract bool Charity { get; set; }
		}
		public partial interface IDonationIcon
		{
			int K { get; set; }
			string IconName { get; set; }
			string IconText { get; set; }
			string ImgUrl { get; set; }
			int ThreadK { get; set; }
			DateTime StartDateTime { get; set; }
			decimal PriceWhenActive { get; set; }
			decimal PriceWhenRetroactive { get; set; }
			Model.Entities.DonationIcon.Control DonatePageControl { get; set; }
			string DonatePageHeader { get; set; }
			string DonatePageCenterText { get; set; }
			string DonatePageLine1Text { get; set; }
			string DonatePageLine2Text { get; set; }
			bool Enabled { get; set; }
			Guid? ImgGuid { get; set; }
			string ImgExtension { get; set; }
			bool? Vatable { get; set; }
			string Description { get; set; }
			bool Charity { get; set; }
		}
		
 		public abstract partial class Event : IEvent
		{
			public abstract int K { get; set; }
			public abstract string Name { get; set; }
			public abstract string ShortDetailsHtml { get; set; }
			public abstract string LongDetailsHtml { get; set; }
			public abstract bool LongDetailsPlain { get; set; }
			public abstract Guid Pic { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract int VenueK { get; set; }
			public abstract string AdminNote { get; set; }
			public abstract int Capacity { get; set; }
			public abstract int OwnerUsrK { get; set; }
			public abstract Guid PicNew { get; set; }
			public abstract int TotalComments { get; set; }
			public abstract DateTime LastPost { get; set; }
			public abstract DateTime AverageCommentDateTime { get; set; }
			public abstract int TotalPhotos { get; set; }
			public abstract int LivePhotos { get; set; }
			public abstract DateTime AddedDateTime { get; set; }
			public abstract bool HasGuestlist { get; set; }
			public abstract DateTime LastLivePhoto { get; set; }
			public abstract bool HasSpotter { get; set; }
			public abstract bool GuestlistOpen { get; set; }
			public abstract bool GuestlistFinished { get; set; }
			public abstract int GuestlistLimit { get; set; }
			public abstract int GuestlistCount { get; set; }
			public abstract string GuestlistDetails { get; set; }
			public abstract int GuestlistPromoterK { get; set; }
			public abstract double GuestlistRegularPrice { get; set; }
			public abstract double GuestlistPrice { get; set; }
			public abstract bool GuestlistPromotion { get; set; }
			public abstract Model.Entities.Event.StartTimes StartTime { get; set; }
			public abstract string AdminEmail { get; set; }
			public abstract bool Donated { get; set; }
			public abstract bool IsDescriptionText { get; set; }
			public abstract bool IsNew { get; set; }
			public abstract bool IsDescriptionCleanHtml { get; set; }
			public abstract bool IsEdited { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract string PicState { get; set; }
			public abstract int PicPhotoK { get; set; }
			public abstract int PicMiscK { get; set; }
			public abstract string UrlFragment { get; set; }
			public abstract string MusicTypesString { get; set; }
			public abstract int ModeratorUsrK { get; set; }
			public abstract DateTime BuyableLockDateTime { get; set; }
			public abstract bool IsTicketsAvailable { get; set; }
			public abstract double TicketHeat { get; set; }
			public abstract bool HasHilight { get; set; }
			public abstract int UsrAttendCount { get; set; }
			public abstract double FixedDiscount { get; set; }
			public abstract bool IsPriceFixed { get; set; }
			public abstract bool? DontShowHotelLink { get; set; }
			public abstract bool? SpotterRequest { get; set; }
			public abstract string SpotterRequestName { get; set; }
			public abstract string SpotterRequestNumber { get; set; }
			public abstract long? FacebookEventId { get; set; }
		}
		public partial interface IEvent
		{
			int K { get; set; }
			string Name { get; set; }
			string ShortDetailsHtml { get; set; }
			string LongDetailsHtml { get; set; }
			bool LongDetailsPlain { get; set; }
			Guid Pic { get; set; }
			DateTime DateTime { get; set; }
			int VenueK { get; set; }
			string AdminNote { get; set; }
			int Capacity { get; set; }
			int OwnerUsrK { get; set; }
			Guid PicNew { get; set; }
			int TotalComments { get; set; }
			DateTime LastPost { get; set; }
			DateTime AverageCommentDateTime { get; set; }
			int TotalPhotos { get; set; }
			int LivePhotos { get; set; }
			DateTime AddedDateTime { get; set; }
			bool HasGuestlist { get; set; }
			DateTime LastLivePhoto { get; set; }
			bool HasSpotter { get; set; }
			bool GuestlistOpen { get; set; }
			bool GuestlistFinished { get; set; }
			int GuestlistLimit { get; set; }
			int GuestlistCount { get; set; }
			string GuestlistDetails { get; set; }
			int GuestlistPromoterK { get; set; }
			double GuestlistRegularPrice { get; set; }
			double GuestlistPrice { get; set; }
			bool GuestlistPromotion { get; set; }
			Model.Entities.Event.StartTimes StartTime { get; set; }
			string AdminEmail { get; set; }
			bool Donated { get; set; }
			bool IsDescriptionText { get; set; }
			bool IsNew { get; set; }
			bool IsDescriptionCleanHtml { get; set; }
			bool IsEdited { get; set; }
			Guid DuplicateGuid { get; set; }
			string PicState { get; set; }
			int PicPhotoK { get; set; }
			int PicMiscK { get; set; }
			string UrlFragment { get; set; }
			string MusicTypesString { get; set; }
			int ModeratorUsrK { get; set; }
			DateTime BuyableLockDateTime { get; set; }
			bool IsTicketsAvailable { get; set; }
			double TicketHeat { get; set; }
			bool HasHilight { get; set; }
			int UsrAttendCount { get; set; }
			double FixedDiscount { get; set; }
			bool IsPriceFixed { get; set; }
			bool? DontShowHotelLink { get; set; }
			bool? SpotterRequest { get; set; }
			string SpotterRequestName { get; set; }
			string SpotterRequestNumber { get; set; }
			long? FacebookEventId { get; set; }
		}
		
 		public abstract partial class EventBrand : IEventBrand
		{
			public abstract int EventK { get; set; }
			public abstract int BrandK { get; set; }
		}
		public partial interface IEventBrand
		{
			int EventK { get; set; }
			int BrandK { get; set; }
		}
		
 		public abstract partial class EventMusicType : IEventMusicType
		{
			public abstract int EventK { get; set; }
			public abstract int MusicTypeK { get; set; }
		}
		public partial interface IEventMusicType
		{
			int EventK { get; set; }
			int MusicTypeK { get; set; }
		}
		
 		public abstract partial class EventPromoter : IEventPromoter
		{
			public abstract int EventK { get; set; }
			public abstract int? PromoterK { get; set; }
		}
		public partial interface IEventPromoter
		{
			int EventK { get; set; }
			int? PromoterK { get; set; }
		}
		
 		public abstract partial class FacebookPost : IFacebookPost
		{
			public abstract int K { get; set; }
			public abstract DateTime? DateTime { get; set; }
			public abstract Model.Entities.FacebookPost.TypeEnum Type { get; set; }
			public abstract int? UsrK { get; set; }
			public abstract string Content { get; set; }
			public abstract long? FacebookUid { get; set; }
			public abstract int DataInt { get; set; }
			public abstract int? Hits { get; set; }
		}
		public partial interface IFacebookPost
		{
			int K { get; set; }
			DateTime? DateTime { get; set; }
			Model.Entities.FacebookPost.TypeEnum Type { get; set; }
			int? UsrK { get; set; }
			string Content { get; set; }
			long? FacebookUid { get; set; }
			int DataInt { get; set; }
			int? Hits { get; set; }
		}
		
 		public abstract partial class Fiat500Entry : IFiat500Entry
		{
			public abstract int K { get; set; }
			public abstract int UsrK { get; set; }
			public abstract DateTime Submitted { get; set; }
			public abstract string FirstName { get; set; }
			public abstract string LastName { get; set; }
			public abstract string MobileNumber { get; set; }
			public abstract string EmailAddress { get; set; }
			public abstract string HouseNumberAndStreetName { get; set; }
			public abstract string Town { get; set; }
			public abstract string City { get; set; }
			public abstract string County { get; set; }
			public abstract string PostCode { get; set; }
			public abstract bool AcceptConditions { get; set; }
			public abstract int NumberOfKids { get; set; }
			public abstract bool NotifyByEmail { get; set; }
			public abstract bool NotifyByPost { get; set; }
			public abstract bool NotifyByPhone { get; set; }
			public abstract bool NotifyBySms { get; set; }
		}
		public partial interface IFiat500Entry
		{
			int K { get; set; }
			int UsrK { get; set; }
			DateTime Submitted { get; set; }
			string FirstName { get; set; }
			string LastName { get; set; }
			string MobileNumber { get; set; }
			string EmailAddress { get; set; }
			string HouseNumberAndStreetName { get; set; }
			string Town { get; set; }
			string City { get; set; }
			string County { get; set; }
			string PostCode { get; set; }
			bool AcceptConditions { get; set; }
			int NumberOfKids { get; set; }
			bool NotifyByEmail { get; set; }
			bool NotifyByPost { get; set; }
			bool NotifyByPhone { get; set; }
			bool NotifyBySms { get; set; }
		}
		
 		public abstract partial class Flyer : IFlyer
		{
			public abstract int K { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract string Name { get; set; }
			public abstract string Subject { get; set; }
			public abstract string BackgroundColor { get; set; }
			public abstract int MiscK { get; set; }
			public abstract DateTime SendDateTime { get; set; }
			public abstract string LinkTargetUrl { get; set; }
			public abstract string PlaceKs { get; set; }
			public abstract string MusicTypeKs { get; set; }
			public abstract int Sends { get; set; }
			public abstract int Views { get; set; }
			public abstract int Clicks { get; set; }
			public abstract int Unsubscribes { get; set; }
			public abstract string MailFromDisplayName { get; set; }
			public abstract bool PromotersOnly { get; set; }
			public abstract bool IsReadyToSend { get; set; }
			public abstract bool IsSending { get; set; }
			public abstract int PausedAtUsrK { get; set; }
			public abstract bool HasFinishedSending { get; set; }
			public abstract string EventKs { get; set; }
			public abstract DateTime? DateTimeLastMessageSent { get; set; }
			public abstract bool IsHtml { get; set; }
			public abstract string Html { get; set; }
			public abstract string TextAlternative { get; set; }
			public abstract int Broken { get; set; }
			public abstract int Exceptions { get; set; }
			public abstract int MailServerRetries { get; set; }
			public abstract DateTime? MailServerLastRetry { get; set; }
		}
		public partial interface IFlyer
		{
			int K { get; set; }
			int PromoterK { get; set; }
			string Name { get; set; }
			string Subject { get; set; }
			string BackgroundColor { get; set; }
			int MiscK { get; set; }
			DateTime SendDateTime { get; set; }
			string LinkTargetUrl { get; set; }
			string PlaceKs { get; set; }
			string MusicTypeKs { get; set; }
			int Sends { get; set; }
			int Views { get; set; }
			int Clicks { get; set; }
			int Unsubscribes { get; set; }
			string MailFromDisplayName { get; set; }
			bool PromotersOnly { get; set; }
			bool IsReadyToSend { get; set; }
			bool IsSending { get; set; }
			int PausedAtUsrK { get; set; }
			bool HasFinishedSending { get; set; }
			string EventKs { get; set; }
			DateTime? DateTimeLastMessageSent { get; set; }
			bool IsHtml { get; set; }
			string Html { get; set; }
			string TextAlternative { get; set; }
			int Broken { get; set; }
			int Exceptions { get; set; }
			int MailServerRetries { get; set; }
			DateTime? MailServerLastRetry { get; set; }
		}
		
 		public abstract partial class Gallery : IGallery
		{
			public abstract int K { get; set; }
			public abstract int EventK { get; set; }
			public abstract int ArticleK { get; set; }
			public abstract string Name { get; set; }
			public abstract int MainPhotoK { get; set; }
			public abstract int OwnerUsrK { get; set; }
			public abstract int TotalPhotos { get; set; }
			public abstract int LivePhotos { get; set; }
			public abstract DateTime CreateDateTime { get; set; }
			public abstract DateTime LastLiveDateTime { get; set; }
			public abstract string AdminNote { get; set; }
			public abstract bool IsMobile { get; set; }
			public abstract string PicState { get; set; }
			public abstract int PicPhotoK { get; set; }
			public abstract int PicMiscK { get; set; }
			public abstract string UrlFragment { get; set; }
			public abstract int ModeratorUsrK { get; set; }
			public abstract int CurrentPackageCount { get; set; }
			public abstract DateTime LastPackageDateTime { get; set; }
			public abstract int LastPackageIndex { get; set; }
			public abstract bool UploadInProgress { get; set; }
			public abstract int UploadFails { get; set; }
			public abstract bool? WatchUploads { get; set; }
			public abstract bool? RunFinishedUploadingTask { get; set; }
		}
		public partial interface IGallery
		{
			int K { get; set; }
			int EventK { get; set; }
			int ArticleK { get; set; }
			string Name { get; set; }
			int MainPhotoK { get; set; }
			int OwnerUsrK { get; set; }
			int TotalPhotos { get; set; }
			int LivePhotos { get; set; }
			DateTime CreateDateTime { get; set; }
			DateTime LastLiveDateTime { get; set; }
			string AdminNote { get; set; }
			bool IsMobile { get; set; }
			string PicState { get; set; }
			int PicPhotoK { get; set; }
			int PicMiscK { get; set; }
			string UrlFragment { get; set; }
			int ModeratorUsrK { get; set; }
			int CurrentPackageCount { get; set; }
			DateTime LastPackageDateTime { get; set; }
			int LastPackageIndex { get; set; }
			bool UploadInProgress { get; set; }
			int UploadFails { get; set; }
			bool? WatchUploads { get; set; }
			bool? RunFinishedUploadingTask { get; set; }
		}
		
 		public abstract partial class GalleryUsr : IGalleryUsr
		{
			public abstract int GalleryK { get; set; }
			public abstract int UsrK { get; set; }
			public abstract DateTime ViewDateTime { get; set; }
			public abstract DateTime ViewDateTimeLatest { get; set; }
			public abstract int ViewPhotos { get; set; }
			public abstract int ViewPhotosLatest { get; set; }
			public abstract bool Favourite { get; set; }
		}
		public partial interface IGalleryUsr
		{
			int GalleryK { get; set; }
			int UsrK { get; set; }
			DateTime ViewDateTime { get; set; }
			DateTime ViewDateTimeLatest { get; set; }
			int ViewPhotos { get; set; }
			int ViewPhotosLatest { get; set; }
			bool Favourite { get; set; }
		}
		
 		public abstract partial class Global : IGlobal
		{
			public abstract int K { get; set; }
			public abstract string Name { get; set; }
			public abstract string Description { get; set; }
			public abstract string ValueString { get; set; }
			public abstract int ValueInt { get; set; }
			public abstract double ValueDouble { get; set; }
			public abstract DateTime ValueDateTime { get; set; }
			public abstract string ValueText { get; set; }
		}
		public partial interface IGlobal
		{
			int K { get; set; }
			string Name { get; set; }
			string Description { get; set; }
			string ValueString { get; set; }
			int ValueInt { get; set; }
			double ValueDouble { get; set; }
			DateTime ValueDateTime { get; set; }
			string ValueText { get; set; }
		}
		
 		public abstract partial class Group : IGroup
		{
			public abstract int K { get; set; }
			public abstract string Name { get; set; }
			public abstract string Description { get; set; }
			public abstract string LongDescriptionHtml { get; set; }
			public abstract bool LongDescriptionPlain { get; set; }
			public abstract string PostingRules { get; set; }
			public abstract DateTime DateTimeCreated { get; set; }
			public abstract int TotalMembers { get; set; }
			public abstract int TotalModerators { get; set; }
			public abstract int TotalOwners { get; set; }
			public abstract int TotalComments { get; set; }
			public abstract DateTime? LastPost { get; set; }
			public abstract DateTime? AverageCommentDateTime { get; set; }
			public abstract bool PrivateGroupPage { get; set; }
			public abstract bool PrivateChat { get; set; }
			public abstract bool PrivateMemberList { get; set; }
			public abstract Model.Entities.Group.RestrictionEnum Restriction { get; set; }
			public abstract Model.Entities.Group.CustomRestrictionTypes CustomRestrictionType { get; set; }
			public abstract int ThemeK { get; set; }
			public abstract int CountryK { get; set; }
			public abstract int PlaceK { get; set; }
			public abstract int MusicTypeK { get; set; }
			public abstract int BrandK { get; set; }
			public abstract string UrlName { get; set; }
			public abstract Guid Pic { get; set; }
			public abstract string PicState { get; set; }
			public abstract int PicPhotoK { get; set; }
			public abstract int PicMiscK { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract bool EmailOnAllThreads { get; set; }
			public abstract int FavouriteCount { get; set; }
			public abstract int WatchCount { get; set; }
		}
		public partial interface IGroup
		{
			int K { get; set; }
			string Name { get; set; }
			string Description { get; set; }
			string LongDescriptionHtml { get; set; }
			bool LongDescriptionPlain { get; set; }
			string PostingRules { get; set; }
			DateTime DateTimeCreated { get; set; }
			int TotalMembers { get; set; }
			int TotalModerators { get; set; }
			int TotalOwners { get; set; }
			int TotalComments { get; set; }
			DateTime? LastPost { get; set; }
			DateTime? AverageCommentDateTime { get; set; }
			bool PrivateGroupPage { get; set; }
			bool PrivateChat { get; set; }
			bool PrivateMemberList { get; set; }
			Model.Entities.Group.RestrictionEnum Restriction { get; set; }
			Model.Entities.Group.CustomRestrictionTypes CustomRestrictionType { get; set; }
			int ThemeK { get; set; }
			int CountryK { get; set; }
			int PlaceK { get; set; }
			int MusicTypeK { get; set; }
			int BrandK { get; set; }
			string UrlName { get; set; }
			Guid Pic { get; set; }
			string PicState { get; set; }
			int PicPhotoK { get; set; }
			int PicMiscK { get; set; }
			Guid DuplicateGuid { get; set; }
			bool EmailOnAllThreads { get; set; }
			int FavouriteCount { get; set; }
			int WatchCount { get; set; }
		}
		
 		public abstract partial class GroupEvent : IGroupEvent
		{
			public abstract int GroupK { get; set; }
			public abstract int EventK { get; set; }
		}
		public partial interface IGroupEvent
		{
			int GroupK { get; set; }
			int EventK { get; set; }
		}
		
 		public abstract partial class GroupPhoto : IGroupPhoto
		{
			public abstract int K { get; set; }
			public abstract int GroupK { get; set; }
			public abstract int PhotoK { get; set; }
			public abstract string Caption { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract int AddedByUsrK { get; set; }
			public abstract bool ShowOnFrontPage { get; set; }
		}
		public partial interface IGroupPhoto
		{
			int K { get; set; }
			int GroupK { get; set; }
			int PhotoK { get; set; }
			string Caption { get; set; }
			DateTime DateTime { get; set; }
			int AddedByUsrK { get; set; }
			bool ShowOnFrontPage { get; set; }
		}
		
 		public abstract partial class GroupUsr : IGroupUsr
		{
			public abstract int UsrK { get; set; }
			public abstract int GroupK { get; set; }
			public abstract Model.Entities.GroupUsr.StatusEnum Status { get; set; }
			public abstract DateTime StatusChangeDateTime { get; set; }
			public abstract int StatusChangeUsrK { get; set; }
			public abstract bool Owner { get; set; }
			public abstract bool Moderator { get; set; }
			public abstract bool NewsAdmin { get; set; }
			public abstract bool MemberAdmin { get; set; }
			public abstract bool Favourite { get; set; }
			public abstract string InviteMessage { get; set; }
			public abstract int InviteUsrK { get; set; }
			public abstract bool MemberAdminNewUserEmails { get; set; }
		}
		public partial interface IGroupUsr
		{
			int UsrK { get; set; }
			int GroupK { get; set; }
			Model.Entities.GroupUsr.StatusEnum Status { get; set; }
			DateTime StatusChangeDateTime { get; set; }
			int StatusChangeUsrK { get; set; }
			bool Owner { get; set; }
			bool Moderator { get; set; }
			bool NewsAdmin { get; set; }
			bool MemberAdmin { get; set; }
			bool Favourite { get; set; }
			string InviteMessage { get; set; }
			int InviteUsrK { get; set; }
			bool MemberAdminNewUserEmails { get; set; }
		}
		
 		public abstract partial class GuestlistCredit : IGuestlistCredit
		{
			public abstract int K { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract DateTime DateTimeCreated { get; set; }
			public abstract int Credits { get; set; }
			public abstract decimal TotalPrice { get; set; }
			public abstract bool Done { get; set; }
			public abstract DateTime DateTimeDone { get; set; }
			public abstract DateTime BuyableLockDateTime { get; set; }
		}
		public partial interface IGuestlistCredit
		{
			int K { get; set; }
			int PromoterK { get; set; }
			DateTime DateTimeCreated { get; set; }
			int Credits { get; set; }
			decimal TotalPrice { get; set; }
			bool Done { get; set; }
			DateTime DateTimeDone { get; set; }
			DateTime BuyableLockDateTime { get; set; }
		}
		
 		public abstract partial class Hit : IHit
		{
			public abstract int K { get; set; }
			public abstract int ServerId { get; set; }
			public abstract DateTime StartTime { get; set; }
			public abstract bool HasEnded { get; set; }
			public abstract DateTime EndTime { get; set; }
			public abstract string GetData { get; set; }
			public abstract string PostData { get; set; }
			public abstract int UsrK { get; set; }
			public abstract string CookieData { get; set; }
		}
		public partial interface IHit
		{
			int K { get; set; }
			int ServerId { get; set; }
			DateTime StartTime { get; set; }
			bool HasEnded { get; set; }
			DateTime EndTime { get; set; }
			string GetData { get; set; }
			string PostData { get; set; }
			int UsrK { get; set; }
			string CookieData { get; set; }
		}
		
 		public abstract partial class HitView : IHitView
		{
			public abstract int K { get; set; }
			public abstract int? ServerId { get; set; }
			public abstract DateTime? StartTime { get; set; }
			public abstract bool? HasEnded { get; set; }
			public abstract DateTime? EndTime { get; set; }
			public abstract string GetData { get; set; }
			public abstract string PostData { get; set; }
			public abstract int? UsrK { get; set; }
			public abstract string CookieData { get; set; }
			public abstract int? Duration { get; set; }
		}
		public partial interface IHitView
		{
			int K { get; set; }
			int? ServerId { get; set; }
			DateTime? StartTime { get; set; }
			bool? HasEnded { get; set; }
			DateTime? EndTime { get; set; }
			string GetData { get; set; }
			string PostData { get; set; }
			int? UsrK { get; set; }
			string CookieData { get; set; }
			int? Duration { get; set; }
		}
		
 		public abstract partial class IncomingSm : IIncomingSm
		{
			public abstract int K { get; set; }
			public abstract string Message { get; set; }
			public abstract DateTime? DateTime { get; set; }
			public abstract int? MobileK { get; set; }
			public abstract int? ServiceType { get; set; }
			public abstract string PostData { get; set; }
			public abstract string MessageID { get; set; }
		}
		public partial interface IIncomingSm
		{
			int K { get; set; }
			string Message { get; set; }
			DateTime? DateTime { get; set; }
			int? MobileK { get; set; }
			int? ServiceType { get; set; }
			string PostData { get; set; }
			string MessageID { get; set; }
		}
		
 		public abstract partial class InsertionOrder : IInsertionOrder
		{
			public abstract int K { get; set; }
			public abstract Model.Entities.InsertionOrder.InsertionOrderStatus Status { get; set; }
			public abstract int CampaignCredits { get; set; }
			public abstract DateTime NextInvoiceDue { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract int UsrK { get; set; }
			public abstract string UsrNameOverride { get; set; }
			public abstract DateTime DateTimeCreated { get; set; }
			public abstract string ClientRef { get; set; }
			public abstract DateTime CampaignStartDate { get; set; }
			public abstract DateTime CampaignEndDate { get; set; }
			public abstract int TrafficUsrK { get; set; }
			public abstract string Notes { get; set; }
			public abstract int ActionUsrK { get; set; }
			public abstract string PaymentTerms { get; set; }
			public abstract string InvoicePeriod { get; set; }
			public abstract string CampaignName { get; set; }
			public abstract double AgencyDiscount { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract bool CampaignCreditsOverriden { get; set; }
		}
		public partial interface IInsertionOrder
		{
			int K { get; set; }
			Model.Entities.InsertionOrder.InsertionOrderStatus Status { get; set; }
			int CampaignCredits { get; set; }
			DateTime NextInvoiceDue { get; set; }
			int PromoterK { get; set; }
			int UsrK { get; set; }
			string UsrNameOverride { get; set; }
			DateTime DateTimeCreated { get; set; }
			string ClientRef { get; set; }
			DateTime CampaignStartDate { get; set; }
			DateTime CampaignEndDate { get; set; }
			int TrafficUsrK { get; set; }
			string Notes { get; set; }
			int ActionUsrK { get; set; }
			string PaymentTerms { get; set; }
			string InvoicePeriod { get; set; }
			string CampaignName { get; set; }
			double AgencyDiscount { get; set; }
			Guid DuplicateGuid { get; set; }
			bool CampaignCreditsOverriden { get; set; }
		}
		
 		public abstract partial class InsertionOrderItem : IInsertionOrderItem
		{
			public abstract int K { get; set; }
			public abstract int InsertionOrderK { get; set; }
			public abstract string Description { get; set; }
			public abstract int BannerPosition { get; set; }
			public abstract int ImpressionQuantity { get; set; }
			public abstract decimal PriceBeforeDiscount { get; set; }
			public abstract double Discount { get; set; }
			public abstract decimal PriceBeforeAgencyDiscount { get; set; }
			public abstract double AgencyDiscount { get; set; }
			public abstract decimal Price { get; set; }
			public abstract decimal Cpm { get; set; }
		}
		public partial interface IInsertionOrderItem
		{
			int K { get; set; }
			int InsertionOrderK { get; set; }
			string Description { get; set; }
			int BannerPosition { get; set; }
			int ImpressionQuantity { get; set; }
			decimal PriceBeforeDiscount { get; set; }
			double Discount { get; set; }
			decimal PriceBeforeAgencyDiscount { get; set; }
			double AgencyDiscount { get; set; }
			decimal Price { get; set; }
			decimal Cpm { get; set; }
		}
		
 		public abstract partial class Invoice : IInvoice
		{
			public abstract int K { get; set; }
			public abstract Model.Entities.Invoice.Types Type { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract int ActionUsrK { get; set; }
			public abstract string Name { get; set; }
			public abstract string Address { get; set; }
			public abstract string Postcode { get; set; }
			public abstract Model.Entities.Invoice.PaymentTypes PaymentType { get; set; }
			public abstract bool Paid { get; set; }
			public abstract DateTime CreatedDateTime { get; set; }
			public abstract DateTime DueDateTime { get; set; }
			public abstract DateTime PaidDateTime { get; set; }
			public abstract decimal Price { get; set; }
			public abstract decimal Vat { get; set; }
			public abstract decimal Total { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract string Notes { get; set; }
			public abstract Model.Entities.Invoice.VATCodes VatCode { get; set; }
			public abstract int SalesUsrK { get; set; }
			public abstract decimal SalesUsrAmount { get; set; }
			public abstract bool IsImmediateCreditCardPayment { get; set; }
			public abstract DateTime TaxDateTime { get; set; }
			public abstract string PurchaseOrderNumber { get; set; }
			public abstract Model.Entities.Invoice.BuyerTypes BuyerType { get; set; }
			public abstract decimal PriceBeforeDiscount { get; set; }
			public abstract double Discount { get; set; }
			public abstract decimal PriceBeforeAgencyDiscount { get; set; }
			public abstract double AgencyDiscount { get; set; }
			public abstract int InsertionOrderK { get; set; }
		}
		public partial interface IInvoice
		{
			int K { get; set; }
			Model.Entities.Invoice.Types Type { get; set; }
			int UsrK { get; set; }
			int PromoterK { get; set; }
			int ActionUsrK { get; set; }
			string Name { get; set; }
			string Address { get; set; }
			string Postcode { get; set; }
			Model.Entities.Invoice.PaymentTypes PaymentType { get; set; }
			bool Paid { get; set; }
			DateTime CreatedDateTime { get; set; }
			DateTime DueDateTime { get; set; }
			DateTime PaidDateTime { get; set; }
			decimal Price { get; set; }
			decimal Vat { get; set; }
			decimal Total { get; set; }
			Guid DuplicateGuid { get; set; }
			string Notes { get; set; }
			Model.Entities.Invoice.VATCodes VatCode { get; set; }
			int SalesUsrK { get; set; }
			decimal SalesUsrAmount { get; set; }
			bool IsImmediateCreditCardPayment { get; set; }
			DateTime TaxDateTime { get; set; }
			string PurchaseOrderNumber { get; set; }
			Model.Entities.Invoice.BuyerTypes BuyerType { get; set; }
			decimal PriceBeforeDiscount { get; set; }
			double Discount { get; set; }
			decimal PriceBeforeAgencyDiscount { get; set; }
			double AgencyDiscount { get; set; }
			int InsertionOrderK { get; set; }
		}
		
 		public abstract partial class InvoiceCredit : IInvoiceCredit
		{
			public abstract int InvoiceK { get; set; }
			public abstract int CreditInvoiceK { get; set; }
			public abstract decimal Amount { get; set; }
		}
		public partial interface IInvoiceCredit
		{
			int InvoiceK { get; set; }
			int CreditInvoiceK { get; set; }
			decimal Amount { get; set; }
		}
		
 		public abstract partial class InvoiceItem : IInvoiceItem
		{
			public abstract int K { get; set; }
			public abstract int InvoiceK { get; set; }
			public abstract Model.Entities.InvoiceItem.Types Type { get; set; }
			public abstract int KeyData { get; set; }
			public abstract string CustomData { get; set; }
			public abstract bool ItemProcessed { get; set; }
			public abstract string Description { get; set; }
			public abstract decimal Price { get; set; }
			public abstract decimal Vat { get; set; }
			public abstract decimal Total { get; set; }
			public abstract DateTime RevenueStartDate { get; set; }
			public abstract DateTime RevenueEndDate { get; set; }
			public abstract Model.Entities.InvoiceItem.VATCodes VatCode { get; set; }
			public abstract Model.Entities.ObjectType BuyableObjectType { get; set; }
			public abstract int BuyableObjectK { get; set; }
			public abstract decimal PriceBeforeDiscount { get; set; }
			public abstract double Discount { get; set; }
			public abstract decimal PriceBeforeAgencyDiscount { get; set; }
			public abstract double AgencyDiscount { get; set; }
		}
		public partial interface IInvoiceItem
		{
			int K { get; set; }
			int InvoiceK { get; set; }
			Model.Entities.InvoiceItem.Types Type { get; set; }
			int KeyData { get; set; }
			string CustomData { get; set; }
			bool ItemProcessed { get; set; }
			string Description { get; set; }
			decimal Price { get; set; }
			decimal Vat { get; set; }
			decimal Total { get; set; }
			DateTime RevenueStartDate { get; set; }
			DateTime RevenueEndDate { get; set; }
			Model.Entities.InvoiceItem.VATCodes VatCode { get; set; }
			Model.Entities.ObjectType BuyableObjectType { get; set; }
			int BuyableObjectK { get; set; }
			decimal PriceBeforeDiscount { get; set; }
			double Discount { get; set; }
			decimal PriceBeforeAgencyDiscount { get; set; }
			double AgencyDiscount { get; set; }
		}
		
 		public abstract partial class InvoiceTransfer : IInvoiceTransfer
		{
			public abstract int InvoiceK { get; set; }
			public abstract int TransferK { get; set; }
			public abstract decimal Amount { get; set; }
		}
		public partial interface IInvoiceTransfer
		{
			int InvoiceK { get; set; }
			int TransferK { get; set; }
			decimal Amount { get; set; }
		}
		
 		public abstract partial class IpCountry : IIpCountry
		{
			public abstract long IpFrom { get; set; }
			public abstract long IpTo { get; set; }
			public abstract string Code2Letter { get; set; }
			public abstract string Code3Letter { get; set; }
			public abstract string Name { get; set; }
			public abstract int CountryK { get; set; }
		}
		public partial interface IIpCountry
		{
			long IpFrom { get; set; }
			long IpTo { get; set; }
			string Code2Letter { get; set; }
			string Code3Letter { get; set; }
			string Name { get; set; }
			int CountryK { get; set; }
		}
		
 		public abstract partial class Log : ILog
		{
			public abstract Model.Entities.Log.Items ItemType { get; set; }
			public abstract DateTime Date { get; set; }
			public abstract int Count { get; set; }
		}
		public partial interface ILog
		{
			Model.Entities.Log.Items ItemType { get; set; }
			DateTime Date { get; set; }
			int Count { get; set; }
		}
		
 		public abstract partial class LogPageTime : ILogPageTime
		{
			public abstract int K { get; set; }
			public abstract DateTime StartDateTime { get; set; }
			public abstract DateTime EndDateTime { get; set; }
			public abstract string CurrentFilter { get; set; }
			public abstract string MasterPath { get; set; }
			public abstract string PagePath { get; set; }
			public abstract int ObjectFilterK { get; set; }
			public abstract Model.Entities.ObjectType ObjectFilterType { get; set; }
			public abstract string MachineName { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int Selects { get; set; }
			public abstract int Updates { get; set; }
			public abstract int Inserts { get; set; }
			public abstract int Deletes { get; set; }
			public abstract bool IsGet { get; set; }
			public abstract string Url { get; set; }
			public abstract string PostData { get; set; }
			public abstract Guid DsiGuid { get; set; }
			public abstract string IpAddress { get; set; }
			public abstract bool? IsCrawler { get; set; }
			public abstract bool? IsAjaxRequest { get; set; }
			public abstract bool? IsRendered { get; set; }
		}
		public partial interface ILogPageTime
		{
			int K { get; set; }
			DateTime StartDateTime { get; set; }
			DateTime EndDateTime { get; set; }
			string CurrentFilter { get; set; }
			string MasterPath { get; set; }
			string PagePath { get; set; }
			int ObjectFilterK { get; set; }
			Model.Entities.ObjectType ObjectFilterType { get; set; }
			string MachineName { get; set; }
			int UsrK { get; set; }
			int Selects { get; set; }
			int Updates { get; set; }
			int Inserts { get; set; }
			int Deletes { get; set; }
			bool IsGet { get; set; }
			string Url { get; set; }
			string PostData { get; set; }
			Guid DsiGuid { get; set; }
			string IpAddress { get; set; }
			bool? IsCrawler { get; set; }
			bool? IsAjaxRequest { get; set; }
			bool? IsRendered { get; set; }
		}
		
 		public abstract partial class Lol : ILol
		{
			public abstract int K { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int CommentK { get; set; }
			public abstract int CommentUsrK { get; set; }
			public abstract DateTime DateTime { get; set; }
		}
		public partial interface ILol
		{
			int K { get; set; }
			int UsrK { get; set; }
			int CommentK { get; set; }
			int CommentUsrK { get; set; }
			DateTime DateTime { get; set; }
		}
		
 		public abstract partial class Misc : IMisc
		{
			public abstract int K { get; set; }
			public abstract Guid Guid { get; set; }
			public abstract string Extention { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract int Size { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract DateTime DateTimeExpires { get; set; }
			public abstract string Folder { get; set; }
			public abstract string Name { get; set; }
			public abstract string Note { get; set; }
			public abstract string Xml { get; set; }
			public abstract bool NeedsAuth { get; set; }
			public abstract bool BannerLinkTag { get; set; }
			public abstract bool BannerTargetTag { get; set; }
			public abstract int Width { get; set; }
			public abstract int Height { get; set; }
			public abstract string RequiredFlashVersion { get; set; }
			public abstract bool BannerBroken { get; set; }
			public abstract string BannerBrokenReason { get; set; }
		}
		public partial interface IMisc
		{
			int K { get; set; }
			Guid Guid { get; set; }
			string Extention { get; set; }
			int UsrK { get; set; }
			int PromoterK { get; set; }
			int Size { get; set; }
			DateTime DateTime { get; set; }
			DateTime DateTimeExpires { get; set; }
			string Folder { get; set; }
			string Name { get; set; }
			string Note { get; set; }
			string Xml { get; set; }
			bool NeedsAuth { get; set; }
			bool BannerLinkTag { get; set; }
			bool BannerTargetTag { get; set; }
			int Width { get; set; }
			int Height { get; set; }
			string RequiredFlashVersion { get; set; }
			bool BannerBroken { get; set; }
			string BannerBrokenReason { get; set; }
		}
		
 		public abstract partial class MixmagEntry : IMixmagEntry
		{
			public abstract int K { get; set; }
			public abstract int MixmagCompK { get; set; }
			public abstract long? FacebookUid { get; set; }
			public abstract DateTime? DateTime { get; set; }
			public abstract string ImageUrl { get; set; }
			public abstract string Email { get; set; }
			public abstract string FirstName { get; set; }
			public abstract string LastName { get; set; }
			public abstract bool SendDailyEmails { get; set; }
		}
		public partial interface IMixmagEntry
		{
			int K { get; set; }
			int MixmagCompK { get; set; }
			long? FacebookUid { get; set; }
			DateTime? DateTime { get; set; }
			string ImageUrl { get; set; }
			string Email { get; set; }
			string FirstName { get; set; }
			string LastName { get; set; }
			bool SendDailyEmails { get; set; }
		}
		
 		public abstract partial class MixmagGreatestDj : IMixmagGreatestDj
		{
			public abstract int K { get; set; }
			public abstract string UrlName { get; set; }
			public abstract string Name { get; set; }
			public abstract string ImageUrl { get; set; }
			public abstract string YoutubeId { get; set; }
			public abstract string Description { get; set; }
			public abstract string LongDescription { get; set; }
			public abstract string ShortDescription { get; set; }
			public abstract string LargeImageUrl { get; set; }
			public abstract string InterviewImageUrl { get; set; }
			public abstract string TwitterName { get; set; }
			public abstract string YoutubeId2 { get; set; }
			public abstract int? TotalVotes { get; set; }
			public abstract bool? IsPublicNominated { get; set; }
			public abstract string PluralWord { get; set; }
			public abstract string ShortName { get; set; }
			public abstract bool StealthMode { get; set; }
		}
		public partial interface IMixmagGreatestDj
		{
			int K { get; set; }
			string UrlName { get; set; }
			string Name { get; set; }
			string ImageUrl { get; set; }
			string YoutubeId { get; set; }
			string Description { get; set; }
			string LongDescription { get; set; }
			string ShortDescription { get; set; }
			string LargeImageUrl { get; set; }
			string InterviewImageUrl { get; set; }
			string TwitterName { get; set; }
			string YoutubeId2 { get; set; }
			int? TotalVotes { get; set; }
			bool? IsPublicNominated { get; set; }
			string PluralWord { get; set; }
			string ShortName { get; set; }
			bool StealthMode { get; set; }
		}
		
 		public abstract partial class MixmagGreatestVote : IMixmagGreatestVote
		{
			public abstract long FacebookUid { get; set; }
			public abstract int MixmagGreatestDjK { get; set; }
			public abstract DateTime? DateTime { get; set; }
			public abstract bool? DidWallPost { get; set; }
			public abstract string FacebookEmail { get; set; }
			public abstract bool? WallPostPermission { get; set; }
			public abstract bool? EmailPermission { get; set; }
			public abstract bool? FacebookSource { get; set; }
		}
		public partial interface IMixmagGreatestVote
		{
			long FacebookUid { get; set; }
			int MixmagGreatestDjK { get; set; }
			DateTime? DateTime { get; set; }
			bool? DidWallPost { get; set; }
			string FacebookEmail { get; set; }
			bool? WallPostPermission { get; set; }
			bool? EmailPermission { get; set; }
			bool? FacebookSource { get; set; }
		}
		
 		public abstract partial class MixmagIssue : IMixmagIssue
		{
			public abstract int K { get; set; }
			public abstract string CerosUrl { get; set; }
			public abstract bool? Ready { get; set; }
			public abstract DateTime? DateTimeSend { get; set; }
			public abstract DateTime? IssueCoverDate { get; set; }
			public abstract int? TotalSent { get; set; }
			public abstract int? TotalRead { get; set; }
			public abstract bool? SendingNow { get; set; }
			public abstract bool? SendingFinished { get; set; }
			public abstract DateTime? LastSendDateTime { get; set; }
			public abstract string Summary { get; set; }
			public abstract string CoverImageUrl { get; set; }
			public abstract string ContentsData { get; set; }
			public abstract string IssueNote { get; set; }
			public abstract int? IssueCoverId { get; set; }
		}
		public partial interface IMixmagIssue
		{
			int K { get; set; }
			string CerosUrl { get; set; }
			bool? Ready { get; set; }
			DateTime? DateTimeSend { get; set; }
			DateTime? IssueCoverDate { get; set; }
			int? TotalSent { get; set; }
			int? TotalRead { get; set; }
			bool? SendingNow { get; set; }
			bool? SendingFinished { get; set; }
			DateTime? LastSendDateTime { get; set; }
			string Summary { get; set; }
			string CoverImageUrl { get; set; }
			string ContentsData { get; set; }
			string IssueNote { get; set; }
			int? IssueCoverId { get; set; }
		}
		
 		public abstract partial class MixmagRead : IMixmagRead
		{
			public abstract int MixmagSubscriberK { get; set; }
			public abstract int MixmagIssueK { get; set; }
			public abstract DateTime? DateTimeRead { get; set; }
			public abstract bool? StoryPublished { get; set; }
			public abstract DateTime? DateTimeLastRead { get; set; }
			public abstract int? TotalReads { get; set; }
			public abstract DateTime? DateTimeLastStoryPublished { get; set; }
		}
		public partial interface IMixmagRead
		{
			int MixmagSubscriberK { get; set; }
			int MixmagIssueK { get; set; }
			DateTime? DateTimeRead { get; set; }
			bool? StoryPublished { get; set; }
			DateTime? DateTimeLastRead { get; set; }
			int? TotalReads { get; set; }
			DateTime? DateTimeLastStoryPublished { get; set; }
		}
		
 		public abstract partial class MixmagSubscription : IMixmagSubscription
		{
			public abstract int K { get; set; }
			public abstract long FacebookUID { get; set; }
			public abstract bool? FacebookPermissionEmail { get; set; }
			public abstract bool? FacebookPermissionPublish { get; set; }
			public abstract DateTime? DateTimeCreated { get; set; }
			public abstract bool? SendMixmag { get; set; }
			public abstract bool? PublishStoryOnRead { get; set; }
			public abstract int? TotalSent { get; set; }
			public abstract int? TotalRead { get; set; }
			public abstract string FirstName { get; set; }
			public abstract string LastName { get; set; }
			public abstract string AddressFirstLine { get; set; }
			public abstract string AddressPostCode { get; set; }
			public abstract int? AddressCountryK { get; set; }
			public abstract bool? IsAddressComplete { get; set; }
			public abstract bool? IsEmailVerified { get; set; }
			public abstract string Email { get; set; }
			public abstract bool? IsEmailComplete { get; set; }
			public abstract string EmailVerificationSecret { get; set; }
			public abstract bool? IsEmailBroken { get; set; }
			public abstract DateTime? EmailBrokenDateTime { get; set; }
			public abstract bool? IsEmailFromFacebook { get; set; }
		}
		public partial interface IMixmagSubscription
		{
			int K { get; set; }
			long FacebookUID { get; set; }
			bool? FacebookPermissionEmail { get; set; }
			bool? FacebookPermissionPublish { get; set; }
			DateTime? DateTimeCreated { get; set; }
			bool? SendMixmag { get; set; }
			bool? PublishStoryOnRead { get; set; }
			int? TotalSent { get; set; }
			int? TotalRead { get; set; }
			string FirstName { get; set; }
			string LastName { get; set; }
			string AddressFirstLine { get; set; }
			string AddressPostCode { get; set; }
			int? AddressCountryK { get; set; }
			bool? IsAddressComplete { get; set; }
			bool? IsEmailVerified { get; set; }
			string Email { get; set; }
			bool? IsEmailComplete { get; set; }
			string EmailVerificationSecret { get; set; }
			bool? IsEmailBroken { get; set; }
			DateTime? EmailBrokenDateTime { get; set; }
			bool? IsEmailFromFacebook { get; set; }
		}
		
 		public abstract partial class MixmagVote : IMixmagVote
		{
			public abstract int K { get; set; }
			public abstract long FacebookUID { get; set; }
			public abstract int? MixmagEntryK { get; set; }
			public abstract DateTime? DateTime { get; set; }
			public abstract string TextField1 { get; set; }
		}
		public partial interface IMixmagVote
		{
			int K { get; set; }
			long FacebookUID { get; set; }
			int? MixmagEntryK { get; set; }
			DateTime? DateTime { get; set; }
			string TextField1 { get; set; }
		}
		
 		public abstract partial class Mobile : IMobile
		{
			public abstract int K { get; set; }
			public abstract int UsrK { get; set; }
			public abstract string Number { get; set; }
			public abstract int Network { get; set; }
			public abstract int TotalIncoming { get; set; }
			public abstract int TotalOutgoing { get; set; }
			public abstract DateTime DateTimeCreated { get; set; }
			public abstract DateTime DateTimeLastIncoming { get; set; }
		}
		public partial interface IMobile
		{
			int K { get; set; }
			int UsrK { get; set; }
			string Number { get; set; }
			int Network { get; set; }
			int TotalIncoming { get; set; }
			int TotalOutgoing { get; set; }
			DateTime DateTimeCreated { get; set; }
			DateTime DateTimeLastIncoming { get; set; }
		}
		
 		public abstract partial class MusicType : IMusicType
		{
			public abstract int K { get; set; }
			public abstract string Name { get; set; }
			public abstract int ParentK { get; set; }
			public abstract string GenericName { get; set; }
			public abstract double Order { get; set; }
			public abstract string SmsCode { get; set; }
			public abstract string SmsName { get; set; }
			public abstract string Description { get; set; }
		}
		public partial interface IMusicType
		{
			int K { get; set; }
			string Name { get; set; }
			int ParentK { get; set; }
			string GenericName { get; set; }
			double Order { get; set; }
			string SmsCode { get; set; }
			string SmsName { get; set; }
			string Description { get; set; }
		}
		
 		public abstract partial class NovemberVisitor : INovemberVisitor
		{
			public abstract int? Pages { get; set; }
			public abstract string Guid { get; set; }
		}
		public partial interface INovemberVisitor
		{
			int? Pages { get; set; }
			string Guid { get; set; }
		}
		
 		public abstract partial class OutgoingSms : IOutgoingSms
		{
			public abstract int K { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract Model.Entities.OutgoingSms.Types Type { get; set; }
			public abstract int? IncomingSmsK { get; set; }
			public abstract string PostString { get; set; }
			public abstract string Message { get; set; }
			public abstract int ErrorCode { get; set; }
			public abstract string ErrorText { get; set; }
			public abstract string SubmissionReference { get; set; }
			public abstract Model.Entities.OutgoingSms.ChargeTypes ChargeType { get; set; }
			public abstract bool Sent { get; set; }
			public abstract int MobileK { get; set; }
			public abstract Model.Entities.IncomingSms.ServiceTypes ServiceType { get; set; }
			public abstract bool Delivered { get; set; }
		}
		public partial interface IOutgoingSms
		{
			int K { get; set; }
			DateTime DateTime { get; set; }
			Model.Entities.OutgoingSms.Types Type { get; set; }
			int? IncomingSmsK { get; set; }
			string PostString { get; set; }
			string Message { get; set; }
			int ErrorCode { get; set; }
			string ErrorText { get; set; }
			string SubmissionReference { get; set; }
			Model.Entities.OutgoingSms.ChargeTypes ChargeType { get; set; }
			bool Sent { get; set; }
			int MobileK { get; set; }
			Model.Entities.IncomingSms.ServiceTypes ServiceType { get; set; }
			bool Delivered { get; set; }
		}
		
 		public abstract partial class PageTime : IPageTime
		{
			public abstract int K { get; set; }
			public abstract int CobK { get; set; }
			public abstract DateTime Date { get; set; }
			public abstract int Impressions { get; set; }
			public abstract int TotalTime { get; set; }
			public abstract int MaxTime { get; set; }
			public abstract int MinTime { get; set; }
			public abstract string MaxUrl { get; set; }
			public abstract string MinUrl { get; set; }
			public abstract string CustPage { get; set; }
			public abstract Model.Entities.PageTime.LogItems LogItem { get; set; }
		}
		public partial interface IPageTime
		{
			int K { get; set; }
			int CobK { get; set; }
			DateTime Date { get; set; }
			int Impressions { get; set; }
			int TotalTime { get; set; }
			int MaxTime { get; set; }
			int MinTime { get; set; }
			string MaxUrl { get; set; }
			string MinUrl { get; set; }
			string CustPage { get; set; }
			Model.Entities.PageTime.LogItems LogItem { get; set; }
		}
		
 		public abstract partial class Para : IPara
		{
			public abstract int K { get; set; }
			public abstract int ArticleK { get; set; }
			public abstract int Page { get; set; }
			public abstract double Order { get; set; }
			public abstract Model.Entities.Para.TypeEnum Type { get; set; }
			public abstract int PhotoK { get; set; }
			public abstract string Text { get; set; }
			public abstract int ThreadK { get; set; }
			public abstract Model.Entities.Para.PhotoAlignEnum PhotoAlign { get; set; }
			public abstract Guid Pic { get; set; }
			public abstract Model.Entities.Para.PhotoTypes PhotoType { get; set; }
			public abstract string Caption { get; set; }
			public abstract int PicWidth { get; set; }
			public abstract int PicHeight { get; set; }
			public abstract string PicState { get; set; }
			public abstract int PicPhotoK { get; set; }
			public abstract int PicMiscK { get; set; }
		}
		public partial interface IPara
		{
			int K { get; set; }
			int ArticleK { get; set; }
			int Page { get; set; }
			double Order { get; set; }
			Model.Entities.Para.TypeEnum Type { get; set; }
			int PhotoK { get; set; }
			string Text { get; set; }
			int ThreadK { get; set; }
			Model.Entities.Para.PhotoAlignEnum PhotoAlign { get; set; }
			Guid Pic { get; set; }
			Model.Entities.Para.PhotoTypes PhotoType { get; set; }
			string Caption { get; set; }
			int PicWidth { get; set; }
			int PicHeight { get; set; }
			string PicState { get; set; }
			int PicPhotoK { get; set; }
			int PicMiscK { get; set; }
		}
		
 		public abstract partial class Phone : IPhone
		{
			public abstract int K { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int Extention { get; set; }
			public abstract string Mac { get; set; }
			public abstract string IpAddress { get; set; }
			public abstract string LocalIpAddress { get; set; }
			public abstract string LocalGateway { get; set; }
			public abstract string LocalDns { get; set; }
			public abstract string NatPort { get; set; }
			public abstract string TestColumn { get; set; }
			public abstract string TestColumn1 { get; set; }
			public abstract string TestColumn2 { get; set; }
		}
		public partial interface IPhone
		{
			int K { get; set; }
			int UsrK { get; set; }
			int Extention { get; set; }
			string Mac { get; set; }
			string IpAddress { get; set; }
			string LocalIpAddress { get; set; }
			string LocalGateway { get; set; }
			string LocalDns { get; set; }
			string NatPort { get; set; }
			string TestColumn { get; set; }
			string TestColumn1 { get; set; }
			string TestColumn2 { get; set; }
		}
		
 		public abstract partial class Photo : IPhoto
		{
			public abstract int K { get; set; }
			public abstract int GalleryK { get; set; }
			public abstract int EventK { get; set; }
			public abstract int ArticleK { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int MobileK { get; set; }
			public abstract double Order { get; set; }
			public abstract int? ThreadK { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract int Views { get; set; }
			public abstract double AverageCoolRating { get; set; }
			public abstract double AverageSexyRating { get; set; }
			public abstract int TotalCoolRatings { get; set; }
			public abstract int TotalSexyRatings { get; set; }
			public abstract double WeightedCoolRating { get; set; }
			public abstract double WeightedSexyRating { get; set; }
			public abstract Guid Master { get; set; }
			public abstract Guid Original { get; set; }
			public abstract Guid Icon { get; set; }
			public abstract Guid Thumb { get; set; }
			public abstract Guid Web { get; set; }
			public abstract int OriginalWidth { get; set; }
			public abstract int OriginalHeight { get; set; }
			public abstract int WebWidth { get; set; }
			public abstract int WebHeight { get; set; }
			public abstract int ThumbWidth { get; set; }
			public abstract int ThumbHeight { get; set; }
			public abstract bool IsLandscape { get; set; }
			public abstract string AdminNote { get; set; }
			public abstract string EquipmentMake { get; set; }
			public abstract string CameraModel { get; set; }
			public abstract int OriginalFileSize { get; set; }
			public abstract int MasterFileSize { get; set; }
			public abstract int TotalComments { get; set; }
			public abstract DateTime LastPost { get; set; }
			public abstract DateTime AverageCommentDateTime { get; set; }
			public abstract Guid Crop { get; set; }
			public abstract bool DsiConverted { get; set; }
			public abstract bool PhotoOfWeek { get; set; }
			public abstract DateTime PhotoOfWeekDateTime { get; set; }
			public abstract string PhotoOfWeekCaption { get; set; }
			public abstract double RandomNumber { get; set; }
			public abstract bool ContentDisabled { get; set; }
			public abstract Model.Entities.Photo.StatusEnum Status { get; set; }
			public abstract int GalleryTimeOrder { get; set; }
			public abstract int GalleryRatingOrder { get; set; }
			public abstract int EnabledByUsrK { get; set; }
			public abstract DateTime EnabledDateTime { get; set; }
			public abstract DateTime ParentDateTime { get; set; }
			public abstract int NextPhoto1K { get; set; }
			public abstract int NextPhoto2K { get; set; }
			public abstract int NextPhoto3K { get; set; }
			public abstract int PreviousPhoto1K { get; set; }
			public abstract int PreviousPhoto2K { get; set; }
			public abstract int PreviousPhoto3K { get; set; }
			public abstract string UrlFragment { get; set; }
			public abstract int UsrCount { get; set; }
			public abstract int FirstUsrK { get; set; }
			public abstract bool IsMasterCompressed { get; set; }
			public abstract bool IsProcessing { get; set; }
			public abstract Model.Entities.Photo.MediaTypes MediaType { get; set; }
			public abstract int ProcessingProgress { get; set; }
			public abstract DateTime? ProcessingStartDateTime { get; set; }
			public abstract Guid VideoLo { get; set; }
			public abstract Guid VideoMed { get; set; }
			public abstract Guid VideoHi { get; set; }
			public abstract Guid AudioLo { get; set; }
			public abstract Guid AudioMed { get; set; }
			public abstract Guid AudioHi { get; set; }
			public abstract Guid AudioMaster { get; set; }
			public abstract Guid VideoMaster { get; set; }
			public abstract string AudioFileExtention { get; set; }
			public abstract string VideoFileExtention { get; set; }
			public abstract int VideoMasterFileSize { get; set; }
			public abstract int AudioMasterFileSize { get; set; }
			public abstract double VideoMasterFramerate { get; set; }
			public abstract int VideoMasterHeight { get; set; }
			public abstract int VideoMasterWidth { get; set; }
			public abstract int VideoDuration { get; set; }
			public abstract int AudioDuration { get; set; }
			public abstract DateTime? ProcessingLastChange { get; set; }
			public abstract double VideoLoFramerate { get; set; }
			public abstract double VideoMedFramerate { get; set; }
			public abstract double VideoHiFramerate { get; set; }
			public abstract int VideoLoHeight { get; set; }
			public abstract int VideoMedHeight { get; set; }
			public abstract int VideoHiHeight { get; set; }
			public abstract int VideoLoWidth { get; set; }
			public abstract int VideoMedWidth { get; set; }
			public abstract int VideoHiWidth { get; set; }
			public abstract int ProcessingAttempts { get; set; }
			public abstract int OriginalHitsToday { get; set; }
			public abstract DateTime OriginalHitsDate { get; set; }
			public abstract string ProcessingServerName { get; set; }
			public abstract bool IsSonyK800i { get; set; }
			public abstract bool IsInCaptionCompetition { get; set; }
			public abstract int Rotate { get; set; }
			public abstract Guid UploadTemporary { get; set; }
			public abstract string UploadTemporaryExtention { get; set; }
			public abstract Model.Entities.Photo.Overlays Overlay { get; set; }
			public abstract string UploadTemporaryTags { get; set; }
			public abstract bool DoneAmazonPixMaster { get; set; }
			public abstract bool IsSonyC902 { get; set; }
			public abstract bool PhotoOfWeekUser { get; set; }
			public abstract string PhotoOfWeekUserCaption { get; set; }
			public abstract DateTime? PhotoOfWeekUserDateTime { get; set; }
			public abstract bool BlockedFromPhotoOfWeekUser { get; set; }
			public abstract Guid? FrontPagePic { get; set; }
			public abstract string FrontPagePicState { get; set; }
			public abstract string FrontPageCaptionClass { get; set; }
		}
		public partial interface IPhoto
		{
			int K { get; set; }
			int GalleryK { get; set; }
			int EventK { get; set; }
			int ArticleK { get; set; }
			int UsrK { get; set; }
			int MobileK { get; set; }
			double Order { get; set; }
			int? ThreadK { get; set; }
			DateTime DateTime { get; set; }
			int Views { get; set; }
			double AverageCoolRating { get; set; }
			double AverageSexyRating { get; set; }
			int TotalCoolRatings { get; set; }
			int TotalSexyRatings { get; set; }
			double WeightedCoolRating { get; set; }
			double WeightedSexyRating { get; set; }
			Guid Master { get; set; }
			Guid Original { get; set; }
			Guid Icon { get; set; }
			Guid Thumb { get; set; }
			Guid Web { get; set; }
			int OriginalWidth { get; set; }
			int OriginalHeight { get; set; }
			int WebWidth { get; set; }
			int WebHeight { get; set; }
			int ThumbWidth { get; set; }
			int ThumbHeight { get; set; }
			bool IsLandscape { get; set; }
			string AdminNote { get; set; }
			string EquipmentMake { get; set; }
			string CameraModel { get; set; }
			int OriginalFileSize { get; set; }
			int MasterFileSize { get; set; }
			int TotalComments { get; set; }
			DateTime LastPost { get; set; }
			DateTime AverageCommentDateTime { get; set; }
			Guid Crop { get; set; }
			bool DsiConverted { get; set; }
			bool PhotoOfWeek { get; set; }
			DateTime PhotoOfWeekDateTime { get; set; }
			string PhotoOfWeekCaption { get; set; }
			double RandomNumber { get; set; }
			bool ContentDisabled { get; set; }
			Model.Entities.Photo.StatusEnum Status { get; set; }
			int GalleryTimeOrder { get; set; }
			int GalleryRatingOrder { get; set; }
			int EnabledByUsrK { get; set; }
			DateTime EnabledDateTime { get; set; }
			DateTime ParentDateTime { get; set; }
			int NextPhoto1K { get; set; }
			int NextPhoto2K { get; set; }
			int NextPhoto3K { get; set; }
			int PreviousPhoto1K { get; set; }
			int PreviousPhoto2K { get; set; }
			int PreviousPhoto3K { get; set; }
			string UrlFragment { get; set; }
			int UsrCount { get; set; }
			int FirstUsrK { get; set; }
			bool IsMasterCompressed { get; set; }
			bool IsProcessing { get; set; }
			Model.Entities.Photo.MediaTypes MediaType { get; set; }
			int ProcessingProgress { get; set; }
			DateTime? ProcessingStartDateTime { get; set; }
			Guid VideoLo { get; set; }
			Guid VideoMed { get; set; }
			Guid VideoHi { get; set; }
			Guid AudioLo { get; set; }
			Guid AudioMed { get; set; }
			Guid AudioHi { get; set; }
			Guid AudioMaster { get; set; }
			Guid VideoMaster { get; set; }
			string AudioFileExtention { get; set; }
			string VideoFileExtention { get; set; }
			int VideoMasterFileSize { get; set; }
			int AudioMasterFileSize { get; set; }
			double VideoMasterFramerate { get; set; }
			int VideoMasterHeight { get; set; }
			int VideoMasterWidth { get; set; }
			int VideoDuration { get; set; }
			int AudioDuration { get; set; }
			DateTime? ProcessingLastChange { get; set; }
			double VideoLoFramerate { get; set; }
			double VideoMedFramerate { get; set; }
			double VideoHiFramerate { get; set; }
			int VideoLoHeight { get; set; }
			int VideoMedHeight { get; set; }
			int VideoHiHeight { get; set; }
			int VideoLoWidth { get; set; }
			int VideoMedWidth { get; set; }
			int VideoHiWidth { get; set; }
			int ProcessingAttempts { get; set; }
			int OriginalHitsToday { get; set; }
			DateTime OriginalHitsDate { get; set; }
			string ProcessingServerName { get; set; }
			bool IsSonyK800i { get; set; }
			bool IsInCaptionCompetition { get; set; }
			int Rotate { get; set; }
			Guid UploadTemporary { get; set; }
			string UploadTemporaryExtention { get; set; }
			Model.Entities.Photo.Overlays Overlay { get; set; }
			string UploadTemporaryTags { get; set; }
			bool DoneAmazonPixMaster { get; set; }
			bool IsSonyC902 { get; set; }
			bool PhotoOfWeekUser { get; set; }
			string PhotoOfWeekUserCaption { get; set; }
			DateTime? PhotoOfWeekUserDateTime { get; set; }
			bool BlockedFromPhotoOfWeekUser { get; set; }
			Guid? FrontPagePic { get; set; }
			string FrontPagePicState { get; set; }
			string FrontPageCaptionClass { get; set; }
		}
		
 		public abstract partial class PhotoReview : IPhotoReview
		{
			public abstract int K { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int PhotoK { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract int Rating { get; set; }
			public abstract Model.Entities.PhotoReview.RatingTypes RatingType { get; set; }
		}
		public partial interface IPhotoReview
		{
			int K { get; set; }
			int UsrK { get; set; }
			int PhotoK { get; set; }
			DateTime DateTime { get; set; }
			int Rating { get; set; }
			Model.Entities.PhotoReview.RatingTypes RatingType { get; set; }
		}
		
 		public abstract partial class Place : IPlace
		{
			public abstract int K { get; set; }
			public abstract string Name { get; set; }
			public abstract string UniqueName { get; set; }
			public abstract double Population { get; set; }
			public abstract double LatitudeDegreesNorth { get; set; }
			public abstract double LongitudeDegreesWest { get; set; }
			public abstract int SubCountry { get; set; }
			public abstract int CountryK { get; set; }
			public abstract bool Enabled { get; set; }
			public abstract Guid Pic { get; set; }
			public abstract string DetailsHtml { get; set; }
			public abstract int TotalEvents { get; set; }
			public abstract int TotalComments { get; set; }
			public abstract DateTime LastPost { get; set; }
			public abstract DateTime AverageCommentDateTime { get; set; }
			public abstract string RegionAbbreviation { get; set; }
			public abstract int RegionK { get; set; }
			public abstract string Code { get; set; }
			public abstract string Type { get; set; }
			public abstract bool IsRegionCapital { get; set; }
			public abstract bool IsCountryCapital { get; set; }
			public abstract bool ExcludeFromMap { get; set; }
			public abstract string UrlName { get; set; }
			public abstract string PicState { get; set; }
			public abstract int PicPhotoK { get; set; }
			public abstract int PicMiscK { get; set; }
			public abstract string UrlFragment { get; set; }
			public abstract int MeridianFeatureId { get; set; }
			public abstract double Lat { get; set; }
			public abstract double Lon { get; set; }
		}
		public partial interface IPlace
		{
			int K { get; set; }
			string Name { get; set; }
			string UniqueName { get; set; }
			double Population { get; set; }
			double LatitudeDegreesNorth { get; set; }
			double LongitudeDegreesWest { get; set; }
			int SubCountry { get; set; }
			int CountryK { get; set; }
			bool Enabled { get; set; }
			Guid Pic { get; set; }
			string DetailsHtml { get; set; }
			int TotalEvents { get; set; }
			int TotalComments { get; set; }
			DateTime LastPost { get; set; }
			DateTime AverageCommentDateTime { get; set; }
			string RegionAbbreviation { get; set; }
			int RegionK { get; set; }
			string Code { get; set; }
			string Type { get; set; }
			bool IsRegionCapital { get; set; }
			bool IsCountryCapital { get; set; }
			bool ExcludeFromMap { get; set; }
			string UrlName { get; set; }
			string PicState { get; set; }
			int PicPhotoK { get; set; }
			int PicMiscK { get; set; }
			string UrlFragment { get; set; }
			int MeridianFeatureId { get; set; }
			double Lat { get; set; }
			double Lon { get; set; }
		}
		
 		public abstract partial class Prefs : IPrefs
		{
			public abstract Guid Guid { get; set; }
			public abstract string PrefsString { get; set; }
		}
		public partial interface IPrefs
		{
			Guid Guid { get; set; }
			string PrefsString { get; set; }
		}
		
 		public abstract partial class Promoter : IPromoter
		{
			public abstract int K { get; set; }
			public abstract string Name { get; set; }
			public abstract Guid Pic { get; set; }
			public abstract int PrimaryUsrK { get; set; }
			public abstract string ContactName { get; set; }
			public abstract string CompanyName { get; set; }
			public abstract string PayPalAddress { get; set; }
			public abstract string PhoneNumber { get; set; }
			public abstract string AddressStreet { get; set; }
			public abstract string AddressArea { get; set; }
			public abstract string AddressTown { get; set; }
			public abstract string AddressCounty { get; set; }
			public abstract string AddressPostcode { get; set; }
			public abstract int AddressCountryK { get; set; }
			public abstract double PricingMultiplier { get; set; }
			public abstract DateTime DateTimeSignUp { get; set; }
			public abstract Model.Entities.Promoter.StatusEnum Status { get; set; }
			public abstract decimal TotalPaid { get; set; }
			public abstract DateTime DateExpires { get; set; }
			public abstract decimal RenewalFee { get; set; }
			public abstract int RenewalMonths { get; set; }
			public abstract string AdminNote { get; set; }
			public abstract int QuestionsThreadK { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract string UrlName { get; set; }
			public abstract bool HasGuestlist { get; set; }
			public abstract decimal GuestlistCharge { get; set; }
			public abstract int GuestlistCredit { get; set; }
			public abstract int GuestlistCreditLimit { get; set; }
			public abstract string PicState { get; set; }
			public abstract int PicPhotoK { get; set; }
			public abstract int PicMiscK { get; set; }
			public abstract int ClientsPerMonth { get; set; }
			public abstract int LastMessage { get; set; }
			public abstract string ManualNote { get; set; }
			public abstract decimal CreditLimit { get; set; }
			public abstract int InvoiceDueDays { get; set; }
			public abstract DateTime EnabledDateTime { get; set; }
			public abstract int EnabledByUsrK { get; set; }
			public abstract int SalesUsrK { get; set; }
			public abstract Model.Entities.Promoter.SalesStatusEnum SalesStatus { get; set; }
			public abstract DateTime? SalesStatusExpires { get; set; }
			public abstract DateTime SalesNextCall { get; set; }
			public abstract Model.Entities.Promoter.LetterTypes LetterType { get; set; }
			public abstract Model.Entities.Promoter.LetterStatusEnum LetterStatus { get; set; }
			public abstract bool IsSkeleton { get; set; }
			public abstract string AccessCodeRandom { get; set; }
			public abstract Model.Entities.Promoter.OfferTypes OfferType { get; set; }
			public abstract DateTime OfferExpireDateTime { get; set; }
			public abstract Model.Entities.Promoter.SalesEstimateEnum SalesEstimate { get; set; }
			public abstract bool SalesHold { get; set; }
			public abstract int FutureEvents { get; set; }
			public abstract bool DisableOverdueRedirect { get; set; }
			public abstract string ContactEmail { get; set; }
			public abstract string ContactTitle { get; set; }
			public abstract string ContactPersonalTitle { get; set; }
			public abstract string PhoneNumber2 { get; set; }
			public abstract string WebAddress { get; set; }
			public abstract bool Alarm { get; set; }
			public abstract string AccountsName { get; set; }
			public abstract string AccountsEmail { get; set; }
			public abstract string AccountsPhone { get; set; }
			public abstract Model.Entities.Promoter.ClientSectorEnum ClientSector { get; set; }
			public abstract bool EnableTickets { get; set; }
			public abstract Model.Entities.Promoter.VatStatusEnum VatStatus { get; set; }
			public abstract string VatNumber { get; set; }
			public abstract int VatCountryK { get; set; }
			public abstract int AddedByUsrK { get; set; }
			public abstract Model.Entities.Promoter.AddedMedhods AddedMethod { get; set; }
			public abstract string BankName { get; set; }
			public abstract string BankAccountName { get; set; }
			public abstract string BankAccountSortCode { get; set; }
			public abstract string BankAccountNumber { get; set; }
			public abstract bool OverrideApplyTicketFundsToInvoices { get; set; }
			public abstract int SalesCallCount { get; set; }
			public abstract bool RecentlyTransferred { get; set; }
			public abstract bool IsAgency { get; set; }
			public abstract int Discount { get; set; }
			public abstract bool AddRandomCodeToTickets { get; set; }
			public abstract bool WillCheckCardsForPurchasedTickets { get; set; }
			public abstract int SalesCampaignK { get; set; }
			public abstract decimal CostPerCampaignCredit { get; set; }
			public abstract bool SuspendReminderEmails { get; set; }
		}
		public partial interface IPromoter
		{
			int K { get; set; }
			string Name { get; set; }
			Guid Pic { get; set; }
			int PrimaryUsrK { get; set; }
			string ContactName { get; set; }
			string CompanyName { get; set; }
			string PayPalAddress { get; set; }
			string PhoneNumber { get; set; }
			string AddressStreet { get; set; }
			string AddressArea { get; set; }
			string AddressTown { get; set; }
			string AddressCounty { get; set; }
			string AddressPostcode { get; set; }
			int AddressCountryK { get; set; }
			double PricingMultiplier { get; set; }
			DateTime DateTimeSignUp { get; set; }
			Model.Entities.Promoter.StatusEnum Status { get; set; }
			decimal TotalPaid { get; set; }
			DateTime DateExpires { get; set; }
			decimal RenewalFee { get; set; }
			int RenewalMonths { get; set; }
			string AdminNote { get; set; }
			int QuestionsThreadK { get; set; }
			Guid DuplicateGuid { get; set; }
			string UrlName { get; set; }
			bool HasGuestlist { get; set; }
			decimal GuestlistCharge { get; set; }
			int GuestlistCredit { get; set; }
			int GuestlistCreditLimit { get; set; }
			string PicState { get; set; }
			int PicPhotoK { get; set; }
			int PicMiscK { get; set; }
			int ClientsPerMonth { get; set; }
			int LastMessage { get; set; }
			string ManualNote { get; set; }
			decimal CreditLimit { get; set; }
			int InvoiceDueDays { get; set; }
			DateTime EnabledDateTime { get; set; }
			int EnabledByUsrK { get; set; }
			int SalesUsrK { get; set; }
			Model.Entities.Promoter.SalesStatusEnum SalesStatus { get; set; }
			DateTime? SalesStatusExpires { get; set; }
			DateTime SalesNextCall { get; set; }
			Model.Entities.Promoter.LetterTypes LetterType { get; set; }
			Model.Entities.Promoter.LetterStatusEnum LetterStatus { get; set; }
			bool IsSkeleton { get; set; }
			string AccessCodeRandom { get; set; }
			Model.Entities.Promoter.OfferTypes OfferType { get; set; }
			DateTime OfferExpireDateTime { get; set; }
			Model.Entities.Promoter.SalesEstimateEnum SalesEstimate { get; set; }
			bool SalesHold { get; set; }
			int FutureEvents { get; set; }
			bool DisableOverdueRedirect { get; set; }
			string ContactEmail { get; set; }
			string ContactTitle { get; set; }
			string ContactPersonalTitle { get; set; }
			string PhoneNumber2 { get; set; }
			string WebAddress { get; set; }
			bool Alarm { get; set; }
			string AccountsName { get; set; }
			string AccountsEmail { get; set; }
			string AccountsPhone { get; set; }
			Model.Entities.Promoter.ClientSectorEnum ClientSector { get; set; }
			bool EnableTickets { get; set; }
			Model.Entities.Promoter.VatStatusEnum VatStatus { get; set; }
			string VatNumber { get; set; }
			int VatCountryK { get; set; }
			int AddedByUsrK { get; set; }
			Model.Entities.Promoter.AddedMedhods AddedMethod { get; set; }
			string BankName { get; set; }
			string BankAccountName { get; set; }
			string BankAccountSortCode { get; set; }
			string BankAccountNumber { get; set; }
			bool OverrideApplyTicketFundsToInvoices { get; set; }
			int SalesCallCount { get; set; }
			bool RecentlyTransferred { get; set; }
			bool IsAgency { get; set; }
			int Discount { get; set; }
			bool AddRandomCodeToTickets { get; set; }
			bool WillCheckCardsForPurchasedTickets { get; set; }
			int SalesCampaignK { get; set; }
			decimal CostPerCampaignCredit { get; set; }
			bool SuspendReminderEmails { get; set; }
		}
		
 		public abstract partial class PromoterUsr : IPromoterUsr
		{
			public abstract int PromoterK { get; set; }
			public abstract int UsrK { get; set; }
		}
		public partial interface IPromoterUsr
		{
			int PromoterK { get; set; }
			int UsrK { get; set; }
		}
		
 		public abstract partial class Region : IRegion
		{
			public abstract int K { get; set; }
			public abstract int CountryK { get; set; }
			public abstract int SubCountry { get; set; }
			public abstract string Name { get; set; }
			public abstract string Abbreviation { get; set; }
			public abstract double Population { get; set; }
			public abstract double Area { get; set; }
		}
		public partial interface IRegion
		{
			int K { get; set; }
			int CountryK { get; set; }
			int SubCountry { get; set; }
			string Name { get; set; }
			string Abbreviation { get; set; }
			double Population { get; set; }
			double Area { get; set; }
		}
		
 		public abstract partial class RoomPin : IRoomPin
		{
			public abstract int UsrK { get; set; }
			public abstract Guid RoomGuid { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract int ListOrder { get; set; }
			public abstract bool Pinned { get; set; }
			public abstract bool Expires { get; set; }
			public abstract DateTime DateTimeExpires { get; set; }
			public abstract bool? Starred { get; set; }
			public abstract string StateStub { get; set; }
		}
		public partial interface IRoomPin
		{
			int UsrK { get; set; }
			Guid RoomGuid { get; set; }
			DateTime DateTime { get; set; }
			int ListOrder { get; set; }
			bool Pinned { get; set; }
			bool Expires { get; set; }
			DateTime DateTimeExpires { get; set; }
			bool? Starred { get; set; }
			string StateStub { get; set; }
		}
		
 		public abstract partial class SalesCall : ISalesCall
		{
			public abstract int K { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract DateTime DateTimeStart { get; set; }
			public abstract DateTime DateTimeEnd { get; set; }
			public abstract double Duration { get; set; }
			public abstract bool InProgress { get; set; }
			public abstract Model.Entities.SalesCall.Directions Direction { get; set; }
			public abstract Model.Entities.SalesCall.Types Type { get; set; }
			public abstract bool Effective { get; set; }
			public abstract bool IsCall { get; set; }
			public abstract string Note { get; set; }
			public abstract bool Dismissed { get; set; }
			public abstract bool IsImportant { get; set; }
			public abstract bool IsCallToNewLead { get; set; }
		}
		public partial interface ISalesCall
		{
			int K { get; set; }
			Guid DuplicateGuid { get; set; }
			int UsrK { get; set; }
			int PromoterK { get; set; }
			DateTime DateTimeStart { get; set; }
			DateTime DateTimeEnd { get; set; }
			double Duration { get; set; }
			bool InProgress { get; set; }
			Model.Entities.SalesCall.Directions Direction { get; set; }
			Model.Entities.SalesCall.Types Type { get; set; }
			bool Effective { get; set; }
			bool IsCall { get; set; }
			string Note { get; set; }
			bool Dismissed { get; set; }
			bool IsImportant { get; set; }
			bool IsCallToNewLead { get; set; }
		}
		
 		public abstract partial class SalesCampaign : ISalesCampaign
		{
			public abstract int K { get; set; }
			public abstract int UsrK { get; set; }
			public abstract string Name { get; set; }
			public abstract string Description { get; set; }
			public abstract DateTime DateStart { get; set; }
			public abstract DateTime DateEnd { get; set; }
		}
		public partial interface ISalesCampaign
		{
			int K { get; set; }
			int UsrK { get; set; }
			string Name { get; set; }
			string Description { get; set; }
			DateTime DateStart { get; set; }
			DateTime DateEnd { get; set; }
		}
		
 		public abstract partial class SalesStatusChange : ISalesStatusChange
		{
			public abstract int K { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract Model.Entities.SalesStatusChange.Types Type { get; set; }
		}
		public partial interface ISalesStatusChange
		{
			int K { get; set; }
			Guid DuplicateGuid { get; set; }
			int UsrK { get; set; }
			int PromoterK { get; set; }
			DateTime DateTime { get; set; }
			Model.Entities.SalesStatusChange.Types Type { get; set; }
		}
		
 		public abstract partial class Setting : ISetting
		{
			public abstract string Name { get; set; }
			public abstract object Value { get; set; }
		}
		public partial interface ISetting
		{
			string Name { get; set; }
			object Value { get; set; }
		}
		
 		public abstract partial class SpottedException : ISpottedException
		{
			public abstract int K { get; set; }
			public abstract int ParentK { get; set; }
			public abstract DateTime ExceptionDateTime { get; set; }
			public abstract string ExceptionType { get; set; }
			public abstract string Message { get; set; }
			public abstract string Source { get; set; }
			public abstract string StackTrace { get; set; }
			public abstract string Url { get; set; }
			public abstract string MasterPath { get; set; }
			public abstract string PagePath { get; set; }
			public abstract string CurrentFilter { get; set; }
			public abstract int ObjectFilterK { get; set; }
			public abstract Model.Entities.ObjectType ObjectFilterType { get; set; }
			public abstract string MachineName { get; set; }
			public abstract int UsrK { get; set; }
			public abstract Guid DsiGuid { get; set; }
			public abstract string Cookies { get; set; }
			public abstract string PostData { get; set; }
			public abstract string IpAddress { get; set; }
			public abstract DateTime CommonTimeNow { get; set; }
		}
		public partial interface ISpottedException
		{
			int K { get; set; }
			int ParentK { get; set; }
			DateTime ExceptionDateTime { get; set; }
			string ExceptionType { get; set; }
			string Message { get; set; }
			string Source { get; set; }
			string StackTrace { get; set; }
			string Url { get; set; }
			string MasterPath { get; set; }
			string PagePath { get; set; }
			string CurrentFilter { get; set; }
			int ObjectFilterK { get; set; }
			Model.Entities.ObjectType ObjectFilterType { get; set; }
			string MachineName { get; set; }
			int UsrK { get; set; }
			Guid DsiGuid { get; set; }
			string Cookies { get; set; }
			string PostData { get; set; }
			string IpAddress { get; set; }
			DateTime CommonTimeNow { get; set; }
		}
		
 		public abstract partial class Tag : ITag
		{
			public abstract int K { get; set; }
			public abstract string TagText { get; set; }
			public abstract bool Blocked { get; set; }
			public abstract int BlockedByUsrK { get; set; }
			public abstract DateTime BlockedDateTime { get; set; }
			public abstract bool ShowInTagCloud { get; set; }
		}
		public partial interface ITag
		{
			int K { get; set; }
			string TagText { get; set; }
			bool Blocked { get; set; }
			int BlockedByUsrK { get; set; }
			DateTime BlockedDateTime { get; set; }
			bool ShowInTagCloud { get; set; }
		}
		
 		public abstract partial class TagPhoto : ITagPhoto
		{
			public abstract int K { get; set; }
			public abstract int TagK { get; set; }
			public abstract int PhotoK { get; set; }
			public abstract bool Disabled { get; set; }
		}
		public partial interface ITagPhoto
		{
			int K { get; set; }
			int TagK { get; set; }
			int PhotoK { get; set; }
			bool Disabled { get; set; }
		}
		
 		public abstract partial class TagPhotoHistory : ITagPhotoHistory
		{
			public abstract int K { get; set; }
			public abstract int TagPhotoK { get; set; }
			public abstract Model.Entities.TagPhotoHistory.TagPhotoHistoryAction Action { get; set; }
			public abstract int UsrK { get; set; }
			public abstract DateTime DateTime { get; set; }
		}
		public partial interface ITagPhotoHistory
		{
			int K { get; set; }
			int TagPhotoK { get; set; }
			Model.Entities.TagPhotoHistory.TagPhotoHistoryAction Action { get; set; }
			int UsrK { get; set; }
			DateTime DateTime { get; set; }
		}
		
 		public abstract partial class TeamTarget : ITeamTarget
		{
			public abstract int Year { get; set; }
			public abstract int Month { get; set; }
			public abstract double Target { get; set; }
			public abstract double Actual { get; set; }
		}
		public partial interface ITeamTarget
		{
			int Year { get; set; }
			int Month { get; set; }
			double Target { get; set; }
			double Actual { get; set; }
		}
		
 		public abstract partial class Theme : ITheme
		{
			public abstract int K { get; set; }
			public abstract string UrlName { get; set; }
			public abstract string Name { get; set; }
			public abstract string Description { get; set; }
			public abstract string Examples { get; set; }
			public abstract double Order { get; set; }
		}
		public partial interface ITheme
		{
			int K { get; set; }
			string UrlName { get; set; }
			string Name { get; set; }
			string Description { get; set; }
			string Examples { get; set; }
			double Order { get; set; }
		}
		
 		public abstract partial class Thread : IThread
		{
			public abstract int K { get; set; }
			public abstract string Subject { get; set; }
			public abstract Model.Entities.ObjectType ParentObjectType { get; set; }
			public abstract int ParentObjectK { get; set; }
			public abstract int UsrK { get; set; }
			public abstract bool Enabled { get; set; }
			public abstract DateTime LastPost { get; set; }
			public abstract int LastPostUsrK { get; set; }
			public abstract int TotalComments { get; set; }
			public abstract DateTime AverageCommentDateTime { get; set; }
			public abstract bool Private { get; set; }
			public abstract bool GroupPrivate { get; set; }
			public abstract bool PrivateGroup { get; set; }
			public abstract int ThemeK { get; set; }
			public abstract int ArticleK { get; set; }
			public abstract int PhotoK { get; set; }
			public abstract int EventK { get; set; }
			public abstract int VenueK { get; set; }
			public abstract int PlaceK { get; set; }
			public abstract int CountryK { get; set; }
			public abstract int BrandK { get; set; }
			public abstract int GroupK { get; set; }
			public abstract int MusicTypeK { get; set; }
			public abstract bool IsNews { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract bool IsNationwideNews { get; set; }
			public abstract bool IsReview { get; set; }
			public abstract bool IsSticky { get; set; }
			public abstract bool IsWorldwideNews { get; set; }
			public abstract int TotalParticipants { get; set; }
			public abstract int FirstParticipantUsrK { get; set; }
			public abstract bool HideFromHighlights { get; set; }
			public abstract DateTime HotTopicsOrder { get; set; }
			public abstract string UrlFragment { get; set; }
			public abstract bool Sealed { get; set; }
			public abstract bool Closed { get; set; }
			public abstract Model.Entities.Thread.NewsStatusEnum NewsStatus { get; set; }
			public abstract int NewsLevel { get; set; }
			public abstract int NewsUsrK { get; set; }
			public abstract int TotalWatching { get; set; }
			public abstract int NewsModeratorUsrK { get; set; }
			public abstract int NewsModeratedByUsrK { get; set; }
			public abstract DateTime NewsModerationDateTime { get; set; }
			public abstract bool IsInCaptionCompetition { get; set; }
		}
		public partial interface IThread
		{
			int K { get; set; }
			string Subject { get; set; }
			Model.Entities.ObjectType ParentObjectType { get; set; }
			int ParentObjectK { get; set; }
			int UsrK { get; set; }
			bool Enabled { get; set; }
			DateTime LastPost { get; set; }
			int LastPostUsrK { get; set; }
			int TotalComments { get; set; }
			DateTime AverageCommentDateTime { get; set; }
			bool Private { get; set; }
			bool GroupPrivate { get; set; }
			bool PrivateGroup { get; set; }
			int ThemeK { get; set; }
			int ArticleK { get; set; }
			int PhotoK { get; set; }
			int EventK { get; set; }
			int VenueK { get; set; }
			int PlaceK { get; set; }
			int CountryK { get; set; }
			int BrandK { get; set; }
			int GroupK { get; set; }
			int MusicTypeK { get; set; }
			bool IsNews { get; set; }
			DateTime DateTime { get; set; }
			bool IsNationwideNews { get; set; }
			bool IsReview { get; set; }
			bool IsSticky { get; set; }
			bool IsWorldwideNews { get; set; }
			int TotalParticipants { get; set; }
			int FirstParticipantUsrK { get; set; }
			bool HideFromHighlights { get; set; }
			DateTime HotTopicsOrder { get; set; }
			string UrlFragment { get; set; }
			bool Sealed { get; set; }
			bool Closed { get; set; }
			Model.Entities.Thread.NewsStatusEnum NewsStatus { get; set; }
			int NewsLevel { get; set; }
			int NewsUsrK { get; set; }
			int TotalWatching { get; set; }
			int NewsModeratorUsrK { get; set; }
			int NewsModeratedByUsrK { get; set; }
			DateTime NewsModerationDateTime { get; set; }
			bool IsInCaptionCompetition { get; set; }
		}
		
 		public abstract partial class ThreadUsr : IThreadUsr
		{
			public abstract int ThreadK { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int InvitingUsrK { get; set; }
			public abstract Model.Entities.ThreadUsr.StatusEnum Status { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract Model.Entities.ThreadUsr.PrivateChatTypes PrivateChatType { get; set; }
			public abstract bool Favourite { get; set; }
			public abstract bool Deleted { get; set; }
			public abstract DateTime ViewDateTime { get; set; }
			public abstract DateTime ViewDateTimeLatest { get; set; }
			public abstract int ViewComments { get; set; }
			public abstract int ViewCommentsLatest { get; set; }
			public abstract DateTime StatusChangeDateTime { get; set; }
			public abstract Model.Entities.ObjectType StatusChangeObjectType { get; set; }
			public abstract int StatusChangeObjectK { get; set; }
		}
		public partial interface IThreadUsr
		{
			int ThreadK { get; set; }
			int UsrK { get; set; }
			int InvitingUsrK { get; set; }
			Model.Entities.ThreadUsr.StatusEnum Status { get; set; }
			DateTime DateTime { get; set; }
			Model.Entities.ThreadUsr.PrivateChatTypes PrivateChatType { get; set; }
			bool Favourite { get; set; }
			bool Deleted { get; set; }
			DateTime ViewDateTime { get; set; }
			DateTime ViewDateTimeLatest { get; set; }
			int ViewComments { get; set; }
			int ViewCommentsLatest { get; set; }
			DateTime StatusChangeDateTime { get; set; }
			Model.Entities.ObjectType StatusChangeObjectType { get; set; }
			int StatusChangeObjectK { get; set; }
		}
		
 		public abstract partial class Ticket : ITicket
		{
			public abstract int K { get; set; }
			public abstract int TicketRunK { get; set; }
			public abstract int EventK { get; set; }
			public abstract int BuyerUsrK { get; set; }
			public abstract bool Enabled { get; set; }
			public abstract bool Cancelled { get; set; }
			public abstract DateTime BuyDateTime { get; set; }
			public abstract string AddressStreet { get; set; }
			public abstract string AddressArea { get; set; }
			public abstract string AddressTown { get; set; }
			public abstract string AddressCounty { get; set; }
			public abstract string AddressPostcode { get; set; }
			public abstract int AddressCountryK { get; set; }
			public abstract string Mobile { get; set; }
			public abstract string MobileCountryCode { get; set; }
			public abstract string MobileNumber { get; set; }
			public abstract string FirstName { get; set; }
			public abstract string LastName { get; set; }
			public abstract Guid CardNumberHash { get; set; }
			public abstract string CardNumberEnd { get; set; }
			public abstract int CardNumberDigits { get; set; }
			public abstract int Quantity { get; set; }
			public abstract string CustomData { get; set; }
			public abstract string CustomXml { get; set; }
			public abstract int InvoiceItemK { get; set; }
			public abstract Guid BrowserGuid { get; set; }
			public abstract decimal Price { get; set; }
			public abstract decimal BookingFee { get; set; }
			public abstract string IpAddress { get; set; }
			public abstract Model.Entities.Ticket.FeedbackEnum Feedback { get; set; }
			public abstract string FeedbackNote { get; set; }
			public abstract DateTime ReserveDateTime { get; set; }
			public abstract string Code { get; set; }
			public abstract int DomainK { get; set; }
			public abstract bool CancelledBeforeFundsRelease { get; set; }
			public abstract DateTime CancelledDateTime { get; set; }
			public abstract string CardCV2 { get; set; }
			public abstract bool CardCheckedByPromoter { get; set; }
			public abstract int CardCheckAttempts { get; set; }
			public abstract string AddressName { get; set; }
			public abstract bool? IsFraud { get; set; }
		}
		public partial interface ITicket
		{
			int K { get; set; }
			int TicketRunK { get; set; }
			int EventK { get; set; }
			int BuyerUsrK { get; set; }
			bool Enabled { get; set; }
			bool Cancelled { get; set; }
			DateTime BuyDateTime { get; set; }
			string AddressStreet { get; set; }
			string AddressArea { get; set; }
			string AddressTown { get; set; }
			string AddressCounty { get; set; }
			string AddressPostcode { get; set; }
			int AddressCountryK { get; set; }
			string Mobile { get; set; }
			string MobileCountryCode { get; set; }
			string MobileNumber { get; set; }
			string FirstName { get; set; }
			string LastName { get; set; }
			Guid CardNumberHash { get; set; }
			string CardNumberEnd { get; set; }
			int CardNumberDigits { get; set; }
			int Quantity { get; set; }
			string CustomData { get; set; }
			string CustomXml { get; set; }
			int InvoiceItemK { get; set; }
			Guid BrowserGuid { get; set; }
			decimal Price { get; set; }
			decimal BookingFee { get; set; }
			string IpAddress { get; set; }
			Model.Entities.Ticket.FeedbackEnum Feedback { get; set; }
			string FeedbackNote { get; set; }
			DateTime ReserveDateTime { get; set; }
			string Code { get; set; }
			int DomainK { get; set; }
			bool CancelledBeforeFundsRelease { get; set; }
			DateTime CancelledDateTime { get; set; }
			string CardCV2 { get; set; }
			bool CardCheckedByPromoter { get; set; }
			int CardCheckAttempts { get; set; }
			string AddressName { get; set; }
			bool? IsFraud { get; set; }
		}
		
 		public abstract partial class TicketPromoterEvent : ITicketPromoterEvent
		{
			public abstract int PromoterK { get; set; }
			public abstract int EventK { get; set; }
			public abstract int TotalTickets { get; set; }
			public abstract int SoldTickets { get; set; }
			public abstract decimal TotalFunds { get; set; }
			public abstract bool FundsLockManual { get; set; }
			public abstract int FundsLockManualUsrK { get; set; }
			public abstract DateTime FundsLockManualDateTime { get; set; }
			public abstract string FundsLockManualNote { get; set; }
			public abstract bool FundsLockFraudIpDuplicate { get; set; }
			public abstract int FundsLockFraudIpCountry { get; set; }
			public abstract bool FundsLockFraudGuid { get; set; }
			public abstract bool FundsLockUsrResponses { get; set; }
			public abstract string FundsLockText { get; set; }
			public abstract bool FundsLockOverride { get; set; }
			public abstract int FundsLockOverrideUsrK { get; set; }
			public abstract DateTime FundsLockOverrideDateTime { get; set; }
			public abstract string FundsLockOverrideNote { get; set; }
			public abstract bool FundsReleased { get; set; }
			public abstract int FundsTransferK { get; set; }
			public abstract int CancelledTickets { get; set; }
			public abstract bool FundsLockTotalFundsDontMatch { get; set; }
			public abstract decimal TotalVat { get; set; }
			public abstract decimal TotalBookingFees { get; set; }
			public abstract string ContactEmail { get; set; }
		}
		public partial interface ITicketPromoterEvent
		{
			int PromoterK { get; set; }
			int EventK { get; set; }
			int TotalTickets { get; set; }
			int SoldTickets { get; set; }
			decimal TotalFunds { get; set; }
			bool FundsLockManual { get; set; }
			int FundsLockManualUsrK { get; set; }
			DateTime FundsLockManualDateTime { get; set; }
			string FundsLockManualNote { get; set; }
			bool FundsLockFraudIpDuplicate { get; set; }
			int FundsLockFraudIpCountry { get; set; }
			bool FundsLockFraudGuid { get; set; }
			bool FundsLockUsrResponses { get; set; }
			string FundsLockText { get; set; }
			bool FundsLockOverride { get; set; }
			int FundsLockOverrideUsrK { get; set; }
			DateTime FundsLockOverrideDateTime { get; set; }
			string FundsLockOverrideNote { get; set; }
			bool FundsReleased { get; set; }
			int FundsTransferK { get; set; }
			int CancelledTickets { get; set; }
			bool FundsLockTotalFundsDontMatch { get; set; }
			decimal TotalVat { get; set; }
			decimal TotalBookingFees { get; set; }
			string ContactEmail { get; set; }
		}
		
 		public abstract partial class TicketRun : ITicketRun
		{
			public abstract int K { get; set; }
			public abstract int EventK { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract int BrandK { get; set; }
			public abstract string Name { get; set; }
			public abstract string Description { get; set; }
			public abstract decimal Price { get; set; }
			public abstract decimal BookingFee { get; set; }
			public abstract bool LockPrice { get; set; }
			public abstract int FollowsTicketRunK { get; set; }
			public abstract DateTime StartDateTime { get; set; }
			public abstract DateTime EndDateTime { get; set; }
			public abstract int MaxTickets { get; set; }
			public abstract int SoldTickets { get; set; }
			public abstract double ListOrder { get; set; }
			public abstract bool Paused { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract bool EmailSent { get; set; }
			public abstract DateTime DeliveryDate { get; set; }
			public abstract Model.Entities.TicketRun.DeliveryMethodType DeliveryMethod { get; set; }
			public abstract decimal DeliveryCharge { get; set; }
		}
		public partial interface ITicketRun
		{
			int K { get; set; }
			int EventK { get; set; }
			int PromoterK { get; set; }
			int BrandK { get; set; }
			string Name { get; set; }
			string Description { get; set; }
			decimal Price { get; set; }
			decimal BookingFee { get; set; }
			bool LockPrice { get; set; }
			int FollowsTicketRunK { get; set; }
			DateTime StartDateTime { get; set; }
			DateTime EndDateTime { get; set; }
			int MaxTickets { get; set; }
			int SoldTickets { get; set; }
			double ListOrder { get; set; }
			bool Paused { get; set; }
			Guid DuplicateGuid { get; set; }
			bool EmailSent { get; set; }
			DateTime DeliveryDate { get; set; }
			Model.Entities.TicketRun.DeliveryMethodType DeliveryMethod { get; set; }
			decimal DeliveryCharge { get; set; }
		}
		
 		public abstract partial class TrafficExceptionDay : ITrafficExceptionDay
		{
			public abstract DateTime ExceptionDate { get; set; }
			public abstract DateTime DateToUseInstead { get; set; }
		}
		public partial interface ITrafficExceptionDay
		{
			DateTime ExceptionDate { get; set; }
			DateTime DateToUseInstead { get; set; }
		}
		
 		public abstract partial class TrafficLevelRelativeToMinuteOfDay : ITrafficLevelRelativeToMinuteOfDay
		{
			public abstract int Minute { get; set; }
			public abstract int TrafficLevel { get; set; }
		}
		public partial interface ITrafficLevelRelativeToMinuteOfDay
		{
			int Minute { get; set; }
			int TrafficLevel { get; set; }
		}
		
 		public abstract partial class Transfer : ITransfer
		{
			public abstract int K { get; set; }
			public abstract Model.Entities.Transfer.TransferTypes Type { get; set; }
			public abstract Model.Entities.Transfer.StatusEnum Status { get; set; }
			public abstract Model.Entities.Transfer.Methods Method { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract int ActionUsrK { get; set; }
			public abstract decimal Amount { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract DateTime DateTimeCreated { get; set; }
			public abstract DateTime DateTimeComplete { get; set; }
			public abstract string ClientHost { get; set; }
			public abstract string CardName { get; set; }
			public abstract string CardAddress1 { get; set; }
			public abstract string CardPostcode { get; set; }
			public abstract int CardSavedTransferK { get; set; }
			public abstract Guid CardNumberHash { get; set; }
			public abstract string CardNumberEnd { get; set; }
			public abstract Model.Entities.BinRange.Types CardType { get; set; }
			public abstract DateTime CardStart { get; set; }
			public abstract DateTime CardExpires { get; set; }
			public abstract int CardIssue { get; set; }
			public abstract string CardCV2 { get; set; }
			public abstract bool CardSaved { get; set; }
			public abstract string BankAccountName { get; set; }
			public abstract string BankName { get; set; }
			public abstract string BankSortCode { get; set; }
			public abstract string BankAccountNumber { get; set; }
			public abstract string BankTransferReference { get; set; }
			public abstract string CardResponseAuthCode { get; set; }
			public abstract string CardResponseCv2Avs { get; set; }
			public abstract string CardResponseMessage { get; set; }
			public abstract string CardResponseRespCode { get; set; }
			public abstract string CardResponseCode { get; set; }
			public abstract bool CardResponseIsCv2Match { get; set; }
			public abstract bool CardResponseIsPostCodeMatch { get; set; }
			public abstract bool CardResponseIsAddressMatch { get; set; }
			public abstract bool CardResponseIsDataChecked { get; set; }
			public abstract string Notes { get; set; }
			public abstract bool IsFullyApplied { get; set; }
			public abstract Guid Guid { get; set; }
			public abstract int TransferRefundedK { get; set; }
			public abstract Model.Entities.Transfer.RefundStatuses RefundStatus { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract string ChequeReferenceNumber { get; set; }
			public abstract int CardDigits { get; set; }
			public abstract Model.Entities.Transfer.DSIBankAccounts DSIBankAccount { get; set; }
			public abstract string CardAddressArea { get; set; }
			public abstract string CardAddressTown { get; set; }
			public abstract string CardAddressCounty { get; set; }
			public abstract int CardAddressCountryK { get; set; }
			public abstract Model.Entities.Transfer.CompanyEnum Company { get; set; }
		}
		public partial interface ITransfer
		{
			int K { get; set; }
			Model.Entities.Transfer.TransferTypes Type { get; set; }
			Model.Entities.Transfer.StatusEnum Status { get; set; }
			Model.Entities.Transfer.Methods Method { get; set; }
			int UsrK { get; set; }
			int PromoterK { get; set; }
			int ActionUsrK { get; set; }
			decimal Amount { get; set; }
			DateTime DateTime { get; set; }
			DateTime DateTimeCreated { get; set; }
			DateTime DateTimeComplete { get; set; }
			string ClientHost { get; set; }
			string CardName { get; set; }
			string CardAddress1 { get; set; }
			string CardPostcode { get; set; }
			int CardSavedTransferK { get; set; }
			Guid CardNumberHash { get; set; }
			string CardNumberEnd { get; set; }
			Model.Entities.BinRange.Types CardType { get; set; }
			DateTime CardStart { get; set; }
			DateTime CardExpires { get; set; }
			int CardIssue { get; set; }
			string CardCV2 { get; set; }
			bool CardSaved { get; set; }
			string BankAccountName { get; set; }
			string BankName { get; set; }
			string BankSortCode { get; set; }
			string BankAccountNumber { get; set; }
			string BankTransferReference { get; set; }
			string CardResponseAuthCode { get; set; }
			string CardResponseCv2Avs { get; set; }
			string CardResponseMessage { get; set; }
			string CardResponseRespCode { get; set; }
			string CardResponseCode { get; set; }
			bool CardResponseIsCv2Match { get; set; }
			bool CardResponseIsPostCodeMatch { get; set; }
			bool CardResponseIsAddressMatch { get; set; }
			bool CardResponseIsDataChecked { get; set; }
			string Notes { get; set; }
			bool IsFullyApplied { get; set; }
			Guid Guid { get; set; }
			int TransferRefundedK { get; set; }
			Model.Entities.Transfer.RefundStatuses RefundStatus { get; set; }
			Guid DuplicateGuid { get; set; }
			string ChequeReferenceNumber { get; set; }
			int CardDigits { get; set; }
			Model.Entities.Transfer.DSIBankAccounts DSIBankAccount { get; set; }
			string CardAddressArea { get; set; }
			string CardAddressTown { get; set; }
			string CardAddressCounty { get; set; }
			int CardAddressCountryK { get; set; }
			Model.Entities.Transfer.CompanyEnum Company { get; set; }
		}
		
 		public abstract partial class Usr : IUsr
		{
			public abstract int K { get; set; }
			public abstract string Email { get; set; }
			public abstract string Password { get; set; }
			public abstract int LoginCount { get; set; }
			public abstract bool IsAdmin { get; set; }
			public abstract bool IsEmailVerified { get; set; }
			public abstract Guid Pic { get; set; }
			public abstract Guid PicOriginal { get; set; }
			public abstract string FirstName { get; set; }
			public abstract string LastName { get; set; }
			public abstract string NickName { get; set; }
			public abstract string Mobile { get; set; }
			public abstract string MobileCountryCode { get; set; }
			public abstract string MobileNumber { get; set; }
			public abstract bool SendSpottedEmails { get; set; }
			public abstract bool SendSpottedTexts { get; set; }
			public abstract bool SendPartnerEmails { get; set; }
			public abstract bool SendPartnerTexts { get; set; }
			public abstract string UpdateData { get; set; }
			public abstract string AdminNote { get; set; }
			public abstract DateTime DateTimeLastAccess { get; set; }
			public abstract DateTime DateTimeSignUp { get; set; }
			public abstract DateTime DateTimeLastPageRequest { get; set; }
			public abstract string PrefsText { get; set; }
			public abstract string LoginString { get; set; }
			public abstract string PersonalStatement { get; set; }
			public abstract int AddedByUsrK { get; set; }
			public abstract Model.Entities.Usr.AdminLevels AdminLevel { get; set; }
			public abstract double RandomNumber { get; set; }
			public abstract DateTime LastPrivateComment { get; set; }
			public abstract DateTime LastPrivateChatMessage { get; set; }
			public abstract bool IsSingle { get; set; }
			public abstract bool IsMale { get; set; }
			public abstract bool IsFemale { get; set; }
			public abstract DateTime DateOfBirth { get; set; }
			public abstract bool DateSexMale { get; set; }
			public abstract bool DateSexFemale { get; set; }
			public abstract int DateAgeRangeLow { get; set; }
			public abstract int DateAgeRangeHigh { get; set; }
			public abstract bool Relationship1 { get; set; }
			public abstract bool Relationship2 { get; set; }
			public abstract bool Relationship3 { get; set; }
			public abstract bool SexHelperMale { get; set; }
			public abstract bool SexHelperFemale { get; set; }
			public abstract int BuddyCount { get; set; }
			public abstract int ChatMessageCount { get; set; }
			public abstract int CommentCount { get; set; }
			public abstract int EventCount { get; set; }
			public abstract int HomePlaceK { get; set; }
			public abstract bool AgreeTerms { get; set; }
			public abstract int GuestClientK { get; set; }
			public abstract int FavouriteMusicTypeK { get; set; }
			public abstract int TotalLol { get; set; }
			public abstract int TotalMadeLol { get; set; }
			public abstract DateTime LastLol { get; set; }
			public abstract int UniqueMadeLol { get; set; }
			public abstract string ChatXml { get; set; }
			public abstract bool IsLoggedOn { get; set; }
			public abstract long LastChatItem { get; set; }
			public abstract string LastIp { get; set; }
			public abstract bool Ignore { get; set; }
			public abstract bool IsProSpotter { get; set; }
			public abstract int LastInvite { get; set; }
			public abstract int IntroducedByUsrK { get; set; }
			public abstract bool SendFlyers { get; set; }
			public abstract bool SendInvites { get; set; }
			public abstract int TotalPhotoUploads { get; set; }
			public abstract int TempInt { get; set; }
			public abstract bool EnhancedSecurity { get; set; }
			public abstract string AddressStreet { get; set; }
			public abstract string AddressArea { get; set; }
			public abstract string AddressTown { get; set; }
			public abstract string AddressCounty { get; set; }
			public abstract string AddressPostcode { get; set; }
			public abstract int AddressCountryK { get; set; }
			public abstract Model.Entities.Usr.CardStatusEnum CardStatus { get; set; }
			public abstract int TotalCardsSent { get; set; }
			public abstract bool IsSpotter { get; set; }
			public abstract bool Banned { get; set; }
			public abstract int BannedByUsrK { get; set; }
			public abstract DateTime BannedDateTime { get; set; }
			public abstract string BannedReason { get; set; }
			public abstract bool UpdateSendGenericMusic { get; set; }
			public abstract int UpdateLargeEvents { get; set; }
			public abstract bool UpdateSendBuddies { get; set; }
			public abstract string PicState { get; set; }
			public abstract int PicPhotoK { get; set; }
			public abstract int PicMiscK { get; set; }
			public abstract bool IsChatting { get; set; }
			public abstract DateTime LastBuddyChange { get; set; }
			public abstract bool NewsModerator { get; set; }
			public abstract int NewsPermissionLevel { get; set; }
			public abstract bool IsBetaTester { get; set; }
			public abstract int PlacesVisitCount { get; set; }
			public abstract int MusicTypesFavouriteCount { get; set; }
			public abstract int PhotosMeCount { get; set; }
			public abstract bool IsSkeleton { get; set; }
			public abstract bool NoInboxEmails { get; set; }
			public abstract int AbuseReportsPending { get; set; }
			public abstract int AbuseReportsUseful { get; set; }
			public abstract int AbuseReportsOverturned { get; set; }
			public abstract int AbuseAccusationsPending { get; set; }
			public abstract int AbuseAccusationsAbuse { get; set; }
			public abstract int AbuseAccusationsNoAbuse { get; set; }
			public abstract bool ModeratePhotos { get; set; }
			public abstract int ChatSessionId { get; set; }
			public abstract int AddedByGroupK { get; set; }
			public abstract int DonateIcon { get; set; }
			public abstract DateTime DonateExpire { get; set; }
			public abstract DateTime EmailDateTime { get; set; }
			public abstract string EmailIp { get; set; }
			public abstract bool EmailHold { get; set; }
			public abstract bool IsHtmlEditor { get; set; }
			public abstract bool IsGroupModerator { get; set; }
			public abstract bool IsSkeletonFromSignup { get; set; }
			public abstract int ExtraIcon { get; set; }
			public abstract DateTime ExtraExpire { get; set; }
			public abstract int SpottingsTotal { get; set; }
			public abstract int SpottingsMonth { get; set; }
			public abstract int SpottingsMonthRank { get; set; }
			public abstract bool IsPromoter { get; set; }
			public abstract int CampTickets { get; set; }
			public abstract bool HasTicket { get; set; }
			public abstract DateTime LastTicketEventDateTime { get; set; }
			public abstract Guid PasswordHash { get; set; }
			public abstract Guid PasswordSalt { get; set; }
			public abstract string PasswordResetEmailSecret { get; set; }
			public abstract bool LegalTermsUser1 { get; set; }
			public abstract bool LegalTermsPromoter1 { get; set; }
			public abstract bool IsSuperAdmin { get; set; }
			public abstract bool IsSalesPerson { get; set; }
			public abstract DateTime BuyableLockDateTime { get; set; }
			public abstract int SalesTeam { get; set; }
			public abstract Guid Guid { get; set; }
			public abstract bool LegalTermsUser2 { get; set; }
			public abstract bool LegalTermsPromoter2 { get; set; }
			public abstract DateTime LastPhotoUpload { get; set; }
			public abstract DateTime DateTimeLastUpdateEmail { get; set; }
			public abstract bool InvitedViaContactImporter { get; set; }
			public abstract bool IsTicketsRegistered { get; set; }
			public abstract bool ExDirectory { get; set; }
			public abstract bool IsEmailBroken { get; set; }
			public abstract DateTime? DateTimeLastChatMessage { get; set; }
			public abstract int? RolloverDonationIconK { get; set; }
			public abstract Guid? ChatPic { get; set; }
			public abstract int? ChatPicPhotoK { get; set; }
			public abstract string ChatPicState { get; set; }
			public abstract DateTime? DateTimeLastBuddyAlertsRoomRefresh { get; set; }
			public abstract Model.Entities.Usr.PhotoUsageEnum PhotoUsage { get; set; }
			public abstract long? FacebookUID { get; set; }
			public abstract bool FacebookConnected { get; set; }
			public abstract DateTime? FacebookConnectedDateTime { get; set; }
			public abstract bool FacebookPermissionEmail { get; set; }
			public abstract bool FacebookPermissionPublish { get; set; }
			public abstract bool FacebookPermissionEvent { get; set; }
			public abstract bool FacebookPermissionRsvp { get; set; }
			public abstract bool FacebookStoryAttendEvent { get; set; }
			public abstract bool FacebookStoryBuyTicket { get; set; }
			public abstract bool FacebookStoryUploadPhoto { get; set; }
			public abstract bool FacebookStorySpotted { get; set; }
			public abstract bool FacebookStoryPhotoFeatured { get; set; }
			public abstract bool FacebookStoryNewBuddy { get; set; }
			public abstract bool FacebookStoryPublishArticle { get; set; }
			public abstract bool FacebookStoryJoinGroup { get; set; }
			public abstract bool FacebookStoryFavourite { get; set; }
			public abstract bool FacebookStoryNewTopic { get; set; }
			public abstract bool FacebookStoryEventReview { get; set; }
			public abstract bool FacebookStoryPostNews { get; set; }
			public abstract bool FacebookStoryLaugh { get; set; }
			public abstract bool FacebookEventAdd { get; set; }
			public abstract bool FacebookEventAttend { get; set; }
			public abstract string FacebookEmail { get; set; }
			public abstract bool? IsDj { get; set; }
			public abstract bool? FacebookStory { get; set; }
			public abstract bool FacebookStory1 { get; set; }
			public abstract string FacebookAccessToken { get; set; }
			public abstract bool FacebookStoryFavouriteTopic { get; set; }
			public abstract bool? NeedsCaptcha { get; set; }
			public abstract bool? PassedCaptcha { get; set; }
			public abstract DateTime? BouncePeriodDateTime { get; set; }
			public abstract int? TotalEmailsSentInPeriod { get; set; }
			public abstract int? MatchedHardBounceInPeriod { get; set; }
			public abstract int? UnmatchedHardBounceInPeriod { get; set; }
			public abstract int? SoftBounceInPeriod { get; set; }
		}
		public partial interface IUsr
		{
			int K { get; set; }
			string Email { get; set; }
			string Password { get; set; }
			int LoginCount { get; set; }
			bool IsAdmin { get; set; }
			bool IsEmailVerified { get; set; }
			Guid Pic { get; set; }
			Guid PicOriginal { get; set; }
			string FirstName { get; set; }
			string LastName { get; set; }
			string NickName { get; set; }
			string Mobile { get; set; }
			string MobileCountryCode { get; set; }
			string MobileNumber { get; set; }
			bool SendSpottedEmails { get; set; }
			bool SendSpottedTexts { get; set; }
			bool SendPartnerEmails { get; set; }
			bool SendPartnerTexts { get; set; }
			string UpdateData { get; set; }
			string AdminNote { get; set; }
			DateTime DateTimeLastAccess { get; set; }
			DateTime DateTimeSignUp { get; set; }
			DateTime DateTimeLastPageRequest { get; set; }
			string PrefsText { get; set; }
			string LoginString { get; set; }
			string PersonalStatement { get; set; }
			int AddedByUsrK { get; set; }
			Model.Entities.Usr.AdminLevels AdminLevel { get; set; }
			double RandomNumber { get; set; }
			DateTime LastPrivateComment { get; set; }
			DateTime LastPrivateChatMessage { get; set; }
			bool IsSingle { get; set; }
			bool IsMale { get; set; }
			bool IsFemale { get; set; }
			DateTime DateOfBirth { get; set; }
			bool DateSexMale { get; set; }
			bool DateSexFemale { get; set; }
			int DateAgeRangeLow { get; set; }
			int DateAgeRangeHigh { get; set; }
			bool Relationship1 { get; set; }
			bool Relationship2 { get; set; }
			bool Relationship3 { get; set; }
			bool SexHelperMale { get; set; }
			bool SexHelperFemale { get; set; }
			int BuddyCount { get; set; }
			int ChatMessageCount { get; set; }
			int CommentCount { get; set; }
			int EventCount { get; set; }
			int HomePlaceK { get; set; }
			bool AgreeTerms { get; set; }
			int GuestClientK { get; set; }
			int FavouriteMusicTypeK { get; set; }
			int TotalLol { get; set; }
			int TotalMadeLol { get; set; }
			DateTime LastLol { get; set; }
			int UniqueMadeLol { get; set; }
			string ChatXml { get; set; }
			bool IsLoggedOn { get; set; }
			long LastChatItem { get; set; }
			string LastIp { get; set; }
			bool Ignore { get; set; }
			bool IsProSpotter { get; set; }
			int LastInvite { get; set; }
			int IntroducedByUsrK { get; set; }
			bool SendFlyers { get; set; }
			bool SendInvites { get; set; }
			int TotalPhotoUploads { get; set; }
			int TempInt { get; set; }
			bool EnhancedSecurity { get; set; }
			string AddressStreet { get; set; }
			string AddressArea { get; set; }
			string AddressTown { get; set; }
			string AddressCounty { get; set; }
			string AddressPostcode { get; set; }
			int AddressCountryK { get; set; }
			Model.Entities.Usr.CardStatusEnum CardStatus { get; set; }
			int TotalCardsSent { get; set; }
			bool IsSpotter { get; set; }
			bool Banned { get; set; }
			int BannedByUsrK { get; set; }
			DateTime BannedDateTime { get; set; }
			string BannedReason { get; set; }
			bool UpdateSendGenericMusic { get; set; }
			int UpdateLargeEvents { get; set; }
			bool UpdateSendBuddies { get; set; }
			string PicState { get; set; }
			int PicPhotoK { get; set; }
			int PicMiscK { get; set; }
			bool IsChatting { get; set; }
			DateTime LastBuddyChange { get; set; }
			bool NewsModerator { get; set; }
			int NewsPermissionLevel { get; set; }
			bool IsBetaTester { get; set; }
			int PlacesVisitCount { get; set; }
			int MusicTypesFavouriteCount { get; set; }
			int PhotosMeCount { get; set; }
			bool IsSkeleton { get; set; }
			bool NoInboxEmails { get; set; }
			int AbuseReportsPending { get; set; }
			int AbuseReportsUseful { get; set; }
			int AbuseReportsOverturned { get; set; }
			int AbuseAccusationsPending { get; set; }
			int AbuseAccusationsAbuse { get; set; }
			int AbuseAccusationsNoAbuse { get; set; }
			bool ModeratePhotos { get; set; }
			int ChatSessionId { get; set; }
			int AddedByGroupK { get; set; }
			int DonateIcon { get; set; }
			DateTime DonateExpire { get; set; }
			DateTime EmailDateTime { get; set; }
			string EmailIp { get; set; }
			bool EmailHold { get; set; }
			bool IsHtmlEditor { get; set; }
			bool IsGroupModerator { get; set; }
			bool IsSkeletonFromSignup { get; set; }
			int ExtraIcon { get; set; }
			DateTime ExtraExpire { get; set; }
			int SpottingsTotal { get; set; }
			int SpottingsMonth { get; set; }
			int SpottingsMonthRank { get; set; }
			bool IsPromoter { get; set; }
			int CampTickets { get; set; }
			bool HasTicket { get; set; }
			DateTime LastTicketEventDateTime { get; set; }
			Guid PasswordHash { get; set; }
			Guid PasswordSalt { get; set; }
			string PasswordResetEmailSecret { get; set; }
			bool LegalTermsUser1 { get; set; }
			bool LegalTermsPromoter1 { get; set; }
			bool IsSuperAdmin { get; set; }
			bool IsSalesPerson { get; set; }
			DateTime BuyableLockDateTime { get; set; }
			int SalesTeam { get; set; }
			Guid Guid { get; set; }
			bool LegalTermsUser2 { get; set; }
			bool LegalTermsPromoter2 { get; set; }
			DateTime LastPhotoUpload { get; set; }
			DateTime DateTimeLastUpdateEmail { get; set; }
			bool InvitedViaContactImporter { get; set; }
			bool IsTicketsRegistered { get; set; }
			bool ExDirectory { get; set; }
			bool IsEmailBroken { get; set; }
			DateTime? DateTimeLastChatMessage { get; set; }
			int? RolloverDonationIconK { get; set; }
			Guid? ChatPic { get; set; }
			int? ChatPicPhotoK { get; set; }
			string ChatPicState { get; set; }
			DateTime? DateTimeLastBuddyAlertsRoomRefresh { get; set; }
			Model.Entities.Usr.PhotoUsageEnum PhotoUsage { get; set; }
			long? FacebookUID { get; set; }
			bool FacebookConnected { get; set; }
			DateTime? FacebookConnectedDateTime { get; set; }
			bool FacebookPermissionEmail { get; set; }
			bool FacebookPermissionPublish { get; set; }
			bool FacebookPermissionEvent { get; set; }
			bool FacebookPermissionRsvp { get; set; }
			bool FacebookStoryAttendEvent { get; set; }
			bool FacebookStoryBuyTicket { get; set; }
			bool FacebookStoryUploadPhoto { get; set; }
			bool FacebookStorySpotted { get; set; }
			bool FacebookStoryPhotoFeatured { get; set; }
			bool FacebookStoryNewBuddy { get; set; }
			bool FacebookStoryPublishArticle { get; set; }
			bool FacebookStoryJoinGroup { get; set; }
			bool FacebookStoryFavourite { get; set; }
			bool FacebookStoryNewTopic { get; set; }
			bool FacebookStoryEventReview { get; set; }
			bool FacebookStoryPostNews { get; set; }
			bool FacebookStoryLaugh { get; set; }
			bool FacebookEventAdd { get; set; }
			bool FacebookEventAttend { get; set; }
			string FacebookEmail { get; set; }
			bool? IsDj { get; set; }
			bool? FacebookStory { get; set; }
			bool FacebookStory1 { get; set; }
			string FacebookAccessToken { get; set; }
			bool FacebookStoryFavouriteTopic { get; set; }
			bool? NeedsCaptcha { get; set; }
			bool? PassedCaptcha { get; set; }
			DateTime? BouncePeriodDateTime { get; set; }
			int? TotalEmailsSentInPeriod { get; set; }
			int? MatchedHardBounceInPeriod { get; set; }
			int? UnmatchedHardBounceInPeriod { get; set; }
			int? SoftBounceInPeriod { get; set; }
		}
		
 		public abstract partial class Usr_FacebookUid_Not_Null : IUsr_FacebookUid_Not_Null
		{
			public abstract long? FacebookUid { get; set; }
		}
		public partial interface IUsr_FacebookUid_Not_Null
		{
			long? FacebookUid { get; set; }
		}
		
 		public abstract partial class UsrDate : IUsrDate
		{
			public abstract int UsrK { get; set; }
			public abstract int DateUsrK { get; set; }
			public abstract Model.Entities.UsrDate.StatusEnum Status { get; set; }
			public abstract DateTime DateTime { get; set; }
			public abstract bool PreMatch { get; set; }
			public abstract Model.Entities.UsrDate.MatchEnum Match { get; set; }
			public abstract DateTime MatchDateTime { get; set; }
			public abstract int MatchThreadK { get; set; }
		}
		public partial interface IUsrDate
		{
			int UsrK { get; set; }
			int DateUsrK { get; set; }
			Model.Entities.UsrDate.StatusEnum Status { get; set; }
			DateTime DateTime { get; set; }
			bool PreMatch { get; set; }
			Model.Entities.UsrDate.MatchEnum Match { get; set; }
			DateTime MatchDateTime { get; set; }
			int MatchThreadK { get; set; }
		}
		
 		public abstract partial class UsrDonationIcon : IUsrDonationIcon
		{
			public abstract int K { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int DonationIconK { get; set; }
			public abstract DateTime BuyDateTime { get; set; }
			public abstract bool Enabled { get; set; }
		}
		public partial interface IUsrDonationIcon
		{
			int K { get; set; }
			int UsrK { get; set; }
			int DonationIconK { get; set; }
			DateTime BuyDateTime { get; set; }
			bool Enabled { get; set; }
		}
		
 		public abstract partial class UsrEventAttended : IUsrEventAttended
		{
			public abstract int UsrK { get; set; }
			public abstract int EventK { get; set; }
			public abstract bool SendUpdate { get; set; }
			public abstract bool Spotter { get; set; }
		}
		public partial interface IUsrEventAttended
		{
			int UsrK { get; set; }
			int EventK { get; set; }
			bool SendUpdate { get; set; }
			bool Spotter { get; set; }
		}
		
 		public abstract partial class UsrEventGuestlist : IUsrEventGuestlist
		{
			public abstract int UsrK { get; set; }
			public abstract int EventK { get; set; }
			public abstract DateTime DateTime { get; set; }
		}
		public partial interface IUsrEventGuestlist
		{
			int UsrK { get; set; }
			int EventK { get; set; }
			DateTime DateTime { get; set; }
		}
		
 		public abstract partial class UsrMusicTypeFavourite : IUsrMusicTypeFavourite
		{
			public abstract int UsrK { get; set; }
			public abstract int MusicTypeK { get; set; }
		}
		public partial interface IUsrMusicTypeFavourite
		{
			int UsrK { get; set; }
			int MusicTypeK { get; set; }
		}
		
 		public abstract partial class UsrPhotoFavourite : IUsrPhotoFavourite
		{
			public abstract int K { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int PhotoK { get; set; }
			public abstract DateTime? DateTimeAdded { get; set; }
		}
		public partial interface IUsrPhotoFavourite
		{
			int K { get; set; }
			int UsrK { get; set; }
			int PhotoK { get; set; }
			DateTime? DateTimeAdded { get; set; }
		}
		
 		public abstract partial class UsrPhotoMe : IUsrPhotoMe
		{
			public abstract int K { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int PhotoK { get; set; }
		}
		public partial interface IUsrPhotoMe
		{
			int K { get; set; }
			int UsrK { get; set; }
			int PhotoK { get; set; }
		}
		
 		public abstract partial class UsrPlaceVisit : IUsrPlaceVisit
		{
			public abstract int UsrK { get; set; }
			public abstract int PlaceK { get; set; }
		}
		public partial interface IUsrPlaceVisit
		{
			int UsrK { get; set; }
			int PlaceK { get; set; }
		}
		
 		public abstract partial class Venue : IVenue
		{
			public abstract int K { get; set; }
			public abstract string Name { get; set; }
			public abstract string DetailsHtml { get; set; }
			public abstract string Postcode { get; set; }
			public abstract int PlaceK { get; set; }
			public abstract string AdminNote { get; set; }
			public abstract Guid Pic { get; set; }
			public abstract string OverrideMapUrl { get; set; }
			public abstract int OwnerUsrK { get; set; }
			public abstract Guid PicNew { get; set; }
			public abstract int Capacity { get; set; }
			public abstract int TotalComments { get; set; }
			public abstract DateTime LastPost { get; set; }
			public abstract DateTime AverageCommentDateTime { get; set; }
			public abstract DateTime AddedDateTime { get; set; }
			public abstract bool NoPhotos { get; set; }
			public abstract string AdminEmail { get; set; }
			public abstract bool IsDescriptionText { get; set; }
			public abstract bool IsNew { get; set; }
			public abstract bool NoPrints { get; set; }
			public abstract bool IsDescriptionCleanHtml { get; set; }
			public abstract bool IsEdited { get; set; }
			public abstract Guid DuplicateGuid { get; set; }
			public abstract bool RegularEvents { get; set; }
			public abstract string UrlName { get; set; }
			public abstract int PromoterK { get; set; }
			public abstract string PicState { get; set; }
			public abstract int PicPhotoK { get; set; }
			public abstract int PicMiscK { get; set; }
			public abstract string UrlFragment { get; set; }
			public abstract int ModeratorUsrK { get; set; }
			public abstract int TotalEvents { get; set; }
			public abstract Model.Entities.Venue.PromoterStatusEnum PromoterStatus { get; set; }
			public abstract bool DetailsPlain { get; set; }
			public abstract string StyledCss { get; set; }
			public abstract string StyledXml { get; set; }
		}
		public partial interface IVenue
		{
			int K { get; set; }
			string Name { get; set; }
			string DetailsHtml { get; set; }
			string Postcode { get; set; }
			int PlaceK { get; set; }
			string AdminNote { get; set; }
			Guid Pic { get; set; }
			string OverrideMapUrl { get; set; }
			int OwnerUsrK { get; set; }
			Guid PicNew { get; set; }
			int Capacity { get; set; }
			int TotalComments { get; set; }
			DateTime LastPost { get; set; }
			DateTime AverageCommentDateTime { get; set; }
			DateTime AddedDateTime { get; set; }
			bool NoPhotos { get; set; }
			string AdminEmail { get; set; }
			bool IsDescriptionText { get; set; }
			bool IsNew { get; set; }
			bool NoPrints { get; set; }
			bool IsDescriptionCleanHtml { get; set; }
			bool IsEdited { get; set; }
			Guid DuplicateGuid { get; set; }
			bool RegularEvents { get; set; }
			string UrlName { get; set; }
			int PromoterK { get; set; }
			string PicState { get; set; }
			int PicPhotoK { get; set; }
			int PicMiscK { get; set; }
			string UrlFragment { get; set; }
			int ModeratorUsrK { get; set; }
			int TotalEvents { get; set; }
			Model.Entities.Venue.PromoterStatusEnum PromoterStatus { get; set; }
			bool DetailsPlain { get; set; }
			string StyledCss { get; set; }
			string StyledXml { get; set; }
		}
		
 		public abstract partial class Visit : IVisit
		{
			public abstract int K { get; set; }
			public abstract Guid Guid { get; set; }
			public abstract int UsrK { get; set; }
			public abstract int Pages { get; set; }
			public abstract int Comments { get; set; }
			public abstract int ChatMessages { get; set; }
			public abstract int Hits { get; set; }
			public abstract int Photos { get; set; }
			public abstract int TopBannerClicks { get; set; }
			public abstract int HotboxClicks { get; set; }
			public abstract int PhotoBannerClicks { get; set; }
			public abstract DateTime DateTimeStart { get; set; }
			public abstract DateTime DateTimeLast { get; set; }
			public abstract string IpAddress { get; set; }
			public abstract bool IsNewGuid { get; set; }
			public abstract int CountryK { get; set; }
			public abstract bool IsFromExternal { get; set; }
			public abstract string ExternalTag { get; set; }
			public abstract int DomainK { get; set; }
			public abstract bool IsCrawler { get; set; }
			public abstract string UserAgent { get; set; }
		}
		public partial interface IVisit
		{
			int K { get; set; }
			Guid Guid { get; set; }
			int UsrK { get; set; }
			int Pages { get; set; }
			int Comments { get; set; }
			int ChatMessages { get; set; }
			int Hits { get; set; }
			int Photos { get; set; }
			int TopBannerClicks { get; set; }
			int HotboxClicks { get; set; }
			int PhotoBannerClicks { get; set; }
			DateTime DateTimeStart { get; set; }
			DateTime DateTimeLast { get; set; }
			string IpAddress { get; set; }
			bool IsNewGuid { get; set; }
			int CountryK { get; set; }
			bool IsFromExternal { get; set; }
			string ExternalTag { get; set; }
			int DomainK { get; set; }
			bool IsCrawler { get; set; }
			string UserAgent { get; set; }
		}
		
 		public abstract partial class VisitView : IVisitView
		{
			public abstract int K { get; set; }
			public abstract string Guid { get; set; }
			public abstract int? UsrK { get; set; }
			public abstract int? Pages { get; set; }
			public abstract int? Hits { get; set; }
			public abstract int? Photos { get; set; }
			public abstract int? TopBannerClicks { get; set; }
			public abstract int? HotboxClicks { get; set; }
			public abstract int? PhotoBannerClicks { get; set; }
			public abstract DateTime? DateTimeStart { get; set; }
			public abstract DateTime? DateTimeLast { get; set; }
			public abstract string IpAddress { get; set; }
			public abstract int? Duration { get; set; }
			public abstract int? Comments { get; set; }
			public abstract int? ChatMessages { get; set; }
			public abstract bool? IsNewGuid { get; set; }
		}
		public partial interface IVisitView
		{
			int K { get; set; }
			string Guid { get; set; }
			int? UsrK { get; set; }
			int? Pages { get; set; }
			int? Hits { get; set; }
			int? Photos { get; set; }
			int? TopBannerClicks { get; set; }
			int? HotboxClicks { get; set; }
			int? PhotoBannerClicks { get; set; }
			DateTime? DateTimeStart { get; set; }
			DateTime? DateTimeLast { get; set; }
			string IpAddress { get; set; }
			int? Duration { get; set; }
			int? Comments { get; set; }
			int? ChatMessages { get; set; }
			bool? IsNewGuid { get; set; }
		}
		
 		public abstract partial class Vw_index_list : IVw_index_list
		{
			public abstract string TableName { get; set; }
			public abstract string IndexName { get; set; }
			public abstract string Col1 { get; set; }
			public abstract string Col2 { get; set; }
			public abstract string Col3 { get; set; }
			public abstract string Col4 { get; set; }
			public abstract string Col5 { get; set; }
			public abstract string Col6 { get; set; }
			public abstract string Col7 { get; set; }
			public abstract string Col8 { get; set; }
			public abstract string Col9 { get; set; }
			public abstract string Col10 { get; set; }
			public abstract string Col11 { get; set; }
			public abstract string Col12 { get; set; }
			public abstract string Col13 { get; set; }
			public abstract string Col14 { get; set; }
			public abstract string Col15 { get; set; }
			public abstract string Col16 { get; set; }
			public abstract int? Dpages { get; set; }
			public abstract int? Used { get; set; }
			public abstract long? Rowcnt { get; set; }
			public abstract int Tableid { get; set; }
		}
		public partial interface IVw_index_list
		{
			string TableName { get; set; }
			string IndexName { get; set; }
			string Col1 { get; set; }
			string Col2 { get; set; }
			string Col3 { get; set; }
			string Col4 { get; set; }
			string Col5 { get; set; }
			string Col6 { get; set; }
			string Col7 { get; set; }
			string Col8 { get; set; }
			string Col9 { get; set; }
			string Col10 { get; set; }
			string Col11 { get; set; }
			string Col12 { get; set; }
			string Col13 { get; set; }
			string Col14 { get; set; }
			string Col15 { get; set; }
			string Col16 { get; set; }
			int? Dpages { get; set; }
			int? Used { get; set; }
			long? Rowcnt { get; set; }
			int Tableid { get; set; }
		}
		
}
