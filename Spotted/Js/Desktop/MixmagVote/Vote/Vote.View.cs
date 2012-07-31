//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.MixmagVote.Vote
{
	public class Server {
		public Server(){}
		public void VoteNow(System.Int32 entryK, System.Int32 compK, System.String imageUrl, Response response) { object[] paramArr = { entryK, compK, imageUrl }; ServerRequest req = (ServerRequest)Script.Eval("PageMethods.ClientRequest"); if (req != null) { try { req("Spotted.MixmagVote.Vote, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "VoteNow", paramArr, response, response); } catch (Exception e) { Dictionary<object, object> d = new Dictionary<object, object>(); d["Exception"] = true; d["ExceptionType"] = "ClientException"; d["Message"] = e.Message; d["StackTrace"] = ""; response(d); } } }
		public void SaveQuestion(System.Int32 entryK, System.Int32 compK, System.String imageUrl, System.String questionString, Response response) { object[] paramArr = { entryK, compK, imageUrl, questionString }; ServerRequest req = (ServerRequest)Script.Eval("PageMethods.ClientRequest"); if (req != null) { try { req("Spotted.MixmagVote.Vote, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "SaveQuestion", paramArr, response, response); } catch (Exception e) { Dictionary<object, object> d = new Dictionary<object, object>(); d["Exception"] = true; d["ExceptionType"] = "ClientException"; d["Message"] = e.Message; d["StackTrace"] = ""; response(d); } } }
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
		public InputElement CompK {get {if (_CompK == null) {_CompK = (InputElement)Document.GetElementById(clientId + "_CompK");}; return _CompK;}} private InputElement _CompK;
		public jQueryObject CompKJ {get {if (_CompKJ == null) {_CompKJ = jQuery.Select("#" + clientId + "_CompK");}; return _CompKJ;}} private jQueryObject _CompKJ;
		public InputElement ImageUrl {get {if (_ImageUrl == null) {_ImageUrl = (InputElement)Document.GetElementById(clientId + "_ImageUrl");}; return _ImageUrl;}} private InputElement _ImageUrl;
		public jQueryObject ImageUrlJ {get {if (_ImageUrlJ == null) {_ImageUrlJ = jQuery.Select("#" + clientId + "_ImageUrl");}; return _ImageUrlJ;}} private jQueryObject _ImageUrlJ;
		public InputElement EntryK {get {if (_EntryK == null) {_EntryK = (InputElement)Document.GetElementById(clientId + "_EntryK");}; return _EntryK;}} private InputElement _EntryK;
		public jQueryObject EntryKJ {get {if (_EntryKJ == null) {_EntryKJ = jQuery.Select("#" + clientId + "_EntryK");}; return _EntryKJ;}} private jQueryObject _EntryKJ;
		public InputElement PageIdToLike {get {if (_PageIdToLike == null) {_PageIdToLike = (InputElement)Document.GetElementById(clientId + "_PageIdToLike");}; return _PageIdToLike;}} private InputElement _PageIdToLike;
		public jQueryObject PageIdToLikeJ {get {if (_PageIdToLikeJ == null) {_PageIdToLikeJ = jQuery.Select("#" + clientId + "_PageIdToLike");}; return _PageIdToLikeJ;}} private jQueryObject _PageIdToLikeJ;
		public DivElement VoteClosedPanel {get {if (_VoteClosedPanel == null) {_VoteClosedPanel = (DivElement)Document.GetElementById(clientId + "_VoteClosedPanel");}; return _VoteClosedPanel;}} private DivElement _VoteClosedPanel;
		public jQueryObject VoteClosedPanelJ {get {if (_VoteClosedPanelJ == null) {_VoteClosedPanelJ = jQuery.Select("#" + clientId + "_VoteClosedPanel");}; return _VoteClosedPanelJ;}} private jQueryObject _VoteClosedPanelJ;
		public DivElement Vote1Panel {get {if (_Vote1Panel == null) {_Vote1Panel = (DivElement)Document.GetElementById(clientId + "_Vote1Panel");}; return _Vote1Panel;}} private DivElement _Vote1Panel;
		public jQueryObject Vote1PanelJ {get {if (_Vote1PanelJ == null) {_Vote1PanelJ = jQuery.Select("#" + clientId + "_Vote1Panel");}; return _Vote1PanelJ;}} private jQueryObject _Vote1PanelJ;
		public ImageElement Vote1Img {get {if (_Vote1Img == null) {_Vote1Img = (ImageElement)Document.GetElementById(clientId + "_Vote1Img");}; return _Vote1Img;}} private ImageElement _Vote1Img;
		public jQueryObject Vote1ImgJ {get {if (_Vote1ImgJ == null) {_Vote1ImgJ = jQuery.Select("#" + clientId + "_Vote1Img");}; return _Vote1ImgJ;}} private jQueryObject _Vote1ImgJ;
		public InputElement Vote1VoteButton {get {if (_Vote1VoteButton == null) {_Vote1VoteButton = (InputElement)Document.GetElementById(clientId + "_Vote1VoteButton");}; return _Vote1VoteButton;}} private InputElement _Vote1VoteButton;
		public jQueryObject Vote1VoteButtonJ {get {if (_Vote1VoteButtonJ == null) {_Vote1VoteButtonJ = jQuery.Select("#" + clientId + "_Vote1VoteButton");}; return _Vote1VoteButtonJ;}} private jQueryObject _Vote1VoteButtonJ;
		public DivElement VoteConfirmPanel {get {if (_VoteConfirmPanel == null) {_VoteConfirmPanel = (DivElement)Document.GetElementById(clientId + "_VoteConfirmPanel");}; return _VoteConfirmPanel;}} private DivElement _VoteConfirmPanel;
		public jQueryObject VoteConfirmPanelJ {get {if (_VoteConfirmPanelJ == null) {_VoteConfirmPanelJ = jQuery.Select("#" + clientId + "_VoteConfirmPanel");}; return _VoteConfirmPanelJ;}} private jQueryObject _VoteConfirmPanelJ;
		public ImageElement VoteConfirm_Img {get {if (_VoteConfirm_Img == null) {_VoteConfirm_Img = (ImageElement)Document.GetElementById(clientId + "_VoteConfirm_Img");}; return _VoteConfirm_Img;}} private ImageElement _VoteConfirm_Img;
		public jQueryObject VoteConfirm_ImgJ {get {if (_VoteConfirm_ImgJ == null) {_VoteConfirm_ImgJ = jQuery.Select("#" + clientId + "_VoteConfirm_Img");}; return _VoteConfirm_ImgJ;}} private jQueryObject _VoteConfirm_ImgJ;
		public AnchorElement VoteConfirm_Link {get {if (_VoteConfirm_Link == null) {_VoteConfirm_Link = (AnchorElement)Document.GetElementById(clientId + "_VoteConfirm_Link");}; return _VoteConfirm_Link;}} private AnchorElement _VoteConfirm_Link;
		public jQueryObject VoteConfirm_LinkJ {get {if (_VoteConfirm_LinkJ == null) {_VoteConfirm_LinkJ = jQuery.Select("#" + clientId + "_VoteConfirm_Link");}; return _VoteConfirm_LinkJ;}} private jQueryObject _VoteConfirm_LinkJ;
		public InputElement VoteConfirm_YesButton {get {if (_VoteConfirm_YesButton == null) {_VoteConfirm_YesButton = (InputElement)Document.GetElementById(clientId + "_VoteConfirm_YesButton");}; return _VoteConfirm_YesButton;}} private InputElement _VoteConfirm_YesButton;
		public jQueryObject VoteConfirm_YesButtonJ {get {if (_VoteConfirm_YesButtonJ == null) {_VoteConfirm_YesButtonJ = jQuery.Select("#" + clientId + "_VoteConfirm_YesButton");}; return _VoteConfirm_YesButtonJ;}} private jQueryObject _VoteConfirm_YesButtonJ;
		public InputElement VoteConfirm_NoButton {get {if (_VoteConfirm_NoButton == null) {_VoteConfirm_NoButton = (InputElement)Document.GetElementById(clientId + "_VoteConfirm_NoButton");}; return _VoteConfirm_NoButton;}} private InputElement _VoteConfirm_NoButton;
		public jQueryObject VoteConfirm_NoButtonJ {get {if (_VoteConfirm_NoButtonJ == null) {_VoteConfirm_NoButtonJ = jQuery.Select("#" + clientId + "_VoteConfirm_NoButton");}; return _VoteConfirm_NoButtonJ;}} private jQueryObject _VoteConfirm_NoButtonJ;
		public DivElement VoteLikePanel {get {if (_VoteLikePanel == null) {_VoteLikePanel = (DivElement)Document.GetElementById(clientId + "_VoteLikePanel");}; return _VoteLikePanel;}} private DivElement _VoteLikePanel;
		public jQueryObject VoteLikePanelJ {get {if (_VoteLikePanelJ == null) {_VoteLikePanelJ = jQuery.Select("#" + clientId + "_VoteLikePanel");}; return _VoteLikePanelJ;}} private jQueryObject _VoteLikePanelJ;
		public DivElement Vote2Panel {get {if (_Vote2Panel == null) {_Vote2Panel = (DivElement)Document.GetElementById(clientId + "_Vote2Panel");}; return _Vote2Panel;}} private DivElement _Vote2Panel;
		public jQueryObject Vote2PanelJ {get {if (_Vote2PanelJ == null) {_Vote2PanelJ = jQuery.Select("#" + clientId + "_Vote2Panel");}; return _Vote2PanelJ;}} private jQueryObject _Vote2PanelJ;
		public DivElement ArmaniTextFieldPanel {get {if (_ArmaniTextFieldPanel == null) {_ArmaniTextFieldPanel = (DivElement)Document.GetElementById(clientId + "_ArmaniTextFieldPanel");}; return _ArmaniTextFieldPanel;}} private DivElement _ArmaniTextFieldPanel;
		public jQueryObject ArmaniTextFieldPanelJ {get {if (_ArmaniTextFieldPanelJ == null) {_ArmaniTextFieldPanelJ = jQuery.Select("#" + clientId + "_ArmaniTextFieldPanel");}; return _ArmaniTextFieldPanelJ;}} private jQueryObject _ArmaniTextFieldPanelJ;
		public InputElement ArmaniTextField {get {if (_ArmaniTextField == null) {_ArmaniTextField = (InputElement)Document.GetElementById(clientId + "_ArmaniTextField");}; return _ArmaniTextField;}} private InputElement _ArmaniTextField;
		public jQueryObject ArmaniTextFieldJ {get {if (_ArmaniTextFieldJ == null) {_ArmaniTextFieldJ = jQuery.Select("#" + clientId + "_ArmaniTextField");}; return _ArmaniTextFieldJ;}} private jQueryObject _ArmaniTextFieldJ;
		public InputElement ArmaniSubmitButton {get {if (_ArmaniSubmitButton == null) {_ArmaniSubmitButton = (InputElement)Document.GetElementById(clientId + "_ArmaniSubmitButton");}; return _ArmaniSubmitButton;}} private InputElement _ArmaniSubmitButton;
		public jQueryObject ArmaniSubmitButtonJ {get {if (_ArmaniSubmitButtonJ == null) {_ArmaniSubmitButtonJ = jQuery.Select("#" + clientId + "_ArmaniSubmitButton");}; return _ArmaniSubmitButtonJ;}} private jQueryObject _ArmaniSubmitButtonJ;
		public Element ArmaniSavedLabel {get {if (_ArmaniSavedLabel == null) {_ArmaniSavedLabel = (Element)Document.GetElementById(clientId + "_ArmaniSavedLabel");}; return _ArmaniSavedLabel;}} private Element _ArmaniSavedLabel;
		public jQueryObject ArmaniSavedLabelJ {get {if (_ArmaniSavedLabelJ == null) {_ArmaniSavedLabelJ = jQuery.Select("#" + clientId + "_ArmaniSavedLabel");}; return _ArmaniSavedLabelJ;}} private jQueryObject _ArmaniSavedLabelJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element DebugOutput {get {if (_DebugOutput == null) {_DebugOutput = (Element)Document.GetElementById(clientId + "_DebugOutput");}; return _DebugOutput;}} private Element _DebugOutput;
		public jQueryObject DebugOutputJ {get {if (_DebugOutputJ == null) {_DebugOutputJ = jQuery.Select("#" + clientId + "_DebugOutput");}; return _DebugOutputJ;}} private jQueryObject _DebugOutputJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element LoadingLabel {get {if (_LoadingLabel == null) {_LoadingLabel = (Element)Document.GetElementById(clientId + "_LoadingLabel");}; return _LoadingLabel;}} private Element _LoadingLabel;
		public jQueryObject LoadingLabelJ {get {if (_LoadingLabelJ == null) {_LoadingLabelJ = jQuery.Select("#" + clientId + "_LoadingLabel");}; return _LoadingLabelJ;}} private jQueryObject _LoadingLabelJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
