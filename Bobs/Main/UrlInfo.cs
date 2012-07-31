using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Principal;
using Common;
using Common.General;
using System.Data;
using System.Linq;

namespace Bobs
{
	public interface ICutDownUrlInfo
	{
		UrlInfo.UrlPart this[string key] { get; }
		string CurrentUrl(params object[] Params);
		List<string> TagFilter { get; set; }

	}
	#region UrlInfo
	public class UrlInfo : IDictionary, ICollection, IEnumerable, ICloneable, ICutDownUrlInfo
	{

		public bool DisableAllActions { get; set; }
		public bool IsMixmagVote { get; set; }
		public bool IsMixmagGreatest { get; set; }
		public bool IsMobile { get; set; }
		public int ScreenWidth { get; set; }
		public int ScreenHeight { get; set; }

		#region public UrlInfo(string path)
		public UrlInfo(string path, HttpBrowserCapabilities browser, bool disableRedirects, bool isMixmagVote, bool isMixmagGreatest)
		{
			this.IsMobile = browser.IsMobileDevice;
			this.ScreenWidth = browser.ScreenPixelsWidth;
			this.ScreenHeight = browser.ScreenPixelsHeight;

			this.DisableAllActions = disableRedirects;
			this.IsMixmagVote = isMixmagVote;
			this.IsMixmagGreatest = isMixmagGreatest;

			if (Usr.Current != null && Usr.Current.Banned)
			{
				PageType = PageTypes.Blank;
				PageName = "Banned";
				return;
			}
			innerHash = new Hashtable();
			innerIndex = new Hashtable();

			#region strip / from start and end of path
			if (path.StartsWith("/"))
				path = path.Substring(1);
			if (path.EndsWith("/"))
				path = path.Substring(0, path.Length - 1);
			#endregion

			string[] urlParts = path.Split('/');

			#region If there's a dot in the last part, handle this file with the default HttpHandler
			if (urlParts[urlParts.Length - 1].Contains("."))
			{
				if (urlParts[urlParts.Length - 1].EndsWith(".rss"))
				{
					IsRss = true;
					urlParts[urlParts.Length - 1] = urlParts[urlParts.Length - 1].Substring(0, urlParts[urlParts.Length - 1].IndexOf("."));
				}
				else
				{
					OverrideHttpHandler = true;
					return;
				}
			}
			#endregion

			#region custom redirects
			if (!DisableAllActions)
			{
				if (urlParts.Length == 1 && urlParts[0] == "facebook")
				{
					Log.Increment(Log.Items.FacebookUrlShortcut);

					HttpContext.Current.Response.Redirect("http://www.facebook.com/pages/dontstayincom/95813938222");
				}

				if (urlParts.Length == 1 && urlParts[0] == "twitter")
				{
					Log.Increment(Log.Items.TwitterUrlShortcut);

					HttpContext.Current.Response.Redirect("http://twitter.com/dontstayin_com");
				}

				if (urlParts.Length == 1 && urlParts[0] == "comps")
				{
					Log.Increment(Log.Items.CompsUrlShortcut);

					string[] par = { };
					HttpContext.Current.Response.Redirect(Vars.GetArchiveUrl(DateTime.Today.Year, DateTime.Today.Month, 0, Model.Entities.ArchiveObjectType.Comp, par, null));
				}

				if (urlParts.Length == 1 && urlParts[0] == "ibizarocks")
				{
					Log.Increment(Log.Items.IbizaRocksUrlShortcut);
					try
					{
						Group ibizaRocksGroup = new Group(10992);
						HttpContext.Current.Response.Redirect(ibizaRocksGroup.Url());
					}
					catch { }
				}

				if (urlParts.Length == 1 && urlParts[0] == "schmucks")
				{
					Log.Increment(Log.Items.SchmucksUrlShortcut);
					try
					{
						Group schmucksGroup = new Group(36543);
						HttpContext.Current.Response.Redirect(schmucksGroup.Url());
					}
					catch { }
				}

				if (urlParts.Length == 1 && urlParts[0] == "tickets")
				{
					try
					{
						Banner b = new Banner(12965);
						b.RegisterClick();
						HttpContext.Current.Response.Redirect(b.LinkTargetUrl);
					}
					catch { }
				}

				if (urlParts.Length == 1 && urlParts[0] == "free")
				{
					try
					{
						Banner b = new Banner(13287);
						b.RegisterClick();
						HttpContext.Current.Response.Redirect(b.LinkTargetUrl);
					}
					catch { }
				}

				if (urlParts.Length == 1 && urlParts[0] == "mixmag")
				{
					try
					{
						Banner b = new Banner(12966);
						b.RegisterClick();
						HttpContext.Current.Response.Redirect(b.LinkTargetUrl);
					}
					catch { }
				}

				if (urlParts.Length == 1 && urlParts[0] == "tick-em-off")
				{
					try
					{
						Group g = new Group(33961);
						HttpContext.Current.Response.Redirect(g.Url());
					}
					catch { }
				}

				if (urlParts.Length == 1 && urlParts[0] == "cream-tickets")
				{
					try
					{
						Banner b = new Banner(13150);
						b.RegisterClick();
						HttpContext.Current.Response.Redirect(b.LinkTargetUrl);
					}
					catch { }
				}

				if (urlParts.Length == 1 && urlParts[0] == "ibizaholiday")
				{
					try
					{
						Banner b = new Banner(13902);
						b.RegisterClick();
						HttpContext.Current.Response.Redirect(b.LinkTargetUrl);
					}
					catch { }
				}

				//13921 north
				if (urlParts.Length == 1 && urlParts[0] == "mixmagfreenorth")
				{
					try
					{
						Banner b = new Banner(13921);
						b.RegisterClick();
						HttpContext.Current.Response.Redirect(b.LinkTargetUrl);
					}
					catch { }
				}

				//13922 south
				if (urlParts.Length == 1 && urlParts[0] == "mixmagfreesouth")
				{
					try
					{
						Banner b = new Banner(13922);
						b.RegisterClick();
						HttpContext.Current.Response.Redirect(b.LinkTargetUrl);
					}
					catch { }
				}
			}
			#endregion

			IsAjaxRequest = (HttpContext.Current.Request.Headers["x-microsoftajax"] != null);

			int currentIndex = 0;

			if (IsMixmagVote)
			{
				if (urlParts.Length > 0 && urlParts[0].Length > 0)
				{
					PageType = PageTypes.MixmagVote;
					if (urlParts[currentIndex].IsNumeric())
					{
						PageName = "Vote";
						ProcessParamParts(urlParts, currentIndex);
					}
					else
					{
						PageName = urlParts[currentIndex];
						ProcessParamParts(urlParts, currentIndex + 1);
					}
				}
				else
				{
					PageType = PageTypes.MixmagVote;
					PageName = "Home";
				}
			}
			else if (IsMixmagGreatest)
			{
				if (urlParts.Length > 0 && urlParts[0].Length > 0)
				{
					bool fbPage = false;
					PageType = PageTypes.MixmagGreatest;
					string name = urlParts[currentIndex];
					if (name == "fb")
					{
						fbPage = true;
						currentIndex++;
						name = urlParts[currentIndex];
					}
					MixmagGreatestDjSet djs = new MixmagGreatestDjSet(new Query(new Q(MixmagGreatestDj.Columns.UrlName, name)));
					if (djs.Count > 0)
					{
						MixmagGreatestDjK = djs[0].K;
						PageName = fbPage ? "Fb" : "Home";
						ProcessParamParts(urlParts, currentIndex);
					}
					else
					{
						PageName = urlParts[currentIndex];
						ProcessParamParts(urlParts, currentIndex + 1);
					}
				}
				else
				{
					PageType = PageTypes.MixmagGreatest;
					PageName = "Home";
				}
			}
			else if (Vars.DevEnv && ((IsMobile && Prefs.Current["ForceMobile"] != "full") || Prefs.Current["ForceMobile"] == "mobile"))
			{
			    PageType = PageTypes.Mobile;
			    PageName = "Home";
			}
			else
			{
				if (ProcessLoginPart(ref currentIndex, urlParts))
					return;

				if (ProcessCustomPage(currentIndex, urlParts))
					return;

				bool searchMore = true;
				int count = 0;
				while (currentIndex < urlParts.Length && searchMore && count < 50)
				{
					count++;

					UrlPartTypes thisType = ProcessFilterPart(ref currentIndex, urlParts);

					if (thisType.Equals(UrlPartTypes.Application))
						searchMore = false;
				}

				ProcessParamParts(urlParts, currentIndex);

				if (PageName == null || PageName.Length == 0)
				{
					PageType = PageTypes.Pages;
					PageName = "Home";
				}
			}

			

			

		}
		#endregion

