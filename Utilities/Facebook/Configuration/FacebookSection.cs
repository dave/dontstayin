using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Hosting;

namespace Facebook.Configuration
{
    /// <summary>Specifies the root element for Facebook API client configuration</summary>
    public class FacebookSection : ConfigurationSection
    {
        /// <summary>Gets or sets a collection of Facebook application configurations.</summary>
        [ConfigurationProperty("applications")]
        [ConfigurationCollection(typeof(FacebookApplicationElement))]
        public FacebookApplicationElementCollection Applications
        {
            get { return (FacebookApplicationElementCollection)this["applications"]; }
            set { this["applications"] = value; }
        }

        /// <summary>Gets or sets the relative path of the Feed template configuration.</summary>
        [ConfigurationProperty("feedTemplateConfigSource")]
        public String FeedTemplateConfigSource
        {
            get { return (String)this["feedTemplateConfigSource"]; }
            set { this["feedTemplateConfigSource"] = value; }
        }

        /// <summary>Gets a <see cref="Boolean" /> value representing whether the current application's configuration specifies a <see cref="FacebookSection" />.</summary>
        public static Boolean IsConfigured
        {
            get
            {
                try { return FacebookSection.Current != null; }
                catch { return false; }
            }
        }

        private static FacebookSection _current;
        /// <summary>Gets a reference to the <see cref="FacebookSection" /> configured for the current application.</summary>
        public static FacebookSection Current
        {
            get
            {
                if (FacebookSection._current == null)
                {
                    System.Configuration.Configuration config = null;
                    if(HostingEnvironment.IsHosted) config = WebConfigurationManager.OpenWebConfiguration("~/");
                    else if (Assembly.GetEntryAssembly() == null)
                    {
                        // this is hacky, but only needs to work for unit testing
                        var dir = new DirectoryInfo(Environment.CurrentDirectory);
                        var path = dir.GetFiles("*.config").First().FullName.Replace(".config", String.Empty);
                        config = ConfigurationManager.OpenExeConfiguration(path);
                    }
                    else config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
                    var sections = config.Sections.OfType<ConfigurationSection>().Where(section => section.GetType() == typeof(FacebookSection)).ToList();
                    if (sections.Count > 1)
                    {
                        throw new ConfigurationErrorsException(String.Format("Too many instances of FacebookSection were found. Expected 1, found {0}.", sections.Count));
                    }
                    else if (sections.Count == 0)
                    {
                        throw new ConfigurationErrorsException("A configuration section of the type FacebookSection was not found in the current configuration.");
                    }
                    else
                    {
                        FacebookSection._current = (FacebookSection)sections.Single();
                    }
                }
                return FacebookSection._current;
            }
        }
    }
}
