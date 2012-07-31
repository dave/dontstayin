<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Photos.ascx.cs" Inherits="Spotted.Pages.Usrs.Photos" %>
<%@ Register TagPrefix="DsiControls" src="/Controls/PhotoBrowser.ascx" tagname="PhotoBrowser" %>
<%@ Register TagPrefix="DsiControls" src="/Controls/PhotoControl.ascx" tagname="PhotoControl" %>
<%@ Register TagPrefix="DsiControls" src="/Controls/LatestChat.ascx" tagname="LatestChat" %>
<%@ Register TagPrefix="DsiControls" src="/Controls/ThreadControl.ascx" tagname="ThreadControl" %>

<a name="ContentStart"></a>
<dsi:UsrIntro runat="server" ID="UsrIntro">
	<p>
		This page shows all photos of <%= UsrFromUrl.Link()%><span runat="server" ID="TakenBySpan"/>.
		You can look for photos taken on a certain day by using the calendar:
	</p>
	<p>
		<a href="<%= GetMonthUrl(DateTime.Today) %>"><img src="/gfx/icon-calendar.png" style="margin-right:3px;" border="0" height="21" width="26" align="absmiddle">Calendar</a>
	</p>
</dsi:UsrIntro>

<dsi:h1 runat="server">Photo browser</dsi:h1>
<div class="ContentBorder">
	<DsiControls:PhotoBrowser ID="uiPhotoBrowser" runat="server" PageSize="<%# Common.Properties.PhotoBrowser.IconsPerPage %>" RowLength="<%# Common.Properties.PhotoBrowser.IconsPerRow %>" />
	<DsiControls:PhotoControl ID="uiPhotoControl" runat="server" IncludeEventInfoInAboutHtml="true" />
</div>
<asp:UpdatePanel runat="server" ID="uiUpdatePanel">
	<ContentTemplate>
		<DsiControls:LatestChat ID="uiLatestChat" runat="server" ParentObjectType="Photo" Items="200" ShowHolder="true" />
		<DsiControls:ThreadControl ID="uiThreadControl" runat="server" ParentObjectType="Photo" />
	</ContentTemplate>
</asp:UpdatePanel>

<input runat="server" id="uiUsrK" type="hidden" />
<input runat="server" id="uiSpottedByUsrK" type="hidden" />
