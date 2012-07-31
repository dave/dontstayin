using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections;
using Cambro.Misc;
using Spotted;
using localNamespace = Spotted;
using Bobs;
using Bobs.Main;
using Spotted.Pages.Countries;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Text;
using Common;
using Facebook;
using System.Collections.Generic;


namespace Local
{

	#region DsiHandler (removed, but this works also...
	public class xDsiHandler : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{

			#region Redirect if incoming on a different domain
			if (!Vars.DevEnv)
			{
				string domain = context.Request.Url.Host.ToLower();

				if (!domain.Equals("www.dontstayin.com"))
				{
					if (domain.Equals("dontstayin.com"))
					{
						context.Response.Redirect(Vars.UrlScheme + "://www.dontstayin.com" + context.Request.Url.PathAndQuery, true);
					}
					else
					{
						System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^server[0-9]{1,}i?\.dontstayin\.com$");
						if (!r.IsMatch(domain))
						{
							if (domain.StartsWith("www."))
								domain = domain.Substring(4);

							DomainSet ds = new DomainSet(new Query(new Q(Domain.Columns.DomainName, domain)));

							if (ds.Count > 0)
							{
								Domain d = ds[0];
								d.IncrementStats();
								d.Redirect();
							}
							else
								context.Response.Redirect(Vars.UrlScheme + "://www.dontstayin.com" + context.Request.Url.PathAndQuery, true);
						}
					}
				}
			}
			#endregion

			UrlInfo urlInfo = new UrlInfo(context.Request.Path, context.Request.Browser, false, false, false);

			//	if (Url.OverrideHttpHandler)
			//		return PageParser.GetCompiledPageInstance(context.Request.Url.ToString(), context.Request.Path, context);

			////if (context.Request.Path.ToLower().EndsWith("/support/photoupload.aspx"))
			////{
			////    Spotted.GenericPage genericPage = null;
			////    genericPage = (Spotted.GenericPage)PageParser.GetCompiledPageInstance(urlInfo.MasterPath, context.Server.MapPath(urlInfo.MasterPath), context);
			////    genericPage.Url = urlInfo;

			////    ((IHttpHandler)genericPage).ProcessRequest(context);
			////    context.ApplicationInstance.CompleteRequest();
			////}

			try
			{
				Spotted.GenericPage genericPage = null;
				genericPage = (Spotted.GenericPage)PageParser.GetCompiledPageInstance(urlInfo.MasterPath, context.Server.MapPath(urlInfo.MasterPath), context);
				genericPage.Url = urlInfo;

				((IHttpHandler)genericPage).ProcessRequest(context);
				context.ApplicationInstance.CompleteRequest();
			}
			catch (Exception ex)
			{
				if (!Vars.DevEnv)
				{
					context.Response.Redirect("/pages/home");
				}
				else
					throw ex;
			}
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

	}
	#endregion

	#region DsiHandler
	//This was removed 21/04/2007 by DaveB. The other method (above) seems to use the impersonated user, so it works properly when using Integrated Authentication. It probably breaks loads of stuff, so watch out!!!
	// I was right - the onther one breaks the site... it somehow breaks redirecting, so when you're logged off, and you click a feature that needs a login, the redirect takes you to the front page??? I've reverted it.
	public class DsiHttpHandlerFactory : IHttpHandlerFactory
	{
 
		public virtual IHttpHandler GetHandler(HttpContext context, String requestType, String url, String pathTranslated)
		{



			#region Redirect if incoming on a different domain
			if (!Vars.DevEnv)
			{
				string domain = context.Request.Url.Host.ToLower();

				if (domain != "www.dontstayin.com" && domain != "mixmag-vote.com" && domain != "mixmag-greatest.com" && domain != "mixmag-greatest.dontstayin.com")
				{
					if (domain == "dontstayin.com")
					{
						context.Response.Redirect(Vars.UrlScheme + "://www.dontstayin.com" + context.Request.Url.PathAndQuery, true);
					}
					else if (domain == "www.mixmagvote.com" || domain == "mixmagvote.com" || domain == "www.mixmag-vote.com")
					{
						context.Response.Redirect(Vars.UrlScheme + "://mixmag-vote.com" + context.Request.Url.PathAndQuery, true);
					}
					else if (domain == "www.greatest.dj" || domain == "greatest.dj" || domain == "www.mixmag-greatest.com" || domain == "www.mixmaggreatest.com" || domain == "mixmaggreatest.com")
					{
						context.Response.Redirect(Vars.UrlScheme + "://mixmag-greatest.com" + context.Request.Url.PathAndQuery, true);
					}
					else
					{
						System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^server[0-9]{1,}i?\.dontstayin\.com$");
						if (!r.IsMatch(domain))
						{
							if (domain.StartsWith("www."))
								domain = domain.Substring(4);

							DomainSet ds = new DomainSet(new Query(new Q(Domain.Columns.DomainName, domain)));

							if (ds.Count > 0)
							{
								Domain d = ds[0];
								d.IncrementStats();
								d.Redirect();
							}
							else
								context.Response.Redirect(Vars.UrlScheme + "://www.dontstayin.com" + context.Request.Url.PathAndQuery, true);
						}
					}
				}
			}
			#endregion

			bool isMixmagVote = context.Request.Url.Host.ToLower() == "mixmag-vote.com";// || (Vars.DevEnv && Common.Properties.MachineName == "DEV1");
			bool isMixmagGreatest = context.Request.Url.Host.ToLower() == "mixmag-greatest.com" || context.Request.Url.Host.ToLower() == "mixmag-greatest.dontstayin.com";// || (Vars.DevEnv && Common.Properties.MachineName=="DEV1");

			UrlInfo Url = new UrlInfo(context.Request.Path, context.Request.Browser, false, isMixmagVote, isMixmagGreatest);
			
			if (Url.OverrideHttpHandler)
				return PageParser.GetCompiledPageInstance(url, pathTranslated, context);

			Spotted.GenericPage genericPage = null;
			try
			{
				genericPage = (Spotted.GenericPage)PageParser.GetCompiledPageInstance(Url.MasterPath, context.Server.MapPath(Url.MasterPath), context);
				genericPage.Url = Url;
				return genericPage;
			}
			catch (Exception ex)
			{
				if (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 2 && !url.Equals("/pages/home"))
				{
					context.Response.Redirect("/pages/home");
					return null;
				}
				else
					throw ex;
			}

		}

