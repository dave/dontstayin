<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventCreator.ascx.cs" Inherits="Spotted.Controls.EventCreator" %>
<%@ Register Src="~/Controls/VenueGetter.ascx" TagPrefix="uc1" TagName="VenueGetter" %>
<div id="uiContainer" runat="server" style="display:none;position:absolute;left:350px;top:150px;z-index:910;width:400px;">
	<dsi:h1 ID="H1" runat="server">Select or add an event</dsi:h1>
	<div class="ContentBorder">
		<p>Fill out the form below to find an event. If the event isn't in the database, you'll be given the option to add it. You'll be able to alter the details later.</p>
		<p>If you want to add more detail you can use the <a href="/pages/events/edit">advanced event wizard</a></p>
		<p><b>What date is the event on?</b><br /><dsi:Cal runat="server" ID="uiCal" /></p>
		<p><b>What venue is the event at?</b><br /><uc1:VenueGetter runat="server" ID="uiVenueGetter" Width="380px"/></p>
		<p><b>What is the name of the event?</b><br /><js:HtmlAutoComplete runat="server" ID="uiEventName" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetEvents" Width="380px" MaxNumberOfItemsToGet="5" /></p>
		<div id="uiAddOptionsPanel" runat="server" style="display:none">
			<p><b>Enter a summary of the event for our listings (Optional)</b><asp:TextBox ID="uiSummary" runat="server" Width="380px" /></p>
			<p><b>Which brand is the event associated with? (Optional)</b><br /><js:HtmlAutoComplete ID="uiBrand" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetBrands" Width="380px" MaxNumberOfItemsToGet="5"/></p>
			<p><asp:Button ID="uiAdd" runat="server" Text="Add new event"/></p>
		</div>
	</div>
</div>
