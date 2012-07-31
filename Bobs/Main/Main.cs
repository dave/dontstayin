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
using Model.Entities.Properties;

namespace Bobs
{



	#region Vars
	public class Vars
	{

		public static bool FacebookCanvasMode
		{
			get
			{
				return Prefs.Current["FacebookCanvasMode1"] == 1;
			}
		}

		public static DataTable GetMemcachedStats() { return Caching.Instances.Main.GetStats(); }
		public static void FlushAllMemcached() { Caching.Instances.Main.FlushAll(); }

		#region SqlConnectionTimeout
		public static bool SetSqlConnectionTimeout { get { return true; } }
		public static int SqlConnectionTimeout { get { return 120; } }
		#endregion

		#region BannerDesignPrice
		public static double BannerDesignPrice(Banner.DesignTypes type)
		{
			switch (type)
			{
				case Banner.DesignTypes.Jpg:
					return 30.0;
				case Banner.DesignTypes.Gif:
					return 50.0;
				case Banner.DesignTypes.Flash:
					return 100.0;
				default:
					return 0.0;
			}
		}
		#endregion

		#region BannerDesignPriceCredits
		public static int BannerDesignPriceCredits(Banner.DesignTypes type)
		{
			switch (type)
			{
				case Banner.DesignTypes.Jpg:
					return 30;
				case Banner.DesignTypes.Gif:
					return 50;
				case Banner.DesignTypes.Flash:
					return 100;
				default:
					return 0;
			}
		}
		#endregion

		#region EventHighlightPriceCredits
		public static int EventHighlightPriceCredits(Event ev)
		{
			try
			{
				if (ev.Venue.Capacity < 500)
					return 50;
				else if (ev.Venue.Capacity < 1000)
					return 100;
				else
					return 200;
			}
			catch (Exception ex)
			{
				string emailBody = "<p>Exception occurred in Vars.EventHilightPriceCredits()</p><p>" + (ev != null ? ev.AsHTML() : "Event = null") + "</p>";
				Utilities.AdminEmailAlert(emailBody, "Exception occurred in Vars.EventHilightPriceCredits()", ex, ev);
				return 200;
			}
		}
		#endregion

		public static bool UseOnlyAmazonForRead
		{
			get
			{
				return true;
				//	if (System.Environment.MachineName == "SOLO")
				//		return true;
				//	else
				//		return false;
			}

		}

		public static bool UseOnlyAmazonForWrite
		{
			get
			{
				return true;
			//	if (System.Environment.MachineName == "SOLO")
			//		return true;
			//	else
			//		return false;
			}
		}

		public static bool Black
		{
			get
			{
				return Vars.DevEnv || (DateTime.Now > new DateTime(2008, 7, 12, 18, 0, 0) && DateTime.Now < new DateTime(2008, 7, 13, 09, 0, 0));
			}
		}
		public static bool Zoo
		{
			get
			{
				return (DateTime.Now > new DateTime(2008, 5, 18) && DateTime.Now < new DateTime(2008, 6, 2));
			}
		}
		public static bool Takeover
		{
			get
			{
				return (DateTime.Now > new DateTime(2009, 1, 12) && DateTime.Now < new DateTime(2009, 1, 26));
			}
		}
		public static bool ZooFull
		{
			get
			{
				return DateTime.Now > new DateTime(2008, 5, 25) && DateTime.Now < new DateTime(2008, 6, 2);
			}
		}
		// this is just for the takeover
		public static bool SonyEricsson
		{
			get
			{
				return DateTime.Now > new DateTime(2008, 6, 30) && DateTime.Now < new DateTime(2008, 7, 7);
			}
		}
		// this is for still enabling features of campaign
		public static bool SonyEricsson2
		{
			get
			{
				return DateTime.Now > new DateTime(2008, 6, 30) && DateTime.Now < new DateTime(2008, 9, 4); // todo: end date
			}
		}
		// this is for the smaller box on the front page
		public static bool SonyEricsson3
		{
			get
			{
				return DateTime.Now > new DateTime(2008, 7, 11) && DateTime.Now < new DateTime(2008, 9, 1); 
			}
		}
		public static bool Gsss
		{
			get
			{
				return false;
			}
		}
		public static bool Heat
		{
			get
			{
				return false;
			}
		}

		public static bool Creamfields
		{
			get { return DateTime.Now > new DateTime(2008, 7, 28) && DateTime.Now < new DateTime(2008, 8, 11); }
		}

		public static bool Creamfields2
		{
			get { return !Vars.DevEnv && DateTime.Now > new DateTime(2008, 8, 11) && DateTime.Now < new DateTime(2008, 8, 18); }
		}


		//0%-------------------------------------------------------X-----------------------------Y-------------------100%
		//-------------------------Promoter------------------------|---------Addvantage----------|------Google-------|
		//Customise this bit to adjust percentage we give to Promoters / Addvantage / google
		//**********************************************************************************
		public const double LeaderboardX = 100.0;
		public const double LeaderboardY = 100.0;

		public const double SkyscraperX = 100.0;
		public const double SkyscraperY = 100.0;

		public const double HotboxX = 100.0;
		public const double HotboxY = 100.0;
		//**********************************************************************************
		//public const double LeaderboardX = 33.0;
		//public const double LeaderboardY = 66.0;
		//public const double SkyscraperX = 33.0;
		//public const double SkyscraperY = 66.0;
		//public const double HotboxX = 33.0;
		//public const double HotboxY = 66.0;