		#region ProcessLoginPart
		public bool ProcessLoginPart(ref int currentIndex, string[] urlParts)
		{
			if (urlParts[currentIndex].StartsWith("login") || urlParts[currentIndex].StartsWith("logout"))
			{
				try
				{
					LoginPartLogOutFirst = urlParts[currentIndex].StartsWith("logout"); // logout is used when forcing a log out (e.g. when admin or mod is needed)

					if (urlParts[currentIndex].Contains('-'))
					{
						string[] loginArr = urlParts[currentIndex].Split('-');
						LoginPartUsrK = loginArr.Length >= 2 ? int.Parse(loginArr[1]) : 0;
						LoginPartLoginString = loginArr.Length >= 3 ? loginArr[2] : "";
					}

					currentIndex++;

					#region construct LoggedInPlainUrl
					string redirectUrl = "";
					for (int part = currentIndex; part < urlParts.Length; part++)
						redirectUrl += "/" + urlParts[part];

					if (redirectUrl.Length == 0)
						LoggedInPlainUrl = "/";
					else
						LoggedInPlainUrl = redirectUrl;

					if (HttpContext.Current.Request.QueryString.ToString().Length > 0)
						LoggedInPlainUrl += "?" + HttpContext.Current.Request.QueryString.ToString();
					#endregion

					Usr u = null;
					try
					{
						u = new Usr(LoginPartUsrK);
						if (u.LoginString.ToLower().Equals(LoginPartLoginString.ToLower()))
						{
							if (!u.IsEmailVerified)
							{
								u.IsEmailVerified = true;
								u.Update();
							}
						}
					}
					catch { }

					if (Usr.Current == null || (LoginPartUsrK > 0 && Usr.Current.K != LoginPartUsrK))
					{
						if (u.LoginString.ToLower().Equals(LoginPartLoginString.ToLower()))
						{
							LoginPartUsrEmail = u.Email;
							LoginPartUsrIsSkeleton = u.IsSkeleton;
							LoginPartUsrIsFacebookNotConfirmed = !u.FacebookStory.HasValue;
							LoginPartUsrIsEnhancedSecurity = u.EnhancedSecurity && (Usr.Current == null || !Usr.Current.IsAdmin);
							LoginPartUsrNeedsCaptcha = u.NeedsCaptcha.HasValue && u.NeedsCaptcha.Value && Settings.CaptchaEnabledStatus == Settings.CaptchaEnabledStatusOption.On;
							LoginPartUsrHomePlaceK = u.HomePlaceK;
							LoginPartUsrFavouriteMusicK = u.FavouriteMusicTypeK;
							LoginPartUsrSendSpottedEmails = u.SendSpottedEmails;
							LoginPartUsrSendEflyers = u.SendFlyers;

							if (LoginPartUsrNeedsCaptcha)
							{
								string text = Cambro.Misc.Utility.GenRandomChars(5).ToUpper() + "|" + HttpUtility.UrlEncode(u.Email.ToLower());
								LoginPartUsrCaptchaEncrypted = Cambro.Misc.Utility.Encrypt(text, DateTime.Now.AddHours(1));
							}

							if (LoginPartUsrIsSkeleton || LoginPartUsrIsEnhancedSecurity || LoginPartUsrNeedsCaptcha)
							{
								PageType = PageTypes.Pages;
								PageName = "AutoLogin";
								return true;
							}
							else
							{
								u.LogInAsThisUserNew();
								if (!DisableAllActions)
								{
									if (u.EmailHold)
										HttpContext.Current.Response.Redirect("/popup/unsubscribe");
									else
										HttpContext.Current.Response.Redirect(LoggedInPlainUrl);
								}
							}
						}
						else
						{
							LoginFailed = true;

							PageType = PageTypes.Pages;
							PageName = "AutoLogin";
							return true;
						}

					}
					else if (LoginPartLogOutFirst)
					{
						PageType = PageTypes.Pages;
						PageName = "AutoLogin";
						return true;
					}
					else
					{
						if (!DisableAllActions)
							HttpContext.Current.Response.Redirect(LoggedInPlainUrl);
					}


					//if (Usr.Current != null && (LoginPartUsrK == 0 || Usr.Current.K == LoginPartUsrK) && LoginPartLogin)
					//{
					//    if (!DisableAllActions)
					//    {
					//        if (!Usr.Current.IsEmailVerified &&
					//            LoginPartUsrK > 0 &&
					//            LoginPartLoginString.Length > 0 &&
					//            Usr.Current.LoginString.ToLower() == LoginPartLoginString.ToLower())
					//        {
					//            Usr.Current.IsEmailVerified = true;
					//            Usr.Current.Update();
					//            Usr.Current = null;
					//        }
						
					//        if (Usr.Current.EmailHold)
					//            HttpContext.Current.Response.Redirect("/popup/unsubscribe");
					//        else
					//            HttpContext.Current.Response.Redirect(LoggedInPlainUrl);

					//    }
					//    return false;
					//}
					//else
					//{
					//    PageType = PageTypes.Pages;
					//    PageName = "AutoLogin";
					//    return true;
					//}
				}
				catch
				{
					PageType = PageTypes.Pages;
					PageName = "AutoLogin";
					return true;
				}
			}
			return false;
		}

		

