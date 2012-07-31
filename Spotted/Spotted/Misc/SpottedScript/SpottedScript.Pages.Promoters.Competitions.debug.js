Type.registerNamespace('SpottedScript.Pages.Promoters.Competitions');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Competitions.View
SpottedScript.Pages.Promoters.Competitions.View = function SpottedScript_Pages_Promoters_Competitions_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Competitions.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Competitions.View.prototype = {
    clientId: null,
    get_panelEdit: function SpottedScript_Pages_Promoters_Competitions_View$get_panelEdit() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelEdit');
    },
    get_editPrizesFirstNumber: function SpottedScript_Pages_Promoters_Competitions_View$get_editPrizesFirstNumber() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPrizesFirstNumber');
    },
    get_editPrizesFirstDesc: function SpottedScript_Pages_Promoters_Competitions_View$get_editPrizesFirstDesc() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPrizesFirstDesc');
    },
    get_editPrizesSecondNumber: function SpottedScript_Pages_Promoters_Competitions_View$get_editPrizesSecondNumber() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPrizesSecondNumber');
    },
    get_editPrizesSecondDesc: function SpottedScript_Pages_Promoters_Competitions_View$get_editPrizesSecondDesc() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPrizesSecondDesc');
    },
    get_editPrizesThirdNumber: function SpottedScript_Pages_Promoters_Competitions_View$get_editPrizesThirdNumber() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPrizesThirdNumber');
    },
    get_editPrizesThirdDesc: function SpottedScript_Pages_Promoters_Competitions_View$get_editPrizesThirdDesc() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPrizesThirdDesc');
    },
    get_editPrizesValue: function SpottedScript_Pages_Promoters_Competitions_View$get_editPrizesValue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPrizesValue');
    },
    get_editLinkNoneRadio: function SpottedScript_Pages_Promoters_Competitions_View$get_editLinkNoneRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLinkNoneRadio');
    },
    get_editLinkEventRadio: function SpottedScript_Pages_Promoters_Competitions_View$get_editLinkEventRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLinkEventRadio');
    },
    get_editLinkBrandRadio: function SpottedScript_Pages_Promoters_Competitions_View$get_editLinkBrandRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLinkBrandRadio');
    },
    get_editLinkEventP: function SpottedScript_Pages_Promoters_Competitions_View$get_editLinkEventP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLinkEventP');
    },
    get_editLinkBrandP: function SpottedScript_Pages_Promoters_Competitions_View$get_editLinkBrandP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLinkBrandP');
    },
    get_editLinkEventDropDown: function SpottedScript_Pages_Promoters_Competitions_View$get_editLinkEventDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLinkEventDropDown');
    },
    get_editLinkBrandDropDown: function SpottedScript_Pages_Promoters_Competitions_View$get_editLinkBrandDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLinkBrandDropDown');
    },
    get_editLinkEventAnchor: function SpottedScript_Pages_Promoters_Competitions_View$get_editLinkEventAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLinkEventAnchor');
    },
    get_editLinkBrandAnchor: function SpottedScript_Pages_Promoters_Competitions_View$get_editLinkBrandAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLinkBrandAnchor');
    },
    get_editQuestion: function SpottedScript_Pages_Promoters_Competitions_View$get_editQuestion() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditQuestion');
    },
    get_editAnswer1: function SpottedScript_Pages_Promoters_Competitions_View$get_editAnswer1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditAnswer1');
    },
    get_editAnswer2: function SpottedScript_Pages_Promoters_Competitions_View$get_editAnswer2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditAnswer2');
    },
    get_editAnswer3: function SpottedScript_Pages_Promoters_Competitions_View$get_editAnswer3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditAnswer3');
    },
    get_editCorrectRadio1: function SpottedScript_Pages_Promoters_Competitions_View$get_editCorrectRadio1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditCorrectRadio1');
    },
    get_editCorrectRadio2: function SpottedScript_Pages_Promoters_Competitions_View$get_editCorrectRadio2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditCorrectRadio2');
    },
    get_editCorrectRadio3: function SpottedScript_Pages_Promoters_Competitions_View$get_editCorrectRadio3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditCorrectRadio3');
    },
    get_editPrizeContact: function SpottedScript_Pages_Promoters_Competitions_View$get_editPrizeContact() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPrizeContact');
    },
    get_editDateClose: function SpottedScript_Pages_Promoters_Competitions_View$get_editDateClose() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditDateClose');
    },
    get_editDateStart: function SpottedScript_Pages_Promoters_Competitions_View$get_editDateStart() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditDateStart');
    },
    get_editLinkTr: function SpottedScript_Pages_Promoters_Competitions_View$get_editLinkTr() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditLinkTr');
    },
    get_panelPic: function SpottedScript_Pages_Promoters_Competitions_View$get_panelPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPic');
    },
    get_picUploadPanel: function SpottedScript_Pages_Promoters_Competitions_View$get_picUploadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicUploadPanel');
    },
    get_panelPicSavedPanel: function SpottedScript_Pages_Promoters_Competitions_View$get_panelPicSavedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPicSavedPanel');
    },
    get_picUc: function SpottedScript_Pages_Promoters_Competitions_View$get_picUc() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicUc');
    },
    get_picUploadDefaultPanel: function SpottedScript_Pages_Promoters_Competitions_View$get_picUploadDefaultPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicUploadDefaultPanel');
    },
    get_picUploadDefaultDataList: function SpottedScript_Pages_Promoters_Competitions_View$get_picUploadDefaultDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicUploadDefaultDataList');
    },
    get_panelList: function SpottedScript_Pages_Promoters_Competitions_View$get_panelList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelList');
    },
    get_compDataGrid: function SpottedScript_Pages_Promoters_Competitions_View$get_compDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompDataGrid');
    },
    get_promoterIntro: function SpottedScript_Pages_Promoters_Competitions_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_infoPanel: function SpottedScript_Pages_Promoters_Competitions_View$get_infoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoPanel');
    },
    get_h14: function SpottedScript_Pages_Promoters_Competitions_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_validationsummary1: function SpottedScript_Pages_Promoters_Competitions_View$get_validationsummary1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary1');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Promoters_Competitions_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_compareValidator1: function SpottedScript_Pages_Promoters_Competitions_View$get_compareValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompareValidator1');
    },
    get_requiredFieldValidator2: function SpottedScript_Pages_Promoters_Competitions_View$get_requiredFieldValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator2');
    },
    get_regularExpressionValidator1: function SpottedScript_Pages_Promoters_Competitions_View$get_regularExpressionValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator1');
    },
    get_compareValidator2: function SpottedScript_Pages_Promoters_Competitions_View$get_compareValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompareValidator2');
    },
    get_regularExpressionValidator2: function SpottedScript_Pages_Promoters_Competitions_View$get_regularExpressionValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator2');
    },
    get_compareValidator3: function SpottedScript_Pages_Promoters_Competitions_View$get_compareValidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompareValidator3');
    },
    get_regularExpressionValidator3: function SpottedScript_Pages_Promoters_Competitions_View$get_regularExpressionValidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator3');
    },
    get_requiredFieldValidator3: function SpottedScript_Pages_Promoters_Competitions_View$get_requiredFieldValidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator3');
    },
    get_regularExpressionValidator4: function SpottedScript_Pages_Promoters_Competitions_View$get_regularExpressionValidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator4');
    },
    get_editSponsorDescriptionHtml: function SpottedScript_Pages_Promoters_Competitions_View$get_editSponsorDescriptionHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_EditSponsorDescriptionHtmlController');
    },
    get_customValidator1: function SpottedScript_Pages_Promoters_Competitions_View$get_customValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator1');
    },
    get_requiredFieldValidator4: function SpottedScript_Pages_Promoters_Competitions_View$get_requiredFieldValidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator4');
    },
    get_regularExpressionValidator5: function SpottedScript_Pages_Promoters_Competitions_View$get_regularExpressionValidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator5');
    },
    get_requiredFieldValidator5: function SpottedScript_Pages_Promoters_Competitions_View$get_requiredFieldValidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator5');
    },
    get_regularExpressionValidator6: function SpottedScript_Pages_Promoters_Competitions_View$get_regularExpressionValidator6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator6');
    },
    get_requiredFieldValidator6: function SpottedScript_Pages_Promoters_Competitions_View$get_requiredFieldValidator6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator6');
    },
    get_regularExpressionValidator7: function SpottedScript_Pages_Promoters_Competitions_View$get_regularExpressionValidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator7');
    },
    get_requiredFieldValidator7: function SpottedScript_Pages_Promoters_Competitions_View$get_requiredFieldValidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator7');
    },
    get_regularExpressionValidator8: function SpottedScript_Pages_Promoters_Competitions_View$get_regularExpressionValidator8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator8');
    },
    get_customValidator2: function SpottedScript_Pages_Promoters_Competitions_View$get_customValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator2');
    },
    get_customvalidator3: function SpottedScript_Pages_Promoters_Competitions_View$get_customvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator3');
    },
    get_customvalidator4: function SpottedScript_Pages_Promoters_Competitions_View$get_customvalidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator4');
    },
    get_customvalidator5: function SpottedScript_Pages_Promoters_Competitions_View$get_customvalidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator5');
    },
    get_customvalidator6: function SpottedScript_Pages_Promoters_Competitions_View$get_customvalidator6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator6');
    },
    get_customValidator7: function SpottedScript_Pages_Promoters_Competitions_View$get_customValidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator7');
    },
    get_button2: function SpottedScript_Pages_Promoters_Competitions_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_button3: function SpottedScript_Pages_Promoters_Competitions_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_validationsummary2: function SpottedScript_Pages_Promoters_Competitions_View$get_validationsummary2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary2');
    },
    get_h12: function SpottedScript_Pages_Promoters_Competitions_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h11: function SpottedScript_Pages_Promoters_Competitions_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Competitions_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Competitions.View.registerClass('SpottedScript.Pages.Promoters.Competitions.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
