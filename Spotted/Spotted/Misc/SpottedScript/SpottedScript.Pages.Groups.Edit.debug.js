Type.registerNamespace('SpottedScript.Pages.Groups.Edit');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Groups.Edit.View
SpottedScript.Pages.Groups.Edit.View = function SpottedScript_Pages_Groups_Edit_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Groups.Edit.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Groups.Edit.View.prototype = {
    clientId: null,
    get_header: function SpottedScript_Pages_Groups_Edit_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_validationsummary6: function SpottedScript_Pages_Groups_Edit_View$get_validationsummary6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary6');
    },
    get_customvalidator1: function SpottedScript_Pages_Groups_Edit_View$get_customvalidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator1');
    },
    get_button1: function SpottedScript_Pages_Groups_Edit_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_button9: function SpottedScript_Pages_Groups_Edit_View$get_button9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button9');
    },
    get_h11: function SpottedScript_Pages_Groups_Edit_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_validationsummary2: function SpottedScript_Pages_Groups_Edit_View$get_validationsummary2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary2');
    },
    get_customvalidator2: function SpottedScript_Pages_Groups_Edit_View$get_customvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator2');
    },
    get_button2: function SpottedScript_Pages_Groups_Edit_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_button10: function SpottedScript_Pages_Groups_Edit_View$get_button10() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button10');
    },
    get_h12: function SpottedScript_Pages_Groups_Edit_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_validationsummary1: function SpottedScript_Pages_Groups_Edit_View$get_validationsummary1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary1');
    },
    get_customvalidator3: function SpottedScript_Pages_Groups_Edit_View$get_customvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator3');
    },
    get_button3: function SpottedScript_Pages_Groups_Edit_View$get_button3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button3');
    },
    get_button4: function SpottedScript_Pages_Groups_Edit_View$get_button4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button4');
    },
    get_button11: function SpottedScript_Pages_Groups_Edit_View$get_button11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button11');
    },
    get_h13: function SpottedScript_Pages_Groups_Edit_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_validationsummary3: function SpottedScript_Pages_Groups_Edit_View$get_validationsummary3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary3');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_Groups_Edit_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_regularexpressionvalidator1: function SpottedScript_Pages_Groups_Edit_View$get_regularexpressionvalidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Regularexpressionvalidator1');
    },
    get_customvalidator4: function SpottedScript_Pages_Groups_Edit_View$get_customvalidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator4');
    },
    get_customvalidator5: function SpottedScript_Pages_Groups_Edit_View$get_customvalidator5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator5');
    },
    get_customvalidator99: function SpottedScript_Pages_Groups_Edit_View$get_customvalidator99() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator99');
    },
    get_requiredFieldValidator2: function SpottedScript_Pages_Groups_Edit_View$get_requiredFieldValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator2');
    },
    get_customvalidator6: function SpottedScript_Pages_Groups_Edit_View$get_customvalidator6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator6');
    },
    get_customValidator7: function SpottedScript_Pages_Groups_Edit_View$get_customValidator7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator7');
    },
    get_introHtml: function SpottedScript_Pages_Groups_Edit_View$get_introHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_IntroHtmlController');
    },
    get_button5: function SpottedScript_Pages_Groups_Edit_View$get_button5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button5');
    },
    get_button6: function SpottedScript_Pages_Groups_Edit_View$get_button6() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button6');
    },
    get_button7: function SpottedScript_Pages_Groups_Edit_View$get_button7() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button7');
    },
    get_h14: function SpottedScript_Pages_Groups_Edit_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_validationsummary4: function SpottedScript_Pages_Groups_Edit_View$get_validationsummary4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary4');
    },
    get_p1: function SpottedScript_Pages_Groups_Edit_View$get_p1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_P1');
    },
    get_customvalidator8: function SpottedScript_Pages_Groups_Edit_View$get_customvalidator8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator8');
    },
    get_button8: function SpottedScript_Pages_Groups_Edit_View$get_button8() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button8');
    },
    get_button12: function SpottedScript_Pages_Groups_Edit_View$get_button12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button12');
    },
    get_button13: function SpottedScript_Pages_Groups_Edit_View$get_button13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button13');
    },
    get_h15: function SpottedScript_Pages_Groups_Edit_View$get_h15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15');
    },
    get_validationsummary5: function SpottedScript_Pages_Groups_Edit_View$get_validationsummary5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Validationsummary5');
    },
    get_customvalidator9: function SpottedScript_Pages_Groups_Edit_View$get_customvalidator9() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator9');
    },
    get_button14: function SpottedScript_Pages_Groups_Edit_View$get_button14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button14');
    },
    get_button15: function SpottedScript_Pages_Groups_Edit_View$get_button15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button15');
    },
    get_button16: function SpottedScript_Pages_Groups_Edit_View$get_button16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button16');
    },
    get_h16: function SpottedScript_Pages_Groups_Edit_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_optionsGroupAnchor: function SpottedScript_Pages_Groups_Edit_View$get_optionsGroupAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionsGroupAnchor');
    },
    get_optionsMenuP: function SpottedScript_Pages_Groups_Edit_View$get_optionsMenuP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionsMenuP');
    },
    get_optionsInviteP: function SpottedScript_Pages_Groups_Edit_View$get_optionsInviteP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OptionsInviteP');
    },
    get_editOptionsP: function SpottedScript_Pages_Groups_Edit_View$get_editOptionsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditOptionsP');
    },
    get_groupIntro: function SpottedScript_Pages_Groups_Edit_View$get_groupIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupIntro');
    },
    get_panelTheme: function SpottedScript_Pages_Groups_Edit_View$get_panelTheme() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelTheme');
    },
    get_panelThemeSaveP: function SpottedScript_Pages_Groups_Edit_View$get_panelThemeSaveP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThemeSaveP');
    },
    get_themesRadioButtonList: function SpottedScript_Pages_Groups_Edit_View$get_themesRadioButtonList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThemesRadioButtonList');
    },
    get_panelLocation: function SpottedScript_Pages_Groups_Edit_View$get_panelLocation() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelLocation');
    },
    get_locationCountryDropDown: function SpottedScript_Pages_Groups_Edit_View$get_locationCountryDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LocationCountryDropDown');
    },
    get_locationPlaceDropDown: function SpottedScript_Pages_Groups_Edit_View$get_locationPlaceDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LocationPlaceDropDown');
    },
    get_locationTypeNone: function SpottedScript_Pages_Groups_Edit_View$get_locationTypeNone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LocationTypeNone');
    },
    get_locationTypeCountry: function SpottedScript_Pages_Groups_Edit_View$get_locationTypeCountry() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LocationTypeCountry');
    },
    get_locationTypePlace: function SpottedScript_Pages_Groups_Edit_View$get_locationTypePlace() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LocationTypePlace');
    },
    get_locationCountryP: function SpottedScript_Pages_Groups_Edit_View$get_locationCountryP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LocationCountryP');
    },
    get_locationPlaceP: function SpottedScript_Pages_Groups_Edit_View$get_locationPlaceP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LocationPlaceP');
    },
    get_panelLocationBackButton: function SpottedScript_Pages_Groups_Edit_View$get_panelLocationBackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelLocationBackButton');
    },
    get_panelLocationSaveP: function SpottedScript_Pages_Groups_Edit_View$get_panelLocationSaveP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelLocationSaveP');
    },
    get_panelMusicType: function SpottedScript_Pages_Groups_Edit_View$get_panelMusicType() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelMusicType');
    },
    get_musicTypesRadioButtonList: function SpottedScript_Pages_Groups_Edit_View$get_musicTypesRadioButtonList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicTypesRadioButtonList');
    },
    get_panelMusicTypeSaveP: function SpottedScript_Pages_Groups_Edit_View$get_panelMusicTypeSaveP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelMusicTypeSaveP');
    },
    get_panelDetails: function SpottedScript_Pages_Groups_Edit_View$get_panelDetails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetails');
    },
    get_namePanel: function SpottedScript_Pages_Groups_Edit_View$get_namePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NamePanel');
    },
    get_nameTextBox: function SpottedScript_Pages_Groups_Edit_View$get_nameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NameTextBox');
    },
    get_descriptionTextBox: function SpottedScript_Pages_Groups_Edit_View$get_descriptionTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DescriptionTextBox');
    },
    get_rulesTextBox: function SpottedScript_Pages_Groups_Edit_View$get_rulesTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RulesTextBox');
    },
    get_panelDetailsSaveP: function SpottedScript_Pages_Groups_Edit_View$get_panelDetailsSaveP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetailsSaveP');
    },
    get_panelMembership: function SpottedScript_Pages_Groups_Edit_View$get_panelMembership() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelMembership');
    },
    get_membershipPublic: function SpottedScript_Pages_Groups_Edit_View$get_membershipPublic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MembershipPublic');
    },
    get_membershipMember: function SpottedScript_Pages_Groups_Edit_View$get_membershipMember() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MembershipMember');
    },
    get_membershipModerator: function SpottedScript_Pages_Groups_Edit_View$get_membershipModerator() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MembershipModerator');
    },
    get_panelMembershipSaveP: function SpottedScript_Pages_Groups_Edit_View$get_panelMembershipSaveP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelMembershipSaveP');
    },
    get_panelPrivate: function SpottedScript_Pages_Groups_Edit_View$get_panelPrivate() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPrivate');
    },
    get_groupPagePublic: function SpottedScript_Pages_Groups_Edit_View$get_groupPagePublic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupPagePublic');
    },
    get_groupPagePrivate: function SpottedScript_Pages_Groups_Edit_View$get_groupPagePrivate() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupPagePrivate');
    },
    get_chatForumPublic: function SpottedScript_Pages_Groups_Edit_View$get_chatForumPublic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatForumPublic');
    },
    get_chatForumPrivate: function SpottedScript_Pages_Groups_Edit_View$get_chatForumPrivate() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatForumPrivate');
    },
    get_membersListPublic: function SpottedScript_Pages_Groups_Edit_View$get_membersListPublic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MembersListPublic');
    },
    get_membersListPrivate: function SpottedScript_Pages_Groups_Edit_View$get_membersListPrivate() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MembersListPrivate');
    },
    get_membersListRadioSpan: function SpottedScript_Pages_Groups_Edit_View$get_membersListRadioSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MembersListRadioSpan');
    },
    get_chatForumRadioSpan: function SpottedScript_Pages_Groups_Edit_View$get_chatForumRadioSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatForumRadioSpan');
    },
    get_panelPrivateSaveP: function SpottedScript_Pages_Groups_Edit_View$get_panelPrivateSaveP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPrivateSaveP');
    },
    get_panelPic: function SpottedScript_Pages_Groups_Edit_View$get_panelPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPic');
    },
    get_groupPic: function SpottedScript_Pages_Groups_Edit_View$get_groupPic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupPic');
    },
    get_genericContainerPage: function SpottedScript_Pages_Groups_Edit_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Groups.Edit.View.registerClass('SpottedScript.Pages.Groups.Edit.View', SpottedScript.DsiUserControl.View);
