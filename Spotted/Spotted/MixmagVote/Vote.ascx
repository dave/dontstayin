<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Vote.ascx.cs" Inherits="Spotted.MixmagVote.Vote" %>

<asp:HiddenField runat="server" ID="CompK" />
<asp:HiddenField runat="server" ID="ImageUrl" />
<asp:HiddenField runat="server" ID="EntryK" />
<asp:HiddenField runat="server" ID="PageIdToLike" />
<style>
	.fb_edge_widget_with_comment span.fb_edge_comment_widget iframe.fb_ltr { display: none !important; }
</style>

<asp:PlaceHolder runat="server" ID="GlobalHeaderPlaceholder" />

<asp:PlaceHolder runat="server" ID="HeaderPlaceholder" />

<asp:Panel runat="server" ID="VoteClosedPanel">
	<p style="margin-top:25px;">
		Sorry, voting has now closed.
	</p>
</asp:Panel>

<asp:Panel runat="server" ID="Vote1Panel">

	<asp:PlaceHolder runat="server" ID="Vote1TopPlaceholder" />

	<p>
		<img runat="server" id="Vote1Img" />
	</p>

	<asp:PlaceHolder runat="server" ID="Vote1MiddlePlaceholder" />

	<p>
		<button runat="server" id="Vote1VoteButton" />
	</p>

	<asp:PlaceHolder runat="server" ID="Vote1BottomPlaceholder" />

</asp:Panel>

<asp:Panel runat="server" ID="VoteConfirmPanel" style="display:none;">

	<h1>
		Is this you?
	</h1>
	<p>
		We need to confirm we've got the right Facebook account...
	</p>
	<p>
		<img runat="server" id="VoteConfirm_Img" src="" width="50" height="50" border="0" align="absmiddle" />
		<a runat="server" id="VoteConfirm_Link" href="" target="_blank"></a>
	</p>
	<p>
		<button runat="server" id="VoteConfirm_YesButton">Yes, this is me</button>
		<button runat="server" id="VoteConfirm_NoButton">Nope, not me</button>
	</p>

</asp:Panel>

<asp:Panel runat="server" ID="VoteLikePanel" style="display:none;">

	<asp:PlaceHolder runat="server" ID="VoteLikePlaceholder" />

</asp:Panel>

<asp:Panel runat="server" ID="Vote2Panel" style="display:none;">

	<asp:PlaceHolder runat="server" ID="Vote2Placeholder" />

	<asp:Panel runat="server" ID="ArmaniTextFieldPanel" style="display:none;">
		<h1>Quick Question</h1>
		<p>WHO IS YOUR FAVOURITE DJ RIGHT NOW?</p>
		<p>
			<input runat="server" type="text" maxlength="250" id="ArmaniTextField" size="30" />
			<button runat="server" ID="ArmaniSubmitButton">SAVE</button>
			<span runat="server" ID="ArmaniSavedLabel" style="display:none;">SAVED</span>
		</p>
	</asp:Panel>

</asp:Panel>

<asp:PlaceHolder runat="server" ID="FooterPlaceholder" />

<pre runat="server" id="DebugOutput" style="position:fixed; padding:10px; border:1px solid #ff0000; margin:10px; top:0px; left:0px; background-color:#ffffff; display:none; width:500px;"></pre>

<div runat="server" id="LoadingLabel" class="ui-state-highlight ui-corner-all" style="position:absolute; right:20px; top:20px; display:none; z-index:995; padding-left:20px; padding-right:20px;">
	<p>
		Loading...
	</p>
</div>
