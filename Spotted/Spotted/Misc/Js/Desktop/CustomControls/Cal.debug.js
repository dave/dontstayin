//! Cal.debug.js
//

(function($) {

Type.registerNamespace('Js.CustomControls.Cal');

////////////////////////////////////////////////////////////////////////////////
// Js.CustomControls.Cal.View

Js.CustomControls.Cal.View = function Js_CustomControls_Cal_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="_TextBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_TextBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
Js.CustomControls.Cal.View.prototype = {
    
    get_textBox: function Js_CustomControls_Cal_View$get_textBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._TextBox == null) {
            this._TextBox = document.getElementById(this.clientId + '_inner');
        }
        return this._TextBox;
    },
    
    _TextBox: null,
    
    get_textBoxJ: function Js_CustomControls_Cal_View$get_textBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._TextBoxJ == null) {
            this._TextBoxJ = $('#' + this.clientId + '_inner');
        }
        return this._TextBoxJ;
    },
    
    _TextBoxJ: null,
    clientId: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.CustomControls.Cal.Controller

Js.CustomControls.Cal.Controller = function Js_CustomControls_Cal_Controller(view) {
    /// <param name="view" type="Js.CustomControls.Cal.View">
    /// </param>
    /// <field name="_view" type="Js.CustomControls.Cal.View">
    /// </field>
    /// <field name="onDateChanged" type="Function">
    /// </field>
    this._view = view;
    view.get_textBoxJ().keydown(ss.Delegate.create(this, this._onKeyDown));
    view.get_textBoxJ().blur(ss.Delegate.create(this, this._onBlur));
}
Js.CustomControls.Cal.Controller.prototype = {
    _view: null,
    onDateChanged: null,
    
    _onBlur: function Js_CustomControls_Cal_Controller$_onBlur(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if (this.getDate() == null) {
            this._view.get_textBox().value = '';
        }
    },
    
    _onKeyDown: function Js_CustomControls_Cal_Controller$_onKeyDown(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if ('ABCDEFGHIJKLMNOPQRSTUVWXYZ,.;#[]'.indexOf(String.fromCharCode(e.which)) > -1) {
            e.preventDefault();
        }
    },
    
    setDate: function Js_CustomControls_Cal_Controller$setDate(dateTime) {
        /// <param name="dateTime" type="Date">
        /// </param>
        this._view.get_textBox().value = dateTime.format('dd/MM/yyyy');
    },
    
    getDate: function Js_CustomControls_Cal_Controller$getDate() {
        /// <returns type="Date"></returns>
        try {
            var parts = this._view.get_textBox().value.split('/');
            return Date.parseDate(parts[1] + '/' + parts[0] + '/' + parts[2]);
        }
        catch (ex) {
            return null;
        }
    },
    
    _focus: function Js_CustomControls_Cal_Controller$_focus() {
        this._view.get_textBox().focus();
    }
}


Js.CustomControls.Cal.View.registerClass('Js.CustomControls.Cal.View');
Js.CustomControls.Cal.Controller.registerClass('Js.CustomControls.Cal.Controller');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
