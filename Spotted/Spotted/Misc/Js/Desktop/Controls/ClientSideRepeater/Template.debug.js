//! Template.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.ClientSideRepeater.Template');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.ClientSideRepeater.Template.Controller

Js.Controls.ClientSideRepeater.Template.Controller = function Js_Controls_ClientSideRepeater_Template_Controller(view) {
    /// <param name="view" type="Js.Controls.ClientSideRepeater.Template.View">
    /// </param>
    /// <field name="_view" type="Js.Controls.ClientSideRepeater.Template.View">
    /// </field>
    this._view = view;
}
Js.Controls.ClientSideRepeater.Template.Controller.prototype = {
    _view: null,
    
    render: function Js_Controls_ClientSideRepeater_Template_Controller$render(data) {
        /// <param name="data" type="Object">
        /// </param>
        /// <returns type="String"></returns>
        var transformed = this.transformData(data);
        var itemTemplate = document.getElementById(this._view.clientId).innerHTML;
        var $enum1 = ss.IEnumerator.getEnumerator(transformed);
        while ($enum1.moveNext()) {
            var key = $enum1.current;
            var regex = new RegExp('{' + key.toString() + '}', 'g');
            itemTemplate = unescape(itemTemplate).replace(regex, transformed[key].toString());
        }
        return itemTemplate;
    },
    
    transformData: function Js_Controls_ClientSideRepeater_Template_Controller$transformData(data) {
        /// <param name="data" type="Object">
        /// </param>
        /// <returns type="Object"></returns>
        return data;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.ClientSideRepeater.Template.View

Js.Controls.ClientSideRepeater.Template.View = function Js_Controls_ClientSideRepeater_Template_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.ClientSideRepeater.Template.View.prototype = {
    clientId: null
}


Js.Controls.ClientSideRepeater.Template.Controller.registerClass('Js.Controls.ClientSideRepeater.Template.Controller');
Js.Controls.ClientSideRepeater.Template.View.registerClass('Js.Controls.ClientSideRepeater.Template.View');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
