Type.registerNamespace('SpottedScript.Controls.ClientSideRepeater.Template');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ClientSideRepeater.Template.Controller
SpottedScript.Controls.ClientSideRepeater.Template.Controller = function SpottedScript_Controls_ClientSideRepeater_Template_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.ClientSideRepeater.Template.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.ClientSideRepeater.Template.View">
    /// </field>
    this._view = view;
}
SpottedScript.Controls.ClientSideRepeater.Template.Controller.prototype = {
    _view: null,
    render: function SpottedScript_Controls_ClientSideRepeater_Template_Controller$render(data) {
        /// <param name="data" type="Object">
        /// </param>
        /// <returns type="String"></returns>
        var transformed = this.transformData(data);
        var itemTemplate = document.getElementById(this._view.clientId).innerHTML;
        var $dict1 = transformed;
        for (var $key2 in $dict1) {
            var de = { key: $key2, value: $dict1[$key2] };
            var regex = new RegExp('{' + de.key + '}', 'g');
            itemTemplate = unescape(itemTemplate).replace(regex, de.value.toString());
        }
        return itemTemplate;
    },
    transformData: function SpottedScript_Controls_ClientSideRepeater_Template_Controller$transformData(data) {
        /// <param name="data" type="Object">
        /// </param>
        /// <returns type="Object"></returns>
        return data;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.ClientSideRepeater.Template.View
SpottedScript.Controls.ClientSideRepeater.Template.View = function SpottedScript_Controls_ClientSideRepeater_Template_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.ClientSideRepeater.Template.View.prototype = {
    clientId: null
}
SpottedScript.Controls.ClientSideRepeater.Template.Controller.registerClass('SpottedScript.Controls.ClientSideRepeater.Template.Controller');
SpottedScript.Controls.ClientSideRepeater.Template.View.registerClass('SpottedScript.Controls.ClientSideRepeater.Template.View');
