Type.registerNamespace('SpottedScript.Pages.UploadPhotos');
SpottedScript.Pages.UploadPhotos.Controller=function(v){this.view=v;this.view.get_picker().eventSelectionSepcificationChanged=Function.createDelegate(this,this.eventSelectionChange);if(SpottedScript.Misc.get_browserIsIE()){jQuery(document.body).ready(Function.createDelegate(this,this.$0));}else{this.$0();}}
SpottedScript.Pages.UploadPhotos.Controller.prototype={view:null,$0:function(){$addHandler(this.view.get_backLink(),'click',Function.createDelegate(this,this.backLinkClick));$addHandler(this.view.get_forwardLink(),'click',Function.createDelegate(this,this.forwardLinkClick));},backLinkClick:function(e){e.preventDefault();this.view.get_picker().dateModify(-7,'d');},forwardLinkClick:function(e){e.preventDefault();this.view.get_picker().dateModify(7,'d');},$1:function($p0){var $0=new Date().getFullYear();var $1=$p0.previousMonday();var $2=$p0.nextSunday();if($p0.nextSunday().month===$p0.previousMonday().month){this.view.get_monthLabel().innerHTML=$1.day+' - '+$2.day+' '+$2.get_monthNameFull()+(($2.year!==$0)?(' '+$2.year.toString()):'');}else{this.view.get_monthLabel().innerHTML=$1.day+' '+$1.get_monthNameFull()+' - '+$2.day+' '+$2.get_monthNameFull()+(($2.year!==$0)?(' '+$2.year.toString()):'');}},$2:'',eventSelectionChange:function(o,e){var $0=e.specification!=null&&e.specification.brand!=null&&e.specification.brand.k>0;var $1=e.specification!=null&&e.specification.place!=null&&e.specification.place.k>0;var $2=e.specification!=null&&e.specification.venue!=null&&e.specification.venue.k>0;var $3=e.specification!=null&&e.specification.music!=null&&e.specification.music.k>0;var $4=e.specification!=null&&e.specification.me;if(!$4&&!$0&&!$2&&!($3&&$1)){this.view.get_calendarHolderOuter().style.display='none';return;}var $5='brand-'+((e.specification.brand==null)?'0':e.specification.brand.k.toString())+'|'+'place-'+((e.specification.place==null)?'0':e.specification.place.k.toString())+'|'+'venue-'+((e.specification.venue==null)?'0':e.specification.venue.k.toString())+'|'+'music-'+((e.specification.music==null)?'0':e.specification.music.k.toString())+'|'+'date-'+((e.specification.date==null)?'0':e.specification.date.toString())+'|'+'me-'+((e.specification.me)?'1':'0');if($5!==this.$2){this.$2=$5;var $6='/support/getuncached.aspx?type=calendar&addgallery=1'+'&brandk='+((e.specification.brand==null)?'0':e.specification.brand.k.toString())+'&placek='+((e.specification.place==null)?'0':e.specification.place.k.toString())+'&venuek='+((e.specification.venue==null)?'0':e.specification.venue.k.toString())+'&musictypek='+((e.specification.music==null)?'0':e.specification.music.k.toString())+'&date='+((e.specification.date==null)?'0':e.specification.date.toString())+'&me='+((e.specification.me)?'1':'0');this.$1(e.specification.date);this.$3++;var $7=this.$3;var $8=this.$4;jQuery.get($6,null,Function.createDelegate(this,this.$5),null,$7.toString());window.setTimeout(Function.createDelegate(this,function(){
if(this.$4===$8){this.view.get_calendarLoadingOverlay().style.height=this.view.get_calendarHolder().offsetHeight.toString()+'px';this.view.get_calendarLoadingOverlay().style.display='';this.view.get_loadingLabel().style.display='';this.view.get_monthLabel().style.display='none';}}),100);}else{this.view.get_calendarHolderOuter().style.display='';this.view.get_calendarLoadingOverlay().style.display='none';this.view.get_loadingLabel().style.display='none';this.view.get_monthLabel().style.display='';}},$3:0,$4:0,$5:function($p0,$p1,$p2,$p3){var $0=Number.parseInvariant($p3);if(this.$3===$0){this.$4++;this.view.get_calendarHolder().innerHTML=$p0;this.view.get_calendarHolderOuter().style.display='';this.view.get_calendarLoadingOverlay().style.display='none';this.view.get_loadingLabel().style.display='none';this.view.get_monthLabel().style.display='';}},$6:0,$7:function($p0){this.view.get_debug().style.display='';this.$6++;this.view.get_debug().value=this.$6.toString()+' '+$p0+'\n'+this.view.get_debug().value;}}
SpottedScript.Pages.UploadPhotos.View=function(clientId){SpottedScript.Pages.UploadPhotos.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.UploadPhotos.View.prototype={clientId:null,get_newUserWizardOptions:function(){return document.getElementById(this.clientId+'_NewUserWizardOptions');},get_findEventsHeader:function(){return document.getElementById(this.clientId+'_FindEventsHeader');},get_topIcon:function(){return document.getElementById(this.clientId+'_TopIcon');},get_debug:function(){return document.getElementById(this.clientId+'_Debug');},get_picker:function(){return eval(this.clientId+'_PickerController');},get_calendarHolderOuter:function(){return document.getElementById(this.clientId+'_CalendarHolderOuter');},get_backLink:function(){return document.getElementById(this.clientId+'_BackLink');},get_monthLabel:function(){return document.getElementById(this.clientId+'_MonthLabel');},get_loadingLabel:function(){return document.getElementById(this.clientId+'_LoadingLabel');},get_forwardLink:function(){return document.getElementById(this.clientId+'_ForwardLink');},get_calendarLoadingOverlay:function(){return document.getElementById(this.clientId+'_CalendarLoadingOverlay');},get_calendarHolder:function(){return document.getElementById(this.clientId+'_CalendarHolder');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.UploadPhotos.Controller.registerClass('SpottedScript.Pages.UploadPhotos.Controller');
SpottedScript.Pages.UploadPhotos.View.registerClass('SpottedScript.Pages.UploadPhotos.View',SpottedScript.DsiUserControl.View);
