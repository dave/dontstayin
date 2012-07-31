Type.registerNamespace('SpottedScript.Controls.MultiBuddyChooser');
SpottedScript.Controls.MultiBuddyChooser.Controller=function(view){this.$0=[];this.$1=[];this.$2=[];this.$3=[];this.$F={};this.$4=view;$addHandler(this.$4.get_uiAddByMusicAndPlace(),'click',Function.createDelegate(this,this.$15));$addHandler(this.$4.get_uiAddAllButton(),'click',Function.createDelegate(this,this.$14));$addHandler(this.$4.get_uiShowAllTownsAndMusic(),'click',Function.createDelegate(this,this.$18));$addHandler(this.$4.get_uiShowAddAll(),'click',Function.createDelegate(this,this.$11));$addHandler(this.$4.get_uiShowAddBy(),'click',Function.createDelegate(this,this.$12));$addHandler(this.$4.get_uiShowBuddyList(),'click',Function.createDelegate(this,this.$B));$addHandler(this.$4.get_uiJustBuddiesRadio(),'click',Function.createDelegate(this,this.$7));$addHandler(this.$4.get_uiAllMembersRadio(),'click',Function.createDelegate(this,this.$7));this.$13(this.$4.get_uiMusicTypes(),this.$1);this.$13(this.$4.get_uiPlaces(),this.$0);this.$5=new SpottedScript.Behaviours.CreateUserFromEmail.Controller(this.$4.get_uiBuddyMultiSelector().htmlAutoComplete);this.$6=new SpottedScript.Behaviours.CreateUsersFromEmails.Controller(this.$4.get_uiBuddyMultiSelector().htmlAutoComplete);this.$9=this.$4.get_uiBuddyMultiSelector().itemRemoved;this.$4.get_uiBuddyMultiSelector().itemRemoved=Function.createDelegate(this,this.$8);}
SpottedScript.Controls.MultiBuddyChooser.Controller.$1A=function($p0){while($p0.options.length>0){$p0.removeChild($p0.options[$p0.options.length-1]);}}
SpottedScript.Controls.MultiBuddyChooser.Controller.prototype={get_selectedValues:function(){var $0=this.$4.get_uiBuddyMultiSelector().getSelections().toArray();var $1=new Array($0.length);for(var $2=0;$2<$0.length;$2++){$1[$2]=($0[$2])[1];}return $1;},$4:null,$5:null,$6:null,$7:function($p0){if(this.$4.get_uiJustBuddiesRadio().checked){this.$4.get_uiBuddyMultiSelector().htmlAutoComplete.setWebMethod('GetBuddies');}else{this.$4.get_uiBuddyMultiSelector().htmlAutoComplete.setWebMethod('GetBuddiesThenUsrs');}},$8:function($p0,$p1){var $0=this.$F[$p1];if($0!=null){delete this.$F[$p1];var $1=document.createElement('OPTION');$1.innerHTML=$0;$1.value=$p1;var $2=0;if(this.$4.get_uiBuddyList().childNodes.length===0){this.$4.get_uiBuddyList().appendChild($1);}else{if(this.$4.get_uiBuddyList().childNodes.length<150){while(this.$4.get_uiBuddyList().childNodes[$2].innerHTML.localeCompare($0)<0&&$2<this.$4.get_uiBuddyList().childNodes.length-1){$2++;}}this.$4.get_uiBuddyList().insertBefore($1,this.$4.get_uiBuddyList().childNodes[$2]);}}if(this.$9!=null){this.$9($p0,$p1);}},$9:null,$A:false,$B:function($p0){if(this.$4.get_uiShowBuddyList().checked&&!this.$A){Spotted.WebServices.Controls.MultiBuddyChooser.Service.getBuddiesSelectListHtml(Function.createDelegate(this,this.$C),Function.createDelegate(null,Utils.Trace.webServiceFailure),null,0);this.$E('<OPTION value=\'-1\'>Loading...</OPTION>');}this.$4.get_uiBuddyListPanel().style.display=(this.$4.get_uiShowBuddyList().checked)?'':'none';},$C:function($p0,$p1,$p2){$clearHandlers(this.$4.get_uiBuddyList());this.$E($p0);$addHandler(this.$4.get_uiBuddyList(),'click',Function.createDelegate(this,this.$10));$addHandler(this.$4.get_uiBuddyList(),'keydown',Function.createDelegate(this,this.$D));this.$F={};this.$A=true;this.$4.get_uiBuddyListPanel().style.display='';this.$4.get_uiBuddyList().focus();if(this.$4.get_uiBuddyList().childNodes.length>0){(this.$4.get_uiBuddyList().childNodes[0]).selected=true;}},$D:function($p0){if($p0.keyCode===32||$p0.keyCode===13){this.$4.get_uiBuddyList().click();}},$E:function($p0){if(ie){this.$4.get_uiBuddyList().innerHTML='';var $0=this.$4.get_uiBuddyListPanel().innerHTML;this.$4.get_uiBuddyListPanel().innerHTML=$0.substring(0,$0.indexOf('</SELECT>'))+$p0+'</SELECT>';}else{this.$4.get_uiBuddyList().innerHTML=$p0;}},$10:function($p0){var $0=this.$4.get_uiBuddyList().selectedIndex;if($0>-1){var $1=this.$4.get_uiBuddyList().scrollTop;var $2=this.$4.get_uiBuddyList().options[$0];this.$F[$2.value]=$2.innerHTML;this.$4.get_uiBuddyMultiSelector().addItem($2.innerHTML,$2.value);this.$4.get_uiBuddyList().selectedIndex=-1;this.$4.get_uiBuddyList().remove($0);this.$4.get_uiBuddyList().scrollTop=$1;}if(this.$4.get_uiBuddyList().childNodes.length>$0&&$0!==-1){(this.$4.get_uiBuddyList().childNodes[$0]).selected=true;}this.$4.get_uiBuddyList().focus();},$11:function($p0){this.$4.get_uiAddAll().style.display=(this.$4.get_uiShowAddAll().checked)?'':'none';},$12:function($p0){this.$4.get_uiAddBy().style.display=(this.$4.get_uiShowAddBy().checked)?'':'none';},$13:function($p0,$p1){for(var $0=0;$0<$p0.options.length;$0++){$p1[$p1.length]=$p0.options[$0];}},$14:function($p0){this.$16($p0,true);},$15:function($p0){this.$16($p0,false);},$16:function($p0,$p1){var $0='All buddies';var $1='';if(!$p1){$0+=(this.$4.get_uiPlaces().value==='-1')?'':' who visit '+this.$4.get_uiPlaces().options[this.$4.get_uiPlaces().selectedIndex].innerHTML;$0+=(this.$4.get_uiMusicTypes().value==='1')?'':(((this.$4.get_uiPlaces().value==='-1')?' who':' and')+' listen to '+this.$4.get_uiMusicTypes().options[this.$4.get_uiMusicTypes().selectedIndex].innerHTML.trim());$1='{\'MusicTypeK\' : \''+this.$4.get_uiMusicTypes().value+'\',\'PlaceK\' : \''+this.$4.get_uiPlaces().value+'\'}';}else{$1='{\'MusicTypeK\' : \'1\',\'PlaceK\' : \'-1\'}';}var $2='expandClicker'+Math.floor(Math.random()*10000000);this.$4.get_uiBuddyMultiSelector().addItem($0+' - <a href=\"\" id=\"'+$2+'\" class=\"MultiSelectorExpandButton\" onmouseover=\"stt(\'Expand this to show buddies (might take a while)\');\" onmouseout=\"htm();\">show</a>',$1);$addHandler(document.getElementById($2),'click',Function.createDelegate(this,function($p1_0){
try{htm();}catch(e){};$p1_0.preventDefault();Spotted.WebServices.Controls.MultiBuddyChooser.Service.resolveUsrsFromMultiBuddyChooserValues([$1],Function.createDelegate(this,function($p2_0,$p2_1,$p2_2){
try{this.$4.get_uiBuddyMultiSelector().removeItem(document.getElementById($2).parentNode.parentNode);}catch($2_0){}var $dict2=$p2_0;for(var $key3 in $dict2){var $2_1={key:$key3,value:$dict2[$key3]};this.$4.get_uiBuddyMultiSelector().addItem($2_1.key,$2_1.value);}}),Function.createDelegate(null,Utils.Trace.webServiceFailure),null,30000);}));$p0.preventDefault();},$17:function($p0,$p1){for(var $0=0;$0<$p0.length;$0++){var $1=$p0[$0];var $2=document.createElement('OPTION');$2.innerHTML=unescape($1.key).replace('&','&amp;').replace(' ','&nbsp;');$2.value=$1.value;$p1[$p1.length]=$2;}},$18:function($p0){if(this.$2.length===0){Spotted.WebServices.Controls.MultiBuddyChooser.Service.getPlacesAndMusicTypes(Function.createDelegate(this,function($p1_0,$p1_1,$p1_2){
if(this.$2.length===0){this.$17($p1_0.musicTypes,this.$3);this.$17($p1_0.places,this.$2);}this.$19(this.$4.get_uiMusicTypes(),(this.$4.get_uiShowAllTownsAndMusic().checked)?this.$3:this.$1);this.$19(this.$4.get_uiPlaces(),(this.$4.get_uiShowAllTownsAndMusic().checked)?this.$2:this.$0);}),Function.createDelegate(null,Utils.Trace.webServiceFailure),null,30000);return;}else{this.$19(this.$4.get_uiMusicTypes(),(this.$4.get_uiShowAllTownsAndMusic().checked)?this.$3:this.$1);this.$19(this.$4.get_uiPlaces(),(this.$4.get_uiShowAllTownsAndMusic().checked)?this.$2:this.$0);}},$19:function($p0,$p1){SpottedScript.Controls.MultiBuddyChooser.Controller.$1A($p0);for(var $0=0;$0<$p1.length;$0++){$p0.appendChild($p1[$0]);}},$1B:function(){try{this.$4.get_uiBuddyMultiSelector().clear();}catch($0){}}}
SpottedScript.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK=function(){}
SpottedScript.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK.prototype={musicTypeK:0,placeK:0}
SpottedScript.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult=function(){}
SpottedScript.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult.prototype={places:null,musicTypes:null}
SpottedScript.Controls.MultiBuddyChooser.Pair=function(){}
SpottedScript.Controls.MultiBuddyChooser.Pair.prototype={key:null,value:null}
SpottedScript.Controls.MultiBuddyChooser.View=function(clientId){this.clientId=clientId;}
SpottedScript.Controls.MultiBuddyChooser.View.prototype={clientId:null,get_uiBuddyMultiSelector:function(){return eval(this.clientId+'_uiBuddyMultiSelectorBehaviour');},get_uiJustBuddiesRadio:function(){return document.getElementById(this.clientId+'_uiJustBuddiesRadio');},get_uiAllMembersRadio:function(){return document.getElementById(this.clientId+'_uiAllMembersRadio');},get_uiShowBuddyList:function(){return document.getElementById(this.clientId+'_uiShowBuddyList');},get_uiBuddyListPanel:function(){return document.getElementById(this.clientId+'_uiBuddyListPanel');},get_uiBuddyList:function(){return document.getElementById(this.clientId+'_uiBuddyList');},get_uiShowAddAll:function(){return document.getElementById(this.clientId+'_uiShowAddAll');},get_uiAddAll:function(){return document.getElementById(this.clientId+'_uiAddAll');},get_uiAddAllButton:function(){return document.getElementById(this.clientId+'_uiAddAllButton');},get_uiShowAddBy:function(){return document.getElementById(this.clientId+'_uiShowAddBy');},get_uiAddBy:function(){return document.getElementById(this.clientId+'_uiAddBy');},get_uiPlaces:function(){return document.getElementById(this.clientId+'_uiPlaces');},get_uiMusicTypes:function(){return document.getElementById(this.clientId+'_uiMusicTypes');},get_uiAddByMusicAndPlace:function(){return document.getElementById(this.clientId+'_uiAddByMusicAndPlace');},get_uiShowAllTownsAndMusic:function(){return document.getElementById(this.clientId+'_uiShowAllTownsAndMusic');}}
SpottedScript.Controls.MultiBuddyChooser.Controller.registerClass('SpottedScript.Controls.MultiBuddyChooser.Controller');
SpottedScript.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK.registerClass('SpottedScript.Controls.MultiBuddyChooser.MusicTypeKAndPlaceK');
SpottedScript.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult.registerClass('SpottedScript.Controls.MultiBuddyChooser.GetMusicTypesAndPlacesResult');
SpottedScript.Controls.MultiBuddyChooser.Pair.registerClass('SpottedScript.Controls.MultiBuddyChooser.Pair');
SpottedScript.Controls.MultiBuddyChooser.View.registerClass('SpottedScript.Controls.MultiBuddyChooser.View');
