 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuddyImporter.ascx.cs" Inherits="Spotted.Controls.BuddyImporter" %>

<asp:Panel runat="server" ID="uiEmailCredentialsPanel">
	<p>Simply enter your email address below, and the password for your email account (not your DontStayIn password!). Don't worry, we don't store your email password. Then you will be able to choose which of your contacts to add to DontStayIn!</p>
	<p>You can do this with any email account, not just the one you signed up with.</p>
	<p>We won't send out any invitations without you choosing to do so.</p>
	<p><small>You won't be able to use this service if your email provider is not on the list.</small></p>
	<table>
		<tr>
			<td>
				Email address
			</td>
			<td>
				<asp:TextBox ID="uiEmailText" runat="server" Width="100px"></asp:TextBox>
				<asp:CustomValidator runat="server" ClientValidationFunction="EmailRequiredVal" ErrorMessage="No email address" Text="*"></asp:CustomValidator>
				@
				<asp:DropDownList ID="uiEmailProviderDropDown" runat="server"></asp:DropDownList>
				<asp:CustomValidator runat="server" ClientValidationFunction="EmailProviderRequiredVal" ErrorMessage="No email provider" Text="*"></asp:CustomValidator>
			</td>
		</tr>
		<tr>
			<td>
				Password
			</td>
			<td>
				<asp:TextBox ID="uiPasswordText" runat="server" TextMode="Password"></asp:TextBox>
				<asp:CustomValidator runat="server" ClientValidationFunction="PasswordRequiredVal" ErrorMessage="No password" Text="*"></asp:CustomValidator>
				<small>we will not store your password</small>
			</td>
		</tr>
	</table>
	<script>
	function EmailRequiredVal(source, args)
	{
		args.IsValid = (document.getElementById('<%=uiEmailCredentialsPanel.ClientID %>').parentNode.parentNode.style.display == "none" || document.getElementById("<%= uiEmailText.ClientID %>").value.trim().length > 1);
	}
	function EmailProviderRequiredVal(source, args)
	{
		args.IsValid = (document.getElementById('<%=uiEmailCredentialsPanel.ClientID %>').parentNode.parentNode.style.display == "none" || document.getElementById("<%= uiEmailProviderDropDown.ClientID %>").value.trim().length > 1);
	}
	function PasswordRequiredVal(source, args)
	{
		args.IsValid = (document.getElementById('<%=uiEmailCredentialsPanel.ClientID %>').parentNode.parentNode.style.display == "none" || document.getElementById("<%= uiPasswordText.ClientID %>").value.trim().length > 1);
	}
	</script>
	<p>
		<asp:ValidationSummary runat="server" DisplayMode="BulletList" HeaderText="Please correct the following info before proceeding:" />
		<p>
			<asp:Label runat="server" ID="uiErrorBadCredentialsLabel" ForeColor="Red" Text="Invalid email address or password. Passwords are case sensitive, please check your CAPS lock key.</small>"></asp:Label>
			<asp:Label runat="server" ID="uiErrorUnknownEmailProvider" ForeColor="Red" Text="Sorry, your email provider is currently not supported by our system."></asp:Label>
		</p>
		<button runat="server" id="uiGetEmailContactsButton" onserverclick="GetEmailContacts_Click">Get my contacts!</button>
	</p>
</asp:Panel>

