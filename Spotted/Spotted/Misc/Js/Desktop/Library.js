// Library.js
(function(){
Type.registerNamespace('Js.Library');Js.Library.ObjectType=function(){};Js.Library.ObjectType.prototype = {photo:1,event:2,venue:3,place:4,none:5,thread:6,country:7,article:8,para:9,brand:10,promoter:11,mainPage:999,usr:12,region:13,gallery:14,group:15,banner:16,guestlistCredit:17,ticket:18,insertionOrder:19,emailSpotlight:20,campaignCredit:21,invoice:22,comp:23,misc:24,usrDonationIcon:25}
Js.Library.ObjectType.registerEnum('Js.Library.ObjectType',false);Js.Library.U=function(){}
Js.Library.U.toString=function(o){return Js.Library.U.$0(o,'');}
Js.Library.U.$0=function($p0,$p1){var $0='    ';var $1='';if(Type.canCast($p0,Date)){$1+=($p0).toDateString()+' '+($p0).toTimeString();}else if(Type.canCast($p0,Array)){var $2=$p0;$1+='\n'+$p1+'{\n';for(var $3=0;$3<$2.length;$3++){$1+=$p1+$0+'['+$3.toString()+'] : '+Js.Library.U.$0($2[$3],$p1+$0)+'\n';}$1+=$p1+'}';}else if(Type.canCast($p0,Boolean)){$1+=$p0.toString();}else if(Type.canCast($p0,Number)){$1+=$p0.toString();}else if(Type.canCast($p0,String)){var $4=$p0.toString().replaceAll('\n','');$1+=($4.length>256)?($4.substr(0,256)+'(...)'):$4;}else if(Type.canCast($p0,Object)){var $5=$p0;$1+='\n'+$p1+'{\n';var $enum1=ss.IEnumerator.getEnumerator(Object.keys($5));while($enum1.moveNext()){var $6=$enum1.current;$1+=$p1+$0+$6+' : '+Js.Library.U.$0($5[$6],$p1+$0)+'\n';}$1+=$p1+'}';}return $1;}
Js.Library.U.get=function(d,query){try{var $0;if(query.indexOf('/')>-1){$0=query.split('/');}else{$0=[query];}for(var $1=0;$1<$0.length;$1++){if($1===$0.length-1){return Js.Library.U.getFromDictionaryByQuery(d,$0[$1]);}else{d=Js.Library.U.getFromDictionaryByQuery(d,$0[$1]);}}return null;}catch($2){return null;}}
Js.Library.U.exists=function(d,query){try{var $0;if(query.indexOf('/')>-1){$0=query.split('/');}else{$0=[query];}for(var $1=0;$1<$0.length;$1++){if($1===$0.length-1){if(Js.Library.U.getFromDictionaryByQuery(d,$0[$1])==null){return false;}}else{d=Js.Library.U.getFromDictionaryByQuery(d,$0[$1]);}}return true;}catch($2){return false;}}
Js.Library.U.getFromDictionaryByQuery=function(d,query){if(!query||query==='*'){return Js.Library.U.getFromDictionaryByIndex(d,0);}else{return d[query];}}
Js.Library.U.getFromDictionaryByIndex=function(d,index){try{return d[Object.keys(d)[index]];}catch($0){return null;}}
Js.Library.U.isTrue=function(d,query){try{if(Js.Library.U.exists(d,query)){var $0=Js.Library.U.get(d,query);if(Type.canCast($0,Boolean)){return $0;}}return false;}catch($1){return false;}}
Js.Library.U.hasValue=function(d,query){try{if(Js.Library.U.exists(d,query)){return Js.Library.U.get(d,query)!=null;}return false;}catch($0){return false;}}
Js.Library.Misc=function(){}
Js.Library.Misc.objectToString=function(o){return Js.Library.Misc.$0(o,0);}
Js.Library.Misc.$0=function($p0,$p1){if(Type.canCast($p0,String)){return $p0;}else if(Type.canCast($p0,Object)){var $0='\n[';var $1=$p0;var $enum1=ss.IEnumerator.getEnumerator(Object.keys($1));while($enum1.moveNext()){var $2=$enum1.current;for(var $3=0;$3<$p1;$3++){$0+=' ';}$0+=$2+': '+Js.Library.Misc.$0($1[$2],$p1+2)+'\n';}$0+=']';return $0;}else{return '';}}
Js.Library.Misc.getPicUrlFromGuid=function(guid){var $0=eval("StoragePath('"+guid+"');");return $0;}
Js.Library.Misc.redirect=function(url){eval("window.location = '"+url+"'");}
Js.Library.Misc.addHoverText=function(el,hoverText){el.addEventListener('mouseover',function(){
Js.Library.Misc.$1(hoverText);},false);el.addEventListener('mouseout',function(){
Js.Library.Misc.$2();},false);}
Js.Library.Misc.redirectToAnchor=function(anchorName){window.location.hash=anchorName;}
Js.Library.Misc.showWaitingCursor=function(){eval('ShowWaitingCursor();');}
Js.Library.Misc.hideWaitingCursor=function(){eval('HideWaitingCursor();');}
Js.Library.Misc.$1=function($p0){eval("stt('"+$p0+"');");}
Js.Library.Misc.$2=function(){eval('htm();');}
Js.Library.Misc.get_browserIsFirefox=function(){return $.browser.mozilla;}
Js.Library.Misc.get_browserIsIE=function(){return $.browser.msie;}
Js.Library.Misc.get_browserVersion=function(){var $0=1;try{$0=parseFloat($.browser.version);}catch($1){}return $0;}
Js.Library.Misc.combineAction=function(runFirst,runSecond){var $0=function(){
if(runFirst!=null){runFirst();}if(runSecond!=null){runSecond();}};return $0;}
Js.Library.Misc.combineEventHandler=function(runFirst,runSecond){var $0=function($p1_0,$p1_1){
if(runFirst!=null){runFirst($p1_0,$p1_1);}if(runSecond!=null){runSecond($p1_0,$p1_1);}};return $0;}
Js.Library.Misc.$3=function(){debugger;;}
Js.Library.IntEventArgs=function(value){Js.Library.IntEventArgs.initializeBase(this);this.value=value;}
Js.Library.IntEventArgs.prototype={value:0}
Js.Library.WebServiceError=function(exceptionType,message,stackTrace,statusCode,timedOut){this.exceptionType=exceptionType;this.message=message;this.stackTrace=stackTrace;this.statusCode=statusCode;this.timedOut=timedOut;}
Js.Library.WebServiceError.prototype={exceptionType:null,message:null,stackTrace:null,statusCode:0,timedOut:false}
Js.Library.WebServiceHelper=function(){}
Js.Library.WebServiceHelper.options=function(methodName,url,parameters,failure,userContext,timeout){var $0={};$0.url=url+'/'+methodName;$0.timeout=timeout;$0.type='POST';$0.async=true;$0.cache=false;$0.contentType='application/json; charset=utf-8';$0.data=JSON.stringify(parameters);$0.dataType='json';$0.error=function($p1_0,$p1_1,$p1_2){
failure(new Js.Library.WebServiceError(Type.getInstanceType($p1_2).toString(),$p1_1,$p1_2.toString(),$p1_0.status,$p1_0.status===408),userContext,methodName);};return $0;}
Js.Library.Trace=function(){}
Js.Library.Trace.write=function(message){}
Js.Library.Trace.webServiceFailure=function(error,userContext,methodName){Js.Library.Trace.write('Message: '+error.message+'<br>Type: '+error.exceptionType+'<br>Stack trace: '+error.stackTrace+'<br>Status code: '+error.statusCode+'<br>Timed out: '+error.timedOut);}
Js.Library.Library$0=function(){}
Js.Library.StringBuilderJs=function(){this.$0=[];}
Js.Library.StringBuilderJs.prototype={$0:null,append:function(s){this.$0[this.$0.length]=s;},toString:function(){return this.$0.join('');},appendAttribute:function(name,value){this.$0[this.$0.length]=' ';this.$0[this.$0.length]=name;this.$0[this.$0.length]='="';this.$0[this.$0.length]=value.replaceAll('"','&#34;');this.$0[this.$0.length]='"';}}
Type.registerNamespace('Js.DsiUserControl');Js.DsiUserControl.View=function(clientId){Js.DsiUserControl.View.initializeBase(this,[clientId]);this.clientId=clientId;}
Js.DsiUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
Type.registerNamespace('Js.GenericUserControl');Js.GenericUserControl.View=function(clientId){this.clientId=clientId;}
Js.GenericUserControl.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
Js.Library.U.registerClass('Js.Library.U');Js.Library.Misc.registerClass('Js.Library.Misc');Js.Library.IntEventArgs.registerClass('Js.Library.IntEventArgs',ss.EventArgs);Js.Library.WebServiceError.registerClass('Js.Library.WebServiceError');Js.Library.WebServiceHelper.registerClass('Js.Library.WebServiceHelper');Js.Library.Trace.registerClass('Js.Library.Trace');Js.Library.Library$0.registerClass('Js.Library.Library$0');Js.Library.StringBuilderJs.registerClass('Js.Library.StringBuilderJs');Js.GenericUserControl.View.registerClass('Js.GenericUserControl.View');Js.DsiUserControl.View.registerClass('Js.DsiUserControl.View',Js.GenericUserControl.View);Js.Library.Library$0.$0=null;})();// This script was generated using Script# v0.7.4.0
