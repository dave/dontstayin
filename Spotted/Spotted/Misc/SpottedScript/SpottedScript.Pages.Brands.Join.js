Type.registerNamespace('SpottedScript.Pages.Brands.Join');
SpottedScript.Pages.Brands.Join.View=function(clientId){SpottedScript.Pages.Brands.Join.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Brands.Join.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Brands.Join.View.registerClass('SpottedScript.Pages.Brands.Join.View',SpottedScript.DsiUserControl.View);
