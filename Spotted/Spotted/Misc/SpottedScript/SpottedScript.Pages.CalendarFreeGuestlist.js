Type.registerNamespace('SpottedScript.Pages.CalendarFreeGuestlist');
SpottedScript.Pages.CalendarFreeGuestlist.View=function(clientId){SpottedScript.Pages.CalendarFreeGuestlist.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.CalendarFreeGuestlist.View.prototype={clientId:null,get_calendarFreeGuestlistContent:function(){return document.getElementById(this.clientId+'_CalendarFreeGuestlistContent');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.CalendarFreeGuestlist.View.registerClass('SpottedScript.Pages.CalendarFreeGuestlist.View',SpottedScript.DsiUserControl.View);
