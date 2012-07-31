Type.registerNamespace('SpottedScript.Controls.LinkCloud');
SpottedScript.Controls.LinkCloud.View=function(clientId){this.clientId=clientId;}
SpottedScript.Controls.LinkCloud.View.prototype={clientId:null,get_panel1:function(){return document.getElementById(this.clientId+'_Panel1');}}
SpottedScript.Controls.LinkCloud.View.registerClass('SpottedScript.Controls.LinkCloud.View');