		public virtual void ReleaseHandler(IHttpHandler handler) { }

	}
	#endregion 

	
}

namespace Spotted
{

	#region GenericPage
	public class GenericPage : System.Web.UI.Page
	{
		public GenericPage()
			: base() 
		{
			Page.Init += new EventHandler(GenericPage_Init);
			Page.PreRender += new EventHandler(GenericPage_PreRender);
		}

		#region SslPage
		public bool SslPage
		{
			get
			{
				return sslPage;
			}
			set
			{
				sslPage = value;
				if (HttpContext.Current != null)
				{
					HttpContext.Current.Items["SslPage"] = value;
				}
			}
		}
		private bool sslPage = false;
		#endregion
		#region Memcached viewstate
		protected override void SavePageStateToPersistenceMedium(object state)
		{

			if (Common.Settings.UseMemcachedToStoreViewState)
			{

				try
				{
					var key = Guid.NewGuid().ToString();
					Caching.Instances.ViewState.Store("ViewStateMemcachedKey" + key, state, TimeSpan.FromHours(48));
					base.SavePageStateToPersistenceMedium(new Pair("ViewStateMemcachedKey|" + DateTime.Now.ToString(), key));
				}
				catch (Caching.Memcached.MemcachedClientException ex)
				{
					SpottedException.TryToSaveExceptionAndChildExceptions(ex);
					base.SavePageStateToPersistenceMedium(state);
				}
			}
			else
			{
				base.SavePageStateToPersistenceMedium(state);
			}

		}
		protected override object LoadPageStateFromPersistenceMedium()
		{
			var pair = base.LoadPageStateFromPersistenceMedium() as Pair;
			if ((pair.First as string ?? "").StartsWith("ViewStateMemcachedKey"))
			{
				var viewState = Caching.Instances.ViewState.Get("ViewStateMemcachedKey" + pair.Second) as object;
				if (viewState == null)
				{
					throw new DsiUserFriendlyException("Your session has expired. It was created at " + (pair.First as string).Split('|')[1]);
				}
				else
				{
					return viewState;
				}
			}
			else
			{
				return pair;
			}
		}

		#endregion
		#region GenericUserControl
		public GenericUserControl GenericUserControl;
		#endregion
		#region MainContentPlaceHolder
		public PlaceHolder MainContentPlaceHolder;
		#endregion

		#region GenericPage_Init
		public void GenericPage_Init(object o, System.EventArgs e)
		{
			Response.AddHeader("X-XSS-Protection", "0");

			if (Usr.CheckForSpamBot(true))
				HttpContext.Current.Response.Redirect("/popup/captcha?url=" + Server.UrlEncode(HttpContext.Current.Request.Url.ToString()));

			#region Load ContentUserControl
			try
			{
				GenericUserControl = (GenericUserControl)this.LoadControl(Url.PagePath);
			}
			catch (HttpException ex)
			{
				if (ex.Message == "The file '" + Url.PagePath + "' does not exist.")
					throw new MalformedUrlException();
				else throw ex;
			}
			GenericUserControl.ID = "Content";
			GenericUserControl.GenericContainerPage = this;
			MainContentPlaceHolder.Controls.Add(GenericUserControl);
			HttpContext.Current.Items["CurrentPage"] = this;
			#endregion
		}
		#endregion
		#region GenericPage_PreRender
		public void GenericPage_PreRender(object o, System.EventArgs e)
		{
			if (!Url.IsMixmagGreatest)
			{
				if (!SslPage && HttpContext.Current.Request.IsSecureConnection && !Vars.DevEnv)
				{
					Response.Redirect("http://" + HttpContext.Current.Request.Url.ToString().Substring(8));
				}
				if (SslPage && !HttpContext.Current.Request.IsSecureConnection && !Vars.DevEnv)
				{
					Response.Redirect("https://" + HttpContext.Current.Request.Url.ToString().Substring(7));
				}
			}
			if (!Page.IsPostBack && Usr.CheckForSpamBot(false))
				HttpContext.Current.Response.Redirect("/popup/captcha?url=" + Server.UrlEncode(HttpContext.Current.Request.Url.ToString()));
		}
		#endregion

