Type.registerNamespace('SpottedScript.Blank.BannerEditLocation');
SpottedScript.Blank.BannerEditLocation.View=function(clientId){SpottedScript.Blank.BannerEditLocation.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.BannerEditLocation.View.prototype={clientId:null,get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_uiPlacesChooser:function(){return eval(this.clientId+'_uiPlacesChooserController');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.BannerEditLocation.View.registerClass('SpottedScript.Blank.BannerEditLocation.View',SpottedScript.BlankUserControl.View);
