//using System;
//using System.Collections;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.Services;
//using System.Web.Services.Protocols;
//using System.Xml.Linq;
//using Spotted.Support.Comet;
//using System.Threading;

//namespace Spotted.WebServices.CometAutoComplete
//{
//    /// <summary>
//    /// Summary description for $codebehindclassname$
//    /// </summary>
//    [WebService(Namespace = "http://tempuri.org/")]
//    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//    public class CometTimeServer : CometAsyncHttpHandler
//    {


//        public override System.Collections.Generic.IEnumerable<Action> GetActions()
//        {
//            yield return () =>
//            {
//                for (int i = 0; i < 6 && httpContext.Response.IsClientConnected; i++)
//                {
//                    WriteMessage(DateTime.Now.ToString());
//                    Thread.Sleep(5000);
//                }
//            };
//        }

//        public override void HandleActionException(Exception ex, Action action)
//        {
//            WriteMessage(ex.Message);
//        }
//    }
//}
