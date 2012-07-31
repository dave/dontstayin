Type.registerNamespace('SpottedScript.MixmagVote.Stop');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Stop.View
SpottedScript.MixmagVote.Stop.View = function SpottedScript_MixmagVote_Stop_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.MixmagVote.Stop.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.MixmagVote.Stop.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_MixmagVote_Stop_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagVote.Stop.View.registerClass('SpottedScript.MixmagVote.Stop.View', SpottedScript.MixmagVoteUserControl.View);
