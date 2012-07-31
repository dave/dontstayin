using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Web;
using JSON;
using Facebook_Graph_Toolkit.FacebookObjects;

namespace Facebook_Graph_Toolkit {
    /// <summary>
    /// The class should be inherited in a Master Page in a Facebook Iframe Application
    /// </summary>
    public class GraphApiMasterPage : System.Web.UI.MasterPage{

        /// <summary>
        /// Whether the user is required to authorize the application to view this page
        /// </summary>
        public bool RequireLogin = false;
        /// <summary>
        /// The user will be prompted to grant these Extended Permissions at authorization
        /// </summary>
        public string ExtendedPermissions = "";

        private GraphApi.Api _Api=null;
        /// <summary>
        ///Stores the current Access Token and provides methods to make Api calls
        /// </summary>
        public GraphApi.Api Api {
            get {
                if (_Api == null) throw new Facebook_Graph_Toolkit.RequiedLoginException();
                return _Api;
            }
        }

        /// <summary>
        /// Determine whether the user has authorized the application
        /// </summary>
        public bool IsAuthorized { get; private set; }
        public BasicUserData UserData { get; private set; }

        /// <summary>
        /// Event handler of 'OnInit' event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e) {
            base.OnInit(e);

            if (!IsPostBack) {
                string SignedRequest = HttpContext.Current.Request["signed_request"];
                if (SignedRequest == null) {
                    throw new FacebookException("Cannot find Facebook POST data as expected. Make sure you're viewing this page in a Facebook Iframe Application.");
                }
                JsonObject JO = Helpers.Generic.ParseSignedRequest(SignedRequest);
                if (JO["user_id"] == null) IsAuthorized = false;
                else IsAuthorized = true;
                _Api = new GraphApi.Api((string)JO["oauth_token"], Helpers.Generic.UnixTimestampToDateTime((double)JO["expires"]));
                UserData = new BasicUserData((JsonObject)JO["user"]);

                Page.Cache["GraphApi"] = _Api;
            } else {
                _Api = (GraphApi.Api)Page.Cache["GraphApi"];
            }

            if (!IsAuthorized && RequireLogin) {
                FacebookGraphToolkitConfiguration Config = (FacebookGraphToolkitConfiguration)WebConfigurationManager.GetSection("FacebookGraphToolkitConfiguration");
                string RedirectUrl = Config._FacebookAppAddress + Config._PostAuthorizeRedirectURL;
                string AuthorizeUrl = String.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}", Config._FacebookAppID, RedirectUrl, ExtendedPermissions);
                Helpers.IframeHelper.IframeRedirect(AuthorizeUrl, false);
            }
        }
    }
}
