Type.registerNamespace('SpottedScript.Pages.Promoters.EventHighlight');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.EventHighlight.View
SpottedScript.Pages.Promoters.EventHighlight.View = function SpottedScript_Pages_Promoters_EventHighlight_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.EventHighlight.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.EventHighlight.View.prototype = {
    clientId: null,
    get_promoterIntro: function SpottedScript_Pages_Promoters_EventHighlight_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_introBannerListLink: function SpottedScript_Pages_Promoters_EventHighlight_View$get_introBannerListLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_IntroBannerListLink');
    },
    get_h13: function SpottedScript_Pages_Promoters_EventHighlight_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_button1: function SpottedScript_Pages_Promoters_EventHighlight_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_h15: function SpottedScript_Pages_Promoters_EventHighlight_View$get_h15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15');
    },
    get_button2: function SpottedScript_Pages_Promoters_EventHighlight_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_h16: function SpottedScript_Pages_Promoters_EventHighlight_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_recommendedCell: function SpottedScript_Pages_Promoters_EventHighlight_View$get_recommendedCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RecommendedCell');
    },
    get_recommendedCellPrice: function SpottedScript_Pages_Promoters_EventHighlight_View$get_recommendedCellPrice() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RecommendedCellPrice');
    },
    get_payment: function SpottedScript_Pages_Promoters_EventHighlight_View$get_payment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Payment');
    },
    get_choicePanel: function SpottedScript_Pages_Promoters_EventHighlight_View$get_choicePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChoicePanel');
    },
    get_payPanel: function SpottedScript_Pages_Promoters_EventHighlight_View$get_payPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PayPanel');
    },
    get_payDonePanel: function SpottedScript_Pages_Promoters_EventHighlight_View$get_payDonePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PayDonePanel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_EventHighlight_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.EventHighlight.View.registerClass('SpottedScript.Pages.Promoters.EventHighlight.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
