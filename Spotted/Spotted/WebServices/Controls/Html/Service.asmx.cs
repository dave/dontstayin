using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using System.Web.UI;
using System.Text;
using Bobs;

namespace Spotted.WebServices.Controls.Html
{
	/// <summary>
	/// Summary description for Html
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	[ScriptService]
	public class Service : System.Web.Services.WebService
	{
		[WebMethod]
		public string HelloWorld(string s)
		{
			return "Hello World";
		}		
		
		[WebMethod]
		public string LinkProfile(string nickName)
		{
			StringBuilder sb = new StringBuilder();

			int usrK = 0;
			try
			{
				usrK = Bobs.Usr.GetFromNickName(nickName).K;
			}
			catch{}

			if (usrK == 0)
				return "[Error!]";

			TagBuilder t = new TagBuilder("dsi:object", sb);

			t.AddAttribute("type", "usr");
			t.AddAttribute("ref", usrK.ToString());

			t.RenderEmptyTag();

			return sb.ToString();
		}

		[WebMethod]
		public string LinkUrl(string url, string str)
		{
			url = url.Trim();
			if (url.ToLower().StartsWith("/"))
				url = "http://www.dontstayin.com" + url;
			else if (!url.ToLower().StartsWith("http://") && !url.ToLower().StartsWith("https://"))
				url = "http://" + url;
			
			StringBuilder sb = new StringBuilder();
			HtmlCleaner.HtmlWriter c = new HtmlCleaner.HtmlWriter(sb);
			if (c.UrlRegex.IsMatch(url))
			{
				return c.UrlReplacementGeneric(c.UrlRegex.Match(url), str);
			}
			else
			{
				return str;
			}

		}

		[WebMethod]
		public string ImageUrl(string urlSrc, string urlHref)
		{
			StringBuilder sb = new StringBuilder();

			TagBuilder tImg = new TagBuilder("img", sb);

			urlSrc = urlSrc.Trim();
			if (urlSrc.StartsWith("/"))
				urlSrc = "http://pix.dontstayin.com" + urlSrc;
			else if (!urlSrc.StartsWith("http://") && !urlSrc.StartsWith("https://"))
				urlSrc = "http://" + urlSrc;
			tImg.AddAttribute("src", urlSrc);

			if (urlSrc.Contains(".dontstayin.com/"))
				tImg.AddAttribute("class", "BorderBlack All");

			urlHref = urlHref.Trim();
			if (urlHref.Length > 0)
			{
				tImg.AddAttribute("border", "0");

				TagBuilder tA = new TagBuilder("a", sb);

				if (urlHref.StartsWith("/"))
					urlHref = "http://www.dontstayin.com" + urlHref;
				else if (!urlHref.StartsWith("http://") && !urlHref.StartsWith("https://"))
					urlHref = "http://" + urlHref;

				tA.AddAttribute("href", urlHref);

				if (urlHref.StartsWith("http://www.dontstayin.com/"))
					tA.AddAttribute("target", "_blank");

				tA.RenderStartTag();
				tImg.RenderEmptyTag();
				tA.RenderEndTag();
			}
			else
			{
				tImg.RenderEmptyTag();
			}

			return sb.ToString();
		}

		[WebMethod]
		public string VideoFlv(string urlFlv, string widthStr, string heightStr)
		{
			StringBuilder sb = new StringBuilder();

			TagBuilder t = new TagBuilder("dsi:video", sb);

			if (!urlFlv.StartsWith("http://") && !urlFlv.StartsWith("https://"))
				urlFlv = "http://" + urlFlv;

			t.AddAttribute("type", "flv");
			t.AddAttribute("src", urlFlv);

			int width = 0;
			try
			{
				width = int.Parse(widthStr);
			}
			catch { }
			if (width > 0)
				t.AddAttribute("width", width.ToString());

			int height = 0;
			try
			{
				height = int.Parse(heightStr);
			}
			catch { }
			if (height > 0)
				t.AddAttribute("height", height.ToString());

			t.RenderEmptyTag();
			
			return sb.ToString();
		}

		[WebMethod]
		public string FlashSwfUrl(string urlSwf, string widthStr, string heightStr, string draw)
		{
			StringBuilder sb = new StringBuilder();

			TagBuilder t = new TagBuilder("dsi:flash", sb);

			if (!urlSwf.StartsWith("http://") && !urlSwf.StartsWith("https://"))
				urlSwf = "http://" + urlSwf;

			t.AddAttribute("src", urlSwf);

			int width = 0;
			try
			{
				width = int.Parse(widthStr);
			}
			catch { }
			if (width > 0)
				t.AddAttribute("width", width.ToString());

			int height = 0;
			try
			{
				height = int.Parse(heightStr);
			}
			catch { }
			if (height > 0)
				t.AddAttribute("height", height.ToString());

			if (draw != null && draw == "1")
				t.AddAttribute("draw", "click");
			else if (draw != null && draw == "2")
				t.AddAttribute("draw", "load");
			//else if (draw != null && draw == "3")
			//	t.AddAttribute("draw", "raw");
			
			t.RenderEmptyTag();

			return sb.ToString();
		}


		

	}
}
