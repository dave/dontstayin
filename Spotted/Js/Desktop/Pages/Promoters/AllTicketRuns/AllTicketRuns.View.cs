//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.AllTicketRuns
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
		public DivElement AddTicketRunPanel {get {if (_AddTicketRunPanel == null) {_AddTicketRunPanel = (DivElement)Document.GetElementById(clientId + "_AddTicketRunPanel");}; return _AddTicketRunPanel;}} private DivElement _AddTicketRunPanel;
		public jQueryObject AddTicketRunPanelJ {get {if (_AddTicketRunPanelJ == null) {_AddTicketRunPanelJ = jQuery.Select("#" + clientId + "_AddTicketRunPanel");}; return _AddTicketRunPanelJ;}} private jQueryObject _AddTicketRunPanelJ;
		public Element TicketSalesSummaryTable {get {if (_TicketSalesSummaryTable == null) {_TicketSalesSummaryTable = (Element)Document.GetElementById(clientId + "_TicketSalesSummaryTable");}; return _TicketSalesSummaryTable;}} private Element _TicketSalesSummaryTable;
		public jQueryObject TicketSalesSummaryTableJ {get {if (_TicketSalesSummaryTableJ == null) {_TicketSalesSummaryTableJ = jQuery.Select("#" + clientId + "_TicketSalesSummaryTable");}; return _TicketSalesSummaryTableJ;}} private jQueryObject _TicketSalesSummaryTableJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTable", ElementGetter("Element"));
		public Element TotalTicketRunsLabel {get {if (_TotalTicketRunsLabel == null) {_TotalTicketRunsLabel = (Element)Document.GetElementById(clientId + "_TotalTicketRunsLabel");}; return _TotalTicketRunsLabel;}} private Element _TotalTicketRunsLabel;
		public jQueryObject TotalTicketRunsLabelJ {get {if (_TotalTicketRunsLabelJ == null) {_TotalTicketRunsLabelJ = jQuery.Select("#" + clientId + "_TotalTicketRunsLabel");}; return _TotalTicketRunsLabelJ;}} private jQueryObject _TotalTicketRunsLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element TotalTicketsSoldLabel {get {if (_TotalTicketsSoldLabel == null) {_TotalTicketsSoldLabel = (Element)Document.GetElementById(clientId + "_TotalTicketsSoldLabel");}; return _TotalTicketsSoldLabel;}} private Element _TotalTicketsSoldLabel;
		public jQueryObject TotalTicketsSoldLabelJ {get {if (_TotalTicketsSoldLabelJ == null) {_TotalTicketsSoldLabelJ = jQuery.Select("#" + clientId + "_TotalTicketsSoldLabel");}; return _TotalTicketsSoldLabelJ;}} private jQueryObject _TotalTicketsSoldLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element TicketFundsReleasedLabel {get {if (_TicketFundsReleasedLabel == null) {_TicketFundsReleasedLabel = (Element)Document.GetElementById(clientId + "_TicketFundsReleasedLabel");}; return _TicketFundsReleasedLabel;}} private Element _TicketFundsReleasedLabel;
		public jQueryObject TicketFundsReleasedLabelJ {get {if (_TicketFundsReleasedLabelJ == null) {_TicketFundsReleasedLabelJ = jQuery.Select("#" + clientId + "_TicketFundsReleasedLabel");}; return _TicketFundsReleasedLabelJ;}} private jQueryObject _TicketFundsReleasedLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element TicketFundsInWaitingLabel {get {if (_TicketFundsInWaitingLabel == null) {_TicketFundsInWaitingLabel = (Element)Document.GetElementById(clientId + "_TicketFundsInWaitingLabel");}; return _TicketFundsInWaitingLabel;}} private Element _TicketFundsInWaitingLabel;
		public jQueryObject TicketFundsInWaitingLabelJ {get {if (_TicketFundsInWaitingLabelJ == null) {_TicketFundsInWaitingLabelJ = jQuery.Select("#" + clientId + "_TicketFundsInWaitingLabel");}; return _TicketFundsInWaitingLabelJ;}} private jQueryObject _TicketFundsInWaitingLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement AllTicketRunsPanel {get {if (_AllTicketRunsPanel == null) {_AllTicketRunsPanel = (DivElement)Document.GetElementById(clientId + "_AllTicketRunsPanel");}; return _AllTicketRunsPanel;}} private DivElement _AllTicketRunsPanel;
		public jQueryObject AllTicketRunsPanelJ {get {if (_AllTicketRunsPanelJ == null) {_AllTicketRunsPanelJ = jQuery.Select("#" + clientId + "_AllTicketRunsPanel");}; return _AllTicketRunsPanelJ;}} private jQueryObject _AllTicketRunsPanelJ;
		public Element H1Title {get {if (_H1Title == null) {_H1Title = (Element)Document.GetElementById(clientId + "_H1Title");}; return _H1Title;}} private Element _H1Title;
		public jQueryObject H1TitleJ {get {if (_H1TitleJ == null) {_H1TitleJ = jQuery.Select("#" + clientId + "_H1Title");}; return _H1TitleJ;}} private jQueryObject _H1TitleJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element SelectCurrentDateRangeLinkButton {get {if (_SelectCurrentDateRangeLinkButton == null) {_SelectCurrentDateRangeLinkButton = (Element)Document.GetElementById(clientId + "_SelectCurrentDateRangeLinkButton");}; return _SelectCurrentDateRangeLinkButton;}} private Element _SelectCurrentDateRangeLinkButton;
		public jQueryObject SelectCurrentDateRangeLinkButtonJ {get {if (_SelectCurrentDateRangeLinkButtonJ == null) {_SelectCurrentDateRangeLinkButtonJ = jQuery.Select("#" + clientId + "_SelectCurrentDateRangeLinkButton");}; return _SelectCurrentDateRangeLinkButtonJ;}} private jQueryObject _SelectCurrentDateRangeLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element SelectPastDateRangeLinkButton {get {if (_SelectPastDateRangeLinkButton == null) {_SelectPastDateRangeLinkButton = (Element)Document.GetElementById(clientId + "_SelectPastDateRangeLinkButton");}; return _SelectPastDateRangeLinkButton;}} private Element _SelectPastDateRangeLinkButton;
		public jQueryObject SelectPastDateRangeLinkButtonJ {get {if (_SelectPastDateRangeLinkButtonJ == null) {_SelectPastDateRangeLinkButtonJ = jQuery.Select("#" + clientId + "_SelectPastDateRangeLinkButton");}; return _SelectPastDateRangeLinkButtonJ;}} private jQueryObject _SelectPastDateRangeLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element SelectAllDateRangeLinkButton {get {if (_SelectAllDateRangeLinkButton == null) {_SelectAllDateRangeLinkButton = (Element)Document.GetElementById(clientId + "_SelectAllDateRangeLinkButton");}; return _SelectAllDateRangeLinkButton;}} private Element _SelectAllDateRangeLinkButton;
		public jQueryObject SelectAllDateRangeLinkButtonJ {get {if (_SelectAllDateRangeLinkButtonJ == null) {_SelectAllDateRangeLinkButtonJ = jQuery.Select("#" + clientId + "_SelectAllDateRangeLinkButton");}; return _SelectAllDateRangeLinkButtonJ;}} private jQueryObject _SelectAllDateRangeLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element TicketRunsGridView {get {if (_TicketRunsGridView == null) {_TicketRunsGridView = (Element)Document.GetElementById(clientId + "_TicketRunsGridView");}; return _TicketRunsGridView;}} private Element _TicketRunsGridView;
		public jQueryObject TicketRunsGridViewJ {get {if (_TicketRunsGridViewJ == null) {_TicketRunsGridViewJ = jQuery.Select("#" + clientId + "_TicketRunsGridView");}; return _TicketRunsGridViewJ;}} private jQueryObject _TicketRunsGridViewJ;//mappings.Add("System.Web.UI.WebControls.GridView", ElementGetter("Element"));
		public DivElement PaginationPanel {get {if (_PaginationPanel == null) {_PaginationPanel = (DivElement)Document.GetElementById(clientId + "_PaginationPanel");}; return _PaginationPanel;}} private DivElement _PaginationPanel;
		public jQueryObject PaginationPanelJ {get {if (_PaginationPanelJ == null) {_PaginationPanelJ = jQuery.Select("#" + clientId + "_PaginationPanel");}; return _PaginationPanelJ;}} private jQueryObject _PaginationPanelJ;
		public Element PrevPageLinkButton {get {if (_PrevPageLinkButton == null) {_PrevPageLinkButton = (Element)Document.GetElementById(clientId + "_PrevPageLinkButton");}; return _PrevPageLinkButton;}} private Element _PrevPageLinkButton;
		public jQueryObject PrevPageLinkButtonJ {get {if (_PrevPageLinkButtonJ == null) {_PrevPageLinkButtonJ = jQuery.Select("#" + clientId + "_PrevPageLinkButton");}; return _PrevPageLinkButtonJ;}} private jQueryObject _PrevPageLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element NextPageLinkButton {get {if (_NextPageLinkButton == null) {_NextPageLinkButton = (Element)Document.GetElementById(clientId + "_NextPageLinkButton");}; return _NextPageLinkButton;}} private Element _NextPageLinkButton;
		public jQueryObject NextPageLinkButtonJ {get {if (_NextPageLinkButtonJ == null) {_NextPageLinkButtonJ = jQuery.Select("#" + clientId + "_NextPageLinkButton");}; return _NextPageLinkButtonJ;}} private jQueryObject _NextPageLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public DivElement AdminLinksPanel {get {if (_AdminLinksPanel == null) {_AdminLinksPanel = (DivElement)Document.GetElementById(clientId + "_AdminLinksPanel");}; return _AdminLinksPanel;}} private DivElement _AdminLinksPanel;
		public jQueryObject AdminLinksPanelJ {get {if (_AdminLinksPanelJ == null) {_AdminLinksPanelJ = jQuery.Select("#" + clientId + "_AdminLinksPanel");}; return _AdminLinksPanelJ;}} private jQueryObject _AdminLinksPanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
