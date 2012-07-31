<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewBrand.ascx.cs" Inherits="Spotted.Blank.NewBrand" %>
<%@ Register TagPrefix="Controls" TagName="Pic" Src="/Controls/Pic.ascx" %>
<script>window.focus();</script>
<link rel="stylesheet" type="text/css" href="/support/style.css?a=2"/>
<SCRIPT LANGUAGE="javascript" SRC="/Misc/Flash.js"></SCRIPT>
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<table cellpadding=0 cellspacing=0 border=0><tr><td>
<div class="Content">
	<asp:Panel Runat="server" ID="PanelName">
		<dsi:h1 runat="server" ID="H12" NAME="H11">Add a promoter/brand</dsi:h1>
		<div class="ContentBorder">
			<h2>
				Promoter / brand-name
			</h2>
			<p>
				Parties are organised on the site by promoter or 'brand-name'. The brand-name 
				doesn't usually change from party to party. For example, if there's a party called 
				"<i>The Hed Kandi Winter Wonderland Party</i>" - the promoter is "Hed Kandi".
			</p>
			<p>
				Some parties are organised by two or more promoters. For example, if the party is 
				called "<i>Frantic vs Wildchild present Boshing Beats!</i>" - the promoters are 
				"Frantic" and "Wildchild".
			</p>
			<p>
				You've chosen to add a new promoter/brand to our database. You should only
				add a new promoter/brand if you're sure it's not already in the database. If
				you're sure we don't already know about this promoter/brand, enter the 
				name below:
			</p>
			<p>
				Name: <asp:TextBox Runat="server" ID="NameTextBox" Columns="50" MaxLength="50"></asp:TextBox>
			</p>
			<p>
				<input type="button" Value="Cancel" onclick="window.close();"/>
				<asp:Button Runat="server" OnClick="PanelNameNext" Text="Next -&gt;" EnableViewState="False" ID="Button3"/>
			</p>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="<p>Please enter a name</p>" 
				Runat="server" 
				Display="Dynamic" 
				ControlToValidate="NameTextBox"/>
			<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="<p>The name should be 50 characters or less</p>" 
				Runat="server" 
				Display="Dynamic" 
				ValidationExpression="^.{1,50}$" 
				ControlToValidate="NameTextBox"/>
			<asp:CustomValidator ID="CustomValidator1" ErrorMessage="<p>This name already exists in our promoter/brand database. Please click cancel and choose this name from the drop-down.</p>"
				Runat="server" 
				EnableClientScript="False" 
				OnServerValidate="NameVal" 
				Display="Dynamic"/>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="PanelPic">
		<dsi:h1 runat="server" ID="H15">Add a picture</dsi:h1>
		<div class="ContentBorder">
			<p>
				If you like, you can upload a logo or picture for this promoter/brand.
			</p>
			<asp:Panel Runat="server" ID="PicUploadPanel">
				<Controls:Pic Runat="server" ID="Pic" OnActionSaved="PicSaved" OnActionNoPic="PicNoPic"/>
			</asp:Panel>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="PanelDone">
		<dsi:h1 runat="server" ID="H11">Done</dsi:h1>
		<div class="ContentBorder">
			<p>
				That's it. Click the button below to close this pop-up, and you'll 
				now be able to select your newly added promoter/brand in the drop-down.
			</p>
			<p>
				<input type="button" Value="Done" onclick="Closing();"/>
			</p>
			<script>
				function Closing()
				{
					window.close();
					opener.focus();
					try
					{
						opener.NewBrand(<%= NewBrandParams %>);
					}
					catch(ex){}
				}
			</script>
		</div>
	</asp:Panel>
</div>
</td>
</tr>
</table>
