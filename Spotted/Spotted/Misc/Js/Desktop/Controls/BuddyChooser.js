// BuddyChooser.js
(function($){
Type.registerNamespace('Js.Controls.BuddyChooser');Js.Controls.BuddyChooser.Controller=function(view){this.$0=view;this.$1=new Js.Controls.MultiBuddyChooser.CreateUserFromEmailController(view.get_uiHtmlAutoComplete());}
Js.Controls.BuddyChooser.Controller.prototype={$0:null,$1:null,get_value:function(){return this.$0.get_uiHtmlAutoComplete().get_value();},set_value:function(value){this.$0.get_uiHtmlAutoComplete().set_value(value);return value;},get_text:function(){return this.$0.get_uiHtmlAutoComplete().get_text();},set_text:function(value){this.$0.get_uiHtmlAutoComplete().set_text(value);return value;}}
Js.Controls.BuddyChooser.View=function(clientId){Js.Controls.BuddyChooser.View.initializeBase(this,[clientId]);this.clientId=clientId;}
Js.Controls.BuddyChooser.View.prototype={clientId:null,get_uiHtmlAutoComplete:function(){return eval(this.clientId+'_uiHtmlAutoCompleteBehaviour');},get_genericContainerPage:function(){if(this.$1_0==null){this.$1_0=document.getElementById(this.clientId+'_GenericContainerPage');}return this.$1_0;},$1_0:null,get_genericContainerPageJ:function(){if(this.$1_1==null){this.$1_1=$('#'+this.clientId+'_GenericContainerPage');}return this.$1_1;},$1_1:null}
Js.Controls.BuddyChooser.Controller.registerClass('Js.Controls.BuddyChooser.Controller');Js.Controls.BuddyChooser.View.registerClass('Js.Controls.BuddyChooser.View',Js.GenericUserControl.View);})(jQuery);// This script was generated using Script# v0.7.4.0
