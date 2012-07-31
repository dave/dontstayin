Type.registerNamespace('SpottedScript.StyledUserControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.StyledUserControl.View
SpottedScript.StyledUserControl.View = function SpottedScript_StyledUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.StyledUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.StyledUserControl.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_StyledUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.StyledUserControl.View.registerClass('SpottedScript.StyledUserControl.View', SpottedScript.GenericUserControl.View);
