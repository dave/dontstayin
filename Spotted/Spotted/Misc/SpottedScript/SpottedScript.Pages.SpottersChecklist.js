Type.registerNamespace('SpottedScript.Pages.SpottersChecklist');
SpottedScript.Pages.SpottersChecklist.View=function(clientId){SpottedScript.Pages.SpottersChecklist.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.SpottersChecklist.View.prototype={clientId:null,get_h19:function(){return document.getElementById(this.clientId+'_H19');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.SpottersChecklist.View.registerClass('SpottedScript.Pages.SpottersChecklist.View',SpottedScript.DsiUserControl.View);
