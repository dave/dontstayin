<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tags.ascx.cs" Inherits="Spotted.Admin.Tags" %>
<dsi:h1 id="uiTitle" runat="server">Filters</dsi:h1>
<asp:Panel ID="uiFiltersPanel" runat="server" DefaultButton="uiFilter" class="ContentBorder" >
	<p>
		Tag:
		<asp:TextBox ID="uiTagTextFilter" runat="server" Text="*"></asp:TextBox>
	</p>
	<p>
		Blocked:
		<asp:RadioButtonList ID="uiBlockedFilter" runat="server">
			<asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
			<asp:ListItem Text="Blocked" Value="1"></asp:ListItem>
			<asp:ListItem Text="Unblocked" Value="0"></asp:ListItem>
		</asp:RadioButtonList>
	</p>
	<p>
		Show in tag cloud:
		<asp:RadioButtonList ID="uiShowInTagCloudFilter" runat="server">
			<asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
			<asp:ListItem Text="Show in tag cloud" Value="1"></asp:ListItem>
			<asp:ListItem Text="Don't show in tag cloud" Value="0"></asp:ListItem>
		</asp:RadioButtonList>
	</p>
	<p>
		<asp:Button ID="uiFilter" runat="server" Text="Get tags matching filters"/>
	</p>
</asp:Panel>
<dsi:h1 id="uiResultsTitle" runat="server" Visible="false">Results</dsi:h1>
<asp:Panel ID="uiResultsPanel" runat="server" Visible="false" CssClass="ContentBorder">
	<asp:Button ID="uiSaveChanges" runat="server" Text="Save changes" disabled="1"/>
	<asp:GridView id="uiResults" runat="server" AutoGenerateColumns="false" >
		<Columns>
			<asp:BoundField DataField="K" />
			<asp:BoundField DataField="TagText" HeaderText="Tag" />
			<asp:TemplateField HeaderText="Block">
				<ItemTemplate>
					<asp:CheckBox ID="uiBlockedCheckBox" runat="server" Checked="<%# ((Tag)Container.DataItem).Blocked %>"/>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Show in tag cloud">
				<ItemTemplate>
					<asp:CheckBox ID="uiShowInTagCloudCheckBox" runat="server" Checked="<%# ((Tag)Container.DataItem).ShowInTagCloud %>" />
				</ItemTemplate>
			</asp:TemplateField>
			
			
		</Columns>
	</asp:GridView>
</asp:Panel>
