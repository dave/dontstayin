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
using Bobs;
using System.Xml;
using Local;
using System.Drawing;
using Bobs.BannerServer;
using SpottedScript.Controls.ChatClient.Shared;
using Bobs.Main;
using System.Collections.Generic;



namespace Spotted.Master
{
	public partial class DsiPage : GenericPage, IRelevanceHolder
	{

		#region Z-index comments
		/*
		 * z-index values for the page:
		 * 
		 * 1000 Tip layer (for rollovers)
		 * 995 jquery dialog, waiting label
		 * 990 jquery dialogs
		 * 975 Photo browser rollover helpers
		 * 970 Photo browser blowup images
		 * 960 Chat client popup
		 * 950 Auto-complete drop-downs
		 * 900 Admin navigation
		 * 510 Leaderboard click helper
		 * 500 Leaderboard
		 * 400 Menus
		 * 310 Hotbox click helper
		 * 300 Hotbox
		 * 210 Skyscraper click helper
		 * 200 Skyscraper
		 * 150 Chat client
		 * 125 Chat client popup animated transfer div
		 * 100 Content
		 * 50 Chat client popup area
		 * -10 Takeover footers (negative numbers was the only way I could stop IE from rendering the footers above the content)
		 */
		#endregion

		#region Controls
		public global::System.Web.UI.HtmlControls.HtmlGenericControl Body;
		public global::Spotted.Controls.Promoters.AccountsWarning PromoterAccountsWarningControl;
		public global::System.Web.UI.HtmlControls.HtmlGenericControl BigLeaderboardOuterDiv;
		public global::System.Web.UI.HtmlControls.HtmlGenericControl LeaderboardOuterDiv;
		public global::System.Web.UI.HtmlControls.HtmlGenericControl BigLeaderboardInnerDiv;
		public global::System.Web.UI.HtmlControls.HtmlGenericControl LeaderboardInnerDiv;
		public global::System.Web.UI.WebControls.Panel HotboxOuterDiv;
		public global::System.Web.UI.HtmlControls.HtmlGenericControl HotboxInnerDiv;
		public global::System.Web.UI.HtmlControls.HtmlGenericControl SkyscraperOuterDiv;
		public global::System.Web.UI.HtmlControls.HtmlGenericControl SkyscraperInnerDiv;
		public global::System.Web.UI.ScriptManager Script;
		public global::Spotted.Controls.Navigation.NewMenu Menu;

		public global::System.Web.UI.HtmlControls.HtmlMeta MetaTitle;
		public global::System.Web.UI.HtmlControls.HtmlMeta MetaDescription;
		public global::System.Web.UI.HtmlControls.HtmlMeta MetaMedium;
		public global::System.Web.UI.HtmlControls.HtmlLink LinkImageSrc;
		public global::System.Web.UI.HtmlControls.HtmlLink LinkVideoSrc;
		public global::System.Web.UI.HtmlControls.HtmlMeta MetaVideoHeight;
		public global::System.Web.UI.HtmlControls.HtmlMeta MetaVideoWidth;
		public global::System.Web.UI.HtmlControls.HtmlMeta MetaVideoType;

		protected HiddenField CountryKFromIp, AuthCookieHasError;
		
		public HtmlTitle PageTitleTag;
		public HtmlForm TemplateForm;

		#endregion

		#region GlobalRandomDouble
		public double GlobalRandomDouble
		{
			get
			{
				if (!generatedGlobalRandomDouble)
				{
					Random r = new Random();
					globalRandomDouble = r.NextDouble();
					generatedGlobalRandomDouble = true;
				}
				return globalRandomDouble;
			}
		}
		public Random Random = new Random();
		double globalRandomDouble;
		bool generatedGlobalRandomDouble = false;
		#endregion

		#region UseLeftHandSideForContent
		public bool UseLeftHandSideForContent
		{
			get
			{
				return ViewState["UseLeaderBoardAndChatForContent"] as bool? ?? false;
			}
			set
			{
				ViewState["UseLeaderBoardAndChatForContent"] = value;
			}
		}
		#endregion

		#region Page_Init
		private void Page_Init(object sender, System.EventArgs e)
		{
			ScriptSharp.Include(Page, "/mscorlib");
			ScriptSharp.Include(Page, "/Library");
			
			JQuery.Include(Page);
			JQuery.Include(Page, "ui.core");
				

			ScriptManager.RegisterClientScriptInclude(Page, typeof(Page), "helpers", "/Misc/Helpers.js?a=1");
			ScriptManager.RegisterClientScriptInclude(Page, typeof(Page), "dsipage", "/Misc/Dsipage.js?a=17");

			ScriptManager.RegisterStartupScript(this, this.GetType(), "Tip", "mig_hand();", true);
			HttpContext.Current.Items["DsiPage"] = 1;

			BindSalesCalls();
			Page.Trace.Write("Master.DsiPage Page_Init");
			HttpContext.Current.Items["VisitPages"] = 1;
			HttpContext.Current.Items["PageCustPage"] = this.Url.PagePath;

			if (!Url.IsAjaxRequest)
			{
				Log.Increment(Log.Items.DsiPages);
			}

			try
			{
				if (Url.ObjectFilterType == Model.Entities.ObjectType.Event && Url.ObjectFilterK == 165980)
					Body.Style["background-color"] = "#000000";
			}
			catch { }

			#region EventList music choice change - Set prefs for caching
			if (Request.Form[Spotted.Controls.MusicTypeDropDownList.Name] != null)
			{
				bool changed = false;
				int currentMusicPref = 1;
				if (Prefs.Current["MusicPref"].Exists)
				{
					currentMusicPref = Prefs.Current["MusicPref"];
				}
				string[] arr = Request.Form[Spotted.Controls.MusicTypeDropDownList.Name].Split(',');
				foreach (string s in arr)
				{
					if (int.Parse(s) != currentMusicPref)
					{
						Prefs.Current["MusicPref"] = int.Parse(s);
						changed = true;
					}
				}
				if (changed)
					AnchorSkip("UcEventList");
			}
			#endregion


			N3BottomBox.Controls.Clear();
			N3BottomBox.Controls.Add(new LiteralControl(Common.Settings.NetworkN3Html));

			if (Request.QueryString["fbpk"] != null)
			{
				try
				{
					int fbpk = int.Parse(Request.QueryString["fbpk"]);
					//FacebookPost fbp = new FacebookPost(fbpk);
					Update u = new Update();
					u.Table = TablesEnum.FacebookPost;
					u.Changes.Add(new Assign.Increment(FacebookPost.Columns.Hits));
					u.Where = new Q(FacebookPost.Columns.K, fbpk);
					u.Run();
				}
				catch
				{
				}
			}


		}
		#endregion

