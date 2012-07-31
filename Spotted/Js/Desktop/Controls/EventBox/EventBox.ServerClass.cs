using System;
using System.Html;
using Js.Library;

namespace Js.Controls.EventBox
{
	public class ServerClass
	{

		#region Members
		Controller Controller;
		#endregion

		#region Event handlers
		public EventHandler GotEventPage = null;
		public EventHandler GotGenericException = null;
		#endregion

		#region public Server(Controller controller)
		public ServerClass(Controller controller)
		{
			Controller = controller;
		}
		#endregion

		#region GetEventPage
		public void GetEventPage(string key)
		{
			Service.GetEventPage(key, GetEventPageSuccessCallback, GetEventPageFailureCallback, "", 2000);
		}
		#region GetEventPageSuccessCallback
		public void GetEventPageSuccessCallback(EventPageStub page, object userContext, string methodName)
		{
			if (page != null)
			{
				if (GotEventPage != null)
					GotEventPage(page, null);
			}
		}
		#endregion
		#region GetEventPageFailureCallback
		public void GetEventPageFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			//Script.Alert(error.Message);

			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

		}
		#endregion
		#endregion

	}

	#region GotErrorEventArgs
	public class GotExceptionEventArgs : EventArgs
	{
		public WebServiceError Error;
		public string Method;
		public GotExceptionEventArgs(WebServiceError error, string method)
		{
			Error = error;
			Method = method;
		}
	}
	#endregion

}
