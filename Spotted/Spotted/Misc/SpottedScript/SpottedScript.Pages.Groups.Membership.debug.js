Type.registerNamespace('SpottedScript.Pages.Groups.Membership');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Groups.Membership.View
SpottedScript.Pages.Groups.Membership.View = function SpottedScript_Pages_Groups_Membership_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Groups.Membership.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Groups.Membership.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Groups_Membership_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Groups.Membership.View.registerClass('SpottedScript.Pages.Groups.Membership.View', SpottedScript.DsiUserControl.View);
