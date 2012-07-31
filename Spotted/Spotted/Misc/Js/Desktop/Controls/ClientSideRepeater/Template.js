// Template.js
(function($){
Type.registerNamespace('Js.Controls.ClientSideRepeater.Template');Js.Controls.ClientSideRepeater.Template.Controller=function(view){this.$0=view;}
Js.Controls.ClientSideRepeater.Template.Controller.prototype={$0:null,render:function(data){var $0=this.transformData(data);var $1=document.getElementById(this.$0.clientId).innerHTML;var $enum1=ss.IEnumerator.getEnumerator($0);while($enum1.moveNext()){var $2=$enum1.current;var $3=new RegExp('{'+$2.toString()+'}','g');$1=unescape($1).replace($3,$0[$2].toString());}return $1;},transformData:function(data){return data;}}
Js.Controls.ClientSideRepeater.Template.View=function(clientId){this.clientId=clientId;}
Js.Controls.ClientSideRepeater.Template.View.prototype={clientId:null}
Js.Controls.ClientSideRepeater.Template.Controller.registerClass('Js.Controls.ClientSideRepeater.Template.Controller');Js.Controls.ClientSideRepeater.Template.View.registerClass('Js.Controls.ClientSideRepeater.Template.View');})(jQuery);// This script was generated using Script# v0.7.4.0
