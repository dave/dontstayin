using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// An individual entry in a profile's feed on Facebook
    /// </summary>
    public class Post {
        JsonObject data;
        internal Post(JsonObject JO) {
            data = JO;
        }
        /// <summary>
        /// Gets information of a Facebook Post using the Post ID
        /// </summary>
        /// <param name="PostID">Post ID</param>
        /// <param name="AccessToken">Access Token</param>
        public Post(string PostID,string AccessToken) {
            data = Helpers.WebResponseHelper.GetJsonFromFacebookObject(PostID, AccessToken);
        }
        /// <summary>
        /// The post ID
        /// </summary>
        public string id { get { return (string)data["id"]; } }
        /// <summary>
        /// The profile of user/page who posted the message
        /// </summary>
        public NameIDPair from { get { return new NameIDPair((JsonObject)data["from"]); } }
        /// <summary>
        /// Profiles mentioned or targeted in this post
        /// </summary>
        public IList<NameIDPair> to {
            get {
                if (data["to"] == null) return null;
                IList<NameIDPair> to = new List<NameIDPair>();
                JsonArray dataArray = (JsonArray)data["data"];
                foreach (JsonObject toJO in dataArray.JsonObjects) {
                    to.Add(new NameIDPair(toJO));
                }
                return to;
            }
        }
        /// <summary>
        /// The message of this post
        /// </summary>
        public string message { get { return (string)data["message"]; } }
        /// <summary>
        /// The URL to the picture in this post
        /// </summary>
        public string picture { get { return (string)data["picture"]; } }
        /// <summary>
        /// The URL to an icon representing the type of this post
        /// </summary>
        public string icon { get { return (string)data["icon"]; } }
        /// <summary>
        /// The link attached to this post
        /// </summary>
        public LinkAttachment link_attachment {
            get {
                if (data["link"] != null) return new LinkAttachment((string)data["name"], (string)data["link"], (string)data["caption"], (string)data["description"]);
                return null;
            }
        }
        /// <summary>
        /// The type of this post
        /// </summary>
        public PostType type {
            get {
                switch ((string)data["type"]) {
                    case "status":
                        return PostType.status;
                    case "link":
                        return PostType.link;
                    case "photo":
                        return PostType.photo;
                    default:
                        return PostType.video;
                }
            }
        }
        /// <summary>
        /// The time the post was initially published
        /// </summary>
        public DateTime created_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["created_time"]);
            }
        }
        /// <summary>
        /// The time of the last comment on this post
        /// </summary>
        public DateTime updated_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["updated_time"]);
            }
        }
        /// <summary>
        /// The number of likes on this post
        /// </summary>
        public int likes_count { get {
            if (data["likes"] == null) return 0;
            return (int)((JsonObject)data["likes"])["count"]; } }
        /// <summary>
        /// The users to like this post
        /// </summary>
        public IList<NameIDPair> likes_from {
            get {
                if (data["likes"] == null) return null;
                IList<NameIDPair> likes = new List<NameIDPair>();
                JsonArray dataArray = (JsonArray)((JsonObject)data["likes"])["data"];
                foreach (JsonObject likesJO in dataArray.JsonObjects) {
                    likes.Add(new NameIDPair(likesJO));
                }
                return likes;
            }
        }
        /// <summary>
        /// The number of comments on this post
        /// </summary>
        public int comments_count { get {
            if (data["comments"] == null) return 0;
            return (int)((JsonObject)data["comments"])["count"]; } }
        /// <summary>
        /// The last 50 comments on this post
        /// </summary>
        public IList<Comment> comments {
            get {
                if (data["comments"] == null) return null;
                IList<Comment> comments = new List<Comment>();
                JsonArray dataArray = (JsonArray)((JsonObject)data["comments"])["data"];
                foreach (JsonObject commentJO in dataArray.JsonObjects) {
                    comments.Add(new Comment(commentJO));
                }
                return comments;
            }
        }
        /// <summary>
        /// The application used to create this post
        /// </summary>
        public NameIDPair application {
            get {
                if (data["application"] == null) return null;
                return new NameIDPair((JsonObject)data["application"]);
            }
        }

        /// <summary>
        /// A list of available actions on the post (includig commenting, liking and an optional app-specified action)
        /// </summary>
        public IList<PostAction> actions {
            get {
                if (data["actions"] == null) return null;
                IList<PostAction> actions = new List<PostAction>();
                JsonArray dataArray = (JsonArray)data["actions"];
                foreach (JsonObject actionJO in dataArray.JsonObjects) {
                    actions.Add(new PostAction(actionJO));
                }
                return actions;
            }
        }
    }
}
