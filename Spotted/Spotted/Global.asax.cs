using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Web;
using Bobs;
using System.Diagnostics;
using Model;
using Model.Entities;
using ServiceLocator;
using Spotted.Controls.PagedData.Templates.Events;
using Country=Bobs.Country;
using SpottedException=Bobs.SpottedException;
using Usr=Bobs.Usr;
using Visit=Bobs.Visit;
using System.Security.Principal;

namespace Spotted
{
	public class Global : System.Web.HttpApplication, IContainerProviderAccessor
	{
		private static IContainerProvider containerProvider;
		public IContainerProvider ContainerProvider { get { return containerProvider; } }
	
		#region Application_Start
		protected void Application_Start(object sender, EventArgs e)
		{
			var containerBuilder = new ContainerBuilder();
			containerBuilder.Register(c => new LinqToSql.Classes.DbSpottedDataContext(Common.Properties.ConnectionString)).As<IDsiDataContext>().ContainerScoped();
			containerBuilder.Register(typeof(CambroFriendlyDateFormatter)).As<IDateFormatter>();
			containerBuilder.Register(typeof(EventTemplateDataCreator));
			containerBuilder.Register(typeof(VenueUrlGenerator)).As<IUrlGenerator<IVenue>>();
			containerBuilder.Register(typeof(EventUrlGenerator)).As<IUrlGenerator<IEvent>>();
			containerBuilder.Register(typeof (PicturePathGenerator)).As<IPicturePathGenerator>();
			containerProvider = new ContainerProvider(containerBuilder.Build());
			SL.Initialize(new DelegateKernel(o => containerProvider.RequestContainer.Resolve(o)));
		}

		#endregion
		#region Visit

