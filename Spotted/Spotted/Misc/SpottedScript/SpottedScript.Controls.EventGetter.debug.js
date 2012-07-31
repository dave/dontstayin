Type.registerNamespace('SpottedScript.Controls.EventGetter');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventGetter.Controller
SpottedScript.Controls.EventGetter.Controller = function SpottedScript_Controls_EventGetter_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.EventGetter.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.EventGetter.View">
    /// </field>
    /// <field name="eventInfo" type="SpottedScript.Controls.EventCreator.EventInfo">
    /// </field>
    /// <field name="_emptyHtml" type="String">
    /// </field>
    this._view = view;
    $addHandler(view.get_uiEventDisplayDiv(), 'mousedown', Function.createDelegate(this, this._onMouseDown));
    $addHandler(view.get_uiEventDisplayDiv(), 'keydown', Function.createDelegate(this, this._onKeyDown));
    this._emptyHtml = view.get_uiEventDisplayDiv().innerHTML;
}
SpottedScript.Controls.EventGetter.Controller.prototype = {
    _view: null,
    eventInfo: null,
    _emptyHtml: null,
    _onKeyDown: function SpottedScript_Controls_EventGetter_Controller$_onKeyDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if (e.keyCode === Sys.UI.Key.backspace || e.keyCode === Sys.UI.Key.del) {
            this.eventInfo = null;
            this._view.get_uiEventDisplayDiv().innerHTML = '';
        }
    },
    _onMouseDown: function SpottedScript_Controls_EventGetter_Controller$_onMouseDown(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        SpottedScript.Controls.EventCreator.Controller.instance.showPopup((this.eventInfo == null) ? null : this.eventInfo.date, (this.eventInfo == null) ? null : this.eventInfo.venueInfo, (this.eventInfo == null) ? null : this.eventInfo.name, Function.createDelegate(this, this._onEventCreated));
    },
    _onEventCreated: function SpottedScript_Controls_EventGetter_Controller$_onEventCreated(eventInfo) {
        /// <param name="eventInfo" type="SpottedScript.Controls.EventCreator.EventInfo">
        /// </param>
        this.eventInfo = eventInfo;
        if (eventInfo != null) {
            this._view.get_uiEventDisplayDiv().innerHTML = ScriptSharpLibrary.Suggestion.getPicTitleDetailTemplateHtml(eventInfo.picPath, eventInfo.name, SpottedScript.Controls.VenueCreator.VenueInfo.nameWithPlace(eventInfo.venueInfo));
        }
        else {
            this._view.get_uiEventDisplayDiv().innerHTML = this._emptyHtml;
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.EventGetter.View
SpottedScript.Controls.EventGetter.View = function SpottedScript_Controls_EventGetter_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.EventGetter.View.prototype = {
    clientId: null,
    get_uiEventDisplayDiv: function SpottedScript_Controls_EventGetter_View$get_uiEventDisplayDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEventDisplayDiv');
    }
}
SpottedScript.Controls.EventGetter.Controller.registerClass('SpottedScript.Controls.EventGetter.Controller');
SpottedScript.Controls.EventGetter.View.registerClass('SpottedScript.Controls.EventGetter.View');
