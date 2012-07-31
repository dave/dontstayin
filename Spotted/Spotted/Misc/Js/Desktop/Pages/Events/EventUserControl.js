// EventUserControl.js
(function($){
Type.registerNamespace('Js.Pages.Events.EventUserControl');Js.Pages.Events.EventUserControl.View=function(clientId){Js.Pages.Events.EventUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
Js.Pages.Events.EventUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){if(this.$2_0==null){this.$2_0=document.getElementById(this.clientId+'_GenericContainerPage');}return this.$2_0;},$2_0:null,get_genericContainerPageJ:function(){if(this.$2_1==null){this.$2_1=$('#'+this.clientId+'_GenericContainerPage');}return this.$2_1;},$2_1:null}
Js.Pages.Events.EventUserControl.View.registerClass('Js.Pages.Events.EventUserControl.View',Js.DsiUserControl.View);})(jQuery);// This script was generated using Script# v0.7.4.0
