Type.registerNamespace('SpottedScript.Blank.BannerPositions');
SpottedScript.Blank.BannerPositions.View=function(clientId){SpottedScript.Blank.BannerPositions.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.BannerPositions.View.prototype={clientId:null,get_h13dx:function(){return document.getElementById(this.clientId+'_H13dx');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.BannerPositions.View.registerClass('SpottedScript.Blank.BannerPositions.View',SpottedScript.BlankUserControl.View);
