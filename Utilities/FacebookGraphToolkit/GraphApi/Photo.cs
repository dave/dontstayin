using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// An individual photo within an album on Facebook
    /// </summary>
    public class Photo {
        private JsonObject data;
        internal Photo(JsonObject JO) {
            data = JO;
        }
        /// <summary>
        /// Get the information of a Facebook Photo
        /// </summary>
        /// <param name="PhotoID">Photo ID</param>
        /// <param name="AccessToken">Access Token</param>
        public Photo(string PhotoID, string AccessToken) {
            data = Helpers.WebResponseHelper.GetJsonFromFacebookObject(PhotoID, AccessToken);
        }

        /// <summary>
        /// The photo ID
        /// </summary>
        public string id {
            get {
                return (string)data["id"];
            }
        }
        /// <summary>
        /// The profile (user or page) that posted this photo
        /// </summary>
        public NameIDPair from { get { return new NameIDPair((JsonObject)data["from"]); } }
        /// <summary>
        /// The caption given to this photo
        /// </summary>
        public string name {
            get {
                return (string)data["name"];
            }
        }
        /// <summary>
        /// The url of the album-sized view of the photo
        /// </summary>
        public string picture {
            get {
                return (string)data["picture"];
            }
        }
        /// <summary>
        /// The url of the full-sized source of the photo
        /// </summary>
        public string source {
            get {
                return (string)data["source"];
            }
        }
        /// <summary>
        /// The height of the photo
        /// </summary>
        public int height {
            get {
                return (int)data["height"];
            }
        }
        /// <summary>
        /// The width of the photo
        /// </summary>
        public int width {
            get {
                return (int)data["width"];
            }
        }
        /// <summary>
        /// A link to the photo on Facebook
        /// </summary>
        public string link {
            get {
                return (string)data["link"];
            }
        }
        /// <summary>
        /// The position of the photo in its album
        /// </summary>
        public int position {
            get {
                return (int)data["position"];
            }
        }
        /// <summary>
        /// The time the photo was initially published
        /// </summary>
        public DateTime created_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["created_time"]);
            }
        }
        /// <summary>
        /// The last time the photo or its caption was updated
        /// </summary>
        public DateTime updated_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["updated_time"]);
            }
        }
        /// <summary>
        /// The tags in this photo. Returns null if there are no tags.
        /// </summary>
        public IList<PhotoTag> tags {
            get {
                IList<PhotoTag> Tags = new List<PhotoTag>();
                if (data["tags"] != null) {
                    foreach (JsonObject TagJO in ((JsonArray)((JsonObject)data["tags"])["data"]).JsonObjects) {
                        Tags.Add(new PhotoTag(TagJO));
                    }
                    return Tags;
                }
                return null;
            }
        }
    }
}