		//public const double LeaderboardX = 100.0;
		//public const double LeaderboardY = 100.0;
		//public const double SkyscraperX = 100.0;
		//public const double SkyscraperY = 100.0;
		//public const double HotboxX = 100.0;
		//public const double HotboxY = 100.0;


		public static int InvoiceDueDaysDefault { get { return 30; } }

		public static bool DevEnvPix { get { return Common.Properties.MachineName == "xDEV1" ? Vars.DevEnv : Common.Properties.IsDevelopmentDatabase; } }
		public static bool UseNewChatClient 
		{ 
			get 
			{
				return true;
			} 
		}

		#region MapPath
		public static string MapPath(string path)
		{
			if (HttpContext.Current != null)
				return HttpContext.Current.Server.MapPath(path);
			else if (Vars.DevEnv)
			{
                if (Common.Properties.MachineName.Equals("DEV1") || Common.Properties.MachineName.Equals("DEV2") || Common.Properties.MachineName.Equals("SOLO") || Common.Properties.MachineName.Equals("CORE") || Common.Properties.MachineName.Equals("HOTH") || Common.Properties.MachineName.Equals("NABOO") || Common.Properties.MachineName.Equals("ENDOR"))
					return path.Replace("~", @"C:\Source\" + Common.Properties.CurrentBranchName + @"\Spotted\Spotted").Replace("/", "\\");
				else
					return path.Replace("~", @"C:\Release\Test\Spotted\Spotted").Replace("/", "\\");
			}
			else
				return path.Replace("~", @"\\extra\Spotted").Replace("/", "\\");
		}
		#endregion

		public static bool CaptionIsBrand = true;
		public static int CaptionBrandK = 0;//2126;
		public static int CompetitionGroupK = Common.Settings.CompetitionGroupK;
		public static bool IsCompetitionActive { get { return Common.Settings.CompetitionEnabled == Settings.CompetitionEnabledOption.On; } }
		public static bool IsCompetitionGroupActive(int groupK)
		{
			return (groupK == CompetitionGroupK) && IsCompetitionActive;
		}

		#region HeatWeek
		public static bool HeatWeek
		{
			get
			{
				return (DateTime.Now >= new DateTime(2007, 8, 20) && DateTime.Now <= new DateTime(2007, 8, 26));
			}
		}
		#endregion
		#region IsBeta
		public static bool IsBeta
		{
			get
			{
				if (DevEnv)
					return true;
				else
					return HttpContext.Current != null && 
						(HttpContext.Current.Request.Url.Host.Equals("www1.dontstayin.com") || 
						HttpContext.Current.Request.Url.Host.Equals("test.dontstayin.com") || 
						HttpContext.Current.Request.Url.Host.Equals("dsi.cambro.net"));
			}
		}
		#endregion
		#region LanguageString
		public static string LanguageString
		{
			get
			{
				if (HttpContext.Current != null)
					return (HttpContext.Current.Request.Headers["Accept-Language"] == null
						|| HttpContext.Current.Request.Headers["Accept-Language"].Length == 0) ? "true" : "false";
				else
					return "false";
			}
		}
		#endregion

		#region Constants
		public const int ContentWidth = 600;
		public const int CommentsPerPage = 20;
		public const int ThreadsPerPage = 20;
		public const int GalleryPageSize = 15;
		public const string EMAIL_ADDRESS_DEV_TEAM = "dev.mail@dontstayin.com";
		public const string EMAIL_ADDRESS_ACCOUNTS = "accounts@dontstayin.com";
		public const string EMAIL_ADDRESS_MAIL = "mail@dontstayin.com";
		public const string EMAIL_ADDRESS_PAYMENTS = "payments@dontstayin.com";
		public const string EMAIL_ADDRESS_TICKETS = "tickets@dontstayin.com";
		public const string EMAIL_ADDRESS_DAVE = "d.brophy@dontstayin.com";
		public const string EMAIL_ADDRESS_TIM = "tim@dontstayin.com";
		public const string EMAIL_ADDRESS_TIMI = "tim.iles@dontstayin.com";
		public const string EMAIL_ADDRESS_HARRY = "harry@dontstayin.com";
		public const int IBUYABLE_LOCK_SECONDS = 60;
		public const int TICKETS_MAX_PER_USR = 20;
		public const int TICKETS_MAX_PER_CARD = 20;
		public const int TICKETS_PENDING_SECONDS = 20;
		public const int TICKETS_RESERVE_SECONDS = 120;
		// Give an additional buffer of 5 seconds so users dont try again prematuraly (as they've shown to do)
		public static readonly string TICKETS_PLEASE_TRY_AGAIN_IN = "Please try again in " + ((int)(Vars.TICKETS_RESERVE_SECONDS + 5)).ToString() + " seconds.";
		public const string CANT_VIEW_DETAILS = "You can't view these details.";
		public const string URL_TICKET_APPLICATION_FORM = "/pages/ticketapplicationform";
		public const string URL_TICKET_INSTRUCTIONS = "/pages/ticketinstructions";
		public const string HTML_LINE_RETURN = "<br>";
		public const int DAYS_TO_SHOW_TICKET_FEEDBACK_ALERT = -14;
		#endregion

		#region UserIpAddress
		public static string UserIpAddress
		{
			get
			{
				try
				{
					if (HttpContext.Current.Request.ServerVariables["REMOTE_HOST"] != null)
					{
						return Utilities.TruncateIp(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);
					}
				}
				catch { }

				return string.Empty;
			}
		}
		#endregion

		public static double VatMultipT1(DateTime d)
		{
			if (d >= new DateTime(2008, 12, 1) && d < new DateTime(2010, 1, 1))
				return 0.15;
			else if (d >= new DateTime(2011, 1, 4))
				return 0.20;
			else
				return 0.175;
		}

		#region TraceQueries
		public static bool TraceQueries
		{
			get
			{
				return false;
				//return Vars.DevEnv;
			}
		}
		#endregion

		#region ExtraIp
		public static string ExtraIp
		{
			get
			{
				return "84.45.14.31";
			}
		}
		#endregion
		#region PixIp
		public static string PixIp
		{
			get
			{
				return "84.45.14.61";
			}
		}
		#endregion
		#region PixMasterIp
		public static string PixMasterIp
		{
			get
			{
				return "84.45.14.62";
			}
		}
		#endregion
		#region MiscServerIp
		public static string MiscServerIp
		{
			get
			{
				//EXTRA now has the SAN plugged into LAN2, so it's not on the internal network any more. 07/11/2006
				//if (Common.Properties.MachineName.Equals("EXTRA"))
				return "84.45.14.31";
				//else
				//	return "192.168.3.61";
			}
		}
		#endregion

		#region StaticContentIp
		public static string StaticContentIp(Guid g)
		{
			return "84.45.14.88";

			//#region removed when we removed STATIC0
			//string ip = "";
			//switch (g.ToString().ToLower()[0])
			//{
			//    case '0':
			//        ip = "80";
			//        break;
			//    case '1':
			//        ip = "81";
			//        break;
			//    case '2':
			//        ip = "82";
			//        break;
			//    case '3':
			//        ip = "83";
			//        break;
			//    case '4':
			//        ip = "84";
			//        break;
			//    case '5':
			//        ip = "85";
			//        break;
			//    case '6':
			//        ip = "86";
			//        break;
			//    case '7':
			//        ip = "87";
			//        break;
			//    case '8':
			//        ip = "88";
			//        break;
			//    case '9':
			//        ip = "89";
			//        break;
			//    case 'a':
			//        ip = "90";
			//        break;
			//    case 'b':
			//        ip = "91";
			//        break;
			//    case 'c':
			//        ip = "92";
			//        break;
			//    case 'd':
			//        ip = "93";
			//        break;
			//    case 'e':
			//        ip = "94";
			//        break;
			//    case 'f':
			//        ip = "95";
			//        break;
			//    default:
			//        ip = "";
			//        break;
			//}
			//if (Common.Properties.MachineName.Equals("EXTRA"))
			//    return "84.45.14." + ip;
			//else
			//    return "192.168.3." + ip;
			//#endregion
		}
		#endregion

		#region InCluster
		public static bool InCluster
		{
			get
			{
				return Common.Properties.MachineName.StartsWith("SERVER");
			}
		}
		#endregion

		#region DomainName
		public static string DomainName = GetDomainName();

		private static string GetDomainName()
		{

			if (Common.Properties.IsDevelopmentEnvironment)
			{
				switch (Common.Properties.MachineName)
				{
					case "SOLO": return "localhost";//return HttpContext.Current == null ? "solo" : HttpContext.Current.Request.Url.Host;
                    case "DEV1": return "dev0.dontstayin.com";
					case "DEV2": return "dev2.dontstayin.com";
					case "KAMINO": return "test.dontstayin.com";
					case "ENDOR": return "localhost";
					case "NABOO": return "localhost";
					default: return Common.Properties.MachineName.ToLower();
				}
			}
			else
			{
				if (IsBeta)
					return "www1.dontstayin.com";
				else
					return "www.dontstayin.com";
			}
		}

		#endregion

		#region DevEnv
		/// <summary>
		/// DevEnv - true if we are in the development environment.
		/// *********************************************** Be sure to update the server name to that of the development server during set-up
		/// </summary>
		public static bool DevEnv
		{
			get
			{
				return Common.Properties.IsDevelopmentEnvironment;
			}

		}
		#endregion


		#region ClientIsDevBox
		public static bool ClientIsDevBox
		{
			get
			{
				//return false;
				return 
					HttpContext.Current != null &&
					(
						HttpContext.Current.Request.UserHostAddress == "84.45.14.10" ||
						HttpContext.Current.Request.UserHostAddress == "192.168.113.10" ||
						HttpContext.Current.Request.UserHostAddress == "::1"
					);
			}
		}
		#endregion


		public static bool UsingChatServer
		{
			get
			{
				return true;
			}
		}

		public static string ChatServerAddress
		{
			get
			{
				return "tcp://" + ChatLibrary.ChatServer.ChatServerIp + ":" + ChatLibrary.ChatServer.ChatServerPort.ToString() + "/Reg";
				//return "tcp://192.168.4.2:9000/Reg";
			}
		}

		#region public static string DefaultConnectionString
		/// <summary>
		/// This returns the default connection string - usually set to System.Configuration.ConfigurationSettings.AppSettings["SqlConnectionString"]
		/// (From web.config)
		/// </summary>
		public static string DefaultConnectionString
		{
			get
			{
				return Common.Properties.ConnectionString;
			}
		}


		#endregion

		#region GenericDatabaseConnection
		public static string GenericDatabaseConnection(string database)
		{
			return Vars.DefaultConnectionString.Replace("db_spotted", database);
		}
		#endregion

		#region public static string LocalNamespace
		/// <summary>
		/// This gets a string reprasenting the name of the main project namespace
		/// </summary>
		public static string LocalNamespace
		{
			get
			{
				return "Spotted";
			}
		}
		#endregion

		#region public static string AdminEmailAddress
		/// <summary>
		/// Use this as the reply-to address in all correspondance to users.
		/// </summary>
		public static string AdminReplyAddress
		{
			get
			{
				return "mail@dontstayin.com";
			}
		}
		#endregion

		#region UrlScheme
		public static string UrlScheme
		{
			get
			{
				if (HttpContext.Current == null)
					return "http";
				else
					return HttpContext.Current.Request.Url.Scheme;
			}
		}
		#endregion

		#region Netscape
		/// <summary>
		/// True if browser = netscape
		/// </summary>
		public static bool Netscape
		{
			get
			{
				return !Vars.IE;
				//	if (HttpContext.Current.Request.Browser.Browser.IndexOf("Netscape") > -1)
				//		return true;
				//	return false;
			}
		}
		#endregion
		#region IE
		/// <summary>
		/// True if browser = ie
		/// </summary>
		public static bool IE
		{
			get
			{
				if (HttpContext.Current.Request.Browser.Browser.IndexOf("IE") > -1)
					return true;
				return false;
			}
		}
		#endregion
		#region Opera
		/// <summary>
		/// True if browser = Opera
		/// </summary>
		public static bool Opera
		{
			get
			{
				if (HttpContext.Current.Request.Browser.Browser.ToLower().IndexOf("opera") > -1)
					return true;
				return false;
			}
		}
		#endregion

		#region Registration keys for products
		public const string CambroDbComboRegistrationKey = "aeaaaaU99999baaaaaaaaaEbbaaaaewm6rdDVUxm6ndD0Uxm6fdDQ70yFVg-TnhB6nuyRjKAT7ftNZw-Yvgz6jdmUidD2ULmYUhm6bdDVULm6FxlWitlVeulVeH-XYsm0ExlZiulZuulVEtAOZsndZsm2ituV3IBl";
		public const string CambroQuickAdminRegistrationKey = "aeaaaaU99999baaaaaaaaaEbbaaaaaJm6jdDUU1m6ndDYUxm6fdD6nuyRjKAT7ftYrgDWadmWUho6jdn6bdDtZWnfZsmcjvlWmtlUqtlWiuyRaHqZZsneZsmgZWngvw~RitrGrhwXR";
		public const string CambroImageAdminRegistrationKey = "aeaaaaU99999baaaaaaaaaEbbaaaa6Zm6jdDUUxm6ndD0Uxm6fdDQ70yFVg-TnhB6nuyRjKAT7ftNZw-Yvgz6jdmUidD2ULmYUhm6bdDVULm6rulVudmRmXqlfJtUZsmXYsmgjulVmunRmXqRaXnZIHBRedouR";
		#endregion

		#region FullUrl
		public static string FullUrl(string page)
		{
			return Vars.UrlScheme + "://" + Vars.DomainName + page;
		}
		#endregion

		#region GetArchiveUrl
		public static string GetArchiveUrl(int Year, int Month, int Day, Model.Entities.ArchiveObjectType Type, string[] par, string UrlFilterPart)
		{
			DateTime month = new DateTime(Year, Month, 1);
			string dayString = "";
			if (Day > 0)
				dayString = "/" + Day.ToString("00");
			string type = "";
			if (Type.Equals(Model.Entities.ArchiveObjectType.Article))
				type = "articles";
			else if (Type.Equals(Model.Entities.ArchiveObjectType.Comp))
				type = "competitions";
			else if (Type.Equals(Model.Entities.ArchiveObjectType.Gallery))
				type = "galleries";
			else if (Type.Equals(Model.Entities.ArchiveObjectType.News))
				type = "news";
			else if (Type.Equals(Model.Entities.ArchiveObjectType.Review))
				type = "reviews";
			else if (Type.Equals(Model.Entities.ArchiveObjectType.Guestlist))
				type = "guestlists";

			par = Cambro.Misc.Utility.JoinStringArrays(new string[] { type, "" }, par);
			return UrlInfo.MakeUrl(UrlFilterPart + "/" + Year + "/" + month.ToString("MMM").ToLower() + dayString, "archive", par);
		}
		#endregion

		#region DSIDetails
		public const string DSI_POSTAL_DETAILS_HTML = "<b>Development&nbsp;HellLimited</b><br>Greenhill&nbsp;House,&nbsp;Thorpe Road,&nbsp;Peterborough,&nbsp;PE3&nbsp;6RU";
		public const string DSI_REGNUMBER_HTML = "Registered in England No. 04333049";
		public const string DSI_REGOFFICE_DETAILS_HTML = "Greenhill House, Thorpe Road,<br>Peterborough, PE3 6RU<br><br>" + DSI_REGNUMBER_HTML;
		public const string DSI_VAT_DETAILS_HTML = "VAT Reg. No. 796 5005 04";
		public const string DSI_VATREG_DETAILS_HTML = DSI_VAT_DETAILS_HTML + "<br>" + DSI_REGNUMBER_HTML + "<br>Greenhill House, Thorpe Road, Peterborough, PE3 6RU";
		public const string DSI_BANK_SORT_CODE = "203763";
		public const string DSI_BANK_ACCOUNT_NUMBER = "00478377";
		public const string DSI_BANK_NAME = "Barclays Bank PLC";
		//public const string DSI_CLIENT_BANK_SORT_CODE = "309696";
		//public const string DSI_CLIENT_BANK_ACCOUNT_NUMBER = "00407728";
		//public const string DSI_CLIENT_BANK_NAME = "LLOYDS TSB";
		#endregion

		public static readonly DateTime TICKETS_NEW_SYSTEM_START_DATE = new DateTime(2007, 5, 1);

		public static ListItem[] BuyableCreditObjectsToListItemArray()
		{
			List<ListItem> buyableObjects = new List<ListItem>();
			buyableObjects.Add(new ListItem(Model.Entities.ObjectType.Banner.ToString(), Convert.ToInt32(Model.Entities.ObjectType.Banner).ToString()));
			buyableObjects.Add(new ListItem(Model.Entities.ObjectType.Event.ToString(), Convert.ToInt32(Model.Entities.ObjectType.Event).ToString()));
			buyableObjects.Add(new ListItem(Model.Entities.ObjectType.Invoice.ToString(), Convert.ToInt32(Model.Entities.ObjectType.Invoice).ToString()));

			return buyableObjects.ToArray();
		}

		public static ListItem[] BuyableObjectsToListItemArray()
		{
			List<ListItem> buyableObjects = new List<ListItem>();
			buyableObjects.Add(new ListItem(Model.Entities.ObjectType.CampaignCredit.ToString(), Convert.ToInt32(Model.Entities.ObjectType.CampaignCredit).ToString()));
			buyableObjects.Add(new ListItem(Model.Entities.ObjectType.Ticket.ToString(), Convert.ToInt32(Model.Entities.ObjectType.Ticket).ToString()));
			buyableObjects.Add(new ListItem(Model.Entities.ObjectType.Usr.ToString(), Convert.ToInt32(Model.Entities.ObjectType.Usr).ToString()));

			return buyableObjects.ToArray();
		}
	}
	#endregion

