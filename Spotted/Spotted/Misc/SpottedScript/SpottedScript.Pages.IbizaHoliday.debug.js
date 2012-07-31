Type.registerNamespace('SpottedScript.Pages.IbizaHoliday');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.IbizaHoliday.View
SpottedScript.Pages.IbizaHoliday.View = function SpottedScript_Pages_IbizaHoliday_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.IbizaHoliday.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.IbizaHoliday.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_IbizaHoliday_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.IbizaHoliday.View.registerClass('SpottedScript.Pages.IbizaHoliday.View', SpottedScript.DsiUserControl.View);
