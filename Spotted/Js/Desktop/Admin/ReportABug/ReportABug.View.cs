//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.ReportABug
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
		public DivElement uiBugFormPanel {get {if (_uiBugFormPanel == null) {_uiBugFormPanel = (DivElement)Document.GetElementById(clientId + "_uiBugFormPanel");}; return _uiBugFormPanel;}} private DivElement _uiBugFormPanel;
		public jQueryObject uiBugFormPanelJ {get {if (_uiBugFormPanelJ == null) {_uiBugFormPanelJ = jQuery.Select("#" + clientId + "_uiBugFormPanel");}; return _uiBugFormPanelJ;}} private jQueryObject _uiBugFormPanelJ;
		public InputElement uiTitle {get {if (_uiTitle == null) {_uiTitle = (InputElement)Document.GetElementById(clientId + "_uiTitle");}; return _uiTitle;}} private InputElement _uiTitle;
		public jQueryObject uiTitleJ {get {if (_uiTitleJ == null) {_uiTitleJ = jQuery.Select("#" + clientId + "_uiTitle");}; return _uiTitleJ;}} private jQueryObject _uiTitleJ;
		public Element RequiredFieldValidator1 {get {if (_RequiredFieldValidator1 == null) {_RequiredFieldValidator1 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1;}} private Element _RequiredFieldValidator1;
		public jQueryObject RequiredFieldValidator1J {get {if (_RequiredFieldValidator1J == null) {_RequiredFieldValidator1J = jQuery.Select("#" + clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1J;}} private jQueryObject _RequiredFieldValidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public InputElement uiDescription {get {if (_uiDescription == null) {_uiDescription = (InputElement)Document.GetElementById(clientId + "_uiDescription");}; return _uiDescription;}} private InputElement _uiDescription;
		public jQueryObject uiDescriptionJ {get {if (_uiDescriptionJ == null) {_uiDescriptionJ = jQuery.Select("#" + clientId + "_uiDescription");}; return _uiDescriptionJ;}} private jQueryObject _uiDescriptionJ;
		public InputElement uiUrl {get {if (_uiUrl == null) {_uiUrl = (InputElement)Document.GetElementById(clientId + "_uiUrl");}; return _uiUrl;}} private InputElement _uiUrl;
		public jQueryObject uiUrlJ {get {if (_uiUrlJ == null) {_uiUrlJ = jQuery.Select("#" + clientId + "_uiUrl");}; return _uiUrlJ;}} private jQueryObject _uiUrlJ;
		public Element uiSubmit {get {if (_uiSubmit == null) {_uiSubmit = (Element)Document.GetElementById(clientId + "_uiSubmit");}; return _uiSubmit;}} private Element _uiSubmit;
		public jQueryObject uiSubmitJ {get {if (_uiSubmitJ == null) {_uiSubmitJ = jQuery.Select("#" + clientId + "_uiSubmit");}; return _uiSubmitJ;}} private jQueryObject _uiSubmitJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement uiSuccessPanel {get {if (_uiSuccessPanel == null) {_uiSuccessPanel = (DivElement)Document.GetElementById(clientId + "_uiSuccessPanel");}; return _uiSuccessPanel;}} private DivElement _uiSuccessPanel;
		public jQueryObject uiSuccessPanelJ {get {if (_uiSuccessPanelJ == null) {_uiSuccessPanelJ = jQuery.Select("#" + clientId + "_uiSuccessPanel");}; return _uiSuccessPanelJ;}} private jQueryObject _uiSuccessPanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