	#region Return - try not to use this - it's a shit idea.
	public class Return
	{
		public bool Success { get; set; }
		public string MessageHtml { get; set; }
	}
	#endregion

	#region PageLinkWriter
	public class PageLinkWriter
	{
		public void SetLastPage(int RecordsPerPage, int TotalItems)
		{
			LastPage = (int)Math.Ceiling((double)TotalItems / (double)RecordsPerPage);
		}
		public void Build(LinkWriter WriteLink, SeperatorWriter WriteSeperator, StringBuilder Builder)
		{
			int currentPage = 1;
			Zone currentZone = FindNextZone(currentPage);
			while (currentZone != null)
			{
				int firstPage = currentZone.FirstPage < 1 ? 1 : currentZone.FirstPage;
				int lastPage = currentZone.LastPage > this.LastPage ? this.LastPage : currentZone.LastPage;
				if (firstPage < currentPage)
					firstPage = currentPage;
				for (int page = firstPage; page <= lastPage; page++)
				{
					WriteLink(page, CurrentPageForLinks, Builder);
				}
				if (lastPage == this.LastPage)
				{
					return;
				}
				else
				{
					currentPage = lastPage + 1;
					currentZone = FindNextZone(currentPage);
					if (currentZone != null && currentZone.FirstPage > currentPage)
						WriteSeperator(currentPage, currentZone.FirstPage, Builder);
				}
			}
		}
		Zone FindNextZone(int CurrentPage)
		{
			int longestCurrentZoneIndex = -1;
			int longestCurrentZoneLastPage = 0;
			int nearestNextZoneIndex = -1;
			int nearestNextZoneFirstPage = 0;
			for (int i = 0; i < Zones.Count; i++)
			{
				Zone z = (Zone)Zones[i];
				if (z.FirstPage <= CurrentPage &&
					z.LastPage >= CurrentPage &&
					z.LastPage > longestCurrentZoneLastPage)
				{
					longestCurrentZoneIndex = i;
					longestCurrentZoneLastPage = z.LastPage;
				}
				if (z.FirstPage > CurrentPage &&
					(z.FirstPage < nearestNextZoneFirstPage || nearestNextZoneFirstPage == 0))
				{
					nearestNextZoneIndex = i;
					nearestNextZoneFirstPage = z.FirstPage;
				}
			}
			if (longestCurrentZoneIndex > -1)
				return (Zone)Zones[longestCurrentZoneIndex];
			else if (nearestNextZoneIndex > -1)
				return (Zone)Zones[nearestNextZoneIndex];
			else
				return null;

		}
		#region CurrentPageForLinks
		public int CurrentPageForLinks
		{
			get
			{
				return currentPageForLinks;
			}
			set
			{
				currentPageForLinks = value;
			}
		}
		private int currentPageForLinks;
		#endregion
		#region LastPage
		public int LastPage
		{
			get
			{
				return lastPage;
			}
			set
			{
				lastPage = value;
			}
		}
		private int lastPage;
		#endregion
		#region Zones
		public ArrayList Zones
		{
			get
			{
				if (zones == null)
					zones = new ArrayList();
				return zones;
			}
			set
			{
				zones = value;
			}
		}
		private ArrayList zones;
		#endregion
		#region class Zone
		public class Zone
		{
			#region FirstPage
			public int FirstPage
			{
				get
				{
					return firstPage;
				}
				set
				{
					firstPage = value;
				}
			}
			private int firstPage;
			#endregion
			#region LastPage
			public int LastPage
			{
				get
				{
					return lastPage;
				}
				set
				{
					lastPage = value;
				}
			}
			private int lastPage;
			#endregion
			#region Zone(int first, int last)
			public Zone(int first, int last)
			{
				this.FirstPage = first;
				this.LastPage = last;
			}
			#endregion
		}
		#endregion
		public delegate void LinkWriter(int Page, int CurrentPage, StringBuilder Builder);
		public delegate void SeperatorWriter(int PrevPage, int NextPage, StringBuilder Builder);

	}
	#endregion