		//void RedirectSkeleton(string redirectUrl)
		//{
		//    if (redirectUrl != "/popup/unsubscribe")
		//    {
		//        if (!DisableRedirects)
		//        {
		//            if (redirectUrl.Length > 1)
		//                HttpContext.Current.Response.Redirect("/popup/welcome?Url=" + HttpUtility.UrlEncode(redirectUrl));
		//            else
		//                HttpContext.Current.Response.Redirect("/popup/welcome");
		//        }
		//    }
		//}
		#endregion
		#region ProcessCustomPage
		public bool ProcessCustomPage(int currentIndex, string[] urlParts)
		{
			#region Process custom pages
			if (currentIndex < urlParts.Length)
			{
				if (urlParts[currentIndex].Equals("admin"))
				{
					Usr.KickUserIfNotAdmin("");
					if (Usr.Current == null || !Usr.Current.IsAdmin)
					{
						throw new DsiUserFriendlyException("You're trying to view an admin page, but you're not logged in as an admin!");
					}
					// TODO: Verify IP Address Range

					PageType = PageTypes.Admin;
					PageName = urlParts[currentIndex + 1];
					HasCustomPage = true;
					ProcessParamParts(urlParts, currentIndex + 2);
					try
					{
						if (this["Year"].IsInt)
						{
							HasYearFilter = true;
							DateFilter = new DateTime(this["Year"], 1, 1);
							if (this["Month"].IsInt)
							{
								HasMonthFilter = true;
								DateFilter = new DateTime(this["Year"], this["Month"], 1);
								if (this["Day"].IsInt)
								{
									HasDayFilter = true;
									DateFilter = new DateTime(this["Year"], this["Month"], this["Day"]);
								}
							}
						}
					}
					catch { }
					return true;
				}
				else if (urlParts[currentIndex].Equals("popup"))
				{
					try
					{
						PageType = PageTypes.Blank;
						PageName = urlParts[currentIndex + 1];
						HasCustomPage = true;
						ProcessParamParts(urlParts, currentIndex + 2);
					}
					catch (IndexOutOfRangeException)
					{
						throw new MalformedUrlException();
					}
					return true;
				}
				else if (urlParts[currentIndex].Equals("pages"))
				{
					try
					{
						string nextPart = urlParts[currentIndex + 1].ToLower();
						if (nextPart.Equals("articles") ||
							nextPart.Equals("brands") ||
							nextPart.Equals("countries") ||
							nextPart.Equals("events") ||
							nextPart.Equals("galleries") ||
							nextPart.Equals("groups") ||
							nextPart.Equals("photos") ||
							nextPart.Equals("places") ||
							nextPart.Equals("promoters") ||
							nextPart.Equals("regions") ||
							nextPart.Equals("usrs") ||
							nextPart.Equals("venues"))
						{
							PageType = PageTypes.PagesFolder;
							PageFolder = nextPart;
							if (urlParts.Length > currentIndex + 2)
								PageName = urlParts[currentIndex + 2];
							else
								PageName = "home";
							HasCustomPage = true;
							ProcessParamParts(urlParts, currentIndex + 3);
							return true;
						}
						else
						{
							PageType = PageTypes.Pages;
							PageName = nextPart;
							HasCustomPage = true;
							ProcessParamParts(urlParts, currentIndex + 2);
							return true;
						}
					}
					catch (IndexOutOfRangeException)
					{
						throw new MalformedUrlException();
					}
				}
				else if (urlParts[currentIndex].Equals("offer") || urlParts[currentIndex].Equals("offers")) //url sent out in clubs list letters (dontstayin.com/offer)
				{
					PageType = PageTypes.Pages;
					PageName = "Offer";
					HasCustomPage = true;
					return true;
				}
				else if (urlParts[currentIndex].Equals("sitemapxml"))
				{
					PageType = PageTypes.Blank;
					PageName = "SiteMapXml";
					HasCustomPage = true;
					return true;
				}
			}
			return false;
			#endregion
		}
		#region HasCustomPage
		public bool HasCustomPage
		{
			get
			{
				return hasCustomPage;
			}
			set
			{
				hasCustomPage = value;
			}
		}
		private bool hasCustomPage = false;
		#endregion
		#endregion
		#region ProcessFilterPart
		public UrlPartTypes ProcessFilterPart(ref int currentIndex, string[] urlParts)
		{
			if (HasYearFilter && urlParts[currentIndex].Equals("tickets"))
			{
				#region tickets calendar
				PageType = PageTypes.Pages;
				PageName = "CalendarTickets";
				CurrentApplication = "tickets";
				currentIndex++;
				return UrlPartTypes.Application;
				#endregion
			}
			else if (HasYearFilter && urlParts[currentIndex].Equals("free"))
			{
				#region Free Guestlist calendar
				PageType = PageTypes.Pages;
				PageName = "CalendarFreeGuestlist";
				CurrentApplication = "free";
				currentIndex++;
				return UrlPartTypes.Application;
				#endregion
			}
			else if (urlParts[currentIndex].Equals("tags"))
			{
				#region tags
				PageType = PageTypes.Pages;
				PageName = "TagSearch";
				CurrentApplication = "tags";
				HasTagFilter = true;

				currentIndex++;
				if (urlParts.Length > currentIndex)
				{

					CurrentApplication = "tags/" + urlParts[currentIndex];

					foreach (string s in urlParts[currentIndex].Split('-'))
					{
						if (!s.Equals("all"))
							TagFilter.Add(Cambro.Web.Helpers.UrlTextDeSerialize(s));
					}

					currentIndex++;
				}
				return UrlPartTypes.Application;
				#endregion
			}
			else if ((HasBrandObjectFilter || HasVenueObjectFilter) && (urlParts[currentIndex].Equals("tickets") || urlParts[currentIndex].Equals("photos")))
			{
				#region Styled page
				currentIndex++;
				PageType = PageTypes.Styled;
				PageName = "Home";
				if (urlParts.Length > currentIndex &&
					YearRegex.IsMatch(urlParts[currentIndex]))
				{
					#region //year and month
					int year = int.Parse(urlParts[currentIndex]);
					if (year > 1990 && year < 2030)
					{
						HasYearFilter = true;
						DateFilter = new DateTime(year, 1, 1);
						PageName = "Calendar";
						currentIndex++;
						if (urlParts.Length > currentIndex &&
							MonthRegex.IsMatch(urlParts[currentIndex]))
						{
							int month = MonthNumber(urlParts[currentIndex]);
							HasMonthFilter = true;
							DateFilter = new DateTime(year, month, 1);
							currentIndex++;
						}
						return UrlPartTypes.Application;
					}
					#endregion
				}
				else if (urlParts.Length > currentIndex &&
					MonthRegex.IsMatch(urlParts[currentIndex]))
				{
					#region //month only - infer the year
					int requestedMonth = MonthNumber(urlParts[currentIndex]);
					HasYearFilter = true;
					HasMonthFilter = true;
					int requestedMonthIndex = (DateTime.Today.Year * 12) + requestedMonth;
					int currentMonthIndex = (DateTime.Today.Year * 12) + DateTime.Today.Month;
					DateTime d = new DateTime(DateTime.Today.Year, requestedMonth, 1);

					if (currentMonthIndex - requestedMonthIndex > 4)
					{
						d = new DateTime(DateTime.Today.Year + 1, requestedMonth, 1);
					}
					else if (currentMonthIndex - requestedMonthIndex < -7)
					{
						d = new DateTime(DateTime.Today.Year - 1, requestedMonth, 1);
					}

					DateFilter = d;
					PageName = "Calendar";
					currentIndex++;
					return UrlPartTypes.Application;
					#endregion
				}
				else if (urlParts.Length > currentIndex &&
					urlParts[currentIndex].ToLower().Equals("calendar"))
				{
					#region //todays month
					DateFilter = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
					PageName = "Calendar";
					currentIndex++;
					return UrlPartTypes.Application;
					#endregion
				}
				else if (urlParts.Length > currentIndex &&
					NumericRegex.IsMatch(urlParts[currentIndex]))
				{
					#region //event id
					PageName = "EventDetail";
					return UrlPartTypes.Application;
					#endregion
				}
				return UrlPartTypes.ObjectFilter;
				#endregion
			}
			else if (urlParts[currentIndex].Equals("parties"))
			{
				#region brand
				currentIndex++;
				Query brandQuery = new Query();
				brandQuery.NoLock = true;
				brandQuery.Columns = new ColumnSet(Brand.Columns.K);
				brandQuery.QueryCondition = new Q(Brand.Columns.UrlName, urlParts[currentIndex]);
				BrandSet bs = new BrandSet(brandQuery);
				if (bs.Count > 0)
				{
					HasObjectFilter = true;
					ObjectFilterType = Model.Entities.ObjectType.Brand;
					ObjectFilterK = bs[0].K;
					PageType = PageTypes.Application;
					PageName = "Home";
					CurrentFilter += "/parties/" + urlParts[currentIndex];
					currentIndex++;
					return UrlPartTypes.ObjectFilter;
				}
				return UrlPartTypes.Null;
				#endregion
			}
			else if (urlParts[currentIndex].Equals("groups"))
			{
				#region group
				currentIndex++;
				if (urlParts.Length > currentIndex)
				{
					string groupUrlName = urlParts[currentIndex];
					if (urlParts[currentIndex].Equals("parties"))
					{
						currentIndex++;
						groupUrlName = "parties/" + urlParts[currentIndex];
					}
					Query groupQuery = new Query();
					groupQuery.NoLock = true;
					groupQuery.Columns = new ColumnSet(Bobs.Group.Columns.K);
					groupQuery.QueryCondition = new Q(Bobs.Group.Columns.UrlName, groupUrlName);
					GroupSet gs = new GroupSet(groupQuery);
					if (gs.Count > 0)
					{
						HasObjectFilter = true;
						ObjectFilterType = Model.Entities.ObjectType.Group;
						ObjectFilterK = gs[0].K;
						PageType = PageTypes.Application;
						PageName = "Home";
						CurrentFilter += "/groups/" + groupUrlName;
						currentIndex++;
						return UrlPartTypes.ObjectFilter;
					}
				}
				PageType = PageTypes.Pages;
				PageName = "GroupBrowser";
				CurrentApplication = "groups";
				return UrlPartTypes.Application;
				#endregion
			}
			else if (!HasObjectFilter && urlParts[currentIndex].Equals("promoters"))
			{
				#region promoter
				currentIndex++;
				Query promoterQuery = new Query();
				promoterQuery.NoLock = true;
				promoterQuery.Columns = new ColumnSet(Promoter.Columns.K);
				promoterQuery.QueryCondition = new Q(Promoter.Columns.UrlName, urlParts[currentIndex]);
				PromoterSet ps = new PromoterSet(promoterQuery);
				if (ps.Count > 0)
				{
					HasObjectFilter = true;
					ObjectFilterType = Model.Entities.ObjectType.Promoter;
					ObjectFilterK = ps[0].K;
					PageType = PageTypes.Application;
					PageName = "Home";
					CurrentFilter += "/promoters/" + urlParts[currentIndex];
					currentIndex++;
					return UrlPartTypes.ObjectFilter;
				}
				return UrlPartTypes.Null;
				#endregion
			}
			else if (!HasObjectFilter && urlParts[currentIndex].Equals("members"))
			{
				#region members - usr
				currentIndex++;
				if (urlParts[currentIndex].Length > 0)
				{
					Usr usr = Usr.GetFromNickName(urlParts[currentIndex]);
					if (usr != null)
					{
						HasObjectFilter = true;
						ObjectFilterType = Model.Entities.ObjectType.Usr;
						ObjectFilterK = usr.K;
						PageType = PageTypes.Application;
						PageName = "Home";
						CurrentFilter += "/members/" + urlParts[currentIndex];
						currentIndex++;
						return UrlPartTypes.ObjectFilter;
					}
					//Query usrQuery = new Query();
					//usrQuery.NoLock = true;
					//usrQuery.Columns = new ColumnSet(Usr.Columns.K);
					//usrQuery.QueryCondition = new Q(Usr.Columns.NickName, urlParts[currentIndex]);
					//UsrSet us = new UsrSet(usrQuery);
					//if (us.Count > 0)
					//{
					//    HasObjectFilter = true;
					//    ObjectFilterType = Model.Entities.ObjectType.Usr;
					//    ObjectFilterK = us[0].K;
					//    PageType = PageTypes.Application;
					//    PageName = "Home";
					//    CurrentFilter += "/members/" + urlParts[currentIndex];
					//    currentIndex++;
					//    return UrlPartTypes.ObjectFilter;
					//}
				}
				return UrlPartTypes.Null;
				#endregion
			}
			else if (urlParts[currentIndex].StartsWith("event-"))
			{
				#region event
				try
				{
					Event ev = new Event(int.Parse(urlParts[currentIndex].Split('-')[1]));
					HasObjectFilter = true;
					ObjectFilterType = Model.Entities.ObjectType.Event;
					ObjectFilterK = ev.K;
					PageType = PageTypes.Application;
					PageName = "Home";
					CurrentFilter += "/" + urlParts[currentIndex];
					currentIndex++;
					return UrlPartTypes.ObjectFilter;
				}
				catch
				{
					currentIndex++;
				}
				return UrlPartTypes.Null;
				#endregion
			}
			else if (urlParts[currentIndex].StartsWith("gallery-"))
			{
				#region gallery
				try
				{
					Gallery g = new Gallery(int.Parse(urlParts[currentIndex].Split('-')[1]));
					HasObjectFilter = true;
					PageType = PageTypes.Application;
					PageName = "Home";
					ObjectFilterType = Model.Entities.ObjectType.Gallery;
					ObjectFilterK = g.K;
					CurrentFilter += "/" + urlParts[currentIndex];
					currentIndex++;
					return UrlPartTypes.ObjectFilter;
				}
				catch
				{
					currentIndex++;
				}
				return UrlPartTypes.Null;
				#endregion
			}
			else if (urlParts[currentIndex].StartsWith("photo-"))
			{
				#region photo
				try
				{
					Photo p = new Photo(int.Parse(urlParts[currentIndex].Split('-')[1]));
					HasObjectFilter = true;
					PageType = PageTypes.Application;
					PageName = "Home";
					ObjectFilterType = Model.Entities.ObjectType.Photo;
					ObjectFilterK = p.K;
					CurrentFilter += "/" + urlParts[currentIndex];
					currentIndex++;
					return UrlPartTypes.ObjectFilter;
				}
				catch
				{
					currentIndex++;
				}
				return UrlPartTypes.Null;
				#endregion
			}
			else if (urlParts[currentIndex].StartsWith("article-"))
			{
				#region article
				try
				{
					Article a = new Article(int.Parse(urlParts[currentIndex].Split('-')[1]));
					HasObjectFilter = true;
					PageType = PageTypes.Application;
					PageName = "Home";
					ObjectFilterType = Model.Entities.ObjectType.Article;
					ObjectFilterK = a.K;
					CurrentFilter += "/" + urlParts[currentIndex];
					currentIndex++;
					return UrlPartTypes.ObjectFilter;
				}
				catch
				{
					currentIndex++;
				}
				return UrlPartTypes.Null;
				#endregion
			}
			else if (GetMusicTypeK(urlParts[currentIndex].ToLower()) > 0)
			{
				#region music filter
				this.HasMusicFilter = true;
				this.MusicFilterK = GetMusicTypeK(urlParts[currentIndex].ToLower());
				CurrentFilter += "/" + urlParts[currentIndex].ToLower();
				currentIndex++;
				return UrlPartTypes.MusicFilter;
				#endregion
			}
			else if (GetThemeK(urlParts[currentIndex].ToLower()) > 0)
			{
				#region theme filter
				this.HasThemeFilter = true;
				this.ThemeFilterK = GetThemeK(urlParts[currentIndex].ToLower());
				CurrentFilter += "/" + urlParts[currentIndex].ToLower();
				currentIndex++;
				return UrlPartTypes.ThemeFilter;
				#endregion
			}
			else if (GetCountryK(urlParts[currentIndex].ToLower()) > 0)
			{
				#region Lookup country / place / venue
				HasObjectFilter = true;
				ObjectFilterType = Model.Entities.ObjectType.Country;
				ObjectFilterK = GetCountryK(urlParts[currentIndex].ToLower());
				PageType = PageTypes.Application;
				PageName = "Home";
				CurrentFilter += "/" + urlParts[currentIndex].ToLower();
				currentIndex++;
				int countryK = this.ObjectFilterK;

				if (urlParts.Length > currentIndex)
				{
					Country country = new Country(countryK);
					Q regionQ = new Q(true);
					if (country.UseRegion)
					{
						Query qRegion = new Query();
						qRegion.NoLock = true;
						qRegion.Columns = new ColumnSet(Region.Columns.K);
						qRegion.TopRecords = 1;
						qRegion.QueryCondition = new And(
							new Q(Region.Columns.CountryK, countryK),
							new Q(Region.Columns.Abbreviation, urlParts[currentIndex]));
						RegionSet rs = new RegionSet(qRegion);
						if (rs.Count > 0)
						{
							HasObjectFilter = true;
							ObjectFilterType = Model.Entities.ObjectType.Region;
							ObjectFilterK = rs[0].K;
							PageType = PageTypes.Application;
							PageName = "Home";
							int regionK = ObjectFilterK;
							regionQ = new Q(Place.Columns.RegionK, regionK);
							CurrentFilter += "/" + urlParts[currentIndex].ToLower();

							currentIndex++;

							if (!(urlParts.Length > currentIndex))
								return UrlPartTypes.ObjectFilter;
						}


					}
					#region Lookup place
					Query placeQuery = new Query();
					placeQuery.NoLock = true;
					placeQuery.Columns = new ColumnSet(Place.Columns.K);
					placeQuery.QueryCondition = new And(
						new Q(Place.Columns.Enabled, true),
						new Q(Place.Columns.CountryK, countryK),
						new Q(Place.Columns.UrlName, urlParts[currentIndex].ToLower()),
						regionQ
					);
					PlaceSet ps = new PlaceSet(placeQuery);
					if (ps.Count > 0)
					{
						HasObjectFilter = true;
						ObjectFilterType = Model.Entities.ObjectType.Place;
						ObjectFilterK = ps[0].K;
						PageType = PageTypes.Application;
						PageName = "Home";
						CurrentFilter += "/" + urlParts[currentIndex].ToLower();
						currentIndex++;
						int placeK = this.ObjectFilterK;

						if (urlParts.Length > currentIndex)
						{
							#region Lookup venue
							Query venueQuery = new Query();
							venueQuery.NoLock = true;
							venueQuery.Columns = new ColumnSet(Venue.Columns.K);
							venueQuery.QueryCondition = new And(
								new Q(Venue.Columns.PlaceK, placeK),
								new Q(Venue.Columns.UrlName, urlParts[currentIndex].ToLower())
								);
							VenueSet vs = new VenueSet(venueQuery);
							if (vs.Count > 0)
							{
								HasObjectFilter = true;
								ObjectFilterType = Model.Entities.ObjectType.Venue;
								ObjectFilterK = vs[0].K;
								PageType = PageTypes.Application;
								PageName = "Home";
								CurrentFilter += "/" + urlParts[currentIndex].ToLower();
								currentIndex++;
								int venueK = this.ObjectFilterK;
							}
							#endregion
						}
					}
					#endregion
				}
				return UrlPartTypes.ObjectFilter;
				#endregion
			}
			else if (YearRegex.IsMatch(urlParts[currentIndex]))
			{
				#region year / month / day
				int year = int.Parse(urlParts[currentIndex]);
				if (year > 1990 && year < 2030)
				{
					HasYearFilter = true;
					DateFilter = new DateTime(year, 1, 1);
					PageType = PageTypes.Pages;
					PageName = "Calendar";
					CurrentFilter += "/" + urlParts[currentIndex];
					currentIndex++;
					if (urlParts.Length > currentIndex && MonthRegex.IsMatch(urlParts[currentIndex]))
					{
						int month = MonthNumber(urlParts[currentIndex]);
						HasMonthFilter = true;
						DateFilter = new DateTime(year, month, 1);
						PageType = PageTypes.Pages;
						PageName = "Calendar";
						CurrentFilter += "/" + urlParts[currentIndex];
						currentIndex++;
						if (urlParts.Length > currentIndex && DayRegex.IsMatch(urlParts[currentIndex]))
						{
							int day = int.Parse(urlParts[currentIndex]);
							try
							{
								DateFilter = new DateTime(year, month, day);
								HasDayFilter = true;
								PageType = PageTypes.Pages;
								PageName = "Calendar";
								CurrentFilter += "/" + urlParts[currentIndex];
								currentIndex++;
							}
							catch
							{
								currentIndex++;
							}
						}
					}
					return UrlPartTypes.DateFilter;
				}
				else
					return UrlPartTypes.Null;
				#endregion
			}
			else if (urlParts[currentIndex].Equals("chat") || urlParts[currentIndex].Equals("messages"))
			{
				#region chat application
				PageType = PageTypes.Pages;
				PageName = "Chat";
				CurrentApplication = "chat";
				currentIndex++;
				if (ObjectFilterType.Equals(Model.Entities.ObjectType.Usr))
				{
					PageType = PageTypes.PagesFolder;
					PageFolder = "Usrs";
					PageName = "MyComments";
					CurrentApplication = "chat";
				}
				return UrlPartTypes.Application;
				#endregion
			}
			else if (urlParts[currentIndex].Equals("archive"))
			{
				#region archive application
				PageType = PageTypes.Pages;
				PageName = "Archive";
				CurrentApplication = "archive";
				currentIndex++;
				return UrlPartTypes.Application;
				#endregion
			}
			else if (urlParts[currentIndex].Equals("hottickets"))
			{
				#region hot tickets application
				PageType = PageTypes.Pages;
				PageName = "HotTickets";
				CurrentApplication = "hottickets";
				currentIndex++;
				return UrlPartTypes.Application;
				#endregion
			}
			else if (urlParts[currentIndex].Equals("home"))
			{
				#region home application
				CurrentApplication = "home";
				currentIndex++;
				return UrlPartTypes.Application;
				#endregion
			}
			else if (HasUsrObjectFilter && urlParts[currentIndex].Equals("photosof"))
			{
				#region photosof page
				currentIndex++;
				PageType = PageTypes.Application;
				PageName = "photosof";
				CurrentApplication = "photosof/" + urlParts[currentIndex].ToLower();
				currentIndex++;
				return UrlPartTypes.Application;
				#endregion
			}
			else
			{
				if (urlParts.Length > currentIndex)
				{
					if (!PageType.Equals(PageTypes.Styled))
						PageType = PageTypes.Application;
					PageName = urlParts[currentIndex].ToLower();
					CurrentApplication = urlParts[currentIndex].ToLower();
					currentIndex++;
					return UrlPartTypes.Application;
				}
				else
				{
					currentIndex++;
					return UrlPartTypes.Null;
				}
			}
		}
		#endregion
		#region ProcessParamParts
		public void ProcessParamParts(string[] urlParts, int startIndex)
		{
			for (int i = startIndex; i < urlParts.Length; i++)
			{
				if (urlParts[i].Length > 0)
				{
					UrlPart p = new UrlPart(urlParts[i], i - startIndex);
					this.Add(p.Key, p);
				}
			}
		}
		#endregion

