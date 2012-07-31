Type.registerNamespace('SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.Controller
SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.Controller = function SpottedScript_Controls_PagedData_Templates_Threads_ItemTemplate_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View">
    /// </param>
    SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.Controller.initializeBase(this, [ view ]);
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View
SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View = function SpottedScript_Controls_PagedData_Templates_Threads_ItemTemplate_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View.prototype = {
    clientId: null
}
SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.Controller.registerClass('SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.Controller', SpottedScript.Controls.ClientSideRepeater.Template.Controller);
SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View.registerClass('SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View', SpottedScript.Controls.ClientSideRepeater.Template.View);
