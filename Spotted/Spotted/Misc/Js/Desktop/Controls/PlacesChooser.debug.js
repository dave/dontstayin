//! PlacesChooser.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.PlacesChooser');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PlacesChooser.Controller

Js.Controls.PlacesChooser.Controller = function Js_Controls_PlacesChooser_Controller(view) {
    /// <param name="view" type="Js.Controls.PlacesChooser.View">
    /// </param>
    /// <field name="view" type="Js.Controls.PlacesChooser.View">
    /// </field>
    /// <field name="_markers" type="Array" elementType="GMarker">
    /// </field>
    this.view = view;
    view.get_uiAddRadiusButtonJ().click(ss.Delegate.create(this, this._onAddNearbyClick));
    this._addMapMarkers();
    GEvent.addListener(this.view.get_uiMap().gmap2, 'moveend', ss.Delegate.create(this, this._addMapMarkers));
    GEvent.addListener(this.view.get_uiMap().gmap2, 'zoomend', ss.Delegate.create(this, this._addMapMarkers));
}
Js.Controls.PlacesChooser.Controller.prototype = {
    view: null,
    
    _onAddNearbyClick: function Js_Controls_PlacesChooser_Controller$_onAddNearbyClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        try {
            Js.Controls.PlacesChooser.Service.getSurroundingPlaces(parseInt(this.view.get_uiRadiusPlaceAutoComplete().get_value()), parseInt(this.view.get_uiNumberOfSurroundingTownsDropDown().value), ss.Delegate.create(this, function(result, context, name) {
                for (var i = 0; i < result.length; i++) {
                    this.view.get_uiPlacesMultiSelector().addItem(result[i].name, result[i].k.toString());
                }
                this.view.get_uiRadiusPlaceAutoComplete().clear();
            }), Js.Library.Trace.webServiceFailure, null, 5000);
        }
        catch ($e1) {
        }
        e.preventDefault();
    },
    
    _addMapMarkers: function Js_Controls_PlacesChooser_Controller$_addMapMarkers() {
        var bounds = this.view.get_uiMap().gmap2.getBounds();
        Js.Controls.PlacesChooser.Service.getPlaces(bounds.getNorthEast().lat(), bounds.getSouthWest().lat(), bounds.getNorthEast().lng(), bounds.getSouthWest().lng(), 20, ss.Delegate.create(this, this._addMapMarkersCallback), Js.Library.Trace.webServiceFailure, null, 3000);
    },
    
    _markers: null,
    
    _addMapMarkersCallback: function Js_Controls_PlacesChooser_Controller$_addMapMarkersCallback(result, userContext, methodName) {
        /// <param name="result" type="Array" elementType="PlaceStub">
        /// </param>
        /// <param name="userContext" type="Object">
        /// </param>
        /// <param name="methodName" type="String">
        /// </param>
        if (this._markers != null) {
            for (var i = 0; i < this._markers.length; i++) {
                GEvent.clearListeners(this._markers[i], 'click');
                GEvent.clearListeners(this._markers[i], 'onmouseover');
                GEvent.clearListeners(this._markers[i], 'onmouseout');
            }
        }
        this.view.get_uiMap().gmap2.clearOverlays();
        this._markers = [];
        for (var i = 0; i < result.length; i++) {
            var placeStub = result[i];
            this._createMarker(placeStub.lat, placeStub.lng, placeStub.name, placeStub.k);
        }
    },
    
    _createMarker: function Js_Controls_PlacesChooser_Controller$_createMarker(lat, lng, name, k) {
        /// <param name="lat" type="Number">
        /// </param>
        /// <param name="lng" type="Number">
        /// </param>
        /// <param name="name" type="String">
        /// </param>
        /// <param name="k" type="Number" integer="true">
        /// </param>
        var latLng = new GLatLng(lat, lng, true);
        var marker = new GMarker(latLng);
        marker.k = k;;
        marker.name = name;;
        GEvent.addListener(marker, 'click', ss.Delegate.create(this, function() {
            this.view.get_uiPlacesMultiSelector().addItem(name, k.toString());
        }));
        GEvent.addListener(marker, 'mouseover', function() {
            stt(name);;
        });
        GEvent.addListener(marker, 'mouseout', function() {
            try {
                htm();;
            }
            catch (e) {
            }
        });
        this.view.get_uiMap().gmap2.addOverlay(marker);
        this._markers[this._markers.length] = marker;
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PlacesChooser.Service

Js.Controls.PlacesChooser.Service = function Js_Controls_PlacesChooser_Service() {
}
Js.Controls.PlacesChooser.Service.getPlaces = function Js_Controls_PlacesChooser_Service$getPlaces(north, south, east, west, maximumNumber, success, failure, userContext, timeout) {
    /// <param name="north" type="Number">
    /// </param>
    /// <param name="south" type="Number">
    /// </param>
    /// <param name="east" type="Number">
    /// </param>
    /// <param name="west" type="Number">
    /// </param>
    /// <param name="maximumNumber" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['north'] = north;
    p['south'] = south;
    p['east'] = east;
    p['west'] = west;
    p['maximumNumber'] = maximumNumber;
    var o = Js.Library.WebServiceHelper.options('GetPlaces', '/WebServices/Controls/PlacesChooser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetPlaces');
    };
    $.ajax(o);
}
Js.Controls.PlacesChooser.Service.getSurroundingPlaces = function Js_Controls_PlacesChooser_Service$getSurroundingPlaces(centredOnPlaceK, numberOfPlacesToGet, success, failure, userContext, timeout) {
    /// <param name="centredOnPlaceK" type="Number" integer="true">
    /// </param>
    /// <param name="numberOfPlacesToGet" type="Number" integer="true">
    /// </param>
    /// <param name="success" type="Function">
    /// </param>
    /// <param name="failure" type="Js.Library.Function">
    /// </param>
    /// <param name="userContext" type="Object">
    /// </param>
    /// <param name="timeout" type="Number" integer="true">
    /// </param>
    var p = {};
    p['centredOnPlaceK'] = centredOnPlaceK;
    p['numberOfPlacesToGet'] = numberOfPlacesToGet;
    var o = Js.Library.WebServiceHelper.options('GetSurroundingPlaces', '/WebServices/Controls/PlacesChooser/Service.asmx', p, failure, userContext, timeout);
    o.success = function(data, textStatus, request) {
        success((data)['d'], userContext, 'GetSurroundingPlaces');
    };
    $.ajax(o);
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PlacesChooser.View

Js.Controls.PlacesChooser.View = function Js_Controls_PlacesChooser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_uiPlacesRadius" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPlacesRadiusJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiNumberOfSurroundingTownsDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiNumberOfSurroundingTownsDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_uiAddRadiusButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiAddRadiusButtonJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.PlacesChooser.View.prototype = {
    clientId: null,
    
    get_uiPlacesMultiSelector: function Js_Controls_PlacesChooser_View$get_uiPlacesMultiSelector() {
        /// <value type="Js.ClientControls.MultiSelectorBehaviour"></value>
        return eval(this.clientId + '_uiPlacesMultiSelectorBehaviour');
    },
    
    get_uiMap: function Js_Controls_PlacesChooser_View$get_uiMap() {
        /// <value type="Js.Controls.MapControl.Controller"></value>
        return eval(this.clientId + '_uiMapController');
    },
    
    get_uiPlacesRadius: function Js_Controls_PlacesChooser_View$get_uiPlacesRadius() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPlacesRadius == null) {
            this._uiPlacesRadius = document.getElementById(this.clientId + '_uiPlacesRadius');
        }
        return this._uiPlacesRadius;
    },
    
    _uiPlacesRadius: null,
    
    get_uiPlacesRadiusJ: function Js_Controls_PlacesChooser_View$get_uiPlacesRadiusJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPlacesRadiusJ == null) {
            this._uiPlacesRadiusJ = $('#' + this.clientId + '_uiPlacesRadius');
        }
        return this._uiPlacesRadiusJ;
    },
    
    _uiPlacesRadiusJ: null,
    
    get_uiNumberOfSurroundingTownsDropDown: function Js_Controls_PlacesChooser_View$get_uiNumberOfSurroundingTownsDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiNumberOfSurroundingTownsDropDown == null) {
            this._uiNumberOfSurroundingTownsDropDown = document.getElementById(this.clientId + '_uiNumberOfSurroundingTownsDropDown');
        }
        return this._uiNumberOfSurroundingTownsDropDown;
    },
    
    _uiNumberOfSurroundingTownsDropDown: null,
    
    get_uiNumberOfSurroundingTownsDropDownJ: function Js_Controls_PlacesChooser_View$get_uiNumberOfSurroundingTownsDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiNumberOfSurroundingTownsDropDownJ == null) {
            this._uiNumberOfSurroundingTownsDropDownJ = $('#' + this.clientId + '_uiNumberOfSurroundingTownsDropDown');
        }
        return this._uiNumberOfSurroundingTownsDropDownJ;
    },
    
    _uiNumberOfSurroundingTownsDropDownJ: null,
    
    get_uiRadiusPlaceAutoComplete: function Js_Controls_PlacesChooser_View$get_uiRadiusPlaceAutoComplete() {
        /// <value type="Js.ClientControls.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiRadiusPlaceAutoCompleteBehaviour');
    },
    
    get_uiAddRadiusButton: function Js_Controls_PlacesChooser_View$get_uiAddRadiusButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiAddRadiusButton == null) {
            this._uiAddRadiusButton = document.getElementById(this.clientId + '_uiAddRadiusButton');
        }
        return this._uiAddRadiusButton;
    },
    
    _uiAddRadiusButton: null,
    
    get_uiAddRadiusButtonJ: function Js_Controls_PlacesChooser_View$get_uiAddRadiusButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiAddRadiusButtonJ == null) {
            this._uiAddRadiusButtonJ = $('#' + this.clientId + '_uiAddRadiusButton');
        }
        return this._uiAddRadiusButtonJ;
    },
    
    _uiAddRadiusButtonJ: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.PlacesChooser.PlaceStub

Js.Controls.PlacesChooser.PlaceStub = function Js_Controls_PlacesChooser_PlaceStub() {
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="name" type="String">
    /// </field>
    /// <field name="lat" type="Number">
    /// </field>
    /// <field name="lng" type="Number">
    /// </field>
}
Js.Controls.PlacesChooser.PlaceStub.prototype = {
    k: 0,
    name: null,
    lat: 0,
    lng: 0
}


Js.Controls.PlacesChooser.Controller.registerClass('Js.Controls.PlacesChooser.Controller');
Js.Controls.PlacesChooser.Service.registerClass('Js.Controls.PlacesChooser.Service');
Js.Controls.PlacesChooser.View.registerClass('Js.Controls.PlacesChooser.View');
Js.Controls.PlacesChooser.PlaceStub.registerClass('Js.Controls.PlacesChooser.PlaceStub');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
