Type.registerNamespace('SpottedScript.Pages.Venues.GoogleMap');
SpottedScript.Pages.Venues.GoogleMap.View=function(clientId){SpottedScript.Pages.Venues.GoogleMap.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Venues.GoogleMap.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Venues.GoogleMap.View.registerClass('SpottedScript.Pages.Venues.GoogleMap.View',SpottedScript.DsiUserControl.View);
