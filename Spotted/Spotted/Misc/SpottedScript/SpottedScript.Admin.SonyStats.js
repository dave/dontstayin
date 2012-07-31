Type.registerNamespace('SpottedScript.Admin.SonyStats');
SpottedScript.Admin.SonyStats.View=function(clientId){SpottedScript.Admin.SonyStats.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.SonyStats.View.prototype={clientId:null,get_mainDiv:function(){return document.getElementById(this.clientId+'_MainDiv');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.SonyStats.View.registerClass('SpottedScript.Admin.SonyStats.View',SpottedScript.DsiUserControl.View);
