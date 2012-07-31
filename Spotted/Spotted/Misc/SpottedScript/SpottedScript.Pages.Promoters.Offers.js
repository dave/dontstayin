Type.registerNamespace('SpottedScript.Pages.Promoters.Offers');
SpottedScript.Pages.Promoters.Offers.View=function(clientId){SpottedScript.Pages.Promoters.Offers.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Promoters.Offers.View.prototype={clientId:null,get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Promoters.Offers.View.registerClass('SpottedScript.Pages.Promoters.Offers.View',SpottedScript.DsiUserControl.View);