	#region Exceptions
	
	public class BobNotFound : ApplicationException
	{
		public BobNotFound() : base("You've requested something that's not in our database. It may have been deleted.") { }
		public BobNotFound(string exceptionText) : base(exceptionText) { }
		public BobNotFound(string exceptionText, Exception innerException) : base(exceptionText, innerException) { }
	}
	public class BobException : ApplicationException
	{
		public BobException(string exceptionText) : base(exceptionText) { }
		public BobException(string exceptionText, Exception innerException) : base(exceptionText, innerException) { }
	}
	#endregion

	#region ColumnData<T>
	[Serializable]
	public class ColumnData<T> : IEnumerable
	{
		System.Collections.Generic.List<T> list;
		public ColumnData()
		{
			list = new System.Collections.Generic.List<T>(15);
		}

		public T this[int columnId]
		{
			get
			{
				int index = (columnId << 16 >> 16) - 1;
				if (index >= list.Count)
					return default(T);
				else
					return list[index];
			}
			set
			{
				int index = (columnId << 16 >> 16) - 1;
				lock (list)
				{
					if (index >= list.Count)
					{
						int toAdd = index - list.Count;
						for (int i = 0; i < toAdd; i++)
							list.Add(default(T));
						list.Add(value);
					}
					else
						list[index] = value;
				}
			}
		}

