using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// Represents basic data about the user who is browsing this Iframe Application
    /// </summary>
    public class BasicUserData {
        JsonObject data;
        internal BasicUserData(JsonObject JO) {
            data = JO;
        }
        /// <summary>
        /// Country of current user
        /// </summary>
        public string country { get { return (string)data["country"]; } }
        /// <summary>
        /// Locale of current user
        /// </summary>
        public string locale { get { return (string)data["locale"]; } }
        /// <summary>
        /// Minimum age of current user
        /// </summary>
        public int age_min { get { return (int)((JsonObject)data["age"])["min"]; } }
        /// <summary>
        /// Maximum age of current user
        /// </summary>
        public int age_max { get { return (int)((JsonObject)data["age"])["max"]; } }
    }
}
