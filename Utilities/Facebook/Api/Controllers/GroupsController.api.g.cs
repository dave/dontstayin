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
    
    
    public class GroupsController : FacebookApiController {
        
        public GroupsController(IFacebookContext facebookContext) : 
                base(facebookContext) {
        }
        
        /// <summary>Returns all visible groups according to the filters specified.</summary>
        public FacebookResponse<FacebookList<Group>> Get() {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            var response = this.ExecuteRequest<FacebookList<Group>>("Groups.get", args);
            return response;
        }
        
        /// <summary>Returns all visible groups according to the filters specified.</summary>
        /// <param name="gids">Filter by this list of group IDs. This is a comma-separated list of GIDs.</param>
        public FacebookResponse<FacebookList<Group>> Get(String gids) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("gids", gids);
            var response = this.ExecuteRequest<FacebookList<Group>>("Groups.get", args);
            return response;
        }
        
        /// <summary>Returns all visible groups according to the filters specified.</summary>
        /// <param name="uid">Filter by groups associated with a user with this UID.</param>
        /// <param name="gids">Filter by this list of group IDs. This is a comma-separated list of GIDs.</param>
        public FacebookResponse<FacebookList<Group>> Get(Int64 uid, String gids) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            args.Add("gids", gids);
            var response = this.ExecuteRequest<FacebookList<Group>>("Groups.get", args);
            return response;
        }
        
        /// <summary>Returns all visible groups according to the filters specified.</summary>
        /// <param name="uid">Filter by groups associated with a user with this UID.</param>
        public FacebookResponse<FacebookList<Group>> Get(Int64 uid) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
            var response = this.ExecuteRequest<FacebookList<Group>>("Groups.get", args);
            return response;
        }
        
        /// <summary>Returns membership list data associated with a group.</summary>
        /// <param name="gid">Group ID for which to retrieve member list.</param>
        public FacebookResponse<GroupMembers> GetMembers(Int64 gid) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("gid", gid);
            var response = this.ExecuteRequest<GroupMembers>("Groups.getMembers", args);
            return response;
        }
    }
}
