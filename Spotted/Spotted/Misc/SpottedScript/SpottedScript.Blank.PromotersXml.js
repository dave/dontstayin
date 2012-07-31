Type.registerNamespace('SpottedScript.Blank.PromotersXml');
SpottedScript.Blank.PromotersXml.View=function(clientId){SpottedScript.Blank.PromotersXml.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.PromotersXml.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.PromotersXml.View.registerClass('SpottedScript.Blank.PromotersXml.View',SpottedScript.BlankUserControl.View);
