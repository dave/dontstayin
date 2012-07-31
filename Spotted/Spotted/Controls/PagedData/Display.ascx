<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="Spotted.Controls.PagedData.Display" %>
<%@ Register Src="~/Controls/PaginationControl2.ascx" TagPrefix="dsi" TagName="PaginationControl2" %>
<%@ Register Src="~/Controls/ClientSideRepeater/Repeater.ascx" TagPrefix="dsi" TagName="ClientSideRepeater" %>

<asp:Panel runat="server" ID="uiPanel">
	
	<dsi:PaginationControl2 runat="server" ID="uiPager" HideBorder="true" />
	<dsi:ClientSideRepeater runat="server" ID="uiRepeater" />
	<asp:HiddenField runat="server" ID="uiDefaultTop" />
	<asp:HiddenField runat="server" ID="uiPageSize" />
	<asp:HiddenField runat="server" ID="uiServicePath" />
	<asp:HiddenField runat="server" ID="uiServiceMethod" />
	<asp:HiddenField runat="server" ID="uiTimeout" />
	<asp:HiddenField runat="server" ID="uiTabName" />
</asp:Panel>
