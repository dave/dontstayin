Type.registerNamespace('SpottedScript.Pages.Promoters.Unconfirmed');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Unconfirmed.View
SpottedScript.Pages.Promoters.Unconfirmed.View = function SpottedScript_Pages_Promoters_Unconfirmed_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Unconfirmed.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Unconfirmed.View.prototype = {
    clientId: null,
    get_panelUnconfirmed: function SpottedScript_Pages_Promoters_Unconfirmed_View$get_panelUnconfirmed() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelUnconfirmed');
    },
    get_promoterIntro: function SpottedScript_Pages_Promoters_Unconfirmed_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Unconfirmed_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Unconfirmed.View.registerClass('SpottedScript.Pages.Promoters.Unconfirmed.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
