Type.registerNamespace('SpottedScript.Blank.FindHotel');
SpottedScript.Blank.FindHotel.View=function(clientId){SpottedScript.Blank.FindHotel.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.FindHotel.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.FindHotel.View.registerClass('SpottedScript.Blank.FindHotel.View',SpottedScript.BlankUserControl.View);