		#region VisitPostAuthenticateRequest()
		void VisitPostAuthenticateRequest()
		{
			if (!HttpContext.Current.Request.Url.LocalPath.ToLower().StartsWith("/webservices/controls/chatclient/service.asmx") && !HttpContext.Current.Request.Url.LocalPath.Equals("/support/DbChatServer.aspx") && !HttpContext.Current.Request.Url.LocalPath.Equals("/WebResource.axd"))
			{
				Guid guid = Guid.Empty;
				int usrK = 0;
				Visit currentVisit = null;
				VisitSet vs = null;
				string userAgent = "";
				bool noLoggedInPreviousVisitExists = true;
				bool browserIsCrawler = false;
				string ipAddress = "";

				try
				{

					#region Get ipAddress
					try
					{
						ipAddress = Utilities.TruncateIp(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);
					}
					catch (Exception ex)
					{
						SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception getting IpAddress!..."), ex));
					}
					#endregion

					#region Get UserAgent
					try
					{
						if (HttpContext.Current.Request.UserAgent != null)
						{
							userAgent = HttpContext.Current.Request.UserAgent;
						}
					}
					catch (Exception ex)
					{
						SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception getting UserAgent!... IpAddress={0}", ipAddress), ex));
					}
					#endregion

					#region Get browserIsCrawler
					try
					{
						if (HttpContext.Current.Request.Browser != null)
						{
							browserIsCrawler = HttpContext.Current.Request.Browser.Crawler;
						}
					}
					catch (Exception ex)
					{
						SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception getting BrowserIsCrawler!... UserAgent={0}, IpAddress={1}", userAgent, ipAddress), ex));
					}
					#endregion

					#region Get UsrK
					try
					{
						//The user is authenticated - lets find the UsrK...
						if (HttpContext.Current.User.Identity.IsAuthenticated)
						{
							string usrStr = HttpContext.Current.User.Identity.Name;
							string[] split = usrStr.Split('&');
							usrK = int.Parse(split[0]);
						}
					}
					catch (Exception ex)
					{
						//We might get a corrupt cookie from the client... (not likely because IsAuthenticated should check for this!)
						SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception getting UsrK!... BrowserIsCrawler={0}, UserAgent={1}, IpAddress={2}", browserIsCrawler, userAgent, ipAddress), ex));
					} 
					
					#endregion

					#region Get Guid
					try
					{
						if (HttpContext.Current.Request.Cookies["DsiGuid"] != null)
						{
							//We have a Guid from the client...
							guid = new Guid(HttpContext.Current.Request.Cookies["DsiGuid"].Value);
						}
					}
					catch (Exception ex)
					{
						//We might get a corrupt cookie from the client...
						SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception getting guid!... BrowserIsCrawler={0}, UserAgent={1}, UsrK={2}, IpAddress={3}", browserIsCrawler, userAgent, usrK, ipAddress), ex));
					}
					#endregion

					#region Get previous Visit(s)
					if (!guid.Equals(Guid.Empty))
					{
						try
						{
							//OK so we've got a Guid, lets see if this page request is part of a previous visit by looking for 
							//visits in the last 30 minutes with this Guid / UsrK combination...
							Query q = new Query();
							q.QueryCondition = new And(
								new Q(Visit.Columns.UsrK, usrK),
								new Q(Visit.Columns.Guid, guid),
								new Q(Visit.Columns.DateTimeLast, QueryOperator.GreaterThan, DateTime.Now.AddMinutes(-30)));
							q.OrderBy = new OrderBy(Visit.Columns.Hits, OrderBy.OrderDirection.Descending);
							vs = new VisitSet(q);
						}
						catch (Exception ex)
						{
							SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception getting previous visit(s) [PART 1]... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress), ex));
						}

						try
						{
							if (browserIsCrawler && (vs == null || vs.Count == 0))
							{
								// if not, still see if we can match the Guid to a previous LOGGED IN visit to reduce chance of detecting a bot when it's not..
								var vs2 = new VisitSet(new Query
								{
									QueryCondition = new And(
										new Q(Visit.Columns.UsrK, QueryOperator.GreaterThan, 0),
										new Q(Visit.Columns.Guid, guid),
										new Q(Visit.Columns.DateTimeLast, QueryOperator.GreaterThan, DateTime.Now.AddMonths(-1))),
									TopRecords = 1
								});
								if (vs2.Count > 0)
								{
									noLoggedInPreviousVisitExists = false;
								}
							}
						}
						catch (Exception ex)
						{
							SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception getting previous visit(s) [PART 2]... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress), ex));
						}
					}
					else if (usrK == 0)
					{
						try
						{
							//We don't have a Guid from the cookie, and they're not logged in. Either it's their first page 
							//request or they have cookies disabled. Lets see if this IP adress has hit the site in the last 
							//30 minutes...
							Query q = new Query();
							q.QueryCondition = new And(
								new Q(Visit.Columns.UsrK, 0),
								new Q(Visit.Columns.IpAddress, Utilities.TruncateIp(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"])),
								new Q(Visit.Columns.DateTimeLast, QueryOperator.GreaterThan, DateTime.Now.AddMinutes(-30)));
							q.OrderBy = new OrderBy(Visit.Columns.Hits, OrderBy.OrderDirection.Descending);
							vs = new VisitSet(q);
						}
						catch (Exception ex)
						{
							SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception getting previous visit(s) [PART 3]... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress), ex));
						}
					}
					else
					{
						try
						{
							//This should never happen, but we've found it happening when the Guid doesn't get set to the 
							//cookie properly when it's done just before a redirect? Lets see if this UsrK has hit the site 
							//in the last 30 minutes...
							Query q = new Query();
							q.QueryCondition = new And(
								new Q(Visit.Columns.UsrK, usrK),
								new Q(Visit.Columns.DateTimeLast, QueryOperator.GreaterThan, DateTime.Now.AddMinutes(-30)));
							q.OrderBy = new OrderBy(Visit.Columns.Hits, OrderBy.OrderDirection.Descending);
							vs = new VisitSet(q);
						}
						catch (Exception ex)
						{
							SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception getting previous visit(s) [PART 4]... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress), ex));
						}
					}
					#endregion

					#region Create / merge visit
					if (vs == null || vs.Count == 0)
					{
						try
						{
							//If we didn't find any visits, lets create one. I wish there was a way we could avoid duplicates!
							Visit v = new Visit();
							if (guid.Equals(Guid.Empty))
							{
								guid = Guid.NewGuid();
								v.Guid = guid;
								v.IsNewGuid = true;
								try
								{
									Cambro.Web.Helpers.SetCookie("DsiGuid", guid.ToString(), true);
								}
								catch (Exception ex)
								{
									SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception setting cookie while creating new visit... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress), ex));
								}
							}
							else
							{
								v.Guid = guid;
								v.IsNewGuid = false;
							}
							v.UsrK = usrK;
							v.Pages = 0;
							v.Photos = 0;
							v.DateTimeStart = DateTime.Now;
							v.DateTimeLast = DateTime.Now;
							v.IpAddress = ipAddress;
							v.CountryK = Bobs.IpCountry.ClientCountryK();
							if (userAgent.Length > 0)
							{
								v.IsCrawler = browserIsCrawler && !userAgent.StartsWith("Opera") && usrK == 0 && noLoggedInPreviousVisitExists;
								v.UserAgent = userAgent.TruncateWithDots(400);
							}
							if (usrK > 0 && browserIsCrawler)
							{
								SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(
									string.Format("False-positive crawler? UsrK={0}, UserAgent={1}", usrK, userAgent)));
							}
							v.Update();
							currentVisit = v;
						}
						catch (Exception ex)
						{
							SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception creating new visit... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress), ex));
						}
					}
					else if (vs.Count == 1)
					{
						try
						{
							//If we found just one visit, then great!
							currentVisit = vs[0];
						}
						catch (Exception ex)
						{
							SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception getting single visit from visit set... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress), ex));
						}
					}
					else// if (vs.Count > 1)
					{
						try
						{
							//If we've found more than one visit from the last half hour, we should merge them together. This 
							//isn't great, and I wish we could avoid duplicates when we create them - good use for a stored 
							//procedure?
							int mergeWithK = vs[0].K;
							int pages = 0;
							int photos = 0;
							for (int i = 1; i < vs.Count; i++)
							{
								pages += vs[i].Pages;
								photos += vs[i].Photos;
								vs[i].Delete();
							}
							Visit.Increment(mergeWithK, pages, photos);
							currentVisit = vs[0];
						}
						catch (Exception ex)
						{
							SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception merging visits... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress), ex));
						}
					}
					#endregion

					if (currentVisit != null)
					{
						#region Update Guid from Visit if needed
						try
						{
							if (guid.Equals(Guid.Empty))
							{
								//If we don't have a guid, lets create a new one and set it in the cookie. 
								guid = currentVisit.Guid;
								Cambro.Web.Helpers.SetCookie("DsiGuid", guid.ToString(), true);
							}
						}
						catch (Exception ex)
						{
							SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception setting guid at end of visit code... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress), ex));
						}
						#endregion

						#region Store current visit in HttpContext.Current.Items
						try
						{
							HttpContext.Current.Items["CurrentVisit"] = currentVisit;
						}
						catch (Exception ex)
						{
							SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception setting current visit in HttpContext.Current.Items... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress), ex));
						}
						#endregion
					}
					else
					{
						SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Got to end of visit code but didn't have a visit!... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress)));
					}
				}
				catch (Exception ex)
				{
					SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("General exception in visit code... UsrK={0}, DsiGuid={1}, UserAgent={2}, BrowserIsCrawler={3}, IpAddress={4}", usrK, guid, userAgent, browserIsCrawler, ipAddress), ex));
				}
			}
		}
		#endregion

		#region VisitEndRequest()
		void VisitEndRequest()
		{
			if (!HttpContext.Current.Request.Url.LocalPath.ToLower().StartsWith("/webservices/controls/chatclient/service.asmx") && !HttpContext.Current.Request.Url.LocalPath.Equals("/support/DbChatServer.aspx") && !HttpContext.Current.Request.Url.LocalPath.Equals("/WebResource.axd"))
			{
				if (Visit.HasCurrent)
				{
					try
					{
						//We've got a visit, so lets update it with the stored stats from the page.
						int pages = 0;
						int photos = 0;

						if (HttpContext.Current.Items["VisitPages"] != null)
							pages = (int)HttpContext.Current.Items["VisitPages"];

						if (HttpContext.Current.Items["VisitPhotos"] != null)
							photos = (int)HttpContext.Current.Items["VisitPhotos"];

						Visit.Increment(Visit.Current.K, pages, photos);
					}
					catch (Exception ex)
					{
						SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Exception while incrementing visit stats..."), ex));
					}
				}
				else
				{
					SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Didn't have a visit when we tried to increment visit stats...")));
				}
			}
		}
		#endregion

		#endregion

		#region LogPageTime

		#region CurrentLogPageTime
		public Bobs.LogPageTime CurrentLogPageTime
		{
			get
			{
				if (HttpContext.Current == null)
					return null;

				if (HttpContext.Current.Items["CurrentLogPageTime"] == null)
					HttpContext.Current.Items["CurrentLogPageTime"] = new Bobs.LogPageTime();

				return (Bobs.LogPageTime)HttpContext.Current.Items["CurrentLogPageTime"];
			}
		}
		#endregion

		#region LogPageTimeBeginRequest()
		void LogPageTimeBeginRequest()
		{
			if (Common.Settings.LoggingPageTime == Common.Settings.LoggingPageTimeOption.On)
			{
				// Start page logging
				CurrentLogPageTime.StartDateTime = DateTime.Now;
			}
		}
		#endregion

		#region LogPageTimeEndRequest()
		void LogPageTimeEndRequest()
		{
			if (Common.Settings.LoggingPageTime == Common.Settings.LoggingPageTimeOption.On)
			{
				try
				{
					// Completes page logging. As this gets called every page, we want to streamline as much as possible
					if (HttpContext.Current.Items["CurrentPage"] is GenericPage)
					{
						GenericPage currentPage = (GenericPage)HttpContext.Current.Items["CurrentPage"];
						// Only log pages with Urls, not DbChat requests
						if (currentPage.Url != null)
						{

							CurrentLogPageTime.IsCrawler = Visit.HasCurrent && Visit.Current.IsCrawler;
							CurrentLogPageTime.IsAjaxRequest = currentPage.Url.IsAjaxRequest;
							CurrentLogPageTime.IsRendered = HttpContext.Current.Items["CurrentPageHasFiredRender"] != null;


							// remove first "/" to save bandwidth
							if (currentPage.Url.CurrentFilter != null && currentPage.Url.CurrentFilter.Length > 0)
								CurrentLogPageTime.CurrentFilter = currentPage.Url.CurrentFilter.Remove(0, 1);
							// Remove "/Master/" and "Page.aspx" to save bandwidth
							CurrentLogPageTime.MasterPath = currentPage.Url.MasterPath.Replace("/Master/", "").Replace("Page.aspx", "");
							// remove first "/" and ".ascx" to save bandwidth
							CurrentLogPageTime.PagePath = currentPage.Url.PagePath.Remove(0, 1).Replace(".ascx", "");
							CurrentLogPageTime.ObjectFilterK = currentPage.Url.ObjectFilterK;
							CurrentLogPageTime.ObjectFilterType = currentPage.Url.ObjectFilterType;
							CurrentLogPageTime.UsrK = Usr.Current != null ? Usr.Current.K : 0;
							CurrentLogPageTime.EndDateTime = DateTime.Now;
							CurrentLogPageTime.MachineName = Common.Properties.MachineName;
                            CurrentLogPageTime.IsGet = HttpContext.Current.Request.HttpMethod.Equals("GET");
							if (HttpContext.Current.Items["DbQueriesSelect"] != null)
								CurrentLogPageTime.Selects = (int)HttpContext.Current.Items["DbQueriesSelect"];
							if (HttpContext.Current.Items["DbQueriesUpdate"] != null)
								CurrentLogPageTime.Updates = (int)HttpContext.Current.Items["DbQueriesUpdate"];
							if (HttpContext.Current.Items["DbQueriesInsert"] != null)
								CurrentLogPageTime.Inserts = (int)HttpContext.Current.Items["DbQueriesInsert"];
							if (HttpContext.Current.Items["DbQueriesDelete"] != null)
								CurrentLogPageTime.Deletes = (int)HttpContext.Current.Items["DbQueriesDelete"];

							CurrentLogPageTime.Url = HttpContext.Current.Request.Url.ToString().Truncate(150);

							if (Visit.HasCurrent)
							{
								CurrentLogPageTime.DsiGuid = Visit.Current.Guid;
							}
							CurrentLogPageTime.IpAddress = HttpContext.Current.Request.UserHostAddress;


							if ((Common.Time.Now - CurrentLogPageTime.StartDateTime).TotalSeconds > 1)
							{
								CurrentLogPageTime.PostData = Utilities.GetPostDataAsXml(HttpContext.Current.Request.Form);
							}

							CurrentLogPageTime.Update();
						}
					}
				}
				catch { }
			}
		}
		#endregion

		#endregion

		#region PerformanceCounters

		[System.Runtime.InteropServices.DllImport("Kernel32.dll")]
		public static extern void QueryPerformanceCounter(ref long ticks);

		#region PerformanceCountersBeginRequest()
		void PerformanceCountersBeginRequest()
		{
			long ticksStart = 0;
			QueryPerformanceCounter(ref ticksStart);
			HttpContext.Current.Items["ApplicationStartTicks"] = ticksStart;
		}
		#endregion

		#region PerformanceCountersEndRequest()
		void PerformanceCountersEndRequest()
		{
			try
			{
			if (HttpContext.Current.Items["DsiPage"] != null)
			{
				if (PerformanceCounterCategory.Exists("DontStayIn"))
				{
					PerformanceCounter DsiPages = new PerformanceCounter();
					DsiPages.CategoryName = "DontStayIn";
					DsiPages.CounterName = "DsiPages per sec";
					DsiPages.MachineName = ".";
					DsiPages.ReadOnly = false;

					PerformanceCounter GenTime = new PerformanceCounter();
					GenTime.CategoryName = "DontStayIn";
					GenTime.CounterName = "DsiPage generation time";
					GenTime.MachineName = ".";
					GenTime.ReadOnly = false;

					PerformanceCounter GenTimeBase = new PerformanceCounter();
					GenTimeBase.CategoryName = "DontStayIn";
					GenTimeBase.CounterName = "DsiPage generation time base";
					GenTimeBase.MachineName = ".";
					GenTimeBase.ReadOnly = false;

					DsiPages.Increment();
					long ticksStart = (long)HttpContext.Current.Items["ApplicationStartTicks"];
					long ticksEnd = 0;
					QueryPerformanceCounter(ref ticksEnd);
					GenTime.IncrementBy(ticksEnd - ticksStart);
					GenTimeBase.Increment();
				}

			}
		}
			catch { }
		}
		#endregion

		#endregion

		#region CustomString (for fragment cache exipration)

		#region GetVaryByCustomString
		public override string GetVaryByCustomString(HttpContext context, string arg)
		{
			if (arg.IndexOf(";") > -1)
			{
				string[] args = arg.Split(';');
				string result = "";
				result += Storage.ServeFromCdn ? "Pix=cdn;" : "Pix=eu;";
				foreach (string partArg in args)
				{
					result += GetCustomString(context, partArg);
				}
				return result;
			}
			else
			{
				return (Storage.ServeFromCdn ? "Pix=cdn;" : "Pix=eu;") + GetCustomString(context, arg);
			}

		}
		#endregion

		#region GetCustomString
		public string GetCustomString(HttpContext context, string arg)
		{
			switch (arg)
			{
				case "Browser":
					string browserName = context.Request.Browser.Browser + "_" + context.Request.Browser.MajorVersion;
					return "Browser=" + browserName + ";";
				case "PageName":
					string pageName = context.Request.Url.LocalPath;
					return "PageName=" + pageName + ";";
				case "PhotoOrder":
					string photoOrder = "DateTime";
					if (Bobs.Prefs.Current["PhotoOrder"].Exists)
						photoOrder = Bobs.Prefs.Current["PhotoOrder"];
					return "PhotoOrder=" + photoOrder + ";";
				case "MusicPref":
					string musicPref = "1";
					if (Bobs.Prefs.Current["MusicPref"].Exists)
						musicPref = Bobs.Prefs.Current["MusicPref"];
					if (Request.Form[Spotted.Controls.MusicTypeDropDownList.Name] != null)
					{
						int currentMusicPref = int.Parse(musicPref);
						string[] arr = Request.Form[Spotted.Controls.MusicTypeDropDownList.Name].Split(',');
						foreach (string s in arr)
						{
							if (int.Parse(s) != currentMusicPref)
								musicPref = s;
						}
					}
					return "MusicPref=" + musicPref + ";";
				case "Country":
					if (context.Request.QueryString["ChangeHomeCountryK"] != null)
						return "CountryK=" + context.Request.QueryString["ChangeHomeCountryK"] + ";";
					return "CountryK=" + Country.FilterK.ToString() + ";";
				case "Usr":
					if (Bobs.Usr.Current == null)
						return "Usr=null;";
					else
						return "Usr=" + Bobs.Usr.Current.K.ToString() + ";";
				case "LastBuddyChange":
					if (Bobs.Usr.Current == null)
						return "LastBuddyChange=0;";
					else
						return "LastBuddyChange=" + Bobs.Usr.Current.LastBuddyChange.Ticks.ToString() + ";";
				default:
					return "";
			}
		}
		#endregion

		#endregion

		#region Application_BeginRequest
		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			PerformanceCountersBeginRequest();
			LogPageTimeBeginRequest();
		}
		#endregion

		#region Application_PostAuthenticateRequest
		protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
		{
			VisitPostAuthenticateRequest();
		}
		#endregion

		#region Application_EndRequest
		protected void Application_EndRequest(Object sender, EventArgs e)
		{
			Bobs.Prefs.UpdateCurrentIfExists();
			VisitEndRequest();
			LogPageTimeEndRequest();
			PerformanceCountersEndRequest();

			#region Old DbQueries logging (removed)
			//try
			//{
			//    if (HttpContext.Current.Items["DbQueriesSelect"] != null)
			//        Bobs.Log.Increment(Bobs.Log.Items.DbQueriesSelect, (int)HttpContext.Current.Items["DbQueriesSelect"]);
			//}
			//catch { }

			//try
			//{
			//    if (HttpContext.Current.Items["DbQueriesUpdate"] != null)
			//        Bobs.Log.Increment(Bobs.Log.Items.DbQueriesUpdate, (int)HttpContext.Current.Items["DbQueriesUpdate"]);
			//}
			//catch { }

			//try
			//{
			//    if (HttpContext.Current.Items["DbQueriesInsert"] != null)
			//        Bobs.Log.Increment(Bobs.Log.Items.DbQueriesInsert, (int)HttpContext.Current.Items["DbQueriesInsert"]);
			//}
			//catch { }

			//try
			//{
			//    if (HttpContext.Current.Items["DbQueriesDelete"] != null)
			//        Bobs.Log.Increment(Bobs.Log.Items.DbQueriesDelete, (int)HttpContext.Current.Items["DbQueriesDelete"]);
			//}
			//catch { }
			#endregion

		}
		#endregion

		#region Application_End
		protected void Application_End(object sender, EventArgs e)
		{
			Caching.Instances.Main.Dispose();
			Caching.Instances.MainCounterStore.Dispose();
		}
		#endregion

		#region Application_Error
		protected void Application_Error(object sender, EventArgs e)
		{

			//temporary while finding errors
			//return;

			if (Common.Properties.IsDevelopmentEnvironment)
			{
				return;
			}
			// get last Exception 
			Exception ex = HttpContext.Current.Server.GetLastError();

			string currentFilter = "", masterPath = "", pagePath = "";
			int objectFilterK = 0;
			Model.Entities.ObjectType objectFilterType = Model.Entities.ObjectType.None;

			if (HttpContext.Current.Items["CurrentPage"] is GenericPage)
			{
				GenericPage currentPage = (GenericPage)HttpContext.Current.Items["CurrentPage"];

				if (currentPage.Url != null)
				{
					// remove first "/" to save bandwidth
					if (currentPage.Url.CurrentFilter != null && currentPage.Url.CurrentFilter.Length > 0)
						currentFilter = currentPage.Url.CurrentFilter.Remove(0, 1);
					// Remove "/Master/" and "Page.aspx" to save bandwidth
					masterPath = currentPage.Url.MasterPath.Replace("/Master/", "").Replace("Page.aspx", "");
					// remove first "/" and ".ascx" to save bandwidth
					pagePath = currentPage.Url.PagePath.Remove(0, 1).Replace(".ascx", "");
					objectFilterK = currentPage.Url.ObjectFilterK;
					objectFilterType = currentPage.Url.ObjectFilterType;
				}
			}

			SpottedException spottedEx = SpottedException.TryToSaveExceptionAndChildExceptions(ex, HttpContext.Current, Usr.Current, (Visit.HasCurrent ? Visit.Current : null), currentFilter, masterPath, pagePath, objectFilterK, objectFilterType);

			// clear Exception so Server can continue
			Server.ClearError();
			
			RenderErrorPage(spottedEx);
		}



		private void RenderErrorPage(Bobs.SpottedException spottedEx)
		{
			HttpResponse resp = HttpContext.Current.Response;
			resp.Clear();
			resp.StatusCode = 500;

			System.Web.UI.WebControls.Literal openPage = new System.Web.UI.WebControls.Literal();
			openPage.Text = @"
<html><head><style>
.{
	font-family: Verdana;
	font-size:12px;
	font-weight:bold;
}
p{
	font-family: Verdana;
	font-size:12px;
	font-weight:bold;
	margin-bottom:3px;
	margin-top:3px;
	line-height:130%;
}
a:link, 
a:visited         { color:#000000; }
a:hover           { color:#FF0000; }
</style></head><body>&nbsp;<br>&nbsp;
<center>
<table width=""400"" cellpadding=""0"" cellspacing=""0"" border=""0"">
			<tr>
				<td valign=bottom align=left width=""100%"" rowspan=""2"">
				
				
<center>
<a href=""/""><img src=""/gfx/dsi-sign-100.png"" border=0 style=""border:1px solid #000000;""></a>
</center>

<div style=""padding:10px;"">
<div style=""width:100%;border:solid 1px #000000;padding:2px 4px 2px 4px; margin:0px 0px 13px 0px;"">
	";

			System.Web.UI.WebControls.Literal closePage = new System.Web.UI.WebControls.Literal();
			closePage.Text = @"
</div>
</td></tr></table>
</center></body></html>";

			System.Web.UI.WebControls.Label exceptionLabel = new System.Web.UI.WebControls.Label();
			exceptionLabel.Text = "<p>";

			if (spottedEx != null && spottedEx.ExceptionType == typeof(Bobs.MalformedUrlException).ToString())
			{
				exceptionLabel.Text += "Page not found.";
			}
			else if (spottedEx != null && (Bobs.Usr.Current != null && Bobs.Usr.Current.IsAdmin || HttpContext.Current.Request.UserHostAddress.StartsWith("84.45.14.") || HttpContext.Current.Request.UserHostAddress.StartsWith("192.168.113.") || HttpContext.Current.Request.UserHostAddress.Equals("127.0.0.1")))
			{
				exceptionLabel.Text += spottedEx.Message + "</p><p>" + spottedEx.StackTrace;
			}
			else if (spottedEx != null && (spottedEx.ShowMessageToUsrs))
			{
				exceptionLabel.Text += spottedEx.Message;
			}
			else
			{
				exceptionLabel.Text += "An error has occurred.";
			}

			exceptionLabel.Text += "</p><p><br></p><p>If this problem persists, you may wish to report this to an Admin";
			if (spottedEx != null && spottedEx.K > 0) exceptionLabel.Text += ", quoting error #" + spottedEx.K;
			exceptionLabel.Text += ".</p>";

			System.Web.UI.WebControls.Button retryButton = new System.Web.UI.WebControls.Button();
			retryButton.Text = "Retry";
			retryButton.OnClientClick = "location.reload();";

			System.Web.UI.WebControls.Button historyBackButton = new System.Web.UI.WebControls.Button();
			historyBackButton.Text = "Back";
			historyBackButton.OnClientClick = @"history.back();";

			System.Web.UI.WebControls.Button homeButton = new System.Web.UI.WebControls.Button();
			homeButton.Text = "Home";
			homeButton.OnClientClick = @"location = ""/"";";


			System.IO.StringWriter stringWriter = new System.IO.StringWriter();

			System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
			openPage.RenderControl(htmlWriter);

			exceptionLabel.RenderControl(htmlWriter);



			htmlWriter.RenderBeginTag("center");
			retryButton.RenderControl(htmlWriter);
			historyBackButton.RenderControl(htmlWriter);
			homeButton.RenderControl(htmlWriter);
			htmlWriter.RenderEndTag();

			closePage.RenderControl(htmlWriter);

			resp.Write(stringWriter.ToString());
		}

		#endregion

		

		protected void Application_AuthenticateRequest(Object sender, EventArgs e) 
		{
			// Extract the forms authentication cookie
			//string cookieName = FormsAuthentication.FormsCookieName;
			string cookieName = "SpottedAuthFix";
			HttpCookie authCookie = Context.Request.Cookies[cookieName];
			
			if (null == authCookie)
			{
				// There is no authentication cookie.
				return;
			}

			//WriteState(authCookie.Value);

			string fullCookie = authCookie.Value;
			string[] cookieParts = fullCookie.Split('-');
			string authPart = cookieParts[0];

			FormsAuthenticationTicket authTicket = null;
			try
			{
				authTicket = FormsAuthentication.Decrypt(authPart);
			}
			catch
			{
				// Log exception details (omitted for simplicity)
				return;
			}
			
			if (null == authTicket)
			{
				// Cookie failed to decrypt.
				return;
			}

			//WriteState("Done");

			// Create an Identity object
			FormsIdentity id = new FormsIdentity(authTicket);
			// This principal will flow throughout the request.
			GenericPrincipal principal = new GenericPrincipal(id, null);
			// Attach the new principal object to the current HttpContext object
			Context.User = principal;
		}


		 
	}
}
