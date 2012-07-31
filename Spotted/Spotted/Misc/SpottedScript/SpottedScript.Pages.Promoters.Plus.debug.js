Type.registerNamespace('SpottedScript.Pages.Promoters.Plus');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Plus.View
SpottedScript.Pages.Promoters.Plus.View = function SpottedScript_Pages_Promoters_Plus_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Plus.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Plus.View.prototype = {
    clientId: null,
    get_promoterIntro1: function SpottedScript_Pages_Promoters_Plus_View$get_promoterIntro1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro1');
    },
    get_h1fd4: function SpottedScript_Pages_Promoters_Plus_View$get_h1fd4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1fd4');
    },
    get_h14: function SpottedScript_Pages_Promoters_Plus_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Plus_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Plus.View.registerClass('SpottedScript.Pages.Promoters.Plus.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
