Type.registerNamespace('SpottedScript.Admin.RegisterNewDomain');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.RegisterNewDomain.View
SpottedScript.Admin.RegisterNewDomain.View = function SpottedScript_Admin_RegisterNewDomain_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.RegisterNewDomain.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.RegisterNewDomain.View.prototype = {
    clientId: null,
    get_uiPromotersAutoComplete: function SpottedScript_Admin_RegisterNewDomain_View$get_uiPromotersAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromotersAutoCompleteBehaviour');
    },
    get_uiNewDomainDetailsPanel: function SpottedScript_Admin_RegisterNewDomain_View$get_uiNewDomainDetailsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNewDomainDetailsPanel');
    },
    get_uiDomainsRegistered: function SpottedScript_Admin_RegisterNewDomain_View$get_uiDomainsRegistered() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDomainsRegistered');
    },
    get_uiDomainName: function SpottedScript_Admin_RegisterNewDomain_View$get_uiDomainName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDomainName');
    },
    get_uiDomainAvailability: function SpottedScript_Admin_RegisterNewDomain_View$get_uiDomainAvailability() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDomainAvailability');
    },
    get_uiOptionsList: function SpottedScript_Admin_RegisterNewDomain_View$get_uiOptionsList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiOptionsList');
    },
    get_uiBrandDiv: function SpottedScript_Admin_RegisterNewDomain_View$get_uiBrandDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBrandDiv');
    },
    get_uiBrandsDdl: function SpottedScript_Admin_RegisterNewDomain_View$get_uiBrandsDdl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBrandsDdl');
    },
    get_uiBrandRedirectApp: function SpottedScript_Admin_RegisterNewDomain_View$get_uiBrandRedirectApp() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBrandRedirectApp');
    },
    get_uiVenueDiv: function SpottedScript_Admin_RegisterNewDomain_View$get_uiVenueDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiVenueDiv');
    },
    get_uiVenuesDdl: function SpottedScript_Admin_RegisterNewDomain_View$get_uiVenuesDdl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiVenuesDdl');
    },
    get_uiVenueRedirectApp: function SpottedScript_Admin_RegisterNewDomain_View$get_uiVenueRedirectApp() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiVenueRedirectApp');
    },
    get_uiEventDiv: function SpottedScript_Admin_RegisterNewDomain_View$get_uiEventDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEventDiv');
    },
    get_uiEventK: function SpottedScript_Admin_RegisterNewDomain_View$get_uiEventK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEventK');
    },
    get_uiGroupDiv: function SpottedScript_Admin_RegisterNewDomain_View$get_uiGroupDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiGroupDiv');
    },
    get_uiGroupK: function SpottedScript_Admin_RegisterNewDomain_View$get_uiGroupK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiGroupK');
    },
    get_uiCustomUrlDiv: function SpottedScript_Admin_RegisterNewDomain_View$get_uiCustomUrlDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCustomUrlDiv');
    },
    get_uiCustomUrlText: function SpottedScript_Admin_RegisterNewDomain_View$get_uiCustomUrlText() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCustomUrlText');
    },
    get_uiRegisterDomainButton: function SpottedScript_Admin_RegisterNewDomain_View$get_uiRegisterDomainButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiRegisterDomainButton');
    },
    get_uiTestRedirectPanel: function SpottedScript_Admin_RegisterNewDomain_View$get_uiTestRedirectPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTestRedirectPanel');
    },
    get_uiAddedDomain: function SpottedScript_Admin_RegisterNewDomain_View$get_uiAddedDomain() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiAddedDomain');
    },
    get_uiErrorLbl: function SpottedScript_Admin_RegisterNewDomain_View$get_uiErrorLbl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiErrorLbl');
    },
    get_genericContainerPage: function SpottedScript_Admin_RegisterNewDomain_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.RegisterNewDomain.View.registerClass('SpottedScript.Admin.RegisterNewDomain.View', SpottedScript.AdminUserControl.View);
