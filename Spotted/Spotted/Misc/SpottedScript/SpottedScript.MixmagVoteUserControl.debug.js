Type.registerNamespace('SpottedScript.MixmagVoteUserControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVoteUserControl.View
SpottedScript.MixmagVoteUserControl.View = function SpottedScript_MixmagVoteUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.MixmagVoteUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.MixmagVoteUserControl.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_MixmagVoteUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagVoteUserControl.View.registerClass('SpottedScript.MixmagVoteUserControl.View', SpottedScript.GenericUserControl.View);
