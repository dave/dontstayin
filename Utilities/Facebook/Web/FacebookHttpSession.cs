using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook.Api;
using System.Collections;
using System.Collections.Specialized;

namespace Facebook.Web
{
    /// <summary>Provides access to session-state values as well as session-level data about the HTTP request.</summary>
    /// <remarks>The <see cref="FacebookHttpSession" /> serves the same purpose for web applications as <see cref="SessionInfo" /> does for
    /// desktop applications, and it can also be used to manage session state for FBML web applications. For FBML web applications,
    /// using the customary <see cref="System.Web.SessionState.HttpSessionState" /> object is not an option since the ASP.NET session will not be persisted between
    /// requests.</remarks>
    [Serializable]
    public class FacebookHttpSession : SessionInfo, ISessionInfo, IDictionary<String, Object>
    {
        /// <summary>Initializes an instance of <see cref="FacebookHttpRequest" /> using data from the specified <see cref="HttpContext" /> object.</summary>
        /// <param name="context">A reference to the <see cref="HttpContext" /> object for the current HTTP request.</param>
        public FacebookHttpSession(HttpContext context)
        {
            this.Merge(context);
        }

		public FacebookHttpSession(SessionInfo sessionInfo)
		{
			this.Init(sessionInfo);
		}

        /// <summary>Initializes an instance of <see cref="FacebookHttpRequest" /> using data from the specified <see cref="IDictionary{String, Object}" /> object.</summary>
        /// <remarks><para>This constructor supports <see cref="IFacebookObject" />.</para></remarks>
        protected internal FacebookHttpSession(IDictionary<String, Object> dictionary)
            : base(dictionary) { }

        /// <summary>Gets a user-specific key used to store and retrieve <see cref="FacebookHttpSession" /> instances.</summary>
        private static String SessionCacheKey
        {
            get { return String.Format("__FBAPI__SESSION__{0}", FacebookHttpRequest.Current.Uid); }
        }

        /// <summary>Gets a reference to the <see cref="FacebookHttpSession" /> object for the current HTTP request.</summary>
        public static FacebookHttpSession Current
        {
            get
			{
				return (FacebookHttpSession)FacebookHttpContext.SessionCache[FacebookHttpSession.SessionCacheKey];
			}
            protected internal set
			{
				FacebookHttpContext.SessionCache[FacebookHttpSession.SessionCacheKey] = value;
			}
        }

        /// <summary>A static initializer for <see cref="FacebookHttpSession" />.</summary>
        /// <param name="context">A reference to the <see cref="HttpContext" /> object for the current HTTP request.</param>
        public static void Init(HttpContext context)
        {
			if (FacebookHttpSession.Current == null)
			{
				FacebookHttpSession.Current = new FacebookHttpSession(context);
			}
			else
			{
				FacebookHttpSession.Current.Merge(context);
			}
        }

		/// <summary>
		/// Added by DaveB?
		/// </summary>
		/// <param name="uid"></param>
		/// <param name="sessionKey"></param>
		/// <param name="secret"></param>
		/// <param name="expires"></param>
		/// <param name="baseDomain"></param>
		public static void Init(string uid, string sessionKey, string secret, string expires, string baseDomain)
		{

			SessionInfo sessionInfo = new SessionInfo();
			sessionInfo.BaseDomain = baseDomain;

			if (expires == "0")
			{
				sessionInfo.Expires = DateTime.MaxValue;
			}
			else
			{
				UnixDateTime expiresUnixDateTime;
				UnixDateTime.TryParse(expires, out expiresUnixDateTime);
				sessionInfo.Expires = expiresUnixDateTime.ToDateTime();
			}

            sessionInfo.Secret = secret;
            sessionInfo.SessionKey = sessionKey;
            sessionInfo.Uid = long.Parse(uid);

			if (FacebookHttpSession.Current == null)
			{
				FacebookHttpSession.Current = new FacebookHttpSession(sessionInfo);
			}
			else
			{
				FacebookHttpSession.Current.Init(sessionInfo);
			}

		}
		public void Init(SessionInfo sessionInfo)
		{
			this.Uid = sessionInfo.Uid;
			this.SessionKey = sessionInfo.SessionKey;
			this.Secret = sessionInfo.Secret;
			this.BaseDomain = sessionInfo.BaseDomain;
			this.Expires = sessionInfo.Expires;
		}

