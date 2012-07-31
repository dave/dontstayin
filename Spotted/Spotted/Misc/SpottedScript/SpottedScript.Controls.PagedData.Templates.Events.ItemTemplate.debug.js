Type.registerNamespace('SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.Controller
SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.Controller = function SpottedScript_Controls_PagedData_Templates_Events_ItemTemplate_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.View">
    /// </param>
    SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.Controller.initializeBase(this, [ view ]);
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.View
SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.View = function SpottedScript_Controls_PagedData_Templates_Events_ItemTemplate_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.View.prototype = {
    clientId: null
}
SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.Controller.registerClass('SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.Controller', SpottedScript.Controls.ClientSideRepeater.Template.Controller);
SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.View.registerClass('SpottedScript.Controls.PagedData.Templates.Events.ItemTemplate.View', SpottedScript.Controls.ClientSideRepeater.Template.View);
