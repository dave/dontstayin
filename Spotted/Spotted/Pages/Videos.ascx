<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Videos.ascx.cs" Inherits="Spotted.Pages.Videos" %>
<%@ Register src="~/Controls/PhotoBrowser.ascx" tagname="PhotoBrowser" tagprefix="uc1" %>
<%@ Register src="~/Controls/PhotoControl.ascx" tagname="PhotoControl" tagprefix="uc2" %>
<%@ Register src="~/Controls/LatestChat.ascx" tagname="LatestChat" tagprefix="uc3" %>
<%@ Register src="~/Controls/ThreadControl.ascx" tagname="ThreadControl" tagprefix="uc4" %>

<dsi:h1 runat="server" id="uiTitle">Recent videos</dsi:h1>

<dsi:h1 runat="server">Photo browser</dsi:h1>
<div class="ContentBorder">
	<uc1:PhotoBrowser ID="uiVideoBrowser" runat="server" PageSize="<%# Common.Properties.PhotoBrowser.IconsPerPage %>" RowLength="<%# Common.Properties.PhotoBrowser.IconsPerRow %>"/>
	<uc2:PhotoControl ID="uiVideoControl" runat="server" />
</div>

<uc3:LatestChat ID="uiLatestChat" runat="server" ParentObjectType="Photo" Items="200" ShowHolder="true" />
<uc4:ThreadControl ID="uiThreadControl" runat="server" ParentObjectType="Photo" />

