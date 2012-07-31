Type.registerNamespace('SpottedScript.Blank.BusinessPlanOutput');
SpottedScript.Blank.BusinessPlanOutput.View=function(clientId){SpottedScript.Blank.BusinessPlanOutput.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.BusinessPlanOutput.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.BusinessPlanOutput.View.registerClass('SpottedScript.Blank.BusinessPlanOutput.View',SpottedScript.BlankUserControl.View);
