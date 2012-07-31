using System;
using System.Web;
using System.Web.Security;

namespace Facebook.Web
{
    public class FacebookHttpModule : IHttpModule
    {
        /// <summary>Occurs when the <see cref="FacebookHttpContext" /> is first initialized for a user session.</summary>
        /// <remarks><para>Note: <see cref="FacebookHttpContext" /> objects are serialized and re-used between requests for each user session,
        /// so initialization should only occur once when the session starts.</para></remarks>
        public event FacebookWebEventHandler FacebookContextInit;

        /// <summary>Sets the handlers for the <see cref="HttpApplication.AcquireRequestState" /> and <see cref="FacebookContextInit" /> events.</summary>
        public virtual void Init(HttpApplication application)
        {
            this.Application = application;
            application.AcquireRequestState += new EventHandler(FacebookApplication_AcquireRequestState);
            this.FacebookContextInit += new FacebookWebEventHandler(FacebookApplication_FacebookContextInit);
        }

        /// <summary>Gets a reference to the <see cref="HttpApplication" /> that initialized this module.</summary>
        public HttpApplication Application { get; private set; }

        /// <summary>Causes initialization of all Facebook web-specific objects and forces Forms-based authentication based on the Facebook session.</summary>
        /// <remarks>
        /// <para>Initalizes the <see cref="FacebookHttpRequest" />, <see cref="FacebookHttpSession" /> and <see cref="FacebookHttpContext" /> objects for the current request.</para>
        /// <para>If an authenticated Facebook session is detected, the ASP.NET session is marked as authenticated using <see cref="FormsAuthentication.SetAuthCookie(String, Boolean)" />.
        /// The <see cref="String" /> form of the user's Facebook UID is passed as the user name.</para>
        /// </remarks>
        void FacebookApplication_AcquireRequestState(Object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            context.Response.AddHeader("P3P", "CP=\"CAO PSA OUR\"");

            context.Items["__FACEBOOK_HTTP_MODULE"] = this;

            if (context.Handler.GetType() != typeof(DefaultHttpHandler))
            {
                FacebookHttpContext.Init(context);

                if (FacebookHttpContext.Current.HasSession)
                {
                    FormsAuthentication.SetAuthCookie(FacebookHttpSession.Current.Uid.ToString(), FacebookHttpSession.Current.Expires == DateTime.MaxValue);
                }
                else
                {
                    FormsAuthentication.SignOut();
                }
            }
        }

        private static Object initFeedTemplatesLock = new Object();
        /// <summary>Causes any configured Feed templates to be initialized, and if needed, registered.</summary>
        /// <remarks><para>While not an entirely optimal solution, this functionality is executed when the context is initialized because it
        /// requires knowledge of which API key is being used. This information isn't available until after the Facebook web objects are initialized.
        /// A value is set per-API key in the Application state so that it only executed once per API key during the lifetime of the appication.</para></remarks>
        void FacebookApplication_FacebookContextInit(Object sender, FacebookWebEventArgs e)
        {
            FacebookWebApplication.Init(FacebookHttpContext.Current);
            if (this.Application.Application["__FB_FEED_INIT_" + e.Context.ApiKey] == null)
            {
                lock (initFeedTemplatesLock)
                {
                    if (this.Application.Application["__FB_FEED_INIT_" + e.Context.ApiKey] == null)
                    {
						//if (Facebook.Configuration.FacebookSection.IsConfigured)
						//{
						//    var config = Facebook.Configuration.FacebookSection.Current;
						//    if (!String.IsNullOrEmpty(Facebook.Configuration.FacebookSection.Current.FeedTemplateConfigSource))
						//    {
						//        var templates = Facebook.Configuration.FeedTemplateBundles.Get(); ;
						//        if (templates.Count > 0)
						//        {
						//            e.Context.Feed.InitFeedTemplates();
						//        }
						//    }
						//}
                    }
                }
                this.Application.Application["__FB_FEED_INIT_" + e.Context.ApiKey] = true;
            }
        }

        /// <summary>Raises the <see cref="FacebookContextInit" /> event of the <see cref="FacebookHttpApplication" /> object.</summary>
        /// <param name="e">A <see cref="FacebookWebEventArgs" /> object that contains data about the event.</param>
        protected internal void OnFacebookContextInit(FacebookWebEventArgs e)
        {
            if (this.FacebookContextInit != null)
            {
                this.FacebookContextInit(this, e);
            }
        }

        void IHttpModule.Dispose() { }
    }
}
