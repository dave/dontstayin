Type.registerNamespace('SpottedScript.Pages.Usrs.Promoters');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.Promoters.View
SpottedScript.Pages.Usrs.Promoters.View = function SpottedScript_Pages_Usrs_Promoters_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.Promoters.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.Promoters.View.prototype = {
    clientId: null,
    get_panelPromoterList: function SpottedScript_Pages_Usrs_Promoters_View$get_panelPromoterList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPromoterList');
    },
    get_h12: function SpottedScript_Pages_Usrs_Promoters_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_promoterRepeater: function SpottedScript_Pages_Usrs_Promoters_View$get_promoterRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterRepeater');
    },
    get_panelNoAccount: function SpottedScript_Pages_Usrs_Promoters_View$get_panelNoAccount() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelNoAccount');
    },
    get_h1bv2: function SpottedScript_Pages_Usrs_Promoters_View$get_h1bv2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1bv2');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_Promoters_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.Promoters.View.registerClass('SpottedScript.Pages.Usrs.Promoters.View', SpottedScript.Pages.Usrs.UsrUserControl.View);
