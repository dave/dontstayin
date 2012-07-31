<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Photos.ascx.cs" Inherits="Spotted.Pages.Groups.Photos" %>
<%@ Register TagPrefix="DsiControls" src="/Controls/PhotoBrowser.ascx" tagname="PhotoBrowser" %>
<%@ Register TagPrefix="DsiControls" src="/Controls/PhotoControl.ascx" tagname="PhotoControl" %>
<%@ Register TagPrefix="DsiControls" src="/Controls/LatestChat.ascx" tagname="LatestChat" %>
<%@ Register TagPrefix="DsiControls" src="/Controls/ThreadControl.ascx" tagname="ThreadControl" %>

<a name="ContentStart"></a>
<dsi:h1 runat="server" id="uiTitle">Group photos</dsi:h1>
<div class="ContentBorder">
	<p style="font-size:18px;font-weight:bold;margin-top:12px;margin-bottom:10px; text-align:center;">
		Photos from <a href="<%= CurrentGroup.Url() %>"><%= CurrentGroup.Name %></a> group
	</p>
</div>

<dsi:h1 runat="server">Photo browser</dsi:h1>
<div class="ContentBorder">
	<DsiControls:PhotoBrowser ID="uiPhotoBrowser" runat="server" PageSize="<%# Common.Properties.PhotoBrowser.IconsPerPage %>" RowLength="<%# Common.Properties.PhotoBrowser.IconsPerRow %>"/>
	<DsiControls:PhotoControl ID="uiPhotoControl" runat="server" IncludeEventInfoInAboutHtml="true" />
</div>
<asp:UpdatePanel runat="server" ID="uiUpdatePanel">
	<ContentTemplate>
		<DsiControls:LatestChat ID="uiLatestChat" runat="server" ParentObjectType="Photo" Items="200" ShowHolder="true" />
		<DsiControls:ThreadControl ID="uiThreadControl" runat="server" ParentObjectType="Photo" />
	</ContentTemplate>
</asp:UpdatePanel>

<input runat="server" id="uiGroupK" type="hidden" />
