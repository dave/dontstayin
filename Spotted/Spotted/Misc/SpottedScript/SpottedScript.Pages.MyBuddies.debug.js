Type.registerNamespace('SpottedScript.Pages.MyBuddies');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyBuddies.View
SpottedScript.Pages.MyBuddies.View = function SpottedScript_Pages_MyBuddies_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyBuddies.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyBuddies.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Pages_MyBuddies_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h11: function SpottedScript_Pages_MyBuddies_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_h14: function SpottedScript_Pages_MyBuddies_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_h13: function SpottedScript_Pages_MyBuddies_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_panelNoBuddies: function SpottedScript_Pages_MyBuddies_View$get_panelNoBuddies() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelNoBuddies');
    },
    get_panelBuddies: function SpottedScript_Pages_MyBuddies_View$get_panelBuddies() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelBuddies');
    },
    get_fullBuddyListPanel: function SpottedScript_Pages_MyBuddies_View$get_fullBuddyListPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FullBuddyListPanel');
    },
    get_halfBuddyListPanel: function SpottedScript_Pages_MyBuddies_View$get_halfBuddyListPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HalfBuddyListPanel');
    },
    get_reverseHalfBuddyListPanel: function SpottedScript_Pages_MyBuddies_View$get_reverseHalfBuddyListPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReverseHalfBuddyListPanel');
    },
    get_fullBuddyList: function SpottedScript_Pages_MyBuddies_View$get_fullBuddyList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FullBuddyList');
    },
    get_halfBuddyList: function SpottedScript_Pages_MyBuddies_View$get_halfBuddyList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HalfBuddyList');
    },
    get_reverseHalfBuddyList: function SpottedScript_Pages_MyBuddies_View$get_reverseHalfBuddyList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ReverseHalfBuddyList');
    },
    get_genericContainerPage: function SpottedScript_Pages_MyBuddies_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyBuddies.View.registerClass('SpottedScript.Pages.MyBuddies.View', SpottedScript.DsiUserControl.View);
