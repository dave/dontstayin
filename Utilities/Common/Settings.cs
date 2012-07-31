using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace Common
{
	public static class Settings
	{

		#region Definitions

		internal const string MixmagGreatestPollThanksPageHtmlSettingName = "MixmagGreatestPollThanksPageHtml"; public static string MixmagGreatestPollThanksPageHtml { get { return (string)Get(MixmagGreatestPollThanksPageHtmlSettingName, "<h1>Thanks for your vote!</h1>"); } set { Set(MixmagGreatestPollThanksPageHtmlSettingName, value); } }

		public static bool DynamicUrlFragments { get { return DynamicUrlFragmentsStatus == DynamicUrlFragmentsStatusOption.On; } }
		public enum DynamicUrlFragmentsStatusOption { Off = 0, On = 1 } internal const string DynamicUrlFragmentsStatusSettingName = "DynamicUrlFragmentsStatus"; public static DynamicUrlFragmentsStatusOption DynamicUrlFragmentsStatus { get { return (DynamicUrlFragmentsStatusOption)Get(DynamicUrlFragmentsStatusSettingName, DynamicUrlFragmentsStatusOption.Off); } set { Set(DynamicUrlFragmentsStatusSettingName, value); } }

		public enum CaptchaEnabledStatusOption { Off = 0, On = 1 } internal const string CaptchaEnabledStatusSettingName = "CaptchaEnabledStatus"; public static CaptchaEnabledStatusOption CaptchaEnabledStatus { get { return (CaptchaEnabledStatusOption)Get(CaptchaEnabledStatusSettingName, CaptchaEnabledStatusOption.Off); } set { Set(CaptchaEnabledStatusSettingName, value); } }

		internal const string WootBigLeaderboardUkPercentageSettingName = "WootBigLeaderboardUkPercentage"; public static int WootBigLeaderboardUkPercentage { get { return (int)Get(WootBigLeaderboardUkPercentageSettingName, 0); } set { Set(WootBigLeaderboardUkPercentageSettingName, value); } }
		internal const string WootBigLeaderboardUkFrequencyCapSettingName = "WootBigLeaderboardUkFrequencyCap"; public static int WootBigLeaderboardUkFrequencyCap { get { return (int)Get(WootBigLeaderboardUkFrequencyCapSettingName, 0); } set { Set(WootBigLeaderboardUkFrequencyCapSettingName, value); } }
		internal const string WootBigLeaderboardTagHtmlSettingName = "WootBigLeaderboardTagHtml"; public static string WootBigLeaderboardTagHtml { get { return (string)Get(WootBigLeaderboardTagHtmlSettingName, "TEST"); } set { Set(WootBigLeaderboardTagHtmlSettingName, value); } }
		public enum WootBigLeaderboardFrontPageRoadblockStatusOption { Off = 0, On = 1 } internal const string WootBigLeaderboardFrontPageRoadblockStatusSettingName = "WootBigLeaderboardFrontPageRoadblockStatus"; public static WootBigLeaderboardFrontPageRoadblockStatusOption WootBigLeaderboardFrontPageRoadblockStatus { get { return (WootBigLeaderboardFrontPageRoadblockStatusOption)Get(WootBigLeaderboardFrontPageRoadblockStatusSettingName, WootBigLeaderboardFrontPageRoadblockStatusOption.Off); } set { Set(WootBigLeaderboardFrontPageRoadblockStatusSettingName, value); } }

		internal const string UsaLeaderboardPercentageSettingName = "UsaLeaderboardPercentage"; public static int UsaLeaderboardPercentage { get { return (int)Get(UsaLeaderboardPercentageSettingName, 0); } set { Set(UsaLeaderboardPercentageSettingName, value); } }
		internal const string UsaMpuPercentageSettingName = "UsaMpuPercentage"; public static int UsaMpuPercentage { get { return (int)Get(UsaMpuPercentageSettingName, 0); } set { Set(UsaMpuPercentageSettingName, value); } }
		internal const string UsaMainLeaderboardBannerKSettingName = "UsaMainLeaderboardBannerK"; public static int UsaMainLeaderboardBannerK { get { return (int)Get(UsaMainLeaderboardBannerKSettingName, 14806); } set { Set(UsaMainLeaderboardBannerKSettingName, value); } }
		internal const string UsaMainMpuBannerKSettingName = "UsaMainMpuBannerK"; public static int UsaMainMpuBannerK { get { return (int)Get(UsaMainMpuBannerKSettingName, 14805); } set { Set(UsaMainMpuBannerKSettingName, value); } }


		internal const string TallMpuTagHtmlSettingName = "TallMpuTagHtml"; public static string TallMpuTagHtml { get { return (string)Get(TallMpuTagHtmlSettingName, "<div style='width:300px; height:600px; background-color:#ff0000;'>Tall MPU</div>"); } set { Set(TallMpuTagHtmlSettingName, value); } }
		internal const string TallMpuPercentageSettingName = "TallMpuPercentage"; public static int TallMpuPercentage { get { return (int)Get(TallMpuPercentageSettingName, 0); } set { Set(TallMpuPercentageSettingName, value); } }

		public enum WootLinkedBannerTypeOption { Leaderboard = 0, BigLeaderboard = 1 } internal const string WootLinkedBannerTypeSettingName = "WootLinkedBannerType"; public static WootLinkedBannerTypeOption WootLinkedBannerType { get { return (WootLinkedBannerTypeOption)Get(WootLinkedBannerTypeSettingName, WootLinkedBannerTypeOption.Leaderboard); } set { Set(WootLinkedBannerTypeSettingName, value); } }
		internal const string WootUkLinkedPercentageSettingName = "WootUkLinkedPercentage"; public static int WootUkLinkedPercentage { get { return (int)Get(WootUkLinkedPercentageSettingName, 0); } set { Set(WootUkLinkedPercentageSettingName, value); } }
		internal const string WootUkLeaderboardPercentageSettingName = "WootUkLeaderboardPercentage"; public static int WootUkLeaderboardPercentage { get { return (int)Get(WootUkLeaderboardPercentageSettingName, 0); } set { Set(WootUkLeaderboardPercentageSettingName, value); } }
		internal const string WootUkMpuPercentageSettingName = "WootUkMpuPercentage"; public static int WootUkMpuPercentage { get { return (int)Get(WootUkMpuPercentageSettingName, 0); } set { Set(WootUkMpuPercentageSettingName, value); } }

		internal const string WootUkLinkedBigLeaderboardTagHtmlSettingName = "WootUkLinkedBigLeaderboardTagHtml"; public static string WootUkLinkedBigLeaderboardTagHtml { get { return (string)Get(WootUkLinkedBigLeaderboardTagHtmlSettingName, "TEST"); } set { Set(WootUkLinkedBigLeaderboardTagHtmlSettingName, value); } }
		internal const string WootUkLinkedLeaderboardBannerKSettingName = "WootUkLinkedLeaderboardBannerK"; public static int WootUkLinkedLeaderboardBannerK { get { return (int)Get(WootUkLinkedLeaderboardBannerKSettingName, 14693); } set { Set(WootUkLinkedLeaderboardBannerKSettingName, value); } }
		internal const string WootUkLinkedMpuBannerKSettingName = "WootUkLinkedMpuBannerK"; public static int WootUkLinkedMpuBannerK { get { return (int)Get(WootUkLinkedMpuBannerKSettingName, 14692); } set { Set(WootUkLinkedMpuBannerKSettingName, value); } }
		internal const string WootUkMainLeaderboardBannerKSettingName = "WootUkMainLeaderboardBannerK"; public static int WootUkMainLeaderboardBannerK { get { return (int)Get(WootUkMainLeaderboardBannerKSettingName, 14691); } set { Set(WootUkMainLeaderboardBannerKSettingName, value); } }
		internal const string WootUkMainMpuBannerKSettingName = "WootUkMainMpuBannerK"; public static int WootUkMainMpuBannerK { get { return (int)Get(WootUkMainMpuBannerKSettingName, 14690); } set { Set(WootUkMainMpuBannerKSettingName, value); } }


		public enum RoadBlockOnGroupPageStatusOption { Off = 0, On = 1 } internal const string RoadBlockOnGroupPageStatusSettingName = "RoadBlockOnGroupPageStatus"; public static RoadBlockOnGroupPageStatusOption RoadBlockOnGroupPageStatus { get { return (RoadBlockOnGroupPageStatusOption)Get(RoadBlockOnGroupPageStatusSettingName, RoadBlockOnGroupPageStatusOption.Off); } set { Set(RoadBlockOnGroupPageStatusSettingName, value); } }
		internal const string RoadblockOnGroupKSettingName = "RoadblockOnGroupK"; public static int RoadblockOnGroupK { get { return (int)Get(RoadblockOnGroupKSettingName, 100); } set { Set(RoadblockOnGroupKSettingName, value); } }
		internal const string RoadblockOnGroupHotboxBannerKSettingName = "RoadblockOnGroupHotboxBannerK"; public static int RoadblockOnGroupHotboxBannerK { get { return (int)Get(RoadblockOnGroupHotboxBannerKSettingName, 0); } set { Set(RoadblockOnGroupHotboxBannerKSettingName, value); } }
		internal const string RoadblockOnGroupLeaderboardBannerKSettingName = "RoadblockOnGroupLeaderboardBannerK"; public static int RoadblockOnGroupLeaderboardBannerK { get { return (int)Get(RoadblockOnGroupLeaderboardBannerKSettingName, 0); } set { Set(RoadblockOnGroupLeaderboardBannerKSettingName, value); } }
		internal const string RoadblockOnGroupSkyscraperBannerKSettingName = "RoadblockOnGroupSkyscraperBannerK"; public static int RoadblockOnGroupSkyscraperBannerK { get { return (int)Get(RoadblockOnGroupSkyscraperBannerKSettingName, 0); } set { Set(RoadblockOnGroupSkyscraperBannerKSettingName, value); } }

		#region CompetitionGroupK
		internal const string CompetitionGroupKSettingName = "CompetitionGroupK";
		public static int CompetitionGroupK
		{
			get { return (int)Get(CompetitionGroupKSettingName, 36206); }
			set { Set(CompetitionGroupKSettingName, value); }
		}
		#endregion

		public enum CompetitionEnabledOption { Off = 0, On = 1 } internal const string CompetitionEnabledSettingName = "CompetitionEnabled"; public static CompetitionEnabledOption CompetitionEnabled { get { return (CompetitionEnabledOption)Get(CompetitionEnabledSettingName, CompetitionEnabledOption.Off); } set { Set(CompetitionEnabledSettingName, value); } }
		internal const string CompetitionHtml1HtmlSettingName = "CompetitionHtml1Html"; public static string CompetitionHtml1Html { get { return (string)Get(CompetitionHtml1HtmlSettingName, "TEST"); } set { Set(CompetitionHtml1HtmlSettingName, value); } }
		internal const string CompetitionHtml2HtmlSettingName = "CompetitionHtml2Html"; public static string CompetitionHtml2Html { get { return (string)Get(CompetitionHtml2HtmlSettingName, ""); } set { Set(CompetitionHtml2HtmlSettingName, value); } }
		internal const string CompetitionHtml3HtmlSettingName = "CompetitionHtml3Html"; public static string CompetitionHtml3Html { get { return (string)Get(CompetitionHtml3HtmlSettingName, ""); } set { Set(CompetitionHtml3HtmlSettingName, value); } }

		internal const string CompetitionTopPhotosHeaderSettingName = "CompetitionTopPhotosHeader"; public static string CompetitionTopPhotosHeader { get { return (string)Get(CompetitionTopPhotosHeaderSettingName, "TEST"); } set { Set(CompetitionTopPhotosHeaderSettingName, value); } }
		internal const string CompetitionAllPhotosHeaderSettingName = "CompetitionAllPhotosHeader"; public static string CompetitionAllPhotosHeader { get { return (string)Get(CompetitionAllPhotosHeaderSettingName, "TEST"); } set { Set(CompetitionAllPhotosHeaderSettingName, value); } }

		internal const string CompetitionButtonAddUrlSettingName = "CompetitionButtonAddUrl"; public static string CompetitionButtonAddUrl { get { return (string)Get(CompetitionButtonAddUrlSettingName, "http://pix-cdn.dontstayin.com/ebe08df9-85a7-4847-acfc-097c5313e271.jpg"); } set { Set(CompetitionButtonAddUrlSettingName, value); } }
		internal const string CompetitionButtonRemoveUrlSettingName = "CompetitionButtonRemoveUrl"; public static string CompetitionButtonRemoveUrl { get { return (string)Get(CompetitionButtonRemoveUrlSettingName, "http://pix-cdn.dontstayin.com/2061f564-d566-41a8-9fc3-3483b22518ca.jpg"); } set { Set(CompetitionButtonRemoveUrlSettingName, value); } }

		internal const string CompetitionButtonWidthSettingName = "CompetitionButtonWidth"; public static int CompetitionButtonWidth { get { return (int)Get(CompetitionButtonWidthSettingName, 100); } set { Set(CompetitionButtonWidthSettingName, value); } }
		internal const string CompetitionButtonHeightSettingName = "CompetitionButtonHeight"; public static int CompetitionButtonHeight { get { return (int)Get(CompetitionButtonHeightSettingName, 20); } set { Set(CompetitionButtonHeightSettingName, value); } }


		internal const string TalkToFrankHtmlTestSettingName = "TalkToFrankHtmlTest"; public static string TalkToFrankHtmlTest { get { return (string)Get(TalkToFrankHtmlTestSettingName, "hello test"); } set { Set(TalkToFrankHtmlTestSettingName, value); } }
		internal const string TalkToFrankHtmlSettingName = "TalkToFrankHtml"; public static string TalkToFrankHtml { get { return (string)Get(TalkToFrankHtmlSettingName, "hello"); } set { Set(TalkToFrankHtmlSettingName, value); } }
		

		#region TakeoverStatus
		public enum TakeoverStatusOption { Off = 0, On = 1, Test = 2 } internal const string TakeoverStatusSettingName = "TakeoverStatus"; public static TakeoverStatusOption TakeoverStatus { get { return (TakeoverStatusOption)Get(TakeoverStatusSettingName, TakeoverStatusOption.Off); } set { Set(TakeoverStatusSettingName, value); } }

		public enum TakeoverBackgroundStatusOption { Off = 0, On = 1 } internal const string TakeoverBackgroundStatusSettingName = "TakeoverBackgroundStatus"; public static TakeoverBackgroundStatusOption TakeoverBackgroundStatus { get { return (TakeoverBackgroundStatusOption)Get(TakeoverBackgroundStatusSettingName, TakeoverBackgroundStatusOption.Off); } set { Set(TakeoverBackgroundStatusSettingName, value); } }
		public enum TakeoverRequiresDrinkingAgeVerificationStatusOption { Off = 0, On = 1 } internal const string TakeoverRequiresDrinkingAgeVerificationStatusSettingName = "TakeoverRequiresDrinkingAgeVerificationStatus"; public static TakeoverRequiresDrinkingAgeVerificationStatusOption TakeoverRequiresDrinkingAgeVerificationStatus { get { return (TakeoverRequiresDrinkingAgeVerificationStatusOption)Get(TakeoverRequiresDrinkingAgeVerificationStatusSettingName, TakeoverRequiresDrinkingAgeVerificationStatusOption.Off); } set { Set(TakeoverRequiresDrinkingAgeVerificationStatusSettingName, value); } }
		internal const string TakeoverBackgroundColourSettingName = "TakeoverBackgroundColour"; public static string TakeoverBackgroundColour { get { return (string)Get(TakeoverBackgroundColourSettingName, "ffffff"); } set { Set(TakeoverBackgroundColourSettingName, value); } }
		internal const string TakeoverBackgroundImageSettingName = "TakeoverBackgroundImage"; public static string TakeoverBackgroundImage { get { return (string)Get(TakeoverBackgroundImageSettingName, "http://pix-eu.dontstayin.com/162a3df3-745a-4a24-9938-091f6a52f529.jpg"); } set { Set(TakeoverBackgroundImageSettingName, value); } }
		internal const string TakeoverBackgroundAttachmentSettingName = "TakeoverBackgroundAttachment"; public static string TakeoverBackgroundAttachment { get { return (string)Get(TakeoverBackgroundAttachmentSettingName, "fixed"); } set { Set(TakeoverBackgroundAttachmentSettingName, value); } }
		internal const string TakeoverBackgroundPositionSettingName = "TakeoverBackgroundPosition"; public static string TakeoverBackgroundPosition { get { return (string)Get(TakeoverBackgroundPositionSettingName, "top right"); } set { Set(TakeoverBackgroundPositionSettingName, value); } }
		internal const string TakeoverBackgroundRepeatSettingName = "TakeoverBackgroundRepeat"; public static string TakeoverBackgroundRepeat { get { return (string)Get(TakeoverBackgroundRepeatSettingName, "repeat-x"); } set { Set(TakeoverBackgroundRepeatSettingName, value); } }

		public enum TakeoverLinksStatusOption { Off = 0, On = 1 } internal const string TakeoverLinksStatusSettingName = "TakeoverLinksStatus"; public static TakeoverLinksStatusOption TakeoverLinksStatus { get { return (TakeoverLinksStatusOption)Get(TakeoverLinksStatusSettingName, TakeoverLinksStatusOption.Off); } set { Set(TakeoverLinksStatusSettingName, value); } }
		internal const string TakeoverLinksHtmlSettingName = "TakeoverLinksHtml"; public static string TakeoverLinksHtml { get { return (string)Get(TakeoverLinksHtmlSettingName, "Links in here"); } set { Set(TakeoverLinksHtmlSettingName, value); } }
		internal const string TakeoverLinksWidthSettingName = "TakeoverLinksWidth"; public static int TakeoverLinksWidth { get { return (int)Get(TakeoverLinksWidthSettingName, 100); } set { Set(TakeoverLinksWidthSettingName, value); } }
		internal const string TakeoverLinksHeightSettingName = "TakeoverLinksHeight"; public static int TakeoverLinksHeight { get { return (int)Get(TakeoverLinksHeightSettingName, 100); } set { Set(TakeoverLinksHeightSettingName, value); } }

		public enum TakeoverFooterRightStatusOption { Off = 0, On = 1 } internal const string TakeoverFooterRightStatusSettingName = "TakeoverFooterRightStatus"; public static TakeoverFooterRightStatusOption TakeoverFooterRightStatus { get { return (TakeoverFooterRightStatusOption)Get(TakeoverFooterRightStatusSettingName, TakeoverFooterRightStatusOption.Off); } set { Set(TakeoverFooterRightStatusSettingName, value); } }
		internal const string TakeoverFooterRightHtmlSettingName = "TakeoverFooterRightHtml"; public static string TakeoverFooterRightHtml { get { return (string)Get(TakeoverFooterRightHtmlSettingName, "Right footer in here"); } set { Set(TakeoverFooterRightHtmlSettingName, value); } }
		internal const string TakeoverFooterRightWidthSettingName = "TakeoverFooterRightWidth"; public static int TakeoverFooterRightWidth { get { return (int)Get(TakeoverFooterRightWidthSettingName, 100); } set { Set(TakeoverFooterRightWidthSettingName, value); } }
		internal const string TakeoverFooterRightHeightSettingName = "TakeoverFooterRightHeight"; public static int TakeoverFooterRightHeight { get { return (int)Get(TakeoverFooterRightHeightSettingName, 100); } set { Set(TakeoverFooterRightHeightSettingName, value); } }

		public enum TakeoverFooterLeftStatusOption { Off = 0, On = 1 } internal const string TakeoverFooterLeftStatusSettingName = "TakeoverFooterLeftStatus"; public static TakeoverFooterLeftStatusOption TakeoverFooterLeftStatus { get { return (TakeoverFooterLeftStatusOption)Get(TakeoverFooterLeftStatusSettingName, TakeoverFooterLeftStatusOption.Off); } set { Set(TakeoverFooterLeftStatusSettingName, value); } }
		internal const string TakeoverFooterLeftHtmlSettingName = "TakeoverFooterLeftHtml"; public static string TakeoverFooterLeftHtml { get { return (string)Get(TakeoverFooterLeftHtmlSettingName, "Left footer in here"); } set { Set(TakeoverFooterLeftHtmlSettingName, value); } }
		internal const string TakeoverFooterLeftWidthSettingName = "TakeoverFooterLeftWidth"; public static int TakeoverFooterLeftWidth { get { return (int)Get(TakeoverFooterLeftWidthSettingName, 100); } set { Set(TakeoverFooterLeftWidthSettingName, value); } }
		internal const string TakeoverFooterLeftHeightSettingName = "TakeoverFooterLeftHeight"; public static int TakeoverFooterLeftHeight { get { return (int)Get(TakeoverFooterLeftHeightSettingName, 100); } set { Set(TakeoverFooterLeftHeightSettingName, value); } }
		

		public enum TakeoverPhotoOverlayStatusOption { Off = 0, On = 1 } internal const string TakeoverPhotoOverlayStatusSettingName = "TakeoverPhotoOverlayStatus"; public static TakeoverPhotoOverlayStatusOption TakeoverPhotoOverlayStatus { get { return (TakeoverPhotoOverlayStatusOption)Get(TakeoverPhotoOverlayStatusSettingName, TakeoverPhotoOverlayStatusOption.Off); } set { Set(TakeoverPhotoOverlayStatusSettingName, value); } }
		internal const string TakeoverPhotoOverlayWidthSettingName = "TakeoverPhotoOverlayWidth"; public static int TakeoverPhotoOverlayWidth { get { return (int)Get(TakeoverPhotoOverlayWidthSettingName, 100); } set { Set(TakeoverPhotoOverlayWidthSettingName, value); } }
		internal const string TakeoverPhotoOverlayHeightSettingName = "TakeoverPhotoOverlayHeight"; public static int TakeoverPhotoOverlayHeight { get { return (int)Get(TakeoverPhotoOverlayHeightSettingName, 100); } set { Set(TakeoverPhotoOverlayHeightSettingName, value); } }
		internal const string TakeoverPhotoOverlayHtmlSettingName = "TakeoverPhotoOverlayHtml"; public static string TakeoverPhotoOverlayHtml { get { return (string)Get(TakeoverPhotoOverlayHtmlSettingName, "Footer in here"); } set { Set(TakeoverPhotoOverlayHtmlSettingName, value); } }

		public enum TakeoverLogoStatusOption { Off = 0, On = 1 } internal const string TakeoverLogoStatusSettingName = "TakeoverLogoStatus"; public static TakeoverLogoStatusOption TakeoverLogoStatus { get { return (TakeoverLogoStatusOption)Get(TakeoverLogoStatusSettingName, TakeoverLogoStatusOption.Off); } set { Set(TakeoverLogoStatusSettingName, value); } }
		internal const string TakeoverLogoHtmlSettingName = "TakeoverLogoHtml"; public static string TakeoverLogoHtml { get { return (string)Get(TakeoverLogoHtmlSettingName, "Logo in here"); } set { Set(TakeoverLogoHtmlSettingName, value); } }
		#endregion

		#region FrontPageRoadblock
		public enum FrontPageRoadblock1StatusOption { Off = 0, On = 1 } internal const string FrontPageRoadblock1StatusSettingName = "FrontPageRoadblock1Status"; public static FrontPageRoadblock1StatusOption FrontPageRoadblock1Status { get { return (FrontPageRoadblock1StatusOption)Get(FrontPageRoadblock1StatusSettingName, FrontPageRoadblock1StatusOption.Off); } set { Set(FrontPageRoadblock1StatusSettingName, value); } }
		internal const string FrontPageRoadblock1WeightSettingName = "FrontPageRoadblock1Weight"; public static int FrontPageRoadblock1Weight { get { return (int)Get(FrontPageRoadblock1WeightSettingName, 1); } set { Set(FrontPageRoadblock1WeightSettingName, value); } }
		internal const string FrontPageRoadblock1HtmlSettingName = "FrontPageRoadblock1Html"; public static string FrontPageRoadblock1Html { get { return (string)Get(FrontPageRoadblock1HtmlSettingName, "Roadblock 1"); } set { Set(FrontPageRoadblock1HtmlSettingName, value); } }
		
		public enum FrontPageRoadblock2StatusOption { Off = 0, On = 1 } internal const string FrontPageRoadblock2StatusSettingName = "FrontPageRoadblock2Status"; public static FrontPageRoadblock2StatusOption FrontPageRoadblock2Status { get { return (FrontPageRoadblock2StatusOption)Get(FrontPageRoadblock2StatusSettingName, FrontPageRoadblock2StatusOption.Off); } set { Set(FrontPageRoadblock2StatusSettingName, value); } }
		internal const string FrontPageRoadblock2WeightSettingName = "FrontPageRoadblock2Weight"; public static int FrontPageRoadblock2Weight { get { return (int)Get(FrontPageRoadblock2WeightSettingName, 1); } set { Set(FrontPageRoadblock2WeightSettingName, value); } }
		internal const string FrontPageRoadblock2HtmlSettingName = "FrontPageRoadblock2Html"; public static string FrontPageRoadblock2Html { get { return (string)Get(FrontPageRoadblock2HtmlSettingName, "Roadblock 2"); } set { Set(FrontPageRoadblock2HtmlSettingName, value); } }
		
		public enum FrontPageRoadblock3StatusOption { Off = 0, On = 1 } internal const string FrontPageRoadblock3StatusSettingName = "FrontPageRoadblock3Status"; public static FrontPageRoadblock3StatusOption FrontPageRoadblock3Status { get { return (FrontPageRoadblock3StatusOption)Get(FrontPageRoadblock3StatusSettingName, FrontPageRoadblock3StatusOption.Off); } set { Set(FrontPageRoadblock3StatusSettingName, value); } }
		internal const string FrontPageRoadblock3WeightSettingName = "FrontPageRoadblock3Weight"; public static int FrontPageRoadblock3Weight { get { return (int)Get(FrontPageRoadblock3WeightSettingName, 1); } set { Set(FrontPageRoadblock3WeightSettingName, value); } }
		internal const string FrontPageRoadblock3HtmlSettingName = "FrontPageRoadblock3Html"; public static string FrontPageRoadblock3Html { get { return (string)Get(FrontPageRoadblock3HtmlSettingName, "Roadblock 3"); } set { Set(FrontPageRoadblock3HtmlSettingName, value); } }
		#endregion

		internal const string OnlineTimeoutMinutesSettingName = "OnlineTimeoutMinutes"; public static int OnlineTimeoutMinutes { get { return (int)Get(OnlineTimeoutMinutesSettingName, 30); } set { Set(OnlineTimeoutMinutesSettingName, value); } }
	
		#region EnablePremiumRate
		public enum EnablePremiumRateOption
		{
			Off = 0,
			On = 1
		}
		internal const string EnablePremiumRateSettingName = "EnablePremiumRate for front page photos?";
		public static EnablePremiumRateOption EnablePremiumRate
		{
			get { return (EnablePremiumRateOption)Get(EnablePremiumRateSettingName, EnablePremiumRateOption.On); }
			set { Set(EnablePremiumRateSettingName, value); }
		}
		#endregion

		#region ShadowNapHtml
		internal const string ShadowNapHtmlSettingName = "Shaddow Nap Html";
		public static string ShadowNapHtml
		{
			get { return (string)Get(ShadowNapHtmlSettingName, "<h1>Shaddow Napping</h1><div class=\"ContentBorder\"><p>Hello...</p></div>"); }
			set
			{
				Set(ShadowNapHtmlSettingName, value);
			}
		}
		#endregion

		public enum AboveChatStatusOption { Off = 0, On = 1, Test = 2 } internal const string AboveChatStatusSettingName = "AboveChatStatus"; public static AboveChatStatusOption AboveChatStatus { get { return (AboveChatStatusOption)Get(AboveChatStatusSettingName, AboveChatStatusOption.Off); } set { Set(AboveChatStatusSettingName, value); } }
		#region AboveChatBoxHtml
		internal const string AboveChatBoxHtmlSettingName = "AboveChatBoxHtml";
		public static string AboveChatBoxHtml
		{
			get { return (string)Get(AboveChatBoxHtmlSettingName, ""); }
			set
			{
				Set(AboveChatBoxHtmlSettingName, value);
			}
		}
		#endregion

		#region BottomVideoBox
		public enum BottomVideoBoxOption
		{
			Off = 0,
			On = 1
		}
		internal const string BottomVideoBoxSettingName = "BottomVideoBox";
		public static BottomVideoBoxOption BottomVideoBox
		{
			get { return (BottomVideoBoxOption)Get(BottomVideoBoxSettingName, BottomVideoBoxOption.On); }
			set { Set(BottomVideoBoxSettingName, value); }
		}
		#endregion

		#region PercentageOfBottomVideoBoxToUnruly
		internal const string PercentageOfBottomVideoBoxToUnrulySettingName = "PercentageOfBottomVideoBoxToUnruly";
		public static int PercentageOfBottomVideoBoxToUnruly
		{
			get { return (int)Get(PercentageOfBottomVideoBoxToUnrulySettingName, 100); }
			set { Set(PercentageOfBottomVideoBoxToUnrulySettingName, value); }
		}
		#endregion

		#region NetworkN3Html
		internal const string NetworkN3HtmlSettingName = "NetworkN3Html";
		public static string NetworkN3Html
		{
			get { return (string)Get(NetworkN3HtmlSettingName, @"<h1>Bored? Watch this!</h1><div style=""border:1px solid #000000;background-color:#FFFFFF;padding-left:90px;""><script type=""text/javascript"" language=""JavaScript"">var sid = 444; var pid = 100; var spo = 1; </script><script type=""text/javascript"" language=""JavaScript"" src=""http://www.networkn3.com/scripts/vplay4-start-paused.js""></script></div>"); }
			set
			{
				Set(NetworkN3HtmlSettingName, value);
			}
		}
		#endregion

		#region FrontPageBannerHtmlHtml
		internal const string FrontPageBannerHtmlHtmlSettingName = "FrontPageBannerHtmlHtml";
		public static string FrontPageBannerHtmlHtml
		{
			get { return (string)Get(FrontPageBannerHtmlHtmlSettingName, @"<div style=""padding-bottom:13px;""><a href=""/popup/bannerclick/bannerk-11350""><img src=""/gfx/wantcardsbanner.jpg"" width=""598"" height=""98"" style=""border-style:solid; border-width:1px; border-color: Black;"" /></a></div>"); }
			set
			{
				Set(FrontPageBannerHtmlHtmlSettingName, value);
			}
		}
		#endregion

		#region ServePixFromCdn
		public enum ServePixFromCdnOption
		{
			Off = 0,
			On = 1
		}
		internal const string ServePixFromCdnSettingName = "ServePixFromCdn";
		public static ServePixFromCdnOption ServePixFromCdn
		{
			get { return (ServePixFromCdnOption)Get(ServePixFromCdnSettingName, ServePixFromCdnOption.On); }
			set { Set(ServePixFromCdnSettingName, value); }
		}
		#endregion

		#region NewChatServerFullyEnabled
		public enum NewChatServerFullyEnabledOption
		{
			Off = 0,
			On = 1
		}
		internal const string NewChatServerFullyEnabledSettingName = "NewChatServerFullyEnabled";
		public static NewChatServerFullyEnabledOption NewChatServerFullyEnabled
		{
			get { return (NewChatServerFullyEnabledOption)Get(NewChatServerFullyEnabledSettingName, NewChatServerFullyEnabledOption.Off); }
			set { Set(NewChatServerFullyEnabledSettingName, value); }
		}
		#endregion

		#region CacheTesting
		public enum CacheTestingOption
		{
			Off = 0,
			On = 1
		}
		internal const string CacheTestingSettingName = "CacheTesting";
		public static CacheTestingOption CacheTesting
		{
			get { return (CacheTestingOption)Get(CacheTestingSettingName, CacheTestingOption.Off); }
			set { Set(CacheTestingSettingName, value); }
		}
		#endregion

		#region LoggingPageTime
		public enum LoggingPageTimeOption
		{
			Off = 0,
			On = 1
		}
		internal const string LoggingPageTimeSettingName = "LoggingPageTime";
		public static LoggingPageTimeOption LoggingPageTime
		{
			get { return (LoggingPageTimeOption)Get(LoggingPageTimeSettingName, LoggingPageTimeOption.Off); }
			set { Set(LoggingPageTimeSettingName, value); }
		}
		#endregion

		#region UseJobProcessorService
		internal const string UseJobProcessorServiceSettingName = "UseJobProcessorService";
		public static bool UseJobProcessorService
		{
			get { return (bool)Get(UseJobProcessorServiceSettingName, false); }
			set { Set(UseJobProcessorServiceSettingName, value); }
		}
		#endregion
		
		#region UsePerBannerTypeStats
		internal const string UsePerBannerTypeStatsSettingName = "UsePerBannerTypeStats";
		public static bool UsePerBannerTypeStats
		{
			get { return (bool)Get(UsePerBannerTypeStatsSettingName, false); }
			set { Set(UsePerBannerTypeStatsSettingName, value); }
		}
		#endregion
		
		#region UseGoogleAds
		internal const string UseGoogleAdsSettingName = "UseGoogleAds";
		public static bool UseGoogleAds
		{
			get { return (bool)Get(UseGoogleAdsSettingName, true); }
			set { Set(UseGoogleAdsSettingName, value); }
		}
		#endregion

		#region UseMemcachedToStoreViewState
		internal const string UseMemcachedToStoreViewStateSettingName = "UseMemcachedToStoreViewState";
		public static bool UseMemcachedToStoreViewState
		{
			get { return (bool)Get(UseMemcachedToStoreViewStateSettingName, false); }
			set { Set(UseMemcachedToStoreViewStateSettingName, value); }
		}
		#endregion
		//#region UseCometToServeRequests
		//internal const string UseCometToServeRequestsSettingName = "UseCometToServeRequests";
		//public static bool UseCometToServeRequests
		//{
		//    get { return (bool)Get(UseCometToServeRequestsSettingName, false); }
		//    set { Set(UseCometToServeRequestsSettingName, value); }
		//}
		//#endregion

        #region BannerVisibilityInDevEnv
		//public enum BannerVisibilityInDevEnvOption
		//{
		//    Hidden, Visible
		//}
		//internal const string BannerVisibilityInDevEnvSettingName = "BannerVisibilityInDevEnv";
		//public static BannerVisibilityInDevEnvOption BannerVisibilityInDevEnv
		//{
		//    get {
		//        if (System.Environment.MachineName == "HOTH")
		//            return BannerVisibilityInDevEnvOption.Hidden;
		//        else
		//            return (BannerVisibilityInDevEnvOption)Get(BannerVisibilityInDevEnvSettingName, BannerVisibilityInDevEnvOption.Hidden); 	
		//    }
		//    set { Set(BannerVisibilityInDevEnvSettingName, value); }
		//}
		#endregion

		#region SpreadBannerHitsSettingName
		internal const string SpreadBannerHitsSettingName = "SpreadBannerHits";
		public static bool SpreadBannerHits
		{
			get { return (bool)Get(SpreadBannerHitsSettingName, false); }
			set { Set(SpreadBannerHitsSettingName, value); }
		}
		#endregion

		#region BatchBannerStatUpdatesSettingName
		internal const string BatchBannerStatUpdatesSettingName = "BatchBannerStatUpdates";
		public static bool BatchBannerStatUpdates
		{
			get { return (bool)Get(BatchBannerStatUpdatesSettingName, false); }
			set { Set(BatchBannerStatUpdatesSettingName, value); }
		}
		#endregion

		#region New string Setting Template

		#region FpBox1Title
		internal const string FpBox1TitleSettingName = "FP box slot 1 title";
		public static string FpBox1Title
		{
			get { return (string)Get(FpBox1TitleSettingName, "NYE NEC!!!"); }
			set { Set(FpBox1TitleSettingName, value); }
		}
		#endregion
		#region FpBox1ImageGuid
		internal const string FpBox1ImageGuidSettingName = "FP box slot 1 image guid";
		public static string FpBox1ImageGuid
		{
			get { return (string)Get(FpBox1ImageGuidSettingName, "0804719d-834d-4490-b99f-792505d18b1e"); }
			set { Set(FpBox1ImageGuidSettingName, value); }
		}
		#endregion
		#region FpBox1ImageExtention
		internal const string FpBox1ImageExtentionSettingName = "FP box slot 1 image extention";
		public static string FpBox1ImageExtention
		{
			get { return (string)Get(FpBox1ImageExtentionSettingName, ".jpg"); }
			set { Set(FpBox1ImageExtentionSettingName, value); }
		}
		#endregion
		#region FpBox1LinkUrl
		internal const string FpBox1LinkUrlSettingName = "FP box slot 1 link url";
		public static string FpBox1LinkUrl
		{
			get { return (string)Get(FpBox1LinkUrlSettingName, ""); }
			set { Set(FpBox1LinkUrlSettingName, value); }
		}
		#endregion
		#region FpBox1BannerK
		internal const string FpBox1BannerKSettingName = "FP box slot 1 tracker bannerk";
		public static string FpBox1BannerK
		{
			get { return (string)Get(FpBox1BannerKSettingName, "9211"); }
			set { Set(FpBox1BannerKSettingName, value); }
		}
		#endregion
		#endregion
		#region FpWelcomeMessage
		internal const string FpWelcomeMessageSettingName = "Fp welcome message";
		public static string FpWelcomeMessage
		{
			get { return (string)Get(FpWelcomeMessageSettingName, "...the world's nightlife community"); }
			set
			{
				Set(FpWelcomeMessageSettingName, value);
			}
		}
		#endregion

		public enum LinkedBannersStatusOption { Off = 0, On = 1 } internal const string LinkedBannersStatusSettingName = "LinkedBannersStatus"; public static LinkedBannersStatusOption LinkedBannersStatus { get { return (LinkedBannersStatusOption)Get(LinkedBannersStatusSettingName, LinkedBannersStatusOption.Off); } set { Set(LinkedBannersStatusSettingName, value); } }
		#region LinkedBannersAsPercentageOfTraffic
		internal const string LinkedBannersAsPercentageOfTrafficSettingName = "LinkedBannersAsPercentageOfTraffic";
		public static string LinkedBannersAsPercentageOfTraffic
		{
			get { return (string)Get(LinkedBannersAsPercentageOfTrafficSettingName, "30.0%"); }
			set { Set(LinkedBannersAsPercentageOfTrafficSettingName, value); }
		}
		#endregion
		internal const string LinkedBannersHotboxBannerKSettingName = "LinkedBannersHotboxBannerK"; public static int LinkedBannersHotboxBannerK { get { return (int)Get(LinkedBannersHotboxBannerKSettingName, 100); } set { Set(LinkedBannersHotboxBannerKSettingName, value); } }
		internal const string LinkedBannersLeaderboardBannerKSettingName = "LinkedBannersLeaderboardBannerK"; public static int LinkedBannersLeaderboardBannerK { get { return (int)Get(LinkedBannersLeaderboardBannerKSettingName, 100); } set { Set(LinkedBannersLeaderboardBannerKSettingName, value); } }
		#region InactivityTimeoutDuration
		internal const string InactivityTimeoutDurationSettingName = "InactivityTimeoutDuration";
		public static int InactivityTimeoutDuration
		{
			get { return (int)Get(InactivityTimeoutDurationSettingName, 300); }
			set { Set(InactivityTimeoutDurationSettingName, value); }
		}
		#endregion
		#region DefaultBannerDurationInSeconds
		internal const string DefaultBannerDurationInSecondsSettingName = "DefaultBannerDurationInSeconds";
		public static int DefaultBannerDurationInSeconds
		{
			get { return (int)Get(DefaultBannerDurationInSecondsSettingName, 120); }
			set { Set(DefaultBannerDurationInSecondsSettingName, value); }
		}
		#endregion
		#region HouseBannersAsPercentageOfNullBanners
		internal const string HouseBannersAsPercentageOfNullBannersSettingName = "HouseBannersAsPercentageOfNullBanners";
		public static string HouseBannersAsPercentageOfNullBanners
		{
			get { return (string)Get(HouseBannersAsPercentageOfNullBannersSettingName, "20.0%"); }
			set { Set(HouseBannersAsPercentageOfNullBannersSettingName, value); }
		}
		#endregion

		#region ShowHotboxToCrawlersPercentage
		internal const string ShowHotboxToCrawlersPercentageSettingName = "ShowHotboxToCrawlersPercentage";
		public static int ShowHotboxToCrawlersPercentage
		{
			get { return (int)Get(ShowHotboxToCrawlersPercentageSettingName, 0); }
			set { Set(ShowHotboxToCrawlersPercentageSettingName, value); }
		}
		#endregion
		#region ShowLeaderboardToCrawlersPercentage
		internal const string ShowLeaderboardToCrawlersPercentageSettingName = "ShowLeaderboardToCrawlersPercentage";
		public static int ShowLeaderboardToCrawlersPercentage
		{
			get { return (int)Get(ShowLeaderboardToCrawlersPercentageSettingName, 0); }
			set { Set(ShowLeaderboardToCrawlersPercentageSettingName, value); }
		}
		#endregion
		#region ShowSkyScraperToCrawlersPercentage
		internal const string ShowSkyScraperToCrawlersPercentageSettingName = "ShowSkyScraperToCrawlersPercentage";
		public static int ShowSkyScraperToCrawlersPercentage
		{
			get { return (int)Get(ShowSkyScraperToCrawlersPercentageSettingName, 0); }
			set { Set(ShowSkyScraperToCrawlersPercentageSettingName, value); }
		}
		#endregion
		#region BannerCreditSettings
 
		#region LeaderboardCredits
		internal const string LeaderboardCreditsSettingName = "LeaderboardCredits";
		public static int LeaderboardCredits
		{
			get { return (int)Get(LeaderboardCreditsSettingName, 650); }
			set { Set(LeaderboardCreditsSettingName, value); }
		}
		#endregion
		#region SkyscaperCredits
		internal const string SkyscaperCreditsSettingName = "SkyscaperCredits";
		public static int SkyscaperCredits
		{
			get { return (int)Get(SkyscaperCreditsSettingName, 1200); }
			set { Set(SkyscaperCreditsSettingName, value); }
		}
		#endregion
		#region EmailBannerCredits
		internal const string EmailBannerCreditsSettingName = "EmailBannerCredits";
		public static int EmailBannerCredits
		{
			get { return (int)Get(EmailBannerCreditsSettingName, 10000); }
			set { Set(EmailBannerCreditsSettingName, value); }
		}
		#endregion
		#region HotboxCredits
		internal const string HotboxCreditsSettingName = "HotboxCredits";
		public static int HotboxCredits
		{
			get { return (int)Get(HotboxCreditsSettingName, 400); }
			set { Set(HotboxCreditsSettingName, value); }
		}
		#endregion
		#region PhotoBannerCredits
		internal const string PhotoBannerCreditsSettingName = "PhotoBannerCredits";
		public static int PhotoBannerCredits
		{
			get { return (int)Get(PhotoBannerCreditsSettingName, 5000); }
			set { Set(PhotoBannerCreditsSettingName, value); }
		}
		#endregion

		#endregion
		#region discount settings
		//0, 50, 100, 250, 500, 1000, 2000
		#region DiscountAt0000CreditsPercentage
		internal const string DiscountAt0000CreditsPercentageSettingName = "DiscountAt0000CreditsPercentage";
		public static int DiscountAt0000CreditsPercentage
		{
			get { return (int)Get(DiscountAt0000CreditsPercentageSettingName, 0); }
			set { Set(DiscountAt0000CreditsPercentageSettingName, value); }
		}
		#endregion
		#region DiscountAt0050CreditsPercentage
		internal const string DiscountAt0050CreditsPercentageSettingName = "DiscountAt0050CreditsPercentage";
		public static int DiscountAt0050CreditsPercentage
		{
			get { return (int)Get(DiscountAt0050CreditsPercentageSettingName, 5); }
			set { Set(DiscountAt0050CreditsPercentageSettingName, value); }
		}
		#endregion
		#region DiscountAt0100CreditsPercentage
		internal const string DiscountAt0100CreditsPercentageSettingName = "DiscountAt0100CreditsPercentage";
		public static int DiscountAt0100CreditsPercentage
		{
			get { return (int)Get(DiscountAt0100CreditsPercentageSettingName, 10); }
			set { Set(DiscountAt0100CreditsPercentageSettingName, value); }
		}
		#endregion
		#region DiscountAtCreditsPercentage
		internal const string DiscountAt0250CreditsPercentageSettingName = "DiscountAt0250CreditsPercentage";
		public static int DiscountAt0250CreditsPercentage
		{
			get { return (int)Get(DiscountAt0250CreditsPercentageSettingName, 15); }
			set { Set(DiscountAt0250CreditsPercentageSettingName, value); }
		}
		#endregion
		#region DiscountAt0500CreditsPercentage
		internal const string DiscountAt0500CreditsPercentageSettingName = "DiscountAt0500CreditsPercentage";
		public static int DiscountAt0500CreditsPercentage
		{
			get { return (int)Get(DiscountAt0500CreditsPercentageSettingName, 20); }
			set { Set(DiscountAt0500CreditsPercentageSettingName, value); }
		}
		#endregion
		#region DiscountAt1000CreditsPercentage
		internal const string DiscountAt1000CreditsPercentageSettingName = "DiscountAt1000CreditsPercentage";
		public static int DiscountAt1000CreditsPercentage
		{
			get { return (int)Get(DiscountAt1000CreditsPercentageSettingName, 25); }
			set { Set(DiscountAt1000CreditsPercentageSettingName, value); }
		}
		#endregion
		#region DiscountAt2000CreditsPercentage
		internal const string DiscountAt2000CreditsPercentageSettingName = "DiscountAt2000CreditsPercentage";
		public static int DiscountAt2000CreditsPercentage
		{
			get { return (int)Get(DiscountAt2000CreditsPercentageSettingName, 30); }
			set { Set(DiscountAt2000CreditsPercentageSettingName, value); }
		}
		#endregion

		#endregion
		#endregion

		#region Templates
		#region New Enum Setting Template
		/*
		#region <SETTING_NAME>
		public enum <SETTING_NAME>Option
		{
			<OPTIONS>
		}
		internal const string <SETTING_NAME>SettingName = "<SETTING_NAME>";
		public static <SETTING_NAME>Option <SETTING_NAME>
		{
			get { return (<SETTING_NAME>Option)Get(<SETTING_NAME>SettingName, <SETTING_NAME>Option.<DEFAULT_VALUE>); }
			set { Set(<SETTING_NAME>SettingName, value); }
		}
		#endregion
		*/
		#endregion

		#region New bool Setting Template
		/*
		#region <SETTING_NAME>
		internal const string <SETTING_NAME>SettingName = "<SETTING_NAME>";
		public static bool <SETTING_NAME>
		{
			get { return (bool)Get(<SETTING_NAME>SettingName, <DEFAULT_VALUE>); }
			set { Set(<SETTING_NAME>SettingName, value); }
		}
		#endregion
		*/
		#endregion

		#region New string Setting Template
		/*
		#region <SETTING_NAME>
		internal const string <SETTING_NAME>SettingName = "<SETTING_NAME>";
		public static string <SETTING_NAME>
		{
			get { return (string)Get(<SETTING_NAME>SettingName, "<DEFAULT_VALUE>"); }
			set { Set(<SETTING_NAME>SettingName, value); }
		}
		#endregion
		*/
		#endregion

		#region New string[] Setting Template
		/*
		#region <SETTING_NAME>
		internal const string <SETTING_NAME>SettingName = "<SETTING_NAME>";
		public static string[] <SETTING_NAME>
		{
			get { return ConvertStringToArray((string)Get(<SETTING_NAME>SettingName, ConvertArrayToString(new string[] { "<DEFAULT_VALUE>" }))); }
			set { Set(<SETTING_NAME>SettingName, ConvertArrayToString(value)); }
		}
		#endregion
		*/
		#endregion
		#endregion

		#region Logic
		static Dictionary<string, object> settings = new Dictionary<string, object>();
		static object Get(string settingName, object defaultValue)
		{
			if (settings.ContainsKey(settingName))
			{
				return settings[settingName];
			}
			else
			{
				lock (settings)
				{
					return GetSettingIfExistsOrInsertDefaultValue(settingName, defaultValue);
				}
			}
		}

		private static object GetSettingIfExistsOrInsertDefaultValue(string settingName, object defaultValue)
		{
			SqlConnection conn = new SqlConnection(Properties.ConnectionString);
			SqlCommand cmd = new SqlCommand("SELECT value FROM setting WHERE name = @name", conn);
			cmd.Parameters.AddWithValue("@name", settingName);
			try
			{
				conn.Open();
				object value = cmd.ExecuteScalar();

				if (value == null)
				{
					value = defaultValue;

					SqlCommand cmdInsertDefault = new SqlCommand("INSERT INTO Setting (Name, Value) values (@name, @value)", conn);
					cmdInsertDefault.Parameters.AddWithValue("@name", settingName);
					cmdInsertDefault.Parameters.AddWithValue("@value", defaultValue);

					if (cmdInsertDefault.ExecuteNonQuery() == 0) // 0 rows affected
					{
						throw new Exception("Failed to create setting for " + settingName);
					}
				}
				settings[settingName] = value;
				return value;
			}
			finally
			{
				conn.Close();
			}
		}

		private static char Separator = '¥';
		private static string[] ConvertStringToArray(string str)
		{
			return str.Split(Separator);
		}
		private static string ConvertArrayToString(string[] strArray)
		{
			return string.Join(Separator.ToString(), strArray);
		}

		static void Set(string settingName, object value)
		{
			SqlConnection conn = new SqlConnection(Properties.ConnectionString);
			SqlCommand cmd = new SqlCommand("UPDATE setting SET value = @value WHERE name = @name", conn);
			cmd.Parameters.AddWithValue("@name", settingName);
			cmd.Parameters.AddWithValue("@value", value);
			try
			{
				conn.Open();
				cmd.ExecuteNonQuery();
				lock (settings)
				{
					settings.Remove(settingName);
				}
			}
			finally
			{
				conn.Close();
			}
		}

		public static void RefreshAll()
		{
			lock (settings)
			{
				settings.Clear();
			}
		}
		#endregion


	}
}
