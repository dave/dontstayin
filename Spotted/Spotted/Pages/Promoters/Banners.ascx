<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banners.ascx.cs" Inherits="Spotted.Pages.Promoters.Banners" %>

<asp:Panel Runat="server" ID="PanelBannerList">
	<dsi:PromoterIntro runat="server" ID="Promoterintro1" Header="Banners">
		<p>
			<a href="<%= CurrentPromoter.UrlApp("banneredit","mode","add") %>"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add a banner</a>
		</p>
		<p>
			<a href="<%= CurrentPromoter.UrlApp("bannerspending") %>"><img src="/gfx/icon-document-tick.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">book pending banners</a>
		</p>
		<p>
			<b>All your banners are listed below:</b>
		</p>
	</dsi:PromoterIntro>
	<dsi:h1 runat="server" ID="BannerListHeader">Banners added for promoter: ???</dsi:h1>
	<div class="ContentBorder" style="padding-right:0px; padding-left:0px;">
		<p>
			<center>
				<asp:DropDownList runat="server" ID="FolderDropDown" OnSelectedIndexChanged="Folder_Change" AutoPostBack="true"></asp:DropDownList>
			</center>
		</p>
		<p>
			<asp:DataGrid Runat="server" ID="BannerListDataGrid" 
				GridLines="None" AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				AlternatingItemStyle-CssClass="dataGridAltItem"
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="BannerListDataGridChangePage"
				PageSize="20" PagerStyle-Mode="NumericPages" Width="100%">
				<Columns>
					<asp:TemplateColumn HeaderText="Our<br>ref" ItemStyle-BorderWidth="0">
						<ItemTemplate>
							<%#((Bobs.Banner)(Container.DataItem)).K%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Your ref">
						<ItemTemplate>
							<a href="<%#((Bobs.Banner)(Container.DataItem)).OptionsUrl()%>"><%#((Bobs.Banner)(Container.DataItem)).Name%></a>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Dates">
						<ItemTemplate>
							<nobr><%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Banner)(Container.DataItem)).FirstDay,true,true)%> -</nobr><br />
							<nobr><%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Banner)(Container.DataItem)).LastDay,false,true)%></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Price" ItemStyle-HorizontalAlign="Right">
						<ItemTemplate>
							<%#((Bobs.Banner)(Container.DataItem)).PriceString%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Booked">
						<ItemTemplate>
							<img src="<%#((Bobs.Banner)(Container.DataItem)).StatusBooked?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Ready">
						<ItemTemplate>
							<img src="<%#((Bobs.Banner)(Container.DataItem)).IsReady?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Live">
						<ItemTemplate>
							<img src="<%#((Bobs.Banner)(Container.DataItem)).IsLive?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Info">
						<ItemTemplate>
							<%#((Bobs.Banner)(Container.DataItem)).InfoText %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Options">
						<ItemTemplate>
							<nobr>
								<a href="<%#((Bobs.Banner)(Container.DataItem)).Promoter.UrlApp("banneroptions","mode","delete","bannerk",((Bobs.Banner)(Container.DataItem)).K.ToString())%>" onclick="return confirm('Are you sure?');">Delete</a>
							</nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
		<p class="MedCenter">
			<a href="<%= CurrentPromoter.UrlApp("banneredit","mode","add") %>"><img src="/gfx/icon-add.png" border="0"  width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add a banner</a>
		</p>
	</div>
</asp:Panel>
