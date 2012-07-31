using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// A user's rsvp to an event
    /// </summary>
    public enum Rsvp {
        /// <summary>
        /// The user has not replied to the event invitation
        /// </summary>
        not_replied,
        /// <summary>
        /// The user replied "Maybe" to the event invitation
        /// </summary>
        maybe,
        /// <summary>
        /// The user is addending the event
        /// </summary>
        attending,
        /// <summary>
        /// The user declined the event invitation
        /// </summary>
        declined
    }
}
