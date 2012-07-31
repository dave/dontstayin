Type.registerNamespace('SpottedScript.Controls.PagedData.Templates.Events.Header');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PagedData.Templates.Events.Header.Controller
SpottedScript.Controls.PagedData.Templates.Events.Header.Controller = function SpottedScript_Controls_PagedData_Templates_Events_Header_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.PagedData.Templates.Events.Header.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.PagedData.Templates.Events.Header.View">
    /// </field>
    /// <field name="_parametersUpdated" type="Sys.EventHandler">
    /// </field>
    this._view = view;
    $addHandler(view.get_uiShowFuture(), 'click', Function.createDelegate(this, this._onParametersUpdated));
    $addHandler(view.get_uiShowPast(), 'click', Function.createDelegate(this, this._onParametersUpdated));
}
SpottedScript.Controls.PagedData.Templates.Events.Header.Controller.prototype = {
    _view: null,
    _onParametersUpdated: function SpottedScript_Controls_PagedData_Templates_Events_Header_Controller$_onParametersUpdated(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (this._parametersUpdated != null) {
            this._parametersUpdated(this, null);
        }
    },
    get_parameters: function SpottedScript_Controls_PagedData_Templates_Events_Header_Controller$get_parameters() {
        /// <value type="Object"></value>
        var parameters = {};
        parameters['showFuture'] = this._view.get_uiShowFuture().checked;
        parameters['showPast'] = this._view.get_uiShowPast().checked;
        parameters['orderDesc'] = (this._view.get_uiShowPast().checked) ? true : false;
        parameters['musicTypeK'] = this._view.get_uiMusicType().value;
        return parameters;
    },
    _parametersUpdated: null,
    get_parametersUpdated: function SpottedScript_Controls_PagedData_Templates_Events_Header_Controller$get_parametersUpdated() {
        /// <value type="Sys.EventHandler"></value>
        return this._parametersUpdated;
    },
    set_parametersUpdated: function SpottedScript_Controls_PagedData_Templates_Events_Header_Controller$set_parametersUpdated(value) {
        /// <value type="Sys.EventHandler"></value>
        this._parametersUpdated = value;
        return value;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PagedData.Templates.Events.Header.View
SpottedScript.Controls.PagedData.Templates.Events.Header.View = function SpottedScript_Controls_PagedData_Templates_Events_Header_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.PagedData.Templates.Events.Header.View.prototype = {
    clientId: null,
    get_uiShowPast: function SpottedScript_Controls_PagedData_Templates_Events_Header_View$get_uiShowPast() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiShowPast');
    },
    get_uiShowFuture: function SpottedScript_Controls_PagedData_Templates_Events_Header_View$get_uiShowFuture() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiShowFuture');
    },
    get_uiMusicType: function SpottedScript_Controls_PagedData_Templates_Events_Header_View$get_uiMusicType() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMusicType');
    }
}
SpottedScript.Controls.PagedData.Templates.Events.Header.Controller.registerClass('SpottedScript.Controls.PagedData.Templates.Events.Header.Controller', null, SpottedScript.Controls.PagedData._iParameterSource);
SpottedScript.Controls.PagedData.Templates.Events.Header.View.registerClass('SpottedScript.Controls.PagedData.Templates.Events.Header.View');
