using System.DHTML;
using System;
using ScriptSharpLibrary;
namespace Net.Comet
{
	public delegate void ProcessMessageDelegate(string message);
	public class CometProxy
	{
		public static CometRequest Invoke(string url, ProcessMessageDelegate ProcessMessage, Action onComplete)
		{
			
			IFrameElement hiddenIFrame = (IFrameElement)Document.CreateElement("IFRAME");
			CometRequest cometRequest = new CometRequest(hiddenIFrame);
			Action notifyComplete = new Action
			(
				delegate() 
				{
					cometRequest.NotifyComplete();
				}
			);
			cometRequest.OnCometRequestComplete = onComplete;
			Script.Literal("{0}.receive = function(message){{{1}(unescape(message));}};", hiddenIFrame, ProcessMessage);
			Script.Literal("{0}.notifyComplete = {1};", hiddenIFrame, notifyComplete);
			hiddenIFrame.Src = url;
			hiddenIFrame.Style.Visibility = "hidden";
			Document.Body.AppendChild(hiddenIFrame);
			return cometRequest;
		}
	}
}
