Type.registerNamespace('SpottedScript.Blank.BuyTickets');
SpottedScript.Blank.BuyTickets.View=function(clientId){SpottedScript.Blank.BuyTickets.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.BuyTickets.View.prototype={clientId:null,get_h13dx:function(){return document.getElementById(this.clientId+'_H13dx');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.BuyTickets.View.registerClass('SpottedScript.Blank.BuyTickets.View',SpottedScript.BlankUserControl.View);
