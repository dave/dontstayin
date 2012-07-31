Type.registerNamespace('SpottedScript.Pages.MyVenues');
SpottedScript.Pages.MyVenues.View=function(clientId){SpottedScript.Pages.MyVenues.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.MyVenues.View.prototype={clientId:null,get_h11:function(){return document.getElementById(this.clientId+'_H11');},get_venuesPanel:function(){return document.getElementById(this.clientId+'_VenuesPanel');},get_venuesDataGrid:function(){return document.getElementById(this.clientId+'_VenuesDataGrid');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.MyVenues.View.registerClass('SpottedScript.Pages.MyVenues.View',SpottedScript.DsiUserControl.View);
