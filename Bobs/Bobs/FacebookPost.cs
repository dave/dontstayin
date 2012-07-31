using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using System.Collections.Generic;
using Cambro;
using Cambro.Web;
using Cambro.Misc;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using Facebook;


namespace Bobs
{

	#region FacebookPost
	/// <summary>
	/// Facebook posts
	/// </summary>
	[Serializable]
	public partial class FacebookPost
	{

		#region Simple members
		/// <summary>
		/// Key
		/// </summary>
		public override int K
		{
			get { return (int)this[FacebookPost.Columns.K] as int? ?? 0; }
			set { this[FacebookPost.Columns.K] = value; }
		}
		/// <summary>
		/// Date/time
		/// </summary>
		public override DateTime? DateTime
		{
			get { return (DateTime?)this[FacebookPost.Columns.DateTime]; }
			set { this[FacebookPost.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Type
		/// </summary>
		public override Model.Entities.FacebookPost.TypeEnum Type
		{
			get { return (Model.Entities.FacebookPost.TypeEnum)this[FacebookPost.Columns.Type]; }
			set { this[FacebookPost.Columns.Type] = (int)value; }
		}
		/// <summary>
		/// The connected Usr at the time
		/// </summary>
		public override int? UsrK
		{
			get { return (int?)this[FacebookPost.Columns.UsrK]; }
			set { this[FacebookPost.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Content data in XML
		/// </summary>
		public override string Content
		{
			get { return (string)this[FacebookPost.Columns.Content]; }
			set { this[FacebookPost.Columns.Content] = value; }
		}
		/// <summary>
		/// Facebook user id
		/// </summary>
		public override long? FacebookUid
		{
			get { return (long?)this[FacebookPost.Columns.FacebookUid]; }
			set { this[FacebookPost.Columns.FacebookUid] = value; }
		}
		/// <summary>
		/// Int data used for de-duplicates
		/// </summary>
		public override int DataInt
		{
			get { return (int)this[FacebookPost.Columns.DataInt]; }
			set { this[FacebookPost.Columns.DataInt] = value; }
		}
		/// <summary>
		/// Total number of hits
		/// </summary>
		public override int? Hits
		{
			get { return (int?)this[FacebookPost.Columns.Hits]; }
			set { this[FacebookPost.Columns.Hits] = value; }
		}
		#endregion

		#region CreateBuyTicket
		public static void CreateBuyTicket(Usr u, Event e)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.BuyTicket));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 5)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, e.K),
					new Q(FacebookPost.Columns.Type, TypeEnum.BuyTicket));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.BuyTicket;
					fp.Content = "EventK=" + e.K.ToString();
					fp.DataInt = e.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					par["picture"] = e.HasAnyPic ? e.AnyPicPath : "http://www.dontstayin.com/gfx/logo-90.png";
					par["link"] = "http://" + Vars.DomainName + e.Url() + "?fbpk=" + fp.K.ToString();
					par["name"] = e.FriendlyNameGeneric(true, false, false, false);
					par["caption"] = "Don't Stay In";
					par["description"] = ("@ " + e.FriendlyNameGeneric(false, true, true, true) + " - " + e.ShortDetailsHtml).TruncateWithDots(990);
					u.Facebook.PutWallPost("I just bought a ticket...", par);
				}
			}
		}
		#endregion

		#region CreateJoinGroup
		public static void CreateJoinGroup(Usr u, Group g)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.JoinGroup));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 10)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, g.K),
					new Q(FacebookPost.Columns.Type, TypeEnum.JoinGroup));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.JoinGroup;
					fp.Content = "GroupK=" + g.K.ToString();
					fp.DataInt = g.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					if (g.HasPic)
					{
						par["picture"] = g.PicPath;
					}
					par["link"] = "http://" + Vars.DomainName + g.Url() + "?fbpk=" + fp.K.ToString();
					par["name"] = g.Name;
					par["caption"] = "Don't Stay In";

					par["description"] = g.Description;
					u.Facebook.PutWallPost("I just joined: " + g.Name, par);
				}
			}
		}
		#endregion

		#region CreateNewBuddy
		public static void CreateNewBuddy(Usr u, Usr u1, bool meInit)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.NewBuddy));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 15)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, u1.K),
					new Q(FacebookPost.Columns.Type, TypeEnum.NewBuddy));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.NewBuddy;
					fp.Content = "UsrK=" + u1.K.ToString();
					fp.DataInt = u1.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					if (u1.HasPicNotFacebook)
					{
						par["picture"] = u1.PicPath;
					}
					par["link"] = "http://" + Vars.DomainName + u1.Url() + "?fbpk=" + fp.K.ToString();
					par["name"] = u1.NickName;
					par["caption"] = "Don't Stay In";
					par["description"] = "";
					u.Facebook.PutWallPost(meInit ? ("I just added " + u1.NickName + " as a buddy.") : (u1.NickName + " just added me as a buddy."), par);
				}
			}
		}
		#endregion

		#region CreateArticlePublish
		public static void CreateArticlePublish(Usr u, Article a)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.ArticlePublish));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 15)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, a.K),
					new Q(FacebookPost.Columns.Type, TypeEnum.ArticlePublish));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.ArticlePublish;
					fp.Content = "ArticleK=" + a.K.ToString();
					fp.DataInt = a.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					if (a.HasPic)
					{
						par["picture"] = a.PicPath;
					}
					par["link"] = "http://" + Vars.DomainName + a.Url() + "?fbpk=" + fp.K.ToString();
					par["name"] = a.Title.TruncateWithDots(50);
					par["caption"] = "Don't Stay In";

					par["description"] = a.Summary.TruncateWithDots(990);
					u.Facebook.PutWallPost("", par);
				}
			}
		}
		#endregion

		#region CreateFavouriteTopic
		public static void CreateFavouriteTopic(Usr u, Thread t)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.FavouriteTopic));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 15)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, t.K),
					new Q(FacebookPost.Columns.Type, TypeEnum.FavouriteTopic));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.FavouriteTopic;
					fp.Content = "ThreadK=" + t.K.ToString();
					fp.DataInt = t.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					if (t.PhotoK > 0 && t.Photo != null)
					{
						par["picture"] = t.Photo.IconPath;
					}
					par["link"] = "http://" + Vars.DomainName + t.Url() + "?fbpk=" + fp.K.ToString();
					par["name"] = t.Subject;
					par["caption"] = "Don't Stay In";

					par["description"] = "";
					u.Facebook.PutWallPost("", par);
				}
			}
		}
		#endregion

		#region CreateFavouritePhoto
		public static void CreateFavouritePhoto(Usr u, Photo p)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.FavouritePhoto));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 10)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, p.K),
					new Q(FacebookPost.Columns.Type, TypeEnum.FavouritePhoto));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.FavouritePhoto;
					fp.Content = "PhotoK=" + p.K.ToString();
					fp.DataInt = p.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					par["picture"] = p.IconPath;
					par["link"] = "http://" + Vars.DomainName + p.Url() + "?fbpk=" + fp.K.ToString();
					par["caption"] = "Don't Stay In";
					if (p.Event != null)
					{
						par["name"] = p.Event.FriendlyNameGeneric(true, false, false, false);
						par["description"] = ("@ " + p.Event.FriendlyNameGeneric(false, true, true, true)).TruncateWithDots(990);
					}
					else if (p.Article != null)
					{
						par["name"] = p.Article.Name;
					}
					par["description"] = "";
					u.Facebook.PutWallPost("", par);
				}
			}
		}
		#endregion

		#region CreateLaugh
		public static void CreateLaugh(Usr u, Comment c)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.Laugh));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 15)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, c.K),
					new Q(FacebookPost.Columns.Type, TypeEnum.Laugh));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.Laugh;
					fp.Content = "CommentK=" + c.K.ToString();
					fp.DataInt = c.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					if (c.Thread.PhotoK > 0 && c.Thread.Photo != null)
					{
						par["picture"] = c.Thread.Photo.IconPath;
					}
					par["link"] = "http://" + Vars.DomainName + c.Thread.Url() + "?fbpk=" + fp.K.ToString() + "#CommentK-" + c.K.ToString();
					par["name"] = c.Thread.Subject;
					par["caption"] = "Don't Stay In";

					par["description"] = c.Text.Strip().TruncateWithDots(990);
					u.Facebook.PutWallPost("This made me laugh...", par);
				}
			}
		}
		#endregion

		#region CreateNewTopic
		public static void CreateNewTopic(Usr u, Thread t, Comment c)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.NewTopic));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 15)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, t.K),
					new Or(new Q(FacebookPost.Columns.Type, TypeEnum.NewTopic), new Q(FacebookPost.Columns.Type, TypeEnum.NewTopicNews), new Q(FacebookPost.Columns.Type, TypeEnum.NewTopicReview)));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.NewTopic;
					fp.Content = "ThreadK=" + t.K.ToString();
					fp.DataInt = t.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					if (t.PhotoK > 0 && t.Photo != null)
					{
						par["picture"] = t.Photo.IconPath;
					}
					par["link"] = "http://" + Vars.DomainName + t.Url() + "?fbpk=" + fp.K.ToString();
					par["name"] = t.Subject;
					par["caption"] = "Don't Stay In";

					par["description"] = c.Text.Strip().TruncateWithDots(990);
					u.Facebook.PutWallPost("", par);
				}
			}
		}
		#endregion

		#region CreateNews
		public static void CreateNews(Usr u, Thread t, Comment c)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.NewTopicNews));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 15)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, t.K),
					new Or(new Q(FacebookPost.Columns.Type, TypeEnum.NewTopic), new Q(FacebookPost.Columns.Type, TypeEnum.NewTopicNews), new Q(FacebookPost.Columns.Type, TypeEnum.NewTopicReview)));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.NewTopicNews;
					fp.Content = "ThreadK=" + t.K.ToString();
					fp.DataInt = t.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					if (t.PhotoK > 0 && t.Photo != null)
					{
						par["picture"] = t.Photo.IconPath;
					}
					par["link"] = "http://" + Vars.DomainName + t.Url() + "?fbpk=" + fp.K.ToString();
					par["name"] = t.Subject;
					par["caption"] = "Don't Stay In";

					par["description"] = c.Text.Strip().TruncateWithDots(990);
					u.Facebook.PutWallPost("", par);
				}
			}
		}
		#endregion

		#region CreateEventReview
		public static void CreateEventReview(Usr u, Thread t, Comment c)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.NewTopicReview));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 15)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, t.K),
					new Or(new Q(FacebookPost.Columns.Type, TypeEnum.NewTopic), new Q(FacebookPost.Columns.Type, TypeEnum.NewTopicNews), new Q(FacebookPost.Columns.Type, TypeEnum.NewTopicReview)));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.NewTopicReview;
					fp.Content = "ThreadK=" + t.K.ToString();
					fp.DataInt = t.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					par["link"] = "http://" + Vars.DomainName + t.Url() + "?fbpk=" + fp.K.ToString();
					par["name"] = t.Event.Name.TruncateWithDots(20) + " @ " + t.Event.Venue.Name.TruncateWithDots(20);
					par["caption"] = "Don't Stay In";

					par["description"] = c.Text.Strip().TruncateWithDots(990);
					u.Facebook.PutWallPost(t.Subject, par);
				}
			}
		}
		#endregion

		#region CreateFrontPagePhotoDsi
		public static void CreateFrontPagePhotoDsi(Photo photo, string caption)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-7)),
				new Q(FacebookPost.Columns.FacebookUid, FacebookCommon.Common(Facebook.Apps.Dsi).AppId),
				new Q(FacebookPost.Columns.DataInt, photo.K),
				new Q(FacebookPost.Columns.Type, TypeEnum.FrontPagePhotoDsi));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count == 0)
			{

				FacebookPost fp = new FacebookPost();
				fp.Hits = 0;
				fp.FacebookUid = FacebookCommon.Common(Facebook.Apps.Dsi).AppId;
				fp.DateTime = System.DateTime.Now;
				fp.Type = TypeEnum.FrontPagePhotoDsi;
				fp.Content = "PhotoK=" + photo.K.ToString();
				fp.DataInt = photo.K;
				fp.UsrK = 0;
				fp.Update();

				//send facebook message
				//http://developers.facebook.com/docs/reference/api/post
				Dictionary<string, object> par = new Dictionary<string, object>();
				par["picture"] = photo.IconPath;
				par["link"] = "http://" + Vars.DomainName + photo.Url() + "?fbpk=" + fp.K.ToString();
				par["name"] = "Photo by " + photo.Usr.NickName;
				par["caption"] = "Don't Stay In";
				if (photo.Event != null)
				{
					par["description"] = ("From " + photo.Event.FriendlyNameGeneric(true, true, true, true)).TruncateWithDots(990);
				}
				else if (photo.Article != null)
				{
					par["description"] = ("From " + photo.Article.Name).TruncateWithDots(990);
				}

				FacebookGraphAPI dsiPage = FacebookGraphAPI.GetPageApi(Facebook.Apps.Dsi);
				dsiPage.PutWallPost(caption, par);
			}
		}
		#endregion

		#region CreateFrontPagePhoto
		public static void CreateFrontPagePhoto(Usr u, Photo p)
		{
			Query q1 = new Query();
			q1.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-7)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.DataInt, p.K),
				new Q(FacebookPost.Columns.Type, TypeEnum.FrontPagePhoto));
			FacebookPostSet fps1 = new FacebookPostSet(q1);
			if (fps1.Count == 0)
			{

				FacebookPost fp = new FacebookPost();
				fp.Hits = 0;
				fp.FacebookUid = u.Facebook.Uid;
				fp.DateTime = System.DateTime.Now;
				fp.Type = TypeEnum.FrontPagePhoto;
				fp.Content = "PhotoK=" + p.K.ToString();
				fp.DataInt = p.K;
				fp.UsrK = u.K;
				fp.Update();

				//send facebook message
				//http://developers.facebook.com/docs/reference/api/post
				Dictionary<string, object> par = new Dictionary<string, object>();
				par["picture"] = p.IconPath;
				par["link"] = "http://" + Vars.DomainName + p.Url() + "?fbpk=" + fp.K.ToString();
				par["caption"] = "Don't Stay In";
				if (p.Event != null)
				{
					par["name"] = p.Event.FriendlyNameGeneric(true, false, false, false);
					par["description"] = ("@ " + p.Event.FriendlyNameGeneric(false, true, true, true)).TruncateWithDots(990);
				}
				else if (p.Article != null)
				{
					par["name"] = p.Article.Name;
				}
				u.Facebook.PutWallPost("I got on the front page of Don't Stay In...", par);
			}
		}
		#endregion

		#region CreatePhotoUpload
		public static void CreatePhotoUpload(Usr u, Gallery g)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.PhotoUpload));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 10)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, g.K),
					new Q(FacebookPost.Columns.Type, TypeEnum.PhotoUpload));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.PhotoUpload;
					fp.Content = "GalleryK=" + g.K.ToString();
					fp.DataInt = g.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					par["picture"] = g.MainPhoto != null ? g.MainPhoto.IconPath : "http://www.dontstayin.com/gfx/logo-90.png";
					par["link"] = "http://" + Vars.DomainName + g.Url() + "?fbpk=" + fp.K.ToString();
					par["caption"] = "Don't Stay In";
					if (g.Event != null)
					{
						par["name"] = g.Event.FriendlyNameGeneric(true, false, false, false);
						par["description"] = ("@ " + g.Event.FriendlyNameGeneric(false, true, true, true)).TruncateWithDots(990);
					}
					else if (g.Article != null)
					{
						par["name"] = g.Article.Name;
					}
					par["description"] = "";
					u.Facebook.PutWallPost("", par);
				}
			}
		}
		#endregion

		#region CreateSpotted
		public static void CreateSpotted(Usr u, Photo p)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.Spotted));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 10)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, p.GalleryK),
					new Q(FacebookPost.Columns.Type, TypeEnum.Spotted));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.Spotted;
					fp.Content = "PhotoK=" + p.K.ToString();
					fp.DataInt = p.GalleryK;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					par["picture"] = p.IconPath;
					par["link"] = "http://" + Vars.DomainName + p.Url() + "?fbpk=" + fp.K.ToString();
					par["caption"] = "Don't Stay In";
					if (p.Event != null)
					{
						par["name"] = p.Event.FriendlyNameGeneric(true, false, false, false);
						par["description"] = ("@ " + p.Event.FriendlyNameGeneric(false, true, true, true)).TruncateWithDots(990);
					}
					else if (p.Article != null)
					{
						par["name"] = p.Article.Name;
					}
					u.Facebook.PutWallPost("I've been spotted...", par);
				}
			}
		}
		#endregion

		#region CreateAttendEvent
		public static void CreateAttendEvent(Usr u, Event e)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.DateTime, QueryOperator.GreaterThan, System.DateTime.Now.AddDays(-1)),
				new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.AttendEvent));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count < 5)
			{
				Query q1 = new Query();
				q1.QueryCondition = new And(
					new Q(FacebookPost.Columns.FacebookUid, u.Facebook.Uid),
					new Q(FacebookPost.Columns.DataInt, e.K),
					new Or(new Q(FacebookPost.Columns.Type, TypeEnum.BuyTicket), new Q(FacebookPost.Columns.Type, TypeEnum.AttendEvent)));
				FacebookPostSet fps1 = new FacebookPostSet(q1);
				if (fps1.Count == 0)
				{

					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.AttendEvent;
					fp.Content = "EventK=" + e.K.ToString();
					fp.DataInt = e.K;
					fp.UsrK = u.K;
					fp.Update();

					//send facebook message
					//http://developers.facebook.com/docs/reference/api/post
					Dictionary<string, object> par = new Dictionary<string, object>();
					par["picture"] = e.HasAnyPic ? e.AnyPicPath : "http://www.dontstayin.com/gfx/logo-90.png";
					par["link"] = "http://" + Vars.DomainName + e.Url() + "?fbpk=" + fp.K.ToString();
					par["name"] = e.FriendlyNameGeneric(true, false, false, false);
					par["caption"] = "Don't Stay In";
					par["description"] = ("@ " + e.FriendlyNameGeneric(false, true, true, true) + " - " + e.ShortDetailsHtml).TruncateWithDots(990);
					u.Facebook.PutWallPost(e.IsFuture ? "I'm going to this..." : "I went to this...", par);
				}
			}
		}
		#endregion

		#region CreateNewConnect
		public static void CreateNewConnect(FacebookGraphAPI facebook, int usrK)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(FacebookPost.Columns.FacebookUid, facebook.Uid),
				new Q(FacebookPost.Columns.Type, TypeEnum.NewConnect));
			FacebookPostSet fps = new FacebookPostSet(q);
			if (fps.Count == 0)
			{

				FacebookPost fp = new FacebookPost();
				fp.Hits = 0;
				fp.FacebookUid = facebook.Uid;
				fp.DateTime = System.DateTime.Now;
				fp.Type = TypeEnum.NewConnect;
				fp.Content = "";
				fp.UsrK = usrK;
				fp.Update();

				//send facebook message
				//http://developers.facebook.com/docs/reference/api/post
				Dictionary<string, object> par = new Dictionary<string, object>();
				par["picture"] = Vars.DevEnv ? "http://pix-cdn.dontstayin.com/db864428-71be-4216-9d06-d641ce992301.png" : "http://www.dontstayin.com/gfx/logo-90.png";
				par["link"] = "http://www.dontstayin.com/?fbpk=" + fp.K.ToString();
				par["name"] = "Don't Stay In";
				facebook.PutWallPost("I've just connected to Don't Stay In...", par);
			}
		}
		#endregion


		public static void AddEvent(Usr u, Event e, bool attendEvent)
		{

			throw new Exception("Disabled");

			try
			{
				long id = 0;
				if (e.FacebookEventId.HasValue)
				{
					id = e.FacebookEventId.Value;
				}
				else
				{
					#region add event
					FacebookGraphAPI dsiPage = FacebookGraphAPI.GetPageApi(Facebook.Apps.Dsi);

					Dictionary<string, object> ev = new Dictionary<string, object>();
					//access_token, name, description, location, street, city, privacy_type, start_time, end_time, picture
					ev["name"] = e.Name;
					ev["location"] = e.FriendlyNameGeneric(false, true, true, false);
					ev["file.jpg"] = Cropper.TryToGetLargerPic(e, 1.79);
					DateTime d = e.DateTime;
					if (e.StartTime == Model.Entities.Event.StartTimes.Morning)
						d = d.AddHours(6);
					else if (e.StartTime == Model.Entities.Event.StartTimes.Daytime)
						d = d.AddHours(14);
					else if (e.StartTime == Model.Entities.Event.StartTimes.Evening)
						d = d.AddHours(22);
					ev["start_time"] = d.ToString("s");
					ev["end_time"] = d.AddHours(8).ToString("s");
					ev["privacy_type"] = "OPEN";
					ev["no_story"] = "1";


					FacebookPost fp = new FacebookPost();
					fp.Hits = 0;
					fp.FacebookUid = u.Facebook.Uid;
					fp.DateTime = System.DateTime.Now;
					fp.Type = TypeEnum.AddEvent;
					fp.Content = "EventK=" + e.K.ToString();
					fp.DataInt = e.K;
					fp.UsrK = u.K;
					fp.Update();

					Newtonsoft.Json.Linq.JObject post;

					try
					{

						ev["description"] = "Click for full details: http://" + Vars.DomainName + e.UrlShort() + "?fbpk=" + fp.K.ToString();

						//Newtonsoft.Json.Linq.JObject post = u.Facebook.PutObject(u.Facebook.Uid.ToString(), "events", ev);
						post = dsiPage.PutObject(FacebookCommon.Common(Facebook.Apps.Dsi).PageId.ToString(), "events", ev);

					}
					catch
					{
						fp.Delete();
						return;
					}



					try
					{
						id = long.Parse(post["id"].ToString());

					}
					catch { }

					if (id > 0)
					{
						e.FacebookEventId = id;
						e.Update();
					}
					#endregion
				}


				if (attendEvent && id > 0 && u.FacebookConnected && u.FacebookEventAttend)
				{
					try
					{
						u.Facebook.PutObject(id.ToString(), "attending", new Dictionary<string, object>());
					}
					catch { }
				}
			}
			catch
			{
			}
		}
		
	}
	#endregion

}
