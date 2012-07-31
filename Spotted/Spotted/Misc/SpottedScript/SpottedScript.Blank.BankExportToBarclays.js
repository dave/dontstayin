Type.registerNamespace('SpottedScript.Blank.BankExportToBarclays');
SpottedScript.Blank.BankExportToBarclays.View=function(clientId){SpottedScript.Blank.BankExportToBarclays.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.BankExportToBarclays.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.BankExportToBarclays.View.registerClass('SpottedScript.Blank.BankExportToBarclays.View',SpottedScript.BlankUserControl.View);
