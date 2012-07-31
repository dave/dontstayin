//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.Doorlist
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
		public Element PromoterIntro {get {if (_PromoterIntro == null) {_PromoterIntro = (Element)Document.GetElementById(clientId + "_PromoterIntro");}; return _PromoterIntro;}} private Element _PromoterIntro;
		public jQueryObject PromoterIntroJ {get {if (_PromoterIntroJ == null) {_PromoterIntroJ = jQuery.Select("#" + clientId + "_PromoterIntro");}; return _PromoterIntroJ;}} private jQueryObject _PromoterIntroJ;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public DivElement DoorlistPanel {get {if (_DoorlistPanel == null) {_DoorlistPanel = (DivElement)Document.GetElementById(clientId + "_DoorlistPanel");}; return _DoorlistPanel;}} private DivElement _DoorlistPanel;
		public jQueryObject DoorlistPanelJ {get {if (_DoorlistPanelJ == null) {_DoorlistPanelJ = jQuery.Select("#" + clientId + "_DoorlistPanel");}; return _DoorlistPanelJ;}} private jQueryObject _DoorlistPanelJ;
		public Element H1Title {get {if (_H1Title == null) {_H1Title = (Element)Document.GetElementById(clientId + "_H1Title");}; return _H1Title;}} private Element _H1Title;
		public jQueryObject H1TitleJ {get {if (_H1TitleJ == null) {_H1TitleJ = jQuery.Select("#" + clientId + "_H1Title");}; return _H1TitleJ;}} private jQueryObject _H1TitleJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element HasTicketsP {get {if (_HasTicketsP == null) {_HasTicketsP = (Element)Document.GetElementById(clientId + "_HasTicketsP");}; return _HasTicketsP;}} private Element _HasTicketsP;
		public jQueryObject HasTicketsPJ {get {if (_HasTicketsPJ == null) {_HasTicketsPJ = jQuery.Select("#" + clientId + "_HasTicketsP");}; return _HasTicketsPJ;}} private jQueryObject _HasTicketsPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public SelectElement EventDropDownList {get {if (_EventDropDownList == null) {_EventDropDownList = (SelectElement)Document.GetElementById(clientId + "_EventDropDownList");}; return _EventDropDownList;}} private SelectElement _EventDropDownList;
		public jQueryObject EventDropDownListJ {get {if (_EventDropDownListJ == null) {_EventDropDownListJ = jQuery.Select("#" + clientId + "_EventDropDownList");}; return _EventDropDownListJ;}} private jQueryObject _EventDropDownListJ;
		public Element DoorlistButton {get {if (_DoorlistButton == null) {_DoorlistButton = (Element)Document.GetElementById(clientId + "_DoorlistButton");}; return _DoorlistButton;}} private Element _DoorlistButton;
		public jQueryObject DoorlistButtonJ {get {if (_DoorlistButtonJ == null) {_DoorlistButtonJ = jQuery.Select("#" + clientId + "_DoorlistButton");}; return _DoorlistButtonJ;}} private jQueryObject _DoorlistButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element NoTicketsP {get {if (_NoTicketsP == null) {_NoTicketsP = (Element)Document.GetElementById(clientId + "_NoTicketsP");}; return _NoTicketsP;}} private Element _NoTicketsP;
		public jQueryObject NoTicketsPJ {get {if (_NoTicketsPJ == null) {_NoTicketsPJ = jQuery.Select("#" + clientId + "_NoTicketsP");}; return _NoTicketsPJ;}} private jQueryObject _NoTicketsPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