        /// <summary>Copies data from the specified <see cref="HttpContext" /> object into the <see cref="FacebookHttpSession" /> object.</summary>
        /// <param name="context">A reference to the <see cref="HttpContext" /> object for the current HTTP request.</param>
        /// <remarks><para>This method allows new data to be updated in the <see cref="FacebookHttpSession" /> object without overwriting existing
        /// values that do not exist in <paramref name="context" />.</para></remarks>
        public void Merge(HttpContext context)
        {
            Byte added;
            if (Byte.TryParse(context.Request.Params["fb_sig_added"], out added))
				this.Added = added == 1;

            UnixDateTime expires;
            if (context.Request.Params["fb_sig_expires"] == "0")
				this.Expires = DateTime.MaxValue;
            else if (UnixDateTime.TryParse(context.Request.Params["fb_sig_expires"], out expires))
				this.Expires = expires.ToDateTime();

            String apiKey = context.Request.Params["fb_sig_api_key"];
            if (!String.IsNullOrEmpty(apiKey))
				this.ApiKey = apiKey;

            String sessionKey = context.Request.Params["fb_sig_session_key"];
            if (!String.IsNullOrEmpty(sessionKey))
				this.SessionKey = sessionKey;

            String strFriends = context.Request.Params["fb_sig_friends"];
            if (!String.IsNullOrEmpty(strFriends))
				this.Friends = new List<Int64>(from strFriend in strFriends.Split(',') select Int64.Parse(strFriend));

            String locale = context.Request.Params["fb_sig_locale"];
            if (!String.IsNullOrEmpty(locale))
				this.Locale = locale;

            Int64 uid;
			if (!Int64.TryParse(context.Request.Params["fb_sig_user"], out uid))
			{
				if (!Int64.TryParse(context.Request.Params["fb_sig_canvas_user"], out uid))
					Int64.TryParse(context.Request.Params["fb_sig_profile_user"], out uid);
			}

            if (uid > 0)
				this.Uid = uid;

            String strLinkedAccounts = context.Request.Params["fg_sig_linked_account_ids"];
            if (!String.IsNullOrEmpty(strLinkedAccounts))
				this.Friends = new List<Int64>(from strLinkedAccount in strLinkedAccounts.Split(',') select Int64.Parse(strLinkedAccounts));

            UnixDateTime lastProfileUpdate;
            if (UnixDateTime.TryParse(context.Request.Params["fb_sig_profile_update_time"], out lastProfileUpdate))
				this.LastProfileUpdate = lastProfileUpdate.ToDateTime();

            String extPerms = context.Request.Params["fb_sig_ext_perms"];
            if (!String.IsNullOrEmpty(extPerms))
				this.ExtendedPermissions = new List<String>(extPerms.Split(','));

            this.MergeFbSig(context.Request.Params);
        }

        private void MergeFbSig(NameValueCollection requestParams)
        {
            if (this.FbSig == null) this.FbSig = new NameValueCollection();
            var keys = requestParams.Keys.OfType<String>().Where(key => key.StartsWith("fb_sig"));
            foreach (var key in keys)
            {
                if (String.IsNullOrEmpty(this.FbSig[key])) this.FbSig[key] = requestParams[key];
            }
        }

        /// <summary>Gets or sets a value representing whether the user for the current HTTP request has added the application.</summary>
        public Boolean Added { get; set; }

        /// <summary>Gets or sets the API key for the application accessed for the current HTTP request.</summary>
        public String ApiKey { get; set; }

        /// <summary>Gets or sets the list of friend <c>UID</c>s of the visiting user included in the current HTTP request.</summary>
        public List<Int64> Friends { get; set; }

        /// <summary>Gets or sets the user's locale.</summary>
        public String Locale { get; set; }

        /// <summary>Gets or sets a list of <c>account_id</c> values that match the user's email hash that were previously sent to Facebook using <see cref="Facebook.Api.Controllers.ConnectController.RegisterUsers" />.</summary>
        public List<Int64> LinkedAccountIds { get; set; }

        /// <summary>Gets or sets the date/time that the user's profile was last updated.</summary>
        public DateTime LastProfileUpdate { get; set; }

        /// <summary>Gets or sets a list of any extended permissions that the user has granted the application.</summary>
        public List<String> ExtendedPermissions { get; set; }

        /// <summary>Gets a <see cref="NameValueCollection" /> containing all <c>fb_sig</c> parameters for the latest request.</summary>
        public NameValueCollection FbSig { get; internal set; }

        private const String SESSION_PREFIX = "__SESSION__";
        private String GetInternalKey(String key)
        {
            return SESSION_PREFIX + key;
        }

