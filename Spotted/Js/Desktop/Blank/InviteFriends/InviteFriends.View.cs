//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.BuddyImporter", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Blank.InviteFriends
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
		public InputElement uiSkipButton {get {if (_uiSkipButton == null) {_uiSkipButton = (InputElement)Document.GetElementById(clientId + "_uiSkipButton");}; return _uiSkipButton;}} private InputElement _uiSkipButton;
		public jQueryObject uiSkipButtonJ {get {if (_uiSkipButtonJ == null) {_uiSkipButtonJ = jQuery.Select("#" + clientId + "_uiSkipButton");}; return _uiSkipButtonJ;}} private jQueryObject _uiSkipButtonJ;
		public InputElement uiGoToSiteButton {get {if (_uiGoToSiteButton == null) {_uiGoToSiteButton = (InputElement)Document.GetElementById(clientId + "_uiGoToSiteButton");}; return _uiGoToSiteButton;}} private InputElement _uiGoToSiteButton;
		public jQueryObject uiGoToSiteButtonJ {get {if (_uiGoToSiteButtonJ == null) {_uiGoToSiteButtonJ = jQuery.Select("#" + clientId + "_uiGoToSiteButton");}; return _uiGoToSiteButtonJ;}} private jQueryObject _uiGoToSiteButtonJ;
		public Element uiBuddyImporterDiv {get {if (_uiBuddyImporterDiv == null) {_uiBuddyImporterDiv = (Element)Document.GetElementById(clientId + "_uiBuddyImporterDiv");}; return _uiBuddyImporterDiv;}} private Element _uiBuddyImporterDiv;
		public jQueryObject uiBuddyImporterDivJ {get {if (_uiBuddyImporterDivJ == null) {_uiBuddyImporterDivJ = jQuery.Select("#" + clientId + "_uiBuddyImporterDiv");}; return _uiBuddyImporterDivJ;}} private jQueryObject _uiBuddyImporterDivJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element uiBuddyImporter {get {if (_uiBuddyImporter == null) {_uiBuddyImporter = (Element)Document.GetElementById(clientId + "_uiBuddyImporter");}; return _uiBuddyImporter;}} private Element _uiBuddyImporter;
		public jQueryObject uiBuddyImporterJ {get {if (_uiBuddyImporterJ == null) {_uiBuddyImporterJ = jQuery.Select("#" + clientId + "_uiBuddyImporter");}; return _uiBuddyImporterJ;}} private jQueryObject _uiBuddyImporterJ;//mappings.Add("Spotted.Controls.BuddyImporter", ElementGetter("Element"));
		public DivElement uiFinishedPanel {get {if (_uiFinishedPanel == null) {_uiFinishedPanel = (DivElement)Document.GetElementById(clientId + "_uiFinishedPanel");}; return _uiFinishedPanel;}} private DivElement _uiFinishedPanel;
		public jQueryObject uiFinishedPanelJ {get {if (_uiFinishedPanelJ == null) {_uiFinishedPanelJ = jQuery.Select("#" + clientId + "_uiFinishedPanel");}; return _uiFinishedPanelJ;}} private jQueryObject _uiFinishedPanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
