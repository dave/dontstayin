using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit {
    /// <summary>
    /// The Dialog class contains methods that return urls of Facebook dialogs
    /// </summary>
    public static class Dialog {
        /// <summary>
        /// Gets the url that prompts the user to send application requests
        /// </summary>
        /// <param name="message">The message the receiving user will see. Must not exceed 255 characters</param>
        /// <param name="to">A user ID or username who must be a friend of the sender. If this is specified, the user will not have a choice of recipients. If this is omitted, the user will see a friend selector and will be able to select a maximum of 50 recipients.</param>
        /// <param name="Filter">Specifies which users will appear in the friend selector</param>
        /// <param name="redirectaddress">An address relative to the Canvas Page address (http://apps.facebook.com/xxx). User will be redirected to this address after the dialog.</param>
        /// <returns></returns>
        public static string GetAppRequestUrl(string message, string to, AppRequestFilter Filter, string redirectaddress) {
            FacebookGraphToolkitConfiguration Config = (FacebookGraphToolkitConfiguration)ConfigurationManager.GetSection("FacebookGraphToolkitConfiguration");

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            if (Filter == AppRequestFilter.app_non_users) parameters.Add("filters", "['app_non_users']");
            if (Filter == AppRequestFilter.app_users) parameters.Add("filters", "['app_users']");
            parameters.Add("app_id", Config._FacebookAppID);
            parameters.Add("message", message);
            parameters.Add("to", to);
            parameters.Add("redirect_uri", Config._FacebookAppAddress + redirectaddress);
            return Helpers.Generic.BuildUrlQuery("http://www.facebook.com/dialog/apprequests", parameters);;
        }
        /// <summary>
        /// Gets the url that prompts the user to post to the wall
        /// </summary>
        /// <param name="redirectaddress">An address relative to the Canvas Page address (http://apps.facebook.com/xxx). User will be redirected to this address after the dialog.</param>
        /// <param name="from">The ID or username of the user posting the message. If unspecified, default is current user. If specified, must be ID of the user or of a page that the user administers.</param>
        /// <param name="to">The ID or username of the profile that this story will be published to. If unspecified, default is the value of <c>from</c></param>
        /// <param name="message">The message to prefill the text field that the user will type in. To be compliant with Facebook Platform Policies, your application may only set this field if the user manually generated the content earlier in the workflow. Most applications should not set this.</param>
        /// <param name="pictureUrl">The Url to a picture attached to this post.</param>
        /// <param name="source">The Url to a media file (e.g. SWF or video file) attached to this post. If both <c>source</c> and <c>picture</c> is specified, only <c>source</c> is used.</param>
        /// <param name="link">A Link Attachment to this post.</param>
        /// <param name="action">An action link that appears next to "Comment" and "Like".</param>
        /// <returns></returns>
        public static string GetFeedDialogUrl(string redirectaddress, string from, string to, string message, string pictureUrl, string source, LinkAttachment link, PostAction action){
            FacebookGraphToolkitConfiguration Config = (FacebookGraphToolkitConfiguration)ConfigurationManager.GetSection("FacebookGraphToolkitConfiguration");

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("app_id", Config._FacebookAppID);
            parameters.Add("redirect_uri", Config._FacebookAppAddress + redirectaddress);
            parameters.Add("display", "page");
            parameters.Add("from", from);
            parameters.Add("to", to);
            parameters.Add("message", message);
            if (link != null) {
                parameters.Add("link", link.link);
                parameters.Add("name", link.name);
                parameters.Add("caption", link.caption);
                parameters.Add("description", link.description);
            }
            parameters.Add("picture", pictureUrl);
            parameters.Add("source", source);
            if (action != null) {
                parameters.Add("actions", "[{\"name\":\"" + action.name + "\",\"link\":\"" + action.link + "\"}]");
            }
            return Helpers.Generic.BuildUrlQuery("http://www.facebook.com/dialog/feed", parameters);
        }
    }
    /// <summary>
    /// Enumeration of filter settings of an Facebook Application Request
    /// </summary>
    public enum AppRequestFilter {
        /// <summary>
        /// All friends of the current user will appear in the friend selector
        /// </summary>
        all,
        /// <summary>
        /// Only those friends who are using the application will appear in the friend selector
        /// </summary>
        app_users,
        /// <summary>
        /// Only those friends who are not using the application will appear in the friend selector
        /// </summary>
        app_non_users
    }
}
