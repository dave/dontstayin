//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.ChatClientTest
{
	public partial class View
		 : Js.BlankUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element ChatClient {get {if (_ChatClient == null) {_ChatClient = (Element)Document.GetElementById(clientId + "_ChatClient");}; return _ChatClient;}} private Element _ChatClient;
		public jQueryObject ChatClientJ {get {if (_ChatClientJ == null) {_ChatClientJ = jQuery.Select("#" + clientId + "_ChatClient");}; return _ChatClientJ;}} private jQueryObject _ChatClientJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Js.Controls.ChatClient.Controller NavChatClient {get {return (Js.Controls.ChatClient.Controller) Script.Eval(clientId + "_NavChatClientController");}}
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
