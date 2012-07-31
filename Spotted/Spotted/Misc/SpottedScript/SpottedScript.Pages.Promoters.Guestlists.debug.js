Type.registerNamespace('SpottedScript.Pages.Promoters.Guestlists');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Guestlists.View
SpottedScript.Pages.Promoters.Guestlists.View = function SpottedScript_Pages_Promoters_Guestlists_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Guestlists.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Guestlists.View.prototype = {
    clientId: null,
    get_h17: function SpottedScript_Pages_Promoters_Guestlists_View$get_h17() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H17');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Promoters_Guestlists_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_compareValidator1: function SpottedScript_Pages_Promoters_Guestlists_View$get_compareValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompareValidator1');
    },
    get_button1: function SpottedScript_Pages_Promoters_Guestlists_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_button2: function SpottedScript_Pages_Promoters_Guestlists_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_h18: function SpottedScript_Pages_Promoters_Guestlists_View$get_h18() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H18');
    },
    get_button3: function SpottedScript_Pages_Promoters_Guestlists_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_h19: function SpottedScript_Pages_Promoters_Guestlists_View$get_h19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19');
    },
    get_h14: function SpottedScript_Pages_Promoters_Guestlists_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_customValidator1: function SpottedScript_Pages_Promoters_Guestlists_View$get_customValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator1');
    },
    get_requiredfieldvalidator2: function SpottedScript_Pages_Promoters_Guestlists_View$get_requiredfieldvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator2');
    },
    get_comparevalidator2: function SpottedScript_Pages_Promoters_Guestlists_View$get_comparevalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Comparevalidator2');
    },
    get_requiredFieldValidator3: function SpottedScript_Pages_Promoters_Guestlists_View$get_requiredFieldValidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator3');
    },
    get_compareValidator3: function SpottedScript_Pages_Promoters_Guestlists_View$get_compareValidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompareValidator3');
    },
    get_compareValidator4: function SpottedScript_Pages_Promoters_Guestlists_View$get_compareValidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompareValidator4');
    },
    get_requiredfieldvalidator4: function SpottedScript_Pages_Promoters_Guestlists_View$get_requiredfieldvalidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator4');
    },
    get_compareValidator5: function SpottedScript_Pages_Promoters_Guestlists_View$get_compareValidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompareValidator5');
    },
    get_customvalidator2: function SpottedScript_Pages_Promoters_Guestlists_View$get_customvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator2');
    },
    get_customvalidator3: function SpottedScript_Pages_Promoters_Guestlists_View$get_customvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator3');
    },
    get_button4: function SpottedScript_Pages_Promoters_Guestlists_View$get_button4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button4');
    },
    get_button5: function SpottedScript_Pages_Promoters_Guestlists_View$get_button5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button5');
    },
    get_h13: function SpottedScript_Pages_Promoters_Guestlists_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_h16: function SpottedScript_Pages_Promoters_Guestlists_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_h12: function SpottedScript_Pages_Promoters_Guestlists_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_button6: function SpottedScript_Pages_Promoters_Guestlists_View$get_button6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button6');
    },
    get_button7: function SpottedScript_Pages_Promoters_Guestlists_View$get_button7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button7');
    },
    get_h11: function SpottedScript_Pages_Promoters_Guestlists_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_infoPanel: function SpottedScript_Pages_Promoters_Guestlists_View$get_infoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoPanel');
    },
    get_panelBuy: function SpottedScript_Pages_Promoters_Guestlists_View$get_panelBuy() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelBuy');
    },
    get_buyCredits: function SpottedScript_Pages_Promoters_Guestlists_View$get_buyCredits() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuyCredits');
    },
    get_panelPayDone: function SpottedScript_Pages_Promoters_Guestlists_View$get_panelPayDone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPayDone');
    },
    get_panelPay: function SpottedScript_Pages_Promoters_Guestlists_View$get_panelPay() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPay');
    },
    get_payment: function SpottedScript_Pages_Promoters_Guestlists_View$get_payment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Payment');
    },
    get_panelEdit: function SpottedScript_Pages_Promoters_Guestlists_View$get_panelEdit() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelEdit');
    },
    get_panelAddError: function SpottedScript_Pages_Promoters_Guestlists_View$get_panelAddError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelAddError');
    },
    get_panelAddCreditsError: function SpottedScript_Pages_Promoters_Guestlists_View$get_panelAddCreditsError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelAddCreditsError');
    },
    get_editEventDropDown: function SpottedScript_Pages_Promoters_Guestlists_View$get_editEventDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditEventDropDown');
    },
    get_editPriceTextBox: function SpottedScript_Pages_Promoters_Guestlists_View$get_editPriceTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPriceTextBox');
    },
    get_editRegularPriceTextBox: function SpottedScript_Pages_Promoters_Guestlists_View$get_editRegularPriceTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditRegularPriceTextBox');
    },
    get_editDetails: function SpottedScript_Pages_Promoters_Guestlists_View$get_editDetails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditDetails');
    },
    get_editLimit: function SpottedScript_Pages_Promoters_Guestlists_View$get_editLimit() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLimit');
    },
    get_editEventTr: function SpottedScript_Pages_Promoters_Guestlists_View$get_editEventTr() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditEventTr');
    },
    get_editEventTr1: function SpottedScript_Pages_Promoters_Guestlists_View$get_editEventTr1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditEventTr1');
    },
    get_panelList: function SpottedScript_Pages_Promoters_Guestlists_View$get_panelList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelList');
    },
    get_eventsDataGrid: function SpottedScript_Pages_Promoters_Guestlists_View$get_eventsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsDataGrid');
    },
    get_noGuestlistsLabel: function SpottedScript_Pages_Promoters_Guestlists_View$get_noGuestlistsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoGuestlistsLabel');
    },
    get_panelClose: function SpottedScript_Pages_Promoters_Guestlists_View$get_panelClose() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelClose');
    },
    get_panelCloseEventLabel: function SpottedScript_Pages_Promoters_Guestlists_View$get_panelCloseEventLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelCloseEventLabel');
    },
    get_panelCloseCountLabel: function SpottedScript_Pages_Promoters_Guestlists_View$get_panelCloseCountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelCloseCountLabel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Guestlists_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Guestlists.View.registerClass('SpottedScript.Pages.Promoters.Guestlists.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
