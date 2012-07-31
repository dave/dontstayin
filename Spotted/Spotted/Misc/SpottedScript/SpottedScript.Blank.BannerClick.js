Type.registerNamespace('SpottedScript.Blank.BannerClick');
SpottedScript.Blank.BannerClick.View=function(clientId){SpottedScript.Blank.BannerClick.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.BannerClick.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.BannerClick.View.registerClass('SpottedScript.Blank.BannerClick.View',SpottedScript.BlankUserControl.View);
