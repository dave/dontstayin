Type.registerNamespace('SpottedScript.Admin.FlyerEdit');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.FlyerEdit.View
SpottedScript.Admin.FlyerEdit.View = function SpottedScript_Admin_FlyerEdit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.FlyerEdit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.FlyerEdit.View.prototype = {
    clientId: null,
    get_uiBasicInfo: function SpottedScript_Admin_FlyerEdit_View$get_uiBasicInfo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBasicInfo');
    },
    get_uiFlyerKLabel: function SpottedScript_Admin_FlyerEdit_View$get_uiFlyerKLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFlyerKLabel');
    },
    get_uiPromotersAutoComplete: function SpottedScript_Admin_FlyerEdit_View$get_uiPromotersAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromotersAutoCompleteBehaviour');
    },
    get_uiNameTextBox: function SpottedScript_Admin_FlyerEdit_View$get_uiNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiNameTextBox');
    },
    get_uiSubjectTextBox: function SpottedScript_Admin_FlyerEdit_View$get_uiSubjectTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSubjectTextBox');
    },
    get_uiFromDisplayNameTextBox: function SpottedScript_Admin_FlyerEdit_View$get_uiFromDisplayNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiFromDisplayNameTextBox');
    },
    get_uiBackgroundColor: function SpottedScript_Admin_FlyerEdit_View$get_uiBackgroundColor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBackgroundColor');
    },
    get_uiUrlTextBox: function SpottedScript_Admin_FlyerEdit_View$get_uiUrlTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUrlTextBox');
    },
    get_uiSendDate: function SpottedScript_Admin_FlyerEdit_View$get_uiSendDate() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_uiSendDateController');
    },
    get_uiSendTime: function SpottedScript_Admin_FlyerEdit_View$get_uiSendTime() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSendTime');
    },
    get_uiPlaceTargettingHidden: function SpottedScript_Admin_FlyerEdit_View$get_uiPlaceTargettingHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPlaceTargettingHidden');
    },
    get_uiPlaceTargettingTextBox: function SpottedScript_Admin_FlyerEdit_View$get_uiPlaceTargettingTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPlaceTargettingTextBox');
    },
    get_uiPlaceTargettingButton: function SpottedScript_Admin_FlyerEdit_View$get_uiPlaceTargettingButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPlaceTargettingButton');
    },
    get_uiMusicTargettingHidden: function SpottedScript_Admin_FlyerEdit_View$get_uiMusicTargettingHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMusicTargettingHidden');
    },
    get_uiMusicTargettingTextBox: function SpottedScript_Admin_FlyerEdit_View$get_uiMusicTargettingTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMusicTargettingTextBox');
    },
    get_uiMusicTargettingButton: function SpottedScript_Admin_FlyerEdit_View$get_uiMusicTargettingButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMusicTargettingButton');
    },
    get_uiPromotersOnly: function SpottedScript_Admin_FlyerEdit_View$get_uiPromotersOnly() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPromotersOnly');
    },
    get_uiEvent: function SpottedScript_Admin_FlyerEdit_View$get_uiEvent() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiEventBehaviour');
    },
    get_uiBrand: function SpottedScript_Admin_FlyerEdit_View$get_uiBrand() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiBrandBehaviour');
    },
    get_uiEventKs: function SpottedScript_Admin_FlyerEdit_View$get_uiEventKs() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEventKs');
    },
    get_uiEvents: function SpottedScript_Admin_FlyerEdit_View$get_uiEvents() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEvents');
    },
    get_uiUsrBaseCountLabel: function SpottedScript_Admin_FlyerEdit_View$get_uiUsrBaseCountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUsrBaseCountLabel');
    },
    get_uiInputFile: function SpottedScript_Admin_FlyerEdit_View$get_uiInputFile() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiInputFile');
    },
    get_uiPreviewFile: function SpottedScript_Admin_FlyerEdit_View$get_uiPreviewFile() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPreviewFile');
    },
    get_uiSaveButton: function SpottedScript_Admin_FlyerEdit_View$get_uiSaveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSaveButton');
    },
    get_uiSavedLabel: function SpottedScript_Admin_FlyerEdit_View$get_uiSavedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSavedLabel');
    },
    get_uiTestButton: function SpottedScript_Admin_FlyerEdit_View$get_uiTestButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTestButton');
    },
    get_uiTestEmail: function SpottedScript_Admin_FlyerEdit_View$get_uiTestEmail() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTestEmail');
    },
    get_uiTestEmailSuccess: function SpottedScript_Admin_FlyerEdit_View$get_uiTestEmailSuccess() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTestEmailSuccess');
    },
    get_uiIsHtml: function SpottedScript_Admin_FlyerEdit_View$get_uiIsHtml() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiIsHtml');
    },
    get_uiHtml: function SpottedScript_Admin_FlyerEdit_View$get_uiHtml() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiHtml');
    },
    get_uiTextAlternative: function SpottedScript_Admin_FlyerEdit_View$get_uiTextAlternative() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTextAlternative');
    },
    get_genericContainerPage: function SpottedScript_Admin_FlyerEdit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.FlyerEdit.View.registerClass('SpottedScript.Admin.FlyerEdit.View', SpottedScript.AdminUserControl.View);
