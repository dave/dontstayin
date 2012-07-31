<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesIdle.ascx.cs" Inherits="Spotted.Admin.SalesIdle" %>
<div class="ContentBorder">
	<p>
		This is the idle list. Status "idle" means the promoter was once on 
		someones proactive list. This means we've had effective call to 
		them, so they're probably a pretty good lead. They didn't buy within 
		a month, so they changed from proactive to idle, are available for 
		the whole sales team to take over.
	</p>
	<p>
		Clients are hidden from this page until the next-call date.
	</p>
	<p>	
		Note: this page remembers what page you were on when you next come 
		back.
	</p>
	<p runat="server" id="PageNumberP" visible="false" class="BigCenter"/>
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
				<asp:TemplateColumn HeaderText="Transferred">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).RecentlyTransferred ? "<b><a href=\"" + ((Bobs.Promoter)Container.DataItem).Url("clearrecentlytransferred", "1") + "\">TRANSFERRED</a></b>" : "" %>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</div>
