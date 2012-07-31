using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;
using Sys.Net;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class Error : Html
	{
		string Method;
		string ExceptionType;
		string Message;
		string StackTrace;
		int StatusCode;
		bool TimedOut;
		public Error(WebServiceError err, string method, Controller parent, string roomGuid, int serverRequestIndex)
			: base(new ItemStub(Math.Round(Math.Random() * 100000).ToString(), ItemType.Error, "0", roomGuid), parent, serverRequestIndex)
		{
			this.Method = method;
			this.ExceptionType = err.ExceptionType;
			this.Message = err.Message;
			this.StackTrace = err.StackTrace;
			this.TimedOut = err.TimedOut;
			this.StatusCode = err.StatusCode;
		}

		public override void AppendHtml(StringBuilder sb)
		{
			sb.Append("<p>ERROR during " + Method + ":<br />");
			if (ExceptionType.Length > 0)
				sb.Append("<small>" + ExceptionType + "</small><br />");
			if (Message.Length > 0)
				sb.Append(Message + "<br />");
			if (StatusCode != 0)
				sb.Append("<small>Status: " + StatusCode.ToString() + "</small><br />");
			if (TimedOut)
				sb.Append("<small>(timed out)</small>");
			sb.Append("</p>");
		}
	}
	
}
