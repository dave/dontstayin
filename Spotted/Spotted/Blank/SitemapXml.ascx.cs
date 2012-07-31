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
using System.Xml;
using Bobs;
using System.Text;

namespace Spotted.Blank
{
	public partial class SitemapXml : BlankUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Response.ContentType = "text/xml";
			Response.Clear();
			Response.Buffer = false;

			XmlTextWriter x = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
			x.Formatting = Formatting.Indented;
			x.WriteRaw("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");

			string dateFormat = "yyyy-MM-ddTHH:mm:sszzz";

			if (Request.QueryString[0].Equals("index"))
			{
				#region sitemapindex
				x.WriteStartElement("sitemapindex");
				x.WriteAttributeString("xmlns", "http://www.google.com/schemas/sitemap/0.84");

				if (true)
				{
					#region sitemap - countries
					x.WriteStartElement("sitemap");
					x.WriteElementString("loc", "http://www.dontstayin.com/sitemapxml?country");
					Query q = new Query();
					q.QueryCondition = new And(
						new Q(Thread.Columns.Private, false),
						new Q(Thread.Columns.PrivateGroup, false),
						new Q(Thread.Columns.GroupPrivate, false)
						);
					q.OrderBy = new OrderBy(Thread.Columns.LastPost, OrderBy.OrderDirection.Descending);
					q.TopRecords = 1;
					ThreadSet bobset = new ThreadSet(q);
					if (bobset.Count > 0)
						x.WriteElementString("lastmod", bobset[0].LastPost.ToString(dateFormat));
					x.WriteEndElement();	//sitemap
					#endregion
				}

				if (true)
				{
					#region sitemap - place
					x.WriteStartElement("sitemap");
					x.WriteElementString("loc", "http://www.dontstayin.com/sitemapxml?place");

					Query q = new Query();
					q.OrderBy = new OrderBy(Place.Columns.LastPost, OrderBy.OrderDirection.Descending);
					q.TopRecords = 1;
					PlaceSet bobset = new PlaceSet(q);
					if (bobset.Count > 0)
						x.WriteElementString("lastmod", bobset[0].LastPost.ToString(dateFormat));

					x.WriteEndElement();	//sitemap
					#endregion
				}

				if (true)
				{
					#region sitemap - venue
					x.WriteStartElement("sitemap");
					x.WriteElementString("loc", "http://www.dontstayin.com/sitemapxml?venue");

					Query q = new Query();
					q.OrderBy = new OrderBy(Venue.Columns.LastPost, OrderBy.OrderDirection.Descending);
					q.TopRecords = 1;
					VenueSet bobset = new VenueSet(q);
					if (bobset.Count > 0)
						x.WriteElementString("lastmod", bobset[0].LastPost.ToString(dateFormat));

					x.WriteEndElement();	//sitemap
					#endregion
				}

				if (true)
				{
					#region sitemap - event
					x.WriteStartElement("sitemap");
					x.WriteElementString("loc", "http://www.dontstayin.com/sitemapxml?event");

					Query q = new Query();
					q.OrderBy = new OrderBy(Event.Columns.LastPost, OrderBy.OrderDirection.Descending);
					q.TopRecords = 1;
					EventSet bobset = new EventSet(q);
					if (bobset.Count > 0)
						x.WriteElementString("lastmod", bobset[0].LastPost.ToString(dateFormat));

					x.WriteEndElement();	//sitemap
					#endregion
				}

				if (true)
				{
					#region sitemap - thread
					x.WriteStartElement("sitemap");
					x.WriteElementString("loc", "http://www.dontstayin.com/sitemapxml?thread");

					Query q = new Query();
					q.QueryCondition = new And(
						new Q(Thread.Columns.Private, false),
						new Q(Thread.Columns.PrivateGroup, false),
						new Q(Thread.Columns.GroupPrivate, false)
						);
					q.OrderBy = new OrderBy(Thread.Columns.LastPost, OrderBy.OrderDirection.Descending);
					q.TopRecords = 1;
					ThreadSet bobset = new ThreadSet(q);
					if (bobset.Count > 0)
						x.WriteElementString("lastmod", bobset[0].LastPost.ToString(dateFormat));

					x.WriteEndElement();	//sitemap
					#endregion
				}

				if (true)
				{
					#region sitemap - photo
					x.WriteStartElement("sitemap");
					x.WriteElementString("loc", "http://www.dontstayin.com/sitemapxml?photo");

					Query q = new Query();
					q.QueryCondition = Photo.EnabledQueryCondition;
					q.OrderBy = new OrderBy(Photo.Columns.EnabledDateTime, OrderBy.OrderDirection.Descending);
					q.TopRecords = 1;
					PhotoSet bobset = new PhotoSet(q);
					if (bobset.Count > 0)
						x.WriteElementString("lastmod", bobset[0].EnabledDateTime.ToString(dateFormat));

					x.WriteEndElement();	//sitemap
					#endregion
				}

				if (true)
				{
					#region sitemap - usr
					x.WriteStartElement("sitemap");
					x.WriteElementString("loc", "http://www.dontstayin.com/sitemapxml?usr");
					x.WriteEndElement();	//sitemap
					#endregion
				}

				if (true)
				{
					#region sitemap - article
					x.WriteStartElement("sitemap");
					x.WriteElementString("loc", "http://www.dontstayin.com/sitemapxml?article");

					Query q = new Query();
					q.QueryCondition = Article.EnabledQueryCondition;
					q.OrderBy = new OrderBy(Article.Columns.EnabledDateTime, OrderBy.OrderDirection.Descending);
					q.TopRecords = 1;
					ArticleSet bobset = new ArticleSet(q);
					if (bobset.Count > 0)
						x.WriteElementString("lastmod", bobset[0].EnabledDateTime.ToString(dateFormat));

					x.WriteEndElement();	//sitemap
					#endregion
				}

				if (true)
				{
					#region sitemap - comp
					x.WriteStartElement("sitemap");
					x.WriteElementString("loc", "http://www.dontstayin.com/sitemapxml?comp");
					x.WriteEndElement();	//sitemap
					#endregion
				}

				if (true)
				{
					#region sitemap - brand
					x.WriteStartElement("sitemap");
					x.WriteElementString("loc", "http://www.dontstayin.com/sitemapxml?brand");

					Query q = new Query();
					q.OrderBy = new OrderBy(Brand.Columns.LastPost, OrderBy.OrderDirection.Descending);
					q.TopRecords = 1;
					BrandSet bobset = new BrandSet(q);
					if (bobset.Count > 0)
						x.WriteElementString("lastmod", bobset[0].LastPost.ToString(dateFormat));

					x.WriteEndElement();	//sitemap
					#endregion
				}


				if (true)
				{
					#region sitemap - group
					x.WriteStartElement("sitemap");
					x.WriteElementString("loc", "http://www.dontstayin.com/sitemapxml?group");

					Query q = new Query();
					q.QueryCondition = new And(
						new Q(Group.Columns.BrandK, 0),
						new Q(Group.Columns.PrivateGroupPage, false));
					q.OrderBy = new OrderBy(Group.Columns.LastPost, OrderBy.OrderDirection.Descending);
					q.TopRecords = 1;
					GroupSet bobset = new GroupSet(q);
					if (bobset.Count > 0)
						x.WriteElementString("lastmod", bobset[0].LastPost.Value.ToString(dateFormat));

					x.WriteEndElement();	//sitemap
					#endregion
				}

				x.WriteEndElement();	//sitemapindex
				#endregion
			}
			else
			{
				#region urlset
				x.WriteStartElement("urlset");
				x.WriteAttributeString("xmlns", "http://www.google.com/schemas/sitemap/0.84");

				#region Countries
				if (Request.QueryString[0].Equals("country"))
				{
					if (true)
					{
						#region url
						x.WriteStartElement("url");
						x.WriteElementString("loc", "http://www.dontstayin.com/");
						Query q = new Query();
						q.QueryCondition = new And(
							new Q(Thread.Columns.Private, false),
							new Q(Thread.Columns.PrivateGroup, false),
							new Q(Thread.Columns.GroupPrivate, false)
							);
						q.OrderBy = new OrderBy(Thread.Columns.LastPost, OrderBy.OrderDirection.Descending);
						q.TopRecords = 1;
						ThreadSet bobset = new ThreadSet(q);
						if (bobset.Count > 0)
							x.WriteElementString("lastmod", bobset[0].LastPost.ToString(dateFormat));
						x.WriteElementString("changefreq", "hourly");
						x.WriteEndElement();	//url
						#endregion
					}

					if (true)
					{
						Query q = new Query();
						q.QueryCondition = new Q(Country.Columns.TotalEvents, QueryOperator.GreaterThan, 0);
						q.OrderBy = new OrderBy(Country.Columns.TotalEvents, OrderBy.OrderDirection.Descending);
						q.Columns = new ColumnSet(Country.LinkColumns, Country.Columns.TotalEvents);
						if (Vars.DevEnv)
							q.TopRecords = 10;
						CountrySet bobset = new CountrySet(q);
						for (int i = 0; i < bobset.Count; i++)
						{
							Country p = bobset[i];
							#region url
							x.WriteStartElement("url");
							x.WriteElementString("loc", "http://www.dontstayin.com" + p.Url());
							if (p.TotalEvents > 100)
								x.WriteElementString("changefreq", "hourly");
							else if (p.TotalEvents > 20)
								x.WriteElementString("changefreq", "daily");
							else if (p.TotalEvents > 10)
								x.WriteElementString("changefreq", "weekly");
							else
								x.WriteElementString("changefreq", "monthly");
							x.WriteEndElement();	//url
							#endregion
							bobset.Kill(i);
						}
					}
				}
				#endregion

				#region Places
				if (Request.QueryString[0].Equals("place"))
				{
					Query q = new Query();
					q.QueryCondition = new Or(
						new Q(Place.Columns.TotalComments, QueryOperator.GreaterThan, 0),
						new Q(Place.Columns.TotalEvents, QueryOperator.GreaterThan, 0));
					q.OrderBy = new OrderBy(Place.Columns.TotalComments, OrderBy.OrderDirection.Descending);
					q.Columns = new ColumnSet(Place.LinkColumns, Place.Columns.LastPost);
					if (Vars.DevEnv)
						q.TopRecords = 10;
					else
						q.TopRecords = 40000;
					PlaceSet bobset = new PlaceSet(q);
					for (int i = 0; i < bobset.Count; i++)
					{
						Place p = bobset[i];
						#region url
						x.WriteStartElement("url");
						x.WriteElementString("loc", "http://www.dontstayin.com" + p.Url());
						if (p.LastPost != DateTime.MinValue)
						{
							x.WriteElementString("lastmod", p.LastPost.ToString(dateFormat));
							TimeSpan ts = DateTime.Now.Subtract(p.LastPost);
							if (ts.TotalHours < 24)
								x.WriteElementString("changefreq", "hourly");
							else if (ts.TotalDays < 7)
								x.WriteElementString("changefreq", "daily");
							else if (ts.TotalDays < 30)
								x.WriteElementString("changefreq", "weekly");
							else
								x.WriteElementString("changefreq", "monthly");
						}
						else
							x.WriteElementString("changefreq", "monthly");
						x.WriteEndElement();	//url
						#endregion
						bobset.Kill(i);
					}
				}
				#endregion

				#region Venues
				if (Request.QueryString[0].Equals("venue"))
				{
					Query q = new Query();
					q.OrderBy = new OrderBy(Venue.Columns.TotalEvents, OrderBy.OrderDirection.Descending);
					q.Columns = new ColumnSet(Venue.LinkColumns, Venue.Columns.LastPost, Venue.Columns.TotalEvents);
					if (Vars.DevEnv)
						q.TopRecords = 10;
					else
						q.TopRecords = 40000;
					VenueSet bobset = new VenueSet(q);
					for (int i = 0; i < bobset.Count; i++)
					{
						Venue v = bobset[i];
						#region url
						x.WriteStartElement("url");
						x.WriteElementString("loc", "http://www.dontstayin.com" + v.Url());
						if (v.LastPost != DateTime.MinValue)
						{
							x.WriteElementString("lastmod", v.LastPost.ToString(dateFormat));
							TimeSpan ts = DateTime.Now.Subtract(v.LastPost);
							if (ts.TotalHours < 24)
								x.WriteElementString("changefreq", "hourly");
							else if (ts.TotalDays < 7)
								x.WriteElementString("changefreq", "daily");
							else if (ts.TotalDays < 30)
								x.WriteElementString("changefreq", "weekly");
							else
								x.WriteElementString("changefreq", "monthly");
						}
						else if (v.TotalEvents > 10)
							x.WriteElementString("changefreq", "weekly");
						else
							x.WriteElementString("changefreq", "monthly");

						x.WriteEndElement();	//url
						#endregion
						bobset.Kill(i);
					}
				}
				#endregion

				#region Events
				if (Request.QueryString[0].Equals("event"))
				{
					Query q = new Query();
					q.OrderBy = new OrderBy(Event.Columns.LastPost, OrderBy.OrderDirection.Descending);
					q.Columns = new ColumnSet(Event.LinkColumns, Event.Columns.LastPost, Event.Columns.DateTime);
					if (Vars.DevEnv)
						q.TopRecords = 10;
					else
						q.TopRecords = 40000;
					EventSet bobset = new EventSet(q);
					for (int i = 0; i < bobset.Count; i++)
					{
						Event ev = bobset[i];
						#region url
						x.WriteStartElement("url");
						x.WriteElementString("loc", "http://www.dontstayin.com" + ev.Url());
						if (ev.LastPost != DateTime.MinValue)
						{
							x.WriteElementString("lastmod", ev.LastPost.ToString(dateFormat));
							TimeSpan ts = DateTime.Now.Subtract(ev.LastPost);
							if (ts.TotalHours < 24)
								x.WriteElementString("changefreq", "hourly");
							else if (ts.TotalDays < 7)
								x.WriteElementString("changefreq", "daily");
							else if (ts.TotalDays < 30)
								x.WriteElementString("changefreq", "weekly");
							else
								x.WriteElementString("changefreq", "monthly");
						}
						else
							x.WriteElementString("changefreq", "monthly");
						x.WriteEndElement();	//url
						#endregion
						bobset.Kill(i);
					}
				}
				#endregion

				#region Threads
				if (Request.QueryString[0].Equals("thread"))
				{
					Query q = new Query();
					q.QueryCondition = new And(
						new Q(Thread.Columns.Private, false),
						new Q(Thread.Columns.PrivateGroup, false),
						new Q(Thread.Columns.GroupPrivate, false)
					);
					q.Columns = new ColumnSet(Thread.Columns.K, Thread.Columns.UrlFragment, Thread.Columns.TotalComments, Thread.Columns.LastPost);
					q.OrderBy = new OrderBy(Thread.Columns.LastPost, OrderBy.OrderDirection.Descending);
					if (Vars.DevEnv)
						q.TopRecords = 10;
					else
						q.TopRecords = 40000;
					ThreadSet bobset = new ThreadSet(q);
					for (int i = 0; i < bobset.Count; i++)
					{
						Thread t = bobset[i];
						#region url
						x.WriteStartElement("url");
						if (t.LastPage == 1)
							x.WriteElementString("loc", "http://www.dontstayin.com" + t.Url());
						else
							x.WriteElementString("loc", "http://www.dontstayin.com" + t.Url("c", t.LastPage.ToString()));
						x.WriteElementString("lastmod", t.LastPost.ToString(dateFormat));
						TimeSpan ts = DateTime.Now.Subtract(t.LastPost);
						if (ts.TotalHours < 24)
							x.WriteElementString("changefreq", "hourly");
						else if (ts.TotalDays < 7)
							x.WriteElementString("changefreq", "daily");
						else if (ts.TotalDays < 30)
							x.WriteElementString("changefreq", "weekly");
						else
							x.WriteElementString("changefreq", "monthly");
						x.WriteEndElement();	//url
						#endregion
						bobset.Kill(i);
					}
				}
				#endregion

				#region Photos
				if (Request.QueryString[0].Equals("photo"))
				{
					Query q = new Query();
					q.QueryCondition = Photo.EnabledQueryCondition;
					q.Columns = new ColumnSet(Photo.Columns.K, Photo.Columns.UrlFragment, Photo.Columns.EventK, Photo.Columns.ArticleK);
					q.OrderBy = new OrderBy(Photo.Columns.EnabledDateTime, OrderBy.OrderDirection.Descending);
					if (Vars.DevEnv)
						q.TopRecords = 10;
					else
						q.TopRecords = 50000;
					PhotoSet bobset = new PhotoSet(q);
					for (int i = 0; i < bobset.Count; i++)
					{
						Photo p = bobset[i];
						#region url
						x.WriteStartElement("url");
						x.WriteElementString("loc", "http://www.dontstayin.com" + p.Url());
						x.WriteEndElement();	//url
						#endregion
						bobset.Kill(i);
					}
				}
				#endregion

				#region Usrs
				if (Request.QueryString[0].Equals("usr"))
				{
					Query q = new Query();
					q.QueryCondition = new And(
						new Q(Usr.Columns.IsSkeleton, false),
						new Q(Usr.Columns.IsEmailVerified, true),
						new Or(
						new Q(Usr.Columns.Pic, QueryOperator.NotEqualTo, Guid.Empty),
						new Q(Usr.Columns.CommentCount, QueryOperator.NotEqualTo, 0)
						)
					);
					q.Columns = new ColumnSet(Usr.Columns.NickName);
					q.OrderBy = new OrderBy(Usr.Columns.DateTimeLastPageRequest, OrderBy.OrderDirection.Descending);
					if (Vars.DevEnv)
						q.TopRecords = 10;
					else
						q.TopRecords = 50000;
					UsrSet bobset = new UsrSet(q);
					for (int i = 0; i < bobset.Count; i++)
					{
						Usr u = bobset[i];
						#region url
						x.WriteStartElement("url");
						x.WriteElementString("loc", "http://www.dontstayin.com" + u.Url());
						x.WriteEndElement();	//url
						#endregion
						bobset.Kill(i);
					}
				}
				#endregion

				#region Articles
				if (Request.QueryString[0].Equals("article"))
				{
					Query q = new Query();
					q.QueryCondition = Article.EnabledQueryCondition;
					q.Columns = new ColumnSet(Article.Columns.UrlFragment,
					Article.Columns.ParentObjectK,
					Article.Columns.ParentObjectType, Article.Columns.K);
					q.OrderBy = new OrderBy(Article.Columns.EnabledDateTime, OrderBy.OrderDirection.Descending);
					if (Vars.DevEnv)
						q.TopRecords = 10;
					else
						q.TopRecords = 50000;
					ArticleSet bobset = new ArticleSet(q);
					for (int i = 0; i < bobset.Count; i++)
					{
						Article a = bobset[i];
						#region url
						x.WriteStartElement("url");
						x.WriteElementString("loc", "http://www.dontstayin.com" + a.Url());
						x.WriteEndElement();	//url
						#endregion
						bobset.Kill(i);
					}
				}
				#endregion

				#region Competitions
				if (Request.QueryString[0].Equals("comp"))
				{
					Query q = new Query();
					q.QueryCondition = new Q(Comp.Columns.Status, Comp.StatusEnum.Enabled);
					q.Columns = new ColumnSet(Comp.Columns.K);
					q.OrderBy = new OrderBy(Comp.Columns.DateTimeClose, OrderBy.OrderDirection.Descending);
					if (Vars.DevEnv)
						q.TopRecords = 10;
					else
						q.TopRecords = 50000;
					CompSet bobset = new CompSet(q);
					for (int i = 0; i < bobset.Count; i++)
					{
						Comp c = bobset[i];
						#region url
						x.WriteStartElement("url");
						x.WriteElementString("loc", "http://www.dontstayin.com" + c.Url());
						x.WriteEndElement();	//url
						#endregion
						bobset.Kill(i);
					}
				}
				#endregion

				#region Brands
				if (Request.QueryString[0].Equals("brand"))
				{
					Query q = new Query();
					q.OrderBy = new OrderBy(Brand.Columns.LastPost, OrderBy.OrderDirection.Descending);
					q.Columns = new ColumnSet(Brand.LinkColumns, Brand.Columns.LastPost);
					if (Vars.DevEnv)
						q.TopRecords = 10;
					else
						q.TopRecords = 40000;
					BrandSet bobset = new BrandSet(q);
					for (int i = 0; i < bobset.Count; i++)
					{
						Brand b = bobset[i];
						#region url
						x.WriteStartElement("url");
						x.WriteElementString("loc", "http://www.dontstayin.com" + b.Url());
						if (b.LastPost > DateTime.MinValue)
						{
							x.WriteElementString("lastmod", b.LastPost.ToString(dateFormat));
							TimeSpan ts = DateTime.Now.Subtract(b.LastPost);
							if (ts.TotalHours < 24)
								x.WriteElementString("changefreq", "hourly");
							else if (ts.TotalDays < 7)
								x.WriteElementString("changefreq", "daily");
							else if (ts.TotalDays < 30)
								x.WriteElementString("changefreq", "weekly");
							else
								x.WriteElementString("changefreq", "monthly");
						}
						else
							x.WriteElementString("changefreq", "monthly");
						x.WriteEndElement();	//url
						#endregion
						bobset.Kill(i);
					}
				}
				#endregion

				#region Groups
				if (Request.QueryString[0].Equals("group"))
				{
					Query q = new Query();
					q.QueryCondition = new And(
						new Q(Group.Columns.BrandK, 0),
						new Q(Group.Columns.PrivateGroupPage, false));
					q.OrderBy = new OrderBy(Group.Columns.LastPost, OrderBy.OrderDirection.Descending);
					q.Columns = new ColumnSet(Group.Columns.BrandK, Group.Columns.UrlName, Group.Columns.LastPost);
					if (Vars.DevEnv)
						q.TopRecords = 10;
					else
						q.TopRecords = 40000;
					GroupSet bobset = new GroupSet(q);
					for (int i = 0; i < bobset.Count; i++)
					{
						Group g = bobset[i];
						#region url
						x.WriteStartElement("url");
						x.WriteElementString("loc", "http://www.dontstayin.com" + g.Url());
						if (g.LastPost.HasValue && g.LastPost > DateTime.MinValue)
						{
							x.WriteElementString("lastmod", g.LastPost.Value.ToString(dateFormat));
							TimeSpan ts = DateTime.Now.Subtract(g.LastPost.Value);
							if (ts.TotalHours < 24)
								x.WriteElementString("changefreq", "hourly");
							else if (ts.TotalDays < 7)
								x.WriteElementString("changefreq", "daily");
							else if (ts.TotalDays < 30)
								x.WriteElementString("changefreq", "weekly");
							else
								x.WriteElementString("changefreq", "monthly");
						}
						else
							x.WriteElementString("changefreq", "monthly");
						x.WriteEndElement();	//url
						#endregion
						bobset.Kill(i);
					}
				}
				#endregion

				x.WriteEndElement();	//urlset
				#endregion
			}
			x.Flush();
			x.Close();
			Response.End();

		}
	}
}
