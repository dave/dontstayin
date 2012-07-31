//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// ------------------------------------------------------------------------------
// <auto-generated>
//     This API code was generated by the DanDoes.NET Facebook API Generator.
//     Facebook.Api.Generator v1.0.3412.20062
//     
//     The following documents were used to generate this code:
//		Facebook API Object Schema:	http://api.facebook.com/1.0/facebook.xsd
//		Facebook API Wiki:			http://wiki.developers.facebook.com/index.php/API
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Facebook.Api.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Facebook;
    
    
    public class StatusController : FacebookApiController {
        
        public StatusController(IFacebookContext facebookContext) : 
                base(facebookContext) {
        }
        
        /// <summary>Updates a user's Facebook status through your application.  This is a streamlined version of <a href="/index.php/Users.setStatus" title="Users.setStatus">users.setStatus</a>.</summary>
        /// <param name="status">The status message to set.<br/><b>Note:</b> The maximum message length is 160 characters; messages longer than that limit will be truncated and appended with "...".</param>
        public FacebookResponse<Boolean> Set(String status) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("status", status);
            var response = this.ExecuteRequest<Boolean>("Status.set", args);
            return response;
        }
        
        /// <summary>Updates a user's Facebook status through your application.  This is a streamlined version of <a href="/index.php/Users.setStatus" title="Users.setStatus">users.setStatus</a>.</summary>
        /// <param name="uid">The <a href="/index.php/User_ID" title="User ID">user ID</a> of the user whose status you are setting. If this parameter is not specified, then it defaults to the session user. <br/><b>Note:</b> This parameter applies only to Web applications and is required by them only if the <code>session_key</code> is not specified. Facebook ignores this parameter if it is passed by a desktop application.</param>
        /// <param name="status">The status message to set.<br/><b>Note:</b> The maximum message length is 160 characters; messages longer than that limit will be truncated and appended with "...".</param>
        public FacebookResponse<Boolean> Set(Int64 uid, String status) {
            if ((this.FacebookContext.ApplicationType & ApplicationType.Website)!= ApplicationType.Website)throw new InvalidOperationException("This overload cannot be called in this context");
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            args.Add("status", status);
            var response = this.ExecuteRequest<Boolean>("Status.set", args);
            return response;
        }
        
        /// <summary>Returns the user's current and most recent statuses.</summary>
        public FacebookResponse<FacebookList<UserStatus>> Get() {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            var response = this.ExecuteRequest<FacebookList<UserStatus>>("Status.get", args);
            return response;
        }
        
        /// <summary>Returns the user's current and most recent statuses.</summary>
        /// <param name="limit">The number of status messages you want to return. <i>(Default value is 100.)</i></param>
        public FacebookResponse<FacebookList<UserStatus>> Get(Int64 limit) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("limit", limit);
            var response = this.ExecuteRequest<FacebookList<UserStatus>>("Status.get", args);
            return response;
        }
        
        /// <summary>Returns the user's current and most recent statuses.</summary>
        /// <param name="uid">The <a href="/index.php/User_ID" title="User ID">user ID</a> of the user whose status messages you want to retrieve.</param>
        public FacebookResponse<FacebookList<UserStatus>> GetByUid(Int64 uid) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            var response = this.ExecuteRequest<FacebookList<UserStatus>>("Status.get", args);
            return response;
        }
        
        /// <summary>Returns the user's current and most recent statuses.</summary>
        /// <param name="uid">The <a href="/index.php/User_ID" title="User ID">user ID</a> of the user whose status messages you want to retrieve.</param>
        /// <param name="limit">The number of status messages you want to return. <i>(Default value is 100.)</i></param>
        public FacebookResponse<FacebookList<UserStatus>> GetByUid(Int64 uid, Int64 limit) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            args.Add("limit", limit);
            var response = this.ExecuteRequest<FacebookList<UserStatus>>("Status.get", args);
            return response;
        }
    }
}
