Type.registerNamespace('SpottedScript.Blank.Guestlist');
SpottedScript.Blank.Guestlist.View=function(clientId){SpottedScript.Blank.Guestlist.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.Guestlist.View.prototype={clientId:null,get_guestlistDataList:function(){return document.getElementById(this.clientId+'_GuestlistDataList');},get_eventLabel:function(){return document.getElementById(this.clientId+'_EventLabel');},get_priceLabel:function(){return document.getElementById(this.clientId+'_PriceLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.Guestlist.View.registerClass('SpottedScript.Blank.Guestlist.View',SpottedScript.BlankUserControl.View);
