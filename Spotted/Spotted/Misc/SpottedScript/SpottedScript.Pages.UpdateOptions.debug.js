Type.registerNamespace('SpottedScript.Pages.UpdateOptions');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.UpdateOptions.View
SpottedScript.Pages.UpdateOptions.View = function SpottedScript_Pages_UpdateOptions_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.UpdateOptions.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.UpdateOptions.View.prototype = {
    clientId: null,
    get_h19: function SpottedScript_Pages_UpdateOptions_View$get_h19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19');
    },
    get_button1: function SpottedScript_Pages_UpdateOptions_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_emailCheck: function SpottedScript_Pages_UpdateOptions_View$get_emailCheck() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailCheck');
    },
    get_musicGeneric: function SpottedScript_Pages_UpdateOptions_View$get_musicGeneric() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicGeneric');
    },
    get_musicLabel: function SpottedScript_Pages_UpdateOptions_View$get_musicLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicLabel');
    },
    get_genericMusicLabel: function SpottedScript_Pages_UpdateOptions_View$get_genericMusicLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericMusicLabel');
    },
    get_placesLabel: function SpottedScript_Pages_UpdateOptions_View$get_placesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlacesLabel');
    },
    get_genericMusicP: function SpottedScript_Pages_UpdateOptions_View$get_genericMusicP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericMusicP');
    },
    get_optionsPanel: function SpottedScript_Pages_UpdateOptions_View$get_optionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionsPanel');
    },
    get_genericContainerPage: function SpottedScript_Pages_UpdateOptions_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.UpdateOptions.View.registerClass('SpottedScript.Pages.UpdateOptions.View', SpottedScript.DsiUserControl.View);
