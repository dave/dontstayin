using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// A status message on a user's wall
    /// </summary>
    public class StatusMessage {
        JsonObject data;

        internal StatusMessage(JsonObject JO) {
            data = JO;
        }
        /// <summary>
        /// The status message ID
        /// </summary>
        public string id { get { return (string)data["id"]; } }
        /// <summary>
        /// The user who posted the message
        /// </summary>
        public NameIDPair from { get { return new NameIDPair((JsonObject)data["from"]); } }
        /// <summary>
        /// The status message content
        /// </summary>
        public string message { get { return (string)data["message"]; } }
        /// <summary>
        /// The time the message was published
        /// </summary>
        public DateTime updated_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["updated_time"]);
            }
        }
        /// <summary>
        /// All of the comments on this message
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
        /// The users that have liked this message
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
    }
}
