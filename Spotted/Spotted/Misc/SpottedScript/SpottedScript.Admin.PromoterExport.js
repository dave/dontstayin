Type.registerNamespace('SpottedScript.Admin.PromoterExport');
SpottedScript.Admin.PromoterExport.View=function(clientId){SpottedScript.Admin.PromoterExport.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.PromoterExport.View.prototype={clientId:null,get_button1:function(){return document.getElementById(this.clientId+'_Button1');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.PromoterExport.View.registerClass('SpottedScript.Admin.PromoterExport.View',SpottedScript.AdminUserControl.View);
