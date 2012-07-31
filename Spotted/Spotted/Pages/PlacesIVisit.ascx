<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlacesIVisit.ascx.cs" Inherits="Spotted.Pages.PlacesIVisit" %>
<%@ Import NameSpace="Bobs" %>
<%@ Register TagPrefix="dsi" TagName="PlacesChooser" Src="~/Controls/PlacesChooser.ascx" %>
<dsi:h1 runat="server" ID="H11">Places I visit</dsi:h1>
<div class="ContentBorder">
	<p>
	Tell us which places you visit. This customises the site to show you relevant information about the places you care about.
	</p>
	<dsi:PlacesChooser ID="uiPlacesChooser" runat="server" />
	<p>
	<asp:Button id="uiSaveButton" runat="server" Text="Save" Height="26px" Width="56px" />
	</p>
</div>