		#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{
			Page.Trace.Write("Master.DsiPage Page_Load");

			

			//	ScriptManager.RegisterStartupScript(this, typeof(Page), "WindowScroll", "$addHandler(window, \"scroll\", WindowScroll);", true);
			//	ScriptManager.RegisterStartupScript(this, typeof(Page), "WindowResize", "$addHandler(window, \"resize\", WindowResize);", true);

			if (Common.Settings.AboveChatStatus == Common.Settings.AboveChatStatusOption.On ||
				(Usr.Current != null && Usr.Current.IsAdmin && Common.Settings.AboveChatStatus == Common.Settings.AboveChatStatusOption.Test))
			{
				AboveChatHtmlPh.Controls.Clear();
				AboveChatHtmlPh.Controls.Add(new LiteralControl(Common.Settings.AboveChatBoxHtml));
			}

			if (Usr.Current != null)
			{
				#region Redirect if the user unsubscribed
				if (Usr.Current.EmailHold && Url.PageName != "Login")
					Response.Redirect("/popup/unsubscribe");
				#endregion

				#region Redirect if skeleton
				if (Usr.Current.IsSkeleton && !OverrideUsrRedirect)
				{
					if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
						Response.Redirect("/popup/welcome?Url=" + HttpUtility.UrlEncode(Request.QueryString["Url"]));
					else
						Response.Redirect("/popup/welcome?Url=" + HttpUtility.UrlEncode(Page.Request.Url.PathAndQuery));
				}
				#endregion

				#region Redirect if hasn't agreed to new terms
				if (!Usr.Current.LegalTermsUser2 && !OverrideUsrRedirect)
				{
					if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
						Response.Redirect("/popup/legaltermsuseragree?Url=" + HttpUtility.UrlEncode(Request.QueryString["Url"]));
					else
						Response.Redirect("/popup/legaltermsuseragree?Url=" + HttpUtility.UrlEncode(Page.Request.Url.PathAndQuery));
				}
				#endregion

				#region Redirect if promoter hasn't agreed to new terms
				if (Usr.Current.IsPromoter && !Usr.Current.LegalTermsPromoter2)
				{
					if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
						Response.Redirect("/popup/legaltermspromoteragree?Url=" + HttpUtility.UrlEncode(Request.QueryString["Url"]));
					else
						Response.Redirect("/popup/legaltermspromoteragree?Url=" + HttpUtility.UrlEncode(Page.Request.Url.PathAndQuery));
				}
				#endregion

				#region Set DateTimeLastPageRequest
				if (!Usr.Current.IsSkeleton)
				{
					DateTime now = DateTime.Now;
					Usr.Current.RegisterPageRequest(false, now);
					Usr.Current.LastIp = Utilities.TruncateIp(Request.ServerVariables["REMOTE_HOST"]);
					Usr.Current.Update();
				}
				#endregion
			}

			UsrK.Value = Usr.Current == null ? "0" : Usr.Current.K.ToString();
			UsrKAtInit.Value = Usr.Current == null ? "0" : Usr.Current.K.ToString();
			UsrNickname.Value = Usr.Current == null ? "" : Usr.Current.NickName;
			UsrLink.Value = Usr.Current == null ? "" : Usr.Current.Link();
			try
			{
				CountryKFromIp.Value = Visit.Current.CountryK.ToString();
			}
			catch { }


			#region Drinking age verification
			bool allowTakeoverAfterAgeCheck = true;
			if (Common.Settings.TakeoverRequiresDrinkingAgeVerificationStatus == Common.Settings.TakeoverRequiresDrinkingAgeVerificationStatusOption.On)
			{
				if (Prefs.Current["Drink"].IsNull)
				{
					bool? isOfDrinkingAge = null;

					if (Usr.Current != null)
					{
						isOfDrinkingAge = Usr.Current.IsOfLegalDrinkingAgeInHomeCountry;
					}
					else
					{
						if (Prefs.Current["DateOfBirth"].Exists && Prefs.Current["HomeCountryK"].Exists)
						{
							isOfDrinkingAge = Usr.IsOfLegalDrinkingAgeInHomeCountryStatic(Prefs.Current["HomeCountryK"], DateTime.Parse(Prefs.Current["DateOfBirth"]));
						}
					}

					if (!isOfDrinkingAge.HasValue)
					{
						// are we on a page that requires drinking age verification?
						if (!Page.IsPostBack && !Visit.Current.IsCrawler)
						{
							if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().Contains("dontstayin.com"))
								Response.Redirect("/popup/drinkingage?url=" + Server.UrlEncode(Request.Url.ToString()) + "&back=" + Server.UrlEncode(Request.UrlReferrer.ToString()));
							else
								Response.Redirect("/popup/drinkingage?url=" + Server.UrlEncode(Request.Url.ToString()));
						}
						else
							allowTakeoverAfterAgeCheck = false;
					}
					else if (isOfDrinkingAge.Value == true)
					{
						Prefs.Current["Drink"] = 1;
						allowTakeoverAfterAgeCheck = true;
					}
					else if (isOfDrinkingAge.Value == false)
					{
						allowTakeoverAfterAgeCheck = false;
					}
				}
				else if (Prefs.Current["Drink"] == 1)
					allowTakeoverAfterAgeCheck = true;
				else
					allowTakeoverAfterAgeCheck = false;

			}
			#endregion

