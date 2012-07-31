Type.registerNamespace('SpottedScript.Pages.PlacesIVisit');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.PlacesIVisit.Controller
SpottedScript.Pages.PlacesIVisit.Controller = function SpottedScript_Pages_PlacesIVisit_Controller(view) {
    /// <param name="view" type="SpottedScript.Pages.PlacesIVisit.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Pages.PlacesIVisit.View">
    /// </field>
    this._view = view;
    this._view.get_uiPlacesChooser().view.get_uiPlacesMultiSelector().itemAdded = Function.createDelegate(this, this._setSaveButtonDisabledState);
    this._view.get_uiPlacesChooser().view.get_uiPlacesMultiSelector().itemRemoved = Function.createDelegate(this, this._setSaveButtonDisabledState);
}
SpottedScript.Pages.PlacesIVisit.Controller.prototype = {
    _view: null,
    _setSaveButtonDisabledState: function SpottedScript_Pages_PlacesIVisit_Controller$_setSaveButtonDisabledState(a, b) {
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
// SpottedScript.Pages.PlacesIVisit.View
SpottedScript.Pages.PlacesIVisit.View = function SpottedScript_Pages_PlacesIVisit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.PlacesIVisit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.PlacesIVisit.View.prototype = {
    clientId: null,
    get_h11: function SpottedScript_Pages_PlacesIVisit_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_uiPlacesChooser: function SpottedScript_Pages_PlacesIVisit_View$get_uiPlacesChooser() {
        /// <value type="SpottedScript.Controls.PlacesChooser.Controller"></value>
        return eval(this.clientId + '_uiPlacesChooserController');
    },
    get_uiSaveButton: function SpottedScript_Pages_PlacesIVisit_View$get_uiSaveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSaveButton');
    },
    get_genericContainerPage: function SpottedScript_Pages_PlacesIVisit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.PlacesIVisit.Controller.registerClass('SpottedScript.Pages.PlacesIVisit.Controller');
SpottedScript.Pages.PlacesIVisit.View.registerClass('SpottedScript.Pages.PlacesIVisit.View', SpottedScript.DsiUserControl.View);
