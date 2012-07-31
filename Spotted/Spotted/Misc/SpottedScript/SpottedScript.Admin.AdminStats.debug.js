Type.registerNamespace('SpottedScript.Admin.AdminStats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.AdminStats.View
SpottedScript.Admin.AdminStats.View = function SpottedScript_Admin_AdminStats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.AdminStats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.AdminStats.View.prototype = {
    clientId: null,
    get_h1: function SpottedScript_Admin_AdminStats_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_uiCurrentJobProcessorQueueSize: function SpottedScript_Admin_AdminStats_View$get_uiCurrentJobProcessorQueueSize() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCurrentJobProcessorQueueSize');
    },
    get_jobProcessorDataItemsGridView: function SpottedScript_Admin_AdminStats_View$get_jobProcessorDataItemsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_JobProcessorDataItemsGridView');
    },
    get_h2: function SpottedScript_Admin_AdminStats_View$get_h2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H2');
    },
    get_uiPhotoUploaderTriesDataItemsGridView: function SpottedScript_Admin_AdminStats_View$get_uiPhotoUploaderTriesDataItemsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoUploaderTriesDataItemsGridView');
    },
    get_uiPhotoUploaderSuccessFailureDataItemsGridView: function SpottedScript_Admin_AdminStats_View$get_uiPhotoUploaderSuccessFailureDataItemsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoUploaderSuccessFailureDataItemsGridView');
    },
    get_genericContainerPage: function SpottedScript_Admin_AdminStats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.AdminStats.View.registerClass('SpottedScript.Admin.AdminStats.View', SpottedScript.AdminUserControl.View);
