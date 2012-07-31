//mappings.Add("Spotted.Controls.TimeControl", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.DonationIcons
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
		public InputElement uiK {get {if (_uiK == null) {_uiK = (InputElement)Document.GetElementById(clientId + "_uiK");}; return _uiK;}} private InputElement _uiK;
		public jQueryObject uiKJ {get {if (_uiKJ == null) {_uiKJ = jQuery.Select("#" + clientId + "_uiK");}; return _uiKJ;}} private jQueryObject _uiKJ;
		public InputElement uiName {get {if (_uiName == null) {_uiName = (InputElement)Document.GetElementById(clientId + "_uiName");}; return _uiName;}} private InputElement _uiName;
		public jQueryObject uiNameJ {get {if (_uiNameJ == null) {_uiNameJ = jQuery.Select("#" + clientId + "_uiName");}; return _uiNameJ;}} private jQueryObject _uiNameJ;
		public InputElement uiGuid {get {if (_uiGuid == null) {_uiGuid = (InputElement)Document.GetElementById(clientId + "_uiGuid");}; return _uiGuid;}} private InputElement _uiGuid;
		public jQueryObject uiGuidJ {get {if (_uiGuidJ == null) {_uiGuidJ = jQuery.Select("#" + clientId + "_uiGuid");}; return _uiGuidJ;}} private jQueryObject _uiGuidJ;
		public InputElement uiExtension {get {if (_uiExtension == null) {_uiExtension = (InputElement)Document.GetElementById(clientId + "_uiExtension");}; return _uiExtension;}} private InputElement _uiExtension;
		public jQueryObject uiExtensionJ {get {if (_uiExtensionJ == null) {_uiExtensionJ = jQuery.Select("#" + clientId + "_uiExtension");}; return _uiExtensionJ;}} private jQueryObject _uiExtensionJ;
		public InputElement uiText {get {if (_uiText == null) {_uiText = (InputElement)Document.GetElementById(clientId + "_uiText");}; return _uiText;}} private InputElement _uiText;
		public jQueryObject uiTextJ {get {if (_uiTextJ == null) {_uiTextJ = jQuery.Select("#" + clientId + "_uiText");}; return _uiTextJ;}} private jQueryObject _uiTextJ;
		public Js.CustomControls.Cal.Controller uiActivationDate {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_uiActivationDateController");}}
		public Element uiActivationTime {get {if (_uiActivationTime == null) {_uiActivationTime = (Element)Document.GetElementById(clientId + "_uiActivationTime");}; return _uiActivationTime;}} private Element _uiActivationTime;
		public jQueryObject uiActivationTimeJ {get {if (_uiActivationTimeJ == null) {_uiActivationTimeJ = jQuery.Select("#" + clientId + "_uiActivationTime");}; return _uiActivationTimeJ;}} private jQueryObject _uiActivationTimeJ;//mappings.Add("Spotted.Controls.TimeControl", ElementGetter("Element"));
		public CheckBoxElement uiEnabled {get {if (_uiEnabled == null) {_uiEnabled = (CheckBoxElement)Document.GetElementById(clientId + "_uiEnabled");}; return _uiEnabled;}} private CheckBoxElement _uiEnabled;
		public jQueryObject uiEnabledJ {get {if (_uiEnabledJ == null) {_uiEnabledJ = jQuery.Select("#" + clientId + "_uiEnabled");}; return _uiEnabledJ;}} private jQueryObject _uiEnabledJ;
		public InputElement uiThreadK {get {if (_uiThreadK == null) {_uiThreadK = (InputElement)Document.GetElementById(clientId + "_uiThreadK");}; return _uiThreadK;}} private InputElement _uiThreadK;
		public jQueryObject uiThreadKJ {get {if (_uiThreadKJ == null) {_uiThreadKJ = jQuery.Select("#" + clientId + "_uiThreadK");}; return _uiThreadKJ;}} private jQueryObject _uiThreadKJ;
		public CheckBoxElement uiVatable {get {if (_uiVatable == null) {_uiVatable = (CheckBoxElement)Document.GetElementById(clientId + "_uiVatable");}; return _uiVatable;}} private CheckBoxElement _uiVatable;
		public jQueryObject uiVatableJ {get {if (_uiVatableJ == null) {_uiVatableJ = jQuery.Select("#" + clientId + "_uiVatable");}; return _uiVatableJ;}} private jQueryObject _uiVatableJ;
		public AnchorElement uiLink {get {if (_uiLink == null) {_uiLink = (AnchorElement)Document.GetElementById(clientId + "_uiLink");}; return _uiLink;}} private AnchorElement _uiLink;
		public jQueryObject uiLinkJ {get {if (_uiLinkJ == null) {_uiLinkJ = jQuery.Select("#" + clientId + "_uiLink");}; return _uiLinkJ;}} private jQueryObject _uiLinkJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
