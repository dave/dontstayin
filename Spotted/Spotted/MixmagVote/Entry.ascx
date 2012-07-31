<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Entry.ascx.cs" Inherits="Spotted.MixmagVote.Entry" %>

<div runat="server" id="LoadingLabel" class="ui-state-highlight ui-corner-all" style="position:absolute; right:20px; top:20px; display:none; z-index:995; padding:0px 20px 0px 20px !important; margin:0px!important;">
	<div style="font-family: Arial Black, Arial, Sans-Serif; font-weight:bold; padding:0px; margin:0px;">
		<b>Loading...</b>
	</div>
</div>

<asp:HiddenField runat="server" ID="ImageUrl" />
<asp:HiddenField runat="server" ID="PageIdToLike" />
<asp:HiddenField runat="server" ID="CompK" />
<style>
	.fb_edge_widget_with_comment span.fb_edge_comment_widget iframe.fb_ltr { display: none !important; }
</style>


<asp:PlaceHolder runat="server" ID="GlobalHeaderPlaceholder" />

<asp:PlaceHolder runat="server" ID="HeaderPlaceholder" />

<asp:Panel runat="server" ID="EntryClosedPanel">
	<p style="margin-top:25px;">
		Sorry, voting has now closed.
	</p>
</asp:Panel>

<asp:Panel runat="server" ID="Entry1Panel">

	<asp:PlaceHolder runat="server" ID="Entry1TopPlaceholder" />
	
	<p>
		<img runat="server" id="Entry1Img" width="150" />
	</p>

	<asp:PlaceHolder runat="server" ID="Entry1MiddlePlaceholder" />

	<p>
		<asp:TextBox runat="server" id="Entry1FacebookMessageTextbox" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
	</p>

	<p runat="server" id="Entry1DailyEmailCheckboxPara">
		<asp:CheckBox runat="server" ID="Entry1DailyEmailCheckbox" Checked="true" />
	</p>

	<p>
		<button runat="server" id="Entry1Button"></button>
	</p>

	<asp:PlaceHolder runat="server" ID="Entry1BottomPlaceholder" />

</asp:Panel>

<asp:Panel runat="server" ID="EntryConfirmPanel" style="display:none;">

	<h1>
		Is this you?
	</h1>
	<p>
		We need to confirm we've got the right Facebook account...
	</p>
	<p>
		<img runat="server" id="EntryConfirm_Img" src="" width="50" height="50" border="0" align="absmiddle" />
		<a runat="server" id="EntryConfirm_Link" href="" target="_blank"></a>
	</p>
	<p>
		<button runat="server" id="EntryConfirm_YesButton">Yes, this is me</button>
		<button runat="server" id="EntryConfirm_NoButton">Nope, not me</button>
	</p>

</asp:Panel>

<asp:Panel runat="server" ID="Entry2Panel" style="display:none;">

	<asp:PlaceHolder runat="server" ID="Entry2TopPlaceholder" />

	<p>
		<img runat="server" id="Entry2Img" width="150" />
	</p>

	<asp:PlaceHolder runat="server" ID="Entry2MiddlePlaceholder" />

	<p>
		<asp:TextBox runat="server" ID="Entry2LinkTextbox" Columns="25" />
	</p>

	<div runat="server" id="Entry2LikeButtonHolder">
		<asp:PlaceHolder runat="server" ID="Entry2LikeButtonPlaceholder" />
	</div>

	<asp:PlaceHolder runat="server" ID="Entry2BottomPlaceholder" />

</asp:Panel>

<asp:PlaceHolder runat="server" ID="FooterPlaceholder" />

<pre runat="server" id="DebugOutput" style="padding:10px; border:1px solid #ff0000; margin:10px; background-color:#ffffff; display:none;"></pre>
