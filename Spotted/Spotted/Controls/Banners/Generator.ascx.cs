using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using Common.Clocks;
using Common;
using Bobs.BannerServer;
using Bobs.DataHolders;
using Spotted.Master;

namespace Spotted.Controls.Banners
{
	[ClientScript]
	public partial class Generator : EnhancedUserControl
	{

		public Generator()
		{
		}

		public Banner.Positions Position;
		public Panel Holder;

		public Banner BannerServerBanner = null;
		System.Threading.Thread bannerRequestThread = null;
		DateTime? timeThatBannnerRequestThreadWasStarted = null;
		//System.Threading.Thread mainThread = null;
		readonly TimeSpan maximumAmountOfTimeLongRunningThreadIsAllowedToTake = new TimeSpan(0, 0, 0, 0, 200);
		private const int maximumNumberOfPollsToCheckBannerExists = 4;
		Identity identity;

		public bool OnNullBannerCssHideHolderOnly { get; set; }

		Master.DsiPage ContainerPage 
		{
			get { return ((Master.DsiPage)Page); }
		}

		private void Page_Init(object sender, System.EventArgs e)
		{
			if (DontRenderBannerBecauseAjaxPartialPageRequestOrCrawler)
			{
				return;
			}
			if (ContainerPage.Url.HasObjectFilter && ContainerPage.Url.ObjectFilterBob is IRelevanceContributor)
				((IRelevanceContributor) ContainerPage.Url.ObjectFilterBob).AddRelevant(ContainerPage);
			//ScriptManager.RegisterClientScriptInclude(this.Page, typeof (Page), "FlashReplace", "/Misc/FlashReplace.js");

			this.identity = Identity.Current;
			if (!ShowLinkedBanner && !ShowLinkedWootUkBanner && !ShowRoadblockedGroupPageBanner)
			{
				this.timeThatBannnerRequestThreadWasStarted = Time.Now;
				bannerRequestThread = Utilities.GetSafeThread(this.GetBannerFromBannerServer);

				//this.mainThread = System.Threading.Thread.CurrentThread;
				bannerRequestThread.Start();
			}
		}

		bool ShowRoadblockedGroupPageBanner
		{
			get
			{
				return Common.Settings.RoadBlockOnGroupPageStatus == Settings.RoadBlockOnGroupPageStatusOption.On &&
					ContainerPage.Url.HasGroupObjectFilter &&
					ContainerPage.Url.LogicalFilterGroupK == Common.Settings.RoadblockOnGroupK;
			}
		}










		bool ShowUsaLeaderboard
		{
			get
			{
				if (!showUsaLeaderboard.HasValue)
				{

					try
					{
						if (Country.ClientCountryK == 225)
						{
							double trafficFraction = (double)Common.Settings.UsaLeaderboardPercentage / 100.0;
							bool result =
								Position == Banner.Positions.Leaderboard &&
								(ContainerPage.Random.NextDouble() < trafficFraction) &&
								(!Vars.UrlScheme.Equals("https"));
							showUsaLeaderboard = result;
						}
						else
							showUsaLeaderboard = false;
					}
					catch
					{
						showUsaLeaderboard = false;
					}


				}
				return showUsaLeaderboard.Value;
			}
		}
		bool? showUsaLeaderboard;

		bool ShowUsaMpu
		{
			get
			{
				if (!showUsaMpu.HasValue)
				{

					try
					{
						if (Country.ClientCountryK == 225)
						{
							double trafficFraction = (double)Common.Settings.UsaMpuPercentage / 100.0;
							bool result =
								Position == Banner.Positions.Hotbox &&
								(ContainerPage.Random.NextDouble() < trafficFraction) &&
								(!Vars.UrlScheme.Equals("https"));
							showUsaMpu = result;
						}
						else
							showUsaMpu = false;
					}
					catch
					{
						showUsaMpu = false;
					}


				}

				return showUsaMpu.Value;
			}
		}
		bool? showUsaMpu;














		bool ShowLinkedWootUkBanner
		{
			get
			{
				try
				{
					if (Country.ClientCountryK == 224)
					{
						double trafficFraction = (double)Common.Settings.WootUkLinkedPercentage / 100.0;
						bool result =
							(Page.Request.Path == "/" || Page.Request.Path == "/pages/home") &&
							(Position == Banner.Positions.Hotbox || Position == Banner.Positions.Leaderboard) &&
							(ContainerPage.GlobalRandomDouble < trafficFraction) &&
							(!Vars.UrlScheme.Equals("https"));
						return result;
					}
				}
				catch { }

				return false;
			}
		}

