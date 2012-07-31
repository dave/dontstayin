Type.registerNamespace('SpottedScript.GenericUserControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.GenericUserControl.View
SpottedScript.GenericUserControl.View = function SpottedScript_GenericUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.GenericUserControl.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_GenericUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.GenericUserControl.View.registerClass('SpottedScript.GenericUserControl.View');
