//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.MixmagVote.Entry
{
	public class Server {
		public Server(){}
		public void EnterComp(System.Int32 compK, System.String imageUrl, System.String facebookMessage, System.Boolean sendEmails, Response response) { object[] paramArr = { compK, imageUrl, facebookMessage, sendEmails }; ServerRequest req = (ServerRequest)Script.Eval("PageMethods.ClientRequest"); if (req != null) { try { req("Spotted.MixmagVote.Entry, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "EnterComp", paramArr, response, response); } catch (Exception e) { Dictionary<object, object> d = new Dictionary<object, object>(); d["Exception"] = true; d["ExceptionType"] = "ClientException"; d["Message"] = e.Message; d["StackTrace"] = ""; response(d); } } }
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
		public Element LoadingLabel {get {if (_LoadingLabel == null) {_LoadingLabel = (Element)Document.GetElementById(clientId + "_LoadingLabel");}; return _LoadingLabel;}} private Element _LoadingLabel;
		public jQueryObject LoadingLabelJ {get {if (_LoadingLabelJ == null) {_LoadingLabelJ = jQuery.Select("#" + clientId + "_LoadingLabel");}; return _LoadingLabelJ;}} private jQueryObject _LoadingLabelJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement ImageUrl {get {if (_ImageUrl == null) {_ImageUrl = (InputElement)Document.GetElementById(clientId + "_ImageUrl");}; return _ImageUrl;}} private InputElement _ImageUrl;
		public jQueryObject ImageUrlJ {get {if (_ImageUrlJ == null) {_ImageUrlJ = jQuery.Select("#" + clientId + "_ImageUrl");}; return _ImageUrlJ;}} private jQueryObject _ImageUrlJ;
		public InputElement PageIdToLike {get {if (_PageIdToLike == null) {_PageIdToLike = (InputElement)Document.GetElementById(clientId + "_PageIdToLike");}; return _PageIdToLike;}} private InputElement _PageIdToLike;
		public jQueryObject PageIdToLikeJ {get {if (_PageIdToLikeJ == null) {_PageIdToLikeJ = jQuery.Select("#" + clientId + "_PageIdToLike");}; return _PageIdToLikeJ;}} private jQueryObject _PageIdToLikeJ;
		public InputElement CompK {get {if (_CompK == null) {_CompK = (InputElement)Document.GetElementById(clientId + "_CompK");}; return _CompK;}} private InputElement _CompK;
		public jQueryObject CompKJ {get {if (_CompKJ == null) {_CompKJ = jQuery.Select("#" + clientId + "_CompK");}; return _CompKJ;}} private jQueryObject _CompKJ;
		public DivElement EntryClosedPanel {get {if (_EntryClosedPanel == null) {_EntryClosedPanel = (DivElement)Document.GetElementById(clientId + "_EntryClosedPanel");}; return _EntryClosedPanel;}} private DivElement _EntryClosedPanel;
		public jQueryObject EntryClosedPanelJ {get {if (_EntryClosedPanelJ == null) {_EntryClosedPanelJ = jQuery.Select("#" + clientId + "_EntryClosedPanel");}; return _EntryClosedPanelJ;}} private jQueryObject _EntryClosedPanelJ;
		public DivElement Entry1Panel {get {if (_Entry1Panel == null) {_Entry1Panel = (DivElement)Document.GetElementById(clientId + "_Entry1Panel");}; return _Entry1Panel;}} private DivElement _Entry1Panel;
		public jQueryObject Entry1PanelJ {get {if (_Entry1PanelJ == null) {_Entry1PanelJ = jQuery.Select("#" + clientId + "_Entry1Panel");}; return _Entry1PanelJ;}} private jQueryObject _Entry1PanelJ;
		public ImageElement Entry1Img {get {if (_Entry1Img == null) {_Entry1Img = (ImageElement)Document.GetElementById(clientId + "_Entry1Img");}; return _Entry1Img;}} private ImageElement _Entry1Img;
		public jQueryObject Entry1ImgJ {get {if (_Entry1ImgJ == null) {_Entry1ImgJ = jQuery.Select("#" + clientId + "_Entry1Img");}; return _Entry1ImgJ;}} private jQueryObject _Entry1ImgJ;
		public InputElement Entry1FacebookMessageTextbox {get {if (_Entry1FacebookMessageTextbox == null) {_Entry1FacebookMessageTextbox = (InputElement)Document.GetElementById(clientId + "_Entry1FacebookMessageTextbox");}; return _Entry1FacebookMessageTextbox;}} private InputElement _Entry1FacebookMessageTextbox;
		public jQueryObject Entry1FacebookMessageTextboxJ {get {if (_Entry1FacebookMessageTextboxJ == null) {_Entry1FacebookMessageTextboxJ = jQuery.Select("#" + clientId + "_Entry1FacebookMessageTextbox");}; return _Entry1FacebookMessageTextboxJ;}} private jQueryObject _Entry1FacebookMessageTextboxJ;
		public Element Entry1DailyEmailCheckboxPara {get {if (_Entry1DailyEmailCheckboxPara == null) {_Entry1DailyEmailCheckboxPara = (Element)Document.GetElementById(clientId + "_Entry1DailyEmailCheckboxPara");}; return _Entry1DailyEmailCheckboxPara;}} private Element _Entry1DailyEmailCheckboxPara;
		public jQueryObject Entry1DailyEmailCheckboxParaJ {get {if (_Entry1DailyEmailCheckboxParaJ == null) {_Entry1DailyEmailCheckboxParaJ = jQuery.Select("#" + clientId + "_Entry1DailyEmailCheckboxPara");}; return _Entry1DailyEmailCheckboxParaJ;}} private jQueryObject _Entry1DailyEmailCheckboxParaJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public CheckBoxElement Entry1DailyEmailCheckbox {get {if (_Entry1DailyEmailCheckbox == null) {_Entry1DailyEmailCheckbox = (CheckBoxElement)Document.GetElementById(clientId + "_Entry1DailyEmailCheckbox");}; return _Entry1DailyEmailCheckbox;}} private CheckBoxElement _Entry1DailyEmailCheckbox;
		public jQueryObject Entry1DailyEmailCheckboxJ {get {if (_Entry1DailyEmailCheckboxJ == null) {_Entry1DailyEmailCheckboxJ = jQuery.Select("#" + clientId + "_Entry1DailyEmailCheckbox");}; return _Entry1DailyEmailCheckboxJ;}} private jQueryObject _Entry1DailyEmailCheckboxJ;
		public InputElement Entry1Button {get {if (_Entry1Button == null) {_Entry1Button = (InputElement)Document.GetElementById(clientId + "_Entry1Button");}; return _Entry1Button;}} private InputElement _Entry1Button;
		public jQueryObject Entry1ButtonJ {get {if (_Entry1ButtonJ == null) {_Entry1ButtonJ = jQuery.Select("#" + clientId + "_Entry1Button");}; return _Entry1ButtonJ;}} private jQueryObject _Entry1ButtonJ;
		public DivElement EntryConfirmPanel {get {if (_EntryConfirmPanel == null) {_EntryConfirmPanel = (DivElement)Document.GetElementById(clientId + "_EntryConfirmPanel");}; return _EntryConfirmPanel;}} private DivElement _EntryConfirmPanel;
		public jQueryObject EntryConfirmPanelJ {get {if (_EntryConfirmPanelJ == null) {_EntryConfirmPanelJ = jQuery.Select("#" + clientId + "_EntryConfirmPanel");}; return _EntryConfirmPanelJ;}} private jQueryObject _EntryConfirmPanelJ;
		public ImageElement EntryConfirm_Img {get {if (_EntryConfirm_Img == null) {_EntryConfirm_Img = (ImageElement)Document.GetElementById(clientId + "_EntryConfirm_Img");}; return _EntryConfirm_Img;}} private ImageElement _EntryConfirm_Img;
		public jQueryObject EntryConfirm_ImgJ {get {if (_EntryConfirm_ImgJ == null) {_EntryConfirm_ImgJ = jQuery.Select("#" + clientId + "_EntryConfirm_Img");}; return _EntryConfirm_ImgJ;}} private jQueryObject _EntryConfirm_ImgJ;
		public AnchorElement EntryConfirm_Link {get {if (_EntryConfirm_Link == null) {_EntryConfirm_Link = (AnchorElement)Document.GetElementById(clientId + "_EntryConfirm_Link");}; return _EntryConfirm_Link;}} private AnchorElement _EntryConfirm_Link;
		public jQueryObject EntryConfirm_LinkJ {get {if (_EntryConfirm_LinkJ == null) {_EntryConfirm_LinkJ = jQuery.Select("#" + clientId + "_EntryConfirm_Link");}; return _EntryConfirm_LinkJ;}} private jQueryObject _EntryConfirm_LinkJ;
		public InputElement EntryConfirm_YesButton {get {if (_EntryConfirm_YesButton == null) {_EntryConfirm_YesButton = (InputElement)Document.GetElementById(clientId + "_EntryConfirm_YesButton");}; return _EntryConfirm_YesButton;}} private InputElement _EntryConfirm_YesButton;
		public jQueryObject EntryConfirm_YesButtonJ {get {if (_EntryConfirm_YesButtonJ == null) {_EntryConfirm_YesButtonJ = jQuery.Select("#" + clientId + "_EntryConfirm_YesButton");}; return _EntryConfirm_YesButtonJ;}} private jQueryObject _EntryConfirm_YesButtonJ;
		public InputElement EntryConfirm_NoButton {get {if (_EntryConfirm_NoButton == null) {_EntryConfirm_NoButton = (InputElement)Document.GetElementById(clientId + "_EntryConfirm_NoButton");}; return _EntryConfirm_NoButton;}} private InputElement _EntryConfirm_NoButton;
		public jQueryObject EntryConfirm_NoButtonJ {get {if (_EntryConfirm_NoButtonJ == null) {_EntryConfirm_NoButtonJ = jQuery.Select("#" + clientId + "_EntryConfirm_NoButton");}; return _EntryConfirm_NoButtonJ;}} private jQueryObject _EntryConfirm_NoButtonJ;
		public DivElement Entry2Panel {get {if (_Entry2Panel == null) {_Entry2Panel = (DivElement)Document.GetElementById(clientId + "_Entry2Panel");}; return _Entry2Panel;}} private DivElement _Entry2Panel;
		public jQueryObject Entry2PanelJ {get {if (_Entry2PanelJ == null) {_Entry2PanelJ = jQuery.Select("#" + clientId + "_Entry2Panel");}; return _Entry2PanelJ;}} private jQueryObject _Entry2PanelJ;
		public ImageElement Entry2Img {get {if (_Entry2Img == null) {_Entry2Img = (ImageElement)Document.GetElementById(clientId + "_Entry2Img");}; return _Entry2Img;}} private ImageElement _Entry2Img;
		public jQueryObject Entry2ImgJ {get {if (_Entry2ImgJ == null) {_Entry2ImgJ = jQuery.Select("#" + clientId + "_Entry2Img");}; return _Entry2ImgJ;}} private jQueryObject _Entry2ImgJ;
		public InputElement Entry2LinkTextbox {get {if (_Entry2LinkTextbox == null) {_Entry2LinkTextbox = (InputElement)Document.GetElementById(clientId + "_Entry2LinkTextbox");}; return _Entry2LinkTextbox;}} private InputElement _Entry2LinkTextbox;
		public jQueryObject Entry2LinkTextboxJ {get {if (_Entry2LinkTextboxJ == null) {_Entry2LinkTextboxJ = jQuery.Select("#" + clientId + "_Entry2LinkTextbox");}; return _Entry2LinkTextboxJ;}} private jQueryObject _Entry2LinkTextboxJ;
		public Element Entry2LikeButtonHolder {get {if (_Entry2LikeButtonHolder == null) {_Entry2LikeButtonHolder = (Element)Document.GetElementById(clientId + "_Entry2LikeButtonHolder");}; return _Entry2LikeButtonHolder;}} private Element _Entry2LikeButtonHolder;
		public jQueryObject Entry2LikeButtonHolderJ {get {if (_Entry2LikeButtonHolderJ == null) {_Entry2LikeButtonHolderJ = jQuery.Select("#" + clientId + "_Entry2LikeButtonHolder");}; return _Entry2LikeButtonHolderJ;}} private jQueryObject _Entry2LikeButtonHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element DebugOutput {get {if (_DebugOutput == null) {_DebugOutput = (Element)Document.GetElementById(clientId + "_DebugOutput");}; return _DebugOutput;}} private Element _DebugOutput;
		public jQueryObject DebugOutputJ {get {if (_DebugOutputJ == null) {_DebugOutputJ = jQuery.Select("#" + clientId + "_DebugOutput");}; return _DebugOutputJ;}} private jQueryObject _DebugOutputJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
