Type.registerNamespace('SpottedScript.MixmagGreatest.Vote');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagGreatest.Vote.View
SpottedScript.MixmagGreatest.Vote.View = function SpottedScript_MixmagGreatest_Vote_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.MixmagGreatest.Vote.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.MixmagGreatest.Vote.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_MixmagGreatest_Vote_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagGreatest.Vote.View.registerClass('SpottedScript.MixmagGreatest.Vote.View', SpottedScript.MixmagGreatestUserControl.View);
