Type.registerNamespace('SpottedScript.Pages.Photos.Send');
SpottedScript.Pages.Photos.Send.View=function(clientId){SpottedScript.Pages.Photos.Send.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Photos.Send.View.prototype={clientId:null,get_h11:function(){return document.getElementById(this.clientId+'_H11');},get_photoAnchor:function(){return document.getElementById(this.clientId+'_PhotoAnchor');},get_photoImg:function(){return document.getElementById(this.clientId+'_PhotoImg');},get_requiredFieldValidator1:function(){return document.getElementById(this.clientId+'_RequiredFieldValidator1');},get_messageHtml:function(){return eval(this.clientId+'_MessageHtmlController');},get_buddyPanel:function(){return document.getElementById(this.clientId+'_BuddyPanel');},get_multiBuddyChooser:function(){return eval(this.clientId+'_MultiBuddyChooserController');},get_button1:function(){return document.getElementById(this.clientId+'_Button1');},get_sentEmailsLabel:function(){return document.getElementById(this.clientId+'_SentEmailsLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Photos.Send.View.registerClass('SpottedScript.Pages.Photos.Send.View',SpottedScript.DsiUserControl.View);