		#region IEnumerable Members
		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}
		#endregion
	}
	#endregion


	#region Interfaces
	public interface IStyledEventHolder : IObjectPage, IBobType, IName
	{
		string StyledCss { get; set; }
		string StyledXml { get; set; }
		int PromoterK { get; set; }
		Promoter Promoter { get; }
		Model.Entities.ObjectType ObjectType { get; }
		int K { get; }
		int Update();
		bool IsEvent(Event evnt);
		string UrlStyledCalendar(int Year, int Month, params string[] par);
		string UrlStyledEvent(int EventK, params string[] par);
		string UrlStyledEventLink(Event evnt, params string[] par);
		string UrlStyledApp(string Application, params string[] par);
		string UrlStyled(params string[] par);
		string UrlCss();
	}
	public static class IStyledEventHolderExtentions
	{
		public static string UrlStyledSetup(IStyledEventHolder styledEventHolder)
		{
			return UrlInfo.MakeUrl(styledEventHolder.Promoter.Url() + "/styledsetup", null, "objtype", Convert.ToInt32(styledEventHolder.ObjectType).ToString(), "k", styledEventHolder.K.ToString());
		}

		public static string UrlStyledCalendar(IStyledEventHolder styledEventHolder, int year, int month, params string[] par)
		{
			int requestedMonthIndex = (year * 12) + month;
			int currentMonthIndex = (DateTime.Today.Year * 12) + DateTime.Today.Month;
			DateTime requestedDate = new DateTime(year, month, 1);

			if (currentMonthIndex - requestedMonthIndex > 4 || currentMonthIndex - requestedMonthIndex < -7)
			{
				return UrlInfo.MakeUrl(styledEventHolder.UrlFilterPart + "/tickets/" + year + "/" + requestedDate.ToString("MMM").ToLower(), "", par);
			}
			else
			{
				return UrlInfo.MakeUrl(styledEventHolder.UrlFilterPart + "/tickets/" + requestedDate.ToString("MMM").ToLower(), "", par);
			}
		}

