<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TagCloud.ascx.cs" Inherits="Spotted.Controls.TagCloud" %>
<%@ Register src="LinkCloud.ascx" tagname="LinkCloud" tagprefix="uc1" %>
<%@ Register src="~/Controls/SearchBoxControl.ascx" tagname="SearchControl" tagprefix="uc1" %>

<dsi:h1 id="uiTitle" runat="server">Popular tags</dsi:h1>
<asp:Panel ID ="uiPanel" runat="server" CssClass="ContentBorder" HorizontalAlign="Center">
	<p>
		<uc1:SearchControl ID="uiSearchControl" runat="server" />
	</p>
	<p>
		<uc1:LinkCloud ID="uiLinkCloud" runat="server" />
	</p>
</asp:Panel>