			#region Takeover
			if (allowTakeoverAfterAgeCheck &&
				!SslPage &&
				Vars.UrlScheme != "https" &&
				!Vars.ClientIsDevBox &&
				(Usr.Current == null || Usr.Current.K != 4) &&
				(Common.Settings.TakeoverStatus == Common.Settings.TakeoverStatusOption.On || (Common.Settings.TakeoverStatus == Common.Settings.TakeoverStatusOption.Test && Usr.Current != null && Usr.Current.IsAdmin))
				)
			{


				if (Common.Settings.TakeoverBackgroundStatus == Common.Settings.TakeoverBackgroundStatusOption.On)
				{

					if (Common.Settings.TakeoverBackgroundColour.Length > 0)
					{
						Body.Style["background-color"] = "#" + Common.Settings.TakeoverBackgroundColour;
						Body.Attributes["bgcolor"] = Common.Settings.TakeoverBackgroundColour;
					}

					if (Common.Settings.TakeoverBackgroundImage.Length > 0)
						Body.Style["background-image"] = "url(" + Common.Settings.TakeoverBackgroundImage + ")";

					if (Common.Settings.TakeoverBackgroundAttachment.Length > 0)
						Body.Style["background-attachment"] = Common.Settings.TakeoverBackgroundAttachment;

					if (Common.Settings.TakeoverBackgroundPosition.Length > 0)
						Body.Style["background-position"] = Common.Settings.TakeoverBackgroundPosition;

					if (Common.Settings.TakeoverBackgroundRepeat.Length > 0)
						Body.Style["background-repeat"] = Common.Settings.TakeoverBackgroundRepeat;

				}

				if (Common.Settings.TakeoverLinksStatus == Common.Settings.TakeoverLinksStatusOption.On)
				{
					uiLinksDiv.Visible = true;
					uiLinksDiv.InnerHtml = Common.Settings.TakeoverLinksHtml;
					uiLinksDiv.Style["width"] = Common.Settings.TakeoverLinksWidth.ToString() + "px";
					uiLinksDiv.Style["height"] = Common.Settings.TakeoverLinksHeight.ToString() + "px";
					ScriptManager.RegisterStartupScript(this, typeof(Page), "Scroll", "$addHandler(window, \"scroll\", UpdateLinksSideBarDiv);", true);
				}

				if (Common.Settings.TakeoverFooterRightStatus == Common.Settings.TakeoverFooterRightStatusOption.On)
				{
					uiFooterRightDiv.Visible = true;
					uiFooterRightDiv.InnerHtml = Common.Settings.TakeoverFooterRightHtml;
					uiFooterRightDiv.Style["width"] = Common.Settings.TakeoverFooterRightWidth.ToString() + "px";
					uiFooterRightDiv.Style["height"] = Common.Settings.TakeoverFooterRightHeight.ToString() + "px";

				}

				if (Common.Settings.TakeoverFooterLeftStatus == Common.Settings.TakeoverFooterLeftStatusOption.On)
				{
					uiFooterLeftDiv.Visible = true;
					uiFooterLeftDiv.InnerHtml = Common.Settings.TakeoverFooterLeftHtml;
					uiFooterLeftDiv.Style["width"] = Common.Settings.TakeoverFooterLeftWidth.ToString() + "px";
					uiFooterLeftDiv.Style["height"] = Common.Settings.TakeoverFooterLeftHeight.ToString() + "px";
				}


				if (Common.Settings.TakeoverLogoStatus == Common.Settings.TakeoverLogoStatusOption.On)
				{
					LogoAnchor.Visible = false;
					LogoTakeoverPh.Controls.Clear();
					LogoTakeoverPh.Controls.Add(new LiteralControl(Common.Settings.TakeoverLogoHtml));
				}

			}
			#endregion

			#region DontStayIn Icon stuff (removed)
			//if (Common.Settings.DsiLogoIconOverrideOn && allowTakeoverAfterAgeCheck)
			//{
			//    try
			//    {
			//        DsiLogoAnchor.Visible = false;
			//        DsiLogoOverrideSpan.Visible = true;

			//        if (Common.Settings.DsiLogoIconOverrideImage.Trim().Length > 0)
			//        {
			//            DsiLogoIconImg.Src = Storage.Path(new Guid(Common.Settings.DsiLogoIconOverrideImage), Common.Settings.DsiLogoIconOverrideImageExtension);
			//        }
			//        if (Common.Settings.DsiLogoIconOverrideLink.Trim().Length > 0)
			//        {
			//            DsiLogoIconAnchor.HRef = Common.Settings.DsiLogoIconOverrideLink;
			//        }

			//        if (Common.Settings.DsiLogoTextOverrideImage.Trim().Length > 0)
			//        {
			//            DsiLogoTextImg.Src = Storage.Path(new Guid(Common.Settings.DsiLogoTextOverrideImage), Common.Settings.DsiLogoTextOverrideImageExtension);
			//        }
			//        if (Common.Settings.DsiLogoTextOverrideLink.Trim().Length > 0)
			//        {
			//            DsiLogoTextAnchor.HRef = Common.Settings.DsiLogoTextOverrideLink;
			//        }
			//    }
			//    catch
			//    {
			//        DsiLogoAnchor.Visible = true;
			//        DsiLogoOverrideSpan.Visible = false;
			//    }
			//}
			//else
			//{
			//    DsiLogoAnchor.Visible = true;
			//    DsiLogoOverrideSpan.Visible = false;
			//}
			#endregion

			#region SalesUsrAlarmPanel
			// We want to check for new alarm as frequently as possible: On page_load, on postback, etc. but if an alarm is already visible, we cant reload the alarm panel because that would clear out any actions done in that panel
			// So its not perfect, but it will check for new alarms on all new page hits and almost every page refresh and still capture actions done within the panel.
			if (!this.SalesUsrAlarmPanel.Visible)
				SalesUsrAlarmPanelSetup();
			#endregion

			#region resize stuff for IE
			if (Vars.IE)
			{
				//HotboxOuterDiv.Style["width"] = "302px";
				HotboxInnerDiv.Style["width"] = "302px";
				HotboxInnerDiv.Style["height"] = "252px";
				//SkyscraperInnerDiv.Style["width"] = "162px";
				//SkyscraperInnerDiv.Style["height"] = "602px";
			}
			else
			{
				//HotboxOuterDiv.Style["width"] = "300px";
				HotboxInnerDiv.Style["width"] = "300px";
				HotboxInnerDiv.Style["height"] = "250px";

				//SkyscraperInnerDiv.Style["height"] = "600px";
			}
			#endregion

			#region SpottedScript
			if (ScriptManager.GetCurrent(Page) == null || !ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
			{
#if DEBUG
				HeaderScriptPlaceHolder.Controls.Add(new LiteralControl(JsLinkCreator.GetRegisterScriptHtml(@"/misc/SpottedScript/spottedscript.debug.js")));
#else
				HeaderScriptPlaceHolder.Controls.Add(new LiteralControl(JsLinkCreator.GetRegisterScriptHtml(@"/misc/SpottedScript/spottedscript.js")));
#endif
			}
			#endregion
		}
		#endregion

