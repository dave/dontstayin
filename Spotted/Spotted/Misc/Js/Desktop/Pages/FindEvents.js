// FindEvents.js
(function($){
Type.registerNamespace('Js.Pages.FindEvents');Js.Pages.FindEvents.Controller=function(v){this.view=v;this.view.get_picker().eventSelectionSepcificationChanged=ss.Delegate.create(this,this.eventSelectionChange);this.view.get_picker().handlersSet();if(Js.Library.Misc.get_browserIsIE()){$(ss.Delegate.create(this,this.$0));}else{this.$0();}}
Js.Pages.FindEvents.Controller.prototype={view:null,$0:function(){this.view.get_backLinkJ().click(ss.Delegate.create(this,this.backLinkClick));this.view.get_forwardLinkJ().click(ss.Delegate.create(this,this.forwardLinkClick));},backLinkClick:function(e){e.preventDefault();this.view.get_picker().dateModify(-7,'d');},forwardLinkClick:function(e){e.preventDefault();this.view.get_picker().dateModify(7,'d');},$1:function($p0){var $0=new Date().getFullYear();var $1=$p0.previousMonday();var $2=$p0.nextSunday();if($p0.nextSunday().month===$p0.previousMonday().month){this.view.get_monthLabel().innerHTML=$1.day+' - '+$2.day+' '+$2.get_monthNameFull()+(($2.year!==$0)?(' '+$2.year.toString()):'');}else{this.view.get_monthLabel().innerHTML=$1.day+' '+$1.get_monthNameFull()+' - '+$2.day+' '+$2.get_monthNameFull()+(($2.year!==$0)?(' '+$2.year.toString()):'');}},$2:'',eventSelectionChange:function(o,e){var $0=e.specification!=null&&e.specification.brand!=null&&e.specification.brand.k>0;var $1=e.specification!=null&&e.specification.place!=null&&e.specification.place.k>0;var $2=e.specification!=null&&e.specification.venue!=null&&e.specification.venue.k>0;var $3=e.specification!=null&&e.specification.music!=null&&e.specification.music.k>0;if(!$0&&!$2&&!($3&&$1)){this.view.get_calendarHolderOuter().style.display='none';return;}var $4='brand-'+((e.specification.brand==null)?'0':e.specification.brand.k.toString())+'|'+'place-'+((e.specification.place==null)?'0':e.specification.place.k.toString())+'|'+'venue-'+((e.specification.venue==null)?'0':e.specification.venue.k.toString())+'|'+'music-'+((e.specification.music==null)?'0':e.specification.music.k.toString())+'|'+'date-'+((e.specification.date==null)?'0':e.specification.date.toString());if($4!==this.$2){this.$2=$4;var $5='/support/getuncached.aspx?type=calendar'+'&brandk='+((e.specification.brand==null)?'0':e.specification.brand.k.toString())+'&placek='+((e.specification.place==null)?'0':e.specification.place.k.toString())+'&venuek='+((e.specification.venue==null)?'0':e.specification.venue.k.toString())+'&musictypek='+((e.specification.music==null)?'0':e.specification.music.k.toString())+'&date='+((e.specification.date==null)?'0':e.specification.date.toString());this.$1(e.specification.date);this.$3++;var $6=this.$3;var $7=this.$4;$.get($5,null,ss.Delegate.create(this,this.$5),null,$6.toString());window.setTimeout(ss.Delegate.create(this,function(){
if(this.$4===$7){this.view.get_calendarLoadingOverlay().style.height=this.view.get_calendarHolder().offsetHeight.toString()+'px';this.view.get_calendarLoadingOverlay().style.display='';this.view.get_loadingLabel().style.display='';this.view.get_monthLabel().style.display='none';}}),100);}},$3:0,$4:0,$5:function($p0,$p1,$p2,$p3){var $0=parseInt($p3);if(this.$3===$0){this.$4++;this.view.get_calendarHolder().innerHTML=$p0;this.view.get_calendarHolderOuter().style.display='';this.view.get_calendarLoadingOverlay().style.display='none';this.view.get_loadingLabel().style.display='none';this.view.get_monthLabel().style.display='';}},$6:0,$7:function($p0){this.view.get_debug().style.display='';this.$6++;this.view.get_debug().value=this.$6.toString()+' '+$p0+'\n'+this.view.get_debug().value;}}
Js.Pages.FindEvents.View=function(clientId){Js.Pages.FindEvents.View.initializeBase(this,[clientId]);this.clientId=clientId;}
Js.Pages.FindEvents.View.prototype={clientId:null,get_newUserWizardOptions:function(){if(this.$2_0==null){this.$2_0=document.getElementById(this.clientId+'_NewUserWizardOptions');}return this.$2_0;},$2_0:null,get_newUserWizardOptionsJ:function(){if(this.$2_1==null){this.$2_1=$('#'+this.clientId+'_NewUserWizardOptions');}return this.$2_1;},$2_1:null,get_findEventsHeader:function(){if(this.$2_2==null){this.$2_2=document.getElementById(this.clientId+'_FindEventsHeader');}return this.$2_2;},$2_2:null,get_findEventsHeaderJ:function(){if(this.$2_3==null){this.$2_3=$('#'+this.clientId+'_FindEventsHeader');}return this.$2_3;},$2_3:null,get_calendarTabHolder:function(){if(this.$2_4==null){this.$2_4=document.getElementById(this.clientId+'_CalendarTabHolder');}return this.$2_4;},$2_4:null,get_calendarTabHolderJ:function(){if(this.$2_5==null){this.$2_5=$('#'+this.clientId+'_CalendarTabHolder');}return this.$2_5;},$2_5:null,get_eventFinderTab:function(){if(this.$2_6==null){this.$2_6=document.getElementById(this.clientId+'_EventFinderTab');}return this.$2_6;},$2_6:null,get_eventFinderTabJ:function(){if(this.$2_7==null){this.$2_7=$('#'+this.clientId+'_EventFinderTab');}return this.$2_7;},$2_7:null,get_myCalendarTab:function(){if(this.$2_8==null){this.$2_8=document.getElementById(this.clientId+'_MyCalendarTab');}return this.$2_8;},$2_8:null,get_myCalendarTabJ:function(){if(this.$2_9==null){this.$2_9=$('#'+this.clientId+'_MyCalendarTab');}return this.$2_9;},$2_9:null,get_buddyCalendarTab:function(){if(this.$2_A==null){this.$2_A=document.getElementById(this.clientId+'_BuddyCalendarTab');}return this.$2_A;},$2_A:null,get_buddyCalendarTabJ:function(){if(this.$2_B==null){this.$2_B=$('#'+this.clientId+'_BuddyCalendarTab');}return this.$2_B;},$2_B:null,get_topIcon:function(){if(this.$2_C==null){this.$2_C=document.getElementById(this.clientId+'_TopIcon');}return this.$2_C;},$2_C:null,get_topIconJ:function(){if(this.$2_D==null){this.$2_D=$('#'+this.clientId+'_TopIcon');}return this.$2_D;},$2_D:null,get_debug:function(){if(this.$2_E==null){this.$2_E=document.getElementById(this.clientId+'_Debug');}return this.$2_E;},$2_E:null,get_debugJ:function(){if(this.$2_F==null){this.$2_F=$('#'+this.clientId+'_Debug');}return this.$2_F;},$2_F:null,get_picker:function(){return eval(this.clientId+'_PickerController');},get_calendarHolderOuter:function(){if(this.$2_10==null){this.$2_10=document.getElementById(this.clientId+'_CalendarHolderOuter');}return this.$2_10;},$2_10:null,get_calendarHolderOuterJ:function(){if(this.$2_11==null){this.$2_11=$('#'+this.clientId+'_CalendarHolderOuter');}return this.$2_11;},$2_11:null,get_backLink:function(){if(this.$2_12==null){this.$2_12=document.getElementById(this.clientId+'_BackLink');}return this.$2_12;},$2_12:null,get_backLinkJ:function(){if(this.$2_13==null){this.$2_13=$('#'+this.clientId+'_BackLink');}return this.$2_13;},$2_13:null,get_monthLabel:function(){if(this.$2_14==null){this.$2_14=document.getElementById(this.clientId+'_MonthLabel');}return this.$2_14;},$2_14:null,get_monthLabelJ:function(){if(this.$2_15==null){this.$2_15=$('#'+this.clientId+'_MonthLabel');}return this.$2_15;},$2_15:null,get_loadingLabel:function(){if(this.$2_16==null){this.$2_16=document.getElementById(this.clientId+'_LoadingLabel');}return this.$2_16;},$2_16:null,get_loadingLabelJ:function(){if(this.$2_17==null){this.$2_17=$('#'+this.clientId+'_LoadingLabel');}return this.$2_17;},$2_17:null,get_forwardLink:function(){if(this.$2_18==null){this.$2_18=document.getElementById(this.clientId+'_ForwardLink');}return this.$2_18;},$2_18:null,get_forwardLinkJ:function(){if(this.$2_19==null){this.$2_19=$('#'+this.clientId+'_ForwardLink');}return this.$2_19;},$2_19:null,get_calendarLoadingOverlay:function(){if(this.$2_1A==null){this.$2_1A=document.getElementById(this.clientId+'_CalendarLoadingOverlay');}return this.$2_1A;},$2_1A:null,get_calendarLoadingOverlayJ:function(){if(this.$2_1B==null){this.$2_1B=$('#'+this.clientId+'_CalendarLoadingOverlay');}return this.$2_1B;},$2_1B:null,get_calendarHolder:function(){if(this.$2_1C==null){this.$2_1C=document.getElementById(this.clientId+'_CalendarHolder');}return this.$2_1C;},$2_1C:null,get_calendarHolderJ:function(){if(this.$2_1D==null){this.$2_1D=$('#'+this.clientId+'_CalendarHolder');}return this.$2_1D;},$2_1D:null,get_genericContainerPage:function(){if(this.$2_1E==null){this.$2_1E=document.getElementById(this.clientId+'_GenericContainerPage');}return this.$2_1E;},$2_1E:null,get_genericContainerPageJ:function(){if(this.$2_1F==null){this.$2_1F=$('#'+this.clientId+'_GenericContainerPage');}return this.$2_1F;},$2_1F:null}
Js.Pages.FindEvents.Controller.registerClass('Js.Pages.FindEvents.Controller');Js.Pages.FindEvents.View.registerClass('Js.Pages.FindEvents.View',Js.DsiUserControl.View);})(jQuery);// This script was generated using Script# v0.7.4.0
