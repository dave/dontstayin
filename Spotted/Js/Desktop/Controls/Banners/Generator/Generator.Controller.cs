using System;
using System.Html;
using jQueryApi;
using Js.Library;
using System.Runtime.CompilerServices;

namespace Js.Controls.Banners.Generator
{
	public class Controller
	{
		private readonly View view;
		private int timeout;
		private readonly int pos;
		private int timerId;
		private const int inactivityTimerPeriod = 15000;
		private static bool initialisedInactivityTimer = false;
		private readonly int userInactivityPeriodDuration;
		public static Action RefreshAllBanners;
		readonly Action oldRefreshBanners;
		public Controller(View view)
		{
			this.view = view;
			this.pos = int.Parse(this.view.uiPosition.Value);
			this.timeout = int.Parse(this.view.uiDuration.Value);
			if (this.view.uiK.Value.Length > 0)
			{
				this.lastK = int.Parse(this.view.uiK.Value);
				this.StartTimer();
			}
			Window.SetInterval(this.CheckInactivity, inactivityTimerPeriod);
			if (!initialisedInactivityTimer)
			{
				jQuery.Window
					.Keydown(OnUserAction)
					.Scroll(OnUserAction);

				jQuery.Document
					.Click(OnUserAction);

				//DomEvent.AddHandler((DOMElement) (object) Window.Self, "keydown", OnUserAction);
				//DomEvent.AddHandler((DOMElement) (object) Window.Self, "scroll", OnUserAction);
				//DomEvent.AddHandler(Document.Body, "click", OnUserAction);
				initialisedInactivityTimer = true;
			}
			userInactivityPeriodDuration = int.Parse(this.view.uiInactivityTimeoutDuration.Value);
			oldRefreshBanners = RefreshAllBanners;
			RefreshAllBanners = delegate
			{
				this.RequestNewBanner();
				if (oldRefreshBanners != null) oldRefreshBanners();
			};

		}

		private void OnUserAction(jQueryEvent e)
		{
			Trace.Write("User action");
			this.RestartActivityTimer();
		}


		private static int inactiveTime = 0;
 
		bool stopped = false;
		private void CheckInactivity()
		{
			inactiveTime += inactivityTimerPeriod;

			if (!stopped && userInactivityPeriodDuration < inactiveTime)
			{
				Trace.Write("User inactive. Stopping banner rotation");
				Window.ClearTimeout(timerId);
				stopped = true;
			}
#if DEBUG
			else
			{
				Trace.Write("Timeout in " + (userInactivityPeriodDuration - inactiveTime) / 1000);
			}
#endif

		}

		private void RestartActivityTimer()
		{
			inactiveTime = 0;
			if (this.stopped)
			{
				Trace.Write("User active. Active banner rotation");
				this.StartTimer();
			}
		}


		private void StartTimer()
		{
			stopped = false;
			if (timeout > 0)
			{
				timerId = Window.SetTimeout(this.OnTimerElapsed, timeout);
			}
		}

		private void OnTimerElapsed()
		{
			Trace.Write("Requesting new banners");
			if (BannerIsVisible())
			{
				this.RequestNewBanner();
			}
			else
			{
				StartTimer();
			}
		}

		private void RequestNewBanner()
		{
			if (this.pos != 1 && this.pos != 2 && this.pos != 5) { return; }
			try
			{
				Window.ClearTimeout(timerId);
				Service.GetBanner(this.pos, this.view.uiPlaceKs.Value, this.view.uiMusicTypes.Value, this.GotBanner, Trace.WebServiceFailure, null, 5000);
			}
			catch (Exception ex)
			{
				Trace.Write(ex.ToString());
				Trace.Write(ex.Message);
			}
		}

		private bool BannerIsVisible()
		{
			int scrollOffset = Document.Body.ScrollTop;
			//JQueryObject j = JQueryAPI.JQuery(this.view.uiBannerJ);
			return scrollOffset < view.uiBannerJ.GetOffset().Top + view.uiBannerJ.GetHeight();
		}

		private int lastK;
		private void GotBanner(BannerRenderInfo result, object userContext, string methodName)
		{
			if (result != null)
			{
				if (this.lastK != result.k)
				{
					timeout = result.duration;
					view.uiBannerJ.Unbind("mouseover");
					//DomEvent.ClearHandlers(this.view.uiBanner);
					if (result.bannerRenderInfoType == BannerRenderInfoType.Flash)
					{

						string bannerHolderId = this.view.uiBanner.ID;

						SWFObject banner = new SWFObject(result.flashUrl, bannerHolderId + "_movie", result.width, result.height, result.flashVersion, "#ffffff");
						banner.addParam("wmode", "transparent");
						banner.addParam("AllowScriptAccess", "always");
						banner.addVariable("targetTag", result.targetTag);
						banner.addVariable("linkTag", result.linkTag);
						banner.addParam("loop", "true");
						banner.addParam("menu", "false");
						banner.write(bannerHolderId);

						//*CLICKHELPER
						if (result.miscNeedsClickHelper)
						{
							//Script.Literal("debugger");
							Trace.Write("linkTag" + result.linkTag);
							DivElement div = (DivElement)Document.CreateElement("DIV");
//#if DEBUG
//							div.Style.Border = "1px solid #ff0000";
//#endif
							div.Style.Left = (0 + int.Parse(this.view.uiClickHelperLeft.Value) ) + "px";
							div.Style.Top = (0 + int.Parse(this.view.uiClickHelperLeft.Value) ) + "px";
							div.Style.Position = "absolute";
							div.Style.ZIndex = 1;
							
							AnchorElement a = (AnchorElement)Document.CreateElement("A");
							
							a.Href = result.linkTag;
							a.Target = result.targetTag;
							a.Style.Border = "solid 0px black";
							a.ClassName = "NoStyle";
							
							ImageElement gif = (ImageElement)Document.CreateElement("IMG");
							gif.Style.Width = result.width + "px";
							gif.Style.Height = result.height + "px";
							gif.Style.Border = "solid 0px black";
							gif.Style.Display = "block";
							gif.Src = "/gfx/1pix.gif";
							a.AppendChild(gif);
							div.AppendChild(a);

							this.view.uiBanner.AppendChild(div);

						}
						this.lastK = result.k;

					}
					else
					{
						Document.GetElementById(this.view.uiBanner.ID).InnerHTML = result.html;
					}
					view.uiBannerJ.MouseOver(StopTimer);
				}
			}
			else
			{
				//Trace.Write("null banner received");
			}
			StartTimer();
		}

	 

		private void StopTimer(jQueryEvent e)
		{
			//Trace.Write("Stopping banner refresh timer");
			Window.ClearTimeout(timerId);
			view.uiBannerJ.Unbind("mouseover");
		}
	}
	[Imported, IgnoreNamespace]
	internal class SWFObject
	{
		public SWFObject(string url, string s, int width, int height, string version, string backgroundColour)
		{
		}


		public void addParam(string s, string s1)
		{
		}

		public void addVariable(string s, string s1)
		{
		}

		public void write(string id)
		{
		}
	}
}
