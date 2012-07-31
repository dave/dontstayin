using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using Bobs;
using Bobs.BannerServer;
using Bobs.BannerServer.Rules;
using Bobs.DataHolders;
using Spotted.Controls.Banners;
using Js.Controls.Banners.Generator;

namespace Spotted.WebServices.Controls.Banners.Generator
{
	/// <summary>
	/// Summary description for Service
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]
	public class Service : System.Web.Services.WebService
	{

		[WebMethod, ScriptMethod]
		public BannerRenderInfo GetBanner(int positionAsInt, string relevantPlacesCsv, string relevantMusicTypesCsv)
		{
			var position = (Banner.Positions) positionAsInt;
			var rules = new RequestRules();
			if (relevantMusicTypesCsv.Length > 0)
			{
				foreach (var i in relevantMusicTypesCsv.Split(',').Select(s => int.Parse(s)))
				{
					rules.MusicTypes.Add(i);
				}
			}
			if (relevantPlacesCsv.Length > 0)
			{
				foreach (var i in relevantPlacesCsv.Split(',').Select(s => int.Parse(s)))
				{
					rules.PlacesVisited.Add(i);
				}
			}

			var server = new Bobs.BannerServer.Server();
			BannerDataHolder bdh = server.GetBanner(position, false, Identity.Current, rules);
			for (int i = 0; i < 5 && bdh == null; i++)
			{
				bdh = server.GetBanner(position, false, Identity.Current, rules);

			}
			BannerRenderInfo bannerRenderInfo = null;
			if (bdh != null)
			{
				
				switch (bdh.Banner.DisplayType)
				{
					case Banner.DisplayTypes.AnimatedGif:
					case Banner.DisplayTypes.AutoEventBanner:
					case Banner.DisplayTypes.CustomAutoEventBanner:
					case Banner.DisplayTypes.Jpg:
						Page pageHolder = new Page();
						var genericBanner = (GenericBanner) pageHolder.LoadControl("/Controls/Banners/GenericBanner.ascx");
						pageHolder.Controls.Add(genericBanner);

						//bControl.ClickHelperLeftOffset = ClickHelperLeftOffset;
						//bControl.ClickHelperTopOffset = ClickHelperTopOffset;
						//bControl.ShowClickHelper = ShowClickHelper;
						genericBanner.CurrentBanner = bdh.Banner;
						genericBanner.Bind();
						
						StringBuilder stringBuilder = new StringBuilder();
						using (var stringWriter = new StringWriter(stringBuilder))
						{
							HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
							
							HttpContext.Current.Server.Execute(pageHolder, stringWriter, false);

						}
						bannerRenderInfo = new BannerRenderInfo()
						{
							bannerRenderInfoType = BannerRenderInfoType.Html,
							html = stringBuilder.ToString()
						};
						
						break;
					case Banner.DisplayTypes.FlashMovie:
					{
						bannerRenderInfo = new BannerRenderInfo()
						{
							bannerRenderInfoType = BannerRenderInfoType.Flash,
							flashUrl = bdh.Banner.Misc.Url(),
							flashVersion = string.IsNullOrEmpty(bdh.Banner.Misc.RequiredFlashVersion) ? "7" : bdh.Banner.Misc.RequiredFlashVersion,
							targetTag = bdh.Banner.InternalLink ? "_self" : "_blank",
							linkTag = bdh.Banner.LinkUrlLive.StartsWith("http://") ? bdh.Banner.LinkUrlLive : "http://" + Vars.DomainName + bdh.Banner.LinkUrlLive,
							miscNeedsClickHelper = !bdh.Banner.Misc.BannerLinkTag
						};
						break;
					}
					case Banner.DisplayTypes.CustomHtml:
					default:
						break;

				}
				if (bannerRenderInfo != null)
				{
					bannerRenderInfo.k = bdh.Banner.K;
					bannerRenderInfo.height = bdh.Banner.Height;
					bannerRenderInfo.width = bdh.Banner.Width;
					bannerRenderInfo.duration = (bdh.Banner.DisplayDuration ?? Common.Settings.DefaultBannerDurationInSeconds)*1000;
					Log.Items itemRotation;
					Log.Items itemHit;
					switch(position)
					{
						case Banner.Positions.Hotbox:
							itemRotation = Log.Items.HotboxRotation;
							itemHit = Log.Items.HotBoxHit;
							break;
						case Banner.Positions.Leaderboard:
							itemRotation = Log.Items.LeaderboardRotation;
							itemHit = Log.Items.LeaderboardHit; break;
						case Banner.Positions.Skyscraper:
							itemRotation = Log.Items.SkyScraperRotation;
							itemHit = Log.Items.SkyScraperHit; break;
						case Banner.Positions.PhotoBanner:
							itemRotation = Log.Items.PhotoBannerRotation;
							itemHit = Log.Items.PhotoBannerHit; break;
						default:
							throw new NotImplementedException();
					}
					Log.Increment(itemRotation);
					Log.Increment(itemHit);
					bdh.Banner.RegisterHit();
				}
			}
			
			return bannerRenderInfo;
		}
	}
}
