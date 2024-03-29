<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopContent.ascx.cs" Inherits="Spotted.Pages.TopContent" %>
<!--%@ OutputCache Duration="600" VaryByCustom="PageName" VaryByParam="*" %-->

<dsi:h1 runat="server" ID="H18" NAME="H18">Top <%= ContainerPage.Url["photos"].Exists ? "photos" : (ContainerPage.Url["videos"].Exists ? "videos" : "photos / videos") %></dsi:h1>
<div class="ContentBorder">
	<p runat="server" id="PhotosPageP1" align="center">
		<asp:HyperLink runat="server" id="PhotosPrevPage1"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... 
		<asp:HyperLink runat="server" id="PhotosNextPage1">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
	</p>
	<p class="CleanLinks">
		<asp:DataList runat="server" ID="PhotosDataList" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" CellPadding="5" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Top" Width="100%" ItemStyle-Width="33%" />
	</p>
	<p runat="server" id="PhotosPageP2" align="center">
		<asp:HyperLink runat="server" id="PhotosPrevPage2"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... 
		<asp:HyperLink runat="server" id="PhotosNextPage2">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
	</p>
</div>
