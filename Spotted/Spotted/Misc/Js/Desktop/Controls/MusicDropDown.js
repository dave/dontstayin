// MusicDropDown.js
(function($){
Type.registerNamespace('Js.Controls.MusicDropDown');Js.Controls.MusicDropDown.Controller=function(view){this.view=view;}
Js.Controls.MusicDropDown.Controller.prototype={view:null}
Js.Controls.MusicDropDown.View=function(clientId){this.clientId=clientId;}
Js.Controls.MusicDropDown.View.prototype={clientId:null,get_dropDown:function(){if(this.$0==null){this.$0=document.getElementById(this.clientId+'_DropDown');}return this.$0;},$0:null,get_dropDownJ:function(){if(this.$1==null){this.$1=$('#'+this.clientId+'_DropDown');}return this.$1;},$1:null}
Js.Controls.MusicDropDown.Controller.registerClass('Js.Controls.MusicDropDown.Controller');Js.Controls.MusicDropDown.View.registerClass('Js.Controls.MusicDropDown.View');})(jQuery);// This script was generated using Script# v0.7.4.0
