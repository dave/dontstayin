using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// represents a link attachment in a Facebook Post
    /// </summary>
    public class LinkAttachment {
        private string _name;
        private string _link;
        private string _caption;
        private string _description;
        /// <summary>
        /// The name of the link
        /// </summary>
        public string name { get { return _name; } }
        /// <summary>
        /// The URL of the link
        /// </summary>
        public string link { get { return _link; } }
        /// <summary>
        /// The caption of the link
        /// </summary>
        public string caption { get { return _caption; } }
        /// <summary>
        /// The description of the link
        /// </summary>
        public string description { get { return _description; } }
        /// <summary>
        /// Creates a new link attachment of a Facebook Post
        /// </summary>
        /// <param name="name">The name of the link</param>
        /// <param name="link">The URL of the link</param>
        /// <param name="caption">The caption of the link</param>
        /// <param name="description">The description of the link</param>
        public LinkAttachment(string name, string link, string caption, string description) {
            _name = name;
            _link = link;
            _caption = caption;
            _description = description;
        }
    }
}
