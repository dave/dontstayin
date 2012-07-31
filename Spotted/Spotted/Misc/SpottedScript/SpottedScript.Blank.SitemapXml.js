Type.registerNamespace('SpottedScript.Blank.SitemapXml');
SpottedScript.Blank.SitemapXml.View=function(clientId){SpottedScript.Blank.SitemapXml.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Blank.SitemapXml.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.Blank.SitemapXml.View.registerClass('SpottedScript.Blank.SitemapXml.View',SpottedScript.BlankUserControl.View);
