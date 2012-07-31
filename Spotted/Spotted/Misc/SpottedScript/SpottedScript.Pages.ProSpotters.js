Type.registerNamespace('SpottedScript.Pages.ProSpotters');
SpottedScript.Pages.ProSpotters.View=function(clientId){SpottedScript.Pages.ProSpotters.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.ProSpotters.View.prototype={clientId:null,get_panelProSpotters:function(){return document.getElementById(this.clientId+'_PanelProSpotters');},get_h11:function(){return document.getElementById(this.clientId+'_H11');},get_proSpottersDataList:function(){return document.getElementById(this.clientId+'_ProSpottersDataList');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.ProSpotters.View.registerClass('SpottedScript.Pages.ProSpotters.View',SpottedScript.DsiUserControl.View);