		bool ShowWootUkLeaderboard
		{
			get
			{
				if (!showWootUkLeaderboard.HasValue)
				{		

					try
					{
						if (Country.ClientCountryK == 224)
						{
							double trafficFraction = (double)Common.Settings.WootUkLeaderboardPercentage / 100.0;
							bool result =
								Position == Banner.Positions.Leaderboard &&
								(ContainerPage.Random.NextDouble() < trafficFraction) &&
								(!Vars.UrlScheme.Equals("https"));
							showWootUkLeaderboard = result;
						}
						else
							showWootUkLeaderboard = false;
					}
					catch {
						showWootUkLeaderboard = false;
					}

					
				}
				return showWootUkLeaderboard.Value;
			}
		}
		bool? showWootUkLeaderboard;

		bool ShowWootUkMpu
		{
			get
			{
				if (!showWootUkMpu.HasValue)
				{

					try
					{
						if (Country.ClientCountryK == 224)
						{
							double trafficFraction = (double)Common.Settings.WootUkMpuPercentage / 100.0;
							bool result =
								Position == Banner.Positions.Hotbox &&
								(ContainerPage.Random.NextDouble() < trafficFraction) &&
								(!Vars.UrlScheme.Equals("https"));
							showWootUkMpu = result;
						}
						else
							showWootUkMpu = false;
					}
					catch {
						showWootUkMpu = false;
					}

					
				}

				return showWootUkMpu.Value;
			}
		}
		bool? showWootUkMpu;

		bool ShowLinkedBanner
		{
			get
			{
				try{
					//if (((DsiPage)Page).Url.PagePath == "/Pages/Home.ascx" && Common.Settings.LinkedBannersStatus == Settings.LinkedBannersStatusOption.On)
					if (
						(Page.Request.Path == "/" || Page.Request.Path == "/pages/home") &&
						Common.Settings.LinkedBannersStatus == Settings.LinkedBannersStatusOption.On)
					{
						double trafficFraction = double.Parse(Common.Settings.LinkedBannersAsPercentageOfTraffic.Replace("%", "")) / 100.0;
						bool result = 
							(Position == Banner.Positions.Hotbox || Position == Banner.Positions.Leaderboard) &&
							(ContainerPage.GlobalRandomDouble < trafficFraction) &&
							(!Vars.UrlScheme.Equals("https"));
						return result;
					}
					else
						return false;
				}
				catch
				{
					return false;
				}
			}
		}

