Type.registerNamespace('SpottedScript.Pages.Groups.Admin');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Groups.Admin.View
SpottedScript.Pages.Groups.Admin.View = function SpottedScript_Pages_Groups_Admin_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Groups.Admin.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Groups.Admin.View.prototype = {
    clientId: null,
    get_uiEventPicker: function SpottedScript_Pages_Groups_Admin_View$get_uiEventPicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_uiEventPickerController');
    },
    get_uiRemoveEventPicker: function SpottedScript_Pages_Groups_Admin_View$get_uiRemoveEventPicker() {
        /// <value type="SpottedScript.Controls.Picker.Controller"></value>
        return eval(this.clientId + '_uiRemoveEventPickerController');
    },
    get_panelOptions: function SpottedScript_Pages_Groups_Admin_View$get_panelOptions() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelOptions');
    },
    get_groupIntro: function SpottedScript_Pages_Groups_Admin_View$get_groupIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupIntro');
    },
    get_optionsGroupAnchor: function SpottedScript_Pages_Groups_Admin_View$get_optionsGroupAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionsGroupAnchor');
    },
    get_optionsMenuP: function SpottedScript_Pages_Groups_Admin_View$get_optionsMenuP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionsMenuP');
    },
    get_editOptionsP: function SpottedScript_Pages_Groups_Admin_View$get_editOptionsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditOptionsP');
    },
    get_optionsInviteP: function SpottedScript_Pages_Groups_Admin_View$get_optionsInviteP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionsInviteP');
    },
    get_buddiesPanel: function SpottedScript_Pages_Groups_Admin_View$get_buddiesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddiesPanel');
    },
    get_h112: function SpottedScript_Pages_Groups_Admin_View$get_h112() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H112');
    },
    get_buddiesOutputP: function SpottedScript_Pages_Groups_Admin_View$get_buddiesOutputP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddiesOutputP');
    },
    get_buddiesIntroTextBox: function SpottedScript_Pages_Groups_Admin_View$get_buddiesIntroTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddiesIntroTextBox');
    },
    get_buddiesIntroVal: function SpottedScript_Pages_Groups_Admin_View$get_buddiesIntroVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BuddiesIntroVal');
    },
    get_uiMultiBuddyChooser: function SpottedScript_Pages_Groups_Admin_View$get_uiMultiBuddyChooser() {
        /// <value type="SpottedScript.Controls.MultiBuddyChooser.Controller"></value>
        return eval(this.clientId + '_uiMultiBuddyChooserController');
    },
    get_button1: function SpottedScript_Pages_Groups_Admin_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_emailPanel: function SpottedScript_Pages_Groups_Admin_View$get_emailPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailPanel');
    },
    get_h111: function SpottedScript_Pages_Groups_Admin_View$get_h111() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H111');
    },
    get_emailOutputP: function SpottedScript_Pages_Groups_Admin_View$get_emailOutputP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailOutputP');
    },
    get_emailIntroTextBox: function SpottedScript_Pages_Groups_Admin_View$get_emailIntroTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailIntroTextBox');
    },
    get_emailIntroVal: function SpottedScript_Pages_Groups_Admin_View$get_emailIntroVal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailIntroVal');
    },
    get_emailTextBox: function SpottedScript_Pages_Groups_Admin_View$get_emailTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailTextBox');
    },
    get_button3: function SpottedScript_Pages_Groups_Admin_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_ownerPanel: function SpottedScript_Pages_Groups_Admin_View$get_ownerPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnerPanel');
    },
    get_h16: function SpottedScript_Pages_Groups_Admin_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_ownerModeratorsDataGrid: function SpottedScript_Pages_Groups_Admin_View$get_ownerModeratorsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OwnerModeratorsDataGrid');
    },
    get_onwerModeratorError: function SpottedScript_Pages_Groups_Admin_View$get_onwerModeratorError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OnwerModeratorError');
    },
    get_onwerModeratorDone: function SpottedScript_Pages_Groups_Admin_View$get_onwerModeratorDone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OnwerModeratorDone');
    },
    get_button2: function SpottedScript_Pages_Groups_Admin_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_uiOwnerModeratorsAutoComplete: function SpottedScript_Pages_Groups_Admin_View$get_uiOwnerModeratorsAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiOwnerModeratorsAutoCompleteBehaviour');
    },
    get_button4: function SpottedScript_Pages_Groups_Admin_View$get_button4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button4');
    },
    get_memberAdminPanel: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminPanel');
    },
    get_h114: function SpottedScript_Pages_Groups_Admin_View$get_h114() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H114');
    },
    get_memberAdminNewUserEmailsCheckBox: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminNewUserEmailsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminNewUserEmailsCheckBox');
    },
    get_memberAdminNewUserEmailsCheckBoxStatusPanel: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminNewUserEmailsCheckBoxStatusPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminNewUserEmailsCheckBoxStatusPanel');
    },
    get_h15: function SpottedScript_Pages_Groups_Admin_View$get_h15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15');
    },
    get_uiMemberAdminInviteAutoComplete: function SpottedScript_Pages_Groups_Admin_View$get_uiMemberAdminInviteAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiMemberAdminInviteAutoCompleteBehaviour');
    },
    get_button5: function SpottedScript_Pages_Groups_Admin_View$get_button5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button5');
    },
    get_memberInviteLabel: function SpottedScript_Pages_Groups_Admin_View$get_memberInviteLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberInviteLabel');
    },
    get_h18: function SpottedScript_Pages_Groups_Admin_View$get_h18() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H18');
    },
    get_memberAdminRequestsDataGridPanel: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminRequestsDataGridPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminRequestsDataGridPanel');
    },
    get_memberAdminRequestsDataGrid: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminRequestsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminRequestsDataGrid');
    },
    get_button6: function SpottedScript_Pages_Groups_Admin_View$get_button6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button6');
    },
    get_memberAdminRequestsNoResultsP: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminRequestsNoResultsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminRequestsNoResultsP');
    },
    get_memberAdminRequestsUpdateResultsP: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminRequestsUpdateResultsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminRequestsUpdateResultsP');
    },
    get_h19: function SpottedScript_Pages_Groups_Admin_View$get_h19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19');
    },
    get_memberAdminOptionsMembersCheckBox: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminOptionsMembersCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminOptionsMembersCheckBox');
    },
    get_memberAdminOptionsRequestCheckBox: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminOptionsRequestCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminOptionsRequestCheckBox');
    },
    get_memberAdminOptionsInvitedCheckBox: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminOptionsInvitedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminOptionsInvitedCheckBox');
    },
    get_memberAdminOptionsRejectedCheckBox: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminOptionsRejectedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminOptionsRejectedCheckBox');
    },
    get_memberAdminOptionsExitedCheckBox: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminOptionsExitedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminOptionsExitedCheckBox');
    },
    get_memberAdminOptionsBarredCheckBox: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminOptionsBarredCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminOptionsBarredCheckBox');
    },
    get_memberAdminOptionsSearchTextBox: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminOptionsSearchTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminOptionsSearchTextBox');
    },
    get_button7: function SpottedScript_Pages_Groups_Admin_View$get_button7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button7');
    },
    get_memberAdminOptionsSearchNoResults: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminOptionsSearchNoResults() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminOptionsSearchNoResults');
    },
    get_memberAdminOptionsSearchNoneSelected: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminOptionsSearchNoneSelected() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminOptionsSearchNoneSelected');
    },
    get_memberAdminOptionsDataGridP: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminOptionsDataGridP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminOptionsDataGridP');
    },
    get_memberAdminOptionsDataGrid: function SpottedScript_Pages_Groups_Admin_View$get_memberAdminOptionsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberAdminOptionsDataGrid');
    },
    get_newsAdminPanel: function SpottedScript_Pages_Groups_Admin_View$get_newsAdminPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsAdminPanel');
    },
    get_h14: function SpottedScript_Pages_Groups_Admin_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_h17: function SpottedScript_Pages_Groups_Admin_View$get_h17() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H17');
    },
    get_newsDisableAllLinkButton: function SpottedScript_Pages_Groups_Admin_View$get_newsDisableAllLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsDisableAllLinkButton');
    },
    get_newsDataGrid: function SpottedScript_Pages_Groups_Admin_View$get_newsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsDataGrid');
    },
    get_newsPageP: function SpottedScript_Pages_Groups_Admin_View$get_newsPageP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsPageP');
    },
    get_newsThreadPanel: function SpottedScript_Pages_Groups_Admin_View$get_newsThreadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsThreadPanel');
    },
    get_h110: function SpottedScript_Pages_Groups_Admin_View$get_h110() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H110');
    },
    get_newsThreadRepeater: function SpottedScript_Pages_Groups_Admin_View$get_newsThreadRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsThreadRepeater');
    },
    get_moderatorPanel: function SpottedScript_Pages_Groups_Admin_View$get_moderatorPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeratorPanel');
    },
    get_h12: function SpottedScript_Pages_Groups_Admin_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h11: function SpottedScript_Pages_Groups_Admin_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_moderatorRecommendedPanel: function SpottedScript_Pages_Groups_Admin_View$get_moderatorRecommendedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeratorRecommendedPanel');
    },
    get_h113: function SpottedScript_Pages_Groups_Admin_View$get_h113() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H113');
    },
    get_button8: function SpottedScript_Pages_Groups_Admin_View$get_button8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button8');
    },
    get_moderatorRecommendedAddErrorP: function SpottedScript_Pages_Groups_Admin_View$get_moderatorRecommendedAddErrorP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeratorRecommendedAddErrorP');
    },
    get_moderatorRecommendedAddDoneP: function SpottedScript_Pages_Groups_Admin_View$get_moderatorRecommendedAddDoneP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeratorRecommendedAddDoneP');
    },
    get_uiModeratorRecommendedRemoveAutoComplete: function SpottedScript_Pages_Groups_Admin_View$get_uiModeratorRecommendedRemoveAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiModeratorRecommendedRemoveAutoCompleteBehaviour');
    },
    get_moderatorRecommendedRemoveFuture: function SpottedScript_Pages_Groups_Admin_View$get_moderatorRecommendedRemoveFuture() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeratorRecommendedRemoveFuture');
    },
    get_moderatorRecommendedRemoveAttend: function SpottedScript_Pages_Groups_Admin_View$get_moderatorRecommendedRemoveAttend() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeratorRecommendedRemoveAttend');
    },
    get_button9: function SpottedScript_Pages_Groups_Admin_View$get_button9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button9');
    },
    get_moderatorRecommendedRemoveErrorP: function SpottedScript_Pages_Groups_Admin_View$get_moderatorRecommendedRemoveErrorP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeratorRecommendedRemoveErrorP');
    },
    get_moderatorRecommendedRemoveDoneP: function SpottedScript_Pages_Groups_Admin_View$get_moderatorRecommendedRemoveDoneP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeratorRecommendedRemoveDoneP');
    },
    get_memberPanel: function SpottedScript_Pages_Groups_Admin_View$get_memberPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberPanel');
    },
    get_h13: function SpottedScript_Pages_Groups_Admin_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_memberFavouriteCheckBox: function SpottedScript_Pages_Groups_Admin_View$get_memberFavouriteCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MemberFavouriteCheckBox');
    },
    get_genericContainerPage: function SpottedScript_Pages_Groups_Admin_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Groups.Admin.View.registerClass('SpottedScript.Pages.Groups.Admin.View', SpottedScript.DsiUserControl.View);
