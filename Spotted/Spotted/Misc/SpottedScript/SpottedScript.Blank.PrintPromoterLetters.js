Type.registerNamespace('SpottedScript.Blank.PrintPromoterLetters');
SpottedScript.Blank.PrintPromoterLetters.View=function(clientId){SpottedScript.Blank.PrintPromoterLetters.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.PrintPromoterLetters.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.PrintPromoterLetters.View.registerClass('SpottedScript.Blank.PrintPromoterLetters.View',SpottedScript.BlankUserControl.View);
