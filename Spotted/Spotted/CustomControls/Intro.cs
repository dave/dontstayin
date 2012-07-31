using System.Web.UI;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.CustomControls
{
	public abstract class Intro : HtmlContainerControl
	{
		public abstract Bobs.IObjectPage ObjectPage { get; }
		public string Header { get; set; }
		public bool CloseDiv { get; set; }

		public Intro() : base() { this.CloseDiv = true; }

		protected Spotted.Master.DsiPage DsiPage
		{
			get { return (Spotted.Master.DsiPage)this.Page; }
		}

		protected override void Render(HtmlTextWriter writer)
		{
			writer.Write("<h1>" + this.Header + "</h1><div class=\"ContentBorder\">");
			if (this.ObjectPage is IPic && ((IPic)this.ObjectPage).HasPic)
			{
				writer.Write("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">");
				writer.Write("<tr>");
				writer.Write("<td valign=\"top\" style=\"padding-right:7px;padding-top:5px;padding-bottom:5px;\">");
				writer.Write("<a href=\"");
				writer.Write(this.ObjectPage.Url());
				writer.Write("\"><img src=\"");
				writer.Write(((IPic)this.ObjectPage).PicPath);
				writer.Write("\" border=\"0\" width=\"100\" height=\"100\" class=\"PhotoThumb\"></a>");
				writer.Write("</td>");
				writer.Write("<td width=\"100%\" valign=\"top\">");
			}
			this.RenderChildren(writer);
			if (this.ObjectPage is IPic && ((IPic)this.ObjectPage).HasPic)
			{
				writer.Write("</td></tr></table>");
			}
			if (this.CloseDiv)
				writer.Write("</div>");
		}
	}
}
