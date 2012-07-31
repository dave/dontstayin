<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerDataGrid.ascx.cs" Inherits="Spotted.Controls.Admin.BannerDataGrid" %>
<asp:DataGrid Runat="server" ID="BannersDataGrid" 
	GridLines="None" AutoGenerateColumns="False"
	BorderWidth=0 CellPadding=3 CssClass=dataGrid 
	AlternatingItemStyle-CssClass="dataGridAltItem"
	HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
	ItemStyle-VerticalAlign="Top">
	<Columns>
		<asp:TemplateColumn HeaderText="K">
			<ItemTemplate>
				<%#((Bobs.Banner)(Container.DataItem)).K%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Dates">
			<ItemTemplate>
				<nobr><%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Banner)(Container.DataItem)).FirstDay,true,false)%></nobr><br>
				<nobr><%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Banner)(Container.DataItem)).LastDay,true,false)%></nobr>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Type" ItemStyle-Width="250">
			<ItemTemplate>
				<b class="CleanLinks"><a href="<%#((Bobs.Banner)(Container.DataItem)).Promoter.Url()%>" target="_blank"><%#((Bobs.Banner)(Container.DataItem)).Promoter.Name%></a></b> - 
				<small class="CleanLinks"><%#((Bobs.Banner)(Container.DataItem)).LinkTargetHtml%><br>
				<%#((Bobs.Banner)(Container.DataItem)).PositionString(true)%>, 
				<%#((Bobs.Banner)(Container.DataItem)).DisplayTypeString(false)%></small>
				
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Ready?">
			<ItemTemplate>
				<img src="<%#((Bobs.Banner)(Container.DataItem)).IsReady?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Live">
			<ItemTemplate>
				<img src="<%#((Bobs.Banner)(Container.DataItem)).IsLive?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Edit">
			<ItemTemplate>
				<nobr>
					<a href="<%#((Bobs.Banner)(Container.DataItem)).OptionsUrl()%>">Options</a> | <a href="<%#((Bobs.Banner)(Container.DataItem)).EditUrl()%>">Edit</a>
				</nobr>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Delete">
			<ItemTemplate>
				<nobr>
					<a href="<%#((Bobs.Banner)(Container.DataItem)).Promoter.UrlApp("banneroptions","mode","delete","bannerk",((Bobs.Banner)(Container.DataItem)).K.ToString())%>" onclick="return confirm('Are you sure?');">Delete</a>
				</nobr>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Hits">
			<ItemTemplate>
				<%#((Bobs.Banner)(Container.DataItem)).TotalHits.ToString("#,##0")%><br>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Total clicks<br>(rate)">
			<ItemTemplate>
				<%#((Bobs.Banner)(Container.DataItem)).TotalClicks.ToString("#,##0")%><br>
				<small>(<%#((double)((Bobs.Banner)(Container.DataItem)).TotalClicks / (double)((Bobs.Banner)(Container.DataItem)).TotalHits).ToString("0.00%")%>)</small>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Hits today">
			<ItemTemplate>
				<%#BannerStat.GetBannerStatTotals(((Bobs.Banner)(Container.DataItem)).K)[0].Hits.ToString("#,##0")%>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:DataGrid>
<asp:Panel Runat="server" ID="NoBanners">
	No banners
</asp:Panel>
