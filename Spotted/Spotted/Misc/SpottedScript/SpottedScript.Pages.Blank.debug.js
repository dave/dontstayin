Type.registerNamespace('SpottedScript.Pages.Blank');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Blank.Controller
SpottedScript.Pages.Blank.Controller = function SpottedScript_Pages_Blank_Controller(v) {
    /// <param name="v" type="SpottedScript.Pages.Blank.View">
    /// </param>
    /// <field name="view" type="SpottedScript.Pages.Blank.View">
    /// </field>
}
SpottedScript.Pages.Blank.Controller.prototype = {
    view: null,
    buttonClick: function SpottedScript_Pages_Blank_Controller$buttonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Blank.View
SpottedScript.Pages.Blank.View = function SpottedScript_Pages_Blank_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Blank.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Blank.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Blank_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Blank.Controller.registerClass('SpottedScript.Pages.Blank.Controller');
SpottedScript.Pages.Blank.View.registerClass('SpottedScript.Pages.Blank.View', SpottedScript.DsiUserControl.View);
