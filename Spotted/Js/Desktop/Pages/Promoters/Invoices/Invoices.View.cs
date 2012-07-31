//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.SetupPayment", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.Invoices
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
		public Element Payment {get {if (_Payment == null) {_Payment = (Element)Document.GetElementById(clientId + "_Payment");}; return _Payment;}} private Element _Payment;
		public jQueryObject PaymentJ {get {if (_PaymentJ == null) {_PaymentJ = jQuery.Select("#" + clientId + "_Payment");}; return _PaymentJ;}} private jQueryObject _PaymentJ;//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
		public Element PromoterIntro {get {if (_PromoterIntro == null) {_PromoterIntro = (Element)Document.GetElementById(clientId + "_PromoterIntro");}; return _PromoterIntro;}} private Element _PromoterIntro;
		public jQueryObject PromoterIntroJ {get {if (_PromoterIntroJ == null) {_PromoterIntroJ = jQuery.Select("#" + clientId + "_PromoterIntro");}; return _PromoterIntroJ;}} private jQueryObject _PromoterIntroJ;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element PromoterIntro1 {get {if (_PromoterIntro1 == null) {_PromoterIntro1 = (Element)Document.GetElementById(clientId + "_PromoterIntro1");}; return _PromoterIntro1;}} private Element _PromoterIntro1;
		public jQueryObject PromoterIntro1J {get {if (_PromoterIntro1J == null) {_PromoterIntro1J = jQuery.Select("#" + clientId + "_PromoterIntro1");}; return _PromoterIntro1J;}} private jQueryObject _PromoterIntro1J;//mappings.Add("Spotted.CustomControls.PromoterIntro", ElementGetter("Element"));
		public DivElement MainPanel {get {if (_MainPanel == null) {_MainPanel = (DivElement)Document.GetElementById(clientId + "_MainPanel");}; return _MainPanel;}} private DivElement _MainPanel;
		public jQueryObject MainPanelJ {get {if (_MainPanelJ == null) {_MainPanelJ = jQuery.Select("#" + clientId + "_MainPanel");}; return _MainPanelJ;}} private jQueryObject _MainPanelJ;
		public Element H1Header {get {if (_H1Header == null) {_H1Header = (Element)Document.GetElementById(clientId + "_H1Header");}; return _H1Header;}} private Element _H1Header;
		public jQueryObject H1HeaderJ {get {if (_H1HeaderJ == null) {_H1HeaderJ = jQuery.Select("#" + clientId + "_H1Header");}; return _H1HeaderJ;}} private jQueryObject _H1HeaderJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element BalanceLabel {get {if (_BalanceLabel == null) {_BalanceLabel = (Element)Document.GetElementById(clientId + "_BalanceLabel");}; return _BalanceLabel;}} private Element _BalanceLabel;
		public jQueryObject BalanceLabelJ {get {if (_BalanceLabelJ == null) {_BalanceLabelJ = jQuery.Select("#" + clientId + "_BalanceLabel");}; return _BalanceLabelJ;}} private jQueryObject _BalanceLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element BalanceValueLabel {get {if (_BalanceValueLabel == null) {_BalanceValueLabel = (Element)Document.GetElementById(clientId + "_BalanceValueLabel");}; return _BalanceValueLabel;}} private Element _BalanceValueLabel;
		public jQueryObject BalanceValueLabelJ {get {if (_BalanceValueLabelJ == null) {_BalanceValueLabelJ = jQuery.Select("#" + clientId + "_BalanceValueLabel");}; return _BalanceValueLabelJ;}} private jQueryObject _BalanceValueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element OutstandingBalanceLabel {get {if (_OutstandingBalanceLabel == null) {_OutstandingBalanceLabel = (Element)Document.GetElementById(clientId + "_OutstandingBalanceLabel");}; return _OutstandingBalanceLabel;}} private Element _OutstandingBalanceLabel;
		public jQueryObject OutstandingBalanceLabelJ {get {if (_OutstandingBalanceLabelJ == null) {_OutstandingBalanceLabelJ = jQuery.Select("#" + clientId + "_OutstandingBalanceLabel");}; return _OutstandingBalanceLabelJ;}} private jQueryObject _OutstandingBalanceLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element CreditLimitTR {get {if (_CreditLimitTR == null) {_CreditLimitTR = (Element)Document.GetElementById(clientId + "_CreditLimitTR");}; return _CreditLimitTR;}} private Element _CreditLimitTR;
		public jQueryObject CreditLimitTRJ {get {if (_CreditLimitTRJ == null) {_CreditLimitTRJ = jQuery.Select("#" + clientId + "_CreditLimitTR");}; return _CreditLimitTRJ;}} private jQueryObject _CreditLimitTRJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public Element CreditLimitLabel {get {if (_CreditLimitLabel == null) {_CreditLimitLabel = (Element)Document.GetElementById(clientId + "_CreditLimitLabel");}; return _CreditLimitLabel;}} private Element _CreditLimitLabel;
		public jQueryObject CreditLimitLabelJ {get {if (_CreditLimitLabelJ == null) {_CreditLimitLabelJ = jQuery.Select("#" + clientId + "_CreditLimitLabel");}; return _CreditLimitLabelJ;}} private jQueryObject _CreditLimitLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element CreditLimitValueLabel {get {if (_CreditLimitValueLabel == null) {_CreditLimitValueLabel = (Element)Document.GetElementById(clientId + "_CreditLimitValueLabel");}; return _CreditLimitValueLabel;}} private Element _CreditLimitValueLabel;
		public jQueryObject CreditLimitValueLabelJ {get {if (_CreditLimitValueLabelJ == null) {_CreditLimitValueLabelJ = jQuery.Select("#" + clientId + "_CreditLimitValueLabel");}; return _CreditLimitValueLabelJ;}} private jQueryObject _CreditLimitValueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FundsAvailableTR {get {if (_FundsAvailableTR == null) {_FundsAvailableTR = (Element)Document.GetElementById(clientId + "_FundsAvailableTR");}; return _FundsAvailableTR;}} private Element _FundsAvailableTR;
		public jQueryObject FundsAvailableTRJ {get {if (_FundsAvailableTRJ == null) {_FundsAvailableTRJ = jQuery.Select("#" + clientId + "_FundsAvailableTR");}; return _FundsAvailableTRJ;}} private jQueryObject _FundsAvailableTRJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public Element FundsAvailableLabel {get {if (_FundsAvailableLabel == null) {_FundsAvailableLabel = (Element)Document.GetElementById(clientId + "_FundsAvailableLabel");}; return _FundsAvailableLabel;}} private Element _FundsAvailableLabel;
		public jQueryObject FundsAvailableLabelJ {get {if (_FundsAvailableLabelJ == null) {_FundsAvailableLabelJ = jQuery.Select("#" + clientId + "_FundsAvailableLabel");}; return _FundsAvailableLabelJ;}} private jQueryObject _FundsAvailableLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FundsAvailableValueLabel {get {if (_FundsAvailableValueLabel == null) {_FundsAvailableValueLabel = (Element)Document.GetElementById(clientId + "_FundsAvailableValueLabel");}; return _FundsAvailableValueLabel;}} private Element _FundsAvailableValueLabel;
		public jQueryObject FundsAvailableValueLabelJ {get {if (_FundsAvailableValueLabelJ == null) {_FundsAvailableValueLabelJ = jQuery.Select("#" + clientId + "_FundsAvailableValueLabel");}; return _FundsAvailableValueLabelJ;}} private jQueryObject _FundsAvailableValueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element TicketFundsTR {get {if (_TicketFundsTR == null) {_TicketFundsTR = (Element)Document.GetElementById(clientId + "_TicketFundsTR");}; return _TicketFundsTR;}} private Element _TicketFundsTR;
		public jQueryObject TicketFundsTRJ {get {if (_TicketFundsTRJ == null) {_TicketFundsTRJ = jQuery.Select("#" + clientId + "_TicketFundsTR");}; return _TicketFundsTRJ;}} private jQueryObject _TicketFundsTRJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public Element TicketFundsValueLabel {get {if (_TicketFundsValueLabel == null) {_TicketFundsValueLabel = (Element)Document.GetElementById(clientId + "_TicketFundsValueLabel");}; return _TicketFundsValueLabel;}} private Element _TicketFundsValueLabel;
		public jQueryObject TicketFundsValueLabelJ {get {if (_TicketFundsValueLabelJ == null) {_TicketFundsValueLabelJ = jQuery.Select("#" + clientId + "_TicketFundsValueLabel");}; return _TicketFundsValueLabelJ;}} private jQueryObject _TicketFundsValueLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement HeaderPanel {get {if (_HeaderPanel == null) {_HeaderPanel = (DivElement)Document.GetElementById(clientId + "_HeaderPanel");}; return _HeaderPanel;}} private DivElement _HeaderPanel;
		public jQueryObject HeaderPanelJ {get {if (_HeaderPanelJ == null) {_HeaderPanelJ = jQuery.Select("#" + clientId + "_HeaderPanel");}; return _HeaderPanelJ;}} private jQueryObject _HeaderPanelJ;
		public Element ViewSummaryLabel {get {if (_ViewSummaryLabel == null) {_ViewSummaryLabel = (Element)Document.GetElementById(clientId + "_ViewSummaryLabel");}; return _ViewSummaryLabel;}} private Element _ViewSummaryLabel;
		public jQueryObject ViewSummaryLabelJ {get {if (_ViewSummaryLabelJ == null) {_ViewSummaryLabelJ = jQuery.Select("#" + clientId + "_ViewSummaryLabel");}; return _ViewSummaryLabelJ;}} private jQueryObject _ViewSummaryLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public SelectElement MonthDropDownList {get {if (_MonthDropDownList == null) {_MonthDropDownList = (SelectElement)Document.GetElementById(clientId + "_MonthDropDownList");}; return _MonthDropDownList;}} private SelectElement _MonthDropDownList;
		public jQueryObject MonthDropDownListJ {get {if (_MonthDropDownListJ == null) {_MonthDropDownListJ = jQuery.Select("#" + clientId + "_MonthDropDownList");}; return _MonthDropDownListJ;}} private jQueryObject _MonthDropDownListJ;
		public SelectElement YearDropDownList {get {if (_YearDropDownList == null) {_YearDropDownList = (SelectElement)Document.GetElementById(clientId + "_YearDropDownList");}; return _YearDropDownList;}} private SelectElement _YearDropDownList;
		public jQueryObject YearDropDownListJ {get {if (_YearDropDownListJ == null) {_YearDropDownListJ = jQuery.Select("#" + clientId + "_YearDropDownList");}; return _YearDropDownListJ;}} private jQueryObject _YearDropDownListJ;
		public Element ViewSummaryButton {get {if (_ViewSummaryButton == null) {_ViewSummaryButton = (Element)Document.GetElementById(clientId + "_ViewSummaryButton");}; return _ViewSummaryButton;}} private Element _ViewSummaryButton;
		public jQueryObject ViewSummaryButtonJ {get {if (_ViewSummaryButtonJ == null) {_ViewSummaryButtonJ = jQuery.Select("#" + clientId + "_ViewSummaryButton");}; return _ViewSummaryButtonJ;}} private jQueryObject _ViewSummaryButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public AnchorElement ViewStatementHyperLink {get {if (_ViewStatementHyperLink == null) {_ViewStatementHyperLink = (AnchorElement)Document.GetElementById(clientId + "_ViewStatementHyperLink");}; return _ViewStatementHyperLink;}} private AnchorElement _ViewStatementHyperLink;
		public jQueryObject ViewStatementHyperLinkJ {get {if (_ViewStatementHyperLinkJ == null) {_ViewStatementHyperLinkJ = jQuery.Select("#" + clientId + "_ViewStatementHyperLink");}; return _ViewStatementHyperLinkJ;}} private jQueryObject _ViewStatementHyperLinkJ;
		public Element ViewSummaryCustomValidator {get {if (_ViewSummaryCustomValidator == null) {_ViewSummaryCustomValidator = (Element)Document.GetElementById(clientId + "_ViewSummaryCustomValidator");}; return _ViewSummaryCustomValidator;}} private Element _ViewSummaryCustomValidator;
		public jQueryObject ViewSummaryCustomValidatorJ {get {if (_ViewSummaryCustomValidatorJ == null) {_ViewSummaryCustomValidatorJ = jQuery.Select("#" + clientId + "_ViewSummaryCustomValidator");}; return _ViewSummaryCustomValidatorJ;}} private jQueryObject _ViewSummaryCustomValidatorJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public DivElement SummaryPanel {get {if (_SummaryPanel == null) {_SummaryPanel = (DivElement)Document.GetElementById(clientId + "_SummaryPanel");}; return _SummaryPanel;}} private DivElement _SummaryPanel;
		public jQueryObject SummaryPanelJ {get {if (_SummaryPanelJ == null) {_SummaryPanelJ = jQuery.Select("#" + clientId + "_SummaryPanel");}; return _SummaryPanelJ;}} private jQueryObject _SummaryPanelJ;
		public Element H1Summmary {get {if (_H1Summmary == null) {_H1Summmary = (Element)Document.GetElementById(clientId + "_H1Summmary");}; return _H1Summmary;}} private Element _H1Summmary;
		public jQueryObject H1SummmaryJ {get {if (_H1SummmaryJ == null) {_H1SummmaryJ = jQuery.Select("#" + clientId + "_H1Summmary");}; return _H1SummmaryJ;}} private jQueryObject _H1SummmaryJ;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element SummaryHeaderLabel {get {if (_SummaryHeaderLabel == null) {_SummaryHeaderLabel = (Element)Document.GetElementById(clientId + "_SummaryHeaderLabel");}; return _SummaryHeaderLabel;}} private Element _SummaryHeaderLabel;
		public jQueryObject SummaryHeaderLabelJ {get {if (_SummaryHeaderLabelJ == null) {_SummaryHeaderLabelJ = jQuery.Select("#" + clientId + "_SummaryHeaderLabel");}; return _SummaryHeaderLabelJ;}} private jQueryObject _SummaryHeaderLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PayOutstandingButton {get {if (_PayOutstandingButton == null) {_PayOutstandingButton = (Element)Document.GetElementById(clientId + "_PayOutstandingButton");}; return _PayOutstandingButton;}} private Element _PayOutstandingButton;
		public jQueryObject PayOutstandingButtonJ {get {if (_PayOutstandingButtonJ == null) {_PayOutstandingButtonJ = jQuery.Select("#" + clientId + "_PayOutstandingButton");}; return _PayOutstandingButtonJ;}} private jQueryObject _PayOutstandingButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element PayNowButton {get {if (_PayNowButton == null) {_PayNowButton = (Element)Document.GetElementById(clientId + "_PayNowButton");}; return _PayNowButton;}} private Element _PayNowButton;
		public jQueryObject PayNowButtonJ {get {if (_PayNowButtonJ == null) {_PayNowButtonJ = jQuery.Select("#" + clientId + "_PayNowButton");}; return _PayNowButtonJ;}} private jQueryObject _PayNowButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element SetupTransferButton {get {if (_SetupTransferButton == null) {_SetupTransferButton = (Element)Document.GetElementById(clientId + "_SetupTransferButton");}; return _SetupTransferButton;}} private Element _SetupTransferButton;
		public jQueryObject SetupTransferButtonJ {get {if (_SetupTransferButtonJ == null) {_SetupTransferButtonJ = jQuery.Select("#" + clientId + "_SetupTransferButton");}; return _SetupTransferButtonJ;}} private jQueryObject _SetupTransferButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element FilterLabel {get {if (_FilterLabel == null) {_FilterLabel = (Element)Document.GetElementById(clientId + "_FilterLabel");}; return _FilterLabel;}} private Element _FilterLabel;
		public jQueryObject FilterLabelJ {get {if (_FilterLabelJ == null) {_FilterLabelJ = jQuery.Select("#" + clientId + "_FilterLabel");}; return _FilterLabelJ;}} private jQueryObject _FilterLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public SelectElement FilterDropDownList {get {if (_FilterDropDownList == null) {_FilterDropDownList = (SelectElement)Document.GetElementById(clientId + "_FilterDropDownList");}; return _FilterDropDownList;}} private SelectElement _FilterDropDownList;
		public jQueryObject FilterDropDownListJ {get {if (_FilterDropDownListJ == null) {_FilterDropDownListJ = jQuery.Select("#" + clientId + "_FilterDropDownList");}; return _FilterDropDownListJ;}} private jQueryObject _FilterDropDownListJ;
		public Element PromoterAccountItemRepeater {get {if (_PromoterAccountItemRepeater == null) {_PromoterAccountItemRepeater = (Element)Document.GetElementById(clientId + "_PromoterAccountItemRepeater");}; return _PromoterAccountItemRepeater;}} private Element _PromoterAccountItemRepeater;
		public jQueryObject PromoterAccountItemRepeaterJ {get {if (_PromoterAccountItemRepeaterJ == null) {_PromoterAccountItemRepeaterJ = jQuery.Select("#" + clientId + "_PromoterAccountItemRepeater");}; return _PromoterAccountItemRepeaterJ;}} private jQueryObject _PromoterAccountItemRepeaterJ;//mappings.Add("System.Web.UI.WebControls.Repeater", ElementGetter("Element"));
		public DivElement PaginationPanel {get {if (_PaginationPanel == null) {_PaginationPanel = (DivElement)Document.GetElementById(clientId + "_PaginationPanel");}; return _PaginationPanel;}} private DivElement _PaginationPanel;
		public jQueryObject PaginationPanelJ {get {if (_PaginationPanelJ == null) {_PaginationPanelJ = jQuery.Select("#" + clientId + "_PaginationPanel");}; return _PaginationPanelJ;}} private jQueryObject _PaginationPanelJ;
		public Element PrevPageLinkButton {get {if (_PrevPageLinkButton == null) {_PrevPageLinkButton = (Element)Document.GetElementById(clientId + "_PrevPageLinkButton");}; return _PrevPageLinkButton;}} private Element _PrevPageLinkButton;
		public jQueryObject PrevPageLinkButtonJ {get {if (_PrevPageLinkButtonJ == null) {_PrevPageLinkButtonJ = jQuery.Select("#" + clientId + "_PrevPageLinkButton");}; return _PrevPageLinkButtonJ;}} private jQueryObject _PrevPageLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public Element NextPageLinkButton {get {if (_NextPageLinkButton == null) {_NextPageLinkButton = (Element)Document.GetElementById(clientId + "_NextPageLinkButton");}; return _NextPageLinkButton;}} private Element _NextPageLinkButton;
		public jQueryObject NextPageLinkButtonJ {get {if (_NextPageLinkButtonJ == null) {_NextPageLinkButtonJ = jQuery.Select("#" + clientId + "_NextPageLinkButton");}; return _NextPageLinkButtonJ;}} private jQueryObject _NextPageLinkButtonJ;//mappings.Add("System.Web.UI.WebControls.LinkButton", ElementGetter("Element"));
		public DivElement AdminPanel {get {if (_AdminPanel == null) {_AdminPanel = (DivElement)Document.GetElementById(clientId + "_AdminPanel");}; return _AdminPanel;}} private DivElement _AdminPanel;
		public jQueryObject AdminPanelJ {get {if (_AdminPanelJ == null) {_AdminPanelJ = jQuery.Select("#" + clientId + "_AdminPanel");}; return _AdminPanelJ;}} private jQueryObject _AdminPanelJ;
		public Element AdminInvoiceLinkLabel {get {if (_AdminInvoiceLinkLabel == null) {_AdminInvoiceLinkLabel = (Element)Document.GetElementById(clientId + "_AdminInvoiceLinkLabel");}; return _AdminInvoiceLinkLabel;}} private Element _AdminInvoiceLinkLabel;
		public jQueryObject AdminInvoiceLinkLabelJ {get {if (_AdminInvoiceLinkLabelJ == null) {_AdminInvoiceLinkLabelJ = jQuery.Select("#" + clientId + "_AdminInvoiceLinkLabel");}; return _AdminInvoiceLinkLabelJ;}} private jQueryObject _AdminInvoiceLinkLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element AdminTransferLinkLabel {get {if (_AdminTransferLinkLabel == null) {_AdminTransferLinkLabel = (Element)Document.GetElementById(clientId + "_AdminTransferLinkLabel");}; return _AdminTransferLinkLabel;}} private Element _AdminTransferLinkLabel;
		public jQueryObject AdminTransferLinkLabelJ {get {if (_AdminTransferLinkLabelJ == null) {_AdminTransferLinkLabelJ = jQuery.Select("#" + clientId + "_AdminTransferLinkLabel");}; return _AdminTransferLinkLabelJ;}} private jQueryObject _AdminTransferLinkLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement PaymentPanel {get {if (_PaymentPanel == null) {_PaymentPanel = (DivElement)Document.GetElementById(clientId + "_PaymentPanel");}; return _PaymentPanel;}} private DivElement _PaymentPanel;
		public jQueryObject PaymentPanelJ {get {if (_PaymentPanelJ == null) {_PaymentPanelJ = jQuery.Select("#" + clientId + "_PaymentPanel");}; return _PaymentPanelJ;}} private jQueryObject _PaymentPanelJ;
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement PaidMessagePanel {get {if (_PaidMessagePanel == null) {_PaidMessagePanel = (DivElement)Document.GetElementById(clientId + "_PaidMessagePanel");}; return _PaidMessagePanel;}} private DivElement _PaidMessagePanel;
		public jQueryObject PaidMessagePanelJ {get {if (_PaidMessagePanelJ == null) {_PaidMessagePanelJ = jQuery.Select("#" + clientId + "_PaidMessagePanel");}; return _PaidMessagePanelJ;}} private jQueryObject _PaidMessagePanelJ;
		public Element H14 {get {if (_H14 == null) {_H14 = (Element)Document.GetElementById(clientId + "_H14");}; return _H14;}} private Element _H14;
		public jQueryObject H14J {get {if (_H14J == null) {_H14J = jQuery.Select("#" + clientId + "_H14");}; return _H14J;}} private jQueryObject _H14J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement SetupTransferPanel {get {if (_SetupTransferPanel == null) {_SetupTransferPanel = (DivElement)Document.GetElementById(clientId + "_SetupTransferPanel");}; return _SetupTransferPanel;}} private DivElement _SetupTransferPanel;
		public jQueryObject SetupTransferPanelJ {get {if (_SetupTransferPanelJ == null) {_SetupTransferPanelJ = jQuery.Select("#" + clientId + "_SetupTransferPanel");}; return _SetupTransferPanelJ;}} private jQueryObject _SetupTransferPanelJ;
		public Element H15 {get {if (_H15 == null) {_H15 = (Element)Document.GetElementById(clientId + "_H15");}; return _H15;}} private Element _H15;
		public jQueryObject H15J {get {if (_H15J == null) {_H15J = jQuery.Select("#" + clientId + "_H15");}; return _H15J;}} private jQueryObject _H15J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element SetupPayment {get {if (_SetupPayment == null) {_SetupPayment = (Element)Document.GetElementById(clientId + "_SetupPayment");}; return _SetupPayment;}} private Element _SetupPayment;
		public jQueryObject SetupPaymentJ {get {if (_SetupPaymentJ == null) {_SetupPaymentJ = jQuery.Select("#" + clientId + "_SetupPayment");}; return _SetupPaymentJ;}} private jQueryObject _SetupPaymentJ;//mappings.Add("Spotted.Controls.SetupPayment", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
