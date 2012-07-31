using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using Bobs;
using Js.Controls.LatestChat;

namespace Spotted.WebServices.Controls.LatestChat
{
	/// <summary>
	/// Summary description for Latest
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[ScriptService]
	public class Service : System.Web.Services.WebService
	{
		[WebMethod]
		[ScriptMethod/*(UseHttpGet = true)*/]
		public ThreadStub[] GetThreads(int objectType, int objectK, int threadsCount, bool hasGroupObjectFilter)
		{
			IDiscussable discussable = Bob.Get((Model.Entities.ObjectType)objectType, objectK) as IDiscussable;
			return Thread.GetThreadsByIDiscussable(discussable, threadsCount, false).ToList().ConvertAll(t => CreateThreadStub(t, discussable, hasGroupObjectFilter)).ToArray();
		}

		private ThreadStub CreateThreadStub(Thread thread, IDiscussable discussable, bool hasGroupObjectFilter)
		{
			return new ThreadStub()
			{
				k = thread.K,
				authorHtml = thread.AuthorHtml,
				commentHtmlEnd = thread.CommentHtmlEnd,
				commentHtmlStart = thread.CommentHtmlStart,
				favouriteHtml = thread.FavouriteHtml("f"),
				favouriteScript = thread.FavouriteScript("f"),
				iconsHtml = thread.IconsHtml(hasGroupObjectFilter),
				pagesHtml = thread.PagesHtml(discussable, new Bobs.Thread.GetUrlByDiscussableDelegate(thread.GetThreadUrl), "k", thread.K.ToString()),
				repliesHtml = thread.RepliesHtml,
				rollover = thread.Rollover,
				subjectSafe = HttpUtility.HtmlEncode(thread.SubjectSafe),
				threadUrlSimple = thread.GetThreadUrlSimple(discussable),
				watchingHtml = thread.WatchingHtml("w"),
				watchingScript = thread.WatchingScript("w", "")
			};
		}
	}
}
