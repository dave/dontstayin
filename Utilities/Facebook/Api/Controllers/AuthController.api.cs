using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Facebook;

namespace Facebook.Api.Controllers {
    
    
    public partial class AuthController : FacebookApiController {
               
        /// <summary>Returns the session key bound to an auth_token, as returned by <a href="/index.php/Auth.createToken" title="Auth.createToken">auth.createToken</a> or in the callback URL.</summary>
        /// <param name="authToken">The token returned by <a href="/index.php/Auth.createToken" title="Auth.createToken">auth.createToken</a> and passed into login.php</param>
        public FacebookResponse<SessionInfo> GetSession(String authToken) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("auth_token", authToken);
            var response = this.ExecuteRequest<SessionInfo>("Auth.getSession", args);
            if (!response.IsError) this.FacebookContext.InitSession(response.Value);
            return response;
        }
        
        /// <summary>Returns the session key bound to an auth_token, as returned by <a href="/index.php/Auth.createToken" title="Auth.createToken">auth.createToken</a> or in the callback URL.</summary>
        /// <param name="generateSessionSecret">Whether to generate a temporary session secret associated with this session.  This is for use only with non-infinite sessions, for applications that want to use a client-side component without exposing the application secret.  Note that the app secret will continue to be used for all server-side calls, for security reasons.</param>
        /// <param name="authToken">The token returned by <a href="/index.php/Auth.createToken" title="Auth.createToken">auth.createToken</a> and passed into login.php</param>
        public FacebookResponse<SessionInfo> GetSession(Boolean generateSessionSecret, String authToken) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("generate_session_secret", generateSessionSecret);
            args.Add("auth_token", authToken);
            var response = this.ExecuteRequest<SessionInfo>("Auth.getSession", args);
            if (!response.IsError) this.FacebookContext.Session.Init(response.Value);
            return response;
        }
    }
}
