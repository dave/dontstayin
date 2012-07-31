//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.Events
{
	public partial class View
		 : Js.Pages.Promoters.PromoterUserControl.View
	{
		public string clientId;
		public View(string clientId)
			 : base(clientId)
		{
			this.clientId = clientId;
		}
		public Element Promoterintro2 {get {if (_Promoterintro2 == null) {_Promoterintro2 = (Element)Document.GetElementById(clientId + "_Promoterintro2");}; return _Promoterintro2;}} private Element _Promoterintro2;
		public jQueryObject Promoterintro2J {get {if (_Promoterintro2J == null) {_Promoterintro2J = jQuery.Select("#" + clientId + "_Promoterintro2");}; return _Promoterintro2J;}} private jQueryObject _Promoterintro2J;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element EventsGridView {get {if (_EventsGridView == null) {_EventsGridView = (Element)Document.GetElementById(clientId + "_EventsGridView");}; return _EventsGridView;}} private Element _EventsGridView;
		public jQueryObject EventsGridViewJ {get {if (_EventsGridViewJ == null) {_EventsGridViewJ = jQuery.Select("#" + clientId + "_EventsGridView");}; return _EventsGridViewJ;}} private jQueryObject _EventsGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public DivElement PanelEventsList {get {if (_PanelEventsList == null) {_PanelEventsList = (DivElement)Document.GetElementById(clientId + "_PanelEventsList");}; return _PanelEventsList;}} private DivElement _PanelEventsList;
		public jQueryObject PanelEventsListJ {get {if (_PanelEventsListJ == null) {_PanelEventsListJ = jQuery.Select("#" + clientId + "_PanelEventsList");}; return _PanelEventsListJ;}} private jQueryObject _PanelEventsListJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
