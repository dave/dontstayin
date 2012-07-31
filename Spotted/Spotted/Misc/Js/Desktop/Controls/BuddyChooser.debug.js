//! BuddyChooser.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.BuddyChooser');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.BuddyChooser.Controller

Js.Controls.BuddyChooser.Controller = function Js_Controls_BuddyChooser_Controller(view) {
    /// <param name="view" type="Js.Controls.BuddyChooser.View">
    /// </param>
    /// <field name="_view" type="Js.Controls.BuddyChooser.View">
    /// </field>
    /// <field name="_behaviour" type="Js.Controls.MultiBuddyChooser.CreateUserFromEmailController">
    /// </field>
    this._view = view;
    this._behaviour = new Js.Controls.MultiBuddyChooser.CreateUserFromEmailController(view.get_uiHtmlAutoComplete());
}
Js.Controls.BuddyChooser.Controller.prototype = {
    _view: null,
    _behaviour: null,
    
    get_value: function Js_Controls_BuddyChooser_Controller$get_value() {
        /// <value type="String"></value>
        return this._view.get_uiHtmlAutoComplete().get_value();
    },
    set_value: function Js_Controls_BuddyChooser_Controller$set_value(value) {
        /// <value type="String"></value>
        this._view.get_uiHtmlAutoComplete().set_value(value);
        return value;
    },
    
    get_text: function Js_Controls_BuddyChooser_Controller$get_text() {
        /// <value type="String"></value>
        return this._view.get_uiHtmlAutoComplete().get_text();
    },
    set_text: function Js_Controls_BuddyChooser_Controller$set_text(value) {
        /// <value type="String"></value>
        this._view.get_uiHtmlAutoComplete().set_text(value);
        return value;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.BuddyChooser.View

Js.Controls.BuddyChooser.View = function Js_Controls_BuddyChooser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_GenericContainerPage$1" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$1" type="jQueryObject">
    /// </field>
    Js.Controls.BuddyChooser.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Controls.BuddyChooser.View.prototype = {
    clientId: null,
    
    get_uiHtmlAutoComplete: function Js_Controls_BuddyChooser_View$get_uiHtmlAutoComplete() {
        /// <value type="Js.ClientControls.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiHtmlAutoCompleteBehaviour');
    },
    
    get_genericContainerPage: function Js_Controls_BuddyChooser_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$1 == null) {
            this._GenericContainerPage$1 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$1;
    },
    
    _GenericContainerPage$1: null,
    
    get_genericContainerPageJ: function Js_Controls_BuddyChooser_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$1 == null) {
            this._GenericContainerPageJ$1 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$1;
    },
    
    _GenericContainerPageJ$1: null
}


Js.Controls.BuddyChooser.Controller.registerClass('Js.Controls.BuddyChooser.Controller');
Js.Controls.BuddyChooser.View.registerClass('Js.Controls.BuddyChooser.View', Js.GenericUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
