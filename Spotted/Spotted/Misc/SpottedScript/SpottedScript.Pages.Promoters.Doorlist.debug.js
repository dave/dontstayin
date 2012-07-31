Type.registerNamespace('SpottedScript.Pages.Promoters.Doorlist');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Doorlist.View
SpottedScript.Pages.Promoters.Doorlist.View = function SpottedScript_Pages_Promoters_Doorlist_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Doorlist.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Doorlist.View.prototype = {
    clientId: null,
    get_promoterIntro: function SpottedScript_Pages_Promoters_Doorlist_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_doorlistPanel: function SpottedScript_Pages_Promoters_Doorlist_View$get_doorlistPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoorlistPanel');
    },
    get_h1Title: function SpottedScript_Pages_Promoters_Doorlist_View$get_h1Title() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1Title');
    },
    get_hasTicketsP: function SpottedScript_Pages_Promoters_Doorlist_View$get_hasTicketsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HasTicketsP');
    },
    get_eventDropDownList: function SpottedScript_Pages_Promoters_Doorlist_View$get_eventDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventDropDownList');
    },
    get_doorlistButton: function SpottedScript_Pages_Promoters_Doorlist_View$get_doorlistButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoorlistButton');
    },
    get_noTicketsP: function SpottedScript_Pages_Promoters_Doorlist_View$get_noTicketsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoTicketsP');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Doorlist_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Doorlist.View.registerClass('SpottedScript.Pages.Promoters.Doorlist.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
