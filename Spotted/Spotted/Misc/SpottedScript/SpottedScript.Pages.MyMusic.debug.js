Type.registerNamespace('SpottedScript.Pages.MyMusic');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyMusic.View
SpottedScript.Pages.MyMusic.View = function SpottedScript_Pages_MyMusic_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyMusic.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyMusic.View.prototype = {
    clientId: null,
    get_button1: function SpottedScript_Pages_MyMusic_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_musicTypes: function SpottedScript_Pages_MyMusic_View$get_musicTypes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicTypes');
    },
    get_updatedLabel: function SpottedScript_Pages_MyMusic_View$get_updatedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UpdatedLabel');
    },
    get_genericContainerPage: function SpottedScript_Pages_MyMusic_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyMusic.View.registerClass('SpottedScript.Pages.MyMusic.View', SpottedScript.DsiUserControl.View);