		private bool? dontRenderBannerBecauseAjaxPartialPageRequestOrCrawler;
		bool DontRenderBannerBecauseAjaxPartialPageRequestOrCrawler
		{
			get
			{
				if (!dontRenderBannerBecauseAjaxPartialPageRequestOrCrawler.HasValue)
				{
					if (ContainerPage.Url.IsAjaxRequest &&
					    this.Position != Banner.Positions.PhotoBanner &&
					    this.Position != Banner.Positions.EmailBanner)
					{
						dontRenderBannerBecauseAjaxPartialPageRequestOrCrawler = true;
					}
					else if (Visit.Current != null && Visit.Current.IsCrawler)
					{
						switch (this.Position)
						{
							case Banner.Positions.Hotbox:
								dontRenderBannerBecauseAjaxPartialPageRequestOrCrawler = Common.ThreadSafeRandom.Next(100) > Common.Settings.ShowHotboxToCrawlersPercentage;
								break;
							case Banner.Positions.Leaderboard:
								dontRenderBannerBecauseAjaxPartialPageRequestOrCrawler = Common.ThreadSafeRandom.Next(100) > Common.Settings.ShowLeaderboardToCrawlersPercentage;
								break;
							case Banner.Positions.Skyscraper:
								dontRenderBannerBecauseAjaxPartialPageRequestOrCrawler = Common.ThreadSafeRandom.Next(100) > Common.Settings.ShowSkyScraperToCrawlersPercentage;
								break;
							default:
								dontRenderBannerBecauseAjaxPartialPageRequestOrCrawler = false;
								break;
						}
					}
					else
					{
						dontRenderBannerBecauseAjaxPartialPageRequestOrCrawler = false;
					}
				}

				return dontRenderBannerBecauseAjaxPartialPageRequestOrCrawler.Value;
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			if (DontRenderBannerBecauseAjaxPartialPageRequestOrCrawler)
			{
				this.Visible = false;
				return;
			}
			this.uiPosition.Value = ((int)Position).ToString();
			
			//if (Position == Banner.Positions.Hotbox && HttpContext.Current != null && (HttpContext.Current.Request.Url.PathAndQuery == "/groups/fosters" || HttpContext.Current.Request.Url.PathAndQuery == "/pages/shadownap"))
			//{
			//    BannerServerBanner = new Banner(11228);
			//    ShowBanner(BannerServerBanner);
			//}
			//else 

			
			if (ShowRoadblockedGroupPageBanner)
			{
				if (Position == Banner.Positions.Hotbox)
				{
					if (Common.Settings.RoadblockOnGroupHotboxBannerK > 0)
						BannerServerBanner = new Banner(Common.Settings.RoadblockOnGroupHotboxBannerK);
					else
						BannerServerBanner = null;
				}
				else if (Position == Banner.Positions.Leaderboard)
				{
					if (Common.Settings.RoadblockOnGroupLeaderboardBannerK > 0)
						BannerServerBanner = new Banner(Common.Settings.RoadblockOnGroupLeaderboardBannerK);
					else
						BannerServerBanner = null;
				}
				else if (Position == Banner.Positions.Skyscraper)
				{
					if (Common.Settings.RoadblockOnGroupSkyscraperBannerK > 0)
						BannerServerBanner = new Banner(Common.Settings.RoadblockOnGroupSkyscraperBannerK);
					else
						BannerServerBanner = null;
				}
			}
			else if (ShowLinkedWootUkBanner)
			{
				if (Position == Banner.Positions.Hotbox)
				{
					BannerServerBanner = new Banner(Common.Settings.WootUkLinkedMpuBannerK);
				}
				else if (Position == Banner.Positions.Leaderboard)
				{
					BannerServerBanner = new Banner(Common.Settings.WootUkLinkedLeaderboardBannerK);
				}
			}
			else if (ShowLinkedBanner)
			{
				if (Position == Banner.Positions.Hotbox)
				{
					BannerServerBanner = new Banner(Common.Settings.LinkedBannersHotboxBannerK);
				}
				else if (Position == Banner.Positions.Leaderboard)
				{
					BannerServerBanner = new Banner(Common.Settings.LinkedBannersLeaderboardBannerK);
				}
			}
			else if (ShowUsaLeaderboard)
			{
				BannerServerBanner = new Banner(Common.Settings.UsaMainLeaderboardBannerK);
			}
			else if (ShowUsaMpu)
			{
				BannerServerBanner = new Banner(Common.Settings.UsaMainMpuBannerK);
			}
			else if (ShowWootUkLeaderboard)
			{
				BannerServerBanner = new Banner(Common.Settings.WootUkMainLeaderboardBannerK);
			}
			else if (ShowWootUkMpu)
			{
				BannerServerBanner = new Banner(Common.Settings.WootUkMainMpuBannerK);
			}
			else
			{
				GiveBannerServerAChanceToCompleteWithinAcceptableTimeLimit();
			}

			if (this.BannerServerBanner != null)
			{
				this.uiK.Value = this.BannerServerBanner.K.ToString();
				this.uiMusicTypes.Value = String.Join(",", ((DsiPage)this.Page).RelevantMusic.Select(mt => mt.ToString()).ToArray());
				this.uiPlaceKs.Value = String.Join(",", ((DsiPage)this.Page).RelevantPlaces.Select(pK => pK.ToString()).ToArray());
				this.uiDuration.Value = ((this.BannerServerBanner.DisplayDuration ?? Common.Settings.DefaultBannerDurationInSeconds) * 1000).ToString();
				this.uiInactivityTimeoutDuration.Value = (Common.Settings.InactivityTimeoutDuration * 1000).ToString();
				this.uiClickHelperLeft.Value = ClickHelperLeftOffset.ToString();
				this.uiClickHelperTop.Value = ClickHelperTopOffset.ToString();
			}

			ShowBanner(BannerServerBanner);
		}

		#region GiveBannerServerAChanceToCompleteWithinAcceptableTimeLimit()
		private void GiveBannerServerAChanceToCompleteWithinAcceptableTimeLimit()
		{
			if (bannerRequestThread == null ||
				(bannerRequestThread.ThreadState & System.Threading.ThreadState.Stopped) != System.Threading.ThreadState.Stopped)
			{
				TimeSpan elapsedTime = Time.Now.Subtract(timeThatBannnerRequestThreadWasStarted.Value);
				if (elapsedTime < maximumAmountOfTimeLongRunningThreadIsAllowedToTake)
				{
					try
					{
						//at this point sleep for the remainder of timeout period
						//the other thread will restart this one if it completes during this time
						int sleepMilliseconds = maximumAmountOfTimeLongRunningThreadIsAllowedToTake.Subtract(elapsedTime).Milliseconds/
						                        maximumNumberOfPollsToCheckBannerExists;
						for (int i = 0; i < maximumNumberOfPollsToCheckBannerExists; i++)
						{
							System.Threading.Thread.Sleep(sleepMilliseconds);
							if (BannerServerBanner != null) break;
						}
					}
					catch (System.Threading.ThreadInterruptedException)
					{
					}
				}
				if (bannerRequestThread == null ||
				    (bannerRequestThread.ThreadState & System.Threading.ThreadState.Stopped) != System.Threading.ThreadState.Stopped)
				{
					LogThatATimeOutOccurred();
				}
			}
		}
		#endregion

		private void LogThatATimeOutOccurred()
		{
			Bobs.Global.IncrementRequestCounter(Bobs.Global.RequestCounter.BannerServerTimeout);
			Timeslots.GetCurrentTimeslot().BannerTimeouts().Increment();
		}

		void GetBannerFromBannerServer()
		{
			Bobs.BannerServer.Server server = new Bobs.BannerServer.Server();
			BannerDataHolder banner = server.GetBanner(Position, Vars.UrlScheme.Equals("https"), identity, ((Master.DsiPage)Page).BannerRequestRules);
			if (banner != null)
			{
				BannerServerBanner = new Banner(banner.K);
			}

			//if ((mainThread.ThreadState & System.Threading.ThreadState.WaitSleepJoin) == System.Threading.ThreadState.WaitSleepJoin)
			//{
			//    mainThread.Interrupt();
			//}
		}

		public int ClickHelperTopOffset { get; set; }
		public int ClickHelperLeftOffset { get; set; }
		public bool ShowClickHelper { get; set; }

		void ShowBanner(Banner banner)
		{

			if (banner != null)
			{
				if (Vars.UrlScheme.Equals("https") && banner.DisplayType.Equals(Banner.DisplayTypes.CustomHtml))
				{
					EmitDefaultText();
				}
				else
				{
					AddPromoterBanner(banner, true);
				}

			}
			else if (ThreadSafeRandom.NextDouble() < (double.Parse(Common.Settings.HouseBannersAsPercentageOfNullBanners.Replace("%", "")) / 100.0))
			{
				try
				{
					Query q = new Query();
					q.CacheDuration = TimeSpan.FromHours(1);
					q.QueryCondition = new And(
						new Q(Banner.Columns.PromoterK, 1622),
						new Q(Banner.Columns.BannerFolderK, 5879),
						new Q(Banner.Columns.Position, this.Position),
						new Q(Banner.Columns.StatusArtwork, true)
						);
					BannerSet bs = new BannerSet(q);
					if (bs.Count > 0)
					{
						AddPromoterBanner(bs[ThreadSafeRandom.Next(bs.Count)], true);
					}
					else
					{
						EmitDefaultText();
					}
				}
				catch 
				{
					EmitDefaultText();
				}
				Timeslots.GetCurrentTimeslot().TotalNotShown().Increment();

			}
			else if( Common.Settings.UseGoogleAds && !(Vars.UrlScheme.Equals("https") ))
			{
				EmitGoogleTag();
			}
			else
			{
				EmitDefaultText();
				Timeslots.GetCurrentTimeslot().TotalNotShown().Increment();
			}

			
		}

		public void AddPromoterBanner(Banner banner, bool registerHit)
		{
			#region Add promoter banner
			GenericBanner bControl = (GenericBanner)this.LoadControl("/Controls/Banners/GenericBanner.ascx");

			if (registerHit)
				banner.RegisterHit();

			bControl.CurrentBanner = banner;
			bControl.ClickHelperLeftOffset = ClickHelperLeftOffset;
			bControl.ClickHelperTopOffset = ClickHelperTopOffset;
			bControl.ShowClickHelper = ShowClickHelper;
			bControl.Bind();
			this.uiBanner.Controls.Add(bControl);
			#endregion
		}
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			switch (this.Position)
			{
				case Banner.Positions.Leaderboard:
					Log.Increment(Log.Items.LeaderboardRender);
					Log.Increment(Log.Items.LeaderboardHit);

					break;
				case Banner.Positions.Skyscraper:
					Log.Increment(Log.Items.SkyScraperRender);
					Log.Increment(Log.Items.SkyScraperHit);
					break;
				case Banner.Positions.Hotbox:
					Log.Increment(Log.Items.HotBoxRender);
					Log.Increment(Log.Items.HotBoxHit);
					break;
				case Banner.Positions.PhotoBanner:
					Log.Increment(Log.Items.PhotoBannerRender);
					Log.Increment(Log.Items.PhotoBannerHit);

					break;
				default:
					throw new NotImplementedException();
			}
		}
		//public void EmitDefaultImage()
		//{
		//    if (this.Position.Equals(Banner.Positions.Leaderboard))
		//    {
		//        #region Emit leaderboard default image
		//        this.uiBanner.Controls.Add(new LiteralControl("<a href=\"/pages/promoters/intro\"><img src=\"/gfx/default1-lb.jpg\" width=\"728\" height=\"90\" border=\"0\" /></a>"));
		//        #endregion
		//    }
		//    else if (false)//this.Position.Equals(Banner.Positions.Skyscraper))
		//    {
		//        #region Emit skyscraper default image
		//        this.uiBanner.Controls.Add(new LiteralControl("<a href=\"/pages/promoters/intro\"><img src=\"/gfx/default1-sky.jpg\" width=\"120\" height=\"600\" border=\"0\" /></a>"));
		//        #endregion
		//    }
		//    else if (false)//this.Position.Equals(Banner.Positions.Hotbox))
		//    {
		//        #region Emit hotbox default image
		//        this.uiBanner.Controls.Add(new LiteralControl("<a href=\"/pages/promoters/intro\"><img src=\"/gfx/default1-hb.jpg\" width=\"300\" height=\"250\" border=\"0\" /></a>"));
		//        #endregion
		//    }
		//    else if (Holder != null)
		//    {
		//        if (OnNullBannerCssHideHolderOnly)
		//            Holder.Style["display"] = "none";
		//        else
		//            Holder.Visible = false;
		//    }
		//}

