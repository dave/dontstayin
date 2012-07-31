//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.PromoterWithTicketRunsVatStatus
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
		public DivElement PromoterWithTicketRunsVatStatusPanel {get {if (_PromoterWithTicketRunsVatStatusPanel == null) {_PromoterWithTicketRunsVatStatusPanel = (DivElement)Document.GetElementById(clientId + "_PromoterWithTicketRunsVatStatusPanel");}; return _PromoterWithTicketRunsVatStatusPanel;}} private DivElement _PromoterWithTicketRunsVatStatusPanel;
		public jQueryObject PromoterWithTicketRunsVatStatusPanelJ {get {if (_PromoterWithTicketRunsVatStatusPanelJ == null) {_PromoterWithTicketRunsVatStatusPanelJ = jQuery.Select("#" + clientId + "_PromoterWithTicketRunsVatStatusPanel");}; return _PromoterWithTicketRunsVatStatusPanelJ;}} private jQueryObject _PromoterWithTicketRunsVatStatusPanelJ;
		public SelectElement VatStatusDropDownList {get {if (_VatStatusDropDownList == null) {_VatStatusDropDownList = (SelectElement)Document.GetElementById(clientId + "_VatStatusDropDownList");}; return _VatStatusDropDownList;}} private SelectElement _VatStatusDropDownList;
		public jQueryObject VatStatusDropDownListJ {get {if (_VatStatusDropDownListJ == null) {_VatStatusDropDownListJ = jQuery.Select("#" + clientId + "_VatStatusDropDownList");}; return _VatStatusDropDownListJ;}} private jQueryObject _VatStatusDropDownListJ;
		public InputElement SendReminderEmailForUnknownVatStatusPromotersButton {get {if (_SendReminderEmailForUnknownVatStatusPromotersButton == null) {_SendReminderEmailForUnknownVatStatusPromotersButton = (InputElement)Document.GetElementById(clientId + "_SendReminderEmailForUnknownVatStatusPromotersButton");}; return _SendReminderEmailForUnknownVatStatusPromotersButton;}} private InputElement _SendReminderEmailForUnknownVatStatusPromotersButton;
		public jQueryObject SendReminderEmailForUnknownVatStatusPromotersButtonJ {get {if (_SendReminderEmailForUnknownVatStatusPromotersButtonJ == null) {_SendReminderEmailForUnknownVatStatusPromotersButtonJ = jQuery.Select("#" + clientId + "_SendReminderEmailForUnknownVatStatusPromotersButton");}; return _SendReminderEmailForUnknownVatStatusPromotersButtonJ;}} private jQueryObject _SendReminderEmailForUnknownVatStatusPromotersButtonJ;
		public Element UnknownPromoterVatStatusGridView {get {if (_UnknownPromoterVatStatusGridView == null) {_UnknownPromoterVatStatusGridView = (Element)Document.GetElementById(clientId + "_UnknownPromoterVatStatusGridView");}; return _UnknownPromoterVatStatusGridView;}} private Element _UnknownPromoterVatStatusGridView;
		public jQueryObject UnknownPromoterVatStatusGridViewJ {get {if (_UnknownPromoterVatStatusGridViewJ == null) {_UnknownPromoterVatStatusGridViewJ = jQuery.Select("#" + clientId + "_UnknownPromoterVatStatusGridView");}; return _UnknownPromoterVatStatusGridViewJ;}} private jQueryObject _UnknownPromoterVatStatusGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
