Type.registerNamespace('SpottedScript.Pages.Photos.Report');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Photos.Report.View
SpottedScript.Pages.Photos.Report.View = function SpottedScript_Pages_Photos_Report_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Photos.Report.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Photos.Report.View.prototype = {
    clientId: null,
    get_photoImg: function SpottedScript_Pages_Photos_Report_View$get_photoImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoImg');
    },
    get_photoAnchor: function SpottedScript_Pages_Photos_Report_View$get_photoAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoAnchor');
    },
    get_reportMessageTextBox: function SpottedScript_Pages_Photos_Report_View$get_reportMessageTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReportMessageTextBox');
    },
    get_reportP: function SpottedScript_Pages_Photos_Report_View$get_reportP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReportP');
    },
    get_buddyCheckBoxesPanel: function SpottedScript_Pages_Photos_Report_View$get_buddyCheckBoxesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddyCheckBoxesPanel');
    },
    get_buddyCheckBoxList: function SpottedScript_Pages_Photos_Report_View$get_buddyCheckBoxList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddyCheckBoxList');
    },
    get_buddyCheckBoxAll: function SpottedScript_Pages_Photos_Report_View$get_buddyCheckBoxAll() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddyCheckBoxAll');
    },
    get_donePanel: function SpottedScript_Pages_Photos_Report_View$get_donePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonePanel');
    },
    get_reportPanel: function SpottedScript_Pages_Photos_Report_View$get_reportPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReportPanel');
    },
    get_h11: function SpottedScript_Pages_Photos_Report_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_button1: function SpottedScript_Pages_Photos_Report_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_h12: function SpottedScript_Pages_Photos_Report_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_genericContainerPage: function SpottedScript_Pages_Photos_Report_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Photos.Report.View.registerClass('SpottedScript.Pages.Photos.Report.View', SpottedScript.DsiUserControl.View);
