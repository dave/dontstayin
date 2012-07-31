using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FacebookGraphToolkit {

    internal class FacebookGraphToolkitConfiguration : ConfigurationSection {
        [ConfigurationProperty("FacebookAppID", IsRequired = true)]
        public string _FacebookAppID {
            get { return (string)this["FacebookAppID"]; }
        }

        [ConfigurationProperty("FacebookAppSecret", IsRequired = true)]
        public string _FacebookAppSecret {
            get { return (string)this["FacebookAppSecret"]; }
        }

        [ConfigurationProperty("FacebookAppAddress", IsRequired = true)]
        public string _FacebookAppAddress {
            get { return (string)this["FacebookAppAddress"]; }
        }

        [ConfigurationProperty("PostAuthorizeRedirectURL", IsRequired = true)]
        public string _PostAuthorizeRedirectURL {
            get { return (string)this["PostAuthorizeRedirectURL"]; }
        }

        [ConfigurationProperty("CachePolicy")]
        public FacebookWebResponseCachePolicy _CachePolicy {
            get { return (FacebookWebResponseCachePolicy)this["CachePolicy"]; }
        }

        [ConfigurationProperty("WebTimeOut")]
        public int _webtimeout {
            get { return (int)this["WebTimeOut"]; }
        }
    }
    internal class FacebookWebResponseCachePolicy : ConfigurationElement {
        [ConfigurationProperty("Time", IsRequired=true)]
        public int _Time {
            get { return (int)this["Time"]; }
        }
        [ConfigurationProperty("SlidingExpiration", IsRequired=true)]
        public bool _SlidingExpiration {
            get { return (bool)this["SlidingExpiration"]; }
        }
    }
}
