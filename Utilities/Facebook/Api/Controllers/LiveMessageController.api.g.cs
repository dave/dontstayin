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
    
    
    public class LiveMessageController : FacebookApiController {
        
        public LiveMessageController(IFacebookContext facebookContext) : 
                base(facebookContext) {
        }
        
        /// <summary>Sends a "message" directly to a user's browser, which can be handled in <a href="/index.php/FBJS" title="FBJS">FBJS</a>.</summary>
        /// <param name="recipient">The <a href="/index.php/User_ID" title="User ID">user ID</a> of the message recipient.</param>
        /// <param name="eventName">Name of the "event" for which messages will be sent and received (max length: 128 bytes).  Use this <code>event_name</code> when you initialize the <a href="/index.php/FBJS_LiveMessage" title="FBJS LiveMessage">LiveMessage FBJS object</a> so your recipient can receive the message.</param>
        /// <param name="message">A JSON-encoded string of the message to send (max length: 1024 bytes).</param>
        public FacebookResponse<Boolean> Send(Int64 recipient, String eventName, String message) {
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("recipient", recipient);
            args.Add("event_name", eventName);
            args.Add("message", message);
            var response = this.ExecuteRequest<Boolean>("LiveMessage.send", args);
            return response;
        }
    }
}
