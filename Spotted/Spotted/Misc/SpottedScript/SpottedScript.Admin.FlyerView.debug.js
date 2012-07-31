Type.registerNamespace('SpottedScript.Admin.FlyerView');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.FlyerView.View
SpottedScript.Admin.FlyerView.View = function SpottedScript_Admin_FlyerView_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.FlyerView.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.FlyerView.View.prototype = {
    clientId: null,
    get_uiBasicInfo: function SpottedScript_Admin_FlyerView_View$get_uiBasicInfo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBasicInfo');
    },
    get_uiValidationErrors: function SpottedScript_Admin_FlyerView_View$get_uiValidationErrors() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiValidationErrors');
    },
    get_uiSendButtons: function SpottedScript_Admin_FlyerView_View$get_uiSendButtons() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSendButtons');
    },
    get_uiSend: function SpottedScript_Admin_FlyerView_View$get_uiSend() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSend');
    },
    get_uiStop: function SpottedScript_Admin_FlyerView_View$get_uiStop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiStop');
    },
    get_uiRestart: function SpottedScript_Admin_FlyerView_View$get_uiRestart() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiRestart');
    },
    get_uiSentSuccessfully: function SpottedScript_Admin_FlyerView_View$get_uiSentSuccessfully() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSentSuccessfully');
    },
    get_genericContainerPage: function SpottedScript_Admin_FlyerView_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.FlyerView.View.registerClass('SpottedScript.Admin.FlyerView.View', SpottedScript.AdminUserControl.View);
