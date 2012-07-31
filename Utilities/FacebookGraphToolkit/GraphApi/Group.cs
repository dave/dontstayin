using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// A Facebook group
    /// </summary>
    public class Group {
        JsonObject data;
        internal Group(JsonObject JO) {
            data = JO;
        }
        /// <summary>
        /// Gets the information of a Facebook Group
        /// </summary>
        /// <param name="GroupID">The group ID</param>
        public Group(string GroupID) {
            data = Helpers.WebResponseHelper.GetJsonFromFacebookObject(GroupID, null);
        }

        /// <summary>
        /// The group ID
        /// </summary>
        public string id { get { return (string)data["id"]; } }
        /// <summary>
        /// The profile that created this group
        /// </summary>
        public NameIDPair owner {
            get {
                return new NameIDPair((JsonObject)data["owner"]);
            }
        }
        /// <summary>
        /// The name of the group
        /// </summary>
        public string name { get { return (string)data["name"]; } }
        /// <summary>
        /// A brief description of the group
        /// </summary>
        public string description { get { return (string)data["description"]; } }
        /// <summary>
        /// The privacy setting of the group
        /// </summary>
        public Privacy privacy {
            get {
                string privacy = (string)data["privacy"];
                switch (privacy) {
                    case "OPEN":
                        return Privacy.open;
                    case "CLOSED":
                        return Privacy.closed;
                    default:
                        return Privacy.secret;
                }
            }
        }
        /// <summary>
        /// The URL for the group's icon
        /// </summary>
        public string icon { get { return (string)data["icon"]; } }
        /// <summary>
        /// The last time the group was updated
        /// </summary>
        public DateTime updated_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["updated_time"]);
            }
        }

        /// <summary>
        /// Get the group's wall
        /// </summary>
        /// <returns>The group's wall</returns>
        public IList<Post> GetFeed() {
            return Helpers.ApiCaller.GetFeed(id, "");
        }

        /// <summary>
        /// Get a group's wall
        /// </summary>
        /// <returns>The group's wall</returns>
        public static IList<Post> GetFeed(string GroupID) {
            return Helpers.ApiCaller.GetFeed(GroupID, "");
        }
    }
}
