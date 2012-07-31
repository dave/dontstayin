using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// An available action on a post
    /// </summary>
    public class PostAction {
        private string _name;
        private string _link;
        /// <summary>
        /// Name of action
        /// </summary>
        public string name { get { return _name; } }
        /// <summary>
        /// Link to action
        /// </summary>
        public string link { get { return _link; } }
        /// <summary>
        /// Creates an object that represents an action link which appear next to "Comment" and "Like". May be used when posting feeds.
        /// </summary>
        /// <param name="name">Name of action link</param>
        /// <param name="link">Url of action link</param>
        public PostAction(string name, string link) {
            _name = name;
            _link = link;
        }
        internal PostAction(JSON.JsonObject JO) {
            _name = (string)JO["name"];
            _link = (string)JO["link"];
        }
    }
}
