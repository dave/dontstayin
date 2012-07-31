using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// A comment on Facebook
    /// </summary>
    public class Comment {
        JsonObject data;
        internal Comment(JsonObject JO) {
            data = JO;
        }
        /// <summary>
        /// Gets Facebook Comment specified by CommmentID
        /// </summary>
        /// <param name="CommentID"></param>
        /// <param name="AccessToken"></param>
        public Comment(string CommentID, string AccessToken) {
            data = Helpers.WebResponseHelper.GetJsonFromFacebookObject(CommentID, AccessToken);
        }
        /// <summary>
        /// The Facebook ID of the comment
        /// </summary>
        public string id { get { return (string)data["id"]; } }
        /// <summary>
        /// The DateTime the comment was created
        /// </summary>
        public DateTime created_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["created_time"]);
            }
        }
        /// <summary>
        /// The comment text
        /// </summary>
        public string message { get { return (string)data["message"]; } }
        /// <summary>
        /// The user that created the comment
        /// </summary>
        public NameIDPair from { get { return new NameIDPair((JsonObject)data["from"]); } }
        /// <summary>
        /// The number of likes on this comment
        /// </summary>
        public int likes {
            get {
                if (data["likes"] == null) return 0;
                return (int)data["likes"];
            }
        }
    }
}
