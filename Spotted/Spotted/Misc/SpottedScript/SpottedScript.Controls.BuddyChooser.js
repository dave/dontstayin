Type.registerNamespace('SpottedScript.Controls.BuddyChooser');
SpottedScript.Controls.BuddyChooser.Controller=function(view){this.$0=view;this.$1=new SpottedScript.Behaviours.CreateUserFromEmail.Controller(view.get_uiHtmlAutoComplete());}
SpottedScript.Controls.BuddyChooser.Controller.prototype={$0:null,$1:null,get_$2:function(){return this.$0.get_uiHtmlAutoComplete().get_value();},set_$2:function($p0){this.$0.get_uiHtmlAutoComplete().set_value($p0);return $p0;},get_$3:function(){return this.$0.get_uiHtmlAutoComplete().get_text();},set_$3:function($p0){this.$0.get_uiHtmlAutoComplete().set_text($p0);return $p0;}}
SpottedScript.Controls.BuddyChooser.View=function(clientId){SpottedScript.Controls.BuddyChooser.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Controls.BuddyChooser.View.prototype={clientId:null,get_uiHtmlAutoComplete:function(){return eval(this.clientId+'_uiHtmlAutoCompleteBehaviour');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Controls.BuddyChooser.Controller.registerClass('SpottedScript.Controls.BuddyChooser.Controller');
SpottedScript.Controls.BuddyChooser.View.registerClass('SpottedScript.Controls.BuddyChooser.View',SpottedScript.GenericUserControl.View);
