using System;
using System.Configuration;

namespace Facebook
{

    /// <summary>Configures Facebook applications that will be used by the client framework to make Facebook API requests.</summary>
    public class FacebookApplicationElement : ConfigurationElement
    {
        /// <summary>Gets or sets an optional environment value that specifies which development environment the configured application should be used for.</summary>
        /// <remarks>This property is only used for desktop applications, and is ignored for website applications.</remarks>
        [ConfigurationProperty("environment", IsKey = true, IsRequired = false)]
        public String Environment
        {
            get { return (String)this["environment"]; }
            set { this["environment"] = value; }
        }

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Url
		{
			get { if (global::Common.Properties.IsDevelopmentEnvironment) { return url_test; } else { return url_live; } }
			set { if (global::Common.Properties.IsDevelopmentEnvironment) { url_test = value; } else { url_live = value; } }
		}

		public string PageName
		{
			get { if (global::Common.Properties.IsDevelopmentEnvironment) { return pageName_test; } else { return pageName_live; } }
			set { if (global::Common.Properties.IsDevelopmentEnvironment) { pageName_test = value; } else { pageName_live = value; } }
		}

		public long PageId
		{
			get { if (global::Common.Properties.IsDevelopmentEnvironment) { return pageId_test; } else { return pageId_live; } }
			set { if (global::Common.Properties.IsDevelopmentEnvironment) { pageId_test = value; } else { pageId_live = value; } }
		}

		public string PageToken
		{
			get { if (global::Common.Properties.IsDevelopmentEnvironment) { return pageToken_test; } else { return pageToken_live; } }
			set { if (global::Common.Properties.IsDevelopmentEnvironment) { pageToken_test = value; } else { pageToken_live = value; } }
		}

		/// <summary>Gets or sets the API key for the application that is supplied by Facebook.</summary>
		public long AppId
		{
			get { if (global::Common.Properties.IsDevelopmentEnvironment) { return appId_test; } else { return appId_live; } }
			set { if (global::Common.Properties.IsDevelopmentEnvironment) { appId_test = value; } else { appId_live = value; } }
		}

        /// <summary>Gets or sets the API key for the application that is supplied by Facebook.</summary>
        public string ApiKey
        {
			get { if (global::Common.Properties.IsDevelopmentEnvironment) { return apiKey_test; } else { return apiKey_live; } }
			set { if (global::Common.Properties.IsDevelopmentEnvironment) { apiKey_test = value; } else { apiKey_live = value; } }
        }

        /// <summary>Gets or sets the application secret for the application that is supplied by Facebook.</summary>
        public String Secret
        {
			get { if (global::Common.Properties.IsDevelopmentEnvironment) { return secret_test; } else { return secret_live; } }
			set { if (global::Common.Properties.IsDevelopmentEnvironment) { secret_test = value; } else { secret_live = value; } }
        }

		[ConfigurationProperty("name", IsKey = true, IsRequired = true)]
		private string name
		{
			get { return (string)this["name"]; }
			set { this["name"] = value; }
		}

		[ConfigurationProperty("url_test", IsKey = true, IsRequired = true)]
		private string url_test
		{
			get { return (string)this["url_test"]; }
			set { this["url_test"] = value; }
		}

		[ConfigurationProperty("url_live", IsKey = true, IsRequired = true)]
		private string url_live
		{
			get { return (string)this["url_live"]; }
			set { this["url_live"] = value; }
		}

		[ConfigurationProperty("pageToken_test", IsKey = true, IsRequired = true)]
		private string pageToken_test
		{
			get { return (string)this["pageToken_test"]; }
			set { this["pageToken_test"] = value; }
		}

		[ConfigurationProperty("pageToken_live", IsKey = true, IsRequired = true)]
		private string pageToken_live
		{
			get { return (string)this["pageToken_live"]; }
			set { this["pageToken_live"] = value; }
		}

		[ConfigurationProperty("pageId_test", IsKey = true, IsRequired = true)]
		private long pageId_test
		{
			get { return (long)this["pageId_test"]; }
			set { this["pageId_test"] = value; }
		}

		[ConfigurationProperty("pageId_live", IsKey = true, IsRequired = true)]
		private long pageId_live
		{
			get { return (long)this["pageId_live"]; }
			set { this["pageId_live"] = value; }
		}

		[ConfigurationProperty("pageName_test", IsKey = true, IsRequired = true)]
		private string pageName_test
		{
			get { return (string)this["pageName_test"]; }
			set { this["pageName_test"] = value; }
		}

		[ConfigurationProperty("pageName_live", IsKey = true, IsRequired = true)]
		private string pageName_live
		{
			get { return (string)this["pageName_live"]; }
			set { this["pageName_live"] = value; }
		}

		[ConfigurationProperty("appId_test", IsKey = true, IsRequired = true)]
		private long appId_test
		{
			get { return (long)this["appId_test"]; }
			set { this["appId_test"] = value; }
		}

		[ConfigurationProperty("appId_live", IsKey = true, IsRequired = true)]
		private long appId_live
		{
			get { return (long)this["appId_live"]; }
			set { this["appId_live"] = value; }
		}

		[ConfigurationProperty("apiKey_test", IsKey = true, IsRequired = true)]
		private String apiKey_test
		{
			get { return (String)this["apiKey_test"]; }
			set { this["apiKey_test"] = value; }
		}

		[ConfigurationProperty("secret_test", IsRequired = true)]
		private String secret_test
		{
			get { return (String)this["secret_test"]; }
			set { this["secret_test"] = value; }
		}

		[ConfigurationProperty("apiKey_live", IsKey = true, IsRequired = true)]
		private String apiKey_live
		{
			get { return (String)this["apiKey_live"]; }
			set { this["apiKey_live"] = value; }
		}

		[ConfigurationProperty("secret_live", IsRequired = true)]
		private String secret_live
		{
			get { return (String)this["secret_live"]; }
			set { this["secret_live"] = value; }
		}

        /// <summary>Gets or sets the name of the Preload FQL configuration.</summary>
        [ConfigurationProperty("preloadFqlParam", IsRequired = false)]
        public String PreloadFqlParam
        {
            get { return (String)this["preloadFqlParam"]; }
            set { this["preloadFqlParam"] = value; }
        }
    }
}
