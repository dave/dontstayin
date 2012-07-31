Type.registerNamespace('SpottedScript.MixmagGreatest.Fb');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagGreatest.Fb.View
SpottedScript.MixmagGreatest.Fb.View = function SpottedScript_MixmagGreatest_Fb_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.MixmagGreatest.Fb.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.MixmagGreatest.Fb.View.prototype = {
    clientId: null,
    get_facebookComments: function SpottedScript_MixmagGreatest_Fb_View$get_facebookComments() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookComments');
    },
    get_genericContainerPage: function SpottedScript_MixmagGreatest_Fb_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagGreatest.Fb.View.registerClass('SpottedScript.MixmagGreatest.Fb.View', SpottedScript.MixmagGreatestUserControl.View);