		public bool IsRss { get; set; }
		#region YearRegex
		static Regex YearRegex
		{
			get
			{
				return new Regex("^[12][90][90123][0-9]$");
			}
		}
		#endregion
		#region MonthRegex
		Regex MonthRegex
		{
			get
			{
				return new Regex("^jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec$");
			}
		}
		#endregion
		#region MonthNumber
		public int MonthNumber(string urlName)
		{
			switch (urlName)
			{
				case "jan": return 1;
				case "feb": return 2;
				case "mar": return 3;
				case "apr": return 4;
				case "may": return 5;
				case "jun": return 6;
				case "jul": return 7;
				case "aug": return 8;
				case "sep": return 9;
				case "oct": return 10;
				case "nov": return 11;
				case "dec": return 12;
				default: return 0;
			}
		}
		#endregion
		#region DayRegex
		Regex DayRegex
		{
			get
			{
				return new Regex("^[0123][0-9]$");
			}
		}
		#endregion
		#region NumericRegex
		Regex NumericRegex
		{
			get
			{
				return new Regex("^[0-9]{1,}$");
			}
		}
		#endregion
		#region IsReservedString
		/// <summary>
		/// We shouldn't let places or venues have the same names as this...
		/// </summary>
		public static bool IsReservedString(string inString)
		{
			if (inString.Equals("chat"))
				return true;
			if (inString.Equals("messages"))
				return true;
			if (inString.Equals("home"))
				return true;
			if (inString.Equals("archive"))
				return true;
			if (inString.Equals("parties"))
				return true;
			if (inString.Equals("groups"))
				return true;
			if (inString.Equals("venues"))
				return true;
			if (inString.StartsWith("event-"))
				return true;
			if (inString.StartsWith("gallery-"))
				return true;
			if (inString.StartsWith("photo-"))
				return true;
			if (inString.StartsWith("article-"))
				return true;
			if (YearRegex.IsMatch(inString))
				return true;
			if (GetMusicTypeK(inString) > 0)
				return true;

			return false;
		}
		#endregion

