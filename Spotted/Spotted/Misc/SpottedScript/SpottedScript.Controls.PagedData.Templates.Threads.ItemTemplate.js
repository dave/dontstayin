Type.registerNamespace('SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate');
SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.Controller=function(view){SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.Controller.initializeBase(this,[view]);}
SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View=function(clientId){SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View.prototype={clientId:null}
SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.Controller.registerClass('SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.Controller',SpottedScript.Controls.ClientSideRepeater.Template.Controller);
SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View.registerClass('SpottedScript.Controls.PagedData.Templates.Threads.ItemTemplate.View',SpottedScript.Controls.ClientSideRepeater.Template.View);
