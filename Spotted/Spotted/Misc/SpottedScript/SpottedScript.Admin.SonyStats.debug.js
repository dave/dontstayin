Type.registerNamespace('SpottedScript.Admin.SonyStats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SonyStats.View
SpottedScript.Admin.SonyStats.View = function SpottedScript_Admin_SonyStats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SonyStats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SonyStats.View.prototype = {
    clientId: null,
    get_mainDiv: function SpottedScript_Admin_SonyStats_View$get_mainDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MainDiv');
    },
    get_genericContainerPage: function SpottedScript_Admin_SonyStats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SonyStats.View.registerClass('SpottedScript.Admin.SonyStats.View', SpottedScript.DsiUserControl.View);
