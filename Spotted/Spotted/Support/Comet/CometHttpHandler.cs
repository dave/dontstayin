
//using System;
//using System.Web;
//using System.Web.UI;
//using System.Collections.Generic;
//using Microsoft.JScript;
//using System.Threading;
//using System.Linq;
//namespace Spotted.Support.Comet
//{
//    public abstract class CometHttpHandler : IHttpHandler
//    {
//        const string Prebuffer = "<!--COMET PREBUFFER-->";
//        public abstract IEnumerable<Action> GetActions();
//        public abstract void HandleActionException(Exception ex, Action action);
//        HttpContext context;
//        protected void WriteMessage(string message)
//        {
//            if (context.Response.IsClientConnected)
//            {
//                context.Response.Write("<script>window.frameElement.receive(\"" + GlobalObject.escape(message) + "\");</script>");
//                context.Response.Flush();
//            }
//        }

//        public void ProcessRequest(HttpContext context)
//        {
//            this.context = context;
//            context.Response.Buffer = true;
//            context.Response.BufferOutput = true;
//            for (int i = 0; i < 25; i++)
//            {
//                context.Response.Write(Prebuffer);
//            }
//            context.Response.Flush();

//            ActionBatchRunner runner = new ActionBatchRunner
//            (
//                GetActions(),
//                HandleActionException
//            );
//            runner.Run();
//            if (context.Response.IsClientConnected)
//            {
//                context.Response.Write("<script>window.frameElement.notifyComplete();</script>");
//                context.Response.Flush();
//            }
//            context.Response.End();
//        }

//        public bool IsReusable { get { return false; } }

//    }
//}
