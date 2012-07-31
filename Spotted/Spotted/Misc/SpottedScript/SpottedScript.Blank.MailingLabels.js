Type.registerNamespace('SpottedScript.Blank.MailingLabels');
SpottedScript.Blank.MailingLabels.View=function(clientId){SpottedScript.Blank.MailingLabels.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.MailingLabels.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.MailingLabels.View.registerClass('SpottedScript.Blank.MailingLabels.View',SpottedScript.BlankUserControl.View);
