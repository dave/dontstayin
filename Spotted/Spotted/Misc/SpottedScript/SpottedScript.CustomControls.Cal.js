Type.registerNamespace('SpottedScript.CustomControls.Cal');
SpottedScript.CustomControls.Cal.Controller=function(view){this.$0=view;$addHandler(this.$0.get_textBox(),'keydown',Function.createDelegate(this,this.$2));$addHandler(this.$0.get_textBox(),'blur',Function.createDelegate(this,this.$1));}
SpottedScript.CustomControls.Cal.Controller.prototype={$0:null,onDateChanged:null,$1:function($p0){if(this.getDate()==null){this.$0.get_textBox().value='';}},$2:function($p0){if('ABCDEFGHIJKLMNOPQRSTUVWXYZ,.;#[]'.indexOf(String.fromCharCode($p0.keyCode))>-1){$p0.preventDefault();}},setDate:function(dateTime){this.$0.get_textBox().value=dateTime.format('dd/MM/yyyy');},getDate:function(){try{var $0=this.$0.get_textBox().value.split('/');return Date.parseLocale($0[1]+'/'+$0[0]+'/'+$0[2]);}catch($1){return null;}},$3:function(){this.$0.get_textBox().focus();}}
SpottedScript.CustomControls.Cal.View=function(clientId){this.clientId=clientId;}
SpottedScript.CustomControls.Cal.View.prototype={clientId:null,get_textBox:function(){return document.getElementById(this.clientId+'_inner');}}
SpottedScript.CustomControls.Cal.Controller.registerClass('SpottedScript.CustomControls.Cal.Controller');
SpottedScript.CustomControls.Cal.View.registerClass('SpottedScript.CustomControls.Cal.View');
