using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using System.Xml;
using System.IO;
using System.Text;

namespace Spotted.Pages
{
	public partial class MixmagRss : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			Q sectionQ = new Q(true);
			if (Request.QueryString["section"] != null)
			{
				sectionQ = new StringQueryCondition(
					"[Article].[MixmagSections] & " + 
					((int)Math.Pow(2.0, (double)int.Parse(Request.QueryString["section"]))).ToString() + 
					" > 0 ");
			}

			Query q = new Query();
			q.TopRecords = 20;
			q.OrderBy = new OrderBy(Article.Columns.EnabledDateTime, OrderBy.OrderDirection.Descending);
			q.QueryCondition = new And(
				new Q(Article.Columns.Status, Article.StatusEnum.Enabled),
				new Q(Article.Columns.IsWorldwide, true),
				new Q(Article.Columns.IsMixmagNews, true),
				sectionQ
				);
			ArticleSet arts = new ArticleSet(q);
			

			// set the content type
			Page.Response.ContentType = "text/xml";
			// create a RSS feed generator for the output
			RSSFeedGenerator gen = new RSSFeedGenerator(Page.Response.Output);
			gen.WriteStartDocument();
			gen.WriteStartChannel("Mixmag articles RSS Feed",
				"http://www.dontstayin.com/",
				"Summary of the latest Mixmag articles published on Don't Stay In",
				"Copyright © Development Hell Ltd, 2007-2009", "DaveB");
			// generate the items here
			foreach (Article a in arts)
			{
				
				Templates.Articles.ParaTemplate para = (Templates.Articles.ParaTemplate)this.LoadControl("/Templates/Articles/ParaTemplate.ascx");
				para.OverridePara = a.FirstPara;
				para.ForceLinksToArticle = true;
				para.IncludeDomainNameInLinks = true;
				para.InlineScript = true;
				para.DisableParagraphTagsRoundContent = true;
				para.RenderAllFlashTags = true;
				para.RenderFlashTagsRaw = ContainerPage.Url[0] == "Raw";
				para.Initialise();

				System.IO.StringWriter stringWrite = new System.IO.StringWriter();
				System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
				para.RenderControl(htmlWrite);
				string html = stringWrite.ToString();
				html += "<p><a href=\"http://" + Vars.DomainName + a.Url() + "\">" + a.TotalComments.ToString() + " comment" + (a.TotalComments == 1 ? "" : "s") + " at Don't Stay In</a></p>";
				html += "<div class=\"submitted\">Submitted by <a href=\"http://www.mixmag.net/users/" + a.Owner.FirstName.ToLower() + "-" + a.Owner.LastName.ToLower() + "\">" + a.Owner.FirstName + " " + a.Owner.LastName + "</a> on " + a.EnabledDateTime.ToString("dd MMMM yyyy - hh:mm") + "</div>";

				gen.WriteItem(
					a.Title,
					"http://" + Vars.DomainName + a.Url(),
					html,
					a.Owner.NickName,
					a.EnabledDateTime,
					a.Title);
			}
			// clear up
			gen.WriteEndChannel();
			gen.WriteEndDocument();
			gen.Close();
			Response.End();
		}
	}

	/// <summary>
	/// Enables the generation of an RSS feed
	/// </summary>
	public class RSSFeedGenerator
	{
		XmlTextWriter writer;
		public RSSFeedGenerator(System.IO.Stream stream, System.Text.Encoding encoding)
		{
			writer = new XmlTextWriter(stream, encoding);
			writer.Formatting = Formatting.Indented;
		}
		public RSSFeedGenerator(System.IO.TextWriter w)
		{
			writer = new XmlTextWriter(w);
			writer.Formatting = Formatting.Indented;
		}
		/// <summary>
		/// Writes the beginning of the RSS document
		/// </summary>
		public void WriteStartDocument()
		{
			writer.WriteStartDocument();
			writer.WriteStartElement("rss");
			writer.WriteAttributeString("version", "2.0");
		}
		/// <summary>
		/// Writes the end of the RSS document
		/// </summary>
		public void WriteEndDocument()
		{
			writer.WriteEndElement(); //rss
			writer.WriteEndDocument();
		}
		/// <summary>
		/// Closes this stream and the underlying stream
		/// </summary>
		public void Close()
		{
			writer.Flush();
			writer.Close();
		}
		/// <summary>
		/// Begins a new channel in the RSS document
		/// </summary>
		/// <param name="title"></param>
		/// <param name="link"></param>
		/// <param name="description"></param>
		public void WriteStartChannel(string title, string link, string description, string copyright, string webMaster)
		{
			writer.WriteStartElement("channel");
			writer.WriteElementString("title", title);
			writer.WriteElementString("link", link);
			writer.WriteElementString("description", description);
			writer.WriteElementString("language", "en-gb");
			writer.WriteElementString("copyright", copyright);
			writer.WriteElementString("generator", "Developer Fusion RSS Feed Generator v1.0");
			writer.WriteElementString("webMaster", webMaster);
			writer.WriteElementString("lastBuildDate", DateTime.Now.ToString("r"));
			writer.WriteElementString("ttl", "20");

		}
		/// <summary>
		/// Writes the end of a channel in the RSS document
		/// </summary>
		public void WriteEndChannel()
		{
			writer.WriteEndElement(); //channel
		}
		/// <summary>
		/// Writes an item to a channel in the RSS document
		/// </summary>
		/// <param name="title"></param>
		/// <param name="link"></param>
		/// <param name="description"></param>
		/// <param name="author"></param>
		/// <param name="publishedDate"></param>
		/// <param name="category"></param>
		public void WriteItem(string title, string link, string description, string author, DateTime publishedDate, string subject)
		{
			writer.WriteStartElement("item");
			writer.WriteElementString("title", title);
			writer.WriteElementString("link", link);
			writer.WriteElementString("description", description);
			writer.WriteElementString("author", author);
			writer.WriteElementString("pubDate", publishedDate.ToString("r"));
			//writer.WriteElementString("category",category);
			writer.WriteElementString("subject", subject);
			writer.WriteEndElement();
		}
	}
}
