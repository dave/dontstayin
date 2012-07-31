//! EventUserControl.debug.js
//

(function($) {

Type.registerNamespace('Js.Pages.Events.EventUserControl');

////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Events.EventUserControl.View

Js.Pages.Events.EventUserControl.View = function Js_Pages_Events_EventUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_GenericContainerPage$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$2" type="jQueryObject">
    /// </field>
    Js.Pages.Events.EventUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Pages.Events.EventUserControl.View.prototype = {
    clientId: null,
    
    get_genericContainerPage: function Js_Pages_Events_EventUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$2 == null) {
            this._GenericContainerPage$2 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$2;
    },
    
    _GenericContainerPage$2: null,
    
    get_genericContainerPageJ: function Js_Pages_Events_EventUserControl_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$2 == null) {
            this._GenericContainerPageJ$2 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$2;
    },
    
    _GenericContainerPageJ$2: null
}


Js.Pages.Events.EventUserControl.View.registerClass('Js.Pages.Events.EventUserControl.View', Js.DsiUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
