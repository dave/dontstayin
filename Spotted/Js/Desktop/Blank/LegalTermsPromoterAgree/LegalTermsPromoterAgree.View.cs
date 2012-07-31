//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.LegalTermsPromoterAgree
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
		public Element Validationsummary1 {get {if (_Validationsummary1 == null) {_Validationsummary1 = (Element)Document.GetElementById(clientId + "_Validationsummary1");}; return _Validationsummary1;}} private Element _Validationsummary1;
		public jQueryObject Validationsummary1J {get {if (_Validationsummary1J == null) {_Validationsummary1J = jQuery.Select("#" + clientId + "_Validationsummary1");}; return _Validationsummary1J;}} private jQueryObject _Validationsummary1J;//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
		public CheckBoxElement TermsCheckbox {get {if (_TermsCheckbox == null) {_TermsCheckbox = (CheckBoxElement)Document.GetElementById(clientId + "_TermsCheckbox");}; return _TermsCheckbox;}} private CheckBoxElement _TermsCheckbox;
		public jQueryObject TermsCheckboxJ {get {if (_TermsCheckboxJ == null) {_TermsCheckboxJ = jQuery.Select("#" + clientId + "_TermsCheckbox");}; return _TermsCheckboxJ;}} private jQueryObject _TermsCheckboxJ;
		public Element Customvalidator7 {get {if (_Customvalidator7 == null) {_Customvalidator7 = (Element)Document.GetElementById(clientId + "_Customvalidator7");}; return _Customvalidator7;}} private Element _Customvalidator7;
		public jQueryObject Customvalidator7J {get {if (_Customvalidator7J == null) {_Customvalidator7J = jQuery.Select("#" + clientId + "_Customvalidator7");}; return _Customvalidator7J;}} private jQueryObject _Customvalidator7J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element PrefsUpdateButton {get {if (_PrefsUpdateButton == null) {_PrefsUpdateButton = (Element)Document.GetElementById(clientId + "_PrefsUpdateButton");}; return _PrefsUpdateButton;}} private Element _PrefsUpdateButton;
		public jQueryObject PrefsUpdateButtonJ {get {if (_PrefsUpdateButtonJ == null) {_PrefsUpdateButtonJ = jQuery.Select("#" + clientId + "_PrefsUpdateButton");}; return _PrefsUpdateButtonJ;}} private jQueryObject _PrefsUpdateButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
