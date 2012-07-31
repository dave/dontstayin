Type.registerNamespace('SpottedScript.Pages.Promoters.ConfirmCardDetails');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.ConfirmCardDetails.View
SpottedScript.Pages.Promoters.ConfirmCardDetails.View = function SpottedScript_Pages_Promoters_ConfirmCardDetails_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.ConfirmCardDetails.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.ConfirmCardDetails.View.prototype = {
    clientId: null,
    get_promoterIntro: function SpottedScript_Pages_Promoters_ConfirmCardDetails_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_uiSelect: function SpottedScript_Pages_Promoters_ConfirmCardDetails_View$get_uiSelect() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSelect');
    },
    get_uiEvents: function SpottedScript_Pages_Promoters_ConfirmCardDetails_View$get_uiEvents() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEvents');
    },
    get_uiNoEvents: function SpottedScript_Pages_Promoters_ConfirmCardDetails_View$get_uiNoEvents() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNoEvents');
    },
    get_uiDoorlistPanel: function SpottedScript_Pages_Promoters_ConfirmCardDetails_View$get_uiDoorlistPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDoorlistPanel');
    },
    get_uiDoorlist: function SpottedScript_Pages_Promoters_ConfirmCardDetails_View$get_uiDoorlist() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDoorlist');
    },
    get_uiSave: function SpottedScript_Pages_Promoters_ConfirmCardDetails_View$get_uiSave() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSave');
    },
    get_uiSomeWrongLabel: function SpottedScript_Pages_Promoters_ConfirmCardDetails_View$get_uiSomeWrongLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSomeWrongLabel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_ConfirmCardDetails_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.ConfirmCardDetails.View.registerClass('SpottedScript.Pages.Promoters.ConfirmCardDetails.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
