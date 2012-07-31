Type.registerNamespace('SpottedScript.Admin.SalesFind');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.SalesFind.View
SpottedScript.Admin.SalesFind.View = function SpottedScript_Admin_SalesFind_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.SalesFind.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.SalesFind.View.prototype = {
    clientId: null,
    get_findByUser: function SpottedScript_Admin_SalesFind_View$get_findByUser() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FindByUser');
    },
    get_uiFindByUserPanel: function SpottedScript_Admin_SalesFind_View$get_uiFindByUserPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFindByUserPanel');
    },
    get_uiUserAutoComplete: function SpottedScript_Admin_SalesFind_View$get_uiUserAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiUserAutoCompleteBehaviour');
    },
    get_uiLookupUserButton: function SpottedScript_Admin_SalesFind_View$get_uiLookupUserButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiLookupUserButton');
    },
    get_uiUserIsNotPromoterPanel: function SpottedScript_Admin_SalesFind_View$get_uiUserIsNotPromoterPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUserIsNotPromoterPanel');
    },
    get_findByBrand: function SpottedScript_Admin_SalesFind_View$get_findByBrand() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FindByBrand');
    },
    get_uiFindByBrandPanel: function SpottedScript_Admin_SalesFind_View$get_uiFindByBrandPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFindByBrandPanel');
    },
    get_uiBrandsAutoComplete: function SpottedScript_Admin_SalesFind_View$get_uiBrandsAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiBrandsAutoCompleteBehaviour');
    },
    get_uiGoToPromoterPageByBrandButton: function SpottedScript_Admin_SalesFind_View$get_uiGoToPromoterPageByBrandButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiGoToPromoterPageByBrandButton');
    },
    get_findByPromoter: function SpottedScript_Admin_SalesFind_View$get_findByPromoter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FindByPromoter');
    },
    get_uiFindByPromoterPanel: function SpottedScript_Admin_SalesFind_View$get_uiFindByPromoterPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFindByPromoterPanel');
    },
    get_uiPromoterAutoComplete: function SpottedScript_Admin_SalesFind_View$get_uiPromoterAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromoterAutoCompleteBehaviour');
    },
    get_uiGoToPromoterPageByPromoterButton: function SpottedScript_Admin_SalesFind_View$get_uiGoToPromoterPageByPromoterButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiGoToPromoterPageByPromoterButton');
    },
    get_uiBrandPromoterIsNullPanel: function SpottedScript_Admin_SalesFind_View$get_uiBrandPromoterIsNullPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBrandPromoterIsNullPanel');
    },
    get_genericContainerPage: function SpottedScript_Admin_SalesFind_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.SalesFind.View.registerClass('SpottedScript.Admin.SalesFind.View', SpottedScript.AdminUserControl.View);
