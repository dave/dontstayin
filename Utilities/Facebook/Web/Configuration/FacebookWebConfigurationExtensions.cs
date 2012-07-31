//using System;
//using System.Linq;
//using Facebook.Configuration;

//namespace Facebook.Web.Configuration
//{
//    /// <summary>Provides web-specific extension methods for <see cref="Facebook.Configuration" /> types.</summary>
//    public static class FacebookWebConfigurationExtensions
//    {
//        /// <summary>Gets the <see cref="FacebookApplicationElement" /> configured for the specified <paramref name="apiKey"/>.</summary>
//        /// <param name="col">A <see cref="FacebookApplicationElementCollection" /> object.</param>
//        /// <param name="apiKey">The API key for the current HTTP request.</param>
//        /// <returns>The <see cref="FacebookApplicationElement" /> configured for the specified <paramref name="apiKey"/>.</returns>
//        public static FacebookApplicationElement GetByApiKey(this FacebookApplicationElementCollection col, String apiKey)
//        {
//            return col.OfType<FacebookApplicationElement>().SingleOrDefault(app => app.ApiKey == apiKey);
//        }
//    }
//}
