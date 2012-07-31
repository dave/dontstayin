Type.registerNamespace('SpottedScript.Pages.Blank');
SpottedScript.Pages.Blank.Controller=function(v){}
SpottedScript.Pages.Blank.Controller.prototype={view:null,buttonClick:function(e){}}
SpottedScript.Pages.Blank.View=function(clientId){SpottedScript.Pages.Blank.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Blank.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Blank.Controller.registerClass('SpottedScript.Pages.Blank.Controller');
SpottedScript.Pages.Blank.View.registerClass('SpottedScript.Pages.Blank.View',SpottedScript.DsiUserControl.View);
