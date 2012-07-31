Type.registerNamespace('SpottedScript.Controls.MapControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MapControl.Controller
SpottedScript.Controls.MapControl.Controller = function SpottedScript_Controls_MapControl_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.MapControl.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.MapControl.View">
    /// </field>
    /// <field name="gmap2" type="GMap2">
    /// </field>
    /// <field name="_markers" type="Object">
    /// </field>
    this._markers = {};
    this._view = view;
    this.gmap2 = new GMap2(view.get_map());
    this.gmap2.addControl(new GLargeMapControl());
    var north = parseFloat(view.get_uiNorth().value);
    var south = parseFloat(view.get_uiSouth().value);
    var west = parseFloat(view.get_uiWest().value);
    var east = parseFloat(view.get_uiEast().value);
    var deltaLat = north - south;
    var delatLng = east - west;
    var ne = new GLatLng(north + deltaLat / 20, east + delatLng / 20, true);
    var sw = new GLatLng(south - deltaLat / 20, west - delatLng / 20, true);
    var bounds = new GLatLngBounds(sw, ne);
    bounds.extend(sw);
    bounds.extend(ne);
    var zoom = this.gmap2.getBoundsZoomLevel(bounds);
    if (zoom > 12) {
        zoom = 12;
    }
    var centre = new GLatLng((north + south) / 2, (east + west) / 2, true);
    this.gmap2.enableScrollWheelZoom();
    this.gmap2.setCenter(centre, zoom);
    this.gmap2.enableContinuousZoom();
    this.gmap2.enableDoubleClickZoom();
    this.gmap2.enableGoogleBar();
    this.gmap2.checkResize();
}
SpottedScript.Controls.MapControl.Controller.prototype = {
    _view: null,
    gmap2: null,
    addMapMarkers: function SpottedScript_Controls_MapControl_Controller$addMapMarkers(items) {
        /// <param name="items" type="Array" elementType="MapItem">
        /// </param>
        if (items != null) {
            for (var i = 0; i < items.length; i++) {
                var marker = this._getMarker(items[i].lat, items[i].lon);
                var div = document.createElement('LI');
                div.innerHTML = items[i].hoverText;
                marker.hover.appendChild(div);
            }
        }
    },
    clearMarkers: function SpottedScript_Controls_MapControl_Controller$clearMarkers() {
        var $dict1 = this._markers;
        for (var $key2 in $dict1) {
            var de = { key: $key2, value: $dict1[$key2] };
            var marker = de.value;
            marker.dispose();
        }
        this.gmap2.clearOverlays();
        this._markers = {};
    },
    _getMarker: function SpottedScript_Controls_MapControl_Controller$_getMarker(lat, lng) {
        /// <param name="lat" type="Number">
        /// </param>
        /// <param name="lng" type="Number">
        /// </param>
        /// <returns type="SpottedScript.Controls.MapControl._marker"></returns>
        var key = lat.toString() + ',' + lng.toString();
        if (this._markers[key] == null) {
            var gMarker = new GMarker(new GLatLng(lat, lng, true));
            this.gmap2.addOverlay(gMarker);
            var marker = new SpottedScript.Controls.MapControl._marker(gMarker);
            this._markers[key] = marker;
            return marker;
        }
        else {
            return this._markers[key];
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MapControl._marker
SpottedScript.Controls.MapControl._marker = function SpottedScript_Controls_MapControl__marker(gMarker) {
    /// <param name="gMarker" type="GMarker">
    /// </param>
    /// <field name="_gMarker" type="GMarker">
    /// </field>
    /// <field name="hover" type="Object" domElement="true">
    /// </field>
    this._gMarker = gMarker;
    this.hover = document.createElement('UL');
    GEvent.addListener(gMarker, 'mouseout', Function.createDelegate(this, function() {
        try {
            htm();;
        }
        catch (e) {
        }
    }));
    GEvent.addListener(gMarker, 'click', Function.createDelegate(this, function() {
    }));
    GEvent.addListener(gMarker, 'mouseover', Function.createDelegate(this, function() {
        stt(this.hover.innerHTML);;
    }));
    GEvent.addListener(gMarker, 'mouseout', Function.createDelegate(this, function() {
        try {
            htm();;
        }
        catch (e) {
        }
    }));
}
SpottedScript.Controls.MapControl._marker.prototype = {
    _gMarker: null,
    hover: null,
    dispose: function SpottedScript_Controls_MapControl__marker$dispose() {
        GEvent.clearListeners(this._gMarker, 'click');
        GEvent.clearListeners(this._gMarker, 'onmouseover');
        GEvent.clearListeners(this._gMarker, 'onmouseout');
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.MapControl.View
SpottedScript.Controls.MapControl.View = function SpottedScript_Controls_MapControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.MapControl.View.prototype = {
    clientId: null,
    get_map: function SpottedScript_Controls_MapControl_View$get_map() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_map');
    },
    get_uiNorth: function SpottedScript_Controls_MapControl_View$get_uiNorth() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNorth');
    },
    get_uiSouth: function SpottedScript_Controls_MapControl_View$get_uiSouth() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSouth');
    },
    get_uiEast: function SpottedScript_Controls_MapControl_View$get_uiEast() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEast');
    },
    get_uiWest: function SpottedScript_Controls_MapControl_View$get_uiWest() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiWest');
    }
}
SpottedScript.Controls.MapControl.Controller.registerClass('SpottedScript.Controls.MapControl.Controller');
SpottedScript.Controls.MapControl._marker.registerClass('SpottedScript.Controls.MapControl._marker');
SpottedScript.Controls.MapControl.View.registerClass('SpottedScript.Controls.MapControl.View');
