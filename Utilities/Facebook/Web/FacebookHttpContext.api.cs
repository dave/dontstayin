using System;
using System.Web;
using Facebook.Api;
//using Facebook.Web.Configuration;

namespace Facebook.Web
{
    /// <summary>A web-specific implementation of <see cref="FacebookContextBase{TCache, TSession}" /> that encapsulates all Facebook-specific information
    /// about an individual HTTP request for a Facebook web application.</summary>
    [Serializable]
    public partial class FacebookHttpContext
    {
        /// <summary>Initializes the Facebook web objects.</summary>
        /// <param name="context">A reference to the <see cref="HttpContext" /> object for the current HTTP request.</param>
		public static void Init(HttpContext context, string appName)
        {
            if (!FacebookHttpRequest.IsReady)
            {
                FacebookHttpRequest.Init(context, 0);
				FacebookHttpSession.Init(context);
                FacebookHttpContext.InitContext(appName);
                FacebookHttpRequest.IsReady = true;
            }
        }

		/// <summary>
		/// Added by DaveB
		/// </summary>
		/// <param name="context"></param>
		/// <param name="session"></param>
		public static void Init(HttpContext context, System.Collections.Hashtable session, string appName)
		{
			Init(context, session["uid"].ToString(), session["session_key"].ToString(), session["secret"].ToString(), session["expires"].ToString(), session["base_domain"].ToString(), appName);
		}

		/// <summary>
		/// Added by DaveB
		/// </summary>
		/// <param name="context"></param>
		/// <param name="uid"></param>
		/// <param name="sessionKey"></param>
		/// <param name="secret"></param>
		/// <param name="expires"></param>
		/// <param name="baseDomain"></param>
		public static void Init(HttpContext context, string uid, string sessionKey, string secret, string expires, string baseDomain, string appName)
		{
			if (!FacebookHttpRequest.IsReady)
			{

				FacebookHttpRequest.Init(context, Int64.Parse(uid));

				FacebookHttpSession.Init(uid, sessionKey, secret, expires, baseDomain);
				FacebookHttpContext.InitContext(appName);
				FacebookHttpRequest.IsReady = true;
			}
		}

        private static void InitContext(string name)
        {
            if (FacebookHttpSession.Current["__FBCONTEXT"] == null)
            {
                FacebookHttpContext context = null;
                try
                {
					context = new FacebookHttpContext(FacebookCommon.Common(name).ApiKey, FacebookCommon.Common(name).Secret);
                    FacebookHttpSession.Current["__FBCONTEXT"] = context;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("An attempt to initialize FacebookHttpContext with the supplied api key, but no application configurations were found.", ex);
                }
                if (HttpContext.Current.Items["__FACEBOOK_HTTP_MODULE"] != null)
                {
                    ((FacebookHttpModule)HttpContext.Current.Items["__FACEBOOK_HTTP_MODULE"]).OnFacebookContextInit(new FacebookWebEventArgs(FacebookHttpRequest.Current, FacebookHttpSession.Current, context));
                }
            }
        }

        /// <summary>Gets a reference to the <see cref="FacebookHttpContext" /> object for the current HTTP request.</summary>
        public static FacebookHttpContext Current
        {
            get
            {
                FacebookHttpContext.InitContext();
                return (FacebookHttpContext)FacebookHttpSession.Current["__FBCONTEXT"];
            }
            internal set { FacebookHttpSession.Current["__FBCONTEXT"] = value; }
        }

        /// <summary>Gets a reference to the <see cref="FacebookHttpSession" /> object for the current HTTP request.</summary>
        public override FacebookHttpSession Session 
        {
            get { return FacebookHttpSession.Current; }
            protected internal set { FacebookHttpSession.Current = value; }
        }

        public FacebookWebApplication ApplicationInfo { get { return FacebookWebApplication.Get(this); } }

        /// <summary>Gets a reference to the <see cref="FacebookHttpRequest" /> object for the current HTTP request.</summary>
        public FacebookHttpRequest Request { get { return FacebookHttpRequest.Current; } }

        /// <summary>Generates a url that a user can be redirected to in order to authenticate for the current Facebook application.</summary>
        /// <param name="next">An optional url to redirect to after authentication.</param>
        /// <returns>A url that a user can be redirected to in order to authenticate for the current Facebook application.</returns>
        public String GetAuthorizationUrl(String next)
        {
            return String.Format("http://www.facebook.com/login.php?v={0}&api_key={1}&next={2}&canvas=", this.Version, this.ApiKey, String.IsNullOrEmpty(next) ? String.Empty : HttpUtility.UrlEncode(next));
        }
    }
}
