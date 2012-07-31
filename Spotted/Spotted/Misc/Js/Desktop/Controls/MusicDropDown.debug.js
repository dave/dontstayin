//! MusicDropDown.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.MusicDropDown');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MusicDropDown.Controller

Js.Controls.MusicDropDown.Controller = function Js_Controls_MusicDropDown_Controller(view) {
    /// <param name="view" type="Js.Controls.MusicDropDown.View">
    /// </param>
    /// <field name="view" type="Js.Controls.MusicDropDown.View">
    /// </field>
    this.view = view;
}
Js.Controls.MusicDropDown.Controller.prototype = {
    view: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MusicDropDown.View

Js.Controls.MusicDropDown.View = function Js_Controls_MusicDropDown_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_DropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_DropDownJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.MusicDropDown.View.prototype = {
    clientId: null,
    
    get_dropDown: function Js_Controls_MusicDropDown_View$get_dropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._DropDown == null) {
            this._DropDown = document.getElementById(this.clientId + '_DropDown');
        }
        return this._DropDown;
    },
    
    _DropDown: null,
    
    get_dropDownJ: function Js_Controls_MusicDropDown_View$get_dropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._DropDownJ == null) {
            this._DropDownJ = $('#' + this.clientId + '_DropDown');
        }
        return this._DropDownJ;
    },
    
    _DropDownJ: null
}


Js.Controls.MusicDropDown.Controller.registerClass('Js.Controls.MusicDropDown.Controller');
Js.Controls.MusicDropDown.View.registerClass('Js.Controls.MusicDropDown.View');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
