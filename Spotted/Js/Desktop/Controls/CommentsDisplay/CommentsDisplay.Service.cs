using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using Js.Controls.CommentsDisplay;

namespace Js.Controls.CommentsDisplay
{
	public class Service
	{
		public static void GetThreadComments(Int32 threadK, Int32 pageNumber, Boolean getCommentsOnly, ServiceCommentResultWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["threadK"] = threadK;
			p["pageNumber"] = pageNumber;
			p["getCommentsOnly"] = getCommentsOnly;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetThreadComments",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((CommentResult)((Dictionary<string, object>)data)["d"], userContext, "GetThreadComments");
				};
			jQuery.Ajax(o);
		}

		public static void LolAtComment(Int32 commentK, ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["commentK"] = commentK;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"LolAtComment",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((String)((Dictionary<string, object>)data)["d"], userContext, "LolAtComment");
				};
			jQuery.Ajax(o);
		}

		public static void DeleteComment(Int32 commentK, ServiceBooleanWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["commentK"] = commentK;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"DeleteComment",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((Boolean)((Dictionary<string, object>)data)["d"], userContext, "DeleteComment");
				};
			jQuery.Ajax(o);
		}

		public static void SetThreadUsr(Int32 threadK, Int32 page, ServiceVoidWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["threadK"] = threadK;
			p["page"] = page;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"SetThreadUsr",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((object)((Dictionary<string, object>)data)["d"], userContext, "SetThreadUsr");
				};
			jQuery.Ajax(o);
		}

		public static void CreateNewThreadInGroup(Int32 groupK, Int32 discussableObjectType, Int32 discussableObjectK, String duplicateGuid, String rawCommentHtml, Boolean formatting, Boolean isNews, String[] inviteUsrOptions, Boolean isPrivate, ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["groupK"] = groupK;
			p["discussableObjectType"] = discussableObjectType;
			p["discussableObjectK"] = discussableObjectK;
			p["duplicateGuid"] = duplicateGuid;
			p["rawCommentHtml"] = rawCommentHtml;
			p["formatting"] = formatting;
			p["isNews"] = isNews;
			p["inviteUsrOptions"] = inviteUsrOptions;
			p["isPrivate"] = isPrivate;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"CreateNewThreadInGroup",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((String)((Dictionary<string, object>)data)["d"], userContext, "CreateNewThreadInGroup");
				};
			jQuery.Ajax(o);
		}

		public static void CreatePrivateThread(Int32 discussableObjectType, Int32 discussableObjectK, String duplicateGuid, String rawCommentHtml, Boolean formatting, String[] inviteUsrOptions, Boolean isSealed, ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["discussableObjectType"] = discussableObjectType;
			p["discussableObjectK"] = discussableObjectK;
			p["duplicateGuid"] = duplicateGuid;
			p["rawCommentHtml"] = rawCommentHtml;
			p["formatting"] = formatting;
			p["inviteUsrOptions"] = inviteUsrOptions;
			p["isSealed"] = isSealed;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"CreatePrivateThread",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((String)((Dictionary<string, object>)data)["d"], userContext, "CreatePrivateThread");
				};
			jQuery.Ajax(o);
		}

		public static void CreateReply(Int32 discussableObjectType, Int32 discussableObjectK, Int32 threadK, String duplicateGuid, String rawCommentHtml, Boolean formatting, Int32 lastKnownCommentK, String[] inviteUsrOptions, ServiceCommentStubArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["discussableObjectType"] = discussableObjectType;
			p["discussableObjectK"] = discussableObjectK;
			p["threadK"] = threadK;
			p["duplicateGuid"] = duplicateGuid;
			p["rawCommentHtml"] = rawCommentHtml;
			p["formatting"] = formatting;
			p["lastKnownCommentK"] = lastKnownCommentK;
			p["inviteUsrOptions"] = inviteUsrOptions;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"CreateReply",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((CommentStub[])((Dictionary<string, object>)data)["d"], userContext, "CreateReply");
				};
			jQuery.Ajax(o);
		}

		public static void CreateNewPublicThread(Int32 discussableObjectType, Int32 discussableObjectK, String duplicateGuid, String rawCommentHtml, Boolean formatting, Boolean isNews, String[] inviteUsrOptions, ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["discussableObjectType"] = discussableObjectType;
			p["discussableObjectK"] = discussableObjectK;
			p["duplicateGuid"] = duplicateGuid;
			p["rawCommentHtml"] = rawCommentHtml;
			p["formatting"] = formatting;
			p["isNews"] = isNews;
			p["inviteUsrOptions"] = inviteUsrOptions;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"CreateNewPublicThread",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((String)((Dictionary<string, object>)data)["d"], userContext, "CreateNewPublicThread");
				};
			jQuery.Ajax(o);
		}

		public static void CreatePublicThread(Int32 discussableObjectType, Int32 discussableObjectK, String duplicateGuid, String rawCommentHtml, Boolean formatting, Boolean isNews, String[] inviteUsrOptions, ServiceCommentStubWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["discussableObjectType"] = discussableObjectType;
			p["discussableObjectK"] = discussableObjectK;
			p["duplicateGuid"] = duplicateGuid;
			p["rawCommentHtml"] = rawCommentHtml;
			p["formatting"] = formatting;
			p["isNews"] = isNews;
			p["inviteUsrOptions"] = inviteUsrOptions;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"CreatePublicThread",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((CommentStub)((Dictionary<string, object>)data)["d"], userContext, "CreatePublicThread");
				};
			jQuery.Ajax(o);
		}

		public static void GetNewGuid(ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			

			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetNewGuid",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((String)((Dictionary<string, object>)data)["d"], userContext, "GetNewGuid");
				};
			jQuery.Ajax(o);
		}

		public static void CleanHtml(String html, ServiceStringWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["html"] = html;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"CleanHtml",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((String)((Dictionary<string, object>)data)["d"], userContext, "CleanHtml");
				};
			jQuery.Ajax(o);
		}

		public static void GetPreviewHtml(Int32 previewType, String rawCommentHtml, Boolean formatting, ServiceStringArrayWebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			p["previewType"] = previewType;
			p["rawCommentHtml"] = rawCommentHtml;
			p["formatting"] = formatting;


			jQueryAjaxOptions o = WebServiceHelper.Options(
				"GetPreviewHtml",
				"/WebServices/Controls/CommentsDisplay/Service.asmx",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success((String[])((Dictionary<string, object>)data)["d"], userContext, "GetPreviewHtml");
				};
			jQuery.Ajax(o);
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

