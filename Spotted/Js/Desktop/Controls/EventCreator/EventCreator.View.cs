//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.VenueGetter", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.EventCreator
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element uiContainer {get {if (_uiContainer == null) {_uiContainer = (Element)Document.GetElementById(clientId + "_uiContainer");}; return _uiContainer;}} private Element _uiContainer;
		public jQueryObject uiContainerJ {get {if (_uiContainerJ == null) {_uiContainerJ = jQuery.Select("#" + clientId + "_uiContainer");}; return _uiContainerJ;}} private jQueryObject _uiContainerJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element H1 {get {if (_H1 == null) {_H1 = (Element)Document.GetElementById(clientId + "_H1");}; return _H1;}} private Element _H1;
		public jQueryObject H1J {get {if (_H1J == null) {_H1J = jQuery.Select("#" + clientId + "_H1");}; return _H1J;}} private jQueryObject _H1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Js.CustomControls.Cal.Controller uiCal {get {return (Js.CustomControls.Cal.Controller) Script.Eval(clientId + "_uiCalController");}}
		public Element uiVenueGetter {get {if (_uiVenueGetter == null) {_uiVenueGetter = (Element)Document.GetElementById(clientId + "_uiVenueGetter");}; return _uiVenueGetter;}} private Element _uiVenueGetter;
		public jQueryObject uiVenueGetterJ {get {if (_uiVenueGetterJ == null) {_uiVenueGetterJ = jQuery.Select("#" + clientId + "_uiVenueGetter");}; return _uiVenueGetterJ;}} private jQueryObject _uiVenueGetterJ;//mappings.Add("Spotted.Controls.VenueGetter", ElementGetter("Element"));
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiEventName {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiEventNameBehaviour");}}
		public Element uiAddOptionsPanel {get {if (_uiAddOptionsPanel == null) {_uiAddOptionsPanel = (Element)Document.GetElementById(clientId + "_uiAddOptionsPanel");}; return _uiAddOptionsPanel;}} private Element _uiAddOptionsPanel;
		public jQueryObject uiAddOptionsPanelJ {get {if (_uiAddOptionsPanelJ == null) {_uiAddOptionsPanelJ = jQuery.Select("#" + clientId + "_uiAddOptionsPanel");}; return _uiAddOptionsPanelJ;}} private jQueryObject _uiAddOptionsPanelJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement uiSummary {get {if (_uiSummary == null) {_uiSummary = (InputElement)Document.GetElementById(clientId + "_uiSummary");}; return _uiSummary;}} private InputElement _uiSummary;
		public jQueryObject uiSummaryJ {get {if (_uiSummaryJ == null) {_uiSummaryJ = jQuery.Select("#" + clientId + "_uiSummary");}; return _uiSummaryJ;}} private jQueryObject _uiSummaryJ;
		public Js.ClientControls.HtmlAutoCompleteBehaviour uiBrand {get {return (Js.ClientControls.HtmlAutoCompleteBehaviour) Script.Eval(clientId + "_uiBrandBehaviour");}}
		public Element uiAdd {get {if (_uiAdd == null) {_uiAdd = (Element)Document.GetElementById(clientId + "_uiAdd");}; return _uiAdd;}} private Element _uiAdd;
		public jQueryObject uiAddJ {get {if (_uiAddJ == null) {_uiAddJ = jQuery.Select("#" + clientId + "_uiAdd");}; return _uiAddJ;}} private jQueryObject _uiAddJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
	}
}
