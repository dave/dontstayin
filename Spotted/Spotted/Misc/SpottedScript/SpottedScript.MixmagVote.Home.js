Type.registerNamespace('SpottedScript.MixmagVote.Home');
SpottedScript.MixmagVote.Home.Controller=function(v){SpottedScript.MixmagVote.Home.Controller.instance=this;this.view=v;if(SpottedScript.Misc.get_browserIsIE()){jQuery(document.body).ready(Function.createDelegate(this,this.$0));}else{this.$0();}}
SpottedScript.MixmagVote.Home.Controller.prototype={view:null,$0:function(){},$1:false,$2:false,$3:'0',$4:null,$5:function($p0){this.$2=ImportedUtilities.U.exists($p0,'status')&&ImportedUtilities.U.get($p0,'status').toString()==='connected';this.$1=ImportedUtilities.U.exists($p0,'status')&&ImportedUtilities.U.get($p0,'status').toString()!=='unknown';this.$3=(this.$2)?ImportedUtilities.U.get($p0,'session/uid').toString():'0';this.$4=(this.$2)?ImportedUtilities.U.get($p0,'session'):null;this.debug(this.$3);},debug:function(txt){}}
SpottedScript.MixmagVote.Home.View=function(clientId){SpottedScript.MixmagVote.Home.View.initializeBase(this,[clientId]);this.clientId=clientId;}
SpottedScript.MixmagVote.Home.View.prototype={clientId:null,get_genericContainerPage:function(){return document.getElementById(this.clientId+'_GenericContainerPage');}}
SpottedScript.MixmagVote.Home.Controller.registerClass('SpottedScript.MixmagVote.Home.Controller');
SpottedScript.MixmagVote.Home.View.registerClass('SpottedScript.MixmagVote.Home.View',SpottedScript.MixmagVoteUserControl.View);
SpottedScript.MixmagVote.Home.Controller.instance=null;
