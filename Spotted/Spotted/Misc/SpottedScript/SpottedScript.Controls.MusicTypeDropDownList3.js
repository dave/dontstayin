Type.registerNamespace('SpottedScript.Controls.MusicTypeDropDownList3');
SpottedScript.Controls.MusicTypeDropDownList3.Controller=function(view){this.view=view;}
SpottedScript.Controls.MusicTypeDropDownList3.Controller.prototype={view:null}
SpottedScript.Controls.MusicTypeDropDownList3.View=function(clientId){this.clientId=clientId;}
SpottedScript.Controls.MusicTypeDropDownList3.View.prototype={clientId:null,get_dropDown:function(){return document.getElementById(this.clientId+'_DropDown');}}
SpottedScript.Controls.MusicTypeDropDownList3.Controller.registerClass('SpottedScript.Controls.MusicTypeDropDownList3.Controller');
SpottedScript.Controls.MusicTypeDropDownList3.View.registerClass('SpottedScript.Controls.MusicTypeDropDownList3.View');
