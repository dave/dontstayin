using System.Web.UI;
using Bobs;

namespace Spotted.CustomControls
{
	public class PromoterIntro : Intro
	{
		public override IObjectPage ObjectPage
		{
			get { return this.DsiPage.Url.HasPromoterObjectFilter ? this.DsiPage.Url.ObjectFilterPromoter : null; }
		}

		protected override void Render(HtmlTextWriter writer)
		{
			if (this.ObjectPage == null)
				return;
			writer.Write("<h1>" + this.Header + "</h1><div class=\"ContentBorder\">");
			writer.Write("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">");
			writer.Write("<tr>");
			writer.Write("<td valign=\"top\" style=\"padding-right:7px;padding-top:5px;padding-bottom:5px;\">");
			writer.Write("<a href=\"");
			writer.Write(this.ObjectPage.Url());
			writer.Write("\"><img src=\"");
			if (this.ObjectPage is IPic && ((IPic)this.ObjectPage).HasPic)
				writer.Write(((IPic)this.ObjectPage).PicPath);
			else
				writer.Write("/gfx/dsi-sign-100.gif");
			writer.Write("\" border=\"0\" width=\"100\" height=\"100\" class=\"PhotoThumb\"></a>");
			writer.Write("</td>");
			writer.Write("<td width=\"100%\" valign=\"top\"><p class=\"CleanLinks\"><a href=\"");
			writer.Write(this.ObjectPage.Url());
			writer.Write("\"><img src=\"/gfx/bck-12.gif\" style=\"margin-right:3px;\" width=\"12\" height=\"21\" align=\"absmiddle\" border=\"0\"><b>Click here to return to your promoter home-page at any time</a></b></p>");
			this.RenderChildren(writer);
			writer.Write("</td></tr></table>");
			if (this.CloseDiv)
				writer.Write("</div>");
		}
	}
}
