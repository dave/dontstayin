Type.registerNamespace('SpottedScript.Pages.FindYourFriends');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.FindYourFriends.View
SpottedScript.Pages.FindYourFriends.View = function SpottedScript_Pages_FindYourFriends_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.FindYourFriends.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.FindYourFriends.View.prototype = {
    clientId: null,
    get_topIcon: function SpottedScript_Pages_FindYourFriends_View$get_topIcon() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TopIcon');
    },
    get_searchTypeHolder: function SpottedScript_Pages_FindYourFriends_View$get_searchTypeHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchTypeHolder');
    },
    get_searchType1: function SpottedScript_Pages_FindYourFriends_View$get_searchType1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchType1');
    },
    get_searchType2: function SpottedScript_Pages_FindYourFriends_View$get_searchType2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchType2');
    },
    get_searchType3: function SpottedScript_Pages_FindYourFriends_View$get_searchType3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchType3');
    },
    get_searchType4: function SpottedScript_Pages_FindYourFriends_View$get_searchType4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchType4');
    },
    get_uiUserName: function SpottedScript_Pages_FindYourFriends_View$get_uiUserName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUserName');
    },
    get_uiUserNameButton: function SpottedScript_Pages_FindYourFriends_View$get_uiUserNameButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUserNameButton');
    },
    get_uiUserNameBuddyControl: function SpottedScript_Pages_FindYourFriends_View$get_uiUserNameBuddyControl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUserNameBuddyControl');
    },
    get_uiSpotterCode: function SpottedScript_Pages_FindYourFriends_View$get_uiSpotterCode() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSpotterCode');
    },
    get_uiSpotterCodeButton: function SpottedScript_Pages_FindYourFriends_View$get_uiSpotterCodeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSpotterCodeButton');
    },
    get_uiInvalidSpottedCode: function SpottedScript_Pages_FindYourFriends_View$get_uiInvalidSpottedCode() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInvalidSpottedCode');
    },
    get_uiSpotterBuddyControl: function SpottedScript_Pages_FindYourFriends_View$get_uiSpotterBuddyControl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSpotterBuddyControl');
    },
    get_uiFirstName: function SpottedScript_Pages_FindYourFriends_View$get_uiFirstName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFirstName');
    },
    get_uiLastName: function SpottedScript_Pages_FindYourFriends_View$get_uiLastName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiLastName');
    },
    get_uiRealNameButton: function SpottedScript_Pages_FindYourFriends_View$get_uiRealNameButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiRealNameButton');
    },
    get_uiRealNameBuddyControl: function SpottedScript_Pages_FindYourFriends_View$get_uiRealNameBuddyControl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiRealNameBuddyControl');
    },
    get_uiBuddyImporter: function SpottedScript_Pages_FindYourFriends_View$get_uiBuddyImporter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBuddyImporter');
    },
    get_genericContainerPage: function SpottedScript_Pages_FindYourFriends_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.FindYourFriends.View.registerClass('SpottedScript.Pages.FindYourFriends.View', SpottedScript.DsiUserControl.View);
