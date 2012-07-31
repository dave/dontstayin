using Sys.Net;
using System;
using SpottedScript.Pages.MapBrowser;
using SpottedScript.Controls.ChatClient.Shared;
using SpottedScript.Controls.MultiBuddyChooser;
using SpottedScript.Controls.CommentsDisplay;
using ScriptSharpLibrary;
using SpottedScript.Controls.PhotoControl;
using SpottedScript.Controls.EventBox.Shared;
using SpottedScript.Controls.Banners.Generator;
using SpottedScript.Controls.LatestChat;
using SpottedScript.Controls.PlacesChooser;
using SpottedScript.Controls.EventCreator;
using SpottedScript.Controls.TaggingControl;
using SpottedScript.Controls.VenueCreator;
namespace Spotted.WebServices.Pages.MapBrowser.Tab
{
	public class Service
	{
		public static WebRequest GetEvents(Int32 firstRowIndex,Int32 lastRowIndex,Double north,Double south,Double east,Double west,Boolean showFuture,Boolean showPast,Boolean orderDesc,String musicTypeK,ServiceMapItemArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["firstRowIndex"] = firstRowIndex;
			_parameters["lastRowIndex"] = lastRowIndex;
			_parameters["north"] = north;
			_parameters["south"] = south;
			_parameters["east"] = east;
			_parameters["west"] = west;
			_parameters["showFuture"] = showFuture;
			_parameters["showPast"] = showPast;
			_parameters["orderDesc"] = orderDesc;
			_parameters["musicTypeK"] = musicTypeK;
			return WebServiceProxy.Invoke(@"/WebServices/Pages/MapBrowser/Tab/Service.asmx", "GetEvents", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetVenues(Int32 firstRowIndex,Int32 lastRowIndex,Double north,Double south,Double east,Double west,ServiceMapItemArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["firstRowIndex"] = firstRowIndex;
			_parameters["lastRowIndex"] = lastRowIndex;
			_parameters["north"] = north;
			_parameters["south"] = south;
			_parameters["east"] = east;
			_parameters["west"] = west;
			return WebServiceProxy.Invoke(@"/WebServices/Pages/MapBrowser/Tab/Service.asmx", "GetVenues", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ServiceMapItemArrayWebServiceSuccessCallback(MapItem[] result, object userContext, string methodName);
}

namespace Spotted
{
	public class GenericPage
	{
		public static WebRequest ClientRequest(String typeName,String methodName,Object[] args,GenericPageObjectWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["typeName"] = typeName;
			_parameters["methodName"] = methodName;
			_parameters["args"] = args;
			return WebServiceProxy.Invoke(@"/GenericPage.asmx", "ClientRequest", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void GenericPageObjectWebServiceSuccessCallback(Object result, object userContext, string methodName);
}

namespace Spotted.WebServices
{
	public class ChatService
	{
		public static WebRequest Send(String message,String roomGuidString,String lastItemGuidString,Int32 sessionID,String pageUrl,StateStub[] roomState,ChatServiceRefreshStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["message"] = message;
			_parameters["roomGuidString"] = roomGuidString;
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "Send", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest ResetSessionAndGetState(Boolean isFirstRequest,String lastItemGuidString,Int32 sessionID,String lastActionTicks,String pageUrl,StateStub[] roomState,ChatServiceGetStateStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["isFirstRequest"] = isFirstRequest;
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["lastActionTicks"] = lastActionTicks;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "ResetSessionAndGetState", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest DeleteArchive(String roomGuid,String lastItemGuidString,Int32 sessionID,String lastActionTicks,String pageUrl,StateStub[] roomState,ChatServiceDeleteArchiveStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["roomGuid"] = roomGuid;
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["lastActionTicks"] = lastActionTicks;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "DeleteArchive", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetArchive(String roomGuid,String lastItemGuidString,Int32 sessionID,String lastActionTicks,String pageUrl,StateStub[] roomState,ChatServiceArchiveStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["roomGuid"] = roomGuid;
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["lastActionTicks"] = lastActionTicks;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "GetArchive", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest Refresh(Boolean isFirstRequest,String lastItemGuidString,Int32 sessionID,String lastActionTicks,String pageUrl,StateStub[] roomState,ChatServiceRefreshStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["isFirstRequest"] = isFirstRequest;
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["lastActionTicks"] = lastActionTicks;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "Refresh", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest MoreInfo(String roomGuid,String lastItemGuidString,Int32 sessionID,String lastActionTicks,String pageUrl,StateStub[] roomState,ChatServiceMoreInfoStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["roomGuid"] = roomGuid;
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["lastActionTicks"] = lastActionTicks;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "MoreInfo", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest Pin(String clientID,String roomGuid,String lastItemGuidString,Int32 sessionID,String lastActionTicks,String pageUrl,StateStub[] roomState,ChatServicePinStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["clientID"] = clientID;
			_parameters["roomGuid"] = roomGuid;
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["lastActionTicks"] = lastActionTicks;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "Pin", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest SwitchPhotoRoom(String clientID,String roomGuid,String lastItemGuidString,Int32 sessionID,String lastActionTicks,String pageUrl,StateStub[] roomState,ChatServicePinStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["clientID"] = clientID;
			_parameters["roomGuid"] = roomGuid;
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["lastActionTicks"] = lastActionTicks;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "SwitchPhotoRoom", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest RePin(String clientID,String roomGuid,String lastItemGuidString,Int32 sessionID,String lastActionTicks,String pageUrl,StateStub[] roomState,ChatServiceRefreshStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["clientID"] = clientID;
			_parameters["roomGuid"] = roomGuid;
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["lastActionTicks"] = lastActionTicks;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "RePin", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest UnPin(String clientID,String roomGuid,String lastItemGuidString,Int32 sessionID,String lastActionTicks,String pageUrl,StateStub[] roomState,ChatServiceUnPinStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["clientID"] = clientID;
			_parameters["roomGuid"] = roomGuid;
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["lastActionTicks"] = lastActionTicks;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "UnPin", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest Star(String clientID,String roomGuid,Boolean starred,String lastItemGuidString,Int32 sessionID,String lastActionTicks,String pageUrl,StateStub[] roomState,ChatServiceRefreshStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["clientID"] = clientID;
			_parameters["roomGuid"] = roomGuid;
			_parameters["starred"] = starred;
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["lastActionTicks"] = lastActionTicks;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "Star", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest StoreUpdatedRoomListOrder(String lastItemGuidString,Int32 sessionID,String lastActionTicks,String pageUrl,StateStub[] roomState,ChatServiceRefreshStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["lastItemGuidString"] = lastItemGuidString;
			_parameters["sessionID"] = sessionID;
			_parameters["lastActionTicks"] = lastActionTicks;
			_parameters["pageUrl"] = pageUrl;
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "StoreUpdatedRoomListOrder", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest StoreState(StateStub[] roomState,ChatServiceBooleanWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["roomState"] = roomState;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "StoreState", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest RandomWait(Int32 min,Int32 max,ChatServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["min"] = min;
			_parameters["max"] = max;
			return WebServiceProxy.Invoke(@"/WebServices/ChatService.asmx", "RandomWait", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ChatServiceRefreshStubWebServiceSuccessCallback(RefreshStub result, object userContext, string methodName);
	public delegate void ChatServiceGetStateStubWebServiceSuccessCallback(GetStateStub result, object userContext, string methodName);
	public delegate void ChatServiceDeleteArchiveStubWebServiceSuccessCallback(DeleteArchiveStub result, object userContext, string methodName);
	public delegate void ChatServiceArchiveStubWebServiceSuccessCallback(ArchiveStub result, object userContext, string methodName);
	public delegate void ChatServiceMoreInfoStubWebServiceSuccessCallback(MoreInfoStub result, object userContext, string methodName);
	public delegate void ChatServicePinStubWebServiceSuccessCallback(PinStub result, object userContext, string methodName);
	public delegate void ChatServiceUnPinStubWebServiceSuccessCallback(UnPinStub result, object userContext, string methodName);
	public delegate void ChatServiceBooleanWebServiceSuccessCallback(Boolean result, object userContext, string methodName);
	public delegate void ChatServiceVoidWebServiceSuccessCallback(object nullObject, object userContext, string methodName);
}

namespace Spotted.WebServices.Controls.MultiBuddyChooser
{
	public class Service
	{
		public static WebRequest GetPlacesAndMusicTypes(ServiceGetMusicTypesAndPlacesResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			return WebServiceProxy.Invoke(@"/WebServices/Controls/MultiBuddyChooser/Service.asmx", "GetPlacesAndMusicTypes", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest ResolveUsrsFromMultiBuddyChooserValues(String[] values,ServiceDictionaryWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["values"] = values;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/MultiBuddyChooser/Service.asmx", "ResolveUsrsFromMultiBuddyChooserValues", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetBuddiesSelectListHtml(ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			return WebServiceProxy.Invoke(@"/WebServices/Controls/MultiBuddyChooser/Service.asmx", "GetBuddiesSelectListHtml", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest CreateUsrFromEmailAndReturnK(String textEnteredByUser,ServiceInt32WebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["textEnteredByUser"] = textEnteredByUser;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/MultiBuddyChooser/Service.asmx", "CreateUsrFromEmailAndReturnK", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest CreateUsrsFromEmails(String spaceSeparatedListOfEmailAddresses,Boolean addAsBuddies,ServiceInt32WebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["spaceSeparatedListOfEmailAddresses"] = spaceSeparatedListOfEmailAddresses;
			_parameters["addAsBuddies"] = addAsBuddies;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/MultiBuddyChooser/Service.asmx", "CreateUsrsFromEmails", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ServiceGetMusicTypesAndPlacesResultWebServiceSuccessCallback(GetMusicTypesAndPlacesResult result, object userContext, string methodName);
	public delegate void ServiceDictionaryWebServiceSuccessCallback(Dictionary result, object userContext, string methodName);
	public delegate void ServiceStringWebServiceSuccessCallback(String result, object userContext, string methodName);
	public delegate void ServiceInt32WebServiceSuccessCallback(Int32 result, object userContext, string methodName);
}

namespace Spotted.WebServices.Controls.CommentsDisplay
{
	public class Service
	{
		public static WebRequest GetThreadComments(Int32 threadK,Int32 pageNumber,Boolean getCommentsOnly,ServiceCommentResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["threadK"] = threadK;
			_parameters["pageNumber"] = pageNumber;
			_parameters["getCommentsOnly"] = getCommentsOnly;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "GetThreadComments", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest LolAtComment(Int32 commentK,ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["commentK"] = commentK;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "LolAtComment", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest DeleteComment(Int32 commentK,ServiceBooleanWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["commentK"] = commentK;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "DeleteComment", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest SetThreadUsr(Int32 threadK,Int32 page,ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["threadK"] = threadK;
			_parameters["page"] = page;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "SetThreadUsr", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest CreateNewThreadInGroup(Int32 groupK,Int32 discussableObjectType,Int32 discussableObjectK,String duplicateGuid,String rawCommentHtml,Boolean formatting,Boolean isNews,String[] inviteUsrOptions,Boolean isPrivate,ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["groupK"] = groupK;
			_parameters["discussableObjectType"] = discussableObjectType;
			_parameters["discussableObjectK"] = discussableObjectK;
			_parameters["duplicateGuid"] = duplicateGuid;
			_parameters["rawCommentHtml"] = rawCommentHtml;
			_parameters["formatting"] = formatting;
			_parameters["isNews"] = isNews;
			_parameters["inviteUsrOptions"] = inviteUsrOptions;
			_parameters["isPrivate"] = isPrivate;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "CreateNewThreadInGroup", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest CreatePrivateThread(Int32 discussableObjectType,Int32 discussableObjectK,String duplicateGuid,String rawCommentHtml,Boolean formatting,String[] inviteUsrOptions,Boolean isSealed,ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["discussableObjectType"] = discussableObjectType;
			_parameters["discussableObjectK"] = discussableObjectK;
			_parameters["duplicateGuid"] = duplicateGuid;
			_parameters["rawCommentHtml"] = rawCommentHtml;
			_parameters["formatting"] = formatting;
			_parameters["inviteUsrOptions"] = inviteUsrOptions;
			_parameters["isSealed"] = isSealed;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "CreatePrivateThread", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest CreateReply(Int32 discussableObjectType,Int32 discussableObjectK,Int32 threadK,String duplicateGuid,String rawCommentHtml,Boolean formatting,Int32 lastKnownCommentK,String[] inviteUsrOptions,ServiceCommentStubArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["discussableObjectType"] = discussableObjectType;
			_parameters["discussableObjectK"] = discussableObjectK;
			_parameters["threadK"] = threadK;
			_parameters["duplicateGuid"] = duplicateGuid;
			_parameters["rawCommentHtml"] = rawCommentHtml;
			_parameters["formatting"] = formatting;
			_parameters["lastKnownCommentK"] = lastKnownCommentK;
			_parameters["inviteUsrOptions"] = inviteUsrOptions;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "CreateReply", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest CreateNewPublicThread(Int32 discussableObjectType,Int32 discussableObjectK,String duplicateGuid,String rawCommentHtml,Boolean formatting,Boolean isNews,String[] inviteUsrOptions,ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["discussableObjectType"] = discussableObjectType;
			_parameters["discussableObjectK"] = discussableObjectK;
			_parameters["duplicateGuid"] = duplicateGuid;
			_parameters["rawCommentHtml"] = rawCommentHtml;
			_parameters["formatting"] = formatting;
			_parameters["isNews"] = isNews;
			_parameters["inviteUsrOptions"] = inviteUsrOptions;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "CreateNewPublicThread", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest CreatePublicThread(Int32 discussableObjectType,Int32 discussableObjectK,String duplicateGuid,String rawCommentHtml,Boolean formatting,Boolean isNews,String[] inviteUsrOptions,ServiceCommentStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["discussableObjectType"] = discussableObjectType;
			_parameters["discussableObjectK"] = discussableObjectK;
			_parameters["duplicateGuid"] = duplicateGuid;
			_parameters["rawCommentHtml"] = rawCommentHtml;
			_parameters["formatting"] = formatting;
			_parameters["isNews"] = isNews;
			_parameters["inviteUsrOptions"] = inviteUsrOptions;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "CreatePublicThread", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetNewGuid(ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "GetNewGuid", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest CleanHtml(String html,ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["html"] = html;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "CleanHtml", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetPreviewHtml(Int32 previewType,String rawCommentHtml,Boolean formatting,ServiceStringArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["previewType"] = previewType;
			_parameters["rawCommentHtml"] = rawCommentHtml;
			_parameters["formatting"] = formatting;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/CommentsDisplay/Service.asmx", "GetPreviewHtml", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ServiceCommentResultWebServiceSuccessCallback(CommentResult result, object userContext, string methodName);
	public delegate void ServiceStringWebServiceSuccessCallback(String result, object userContext, string methodName);
	public delegate void ServiceBooleanWebServiceSuccessCallback(Boolean result, object userContext, string methodName);
	public delegate void ServiceVoidWebServiceSuccessCallback(object nullObject, object userContext, string methodName);
	public delegate void ServiceCommentStubArrayWebServiceSuccessCallback(CommentStub[] result, object userContext, string methodName);
	public delegate void ServiceCommentStubWebServiceSuccessCallback(CommentStub result, object userContext, string methodName);
	public delegate void ServiceStringArrayWebServiceSuccessCallback(String[] result, object userContext, string methodName);
}

namespace Spotted.WebServices
{
	public class AutoComplete
	{
		public static WebRequest GetTags(String text,Int32 maxNumberOfItemsToGet,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["text"] = text;
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetTags", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetTagSearchString(String prefixText,Int32 count,AutoCompleteStringArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["prefixText"] = prefixText;
			_parameters["count"] = count;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetTagSearchString", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetGroupMembers(Int32 maxNumberOfItemsToGet,String text,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["text"] = text;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetGroupMembers", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetUsrsPublic(String text,Int32 maxNumberOfItemsToGet,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["text"] = text;
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetUsrsPublic", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetBuddiesThenUsrs(String text,Int32 maxNumberOfItemsToGet,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["text"] = text;
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetBuddiesThenUsrs", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetBuddies(String text,Int32 maxNumberOfItemsToGet,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["text"] = text;
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetBuddies", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetBrands(String text,Int32 maxNumberOfItemsToGet,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["text"] = text;
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetBrands", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetPromotersWithK(String text,Int32 maxNumberOfItemsToGet,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["text"] = text;
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetPromotersWithK", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetUsersWithK(String text,Int32 maxNumberOfItemsToGet,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["text"] = text;
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetUsersWithK", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetPlacesEnabled(Int32 maxNumberOfItemsToGet,String text,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["text"] = text;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetPlacesEnabled", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetVenuesFull(Int32 maxNumberOfItemsToGet,String text,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["text"] = text;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetVenuesFull", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetVenues(String text,Int32 maxNumberOfItemsToGet,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["text"] = text;
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetVenues", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetEvents(Int32 maxNumberOfItemsToGet,String text,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["text"] = text;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetEvents", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetPlaces(Int32 maxNumberOfItemsToGet,String text,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["text"] = text;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetPlaces", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetSiteSearchResults(String text,Int32 maxNumberOfItemsToGet,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["text"] = text;
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetSiteSearchResults", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetGroups(String text,Int32 maxNumberOfItemsToGet,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["text"] = text;
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetGroups", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetGroupsNoBrands(Int32 maxNumberOfItemsToGet,String text,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["text"] = text;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetGroupsNoBrands", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetObjects(String text,Int32 maxNumberOfItemsToGet,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["text"] = text;
			_parameters["maxNumberOfItemsToGet"] = maxNumberOfItemsToGet;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetObjects", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetCountries(Int32 get,String text,Dictionary parameters,AutoCompleteSuggestionArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["get"] = get;
			_parameters["text"] = text;
			_parameters["parameters"] = parameters;
			return WebServiceProxy.Invoke(@"/WebServices/AutoComplete.asmx", "GetCountries", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void AutoCompleteSuggestionArrayWebServiceSuccessCallback(Suggestion[] result, object userContext, string methodName);
	public delegate void AutoCompleteStringArrayWebServiceSuccessCallback(String[] result, object userContext, string methodName);
}

namespace Spotted.WebServices.Controls.PhotoControl
{
	public class Service
	{
		public static WebRequest GetRecentVideos(Int32 pageNumber,ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["pageNumber"] = pageNumber;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "GetRecentVideos", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetPhotosByEventAndPage(Int32 eventK,Int32 pageNumber,ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["eventK"] = eventK;
			_parameters["pageNumber"] = pageNumber;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "GetPhotosByEventAndPage", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetPhotosByGalleryAndPage(Int32 galleryK,Int32 pageNumber,ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["galleryK"] = galleryK;
			_parameters["pageNumber"] = pageNumber;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "GetPhotosByGalleryAndPage", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetPhotosByArticle(Int32 articleK,Int32 pageNumber,ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["articleK"] = articleK;
			_parameters["pageNumber"] = pageNumber;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "GetPhotosByArticle", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetPhotosOfUsr(Int32 usrK,Int32 pageNumber,Int32 spottedByUsrK,ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["usrK"] = usrK;
			_parameters["pageNumber"] = pageNumber;
			_parameters["spottedByUsrK"] = spottedByUsrK;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "GetPhotosOfUsr", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetFavouritePhotosOfUsr(Int32 usrK,Int32 pageNumber,ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["usrK"] = usrK;
			_parameters["pageNumber"] = pageNumber;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "GetFavouritePhotosOfUsr", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetPhotosByGroup(Int32 groupK,Int32 pageNumber,ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["groupK"] = groupK;
			_parameters["pageNumber"] = pageNumber;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "GetPhotosByGroup", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetPhotosByTag(Int32 tagK,Int32 pageNumber,ServicePhotoResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["tagK"] = tagK;
			_parameters["pageNumber"] = pageNumber;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "GetPhotosByTag", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest SetCurrentUsrSpottedInPhoto(Int32 photoK,Boolean isInPhoto,ServiceStringArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["photoK"] = photoK;
			_parameters["isInPhoto"] = isInPhoto;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "SetCurrentUsrSpottedInPhoto", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest SetUsrSpottedInPhoto(Int32 spottedUsrK,Int32 photoK,Boolean isInPhoto,ServiceStringArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["spottedUsrK"] = spottedUsrK;
			_parameters["photoK"] = photoK;
			_parameters["isInPhoto"] = isInPhoto;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "SetUsrSpottedInPhoto", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest SetAsPhotoOfWeek(Int32 photoK,Boolean isPhotoOfWeek,String photoOfWeekCaption,ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["photoK"] = photoK;
			_parameters["isPhotoOfWeek"] = isPhotoOfWeek;
			_parameters["photoOfWeekCaption"] = photoOfWeekCaption;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "SetAsPhotoOfWeek", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest SetIsFavouritePhoto(Int32 photoK,Boolean isFavourite,ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["photoK"] = photoK;
			_parameters["isFavourite"] = isFavourite;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "SetIsFavouritePhoto", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest IncrementViews(Int32 photoK,ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["photoK"] = photoK;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "IncrementViews", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetBanners(String placeholderClientID,ServiceBannerStubArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["placeholderClientID"] = placeholderClientID;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "GetBanners", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest RegisterBannerHit(Int32 bannerK,ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["bannerK"] = bannerK;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "RegisterBannerHit", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest SetAsCompetitionGroupPhoto(Int32 photoK,Boolean isCompetitionPhoto,ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["photoK"] = photoK;
			_parameters["isCompetitionPhoto"] = isCompetitionPhoto;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PhotoControl/Service.asmx", "SetAsCompetitionGroupPhoto", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ServicePhotoResultWebServiceSuccessCallback(PhotoResult result, object userContext, string methodName);
	public delegate void ServiceStringArrayWebServiceSuccessCallback(String[] result, object userContext, string methodName);
	public delegate void ServiceVoidWebServiceSuccessCallback(object nullObject, object userContext, string methodName);
	public delegate void ServiceBannerStubArrayWebServiceSuccessCallback(BannerStub[] result, object userContext, string methodName);
}

namespace Spotted.WebServices.Controls.EventBox
{
	public class Service
	{
		public static WebRequest GetEventPage(String key,ServiceEventPageStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["key"] = key;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/EventBox/Service.asmx", "GetEventPage", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ServiceEventPageStubWebServiceSuccessCallback(EventPageStub result, object userContext, string methodName);
}

namespace Spotted.WebServices.Controls.Banners.Generator
{
	public class Service
	{
		public static WebRequest GetBanner(Int32 positionAsInt,String relevantPlacesCsv,String relevantMusicTypesCsv,ServiceBannerRenderInfoWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["positionAsInt"] = positionAsInt;
			_parameters["relevantPlacesCsv"] = relevantPlacesCsv;
			_parameters["relevantMusicTypesCsv"] = relevantMusicTypesCsv;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/Banners/Generator/Service.asmx", "GetBanner", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ServiceBannerRenderInfoWebServiceSuccessCallback(BannerRenderInfo result, object userContext, string methodName);
}

namespace Spotted.WebServices.Controls.LatestChat
{
	public class Service
	{
		public static WebRequest GetThreads(Int32 objectType,Int32 objectK,Int32 threadsCount,Boolean hasGroupObjectFilter,ServiceThreadStubArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["objectType"] = objectType;
			_parameters["objectK"] = objectK;
			_parameters["threadsCount"] = threadsCount;
			_parameters["hasGroupObjectFilter"] = hasGroupObjectFilter;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/LatestChat/Service.asmx", "GetThreads", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ServiceThreadStubArrayWebServiceSuccessCallback(ThreadStub[] result, object userContext, string methodName);
}

namespace Spotted.WebServices.Controls.PlacesChooser
{
	public class Service
	{
		public static WebRequest GetPlaces(Double north,Double south,Double east,Double west,Int32 maximumNumber,ServicePlaceStubArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["north"] = north;
			_parameters["south"] = south;
			_parameters["east"] = east;
			_parameters["west"] = west;
			_parameters["maximumNumber"] = maximumNumber;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PlacesChooser/Service.asmx", "GetPlaces", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetSurroundingPlaces(Int32 centredOnPlaceK,Int32 numberOfPlacesToGet,ServicePlaceStubArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["centredOnPlaceK"] = centredOnPlaceK;
			_parameters["numberOfPlacesToGet"] = numberOfPlacesToGet;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/PlacesChooser/Service.asmx", "GetSurroundingPlaces", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ServicePlaceStubArrayWebServiceSuccessCallback(PlaceStub[] result, object userContext, string methodName);
}

namespace Spotted.WebServices.Controls.EventCreator
{
	public class Service
	{
		public static WebRequest AddEvent(DateTime date,Int32 venueK,String name,String shortDetails,Int32[] brands,ServiceEventInfoWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["date"] = date;
			_parameters["venueK"] = venueK;
			_parameters["name"] = name;
			_parameters["shortDetails"] = shortDetails;
			_parameters["brands"] = brands;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/EventCreator/Service.asmx", "AddEvent", false, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ServiceEventInfoWebServiceSuccessCallback(EventInfo result, object userContext, string methodName);
}

namespace Spotted.WebServices.Controls.TaggingControl
{
	public class Service
	{
		public static WebRequest RemoveTagFromPhoto(Int32 tagK,Int32 photoK,ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["tagK"] = tagK;
			_parameters["photoK"] = photoK;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/TaggingControl/Service.asmx", "RemoveTagFromPhoto", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest AddTagToPhoto(String tagText,Int32 photoK,ServiceTagStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["tagText"] = tagText;
			_parameters["photoK"] = photoK;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/TaggingControl/Service.asmx", "AddTagToPhoto", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
		public static WebRequest GetTagsForPhotoKs(Int32[] photoKs,ServiceDictionaryWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["photoKs"] = photoKs;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/TaggingControl/Service.asmx", "GetTagsForPhotoKs", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ServiceVoidWebServiceSuccessCallback(object nullObject, object userContext, string methodName);
	public delegate void ServiceTagStubWebServiceSuccessCallback(TagStub result, object userContext, string methodName);
	public delegate void ServiceDictionaryWebServiceSuccessCallback(Dictionary result, object userContext, string methodName);
}

namespace Spotted.WebServices.Controls.VenueCreator
{
	public class Service
	{
		public static WebRequest CreateVenue(String name,Int32 placeK,String postcode,ServiceVenueInfoWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary _parameters = new Dictionary();
			_parameters["name"] = name;
			_parameters["placeK"] = placeK;
			_parameters["postcode"] = postcode;
			return WebServiceProxy.Invoke(@"/WebServices/Controls/VenueCreator/Service.asmx", "CreateVenue", true, _parameters, (WebServiceSuccessCallback) (object) success, failure, userContext, timeout);
		}
	}
	public delegate void ServiceVenueInfoWebServiceSuccessCallback(VenueInfo result, object userContext, string methodName);
}

