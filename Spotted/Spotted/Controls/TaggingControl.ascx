<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaggingControl.ascx.cs" Inherits="Spotted.Controls.TaggingControl" %>
<asp:Panel ID="uiTagPanel" runat="server" DefaultButton="uiAddTagButton">
	<p><div runat="server" id="uiTagsDiv" style="text-align:center;"></div></p>
	<div runat="server" id="uiTagsDivServerSide">
		<asp:Repeater ID="uiTagRepeater" runat="server">
			<HeaderTemplate><p style="text-align:center;"></HeaderTemplate>
			<ItemTemplate>
				<span>
					<%# ((Tag)Container.DataItem).Link() %>
					<asp:ImageButton ID="uiRemove" runat="server" Visible='<%# Usr.Current != null %>' CommandArgument='<%# ((Tag)Container.DataItem).TagText  %>' CommandName="Remove" CssClass="RemoveTagButton" CausesValidation="false" ToolTip="Remove this tag" ImageUrl="/gfx/minus.gif" AlternateText="X"></asp:ImageButton>
				</span>
			</ItemTemplate>
			<FooterTemplate></p></FooterTemplate>
		</asp:Repeater>
	</div>
	<asp:Panel ID="uiAddPanel" runat="server" DefaultButton="uiAddTagButton" Visible='<%# Usr.Current != null %>'>
		<p style="text-align:center;">
			<span style="width:16px;"></span>
			<js:HtmlAutoSuggest	runat="server" ID="uiTagAutoSuggest" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetTags" Watermark="Add a tag" />
			<asp:ImageButton ID="uiAddTagButton" runat="server" Text="Add tag" ValidationGroup="AddTagGroup" ImageUrl="/gfx/plus.gif" AlternateText="+" ToolTip="Add a tag" style="vertical-align:top;position:relative;top:6px; left:-3px;" />
		</p>
	</asp:Panel>
	<script>
		function removeConfirm(tag){
			return confirm('Are you sure you want to remove the tag "' + tag + '" from this photo?');
		}
	</script>
</asp:Panel>
