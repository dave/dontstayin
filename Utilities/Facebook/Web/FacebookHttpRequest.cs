using System;
using System.Web;
using Facebook.Json;
using System.Xml.Linq;
using Facebook;
using Facebook.Api;

namespace Facebook.Web
{
    /// <summary>Enables ASP.NET to read the Facebook-specific HTTP values sent by a client and the Facebook API during a Facebook Web request.</summary>
    [Serializable]
    public class FacebookHttpRequest
    {
        /// <summary>Initializes an instance of <see cref="FacebookHttpRequest" />.</summary>
        public FacebookHttpRequest() { }

        /// <summary>Initializes an instance of <see cref="FacebookHttpRequest" /> using data from the specified <see cref="HttpContext" /> object.</summary>
        /// <param name="context">A reference to the <see cref="HttpContext" /> object for the current HTTP request.</param>
		public FacebookHttpRequest(HttpContext context, Int64 overrideUid)
        {
			this.Merge(context, overrideUid);
        }

        private const String KEY_FBREADY = "__FBREADY";
        /// <summary>Gets a value representing whether all Facebook HTTP objects have been initialized for the current HTTP request.</summary>
        public static Boolean IsReady
        {
            get { return (Boolean)(HttpContext.Current.Items[KEY_FBREADY] ?? false); }
            internal set { HttpContext.Current.Items[KEY_FBREADY] = value; }
        }

        /// <summary>A static initializer for <see cref="FacebookHttpRequest" />.</summary>
        /// <param name="context">A reference to the <see cref="HttpContext" /> object for the current HTTP request.</param>
		public static void Init(HttpContext context, Int64 overrideUid)
        {
            if (FacebookHttpRequest.Current == null)
				FacebookHttpRequest.Current = new FacebookHttpRequest(context, overrideUid);
            else
				FacebookHttpRequest.Current.Merge(context, overrideUid);
        }

        private const String KEY_FBREQUEST = "__FBREQUEST";
        /// <summary>Gets a reference to the <see cref="FacebookHttpRequest" /> object for the current HTTP request.</summary>
        /// <remarks><para>In order to provide stable functionality across both FBML and IFrame applications, the data found in
        /// <see cref="FacebookHttpRequest" /> is stored in several places.</para>
        /// <para>First, it is stored in <see cref="HttpContext.Items" />, which is scoped at the request-level. This provides functionality
        /// for the duration of the request, and for FBML applications, is the only place the data is required to be stored.</para>
        /// <para>The data is also stored in a session variable, which allows IFrame applications to access the data between requests without requiring
        /// the information to be manually appended to the querystring on each link and redirect.</para>
        /// <para>Finally, the information is also serialized into an HTTP cookie, which allows the information to be retrieved in the event of an application
        /// recycle event during the course of a user's session.</para>
        /// </remarks>
        public static FacebookHttpRequest Current
        {
            get
            {
				if (HttpContext.Current.Session != null)
				{
					if (HttpContext.Current.Items[KEY_FBREQUEST] == null && HttpContext.Current.Session[KEY_FBREQUEST] != null)
					{
						HttpContext.Current.Items[KEY_FBREQUEST] = HttpContext.Current.Session[KEY_FBREQUEST];
					}
					else if (HttpContext.Current.Items[KEY_FBREQUEST] == null && HttpContext.Current.Request.Cookies[KEY_FBREQUEST] != null)
					{
						var request = JsonConvert.DeserializeObject<FacebookHttpRequest>(HttpContext.Current.Request.Cookies[KEY_FBREQUEST].Value);
						HttpContext.Current.Session[KEY_FBREQUEST] = request;
						HttpContext.Current.Items[KEY_FBREQUEST] = request;
					}
				}
				else if (HttpContext.Current.Items[KEY_FBREQUEST] == null && HttpContext.Current.Request.Cookies[KEY_FBREQUEST] != null)
				{
					var request = JsonConvert.DeserializeObject<FacebookHttpRequest>(HttpContext.Current.Request.Cookies[KEY_FBREQUEST].Value);
					HttpContext.Current.Items[KEY_FBREQUEST] = request;
				}
                return (FacebookHttpRequest)HttpContext.Current.Items[KEY_FBREQUEST];
            }
            internal set
            {
                HttpContext.Current.Items[KEY_FBREQUEST] = value;
                HttpContext.Current.Session[KEY_FBREQUEST] = value;
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(KEY_FBREQUEST, JsonConvert.SerializeObject(value)) { Expires = DateTime.Now.AddMinutes(30) });
            }
        }

