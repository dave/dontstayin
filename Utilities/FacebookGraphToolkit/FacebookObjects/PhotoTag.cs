using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// Represents a photo tag in a photo
    /// </summary>
    public class PhotoTag {
        JsonObject data;

        internal PhotoTag(JsonObject JO) {
            data = JO;
        }

        /// <summary>
        /// ID of subject
        /// </summary>
        public string id {
            get {
                return (string)data["id"];
            }
        }

        /// <summary>
        /// name of subject
        /// </summary>
        public string name {
            get {
                return (string)data["name"];
            }
        }
        /// <summary>
        /// x position of tag
        /// </summary>
        public double x {
            get {
                return Convert.ToDouble(data["x"]);
            }
        }
        /// <summary>
        /// y position of tag
        /// </summary>
        public double y {
            get {
                return Convert.ToDouble(data["y"]);
            }
        }
        /// <summary>
        /// the time the tag was created
        /// </summary>
        public DateTime created_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["created_time"]);
            }
        }
    }
}