		#region GetCountryK
		public int GetCountryK(string name)
		{
			switch (name)
			{
				case "uk": return 224;
				case "spain": return 197;
				case "usa": return 225;
				case "poland": return 172;
				case "australia": return 13;
				case "south-africa": return 195;
				case "sweden": return 205;
				case "germany": return 81;
				case "new-zealand": return 154;
				case "slovenia": return 192;
				case "ireland": return 104;
				case "norway": return 161;
				case "italy": return 106;
				case "switzerland": return 206;
				case "serbia": return 237;
				case "cyprus": return 56;
				case "greece": return 84;
				case "mauritius": return 137;
				case "netherlands": return 151;
				case "portugal": return 173;
				case "russian-federation": return 178;
				case "singapore": return 190;
				case "sri-lanka": return 198;
				case "ecuador": return 63;
				case "thailand": return 211;
				case "afghanistan": return 1;
				case "albania": return 2;
				case "algeria": return 3;
				case "american-samoa": return 4;
				case "andorra": return 5;
				case "angola": return 6;
				case "anguilla": return 7;
				case "antarctica": return 8;
				case "antigua-and-barbuda": return 9;
				case "argentina": return 10;
				case "armenia": return 11;
				case "aruba": return 12;
				case "austria": return 14;
				case "azerbaijan": return 15;
				case "bahamas": return 16;
				case "bahrain": return 17;
				case "bangladesh": return 18;
				case "barbados": return 19;
				case "belarus": return 20;
				case "belgium": return 21;
				case "belize": return 22;
				case "benin": return 23;
				case "bermuda": return 24;
				case "bhutan": return 25;
				case "bolivia": return 26;
				case "bosnia-and-herzegowina": return 27;
				case "botswana": return 28;
				case "bouvet-island": return 29;
				case "brazil": return 30;
				case "british-indian-ocean-territory": return 31;
				case "british-virgin-islands": return 232;
				case "brunei-darussalam": return 32;
				case "bulgaria": return 33;
				case "burkina-faso": return 34;
				case "burundi": return 35;
				case "cambodia": return 36;
				case "cameroon": return 37;
				case "canada": return 38;
				case "cape-verde": return 39;
				case "cayman-islands": return 40;
				case "central-african-republic": return 41;
				case "chad": return 42;
				case "chile": return 43;
				case "china": return 44;
				case "christmas-island": return 45;
				case "cocos-islands": return 46;
				case "colombia": return 47;
				case "comoros": return 48;
				case "congo": return 49;
				case "cook-islands": return 51;
				case "costa-rica": return 52;
				case "cote-d-ivoire": return 53;
				case "croatia": return 54;
				case "cuba": return 55;
				case "czech-republic": return 57;
				case "democratic-peoples-republic-of-korea": return 113;
				case "democratic-republic-of-congo": return 50;
				case "denmark": return 58;
				case "djibouti": return 59;
				case "dominica": return 60;
				case "dominican-republic": return 61;
				case "east-timor": return 62;
				case "egypt": return 64;
				case "el-salvador": return 65;
				case "equatorial-guinea": return 66;
				case "estonia": return 67;
				case "ethiopia": return 68;
				case "falkland-islands": return 69;
				case "faroe-islands": return 70;
				case "fiji": return 71;
				case "finland": return 72;
				case "france": return 73;
				case "french-guiana": return 75;
				case "french-polynesia": return 76;
				case "french-southern-territories": return 77;
				case "gabon": return 78;
				case "gambia": return 79;
				case "georgia": return 80;
				case "ghana": return 82;
				case "gibraltar": return 83;
				case "greenland": return 85;
				case "grenada": return 86;
				case "guadeloupe": return 87;
				case "guam": return 88;
				case "guatemala": return 89;
				case "guinea": return 90;
				case "guinea-bissau": return 91;
				case "guyana": return 92;
				case "haiti": return 93;
				case "heard-and-mcdonald-islands": return 94;
				case "honduras": return 96;
				case "hong-kong": return 97;
				case "hungary": return 98;
				case "iceland": return 99;
				case "india": return 100;
				case "indonesia": return 101;
				case "iran": return 102;
				case "iraq": return 103;
				case "israel": return 105;
				case "jamaica": return 107;
				case "japan": return 108;
				case "jordan": return 109;
				case "kazakhstan": return 110;
				case "kenya": return 111;
				case "kiribati": return 112;
				case "kuwait": return 115;
				case "kyrgyzstan": return 116;
				case "lao-peoples-democratic-republic": return 117;
				case "latvia": return 118;
				case "lebanon": return 119;
				case "lesotho": return 120;
				case "liberia": return 121;
				case "libyan-arab-jamahiriya": return 122;
				case "liechtenstein": return 123;
				case "lithuania": return 124;
				case "luxembourg": return 125;
				case "macau": return 126;
				case "macedonia": return 127;
				case "madagascar": return 128;
				case "malawi": return 129;
				case "malaysia": return 130;
				case "maldives": return 131;
				case "mali": return 132;
				case "malta": return 133;
				case "marshall-islands": return 134;
				case "martinique": return 135;
				case "mauritania": return 136;
				case "mayotte": return 138;
				case "metropolitan-france": return 74;
				case "mexico": return 139;
				case "micronesia": return 140;
				case "moldova": return 141;
				case "monaco": return 142;
				case "mongolia": return 143;
				case "montserrat": return 144;
				case "morocco": return 145;
				case "mozambique": return 146;
				case "myanmar": return 147;
				case "namibia": return 148;
				case "nauru": return 149;
				case "nepal": return 150;
				case "netherlands-antilles": return 152;
				case "new-caledonia": return 153;
				case "nicaragua": return 155;
				case "niger": return 156;
				case "nigeria": return 157;
				case "niue": return 158;
				case "norfolk-island": return 159;
				case "northern-mariana-islands": return 160;
				case "oman": return 162;
				case "pakistan": return 163;
				case "palau": return 164;
				case "palestinian-territory": return 165;
				case "panama": return 166;
				case "papua-new-guinea": return 167;
				case "paraguay": return 168;
				case "peru": return 169;
				case "philippines": return 170;
				case "pitcairn": return 171;
				case "puerto-rico": return 174;
				case "qatar": return 175;
				case "republic-of-korea": return 114;
				case "reunion": return 176;
				case "romania": return 177;
				case "rwanda": return 179;
				case "saint-kitts-and-nevis": return 180;
				case "saint-lucia": return 181;
				case "saint-vincent-and-the-grenadines": return 182;
				case "samoa": return 183;
				case "san-marino": return 184;
				case "sao-tome-and-principe": return 185;
				case "saudi-arabia": return 186;
				case "senegal": return 187;
				case "seychelles": return 188;
				case "sierra-leone": return 189;
				case "slovakia": return 191;
				case "solomon-islands": return 193;
				case "somalia": return 194;
				case "south-georgia-and-the-south-sandwich-islands": return 196;
				case "st-helena": return 199;
				case "st-pierre-and-miquelon": return 200;
				case "sudan": return 201;
				case "suriname": return 202;
				case "svalbard-and-jan-mayen-islands": return 203;
				case "swaziland": return 204;
				case "syrian-arab-republic": return 207;
				case "taiwan": return 208;
				case "tajikistan": return 209;
				case "tanzania": return 210;
				case "togo": return 212;
				case "tokelau": return 213;
				case "tonga": return 214;
				case "trinidad-and-tobago": return 215;
				case "tunisia": return 216;
				case "turkey": return 217;
				case "turkmenistan": return 218;
				case "turks-and-caicos-islands": return 219;
				case "tuvalu": return 220;
				case "uganda": return 221;
				case "ukraine": return 222;
				case "united-arab-emirates": return 223;
				case "united-states-minor-outlying-islands": return 226;
				case "uruguay": return 227;
				case "us-virgin-islands": return 233;
				case "uzbekistan": return 228;
				case "vanuatu": return 229;
				case "vatican-city": return 95;
				case "venezuela": return 230;
				case "viet-nam": return 231;
				case "wallis-and-futuna-islands": return 234;
				case "western-sahara": return 235;
				case "yemen": return 236;
				case "zambia": return 238;
				case "zimbabwe": return 239;
				default: return 0;
			}
		}
		#endregion
		#region GetMusicTypeK
		public static int GetMusicTypeK(string name)
		{
			switch (name)
			{
				case "house": return 4;
				case "hard-dance": return 10;
				case "alternative-dance": return 15;
				case "techno": return 20;
				case "drum-and-bass": return 24;
				case "urban": return 28;
				case "alternative": return 36;
				case "commercial": return 42;
				case "retro": return 46;
				case "chillout": return 35;
				default: return 0;
			}
		}
		#endregion
		#region GetThemeK
		public static int GetThemeK(string name)
		{
			switch (name)
			{
				case "nightlife": return 1;
				case "music": return 2;
				case "photography": return 3;
				case "style": return 4;
				case "technology": return 5;
				case "entertainment": return 6;
				case "automotive": return 7;
				case "sports": return 8;
				case "news-and-politics": return 9;
				case "culture": return 10;
				case "home-and-family": return 11;
				case "food-and-drink": return 12;
				case "games": return 13;
				case "relationships": return 14;
				case "science": return 15;
				case "philosophy": return 16;
				case "business": return 17;
				case "other": return 18;
				default: return 0;
			}
		}
		#endregion

		#region GetUrlName
		public static string GetUrlName(string Name)
		{
			Regex charReg = new Regex("[0-9a-zA-Z\\-]");
			Regex multiHyphenReg = new Regex("[\\-]{2,}");
			Regex startsWithHyphenReg = new Regex("^[\\-]");
			Regex endsWithHyphenReg = new Regex("[\\-]$");
			string name = Name;
			string nameOut = "";
			for (int i = 0; i < name.Length; i++)
			{
				if (charReg.IsMatch(name.Substring(i, 1)))
					nameOut += name.Substring(i, 1).ToLower();
				else if (name.Substring(i, 1).Equals(" "))
					nameOut += "-";
				else
					nameOut += UrlNameReplacement(name.Substring(i, 1).ToLower());
			}
			nameOut = multiHyphenReg.Replace(nameOut, "-");
			nameOut = startsWithHyphenReg.Replace(nameOut, "");
			nameOut = endsWithHyphenReg.Replace(nameOut, "");

			return nameOut;
		}
		#region UrlNameReplacement
		public static string UrlNameReplacement(string inStr)
		{
			switch (inStr)
			{
				case "Í": return "I";
				case "Â": return "A";
				case "Á": return "A";
				case "Ç": return "C";
				case "Ä": return "A";
				case "Å": return "A";
				case "ú": return "u";
				case "û": return "u";
				case "ø": return "o";
				case "ù": return "u";
				case "ü": return "u";
				case "ý": return "y";
				case "ò": return "o";
				case "ó": return "o";
				case "ð": return "o";
				case "ñ": return "n";
				case "ö": return "o";
				case "ô": return "o";
				case "õ": return "o";
				case "ê": return "e";
				case "ë": return "e";
				case "è": return "e";
				case "é": return "e";
				case "î": return "i";
				case "ï": return "i";
				case "ì": return "i";
				case "í": return "i";
				case "â": return "a";
				case "ã": return "a";
				case "à": return "a";
				case "á": return "a";
				case "æ": return "ae";
				case "ç": return "c";
				case "ä": return "a";
				case "å": return "a";
				case "Ú": return "U";
				case "Û": return "U";
				case "Ø": return "O";
				case "Þ": return "D";
				case "ß": return "B";
				case "Ü": return "U";
				case "Ó": return "O";
				case "Ñ": return "N";
				case "Ö": return "O";
				case "Ô": return "O";
				case "Ê": return "E";
				case "È": return "E";
				case "É": return "E";
				case "Î": return "I";
				default: return "";
			}
		}
		#endregion
		#endregion

		#region CurrentFilter
		public string CurrentFilter
		{
			get
			{
				return currentFilter;
			}
			set
			{
				currentFilter = value;
			}
		}
		private string currentFilter;
		#endregion
		#region CurrentApplication
		public string CurrentApplication
		{
			get
			{
				return currentApplication;
			}
			set
			{
				currentApplication = value;
			}
		}
		private string currentApplication = "";
		#endregion
		#region GlobalApplication
		public bool GlobalApplication
		{
			get
			{
				return globalApplication;
			}
			set
			{
				globalApplication = value;
			}
		}
		bool globalApplication;
		#endregion

		#region CurrentUrl
		public string CurrentUrl(params object[] Params)
		{
			return CurrentUrlGeneric(null, CurrentApplication, Params);
		}
		#endregion
		#region CurrentTagsUrl
		public string CurrentTagsUrl(List<string> NewTags, params object[] Params)
		{
			return CurrentUrlGeneric(NewTags, "", Params);
		}
		#endregion
		#region CurrentUrlGeneric
		private string CurrentUrlGeneric(List<string> NewTags, string Application, params object[] Params)
		{
			Hashtable par = new Hashtable();
			for (int i = 0; i < Params.Length - 1; i = i + 2)
			{
				if (Params[i] != null)
				{
					par[Params[i].ToString()] = Params[i + 1];
				}
			}
			ArrayList alParams = new ArrayList();

			foreach (string key in this.Keys)
			{
				if (par.ContainsKey(key))
				{
					if (par[key] != null)
					{
						alParams.Add(key);
						alParams.Add(par[key].ToString());
					}
				}
				else
				{
					alParams.Add(key);
					alParams.Add(this[key].ValuePlain.ToString());
				}
			}

			foreach (string key in par.Keys)
			{
				if (!this.ContainsKey(key) && par[key] != null)
				{
					alParams.Add(key);
					alParams.Add(par[key].ToString());
				}
			}

			string currentFilter = "";

			string[] finalParams = (string[])alParams.ToArray(typeof(string));

			if (HasCustomPage)
			{
				return PageUrl(this.PageType, this.PageName, finalParams);
			}
			else
			{
				if (!HasTagFilter && NewTags == null && alParams.Count > 0 && Application.Length == 0)
					Application = "home";

				if (CurrentFilter != null)
				{
					currentFilter = CurrentFilter;
					if (currentFilter.Length > 0)
						currentFilter = currentFilter.Substring(1);
				}

				if (NewTags != null)
				{
					Application = "tags/" + ((NewTags.Count == 0 && alParams.Count > 0) ? "all" : String.Join("-", NewTags.ConvertAll(k => Cambro.Web.Helpers.UrlTextSerialize(k)).ToList().ToArray()));
				}
				else if (alParams.Count > 0 && HasTagFilter && TagFilter.Count == 0)
					Application = "tags/all";



				return MakeUrl(currentFilter, Application, finalParams);
			}
		}
		#endregion
		#region CurrentUrlApp
		public string CurrentUrlApp(string Application, params object[] Params)
		{
			return CurrentUrlGeneric(null, Application, Params);
		}
		#endregion

