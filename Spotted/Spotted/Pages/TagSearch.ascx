<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TagSearch.ascx.cs" Inherits="Spotted.Pages.TagSearch" %>
<%@ Register src="../Controls/SearchBoxControl.ascx" tagname="SearchBoxControl" tagprefix="uc1" %>
<%@ Register src="../Controls/TagCloud.ascx" tagname="TagCloud" tagprefix="uc5" %>
<%@ Register src="../Controls/PhotoBrowser.ascx" tagname="PhotoBrowser" tagprefix="uc2" %>
<%@ Register src="../Controls/PhotoControl.ascx" tagname="PhotoControl" tagprefix="uc3" %>
<%@ Register src="../Controls/LatestChat.ascx" tagname="LatestChat" tagprefix="uc6" %>
<%@ Register src="../Controls/ThreadControl.ascx" tagname="ThreadControl" tagprefix="uc7" %>

<dsi:h1 id="uiTitle" runat="server">Tag search</dsi:h1>
<uc5:TagCloud ID="uiTagCloud" runat="server" NumberOfItems="100"></uc5:TagCloud>

<asp:Panel ID="uiSearchBoxPanel" runat="server" class="ContentBorder" style="text-align:center">
<p>
	<uc1:SearchBoxControl ID="uiSearchBoxControl" runat="server" Title="Tag search" />
</p>
</asp:Panel>

<dsi:h1 runat="server">Photo browser</dsi:h1>
<div class="ContentBorder">
	<uc2:PhotoBrowser ID="uiPhotoBrowser" runat="server" PageSize="<%# Common.Properties.PhotoBrowser.IconsPerPage %>" RowLength="<%# Common.Properties.PhotoBrowser.IconsPerRow %>" />
	<uc3:PhotoControl ID="uiPhotoControl" runat="server" IncludeEventInfoInAboutHtml="true" />
</div>
<uc6:LatestChat ID="uiLatestChat" runat="server" ParentObjectType="Photo" Items="200" ShowHolder="true" />
<uc7:ThreadControl ID="uiThreadControl" runat="server" />

<input runat="server" id="uiTagK" type="hidden" />
