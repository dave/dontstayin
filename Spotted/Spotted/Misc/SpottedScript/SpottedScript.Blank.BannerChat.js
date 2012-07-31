Type.registerNamespace('SpottedScript.Blank.BannerChat');
SpottedScript.Blank.BannerChat.View=function(clientId){SpottedScript.Blank.BannerChat.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.BannerChat.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.BannerChat.View.registerClass('SpottedScript.Blank.BannerChat.View',SpottedScript.BlankUserControl.View);
