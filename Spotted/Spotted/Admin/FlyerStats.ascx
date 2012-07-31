<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlyerStats.ascx.cs" Inherits="Spotted.Admin.FlyerStats" %>

<p>
	<asp:GridView runat="server" ID="uiGridView" AutoGenerateColumns="false" class="dataGrid">
		<Columns>
			<asp:TemplateField HeaderText="View">
				<ItemTemplate>
					<a href='/admin/flyerview/k-<%# ((Flyer)Container.DataItem).K %>'>View</a>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Edit">
				<ItemTemplate>
					<a href='<%# (((Flyer)Container.DataItem).IsEditable) ? "/admin/flyeredit/k-"+((Flyer)Container.DataItem).K : "" %>' ><%# (((Flyer)Container.DataItem).IsEditable) ? "Edit" : "" %></a>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="K">
				<ItemTemplate>
					<%# ((Flyer)Container.DataItem).K %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Promoter / from">
				<ItemTemplate>
					<a href='<%# ((Flyer)Container.DataItem).PromoterK > 0 ? ((Flyer)Container.DataItem).Promoter.Url() : "" %>'><%# ((Flyer)Container.DataItem).PromoterK > 0 ? ((Flyer)Container.DataItem).Promoter.Name : "" %></a> / 
					<%# ((Flyer)Container.DataItem).MailFromDisplayName %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Name">
				<ItemTemplate>
					<%# ((Flyer)Container.DataItem).Name.TruncateWithDots(40) %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Subject" Visible="false">
				<ItemTemplate>
					<%# ((Flyer)Container.DataItem).Subject.TruncateWithDots(40) %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Preview">
				<ItemTemplate>
					<a href='<%# (((Flyer)Container.DataItem).MiscK > 0) ? ((Flyer)Container.DataItem).Misc.Url() : "" %>'><%# (((Flyer)Container.DataItem).MiscK > 0) ? "Preview" : "" %></a>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Link">
				<ItemTemplate>
					<a href='<%# ((Flyer)Container.DataItem).LinkTargetUrl %>' title='<%# ((Flyer)Container.DataItem).LinkTargetUrl %>'><%# (((Flyer)Container.DataItem).LinkTargetUrl.Length > 0) ? "Link" : ""%></a>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Popup">
				<ItemTemplate>
					<a href='<%# (((Flyer)Container.DataItem).MiscK > 0) ? "/popup/flyer/k-" + ((Flyer)Container.DataItem).K : "" %>'><%# (((Flyer)Container.DataItem).MiscK > 0) ? "Popup" : "" %></a>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Send date">
				<ItemTemplate>
					<nobr><%# ((Flyer)Container.DataItem).SendDateTime.ToString("yyyy-MM-dd") %></nobr>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Sent">
				<ItemTemplate>
					<center><%# ((Flyer)Container.DataItem).Sends.ToString("N0") %></center>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Broken">
				<ItemTemplate>
					<center><%# ((Flyer)Container.DataItem).Broken.ToString("N0") %></center>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Exceptions">
				<ItemTemplate>
					<center><%# ((Flyer)Container.DataItem).Exceptions.ToString("N0") %></center>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Retries">
				<ItemTemplate>
					<center><%# ((Flyer)Container.DataItem).MailServerRetries.ToString("N0") %></center>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Viewed">
				<ItemTemplate>
					<center><%# ((Flyer)Container.DataItem).Views.ToString("N0") %></center>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Clicked">
				<ItemTemplate>
					<center><%# ((Flyer)Container.DataItem).Clicks.ToString("N0") %></center>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Click rate">
				<ItemTemplate>
					<nobr><%# ((((Flyer)Container.DataItem).Views == 0) ? 0 : (((double)((Flyer)Container.DataItem).Clicks) / ((double)((Flyer)Container.DataItem).Views))).ToString("p2")%></nobr>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Unsubscribed">
				<ItemTemplate>
					<center><%# ((Flyer)Container.DataItem).Unsubscribes %></center>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Unsubscribe rate">
				<ItemTemplate>
					<nobr><%# ((((Flyer)Container.DataItem).Views == 0) ? 0 : (((double)((Flyer)Container.DataItem).Unsubscribes) / ((double)((Flyer)Container.DataItem).Views))).ToString("p2")%></nobr>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
</p>
<p>
	<center>
		<a href="/admin/flyerstats/all">View all</a>
	</center>
</p>
