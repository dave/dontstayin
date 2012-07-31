using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using System.Web.Configuration;
using System.Web;
using FacebookGraphToolkit.FacebookObjects;
using System.Net;
using System.Drawing;
using System.IO;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// Stores the current Access Token and provides methods to make Api calls using this Access Token
    /// </summary>
    public class Api {

        #region public properties
        /// <summary>
        /// Access Token of the current session. Used to make Api calls.
        /// </summary>
        public string AccessToken {
            get ; private set;
        }

        /// <summary>
        /// ID of current user
        /// </summary>
        public string UserID {
            get;
            private set;
        }

        /// <summary>
        /// The DateTime, in UTC, in which the access_token expires
        /// </summary>
        public DateTime Expires {
            get;
            private set;
        }
        #endregion

        #region constructors

        internal Api(string AccessToken, DateTime Expires) {
            this.AccessToken = AccessToken;
            this.Expires = Expires;
            this.UserID = ((AccessToken.Split('|')[1]).Split('-'))[1];
        }

        /// <summary>
        /// Constructor of new Api object. Use this if you already have an Access Token.
        /// </summary>
        /// <param name="AccessToken">Access Token given by Facebook</param>
        public Api(string AccessToken) {
            this.AccessToken = AccessToken;
            this.UserID = ((AccessToken.Split('|')[1]).Split('-'))[1];
        }
        #endregion

        #region public
        /// <summary>
        /// Get the URL of the picture of a Facebook object
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>The URL of picture</returns>
        public string GetPictureURL(string ID) {
            return Helpers.WebResponseHelper.GetWebResponseRedirectURL(String.Format("https://graph.facebook.com/{0}/picture?access_token={1}", ID, AccessToken));
        }

        /// <summary>
        /// Performs a FQL query and obtain the result
        /// </summary>
        /// <param name="FqlQuery">FQL query</param>
        /// <returns>query result</returns>
        public JsonArray Fql(string FqlQuery) {
            string url = string.Format("https://api.facebook.com/method/fql.query?query={0}&format=JSON&access_token={1}", HttpContext.Current.Server.UrlPathEncode(FqlQuery), AccessToken);
            return new JsonArray(Helpers.WebResponseHelper.GetWebResponse(url));
        }

        /// <summary>
        /// Performs a FQL query that does not require an access token and obtain the result
        /// </summary>
        /// <param name="FqlQuery">FQL query</param>
        /// <returns>query result</returns>
        public static JsonArray FqlPublic(string FqlQuery) {
            string url = string.Format("https://api.facebook.com/method/fql.query?query={0}&format=JSON", HttpContext.Current.Server.UrlPathEncode(FqlQuery));
            return new JsonArray(Helpers.WebResponseHelper.GetWebResponse(url));
        }

        #endregion

        #region current user
        /// <summary>
        /// Gets the Facebook User object of the current user
        /// </summary>
        /// <returns>Facebook User object</returns>
        public User GetUser() {
            return new User(Helpers.WebResponseHelper.GetJsonFromFacebookObject(UserID, AccessToken));
        }

        /// <summary>
        /// Gets all Photo Albums of the current user
        /// </summary>
        /// <returns>The albums that belong to the current user</returns>
        public IList<Album> GetAlbums() {
            return Helpers.ApiCaller.GetAlbums(UserID,AccessToken);
        }

        /// <summary>
        /// Gets the wall of the current user
        /// </summary>
        /// <returns>The user's wall</returns>
        public IList<Post> GetFeed() {
            return Helpers.ApiCaller.GetFeed(UserID,AccessToken);
        }

        /// <summary>
        /// Gets the friends of the current user
        /// </summary>
        /// <returns>The current user's friends</returns>
        public IList<NameIDPair> GetFriends() {
            return Helpers.ApiCaller.GetFriends(UserID, AccessToken);
        }

        /// <summary>
        /// Gets the user's news feed. Requires read_stream permission.
        /// </summary>
        /// <returns></returns>
        public IList<Post> GetHome() {
            return Helpers.ApiCaller.GetHome(UserID, AccessToken);
        }

        /// <summary>
        /// Gets the links posted by the current user
        /// </summary>
        /// <returns>The user's posted links</returns>
        public IList<Link> GetLinks() {
            return Helpers.ApiCaller.GetLinks(UserID, AccessToken);
        }

        /// <summary>
        /// Gets the user's status updates
        /// </summary>
        /// <returns>The user's status updates</returns>
        public IList<StatusMessage> GetStatusMessages() {
            return Helpers.ApiCaller.GetStatusMessages(UserID, AccessToken);
        }

        /// <summary>
        /// Gets the notes of the current user
        /// </summary>
        /// <returns>The user's notes</returns>
        public IList<Note> GetNotes() {
            return Helpers.ApiCaller.GetNotes(UserID, AccessToken);
        }

        /// <summary>
        /// Gets the groups this user is a member of
        /// </summary>
        /// <returns>The groups this user is a member of</returns>
        public IList<NameIDPair> GetGroups() {
            return Helpers.ApiCaller.GetGroups(UserID, AccessToken);
        }

        /// <summary>
        /// Gets the events this user is or maybe attending
        /// </summary>
        /// <returns>The events this user is or maybe attending</returns>
        public IList<UserEvent> GetEvents() {
            return Helpers.ApiCaller.GetEvents(UserID, AccessToken);
        }
        #endregion

        #region Publish

        /// <summary>
        /// Post a comment to a certain post on Facebook
        /// </summary>
        /// <param name="PostID">Post ID</param>
        /// <param name="message">Comment to be posted</param>
        /// <returns>Comment ID</returns>
        public string PostComment(string PostID, string message) {
            WebClient client = new WebClient();
            string response = client.UploadString(string.Format("https://graph.facebook.com/{0}/comments", PostID), string.Format("access_token={0}&message={1}", AccessToken, message));
            client.Dispose();
            JsonObject JO = new JsonObject(response);
            return (string)JO["id"];
        }

        /// <summary>
        /// Delete an object on Facebok. The object must be created by the application.
        /// </summary>
        /// <param name="ID">ID of object</param>
        public void DeleteObject(string ID) {
            WebClient client = new WebClient();
            client.UploadString(string.Format("https://graph.facebook.com/{0}", ID), "DELETE", string.Format("access_token={0}", AccessToken));
            client.Dispose();
        }

        /// <summary>
        /// Like a certain post / comment on Facebook
        /// </summary>
        /// <param name="ID">Post ID / Comment ID</param>
        public void PostLike(string ID) {
            WebClient client = new WebClient();
            client.UploadString(string.Format("https://graph.facebook.com/{0}/likes", ID), string.Format("access_token={0}", AccessToken));
            client.Dispose();
        }

        /// <summary>
        /// Delete a like on a certain post / comment on Facebook
        /// </summary>
        /// <param name="ID">Post ID / Comment ID</param>
        public void DeleteLike(string ID) {
            WebClient client = new WebClient();
            client.UploadString(string.Format("https://graph.facebook.com/{0}/likes", ID), "DELETE", string.Format("access_token={0}", AccessToken));
            client.Dispose();
        }

        /// <summary>
        /// Posts a message to the user's wall
        /// </summary>
        /// <param name="message">Message to be posted</param>
        /// <returns>Post ID</returns>
        public string PostFeed(string message) {
            return PostFeedToTarget("me", message);
        }

        /// <summary>
        /// Posts a message to the user's wall with an async method
        /// </summary>
        /// <param name="message">Message to be posted</param>
        public void PostFeedAsync(string message) {
            PostFeedToTargetAsync("me", message);
        }

        /// <summary>
        /// Post a message to the wall of a Page or user's friend
        /// </summary>
        /// <param name="ID">User ID / Page ID</param>
        /// <param name="message">Message to be posted</param>
        /// <returns>Post ID</returns>
        public string PostFeedToTarget(string ID, string message) {
            WebClient client = new WebClient();
            string response = client.UploadString(string.Format("https://graph.facebook.com/{0}/feed",ID), string.Format("access_token={0}&message={1}", AccessToken, message));
            client.Dispose();
            JsonObject JO = new JsonObject(response);
            return (string)JO["id"];
        }

        /// <summary>
        /// Post a message to the wall of a Page or user's friend via an async method
        /// </summary>
        /// <param name="ID">User ID / Page ID</param>
        /// <param name="message">Message to be posted</param>
        public void PostFeedToTargetAsync(string ID, string message) {
            WebClient client = new WebClient();
            client.UploadStringAsync(new Uri(string.Format("https://graph.facebook.com/{0}/feed", ID)), string.Format("access_token={0}&message={1}", AccessToken, message));
            client.Dispose();
        }

        /// <summary>
        /// Post a message with link attachment to the user's wall
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="picture">If available, a URL to the picture included with this post.</param>
        /// <param name="link">The link attached to this post</param>
        /// <param name="name">The name of the link</param>
        /// <param name="caption">The caption of the link</param>
        /// <param name="description">The description of the link</param>
        /// <returns>Post ID</returns>
        public string PostFeed(string message, string picture, string link, string name, string caption, string description) {
            return PostFeedToTarget("me", message, picture, link, name, caption, description);
        }

        /// <summary>
        /// Post a message with link attachment to the user's wall with an async method
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="picture">If available, a URL to the picture included with this post.</param>
        /// <param name="link">The link attached to this post</param>
        /// <param name="name">The name of the link</param>
        /// <param name="caption">The caption of the link</param>
        /// <param name="description">The description of the link</param>
        public void PostFeedAsync(string message, string picture, string link, string name, string caption, string description) {
            PostFeedToTargetAsync("me", message, picture, link, name, caption, description);
        }

        /// <summary>
        /// Post a message with link attachment to the wall of a Page or user's friend
        /// </summary>
        /// <param name="ID">User ID / Page ID</param>
        /// <param name="message">The message</param>
        /// <param name="picture">If available, a URL to the picture included with this post.</param>
        /// <param name="link">The link attached to this post</param>
        /// <param name="name">The name of the link</param>
        /// <param name="caption">The caption of the link</param>
        /// <param name="description">The description of the link</param>
        /// <returns>Post ID</returns>
        public string PostFeedToTarget(string ID, string message, string picture, string link, string name, string caption, string description) {
            WebClient client = new WebClient();
            string response = client.UploadString(string.Format("https://graph.facebook.com/{0}/feed",ID), string.Format("access_token={0}&message={1}&picture={2}&link={3}&name={4}&caption={5}&description={6}", AccessToken, message, picture, link, name, caption, description));
            client.Dispose();
            JsonObject JO = new JsonObject(response);
            return (string)JO["id"];
        }

        /// <summary>
        /// Post a message with link attachment to the wall of a Page or user's friend with an async method
        /// </summary>
        /// <param name="ID">User ID / Page ID</param>
        /// <param name="message">The message</param>
        /// <param name="picture">If available, a URL to the picture included with this post.</param>
        /// <param name="link">The link attached to this post</param>
        /// <param name="name">The name of the link</param>
        /// <param name="caption">The caption of the link</param>
        /// <param name="description">The description of the link</param>
        public void PostFeedToTargetAsync(string ID, string message, string picture, string link, string name, string caption, string description) {
            WebClient client = new WebClient();
            client.UploadStringAsync(new Uri(string.Format("https://graph.facebook.com/{0}/feed", ID)), string.Format("access_token={0}&message={1}&picture={2}&link={3}&name={4}&caption={5}&description={6}", AccessToken, message, picture, link, name, caption, description));
            client.Dispose();
        }

        /// <summary>
        /// Publishes a photo to the current user's wall
        /// </summary>
        /// <param name="photo">photo to be published</param>
        /// <param name="message">message with this photo</param>
        /// <returns>PhotoID of the photo published</returns>
        public string PublishPhoto(Bitmap photo,string message) {
            MemoryStream MS = new MemoryStream();
            photo.Save(MS, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] Imagebytes = MS.ToArray();
            MS.Dispose();

            //Set up basic variables for constructing the multipart/form-data data
            string newline = "\r\n";
            string boundary = DateTime.Now.Ticks.ToString("x");
            string data = "";

            //Construct data
            data += "--" + boundary + newline;
            data += "Content-Disposition: form-data; name=\"message\"" + newline + newline;
            data += message + newline;

            data += "--" + boundary + newline;
            data += "Content-Disposition: form-data; filename=\"test.jpg\"" + newline;
            data += "Content-Type: image/jpeg" + newline + newline;

            string ending = newline + "--" + boundary + "--" + newline;

            //Convert data to byte[] array
            MemoryStream finaldatastream = new MemoryStream();
            byte[] databytes = Encoding.UTF8.GetBytes(data);
            byte[] endingbytes = Encoding.UTF8.GetBytes(ending);
            finaldatastream.Write(databytes, 0, databytes.Length);
            finaldatastream.Write(Imagebytes, 0, Imagebytes.Length);
            finaldatastream.Write(endingbytes, 0, endingbytes.Length);
            byte[] finaldatabytes = finaldatastream.ToArray();
            finaldatastream.Dispose();

            //Make the request
            WebRequest request = HttpWebRequest.Create("https://graph.facebook.com/me/photos?access_token=" + AccessToken);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.ContentLength = finaldatabytes.Length;
            request.Method = "POST";
            using (Stream RStream = request.GetRequestStream()) {
                RStream.Write(finaldatabytes, 0, finaldatabytes.Length);
            }
            WebResponse WR = request.GetResponse();
            string _Response = "";
            using (StreamReader sr = new StreamReader(WR.GetResponseStream())) {
                _Response = sr.ReadToEnd();
                sr.Close();
            }
            JsonObject JO = new JsonObject(_Response);
            return (string)JO["id"];
        }

        /// <summary>
        /// Publishes a photo to the current user's wall with an async method
        /// </summary>
        /// <param name="photo">photo to be published</param>
        /// <param name="message">message with this photo</param>
        public void PublishPhotoAsync(Bitmap photo, string message) {
            MemoryStream MS = new MemoryStream();
            photo.Save(MS, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] Imagebytes = MS.ToArray();
            MS.Dispose();

            //Set up basic variables for constructing the multipart/form-data data
            string newline = "\r\n";
            string boundary = DateTime.Now.Ticks.ToString("x");
            string data = "";

            //Construct data
            data += "--" + boundary + newline;
            data += "Content-Disposition: form-data; name=\"message\"" + newline + newline;
            data += message + newline;

            data += "--" + boundary + newline;
            data += "Content-Disposition: form-data; filename=\"test.jpg\"" + newline;
            data += "Content-Type: image/jpeg" + newline + newline;

            string ending = newline + "--" + boundary + "--" + newline;

            //Convert data to byte[] array
            MemoryStream finaldatastream = new MemoryStream();
            byte[] databytes = Encoding.UTF8.GetBytes(data);
            byte[] endingbytes = Encoding.UTF8.GetBytes(ending);
            finaldatastream.Write(databytes, 0, databytes.Length);
            finaldatastream.Write(Imagebytes, 0, Imagebytes.Length);
            finaldatastream.Write(endingbytes, 0, endingbytes.Length);
            byte[] finaldatabytes = finaldatastream.ToArray();
            finaldatastream.Dispose();

            //Make the request
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
            client.UploadDataAsync(new Uri("https://graph.facebook.com/me/photos?access_token=" + AccessToken), "POST", finaldatabytes);
        }
        #endregion
    }
}
