Type.registerNamespace('SpottedScript.Blank.ReportGenerator');
SpottedScript.Blank.ReportGenerator.View=function(clientId){SpottedScript.Blank.ReportGenerator.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.ReportGenerator.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.ReportGenerator.View.registerClass('SpottedScript.Blank.ReportGenerator.View',SpottedScript.BlankUserControl.View);
