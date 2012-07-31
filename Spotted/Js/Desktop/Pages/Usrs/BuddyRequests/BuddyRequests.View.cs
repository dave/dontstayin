//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Usrs.BuddyRequests
{
	public partial class View
		 : Js.Pages.Usrs.UsrUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element H18 {get {if (_H18 == null) {_H18 = (Element)Document.GetElementById(clientId + "_H18");}; return _H18;}} private Element _H18;
		public jQueryObject H18J {get {if (_H18J == null) {_H18J = jQuery.Select("#" + clientId + "_H18");}; return _H18J;}} private jQueryObject _H18J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement uiNoBuddyRequestsPanel {get {if (_uiNoBuddyRequestsPanel == null) {_uiNoBuddyRequestsPanel = (DivElement)Document.GetElementById(clientId + "_uiNoBuddyRequestsPanel");}; return _uiNoBuddyRequestsPanel;}} private DivElement _uiNoBuddyRequestsPanel;
		public jQueryObject uiNoBuddyRequestsPanelJ {get {if (_uiNoBuddyRequestsPanelJ == null) {_uiNoBuddyRequestsPanelJ = jQuery.Select("#" + clientId + "_uiNoBuddyRequestsPanel");}; return _uiNoBuddyRequestsPanelJ;}} private jQueryObject _uiNoBuddyRequestsPanelJ;
		public DivElement uiBuddyRequestsPanel {get {if (_uiBuddyRequestsPanel == null) {_uiBuddyRequestsPanel = (DivElement)Document.GetElementById(clientId + "_uiBuddyRequestsPanel");}; return _uiBuddyRequestsPanel;}} private DivElement _uiBuddyRequestsPanel;
		public jQueryObject uiBuddyRequestsPanelJ {get {if (_uiBuddyRequestsPanelJ == null) {_uiBuddyRequestsPanelJ = jQuery.Select("#" + clientId + "_uiBuddyRequestsPanel");}; return _uiBuddyRequestsPanelJ;}} private jQueryObject _uiBuddyRequestsPanelJ;
		public DivElement uiMultiButtonsPanel {get {if (_uiMultiButtonsPanel == null) {_uiMultiButtonsPanel = (DivElement)Document.GetElementById(clientId + "_uiMultiButtonsPanel");}; return _uiMultiButtonsPanel;}} private DivElement _uiMultiButtonsPanel;
		public jQueryObject uiMultiButtonsPanelJ {get {if (_uiMultiButtonsPanelJ == null) {_uiMultiButtonsPanelJ = jQuery.Select("#" + clientId + "_uiMultiButtonsPanel");}; return _uiMultiButtonsPanelJ;}} private jQueryObject _uiMultiButtonsPanelJ;
		public Element uiBuddiesRequested {get {if (_uiBuddiesRequested == null) {_uiBuddiesRequested = (Element)Document.GetElementById(clientId + "_uiBuddiesRequested");}; return _uiBuddiesRequested;}} private Element _uiBuddiesRequested;
		public jQueryObject uiBuddiesRequestedJ {get {if (_uiBuddiesRequestedJ == null) {_uiBuddiesRequestedJ = jQuery.Select("#" + clientId + "_uiBuddiesRequested");}; return _uiBuddiesRequestedJ;}} private jQueryObject _uiBuddiesRequestedJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
