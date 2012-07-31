Type.registerNamespace('SpottedScript.Blank.CheckBannerBlank');
SpottedScript.Blank.CheckBannerBlank.View=function(clientId){SpottedScript.Blank.CheckBannerBlank.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.CheckBannerBlank.View.prototype={clientId:null,get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.CheckBannerBlank.View.registerClass('SpottedScript.Blank.CheckBannerBlank.View',SpottedScript.BlankUserControl.View);
