using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;

namespace Spotted.CustomControls
{

	#region Intros
	#region Intro
	public abstract class Intro : HtmlContainerControl
	{
		public abstract Bobs.IObjectPage ObjectPage { get; }
		public string Header { get; set; }
		public bool CloseDiv { get; set; }

		public Intro() : base() { this.CloseDiv = true; }

		protected Spotted.Master.DsiPage DsiPage
		{
			get { return (Spotted.Master.DsiPage)Page; }
		}

		protected override void Render(HtmlTextWriter writer)
		{
			writer.Write("<h1><span class=\"Inner\">" + Header + "</span></h1><div class=\"ContentBorder\">");
			if (ObjectPage is IPic && ((IPic)ObjectPage).HasPic)
			{
				writer.Write("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">");
				writer.Write("<tr>");
				writer.Write("<td valign=\"top\" style=\"padding-right:7px;padding-top:5px;padding-bottom:5px;\">");
				writer.Write("<a href=\"");
				writer.Write(ObjectPage.Url());
				writer.Write("\"><img src=\"");
				writer.Write(((IPic)ObjectPage).PicPath);
				writer.Write("\" border=\"0\" width=\"100\" height=\"100\" class=\"BorderBlack All\"></a>");
				writer.Write("</td>");
				writer.Write("<td width=\"100%\" valign=\"top\">");
			}
			this.RenderChildren(writer);
			if (ObjectPage is IPic && ((IPic)ObjectPage).HasPic)
			{
				writer.Write("</td></tr></table>");
			}
			if (this.CloseDiv)
				writer.Write("</div>");
		}
	}
	#endregion
	#region CustomIntro
	public class CustomIntro : Intro
	{
		IObjectPage objectPage;
		public override IObjectPage ObjectPage
		{
			get { return objectPage; }
		}
		public IPicObjectPage PicObjectPage
		{
			get { return (IPicObjectPage)objectPage; }
		}
		public void Set(IObjectPage p)
		{
			objectPage = p;
		}
	}
	#endregion
	#region UsrIntro
	public class UsrIntro : Intro
	{
		public override IObjectPage ObjectPage
		{
			get { return DsiPage.Url.HasUsrObjectFilter ? DsiPage.Url.ObjectFilterUsr : null; }
		}
	}
	#endregion
	#region GroupIntro
	public class GroupIntro : Intro
	{
		public override IObjectPage ObjectPage
		{
			get { return DsiPage.Url.HasGroupObjectFilter ? DsiPage.Url.ObjectFilterGroup : null; }
		}
	}
	#endregion

	#region PromoterIntro
	public class PromoterIntro : Intro
	{
		public override IObjectPage ObjectPage
		{
			get { return DsiPage.Url.HasPromoterObjectFilter ? DsiPage.Url.ObjectFilterPromoter : null; }
		}

		protected override void Render(HtmlTextWriter writer)
		{
			if (ObjectPage == null)
				return;
			writer.Write("<h1><span class=\"Inner\">" + Header + "</span></h1><div class=\"ContentBorder\">");
			writer.Write("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">");
			writer.Write("<tr>");
			writer.Write("<td width=\"100%\" valign=\"top\"><p class=\"CleanLinks\"><a href=\"");
			writer.Write(ObjectPage.Url());
			writer.Write("\"><img src=\"/gfx/icon-back-12.png\" style=\"margin-right:3px;\" width=\"12\" height=\"21\" align=\"absmiddle\" border=\"0\"><b>Click here to return to your promoter home-page at any time</a></b></p>");
			this.RenderChildren(writer);
			writer.Write("</td></tr></table>");
			if (this.CloseDiv)
				writer.Write("</div>");
		}
	}
	#endregion
	#endregion

	#region h1
	public class h1 : HtmlContainerControl
	{
		public h1()
			: base()
		{
		}
		#region TopLink
		public bool TopLink
		{
			get
			{
				return topLink;
			}
			set
			{
				topLink = value;
			}
		}
		private bool topLink;
		#endregion
		#region Type
		public Types Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}
		private Types type = Types.Content;
		public enum Types
		{
			Nav,
			Content
		}
		#endregion

		//public static string After
		//{
		//    get
		//    {
		//        return "</span></h1>";
		//    }
		//}
		protected override void Render(HtmlTextWriter writer)
		{
			string onClickString = "";
			string classString = "";
			string styleString = "";

			if (OnClick != null && OnClick.Length > 0)
			{
				onClickString = " onclick=\"" + OnClick + "\"";
			}

			if (Width > 0 && StyleAttribute.Length > 0)
			{
				styleString = " style=\"width:" + Width.ToString() + "px;" + StyleAttribute + "\"";
			}
			else if (Width > 0)
			{
				styleString = " style=\"width:" + Width.ToString() + "px;\"";
			}
			else if (StyleAttribute.Length > 0)
			{
				styleString = " style=\"" + StyleAttribute + "\"";
			}

			if (ClassAttribute.Length > 0)
			{
				classString = " class=\"" + ClassAttribute + "\"";
			}

			
			writer.Write("<h1 id=\"" + this.ClientID + "\"" + onClickString + styleString + classString + ">");

			if (!ClassAttribute.Contains("TabHolder"))
				writer.Write("<span class=\"Inner\">");

			this.RenderChildren(writer);

			if (!ClassAttribute.Contains("TabHolder"))
				writer.Write("</span>");

			writer.Write("</h1>");
		}

		public string ClassAttribute = "";
		public string StyleAttribute = "";

		#region OnClick
		public string OnClick
		{
			get
			{
				return onClick;
			}
			set
			{
				onClick = value;
			}
		}
		string onClick;
		#endregion

		#region Width
		public int Width
		{
			get
			{
				return width;
			}
			set
			{
				width = value;
			}
		}
		int width;
		#endregion

		public static string DivNoStyle
		{
			get
			{
				return "<div style=\"width:100%;border:solid 1px #000000;padding:2px 8px 2px 8px; margin:0px 0px 13px 0px;\">";
			}
		}

	}
	#endregion
}
