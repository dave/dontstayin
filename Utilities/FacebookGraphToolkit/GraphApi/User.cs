using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// represents a Facebook User
    /// </summary>
    public class User{
        JsonObject data;

        internal User(JsonObject JO) {
            data = JO;
        }

        /// <summary>
        /// Gets the general information of a Facebook User. For detailed information, pass the AccessToken in a overloaded of this constructor.
        /// </summary>
        /// <param name="UserID">User ID</param>
        public User(string UserID) {
            data = Helpers.WebResponseHelper.GetJsonFromFacebookObject(UserID, null);
        }
        /// <summary>
        /// Gets the information of a Facebook User.
        /// </summary>
        /// <param name="UserID">User ID</param>
        /// <param name="AccessToken">Access Token</param>
        public User(string UserID, string AccessToken) {
            data = Helpers.WebResponseHelper.GetJsonFromFacebookObject(UserID, AccessToken);
        }

        #region Public Properties
        /// <summary>
        /// The user's Facebook ID
        /// </summary>
        public string id {
            get {
                return (string)data["id"];
            }
        }
        /// <summary>
        /// The user's full name
        /// </summary>
        public string name {
            get {
                return (string)data["name"];
            }
        }
        /// <summary>
        /// The user's first name
        /// </summary>
        public string first_name {
            get {
                return (string)data["first_name"];
            }
        }
        /// <summary>
        /// The user's last name
        /// </summary>
        public string last_name {
            get {
                return (string)data["last_name"];
            }
        }
        /// <summary>
        /// The URL of the profile for the user on Facebook
        /// </summary>
        public string link {
            get {
                return (string)data["link"];
            }
        }
        /// <summary>
        /// The user's gender
        /// </summary>
        public string gender {
            get {
                return (string)data["gender"];
            }
        }
        /// <summary>
        /// The proxied or contact email address granted by the user
        /// </summary>
        public string email { get { return (string)data["email"]; } }
        /// <summary>
        /// The user's timezone offset from UTC
        /// </summary>
        public int? timezone {
            get {
                return (int?)data["timezone"];
            }
        }
        /// <summary>
        /// The user's locale
        /// </summary>
        public string locale {
            get {
                return (string)data["locale"];
            }
        }
        /// <summary>
        /// The user's account verification status
        /// </summary>
        public bool? verified {
            get {
                return (bool?)data["verified"];
            }
        }
        /// <summary>
        /// The last time the user's profile was updated
        /// </summary>
        public DateTime? updated_time {
            get {
                if (data["updated_time"] == null) return null;
                return Helpers.Generic.RFC3339ToDateTime((string)data["updated_time"]);
            }
        }
        #endregion

        #region connections
        /// <summary>
        /// Gets all Photo Albums of the user
        /// </summary>
        /// <returns>The albums that belong to the user</returns>
        public IList<Album> GetAlbums(string AccessToken) {
            return Helpers.ApiCaller.GetAlbums(id, AccessToken);
        }

        /// <summary>
        /// Gets the wall of the user
        /// </summary>
        /// <returns>The user's wall</returns>
        public IList<Post> GetFeed(string AccessToken) {
            return Helpers.ApiCaller.GetFeed(id, AccessToken);
        }

        /// <summary>
        /// Gets the wall of a user
        /// </summary>
        /// <param name="UserID">User ID</param>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>The user's wall</returns>
        public static IList<Post> GetFeed(string UserID, string AccessToken) {
            return Helpers.ApiCaller.GetFeed(UserID, AccessToken);
        }

        /// <summary>
        /// Gets all the posts by the user
        /// </summary>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>The user's own posts</returns>
        public IList<Post> GetPosts(string AccessToken) {
            return Helpers.ApiCaller.GetPosts(id, AccessToken);
        }

        /// <summary>
        /// Gets the links posted by the current user
        /// </summary>
        /// <returns>The user's posted links</returns>
        public IList<Link> GetLinks(string AccessToken) {
            return Helpers.ApiCaller.GetLinks(id, AccessToken);
        }

        /// <summary>
        /// Gets the friends of the user
        /// </summary>
        /// <returns>The user's friends</returns>
        public IList<NameIDPair> GetFriends(string AccessToken) {
            return Helpers.ApiCaller.GetFriends(id, AccessToken);
        }

        /// <summary>
        /// Gets the user's status updates
        /// </summary>
        /// <returns>The user's status updates</returns>
        public IList<StatusMessage> GetStatusMessages(string AccessToken) {
            return Helpers.ApiCaller.GetStatusMessages(id, AccessToken);
        }

        /// <summary>
        /// Gets a particular user's status updates
        /// </summary>
        /// <param name="UserID">User ID</param>
        /// <param name="AccessToken">Access Token</param>
        /// <returns></returns>
        public static IList<StatusMessage> GetStatusMessages(string UserID, string AccessToken) {
            return Helpers.ApiCaller.GetStatusMessages(UserID, AccessToken);
        }

        /// <summary>
        /// Gets the notes of the user
        /// </summary>
        /// <returns>The user's notes</returns>
        public IList<Note> GetNotes(string AccessToken) {
            return Helpers.ApiCaller.GetNotes(id, AccessToken);
        }

        /// <summary>
        /// Gets the notes of a particular user
        /// </summary>
        /// <param name="UserID">User ID</param>
        /// <param name="AccessToken">Access Token</param>
        /// <returns></returns>
        public static IList<Note> GetNotes(string UserID, string AccessToken) {
            return Helpers.ApiCaller.GetNotes(UserID, AccessToken);
        }

        /// <summary>
        /// Gets all the Pages the user has liked
        /// </summary>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>All the Pages this user has liked</returns>
        public IList<LikedPage> GetLikedPages(string AccessToken) {
            return Helpers.ApiCaller.GetLikedPages(id, AccessToken);
        }

        /// <summary>
        /// Gets the groups this user is a member of
        /// </summary>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>The groups this user is a member of</returns>
        public IList<NameIDPair> GetGroups(string AccessToken) {
            return Helpers.ApiCaller.GetGroups(id, AccessToken);
        }

        /// <summary>
        /// Gets the events this user is or maybe attending
        /// </summary>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>The events this user is or maybe attending</returns>
        public IList<UserEvent> GetEvents(string AccessToken) {
            return Helpers.ApiCaller.GetEvents(id, AccessToken);
        }

        #endregion
    }
}
