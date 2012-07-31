using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// An application registered on Facebook Platform
    /// </summary>
    public class Application {
        JsonObject data;
        /// <summary>
        /// Gets the information of a Facebook Application
        /// </summary>
        /// <param name="ApplicationID">Application ID</param>
        public Application(string ApplicationID) {
            data = Helpers.WebResponseHelper.GetJsonFromFacebookObject(ApplicationID, "");
        }

        #region public properties
        /// <summary>
        /// The application ID
        /// </summary>
        public string id { get { return (string)data["id"]; } }
        /// <summary>
        /// The title of the application
        /// </summary>
        public string name { get { return (string)data["name"]; } }
        /// <summary>
        /// The description of the application written by the 3rd party developers
        /// </summary>
        public string description { get { return (string)data["description"]; } }
        /// <summary>
        /// The category of the application
        /// </summary>
        public string category { get { return (string)data["category"]; } }
        /// <summary>
        /// A link to the application dashboard on Facebook
        /// </summary>
        public string link { get { return (string)data["link"]; } }
#endregion
        /// <summary>
        /// Get all posts by this application on its wall
        /// </summary>
        /// <returns>The application's own posts</returns>
        public IList<Post> GetPosts() {
            return GetPosts(id);
        }
        /// <summary>
        /// Get all the posts on the application's wall by the application itself, specified by the Application ID
        /// </summary>
        /// <param name="ApplicationID">Application ID</param>
        /// <returns>The application's own posts</returns>
        static public IList<Post> GetPosts(string ApplicationID) {
            return Helpers.ApiCaller.GetPosts(ApplicationID, "");
        }

        /// <summary>
        /// Get all posts on the application's wall
        /// </summary>
        /// <returns>The application's wall</returns>
        public IList<Post> GetFeed() {
            return GetFeed(id);
        }
        /// <summary>
        /// Get all posts on the wall of the application specified by the Application ID
        /// </summary>
        /// <param name="ApplicationID">The application's wall</param>
        /// <returns>The application's wall</returns>
        static public IList<Post> GetFeed(string ApplicationID) {
            return Helpers.ApiCaller.GetFeed(ApplicationID, "");
        }

        /// <summary>
        /// Gets the photo albums created by this application
        /// </summary>
        /// <returns>The photo albums this application created</returns>
        public IList<Album> GetAlbums() {
            return GetAlbums(id);
        }
        
        /// <summary>
        /// Gets the photo albums created by the application specified by the Application ID
        /// </summary>
        /// <param name="ApplicationID">Application ID</param>
        /// <returns>The photo albums this application created</returns>
        static public IList<Album> GetAlbums(string ApplicationID) {
            return Helpers.ApiCaller.GetAlbums(ApplicationID, "");
        }
    }
}