		public static string UrlStyled(IStyledEventHolder styledEventHolder, params string[] par)
		{
			return UrlInfo.MakeUrl(styledEventHolder.UrlFilterPart + "/tickets", null, par);
		}

		public static string UrlStyledApp(IStyledEventHolder styledEventHolder, string application, params string[] par)
		{
			return UrlInfo.MakeUrl(styledEventHolder.UrlFilterPart + "/tickets", application, par);
		}

		public static string UrlStyledEvent(IStyledEventHolder styledEventHolder, int eventK, params string[] par)
		{
			return UrlInfo.MakeUrl(styledEventHolder.UrlFilterPart + "/tickets", eventK.ToString(), par);
		}

		public static string UrlStyledEventLink(IStyledEventHolder styledEventHolder, Event evnt, params string[] par)
		{
			return "<div class=\"Link\"><div class=\"EventLink\"><a href=\"" + UrlInfo.MakeUrl(styledEventHolder.UrlFilterPart + "/tickets", evnt.K.ToString(), par) + "\" class=\"Link\">" + evnt.ToStyledHtml() + "</a></div></div>";
		}

		public static string UrlCss(IStyledEventHolder styledEventHolder)
		{
			return "/files/styledcss/" + styledEventHolder.TypeName.ToLower() + "-" + styledEventHolder.K.ToString() + ".css";
		}
	}



	public interface IDeleteAll
	{
		void DeleteAll(Transaction transaction);
	}
	public interface IRefreshable
	{
		void Refresh();
	}
	public interface IRelevanceContributor
	{
		void AddRelevant(IRelevanceHolder ContainerPage);
	}
	public interface IConnectedTo
	{
		bool IsConnectedTo(Model.Entities.ObjectType objectType, int objectK);
		bool CanBeConnectedTo(Model.Entities.ObjectType o);
	}

