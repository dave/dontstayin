using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Facebook.Api;

namespace Facebook
{
    /// <summary>Encapsulates all Facebook API-specific information about an individual Facebook API session and provides access to
    /// all the API methods.</summary>
    /// <typeparam name="TCache">The type of object that will be used to cache certain API results.</typeparam>
    /// <typeparam name="TSession">The type of <see cref="ISessionInfo" /> object used to store and provide access to session-level data.</typeparam>
    [Serializable]
    public partial class FacebookContextBase<TCache, TSession> : IFacebookContext, ISerializable
        where TCache : ICacheWrapper, new()
        where TSession : class, ISessionInfo
    {
        /// <summary>The default value that is passed as the "v" parameter for all API calls.</summary>
        internal const String DEFAULT_VERSION = "1.0";

        /// <summary>The default maximum amount of time, in milliseconds, that a request will be active before timing out.</summary>
        internal const Int32 DEFAULT_EXECUTE_TIMEOUT = 5000;

        /// <summary>Initializes a new instance of the <see cref="FacebookContextBase{TCache,TSession}" /> class.</summary>
        /// <param name="apiKey">The <paramref name="apiKey" /> of the application initializing the context.</param>
        /// <param name="secret">The <paramref name="secret"/> corresponding to <paramref name="apiKey"/>.</param>
        /// <remarks>
        /// Information about API Keys and secrets is available in Facebook's <a href="http://developers.facebook.com/get_started.php?tab=tutorial">Getting Started Tutorial</a>.
        /// </remarks>
        public FacebookContextBase(String apiKey, String secret)
            : this(apiKey, secret, DEFAULT_VERSION) { }

        /// <summary>Initializes a new instance of the <see cref="FacebookContextBase{TCache,TSession}" /> class.</summary>
        /// <param name="apiKey">The <paramref name="apiKey" /> of the application initializing the context.</param>
        /// <param name="secret">The <paramref name="secret"/> corresponding to <paramref name="apiKey"/>.</param>
        /// <param name="version">The version of the API to use.</param>
        /// <remarks>
        /// Information about API Keys and secrets is available in Facebook's <a href="http://developers.facebook.com/get_started.php?tab=tutorial">Getting Started Tutorial</a>.
        /// </remarks>
        public FacebookContextBase(String apiKey, String secret, String version)
        {
            this.ApiKey = apiKey;
            this.Secret = secret;
            this.Version = version;
            this.Cache = new FacebookCache(apiKey, new TCache());
            this.InitNonGeneratedControllers();
        }

        /// <summary>Initializes a new instance of the <see cref="FacebookContextBase{TCache,TSession}" /> class.</summary>
        /// <remarks>This constructor is used for deserialization.</remarks>
        private FacebookContextBase(SerializationInfo info, StreamingContext context)
            : this(info.GetString("ApiKey"), info.GetString("Secret"), info.GetString("Version"))
        {
            this.Session = (TSession)info.GetValue("Session", typeof(TSession));
        }

        private static FacebookCache _sessionCache;
        /// <summary>Gets a reference to an instance of <see cref="FacebookCache" /> used specifically for storing <see cref="ISessionInfo" /> data.</summary>
        internal static FacebookCache SessionCache
        {
            get
            {
                if (_sessionCache == null)
                {
                    _sessionCache = new FacebookCache("__FB_SESSION", new TCache());
                }
                return _sessionCache;
            }
        }

        /// <summary>Gets the API key used to initialize this context.</summary>
        public String ApiKey { get; internal set; }
        
        /// <summary>Gets the application secret used to initialize this context.</summary>
        public String Secret { get; internal set; }

        /// <summary>Gets the API version this context targets.</summary>
        public String Version { get; internal set; }

        /// <summary>Gets or sets the amount of time, in milliseconds, API requests will be active before timing out.</summary>
        public Int32? Timeout { get; set; }

        /// <summary>Gets the <see cref="SessionInfo" /> object that contains information about the current user's session.</summary>
        public virtual TSession Session { get; protected internal set; }

        ISessionInfo IFacebookContext.Session { get { return this.Session; } }

        /// <summary>Gets a value representing whether an authenticated session has been established between the current use and the application.</summary>
        public Boolean HasSession
        {
            get
            {
                if (this.Session == null || String.IsNullOrEmpty(this.Session.SessionKey))
					return false;
                return this.Session.Expires > DateTime.Now;
            }
        }

        public void InitSession(TSession sessionInfo)
        {
            if (this.Session == null) this.Session = sessionInfo;
            else this.Session.Init(sessionInfo);
        }

        void IFacebookContext.InitSession(ISessionInfo sessionInfo)
        {
            this.InitSession((TSession)sessionInfo);
        }

        /// <summary>Gets or sets the reference to the cache object used by the current context.</summary>
        public FacebookCache Cache { get; protected internal set; }

        /// <summary>Gets the <see cref="ApplicationType" /> of the context.</summary>
        public virtual ApplicationType ApplicationType { get; private set; }

        /// <summary>Gets the current batch, if any, for the context.</summary>
        Batch IFacebookContext.Batch
        {
            get { return this.Batch; }
            set { this.Batch = value; }
        }

        /// <summary>Provides internal access to the current batch, if any, for the context.</summary>
        internal Batch Batch { get; set; }

        /// <summary>Executes a <see cref="FacebookRequest" /> for <paramref name="methodName" /> with the specified arguments.</summary>
        /// <typeparam name="TValue">The expected return type of the method call.</typeparam>
        /// <param name="methodName">The name of the API method.</param>
        /// <param name="args">An <see cref="IDictionary{String,Object} " /> of arguments that will be passed into the API call.</param>
        /// <param name="excludedArgs">An array of <see cref="String" /> values specifying arguments that should be left out of the API method call.</param>
        /// <returns>A reference to a <see cref="FacebookResponse{TValue}" /> object that will contain the value of the response.</returns>
        /// <remarks>
        /// If a <see cref="Batch" /> is currently active, the <see cref="FacebookResponse{TValue}" /> returned by this method will not contain
        /// a value until <see cref="Api.Batch.Complete" /> is called.
        /// </remarks>
        FacebookResponse<TValue> IFacebookContext.ExecuteRequest<TValue>(String methodName, IDictionary<String, Object> args, params String[] excludedArgs)
        {
            var request = new FacebookRequest(this, methodName, args, excludedArgs);

            if (this.Batch != null && !this.Batch.IsCompleting)
            {
                var response = new FacebookResponse<TValue>();
                Batch.Add<TValue>(request, response);
                return response;
            }
            else
            {
                var response = request.Execute<TValue>(this.Timeout ?? DEFAULT_EXECUTE_TIMEOUT);
                if (response.IsError && response.ResponseException is FacebookApiException)
                {
                    var exception = (FacebookApiException)response.ResponseException;
                    if (exception.ErrorCode == ErrorCode.SessionInvalid || exception.ErrorCode == ErrorCode.SessionTimedOut)
                    {
                        this.Session.Expires = DateTime.Now;
                    }
                }
                return response;
            }
        }

        #region [ ISerializable Members ]

        /// <summary>Populates a <see cref="SerializationInfo" /> object with the data needed to serialize the <see cref="FacebookContextBase{TCache,TSession}" />.</summary>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ApiKey", this.ApiKey);
            info.AddValue("Secret", this.Secret);
            info.AddValue("Session", this.Session);
            info.AddValue("Version", this.Version);
        }

        #endregion
    }
}
