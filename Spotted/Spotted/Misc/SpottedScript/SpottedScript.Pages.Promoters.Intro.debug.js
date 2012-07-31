Type.registerNamespace('SpottedScript.Pages.Promoters.Intro');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Intro.View
SpottedScript.Pages.Promoters.Intro.View = function SpottedScript_Pages_Promoters_Intro_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Intro.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Intro.View.prototype = {
    clientId: null,
    get_panelNoAccount: function SpottedScript_Pages_Promoters_Intro_View$get_panelNoAccount() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelNoAccount');
    },
    get_panelPromoterList: function SpottedScript_Pages_Promoters_Intro_View$get_panelPromoterList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPromoterList');
    },
    get_promoterRepeater: function SpottedScript_Pages_Promoters_Intro_View$get_promoterRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterRepeater');
    },
    get_h12: function SpottedScript_Pages_Promoters_Intro_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h11: function SpottedScript_Pages_Promoters_Intro_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Intro_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Intro.View.registerClass('SpottedScript.Pages.Promoters.Intro.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
