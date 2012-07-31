Type.registerNamespace('SpottedScript.Pages.IbizaHoliday');
SpottedScript.Pages.IbizaHoliday.View=function(clientId){SpottedScript.Pages.IbizaHoliday.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.IbizaHoliday.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.IbizaHoliday.View.registerClass('SpottedScript.Pages.IbizaHoliday.View',SpottedScript.DsiUserControl.View);