        /// <summary>Copies data from the specified <see cref="HttpContext" /> object into the <see cref="FacebookHttpRequest" /> object.</summary>
        /// <param name="context">A reference to the <see cref="HttpContext" /> object for the current HTTP request.</param>
        /// <remarks><para>This method allows new data to be updated in the <see cref="FacebookHttpRequest" /> object without overwriting existing
        /// values that do not exist in <paramref name="context" />.</para></remarks>
		public void Merge(HttpContext context, Int64 overrideUid)
        {
            Byte inCanvas;
            if (Byte.TryParse(context.Request.Params["fb_sig_in_canvas"], out inCanvas)) this.InCanvas = inCanvas == 1;

            Byte inIFrame;
            if (Byte.TryParse(context.Request.Params["fb_sig_in_iframe"], out inIFrame)) this.InIFrame = inIFrame == 1;

            Byte inProfileTab;
            if (Byte.TryParse(context.Request.Params["fb_sig_in_profile_tab"], out inProfileTab)) this.InProfileTab = inProfileTab == 1;

            Int64 profileUserId;
            if (Int64.TryParse(context.Request.Params["fb_sig_profile_user"], out profileUserId)) this.ProfileUserId = profileUserId;

            String profileSessionKey = context.Request.Params["fb_sig_profile_session_key"];
            if (!String.IsNullOrEmpty(profileSessionKey)) this.ProfileSessionKey = profileSessionKey;

            Int64 pageId;
            if (Int64.TryParse(context.Request.Params["fb_sig_page_id"], out pageId)) this.PageId = pageId;

            Byte isPageAdded;
            if (Byte.TryParse(context.Request.Params["fb_sig_page_added"], out isPageAdded)) this.IsPageAdded = isPageAdded == 1;

            Byte isLoggedOutOfFacebook;
            if (Byte.TryParse(context.Request.Params["fb_sig_logged_out_facebook"], out isLoggedOutOfFacebook)) this.IsLoggedOutOfFacebook = isLoggedOutOfFacebook == 1;

            Int64 uid;

			if (overrideUid > 0)
			{
				uid = overrideUid;
			}
			else
			{
				if (!Int64.TryParse(context.Request.Params["fb_sig_user"], out uid))
				{
					if (!Int64.TryParse(context.Request.Params["fb_sig_canvas_user"], out uid))
					{
						Int64.TryParse(context.Request.Params["fb_sig_profile_user"], out uid);
					}
				}
			}
            if (uid > 0)
				this.Uid = uid;
        }

        /// <summary>Gets a <see cref="Boolean" /> value representing whether the current HTTP request came from the Facebook Canvas.</summary>
        public Boolean InCanvas { get; private set; }

        /// <summary>Gets a <see cref="Boolean" /> value representing whether the current HTTP request is in a Facebook IFrame.</summary>
        public Boolean InIFrame { get; private set; }

        /// <summary>Gets a <see cref="Boolean" /> value representing whether the current HTTP request came from a Profile Tab.</summary>
        public Boolean InProfileTab { get; private set; }

        /// <summary>Gets the <c>UID</c> of the Facebook user that is the owner of the profile for requests coming from a Profile Tab.</summary>
        public Int64 ProfileUserId { get; private set; }

        /// <summary>Gets the session key for the profile owner which must be used to render the user's profile tab content.</summary>
        public String ProfileSessionKey { get; private set; }

        /// <summary>Gets the id of the Page if the HTTP request is on the behalf of a Page.</summary>
        public Int64 PageId { get; private set; }

        /// <summary>Gets a <see cref="Boolean" /> value representing whether the Page has added the current application if the HTTP request is on the behalf of a Page.</summary>
        public Boolean IsPageAdded { get; private set; }

        /// <summary>Gets a <see cref="Boolean" /> value representing whether the current user is not logged into Facebook.</summary>
        public Boolean IsLoggedOutOfFacebook { get; private set; }

        /// <summary>Gets the <c>UID</c> of the user making the request.</summary>
        /// <remarks><para>This value is the coalescence of the <c>fb_sig_user</c>, <c>fb_sig_canvas_user</c> and <c>fb_sig_profile_user</c> parameters, in that order.</para></remarks>
        public Int64 Uid { get; private set; }

		/// <summary>
		/// Added by DaveB
		/// </summary>
		/// <param name="uid"></param>
		public void SetUid(Int64 uid)
		{
			Uid = uid;
		}

        // TODO: Add XML Version, check for no fqlParam

        public TValue GetPreloadFql<TValue>() where TValue : class, IFacebookObject
        {
			String fqlParam = "";// Facebook.Configuration.FacebookSection.Current.Applications.GetCurrent().PreloadFqlParam;
            return JsonConvert.DeserializeObject<TValue>(HttpContext.Current.Request.Params[String.Format("fb_post_sig_{0}", fqlParam)]);
        }


        public String GetPreloadFqlJSON()
        {
			String fqlParam = "";// Facebook.Configuration.FacebookSection.Current.Applications.GetCurrent().PreloadFqlParam;
            return HttpContext.Current.Request.Params[String.Format("fb_post_sig_{0}", fqlParam)];
        }
    }
}
