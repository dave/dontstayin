using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// The type of a Facebook Post
    /// </summary>
    public enum PostType {
        /// <summary>
        /// status update
        /// </summary>
        status,
        /// <summary>
        /// post with link attachment
        /// </summary>
        link,
        /// <summary>
        /// post with photo attachment
        /// </summary>
        photo,
        /// <summary>
        /// post with video attachment
        /// </summary>
        video }
}
