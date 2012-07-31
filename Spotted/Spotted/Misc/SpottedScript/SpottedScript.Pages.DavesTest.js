Type.registerNamespace('SpottedScript.Pages.DavesTest');
SpottedScript.Pages.DavesTest.Controller=function(v){this.view=v;if(SpottedScript.Misc.get_browserIsIE()){jQuery(document.body).ready(Function.createDelegate(this,this.$0));}else{this.$0();}}
SpottedScript.Pages.DavesTest.Controller.prototype={view:null,$0:function(){}}
SpottedScript.Pages.DavesTest.View=function(clientId){SpottedScript.Pages.DavesTest.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.DavesTest.View.prototype={clientId:null,get_myAspButton:function(){return document.getElementById(this.clientId+'_MyAspButton');},get_myButton:function(){return document.getElementById(this.clientId+'_MyButton');},get_serverP:function(){return document.getElementById(this.clientId+'_ServerP');},get_clientP:function(){return document.getElementById(this.clientId+'_ClientP');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.DavesTest.Controller.registerClass('SpottedScript.Pages.DavesTest.Controller');
SpottedScript.Pages.DavesTest.View.registerClass('SpottedScript.Pages.DavesTest.View',SpottedScript.DsiUserControl.View);
