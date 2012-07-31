Type.registerNamespace('SpottedScript.Pages.Offer');
SpottedScript.Pages.Offer.View=function(clientId){SpottedScript.Pages.Offer.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Offer.View.prototype={clientId:null,get_panelEnd:function(){return document.getElementById(this.clientId+'_PanelEnd');},get_h5:function(){return document.getElementById(this.clientId+'_H5');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Offer.View.registerClass('SpottedScript.Pages.Offer.View',SpottedScript.DsiUserControl.View);