		#region BindSalesCalls()
		public void BindSalesCalls()
		{
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				Query q = new Query();
				q.QueryCondition = new And(new Q(SalesCall.Columns.UsrK, Usr.Current.K),
										   new Q(SalesCall.Columns.IsCall, true),
										   new Or(new Q(SalesCall.Columns.InProgress, true),
												  new Q(SalesCall.Columns.Dismissed, false)));
				SalesCallSet scs = new SalesCallSet(q);
				SalesCallPanel.Controls.Clear();
				SalesCallPanel.Visible = scs.Count > 0;
				if (scs.Count > 0)
				{
					foreach (SalesCall sc in scs)
					{
						Spotted.Controls.Admin.SalesCallControl c = (Spotted.Controls.Admin.SalesCallControl)this.LoadControl("~/Controls/Admin/SalesCallControl.ascx");
						c.CurrentSalesCall = sc;
						SalesCallPanel.Controls.Add(c);
					}
				}
			}
			else
				SalesCallPanel.Visible = false;
		}
		#endregion

		#region ToggleAdmin
		protected void ToggleAdmin(object sender, EventArgs eventArgs)
		{
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				if (Prefs.Current["HideAdmin"] == 1)
					Prefs.Current["HideAdmin"] = 0;
				else
					Prefs.Current["HideAdmin"] = 1;
				Response.Redirect(Request.Url.PathAndQuery);
			}
		}
		#endregion

		#region ToggleBanners
		protected void ToggleBanners(object sender, EventArgs eventArgs)
		{
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				if (Prefs.HideBanners)
					Prefs.Current["HideBanners"] = 0;
				else
					Prefs.Current["HideBanners"] = 1;
				Response.Redirect(Request.Url.PathAndQuery);
			}
		}
		#endregion

		#region ToggleChat
		protected void ToggleChat(object sender, EventArgs eventArgs)
		{
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				if (Prefs.Current["HideChat1"] != 0)
					Prefs.Current["HideChat1"] = 0;
				else
					Prefs.Current["HideChat1"] = 1;
				Response.Redirect(Request.Url.PathAndQuery);
			}
		}
		#endregion

		#region ToggleCanvas
		protected void ToggleCanvas(object sender, EventArgs eventArgs)
		{
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				if (Prefs.Current["FacebookCanvasMode1"] != 0)
					Prefs.Current["FacebookCanvasMode1"] = 0;
				else
					Prefs.Current["FacebookCanvasMode1"] = 1;
				Response.Redirect(Request.Url.PathAndQuery);
			}
		}
		#endregion

		#region Mobile
		protected void Mobile(object sender, EventArgs eventArgs)
		{
			Prefs.Current["ForceMobile"] = "mobile";
			Response.Redirect("/");
		}
		#endregion

		#region Page_PreRender
		internal bool HideBottomVideoBox { get; set; }
		public void Page_PreRender(object o, System.EventArgs e)
		{
			if (Title.Length == 0)
				SetPageTitle(Url.PageName);

			PageTitleTag.Text = Title;

			ToggleAdminLinkButton.Visible = Usr.Current != null && Usr.Current.IsAdmin;
			ToggleBannersLinkButton.Visible = Usr.Current != null && Usr.Current.IsAdmin;
			ToggleChatLinkButton.Visible = Usr.Current != null && Usr.Current.IsAdmin;
			ToggleCanvasLinkButton.Visible = Usr.Current != null && Usr.Current.IsAdmin;
//			NavAdminOuter.Visible = false;// Usr.Current != null && Usr.Current.IsAdmin;


			UnrulyBottomBox.Visible = Common.Settings.BottomVideoBox == Common.Settings.BottomVideoBoxOption.On && !HideBottomVideoBox && !Vars.ClientIsDevBox;
			N3BottomBox.Visible = Common.Settings.BottomVideoBox == Common.Settings.BottomVideoBoxOption.On && !HideBottomVideoBox && !Vars.ClientIsDevBox;


			if (SslPage || (Vars.DevEnv && Url["ssl"].Exists))
			{
				//		Leaderboard.Visible = false;
				//		SkyscraperDiv.Visible = false;
				//		HotboxDiv.Visible = false;
//				GoogleAnalyticsPlaceholder.Visible = false;
				UnrulyBottomBox.Visible = false;
				N3BottomBox.Visible = false;
				//WhosOnlineHolder.Visible = false;
				//NavSpotterRankHolder.Visible = false;
				//NavRecentDonatorsHolder.Visible = false;
			}
			else
			{
				//NavSpotterRankHolder.Visible = (new Random().Next(2) == 0);
				//NavRecentDonatorsHolder.Visible = !NavSpotterRankHolder.Visible;
			}

			


			if (Prefs.Current["HideAdmin"] == 1)
			{
				Menu.Admin.Visible = false;
			}
			//new chat client (not finished!)
			ChatClientHolder.Visible = !this.UseLeftHandSideForContent && !Prefs.HideChat && Vars.UseNewChatClient;
			SkyscraperOuterDiv.Visible = !UseLeftHandSideForContent && !Prefs.HideBanners;
			//chat client - modify to turn off in devenv...
			LeaderboardOuterDiv.Visible = !Prefs.HideBanners;
			BigLeaderboardOuterDiv.Visible = !Prefs.HideBanners;
			if (UseLeftHandSideForContent || Prefs.HideBanners)
				HotboxOuterDiv.Visible = false;

			if (Prefs.HideBanners || UseLeftHandSideForContent)
			{
				UnrulyBottomBox.Visible = false;
				N3BottomBox.Visible = false;
			}

			if (UnrulyBottomBox.Visible && N3BottomBox.Visible)
			{
				int rnd = Common.ThreadSafeRandom.Next(0, 99);
				if (rnd < Common.Settings.PercentageOfBottomVideoBoxToUnruly)
				{
					UnrulyBottomBox.Visible = true;
					N3BottomBox.Visible = false;
				}
				else
				{
					UnrulyBottomBox.Visible = false;
					N3BottomBox.Visible = true;
				}
			}

			if (Vars.FacebookCanvasMode)
			{
				BodyMainBackgroundOuter.Style.Clear();
				BodyMainBackgroundInner.Style.Clear();

				ChatClientHolder.Visible = false;
				HotboxOuterDiv.Visible = false;

				LogoOuterDiv1.Visible = false;

				ContentDiv.Style["left"] = "10px";
				ContentDiv.Style["width"] = "620px";
				LeaderboardOuterDiv.Style["left"] = "10px";
				LeaderboardOuterDiv.Style["width"] = "728px";
				LeaderboardInnerDiv.Style["left"] = "6px";

				FacebookCanvasNavOuterDiv.Visible = true;
				HtmlTag.Style.Clear();
			}


			bool showBigLeaderboard = false;

			if (
				(Page.Request.Path == "/" || Page.Request.Path == "/pages/home") && 
				Common.Settings.WootBigLeaderboardFrontPageRoadblockStatus == Common.Settings.WootBigLeaderboardFrontPageRoadblockStatusOption.On
				)
			{
				showBigLeaderboard = true;
			}
			else if (
				Common.Settings.WootBigLeaderboardUkPercentage > 0 && 
				Visit.Current.CountryK == 224 && 
				Random.Next(100) < Common.Settings.WootBigLeaderboardUkPercentage
				)
			{
				bool bigLeaderboardOverFrequencyCap = false;
				int bigLeaderboardHitsToday = 0;
				if (Common.Settings.WootBigLeaderboardUkFrequencyCap > 0)
				{
					
					DateTime lastBigLeaderboardHit = DateTime.MinValue;
					if (Prefs.Current["lastBigLeaderboardHit"].Exists)
					{
						lastBigLeaderboardHit = new DateTime(long.Parse(Prefs.Current["lastBigLeaderboardHit"].Value)).Date;

						if (lastBigLeaderboardHit == DateTime.Today && Prefs.Current["bigLeaderboardHits"].Exists)
						{
							bigLeaderboardHitsToday = int.Parse(Prefs.Current["bigLeaderboardHits"].Value);
							bigLeaderboardOverFrequencyCap = bigLeaderboardHitsToday > Common.Settings.WootBigLeaderboardUkFrequencyCap;
						}
					}
				}

				if (!bigLeaderboardOverFrequencyCap)
				{
					showBigLeaderboard = true;

					Prefs.Current["lastBigLeaderboardHit"] = DateTime.Today.Ticks.ToString();
					Prefs.Current["bigLeaderboardHits"] = bigLeaderboardHitsToday + 1;
				}
			}
			else if (Page.Request.QueryString["bannertest"] != null)
			{
				showBigLeaderboard = true;
			}
			
			BigLeaderboardOuterDiv.Visible = false;
			if (ShowLinkedWootUkBanner && Common.Settings.WootLinkedBannerType == Common.Settings.WootLinkedBannerTypeOption.BigLeaderboard)
			{
				LeaderboardOuterDiv.Visible = false;
				BigLeaderboardOuterDiv.Visible = true;
				BigLeaderboardInnerDiv.InnerHtml = Common.Settings.WootUkLinkedBigLeaderboardTagHtml;
			}
			else if (showBigLeaderboard && !ShowLinkedWootUkBanner)
			{
				LeaderboardOuterDiv.Visible = false;
				BigLeaderboardOuterDiv.Visible = true;
				BigLeaderboardInnerDiv.InnerHtml = Common.Settings.WootBigLeaderboardTagHtml;
			}

			AuthCookieHasError.Value = Usr.CurrentAuthCookieHasError().ToString();

			if (!ShowLinkedWootUkBanner && ShowTallMpu && !UseLeftHandSideForContent && !Prefs.HideBanners)
			{
				HotboxOuterDiv.Visible = false;
				SkyscraperOuterDiv.Visible = false;
				TallMpuOuterDiv.Visible = true;
				TallMpuInnerDiv.InnerHtml = Common.Settings.TallMpuTagHtml;
			}
			


		}
		protected HtmlGenericControl BodyMainBackgroundOuter, BodyMainBackgroundInner, LogoOuterDiv1, FacebookCanvasNavOuterDiv;
		#endregion

		#region ShowLinkedWootUkBanner
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
							(GlobalRandomDouble < trafficFraction) &&
							(!Vars.UrlScheme.Equals("https"));
						return result;
					}
				}
				catch { }

				return false;
			}
		}
		#endregion

		#region ShowTallMpu
		bool ShowTallMpu
		{
			get
			{
				try
				{
					if (Country.ClientCountryK == 225 || Vars.DevEnv)
					{
						double trafficFraction = (double)Common.Settings.TallMpuPercentage / 100.0;
						bool result =
							!ShowLinkedWootUkBanner &&
							(GlobalRandomDouble < trafficFraction) &&
							(!Vars.UrlScheme.Equals("https"));
						return result;
					}
				}
				catch { }

				return false;
			}
		}
		#endregion
	
		#region Render
		protected override void Render(HtmlTextWriter writer)
		{
			
			NavigationMove();
			base.Render(writer);



			if (!Url.IsAjaxRequest)
			{
				Log.Increment(Log.Items.DsiPageRender);
			}

			if (!Url.IsAjaxRequest && !Visit.Current.IsCrawler)
			{
				Log.Increment(Log.Items.DsiPageRenderNoCrawlers);
			}

		}
		#endregion

		#region WriteToTopRightHandCorner
		int topRightHandCornerPosition = 5;
		void WriteToTopRightHandCorner(string text)
		{
			if (this.Url.IsAjaxRequest || Visit.Current.IsCrawler)
				return;
	
			Response.Write("<small style=\"position:absolute;top:" + topRightHandCornerPosition.ToString() + "px;left:950px;font-weight:bold;\">" + text + "</small>");
			topRightHandCornerPosition += 20;
		}
		#endregion

		#region Top navigation
		protected Controls.Banners.Generator Leaderboard;
		#region TopNavigation_Load
		protected void TopNavigation_Load(object sender, EventArgs eventArgs)
		{
			Leaderboard.Visible = true;
		}
		#endregion

		#endregion

		#region Above content
		public Controls.Login NavLogin;
		public Panel PromoterStuckPanel;
		protected Panel
			AutoLoginFailedPanel,
			PendingAbusePanel;
		#region AboveContent_Init
		public void AboveContent_Init(object o, System.EventArgs e)
		{

		}
		#endregion
		#region AboveContent_Load
		protected void AboveContent_Load(object sender, EventArgs eventArgs)
		{
			#region Promoter Panels
			if (Usr.Current != null && Usr.Current.IsEnabledPromoter())
			{
				if (Prefs.Current["HideStuck"].Exists && Prefs.Current["HideStuck"] == 1)
					PromoterStuckPanel.Visible = false;
				else
					PromoterStuckPanel.Visible = true;
			}
			else
			{
				PromoterStuckPanel.Visible = false;
			}
			#endregion
		}
		#endregion
		#region AboveContent_PreRender
		protected void AboveContent_PreRender(object sender, EventArgs eventArgs)
		{
			if (Usr.Current != null && Usr.Current.IsSenior)
			{
				Bobs.Global g = new Bobs.Global(Bobs.Global.Records.PendingPhotoAbuseReports);
				if (g.ValueInt > 0)
					PendingAbusePanel.Visible = true;
				else
					PendingAbusePanel.Visible = false;
			}
		}
		#endregion
		#region PromoterStuckPanel_Hide
		public void PromoterStuckPanel_Hide(object o, System.EventArgs e)
		{
			Prefs.Current["HideStuck"] = 1;
			PromoterStuckPanel.Visible = false;
		}
		#endregion
		#region ActivitiesPanel
		protected Panel ActivitiesPanel;
		protected HtmlGenericControl ActivityPicture, ActivityDj, ActivityPlaces, ActivityMusic, ActivityHomeTown, ActivityFavouriteMusic, ActivityEmailVerify;

		public void ActivitiesPanel_PreRender(object o, System.EventArgs e)
		{
			if (Usr.Current != null)
			{
				
				if (Prefs.Current["SkipActivityMusic"].IsNull)
				{
					if ((Prefs.Current["SeenMusic"].IsNull && Usr.Current.MusicTypesFavouriteCount == 1) || Usr.Current.MusicTypesFavouriteCount == 0)
						ActivityMusic.Visible = true;
					else
					{
						ActivityMusic.Visible = false;
						Prefs.Current["SkipActivityMusic"] = "1";
					}
				}
				else
					ActivityMusic.Visible = false;

				if (Prefs.Current["SkipActivityPlaces"].IsNull)
				{
					if ((Prefs.Current["SeenVisit"].IsNull && Usr.Current.PlacesVisitCount == 1) || Usr.Current.PlacesVisitCount == 0)
						ActivityPlaces.Visible = true;
					else
					{
						ActivityPlaces.Visible = false;
						Prefs.Current["SkipActivityPlaces"] = 1;
					}
				}
				else
					ActivityPlaces.Visible = false;

				if (Prefs.Current["SkipActivityPicture"].IsNull)
				{
					if (!Usr.Current.HasPic)
					{
						if (Usr.Current.PhotosMeCount > 0)
							ActivityPicture.Visible = true;
						else
							ActivityPicture.Visible = false;
					}
					else
					{
						ActivityPicture.Visible = false;
						Prefs.Current["SkipActivityPicture"] = 1;
					}
				}
				else
					ActivityPicture.Visible = false;

				ShowTicketFeedbackToDo();
				ActivityBrokenEmail.Visible = Usr.Current.IsEmailBroken;
				ActivityDj.Visible = !Usr.Current.IsDj.HasValue;
				ActivityEmailVerify.Visible = !Usr.Current.IsEmailVerified;
				ActivityMixmag.Visible = Prefs.Current["ActivityMixmagDone"].IsNull;
				ActivityDetails.Visible = (!Usr.Current.IsMale && !Usr.Current.IsFemale) || Usr.Current.DateOfBirth.Equals(DateTime.MinValue) || Usr.Current.HomePlaceK == 0 || Usr.Current.FavouriteMusicTypeK == 0;
				ActivityBuddyImporter.Visible = Prefs.Current["UsedBuddyImporter"].IsNull;
				ActivityExDirectoryOption.Visible = Prefs.Current["SetExDirectoryOption"].IsNull;

				showHideActivitiesPanel();

			}
			else
				ActivitiesPanel.Visible = false;

		}

		void showHideActivitiesPanel()
		{
			ActivitiesPanel.Visible =
					ActivityBrokenEmail.Visible ||
					ActivityDetails.Visible ||
					ActivityDj.Visible ||
					ActivityEmailVerify.Visible ||
					ActivityMusic.Visible ||
					ActivityPlaces.Visible ||
					ActivityPicture.Visible ||
					ActivityMixmag.Visible ||
					ActivityTicketFeedback.Visible ||
					ActivityBuddyImporter.Visible ||
					ActivityExDirectoryOption.Visible;
		}

		public void ShowTicketFeedbackToDo()
		{
			ActivityTicketFeedback.Visible = false;
			if (Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_NEXT_DATE_KEY].IsNull)
			{
				// Must run once for each user to store their existing ticket feedback details.
				Usr.Current.SetPrefsNextTicketFeedbackDate();
			}

			if (!Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_NEXT_DATE_KEY].IsNull)
			{
				DateTime dt = Convert.ToDateTime(Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_NEXT_DATE_KEY].Value);
				if (dt < DateTime.Today && dt > DateTime.Today.AddDays(Vars.DAYS_TO_SHOW_TICKET_FEEDBACK_ALERT))
				{
					string eventLinks = Usr.Current.GetPrefsTicketFeedbackLinks();

					if (eventLinks.Length > 0)
					{
						ActivityTicketFeedback.Visible = true;
						ActivityTicketFeedback.InnerHtml = "Ticket feedback for: " + eventLinks;
					}
				}
			}

			showHideActivitiesPanel();
		}
		#endregion
		#endregion

		#region SalesUsr Alarm Panel
		private void SalesUsrAlarmPanelSetup()
		{
			if (Usr.Current != null && Usr.Current.SalesTeam > 0)
			{
				Query promoterAlarmQuery = new Query(new And(new Q(Promoter.Columns.Alarm, true),
															 new Q(Promoter.Columns.SalesNextCall, QueryOperator.LessThanOrEqualTo, DateTime.Now),
															 new Q(Promoter.Columns.SalesUsrK, Usr.Current.K)));
				promoterAlarmQuery.Columns = new ColumnSet(Promoter.Columns.K, Promoter.Columns.Name, Promoter.Columns.UrlName, Promoter.Columns.SalesNextCall, Promoter.Columns.Alarm);
				promoterAlarmQuery.OrderBy = new OrderBy(Promoter.Columns.SalesNextCall);
				PromoterSet alarmedPromoters = new PromoterSet(promoterAlarmQuery);

				this.SalesUsrAlarmPanel.Visible = alarmedPromoters.Count > 0;
				if (alarmedPromoters.Count > 0)
				{
					this.SalesUsrAlarmGridView.ShowFooter = alarmedPromoters.Count > 1;
					this.SalesUsrAlarmGridView.DataSource = alarmedPromoters;
					this.SalesUsrAlarmGridView.DataBind();
					if (alarmedPromoters.Count > 10)
					{
						SalesUsrAlarmPanelInner.Style["height"] = "300px";
						SalesUsrAlarmPanelInner.Style["overflow"] = "auto";
					}
				}
			}
			else
			{
				SalesUsrAlarmPanel.Visible = false;
			}
		}

		protected void SnoozeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList dropDownList = (DropDownList)sender;
			if (dropDownList.SelectedValue != "")
			{
				GridViewRow row = (GridViewRow)dropDownList.NamingContainer;
				Promoter promoter = new Promoter(Convert.ToInt32(((Label)row.FindControl("PromoterKLabel")).Text));
				promoter.SalesNextCall = DateTime.Now.AddMinutes(Convert.ToInt32(dropDownList.SelectedValue));
				promoter.Alarm = true;
				promoter.Update();
				SalesUsrAlarmPanelSetup();
			}
		}

		protected void SnoozeAllDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList dropDownList = (DropDownList)sender;
			if (dropDownList.SelectedValue != "")
			{
				//GridViewRow row = (GridViewRow)dropDownList.NamingContainer;
				foreach (GridViewRow row in this.SalesUsrAlarmGridView.Rows)
				{
					Promoter promoter = new Promoter(Convert.ToInt32(((Label)row.FindControl("PromoterKLabel")).Text));
					promoter.SalesNextCall = DateTime.Now.AddMinutes(Convert.ToInt32(dropDownList.SelectedValue));
					promoter.Alarm = true;
					promoter.Update();
				}
				SalesUsrAlarmPanelSetup();
			}
		}

		protected void SalesUsrAlarmGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (e.Row.FindControl("DeleteLinkButton") != null)
				{
					LinkButton removeAlarmLinkButton = (LinkButton)e.Row.FindControl("DeleteLinkButton");
					removeAlarmLinkButton.Attributes.Remove("onclick");
					removeAlarmLinkButton.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to turn this alarm off?')");
				}
				if (e.Row.FindControl("SnoozeDropDownList") != null)
				{
					DropDownList snoozeAlarmDropDownList = (DropDownList)e.Row.FindControl("SnoozeDropDownList");
					snoozeAlarmDropDownList.Attributes.Remove("onchange");
					snoozeAlarmDropDownList.Attributes.Add("onchange", "javascript:if(confirm('Are you sure you want to snooze this alarm?')){__doPostBack('" + snoozeAlarmDropDownList.UniqueID + "','');return false;}else{return false;};");
				}
			}
			else if (e.Row.RowType == DataControlRowType.Footer)
			{
				if (e.Row.FindControl("SnoozeAllDropDownList") != null)
				{
					DropDownList snoozeAllAlarmDropDownList = (DropDownList)e.Row.FindControl("SnoozeAllDropDownList");
					snoozeAllAlarmDropDownList.Attributes.Remove("onchange");
					snoozeAllAlarmDropDownList.Attributes.Add("onchange", "javascript:if(confirm('Are you sure you want to snooze ALL alarms?')){__doPostBack('" + snoozeAllAlarmDropDownList.UniqueID + "','');return false;}else{return false;};");
				}
				if (e.Row.FindControl("DeleteAllLinkButton") != null)
				{
					LinkButton removeAllAlarmLinkButton = (LinkButton)e.Row.FindControl("DeleteAllLinkButton");
					removeAllAlarmLinkButton.Attributes.Remove("onclick");
					removeAllAlarmLinkButton.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to turn ALL alarms off?')");
				}
			}
		}

		protected void SalesUsrAlarmGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				Promoter promoter = new Promoter(Convert.ToInt32(((Label)this.SalesUsrAlarmGridView.Rows[e.RowIndex].FindControl("PromoterKLabel")).Text));
				promoter.Alarm = false;
				promoter.Update();
				SalesUsrAlarmPanelSetup();
			}
			catch
			{ }
		}

		protected void SalesUsrAlarmGridView_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName.ToUpper().Equals("DELETEALL"))
			{
				try
				{
					//GridViewRow row = (GridViewRow)dropDownList.NamingContainer;
					foreach (GridViewRow row in this.SalesUsrAlarmGridView.Rows)
					{
						Promoter promoter = new Promoter(Convert.ToInt32(((Label)row.FindControl("PromoterKLabel")).Text));
						promoter.Alarm = false;
						promoter.Update();
					}
					SalesUsrAlarmPanelSetup();
				}
				catch
				{ }
			}
		}



		#endregion

		#region Content
		public HtmlGenericControl ContentDiv;
		#region ContentUserControl
		public DsiUserControl ContentUserControl
		{
			get
			{
				return (DsiUserControl)GenericUserControl;
			}
		}
		#endregion
		#endregion

		#region Left navigation
		protected Controls.Banners.Generator Hotbox;

		public Panel HotboxDiv;
		protected Controls.Banners.Generator Skyscraper;
		#region Navigation_Load
		
		protected void Navigation_Load(object sender, EventArgs eventArgs)
		{
			Hotbox.Holder = HotboxOuterDiv;
			Skyscraper.Holder = SkyscraperHolder;
			#region Brand controls in admin navigation
			if (Url.HasBrandLogicalFilter)
			{
				if (Usr.Current != null && Usr.Current.IsAdmin)
				{
					Menu.Admin.AdminPanelContents.Controls.Add(new LiteralControl("<p><b>Brand: " + Url.LogicalFilterBrand.Name + "</b><br>"));
					if (Url.LogicalFilterBrand.Promoter != null)
					{
						Menu.Admin.AdminPanelContents.Controls.Add(new LiteralControl("Promoter:<br><a href=\"" + Url.LogicalFilterBrand.Promoter.Url() + "\">" + Url.LogicalFilterBrand.Promoter.Name + "</a>"));
						if (Url.LogicalFilterBrand.PromoterStatus.Equals(Brand.PromoterStatusEnum.Unconfirmed))
							Menu.Admin.AdminPanelContents.Controls.Add(new LiteralControl("<br><font color=0000ff><b>Status unconfirmed</b></font>"));
						Menu.Admin.AdminPanelContents.Controls.Add(new LiteralControl("<br>"));
					}
					Menu.Admin.AdminPanelContents.Controls.Add(new LiteralControl("<a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/brand?ID=" + Url.LogicalFilterBrand.K.ToString() + "\">Edit this brand (admin)</a>"));
					Menu.Admin.AdminPanelContents.Controls.Add(new LiteralControl("<br><a href=\"" + Url.LogicalFilterBrand.UrlApp("edit") + "\">Edit this brand (mod)</a>"));
					Menu.Admin.AdminPanelContents.Controls.Add(new LiteralControl("</p>"));
				}
				if (Usr.Current != null && Usr.Current.IsSuper)
				{
					Menu.Admin.SuperAdmin.Controls.Add(new LiteralControl("<p><a href=\"" + Url.LogicalFilterBrand.UrlApp("edit") + "\">Edit this brand</a></p>"));
				}
			}
			#endregion

			#region Group controls in admin navigation
			if (Url.HasGroupLogicalFilter)
			{
				if (Usr.Current != null && Usr.Current.IsAdmin)
				{
					Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><b>Group: " + Url.LogicalFilterGroup.FriendlyName + "</b><br>"));
					Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<b>GroupK: " + Url.LogicalFilterGroup.K + "</b><br>"));
					Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<a href=\"" + Url.LogicalFilterGroup.UrlApp("admin") + "\">Group options</a></p>"));
				}
			}
			#endregion

		}
		#endregion
		#endregion

		#region NavigationMove
		public bool ContentHasNoTitleAtTop = false;
		protected void NavigationMove()
		{

			//foreach (Control c in TopContentHolder.Controls)
			//{
			//    c.Visible = true;
			//}

			bool topContentIsVisible = false;
			foreach (Control c in TopContentHolder.Controls)
			{
				if (c.Visible && !(c is LiteralControl))
				{
					topContentIsVisible = true;
					break;
				}
			}


			int hotboxHeight = 320;
			int tallMpuHeight = 670;
			int leaderboardHeight = 122;
			int bigLeaderboardHeight = 282;
			//XMAS
			//int skyscraperHeight = 632;

			int leftTop = 68;
			int rightTop = 68;

			LeaderboardOuterDiv.Style["Top"] = leftTop.ToString() + "px";
			BigLeaderboardOuterDiv.Style["Top"] = leftTop.ToString() + "px";

			if (LeaderboardOuterDiv.Visible)
			{
				leftTop += leaderboardHeight;
				rightTop += leaderboardHeight;
			}

			if (BigLeaderboardOuterDiv.Visible)
			{
				leftTop += bigLeaderboardHeight;
				rightTop += bigLeaderboardHeight;
			}

			if ((HotboxOuterDiv.Visible || TallMpuOuterDiv.Visible) && (topContentIsVisible || !ContentHasNoTitleAtTop))
				rightTop += 27;
			else if (!(HotboxOuterDiv.Visible || TallMpuOuterDiv.Visible) && (!topContentIsVisible && ContentHasNoTitleAtTop))
				leftTop += 27;

			ContentDiv.Style["Top"] = leftTop.ToString() + "px";
			NavLoginDiv.Style["Top"] = leftTop.ToString() + "px";

			TallMpuOuterDiv.Style["Top"] = rightTop.ToString() + "px";
			HotboxOuterDiv.Style["Top"] = rightTop.ToString() + "px";
			FacebookCanvasNavOuterDiv.Style["Top"] = rightTop.ToString() + "px";

			if (HotboxOuterDiv.Visible)
				rightTop += hotboxHeight;

			if (TallMpuOuterDiv.Visible)
				rightTop += tallMpuHeight;

			ChatClientHolder.Style["Top"] = rightTop.ToString() + "px";

			//NavAdminOuter.Style["Top"] = rightTop.ToString() + "px";
			
			//if (ChatClientHolder.Visible)
			//	leftTop += chatClientHeight;

			//NavAdminOuter.Style["top"] = rightTop.ToString() + "px";
			
		}
		#endregion

		#region IRelevanceHolder
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
		public void RelevantPlacesAdd(int placeK)
		{
			if (!RelevantPlaces.Contains(placeK))
			{
				RelevantPlaces.Add(placeK);
				if (Usr.Current == null)
				{
					Identity.Current.AddPlaceVisited(placeK);
				}
			}
		}
		public void RelevantMusicAdd(int musicTypeK)
		{
			if (!RelevantMusic.Contains(musicTypeK))
			{
				RelevantMusic.Add(musicTypeK);
				if (Usr.Current == null)
				{
					Identity.Current.AddFavouriteMusicType(musicTypeK);
				}
			}
		}
		#region BannerRequestRules
		/// <summary>
		/// Get rules based on context infos of Current Page
		/// </summary>
		public Bobs.BannerServer.Rules.RequestRules BannerRequestRules
		{
			get
			{
				Bobs.BannerServer.Rules.RequestRules rules = new Bobs.BannerServer.Rules.RequestRules();
				foreach (int k in RelevantMusic)
				{
					rules.MusicTypes.Add(k);
				}
				foreach (int k in RelevantPlaces)
				{
					rules.PlacesVisited.Add(k);
				}
				return rules;
			}
		}
		#endregion
		#endregion

		#region AnchorSkip
		public Panel AnchorSkipJs;
		public PlaceHolder AnchorSkipName;
		public void AnchorSkip(string AnchorName)
		{
			AnchorSkipName.Controls.Clear();
			AnchorSkipName.Controls.Add(new LiteralControl(AnchorName));
			AnchorSkipJs.Visible = true;
		}
		#endregion

		#region SetPageTitle
		public void SetPageTitle(string title)
		{
			//	Local.History.RenameCurrentPage(title);
			Title = title + " - DontStayIn";
		}
		public void SetPageTitle(string title, string historyName)
		{
			//	Local.History.RenameCurrentPage(historyName);
			Title = title + " - DontStayIn";
		}
		#endregion

		#region OverrideUsrRedirect
		public bool OverrideUsrRedirect
		{
			get
			{
				if (this.ViewState["OverrideUsrRedirect"] != null)
					overrideUsrRedirect = Convert.ToBoolean(this.ViewState["OverrideUsrRedirect"]);
				return overrideUsrRedirect;
			}
			set
			{
				this.ViewState["OverrideUsrRedirect"] = value;
			}
		}
		private bool overrideUsrRedirect = false;

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Init += new System.EventHandler(this.AboveContent_Init);
			this.Load += new System.EventHandler(this.TopNavigation_Load);
			this.Load += new System.EventHandler(this.AboveContent_Load);
			this.Load += new System.EventHandler(this.Navigation_Load);

			this.PreRender += new System.EventHandler(this.ActivitiesPanel_PreRender);
			this.PreRender += new System.EventHandler(this.AboveContent_PreRender);
			this.PreRender += new EventHandler(DsiPage_PreRender);
		}

		void DsiPage_PreRender(object sender, EventArgs e)
		{
			if (this.UseLeftHandSideForContent)
			{
				ContentDiv.Style["left"] = "0px";
				ContentDiv.Style["width"] = "983px";
			}
		}
		#endregion

	}
}
