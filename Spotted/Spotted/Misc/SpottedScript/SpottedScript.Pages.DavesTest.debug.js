Type.registerNamespace('SpottedScript.Pages.DavesTest');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.DavesTest.Controller
SpottedScript.Pages.DavesTest.Controller = function SpottedScript_Pages_DavesTest_Controller(v) {
    /// <param name="v" type="SpottedScript.Pages.DavesTest.View">
    /// </param>
    /// <field name="view" type="SpottedScript.Pages.DavesTest.View">
    /// </field>
    this.view = v;
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.Pages.DavesTest.Controller.prototype = {
    view: null,
    _initialise: function SpottedScript_Pages_DavesTest_Controller$_initialise() {
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.DavesTest.View
SpottedScript.Pages.DavesTest.View = function SpottedScript_Pages_DavesTest_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.DavesTest.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.DavesTest.View.prototype = {
    clientId: null,
    get_myAspButton: function SpottedScript_Pages_DavesTest_View$get_myAspButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyAspButton');
    },
    get_myButton: function SpottedScript_Pages_DavesTest_View$get_myButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyButton');
    },
    get_serverP: function SpottedScript_Pages_DavesTest_View$get_serverP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ServerP');
    },
    get_clientP: function SpottedScript_Pages_DavesTest_View$get_clientP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClientP');
    },
    get_genericContainerPage: function SpottedScript_Pages_DavesTest_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.DavesTest.Controller.registerClass('SpottedScript.Pages.DavesTest.Controller');
SpottedScript.Pages.DavesTest.View.registerClass('SpottedScript.Pages.DavesTest.View', SpottedScript.DsiUserControl.View);
