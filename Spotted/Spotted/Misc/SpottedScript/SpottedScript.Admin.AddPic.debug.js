Type.registerNamespace('SpottedScript.Admin.AddPic');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.AddPic.View
SpottedScript.Admin.AddPic.View = function SpottedScript_Admin_AddPic_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.AddPic.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.AddPic.View.prototype = {
    clientId: null,
    get_button1: function SpottedScript_Admin_AddPic_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_button2: function SpottedScript_Admin_AddPic_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_objectPanel: function SpottedScript_Admin_AddPic_View$get_objectPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ObjectPanel');
    },
    get_objectTypeList: function SpottedScript_Admin_AddPic_View$get_objectTypeList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ObjectTypeList');
    },
    get_objectKTextBox: function SpottedScript_Admin_AddPic_View$get_objectKTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ObjectKTextBox');
    },
    get_viewPicPanel: function SpottedScript_Admin_AddPic_View$get_viewPicPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewPicPanel');
    },
    get_viewPicImg: function SpottedScript_Admin_AddPic_View$get_viewPicImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewPicImg');
    },
    get_cropperPanel: function SpottedScript_Admin_AddPic_View$get_cropperPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CropperPanel');
    },
    get_picCropper: function SpottedScript_Admin_AddPic_View$get_picCropper() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicCropper');
    },
    get_genericContainerPage: function SpottedScript_Admin_AddPic_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.AddPic.View.registerClass('SpottedScript.Admin.AddPic.View', SpottedScript.AdminUserControl.View);
