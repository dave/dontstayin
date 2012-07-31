using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using Js.Controls.ChatClient.Shared;

namespace Js.Controls.ChatClient
{
	public class ChatClient
	{
		public static void Send(String message, String roomGuidString, String lastItemGuidString, Int32 sessionID, String pageUrl, StateStub[] roomState, ChatClientRefreshStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["message"] = message;
			p["roomGuidString"] = roomGuidString;
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"Send",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((RefreshStub)((Dictionary<string, object>)data)["d"], userContext, "Send");
				};
			jQuery.Ajax(o);
		}

		public static void ResetSessionAndGetState(Boolean isFirstRequest, String lastItemGuidString, Int32 sessionID, String lastActionTicks, String pageUrl, StateStub[] roomState, ChatClientGetStateStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["isFirstRequest"] = isFirstRequest;
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["lastActionTicks"] = lastActionTicks;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"ResetSessionAndGetState",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((GetStateStub)((Dictionary<string, object>)data)["d"], userContext, "ResetSessionAndGetState");
				};
			jQuery.Ajax(o);
		}

		public static void DeleteArchive(String roomGuid, String lastItemGuidString, Int32 sessionID, String lastActionTicks, String pageUrl, StateStub[] roomState, ChatClientDeleteArchiveStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["roomGuid"] = roomGuid;
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["lastActionTicks"] = lastActionTicks;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"DeleteArchive",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((DeleteArchiveStub)((Dictionary<string, object>)data)["d"], userContext, "DeleteArchive");
				};
			jQuery.Ajax(o);
		}

		public static void GetArchive(String roomGuid, String lastItemGuidString, Int32 sessionID, String lastActionTicks, String pageUrl, StateStub[] roomState, ChatClientArchiveStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["roomGuid"] = roomGuid;
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["lastActionTicks"] = lastActionTicks;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetArchive",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((ArchiveStub)((Dictionary<string, object>)data)["d"], userContext, "GetArchive");
				};
			jQuery.Ajax(o);
		}

		public static void Refresh(Boolean isFirstRequest, String lastItemGuidString, Int32 sessionID, String lastActionTicks, String pageUrl, StateStub[] roomState, ChatClientRefreshStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["isFirstRequest"] = isFirstRequest;
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["lastActionTicks"] = lastActionTicks;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"Refresh",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((RefreshStub)((Dictionary<string, object>)data)["d"], userContext, "Refresh");
				};
			jQuery.Ajax(o);
		}

		public static void MoreInfo(String roomGuid, String lastItemGuidString, Int32 sessionID, String lastActionTicks, String pageUrl, StateStub[] roomState, ChatClientMoreInfoStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["roomGuid"] = roomGuid;
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["lastActionTicks"] = lastActionTicks;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"MoreInfo",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((MoreInfoStub)((Dictionary<string, object>)data)["d"], userContext, "MoreInfo");
				};
			jQuery.Ajax(o);
		}

		public static void Pin(String clientID, String roomGuid, String lastItemGuidString, Int32 sessionID, String lastActionTicks, String pageUrl, StateStub[] roomState, ChatClientPinStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["clientID"] = clientID;
			p["roomGuid"] = roomGuid;
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["lastActionTicks"] = lastActionTicks;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"Pin",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PinStub)((Dictionary<string, object>)data)["d"], userContext, "Pin");
				};
			jQuery.Ajax(o);
		}

		public static void SwitchPhotoRoom(String clientID, String roomGuid, String lastItemGuidString, Int32 sessionID, String lastActionTicks, String pageUrl, StateStub[] roomState, ChatClientPinStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["clientID"] = clientID;
			p["roomGuid"] = roomGuid;
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["lastActionTicks"] = lastActionTicks;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"SwitchPhotoRoom",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((PinStub)((Dictionary<string, object>)data)["d"], userContext, "SwitchPhotoRoom");
				};
			jQuery.Ajax(o);
		}

		public static void RePin(String clientID, String roomGuid, String lastItemGuidString, Int32 sessionID, String lastActionTicks, String pageUrl, StateStub[] roomState, ChatClientRefreshStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["clientID"] = clientID;
			p["roomGuid"] = roomGuid;
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["lastActionTicks"] = lastActionTicks;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"RePin",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((RefreshStub)((Dictionary<string, object>)data)["d"], userContext, "RePin");
				};
			jQuery.Ajax(o);
		}

		public static void UnPin(String clientID, String roomGuid, String lastItemGuidString, Int32 sessionID, String lastActionTicks, String pageUrl, StateStub[] roomState, ChatClientUnPinStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["clientID"] = clientID;
			p["roomGuid"] = roomGuid;
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["lastActionTicks"] = lastActionTicks;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"UnPin",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((UnPinStub)((Dictionary<string, object>)data)["d"], userContext, "UnPin");
				};
			jQuery.Ajax(o);
		}

		public static void Star(String clientID, String roomGuid, Boolean starred, String lastItemGuidString, Int32 sessionID, String lastActionTicks, String pageUrl, StateStub[] roomState, ChatClientRefreshStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["clientID"] = clientID;
			p["roomGuid"] = roomGuid;
			p["starred"] = starred;
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["lastActionTicks"] = lastActionTicks;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"Star",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((RefreshStub)((Dictionary<string, object>)data)["d"], userContext, "Star");
				};
			jQuery.Ajax(o);
		}

		public static void StoreUpdatedRoomListOrder(String lastItemGuidString, Int32 sessionID, String lastActionTicks, String pageUrl, StateStub[] roomState, ChatClientRefreshStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["lastItemGuidString"] = lastItemGuidString;
			p["sessionID"] = sessionID;
			p["lastActionTicks"] = lastActionTicks;
			p["pageUrl"] = pageUrl;
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"StoreUpdatedRoomListOrder",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((RefreshStub)((Dictionary<string, object>)data)["d"], userContext, "StoreUpdatedRoomListOrder");
				};
			jQuery.Ajax(o);
		}

		public static void StoreState(StateStub[] roomState, ChatClientBooleanWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["roomState"] = roomState;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"StoreState",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Boolean)((Dictionary<string, object>)data)["d"], userContext, "StoreState");
				};
			jQuery.Ajax(o);
		}

		public static void RandomWait(Int32 min, Int32 max, ChatClientVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["min"] = min;
			p["max"] = max;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"RandomWait",
				"/WebServices/Controls/ChatClient/ChatClient.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((object)((Dictionary<string, object>)data)["d"], userContext, "RandomWait");
				};
			jQuery.Ajax(o);
		}
	}
	public delegate void ChatClientRefreshStubWebServiceSuccessCallback(RefreshStub result, object userContext, string methodName);
	public delegate void ChatClientGetStateStubWebServiceSuccessCallback(GetStateStub result, object userContext, string methodName);
	public delegate void ChatClientDeleteArchiveStubWebServiceSuccessCallback(DeleteArchiveStub result, object userContext, string methodName);
	public delegate void ChatClientArchiveStubWebServiceSuccessCallback(ArchiveStub result, object userContext, string methodName);
	public delegate void ChatClientMoreInfoStubWebServiceSuccessCallback(MoreInfoStub result, object userContext, string methodName);
	public delegate void ChatClientPinStubWebServiceSuccessCallback(PinStub result, object userContext, string methodName);
	public delegate void ChatClientUnPinStubWebServiceSuccessCallback(UnPinStub result, object userContext, string methodName);
	public delegate void ChatClientBooleanWebServiceSuccessCallback(Boolean result, object userContext, string methodName);
	public delegate void ChatClientVoidWebServiceSuccessCallback(object nullObject, object userContext, string methodName);
}

