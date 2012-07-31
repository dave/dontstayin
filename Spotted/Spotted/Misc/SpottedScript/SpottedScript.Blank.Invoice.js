Type.registerNamespace('SpottedScript.Blank.Invoice');
SpottedScript.Blank.Invoice.View=function(clientId){SpottedScript.Blank.Invoice.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Invoice.View.prototype={clientId:null,get_itemsDataGrid:function(){return document.getElementById(this.clientId+'_ItemsDataGrid');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Invoice.View.registerClass('SpottedScript.Blank.Invoice.View',SpottedScript.BlankUserControl.View);
