Type.registerNamespace('SpottedScript.Blank.DatePhotoList');
SpottedScript.Blank.DatePhotoList.View=function(clientId){SpottedScript.Blank.DatePhotoList.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.DatePhotoList.View.prototype={clientId:null,get_datePhotoListContent:function(){return document.getElementById(this.clientId+'_DatePhotoListContent');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.DatePhotoList.View.registerClass('SpottedScript.Blank.DatePhotoList.View',SpottedScript.BlankUserControl.View);
