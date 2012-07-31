Type.registerNamespace('SpottedScript.Pages.MyCalendar');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyCalendar.View
SpottedScript.Pages.MyCalendar.View = function SpottedScript_Pages_MyCalendar_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyCalendar.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyCalendar.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_MyCalendar_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyCalendar.View.registerClass('SpottedScript.Pages.MyCalendar.View', SpottedScript.DsiUserControl.View);
