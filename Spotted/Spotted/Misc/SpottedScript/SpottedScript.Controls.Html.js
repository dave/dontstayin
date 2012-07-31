Type.registerNamespace('SpottedScript.Controls.Html');
SpottedScript.Controls.Html.Controller=function(view){this.$0=view;if(view.get_linkUrlButton()!=null){$addHandler(view.get_linkUrlButton(),'click',Function.createDelegate(this,this.$5));$addHandler(view.get_linkUrlPanelBackButton(),'click',Function.createDelegate(this,this.$6));$addHandler(view.get_flashSwfUrlButton(),'click',Function.createDelegate(this,this.$8));$addHandler(view.get_flashSwfUrlPanelBackButton(),'click',Function.createDelegate(this,this.$9));$addHandler(view.get_videoFlvButton(),'click',Function.createDelegate(this,this.$B));$addHandler(view.get_videoFlvPanelBackButton(),'click',Function.createDelegate(this,this.$C));$addHandler(view.get_advancedTagsToggleButton(),'click',Function.createDelegate(this,this.$E));$addHandler(view.get_advancedParseNowButton(),'click',Function.createDelegate(this,this.$F));$addHandler(view.get_previewButton(),'click',Function.createDelegate(this,this.$11));$addHandler(view.get_hidePreviewButton(),'click',Function.createDelegate(this,this.$13));}}
SpottedScript.Controls.Html.Controller.prototype={$0:null,get_$1:function(){return this.$0.get_saveButton();},get_$2:function(){return this.$0.get_htmlTextBox().value;},get_$3:function(){return this.$0.get_advancedFormattingTrueRadio().checked;},$4:function(){this.$0.get_htmlTextBox().value='';},$5:function($p0){$p0.preventDefault();this.$7(true);},$6:function($p0){$p0.preventDefault();this.$7(false);},$7:function($p0){this.$0.get_linkMainPanel().style.display=($p0)?'none':'';this.$0.get_linkUrlPanel().style.display=($p0)?'':'none';},$8:function($p0){$p0.preventDefault();this.$A(true);},$9:function($p0){$p0.preventDefault();this.$A(false);},$A:function($p0){this.$0.get_flashMainPanel().style.display=($p0)?'none':'';this.$0.get_flashSwfUrlPanel().style.display=($p0)?'':'none';},$B:function($p0){$p0.preventDefault();this.$D(true);},$C:function($p0){$p0.preventDefault();this.$D(false);},$D:function($p0){this.$0.get_videoMainPanel().style.display=($p0)?'none':'';this.$0.get_videoFlvPanel().style.display=($p0)?'':'none';},$E:function($p0){$p0.preventDefault();this.$0.get_advancedTagsPanel().style.display=(this.$0.get_advancedTagsPanel().style.display==='none')?'':'none';},$F:function($p0){$p0.preventDefault();SpottedScript.Misc.showWaitingCursor();Spotted.WebServices.Controls.CommentsDisplay.Service.cleanHtml(this.$0.get_htmlTextBox().value,Function.createDelegate(this,this.$10),null,null,-1);},$10:function($p0,$p1,$p2){SpottedScript.Misc.hideWaitingCursor();this.$0.get_htmlTextBox().value=$p0;},$11:function($p0){$p0.preventDefault();WhenLoggedIn(Function.createDelegate(this,function(){
Spotted.WebServices.Controls.CommentsDisplay.Service.getPreviewHtml((this.$0.get_uiPreviewType().value!=='')?Number.parseInvariant(this.$0.get_uiPreviewType().value):0,this.$0.get_htmlTextBox().value,this.get_$3(),Function.createDelegate(this,this.$12),null,null,-1);}));},$12:function($p0,$p1,$p2){this.$0.get_previewPanel().innerHTML=$p0[0];eval($p0[1]);this.$0.get_hidePreviewButton().style.display='';this.$0.get_previewButton().innerHTML='Update preview';this.$0.get_previewPanelContainer().style.display='';},$13:function($p0){$p0.preventDefault();this.$0.get_previewButton().innerHTML='Preview';this.$0.get_hidePreviewButton().style.display='none';this.$0.get_previewPanelContainer().style.display='none';}}
SpottedScript.Controls.Html.View=function(clientId){SpottedScript.Controls.Html.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Controls.Html.View.prototype={clientId:null,get_helpersDiv:function(){return document.getElementById(this.clientId+'_HelpersDiv');},get_linkAnchor:function(){return document.getElementById(this.clientId+'_LinkAnchor');},get_imageAnchor:function(){return document.getElementById(this.clientId+'_ImageAnchor');},get_videoAnchor:function(){return document.getElementById(this.clientId+'_VideoAnchor');},get_mixmagAnchor:function(){return document.getElementById(this.clientId+'_MixmagAnchor');},get_flashAnchor:function(){return document.getElementById(this.clientId+'_FlashAnchor');},get_advancedAnchor:function(){return document.getElementById(this.clientId+'_AdvancedAnchor');},get_mixmagDiv:function(){return document.getElementById(this.clientId+'_MixmagDiv');},get_linkDiv:function(){return document.getElementById(this.clientId+'_LinkDiv');},get_linkMainPanel:function(){return document.getElementById(this.clientId+'_LinkMainPanel');},get_linkUrlButton:function(){return document.getElementById(this.clientId+'_LinkUrlButton');},get_linkUrlPanel:function(){return document.getElementById(this.clientId+'_LinkUrlPanel');},get_linkUrlPanelBackButton:function(){return document.getElementById(this.clientId+'_LinkUrlPanelBackButton');},get_linkUrlTextBox:function(){return document.getElementById(this.clientId+'_LinkUrlTextBox');},get_imageDiv:function(){return document.getElementById(this.clientId+'_ImageDiv');},get_imageMainPanel:function(){return document.getElementById(this.clientId+'_ImageMainPanel');},get_videoDiv:function(){return document.getElementById(this.clientId+'_VideoDiv');},get_videoMainPanel:function(){return document.getElementById(this.clientId+'_VideoMainPanel');},get_videoFlvButton:function(){return document.getElementById(this.clientId+'_VideoFlvButton');},get_videoFlvPanel:function(){return document.getElementById(this.clientId+'_VideoFlvPanel');},get_videoFlvPanelBackButton:function(){return document.getElementById(this.clientId+'_VideoFlvPanelBackButton');},get_videoFlvUrlTextBox:function(){return document.getElementById(this.clientId+'_VideoFlvUrlTextBox');},get_videoFlvWidthTextBox:function(){return document.getElementById(this.clientId+'_VideoFlvWidthTextBox');},get_videoFlvHeightTextBox:function(){return document.getElementById(this.clientId+'_VideoFlvHeightTextBox');},get_flashDiv:function(){return document.getElementById(this.clientId+'_FlashDiv');},get_flashMainPanel:function(){return document.getElementById(this.clientId+'_FlashMainPanel');},get_flashSwfUrlButton:function(){return document.getElementById(this.clientId+'_FlashSwfUrlButton');},get_flashSwfUrlPanel:function(){return document.getElementById(this.clientId+'_FlashSwfUrlPanel');},get_flashSwfUrlPanelBackButton:function(){return document.getElementById(this.clientId+'_FlashSwfUrlPanelBackButton');},get_flashSwfUrlUrlTextBox:function(){return document.getElementById(this.clientId+'_FlashSwfUrlUrlTextBox');},get_flashSwfUrlWidthTextBox:function(){return document.getElementById(this.clientId+'_FlashSwfUrlWidthTextBox');},get_flashSwfUrlHeightTextBox:function(){return document.getElementById(this.clientId+'_FlashSwfUrlHeightTextBox');},get_flashSwfUrlDrawDropDownList:function(){return document.getElementById(this.clientId+'_FlashSwfUrlDrawDropDownList');},get_advancedDiv:function(){return document.getElementById(this.clientId+'_AdvancedDiv');},get_advancedFormattingPanel:function(){return document.getElementById(this.clientId+'_AdvancedFormattingPanel');},get_advancedFormattingTrueRadio:function(){return document.getElementById(this.clientId+'_AdvancedFormattingTrueRadio');},get_advancedFormattingFalseRadio:function(){return document.getElementById(this.clientId+'_AdvancedFormattingFalseRadio');},get_advancedContainerPanel:function(){return document.getElementById(this.clientId+'_AdvancedContainerPanel');},get_advancedContainerTrueRadio:function(){return document.getElementById(this.clientId+'_AdvancedContainerTrueRadio');},get_advancedContainerFalseRadio:function(){return document.getElementById(this.clientId+'_AdvancedContainerFalseRadio');},get_advancedParseNowPanel:function(){return document.getElementById(this.clientId+'_AdvancedParseNowPanel');},get_advancedParseNowButton:function(){return document.getElementById(this.clientId+'_AdvancedParseNowButton');},get_advancedTagsToggleButton:function(){return document.getElementById(this.clientId+'_AdvancedTagsToggleButton');},get_advancedTagsPanel:function(){return document.getElementById(this.clientId+'_AdvancedTagsPanel');},get_textBoxDiv:function(){return document.getElementById(this.clientId+'_TextBoxDiv');},get_htmlTextBox:function(){return document.getElementById(this.clientId+'_HtmlTextBox');},get_disabledMessageDiv:function(){return document.getElementById(this.clientId+'_DisabledMessageDiv');},get_buttonsContainer:function(){return document.getElementById(this.clientId+'_ButtonsContainer');},get_saveDiv:function(){return document.getElementById(this.clientId+'_SaveDiv');},get_saveButton:function(){return document.getElementById(this.clientId+'_SaveButton');},get_previewButton:function(){return document.getElementById(this.clientId+'_PreviewButton');},get_hidePreviewButton:function(){return document.getElementById(this.clientId+'_HidePreviewButton');},get_previewPanelContainer:function(){return document.getElementById(this.clientId+'_PreviewPanelContainer');},get_previewPanel:function(){return document.getElementById(this.clientId+'_PreviewPanel');},get_uiEnabled:function(){return document.getElementById(this.clientId+'_uiEnabled');},get_uiPreviewType:function(){return document.getElementById(this.clientId+'_uiPreviewType');},get_helperPanelDisplayState:function(){return document.getElementById(this.clientId+'_HelperPanelDisplayState');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Controls.Html.Controller.registerClass('SpottedScript.Controls.Html.Controller');
SpottedScript.Controls.Html.View.registerClass('SpottedScript.Controls.Html.View',SpottedScript.DsiUserControl.View);
