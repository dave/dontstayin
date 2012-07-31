//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Usrs.MyTickets
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
		public DivElement MyTicketsPanel {get {if (_MyTicketsPanel == null) {_MyTicketsPanel = (DivElement)Document.GetElementById(clientId + "_MyTicketsPanel");}; return _MyTicketsPanel;}} private DivElement _MyTicketsPanel;
		public jQueryObject MyTicketsPanelJ {get {if (_MyTicketsPanelJ == null) {_MyTicketsPanelJ = jQuery.Select("#" + clientId + "_MyTicketsPanel");}; return _MyTicketsPanelJ;}} private jQueryObject _MyTicketsPanelJ;
		public Element MyTicketsHeading {get {if (_MyTicketsHeading == null) {_MyTicketsHeading = (Element)Document.GetElementById(clientId + "_MyTicketsHeading");}; return _MyTicketsHeading;}} private Element _MyTicketsHeading;
		public jQueryObject MyTicketsHeadingJ {get {if (_MyTicketsHeadingJ == null) {_MyTicketsHeadingJ = jQuery.Select("#" + clientId + "_MyTicketsHeading");}; return _MyTicketsHeadingJ;}} private jQueryObject _MyTicketsHeadingJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element SelectCurrentDateRangeLinkButton {get {if (_SelectCurrentDateRangeLinkButton == null) {_SelectCurrentDateRangeLinkButton = (Element)Document.GetElementById(clientId + "_SelectCurrentDateRangeLinkButton");}; return _SelectCurrentDateRangeLinkButton;}} private Element _SelectCurrentDateRangeLinkButton;
		public jQueryObject SelectCurrentDateRangeLinkButtonJ {get {if (_SelectCurrentDateRangeLinkButtonJ == null) {_SelectCurrentDateRangeLinkButtonJ = jQuery.Select("#" + clientId + "_SelectCurrentDateRangeLinkButton");}; return _SelectCurrentDateRangeLinkButtonJ;}} private jQueryObject _SelectCurrentDateRangeLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element SelectPastDateRangeLinkButton {get {if (_SelectPastDateRangeLinkButton == null) {_SelectPastDateRangeLinkButton = (Element)Document.GetElementById(clientId + "_SelectPastDateRangeLinkButton");}; return _SelectPastDateRangeLinkButton;}} private Element _SelectPastDateRangeLinkButton;
		public jQueryObject SelectPastDateRangeLinkButtonJ {get {if (_SelectPastDateRangeLinkButtonJ == null) {_SelectPastDateRangeLinkButtonJ = jQuery.Select("#" + clientId + "_SelectPastDateRangeLinkButton");}; return _SelectPastDateRangeLinkButtonJ;}} private jQueryObject _SelectPastDateRangeLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element SelectAllDateRangeLinkButton {get {if (_SelectAllDateRangeLinkButton == null) {_SelectAllDateRangeLinkButton = (Element)Document.GetElementById(clientId + "_SelectAllDateRangeLinkButton");}; return _SelectAllDateRangeLinkButton;}} private Element _SelectAllDateRangeLinkButton;
		public jQueryObject SelectAllDateRangeLinkButtonJ {get {if (_SelectAllDateRangeLinkButtonJ == null) {_SelectAllDateRangeLinkButtonJ = jQuery.Select("#" + clientId + "_SelectAllDateRangeLinkButton");}; return _SelectAllDateRangeLinkButtonJ;}} private jQueryObject _SelectAllDateRangeLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public DivElement uiHasETickets {get {if (_uiHasETickets == null) {_uiHasETickets = (DivElement)Document.GetElementById(clientId + "_uiHasETickets");}; return _uiHasETickets;}} private DivElement _uiHasETickets;
		public jQueryObject uiHasETicketsJ {get {if (_uiHasETicketsJ == null) {_uiHasETicketsJ = jQuery.Select("#" + clientId + "_uiHasETickets");}; return _uiHasETicketsJ;}} private jQueryObject _uiHasETicketsJ;
		public Element MyEventTicketsRepeater {get {if (_MyEventTicketsRepeater == null) {_MyEventTicketsRepeater = (Element)Document.GetElementById(clientId + "_MyEventTicketsRepeater");}; return _MyEventTicketsRepeater;}} private Element _MyEventTicketsRepeater;
		public jQueryObject MyEventTicketsRepeaterJ {get {if (_MyEventTicketsRepeaterJ == null) {_MyEventTicketsRepeaterJ = jQuery.Select("#" + clientId + "_MyEventTicketsRepeater");}; return _MyEventTicketsRepeaterJ;}} private jQueryObject _MyEventTicketsRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public DivElement NoTickets {get {if (_NoTickets == null) {_NoTickets = (DivElement)Document.GetElementById(clientId + "_NoTickets");}; return _NoTickets;}} private DivElement _NoTickets;
		public jQueryObject NoTicketsJ {get {if (_NoTicketsJ == null) {_NoTicketsJ = jQuery.Select("#" + clientId + "_NoTickets");}; return _NoTicketsJ;}} private jQueryObject _NoTicketsJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
