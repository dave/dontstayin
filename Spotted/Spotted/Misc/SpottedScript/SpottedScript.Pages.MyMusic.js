Type.registerNamespace('SpottedScript.Pages.MyMusic');
SpottedScript.Pages.MyMusic.View=function(clientId){SpottedScript.Pages.MyMusic.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Pages.MyMusic.View.prototype={clientId:null,get_button1:function(){return document.getElementById(this.clientId+'_Button1');},get_musicTypes:function(){return document.getElementById(this.clientId+'_MusicTypes');},get_updatedLabel:function(){return document.getElementById(this.clientId+'_UpdatedLabel');},get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Pages.MyMusic.View.registerClass('SpottedScript.Pages.MyMusic.View',SpottedScript.DsiUserControl.View);
