//! PlacesIVisit.debug.js
//

(function($) {

Type.registerNamespace('Js.Pages.PlacesIVisit');

////////////////////////////////////////////////////////////////////////////////
// Js.Pages.PlacesIVisit.Controller

Js.Pages.PlacesIVisit.Controller = function Js_Pages_PlacesIVisit_Controller(view) {
    /// <param name="view" type="Js.Pages.PlacesIVisit.View">
    /// </param>
    /// <field name="_view" type="Js.Pages.PlacesIVisit.View">
    /// </field>
    this._view = view;
    this._view.get_uiPlacesChooser().view.get_uiPlacesMultiSelector().itemAdded = ss.Delegate.create(this, this._setSaveButtonDisabledState);
    this._view.get_uiPlacesChooser().view.get_uiPlacesMultiSelector().itemRemoved = ss.Delegate.create(this, this._setSaveButtonDisabledState);
}
Js.Pages.PlacesIVisit.Controller.prototype = {
    _view: null,
    
    _setSaveButtonDisabledState: function Js_Pages_PlacesIVisit_Controller$_setSaveButtonDisabledState(a, b) {
        /// <param name="a" type="String">
        /// </param>
        /// <param name="b" type="String">
        /// </param>
        if (this._view.get_uiPlacesChooser().view.get_uiPlacesMultiSelector().getSelections().get_count() > 0) {
            this._view.get_uiSaveButton().disabled = false;
        }
        else {
            this._view.get_uiSaveButton().disabled = true;
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Pages.PlacesIVisit.View

Js.Pages.PlacesIVisit.View = function Js_Pages_PlacesIVisit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_H11$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_H11J$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiSaveButton$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiSaveButtonJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_GenericContainerPage$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$2" type="jQueryObject">
    /// </field>
    Js.Pages.PlacesIVisit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Pages.PlacesIVisit.View.prototype = {
    clientId: null,
    
    get_h11: function Js_Pages_PlacesIVisit_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        if (this._H11$2 == null) {
            this._H11$2 = document.getElementById(this.clientId + '_H11');
        }
        return this._H11$2;
    },
    
    _H11$2: null,
    
    get_h11J: function Js_Pages_PlacesIVisit_View$get_h11J() {
        /// <value type="jQueryObject"></value>
        if (this._H11J$2 == null) {
            this._H11J$2 = $('#' + this.clientId + '_H11');
        }
        return this._H11J$2;
    },
    
    _H11J$2: null,
    
    get_uiPlacesChooser: function Js_Pages_PlacesIVisit_View$get_uiPlacesChooser() {
        /// <value type="Js.Controls.PlacesChooser.Controller"></value>
        return eval(this.clientId + '_uiPlacesChooserController');
    },
    
    get_uiSaveButton: function Js_Pages_PlacesIVisit_View$get_uiSaveButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiSaveButton$2 == null) {
            this._uiSaveButton$2 = document.getElementById(this.clientId + '_uiSaveButton');
        }
        return this._uiSaveButton$2;
    },
    
    _uiSaveButton$2: null,
    
    get_uiSaveButtonJ: function Js_Pages_PlacesIVisit_View$get_uiSaveButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiSaveButtonJ$2 == null) {
            this._uiSaveButtonJ$2 = $('#' + this.clientId + '_uiSaveButton');
        }
        return this._uiSaveButtonJ$2;
    },
    
    _uiSaveButtonJ$2: null,
    
    get_genericContainerPage: function Js_Pages_PlacesIVisit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$2 == null) {
            this._GenericContainerPage$2 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$2;
    },
    
    _GenericContainerPage$2: null,
    
    get_genericContainerPageJ: function Js_Pages_PlacesIVisit_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$2 == null) {
            this._GenericContainerPageJ$2 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$2;
    },
    
    _GenericContainerPageJ$2: null
}


Js.Pages.PlacesIVisit.Controller.registerClass('Js.Pages.PlacesIVisit.Controller');
Js.Pages.PlacesIVisit.View.registerClass('Js.Pages.PlacesIVisit.View', Js.DsiUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
