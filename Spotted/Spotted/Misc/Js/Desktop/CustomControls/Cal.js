// Cal.js
(function($){
Type.registerNamespace('Js.CustomControls.Cal');Js.CustomControls.Cal.View=function(clientId){this.clientId=clientId;}
Js.CustomControls.Cal.View.prototype={get_textBox:function(){if(this.$0==null){this.$0=document.getElementById(this.clientId+'_inner');}return this.$0;},$0:null,get_textBoxJ:function(){if(this.$1==null){this.$1=$('#'+this.clientId+'_inner');}return this.$1;},$1:null,clientId:null}
Js.CustomControls.Cal.Controller=function(view){this.$0=view;view.get_textBoxJ().keydown(ss.Delegate.create(this,this.$2));view.get_textBoxJ().blur(ss.Delegate.create(this,this.$1));}
Js.CustomControls.Cal.Controller.prototype={$0:null,onDateChanged:null,$1:function($p0){if(this.getDate()==null){this.$0.get_textBox().value='';}},$2:function($p0){if('ABCDEFGHIJKLMNOPQRSTUVWXYZ,.;#[]'.indexOf(String.fromCharCode($p0.which))>-1){$p0.preventDefault();}},setDate:function(dateTime){this.$0.get_textBox().value=dateTime.format('dd/MM/yyyy');},getDate:function(){try{var $0=this.$0.get_textBox().value.split('/');return Date.parseDate($0[1]+'/'+$0[0]+'/'+$0[2]);}catch($1){return null;}},$3:function(){this.$0.get_textBox().focus();}}
Js.CustomControls.Cal.View.registerClass('Js.CustomControls.Cal.View');Js.CustomControls.Cal.Controller.registerClass('Js.CustomControls.Cal.Controller');})(jQuery);// This script was generated using Script# v0.7.4.0
