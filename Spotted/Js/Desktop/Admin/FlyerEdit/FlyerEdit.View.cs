//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.TimeControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlInputFile", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.FlyerEdit
{
	public partial class View
		 : Js.AdminUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public CheckBoxElement uiIsHtml {get {if (_uiIsHtml == null) {_uiIsHtml = (CheckBoxElement)Document.GetElementById(clientId + "_uiIsHtml");}; return _uiIsHtml;}} private CheckBoxElement _uiIsHtml;
		public jQueryObject uiIsHtmlJ {get {if (_uiIsHtmlJ == null) {_uiIsHtmlJ = jQuery.Select("#" + clientId + "_uiIsHtml");}; return _uiIsHtmlJ;}} private jQueryObject _uiIsHtmlJ;
		public InputElement uiHtml {get {if (_uiHtml == null) {_uiHtml = (InputElement)Document.GetElementById(clientId + "_uiHtml");}; return _uiHtml;}} private InputElement _uiHtml;
		public jQueryObject uiHtmlJ {get {if (_uiHtmlJ == null) {_uiHtmlJ = jQuery.Select("#" + clientId + "_uiHtml");}; return _uiHtmlJ;}} private jQueryObject _uiHtmlJ;
		public InputElement uiTextAlternative {get {if (_uiTextAlternative == null) {_uiTextAlternative = (InputElement)Document.GetElementById(clientId + "_uiTextAlternative");}; return _uiTextAlternative;}} private InputElement _uiTextAlternative;
		public jQueryObject uiTextAlternativeJ {get {if (_uiTextAlternativeJ == null) {_uiTextAlternativeJ = jQuery.Select("#" + clientId + "_uiTextAlternative");}; return _uiTextAlternativeJ;}} private jQueryObject _uiTextAlternativeJ;
		public DivElement uiBasicInfo {get {if (_uiBasicInfo == null) {_uiBasicInfo = (DivElement)Document.GetElementById(clientId + "_uiBasicInfo");}; return _uiBasicInfo;}} private DivElement _uiBasicInfo;
		public jQueryObject uiBasicInfoJ {get {if (_uiBasicInfoJ == null) {_uiBasicInfoJ = jQuery.Select("#" + clientId + "_uiBasicInfo");}; return _uiBasicInfoJ;}} private jQueryObject _uiBasicInfoJ;
		public Element uiFlyerKLabel {get {if (_uiFlyerKLabel == null) {_uiFlyerKLabel = (Element)Document.GetElementById(clientId + "_uiFlyerKLabel");}; return _uiFlyerKLabel;}} private Element _uiFlyerKLabel;
		public jQueryObject uiFlyerKLabelJ {get {if (_uiFlyerKLabelJ == null) {_uiFlyerKLabelJ = jQuery.Select("#" + clientId + "_uiFlyerKLabel");}; return _uiFlyerKLabelJ;}} private jQueryObject _uiFlyerKLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiPromotersAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiPromotersAutoCompleteBehaviour");}}
		public InputElement uiNameTextBox {get {if (_uiNameTextBox == null) {_uiNameTextBox = (InputElement)Document.GetElementById(clientId + "_uiNameTextBox");}; return _uiNameTextBox;}} private InputElement _uiNameTextBox;
		public jQueryObject uiNameTextBoxJ {get {if (_uiNameTextBoxJ == null) {_uiNameTextBoxJ = jQuery.Select("#" + clientId + "_uiNameTextBox");}; return _uiNameTextBoxJ;}} private jQueryObject _uiNameTextBoxJ;
		public InputElement uiSubjectTextBox {get {if (_uiSubjectTextBox == null) {_uiSubjectTextBox = (InputElement)Document.GetElementById(clientId + "_uiSubjectTextBox");}; return _uiSubjectTextBox;}} private InputElement _uiSubjectTextBox;
		public jQueryObject uiSubjectTextBoxJ {get {if (_uiSubjectTextBoxJ == null) {_uiSubjectTextBoxJ = jQuery.Select("#" + clientId + "_uiSubjectTextBox");}; return _uiSubjectTextBoxJ;}} private jQueryObject _uiSubjectTextBoxJ;
		public InputElement uiFromDisplayNameTextBox {get {if (_uiFromDisplayNameTextBox == null) {_uiFromDisplayNameTextBox = (InputElement)Document.GetElementById(clientId + "_uiFromDisplayNameTextBox");}; return _uiFromDisplayNameTextBox;}} private InputElement _uiFromDisplayNameTextBox;
		public jQueryObject uiFromDisplayNameTextBoxJ {get {if (_uiFromDisplayNameTextBoxJ == null) {_uiFromDisplayNameTextBoxJ = jQuery.Select("#" + clientId + "_uiFromDisplayNameTextBox");}; return _uiFromDisplayNameTextBoxJ;}} private jQueryObject _uiFromDisplayNameTextBoxJ;
		public InputElement uiBackgroundColor {get {if (_uiBackgroundColor == null) {_uiBackgroundColor = (InputElement)Document.GetElementById(clientId + "_uiBackgroundColor");}; return _uiBackgroundColor;}} private InputElement _uiBackgroundColor;
		public jQueryObject uiBackgroundColorJ {get {if (_uiBackgroundColorJ == null) {_uiBackgroundColorJ = jQuery.Select("#" + clientId + "_uiBackgroundColor");}; return _uiBackgroundColorJ;}} private jQueryObject _uiBackgroundColorJ;
		public InputElement uiUrlTextBox {get {if (_uiUrlTextBox == null) {_uiUrlTextBox = (InputElement)Document.GetElementById(clientId + "_uiUrlTextBox");}; return _uiUrlTextBox;}} private InputElement _uiUrlTextBox;
		public jQueryObject uiUrlTextBoxJ {get {if (_uiUrlTextBoxJ == null) {_uiUrlTextBoxJ = jQuery.Select("#" + clientId + "_uiUrlTextBox");}; return _uiUrlTextBoxJ;}} private jQueryObject _uiUrlTextBoxJ;
		public Js.CustomControls.Cal.Controller uiSendDate {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_uiSendDateController");}}
		public Element uiSendTime {get {if (_uiSendTime == null) {_uiSendTime = (Element)Document.GetElementById(clientId + "_uiSendTime");}; return _uiSendTime;}} private Element _uiSendTime;
		public jQueryObject uiSendTimeJ {get {if (_uiSendTimeJ == null) {_uiSendTimeJ = jQuery.Select("#" + clientId + "_uiSendTime");}; return _uiSendTimeJ;}} private jQueryObject _uiSendTimeJ;//mappings.Add("Spotted.Controls.TimeControl", ElementGetter("Element"));
		public InputElement uiPlaceTargettingHidden {get {if (_uiPlaceTargettingHidden == null) {_uiPlaceTargettingHidden = (InputElement)Document.GetElementById(clientId + "_uiPlaceTargettingHidden");}; return _uiPlaceTargettingHidden;}} private InputElement _uiPlaceTargettingHidden;
		public jQueryObject uiPlaceTargettingHiddenJ {get {if (_uiPlaceTargettingHiddenJ == null) {_uiPlaceTargettingHiddenJ = jQuery.Select("#" + clientId + "_uiPlaceTargettingHidden");}; return _uiPlaceTargettingHiddenJ;}} private jQueryObject _uiPlaceTargettingHiddenJ;
		public InputElement uiPlaceTargettingTextBox {get {if (_uiPlaceTargettingTextBox == null) {_uiPlaceTargettingTextBox = (InputElement)Document.GetElementById(clientId + "_uiPlaceTargettingTextBox");}; return _uiPlaceTargettingTextBox;}} private InputElement _uiPlaceTargettingTextBox;
		public jQueryObject uiPlaceTargettingTextBoxJ {get {if (_uiPlaceTargettingTextBoxJ == null) {_uiPlaceTargettingTextBoxJ = jQuery.Select("#" + clientId + "_uiPlaceTargettingTextBox");}; return _uiPlaceTargettingTextBoxJ;}} private jQueryObject _uiPlaceTargettingTextBoxJ;
		public InputElement uiPlaceTargettingButton {get {if (_uiPlaceTargettingButton == null) {_uiPlaceTargettingButton = (InputElement)Document.GetElementById(clientId + "_uiPlaceTargettingButton");}; return _uiPlaceTargettingButton;}} private InputElement _uiPlaceTargettingButton;
		public jQueryObject uiPlaceTargettingButtonJ {get {if (_uiPlaceTargettingButtonJ == null) {_uiPlaceTargettingButtonJ = jQuery.Select("#" + clientId + "_uiPlaceTargettingButton");}; return _uiPlaceTargettingButtonJ;}} private jQueryObject _uiPlaceTargettingButtonJ;
		public InputElement uiMusicTargettingHidden {get {if (_uiMusicTargettingHidden == null) {_uiMusicTargettingHidden = (InputElement)Document.GetElementById(clientId + "_uiMusicTargettingHidden");}; return _uiMusicTargettingHidden;}} private InputElement _uiMusicTargettingHidden;
		public jQueryObject uiMusicTargettingHiddenJ {get {if (_uiMusicTargettingHiddenJ == null) {_uiMusicTargettingHiddenJ = jQuery.Select("#" + clientId + "_uiMusicTargettingHidden");}; return _uiMusicTargettingHiddenJ;}} private jQueryObject _uiMusicTargettingHiddenJ;
		public InputElement uiMusicTargettingTextBox {get {if (_uiMusicTargettingTextBox == null) {_uiMusicTargettingTextBox = (InputElement)Document.GetElementById(clientId + "_uiMusicTargettingTextBox");}; return _uiMusicTargettingTextBox;}} private InputElement _uiMusicTargettingTextBox;
		public jQueryObject uiMusicTargettingTextBoxJ {get {if (_uiMusicTargettingTextBoxJ == null) {_uiMusicTargettingTextBoxJ = jQuery.Select("#" + clientId + "_uiMusicTargettingTextBox");}; return _uiMusicTargettingTextBoxJ;}} private jQueryObject _uiMusicTargettingTextBoxJ;
		public InputElement uiMusicTargettingButton {get {if (_uiMusicTargettingButton == null) {_uiMusicTargettingButton = (InputElement)Document.GetElementById(clientId + "_uiMusicTargettingButton");}; return _uiMusicTargettingButton;}} private InputElement _uiMusicTargettingButton;
		public jQueryObject uiMusicTargettingButtonJ {get {if (_uiMusicTargettingButtonJ == null) {_uiMusicTargettingButtonJ = jQuery.Select("#" + clientId + "_uiMusicTargettingButton");}; return _uiMusicTargettingButtonJ;}} private jQueryObject _uiMusicTargettingButtonJ;
		public CheckBoxElement uiPromotersOnly {get {if (_uiPromotersOnly == null) {_uiPromotersOnly = (CheckBoxElement)Document.GetElementById(clientId + "_uiPromotersOnly");}; return _uiPromotersOnly;}} private CheckBoxElement _uiPromotersOnly;
		public jQueryObject uiPromotersOnlyJ {get {if (_uiPromotersOnlyJ == null) {_uiPromotersOnlyJ = jQuery.Select("#" + clientId + "_uiPromotersOnly");}; return _uiPromotersOnlyJ;}} private jQueryObject _uiPromotersOnlyJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiEvent {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiEventBehaviour");}}
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiBrand {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiBrandBehaviour");}}
		public InputElement uiEventKs {get {if (_uiEventKs == null) {_uiEventKs = (InputElement)Document.GetElementById(clientId + "_uiEventKs");}; return _uiEventKs;}} private InputElement _uiEventKs;
		public jQueryObject uiEventKsJ {get {if (_uiEventKsJ == null) {_uiEventKsJ = jQuery.Select("#" + clientId + "_uiEventKs");}; return _uiEventKsJ;}} private jQueryObject _uiEventKsJ;
		public Element uiEvents {get {if (_uiEvents == null) {_uiEvents = (Element)Document.GetElementById(clientId + "_uiEvents");}; return _uiEvents;}} private Element _uiEvents;
		public jQueryObject uiEventsJ {get {if (_uiEventsJ == null) {_uiEventsJ = jQuery.Select("#" + clientId + "_uiEvents");}; return _uiEventsJ;}} private jQueryObject _uiEventsJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element uiUsrBaseCountLabel {get {if (_uiUsrBaseCountLabel == null) {_uiUsrBaseCountLabel = (Element)Document.GetElementById(clientId + "_uiUsrBaseCountLabel");}; return _uiUsrBaseCountLabel;}} private Element _uiUsrBaseCountLabel;
		public jQueryObject uiUsrBaseCountLabelJ {get {if (_uiUsrBaseCountLabelJ == null) {_uiUsrBaseCountLabelJ = jQuery.Select("#" + clientId + "_uiUsrBaseCountLabel");}; return _uiUsrBaseCountLabelJ;}} private jQueryObject _uiUsrBaseCountLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element uiInputFile {get {if (_uiInputFile == null) {_uiInputFile = (Element)Document.GetElementById(clientId + "_uiInputFile");}; return _uiInputFile;}} private Element _uiInputFile;
		public jQueryObject uiInputFileJ {get {if (_uiInputFileJ == null) {_uiInputFileJ = jQuery.Select("#" + clientId + "_uiInputFile");}; return _uiInputFileJ;}} private jQueryObject _uiInputFileJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlInputFile", ElementGetter("Element"));
		public AnchorElement uiPreviewFile {get {if (_uiPreviewFile == null) {_uiPreviewFile = (AnchorElement)Document.GetElementById(clientId + "_uiPreviewFile");}; return _uiPreviewFile;}} private AnchorElement _uiPreviewFile;
		public jQueryObject uiPreviewFileJ {get {if (_uiPreviewFileJ == null) {_uiPreviewFileJ = jQuery.Select("#" + clientId + "_uiPreviewFile");}; return _uiPreviewFileJ;}} private jQueryObject _uiPreviewFileJ;
		public Element uiSaveButton {get {if (_uiSaveButton == null) {_uiSaveButton = (Element)Document.GetElementById(clientId + "_uiSaveButton");}; return _uiSaveButton;}} private Element _uiSaveButton;
		public jQueryObject uiSaveButtonJ {get {if (_uiSaveButtonJ == null) {_uiSaveButtonJ = jQuery.Select("#" + clientId + "_uiSaveButton");}; return _uiSaveButtonJ;}} private jQueryObject _uiSaveButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element uiSavedLabel {get {if (_uiSavedLabel == null) {_uiSavedLabel = (Element)Document.GetElementById(clientId + "_uiSavedLabel");}; return _uiSavedLabel;}} private Element _uiSavedLabel;
		public jQueryObject uiSavedLabelJ {get {if (_uiSavedLabelJ == null) {_uiSavedLabelJ = jQuery.Select("#" + clientId + "_uiSavedLabel");}; return _uiSavedLabelJ;}} private jQueryObject _uiSavedLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element uiTestButton {get {if (_uiTestButton == null) {_uiTestButton = (Element)Document.GetElementById(clientId + "_uiTestButton");}; return _uiTestButton;}} private Element _uiTestButton;
		public jQueryObject uiTestButtonJ {get {if (_uiTestButtonJ == null) {_uiTestButtonJ = jQuery.Select("#" + clientId + "_uiTestButton");}; return _uiTestButtonJ;}} private jQueryObject _uiTestButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public InputElement uiTestEmail {get {if (_uiTestEmail == null) {_uiTestEmail = (InputElement)Document.GetElementById(clientId + "_uiTestEmail");}; return _uiTestEmail;}} private InputElement _uiTestEmail;
		public jQueryObject uiTestEmailJ {get {if (_uiTestEmailJ == null) {_uiTestEmailJ = jQuery.Select("#" + clientId + "_uiTestEmail");}; return _uiTestEmailJ;}} private jQueryObject _uiTestEmailJ;
		public Element uiTestEmailSuccess {get {if (_uiTestEmailSuccess == null) {_uiTestEmailSuccess = (Element)Document.GetElementById(clientId + "_uiTestEmailSuccess");}; return _uiTestEmailSuccess;}} private Element _uiTestEmailSuccess;
		public jQueryObject uiTestEmailSuccessJ {get {if (_uiTestEmailSuccessJ == null) {_uiTestEmailSuccessJ = jQuery.Select("#" + clientId + "_uiTestEmailSuccess");}; return _uiTestEmailSuccessJ;}} private jQueryObject _uiTestEmailSuccessJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
