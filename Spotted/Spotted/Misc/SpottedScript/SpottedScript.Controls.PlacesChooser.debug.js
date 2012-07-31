Type.registerNamespace('SpottedScript.Controls.PlacesChooser');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PlacesChooser.Controller
SpottedScript.Controls.PlacesChooser.Controller = function SpottedScript_Controls_PlacesChooser_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.PlacesChooser.View">
    /// </param>
    /// <field name="view" type="SpottedScript.Controls.PlacesChooser.View">
    /// </field>
    /// <field name="_markers" type="Array" elementType="GMarker">
    /// </field>
    this.view = view;
    $addHandler(view.get_uiAddRadiusButton(), 'click', Function.createDelegate(this, this._onAddNearbyClick));
    this._addMapMarkers();
    GEvent.addListener(this.view.get_uiMap().gmap2, 'moveend', Function.createDelegate(this, this._addMapMarkers));
    GEvent.addListener(this.view.get_uiMap().gmap2, 'zoomend', Function.createDelegate(this, this._addMapMarkers));
}
SpottedScript.Controls.PlacesChooser.Controller.prototype = {
    view: null,
    _onAddNearbyClick: function SpottedScript_Controls_PlacesChooser_Controller$_onAddNearbyClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        try {
            Spotted.WebServices.Controls.PlacesChooser.Service.getSurroundingPlaces(Number.parseInvariant(this.view.get_uiRadiusPlaceAutoComplete().get_value()), Number.parseInvariant(this.view.get_uiNumberOfSurroundingTownsDropDown().value), Function.createDelegate(this, function(result, context, name) {
                for (var i = 0; i < result.length; i++) {
                    this.view.get_uiPlacesMultiSelector().addItem(result[i].name, result[i].k.toString());
                }
                this.view.get_uiRadiusPlaceAutoComplete().clear();
            }), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, 5000);
        }
        catch ($e1) {
        }
        e.preventDefault();
    },
    _addMapMarkers: function SpottedScript_Controls_PlacesChooser_Controller$_addMapMarkers() {
        var bounds = this.view.get_uiMap().gmap2.getBounds();
        Spotted.WebServices.Controls.PlacesChooser.Service.getPlaces(bounds.getNorthEast().lat(), bounds.getSouthWest().lat(), bounds.getNorthEast().lng(), bounds.getSouthWest().lng(), 20, Function.createDelegate(this, this._addMapMarkersCallback), Function.createDelegate(null, Utils.Trace.webServiceFailure), null, 3000);
    },
    _markers: null,
    _addMapMarkersCallback: function SpottedScript_Controls_PlacesChooser_Controller$_addMapMarkersCallback(result, userContext, methodName) {
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
    _createMarker: function SpottedScript_Controls_PlacesChooser_Controller$_createMarker(lat, lng, name, k) {
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
        GEvent.addListener(marker, 'click', Function.createDelegate(this, function() {
            this.view.get_uiPlacesMultiSelector().addItem(name, k.toString());
        }));
        GEvent.addListener(marker, 'mouseover', Function.createDelegate(this, function() {
            stt(name);;
        }));
        GEvent.addListener(marker, 'mouseout', Function.createDelegate(this, function() {
            try {
                htm();;
            }
            catch (e) {
            }
        }));
        this.view.get_uiMap().gmap2.addOverlay(marker);
        this._markers[this._markers.length] = marker;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PlacesChooser.PlaceStub
SpottedScript.Controls.PlacesChooser.PlaceStub = function SpottedScript_Controls_PlacesChooser_PlaceStub() {
    /// <field name="k" type="Number" integer="true">
    /// </field>
    /// <field name="name" type="String">
    /// </field>
    /// <field name="lat" type="Number">
    /// </field>
    /// <field name="lng" type="Number">
    /// </field>
}
SpottedScript.Controls.PlacesChooser.PlaceStub.prototype = {
    k: 0,
    name: null,
    lat: 0,
    lng: 0
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.PlacesChooser.View
SpottedScript.Controls.PlacesChooser.View = function SpottedScript_Controls_PlacesChooser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.PlacesChooser.View.prototype = {
    clientId: null,
    get_uiPlacesMultiSelector: function SpottedScript_Controls_PlacesChooser_View$get_uiPlacesMultiSelector() {
        /// <value type="ScriptSharpLibrary.MultiSelectorBehaviour"></value>
        return eval(this.clientId + '_uiPlacesMultiSelectorBehaviour');
    },
    get_uiMap: function SpottedScript_Controls_PlacesChooser_View$get_uiMap() {
        /// <value type="SpottedScript.Controls.MapControl.Controller"></value>
        return eval(this.clientId + '_uiMapController');
    },
    get_uiPlacesRadius: function SpottedScript_Controls_PlacesChooser_View$get_uiPlacesRadius() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPlacesRadius');
    },
    get_uiNumberOfSurroundingTownsDropDown: function SpottedScript_Controls_PlacesChooser_View$get_uiNumberOfSurroundingTownsDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNumberOfSurroundingTownsDropDown');
    },
    get_uiRadiusPlaceAutoComplete: function SpottedScript_Controls_PlacesChooser_View$get_uiRadiusPlaceAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiRadiusPlaceAutoCompleteBehaviour');
    },
    get_uiAddRadiusButton: function SpottedScript_Controls_PlacesChooser_View$get_uiAddRadiusButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddRadiusButton');
    }
}
SpottedScript.Controls.PlacesChooser.Controller.registerClass('SpottedScript.Controls.PlacesChooser.Controller');
SpottedScript.Controls.PlacesChooser.PlaceStub.registerClass('SpottedScript.Controls.PlacesChooser.PlaceStub');
SpottedScript.Controls.PlacesChooser.View.registerClass('SpottedScript.Controls.PlacesChooser.View');
