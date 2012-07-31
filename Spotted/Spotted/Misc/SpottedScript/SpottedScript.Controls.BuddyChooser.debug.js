Type.registerNamespace('SpottedScript.Controls.BuddyChooser');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.BuddyChooser.Controller
SpottedScript.Controls.BuddyChooser.Controller = function SpottedScript_Controls_BuddyChooser_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.BuddyChooser.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.BuddyChooser.View">
    /// </field>
    /// <field name="_behaviour" type="SpottedScript.Behaviours.CreateUserFromEmail.Controller">
    /// </field>
    this._view = view;
    this._behaviour = new SpottedScript.Behaviours.CreateUserFromEmail.Controller(view.get_uiHtmlAutoComplete());
}
SpottedScript.Controls.BuddyChooser.Controller.prototype = {
    _view: null,
    _behaviour: null,
    get__value: function SpottedScript_Controls_BuddyChooser_Controller$get__value() {
        /// <value type="String"></value>
        return this._view.get_uiHtmlAutoComplete().get_value();
    },
    set__value: function SpottedScript_Controls_BuddyChooser_Controller$set__value(value) {
        /// <value type="String"></value>
        this._view.get_uiHtmlAutoComplete().set_value(value);
        return value;
    },
    get__text: function SpottedScript_Controls_BuddyChooser_Controller$get__text() {
        /// <value type="String"></value>
        return this._view.get_uiHtmlAutoComplete().get_text();
    },
    set__text: function SpottedScript_Controls_BuddyChooser_Controller$set__text(value) {
        /// <value type="String"></value>
        this._view.get_uiHtmlAutoComplete().set_text(value);
        return value;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.BuddyChooser.View
SpottedScript.Controls.BuddyChooser.View = function SpottedScript_Controls_BuddyChooser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Controls.BuddyChooser.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Controls.BuddyChooser.View.prototype = {
    clientId: null,
    get_uiHtmlAutoComplete: function SpottedScript_Controls_BuddyChooser_View$get_uiHtmlAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiHtmlAutoCompleteBehaviour');
    },
    get_genericContainerPage: function SpottedScript_Controls_BuddyChooser_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Controls.BuddyChooser.Controller.registerClass('SpottedScript.Controls.BuddyChooser.Controller');
SpottedScript.Controls.BuddyChooser.View.registerClass('SpottedScript.Controls.BuddyChooser.View', SpottedScript.GenericUserControl.View);