		#region Render
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			HttpContext.Current.Items["CurrentPageHasFiredRender"] = true;
		}
		#endregion
		
		#region ViewStatePublic
		public StateBag ViewStatePublic
		{
			get
			{
				return this.ViewState;
			}
		}
		#endregion
		#region Prefs
		protected override object SaveViewState()
		{
			//if (HttpContext.Current.Items["Prefs"] != null)
			//{
			//    if (Usr.PrefsHaveChanged)
			//    {
			//        Cambro.Web.Helpers.SetCookie("Prefs", Cambro.Misc.Utility.SerialiseHashtable(Usr.Prefs), true);
			//        if (Usr.Current != null)
			//            Usr.Current.Update();
			//    }
			//}
			return base.SaveViewState();
		}
		#endregion
		#region Url
		public UrlInfo Url
		{
			get
			{
				//if (HttpContext.Current.Items["Url"] != null)
				//	url = (UrlInfo)HttpContext.Current.Items["Url"];
				return url;
			}
			set
			{
				url = value;
			}
		}
		private UrlInfo url;
		#endregion

		#region ClientRequest
		[WebMethod(EnableSession = true)]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
		public static object ClientRequest(string typeName, string methodName, object[] args)
		{
			try
			{
				Type ctl = Type.GetType(typeName);
				if (ctl != null)
				{
					MethodInfo method = ctl.GetMethod(methodName);
					if (method.GetCustomAttributes(typeof(ClientAttribute), true).Length == 0)
						throw new Exception("Clients methods must have Client attribute set.");

					ParameterInfo[] parameters = method.GetParameters();
					for (int i = 0; i < parameters.Length; i++)
					{
						ParameterInfo param = parameters[i];
						object value = args[i];
						if (param.ParameterType == typeof(string))
						{
							args[i] = value.ToString();
						}
						else if (args[i] is System.Collections.Generic.Dictionary<string, object> && param.ParameterType == typeof(System.Collections.Hashtable))
						{
							System.Collections.Generic.Dictionary<string, object> dict = (System.Collections.Generic.Dictionary<string, object>)args[i];
							Hashtable h = new Hashtable();
							foreach (string key in dict.Keys)
								h[key] = dict[key];
							args[i] = h;
						}
					}

					object o = method.Invoke(
						null,
						  System.Reflection.BindingFlags.Static
						| System.Reflection.BindingFlags.InvokeMethod
						| System.Reflection.BindingFlags.Public
						| System.Reflection.BindingFlags.IgnoreCase,
						null, args ?? new object[] { }, null);
					if (o != null)
					{
						if (o is string || o.GetType().IsValueType)
						{
							//return o.ToString(); // If it is a string or value type, return a string
							Hashtable ret = new Hashtable();
							ret["Value"] = o;
							return ret;
						}

						// If it is a complex object, return a serialized version of it.
						//JavaScriptSerializer serializer = new JavaScriptSerializer();
						//return serializer.Serialize(o); // allow anonymous types, etc

						return o;
					}
				}
				return null;
				//return "{}"; // return an empty JSON object
			}
			catch (Exception e)
			{
				Exception exceptionToReturn = null;
				if (e is TargetInvocationException)
					exceptionToReturn = e.InnerException;
				else
					exceptionToReturn = e;

				Hashtable ex = new Hashtable();
				ex["Exception"] = true;
				ex["Message"] = exceptionToReturn.Message;
				ex["StackTrace"] = exceptionToReturn.StackTrace;
				ex["ExceptionType"] = exceptionToReturn.GetType().ToString();
				return ex;

				//Hashtable ret = new Hashtable();
				//ret["d"] = ex;
				//return ret;
			}
		}
		#endregion

