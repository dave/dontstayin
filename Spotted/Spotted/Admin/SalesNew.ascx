<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesNew.ascx.cs" Inherits="Spotted.Admin.SalesNew" %>
<div class="ContentBorder">
	<p>
		This page lists new clients as they sign up to the site. Clients that haven't
		had a call yet are in the top list. As soon as you make a call to them, they 
		move from the top list to the bottom list. They are hidden from the bottom 
		list until the next-call date. 
	</p>
	<p>
		<%= ContainerPage.Url[0].Equals("all") ? "We're showing the new list for the whole sales team - feel free to take over any of these clients" : "We're only showing clients assigned to you" %>
	</p>
	<h2>
		New to call today
	</h2>
	<p>
		<asp:DataGrid Runat="server" ID="NewPromoterDataGrid" 
			GridLines="None" AutoGenerateColumns="False"
			BorderWidth=0 CellPadding=3 CssClass=dataGrid 
			AlternatingItemStyle-CssClass="dataGridAltItem"
			HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
			ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="NewChangePage"
			PageSize="10" PagerStyle-Mode="NumericPages" OnItemCommand="NewDataGridCommand">
			<Columns>
				<asp:TemplateColumn HeaderText="Estimate">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).SalesEstimateString%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Name">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).Link()%>
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
				<asp:TemplateColumn HeaderText="Status">
					<ItemTemplate>
						<nobr><%#((Bobs.Promoter)Container.DataItem).EffectiveSalesStatus%></nobr>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Notes">
					<ItemTemplate>
						<%#Cambro.Misc.Utility.Snip(((Bobs.Promoter)Container.DataItem).ManualNote, 100).Replace("\n", "<br>")%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Signed up">
					<ItemTemplate>
						<%#Cambro.Misc.Utility.FriendlyTime(((Bobs.Promoter)Container.DataItem).DateTimeSignUp, true)%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Remove">
					<ItemTemplate>
						<asp:LinkButton OnClientClick="return confirm('Are you sure?');" runat="server" ID="RemoveLinkButton" CommandArgument="<%#((Bobs.Promoter)Container.DataItem).K%>" CommandName="Remove" Text="Remove" />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Transferred">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).RecentlyTransferred ? "<b><a href=\"" + ((Bobs.Promoter)Container.DataItem).Url("clearrecentlytransferred", "1") + "\">TRANSFERRED</a></b>" : "" %>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Sales call">
					<ItemTemplate>
						<a href="<%#((Bobs.Promoter)Container.DataItem).Url("callnow", "true")%>" target="_blank"><%#((Bobs.Promoter)Container.DataItem).PhoneNumber%></a>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
	
	<h2>
		Call backs
	</h2>
	
	<p>
		<asp:DataGrid Runat="server" ID="CallBacksDataGrid" 
			GridLines="None" AutoGenerateColumns="False"
			BorderWidth=0 CellPadding=3 CssClass=dataGrid 
			AlternatingItemStyle-CssClass="dataGridAltItem"
			HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
			ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="CallBacksChangePage"
			PageSize="10" PagerStyle-Mode="NumericPages" OnItemCommand="CallBacksDataGridCommand">
			<Columns>
				<asp:TemplateColumn HeaderText="Estimate">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).SalesEstimateString%>
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
				<asp:TemplateColumn HeaderText="Status">
					<ItemTemplate>
						<nobr><%#((Bobs.Promoter)Container.DataItem).EffectiveSalesStatus%></nobr>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Notes">
					<ItemTemplate>
						<%#Cambro.Misc.Utility.Snip(((Bobs.Promoter)Container.DataItem).ManualNote, 100).Replace("\n", "<br>")%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Next call up">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).SalesNextCallRender%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Remove">
					<ItemTemplate>
						<asp:LinkButton OnClientClick="return confirm('Are you sure?');" runat="server" ID="RemoveLinkButton" CommandArgument="<%#((Bobs.Promoter)Container.DataItem).K%>" CommandName="Remove" Text="Remove" />
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Transferred">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).RecentlyTransferred ? "<b><a href=\"" + ((Bobs.Promoter)Container.DataItem).Url("clearrecentlytransferred", "1") + "\">TRANSFERRED</a></b>" : "" %>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Sales call">
					<ItemTemplate>
						<a href="<%#((Bobs.Promoter)Container.DataItem).Url("callnow", "true")%>" target="_blank"><%#((Bobs.Promoter)Container.DataItem).PhoneNumber%></a>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</div>
