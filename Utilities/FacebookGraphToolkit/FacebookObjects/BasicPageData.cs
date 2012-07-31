using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// Represents basic data about the current view of this page at a Page tab
    /// </summary>
    public class BasicPageData {
        JsonObject data;
        internal BasicPageData(JsonObject JO) {
            data = JO;
        }
        /// <summary>
        /// Page ID
        /// </summary>
        public string ID { get { return (string)data["id"]; } }
        /// <summary>
        /// Whether the user has liked the Page
        /// </summary>
        public bool Liked { get { return (bool)data["liked"]; } }
        /// <summary>
        /// Whether the user is Page Admin
        /// </summary>
        public bool Admin { get { return (bool)data["admin"]; } }
    }
}
