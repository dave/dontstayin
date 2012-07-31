Type.registerNamespace('SpottedScript.Pages.Usrs.Spottings');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.Spottings.View
SpottedScript.Pages.Usrs.Spottings.View = function SpottedScript_Pages_Usrs_Spottings_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.Spottings.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.Spottings.View.prototype = {
    clientId: null,
    get_panelSpottings: function SpottedScript_Pages_Usrs_Spottings_View$get_panelSpottings() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelSpottings');
    },
    get_spotterIcon: function SpottedScript_Pages_Usrs_Spottings_View$get_spotterIcon() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterIcon');
    },
    get_spottingsDataList: function SpottedScript_Pages_Usrs_Spottings_View$get_spottingsDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpottingsDataList');
    },
    get_noRecordsNewAnchor: function SpottedScript_Pages_Usrs_Spottings_View$get_noRecordsNewAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoRecordsNewAnchor');
    },
    get_noRecordsP: function SpottedScript_Pages_Usrs_Spottings_View$get_noRecordsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoRecordsP');
    },
    get_dataListP: function SpottedScript_Pages_Usrs_Spottings_View$get_dataListP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DataListP');
    },
    get_listPageLinksP: function SpottedScript_Pages_Usrs_Spottings_View$get_listPageLinksP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ListPageLinksP');
    },
    get_usrIntro: function SpottedScript_Pages_Usrs_Spottings_View$get_usrIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrIntro');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_Spottings_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.Spottings.View.registerClass('SpottedScript.Pages.Usrs.Spottings.View', SpottedScript.Pages.Usrs.UsrUserControl.View);
