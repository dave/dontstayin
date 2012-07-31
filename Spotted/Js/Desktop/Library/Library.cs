using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;

namespace Js.Library
{
	public delegate void ActionObject(object o);
	public delegate void ActionBool(bool parameter);
	public delegate void ActionBoolBool(bool parameter1, bool parameter2);
	public delegate void ActionBoolBoolBool(bool parameter1, bool parameter2, bool parameter3);
	public delegate void ActionString(string parameter);
	public delegate void ActionDictionary(Dictionary<object, object> parameter);
	public delegate void ActionObjectObject(object o1, object o2);
	public delegate void Response(Dictionary<object, object> parameter);
	public delegate void ServerRequest(string type, string method, object[] paramArr, Response onSuccess, Response onFailure);

	#region Misc
	public class Misc
	{

		public static string ObjectToString(object o)
		{
			return objectToString(o, 0);
		}
		static string objectToString(object o, int indent)
		{
			if (o is string)
			{
				return (string)o;
			}
			else if (o is Dictionary<string, object>)
			{
				string s = "\n[";
				Dictionary<string, object> d = (Dictionary<string, object>)o;
				foreach (string key in d.Keys)
				{
					for (int i = 0; i < indent; i++) { s += " "; }
					s += key + ": " + objectToString(d[key], indent + 2) + "\n";
				}
				s += "]";
				return s;
			}
			else
				return "";
		}

		public static string GetPicUrlFromGuid(string guid)
		{
			string s = (string)Script.Eval("StoragePath('" + guid + "');");
			return s;
			//return "http://s" + guid.Substr(0, 1) + ".dontstayin.com/" + guid.Substr(0, 2) + "/" + guid.Substr(2, 2) + "/" + guid + ".jpg";
		}

		public static void Redirect(string url)
		{
			// cannot set location through ScriptSharp?
			Script.Eval("window.location = '" + url + "'");
		}
		public static void AddHoverText(Element el, string hoverText)
		{
			el.AddEventListener("mouseover", delegate { Stt(hoverText); }, false);
			el.AddEventListener("mouseout", delegate { Htm(); }, false);
		}



		public static void RedirectToAnchor(string anchorName)
		{
			Window.Location.Hash = anchorName;
		}

		public static void ShowWaitingCursor()
		{
			Script.Eval("ShowWaitingCursor();");
		}
		public static void HideWaitingCursor()
		{
			Script.Eval("HideWaitingCursor();");
		}

		internal static void Stt(string p)
		{
			Script.Eval("stt('" + p + "');");
		}

		internal static void Htm()
		{
			Script.Eval("htm();");
		}

		public static bool BrowserIsFirefox
		{
			get
			{
				return jQuery.Browser.Mozilla;
			}
		}
		public static bool BrowserIsIE
		{
			get
			{
				return jQuery.Browser.MSIE;
			}
		}
		public static float BrowserVersion
		{
			get
			{
				float version = 1.0F;
				try
				{
					version = float.Parse(jQuery.Browser.Version);
				}
				catch { }
				return version;
			}
		}

		public static Action CombineAction(Action runFirst, Action runSecond)
		{
			Action composite = new Action(
				delegate()
				{
					if (runFirst != null) runFirst();
					if (runSecond != null) runSecond();
				}
			);
			return composite;
		}
		public static EventHandler CombineEventHandler(EventHandler runFirst, EventHandler runSecond)
		{
			EventHandler composite = new EventHandler(
				delegate(object sender, EventArgs e)
				{
					if (runFirst != null) runFirst(sender, e);
					if (runSecond != null) runSecond(sender, e);
				}
			);
			return composite;
		}

		internal static void Debug()
		{
			Script.Literal("debugger;");
		}
	}
	#endregion

	#region IntEventArgs
	public class IntEventArgs : EventArgs
	{
		public int value;
		public IntEventArgs(int value)
		{
			this.value = value;
		}
	}
	#endregion

	#region Web services
	public delegate void WebServiceSuccessCallback(object result, object userContext, string methodName);
	public delegate void WebServiceFailureCallback(WebServiceError error, object userContext, string methodName);
	public sealed class WebServiceError
	{
		public string ExceptionType;
		public string Message;
		public string StackTrace;
		public int StatusCode;
		public bool TimedOut;

		public WebServiceError(string exceptionType, string message, string stackTrace, int statusCode, bool timedOut)
		{
			ExceptionType = exceptionType; Message = message; StackTrace = stackTrace; StatusCode = statusCode; TimedOut = timedOut;
		}
	}
	public static class WebServiceHelper
	{
		public static jQueryAjaxOptions Options(
			string methodName,
			string url,
			Dictionary<string, object> parameters,
			WebServiceFailureCallback failure,
			object userContext,
			int timeout)
		{
			jQueryAjaxOptions o = new jQueryAjaxOptions();
			o.Url = url + "/" + methodName;
			o.Timeout = timeout;
			o.Type = "POST";
			o.Async = true;
			o.Cache = false;
			o.ContentType = "application/json; charset=utf-8";
			o.Data = JSON.stringify(parameters);
			o.DataType = "json";
			o.Error =
				delegate(jQueryXmlHttpRequest request, string error, Exception exception)
				{
					failure(
						new WebServiceError(
							exception.GetType().ToString(),
							error,
							exception.ToString(),
							request.Status,
							request.Status == 408),
						userContext,
						methodName);
				};
			return o;
		}
	}
	#endregion

	#region Trace
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
	#endregion

	public class StringBuilderJs
	{
		Array stringArray;
		public StringBuilderJs()
		{
			stringArray = new Array();
		}
		public void Append(string s)
		{
			stringArray[stringArray.Length] = s;
		}
		public override string ToString()
		{
			return stringArray.Join("");
		}
		public void AppendAttribute(string name, string value)
		{
			stringArray[stringArray.Length] = " ";
			stringArray[stringArray.Length] = name;
			stringArray[stringArray.Length] = "=\"";
			stringArray[stringArray.Length] = value.Replace("\"", "&#34;");
			stringArray[stringArray.Length] = "\"";
		}
	}

}