        #region [ IDictionary<String, Object> Members ]
        /// <summary>Adds the specified <paramref name="value" /> to the session state using the specified <paramref name="key" />.</summary>
        /// <param name="key">The key used to reference the session state item.</param>
        /// <param name="value">The object to be added to the session state.</param>
        public void Add(String key, Object value)
        {
            this.InnerDictionary.Add(this.GetInternalKey(key), value);
        }

        /// <summary>Gets a <see cref="Boolean" /> value representing whether an item with the specified <paramref name="key" /> is contained in the session state.</summary>
        /// <param name="key">The key used to reference the session state item.</param>
        /// <returns><c>true</c> if the session state contains an item identified by the specified <paramref name="key"/>; otherwise, <c>false</c>.</returns>
        public Boolean ContainsKey(String key)
        {
            return this.InnerDictionary.ContainsKey(this.GetInternalKey(key));
        }

        /// <summary>Gets a collection of keys contained in the session state.</summary>
        public ICollection<String> Keys
        {
            get { return this.InnerDictionary.Keys.Where(key => key.StartsWith(SESSION_PREFIX)).ToList(); }
        }

        /// <summary>Removes the specified item from the session state.</summary>
        /// <param name="key">The key used to reference the session state item.</param>
        /// <returns><c>true</c> if an item was removed; otheriwse, <c>false</c>.</returns>
        public Boolean Remove(String key)
        {
            return this.InnerDictionary.Remove(this.GetInternalKey(key));
        }

        /// <summary>Gets the session state item associated with the specified <paramref name="key" />.</summary>
        /// <param name="key">The key used to reference the session state item.</param>
        /// <param name="value">When this method returns, the session state item associated with the specified <paramref name="key" /> if the key is found; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if a session state item associated with the specified <paramref name="key" /> is found; otherwise, <c>false</c>.</returns>
        public Boolean TryGetValue(String key, out Object value)
        {
            return this.InnerDictionary.TryGetValue(this.GetInternalKey(key), out value);
        }

        /// <summary>Gets a collection of session state items contained in the session state.</summary>
        public ICollection<Object> Values
        {
            get { return this.InnerDictionary.Where(kvp => kvp.Key.StartsWith(SESSION_PREFIX)).Select(kvp => kvp.Value).ToList(); }
        }

        /// <summary>Gets or sets the session state item at the specified key.</summary>
        /// <param name="key">The key used to reference the session state item.</param>
        /// <returns>The specified session state item.</returns>
        public Object this[String key]
        {
            get
            {
                Object value;
                this.TryGetValue(key, out value);
                return value;
            }
            set { this.InnerDictionary[this.GetInternalKey(key)] = value; }
        }

        #endregion

        #region [ ICollection<KeyValuePair<String, Object>> Members ]

        void ICollection<KeyValuePair<String, Object>>.Add(KeyValuePair<String, Object> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>Removes all items from the session state.</summary>
        public void Clear()
        {
            while (this.InnerDictionary.Keys.Any(key => key.StartsWith(SESSION_PREFIX)))
            {
                this.InnerDictionary.Remove(this.InnerDictionary.Keys.First(key => key.StartsWith(SESSION_PREFIX)));
            }
        }

        Boolean ICollection<KeyValuePair<String, Object>>.Contains(KeyValuePair<String, Object> item)
        {
            return this.InnerDictionary.Contains(new KeyValuePair<String, Object>(this.GetInternalKey(item.Key), item.Value));
        }

        void ICollection<KeyValuePair<String, Object>>.CopyTo(KeyValuePair<String, Object>[] array, Int32 arrayIndex)
        {
            this.InnerDictionary.Where(kvp => kvp.Key.StartsWith(SESSION_PREFIX)).ToArray().CopyTo(array, arrayIndex);
        }

        /// <summary>Gets the number of items stored in the session state.</summary>
        public Int32 Count
        {
            get { return this.InnerDictionary.Keys.Where(key => key.StartsWith(SESSION_PREFIX)).Count(); }
        }

        Boolean ICollection<KeyValuePair<String, Object>>.IsReadOnly
        {
            get { return false; }
        }

        Boolean ICollection<KeyValuePair<String, Object>>.Remove(KeyValuePair<String, Object> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region [ IEnumerable<KeyValuePair<string,object>> Members ]

        IEnumerator<KeyValuePair<String, Object>> IEnumerable<KeyValuePair<String, Object>>.GetEnumerator()
        {
            return this.InnerDictionary.Where(kvp => kvp.Key.StartsWith(SESSION_PREFIX)).GetEnumerator();
        }

        #endregion

        #region [ IEnumerable Members ]

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.InnerDictionary.Where(kvp => kvp.Key.StartsWith(SESSION_PREFIX)).GetEnumerator();
        }

        #endregion
    }
}
