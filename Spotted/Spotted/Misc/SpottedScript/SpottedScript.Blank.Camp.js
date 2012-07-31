Type.registerNamespace('SpottedScript.Blank.Camp');
SpottedScript.Blank.Camp.View=function(clientId){SpottedScript.Blank.Camp.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Camp.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Camp.View.registerClass('SpottedScript.Blank.Camp.View',SpottedScript.DsiUserControl.View);
