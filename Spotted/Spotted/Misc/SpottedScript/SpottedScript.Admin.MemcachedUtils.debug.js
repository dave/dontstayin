Type.registerNamespace('SpottedScript.Admin.MemcachedUtils');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.MemcachedUtils.View
SpottedScript.Admin.MemcachedUtils.View = function SpottedScript_Admin_MemcachedUtils_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.MemcachedUtils.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.MemcachedUtils.View.prototype = {
    clientId: null,
    get_setKey: function SpottedScript_Admin_MemcachedUtils_View$get_setKey() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SetKey');
    },
    get_setValue: function SpottedScript_Admin_MemcachedUtils_View$get_setValue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SetValue');
    },
    get_setResponse: function SpottedScript_Admin_MemcachedUtils_View$get_setResponse() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SetResponse');
    },
    get_deleteKey: function SpottedScript_Admin_MemcachedUtils_View$get_deleteKey() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteKey');
    },
    get_deleteKeyResponse: function SpottedScript_Admin_MemcachedUtils_View$get_deleteKeyResponse() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteKeyResponse');
    },
    get_setCounterKey: function SpottedScript_Admin_MemcachedUtils_View$get_setCounterKey() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SetCounterKey');
    },
    get_setCounterValue: function SpottedScript_Admin_MemcachedUtils_View$get_setCounterValue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SetCounterValue');
    },
    get_setCounterKeyResponse: function SpottedScript_Admin_MemcachedUtils_View$get_setCounterKeyResponse() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SetCounterKeyResponse');
    },
    get_getKey: function SpottedScript_Admin_MemcachedUtils_View$get_getKey() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GetKey');
    },
    get_getPara: function SpottedScript_Admin_MemcachedUtils_View$get_getPara() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GetPara');
    },
    get_getCounterKey: function SpottedScript_Admin_MemcachedUtils_View$get_getCounterKey() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GetCounterKey');
    },
    get_getCounterKeyLabel: function SpottedScript_Admin_MemcachedUtils_View$get_getCounterKeyLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GetCounterKeyLabel');
    },
    get_getCounterValue: function SpottedScript_Admin_MemcachedUtils_View$get_getCounterValue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GetCounterValue');
    },
    get_genericContainerPage: function SpottedScript_Admin_MemcachedUtils_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.MemcachedUtils.View.registerClass('SpottedScript.Admin.MemcachedUtils.View', SpottedScript.AdminUserControl.View);
