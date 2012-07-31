using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;

namespace FacebookGraphToolkit.FacebookObjects {
    /// <summary>
    /// An event of a user
    /// </summary>
    public class UserEvent {
        JsonObject data;
        internal UserEvent(JsonObject JO) {
            data = JO;
        }

        /// <summary>
        /// The event title
        /// </summary>
        public string name { get { return (string)data["name"]; } }
        /// <summary>
        /// The start time of the event
        /// </summary>
        public DateTime start_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["start_time"]);
            }
        }
        /// <summary>
        /// The end time of the event
        /// </summary>
        public DateTime end_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["end_time"]);
            }
        }
        /// <summary>
        /// The location of the event
        /// </summary>
        public string location { get { return (string)data["location"]; } }
        /// <summary>
        /// The event ID
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
