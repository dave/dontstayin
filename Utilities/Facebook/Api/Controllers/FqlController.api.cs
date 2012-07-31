using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Facebook.Api.Controllers
{
    public class FqlController : FacebookApiController
    {
        public FqlController(IFacebookContext facebookContext) : 
                base(facebookContext) {
        }

        public FacebookResponse<XElement> QueryXml(String fql)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("query", fql);
            var response = this.ExecuteRequest<XElement>("Fql.query", args);
            return response;
        }

        public FacebookJsonResponse<XElement> QueryJson(String fql)
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("query", fql);
            var response = this.ExecuteRequest<XElement>("Fql.query", args);
            return new FacebookJsonResponse<XElement>(response);
        }        

        public FacebookResponse<TValue> Query<TValue>(String fql)
            where TValue : class, IFacebookObject
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("query", fql);
            var response = this.ExecuteRequest<TValue>("Fql.query", args);
            return response;
        }
    }
}
