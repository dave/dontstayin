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
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Spotted.Pages
{
	public partial class Home : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			ContainerPage.SetPageTitle("Home");
			ContainerPage.ContentHasNoTitleAtTop = true;


			Query q = new Query();
			q.Columns = Templates.Articles.LatestNew.Columns;
			q.TableElement = Templates.Articles.LatestNew.PerformJoins(q.TableElement);
			q.QueryCondition = new And(
				Article.EnabledQueryCondition,
				new Q(Article.Columns.Relevance, Article.RelevanceEnum.Worldwide),
				new And(new Q(Article.Columns.ShowAboveFoldOnFrontPage, true), new Q(Article.Columns.EnabledDateTime, QueryOperator.GreaterThan, DateTime.Now.AddHours(-48)))
			);
			q.OrderBy = new OrderBy(Article.Columns.EnabledDateTime, OrderBy.OrderDirection.Descending);
			ArticleSet ars = new ArticleSet(q);

			if (ars.Count == 0)
			{
				NewArticlesPanel.Visible = false;
			}
			else
			{
			
				NewArticlesDataList.DataSource = ars;
				NewArticlesDataList.ItemTemplate = this.LoadTemplate("/Templates/Articles/LatestNew.ascx");
				NewArticlesDataList.DataBind();

			}


			Cambro.Web.Helpers.TieButton(SpotterCode, SpotterCodeButton);
			if (Vars.IE)
			{
				SpotterCodeButton.Style["height"] = "23px";
				SpotterCodeButton.Style["font-size"] = "12px";
				SpotterCodeButton.Style["margin-left"] = "4px";
			}
			else
			{
				SpotterCodeButton.Style["height"] = "24px";
				SpotterCodeButton.Style["font-size"] = "14px";
				SpotterCodeButton.Style["margin-left"] = "0px";
			}



			
            bool allowTakeoverAfterAgeCheck = true;
			if (Common.Settings.TakeoverRequiresDrinkingAgeVerificationStatus == Common.Settings.TakeoverRequiresDrinkingAgeVerificationStatusOption.On)
			{
				if (Prefs.Current["Drink"] != 1)
				{
					allowTakeoverAfterAgeCheck = false;
				}
			}
			if (allowTakeoverAfterAgeCheck)
			{
				FrontPageBannerPh.Controls.Clear();
				FrontPageBannerPh.Controls.Add(new LiteralControl(Common.Settings.FrontPageBannerHtmlHtml));
			}

			try
			{
				bool rb1 = Common.Settings.FrontPageRoadblock1Status == Common.Settings.FrontPageRoadblock1StatusOption.On;
				bool rb2 = Common.Settings.FrontPageRoadblock2Status == Common.Settings.FrontPageRoadblock2StatusOption.On;
				bool rb3 = Common.Settings.FrontPageRoadblock3Status == Common.Settings.FrontPageRoadblock3StatusOption.On;
				int w1 = Common.Settings.FrontPageRoadblock1Weight;
				int w2 = Common.Settings.FrontPageRoadblock2Weight;
				int w3 = Common.Settings.FrontPageRoadblock3Weight;
				
				//we have a roadblock to show...
				List<string> alwaysOn = new List<string>();
				Dictionary<string, int> roadblocks = new Dictionary<string, int>();
				int totalWeight = 0;

				if (rb1 && w1 == 0)
					alwaysOn.Add(Common.Settings.FrontPageRoadblock1Html);
				else if (rb1)
				{
					roadblocks.Add(Common.Settings.FrontPageRoadblock1Html, w1);
					totalWeight += w1;
				}

				if (rb2 && w2 == 0)
					alwaysOn.Add(Common.Settings.FrontPageRoadblock2Html);
				else if (rb2)
				{
					roadblocks.Add(Common.Settings.FrontPageRoadblock2Html, w2);
					totalWeight += w2;
				}

				if (rb3 && w3 == 0)
					alwaysOn.Add(Common.Settings.FrontPageRoadblock3Html);
				else if (rb3)
				{
					roadblocks.Add(Common.Settings.FrontPageRoadblock3Html, w3);
					totalWeight += w3;
				}

				Random r = new Random();

				if (alwaysOn.Count > 1)
				{
					int index = r.Next(alwaysOn.Count);
					renderRoadblock(alwaysOn[index]);
				}
				else if (alwaysOn.Count == 1)
				{
					renderRoadblock(alwaysOn[0]);
				}
				else
				{
					int chosenRnd = r.Next(10 + totalWeight);

					if (chosenRnd < 10)
					{
						Photo p = FrontPagePhotos[chosenRnd];
						PhotoAnchor.HRef = p.Url();
						PhotoImg.Src = Storage.Path(p.FrontPagePic.Value);
						PhotoCaption.InnerHtml = p.PhotoOfWeekCaption;
						PhotoCaption.Attributes["class"] = "PhotoOverlay " + p.FrontPageCaptionClass;
						PhotoSpotterLink.InnerHtml = p.Usr.NickName;
						PhotoSpotterLink.HRef = p.Usr.Url();

						string color = p.FrontPageCaptionClass.StartsWith("White") ? "White" : "Black";
						string topBottom = p.FrontPageCaptionClass.Contains("Bottom") ? "Bottom" : "Top";
						string topBottomOpposite = p.FrontPageCaptionClass.Contains("Bottom") ? "Top" : "Bottom";
						string leftRight = p.FrontPageCaptionClass.Contains("Left") ? "Left" : "Right";
						string leftRightOpposite = p.FrontPageCaptionClass.Contains("Left") ? "Right" : "Left";

						PhotoSoptterHolder.Attributes["class"] = "PhotoOverlay " + color + " " + topBottom + " " + leftRightOpposite;
						PhotoLinksHolder.Attributes["class"] = "PhotoOverlay " + color + " " + topBottomOpposite + " Left";
						TopPhotoArchiveHolder.Attributes["class"] = "PhotoOverlay " + color + " " + topBottomOpposite + " Right";

					}
					else
					{

						chosenRnd = chosenRnd - 10;
						for (int i = 0; i < roadblocks.Count; i++)
						{
							chosenRnd = chosenRnd - roadblocks.ToArray()[i].Value;
							if (chosenRnd <= 0)
							{
								renderRoadblock(roadblocks.ToArray()[i].Key);
								break;
							}
						}
					}
				}

				
			}
			catch { }

			try
			{
				StringBuilder sb = new StringBuilder();
				foreach (Photo p in FrontPagePhotos)
				{
					string s = "";
					string s1 = "";
					if (Usr.Current != null && Usr.Current.IsAdmin)
					{
						s = "stt('" + Cambro.Misc.Utility.FriendlyDate(p.PhotoOfWeekDateTime) + "');";
						s1 = "onmouseout=\"htm();\"";
					}
					sb.Append(@"
<div style=""float:left; margin-top:3px; margin-bottom:3px;" + (sb.Length == 0 ? "" : " margin-left:5px;") + @""">
	<img src=""" + p.IconPath + @""" width=""30"" height=""30"" class=""Block"" onmouseover=""" + s + @"TopPhotoChangeImage('" + Storage.Path(p.FrontPagePic.Value) + @"', '" + p.Url() + @"', '" + Cambro.Misc.Utility.JsStringEncode(p.PhotoOfWeekCaption) + @"', '" + p.Usr.NickName + @"');return false;"" " + s1 + @" />
</div>
");
				}
				PhotoLinksPh.Controls.Clear();
				PhotoLinksPh.Controls.Add(new LiteralControl(sb.ToString()));
			}
			catch { }
			
		}
		PhotoSet FrontPagePhotos
		{
			get
			{
				if (frontPagePhotos == null)
				{
					Query q = new Query();
					q.QueryCondition = new Q(Photo.Columns.PhotoOfWeek, true);
					q.OrderBy = new OrderBy(Photo.Columns.PhotoOfWeekDateTime, OrderBy.OrderDirection.Descending);
					q.TopRecords = 10;
					frontPagePhotos = new PhotoSet(q);
				}
				return frontPagePhotos;
			}
		}
		PhotoSet frontPagePhotos = null;


		void renderRoadblock(string html)
		{
			TopPhotoHolder.Visible = false;

			HtmlRenderer r = new HtmlRenderer();
			r.LoadHtml(html);
			r.Container = false;
			r.Formatting = false;

			RoadblockPh.Controls.Clear();
			RoadblockPh.Controls.Add(new LiteralControl(r.Render(RoadblockPh)));
		}

		protected void SpotterCodeClick(object o, EventArgs e)
		{
			try
			{
				int k = int.Parse(SpotterCode.Text.Replace("-", string.Empty));
				Usr u = new Usr(k);
				if (u.IsSkeleton)
					SpotterCodeError.Visible = true;
				else
					Response.Redirect(u.UrlApp("mygalleries"));
			}
			catch
			{
				SpotterCodeError.Visible = true;
			}
		}



		//void renderTabs()
		//{
		//    ControlPanelTab.Attributes["class"] = !Prefs.Current["FrontPageSelectedTab"].Exists || Prefs.Current["FrontPageSelectedTab"] == "ControlPanelTab" ? "TabbedHeading Selected" : "TabbedHeading";
		//    FeedTab.Attributes["class"] = Prefs.Current["FrontPageSelectedTab"] == "FeedTab" ? "TabbedHeading Selected" : "TabbedHeading";
		//    InboxTab.Attributes["class"] = Prefs.Current["FrontPageSelectedTab"] == "InboxTab" ? "TabbedHeading Selected" : "TabbedHeading";
		//    ChatTab.Attributes["class"] = Prefs.Current["FrontPageSelectedTab"] == "ChatTab" ? "TabbedHeading Selected" : "TabbedHeading";
		//    TopPhotosTab.Attributes["class"] = Prefs.Current["FrontPageSelectedTab"] == "TopPhotosTab" ? "TabbedHeading Selected" : "TabbedHeading";
		//    LatestTab.Attributes["class"] = Prefs.Current["FrontPageSelectedTab"] == "LatestTab" ? "TabbedHeading Selected" : "TabbedHeading";

		//    PanelControlPanel.Visible = !Prefs.Current["FrontPageSelectedTab"].Exists || Prefs.Current["FrontPageSelectedTab"] == "ControlPanelTab";
		//    PanelFeed.Visible = Prefs.Current["FrontPageSelectedTab"] == "FeedTab";
		//    PanelInbox.Visible = Prefs.Current["FrontPageSelectedTab"] == "InboxTab";
		//    PanelChat.Visible = Prefs.Current["FrontPageSelectedTab"] == "ChatTab";
		//    PanelTopPhotos.Visible = Prefs.Current["FrontPageSelectedTab"] == "TopPhotosTab";
		//    PanelLatest.Visible = Prefs.Current["FrontPageSelectedTab"] == "LatestTab";

		//    if (PanelControlPanel.Visible)
		//    {
		//        #region Home country
		//        {
		//            if (Country.Current != null)
		//            {
		//                HomeCountryLink.HRef = Country.Current.Url();
		//                HomeCountryLink.InnerHtml = Country.Current.FriendlyName;
		//                HomeCountryLabel.Text = Country.Current.FriendlyName;
		//            }
		//        }
		//        #endregion

		//        #region Favourite groups
		//        {
		//            Query q = new Query();
		//            q.TableElement = new Join(
		//                new TableElement(TablesEnum.Group),
		//                new TableElement(TablesEnum.GroupUsr),
		//                QueryJoinType.Inner,
		//                new And(
		//                    new Q(Group.Columns.K, GroupUsr.Columns.GroupK, true),
		//                    new Q(GroupUsr.Columns.Favourite, true),
		//                    new Q(GroupUsr.Columns.UsrK, Usr.Current.K),
		//                    new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)
		//                )
		//            );
		//            q.Columns = new ColumnSet(Group.Columns.UrlName, Group.Columns.Name);
		//            q.OrderBy = new OrderBy(Group.Columns.Name);
		//            GroupSet gs = new GroupSet(q);
		//            if (gs.Count > 0)
		//            {
		//                StringBuilder sb = new StringBuilder();
		//                foreach (Group g in gs)
		//                {
		//                    sb.Append(string.Format("{0}<a href=\"{1}\">{2}</a>", sb.Length == 0 ? "" : ", ", g.Url(), g.Name));
		//                }
		//                FavouriteGroupsPlaceholder.Controls.Add(new LiteralControl(sb.ToString()));
		//                FavouriteGroupsHolder.Visible = true;
		//                NoFavouriteGroupsHolder.Visible = false;
		//            }
		//            else
		//            {
		//                FavouriteGroupsHolder.Visible = false;
		//                NoFavouriteGroupsHolder.Visible = true;
		//            }
		//        }
		//        #endregion

		//        #region Places I visit
		//        {
		//            Query q = new Query();
		//            q.TableElement = new Join(
		//                new TableElement(TablesEnum.Place),
		//                new TableElement(TablesEnum.UsrPlaceVisit),
		//                QueryJoinType.Inner,
		//                new And(
		//                    new Q(Place.Columns.K, UsrPlaceVisit.Columns.PlaceK, true),
		//                    new Q(UsrPlaceVisit.Columns.UsrK, Usr.Current.K)
		//                )
		//            );
		//            q.Columns = new ColumnSet(Place.Columns.UrlName, Place.Columns.Name, Place.Columns.RegionAbbreviation, Place.Columns.UrlFragment);
		//            q.OrderBy = new OrderBy(Place.Columns.Name);
		//            PlaceSet ps = new PlaceSet(q);
		//            if (ps.Count > 0)
		//            {
		//                StringBuilder sb = new StringBuilder();
		//                foreach (Place p in ps)
		//                {
		//                    sb.Append(string.Format("{0}<a href=\"{1}\">{2}</a>", sb.Length == 0 ? "" : ", ", p.Url(), p.NamePlain));
		//                }
		//                PlacesPlaceholder.Controls.Add(new LiteralControl(sb.ToString()));
		//            }
		//            else
		//            {
		//                PlacesHolder.Visible = false;
		//            }
		//        }
		//        #endregion

		//        #region Calendars
		//        {
		//            MyCalendarLink.HRef = "/pages/mycalendar#Day" + DateTime.Now.ToString("yyyyMMdd");
		//            BuddyCalendarLink.HRef = "/pages/mycalendar/type-buddy#Day" + DateTime.Now.ToString("yyyyMMdd");
		//        }
		//        #endregion

		//        #region My comments
		//        {
		//            MyCommentsLink.HRef = Usr.Current.UrlApp("chat");
		//        }
		//        #endregion

		//        #region Online buddies
		//        {
		//            Query q = new Query();
		//            q.TableElement = Usr.BuddyJoin;
		//            q.QueryCondition = new And(
		//                new Q(Buddy.Columns.UsrK, Usr.Current.K),
		//                Usr.LoggedIn30MinIncludeNonEmailVerifiedQ);
		//            q.OrderBy = new OrderBy(Usr.Columns.NickName);
		//            UsrSet us = new UsrSet(q);
		//            StringBuilder sb = new StringBuilder();
		//            foreach (Usr u in us)
		//            {
		//                sb.Append(sb.Length == 0 ? "" : ", ");
		//                u.LinkAppend(sb, false);
		//            }
		//            OnlineBuddiesPlaceholder.Controls.Add(new LiteralControl(sb.ToString()));
		//        }
		//        {
		//            Query q = new Query();
		//            q.QueryCondition = Usr.LoggedIn30MinIncludeNonEmailVerifiedQ;
		//            q.CacheDuration = TimeSpan.FromMinutes(15);
		//            q.ReturnCountOnly = true;
		//            UsrSet us = new UsrSet(q);
		//            NumberOnlineLabel.Text = us.Count.ToString("#,##0");
		//        }
		//        #endregion

		//        #region Favourite photos
		//        {
		//            Query q = new Query();
		//            q.TableElement = Photo.UsrFavouritesJoin;
		//            q.OrderBy = new OrderBy(UsrPhotoFavourite.Columns.DateTimeAdded, OrderBy.OrderDirection.Descending);
		//            q.TopRecords = 10;
		//            q.QueryCondition = new Q(UsrPhotoFavourite.Columns.UsrK, Usr.Current.K);
		//            PhotoSet ps = new PhotoSet(q);
		//            StringBuilder sb = new StringBuilder();
		//            foreach (Photo p in ps)
		//            {
		//                sb.Append(string.Format("<a href=\"{0}\"><img src=\"{1}\" border=\"0\" width=\"50\" height=\"50\" class=\"BorderBlack All\" /></a> ", p.Url(), p.IconPath));
		//            }
		//            FavouritePhotosPlaceholder.Controls.Add(new LiteralControl(sb.ToString()));

		//            FavouritePhotosLink.HRef = Usr.Current.UrlApp("favouritephotos");
		//        }
		//        #endregion
		//    }
		//    else if (PanelFeed.Visible)
		//    {
		//        //coming soon...
		//    }
		//    else if (PanelInbox.Visible)
		//    {
		//        InboxControl.CaptureUrlParameters();
		//        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
		//        InboxControl.BindThreads();
		//    }
		//    else if (PanelChat.Visible)
		//    {

		//    }
		//    else if (PanelTopPhotos.Visible)
		//    {
		//        //TopPhotosUc.Bind();
		//    }
		//    else if (PanelLatest.Visible)
		//    {
		//        //Latest1.DataBind();
		//    }
		//}

		//protected void TabClick(object sender, EventArgs eventArgs)
		//{
		//    if (sender == ControlPanelTab)
		//        TabGo("ControlPanelTab");
		//    else if (sender == FeedTab)
		//        TabGo("FeedTab");
		//    else if (sender == InboxTab)
		//        TabGo("InboxTab");
		//    else if (sender == ChatTab)
		//        TabGo("ChatTab");
		//    else if (sender == TopPhotosTab)
		//        TabGo("TopPhotosTab");
		//    else if (sender == LatestTab)
		//        TabGo("LatestTab");
		//}

		//void TabNavigate(object sender, HistoryEventArgs e)
		//{
		//    string tab = e.State["FrontPageSelectedTab"];
		//    TabGo(tab ?? "");
		//}

		//#region TabGo
		//protected void TabGo(string tab)
		//{
		//    if (tab == null || tab.Length == 0)
		//        tab = ViewState["DefaultTabAtPageLoad"].ToString();

		//    string title = "";
		//    if (tab == "ControlPanelTab") title = "Control panel";
		//    else if (tab == "FeedTab") title = "Feed";
		//    else if (tab == "InboxTab") title = "Inbox";
		//    else if (tab == "ChatTab") title = "Chat";
		//    else if (tab == "TopPhotosTab") title = "Top photos";
		//    else if (tab == "LatestTab") title = "Latest";

		//    Prefs.Current["FrontPageSelectedTab"] = tab;
		//    ContainerPage.Script.AddHistoryPoint("FrontPageSelectedTab", tab, title);
		//    renderTabs();
		//}
		//#endregion

		protected IDiscussable Discussable
		{
			get
			{
				return null;
			}
		}
	}
}
