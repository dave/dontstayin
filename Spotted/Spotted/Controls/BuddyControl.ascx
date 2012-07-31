<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuddyControl.ascx.cs" Inherits="Spotted.Controls.BuddyControl" %>

<dsi:InlineScript runat="server">
	<script>
		DbButtonInit(<%= Bobs.Vars.LanguageString %>);
	</script>
</dsi:InlineScript>

<div runat="server" id="uiToggleSelectAll">
	<dsi:InlineScript runat="server">
		<script>
		function ToggleSelectAllMemberContacts()
		{
			var check = document.getElementById('<%= uiToggleSelectAllCheckBox.ClientID %>').checked;
			var checkboxList = new Array(<%= MemberContactCheckBoxClientIDsAsString %>);
			for (i=0; i < checkboxList.length; i++)
			{
				document.getElementById(checkboxList[i]).checked = check;
			}
		}
		</script>
	</dsi:InlineScript>
	<asp:Checkbox runat="server" onclick="ToggleSelectAllMemberContacts()" id="uiToggleSelectAllCheckBox" Text="<small>Select / Deselect All</small>" />
</div>

<asp:GridView Runat="server" 
	ID="uiBuddy" 
	DataKeyNames="K"
	GridLines="None" 
	AutoGenerateColumns="False"
	ShowHeader="False"
	BorderWidth="0" 
	CellPadding="3" 
	CssClass="dataGrid" 
	RowStyle-VerticalAlign="Middle" 
	AllowPaging="True" 
	OnPageIndexChanging="uiBuddy_ChangePage"
	PageSize="20" 
	PagerStyle-Mode="NumericPages" 
	PagerStyle-CssClass="dataGridFooterGrey"
	OnRowDataBound="uiBuddy_RowDataBound">
	<Columns>
		<asp:TemplateField>
			<ItemTemplate>
				<asp:CheckBox runat="server" ID="uiCheckBox" />
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
			<ItemTemplate>
				<a <%# (((Bobs.Usr)(Container.DataItem)).AllowLinkToProfile(FindMethod) ? "href=\"" + ((Bobs.Usr)(Container.DataItem)).Url() + "\"" : "") %>>
					<img src="<%#((Bobs.Usr)(Container.DataItem)).AnyPicPath%>" class="BorderBlack All" border="0"
						<%= ImageSize == ImageSizes.Small ? "width=\"50\" height=\"50\"" : "width=\"100\" height=\"100\"" %>
					 />
				</a>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
			<ItemTemplate>
				<a style="<%= NameStyle %>" <%# (((Bobs.Usr)(Container.DataItem)).AllowLinkToProfile(FindMethod) ? "href=\"" + ((Bobs.Usr)(Container.DataItem)).Url() + "\"" : "") %> <%#((Bobs.Usr)(Container.DataItem)).Rollover%>>
					<%# ((Bobs.Usr)(Container.DataItem)).DisplayName(FindMethod) %>
				</a>
				<dsi:InlineScript runat="server">
					<asp:Literal runat="server" ID="uiDbButtonScripts"></asp:Literal>
				</dsi:InlineScript>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</asp:GridView>

<p runat="server" ID="uiNoRecords" Visible="false">No results to display.</p>
