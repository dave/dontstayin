Type.registerNamespace('SpottedScript.Pages.Events.Delete');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.Delete.View
SpottedScript.Pages.Events.Delete.View = function SpottedScript_Pages_Events_Delete_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.Delete.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.Delete.View.prototype = {
    clientId: null,
    get_panelDelete: function SpottedScript_Pages_Events_Delete_View$get_panelDelete() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDelete');
    },
    get_h10: function SpottedScript_Pages_Events_Delete_View$get_h10() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H10');
    },
    get_eventDescriptionP: function SpottedScript_Pages_Events_Delete_View$get_eventDescriptionP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventDescriptionP');
    },
    get_password: function SpottedScript_Pages_Events_Delete_View$get_password() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Password');
    },
    get_panelError: function SpottedScript_Pages_Events_Delete_View$get_panelError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelError');
    },
    get_h11: function SpottedScript_Pages_Events_Delete_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_deleteFailedP: function SpottedScript_Pages_Events_Delete_View$get_deleteFailedP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteFailedP');
    },
    get_errorBackAnchor: function SpottedScript_Pages_Events_Delete_View$get_errorBackAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ErrorBackAnchor');
    },
    get_panelDone: function SpottedScript_Pages_Events_Delete_View$get_panelDone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDone');
    },
    get_h12: function SpottedScript_Pages_Events_Delete_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_genericContainerPage: function SpottedScript_Pages_Events_Delete_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.Delete.View.registerClass('SpottedScript.Pages.Events.Delete.View', SpottedScript.DsiUserControl.View);
