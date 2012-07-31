Type.registerNamespace('SpottedScript.Blank.Terms');
SpottedScript.Blank.Terms.View=function(clientId){SpottedScript.Blank.Terms.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Terms.View.prototype={clientId:null,get_h12:function(){return document.getElementById(this.clientId+'_H12');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Terms.View.registerClass('SpottedScript.Blank.Terms.View',SpottedScript.BlankUserControl.View);
