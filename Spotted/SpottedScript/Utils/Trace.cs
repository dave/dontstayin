//using System;
//using Sys.Net;
//using Sys.UI;
//using System.DHTML;

//namespace SpottedScript.Utils
//{
//    [GlobalMethods]
//    static class Trace
//    {
		
		
		 
		
//        public static void Write(string message)
//        {
//            DivElement traceWindow = TraceObjects.traceWindow;
//            if (traceWindow == null)
//            {
//                traceWindow = (DivElement) Document.CreateElement("DIV");
//                traceWindow.Style.Position = "absolute";
//                traceWindow.Style.Top = "10px";
//                traceWindow.Style.Left = "10px";
//                traceWindow.Style.Height = "100px";
//                traceWindow.Style.Width = "800px";
//                traceWindow.Style.ZIndex = 1000;
//                traceWindow.Style.Opacity = "0.8";
//                Document.Body.AppendChild(traceWindow);
//                TraceObjects.traceWindow = traceWindow;
//            }
//            traceWindow.InnerHTML = message + "<br>" + traceWindow.InnerHTML;
//        }
//        public static void WebServiceFailure(WebServiceError error, object userContext, string MethodName)
//        {
//            Write(error.Message);
//        }
//    }
//    static class TraceObjects
//    {
//        internal static DivElement traceWindow;
//    }
//}
