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
namespace Facebook.Desktop {
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Facebook;
    
    
    public partial class FacebookDesktopContext : FacebookContextBase<StaticDictionaryCache, Facebook.Api.SessionInfo> {
        
        public FacebookDesktopContext(String apiKey, String secret) : 
                base(ApplicationType.Desktop, apiKey, secret) {
        }
        
        public FacebookDesktopContext(String apiKey, String secret, String version) : 
                base(ApplicationType.Desktop, apiKey, secret, version) {
        }
    }
}
