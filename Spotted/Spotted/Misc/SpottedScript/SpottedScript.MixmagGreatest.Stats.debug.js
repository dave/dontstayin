Type.registerNamespace('SpottedScript.MixmagGreatest.Stats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagGreatest.Stats.View
SpottedScript.MixmagGreatest.Stats.View = function SpottedScript_MixmagGreatest_Stats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.MixmagGreatest.Stats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.MixmagGreatest.Stats.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_MixmagGreatest_Stats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagGreatest.Stats.View.registerClass('SpottedScript.MixmagGreatest.Stats.View', SpottedScript.MixmagGreatestUserControl.View);
