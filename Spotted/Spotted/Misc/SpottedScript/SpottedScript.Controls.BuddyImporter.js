Type.registerNamespace('SpottedScript.Controls.BuddyImporter');
SpottedScript.Controls.BuddyImporter.View=function(clientId){this.clientId=clientId;}
SpottedScript.Controls.BuddyImporter.View.prototype={clientId:null,get_uiEmailCredentialsPanel:function(){return document.getElementById(this.clientId+'_uiEmailCredentialsPanel');},get_uiEmailText:function(){return document.getElementById(this.clientId+'_uiEmailText');},get_uiEmailProviderDropDown:function(){return document.getElementById(this.clientId+'_uiEmailProviderDropDown');},get_uiPasswordText:function(){return document.getElementById(this.clientId+'_uiPasswordText');},get_uiErrorBadCredentialsLabel:function(){return document.getElementById(this.clientId+'_uiErrorBadCredentialsLabel');},get_uiErrorUnknownEmailProvider:function(){return document.getElementById(this.clientId+'_uiErrorUnknownEmailProvider');},get_uiGetEmailContactsButton:function(){return document.getElementById(this.clientId+'_uiGetEmailContactsButton');},get_uiSelectContactsPanel:function(){return document.getElementById(this.clientId+'_uiSelectContactsPanel');},get_uiAlreadyBuddiesLabel:function(){return document.getElementById(this.clientId+'_uiAlreadyBuddiesLabel');},get_uiNonBuddyMembersLabel:function(){return document.getElementById(this.clientId+'_uiNonBuddyMembersLabel');},get_uiToggleSelectAllMemberContactsCheckBox:function(){return document.getElementById(this.clientId+'_uiToggleSelectAllMemberContactsCheckBox');},get_uiSelectMemberContactsDiv:function(){return document.getElementById(this.clientId+'_uiSelectMemberContactsDiv');},get_uiSelectMemberContactsGridView:function(){return document.getElementById(this.clientId+'_uiSelectMemberContactsGridView');},get_uiNonMembersLabel:function(){return document.getElementById(this.clientId+'_uiNonMembersLabel');},get_uiToggleSelectAllNonMemberContactsCheckBox:function(){return document.getElementById(this.clientId+'_uiToggleSelectAllNonMemberContactsCheckBox');},get_uiSelectNonMemberContactsDiv:function(){return document.getElementById(this.clientId+'_uiSelectNonMemberContactsDiv');},get_uiSelectNonMemberContactsGridView:function(){return document.getElementById(this.clientId+'_uiSelectNonMemberContactsGridView');},get_uiSuccess:function(){return document.getElementById(this.clientId+'_uiSuccess');},get_uiNoContactsAddedLabel:function(){return document.getElementById(this.clientId+'_uiNoContactsAddedLabel');},get_uiBuddiesRequestedList:function(){return document.getElementById(this.clientId+'_uiBuddiesRequestedList');},get_uiEmailsSentList:function(){return document.getElementById(this.clientId+'_uiEmailsSentList');}}
SpottedScript.Controls.BuddyImporter.View.registerClass('SpottedScript.Controls.BuddyImporter.View');
