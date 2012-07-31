Type.registerNamespace('SpottedScript.Controls.LatestChat');
SpottedScript.Controls.LatestChat.Controller=function(view){this.$0=view;this.$1=new SpottedScript.Controls.LatestChat._LatestThreadsProvider(Number.parseInvariant(view.get_uiThreadsCount().value),Boolean.parse(view.get_uiHasGroupObjectFilter().value),Number.parseInvariant(view.get_uiObjectType().value));this.$1.$5=Function.createDelegate(this,this.$6);}
SpottedScript.Controls.LatestChat.Controller.$A=function($p0){var $0=document.createElement('tr');$0.style.verticalAlign='top';$0.appendChild(SpottedScript.Controls.LatestChat.Controller.$C($p0.watchingHtml,'dataGridThreadTitlesTight',null));$0.appendChild(SpottedScript.Controls.LatestChat.Controller.$C($p0.favouriteHtml,'dataGridThreadTitlesTight',null));$0.appendChild(SpottedScript.Controls.LatestChat.Controller.$C($p0.iconsHtml+$p0.commentHtmlStart+'<a href=\"'+$p0.threadUrlSimple+'\" '+$p0.rollover+'>'+$p0.subjectSafe+'</a>'+$p0.commentHtmlEnd+$p0.pagesHtml,'dataGridThreadTitles',null));$0.childNodes[2].style.width='100%';$0.appendChild(SpottedScript.Controls.LatestChat.Controller.$C('<small>'+$p0.authorHtml+'</small>','dataGridThread','3px'));$0.appendChild(SpottedScript.Controls.LatestChat.Controller.$C('<small>'+$p0.repliesHtml+'</small>','dataGridThread','3px'));return $0;}
SpottedScript.Controls.LatestChat.Controller.$B=function(){var $0=document.createElement('tr');$0.className='dataGridHeader';var $1=document.createElement('td');$1.colSpan=3;$0.appendChild($1);$0.appendChild(SpottedScript.Controls.LatestChat.Controller.$C('Author',null,null));$0.appendChild(SpottedScript.Controls.LatestChat.Controller.$C('Replies&nbsp;/&nbsp;last',null,null));return $0;}
SpottedScript.Controls.LatestChat.Controller.$C=function($p0,$p1,$p2){var $0=document.createElement('td');$0.innerHTML=$p0;if($p2!=null){$0.style.padding=$p2;}if($p1!=null){$0.className=$p1;}return $0;}
SpottedScript.Controls.LatestChat.Controller.prototype={$0:null,$1:null,get_$2:function(){try{return Number.parseInvariant(this.$0.get_uiObjectK().value);}catch($0){return 0;}},set_$2:function($p0){this.$0.get_uiObjectK().value=$p0.toString();return $p0;},$3:function(){this.$0.get_holder().style.display='none';},$4:function($p0){this.set_$2($p0);this.$1.$7(this.get_$2());},$5:function($p0,$p1){this.$1.$B(this.get_$2());},$6:function($p0,$p1){this.$7();},$7:function(){var $0=this.$1.get_$6();if($0.length>0){this.$8(this.$0.get_threadsPanel());this.$0.get_threadsPanel().appendChild(this.$9($0));for(var $1=0;$1<$0.length;$1++){eval($0[$1].watchingScript);eval($0[$1].favouriteScript);}this.$0.get_holder().style.display='';this.$0.get_threadsPanel().style.display='';}},$8:function($p0){$p0.innerHTML='';},$9:function($p0){var $0=document.createElement('table');$0.style.border='0px';$0.style.width='100%';$0.style.borderCollapse='collapse';var $1=document.createElement('tbody');$0.appendChild($1);$1.appendChild(SpottedScript.Controls.LatestChat.Controller.$B());for(var $2=0;$2<$p0.length;$2++){$1.appendChild(SpottedScript.Controls.LatestChat.Controller.$A($p0[$2]));}return $0;}}
SpottedScript.Controls.LatestChat._LatestThreadsProvider=function(threadsCount,hasGroupObjectFilter,objectType){this.$0=threadsCount;this.$1=hasGroupObjectFilter;this.$2=objectType;this.$3=[];}
SpottedScript.Controls.LatestChat._LatestThreadsProvider.prototype={$0:0,$1:false,$2:0,$3:null,$4:0,$5:null,get_$6:function(){return this.$3[this.$4];},set_$6:function($p0){this.$3[this.$4]=$p0;return $p0;},$7:function($p0){this.$4=$p0;if(this.get_$6()!=null){this.$A();}else{this.$8();}},$8:function(){Spotted.WebServices.Controls.LatestChat.Service.getThreads(this.$2,this.$4,this.$0,this.$1,Function.createDelegate(this,this.$9),Function.createDelegate(null,Utils.Trace.webServiceFailure),null,-1);},$9:function($p0,$p1,$p2){this.set_$6($p0);this.$A();},$A:function(){if(this.$5!=null){this.$5(this,Sys.EventArgs.Empty);}},$B:function($p0){this.$4=$p0;this.$8();}}
SpottedScript.Controls.LatestChat.ThreadStub=function(){}
SpottedScript.Controls.LatestChat.ThreadStub.prototype={k:0,watchingHtml:null,watchingScript:null,favouriteHtml:null,favouriteScript:null,threadUrlSimple:null,iconsHtml:null,commentHtmlStart:null,rollover:null,subjectSafe:null,commentHtmlEnd:null,pagesHtml:null,authorHtml:null,repliesHtml:null}
SpottedScript.Controls.LatestChat.View=function(clientId){this.clientId=clientId;}
SpottedScript.Controls.LatestChat.View.prototype={clientId:null,get_externalHeader:function(){return document.getElementById(this.clientId+'_ExternalHeader');},get_externalHolder:function(){return document.getElementById(this.clientId+'_ExternalHolder');},get_holder:function(){return document.getElementById(this.clientId+'_Holder');},get_header:function(){return document.getElementById(this.clientId+'_Header');},get_innerHolder:function(){return document.getElementById(this.clientId+'_InnerHolder');},get_threadsNoPermissionPanel:function(){return document.getElementById(this.clientId+'_ThreadsNoPermissionPanel');},get_threadsNoPermissionJoinAnchor:function(){return document.getElementById(this.clientId+'_ThreadsNoPermissionJoinAnchor');},get_threadsPanel:function(){return document.getElementById(this.clientId+'_ThreadsPanel');},get_brandChatControlsP:function(){return document.getElementById(this.clientId+'_BrandChatControlsP');},get_showGroupChatEnabled:function(){return document.getElementById(this.clientId+'_ShowGroupChatEnabled');},get_showGroupChatLinkButton:function(){return document.getElementById(this.clientId+'_ShowGroupChatLinkButton');},get_showBrandChatLinkButton:function(){return document.getElementById(this.clientId+'_ShowBrandChatLinkButton');},get_showBrandChatEnabled:function(){return document.getElementById(this.clientId+'_ShowBrandChatEnabled');},get_inlineScript1:function(){return document.getElementById(this.clientId+'_InlineScript1');},get_threadsDataGrid:function(){return document.getElementById(this.clientId+'_ThreadsDataGrid');},get_commentsFooter:function(){return document.getElementById(this.clientId+'_CommentsFooter');},get_moreThreadsAnchor:function(){return document.getElementById(this.clientId+'_MoreThreadsAnchor');},get_moreThreadsCountLabel:function(){return document.getElementById(this.clientId+'_MoreThreadsCountLabel');},get_uiObjectType:function(){return document.getElementById(this.clientId+'_uiObjectType');},get_uiObjectK:function(){return document.getElementById(this.clientId+'_uiObjectK');},get_uiThreadsCount:function(){return document.getElementById(this.clientId+'_uiThreadsCount');},get_uiHasGroupObjectFilter:function(){return document.getElementById(this.clientId+'_uiHasGroupObjectFilter');}}
SpottedScript.Controls.LatestChat.Controller.registerClass('SpottedScript.Controls.LatestChat.Controller');
SpottedScript.Controls.LatestChat._LatestThreadsProvider.registerClass('SpottedScript.Controls.LatestChat._LatestThreadsProvider');
SpottedScript.Controls.LatestChat.ThreadStub.registerClass('SpottedScript.Controls.LatestChat.ThreadStub');
SpottedScript.Controls.LatestChat.View.registerClass('SpottedScript.Controls.LatestChat.View');
