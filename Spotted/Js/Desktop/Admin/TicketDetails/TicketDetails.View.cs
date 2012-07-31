//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Admin.TicketDetails
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
		public DivElement TicketDetailsPanel {get {if (_TicketDetailsPanel == null) {_TicketDetailsPanel = (DivElement)Document.GetElementById(clientId + "_TicketDetailsPanel");}; return _TicketDetailsPanel;}} private DivElement _TicketDetailsPanel;
		public jQueryObject TicketDetailsPanelJ {get {if (_TicketDetailsPanelJ == null) {_TicketDetailsPanelJ = jQuery.Select("#" + clientId + "_TicketDetailsPanel");}; return _TicketDetailsPanelJ;}} private jQueryObject _TicketDetailsPanelJ;
		public Element CancelledRow {get {if (_CancelledRow == null) {_CancelledRow = (Element)Document.GetElementById(clientId + "_CancelledRow");}; return _CancelledRow;}} private Element _CancelledRow;
		public jQueryObject CancelledRowJ {get {if (_CancelledRowJ == null) {_CancelledRowJ = jQuery.Select("#" + clientId + "_CancelledRow");}; return _CancelledRowJ;}} private jQueryObject _CancelledRowJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public Element TicketKLabel {get {if (_TicketKLabel == null) {_TicketKLabel = (Element)Document.GetElementById(clientId + "_TicketKLabel");}; return _TicketKLabel;}} private Element _TicketKLabel;
		public jQueryObject TicketKLabelJ {get {if (_TicketKLabelJ == null) {_TicketKLabelJ = jQuery.Select("#" + clientId + "_TicketKLabel");}; return _TicketKLabelJ;}} private jQueryObject _TicketKLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element UserNickNameLabel {get {if (_UserNickNameLabel == null) {_UserNickNameLabel = (Element)Document.GetElementById(clientId + "_UserNickNameLabel");}; return _UserNickNameLabel;}} private Element _UserNickNameLabel;
		public jQueryObject UserNickNameLabelJ {get {if (_UserNickNameLabelJ == null) {_UserNickNameLabelJ = jQuery.Select("#" + clientId + "_UserNickNameLabel");}; return _UserNickNameLabelJ;}} private jQueryObject _UserNickNameLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FullNameLabel {get {if (_FullNameLabel == null) {_FullNameLabel = (Element)Document.GetElementById(clientId + "_FullNameLabel");}; return _FullNameLabel;}} private Element _FullNameLabel;
		public jQueryObject FullNameLabelJ {get {if (_FullNameLabelJ == null) {_FullNameLabelJ = jQuery.Select("#" + clientId + "_FullNameLabel");}; return _FullNameLabelJ;}} private jQueryObject _FullNameLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PurchaseDateLabel {get {if (_PurchaseDateLabel == null) {_PurchaseDateLabel = (Element)Document.GetElementById(clientId + "_PurchaseDateLabel");}; return _PurchaseDateLabel;}} private Element _PurchaseDateLabel;
		public jQueryObject PurchaseDateLabelJ {get {if (_PurchaseDateLabelJ == null) {_PurchaseDateLabelJ = jQuery.Select("#" + clientId + "_PurchaseDateLabel");}; return _PurchaseDateLabelJ;}} private jQueryObject _PurchaseDateLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element EventLabel {get {if (_EventLabel == null) {_EventLabel = (Element)Document.GetElementById(clientId + "_EventLabel");}; return _EventLabel;}} private Element _EventLabel;
		public jQueryObject EventLabelJ {get {if (_EventLabelJ == null) {_EventLabelJ = jQuery.Select("#" + clientId + "_EventLabel");}; return _EventLabelJ;}} private jQueryObject _EventLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element TicketRunLabel {get {if (_TicketRunLabel == null) {_TicketRunLabel = (Element)Document.GetElementById(clientId + "_TicketRunLabel");}; return _TicketRunLabel;}} private Element _TicketRunLabel;
		public jQueryObject TicketRunLabelJ {get {if (_TicketRunLabelJ == null) {_TicketRunLabelJ = jQuery.Select("#" + clientId + "_TicketRunLabel");}; return _TicketRunLabelJ;}} private jQueryObject _TicketRunLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element QuantityLabel {get {if (_QuantityLabel == null) {_QuantityLabel = (Element)Document.GetElementById(clientId + "_QuantityLabel");}; return _QuantityLabel;}} private Element _QuantityLabel;
		public jQueryObject QuantityLabelJ {get {if (_QuantityLabelJ == null) {_QuantityLabelJ = jQuery.Select("#" + clientId + "_QuantityLabel");}; return _QuantityLabelJ;}} private jQueryObject _QuantityLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PriceLabel {get {if (_PriceLabel == null) {_PriceLabel = (Element)Document.GetElementById(clientId + "_PriceLabel");}; return _PriceLabel;}} private Element _PriceLabel;
		public jQueryObject PriceLabelJ {get {if (_PriceLabelJ == null) {_PriceLabelJ = jQuery.Select("#" + clientId + "_PriceLabel");}; return _PriceLabelJ;}} private jQueryObject _PriceLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element BookingFeeLabel {get {if (_BookingFeeLabel == null) {_BookingFeeLabel = (Element)Document.GetElementById(clientId + "_BookingFeeLabel");}; return _BookingFeeLabel;}} private Element _BookingFeeLabel;
		public jQueryObject BookingFeeLabelJ {get {if (_BookingFeeLabelJ == null) {_BookingFeeLabelJ = jQuery.Select("#" + clientId + "_BookingFeeLabel");}; return _BookingFeeLabelJ;}} private jQueryObject _BookingFeeLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element InvoiceLabel {get {if (_InvoiceLabel == null) {_InvoiceLabel = (Element)Document.GetElementById(clientId + "_InvoiceLabel");}; return _InvoiceLabel;}} private Element _InvoiceLabel;
		public jQueryObject InvoiceLabelJ {get {if (_InvoiceLabelJ == null) {_InvoiceLabelJ = jQuery.Select("#" + clientId + "_InvoiceLabel");}; return _InvoiceLabelJ;}} private jQueryObject _InvoiceLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element CardNumberEndLabel {get {if (_CardNumberEndLabel == null) {_CardNumberEndLabel = (Element)Document.GetElementById(clientId + "_CardNumberEndLabel");}; return _CardNumberEndLabel;}} private Element _CardNumberEndLabel;
		public jQueryObject CardNumberEndLabelJ {get {if (_CardNumberEndLabelJ == null) {_CardNumberEndLabelJ = jQuery.Select("#" + clientId + "_CardNumberEndLabel");}; return _CardNumberEndLabelJ;}} private jQueryObject _CardNumberEndLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element CodeLabel {get {if (_CodeLabel == null) {_CodeLabel = (Element)Document.GetElementById(clientId + "_CodeLabel");}; return _CodeLabel;}} private Element _CodeLabel;
		public jQueryObject CodeLabelJ {get {if (_CodeLabelJ == null) {_CodeLabelJ = jQuery.Select("#" + clientId + "_CodeLabel");}; return _CodeLabelJ;}} private jQueryObject _CodeLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element AddressLabel {get {if (_AddressLabel == null) {_AddressLabel = (Element)Document.GetElementById(clientId + "_AddressLabel");}; return _AddressLabel;}} private Element _AddressLabel;
		public jQueryObject AddressLabelJ {get {if (_AddressLabelJ == null) {_AddressLabelJ = jQuery.Select("#" + clientId + "_AddressLabel");}; return _AddressLabelJ;}} private jQueryObject _AddressLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FeedbackLabel {get {if (_FeedbackLabel == null) {_FeedbackLabel = (Element)Document.GetElementById(clientId + "_FeedbackLabel");}; return _FeedbackLabel;}} private Element _FeedbackLabel;
		public jQueryObject FeedbackLabelJ {get {if (_FeedbackLabelJ == null) {_FeedbackLabelJ = jQuery.Select("#" + clientId + "_FeedbackLabel");}; return _FeedbackLabelJ;}} private jQueryObject _FeedbackLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element FeedbackNoteRow {get {if (_FeedbackNoteRow == null) {_FeedbackNoteRow = (Element)Document.GetElementById(clientId + "_FeedbackNoteRow");}; return _FeedbackNoteRow;}} private Element _FeedbackNoteRow;
		public jQueryObject FeedbackNoteRowJ {get {if (_FeedbackNoteRowJ == null) {_FeedbackNoteRowJ = jQuery.Select("#" + clientId + "_FeedbackNoteRow");}; return _FeedbackNoteRowJ;}} private jQueryObject _FeedbackNoteRowJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public Element FeedbackNoteLabel {get {if (_FeedbackNoteLabel == null) {_FeedbackNoteLabel = (Element)Document.GetElementById(clientId + "_FeedbackNoteLabel");}; return _FeedbackNoteLabel;}} private Element _FeedbackNoteLabel;
		public jQueryObject FeedbackNoteLabelJ {get {if (_FeedbackNoteLabelJ == null) {_FeedbackNoteLabelJ = jQuery.Select("#" + clientId + "_FeedbackNoteLabel");}; return _FeedbackNoteLabelJ;}} private jQueryObject _FeedbackNoteLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element RefundButton {get {if (_RefundButton == null) {_RefundButton = (Element)Document.GetElementById(clientId + "_RefundButton");}; return _RefundButton;}} private Element _RefundButton;
		public jQueryObject RefundButtonJ {get {if (_RefundButtonJ == null) {_RefundButtonJ = jQuery.Select("#" + clientId + "_RefundButton");}; return _RefundButtonJ;}} private jQueryObject _RefundButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element RefundFullButton {get {if (_RefundFullButton == null) {_RefundFullButton = (Element)Document.GetElementById(clientId + "_RefundFullButton");}; return _RefundFullButton;}} private Element _RefundFullButton;
		public jQueryObject RefundFullButtonJ {get {if (_RefundFullButtonJ == null) {_RefundFullButtonJ = jQuery.Select("#" + clientId + "_RefundFullButton");}; return _RefundFullButtonJ;}} private jQueryObject _RefundFullButtonJ;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public CheckBoxElement ChargePromoterForRefundCheckBox {get {if (_ChargePromoterForRefundCheckBox == null) {_ChargePromoterForRefundCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_ChargePromoterForRefundCheckBox");}; return _ChargePromoterForRefundCheckBox;}} private CheckBoxElement _ChargePromoterForRefundCheckBox;
		public jQueryObject ChargePromoterForRefundCheckBoxJ {get {if (_ChargePromoterForRefundCheckBoxJ == null) {_ChargePromoterForRefundCheckBoxJ = jQuery.Select("#" + clientId + "_ChargePromoterForRefundCheckBox");}; return _ChargePromoterForRefundCheckBoxJ;}} private jQueryObject _ChargePromoterForRefundCheckBoxJ;
		public InputElement ChargePromoterAmountTextBox {get {if (_ChargePromoterAmountTextBox == null) {_ChargePromoterAmountTextBox = (InputElement)Document.GetElementById(clientId + "_ChargePromoterAmountTextBox");}; return _ChargePromoterAmountTextBox;}} private InputElement _ChargePromoterAmountTextBox;
		public jQueryObject ChargePromoterAmountTextBoxJ {get {if (_ChargePromoterAmountTextBoxJ == null) {_ChargePromoterAmountTextBoxJ = jQuery.Select("#" + clientId + "_ChargePromoterAmountTextBox");}; return _ChargePromoterAmountTextBoxJ;}} private jQueryObject _ChargePromoterAmountTextBoxJ;
		public Element ProcessingVal {get {if (_ProcessingVal == null) {_ProcessingVal = (Element)Document.GetElementById(clientId + "_ProcessingVal");}; return _ProcessingVal;}} private Element _ProcessingVal;
		public jQueryObject ProcessingValJ {get {if (_ProcessingValJ == null) {_ProcessingValJ = jQuery.Select("#" + clientId + "_ProcessingVal");}; return _ProcessingValJ;}} private jQueryObject _ProcessingValJ;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element TicketValidationSummary {get {if (_TicketValidationSummary == null) {_TicketValidationSummary = (Element)Document.GetElementById(clientId + "_TicketValidationSummary");}; return _TicketValidationSummary;}} private Element _TicketValidationSummary;
		public jQueryObject TicketValidationSummaryJ {get {if (_TicketValidationSummaryJ == null) {_TicketValidationSummaryJ = jQuery.Select("#" + clientId + "_TicketValidationSummary");}; return _TicketValidationSummaryJ;}} private jQueryObject _TicketValidationSummaryJ;//mappings.Add("System.Web.UI.WebControls.ValidationSummary", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
