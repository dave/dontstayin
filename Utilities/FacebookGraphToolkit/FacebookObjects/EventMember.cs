using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// The name, id, and rsvp of a user to an event
    /// </summary>
    public class EventMember {
        JsonObject data;
        internal EventMember(JsonObject JO) {
            data = JO;
        }
        /// <summary>
        /// The user's name
        /// </summary>
        public string name { get { return (string)data["name"]; } }
        /// <summary>
        /// The user's id
        /// </summary>
        public string id { get { return (string)data["id"]; } }
        /// <summary>
        /// The user's rsvp to this event
        /// </summary>
        public Rsvp rsvp_status {
            get {
                string rsvp = (string)data["rsvp_status"];
                switch (rsvp) {
                    case "not_replied":
                        return Rsvp.not_replied;
                    case "unsure":
                        return Rsvp.maybe;
                    case "attending":
                        return Rsvp.attending;
                    default:
                        return Rsvp.declined;
                }
            }
        }
    }
}