		public void EmitDefaultText()
		{
			if (this.Position.Equals(Banner.Positions.Leaderboard))
			{
				#region Emit leaderboard default text
				this.uiBanner.Controls.Add(new LiteralControl("<div style=\"background-color:#ffffff;\"><table style=\"background-color:#FFFFFF;width:728px;height:90px;\"><tr><td valign=\"middle\" align=\"center\" style=\"padding:10px;\"><b>To find out more about advertising, <a href=\"/pages/promoters/intro\">click here</a>.</b></td></tr></table></div>"));
				#endregion
			}
			else if (false)//this.Position.Equals(Banner.Positions.Skyscraper))
			{
				#region Emit skyscraper default text
				this.uiBanner.Controls.Add(new LiteralControl("<div style=\"background-color:#ffffff;\"><table style=\"background-color:#ffffff;width:300px;height:600px;\"><tr><td valign=\"middle\" align=\"center\" style=\"padding:10px;\">This is not a banner. If you'd like your own message here, <a href=\"/pages/promoters/intro\">click here</a>.</td></tr></table></div>"));
				#endregion
			}
			else if (false)//this.Position.Equals(Banner.Positions.Hotbox))
			{
				#region Emit hotbox default text
				this.uiBanner.Controls.Add(new LiteralControl("<div style=\"background-color:#ffffff;\"><table style=\"background-color:#ffffff;width:300px;height:250px;\"><tr><td valign=\"middle\" align=\"center\" style=\"padding:10px;\">This is not a banner. If you'd like your own message here, <a href=\"/pages/promoters/intro\">click here</a>.</td></tr></table></div>"));
				#endregion
			}
			else if (Holder != null)
			{
				if (OnNullBannerCssHideHolderOnly)
					Holder.Style["display"] = "none";
				else
					Holder.Visible = false;
			}
		}

