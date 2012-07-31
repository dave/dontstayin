using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using System.Web;
using FacebookGraphToolkit.FacebookObjects;

namespace FacebookGraphToolkit {
    /// <summary>
    /// Inherit this class in a page that will be displayed in an Page tab
    /// </summary>
    public class TabPage : System.Web.UI.Page{
        private JsonObject data;
        private GraphApi.Api _Api=null;
        /// <summary>
        ///Stores the current Access Token and provides methods to make Api calls
        /// </summary>
        public GraphApi.Api Api {
            get {
                return _Api;
            }
        }

        /// <summary>
        /// Basic data about the current view of this page at a Page tab
        /// </summary>
        public BasicPageData PageData { get { return new BasicPageData((JsonObject)data["page"]); } }
        /// <summary>
        /// Basic data about the user who is browsing this Iframe Application
        /// </summary>
        public BasicUserData UserData { get { return new BasicUserData((JsonObject)data["user"]); } }

        /// <summary>
        /// OnLoad method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e) {
            string SignedRequest = HttpContext.Current.Request["signed_request"];
            if (SignedRequest == null) {
                throw new FacebookException("Cannot find Facebook POST data as expected. Make sure you're viewing this page in an Iframe Application Tab of a Facebook Page.");
            }
            data = Helpers.Generic.ParseSignedRequest(SignedRequest);

            if (data["oauth_token"] != null) {
                _Api = new GraphApi.Api((string)data["oauth_token"], Helpers.Generic.UnixTimestampToDateTime((int)data["expires"]));
            }

            base.OnLoad(e);
        }
    }
}
