<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Photos.ascx.cs" Inherits="Spotted.Pages.Articles.Photos" %>
<%@ Register src="../../Controls/PhotoBrowser.ascx" tagname="PhotoBrowser" tagprefix="uc1" %>
<%@ Register src="../../Controls/PhotoControl.ascx" tagname="PhotoControl" tagprefix="uc2" %>
<%@ Register src="../../Controls/LatestChat.ascx" tagname="LatestChat" tagprefix="uc3" %>
<%@ Register src="../../Controls/ThreadControl.ascx" tagname="ThreadControl" tagprefix="uc4" %>

<a name="ContentStart"></a>
<dsi:h1 runat="server" id="uiTitle">Article photos</dsi:h1>
<div class="ContentBorder" style="text-align:center">
	<p style="font-size:18px;font-weight:bold;margin-top:12px;margin-bottom:10px; text-align:center;">
		<span ID="uiArticleInfoSpan" runat="server"><%# ArticleInfoHtml %></span>
	</p>
</div>

<dsi:h1 runat="server">Photo browser</dsi:h1>
<div class="ContentBorder">
	<uc1:PhotoBrowser ID="uiPhotoBrowser" runat="server" PageSize="<%# Common.Properties.PhotoBrowser.IconsPerPage %>" RowLength="<%# Common.Properties.PhotoBrowser.IconsPerRow %>"/>
	<uc2:PhotoControl ID="uiPhotoControl" runat="server" />
</div>

<uc3:LatestChat ID="uiLatestChat" runat="server" ParentObjectType="Photo" Items="200" ShowHolder="true" />
<uc4:ThreadControl ID="uiThreadControl" runat="server" ParentObjectType="Photo" />

<input runat="server" id="uiArticleK" type="hidden" />