		#region CurrentPageWithoutParams(params object[] finalParams)
		public string CurrentPageWithoutParams(params object[] finalParams)
		{
			if (HasCustomPage)
			{
				return PageUrl(this.PageType, this.PageName, finalParams);
			}
			else
			{
				if (finalParams.Length > 0 && this.CurrentApplication.Length == 0)
					this.CurrentApplication = "home";

				if (CurrentFilter != null)
				{
					currentFilter = CurrentFilter;
					if (currentFilter.Length > 0)
						currentFilter = currentFilter.Substring(1);
				}
				return MakeUrl(currentFilter, this.CurrentApplication, finalParams);
			}
		}
		#endregion

		#region MakeUrl / PageUrl
		public static string PageUrl(string PageName, params object[] Params)
		{
			return PageUrl(PageTypes.Pages, PageName, Params);
		}
		public static string MakeUrl(object Filter, object Application, params object[] Params)
		{
			string filter = "";
			string app = "";
			string par = "";
			if (Filter != null && Filter is string && Filter.ToString().Length > 0)
			{
				if (Filter.ToString().StartsWith("/"))
					filter = Filter.ToString();
				else
					filter = "/" + Filter.ToString();
			}
			if (Application != null && Application is string && Application.ToString().Length > 0)
				app = "/" + Application.ToString();
			if ((Application == null || Application.ToString().Length == 0) && Params != null && Params.Length > 0)
				app = "/home";
			if (Params != null)
			{
				ArrayList keys = new ArrayList();
				for (int i = 0; i < Params.Length - 1; i = i + 2)
				{
					if (Params[i] != null && Params[i + 1] != null)
					{
						if (Params[i].ToString().Length > 0)
						{
							if (!keys.Contains(Params[i]))
							{
								if (Params[i + 1].ToString().Length > 0)
									par += "/" + Cambro.Web.Helpers.UrlTextSerialize(Params[i].ToString()) + "-" + Cambro.Web.Helpers.UrlTextSerialize(Params[i + 1].ToString());
								else
									par += "/" + Cambro.Web.Helpers.UrlTextSerialize(Params[i].ToString());
								keys.Add(Params[i]);
							}

						}
					}
				}
			}

			return filter + app + par;

		}
		#endregion
		#region PageUrl(HolderTypes holderType, string pageName, params string[] Params)
		public static string PageUrl(PageTypes pageType, string pageName, params object[] Params)
		{
			string filter = "/pages";
			if (pageType.Equals(PageTypes.Admin))
				filter = "/admin";
			else if (pageType.Equals(PageTypes.Blank))
				filter = "/popup";

			filter += "/" + pageName.ToLower();

			string par = "";
			if (Params != null)
			{
				ArrayList keys = new ArrayList();
				for (int i = 0; i < Params.Length - 1; i = i + 2)
				{
					if (Params[i] != null && Params[i + 1] != null)
					{
						if (!keys.Contains(Params[i]))
						{
							if (Params[i + 1].ToString().Length > 0)
								par += "/" + Cambro.Web.Helpers.UrlTextSerialize(Params[i].ToString()) + "-" + Cambro.Web.Helpers.UrlTextSerialize(Params[i + 1].ToString());
							else
								par += "/" + Cambro.Web.Helpers.UrlTextSerialize(Params[i].ToString());
							keys.Add(Params[i]);
						}
					}
				}
			}

			return filter + par;

		}
		#endregion

		#region PagePath
		/// <summary>
		/// This is the path of the user control that will be loaded into the master page - e.g. /Pages/Event.ascx
		/// </summary>
		public string PagePath
		{
			get
			{
				if (HasTagFilter)
					return "/Pages/TagSearch.ascx";
				else if (PageType == PageTypes.MixmagVote)
					return "/MixmagVote/" + PageName + ".ascx";
				else if (PageType == PageTypes.MixmagGreatest)
					return "/MixmagGreatest/" + PageName + ".ascx";
				else if (PageType == PageTypes.Mobile)
					return "/Mobile/" + PageName + ".ascx";
				else if (PageType.Equals(PageTypes.Pages))
					return "/Pages/" + PageName + ".ascx";
				else if (PageType.Equals(PageTypes.Styled))
					return "/Styled/" + PageName + ".ascx";
				else if (PageType.Equals(PageTypes.Blank))
					return "/Blank/" + PageName + ".ascx";
				else if (PageType.Equals(PageTypes.Admin))
					return "/Admin/" + PageName + ".ascx";
				else if (PageType.Equals(PageTypes.PagesFolder))
					return "/Pages/" + PageFolder + "/" + PageName + ".ascx";
				else if (PageType.Equals(PageTypes.Application))
				{
					if (HasObjectFilter)
					{
						string Folder = "";
						if (PageName.ToLower().Equals("home") && ObjectFilterType == Model.Entities.ObjectType.Gallery)
						{
							PageName = "Photos";
							if (ObjectFilterGallery.ArticleK > 0)
								Folder = "Articles";
							else if (ObjectFilterGallery.EventK > 0)
								Folder = "Events";
						}
						else if (PageName.ToLower().Equals("home") && ObjectFilterType == Model.Entities.ObjectType.Photo)
						{
							PageName = "Photos";
							if (ObjectFilterPhoto.ArticleK > 0)
								Folder = "Articles";
							else if (ObjectFilterPhoto.EventK > 0)
								Folder = "Events";
						}
						else
						{
							switch (ObjectFilterType)
							{
								case Model.Entities.ObjectType.Brand: Folder = (PageName.ToLower().Equals("home") ? "Groups" : "Brands"); break;
								case Model.Entities.ObjectType.Country: Folder = "Countries"; break;
								case Model.Entities.ObjectType.Event: Folder = "Events"; break;
								case Model.Entities.ObjectType.Place: Folder = "Places"; break;
								case Model.Entities.ObjectType.Promoter: Folder = "Promoters"; break;
								case Model.Entities.ObjectType.Usr: Folder = "Usrs"; break;
								case Model.Entities.ObjectType.Venue: Folder = "Venues"; break;
								case Model.Entities.ObjectType.Region: Folder = "Regions"; break;
								case Model.Entities.ObjectType.Gallery: Folder = "Galleries"; break;
								case Model.Entities.ObjectType.Photo: Folder = "Photos"; break;
								case Model.Entities.ObjectType.Article: Folder = "Articles"; break;
								case Model.Entities.ObjectType.Group: Folder = "Groups"; break;
								default:
									throw new Exception("Funny ObjectFilterType in ControlName");
							}
						}
						return "/Pages/" + Folder + "/" + PageName + ".ascx";
					}
					else
						throw new Bobs.MalformedUrlException();
					//throw new Exception("ControlType = Application but not HasObjectFilter") ;
				}
				throw new Exception("Funny ControlType in ControlName");

			}
		}
		#endregion

		string PageFolder { get; set; }

		#region MasterPath
		public string MasterPath
		{
			get
			{
				switch (PageType)
				{
					case PageTypes.Pages:
					case PageTypes.PagesFolder:
					case PageTypes.Application:
						return "/Master/DsiPage.aspx";
					case PageTypes.MixmagVote:
						return "/Master/MixmagVotePage.aspx";
					case PageTypes.MixmagGreatest:
						return "/Master/MixmagGreatestPage.aspx";
					case PageTypes.Mobile:
						return "/Master/MobilePage.aspx";
					case PageTypes.Styled:
						return "/Master/StyledPage.aspx";
					case PageTypes.Blank:
						return "/Master/BlankPage.aspx";
					case PageTypes.Admin:
						return "/Master/AdminPage.aspx";
					default: return "";
				}
			}
		}
		#endregion

		#region OverrideHttpHandler
		/// <summary>
		/// If this is true, the custom httphandler will be bypassed - used for any files with an extention.
		/// </summary>
		public bool OverrideHttpHandler { get; private set; }
		#endregion
		#region IsAjaxRequest
		/// <summary>
		/// Is true, must avoid any superfluous Response.Write()s, otherwise will confuse returned Ajax
		/// </summary>
		public bool IsAjaxRequest { get; private set; }
		#endregion

		#region UrlPartTypes
		/// <summary>
		/// Each part of the url is one of these types
		/// </summary>
		public enum UrlPartTypes
		{
			Null,
			/// <summary>
			/// e.g. promoter, party, member, dj etc.
			/// </summary>
			ObjectFilter,
			/// <summary>
			/// e.g. chat
			/// </summary>
			Application,
			/// <summary>
			/// e.g. hard-dance
			/// </summary>
			MusicFilter,
			/// <summary>
			/// e.g. style
			/// </summary>
			ThemeFilter,
			/// <summary>
			/// e.g. month or day
			/// </summary>
			DateFilter,
			/// <summary>
			/// e.g. tags
			/// </summary>
			TagFilter
		}
		#endregion

		#region PageType
		public PageTypes PageType { get; set; }
		public enum PageTypes
		{
			/// <summary>
			/// Custom page from the Pages dir
			/// </summary>
			Pages,
			/// <summary>
			/// Custom popup page from the Popup dir
			/// </summary>
			Blank,
			/// <summary>
			/// Custom admin page from the Admin dir
			/// </summary>
			Admin,
			/// <summary>
			/// Custom object application from the Pages/[object] dir
			/// </summary>
			Application,
			/// <summary>
			/// Custom page from the Pages/[object] dir
			/// </summary>
			PagesFolder,
			/// <summary>
			/// Simple page styled sing a custom CSS file
			/// </summary>
			Styled,
			MixmagVote,
			MixmagGreatest,
			Mobile
		}
		#endregion
		#region PageName
		public string PageName { get; set; }
		#endregion

