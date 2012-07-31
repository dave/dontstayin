Type.registerNamespace('SpottedScript.Pages.Out');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Out.View
SpottedScript.Pages.Out.View = function SpottedScript_Pages_Out_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Out.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Out.View.prototype = {
    clientId: null,
    get_topPChoose: function SpottedScript_Pages_Out_View$get_topPChoose() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TopPChoose');
    },
    get_topP: function SpottedScript_Pages_Out_View$get_topP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TopP');
    },
    get_thumb1: function SpottedScript_Pages_Out_View$get_thumb1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Thumb1');
    },
    get_thumb2: function SpottedScript_Pages_Out_View$get_thumb2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Thumb2');
    },
    get_thumb3: function SpottedScript_Pages_Out_View$get_thumb3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Thumb3');
    },
    get_thumb4: function SpottedScript_Pages_Out_View$get_thumb4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Thumb4');
    },
    get_thumb5: function SpottedScript_Pages_Out_View$get_thumb5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Thumb5');
    },
    get_next: function SpottedScript_Pages_Out_View$get_next() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Next');
    },
    get_webHolder: function SpottedScript_Pages_Out_View$get_webHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WebHolder');
    },
    get_link: function SpottedScript_Pages_Out_View$get_link() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Link');
    },
    get_web: function SpottedScript_Pages_Out_View$get_web() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Web');
    },
    get_bottomPara: function SpottedScript_Pages_Out_View$get_bottomPara() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BottomPara');
    },
    get_genericContainerPage: function SpottedScript_Pages_Out_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Out.View.registerClass('SpottedScript.Pages.Out.View', SpottedScript.DsiUserControl.View);
