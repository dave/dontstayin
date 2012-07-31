Type.registerNamespace('SpottedScript.Controls.ClientSideRepeater.Template');
SpottedScript.Controls.ClientSideRepeater.Template.Controller=function(view){this.$0=view;}
SpottedScript.Controls.ClientSideRepeater.Template.Controller.prototype={$0:null,render:function(data){var $0=this.transformData(data);var $1=document.getElementById(this.$0.clientId).innerHTML;var $dict1=$0;for(var $key2 in $dict1){var $2={key:$key2,value:$dict1[$key2]};var $3=new RegExp('{'+$2.key+'}','g');$1=unescape($1).replace($3,$2.value.toString());}return $1;},transformData:function(data){return data;}}
SpottedScript.Controls.ClientSideRepeater.Template.View=function(clientId){this.clientId=clientId;}
SpottedScript.Controls.ClientSideRepeater.Template.View.prototype={clientId:null}
SpottedScript.Controls.ClientSideRepeater.Template.Controller.registerClass('SpottedScript.Controls.ClientSideRepeater.Template.Controller');
SpottedScript.Controls.ClientSideRepeater.Template.View.registerClass('SpottedScript.Controls.ClientSideRepeater.Template.View');
