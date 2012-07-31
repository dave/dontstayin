using System;
using System.Configuration;
using System.Linq;

namespace Facebook.Configuration
{
    /// <summary>A collection of <see cref="FacebookApplicationElement" /> elements that makes up the list of Facebook applications the application
    /// is equipped to service requests for.</summary>
    [ConfigurationCollection(typeof(FacebookApplicationElement))]
    public class FacebookApplicationElementCollection : ConfigurationElementCollection
    {
        /// <summary>Creates a new <see cref="FacebookApplicationElement" />.</summary>
        /// <returns>A new <see cref="FacebookApplicationElement" />.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new FacebookApplicationElement();
        }

        /// <summary>Gets the element key for the specified <see cref="FacebookApplicationElement" />.</summary>
        /// <param name="element">The <see cref="FacebookApplicationElement" /> to return the key for.</param>
        /// <returns>An <see cref="Object" /> that acts as the key for the specified <see cref="FacebookApplicationElement" />.</returns>
        protected override Object GetElementKey(ConfigurationElement element)
        {
            var app = (FacebookApplicationElement)element;
            return app.Environment + app.ApiKey;
        }

        /// <summary>Gets the <see cref="FacebookApplicationElement" /> configured for the current environment.</summary>
        /// <returns>The <see cref="FacebookApplicationElement" /> configured for the current environment.</returns>
        /// <remarks>
        /// <para>This method is called by <see cref="Facebook.Desktop.FacebookDesktopContext" /> during initialization. The method checks for
        /// an appSetting with the key <c>Environment</c>. If one is not specified, the <see cref="FacebookApplicationElement" /> with no
        /// <see cref="FacebookApplicationElement.Environment" /> property specified is returned.</para>
        /// </remarks>
        public FacebookApplicationElement GetCurrent()
        {
            String _environment;
            _environment = ConfigurationManager.AppSettings["Environment"] ?? String.Empty;

            return (
                from app in this.OfType<FacebookApplicationElement>()
                where (_environment == app.Environment || (String.IsNullOrEmpty(_environment) && String.IsNullOrEmpty(app.Environment)))
                select app).FirstOrDefault();
        }
    }
}