	public interface IPic
	{
		Guid Pic { get; set; }
		bool HasPic { get; }
		string PicPath { get; }
		int PicMiscK { get; set; }
		int PicPhotoK { get; set; }
		string PicState { get; set; }
		Misc PicMisc { get; set; }
		Photo PicPhoto { get; set; }

	}
	public interface ICalendar
	{
		string UrlCalendarDay(int Year, int Month, int Day, params string[] par);
		string UrlCalendarDay(bool Tickets, bool FreeGuestlist, int Year, int Month, int Day, params string[] par);
		string UrlCalendarMonth(int Year, int Month, int SkipDay, params string[] par);
		string UrlCalendarMonth(bool Tickets, bool FreeGuestlist, int Year, int Month, int SkipDay, params string[] par);
		string UrlCalendar(params string[] par);
		string UrlCalendar(bool Tickets, bool FreeGuestlist, params string[] par);
		Event HasSingleEvent(int Year, int Month, int Day);
	}
	public interface IObjectPage
	{
		string UrlApp(string Application, params string[] par);
		string UrlFragment { get; }
		string UrlFilterPart { get; }
		string Url(params string[] par);

		void UpdateChildUrlFragments(bool cascade);
	}
	public interface IPage
	{
		string Url(params string[] par);
	}
	public interface IAdminPage
	{
		string UrlAdmin(params string[] par);
	}
	
	public interface IUpdateable
	{
		int Update();
	}
	public interface IBobType : IHasSinglePrimaryKey, IHasObjectType, IUpdateable
	{
		string TypeName { get; }
	}
	public interface IHasObjectType
	{
		Model.Entities.ObjectType ObjectType { get; }
	}
	public interface ICanView
	{
		bool CanView(Usr u);
	}
	public interface IDiscussable : IBobType
	{
		int TotalComments { get; set; }
		string UrlDiscussion(params string[] par);
		void UpdateTotalComments(Transaction transaction);
		Q QueryConditionForGettingThreads { get; }
		bool ShowPrivateThreads { get; }
		bool OnlyShowThreads { get; }
		IDiscussable UsedDiscussable { get; }

	}
	public static class DavesExtensions
	{
		public static bool IsNullOrZero(this int? i)
		{
			return i == null || i == 0;
		}
		public static bool IsNotNullOrZero(this int? i)
		{
			return i != null && i != 0;
		}
	}
	public interface IHasPrimaryThread : IDiscussable
	{
		int? ThreadK { get; set; }
		object ThreadTableColumnToBeSet { get; } //e.g. Thread.Columns.ArticleK
	}
	public static class InterfaceExtensions
	{
		public static void UpdateSingleThread(this IHasPrimaryThread hasPrimaryThread)
		{
			Query q1 = new Query();
			q1.TopRecords = 1;
			q1.OrderBy = new OrderBy(Thread.Columns.K);
			q1.QueryCondition = new And(
				new Q(hasPrimaryThread.ThreadTableColumnToBeSet, hasPrimaryThread.K),
				new Q(Thread.Columns.Private, false),
				new Q(Thread.Columns.GroupK, 0));

			ThreadSet ts = new ThreadSet(q1);
			int newThreadK = 0;
			if (ts.Count > 0)
				newThreadK = ts[0].K;

			if (hasPrimaryThread.ThreadK != newThreadK)
			{
				hasPrimaryThread.ThreadK = newThreadK;
				hasPrimaryThread.Update();
			}
		}
	}
	[Serializable]
	public class BobHolder<T> where T : class, IBobType
	{
		bool hasValue = false;
		int K { get; set; }
		Model.Entities.ObjectType ObjectType { get; set; }
		public T Item { get { return hasValue ? (T)(object)Bob.Get(ObjectType, K) : null; } }
		public BobHolder(T instance)
		{
			if (instance != null)
			{
				hasValue = true;
				this.K = instance.K;
				this.ObjectType = instance.ObjectType;
			}
			else
			{
				hasValue = false;
			}
		}
	}
	

	public interface IHasSinglePrimaryKey 
	{
		int K { get; }
	}
	public interface IHasParentDiscussable
	{
		IDiscussable ParentDiscussable { get; }
		int ParentObjectK { get; set; }
		Model.Entities.ObjectType ParentObjectType { get; set; }
	}
	public interface IArchive
	{
		DateTime ArchiveDateTime { get; }
		string ArchiveHtml(bool showCountry, bool showPlace, bool showVenue, bool showEvent, int size);
	}
	public interface IHasArchive
	{
		string UrlArchiveDate(int Year, int Month, int Day, Model.Entities.ArchiveObjectType Type, params string[] par);
		string UrlArchive(Model.Entities.ArchiveObjectType Type, params string[] par);
	}

	public interface IHasParent
	{
		IBob ParentObject { get; }
		int ParentObjectK { get; set; }
		Model.Entities.ObjectType ParentObjectType { get; set; }
	}

	public interface IName
	{
		string Name { get; set; }
		string FriendlyName { get; }
	}
	public interface IReadableReference
	{
		string ReadableReference { get; }
	}
	public interface ILinkable : IPage, IReadableReference
	{
		string Link(params string[] par);
		string LinkNewWindow(params string[] par);
	}

