//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.AreYouDj
{
	public partial class View
		 : Js.DsiUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public CheckBoxElement IsDjYes {get {if (_IsDjYes == null) {_IsDjYes = (CheckBoxElement)Document.GetElementById(clientId + "_IsDjYes");}; return _IsDjYes;}} private CheckBoxElement _IsDjYes;
		public jQueryObject IsDjYesJ {get {if (_IsDjYesJ == null) {_IsDjYesJ = jQuery.Select("#" + clientId + "_IsDjYes");}; return _IsDjYesJ;}} private jQueryObject _IsDjYesJ;
		public CheckBoxElement IsDjNo {get {if (_IsDjNo == null) {_IsDjNo = (CheckBoxElement)Document.GetElementById(clientId + "_IsDjNo");}; return _IsDjNo;}} private CheckBoxElement _IsDjNo;
		public jQueryObject IsDjNoJ {get {if (_IsDjNoJ == null) {_IsDjNoJ = jQuery.Select("#" + clientId + "_IsDjNo");}; return _IsDjNoJ;}} private jQueryObject _IsDjNoJ;
		public Element CustomValidatorIsDj {get {if (_CustomValidatorIsDj == null) {_CustomValidatorIsDj = (Element)Document.GetElementById(clientId + "_CustomValidatorIsDj");}; return _CustomValidatorIsDj;}} private Element _CustomValidatorIsDj;
		public jQueryObject CustomValidatorIsDjJ {get {if (_CustomValidatorIsDjJ == null) {_CustomValidatorIsDjJ = jQuery.Select("#" + clientId + "_CustomValidatorIsDj");}; return _CustomValidatorIsDjJ;}} private jQueryObject _CustomValidatorIsDjJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public DivElement SavedPanel {get {if (_SavedPanel == null) {_SavedPanel = (DivElement)Document.GetElementById(clientId + "_SavedPanel");}; return _SavedPanel;}} private DivElement _SavedPanel;
		public jQueryObject SavedPanelJ {get {if (_SavedPanelJ == null) {_SavedPanelJ = jQuery.Select("#" + clientId + "_SavedPanel");}; return _SavedPanelJ;}} private jQueryObject _SavedPanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
