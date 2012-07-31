using System;
using System.DHTML;
using ScriptSharpLibrary;

namespace Net.Comet
{
	public delegate void CometRequestCompleteDelegate();
	public class CometRequest
	{
		public Action OnCometRequestComplete;
		IFrameElement iFrame;
		bool completed = false;
		internal CometRequest(IFrameElement iFrame)
		{
			this.iFrame = iFrame;
		}
		public void Abort()
		{
			if (!completed)
			{

				this.iFrame.Src = "";
				try
				{
					this.iFrame.ParentNode.RemoveChild(this.iFrame);
				}
				catch (Exception)
				{

				}
				
				if (OnCometRequestComplete != null) OnCometRequestComplete();
				completed = true;
			}
		}

		internal void NotifyComplete()
		{
			Abort();
		}
	}
}
