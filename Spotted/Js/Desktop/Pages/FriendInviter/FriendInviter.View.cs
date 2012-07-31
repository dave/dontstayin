//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.BuddyImporter", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.FriendInviter
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
		public Element uiH1 {get {if (_uiH1 == null) {_uiH1 = (Element)Document.GetElementById(clientId + "_uiH1");}; return _uiH1;}} private Element _uiH1;
		public jQueryObject uiH1J {get {if (_uiH1J == null) {_uiH1J = jQuery.Select("#" + clientId + "_uiH1");}; return _uiH1J;}} private jQueryObject _uiH1J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement uiIntroPanel {get {if (_uiIntroPanel == null) {_uiIntroPanel = (DivElement)Document.GetElementById(clientId + "_uiIntroPanel");}; return _uiIntroPanel;}} private DivElement _uiIntroPanel;
		public jQueryObject uiIntroPanelJ {get {if (_uiIntroPanelJ == null) {_uiIntroPanelJ = jQuery.Select("#" + clientId + "_uiIntroPanel");}; return _uiIntroPanelJ;}} private jQueryObject _uiIntroPanelJ;
		public Element uiBuddyImporter {get {if (_uiBuddyImporter == null) {_uiBuddyImporter = (Element)Document.GetElementById(clientId + "_uiBuddyImporter");}; return _uiBuddyImporter;}} private Element _uiBuddyImporter;
		public jQueryObject uiBuddyImporterJ {get {if (_uiBuddyImporterJ == null) {_uiBuddyImporterJ = jQuery.Select("#" + clientId + "_uiBuddyImporter");}; return _uiBuddyImporterJ;}} private jQueryObject _uiBuddyImporterJ;//mappings.Add("Spotted.Controls.BuddyImporter", ElementGetter("Element"));
		public DivElement uiSuccessPanel {get {if (_uiSuccessPanel == null) {_uiSuccessPanel = (DivElement)Document.GetElementById(clientId + "_uiSuccessPanel");}; return _uiSuccessPanel;}} private DivElement _uiSuccessPanel;
		public jQueryObject uiSuccessPanelJ {get {if (_uiSuccessPanelJ == null) {_uiSuccessPanelJ = jQuery.Select("#" + clientId + "_uiSuccessPanel");}; return _uiSuccessPanelJ;}} private jQueryObject _uiSuccessPanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
