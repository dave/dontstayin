Type.registerNamespace('SpottedScript.CustomControls.Cal');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.CustomControls.Cal.Controller
SpottedScript.CustomControls.Cal.Controller = function SpottedScript_CustomControls_Cal_Controller(view) {
    /// <param name="view" type="SpottedScript.CustomControls.Cal.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.CustomControls.Cal.View">
    /// </field>
    /// <field name="onDateChanged" type="SpottedScript.CustomControls.Cal.DateDelegate">
    /// </field>
    this._view = view;
    $addHandler(this._view.get_textBox(), 'keydown', Function.createDelegate(this, this._onKeyDown));
    $addHandler(this._view.get_textBox(), 'blur', Function.createDelegate(this, this._onBlur));
}
SpottedScript.CustomControls.Cal.Controller.prototype = {
    _view: null,
    onDateChanged: null,
    _onBlur: function SpottedScript_CustomControls_Cal_Controller$_onBlur(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this.getDate() == null) {
            this._view.get_textBox().value = '';
        }
    },
    _onKeyDown: function SpottedScript_CustomControls_Cal_Controller$_onKeyDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if ('ABCDEFGHIJKLMNOPQRSTUVWXYZ,.;#[]'.indexOf(String.fromCharCode(e.keyCode)) > -1) {
            e.preventDefault();
        }
    },
    setDate: function SpottedScript_CustomControls_Cal_Controller$setDate(dateTime) {
        /// <param name="dateTime" type="Date">
        /// </param>
        this._view.get_textBox().value = dateTime.format('dd/MM/yyyy');
    },
    getDate: function SpottedScript_CustomControls_Cal_Controller$getDate() {
        /// <returns type="Date"></returns>
        try {
            var parts = this._view.get_textBox().value.split('/');
            return Date.parseLocale(parts[1] + '/' + parts[0] + '/' + parts[2]);
        }
        catch (ex) {
            return null;
        }
    },
    _focus: function SpottedScript_CustomControls_Cal_Controller$_focus() {
        this._view.get_textBox().focus();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.CustomControls.Cal.View
SpottedScript.CustomControls.Cal.View = function SpottedScript_CustomControls_Cal_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.CustomControls.Cal.View.prototype = {
    clientId: null,
    get_textBox: function SpottedScript_CustomControls_Cal_View$get_textBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_inner');
    }
}
SpottedScript.CustomControls.Cal.Controller.registerClass('SpottedScript.CustomControls.Cal.Controller');
SpottedScript.CustomControls.Cal.View.registerClass('SpottedScript.CustomControls.Cal.View');
