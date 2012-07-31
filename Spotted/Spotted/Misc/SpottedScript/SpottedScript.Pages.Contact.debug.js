Type.registerNamespace('SpottedScript.Pages.Contact');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Contact.View
SpottedScript.Pages.Contact.View = function SpottedScript_Pages_Contact_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Contact.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Contact.View.prototype = {
    clientId: null,
    get_h11: function SpottedScript_Pages_Contact_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_h14: function SpottedScript_Pages_Contact_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_h12: function SpottedScript_Pages_Contact_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h1ghd4: function SpottedScript_Pages_Contact_View$get_h1ghd4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1ghd4');
    },
    get_h13: function SpottedScript_Pages_Contact_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_johnPic: function SpottedScript_Pages_Contact_View$get_johnPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_JohnPic');
    },
    get_timPic: function SpottedScript_Pages_Contact_View$get_timPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TimPic');
    },
    get_davePic: function SpottedScript_Pages_Contact_View$get_davePic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DavePic');
    },
    get_johnLink: function SpottedScript_Pages_Contact_View$get_johnLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_JohnLink');
    },
    get_timLink: function SpottedScript_Pages_Contact_View$get_timLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TimLink');
    },
    get_daveLink: function SpottedScript_Pages_Contact_View$get_daveLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DaveLink');
    },
    get_superAdminDataList: function SpottedScript_Pages_Contact_View$get_superAdminDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuperAdminDataList');
    },
    get_seniorAdminDataList: function SpottedScript_Pages_Contact_View$get_seniorAdminDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SeniorAdminDataList');
    },
    get_juniorAdminDataList: function SpottedScript_Pages_Contact_View$get_juniorAdminDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_JuniorAdminDataList');
    },
    get_genericContainerPage: function SpottedScript_Pages_Contact_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Contact.View.registerClass('SpottedScript.Pages.Contact.View', SpottedScript.DsiUserControl.View);
