Type.registerNamespace('SpottedScript.Pages.Articles.Home');
SpottedScript.Pages.Articles.Home.Controller=function(view){view.get_threadControl().get_uiCommentsDisplay().showComments(Number.parseInvariant(view.get_uiThreadK().value),Number.parseInvariant(view.get_uiPageNumber().value));view.get_threadControl().set_currentParentObjectK(Number.parseInvariant(view.get_uiArticleK().value));}
SpottedScript.Pages.Articles.Home.View=function(clientId){SpottedScript.Pages.Articles.Home.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.Articles.Home.View.prototype={clientId:null,get_homeContent:function(){return document.getElementById(this.clientId+'_HomeContent');},get_latestChat:function(){return eval(this.clientId+'_LatestChatController');},get_threadControl:function(){return eval(this.clientId+'_ThreadControlController');},get_uiThreadK:function(){return document.getElementById(this.clientId+'_uiThreadK');},get_uiArticleK:function(){return document.getElementById(this.clientId+'_uiArticleK');},get_uiPageNumber:function(){return document.getElementById(this.clientId+'_uiPageNumber');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.Articles.Home.Controller.registerClass('SpottedScript.Pages.Articles.Home.Controller');
SpottedScript.Pages.Articles.Home.View.registerClass('SpottedScript.Pages.Articles.Home.View',SpottedScript.DsiUserControl.View);
