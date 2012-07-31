using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// A Facebook note
    /// </summary>
    public class Note {
        JsonObject data;
        internal Note(JsonObject JO) {
            data = JO;
        }
        /// <summary>
        /// The note ID
        /// </summary>
        public string id { get { return (string)data["id"]; } }
        /// <summary>
        /// The profile that created the note
        /// </summary>
        public NameIDPair from { get { return new NameIDPair((JsonObject)data["from"]); } }
        /// <summary>
        /// The titel of the note
        /// </summary>
        public string subject { get { return (string)data["subject"]; } }
        /// <summary>
        /// The content of the note (a html string)
        /// </summary>
        public string message { get { return (string)data["message"]; } }
        /// <summary>
        /// The time the note was initially published
        /// </summary>
        public DateTime created_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["created_time"]);
            }
        }
        /// <summary>
        /// The time the note was last updated
        /// </summary>
        public DateTime updated_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["updated_time"]);
            }
        }
        /// <summary>
        /// The URL to the icon that Facebook display with notes
        /// </summary>
        public string icon { get { return (string)data["icon"]; } }

        /// <summary>
        /// All of the comments on this note
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
    }
}
