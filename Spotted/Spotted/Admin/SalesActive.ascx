<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesActive.ascx.cs" Inherits="Spotted.Admin.SalesActive" %>
<div class="ContentBorder">
	<p>
		This is your active list. If a client makes a purchase, they go onto your active list for the next 
		three months. Each time they make a sale, the three months is extended. After the three months 
		is up, the client will be listed on the shared idle page for other sales tem members to have a go. 
		We also list expired clients in the bottom table.
	</p>
	<p>
		We list all your clients here, even before the next-call date.
	</p>
	
	<table border="0" cellpadding="3" cellspacing="0">
		<tr>
			<td>Sales estimate filter:</td>
			<td><asp:DropDownList ID="SalesEstimateFilterDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SalesEstimateFilterDropDownList_SelectedIndexChanged">
				</asp:DropDownList></td>
			<td style="padding-left:20px;">Sector filter:</td>
			<td><asp:DropDownList ID="SectorFilterDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="SectorFilterDropDownList_SelectedIndexChanged">
				</asp:DropDownList></td>
		</tr>
	</table>
	<p>
		<asp:DataGrid Runat="server" ID="PromoterDataGrid" 
			GridLines="None" AutoGenerateColumns="False"
			BorderWidth=0 CellPadding=3 CssClass=dataGrid 
			AlternatingItemStyle-CssClass="dataGridAltItem"
			HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
			ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="DataGridChangePage"
			PageSize="100" PagerStyle-Mode="NumericPages">
			<Columns>
				<asp:TemplateColumn HeaderText="Estimate">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).SalesEstimateString%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Next call">
					<ItemTemplate>
						<nobr><%#((Bobs.Promoter)Container.DataItem).SalesNextCallRender%></nobr>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Name">
					<ItemTemplate>
						<a href="<%#((Bobs.Promoter)Container.DataItem).Url()%>"><%#((Bobs.Promoter)Container.DataItem).Name%></a>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Contact">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).ContactName%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Primary usr">
					<ItemTemplate>
						<asp:Image runat="server" ID="PrimaryUsrOnlineImage" ImageAlign="TextTop" ImageUrl="/gfx/icon-me-small-up.png" Visible="<%# ((Bobs.Promoter)Container.DataItem).PrimaryUsr == null ? false : ((Bobs.Promoter)Container.DataItem).PrimaryUsr.LoggedInNow %>" />
						<asp:Image runat="server" ID="PrimaryUsrOfflineImage" ImageAlign="TextTop" ImageUrl="/gfx/icon-me-small-dn.png" Visible="<%# ((Bobs.Promoter)Container.DataItem).PrimaryUsr == null ? true : !((Bobs.Promoter)Container.DataItem).PrimaryUsr.LoggedInNow %>" />
						<%#((Bobs.Promoter)Container.DataItem).PrimaryUsrLink%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Expires">
					<ItemTemplate>
						<nobr><%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Promoter)Container.DataItem).SalesStatusExpires, true, false)%></nobr>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Notes">
					<ItemTemplate>
						<%#Cambro.Misc.Utility.Snip(((Bobs.Promoter)Container.DataItem).ManualNote, 100).Replace("\n", "<br>")%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Transferred">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).RecentlyTransferred ? "<b><a href=\"" + ((Bobs.Promoter)Container.DataItem).Url("clearrecentlytransferred", "1") + "\">TRANSFERRED</a></b>" : "" %>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
	
	
	<h2>Expired</h2>
	<p>
		<asp:DataGrid Runat="server" ID="ExpiredDataGrid" 
			GridLines="None" AutoGenerateColumns="False"
			BorderWidth=0 CellPadding=3 CssClass=dataGrid 
			AlternatingItemStyle-CssClass="dataGridAltItem"
			HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
			ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="ExpiredDataGridChangePage"
			PageSize="100" PagerStyle-Mode="NumericPages">
			<Columns>
				<asp:TemplateColumn HeaderText="Estimate">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).SalesEstimateString%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Next call">
					<ItemTemplate>
						<nobr><%#((Bobs.Promoter)Container.DataItem).SalesNextCallRender%></nobr>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Name">
					<ItemTemplate>
						<a href="<%#((Bobs.Promoter)Container.DataItem).Url()%>"><%#((Bobs.Promoter)Container.DataItem).Name%></a>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Contact">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).ContactName%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Primary usr">
					<ItemTemplate>
						<asp:Image runat="server" ID="PrimaryUsrOnlineImage" ImageAlign="TextTop" ImageUrl="/gfx/icon-me-small-up.png" Visible="<%# ((Bobs.Promoter)Container.DataItem).PrimaryUsr == null ? false : ((Bobs.Promoter)Container.DataItem).PrimaryUsr.LoggedInNow %>" />
						<asp:Image runat="server" ID="PrimaryUsrOfflineImage" ImageAlign="TextTop" ImageUrl="/gfx/icon-me-small-dn.png" Visible="<%# ((Bobs.Promoter)Container.DataItem).PrimaryUsr == null ? true : !((Bobs.Promoter)Container.DataItem).PrimaryUsr.LoggedInNow %>" />
						<%#((Bobs.Promoter)Container.DataItem).PrimaryUsrLink%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Expires">
					<ItemTemplate>
						<nobr><%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Promoter)Container.DataItem).SalesStatusExpires, true, false)%></nobr>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Notes">
					<ItemTemplate>
						<%#Cambro.Misc.Utility.Snip(((Bobs.Promoter)Container.DataItem).ManualNote, 100).Replace("\n", "<br>")%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Transferred">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).RecentlyTransferred ? "<b><a href=\"" + ((Bobs.Promoter)Container.DataItem).Url("clearrecentlytransferred", "1") + "\">TRANSFERRED</a></b>" : "" %>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</div>
