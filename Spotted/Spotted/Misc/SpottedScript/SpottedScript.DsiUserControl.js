Type.registerNamespace('SpottedScript.DsiUserControl');
SpottedScript.DsiUserControl.View=function(clientId){SpottedScript.DsiUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.DsiUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.DsiUserControl.View.registerClass('SpottedScript.DsiUserControl.View',SpottedScript.GenericUserControl.View);
