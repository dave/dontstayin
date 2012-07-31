using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// The privacy setting of a group / event
    /// </summary>
    public enum Privacy {
        /// <summary>
        /// An open group / event
        /// </summary>
        open,
        /// <summary>
        /// A closed group / event
        /// </summary>
        closed,
        /// <summary>
        /// A secret group / event
        /// </summary>
        secret
    }
}
