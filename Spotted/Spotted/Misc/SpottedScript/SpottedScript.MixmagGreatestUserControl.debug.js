Type.registerNamespace('SpottedScript.MixmagGreatestUserControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagGreatestUserControl.View
SpottedScript.MixmagGreatestUserControl.View = function SpottedScript_MixmagGreatestUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.MixmagGreatestUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.MixmagGreatestUserControl.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_MixmagGreatestUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagGreatestUserControl.View.registerClass('SpottedScript.MixmagGreatestUserControl.View', SpottedScript.GenericUserControl.View);
