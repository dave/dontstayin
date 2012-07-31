//mappings.Add("System.Web.UI.WebControls.Literal", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.OptionsList", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.RegisterNewDomain
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
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiPromotersAutoComplete {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiPromotersAutoCompleteBehaviour");}}
		public DivElement uiNewDomainDetailsPanel {get {if (_uiNewDomainDetailsPanel == null) {_uiNewDomainDetailsPanel = (DivElement)Document.GetElementById(clientId + "_uiNewDomainDetailsPanel");}; return _uiNewDomainDetailsPanel;}} private DivElement _uiNewDomainDetailsPanel;
		public jQueryObject uiNewDomainDetailsPanelJ {get {if (_uiNewDomainDetailsPanelJ == null) {_uiNewDomainDetailsPanelJ = jQuery.Select("#" + clientId + "_uiNewDomainDetailsPanel");}; return _uiNewDomainDetailsPanelJ;}} private jQueryObject _uiNewDomainDetailsPanelJ;
		public Element uiDomainsRegistered {get {if (_uiDomainsRegistered == null) {_uiDomainsRegistered = (Element)Document.GetElementById(clientId + "_uiDomainsRegistered");}; return _uiDomainsRegistered;}} private Element _uiDomainsRegistered;
		public jQueryObject uiDomainsRegisteredJ {get {if (_uiDomainsRegisteredJ == null) {_uiDomainsRegisteredJ = jQuery.Select("#" + clientId + "_uiDomainsRegistered");}; return _uiDomainsRegisteredJ;}} private jQueryObject _uiDomainsRegisteredJ;//mappings.Add("System.Web.UI.WebControls.Literal", ElementGetter("Element"));
		public InputElement uiDomainName {get {if (_uiDomainName == null) {_uiDomainName = (InputElement)Document.GetElementById(clientId + "_uiDomainName");}; return _uiDomainName;}} private InputElement _uiDomainName;
		public jQueryObject uiDomainNameJ {get {if (_uiDomainNameJ == null) {_uiDomainNameJ = jQuery.Select("#" + clientId + "_uiDomainName");}; return _uiDomainNameJ;}} private jQueryObject _uiDomainNameJ;
		public Element uiDomainAvailability {get {if (_uiDomainAvailability == null) {_uiDomainAvailability = (Element)Document.GetElementById(clientId + "_uiDomainAvailability");}; return _uiDomainAvailability;}} private Element _uiDomainAvailability;
		public jQueryObject uiDomainAvailabilityJ {get {if (_uiDomainAvailabilityJ == null) {_uiDomainAvailabilityJ = jQuery.Select("#" + clientId + "_uiDomainAvailability");}; return _uiDomainAvailabilityJ;}} private jQueryObject _uiDomainAvailabilityJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element uiOptionsList {get {if (_uiOptionsList == null) {_uiOptionsList = (Element)Document.GetElementById(clientId + "_uiOptionsList");}; return _uiOptionsList;}} private Element _uiOptionsList;
		public jQueryObject uiOptionsListJ {get {if (_uiOptionsListJ == null) {_uiOptionsListJ = jQuery.Select("#" + clientId + "_uiOptionsList");}; return _uiOptionsListJ;}} private jQueryObject _uiOptionsListJ;//mappings.Add("Spotted.Controls.OptionsList", ElementGetter("Element"));
		public Element uiBrandDiv {get {if (_uiBrandDiv == null) {_uiBrandDiv = (Element)Document.GetElementById(clientId + "_uiBrandDiv");}; return _uiBrandDiv;}} private Element _uiBrandDiv;
		public jQueryObject uiBrandDivJ {get {if (_uiBrandDivJ == null) {_uiBrandDivJ = jQuery.Select("#" + clientId + "_uiBrandDiv");}; return _uiBrandDivJ;}} private jQueryObject _uiBrandDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public SelectElement uiBrandsDdl {get {if (_uiBrandsDdl == null) {_uiBrandsDdl = (SelectElement)Document.GetElementById(clientId + "_uiBrandsDdl");}; return _uiBrandsDdl;}} private SelectElement _uiBrandsDdl;
		public jQueryObject uiBrandsDdlJ {get {if (_uiBrandsDdlJ == null) {_uiBrandsDdlJ = jQuery.Select("#" + clientId + "_uiBrandsDdl");}; return _uiBrandsDdlJ;}} private jQueryObject _uiBrandsDdlJ;
		public InputElement uiBrandRedirectApp {get {if (_uiBrandRedirectApp == null) {_uiBrandRedirectApp = (InputElement)Document.GetElementById(clientId + "_uiBrandRedirectApp");}; return _uiBrandRedirectApp;}} private InputElement _uiBrandRedirectApp;
		public jQueryObject uiBrandRedirectAppJ {get {if (_uiBrandRedirectAppJ == null) {_uiBrandRedirectAppJ = jQuery.Select("#" + clientId + "_uiBrandRedirectApp");}; return _uiBrandRedirectAppJ;}} private jQueryObject _uiBrandRedirectAppJ;
		public Element uiVenueDiv {get {if (_uiVenueDiv == null) {_uiVenueDiv = (Element)Document.GetElementById(clientId + "_uiVenueDiv");}; return _uiVenueDiv;}} private Element _uiVenueDiv;
		public jQueryObject uiVenueDivJ {get {if (_uiVenueDivJ == null) {_uiVenueDivJ = jQuery.Select("#" + clientId + "_uiVenueDiv");}; return _uiVenueDivJ;}} private jQueryObject _uiVenueDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public SelectElement uiVenuesDdl {get {if (_uiVenuesDdl == null) {_uiVenuesDdl = (SelectElement)Document.GetElementById(clientId + "_uiVenuesDdl");}; return _uiVenuesDdl;}} private SelectElement _uiVenuesDdl;
		public jQueryObject uiVenuesDdlJ {get {if (_uiVenuesDdlJ == null) {_uiVenuesDdlJ = jQuery.Select("#" + clientId + "_uiVenuesDdl");}; return _uiVenuesDdlJ;}} private jQueryObject _uiVenuesDdlJ;
		public InputElement uiVenueRedirectApp {get {if (_uiVenueRedirectApp == null) {_uiVenueRedirectApp = (InputElement)Document.GetElementById(clientId + "_uiVenueRedirectApp");}; return _uiVenueRedirectApp;}} private InputElement _uiVenueRedirectApp;
		public jQueryObject uiVenueRedirectAppJ {get {if (_uiVenueRedirectAppJ == null) {_uiVenueRedirectAppJ = jQuery.Select("#" + clientId + "_uiVenueRedirectApp");}; return _uiVenueRedirectAppJ;}} private jQueryObject _uiVenueRedirectAppJ;
		public Element uiEventDiv {get {if (_uiEventDiv == null) {_uiEventDiv = (Element)Document.GetElementById(clientId + "_uiEventDiv");}; return _uiEventDiv;}} private Element _uiEventDiv;
		public jQueryObject uiEventDivJ {get {if (_uiEventDivJ == null) {_uiEventDivJ = jQuery.Select("#" + clientId + "_uiEventDiv");}; return _uiEventDivJ;}} private jQueryObject _uiEventDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement uiEventK {get {if (_uiEventK == null) {_uiEventK = (InputElement)Document.GetElementById(clientId + "_uiEventK");}; return _uiEventK;}} private InputElement _uiEventK;
		public jQueryObject uiEventKJ {get {if (_uiEventKJ == null) {_uiEventKJ = jQuery.Select("#" + clientId + "_uiEventK");}; return _uiEventKJ;}} private jQueryObject _uiEventKJ;
		public Element uiGroupDiv {get {if (_uiGroupDiv == null) {_uiGroupDiv = (Element)Document.GetElementById(clientId + "_uiGroupDiv");}; return _uiGroupDiv;}} private Element _uiGroupDiv;
		public jQueryObject uiGroupDivJ {get {if (_uiGroupDivJ == null) {_uiGroupDivJ = jQuery.Select("#" + clientId + "_uiGroupDiv");}; return _uiGroupDivJ;}} private jQueryObject _uiGroupDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement uiGroupK {get {if (_uiGroupK == null) {_uiGroupK = (InputElement)Document.GetElementById(clientId + "_uiGroupK");}; return _uiGroupK;}} private InputElement _uiGroupK;
		public jQueryObject uiGroupKJ {get {if (_uiGroupKJ == null) {_uiGroupKJ = jQuery.Select("#" + clientId + "_uiGroupK");}; return _uiGroupKJ;}} private jQueryObject _uiGroupKJ;
		public Element uiCustomUrlDiv {get {if (_uiCustomUrlDiv == null) {_uiCustomUrlDiv = (Element)Document.GetElementById(clientId + "_uiCustomUrlDiv");}; return _uiCustomUrlDiv;}} private Element _uiCustomUrlDiv;
		public jQueryObject uiCustomUrlDivJ {get {if (_uiCustomUrlDivJ == null) {_uiCustomUrlDivJ = jQuery.Select("#" + clientId + "_uiCustomUrlDiv");}; return _uiCustomUrlDivJ;}} private jQueryObject _uiCustomUrlDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement uiCustomUrlText {get {if (_uiCustomUrlText == null) {_uiCustomUrlText = (InputElement)Document.GetElementById(clientId + "_uiCustomUrlText");}; return _uiCustomUrlText;}} private InputElement _uiCustomUrlText;
		public jQueryObject uiCustomUrlTextJ {get {if (_uiCustomUrlTextJ == null) {_uiCustomUrlTextJ = jQuery.Select("#" + clientId + "_uiCustomUrlText");}; return _uiCustomUrlTextJ;}} private jQueryObject _uiCustomUrlTextJ;
		public Element uiRegisterDomainButton {get {if (_uiRegisterDomainButton == null) {_uiRegisterDomainButton = (Element)Document.GetElementById(clientId + "_uiRegisterDomainButton");}; return _uiRegisterDomainButton;}} private Element _uiRegisterDomainButton;
		public jQueryObject uiRegisterDomainButtonJ {get {if (_uiRegisterDomainButtonJ == null) {_uiRegisterDomainButtonJ = jQuery.Select("#" + clientId + "_uiRegisterDomainButton");}; return _uiRegisterDomainButtonJ;}} private jQueryObject _uiRegisterDomainButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement uiTestRedirectPanel {get {if (_uiTestRedirectPanel == null) {_uiTestRedirectPanel = (DivElement)Document.GetElementById(clientId + "_uiTestRedirectPanel");}; return _uiTestRedirectPanel;}} private DivElement _uiTestRedirectPanel;
		public jQueryObject uiTestRedirectPanelJ {get {if (_uiTestRedirectPanelJ == null) {_uiTestRedirectPanelJ = jQuery.Select("#" + clientId + "_uiTestRedirectPanel");}; return _uiTestRedirectPanelJ;}} private jQueryObject _uiTestRedirectPanelJ;
		public Element uiAddedDomain {get {if (_uiAddedDomain == null) {_uiAddedDomain = (Element)Document.GetElementById(clientId + "_uiAddedDomain");}; return _uiAddedDomain;}} private Element _uiAddedDomain;
		public jQueryObject uiAddedDomainJ {get {if (_uiAddedDomainJ == null) {_uiAddedDomainJ = jQuery.Select("#" + clientId + "_uiAddedDomain");}; return _uiAddedDomainJ;}} private jQueryObject _uiAddedDomainJ;//mappings.Add("System.Web.UI.WebControls.Literal", ElementGetter("Element"));
		public Element uiErrorLbl {get {if (_uiErrorLbl == null) {_uiErrorLbl = (Element)Document.GetElementById(clientId + "_uiErrorLbl");}; return _uiErrorLbl;}} private Element _uiErrorLbl;
		public jQueryObject uiErrorLblJ {get {if (_uiErrorLblJ == null) {_uiErrorLblJ = jQuery.Select("#" + clientId + "_uiErrorLbl");}; return _uiErrorLblJ;}} private jQueryObject _uiErrorLblJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
