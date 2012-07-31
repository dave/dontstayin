
namespace Facebook.Api.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Facebook;
    
    /// <summary>
    /// 
    /// </summary>
    public class DashboardController : FacebookApiController {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="facebookContext"></param>
		public DashboardController(IFacebookContext facebookContext) : 
                base(facebookContext) {
        }
        
		/// <summary>
		/// 
		/// </summary>
		/// <param name="uid"></param>
		/// <returns></returns>
		public FacebookResponse<String> IncrementCount(string uid)
		{
            System.Collections.Generic.Dictionary<string, object> args = new System.Collections.Generic.Dictionary<string, object>();
            args.Add("uid", uid);
			var response = this.ExecuteRequest<String>("Dashboard.incrementCount", args);
            return response;
        }
    }
}
