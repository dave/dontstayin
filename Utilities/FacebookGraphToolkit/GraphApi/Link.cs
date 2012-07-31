using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// A link shared on a user's wall
    /// </summary>
    public class Link {
        JsonObject data;
        internal Link(JsonObject JO) {
            data = JO;
        }
        /// <summary>
        /// The link ID
        /// </summary>
        public string id { get { return (string)data["id"]; } }
        /// <summary>
        /// The user that created the link
        /// </summary>
        public NameIDPair from { get { return new NameIDPair((JsonObject)data["from"]); } }
        /// <summary>
        /// The URL that was shared
        /// </summary>
        public string link { get { return (string)data["link"]; } }
        /// <summary>
        /// The name of the link
        /// </summary>
        public string name { get { return (string)data["name"]; } }
        /// <summary>
        /// The caption of the link (appears beneath the link name)
        /// </summary>
        public string caption { get { return (string)data["caption"]; } }
        /// <summary>
        /// A description of the link (appears beneath the link caption)
        /// </summary>
        public string description { get { return (string)data["description"]; } }
        /// <summary>
        /// A URL to the link icon that Facebook displays in the news feed
        /// </summary>
        public string icon { get { return (string)data["icon"]; } }
        /// <summary>
        /// A URL to the thumbnail image used in the link post
        /// </summary>
        public string picture { get { return (string)data["picture"]; } }
        /// <summary>
        /// The optional message from the user about this link
        /// </summary>
        public string message { get { return (string)data["message"]; } }
        /// <summary>
        /// The time the message was published
        /// </summary>
        public DateTime created_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["created_time"]);
            }
        }
    }
}