	public static class ILinkableExtentions
	{
		public static string Link(ILinkable linkable, params string[] par)
		{
			return "<a href=\"" + linkable.Url(par) + "\">" + linkable.ReadableReference + "</a>";
		}
		public static string LinkNewWindow(ILinkable linkable, params string[] par)
		{
			return "<a href=\"" + linkable.Url(par) + "\" target=\"_blank\">" + linkable.ReadableReference + "</a>";
		}
	}

	public interface ILinkableAdmin : IAdminPage, IReadableReference
	{
		string AdminLink(params string[] par);
		string AdminLinkNewWindow(params string[] par);
	}
	public static class ILinkableAdminExtentions
	{
		public static string AdminLink(ILinkableAdmin linkable, params string[] par)
		{
			return "<a href=\"" + linkable.UrlAdmin(par) + "\">" + linkable.ReadableReference + "</a>";
		}
		public static string AdminLinkNewWindow(ILinkableAdmin linkable, params string[] par)
		{
			return "<a href=\"" + linkable.UrlAdmin(par) + "\" target=\"_blank\">" + linkable.ReadableReference + "</a>";
		}
	}

	/// <summary>
	/// IBuyable needs to be implemented by all Bobs that have an invoice item that can be purchased on the client side of the site
	/// </summary>
	public interface IBuyable
	{
		int K { get; }
		bool IsLocked { get; }
		DateTime BuyableLockDateTime { get; set; }

		bool IsReadyForProcessing(InvoiceItem.Types invoiceItemType, decimal price, decimal total);
		bool Process(InvoiceItem.Types invoiceItemType, decimal price, decimal total);
		bool Unprocess(InvoiceItem.Types invoiceItemType);
		bool IsProcessed(InvoiceItem.Types invoiceItemType);
		bool VerifyPrice(InvoiceItem.Types invoiceItemType, decimal price, decimal total);
		void Lock();
		void Unlock();
	}

	/// <summary>
	/// IBuyable needs to be implemented by all Bobs that can be bought with campaign credits
	/// </summary>
	public interface IBuyableCredits : IBobAsHTML
	{
		//int K { get; }
		bool IsLocked { get; }
		DateTime BuyableLockDateTime { get; set; }
		double FixedDiscount { get; set; }
		bool IsPriceFixed { get; set; }

		bool IsReadyForProcessingCredits(InvoiceItem.Types invoiceItemType, int priceCredits);
		bool ProcessCredits(InvoiceItem.Types invoiceItemType, int priceCredits);
		bool Unprocess(InvoiceItem.Types invoiceItemType);
		bool IsProcessed(InvoiceItem.Types invoiceItemType);
		bool VerifyPriceCredits(InvoiceItem.Types invoiceItemType, int priceCredits);
		void Lock();
		void Unlock();

		List<CampaignCredit> ToCampaignCredits(Usr usr, int promoterK, bool saveToDatabase);
	}

	/// <summary>
	/// IBuyer needs to be implemented by all Bobs that can purchase things on the client side of the site
	/// </summary>
	public interface IBuyer : IBobAsHTML
	{
		int K { get; }
		string AddressStreet { get; }
		string AddressArea { get; }
		string AddressPostcode { get; }
		string AddressTown { get; }
		string AddressCounty { get; }
		int AddressCountryK { get; }
		decimal GetBalance();
		decimal CreditLimit { get; }
		int InvoiceDueDaysEffective { get; }
		int CampaignCredits { get; }
	}

	/// <summary>
	/// IBobReport needs to be implemented by all Bobs that can be output as a report
	/// </summary>
	public interface IBobReport
	{
		//int K { get;}
		StringBuilder GenerateReportStringBuilder(bool linksEnabled);
		bool IsUsrAllowedAccess(Usr usr);
	}

	/// <summary>
	/// IBobReport needs to be implemented by all Bobs that can be output as HTML for admin emails and reports
	/// </summary>
	public interface IBobAsHTML
	{
		string AsHTML();
	}

	public interface IHasIcon
	{
		string IconSrc { get; }
	}
	public static class IHasIconExtension
	{
		public static string IconHtml(this IHasIcon bob)
		{
			return @"<img src=""" + bob.IconSrc + @""" align=""absmiddle"" border=""0"" height=""21"" width=""26"">";
		}
	}

	public interface IPicObjectPage : IPic, IObjectPage
	{ }
	public interface IPicHasIconObjectPage : IPic, IHasIcon, IObjectPage, IPicObjectPage
	{ }
	#endregion

	public interface IHasAddress
	{
		string AddressStreet { get; }
		string AddressArea { get; }
		string AddressTown { get; }
		string AddressCounty { get; }
		string AddressPostcode { get; }
		int AddressCountryK { get; }
		Country AddressCountry { get; }
	}

	public static class IHasAddressExtensions
	{
		public static string AddressHtml(this IHasAddress hasAddress)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(hasAddress.AddressStreet);
			if (!string.IsNullOrEmpty(hasAddress.AddressArea))
			{
				sb.Append("<br>");
				sb.Append(hasAddress.AddressArea);
			}
			sb.Append("<br>");
			sb.Append(hasAddress.AddressTown);
			sb.Append("<br>");
			sb.Append(hasAddress.AddressPostcode);
			if (hasAddress.AddressCountryK != 224)
			{
				if (!string.IsNullOrEmpty(hasAddress.AddressCounty))
				{
					sb.Append("<br>");
					sb.Append(hasAddress.AddressCounty);
				}
				if (hasAddress.AddressCountry != null)
				{
					sb.Append("<br>");
					sb.Append(hasAddress.AddressCountry.Name);
				}
			}
			return sb.ToString();
		}
	}

}
