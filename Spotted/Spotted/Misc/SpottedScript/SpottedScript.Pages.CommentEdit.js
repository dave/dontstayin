Type.registerNamespace('SpottedScript.Pages.CommentEdit');
SpottedScript.Pages.CommentEdit.View=function(clientId){SpottedScript.Pages.CommentEdit.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.CommentEdit.View.prototype={clientId:null,get_subjectPanel:function(){return document.getElementById(this.clientId+'_SubjectPanel');},get_threadSubjectEditBox:function(){return document.getElementById(this.clientId+'_ThreadSubjectEditBox');},get_commentEditHtml:function(){return eval(this.clientId+'_CommentEditHtmlController');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.CommentEdit.View.registerClass('SpottedScript.Pages.CommentEdit.View',SpottedScript.DsiUserControl.View);
