using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit.GraphApi {
    /// <summary>
    /// Information about an event on Facebook
    /// </summary>
    public class Event {
        JsonObject data;
        internal Event(JsonObject JO) {
            data = JO;
        }

        #region properties
        /// <summary>
        /// The event ID
        /// </summary>
        public string id { get { return (string)data["id"]; } }
        /// <summary>
        /// The profile that created the event
        /// </summary>
        public NameIDPair owner { get { return new NameIDPair((JsonObject)data["owner"]); } }
        /// <summary>
        /// The event title
        /// </summary>
        public string name { get { return (string)data["name"]; } }
        /// <summary>
        /// The event description
        /// </summary>
        public string description { get { return (string)data["description"]; } }
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
        /// The location for the event
        /// </summary>
        public string location { get { return (string)data["location"]; } }
        /// <summary>
        /// The visibility of the event
        /// </summary>
        public Privacy privacy {
            get {
                string privacy = (string)data["privacy"];
                switch (privacy) {
                    case "OPEN":
                        return Privacy.open;
                    case "CLOSED":
                        return Privacy.closed;
                    default:
                        return Privacy.secret;
                }
            }
        }
        /// <summary>
        /// The last time the event was updated
        /// </summary>
        public DateTime updated_time {
            get {
                return Helpers.Generic.RFC3339ToDateTime((string)data["updated_time"]);
            }
        }
        #endregion

        #region connections
        /// <summary>
        /// Get the event's wall
        /// </summary>
        /// <returns>The event's wall</returns>
        public IList<Post> GetFeed() {
            return Helpers.ApiCaller.GetFeed(id, "");
        }

        /// <summary>
        /// Get a event's wall
        /// </summary>
        /// <returns>The event's wall</returns>
        public static IList<Post> GetFeed(string EventID) {
            return Helpers.ApiCaller.GetFeed(EventID, "");
        }

        /// <summary>
        /// Gets all the users who have not responded to their event invitation yet
        /// </summary>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>users who have not responded to their event invitation yet</returns>
        public IList<EventMember> GetNoReply(string AccessToken) {
            IList<EventMember> _List = new List<EventMember>();

            string URL = string.Format("https://graph.facebook.com/{0}/noreply?access_token={1}", id, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject _JO in dataArray.JsonObjects) {
                _List.Add(new EventMember(_JO));
            }
            return _List;
        }

        /// <summary>
        /// Gets all the users who responded "Maybe" to their event invitation
        /// </summary>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>All the users who responded "Maybe" to their event invitation</returns>
        public IList<EventMember> GetMaybe(string AccessToken) {
            IList<EventMember> _List = new List<EventMember>();

            string URL = string.Format("https://graph.facebook.com/{0}/maybe?access_token={1}", id, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject _JO in dataArray.JsonObjects) {
                _List.Add(new EventMember(_JO));
            }
            return _List;
        }

        /// <summary>
        /// Gets all the users who have been invited to this event
        /// </summary>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>All the users who have been invited to this event</returns>
        public IList<EventMember> GetInvited(string AccessToken) {
            IList<EventMember> _List = new List<EventMember>();

            string URL = string.Format("https://graph.facebook.com/{0}/invited?access_token={1}", id, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject _JO in dataArray.JsonObjects) {
                _List.Add(new EventMember(_JO));
            }
            return _List;
        }

        /// <summary>
        /// Gets all the users who are attending this event
        /// </summary>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>All the users who are attending this event</returns>
        public IList<EventMember> GetAttending(string AccessToken) {
            IList<EventMember> _List = new List<EventMember>();

            string URL = string.Format("https://graph.facebook.com/{0}/attending?access_token={1}", id, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject _JO in dataArray.JsonObjects) {
                _List.Add(new EventMember(_JO));
            }
            return _List;
        }

        /// <summary>
        /// Gets all the users who declined their invitation to this event
        /// </summary>
        /// <param name="AccessToken">Access Token</param>
        /// <returns>All the users who declined their invitation to this event</returns>
        public IList<EventMember> GetDeclined(string AccessToken) {
            IList<EventMember> _List = new List<EventMember>();

            string URL = string.Format("https://graph.facebook.com/{0}/declined?access_token={1}", id, AccessToken);
            JsonObject JO = new JsonObject(Helpers.WebResponseHelper.GetWebResponse(URL));
            JsonArray dataArray = (JsonArray)JO["data"];
            foreach (JsonObject _JO in dataArray.JsonObjects) {
                _List.Add(new EventMember(_JO));
            }
            return _List;
        }

        #endregion
    }
}
