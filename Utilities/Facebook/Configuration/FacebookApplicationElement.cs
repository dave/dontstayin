using System;
using System.Configuration;
using Common;

namespace Facebook.Configuration
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

        /// <summary>Gets or sets the API key for the application that is supplied by Facebook.</summary>
        public String ApiKey
        {
			get { if (Common.Properties.IsDevelopmentEnvironment) { return apiKey_test; } else { return apiKey_live; } }
			set { if (Common.Properties.IsDevelopmentEnvironment) { apiKey_test = value; } else { apiKey_live = value; } }
        }

        /// <summary>Gets or sets the application secret for the application that is supplied by Facebook.</summary>
        public String Secret
        {
            get { if (Common.Properties.IsDevelopmentEnvironment) { return secret_test; } else { return secret_live; } }
			set { if (Common.Properties.IsDevelopmentEnvironment) { secret_test = value; } else { secret_live = value; } }
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
