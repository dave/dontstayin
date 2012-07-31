//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Doorlist", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.ConfirmCardDetails
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
		public DivElement uiSelect {get {if (_uiSelect == null) {_uiSelect = (DivElement)Document.GetElementById(clientId + "_uiSelect");}; return _uiSelect;}} private DivElement _uiSelect;
		public jQueryObject uiSelectJ {get {if (_uiSelectJ == null) {_uiSelectJ = jQuery.Select("#" + clientId + "_uiSelect");}; return _uiSelectJ;}} private jQueryObject _uiSelectJ;
		public SelectElement uiEvents {get {if (_uiEvents == null) {_uiEvents = (SelectElement)Document.GetElementById(clientId + "_uiEvents");}; return _uiEvents;}} private SelectElement _uiEvents;
		public jQueryObject uiEventsJ {get {if (_uiEventsJ == null) {_uiEventsJ = jQuery.Select("#" + clientId + "_uiEvents");}; return _uiEventsJ;}} private jQueryObject _uiEventsJ;
		public DivElement uiNoEvents {get {if (_uiNoEvents == null) {_uiNoEvents = (DivElement)Document.GetElementById(clientId + "_uiNoEvents");}; return _uiNoEvents;}} private DivElement _uiNoEvents;
		public jQueryObject uiNoEventsJ {get {if (_uiNoEventsJ == null) {_uiNoEventsJ = jQuery.Select("#" + clientId + "_uiNoEvents");}; return _uiNoEventsJ;}} private jQueryObject _uiNoEventsJ;
		public DivElement uiDoorlistPanel {get {if (_uiDoorlistPanel == null) {_uiDoorlistPanel = (DivElement)Document.GetElementById(clientId + "_uiDoorlistPanel");}; return _uiDoorlistPanel;}} private DivElement _uiDoorlistPanel;
		public jQueryObject uiDoorlistPanelJ {get {if (_uiDoorlistPanelJ == null) {_uiDoorlistPanelJ = jQuery.Select("#" + clientId + "_uiDoorlistPanel");}; return _uiDoorlistPanelJ;}} private jQueryObject _uiDoorlistPanelJ;
		public Element uiDoorlist {get {if (_uiDoorlist == null) {_uiDoorlist = (Element)Document.GetElementById(clientId + "_uiDoorlist");}; return _uiDoorlist;}} private Element _uiDoorlist;
		public jQueryObject uiDoorlistJ {get {if (_uiDoorlistJ == null) {_uiDoorlistJ = jQuery.Select("#" + clientId + "_uiDoorlist");}; return _uiDoorlistJ;}} private jQueryObject _uiDoorlistJ;//mappings.Add("Spotted.Controls.Doorlist", ElementGetter("Element"));
		public InputElement uiSave {get {if (_uiSave == null) {_uiSave = (InputElement)Document.GetElementById(clientId + "_uiSave");}; return _uiSave;}} private InputElement _uiSave;
		public jQueryObject uiSaveJ {get {if (_uiSaveJ == null) {_uiSaveJ = jQuery.Select("#" + clientId + "_uiSave");}; return _uiSaveJ;}} private jQueryObject _uiSaveJ;
		public Element uiSomeWrongLabel {get {if (_uiSomeWrongLabel == null) {_uiSomeWrongLabel = (Element)Document.GetElementById(clientId + "_uiSomeWrongLabel");}; return _uiSomeWrongLabel;}} private Element _uiSomeWrongLabel;
		public jQueryObject uiSomeWrongLabelJ {get {if (_uiSomeWrongLabelJ == null) {_uiSomeWrongLabelJ = jQuery.Select("#" + clientId + "_uiSomeWrongLabel");}; return _uiSomeWrongLabelJ;}} private jQueryObject _uiSomeWrongLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