<asp:Panel runat="server" ID="uiSelectContactsPanel">
	<p><asp:Label runat="server" ID="uiAlreadyBuddiesLabel"></asp:Label></p>
	<p><asp:Label runat="server" ID="uiNonBuddyMembersLabel"></asp:Label></p>
	<asp:CheckBox runat="server" ID="uiToggleSelectAllMemberContactsCheckBox" Checked="true" Text="<small>Select / Deselect All</small>" />
	<div runat="server" id="uiSelectMemberContactsDiv">
		<asp:GridView ID="uiSelectMemberContactsGridView" runat="server" DataKeyNames="K"
			AutoGenerateColumns="false" 
			CssClass="dataGrid" BorderWidth="0" CellPadding="3" OnRowDataBound="uiSelectMemberContactsGridView_RowDataBound"
			AlternatingRowStyle-CssClass="dataGridAltItem" RowStyle-CssClass="dataGridItem" SelectedRowStyle-CssClass="dataGridSelectedItem"
			HeaderStyle-CssClass="dataGridHeader" AlternatingRowStyle-VerticalAlign="Middle" RowStyle-VerticalAlign="Middle" ShowHeader="false">
			<Columns>
				<asp:TemplateField>
					<ItemTemplate>
						<asp:CheckBox runat="server" ID="uiCheckBox" Checked="true" Enabled='<%# !(bool)((Bobs.Usr)Container.DataItem).ExtraSelectElements["IsAlreadyBuddy"] && !(bool)((Bobs.Usr)Container.DataItem).ExtraSelectElements["BuddyRequested"] %>' />
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<ItemTemplate>
						<a <%# ((Bobs.Usr)Container.DataItem).Rollover %>><%# ((Bobs.Usr)Container.DataItem).AllowLinkToProfile() ? ((Bobs.Usr)Container.DataItem).NickName : "&lt;withheld&gt;" %></a>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<ItemTemplate>
							<%# (((Bobs.Usr)Container.DataItem).AllowLinkToProfile() ? ((Bobs.Usr)Container.DataItem).FullName : "") + " &lt;" + ((Bobs.Usr)Container.DataItem).Email + "&gt;" %>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</div>

	<p><asp:Label runat="server" ID="uiNonMembersLabel"></asp:Label></p>
	<asp:CheckBox runat="server" ID="uiToggleSelectAllNonMemberContactsCheckBox" Checked="true" Text="<small>Select / Deselect All</small>" />
	<div runat="server" id="uiSelectNonMemberContactsDiv">
		<asp:GridView ID="uiSelectNonMemberContactsGridView" runat="server"
			AutoGenerateColumns="false" 
			CssClass="dataGrid" BorderWidth="0" CellPadding="3"
			AlternatingRowStyle-CssClass="dataGridAltItem" RowStyle-CssClass="dataGridItem" SelectedRowStyle-CssClass="dataGridSelectedItem"
			HeaderStyle-CssClass="dataGridHeader" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top" OnRowDataBound="uiSelectNonMemberContactsGridView_RowDataBound" ShowHeader="false">
			<Columns>
				<asp:TemplateField>
					<ItemTemplate>
						<asp:CheckBox runat="server" ID="uiCheckBox" Checked="true" />
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<ItemTemplate>
						<div style="width:220; overflow:hidden">
							<asp:Label runat="server" ID="uiName" Text="<%# ((Octazen.AddressBook.Contact)Container.DataItem).Name %>"></asp:Label>
						</div>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<ItemTemplate>
						<div style="width:300; overflow:hidden">
							<asp:Label runat="server" ID="uiEmailAddress" Text="<%# ((Octazen.AddressBook.Contact)Container.DataItem).Email %>"></asp:Label>
						</div>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</div>
	<p><button onclick="GoBack()">Back</button>&nbsp;<button runat="server" onserverclick="InviteSelectedContacts_Click">Invite selected contacts</button></p>

	<dsi:InlineScript runat="server">
	<script>
		function GoBack()
		{
			window.location = '<%= HttpContext.Current.Request.Url %>';
		}
		function ToggleSelectAllMemberContacts()
		{
			var check = document.getElementById('<%= uiToggleSelectAllMemberContactsCheckBox.ClientID %>').checked;
			var checkboxList = new Array(<%= MemberContactCheckBoxClientIDsAsString %>);
			for (i=0; i < checkboxList.length; i++)
			{
				document.getElementById(checkboxList[i]).checked = check;
			}
		}
		function ToggleSelectAllNonMemberContacts()
		{
			var check = document.getElementById('<%= uiToggleSelectAllNonMemberContactsCheckBox.ClientID %>').checked;
			var checkboxList = new Array(<%= NonMemberContactCheckBoxClientIDsAsString %>);
			for (i=0; i < checkboxList.length; i++)
			{
				document.getElementById(checkboxList[i]).checked = check;
			}
		}
	</script>
	</dsi:InlineScript>
</asp:Panel>

<asp:Panel runat="server" ID="uiSuccess">
	<asp:Label runat="server" ID="uiNoContactsAddedLabel" Text="No contacts were added."></asp:Label>
	<asp:Literal runat="server" ID="uiBuddiesRequestedList"></asp:Literal>
	<asp:Literal runat="server" ID="uiEmailsSentList"></asp:Literal>
</asp:Panel>

