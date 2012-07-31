//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.MixmagVote.Repost
{
	public class Server {
		public Server(){}
		public void RepostNow(System.Int32 entryK, System.String message, Response response) { object[] paramArr = { entryK, message }; ServerRequest req = (ServerRequest)Script.Eval("PageMethods.ClientRequest"); if (req != null) { try { req("Spotted.MixmagVote.Repost, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "RepostNow", paramArr, response, response); } catch (Exception e) { Dictionary<object, object> d = new Dictionary<object, object>(); d["Exception"] = true; d["ExceptionType"] = "ClientException"; d["Message"] = e.Message; d["StackTrace"] = ""; response(d); } } }
	}
	public partial class View
		 : Js.MixmagVoteUserControl.View
	{
		public string clientId;
		public Server server;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
			this.server = new Server();
		}
		public InputElement EntryK {get {if (_EntryK == null) {_EntryK = (InputElement)Document.GetElementById(clientId + "_EntryK");}; return _EntryK;}} private InputElement _EntryK;
		public jQueryObject EntryKJ {get {if (_EntryKJ == null) {_EntryKJ = jQuery.Select("#" + clientId + "_EntryK");}; return _EntryKJ;}} private jQueryObject _EntryKJ;
		public DivElement Repost1Panel {get {if (_Repost1Panel == null) {_Repost1Panel = (DivElement)Document.GetElementById(clientId + "_Repost1Panel");}; return _Repost1Panel;}} private DivElement _Repost1Panel;
		public jQueryObject Repost1PanelJ {get {if (_Repost1PanelJ == null) {_Repost1PanelJ = jQuery.Select("#" + clientId + "_Repost1Panel");}; return _Repost1PanelJ;}} private jQueryObject _Repost1PanelJ;
		public ImageElement Repost1Img {get {if (_Repost1Img == null) {_Repost1Img = (ImageElement)Document.GetElementById(clientId + "_Repost1Img");}; return _Repost1Img;}} private ImageElement _Repost1Img;
		public jQueryObject Repost1ImgJ {get {if (_Repost1ImgJ == null) {_Repost1ImgJ = jQuery.Select("#" + clientId + "_Repost1Img");}; return _Repost1ImgJ;}} private jQueryObject _Repost1ImgJ;
		public InputElement Repost1FacebookMessageTextbox {get {if (_Repost1FacebookMessageTextbox == null) {_Repost1FacebookMessageTextbox = (InputElement)Document.GetElementById(clientId + "_Repost1FacebookMessageTextbox");}; return _Repost1FacebookMessageTextbox;}} private InputElement _Repost1FacebookMessageTextbox;
		public jQueryObject Repost1FacebookMessageTextboxJ {get {if (_Repost1FacebookMessageTextboxJ == null) {_Repost1FacebookMessageTextboxJ = jQuery.Select("#" + clientId + "_Repost1FacebookMessageTextbox");}; return _Repost1FacebookMessageTextboxJ;}} private jQueryObject _Repost1FacebookMessageTextboxJ;
		public InputElement Repost1Button {get {if (_Repost1Button == null) {_Repost1Button = (InputElement)Document.GetElementById(clientId + "_Repost1Button");}; return _Repost1Button;}} private InputElement _Repost1Button;
		public jQueryObject Repost1ButtonJ {get {if (_Repost1ButtonJ == null) {_Repost1ButtonJ = jQuery.Select("#" + clientId + "_Repost1Button");}; return _Repost1ButtonJ;}} private jQueryObject _Repost1ButtonJ;
		public DivElement RepostConfirmPanel {get {if (_RepostConfirmPanel == null) {_RepostConfirmPanel = (DivElement)Document.GetElementById(clientId + "_RepostConfirmPanel");}; return _RepostConfirmPanel;}} private DivElement _RepostConfirmPanel;
		public jQueryObject RepostConfirmPanelJ {get {if (_RepostConfirmPanelJ == null) {_RepostConfirmPanelJ = jQuery.Select("#" + clientId + "_RepostConfirmPanel");}; return _RepostConfirmPanelJ;}} private jQueryObject _RepostConfirmPanelJ;
		public ImageElement RepostConfirm_Img {get {if (_RepostConfirm_Img == null) {_RepostConfirm_Img = (ImageElement)Document.GetElementById(clientId + "_RepostConfirm_Img");}; return _RepostConfirm_Img;}} private ImageElement _RepostConfirm_Img;
		public jQueryObject RepostConfirm_ImgJ {get {if (_RepostConfirm_ImgJ == null) {_RepostConfirm_ImgJ = jQuery.Select("#" + clientId + "_RepostConfirm_Img");}; return _RepostConfirm_ImgJ;}} private jQueryObject _RepostConfirm_ImgJ;
		public AnchorElement RepostConfirm_Link {get {if (_RepostConfirm_Link == null) {_RepostConfirm_Link = (AnchorElement)Document.GetElementById(clientId + "_RepostConfirm_Link");}; return _RepostConfirm_Link;}} private AnchorElement _RepostConfirm_Link;
		public jQueryObject RepostConfirm_LinkJ {get {if (_RepostConfirm_LinkJ == null) {_RepostConfirm_LinkJ = jQuery.Select("#" + clientId + "_RepostConfirm_Link");}; return _RepostConfirm_LinkJ;}} private jQueryObject _RepostConfirm_LinkJ;
		public InputElement RepostConfirm_YesButton {get {if (_RepostConfirm_YesButton == null) {_RepostConfirm_YesButton = (InputElement)Document.GetElementById(clientId + "_RepostConfirm_YesButton");}; return _RepostConfirm_YesButton;}} private InputElement _RepostConfirm_YesButton;
		public jQueryObject RepostConfirm_YesButtonJ {get {if (_RepostConfirm_YesButtonJ == null) {_RepostConfirm_YesButtonJ = jQuery.Select("#" + clientId + "_RepostConfirm_YesButton");}; return _RepostConfirm_YesButtonJ;}} private jQueryObject _RepostConfirm_YesButtonJ;
		public InputElement RepostConfirm_NoButton {get {if (_RepostConfirm_NoButton == null) {_RepostConfirm_NoButton = (InputElement)Document.GetElementById(clientId + "_RepostConfirm_NoButton");}; return _RepostConfirm_NoButton;}} private InputElement _RepostConfirm_NoButton;
		public jQueryObject RepostConfirm_NoButtonJ {get {if (_RepostConfirm_NoButtonJ == null) {_RepostConfirm_NoButtonJ = jQuery.Select("#" + clientId + "_RepostConfirm_NoButton");}; return _RepostConfirm_NoButtonJ;}} private jQueryObject _RepostConfirm_NoButtonJ;
		public DivElement Repost2Panel {get {if (_Repost2Panel == null) {_Repost2Panel = (DivElement)Document.GetElementById(clientId + "_Repost2Panel");}; return _Repost2Panel;}} private DivElement _Repost2Panel;
		public jQueryObject Repost2PanelJ {get {if (_Repost2PanelJ == null) {_Repost2PanelJ = jQuery.Select("#" + clientId + "_Repost2Panel");}; return _Repost2PanelJ;}} private jQueryObject _Repost2PanelJ;
		public Element DebugOutput {get {if (_DebugOutput == null) {_DebugOutput = (Element)Document.GetElementById(clientId + "_DebugOutput");}; return _DebugOutput;}} private Element _DebugOutput;
		public jQueryObject DebugOutputJ {get {if (_DebugOutputJ == null) {_DebugOutputJ = jQuery.Select("#" + clientId + "_DebugOutput");}; return _DebugOutputJ;}} private jQueryObject _DebugOutputJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element LoadingLabel {get {if (_LoadingLabel == null) {_LoadingLabel = (Element)Document.GetElementById(clientId + "_LoadingLabel");}; return _LoadingLabel;}} private Element _LoadingLabel;
		public jQueryObject LoadingLabelJ {get {if (_LoadingLabelJ == null) {_LoadingLabelJ = jQuery.Select("#" + clientId + "_LoadingLabel");}; return _LoadingLabelJ;}} private jQueryObject _LoadingLabelJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
