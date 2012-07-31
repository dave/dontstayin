using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using System.Web.Configuration;
using System.Web;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit {
    /// <summary>
    /// The class should be inherited in a .aspx page in a Facebook Iframe Application
    /// </summary>
    public class GraphApiPage : System.Web.UI.Page{

        /// <summary>
        /// Whether the user is required to authorize the application to view this page
        /// </summary>
        public bool RequireLogin = false;
        /// <summary>
        /// The user will be prompted to grant these Extended Permissions at authorization
        /// </summary>
        public string ExtendedPermissions = "";
        /// <summary>
        /// Whether or not to check if the user has already granted the extended permissions. If not, the library will redirect the user to do so.
        /// </summary>
        public bool CheckExtendedPermissions = false;

        private GraphApi.Api _Api = null;
        /// <summary>
        ///Stores the current Access Token and provides methods to make Api calls
        /// </summary>
        public GraphApi.Api Api {
            get {
                if (_Api == null) throw new FacebookGraphToolkit.RequiedLoginException();
                return _Api;
            }
        }

        /// <summary>
        /// Determine whether the user has authorized the application
        /// </summary>
        public bool IsAuthorized { get; private set; }
        /// <summary>
        /// Basic data about the user who is browsing this Iframe Application
        /// </summary>
        public BasicUserData UserData { get; private set; }

        /// <summary>
        /// Event handler of 'OnInit' event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e) {
            if (!IsPostBack) {
                string SignedRequest = HttpContext.Current.Request["signed_request"];
                if (SignedRequest == null) {
                    throw new FacebookException("Cannot find Facebook POST data as expected. Make sure you're viewing this page in a Facebook Iframe Application.");
                }

                JsonObject JO = Helpers.Generic.ParseSignedRequest(SignedRequest);
                if (JO["user_id"] == null) IsAuthorized = false;
                else {
                    IsAuthorized = true;
                    _Api = new GraphApi.Api((string)JO["oauth_token"], Helpers.Generic.UnixTimestampToDateTime((int)JO["expires"]));

                    Page.Cache["GraphApi"] = _Api;
                }
                UserData = new BasicUserData((JsonObject)JO["user"]);
            } else {
                _Api = (GraphApi.Api)Page.Cache["GraphApi"];
            }

            bool RedirectForExtendedPermission = false;
            if (_Api != null&&CheckExtendedPermissions&&!string.IsNullOrEmpty(ExtendedPermissions.Trim())) {
                JsonArray JA = _Api.Fql(string.Format("SELECT {0} FROM permissions WHERE uid = me()",ExtendedPermissions));
                JsonObject JO = JA.JsonObjects[0];
                foreach (KeyValuePair<string, object> KVP in JO.Properties) {
                    if ((int)KVP.Value == 0) {
                        RedirectForExtendedPermission = true;
                        break;
                    }
                }
            }

            if ((_Api==null && RequireLogin)||RedirectForExtendedPermission) {
                FacebookGraphToolkitConfiguration Config = (FacebookGraphToolkitConfiguration)WebConfigurationManager.GetSection("FacebookGraphToolkitConfiguration");
                string RedirectUrl = Config._FacebookAppAddress + Config._PostAuthorizeRedirectURL;
                string AuthorizeUrl = String.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}", Config._FacebookAppID, RedirectUrl, ExtendedPermissions);
                Helpers.IframeHelper.IframeRedirect(AuthorizeUrl, false);
            }

            base.OnLoad(e);
        }
    }
}
