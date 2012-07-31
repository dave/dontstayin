Type.registerNamespace('SpottedScript.Pages.HotTickets');
SpottedScript.Pages.HotTickets.View=function(clientId){SpottedScript.Pages.HotTickets.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.HotTickets.View.prototype={clientId:null,get_calendar:function(){return document.getElementById(this.clientId+'_Calendar');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.HotTickets.View.registerClass('SpottedScript.Pages.HotTickets.View',SpottedScript.DsiUserControl.View);
