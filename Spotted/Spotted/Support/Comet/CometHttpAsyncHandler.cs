
//using System;
//using System.Web;
//using System.Web.UI;
//using System.Collections.Generic;
//using Microsoft.JScript;
//using System.Threading;
//using System.Linq;
//using Bobs;
//namespace Spotted.Support.Comet
//{
//    public abstract class CometAsyncHttpHandler : AsyncHandler
//    {
//        const string Prebuffer = "<!--COMET PREBUFFER-->";
//        public abstract IEnumerable<Action> GetActions();
//        public abstract void HandleActionException(Exception ex, Action action);
//        protected HttpContext httpContext;
//        protected Usr currentUsr;
//        protected void WriteMessage(string message)
//        {
//            if (httpContext.Response.IsClientConnected)
//            {
//                httpContext.Response.Write("<script>window.frameElement.receive(\"" + GlobalObject.escape(message) + "\");</script>");
//                httpContext.Response.Flush();
//            }
//        }

	 
//        protected override void BeginProcessRequest()
//        {
//            httpContext = HttpContext.Current;
//            currentUsr = Usr.Current;
//            httpContext.Response.Buffer = true;
//            httpContext.Response.BufferOutput = true;
//            for (int i = 0; i < 25; i++)
//            {
//                httpContext.Response.Write(Prebuffer);
//            }
//            httpContext.Response.Flush();
//        }
		
//        protected override void LongRunningProcess()
//        {
//            ActionBatchRunner runner = new ActionBatchRunner
//            (
//                GetActions(),
//                HandleActionException
//            );
//            runner.Run();
//        }
//        protected override void EndProcessRequest()
//        {
//            if (httpContext.Response.IsClientConnected)
//            {
//                httpContext.Response.Write("<script>window.frameElement.notifyComplete();</script>");
//                httpContext.Response.Flush();
//            }
//            httpContext.Response.End();
//        }

//    }
//}
