Type.registerNamespace('SpottedScript.Pages.Faq');
SpottedScript.Pages.Faq.View=function(clientId){SpottedScript.Pages.Faq.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Faq.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Faq.View.registerClass('SpottedScript.Pages.Faq.View',SpottedScript.DsiUserControl.View);
