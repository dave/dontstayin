Type.registerNamespace('SpottedScript.MixmagVote.Results');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Results.View
SpottedScript.MixmagVote.Results.View = function SpottedScript_MixmagVote_Results_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.MixmagVote.Results.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.MixmagVote.Results.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_MixmagVote_Results_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagVote.Results.View.registerClass('SpottedScript.MixmagVote.Results.View', SpottedScript.MixmagVoteUserControl.View);
