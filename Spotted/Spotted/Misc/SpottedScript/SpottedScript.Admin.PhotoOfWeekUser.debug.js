Type.registerNamespace('SpottedScript.Admin.PhotoOfWeekUser');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.PhotoOfWeekUser.View
SpottedScript.Admin.PhotoOfWeekUser.View = function SpottedScript_Admin_PhotoOfWeekUser_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.PhotoOfWeekUser.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.PhotoOfWeekUser.View.prototype = {
    clientId: null,
    get_uiPhotoK: function SpottedScript_Admin_PhotoOfWeekUser_View$get_uiPhotoK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoK');
    },
    get_uiPhotoDetails: function SpottedScript_Admin_PhotoOfWeekUser_View$get_uiPhotoDetails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoDetails');
    },
    get_uiPhotoKLabel: function SpottedScript_Admin_PhotoOfWeekUser_View$get_uiPhotoKLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoKLabel');
    },
    get_uiPhotoImg: function SpottedScript_Admin_PhotoOfWeekUser_View$get_uiPhotoImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoImg');
    },
    get_uiPhotoOfWeek: function SpottedScript_Admin_PhotoOfWeekUser_View$get_uiPhotoOfWeek() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoOfWeek');
    },
    get_uiPhotoOfWeekUserCaption: function SpottedScript_Admin_PhotoOfWeekUser_View$get_uiPhotoOfWeekUserCaption() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoOfWeekUserCaption');
    },
    get_uiResetDateTime: function SpottedScript_Admin_PhotoOfWeekUser_View$get_uiResetDateTime() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiResetDateTime');
    },
    get_uiPhotoOfWeekUserBlocked: function SpottedScript_Admin_PhotoOfWeekUser_View$get_uiPhotoOfWeekUserBlocked() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPhotoOfWeekUserBlocked');
    },
    get_genericContainerPage: function SpottedScript_Admin_PhotoOfWeekUser_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.PhotoOfWeekUser.View.registerClass('SpottedScript.Admin.PhotoOfWeekUser.View', SpottedScript.AdminUserControl.View);
