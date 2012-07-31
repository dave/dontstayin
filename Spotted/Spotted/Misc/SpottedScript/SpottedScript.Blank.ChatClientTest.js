Type.registerNamespace('SpottedScript.Blank.ChatClientTest');
SpottedScript.Blank.ChatClientTest.View=function(clientId){SpottedScript.Blank.ChatClientTest.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.ChatClientTest.View.prototype={clientId:null,get_chatClient:function(){return document.getElementById(this.clientId+'_ChatClient');},get_navChatClient:function(){return eval(this.clientId+'_NavChatClientController');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.ChatClientTest.View.registerClass('SpottedScript.Blank.ChatClientTest.View',SpottedScript.BlankUserControl.View);