		public void EmitGoogleTag()
		{
			var gb = (GoogleBanner) Page.LoadControl(@"~\Controls\Banners\GoogleBanner.ascx");
			switch(this.Position)
			{
				case Banner.Positions.Leaderboard:
				{
					gb.Width = 728;
					gb.Height = 90;
					Log.Increment(Log.Items.GoogleLeaderboard);
					break;
				}
				case Banner.Positions.Skyscraper:
				{
					Log.Increment(Log.Items.GoogleSkyscraper);
					gb.Width = 160;
					gb.Height = 600;
					break;
				}
				case Banner.Positions.Hotbox:
				{
					Log.Increment(Log.Items.GoogleHotbox);
					gb.Width = 300;
					gb.Height = 250;
					break;
				}
				default:
					break;
			}
			this.uiBanner.Controls.Add(gb);
		}

		//        public void EmitAddvantageTag(string Sub1)
//        {
//            if (this.Position.Equals(Banner.Positions.Leaderboard))
//            {
//                Bobs.Log.Increment(Bobs.Log.Items.AddvantageLeaderboard);
//                #region Emit Addvantage leaderboard tag
//                this.uiBanner.Controls.Add(new LiteralControl(@"<script language=""javascript"">
//<!--
//if (window.adgroupid == undefined) {
//	window.adgroupid = Math.round(Math.random() * 1000);
//}
//document.write('<scr'+'ipt language=""javascript1.1"" src=""http://adserver.adtech.de/addyn|3.0|338|1048686|0|225|ADTECH;loc=100;target=_blank;grp='+window.adgroupid+';sub1=" + Sub1 + @";misc='+new Date().getTime()+'""></scri'+'pt>');
////-->
//</script><noscript><a href=""http://adserver.adtech.de/adlink|3.0|338|1048686|0|225|ADTECH;loc=300;sub1=" + Sub1 + @";"" target=""_blank""><img src=""http://adserver.adtech.de/adserv|3.0|338|1048686|0|225|ADTECH;loc=300"" border=""0"" width=""728"" height=""90""></a></noscript>"));
//                #endregion
//            }
//            else if (this.Position.Equals(Banner.Positions.Skyscraper))
//            {
//                Bobs.Log.Increment(Bobs.Log.Items.AddvantageSkyscraper);
//                #region Emit Addvantage skyscraper tag
//                this.uiBanner.Controls.Add(new LiteralControl(@"<script language=""javascript"">
//<!--
//if (window.adgroupid == undefined) {
//	window.adgroupid = Math.round(Math.random() * 1000);
//}
//document.write('<scr'+'ipt language=""javascript1.1"" src=""http://adserver.adtech.de/addyn|3.0|338|1037899|0|168|ADTECH;loc=100;target=_blank;grp='+window.adgroupid+';sub1=" + Sub1 + @";misc='+new Date().getTime()+'""></scri'+'pt>');
////-->
//</script><noscript><a href=""http://adserver.adtech.de/adlink|3.0|338|1037899|0|168|ADTECH;loc=300;sub1=" + Sub1 + @";"" target=""_blank""><img src=""http://adserver.adtech.de/adserv|3.0|338|1037899|0|168|ADTECH;loc=300"" border=""0"" width=""120"" height=""600""></a></noscript>"));
//                #endregion
//            }
//            else if (this.Position.Equals(Banner.Positions.Hotbox))
//            {
//                Bobs.Log.Increment(Bobs.Log.Items.AddvantageHotbox);
//                #region Emit Addvantage hotbox tag
//                this.uiBanner.Controls.Add(new LiteralControl(@"<script language=""javascript"">
//<!--
//if (window.adgroupid == undefined) {
//	window.adgroupid = Math.round(Math.random() * 1000);
//}
//document.write('<scr'+'ipt language=""javascript1.1"" src=""http://adserver.adtech.de/addyn|3.0|338|1359192|0|170|ADTECH;loc=100;target=_blank;grp='+window.adgroupid+';sub1=" + Sub1 + @";misc='+new Date().getTime()+'""></scri'+'pt>');
////-->
//</script><noscript><a href=""http://adserver.adtech.de/adlink|3.0|338|1359192|0|170|ADTECH;loc=300;sub1=" + Sub1 + @";"" target=""_blank""><img src=""http://adserver.adtech.de/adserv|3.0|338|1359192|0|170|ADTECH;loc=300"" border=""0"" width=""300"" height=""250""></a></noscript>"));
//                #endregion
//            }
//        }
 
	}
}
