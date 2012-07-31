Type.registerNamespace('SpottedScript.Admin.Mailing');
SpottedScript.Admin.Mailing.View=function(clientId){SpottedScript.Admin.Mailing.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.Mailing.View.prototype={clientId:null,get_titleLabel:function(){return document.getElementById(this.clientId+'_TitleLabel');},get_doneLabel:function(){return document.getElementById(this.clientId+'_DoneLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.Mailing.View.registerClass('SpottedScript.Admin.Mailing.View',SpottedScript.AdminUserControl.View);
