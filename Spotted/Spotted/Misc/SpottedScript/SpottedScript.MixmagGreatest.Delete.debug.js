Type.registerNamespace('SpottedScript.MixmagGreatest.Delete');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagGreatest.Delete.View
SpottedScript.MixmagGreatest.Delete.View = function SpottedScript_MixmagGreatest_Delete_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.MixmagGreatest.Delete.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.MixmagGreatest.Delete.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_MixmagGreatest_Delete_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagGreatest.Delete.View.registerClass('SpottedScript.MixmagGreatest.Delete.View', SpottedScript.MixmagGreatestUserControl.View);
