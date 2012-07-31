//using System;
//using System.Data;
//using System.Configuration;
//using System.Linq;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
//using System.Threading;

//namespace Spotted.Support.Comet
//{
//    class AsyncRequest
//    {
//        private AsyncRequestState asyncRequestState;

//        public AsyncRequest(AsyncRequestState asyncRequestState)
//        {
//            this.asyncRequestState = asyncRequestState;
//        }

//        public void ProcessRequest()
//        {
//            for (int i = 0; i < 100; i++)
//            {
//                asyncRequestState.HttpContext.Response.Write("asdsdaasdasdasd");
//            }
//            //asyncRequestState.HttpContext.Response.Flush();
//            // This is where the non-cpu-bound activity would take place, such as
//            // accessing a Web Service, polling a slow piece of hardware, or 
//            // performing a lengthy database operation. I put the thread to sleep 
//            // for two seconds to simulate a lengthy operation.
//            while (this.asyncRequestState.HttpContext.Response.IsClientConnected)
//            {
//                Thread.Sleep(2000);

//                asyncRequestState.HttpContext.Response.Write(String.Format(
//                        "<br>AsyncThread, {0}",
//                        Thread.CurrentThread.ManagedThreadId
//                        ));
//                //asyncRequestState.HttpContext.Response.Flush();
//            }
//            // tell asp.net I am finished processing this request
//            asyncRequestState.CompleteRequest();
//        }
//    }
//}
