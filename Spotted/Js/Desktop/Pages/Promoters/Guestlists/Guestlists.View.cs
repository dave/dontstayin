//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CompareValidator", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Pages.Promoters.Guestlists
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
		public Element H17 {get {if (_H17 == null) {_H17 = (Element)Document.GetElementById(clientId + "_H17");}; return _H17;}} private Element _H17;
		public jQueryObject H17J {get {if (_H17J == null) {_H17J = jQuery.Select("#" + clientId + "_H17");}; return _H17J;}} private jQueryObject _H17J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element RequiredFieldValidator1 {get {if (_RequiredFieldValidator1 == null) {_RequiredFieldValidator1 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1;}} private Element _RequiredFieldValidator1;
		public jQueryObject RequiredFieldValidator1J {get {if (_RequiredFieldValidator1J == null) {_RequiredFieldValidator1J = jQuery.Select("#" + clientId + "_RequiredFieldValidator1");}; return _RequiredFieldValidator1J;}} private jQueryObject _RequiredFieldValidator1J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element CompareValidator1 {get {if (_CompareValidator1 == null) {_CompareValidator1 = (Element)Document.GetElementById(clientId + "_CompareValidator1");}; return _CompareValidator1;}} private Element _CompareValidator1;
		public jQueryObject CompareValidator1J {get {if (_CompareValidator1J == null) {_CompareValidator1J = jQuery.Select("#" + clientId + "_CompareValidator1");}; return _CompareValidator1J;}} private jQueryObject _CompareValidator1J;//mappings.Add("System.Web.UI.WebControls.CompareValidator", ElementGetter("Element"));
		public InputElement Button1 {get {if (_Button1 == null) {_Button1 = (InputElement)Document.GetElementById(clientId + "_Button1");}; return _Button1;}} private InputElement _Button1;
		public jQueryObject Button1J {get {if (_Button1J == null) {_Button1J = jQuery.Select("#" + clientId + "_Button1");}; return _Button1J;}} private jQueryObject _Button1J;
		public Element Button2 {get {if (_Button2 == null) {_Button2 = (Element)Document.GetElementById(clientId + "_Button2");}; return _Button2;}} private Element _Button2;
		public jQueryObject Button2J {get {if (_Button2J == null) {_Button2J = jQuery.Select("#" + clientId + "_Button2");}; return _Button2J;}} private jQueryObject _Button2J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element H18 {get {if (_H18 == null) {_H18 = (Element)Document.GetElementById(clientId + "_H18");}; return _H18;}} private Element _H18;
		public jQueryObject H18J {get {if (_H18J == null) {_H18J = jQuery.Select("#" + clientId + "_H18");}; return _H18J;}} private jQueryObject _H18J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public InputElement Button3 {get {if (_Button3 == null) {_Button3 = (InputElement)Document.GetElementById(clientId + "_Button3");}; return _Button3;}} private InputElement _Button3;
		public jQueryObject Button3J {get {if (_Button3J == null) {_Button3J = jQuery.Select("#" + clientId + "_Button3");}; return _Button3J;}} private jQueryObject _Button3J;
		public Element H19 {get {if (_H19 == null) {_H19 = (Element)Document.GetElementById(clientId + "_H19");}; return _H19;}} private Element _H19;
		public jQueryObject H19J {get {if (_H19J == null) {_H19J = jQuery.Select("#" + clientId + "_H19");}; return _H19J;}} private jQueryObject _H19J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H14 {get {if (_H14 == null) {_H14 = (Element)Document.GetElementById(clientId + "_H14");}; return _H14;}} private Element _H14;
		public jQueryObject H14J {get {if (_H14J == null) {_H14J = jQuery.Select("#" + clientId + "_H14");}; return _H14J;}} private jQueryObject _H14J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element CustomValidator1 {get {if (_CustomValidator1 == null) {_CustomValidator1 = (Element)Document.GetElementById(clientId + "_CustomValidator1");}; return _CustomValidator1;}} private Element _CustomValidator1;
		public jQueryObject CustomValidator1J {get {if (_CustomValidator1J == null) {_CustomValidator1J = jQuery.Select("#" + clientId + "_CustomValidator1");}; return _CustomValidator1J;}} private jQueryObject _CustomValidator1J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element Requiredfieldvalidator2 {get {if (_Requiredfieldvalidator2 == null) {_Requiredfieldvalidator2 = (Element)Document.GetElementById(clientId + "_Requiredfieldvalidator2");}; return _Requiredfieldvalidator2;}} private Element _Requiredfieldvalidator2;
		public jQueryObject Requiredfieldvalidator2J {get {if (_Requiredfieldvalidator2J == null) {_Requiredfieldvalidator2J = jQuery.Select("#" + clientId + "_Requiredfieldvalidator2");}; return _Requiredfieldvalidator2J;}} private jQueryObject _Requiredfieldvalidator2J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element Comparevalidator2 {get {if (_Comparevalidator2 == null) {_Comparevalidator2 = (Element)Document.GetElementById(clientId + "_Comparevalidator2");}; return _Comparevalidator2;}} private Element _Comparevalidator2;
		public jQueryObject Comparevalidator2J {get {if (_Comparevalidator2J == null) {_Comparevalidator2J = jQuery.Select("#" + clientId + "_Comparevalidator2");}; return _Comparevalidator2J;}} private jQueryObject _Comparevalidator2J;//mappings.Add("System.Web.UI.WebControls.CompareValidator", ElementGetter("Element"));
		public Element RequiredFieldValidator3 {get {if (_RequiredFieldValidator3 == null) {_RequiredFieldValidator3 = (Element)Document.GetElementById(clientId + "_RequiredFieldValidator3");}; return _RequiredFieldValidator3;}} private Element _RequiredFieldValidator3;
		public jQueryObject RequiredFieldValidator3J {get {if (_RequiredFieldValidator3J == null) {_RequiredFieldValidator3J = jQuery.Select("#" + clientId + "_RequiredFieldValidator3");}; return _RequiredFieldValidator3J;}} private jQueryObject _RequiredFieldValidator3J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element CompareValidator3 {get {if (_CompareValidator3 == null) {_CompareValidator3 = (Element)Document.GetElementById(clientId + "_CompareValidator3");}; return _CompareValidator3;}} private Element _CompareValidator3;
		public jQueryObject CompareValidator3J {get {if (_CompareValidator3J == null) {_CompareValidator3J = jQuery.Select("#" + clientId + "_CompareValidator3");}; return _CompareValidator3J;}} private jQueryObject _CompareValidator3J;//mappings.Add("System.Web.UI.WebControls.CompareValidator", ElementGetter("Element"));
		public Element CompareValidator4 {get {if (_CompareValidator4 == null) {_CompareValidator4 = (Element)Document.GetElementById(clientId + "_CompareValidator4");}; return _CompareValidator4;}} private Element _CompareValidator4;
		public jQueryObject CompareValidator4J {get {if (_CompareValidator4J == null) {_CompareValidator4J = jQuery.Select("#" + clientId + "_CompareValidator4");}; return _CompareValidator4J;}} private jQueryObject _CompareValidator4J;//mappings.Add("System.Web.UI.WebControls.CompareValidator", ElementGetter("Element"));
		public Element Requiredfieldvalidator4 {get {if (_Requiredfieldvalidator4 == null) {_Requiredfieldvalidator4 = (Element)Document.GetElementById(clientId + "_Requiredfieldvalidator4");}; return _Requiredfieldvalidator4;}} private Element _Requiredfieldvalidator4;
		public jQueryObject Requiredfieldvalidator4J {get {if (_Requiredfieldvalidator4J == null) {_Requiredfieldvalidator4J = jQuery.Select("#" + clientId + "_Requiredfieldvalidator4");}; return _Requiredfieldvalidator4J;}} private jQueryObject _Requiredfieldvalidator4J;//mappings.Add("System.Web.UI.WebControls.RequiredFieldValidator", ElementGetter("Element"));
		public Element CompareValidator5 {get {if (_CompareValidator5 == null) {_CompareValidator5 = (Element)Document.GetElementById(clientId + "_CompareValidator5");}; return _CompareValidator5;}} private Element _CompareValidator5;
		public jQueryObject CompareValidator5J {get {if (_CompareValidator5J == null) {_CompareValidator5J = jQuery.Select("#" + clientId + "_CompareValidator5");}; return _CompareValidator5J;}} private jQueryObject _CompareValidator5J;//mappings.Add("System.Web.UI.WebControls.CompareValidator", ElementGetter("Element"));
		public Element Customvalidator2 {get {if (_Customvalidator2 == null) {_Customvalidator2 = (Element)Document.GetElementById(clientId + "_Customvalidator2");}; return _Customvalidator2;}} private Element _Customvalidator2;
		public jQueryObject Customvalidator2J {get {if (_Customvalidator2J == null) {_Customvalidator2J = jQuery.Select("#" + clientId + "_Customvalidator2");}; return _Customvalidator2J;}} private jQueryObject _Customvalidator2J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public Element Customvalidator3 {get {if (_Customvalidator3 == null) {_Customvalidator3 = (Element)Document.GetElementById(clientId + "_Customvalidator3");}; return _Customvalidator3;}} private Element _Customvalidator3;
		public jQueryObject Customvalidator3J {get {if (_Customvalidator3J == null) {_Customvalidator3J = jQuery.Select("#" + clientId + "_Customvalidator3");}; return _Customvalidator3J;}} private jQueryObject _Customvalidator3J;//mappings.Add("System.Web.UI.WebControls.CustomValidator", ElementGetter("Element"));
		public InputElement Button4 {get {if (_Button4 == null) {_Button4 = (InputElement)Document.GetElementById(clientId + "_Button4");}; return _Button4;}} private InputElement _Button4;
		public jQueryObject Button4J {get {if (_Button4J == null) {_Button4J = jQuery.Select("#" + clientId + "_Button4");}; return _Button4J;}} private jQueryObject _Button4J;
		public Element Button5 {get {if (_Button5 == null) {_Button5 = (Element)Document.GetElementById(clientId + "_Button5");}; return _Button5;}} private Element _Button5;
		public jQueryObject Button5J {get {if (_Button5J == null) {_Button5J = jQuery.Select("#" + clientId + "_Button5");}; return _Button5J;}} private jQueryObject _Button5J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element H13 {get {if (_H13 == null) {_H13 = (Element)Document.GetElementById(clientId + "_H13");}; return _H13;}} private Element _H13;
		public jQueryObject H13J {get {if (_H13J == null) {_H13J = jQuery.Select("#" + clientId + "_H13");}; return _H13J;}} private jQueryObject _H13J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H16 {get {if (_H16 == null) {_H16 = (Element)Document.GetElementById(clientId + "_H16");}; return _H16;}} private Element _H16;
		public jQueryObject H16J {get {if (_H16J == null) {_H16J = jQuery.Select("#" + clientId + "_H16");}; return _H16J;}} private jQueryObject _H16J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public Element H12 {get {if (_H12 == null) {_H12 = (Element)Document.GetElementById(clientId + "_H12");}; return _H12;}} private Element _H12;
		public jQueryObject H12J {get {if (_H12J == null) {_H12J = jQuery.Select("#" + clientId + "_H12");}; return _H12J;}} private jQueryObject _H12J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public InputElement Button6 {get {if (_Button6 == null) {_Button6 = (InputElement)Document.GetElementById(clientId + "_Button6");}; return _Button6;}} private InputElement _Button6;
		public jQueryObject Button6J {get {if (_Button6J == null) {_Button6J = jQuery.Select("#" + clientId + "_Button6");}; return _Button6J;}} private jQueryObject _Button6J;
		public Element Button7 {get {if (_Button7 == null) {_Button7 = (Element)Document.GetElementById(clientId + "_Button7");}; return _Button7;}} private Element _Button7;
		public jQueryObject Button7J {get {if (_Button7J == null) {_Button7J = jQuery.Select("#" + clientId + "_Button7");}; return _Button7J;}} private jQueryObject _Button7J;//mappings.Add("System.Web.UI.WebControls.Button", ElementGetter("Element"));
		public Element H11 {get {if (_H11 == null) {_H11 = (Element)Document.GetElementById(clientId + "_H11");}; return _H11;}} private Element _H11;
		public jQueryObject H11J {get {if (_H11J == null) {_H11J = jQuery.Select("#" + clientId + "_H11");}; return _H11J;}} private jQueryObject _H11J;//mappings.Add("Spotted.CustomControls.h1", ElementGetter("Element"));
		public DivElement InfoPanel {get {if (_InfoPanel == null) {_InfoPanel = (DivElement)Document.GetElementById(clientId + "_InfoPanel");}; return _InfoPanel;}} private DivElement _InfoPanel;
		public jQueryObject InfoPanelJ {get {if (_InfoPanelJ == null) {_InfoPanelJ = jQuery.Select("#" + clientId + "_InfoPanel");}; return _InfoPanelJ;}} private jQueryObject _InfoPanelJ;
		public DivElement PanelBuy {get {if (_PanelBuy == null) {_PanelBuy = (DivElement)Document.GetElementById(clientId + "_PanelBuy");}; return _PanelBuy;}} private DivElement _PanelBuy;
		public jQueryObject PanelBuyJ {get {if (_PanelBuyJ == null) {_PanelBuyJ = jQuery.Select("#" + clientId + "_PanelBuy");}; return _PanelBuyJ;}} private jQueryObject _PanelBuyJ;
		public InputElement BuyCredits {get {if (_BuyCredits == null) {_BuyCredits = (InputElement)Document.GetElementById(clientId + "_BuyCredits");}; return _BuyCredits;}} private InputElement _BuyCredits;
		public jQueryObject BuyCreditsJ {get {if (_BuyCreditsJ == null) {_BuyCreditsJ = jQuery.Select("#" + clientId + "_BuyCredits");}; return _BuyCreditsJ;}} private jQueryObject _BuyCreditsJ;
		public DivElement PanelPayDone {get {if (_PanelPayDone == null) {_PanelPayDone = (DivElement)Document.GetElementById(clientId + "_PanelPayDone");}; return _PanelPayDone;}} private DivElement _PanelPayDone;
		public jQueryObject PanelPayDoneJ {get {if (_PanelPayDoneJ == null) {_PanelPayDoneJ = jQuery.Select("#" + clientId + "_PanelPayDone");}; return _PanelPayDoneJ;}} private jQueryObject _PanelPayDoneJ;
		public DivElement PanelPay {get {if (_PanelPay == null) {_PanelPay = (DivElement)Document.GetElementById(clientId + "_PanelPay");}; return _PanelPay;}} private DivElement _PanelPay;
		public jQueryObject PanelPayJ {get {if (_PanelPayJ == null) {_PanelPayJ = jQuery.Select("#" + clientId + "_PanelPay");}; return _PanelPayJ;}} private jQueryObject _PanelPayJ;
		public Element Payment {get {if (_Payment == null) {_Payment = (Element)Document.GetElementById(clientId + "_Payment");}; return _Payment;}} private Element _Payment;
		public jQueryObject PaymentJ {get {if (_PaymentJ == null) {_PaymentJ = jQuery.Select("#" + clientId + "_Payment");}; return _PaymentJ;}} private jQueryObject _PaymentJ;//mappings.Add("Spotted.Controls.Payment", ElementGetter("Element"));
		public DivElement PanelEdit {get {if (_PanelEdit == null) {_PanelEdit = (DivElement)Document.GetElementById(clientId + "_PanelEdit");}; return _PanelEdit;}} private DivElement _PanelEdit;
		public jQueryObject PanelEditJ {get {if (_PanelEditJ == null) {_PanelEditJ = jQuery.Select("#" + clientId + "_PanelEdit");}; return _PanelEditJ;}} private jQueryObject _PanelEditJ;
		public DivElement PanelAddError {get {if (_PanelAddError == null) {_PanelAddError = (DivElement)Document.GetElementById(clientId + "_PanelAddError");}; return _PanelAddError;}} private DivElement _PanelAddError;
		public jQueryObject PanelAddErrorJ {get {if (_PanelAddErrorJ == null) {_PanelAddErrorJ = jQuery.Select("#" + clientId + "_PanelAddError");}; return _PanelAddErrorJ;}} private jQueryObject _PanelAddErrorJ;
		public DivElement PanelAddCreditsError {get {if (_PanelAddCreditsError == null) {_PanelAddCreditsError = (DivElement)Document.GetElementById(clientId + "_PanelAddCreditsError");}; return _PanelAddCreditsError;}} private DivElement _PanelAddCreditsError;
		public jQueryObject PanelAddCreditsErrorJ {get {if (_PanelAddCreditsErrorJ == null) {_PanelAddCreditsErrorJ = jQuery.Select("#" + clientId + "_PanelAddCreditsError");}; return _PanelAddCreditsErrorJ;}} private jQueryObject _PanelAddCreditsErrorJ;
		public SelectElement EditEventDropDown {get {if (_EditEventDropDown == null) {_EditEventDropDown = (SelectElement)Document.GetElementById(clientId + "_EditEventDropDown");}; return _EditEventDropDown;}} private SelectElement _EditEventDropDown;
		public jQueryObject EditEventDropDownJ {get {if (_EditEventDropDownJ == null) {_EditEventDropDownJ = jQuery.Select("#" + clientId + "_EditEventDropDown");}; return _EditEventDropDownJ;}} private jQueryObject _EditEventDropDownJ;
		public InputElement EditPriceTextBox {get {if (_EditPriceTextBox == null) {_EditPriceTextBox = (InputElement)Document.GetElementById(clientId + "_EditPriceTextBox");}; return _EditPriceTextBox;}} private InputElement _EditPriceTextBox;
		public jQueryObject EditPriceTextBoxJ {get {if (_EditPriceTextBoxJ == null) {_EditPriceTextBoxJ = jQuery.Select("#" + clientId + "_EditPriceTextBox");}; return _EditPriceTextBoxJ;}} private jQueryObject _EditPriceTextBoxJ;
		public InputElement EditRegularPriceTextBox {get {if (_EditRegularPriceTextBox == null) {_EditRegularPriceTextBox = (InputElement)Document.GetElementById(clientId + "_EditRegularPriceTextBox");}; return _EditRegularPriceTextBox;}} private InputElement _EditRegularPriceTextBox;
		public jQueryObject EditRegularPriceTextBoxJ {get {if (_EditRegularPriceTextBoxJ == null) {_EditRegularPriceTextBoxJ = jQuery.Select("#" + clientId + "_EditRegularPriceTextBox");}; return _EditRegularPriceTextBoxJ;}} private jQueryObject _EditRegularPriceTextBoxJ;
		public InputElement EditDetails {get {if (_EditDetails == null) {_EditDetails = (InputElement)Document.GetElementById(clientId + "_EditDetails");}; return _EditDetails;}} private InputElement _EditDetails;
		public jQueryObject EditDetailsJ {get {if (_EditDetailsJ == null) {_EditDetailsJ = jQuery.Select("#" + clientId + "_EditDetails");}; return _EditDetailsJ;}} private jQueryObject _EditDetailsJ;
		public InputElement EditLimit {get {if (_EditLimit == null) {_EditLimit = (InputElement)Document.GetElementById(clientId + "_EditLimit");}; return _EditLimit;}} private InputElement _EditLimit;
		public jQueryObject EditLimitJ {get {if (_EditLimitJ == null) {_EditLimitJ = jQuery.Select("#" + clientId + "_EditLimit");}; return _EditLimitJ;}} private jQueryObject _EditLimitJ;
		public Element EditEventTr {get {if (_EditEventTr == null) {_EditEventTr = (Element)Document.GetElementById(clientId + "_EditEventTr");}; return _EditEventTr;}} private Element _EditEventTr;
		public jQueryObject EditEventTrJ {get {if (_EditEventTrJ == null) {_EditEventTrJ = jQuery.Select("#" + clientId + "_EditEventTr");}; return _EditEventTrJ;}} private jQueryObject _EditEventTrJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public Element EditEventTr1 {get {if (_EditEventTr1 == null) {_EditEventTr1 = (Element)Document.GetElementById(clientId + "_EditEventTr1");}; return _EditEventTr1;}} private Element _EditEventTr1;
		public jQueryObject EditEventTr1J {get {if (_EditEventTr1J == null) {_EditEventTr1J = jQuery.Select("#" + clientId + "_EditEventTr1");}; return _EditEventTr1J;}} private jQueryObject _EditEventTr1J;//mappings.Add("System.Web.UI.HtmlControls.HtmlTableRow", ElementGetter("Element"));
		public DivElement PanelList {get {if (_PanelList == null) {_PanelList = (DivElement)Document.GetElementById(clientId + "_PanelList");}; return _PanelList;}} private DivElement _PanelList;
		public jQueryObject PanelListJ {get {if (_PanelListJ == null) {_PanelListJ = jQuery.Select("#" + clientId + "_PanelList");}; return _PanelListJ;}} private jQueryObject _PanelListJ;
		public Element EventsDataGrid {get {if (_EventsDataGrid == null) {_EventsDataGrid = (Element)Document.GetElementById(clientId + "_EventsDataGrid");}; return _EventsDataGrid;}} private Element _EventsDataGrid;
		public jQueryObject EventsDataGridJ {get {if (_EventsDataGridJ == null) {_EventsDataGridJ = jQuery.Select("#" + clientId + "_EventsDataGrid");}; return _EventsDataGridJ;}} private jQueryObject _EventsDataGridJ;//mappings.Add("System.Web.UI.WebControls.DataGrid", ElementGetter("Element"));
		public Element NoGuestlistsLabel {get {if (_NoGuestlistsLabel == null) {_NoGuestlistsLabel = (Element)Document.GetElementById(clientId + "_NoGuestlistsLabel");}; return _NoGuestlistsLabel;}} private Element _NoGuestlistsLabel;
		public jQueryObject NoGuestlistsLabelJ {get {if (_NoGuestlistsLabelJ == null) {_NoGuestlistsLabelJ = jQuery.Select("#" + clientId + "_NoGuestlistsLabel");}; return _NoGuestlistsLabelJ;}} private jQueryObject _NoGuestlistsLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public DivElement PanelClose {get {if (_PanelClose == null) {_PanelClose = (DivElement)Document.GetElementById(clientId + "_PanelClose");}; return _PanelClose;}} private DivElement _PanelClose;
		public jQueryObject PanelCloseJ {get {if (_PanelCloseJ == null) {_PanelCloseJ = jQuery.Select("#" + clientId + "_PanelClose");}; return _PanelCloseJ;}} private jQueryObject _PanelCloseJ;
		public Element PanelCloseEventLabel {get {if (_PanelCloseEventLabel == null) {_PanelCloseEventLabel = (Element)Document.GetElementById(clientId + "_PanelCloseEventLabel");}; return _PanelCloseEventLabel;}} private Element _PanelCloseEventLabel;
		public jQueryObject PanelCloseEventLabelJ {get {if (_PanelCloseEventLabelJ == null) {_PanelCloseEventLabelJ = jQuery.Select("#" + clientId + "_PanelCloseEventLabel");}; return _PanelCloseEventLabelJ;}} private jQueryObject _PanelCloseEventLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element PanelCloseCountLabel {get {if (_PanelCloseCountLabel == null) {_PanelCloseCountLabel = (Element)Document.GetElementById(clientId + "_PanelCloseCountLabel");}; return _PanelCloseCountLabel;}} private Element _PanelCloseCountLabel;
		public jQueryObject PanelCloseCountLabelJ {get {if (_PanelCloseCountLabelJ == null) {_PanelCloseCountLabelJ = jQuery.Select("#" + clientId + "_PanelCloseCountLabel");}; return _PanelCloseCountLabelJ;}} private jQueryObject _PanelCloseCountLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element GenericContainerPage {get {if (_GenericContainerPage == null) {_GenericContainerPage = (Element)Document.GetElementById(clientId + "_GenericContainerPage");}; return _GenericContainerPage;}} private Element _GenericContainerPage;
		public jQueryObject GenericContainerPageJ {get {if (_GenericContainerPageJ == null) {_GenericContainerPageJ = jQuery.Select("#" + clientId + "_GenericContainerPage");}; return _GenericContainerPageJ;}} private jQueryObject _GenericContainerPageJ;//mappings.Add("Spotted.GenericPage", ElementGetter("Element"));
	}
}
