Type.registerNamespace('SpottedScript.Admin.WeightedPages');
SpottedScript.Admin.WeightedPages.View=function(clientId){SpottedScript.Admin.WeightedPages.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.WeightedPages.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.WeightedPages.View.registerClass('SpottedScript.Admin.WeightedPages.View',SpottedScript.AdminUserControl.View);
