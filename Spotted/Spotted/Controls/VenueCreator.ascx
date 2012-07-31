<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VenueCreator.ascx.cs" Inherits="Spotted.Controls.VenueCreator" %>
<div id="uiContainer" runat="server" style="display:none;position:absolute;left:400px;top:200px;z-index:920;width:400px;">
	<dsi:h1 ID="H1" runat="server">Add a venue</dsi:h1>
	<div class="ContentBorder">
		<p>
			<ul style="margin: 0;padding: 0;list-style-type: none;">
				<li>Fill out the form below to add a venue. Remember this is for public venues, not for your house!</li>
				<li><b>Which town is the venue in?</b><br /><js:HtmlAutoComplete runat="server" ID="uiPlace" Width="380px" WebServiceMethod="GetPlaces" WebServiceUrl="/WebServices/AutoComplete.asmx" MaxNumberOfItemsToGet="5"/></li>
				<li><b>Whats the venue called?</b><br /><js:HtmlAutoComplete runat="server" ID="uiNameSuggest" WebServiceMethod="GetVenues" WebServiceUrl="/WebServices/AutoComplete.asmx" MaxNumberOfItemsToGet="5" Width="380px"/></li>
				<li><b>What's the postcode? You can leave this out if you don't know it.</b><br /><asp:TextBox ID="uiPostCode" runat="server"></asp:TextBox></li>
			</ul>
		</p>
	</div>
</div>
