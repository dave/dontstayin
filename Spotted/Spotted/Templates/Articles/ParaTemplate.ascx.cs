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
using Spotted.CustomControls;

namespace Spotted.Templates.Articles
{
	public partial class ParaTemplate : System.Web.UI.UserControl
	{
		protected PlaceHolder ParaText;
		protected HtmlImage ParaPicLeftRight, ParaPicTop, ParaPicBottom;
		protected HtmlGenericControl ParaPicTopDiv, ParaPicBottomDiv;
		protected HtmlGenericControl ArticleTitle;
		protected HtmlAnchor ParaPicTopAnchor, ParaPicLeftRightAnchor, ParaPicBottomAnchor;

		public bool ForceLinksToArticle { get; set; }
		public bool IncludeDomainNameInLinks { get; set; }

		private void Page_Init(object sender, System.EventArgs e)
		{
			if (CurrentPara.HasPic || (PrivateMode && CurrentPara.HasPicPrivate) || (Usr.Current != null && (CurrentPara.Article.OwnerUsrK == Usr.Current.K || Usr.Current.IsAdmin) && CurrentPara.HasPicPrivate))
			{
				if ((CurrentPara.Type.Equals(Para.TypeEnum.Para) || CurrentPara.Type.Equals(Para.TypeEnum.Photo)) && CurrentPara.PhotoType != Para.PhotoTypes.None)
				{
					if (CurrentPara.PhotoType.Equals(Para.PhotoTypes.VideoLo) || 
						CurrentPara.PhotoType.Equals(Para.PhotoTypes.VideoMed) ||
						CurrentPara.PhotoType.Equals(Para.PhotoTypes.VideoHi))
					{
						Spotted.Controls.Video vid = new Spotted.Controls.Video();
						vid.Width = CurrentPara.Photo.VideoMedWidth;
						vid.Height = CurrentPara.Photo.VideoMedHeight;
						vid.JpgUrl = CurrentPara.Photo.WebPath;
						vid.VideoUrl = CurrentPara.Photo.VideoMedPath;
						vid.AutoStart = false;
						if (ForceLinksToArticle)
							vid.LinkUrl = (IncludeDomainNameInLinks ? "http://" + Vars.DomainName : "") + CurrentPara.Article.Url();
						else
							vid.LinkUrl = (IncludeDomainNameInLinks ? "http://" + Vars.DomainName : "") + CurrentPara.Photo.Url();

						if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Top))
						{
							ParaPicTopDiv.Controls.Add(vid);
						}
						else if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Bottom))
						{
							ParaPicBottomDiv.Controls.Add(vid);
						}

						ParaPicTopDiv.Visible = CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Top);
						ParaPicBottomDiv.Visible = CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Bottom);
					}
					else
					{
						HtmlAnchor link = new HtmlAnchor();
						if (ForceLinksToArticle)
							link.HRef = (IncludeDomainNameInLinks ? "http://" + Vars.DomainName : "") + CurrentPara.Article.Url();
						else
							link.HRef = (IncludeDomainNameInLinks ? "http://" + Vars.DomainName : "") + CurrentPara.Photo.Url();
						link.Attributes["class"] = "NoStyle";
						HtmlImage img = new HtmlImage();

						if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Top))
						{
							img.Attributes["class"] = "ArticlePicCenter";
						}
						else if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Bottom))
						{
							img.Attributes["class"] = "ArticlePicCenter";
						}
						else if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Left))
						{
							img.Attributes["class"] = "ArticlePicLeft";
							img.Attributes["align"] = "left";
						}
						else if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Right))
						{
							img.Attributes["class"] = "ArticlePicRight";
							img.Attributes["align"] = "right";
						}

						if (!PrivateMode)
							CurrentPara.Photo.IncrementViews();

						#region Rollover
						string rolloverHtml = "";

						if (CurrentPara.Photo.UsrCount > 0)
						{
							rolloverHtml += CurrentPara.Photo.UsrString;
						}
						if (rolloverHtml.Length > 0)
							rolloverHtml = "This is: " + rolloverHtml;

						string totalsText = "";
						if (CurrentPara.Photo.TotalComments > 0)
							totalsText += CurrentPara.Photo.TotalComments.ToString() + " comment" + (CurrentPara.Photo.TotalComments == 1 ? "" : "s");

						if (totalsText.Length > 0)
							rolloverHtml = rolloverHtml + (rolloverHtml.Length > 0 ? "<br>" : "") + totalsText;
						if (rolloverHtml.Length > 0)
							rolloverHtml = HttpUtility.UrlEncodeUnicode("<b>" + rolloverHtml + "</b>").Replace("'", "\\'");

						if (rolloverHtml.Length > 0)
						{
							img.Attributes["onmouseover"] = "stt('" + rolloverHtml + "');";
							img.Attributes["onmouseout"] = "htm();";
						}
						#endregion

						img.Border = 0;
						img.Src = CurrentPara.PicPath;
						img.Width = CurrentPara.PicWidth;
						img.Height = CurrentPara.PicHeight;

						link.Controls.Add(img);


						if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Top))
						{
							ParaPicTopDiv.Controls.Add(link);
							ParaPicTopDiv.Style["text-align"] = "center";
						}
						else if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Bottom))
						{
							ParaPicBottomDiv.Controls.Add(link);
							ParaPicBottomDiv.Style["text-align"] = "center";
						}
						else if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Left) || CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Right))
						{
							ParaDiv.Controls.AddAt(0, link);
						}
						ParaPicTopDiv.Visible = CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Top);
						ParaPicBottomDiv.Visible = CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Bottom);
					}
				}
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (CurrentPara.Type.Equals(Para.TypeEnum.Title))
			{
				ParaDiv.Visible = false;
				ArticleTitle.Visible = true;
				ArticleTitle.InnerText = CurrentPara.Text;
			}
			else
			{
				if (CurrentPara.Type.Equals(Para.TypeEnum.Para))
				{
					ParaDiv.Style["text-align"] = "justify";
					HtmlRenderer r = new HtmlRenderer();
					r.LoadHtml(CurrentPara.Text);

					if (DisableParagraphTagsRoundContent)
						r.AddPTagsWhenRenderingFormattedHtmlInContainer = false;

					if (RenderAllFlashTags)
						r.RenderAllFlashTags = true;

					if (RenderFlashTagsRaw)
						r.RenderFlashTagsRaw = true;

					ParaText.Controls.Clear();
					ParaText.Controls.Add(new LiteralControl(r.Render(ParaText, InlineScript)));

				}
				
			}
		}
		public bool RenderAllFlashTags { get; set; }
		public bool RenderFlashTagsRaw { get; set; }
		public Para OverridePara
		{
			set
			{
				currentPara = value;
			}
		}
		public bool PrivateMode = false;
		#region CurrentPara
		public Para CurrentPara
		{
			get
			{
				if (currentPara == null)
					currentPara = ((Para)((DataListItem)NamingContainer).DataItem);
				return currentPara;
			}
		}
		private Para currentPara;
		#endregion

		public bool DisableParagraphTagsRoundContent = false;
		public bool InlineScript = false;
		public void Initialise()
		{
			Page_Init(null, null);
			Page_Load(null, null);
		}

	}
}
