using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// a photo album on Facebook
    /// </summary>
    public class Album {
        private JsonObject data;
        internal Album(JsonObject JO) {
            data = JO;
        }

        /// <summary>
        /// Gets the information of a photo album on Facebook
        /// </summary>
        /// <param name="AlbumID">Album ID</param>
        /// <param name="AccessToken">Access Token</param>
        public Album(string AlbumID, string AccessToken) {
            data = Helpers.WebResponseHelper.GetJsonFromFacebookObject(AlbumID, AccessToken);
        }

        #region public properties
        /// <summary>The photo album ID</summary>
        public string id {
            get {
                return (string)data["id"];
            }
        }
        /// <summary>
        /// The profile that created this album
        /// </summary>
        public NameIDPair from { get { return new NameIDPair((JsonObject)data["from"]); } }
        /// <summary>
        /// The title of the album
        /// </summary>
        public string name {
            get {
                return (string)data["name"];
            }
        }
        /// <summary>
        /// The location of the album
        /// </summary>
        public string location {
            get {
                return (string)data["location"];
            }
        }
        /// <summary>
        /// The description of the album
        /// </summary>
        public string description {
            get {
                return (string)data["description"];
            }
        }
        /// <summary>
        /// A link to this album on Facebook
        /// </summary>
        public string link {
            get {
                return (string)data["link"];
            }
        }
        /// <summary>
        /// The number of photos in this album
        /// </summary>
        public int count {
            get {
                if (data["count"] == null) return 0;
                return (int)data["count"];
            }
        }
        /// <summary>
        /// The type of the album
        /// </summary>
        public string type {
            get {
                return (string)data["type"];
            }
        }
        /// <summary>
        /// The time the photo album was initially created
        /// </summary>
        public DateTime created_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["created_time"]);
            }
        }
        /// <summary>
        /// The last time the photo album was updated
        /// </summary>
        public DateTime updated_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["updated_time"]);
            }
        }
        #endregion

        /// <summary>
        /// Get all photos in this Facebook Album
        /// </summary>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>The photos in this album</returns>
        public IList<Photo> GetPhotos(string AccessToken) {
            return Helpers.ApiCaller.GetPhotos(id, AccessToken);
        }

        /// <summary>
        /// Get all photos in an Facebook Album
        /// </summary>
        /// <param name="AlbumID">Album ID</param>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>The photos in this album</returns>
        public static IList<Photo> GetPhotos(string AlbumID, string AccessToken) {
            return Helpers.ApiCaller.GetPhotos(AlbumID, AccessToken);
        }
    }
}
