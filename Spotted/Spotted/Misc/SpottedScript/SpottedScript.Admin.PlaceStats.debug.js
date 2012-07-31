Type.registerNamespace('SpottedScript.Admin.PlaceStats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.PlaceStats.View
SpottedScript.Admin.PlaceStats.View = function SpottedScript_Admin_PlaceStats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.PlaceStats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.PlaceStats.View.prototype = {
    clientId: null,
    get_placeName: function SpottedScript_Admin_PlaceStats_View$get_placeName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceName');
    },
    get_tab: function SpottedScript_Admin_PlaceStats_View$get_tab() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Tab');
    },
    get_genericContainerPage: function SpottedScript_Admin_PlaceStats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.PlaceStats.View.registerClass('SpottedScript.Admin.PlaceStats.View', SpottedScript.AdminUserControl.View);