		//public void JQueryInclude(string name)
		//{
		//    ScriptManager.RegisterClientScriptInclude(this, typeof(System.Web.UI.Page), "jquery." + name, "/misc/jquery/jquery-ui-1-8-1/ui/minified/jquery." + name + ".min.js");
		//}
		//public void JQueryInclude()
		//{
		//    ScriptManager.RegisterClientScriptInclude(this, typeof(System.Web.UI.Page), "jquery", "/misc/jquery/jquery-ui-1-8-1/jquery-1.4.2.min.js");
		//}
	}
	#endregion

	#region ClientScriptAttribute
	[AttributeUsage(AttributeTargets.All)]
	public class ClientScriptAttribute : System.Attribute
	{
	}
	#endregion
	#region ScriptSharp
	public class ScriptSharp
	{
		public static bool HasClientScript(object ob, bool checkParentIfThisIsAscxAspx)
		{
			Type t = ob.GetType();

			if (checkParentIfThisIsAscxAspx && (t.Name.EndsWith("_ascx") || t.Name.EndsWith("_aspx")))
				t = t.BaseType;

			return HasClientScript(t);
		}
		public static bool HasClientScript(Type t)
		{
			foreach (object attribute in t.GetCustomAttributes(false))
			{
				if (attribute is ClientScriptAttribute)
				{
					return true;
				}
			}
			return false;
		}
		public static void Include(Page p, string name)
		{
#if DEBUG
			string ending = ".debug.js";
#else
			string ending = ".js";
#endif
			string fullName = "/misc/Js/Desktop" + name + ending;

			ScriptManager.RegisterClientScriptInclude(p, typeof(System.Web.UI.Page), fullName, fullName);
		}

		public static void RegisterAllIncludes(Control control)
		{
			if (control is IIncludesJs)
				((IIncludesJs)control).IncludeJsInternal();

			RegisterInclude(control.Page, control.GetType());

		}
		public static void RegisterInclude(Page page, Type type)
		{
			if (ScriptManager.GetCurrent(page) == null || !ScriptManager.GetCurrent(page).IsInAsyncPostBack)
			{
				if (type.Name.EndsWith("_ascx") || type.Name.EndsWith("_aspx"))
					type = type.BaseType;
				
				string typeName = "";
				try
				{
					typeName = type.FullName;
				}
				catch { }


				while (
					typeName.Length > 0 &&
					typeName != "Spotted.EnhancedUserControl" &&
					typeName != "Spotted.EnhancedHtmlControl" &&
					typeName != "Spotted.MobileEnhancedUserControl" &&
					typeName != "Spotted.MobileEnhancedHtmlControl" &&
					typeName != "Spotted.DsiUserControl" &&  //these last two are now in Library
					typeName != "Spotted.GenericUserControl" //these last two are now in Library
				)
				{
					string name = "";
					if (typeName.StartsWith("JsWebControls."))
					{
						name = "/ClientControls";
					}
					else
					{
						name = typeName.Substring(7).Replace(".", "/");
					}

					if (ScriptSharp.HasClientScript(type))
						ScriptSharp.Include(page, name);
					
					if (type.BaseType == null)
						return;

					type = type.BaseType;
					typeName = type.FullName;

				}

			}
		}
		//internal void RegisterIncludes(Control control)
		//{
		//    if (ScriptManager.GetCurrent(control.Page) == null || !ScriptManager.GetCurrent(control.Page).IsInAsyncPostBack)
		//    {
		//        List<string> parts = new List<string>();
		//        var current = control.GetType();
		//        if (current.Name.EndsWith("_ascx"))
		//            current = current.BaseType;

		//        while (
		//            current.FullName != "Spotted.EnhancedUserControl" &&
		//            current.FullName != "Spotted.EnhancedHtmlControl" &&
		//            current.FullName != "Spotted.MobileEnhancedUserControl" &&
		//            current.FullName != "Spotted.MobileEnhancedHtmlControl" &&
		//            current.FullName != "Spotted.DsiUserControl" &&  //these last two are now in Library
		//            current.FullName != "Spotted.GenericUserControl" //these last two are now in Library
		//        )
		//        {
		//            ScriptSharp.Include(control.Page, current.FullName.Substring(7).Replace(".", "/"));
		//            current = current.BaseType;
		//        }

		//    }
		//}

		public static void RenderAfterContent(Control control, HtmlTextWriter writer)
		{
			if (ScriptManager.GetCurrent(control.Page) == null || !ScriptManager.GetCurrent(control.Page).IsInAsyncPostBack)
			{
				Type current = control.GetType();
				if (current.Name.EndsWith("_ascx") || current.Name.EndsWith("_aspx"))
					current = current.BaseType;

				string currentTypeName = "Js" + current.FullName.Substring(7);

				writer.Write(
					String.Format(
						@"
<script>
var {0}View = new {1}.View('{0}');
if ({1}.Controller) 
{{
	var {0}Controller = new {1}.Controller({0}View); 
}}
</script>",
						control.ClientID,
						currentTypeName
					)
				);
			}
		}
	}
	#endregion
	#region JQuery
	public class JQuery
	{
		
		public static void Include(Page p)
		{
	//		ScriptManager.RegisterClientScriptInclude(p, typeof(System.Web.UI.Page), "json", "/misc/json-min.js");
	//		ScriptManager.RegisterClientScriptInclude(p, typeof(System.Web.UI.Page), "jquery", "/misc/jquery/jquery-ui-1-8-1/jquery-1.4.2.min.js?version=2");
			//ScriptManager.RegisterClientScriptInclude(p, typeof(System.Web.UI.Page), "jquery", "/misc/jquery/jquery-ui-1-8-1/jquery-1.4.2.js");
		}
		public static void Include(Page p, string name)
		{
			//ScriptManager.RegisterClientScriptInclude(p, typeof(System.Web.UI.Page), "jquery." + name, "/misc/jquery/jquery-ui-1-8-1/ui/minified/jquery." + name + ".min.js");
			ScriptManager.RegisterClientScriptInclude(p, typeof(System.Web.UI.Page), "jquery." + name, "/misc/jquery/jquery-ui-1-8-1/ui/jquery." + name + ".js");
		}
		public static void Include(Page p, string dir, string name)
		{
			ScriptManager.RegisterClientScriptInclude(p, typeof(System.Web.UI.Page), "jquery." + dir + "." + name, "/misc/jquery/" + dir + "/jquery." + name + ".min.js");
		}
	}
	#endregion
	#region IIncludesJs
	public interface IIncludesJs
	{
		void IncludeJsInternal();
	}
	#endregion


	#region DsiUserControl
	public class DsiUserControl : GenericUserControl
	{
		#region AnchorSkip
		public void AnchorSkip(string AnchorName)
		{
			ContainerPage.AnchorSkip(AnchorName);
		}
		#endregion
		#region SetPageTitle
		public void SetPageTitle(string title)
		{
			ContainerPage.SetPageTitle(title);
		}
		public void SetPageTitle(string title, string historyName)
		{
			ContainerPage.SetPageTitle(title, historyName);
		}
		#endregion
		#region ContainerPage
		public Master.DsiPage ContainerPage
		{
			get
			{
				return (Spotted.Master.DsiPage)GenericContainerPage;
			}
		}
		#endregion
	}
	#endregion
	#region BlankUserControl
	public class BlankUserControl : GenericUserControl
	{
		public BlankUserControl()
		{

		}
		#region ContainerPage
		public Master.BlankPage ContainerPage
		{
			get
			{
				return (Spotted.Master.BlankPage)GenericContainerPage;
			}
		}
		#endregion

	}
	#endregion
	#region MixmagVoteUserControl
	public class MixmagVoteUserControl : GenericUserControl
	{
		#region ContainerPage
		public Master.MixmagVotePage ContainerPage
		{
			get
			{
				return (Spotted.Master.MixmagVotePage)GenericContainerPage;
			}
		}
		#endregion
	}
	#endregion
	#region MixmagGreatestUserControl
	public class MixmagGreatestUserControl : GenericUserControl
	{
		#region ContainerPage
		public Master.MixmagGreatestPage ContainerPage
		{
			get
			{
				return (Spotted.Master.MixmagGreatestPage)GenericContainerPage;
			}
		}
		#endregion
	}
	#endregion
	#region MobileUserControl
	public class MobileUserControl : GenericUserControl
	{
		#region ContainerPage
		public Master.MobilePage ContainerPage
		{
			get
			{
				return (Spotted.Master.MobilePage)GenericContainerPage;
			}
		}
		#endregion
	}
	#endregion
	#region AdminUserControl
	public class AdminUserControl : GenericUserControl
	{

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Response.CacheControl = "no-cache";
			Response.AddHeader("Pragma", "no-cache");
			Response.Expires = -1;
			if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
			{
				JQuery.Include(Page);

#if DEBUG
				var path = @"/misc/SpottedScript/spottedscript.debug.js";
#else
				var path = @"/misc/SpottedScript/spottedscript.js";
#endif

				var scriptPath =  JsLinkCreator.GetRegisterScriptUrl(path);
				if (scriptPath.Length > 0)
					ScriptManager.GetCurrent(this.Page).Scripts.Add(new ScriptReference(scriptPath));
			}
		}
		
		#region ContainerPage
		public Master.AdminPage ContainerPage
		{
			get
			{
				return (Spotted.Master.AdminPage)GenericContainerPage;
			}
		}
		#endregion
	}
	#endregion
	#region StyledUserControl
	public class StyledUserControl : GenericUserControl
	{
		#region StyledObject
		public IStyledEventHolder StyledObject
		{
			get
			{
				return ContainerPage.StyledObject;
			}
		}
		#endregion
		#region AnchorSkip
		public void AnchorSkip(string AnchorName)
		{
			ContainerPage.AnchorSkip(AnchorName);
		}
		#endregion
		#region SetPageTitle
		public void SetPageTitle(string title)
		{
			ContainerPage.SetPageTitle(title);
		}
		#endregion
		#region ContainerPage
		public Master.StyledPage ContainerPage
		{
			get
			{
				return (Spotted.Master.StyledPage)GenericContainerPage;
			}
		}
		#endregion
	}
	#endregion

	#region GenericUserControl
	public class GenericUserControl : EnhancedUserControl
	{
		public GenericPage GenericContainerPage;
		#region DbComboOnLoad
		public void DbComboOnLoad(object o, EventArgs e)
		{
			Cambro.Web.DbCombo.DbCombo d = (Cambro.Web.DbCombo.DbCombo)o;
			d.RegistrationKey = Vars.CambroDbComboRegistrationKey;
			d.ServerDir = "/Support/";
			d.TextBoxColumns = 40;
		}
		#endregion
		#region DbComboOnLoadResizable
		public void DbComboOnLoadResizable(object o, EventArgs e)
		{
			Cambro.Web.DbCombo.DbCombo d = (Cambro.Web.DbCombo.DbCombo)o;
			d.RegistrationKey = Vars.CambroDbComboRegistrationKey;
			d.ServerDir = "/Support/";
		}
		#endregion
		#region SslPage
		public bool SslPage
		{
			get
			{
				return sslPage;
			}
			set
			{
				sslPage = value;
				if (HttpContext.Current != null)
				{
					HttpContext.Current.Items["SslPage"] = value;
				}
			}
		}
		private bool sslPage = false;
		#endregion
	}
	#endregion

	#region EnhancedUserControl
	public class MobileEnhancedUserControl : EnhancedUserControl { }
	public class EnhancedUserControl : UserControl
	{
		public EnhancedUserControl()
		{
		}
		public new IDictionary ViewState { get { return base.ViewState; } }
		public UrlInfo Url
		{
			get
			{
				return ((GenericPage)Page).Url;
			}
		}
		public ICutDownUrlInfo CutDownUrl
		{
			get
			{
				return (ICutDownUrlInfo)((GenericPage)Page).Url;
			}
		}
		public void Redirect(string url) { this.Response.Redirect(url); }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			if (ScriptSharp.HasClientScript(this, true))
				ScriptSharp.RegisterAllIncludes(this);
			
		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);

			if (ScriptSharp.HasClientScript(this, true))
				ScriptSharp.RenderAfterContent(this, writer);

		}

		#region Do we need PageMethods exposed to normal JavaScript? If so, un-comment this:
		//protected override void OnPreRender(EventArgs e)
		//{
		//    base.OnPreRender(e);
		//    RegisterClientMethods();
		//}

		//protected void RegisterClientMethods()
		//{
		//    foreach (MethodInfo method in this.GetType().GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod))
		//        if (method.GetCustomAttributes(typeof(ClientAttribute), true).Length > 0)
		//            RegisterClientMethod(method);

		//    Type baseType = this.GetType().BaseType;
		//    if (baseType != null && (baseType.Namespace == null || !baseType.Namespace.StartsWith("System")))
		//        foreach (MethodInfo method in baseType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod))
		//            if (method.GetCustomAttributes(typeof(ClientAttribute), true).Length > 0)
		//                RegisterClientMethod(method);

		//}
		//private void RegisterClientMethod(MethodInfo method)
		//{
		//    string blockName = string.Concat(method.Name, "_webMethod_uc");

		//    StringBuilder funcBuilder = new StringBuilder();
		//    funcBuilder.Append("function ");
		//    funcBuilder.Append(method.Name);
		//    funcBuilder.Append("(");
		//    foreach (var par in method.GetParameters())
		//        funcBuilder.AppendFormat("{0},", par.Name);
		//    funcBuilder.Append("successCallback,failureCallback){if(PageMethods.ClientRequest){try{var parms=[];for(var i=0;i<arguments.length-2;i++){parms.push(arguments[i]);}PageMethods.ClientRequest(");
		//    funcBuilder.AppendFormat("'{0}','{1}'", method.DeclaringType.AssemblyQualifiedName, method.Name);
		//    funcBuilder.Append(",parms,successCallback,failureCallback);}catch(e){failureCallback.call(null, e.toString());}}}");

		//    ScriptManager.RegisterClientScriptBlock(this, GetType(), blockName, funcBuilder.ToString(), true);
		//}
		#endregion

	}
	#endregion
	#region EnhancedHtmlControl
	public class MobileEnhancedHtmlControl : EnhancedHtmlControl { }
	public class EnhancedHtmlControl : HtmlControl
	{
		public EnhancedHtmlControl()
		{
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			if (ScriptSharp.HasClientScript(this, true))
				ScriptSharp.RegisterAllIncludes(this);

		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);

			if (ScriptSharp.HasClientScript(this, true))
				ScriptSharp.RenderAfterContent(this, writer);

		}

	}
	#endregion
	#region EnhancedWebControl
	public class MobileEnhancedWebControl : EnhancedWebControl { }
	public class EnhancedWebControl : WebControl
	{
		public EnhancedWebControl(HtmlTextWriterTag t)
			: base(t)
		{
		}
		public EnhancedWebControl()
		{
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			if (ScriptSharp.HasClientScript(this, true))
				ScriptSharp.RegisterAllIncludes(this);

		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);

			//All the EnhancedWebControls do their own wiring up.
			//if (ScriptSharp.HasClientScript(this, true))
			//	ScriptSharp.RenderAfterContent(this, writer);

		}

	}
	#endregion


	#region Main15Script
	public static class Main15Script
	{
		public static string Register
		{
			get
			{
				var sb = new System.Text.StringBuilder(
					"<SCRIPT language=\"JavaScript\" src=\"/Misc/main15.js?a=17\" type=\"text/javascript\"></SCRIPT><SCRIPT>");

				foreach (var d in DonationIcon.GetAllDonationIcons())
				{
					sb.Append(string.Format("var donationIcon{2}Path = '{0}'; var donationIcon{2}Name = '{1}'; ",
						d.IconPath, d.IconText, d.K));
				}

				sb.Append("</SCRIPT>");
				return sb.ToString();
			}
		}
	}
	#endregion

	#region DbComboQueries
	public class DbComboQueries
	{


		#region Groups
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetGroupsNoBrands(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " [Group].[UrlName] as DbComboText, [Group].[K] as DbComboValue from [Group] where [Group].[UrlName] like '%" + Db.PStr(args.Query) + "%' AND (BrandK=0 OR BrandK IS NULL) AND PrivateGroupPage=0 AND PrivateChat=0 AND PrivateMemberList=0 order by [Group].[UrlName]", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region Groups
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetGroups(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " [Group].[UrlName] as DbComboText, [Group].[K] as DbComboValue from [Group] where [Group].[UrlName] like '%" + Db.PStr(args.Query) + "%' AND PrivateGroupPage=0 AND PrivateChat=0 AND PrivateMemberList=0 order by [Group].[UrlName]", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region UsrsPublic
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetUsrsPublic(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " NickName as DbComboText, K as DbComboValue from Usr where NickName like '%" + Db.PStr(args.Query) + "%' and not NickName = '' and not FirstName = '' and not LastName = '' and IsEmailVerified=1 order by NickName", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region Usrs
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetUsrs(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			if (!Usr.Current.IsAdmin)
				throw new Exception("Only admin");

			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " NickName as DbComboText, K as DbComboValue from Usr where not NickName='' and NickName like '%" + Db.PStr(args.Query) + "%' order by NickName", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region MultiUsrs
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetMultiUsrs(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			string extraWhereClause = "";
			string extraFromClause = "";
			if (
				args.ServerState != null &&
				args.ServerState.Authenticate("fgdfjshgofdshdf$£%%$FBskjhgjklsd") &&
				args.ServerState["BuddiesUsrK"] != null &&
				args.ServerState["BuddiesUsrK"].ToString().Length > 0
				)
			{
				int BuddiesUsrK = int.Parse(args.ServerState["BuddiesUsrK"].ToString());
				if (BuddiesUsrK > 0)
				{
					if (Usr.Current.K != BuddiesUsrK && !Usr.Current.IsAdmin)
						throw new Exception("Wrong Usr");
					extraWhereClause = " AND FullBuddy=1 AND Buddy.UsrK = " + BuddiesUsrK.ToString() + " ";
					extraFromClause = " INNER JOIN Buddy ON Usr.K = Buddy.BuddyUsrK ";
				}
			}

			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " NickName as DbComboText, (CAST(K as VarChar))+'$'+(CASE WHEN Pic IS NULL THEN '' ELSE CONVERT(varchar(255), Pic) END) as DbComboValue from Usr " + extraFromClause + " where NickName like '%" + Db.PStr(args.Query) + "%' AND NOT NickName='' " + extraWhereClause + " order by NickName", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region DbComboGetGroupMembers
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetGroupMembers(Cambro.Web.DbCombo.ServerMethodArgs args)
		{

			string extraWhereClause = "";
			string extraFromClause = "";
			if (
				args.ServerState != null &&
				args.ServerState.Authenticate("fgdfjshgofdshdf$£%%$FBskjhgjklsd") &&
				args.ServerState["GroupK"] != null &&
				args.ServerState["GroupK"].ToString().Length > 0
				)
			{
				int GroupK = int.Parse(args.ServerState["GroupK"].ToString());
				if (GroupK > 0)
				{
					extraWhereClause = " AND GroupUsr.GroupK=" + GroupK.ToString() + " AND GroupUsr.Status = 1 ";
					extraFromClause = " INNER JOIN GroupUsr ON Usr.K = GroupUsr.UsrK ";
				}
			}

			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " NickName as DbComboText, K as DbComboValue from Usr " + extraFromClause + " where NickName like '%" + Db.PStr(args.Query) + "%' AND NOT NickName='' " + extraWhereClause + " order by NickName", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region Articles
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetArticles(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			if (!Usr.Current.IsAdmin)
				throw new Exception("Only admin!");

			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " Title+' by '+NickName as DbComboText, Article.K as DbComboValue from Article inner Join Usr on Article.ownerUsrK=Usr.K where Title+' '+NickName like '%" + Db.PStr(args.Query) + "%' order by Title", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region Places
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetPlaces(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " ISNULL(CAST ([place].[Name] AS VARCHAR) COLLATE SQL_Latin1_General_CP1_CI_AS,'') + ' (' + ISNULL(CAST ([place].[Type] AS VARCHAR) COLLATE SQL_Latin1_General_CP1_CI_AS,'') + ') ' +  ISNULL(CAST ([place].[RegionAbbreviation] AS VARCHAR) COLLATE SQL_Latin1_General_CP1_CI_AS,'') + ' ' + ISNULL(CAST ([Country].[Name] AS VARCHAR) COLLATE SQL_Latin1_General_CP1_CI_AS,'') as DbComboText, place.K as DbComboValue from Place inner join Country on place.countryk = country.k where place.Name like '%" + Db.PStr(args.Query) + "%' order by place.Name", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region PlacesEnabled
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetPlacesEnabled(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			bool Int = false;
			if (
				args.ServerState != null &&
				args.ServerState["Int"] != null &&
				args.ServerState["Int"].ToString().Equals("1")
				)
			{
				Int = true;
			}

			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = null;

				if (Int)
					myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " ISNULL(CAST ([place].[Name] AS VARCHAR) COLLATE SQL_Latin1_General_CP1_CI_AS,'') + ' ' +  ISNULL(CAST ([place].[RegionAbbreviation] AS VARCHAR) COLLATE SQL_Latin1_General_CP1_CI_AS,'') + (case when [place].[RegionAbbreviation]='' then '' else ' ' end) +'('+ ISNULL(CAST ([Country].[FriendlyName] AS VARCHAR) COLLATE SQL_Latin1_General_CP1_CI_AS,'') + ')' as DbComboText, place.K as DbComboValue from Place inner join Country on place.countryk = country.k where place.Name like '%" + Db.PStr(args.Query) + "%' AND Place.Enabled=1 order by place.Name", conn);
				else
					myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " ISNULL(CAST ([place].[Name] AS VARCHAR) COLLATE SQL_Latin1_General_CP1_CI_AS,'') as DbComboText, place.K as DbComboValue from Place inner join Country on place.countryk = country.k where place.Name like '%" + Db.PStr(args.Query) + "%' AND Place.Enabled=1 AND " + Country.PlaceFilterSqlString + " order by place.Name", conn);

				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region DbComboGetEvents
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetEvents(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " Event.Name+' @ '+Venue.Name+' in '+Place.Name+', '+CONVERT(VarChar,Event.DateTime,3) as DbComboText, Event.K as DbComboValue from Event INNER JOIN Venue ON Event.VenueK = Venue.K INNER JOIN Place ON Venue.PlaceK=Place.K where Event.Name+' @ '+Venue.Name+' in '+Place.Name+', '+CONVERT(VarChar,Event.DateTime,3) like '%" + Db.PStr(args.Query) + "%' order by Event.Name, Venue.Name, Event.DateTime DESC", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;

		}
		#endregion

		#region Venues
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetVenues(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " Venue.Name+' in '+Place.Name as DbComboText, Venue.K as DbComboValue from Venue INNER JOIN Place ON Venue.PlaceK=Place.K where Venue.Name+' in '+Place.Name like '%" + Db.PStr(args.Query) + "%' order by Venue.Name", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region VenuesNotDisabled
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetVenuesNotDisabled(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " Venue.Name+' in '+Place.Name as DbComboText, Venue.K as DbComboValue from Venue INNER JOIN Place ON Venue.PlaceK=Place.K where Venue.Name+' in '+Place.Name like '%" + Db.PStr(args.Query) + "%' order by Venue.Name", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region VenuesNotDisabledCountryFiltered
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetVenuesNotDisabledCountryFiltered(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " Venue.Name+' in '+Place.Name as DbComboText, Venue.K as DbComboValue from Venue INNER JOIN Place ON Venue.PlaceK=Place.K where Venue.Name+' in '+Place.Name like '%" + Db.PStr(args.Query) + "%' AND " + Country.PlaceFilterSqlString + " order by Venue.Name", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region Brands

		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetBrandUrls(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				string extraSql = "";
				if (args.ServerState != null && args.ServerState["NotBrandK"] != null)
				{
					int brandK = (int)args.ServerState["NotBrandK"];
					extraSql = " AND NOT Brand.K=" + brandK + " ";
				}

				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " UrlName as DbComboText, K as DbComboValue from Brand where UrlName like '%" + Db.PStr(args.Query) + "%' " + extraSql + " order by UrlName", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}


		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetBrands(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				string extraSql = "";
				if (args.ServerState != null && args.ServerState["NotBrandK"] != null)
				{
					int brandK = (int)args.ServerState["NotBrandK"];
					extraSql = " AND NOT Brand.K=" + brandK + " ";
				}

				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " Name as DbComboText, K as DbComboValue from Brand where Name like '%" + Db.PStr(args.Query) + "%' " + extraSql + " order by Name", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}

		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetBrandsDetail(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				string extraSql = "";
				if (args.ServerState != null && args.ServerState["NotBrandK"] != null)
				{
					int brandK = (int)args.ServerState["NotBrandK"];
					extraSql = " AND NOT Brand.K=" + brandK + " ";
				}
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " Brand.Name+' (#'+CAST(Brand.K as varchar)+')' + ISNULL(CAST (' ('+Promoter.Name+')' AS VARCHAR) COLLATE SQL_Latin1_General_CP1_CI_AS,'') as DbComboText, Brand.K as DbComboValue from Brand LEFT JOIN Promoter ON Brand.PromoterK = Promoter.K where Brand.Name like '%" + Db.PStr(args.Query) + "%' " + extraSql + " order by Brand.Name", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
		#region Promoters
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetPromoters(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			if (!Usr.Current.IsAdmin)
				throw new Exception("Only admin");
			DataView dv;
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " Name as DbComboText, K as DbComboValue from Promoter where Name like '%" + Db.PStr(args.Query) + "%' order by Name", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}

		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboGetPromotersWithK(Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			if (!Usr.Current.IsAdmin)
				throw new Exception("Only admin");
			DataView dv;
			string query = args.Query;
			if (args.Query.Contains(" (K="))
				query = args.Query.Substring(0, args.Query.LastIndexOf(" (K="));
			SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
			try
			{
				SqlCommand myCommand = new SqlCommand("select top " + Db.PNum(args.Top) + " Name + ' (K=' + CONVERT(varchar(20),K) + ')' + CASE WHEN Status = 4 THEN ' *disabled' ELSE '' END as DbComboText, K as DbComboValue from Promoter where Name like '%" + Db.PStr(query) + "%' order by Name", conn);
				DataSet dataset = new DataSet();
				SqlDataAdapter adapter = new SqlDataAdapter();
				adapter.SelectCommand = myCommand;
				Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				adapter.Fill(dataset);
				dv = dataset.Tables[0].DefaultView;
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
			return dv;
		}
		#endregion
	}
	#endregion
	
}
