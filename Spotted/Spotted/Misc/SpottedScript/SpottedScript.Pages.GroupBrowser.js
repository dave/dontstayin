Type.registerNamespace('SpottedScript.Pages.GroupBrowser');
SpottedScript.Pages.GroupBrowser.View=function(clientId){SpottedScript.Pages.GroupBrowser.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.GroupBrowser.View.prototype={clientId:null,get_panelGroups:function(){return document.getElementById(this.clientId+'_PanelGroups');},get_groupsDataList:function(){return document.getElementById(this.clientId+'_GroupsDataList');},get_header:function(){return document.getElementById(this.clientId+'_Header');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.GroupBrowser.View.registerClass('SpottedScript.Pages.GroupBrowser.View',SpottedScript.DsiUserControl.View);
