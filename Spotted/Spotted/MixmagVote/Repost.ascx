<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Repost.ascx.cs" Inherits="Spotted.MixmagVote.Repost" %>

<asp:HiddenField runat="server" ID="EntryK" />
<style>
	.fb_edge_widget_with_comment span.fb_edge_comment_widget iframe.fb_ltr { display: none !important; }
</style>


<asp:PlaceHolder runat="server" ID="GlobalHeaderPlaceholder" />

<asp:PlaceHolder runat="server" ID="HeaderPlaceholder" />

<asp:Panel runat="server" id="Repost1Panel">

	<asp:PlaceHolder runat="server" ID="Repost1TopPlaceholder" />

	<p>
		<img runat="server" id="Repost1Img" width="150" />
	</p>

	<asp:PlaceHolder runat="server" ID="Repost1MiddlePlaceholder" />

	<p>
		<asp:TextBox runat="server" id="Repost1FacebookMessageTextbox" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
	</p>

	<p>
		<button runat="server" id="Repost1Button"></button>
	</p>

	<asp:PlaceHolder runat="server" ID="Repost1BottomPlaceholder" />

</asp:Panel>

<asp:Panel runat="server" id="RepostConfirmPanel" style="display:none;">

	<h1>
		Is this you?
	</h1>
	<p>
		We need to confirm we've got the right Facebook account...
	</p>
	<p>
		<img runat="server" id="RepostConfirm_Img" src="" width="50" height="50" border="0" align="absmiddle" />
		<a runat="server" id="RepostConfirm_Link" href="" target="_blank"></a>
	</p>
	<p>
		<button runat="server" id="RepostConfirm_YesButton">Yes, this is me</button>
		<button runat="server" id="RepostConfirm_NoButton">Nope, not me</button>
	</p>

</asp:Panel>

<asp:Panel runat="server" id="Repost2Panel" style="display:none;">

	<asp:PlaceHolder runat="server" ID="Repost2Placeholder" />

</asp:Panel>

<asp:PlaceHolder runat="server" ID="FooterPlaceholder" />

<pre runat="server" id="DebugOutput" style="position:fixed; padding:10px; border:1px solid #ff0000; margin:10px; top:0px; left:0px; background-color:#ffffff; display:none; width:500px;"></pre>

<div runat="server" id="LoadingLabel" class="ui-state-highlight ui-corner-all" style="position:absolute; right:20px; top:20px; display:none; z-index:995; padding-left:20px; padding-right:20px;">
	<p>
		Loading...
	</p>
</div>
