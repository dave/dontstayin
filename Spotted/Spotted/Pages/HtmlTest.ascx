<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HtmlTest.ascx.cs" Inherits="Spotted.Pages.HtmlTest" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<%@ Register src="../Controls/MultiBuddyChooser.ascx" tagname="MultiBuddyChooser" tagprefix="uc1" %>
<%@ Register src="../Controls/VenueGetter.ascx" tagname="VenueGetter" tagprefix="uc1" %>
<%@ Register src="../Controls/EventGetter.ascx" tagname="EventGetter" tagprefix="uc1" %>

<dsi:h1 runat="server">Event getter test</dsi:h1>
<div class="ContentBorder">
	<p>
		
		<uc1:EventGetter runat="server" ID="uiEventGetter" />
	</p>
</div>


<dsi:h1 runat="server">Venue getter test</dsi:h1>
<div class="ContentBorder">
	<p>
		<uc1:VenueGetter runat="server" ID="uiVenueGetter" />
	</p>
</div>

<dsi:h1 runat="server">Html control test</dsi:h1>
<div class="ContentBorder">
	<p>
		<a href="/pages/html">Reload</a>
	</p>
	<p>
		&nbsp;<uc1:MultiBuddyChooser ID="MultiBuddyChooser1" runat="server" />
		<asp:Button ID="Button1" runat="server" Text="Button" />
	</p>
	<p>
		<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
		</asp:GridView>
	</p>
		
</div>
