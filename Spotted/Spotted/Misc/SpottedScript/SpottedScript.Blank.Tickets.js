Type.registerNamespace('SpottedScript.Blank.Tickets');
SpottedScript.Blank.Tickets.View=function(clientId){SpottedScript.Blank.Tickets.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Tickets.View.prototype={clientId:null,get_h1:function(){return document.getElementById(this.clientId+'_H1');},get_tab:function(){return document.getElementById(this.clientId+'_Tab');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Tickets.View.registerClass('SpottedScript.Blank.Tickets.View',SpottedScript.BlankUserControl.View);
