using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// Information about a Page that is liked by a user
    /// </summary>
    public class LikedPage {
        JsonObject data;
        internal LikedPage(JsonObject JO) { data = JO; }

        /// <summary>
        /// Name of the Page
        /// </summary>
        public string name { get { return (string)data["name"]; } }
        /// <summary>
        /// Category of the Page
        /// </summary>
        public string category { get { return (string)data["category"]; } }
        /// <summary>
        /// Page ID
        /// </summary>
        public string id { get { return (string)data["id"]; } }
        /// <summary>
        /// The time this Page is liked
        /// </summary>
        public DateTime created_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["created_time"]);
            }
        }
    }
}
