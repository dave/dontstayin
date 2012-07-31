//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Camp
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
		public DivElement QuantityPanel {get {if (_QuantityPanel == null) {_QuantityPanel = (DivElement)Document.GetElementById(clientId + "_QuantityPanel");}; return _QuantityPanel;}} private DivElement _QuantityPanel;
		public jQueryObject QuantityPanelJ {get {if (_QuantityPanelJ == null) {_QuantityPanelJ = jQuery.Select("#" + clientId + "_QuantityPanel");}; return _QuantityPanelJ;}} private jQueryObject _QuantityPanelJ;
		public Element NameLabel {get {if (_NameLabel == null) {_NameLabel = (Element)Document.GetElementById(clientId + "_NameLabel");}; return _NameLabel;}} private Element _NameLabel;
		public jQueryObject NameLabelJ {get {if (_NameLabelJ == null) {_NameLabelJ = jQuery.Select("#" + clientId + "_NameLabel");}; return _NameLabelJ;}} private jQueryObject _NameLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element AlreadyHaveTicketsP {get {if (_AlreadyHaveTicketsP == null) {_AlreadyHaveTicketsP = (Element)Document.GetElementById(clientId + "_AlreadyHaveTicketsP");}; return _AlreadyHaveTicketsP;}} private Element _AlreadyHaveTicketsP;
		public jQueryObject AlreadyHaveTicketsPJ {get {if (_AlreadyHaveTicketsPJ == null) {_AlreadyHaveTicketsPJ = jQuery.Select("#" + clientId + "_AlreadyHaveTicketsP");}; return _AlreadyHaveTicketsPJ;}} private jQueryObject _AlreadyHaveTicketsPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element AlreadyHaveTicketsLabel {get {if (_AlreadyHaveTicketsLabel == null) {_AlreadyHaveTicketsLabel = (Element)Document.GetElementById(clientId + "_AlreadyHaveTicketsLabel");}; return _AlreadyHaveTicketsLabel;}} private Element _AlreadyHaveTicketsLabel;
		public jQueryObject AlreadyHaveTicketsLabelJ {get {if (_AlreadyHaveTicketsLabelJ == null) {_AlreadyHaveTicketsLabelJ = jQuery.Select("#" + clientId + "_AlreadyHaveTicketsLabel");}; return _AlreadyHaveTicketsLabelJ;}} private jQueryObject _AlreadyHaveTicketsLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public InputElement TicketsQuantityTextBox {get {if (_TicketsQuantityTextBox == null) {_TicketsQuantityTextBox = (InputElement)Document.GetElementById(clientId + "_TicketsQuantityTextBox");}; return _TicketsQuantityTextBox;}} private InputElement _TicketsQuantityTextBox;
		public jQueryObject TicketsQuantityTextBoxJ {get {if (_TicketsQuantityTextBoxJ == null) {_TicketsQuantityTextBoxJ = jQuery.Select("#" + clientId + "_TicketsQuantityTextBox");}; return _TicketsQuantityTextBoxJ;}} private jQueryObject _TicketsQuantityTextBoxJ;
		public Element Button1 {get {if (_Button1 == null) {_Button1 = (Element)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private Element _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement QuantityErrorPanel {get {if (_QuantityErrorPanel == null) {_QuantityErrorPanel = (DivElement)Document.GetElementById(clientId + "_QuantityErrorPanel");}; return _QuantityErrorPanel;}} private DivElement _QuantityErrorPanel;
		public jQueryObject QuantityErrorPanelJ {get {if (_QuantityErrorPanelJ == null) {_QuantityErrorPanelJ = jQuery.Select("#" + clientId + "_QuantityErrorPanel");}; return _QuantityErrorPanelJ;}} private jQueryObject _QuantityErrorPanelJ;
		public Element QuantityErrorP {get {if (_QuantityErrorP == null) {_QuantityErrorP = (Element)Document.GetElementById(clientId + "_QuantityErrorP");}; return _QuantityErrorP;}} private Element _QuantityErrorP;
		public jQueryObject QuantityErrorPJ {get {if (_QuantityErrorPJ == null) {_QuantityErrorPJ = jQuery.Select("#" + clientId + "_QuantityErrorP");}; return _QuantityErrorPJ;}} private jQueryObject _QuantityErrorPJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public DivElement PayPanel {get {if (_PayPanel == null) {_PayPanel = (DivElement)Document.GetElementById(clientId + "_PayPanel");}; return _PayPanel;}} private DivElement _PayPanel;
		public jQueryObject PayPanelJ {get {if (_PayPanelJ == null) {_PayPanelJ = jQuery.Select("#" + clientId + "_PayPanel");}; return _PayPanelJ;}} private jQueryObject _PayPanelJ;
		public Element TicketPayment {get {if (_TicketPayment == null) {_TicketPayment = (Element)Document.GetElementById(clientId + "_TicketPayment");}; return _TicketPayment;}} private Element _TicketPayment;
		public jQueryObject TicketPaymentJ {get {if (_TicketPaymentJ == null) {_TicketPaymentJ = jQuery.Select("#" + clientId + "_TicketPayment");}; return _TicketPaymentJ;}} private jQueryObject _TicketPaymentJ;//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
		public DivElement DonePanel {get {if (_DonePanel == null) {_DonePanel = (DivElement)Document.GetElementById(clientId + "_DonePanel");}; return _DonePanel;}} private DivElement _DonePanel;
		public jQueryObject DonePanelJ {get {if (_DonePanelJ == null) {_DonePanelJ = jQuery.Select("#" + clientId + "_DonePanel");}; return _DonePanelJ;}} private jQueryObject _DonePanelJ;
		public Element DoneQuantityLabel {get {if (_DoneQuantityLabel == null) {_DoneQuantityLabel = (Element)Document.GetElementById(clientId + "_DoneQuantityLabel");}; return _DoneQuantityLabel;}} private Element _DoneQuantityLabel;
		public jQueryObject DoneQuantityLabelJ {get {if (_DoneQuantityLabelJ == null) {_DoneQuantityLabelJ = jQuery.Select("#" + clientId + "_DoneQuantityLabel");}; return _DoneQuantityLabelJ;}} private jQueryObject _DoneQuantityLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement SoldOutPanel {get {if (_SoldOutPanel == null) {_SoldOutPanel = (DivElement)Document.GetElementById(clientId + "_SoldOutPanel");}; return _SoldOutPanel;}} private DivElement _SoldOutPanel;
		public jQueryObject SoldOutPanelJ {get {if (_SoldOutPanelJ == null) {_SoldOutPanelJ = jQuery.Select("#" + clientId + "_SoldOutPanel");}; return _SoldOutPanelJ;}} private jQueryObject _SoldOutPanelJ;
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
