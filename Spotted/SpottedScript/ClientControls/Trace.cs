using System;
using System.DHTML;
using Sys.Net;
using System.Diagnostics;


namespace Utils
{

	public static class Trace
	{

		
		public static void Write(string message)
		{
#if DEBUG
			//DivElement traceWindow = TraceObjects.traceWindow;
			//if (traceWindow == null)
			//{
			//    traceWindow = (DivElement)Document.CreateElement("DIV");
			//    traceWindow.Style.Position = "absolute";
			//    traceWindow.Style.BorderBottomColor = "#000000";
			//    traceWindow.Style.BorderBottomWidth = "1px";
			//    traceWindow.Style.BorderBottomStyle = "solid";
			//    traceWindow.Style.Top = "10px";
			//    traceWindow.Style.Left = "800px";
			//    traceWindow.Style.Height = "400px";
			//    traceWindow.Style.Width = "400px";
			//    traceWindow.Style.Overflow = "auto";
			//    traceWindow.Style.ZIndex = 1000;
			//    traceWindow.Style.Opacity = "0.8";
			//    Document.Body.AppendChild(traceWindow);
			//    TraceObjects.traceWindow = traceWindow;
			//}
			//DivElement messageDiv = (DivElement)Document.CreateElement("DIV");
			//messageDiv.InnerHTML = message;
			//messageDiv.Style.BorderBottom = "solid 1px Gray";
			//traceWindow.InsertBefore(messageDiv, traceWindow.FirstChild);
#endif
		}
		public static void WebServiceFailure(WebServiceError error, object userContext, string methodName)
		{
			Write("Message: " + error.Message + "<br>Type: " + error.ExceptionType + "<br>Stack trace: " + error.StackTrace + "<br>Status code: " + error.StatusCode + "<br>Timed out: " + error.TimedOut);
		}
	}
	static class TraceObjects
	{
		internal static DivElement traceWindow;
	}
}
