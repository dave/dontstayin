using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// The name and id of a Facebook User/Page/Application
    /// </summary>
    public class NameIDPair {
        private string _name;
        private string _id;
        /// <summary>
        /// name of the Facebook User/Page/Application
        /// </summary>
        public string name { get { return _name; } }
        /// <summary>
        /// id of the Facebook User/Page/Application
        /// </summary>
        public string id { get { return _id; } }
        internal NameIDPair(string name, string id) {
            _name = name;
            _id = id;
        }
        internal NameIDPair(JSON.JsonObject JO) {
            _name = (string)JO["name"];
            _id = (string)JO["id"];
        }
    }
}
