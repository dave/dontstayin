using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// a Facebook Page
    /// </summary>
    public class Page {
        private JsonObject data;
        /// <summary>
        /// Gets information of a Facebook Page
        /// </summary>
        /// <param name="PageID">Page ID</param>
        public Page(string PageID) {
            data = Helpers.WebResponseHelper.GetJsonFromFacebookObject(PageID, "");
        }

        #region public properties
        /// <summary>
        /// The Page's ID
        /// </summary>
        public string id { get { return (string)data["id"]; } }
        /// <summary>
        /// The Page's name
        /// </summary>
        public string name { get { return (string)data["name"]; } }
        /// <summary>
        /// The URL to the Page's profile picture
        /// </summary>
        public string picture { get { return (string)data["picture"]; } }
        /// <summary>
        /// The link to the Page on Facebook
        /// </summary>
        public string link { get { return (string)data["link"]; } }
        /// <summary>
        /// The Page's category
        /// </summary>
        public string category { get { return (string)data["category"]; } }
        /// <summary>
        /// The Page's website
        /// </summary>
        public string website { get { return (string)data["website"]; } }
        /// <summary>
        /// The Page's username
        /// </summary>
        public string username { get { return (string)data["username"]; } }
        /// <summary>
        /// How and by whom the subject of the Page is founded
        /// </summary>
        public string founded { get { return (string)data["founded"]; } }
        /// <summary>
        /// The company overview of the Page's subject
        /// </summary>
        public string company_overview { get { return (string)data["company_overview"]; } }
        /// <summary>
        /// The Mission of the Page's subject
        /// </summary>
        public string mission { get { return (string)data["mission"]; } }
        /// <summary>
        /// The number of users who like the Page
        /// </summary>
        public int likes { get { return (int)data["likes"]; } }
        /// <summary>
        /// The genre of the Page's subject
        /// </summary>
        public string genre { get { return (string)data["genre"]; } }
        /// <summary>
        /// The biography of the Page's subject
        /// </summary>
        public string bio { get { return (string)data["bio"]; } }
        #endregion

        /// <summary>
        /// Gets the albums this Page has created
        /// </summary>
        /// <returns>The photo albums this Page has created</returns>
        public IList<Album> GetAlbums() {
            return Helpers.ApiCaller.GetAlbums(id, "");
        }

        /// <summary>
        /// Gets the albums a Page has created
        /// </summary>
        /// <param name="PageID">Page ID</param>
        /// <returns>The photo albums a Page has created</returns>
        public static IList<Album> GetAlbums(string PageID) {
            return Helpers.ApiCaller.GetAlbums(PageID, "");
        }
        
        /// <summary>
        /// Gets the Page's wall
        /// </summary>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>The Page's wall</returns>
        public IList<Post> GetFeed(string AccessToken) {
            return Helpers.ApiCaller.GetFeed(id, AccessToken);
        }

        /// <summary>
        /// Gets a Page's wall
        /// </summary>
        /// <param name="PageID">Page ID</param>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>The Page's wall</returns>
        public static IList<Post> GetFeed(string PageID, string AccessToken) {
            return Helpers.ApiCaller.GetFeed(PageID, AccessToken);
        }

        /// <summary>
        /// Gets the Page's own posts
        /// </summary>
        /// <returns>The Page's own posts</returns>
        public IList<Post> GetPosts() {
            return Helpers.ApiCaller.GetPosts(id,"");
        }

        /// <summary>
        /// Gets a Page's own posts
        /// </summary>
        /// <param name="PageID">Page ID</param>
        /// <returns>The Page's own posts</returns>
        public static IList<Post> GetPosts(string PageID) {
            return Helpers.ApiCaller.GetPosts(PageID, "");
        }
    }
}
