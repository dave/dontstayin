Type.registerNamespace('SpottedScript.Blank.LegalInformationPolicy');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.LegalInformationPolicy.View
SpottedScript.Blank.LegalInformationPolicy.View = function SpottedScript_Blank_LegalInformationPolicy_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.LegalInformationPolicy.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.LegalInformationPolicy.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Blank_LegalInformationPolicy_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_genericContainerPage: function SpottedScript_Blank_LegalInformationPolicy_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.LegalInformationPolicy.View.registerClass('SpottedScript.Blank.LegalInformationPolicy.View', SpottedScript.BlankUserControl.View);
