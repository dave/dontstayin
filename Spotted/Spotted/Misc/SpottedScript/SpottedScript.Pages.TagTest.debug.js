Type.registerNamespace('SpottedScript.Pages.TagTest');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.TagTest.View
SpottedScript.Pages.TagTest.View = function SpottedScript_Pages_TagTest_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.TagTest.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.TagTest.View.prototype = {
    clientId: null,
    get_tagOut: function SpottedScript_Pages_TagTest_View$get_tagOut() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TagOut');
    },
    get_tagIn: function SpottedScript_Pages_TagTest_View$get_tagIn() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TagIn');
    },
    get_genericContainerPage: function SpottedScript_Pages_TagTest_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.TagTest.View.registerClass('SpottedScript.Pages.TagTest.View', SpottedScript.DsiUserControl.View);
