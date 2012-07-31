using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;
using System.Web;

namespace Facebook_Graph_Toolkit.GraphApi {
    public class AuthInfo {
        public JsonObject JO;

        public string AccessToken {
            get {
                if (!IsAuthorized) throw new RequiedLoginException();
                return (string)JO["oauth_token"];
            }
        }

        public bool IsAuthorized {
            get {
                if (JO["user_id"] == null) return false;
                return true;
            }
        }

        public string UserID {
            get {
                if (!IsAuthorized) throw new RequiedLoginException();
                return (string)JO["user_id"];
            }
        }

        public AuthInfo() {
            string SignedRequest = HttpContext.Current.Request["signed_request"];
            if (SignedRequest == null) {
                throw new FacebookException("Cannot find Facebook POST data as expected. Make sure you're viewing this page in Facebook Iframe.");
            }
            int SeperatorIndex = SignedRequest.IndexOf(".");
            string data = SignedRequest.Substring(SeperatorIndex + 1);
            string JSONString = Helper.Base64UrlDecode(data);
            JO = new JsonObject(JSONString);
        }
    }
}
