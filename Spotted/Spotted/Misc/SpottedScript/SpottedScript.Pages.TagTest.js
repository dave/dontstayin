Type.registerNamespace('SpottedScript.Pages.TagTest');
SpottedScript.Pages.TagTest.View=function(clientId){SpottedScript.Pages.TagTest.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.TagTest.View.prototype={clientId:null,get_tagOut:function(){return document.getElementById(this.clientId+'_TagOut');},get_tagIn:function(){return document.getElementById(this.clientId+'_TagIn');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.TagTest.View.registerClass('SpottedScript.Pages.TagTest.View',SpottedScript.DsiUserControl.View);
