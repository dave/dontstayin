Type.registerNamespace('SpottedScript.Controls.MapBrowser.Map');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MapBrowser.Map.Controller
SpottedScript.Controls.MapBrowser.Map.Controller = function SpottedScript_Controls_MapBrowser_Map_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.MapBrowser.Map.View">
    /// </param>
    /// <field name="view" type="SpottedScript.Controls.MapBrowser.Map.View">
    /// </field>
    /// <field name="_parametersUpdated" type="Sys.EventHandler">
    /// </field>
    this.view = view;
    GEvent.addListener(this.view.get_uiMapControl().gmap2, 'moveend', Function.createDelegate(this, function() {
        Utils.Trace.write(this.view.get_uiMapControl().gmap2.getBounds().getNorthEast().lat() + ',' + this.view.get_uiMapControl().gmap2.getBounds().getNorthEast().lng() + ',' + this.view.get_uiMapControl().gmap2.getBounds().getSouthWest().lat() + ',' + this.view.get_uiMapControl().gmap2.getBounds().getSouthWest().lng() + ': ZOOM:' + this.view.get_uiMapControl().gmap2.getBoundsZoomLevel(this.view.get_uiMapControl().gmap2.getBounds()));
        if (this._parametersUpdated != null) {
            this._parametersUpdated(this, null);
        }
    }));
}
SpottedScript.Controls.MapBrowser.Map.Controller.prototype = {
    view: null,
    get_parameters: function SpottedScript_Controls_MapBrowser_Map_Controller$get_parameters() {
        /// <value type="Object"></value>
        var parameters = {};
        var bounds = this.view.get_uiMapControl().gmap2.getBounds();
        parameters['north'] = bounds.getNorthEast().lat();
        parameters['south'] = bounds.getSouthWest().lat();
        parameters['east'] = bounds.getNorthEast().lng();
        parameters['west'] = bounds.getSouthWest().lng();
        return parameters;
    },
    _parametersUpdated: null,
    get_parametersUpdated: function SpottedScript_Controls_MapBrowser_Map_Controller$get_parametersUpdated() {
        /// <value type="Sys.EventHandler"></value>
        return this._parametersUpdated;
    },
    set_parametersUpdated: function SpottedScript_Controls_MapBrowser_Map_Controller$set_parametersUpdated(value) {
        /// <value type="Sys.EventHandler"></value>
        this._parametersUpdated = value;
        return value;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MapBrowser.Map.View
SpottedScript.Controls.MapBrowser.Map.View = function SpottedScript_Controls_MapBrowser_Map_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.MapBrowser.Map.View.prototype = {
    clientId: null,
    get_uiMapControl: function SpottedScript_Controls_MapBrowser_Map_View$get_uiMapControl() {
        /// <value type="SpottedScript.Controls.MapControl.Controller"></value>
        return eval(this.clientId + '_uiMapControlController');
    }
}
SpottedScript.Controls.MapBrowser.Map.Controller.registerClass('SpottedScript.Controls.MapBrowser.Map.Controller', null, SpottedScript.Controls.PagedData._iParameterSource);
SpottedScript.Controls.MapBrowser.Map.View.registerClass('SpottedScript.Controls.MapBrowser.Map.View');
