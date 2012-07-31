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
using System.Collections.Generic;
using System.Reflection;
using Bobs;
using ScriptSharpLibrary;

namespace TestWebApp
{
	/// <summary>
	/// Summary description for WebService
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	[ScriptService]
	public class WebService : System.Web.Services.WebService
	{
		[WebMethod]
		[ScriptMethod(UseHttpGet = true)]
		public Suggestion[] GetItems(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			SetCacheExpiry(new TimeSpan(0, 1, 0));
			Query query = new Query(new And(new Q(Bobs.Usr.Columns.NickName, QueryOperator.TextStartsWith, text), new Q(Usr.Columns.NickName, QueryOperator.NotEqualTo, "")));
			query.OrderBy = new OrderBy(Usr.Columns.NickName);
			query.TopRecords = maxNumberOfItemsToGet;
			UsrSet usrSet = new UsrSet(query);
			return usrSet.ToList().ConvertAll
			(
				u => new Suggestion()
				{
					html = "<span style='color:Red;' >" + u.K.ToString() + "</span>" + u.NickName, 
					value = u.K.ToString(), 
					text = u.NickName ,
					priority = 1
				}
			).ToArray();
			
		}
		[WebMethod]
		[ScriptMethod(UseHttpGet = true)]
		public Suggestion[] GetEmails(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			SetCacheExpiry(new TimeSpan(0, 1, 0));
			Query query = new Query(new And(new Q(Bobs.Usr.Columns.Email, QueryOperator.TextContains , "%" + text+ "%"), new Q(Usr.Columns.Email, QueryOperator.NotEqualTo, "")));
			query.OrderBy = new OrderBy(Usr.Columns.NickName);
			query.TopRecords = maxNumberOfItemsToGet;
			UsrSet usrSet = new UsrSet(query);
			return usrSet.ToList().ConvertAll(u => new Suggestion() { html = u.Email, value = u.K.ToString(), text = u.Email, priority = 1}).ToArray();
			
		}
		[WebMethod]
		[ScriptMethod(UseHttpGet = true)]
		public Suggestion[] GetTags(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			SetCacheExpiry(new TimeSpan(0, 1, 0));
			Query query = new Query(new Q(Bobs.Tag.Columns.TagText, QueryOperator.TextStartsWith, text));
			query.OrderBy = new OrderBy(Bobs.Tag.Columns.TagText);
			query.TopRecords = maxNumberOfItemsToGet;
			TagSet tagSet = new TagSet(query);
			return tagSet.ToList().ConvertAll(t => new Suggestion() { html = t.TagText, text = t.TagText, value = t.K.ToString(), priority = 1 }).ToArray();
			
		}
		private void SetCacheExpiry(TimeSpan cacheDuration)
		{
			Context.Response.Cache.SetCacheability(HttpCacheability.Public);
			Context.Response.Cache.SetExpires(DateTime.Now.Add(cacheDuration));
			Context.Response.Cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
			FieldInfo maxAge = HttpContext.Current.Response.Cache.GetType().GetField("_maxAge", BindingFlags.Instance | BindingFlags.NonPublic);
			maxAge.SetValue(HttpContext.Current.Response.Cache, cacheDuration);
		}
	}
}
