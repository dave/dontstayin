Type.registerNamespace('SpottedScript.Blank.NewBrand');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.NewBrand.View
SpottedScript.Blank.NewBrand.View = function SpottedScript_Blank_NewBrand_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.NewBrand.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.NewBrand.View.prototype = {
    clientId: null,
    get_nameTextBox: function SpottedScript_Blank_NewBrand_View$get_nameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NameTextBox');
    },
    get_panelName: function SpottedScript_Blank_NewBrand_View$get_panelName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelName');
    },
    get_panelPic: function SpottedScript_Blank_NewBrand_View$get_panelPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPic');
    },
    get_picUploadPanel: function SpottedScript_Blank_NewBrand_View$get_picUploadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicUploadPanel');
    },
    get_pic: function SpottedScript_Blank_NewBrand_View$get_pic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Pic');
    },
    get_panelDone: function SpottedScript_Blank_NewBrand_View$get_panelDone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDone');
    },
    get_h12: function SpottedScript_Blank_NewBrand_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_button3: function SpottedScript_Blank_NewBrand_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_requiredFieldValidator1: function SpottedScript_Blank_NewBrand_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_regularExpressionValidator1: function SpottedScript_Blank_NewBrand_View$get_regularExpressionValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator1');
    },
    get_customValidator1: function SpottedScript_Blank_NewBrand_View$get_customValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator1');
    },
    get_h15: function SpottedScript_Blank_NewBrand_View$get_h15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15');
    },
    get_h11: function SpottedScript_Blank_NewBrand_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_genericContainerPage: function SpottedScript_Blank_NewBrand_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.NewBrand.View.registerClass('SpottedScript.Blank.NewBrand.View', SpottedScript.BlankUserControl.View);
