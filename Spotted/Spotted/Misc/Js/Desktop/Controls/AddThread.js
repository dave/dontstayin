// AddThread.js
(function($){
Type.registerNamespace('Js.Controls.AddThread');Js.Controls.AddThread.Controller=function(view){view.get_addThreadAdvancedCheckBoxJ().click(function($p1_0){
WhenLoggedIn(function(){
view.get_addThreadAdvancedPanel().style.display=(view.get_addThreadAdvancedCheckBox().checked)?'':'none';});});}
Js.Controls.AddThread.View=function(clientId){this.clientId=clientId;}
Js.Controls.AddThread.View.prototype={clientId:null,get_addThreadNotGroupMemberPanel:function(){if(this.$0==null){this.$0=document.getElementById(this.clientId+'_AddThreadNotGroupMemberPanel');}return this.$0;},$0:null,get_addThreadNotGroupMemberPanelJ:function(){if(this.$1==null){this.$1=$('#'+this.clientId+'_AddThreadNotGroupMemberPanel');}return this.$1;},$1:null,get_addThreadNotGroupMemberGroupPageAnchor:function(){if(this.$2==null){this.$2=document.getElementById(this.clientId+'_AddThreadNotGroupMemberGroupPageAnchor');}return this.$2;},$2:null,get_addThreadNotGroupMemberGroupPageAnchorJ:function(){if(this.$3==null){this.$3=$('#'+this.clientId+'_AddThreadNotGroupMemberGroupPageAnchor');}return this.$3;},$3:null,get_addThreadLoginPanel:function(){if(this.$4==null){this.$4=document.getElementById(this.clientId+'_AddThreadLoginPanel');}return this.$4;},$4:null,get_addThreadLoginPanelJ:function(){if(this.$5==null){this.$5=$('#'+this.clientId+'_AddThreadLoginPanel');}return this.$5;},$5:null,get_addThreadEmailVerifyPanel:function(){if(this.$6==null){this.$6=document.getElementById(this.clientId+'_AddThreadEmailVerifyPanel');}return this.$6;},$6:null,get_addThreadEmailVerifyPanelJ:function(){if(this.$7==null){this.$7=$('#'+this.clientId+'_AddThreadEmailVerifyPanel');}return this.$7;},$7:null,get_requiredfieldvalidator1:function(){if(this.$8==null){this.$8=document.getElementById(this.clientId+'_Requiredfieldvalidator1');}return this.$8;},$8:null,get_requiredfieldvalidator1J:function(){if(this.$9==null){this.$9=$('#'+this.clientId+'_Requiredfieldvalidator1');}return this.$9;},$9:null,get_requiredfieldvalidator2:function(){if(this.$A==null){this.$A=document.getElementById(this.clientId+'_Requiredfieldvalidator2');}return this.$A;},$A:null,get_requiredfieldvalidator2J:function(){if(this.$B==null){this.$B=$('#'+this.clientId+'_Requiredfieldvalidator2');}return this.$B;},$B:null,get_addThreadAdvancedPanel:function(){if(this.$C==null){this.$C=document.getElementById(this.clientId+'_AddThreadAdvancedPanel');}return this.$C;},$C:null,get_addThreadAdvancedPanelJ:function(){if(this.$D==null){this.$D=$('#'+this.clientId+'_AddThreadAdvancedPanel');}return this.$D;},$D:null,get_addThreadPublicRadioButtonSpan:function(){if(this.$E==null){this.$E=document.getElementById(this.clientId+'_AddThreadPublicRadioButtonSpan');}return this.$E;},$E:null,get_addThreadPublicRadioButtonSpanJ:function(){if(this.$F==null){this.$F=$('#'+this.clientId+'_AddThreadPublicRadioButtonSpan');}return this.$F;},$F:null,get_addThreadPrivateRadioButtonSpan:function(){if(this.$10==null){this.$10=document.getElementById(this.clientId+'_AddThreadPrivateRadioButtonSpan');}return this.$10;},$10:null,get_addThreadPrivateRadioButtonSpanJ:function(){if(this.$11==null){this.$11=$('#'+this.clientId+'_AddThreadPrivateRadioButtonSpan');}return this.$11;},$11:null,get_addThreadGroupRadioButtonSpan:function(){if(this.$12==null){this.$12=document.getElementById(this.clientId+'_AddThreadGroupRadioButtonSpan');}return this.$12;},$12:null,get_addThreadGroupRadioButtonSpanJ:function(){if(this.$13==null){this.$13=$('#'+this.clientId+'_AddThreadGroupRadioButtonSpan');}return this.$13;},$13:null,get_addThreadGroupDropDown:function(){if(this.$14==null){this.$14=document.getElementById(this.clientId+'_AddThreadGroupDropDown');}return this.$14;},$14:null,get_addThreadGroupDropDownJ:function(){if(this.$15==null){this.$15=$('#'+this.clientId+'_AddThreadGroupDropDown');}return this.$15;},$15:null,get_addThreadGroupPrivateCheckBoxSpan:function(){if(this.$16==null){this.$16=document.getElementById(this.clientId+'_AddThreadGroupPrivateCheckBoxSpan');}return this.$16;},$16:null,get_addThreadGroupPrivateCheckBoxSpanJ:function(){if(this.$17==null){this.$17=$('#'+this.clientId+'_AddThreadGroupPrivateCheckBoxSpan');}return this.$17;},$17:null,get_addThreadNewsCheckBoxSpan:function(){if(this.$18==null){this.$18=document.getElementById(this.clientId+'_AddThreadNewsCheckBoxSpan');}return this.$18;},$18:null,get_addThreadNewsCheckBoxSpanJ:function(){if(this.$19==null){this.$19=$('#'+this.clientId+'_AddThreadNewsCheckBoxSpan');}return this.$19;},$19:null,get_addThreadEventCheckBoxSpan:function(){if(this.$1A==null){this.$1A=document.getElementById(this.clientId+'_AddThreadEventCheckBoxSpan');}return this.$1A;},$1A:null,get_addThreadEventCheckBoxSpanJ:function(){if(this.$1B==null){this.$1B=$('#'+this.clientId+'_AddThreadEventCheckBoxSpan');}return this.$1B;},$1B:null,get_addThreadEventDropDown:function(){if(this.$1C==null){this.$1C=document.getElementById(this.clientId+'_AddThreadEventDropDown');}return this.$1C;},$1C:null,get_addThreadEventDropDownJ:function(){if(this.$1D==null){this.$1D=$('#'+this.clientId+'_AddThreadEventDropDown');}return this.$1D;},$1D:null,get_addThreadSealedCheckBoxSpan:function(){if(this.$1E==null){this.$1E=document.getElementById(this.clientId+'_AddThreadSealedCheckBoxSpan');}return this.$1E;},$1E:null,get_addThreadSealedCheckBoxSpanJ:function(){if(this.$1F==null){this.$1F=$('#'+this.clientId+'_AddThreadSealedCheckBoxSpan');}return this.$1F;},$1F:null,get_addThreadInviteCheckBoxSpan:function(){if(this.$20==null){this.$20=document.getElementById(this.clientId+'_AddThreadInviteCheckBoxSpan');}return this.$20;},$20:null,get_addThreadInviteCheckBoxSpanJ:function(){if(this.$21==null){this.$21=$('#'+this.clientId+'_AddThreadInviteCheckBoxSpan');}return this.$21;},$21:null,get_addThreadInvitePanel:function(){if(this.$22==null){this.$22=document.getElementById(this.clientId+'_AddThreadInvitePanel');}return this.$22;},$22:null,get_addThreadInvitePanelJ:function(){if(this.$23==null){this.$23=$('#'+this.clientId+'_AddThreadInvitePanel');}return this.$23;},$23:null,get_uiMultiBuddyChooser:function(){return eval(this.clientId+'_uiMultiBuddyChooserController');},get_inlineScript1:function(){if(this.$24==null){this.$24=document.getElementById(this.clientId+'_InlineScript1');}return this.$24;},$24:null,get_inlineScript1J:function(){if(this.$25==null){this.$25=$('#'+this.clientId+'_InlineScript1');}return this.$25;},$25:null,get_addThreadSubjectTextBox:function(){if(this.$26==null){this.$26=document.getElementById(this.clientId+'_AddThreadSubjectTextBox');}return this.$26;},$26:null,get_addThreadSubjectTextBoxJ:function(){if(this.$27==null){this.$27=$('#'+this.clientId+'_AddThreadSubjectTextBox');}return this.$27;},$27:null,get_commentHtml:function(){return eval(this.clientId+'_CommentHtmlController');},get_addThreadPublicRadioButton:function(){if(this.$28==null){this.$28=document.getElementById(this.clientId+'_AddThreadPublicRadioButton');}return this.$28;},$28:null,get_addThreadPublicRadioButtonJ:function(){if(this.$29==null){this.$29=$('#'+this.clientId+'_AddThreadPublicRadioButton');}return this.$29;},$29:null,get_addThreadPrivateRadioButton:function(){if(this.$2A==null){this.$2A=document.getElementById(this.clientId+'_AddThreadPrivateRadioButton');}return this.$2A;},$2A:null,get_addThreadPrivateRadioButtonJ:function(){if(this.$2B==null){this.$2B=$('#'+this.clientId+'_AddThreadPrivateRadioButton');}return this.$2B;},$2B:null,get_addThreadGroupRadioButton:function(){if(this.$2C==null){this.$2C=document.getElementById(this.clientId+'_AddThreadGroupRadioButton');}return this.$2C;},$2C:null,get_addThreadGroupRadioButtonJ:function(){if(this.$2D==null){this.$2D=$('#'+this.clientId+'_AddThreadGroupRadioButton');}return this.$2D;},$2D:null,get_addThreadAdvancedCheckBox:function(){if(this.$2E==null){this.$2E=document.getElementById(this.clientId+'_AddThreadAdvancedCheckBox');}return this.$2E;},$2E:null,get_addThreadAdvancedCheckBoxJ:function(){if(this.$2F==null){this.$2F=$('#'+this.clientId+'_AddThreadAdvancedCheckBox');}return this.$2F;},$2F:null,get_addThreadGroupPrivateCheckBox:function(){if(this.$30==null){this.$30=document.getElementById(this.clientId+'_AddThreadGroupPrivateCheckBox');}return this.$30;},$30:null,get_addThreadGroupPrivateCheckBoxJ:function(){if(this.$31==null){this.$31=$('#'+this.clientId+'_AddThreadGroupPrivateCheckBox');}return this.$31;},$31:null,get_addThreadEventCheckBox:function(){if(this.$32==null){this.$32=document.getElementById(this.clientId+'_AddThreadEventCheckBox');}return this.$32;},$32:null,get_addThreadEventCheckBoxJ:function(){if(this.$33==null){this.$33=$('#'+this.clientId+'_AddThreadEventCheckBox');}return this.$33;},$33:null,get_addThreadSealedCheckBox:function(){if(this.$34==null){this.$34=document.getElementById(this.clientId+'_AddThreadSealedCheckBox');}return this.$34;},$34:null,get_addThreadSealedCheckBoxJ:function(){if(this.$35==null){this.$35=$('#'+this.clientId+'_AddThreadSealedCheckBox');}return this.$35;},$35:null,get_addThreadNewsCheckBox:function(){if(this.$36==null){this.$36=document.getElementById(this.clientId+'_AddThreadNewsCheckBox');}return this.$36;},$36:null,get_addThreadNewsCheckBoxJ:function(){if(this.$37==null){this.$37=$('#'+this.clientId+'_AddThreadNewsCheckBox');}return this.$37;},$37:null,get_addThreadInviteCheckBox:function(){if(this.$38==null){this.$38=document.getElementById(this.clientId+'_AddThreadInviteCheckBox');}return this.$38;},$38:null,get_addThreadInviteCheckBoxJ:function(){if(this.$39==null){this.$39=$('#'+this.clientId+'_AddThreadInviteCheckBox');}return this.$39;},$39:null}
Js.Controls.AddThread.Controller.registerClass('Js.Controls.AddThread.Controller');Js.Controls.AddThread.View.registerClass('Js.Controls.AddThread.View');})(jQuery);// This script was generated using Script# v0.7.4.0
