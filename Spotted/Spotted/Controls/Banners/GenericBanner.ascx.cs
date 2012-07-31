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

namespace Spotted.Controls.Banners
{
	public partial class GenericBanner : System.Web.UI.UserControl
	{
		 

		public int ClickHelperTopOffset { get; set; }
		public int ClickHelperLeftOffset { get; set; }
		public bool ShowClickHelper { get; set; }

		public bool MusicTargetted { get; set; }
		public bool PlaceTargetted { get; set; }
		public Banner CurrentBanner { get; set; }
		public bool UseNewMisc { get; set; }
	 
		public void Bind()
		{
			try
			{
				bool miscNeedsClickHelper = false;
				if (CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.AnimatedGif) || CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.Jpg) || CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.FlashMovie))
				{
					FileBanner b = null;
					var misc = UseNewMisc ? CurrentBanner.NewMisc : CurrentBanner.Misc;
					var displayType = UseNewMisc ? misc.DisplayType : CurrentBanner.DisplayType;
					if (displayType == Banner.DisplayTypes.FlashMovie)
					{
						b = (FileBanner)this.LoadControl("/Controls/Banners/FlashBanner.ascx");

						if (misc != null)
						{
							b.FlashVersion = misc.RequiredFlashVersion;
							miscNeedsClickHelper = !misc.BannerLinkTag;
						}
					}
					else
					{
						b = (FileBanner)this.LoadControl("/Controls/Banners/ImageBanner.ascx");
					}
					if (misc != null) b.BannerUrl = misc.Url();
					
					b.LinkTargetBlank = !CurrentBanner.InternalLink;
					b.LinkUrl = CurrentBanner.LinkUrlLive;
					if (CurrentBanner.Position.Equals(Banner.Positions.Leaderboard))
					{
						b.Width = 728;
						b.Height = 90;
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.EmailBanner))
					{
						b.Width = 331;
						b.Height = 51;
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.Hotbox))
					{
						b.Width = 300;
						b.Height = 250;
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.Skyscraper))
					{
						//XMAS
						b.Width = 300;
						b.Height = 250;//600;
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.PhotoBanner))
					{
						b.Width = 450;
						b.Height = 50;
					}

					try
					{
						if (CurrentBanner.DisplayType == Model.Entities.Banner.DisplayTypes.AnimatedGif ||
							CurrentBanner.DisplayType == Model.Entities.Banner.DisplayTypes.FlashMovie ||
							CurrentBanner.DisplayType == Model.Entities.Banner.DisplayTypes.Jpg)
						{
							if (CurrentBanner.Misc.Width > 0 && CurrentBanner.Misc.Width <= b.Width &&
								CurrentBanner.Misc.Height > 0 && CurrentBanner.Misc.Height <= b.Height)
							{
								b.Width = CurrentBanner.Misc.Width;
								b.Height = CurrentBanner.Misc.Height;
							}

						}
					}
					catch { }

					if (ShowClickHelper && miscNeedsClickHelper)
					{
						string clickHelperLeftString = "";
						if (ClickHelperLeftOffset > 0)
							clickHelperLeftString = "left:" + ClickHelperLeftOffset.ToString() + "px;";

						string clickHelperTopString = "";
						if (ClickHelperTopOffset > 0)
							clickHelperTopString = "top:" + ClickHelperTopOffset.ToString() + "px;";
						
						string clickHelperTargetString = "";
						if (!CurrentBanner.InternalLink)
							clickHelperTargetString = " target=\"_blank\"";

						string url = CurrentBanner.LinkUrlLive;
						if (!url.StartsWith("http://"))
							url = "http://" + Vars.DomainName + url;
						
						//*CLICKHELPER

						//this.Controls.Add(new LiteralControl("<div style=\""+(Vars.DevEnv?"border:1px solid #ff0000;":"")+"position:absolute;" + clickHelperLeftString + clickHelperTopString + "z-index:1;\"><a href=\"" + url + "\"" + clickHelperTargetString + "><img src=\"/gfx/1pix.gif\" width=\"" + b.Width.ToString() + "\" height=\"" + b.Height.ToString() + "\" border=\"0\" style=\"display:block;\" /></a></div>"));
						this.Controls.Add(new LiteralControl("<div style=\"position:absolute;" + clickHelperLeftString + clickHelperTopString + "z-index:1;\"><a href=\"" + url + "\"" + clickHelperTargetString + " class=\"NoStyle\"><img src=\"/gfx/1pix.gif\" width=\"" + b.Width.ToString() + "\" height=\"" + b.Height.ToString() + "\" border=\"0\" style=\"display:block;\" /></a></div>"));
					}

					if (CurrentBanner.Position.Equals(Banner.Positions.Hotbox) && ((UseNewMisc && CurrentBanner.NewMisc.Width == 191 && CurrentBanner.NewMisc.Height == 191) || (!UseNewMisc && CurrentBanner.Misc.Width == 191 && CurrentBanner.Misc.Height == 191)))
					{
						b.Width = 191;
						b.Height = 191;
						this.Controls.Add(new LiteralControl("<img src=\"/gfx/1pix.gif\" height=\"220\" width=\"52\" valign=\"middle\">"));
					}

					b.DataBind();
					this.Controls.Add(b);
				}
				else if (CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner) || CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
				{
					AutoBanner b = null;
					if (CurrentBanner.Position.Equals(Banner.Positions.Leaderboard))
					{
						b = (AutoBanner)this.LoadControl("/Controls/Banners/AutoLeaderboard.ascx");
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.PhotoBanner))
					{
						b = (AutoBanner)this.LoadControl("/Controls/Banners/AutoPhotoBanner.ascx");
					}

					b.FirstLine = CurrentBanner.FirstLine;
					b.FontSize = CurrentBanner.CustomiseFirstLineSize;
					b.SecondLine = CurrentBanner.SecondLine;
					b.ThirdLine = CurrentBanner.ThirdLine;

					b.PicGuid = CurrentBanner.Event.AnyPic;
					b.BannerUrl = CurrentBanner.LinkUrlLive;
					b.LinkTargetBlank = !CurrentBanner.InternalLink;
					b.Bind(); //I removed this because it was being called twice... Not sure why it was called here, because it's called in Page_Load...
					b.DataBind();
					this.Controls.Add(b);

				}
				else if (CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.CustomHtml))
				{
					this.Controls.Add(new LiteralControl(CurrentBanner.CustomHtml.Replace("[CACHEBUSTER]", Common.ThreadSafeRandom.Next(99999999).ToString())));
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Exception while binding Banner-" + CurrentBanner.K, ex);
			}
		}


	
	}
}
