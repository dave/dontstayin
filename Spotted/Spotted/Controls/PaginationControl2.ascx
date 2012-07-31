<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="PaginationControl2.ascx.cs" Inherits="Spotted.Controls.PaginationControl2" %>
<div style="padding:2px 8px 2px 8px;" runat="server" id="uiContainer">
	<p>
		<center>
			<dsi:UrlButton CausesValidation="false" ID="uiPrevPage" runat="server" Href="<%# PageUrl(PrevPage, UrlPartsThatShouldBeUsedWhenMakingNextAndPrevPageLinks.ToArray()) %>"><img src="/gfx/icon-back-12.png" alt="Prev page" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</dsi:UrlButton>
			<span runat="server" id="uiCurrentPage"><%# CurrentPage.ToString() %></span> of <span runat="server" id="uiLastPage"><%# LastPage.ToString() %></span>
			<dsi:UrlButton CausesValidation="false" ID="uiNextPage" runat="server" Href="<%# PageUrl(NextPage, UrlPartsThatShouldBeUsedWhenMakingNextAndPrevPageLinks.ToArray()) %>">next page<img src="/gfx/icon-forward-12.png" alt="Next page" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></dsi:UrlButton>
		</center>
	</p>
</div>
