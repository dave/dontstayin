Type.registerNamespace('SpottedScript.Blank.SettingsRefresh');
SpottedScript.Blank.SettingsRefresh.View=function(clientId){SpottedScript.Blank.SettingsRefresh.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.SettingsRefresh.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.SettingsRefresh.View.registerClass('SpottedScript.Blank.SettingsRefresh.View',SpottedScript.BlankUserControl.View);