		#region Object filter
		#region HasObjectFilter
		public bool HasObjectFilter
		{
			get
			{
				return hasObjectFilter;
			}
			set
			{
				hasObjectFilter = value;
			}
		}
		private bool hasObjectFilter;
		#endregion
		#region ObjectFilterType
		public Model.Entities.ObjectType ObjectFilterType
		{
			get
			{
				return objectFilterType;
			}
			set
			{
				objectFilterBob = null;
				objectFilterType = value;
			}
		}
		private Model.Entities.ObjectType objectFilterType;
		#endregion
		#region ObjectFilterK
		public int ObjectFilterK
		{
			get
			{
				return objectFilterK;
			}
			set
			{
				objectFilterBob = null;
				objectFilterK = value;
			}
		}
		private int objectFilterK;
		#endregion
		#region ObjectFilterBob
		public IBob ObjectFilterBob
		{
			get
			{
				if (objectFilterBob == null && HasObjectFilter)
				{
					switch (ObjectFilterType)
					{
						case Model.Entities.ObjectType.Brand: objectFilterBob = new Brand(ObjectFilterK); break;
						case Model.Entities.ObjectType.Country: objectFilterBob = new Country(ObjectFilterK); break;
						case Model.Entities.ObjectType.Event: objectFilterBob = new Event(ObjectFilterK); break;
						case Model.Entities.ObjectType.Place: objectFilterBob = new Place(ObjectFilterK); break;
						case Model.Entities.ObjectType.Promoter: objectFilterBob = new Promoter(ObjectFilterK); break;
						case Model.Entities.ObjectType.Usr: objectFilterBob = new Usr(ObjectFilterK); break;
						case Model.Entities.ObjectType.Venue: objectFilterBob = new Venue(ObjectFilterK); break;
						case Model.Entities.ObjectType.Region: objectFilterBob = new Region(ObjectFilterK); break;
						case Model.Entities.ObjectType.Gallery: objectFilterBob = new Gallery(ObjectFilterK); break;
						case Model.Entities.ObjectType.Photo: objectFilterBob = new Photo(ObjectFilterK); break;
						case Model.Entities.ObjectType.Article: objectFilterBob = new Article(ObjectFilterK); break;
						case Model.Entities.ObjectType.Group: objectFilterBob = new Bobs.Group(ObjectFilterK); break;
						default: break;
					}
				}
				return objectFilterBob;
			}
			set
			{
				objectFilterBob = value;
			}
		}
		private IBob objectFilterBob;
		#endregion
		#region ResetObjectFilterObject()
		public void ResetObjectFilterObject()
		{
			objectFilterBob = null;
		}
		#endregion
		#region Typed object accessors
		public bool HasBrandObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Brand); } }
		public bool HasCountryObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Country); } }
		public bool HasEventObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Event); } }
		public bool HasPlaceObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Place); } }
		public bool HasPromoterObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Promoter); } }
		public bool HasUsrObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Usr); } }
		public bool HasVenueObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Venue); } }
		public bool HasRegionObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Region); } }
		public bool HasGalleryObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Gallery); } }
		public bool HasPhotoObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Photo); } }
		public bool HasArticleObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Article); } }
		public bool HasGroupObjectFilter { get { return HasObjectFilter && ObjectFilterType.Equals(Model.Entities.ObjectType.Group); } }
		public Brand ObjectFilterBrand { get { return ObjectFilterBob as Brand; } }
		public Country ObjectFilterCountry { get { return ObjectFilterBob as Country; } }
		public Event ObjectFilterEvent { get { return ObjectFilterBob as Event; } }
		public Place ObjectFilterPlace { get { return ObjectFilterBob as Place; } }
		public Promoter ObjectFilterPromoter { get { return ObjectFilterBob as Promoter; } }
		public Usr ObjectFilterUsr { get { return ObjectFilterBob as Usr; } }
		public Venue ObjectFilterVenue { get { return ObjectFilterBob as Venue; } }
		public Region ObjectFilterRegion { get { return ObjectFilterBob as Region; } }
		public Gallery ObjectFilterGallery { get { return ObjectFilterBob as Gallery; } }
		public Photo ObjectFilterPhoto { get { return ObjectFilterBob as Photo; } }
		public Article ObjectFilterArticle { get { return ObjectFilterBob as Article; } }
		public Bobs.Group ObjectFilterGroup { get { return ObjectFilterBob as Bobs.Group; } }
		#endregion
		#region HasBrandLogicalFilter
		public bool HasBrandLogicalFilter
		{
			get
			{
				return (HasBrandObjectFilter || (HasGroupObjectFilter && ObjectFilterGroup.BrandK > 0));
			}
		}
		#endregion
		#region LogicalFilterBrandK
		public int LogicalFilterBrandK
		{
			get
			{
				if (HasBrandObjectFilter)
					return ObjectFilterBrand.K;
				else
					return ObjectFilterGroup.BrandK;
			}
		}
		#endregion
		#region LogicalFilterBrand
		public Brand LogicalFilterBrand
		{
			get
			{
				if (HasBrandObjectFilter)
					return ObjectFilterBrand;
				else
					return ObjectFilterGroup.Brand;
			}
		}
		#endregion
		#region HasGroupLogicalFilter
		public bool HasGroupLogicalFilter
		{
			get
			{
				return (HasBrandObjectFilter || HasGroupObjectFilter);
			}
		}
		#endregion
		#region LogicalFilterGroupK
		public int LogicalFilterGroupK
		{
			get
			{
				if (HasGroupObjectFilter)
					return ObjectFilterGroup.K;
				else
					return ObjectFilterBrand.GroupK;
			}
		}
		#endregion
		#region LogicalFilterGroup
		public Group LogicalFilterGroup
		{
			get
			{
				if (HasGroupObjectFilter)
					return ObjectFilterGroup;
				else
					return ObjectFilterBrand.Group;
			}
		}
		#endregion
		#endregion
		#region Music filter
		#region HasMusicFilter
		public bool HasMusicFilter
		{
			get
			{
				return hasMusicFilter;
			}
			set
			{
				hasMusicFilter = value;
			}
		}
		private bool hasMusicFilter;
		#endregion
		#region MusicFilterK
		public int MusicFilterK
		{
			get
			{
				return musicFilterK;
			}
			set
			{
				musicFilterK = value;
			}
		}
		private int musicFilterK;
		#endregion
		#endregion
		#region Theme filter
		#region HasThemeFilter
		public bool HasThemeFilter
		{
			get
			{
				return hasThemeFilter;
			}
			set
			{
				hasThemeFilter = value;
			}
		}
		private bool hasThemeFilter;
		#endregion
		#region ThemeFilterK
		public int ThemeFilterK
		{
			get
			{
				return themeFilterK;
			}
			set
			{
				themeFilterK = value;
			}
		}
		private int themeFilterK;
		#endregion
		#endregion
		#region Time filter
		#region HasYearFilter
		public bool HasYearFilter
		{
			get
			{
				return hasYearFilter;
			}
			set
			{
				hasYearFilter = value;
			}
		}
		private bool hasYearFilter;
		#endregion
		#region HasMonthFilter
		public bool HasMonthFilter
		{
			get
			{
				return hasMonthFilter;
			}
			set
			{
				hasMonthFilter = value;
			}
		}
		private bool hasMonthFilter;
		#endregion
		#region HasDayFilter
		public bool HasDayFilter
		{
			get
			{
				return hasDayFilter;
			}
			set
			{
				hasDayFilter = value;
			}
		}
		private bool hasDayFilter;
		#endregion
		#region DateFilter
		public DateTime DateFilter
		{
			get
			{
				return dateFilter;
			}
			set
			{
				dateFilter = value;
			}
		}
		private DateTime dateFilter;
		#endregion
		#endregion
		#region Tags filter
		public bool HasTagFilter { get; set; }
		public List<string> TagFilter
		{
			get
			{
				if (tagFilter == null)
					tagFilter = new List<string>();

				return tagFilter;

			}
			set
			{
				tagFilter = value;
			}
		}
		private List<string> tagFilter;
		#endregion

		#region UrlPart
		public class UrlPart
		{
			#region public UrlPart(string raw)
			public UrlPart(string raw, int index)
			{
				this.Exists = true;
				this.Raw = raw;
				this.Index = index;
				if (raw.IndexOf("-") > -1)
				{
					this.Key = Cambro.Web.Helpers.UrlTextDeSerialize(raw.Substring(0, raw.IndexOf("-"))).ToLower();
					this.Value = Cambro.Web.Helpers.UrlTextDeSerialize(raw.Substring(raw.IndexOf("-") + 1));
					this.ValuePlain = this.Value;
				}
				else
				{
					this.Key = Cambro.Web.Helpers.UrlTextDeSerialize(raw).ToLower();
					this.Value = Cambro.Web.Helpers.UrlTextDeSerialize(raw).ToLower();
					this.ValuePlain = "";
				}
			}
			#endregion
			public bool HasKeyValuePair()
			{
				return Raw.IndexOf("-") > -1;
			}
			#region public UrlPart(string key, bool nullUrlPart)
			public UrlPart(string key, bool nullUrlPart)
			{
				this.Exists = false;
				this.Index = 0;
				this.Raw = key;
				this.Key = key;
				this.Value = "";
				this.ValuePlain = "";
			}
			#endregion
			#region public UrlPart(int index, bool nullUrlPart)
			public UrlPart(int index, bool nullUrlPart)
			{
				this.Exists = false;
				this.Index = index;
				this.Raw = "";
				this.Key = "";
				this.Value = "";
				this.ValuePlain = "";
			}
			#endregion
			#region Index
			public int Index { get; set; }
			#endregion
			#region Exists
			public bool Exists { get; set; }
			#endregion
			#region IsNull
			public bool IsNull
			{
				get
				{
					return !Exists;
				}
			}
			#endregion
			#region Raw
			public string Raw { get; set; }
			#endregion
			#region Key
			public string Key { get; set; }
			#endregion
			#region KeyInt
			public int KeyInt
			{
				get
				{
					// use TryParse, tis faster than catching exceptions. http://www.codinghorror.com/blog/archives/000358.html
					int i;
					if (int.TryParse(Key, out i))
					{
						return i;
					}
					return 0;

					// instead of
					//try
					//{
					//    return int.Parse(Key);
					//}
					//catch
					//{
					//    return 0;
					//}
				}
			}
			#endregion
			#region Value
			public string Value { get; set; }
			#endregion
			#region ValueInt
			public int ValueInt
			{
				get
				{
					if (Exists)
					{
						int i;
						if (int.TryParse(Value, out i))
						{
							return i;
						}
						return 0;
					}
					return 0;
				}
			}
			#endregion
			#region ValuePlain
			public string ValuePlain { get; set; }
			#endregion
			#region IsInt
			public bool IsInt
			{
				get
				{
					if (Exists)
					{
						// use TryParse, tis faster than catching exceptions. http://www.codinghorror.com/blog/archives/000358.html
						int i;
						return int.TryParse(Value, out i);

						// instead of
						//try
						//{
						//    int i = int.Parse(Value);
						//    return true;
						//}
						//catch
						//{
						//    return false;
						//}
					}
					else
						return false;
				}
			}
			#endregion
			#region Operators
			public override string ToString()
			{
				return Value.ToLower();
			}
			public override bool Equals(object o)
			{
				return this.Exists && this.Value.ToLower().Equals(o.ToString().ToLower());
			}
			public static bool operator ==(UrlPart url, string s)
			{
				return url.Equals(s);
			}
			public static bool operator !=(UrlPart url, string s)
			{
				return !url.Equals(s);
			}
			public static bool operator ==(UrlPart url, int i)
			{
				return url.Equals(i);
			}
			public static bool operator !=(UrlPart url, int i)
			{
				return !url.Equals(i);
			}
			public override int GetHashCode()
			{
				return Raw.GetHashCode();
			}
			public static implicit operator string(UrlPart url)
			{
				return url.ToString();
			}
			public static implicit operator int(UrlPart url)
			{
				return url.ValueInt;
				//if (url.IsInt)
				//    return int.Parse(url.Value);
				//else
				//    return 0;
			}
			#endregion
		}
		#region UrlPartConverter - deleted
		//		public class UrlPartConverter : TypeConverter 
		//		{
		//			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		//			{
		//				return base.ConvertFrom (context, culture, value);
		//			}
		//			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
		//			{
		//				return base.CanConvertFrom(context, sourceType);
		//			}
		//			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) 
		//			{
		//				if (destinationType == typeof(string)) 
		//					return true;
		//				if (destinationType == typeof(int)) 
		//					return true;
		//				return base.CanConvertTo(context, destinationType);
		//			}
		//			public override object ConvertTo(ITypeDescriptorContext context, 
		//				CultureInfo culture, object value, Type destinationType) 
		//			{
		//				if (destinationType == typeof(string)) 
		//					return ((UrlPart)value).Value.ToLower();
		//				if (destinationType == typeof(int)) 
		//					return int.Parse(((UrlPart)value).Value);
		//				return base.ConvertTo(context, culture, value, destinationType);
		//			}
		//		}
		#endregion
		#endregion
		#region UrlPart collection methods
		protected Hashtable innerHash;
		protected Hashtable innerIndex;

		#region Constructors
		public UrlInfo()
		{
			innerHash = new Hashtable();
			innerIndex = new Hashtable();
		}

		#region Constructors - deleted
		//		public UrlInfo(UrlInfo original)
		//		{
		//			innerHash = new Hashtable (original.innerHash);
		//		}
		//	
		//		public UrlInfo(IDictionary dictionary)
		//		{
		//			innerHash = new Hashtable(dictionary);
		//		}
		//	
		//		public UrlInfo(int capacity)
		//		{
		//			innerHash = new Hashtable(capacity);
		//		}
		//	
		//		public UrlInfo(IDictionary dictionary, float loadFactor)
		//		{
		//			innerHash = new Hashtable(dictionary, loadFactor);
		//		}
		//	
		//		public UrlInfo(IHashCodeProvider codeProvider, IComparer comparer)
		//		{
		//			innerHash = new Hashtable (codeProvider, comparer);
		//		}
		//	
		//		public UrlInfo(int capacity, int loadFactor)
		//		{
		//			innerHash = new Hashtable(capacity, loadFactor);
		//		}
		//	
		//		public UrlInfo(IDictionary dictionary, IHashCodeProvider codeProvider, IComparer comparer)
		//		{
		//			innerHash = new Hashtable (dictionary, codeProvider, comparer);
		//		}
		//	
		//		public UrlInfo(int capacity, IHashCodeProvider codeProvider, IComparer comparer)
		//		{
		//			innerHash = new Hashtable (capacity, codeProvider, comparer);
		//		}
		//	
		//		public UrlInfo(IDictionary dictionary, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		//		{
		//			innerHash = new Hashtable (dictionary, loadFactor, codeProvider, comparer);
		//		}
		//	
		//		public UrlInfo(int capacity, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		//		{
		//			innerHash = new Hashtable (capacity, loadFactor, codeProvider, comparer);
		//		}
		#endregion
		#endregion

		#region Implementation of IDictionary
		public UrlInfoEnumerator GetEnumerator()
		{
			return new UrlInfoEnumerator(this);
		}

		System.Collections.IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new UrlInfoEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Remove(string key)
		{
			throw new NotImplementedException();
			//if (this.ContainsKey(key.ToLower()))
			//{
			//    innerIndex.Remove(this[key.ToLower()].Index);
			//    innerHash.Remove(key.ToLower());
			//}
		}

		void IDictionary.Remove(object key)
		{
			Remove((string)key);
		}

		public bool Contains(string key)
		{
			return innerHash.Contains(key.ToLower());
		}

		bool IDictionary.Contains(object key)
		{
			return Contains((string)key);
		}

		public void Clear()
		{
			innerHash.Clear();
			innerIndex.Clear();
		}

		public void Add(string key, UrlPart value)
		{
			if (!this.Contains(key))
				innerHash.Add(key.ToLower(), value);

			innerIndex.Add(value.Index, value);
		}

		void IDictionary.Add(object key, object value)
		{
			Add((string)key, (UrlPart)value);
		}

		public bool IsReadOnly
		{
			get
			{
				return innerHash.IsReadOnly;
			}
		}

		public UrlPart this[int index]
		{
			get
			{
				if (innerIndex[index] == null)
					return new UrlPart(index, true);
				else
					return (UrlPart)innerIndex[index];
			}
			set
			{
				innerHash[value.Key.ToLower()] = value;
				innerIndex[index] = value;
			}
		}

		public UrlPart this[string key]
		{
			get
			{
				if (innerHash[key.ToLower()] == null)
					return new UrlPart(key.ToLower(), true);
				else
					return (UrlPart)innerHash[key.ToLower()];
			}
			set
			{
				innerHash[key.ToLower()] = value;
				innerIndex[value.Index] = value;
			}
		}

		object IDictionary.this[object key]
		{
			get
			{
				if (key is string)
					return this[(string)key];
				else if (key is int)
					return this[(int)key];
				else
					return null;
			}
			set
			{
				if (key is string)
					this[(string)key] = (UrlPart)value;
				else if (key is int)
					this[(int)key] = (UrlPart)value;

			}
		}

		public System.Collections.ICollection Values
		{
			get
			{
				return innerHash.Values;
			}
		}

		public System.Collections.ICollection Keys
		{
			get
			{
				return innerHash.Keys;
			}
		}

		public bool IsFixedSize
		{
			get
			{
				return innerHash.IsFixedSize;
			}
		}
		#endregion

		#region Implementation of ICollection
		public void CopyTo(System.Array array, int index)
		{
			innerHash.CopyTo(array, index);
		}

		public bool IsSynchronized
		{
			get
			{
				return innerHash.IsSynchronized;
			}
		}

		public int Count
		{
			get
			{
				return innerIndex.Count;
			}
		}

		public object SyncRoot
		{
			get
			{
				return innerHash.SyncRoot;
			}
		}
		#endregion

		#region Implementation of ICloneable
		public UrlInfo Clone()
		{
			UrlInfo clone = new UrlInfo();
			clone.innerHash = (Hashtable)innerHash.Clone();
			clone.innerIndex = (Hashtable)innerIndex.Clone();

			return clone;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}
		#endregion

		#region HashTable Methods
		public bool ContainsKey(string key)
		{
			return innerHash.ContainsKey(key);
		}

		public bool ContainsValue(UrlPart value)
		{
			return innerIndex.ContainsValue(value);
		}

		public static UrlInfo Synchronized(UrlInfo nonSync)
		{
			UrlInfo sync = new UrlInfo();
			sync.innerHash = Hashtable.Synchronized(nonSync.innerHash);
			sync.innerIndex = Hashtable.Synchronized(nonSync.innerIndex);

			return sync;
		}
		#endregion

		#region InnerHash
		internal Hashtable InnerHash
		{
			get
			{
				return innerHash;
			}
		}
		#endregion
		#region InnerIndex
		internal Hashtable InnerIndex
		{
			get
			{
				return innerIndex;
			}
		}
		#endregion

		#region UrlInfoEnumerator
		public class UrlInfoEnumerator : IDictionaryEnumerator
		{
			private IDictionaryEnumerator innerEnumerator;

			internal UrlInfoEnumerator(UrlInfo enumerable)
			{
				innerEnumerator = enumerable.InnerIndex.GetEnumerator();
			}

			#region Implementation of IDictionaryEnumerator
			public string Key
			{
				get
				{
					return (string)((UrlPart)innerEnumerator.Value).Key;
				}
			}

			object IDictionaryEnumerator.Key
			{
				get
				{
					return ((UrlPart)innerEnumerator.Value).Key;
				}
			}

			public UrlPart Value
			{
				get
				{
					return (UrlPart)innerEnumerator.Value;
				}
			}

			object IDictionaryEnumerator.Value
			{
				get
				{
					return Value;
				}
			}

			public System.Collections.DictionaryEntry Entry
			{
				get
				{
					return innerEnumerator.Entry;
				}
			}
			#endregion

			#region Implementation of IEnumerator
			public void Reset()
			{
				innerEnumerator.Reset();
			}

			public bool MoveNext()
			{
				return innerEnumerator.MoveNext();
			}

			public object Current
			{
				get
				{
					return innerEnumerator.Current;
				}
			}
			#endregion
		}
		#endregion
		#endregion


		#region LoggedInHotmailDetected
		public bool LoggedInHotmailDetected
		{
			get
			{
				return loggedInHotmailDetected;
			}
			set
			{
				loggedInHotmailDetected = value;
			}
		}
		private bool loggedInHotmailDetected = false;
		#endregion
		#region LoggedInPlainUrl
		public string LoggedInPlainUrl
		{
			get
			{
				return loggedInPlainUrl;
			}
			set
			{
				loggedInPlainUrl = value;
			}
		}
		private string loggedInPlainUrl = "/";
		#endregion

		public int LoginPartUsrK = 0;
		public string LoginPartLoginString = "";
		public bool LoginPartLogOutFirst = false;
		public string LoginPartUsrEmail = "";
		public bool LoginPartUsrIsSkeleton = false;
		public bool LoginPartUsrIsEnhancedSecurity = false;
		public bool LoginPartUsrIsFacebookNotConfirmed = false;
		public bool LoginPartUsrNeedsCaptcha = false;
		public string LoginPartUsrCaptchaEncrypted = "";
		public int LoginPartUsrHomePlaceK = 0;
		public int LoginPartUsrFavouriteMusicK = 0;
		public bool LoginPartUsrSendSpottedEmails = false;
		public bool LoginPartUsrSendEflyers = false;
		public int MixmagGreatestDjK = 0;

		#region LoginFailed
		public bool LoginFailed
		{
			get
			{
				return loginFailed;
			}
			set
			{
				loginFailed = value;
			}
		}
		private bool loginFailed;
		#endregion
		
	}
	#endregion
}
