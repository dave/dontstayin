Type.registerNamespace('SpottedScript.Pages.MyCalendar');
SpottedScript.Pages.MyCalendar.View=function(clientId){SpottedScript.Pages.MyCalendar.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.MyCalendar.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.MyCalendar.View.registerClass('SpottedScript.Pages.MyCalendar.View',SpottedScript.DsiUserControl.View);
