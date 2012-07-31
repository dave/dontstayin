//! MapControl.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.MapControl');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MapControl.Controller

Js.Controls.MapControl.Controller = function Js_Controls_MapControl_Controller(view) {
    /// <param name="view" type="Js.Controls.MapControl.View">
    /// </param>
    /// <field name="_view" type="Js.Controls.MapControl.View">
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
Js.Controls.MapControl.Controller.prototype = {
    _view: null,
    gmap2: null,
    
    addMapMarkers: function Js_Controls_MapControl_Controller$addMapMarkers(items) {
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
    
    clearMarkers: function Js_Controls_MapControl_Controller$clearMarkers() {
        var $enum1 = ss.IEnumerator.getEnumerator(Object.keys(this._markers));
        while ($enum1.moveNext()) {
            var key = $enum1.current;
            var marker = this._markers[key];
            marker.dispose();
        }
        this.gmap2.clearOverlays();
        this._markers = {};
    },
    
    _getMarker: function Js_Controls_MapControl_Controller$_getMarker(lat, lng) {
        /// <param name="lat" type="Number">
        /// </param>
        /// <param name="lng" type="Number">
        /// </param>
        /// <returns type="Js.Controls.MapControl._marker"></returns>
        var key = lat.toString() + ',' + lng.toString();
        if (this._markers[key] == null) {
            var gMarker = new GMarker(new GLatLng(lat, lng, true));
            this.gmap2.addOverlay(gMarker);
            var marker = new Js.Controls.MapControl._marker(gMarker);
            this._markers[key] = marker;
            return marker;
        }
        else {
            return this._markers[key];
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MapControl.View

Js.Controls.MapControl.View = function Js_Controls_MapControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_map" type="Object" domElement="true">
    /// </field>
    /// <field name="_mapJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiNorth" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiNorthJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiSouth" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiSouthJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiEast" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiEastJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiWest" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiWestJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.MapControl.View.prototype = {
    clientId: null,
    
    get_map: function Js_Controls_MapControl_View$get_map() {
        /// <value type="Object" domElement="true"></value>
        if (this._map == null) {
            this._map = document.getElementById(this.clientId + '_map');
        }
        return this._map;
    },
    
    _map: null,
    
    get_mapJ: function Js_Controls_MapControl_View$get_mapJ() {
        /// <value type="jQueryObject"></value>
        if (this._mapJ == null) {
            this._mapJ = $('#' + this.clientId + '_map');
        }
        return this._mapJ;
    },
    
    _mapJ: null,
    
    get_uiNorth: function Js_Controls_MapControl_View$get_uiNorth() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiNorth == null) {
            this._uiNorth = document.getElementById(this.clientId + '_uiNorth');
        }
        return this._uiNorth;
    },
    
    _uiNorth: null,
    
    get_uiNorthJ: function Js_Controls_MapControl_View$get_uiNorthJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiNorthJ == null) {
            this._uiNorthJ = $('#' + this.clientId + '_uiNorth');
        }
        return this._uiNorthJ;
    },
    
    _uiNorthJ: null,
    
    get_uiSouth: function Js_Controls_MapControl_View$get_uiSouth() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiSouth == null) {
            this._uiSouth = document.getElementById(this.clientId + '_uiSouth');
        }
        return this._uiSouth;
    },
    
    _uiSouth: null,
    
    get_uiSouthJ: function Js_Controls_MapControl_View$get_uiSouthJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiSouthJ == null) {
            this._uiSouthJ = $('#' + this.clientId + '_uiSouth');
        }
        return this._uiSouthJ;
    },
    
    _uiSouthJ: null,
    
    get_uiEast: function Js_Controls_MapControl_View$get_uiEast() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiEast == null) {
            this._uiEast = document.getElementById(this.clientId + '_uiEast');
        }
        return this._uiEast;
    },
    
    _uiEast: null,
    
    get_uiEastJ: function Js_Controls_MapControl_View$get_uiEastJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiEastJ == null) {
            this._uiEastJ = $('#' + this.clientId + '_uiEast');
        }
        return this._uiEastJ;
    },
    
    _uiEastJ: null,
    
    get_uiWest: function Js_Controls_MapControl_View$get_uiWest() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiWest == null) {
            this._uiWest = document.getElementById(this.clientId + '_uiWest');
        }
        return this._uiWest;
    },
    
    _uiWest: null,
    
    get_uiWestJ: function Js_Controls_MapControl_View$get_uiWestJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiWestJ == null) {
            this._uiWestJ = $('#' + this.clientId + '_uiWest');
        }
        return this._uiWestJ;
    },
    
    _uiWestJ: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MapControl.MapItem

Js.Controls.MapControl.MapItem = function Js_Controls_MapControl_MapItem() {
    /// <field name="data" type="Object">
    /// </field>
    /// <field name="lat" type="Number">
    /// </field>
    /// <field name="lon" type="Number">
    /// </field>
    /// <field name="hoverText" type="String">
    /// </field>
}
Js.Controls.MapControl.MapItem.prototype = {
    data: null,
    lat: 0,
    lon: 0,
    hoverText: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.MapControl._marker

Js.Controls.MapControl._marker = function Js_Controls_MapControl__marker(gMarker) {
    /// <param name="gMarker" type="GMarker">
    /// </param>
    /// <field name="_gMarker" type="GMarker">
    /// </field>
    /// <field name="hover" type="Object" domElement="true">
    /// </field>
    this._gMarker = gMarker;
    this.hover = document.createElement('UL');
    GEvent.addListener(gMarker, 'mouseout', function() {
        try {
            htm();;
        }
        catch (e) {
        }
    });
    GEvent.addListener(gMarker, 'click', function() {
    });
    GEvent.addListener(gMarker, 'mouseover', ss.Delegate.create(this, function() {
        stt(this.hover.innerHTML);;
    }));
    GEvent.addListener(gMarker, 'mouseout', function() {
        try {
            htm();;
        }
        catch (e) {
        }
    });
}
Js.Controls.MapControl._marker.prototype = {
    _gMarker: null,
    hover: null,
    
    dispose: function Js_Controls_MapControl__marker$dispose() {
        GEvent.clearListeners(this._gMarker, 'click');
        GEvent.clearListeners(this._gMarker, 'onmouseover');
        GEvent.clearListeners(this._gMarker, 'onmouseout');
    }
}


Js.Controls.MapControl.Controller.registerClass('Js.Controls.MapControl.Controller');
Js.Controls.MapControl.View.registerClass('Js.Controls.MapControl.View');
Js.Controls.MapControl.MapItem.registerClass('Js.Controls.MapControl.MapItem');
Js.Controls.MapControl._marker.registerClass('Js.Controls.MapControl._marker');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
