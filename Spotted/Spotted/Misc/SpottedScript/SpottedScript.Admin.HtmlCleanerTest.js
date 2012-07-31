Type.registerNamespace('SpottedScript.Admin.HtmlCleanerTest');
SpottedScript.Admin.HtmlCleanerTest.View=function(clientId){SpottedScript.Admin.HtmlCleanerTest.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Admin.HtmlCleanerTest.View.prototype={clientId:null,get_input:function(){return document.getElementById(this.clientId+'_Input');},get_button1:function(){return document.getElementById(this.clientId+'_Button1');},get_output:function(){return document.getElementById(this.clientId+'_Output');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Admin.HtmlCleanerTest.View.registerClass('SpottedScript.Admin.HtmlCleanerTest.View',SpottedScript.AdminUserControl.View);
