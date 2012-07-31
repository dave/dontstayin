<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaginationControl.ascx.cs" Inherits="Spotted.Controls.PaginationControl" %>
<div align="center" style="width:auto;">
	<asp:LinkButton ID="uiPrevPage" runat="server" onclick="uiPrevPage_Click"><img src="/gfx/icon-back-12.png" alt="Prev page" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:LinkButton>
	<asp:Label ID="uiCurrentPage" runat="server" Text=""></asp:Label>&nbsp;of&nbsp;<asp:Label ID="uiPageCount" runat="server" Text=""></asp:Label>
	<asp:LinkButton ID="uiNextPage" runat="server" onclick="uiNextPage_Click">next page<img src="/gfx/icon-forward-12.png" alt="Next page" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:LinkButton>
</div>
