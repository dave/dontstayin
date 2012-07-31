Type.registerNamespace('SpottedScript.Blank.SW4Terms');
SpottedScript.Blank.SW4Terms.View=function(clientId){SpottedScript.Blank.SW4Terms.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.SW4Terms.View.prototype={clientId:null,get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.SW4Terms.View.registerClass('SpottedScript.Blank.SW4Terms.View',SpottedScript.BlankUserControl.View);
