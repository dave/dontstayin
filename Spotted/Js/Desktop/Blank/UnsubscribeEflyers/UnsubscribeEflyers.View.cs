//mappings.Add("System.Web.UI.WebControls.RadioButtonList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.UnsubscribeEflyers
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
		public DivElement uiOptionsPanel {get {if (_uiOptionsPanel == null) {_uiOptionsPanel = (DivElement)Document.GetElementById(clientId + "_uiOptionsPanel");}; return _uiOptionsPanel;}} private DivElement _uiOptionsPanel;
		public jQueryObject uiOptionsPanelJ {get {if (_uiOptionsPanelJ == null) {_uiOptionsPanelJ = jQuery.Select("#" + clientId + "_uiOptionsPanel");}; return _uiOptionsPanelJ;}} private jQueryObject _uiOptionsPanelJ;
		public Element uiSendEflyersOptions {get {if (_uiSendEflyersOptions == null) {_uiSendEflyersOptions = (Element)Document.GetElementById(clientId + "_uiSendEflyersOptions");}; return _uiSendEflyersOptions;}} private Element _uiSendEflyersOptions;
		public jQueryObject uiSendEflyersOptionsJ {get {if (_uiSendEflyersOptionsJ == null) {_uiSendEflyersOptionsJ = jQuery.Select("#" + clientId + "_uiSendEflyersOptions");}; return _uiSendEflyersOptionsJ;}} private jQueryObject _uiSendEflyersOptionsJ;//mappings.Add("System.Web.UI.WebControls.RadioButtonList", ElementGetter("Element"));
		public DivElement uiSavedPanel {get {if (_uiSavedPanel == null) {_uiSavedPanel = (DivElement)Document.GetElementById(clientId + "_uiSavedPanel");}; return _uiSavedPanel;}} private DivElement _uiSavedPanel;
		public jQueryObject uiSavedPanelJ {get {if (_uiSavedPanelJ == null) {_uiSavedPanelJ = jQuery.Select("#" + clientId + "_uiSavedPanel");}; return _uiSavedPanelJ;}} private jQueryObject _uiSavedPanelJ;
		public Element uiSavedSettingLabel {get {if (_uiSavedSettingLabel == null) {_uiSavedSettingLabel = (Element)Document.GetElementById(clientId + "_uiSavedSettingLabel");}; return _uiSavedSettingLabel;}} private Element _uiSavedSettingLabel;
		public jQueryObject uiSavedSettingLabelJ {get {if (_uiSavedSettingLabelJ == null) {_uiSavedSettingLabelJ = jQuery.Select("#" + clientId + "_uiSavedSettingLabel");}; return _uiSavedSettingLabelJ;}} private jQueryObject _uiSavedSettingLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
