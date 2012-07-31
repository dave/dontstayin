Type.registerNamespace('SpottedScript.Blank.MailingReport');
SpottedScript.Blank.MailingReport.View=function(clientId){SpottedScript.Blank.MailingReport.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.MailingReport.View.prototype={clientId:null,get_button2:function(){return document.getElementById(this.clientId+'_Button2');},get_pass:function(){return document.getElementById(this.clientId+'_Pass');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.MailingReport.View.registerClass('SpottedScript.Blank.MailingReport.View',SpottedScript.BlankUserControl.View);
