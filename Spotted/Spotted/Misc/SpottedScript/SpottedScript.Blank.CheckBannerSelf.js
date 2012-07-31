Type.registerNamespace('SpottedScript.Blank.CheckBannerSelf');
SpottedScript.Blank.CheckBannerSelf.View=function(clientId){SpottedScript.Blank.CheckBannerSelf.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.CheckBannerSelf.View.prototype={clientId:null,get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.CheckBannerSelf.View.registerClass('SpottedScript.Blank.CheckBannerSelf.View',SpottedScript.BlankUserControl.View);
