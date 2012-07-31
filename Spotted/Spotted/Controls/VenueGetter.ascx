<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VenueGetter.ascx.cs" Inherits="Spotted.Controls.VenueGetter" %>
<%@ Register Src="~/Controls/VenueCreator.ascx" TagPrefix="uc" TagName="VenueCreator"%>
<asp:Panel runat="server" id="uiOuterPanel" Width="380px" Height="40px" BackColor="White" BorderColor="#999999" BorderWidth="1px" BorderStyle="Solid">
	<js:HtmlAutoComplete ID="uiAuto" runat="server" Border="solid 0px black" WebServiceMethod="GetVenues" WebServiceUrl="/WebServices/AutoComplete.asmx" Width="100%" MaxNumberOfItemsToGet="5" Watermark="e.g. The Venue, Edinburgh, United Kingdom" />
	<asp:Panel runat="server" id="uiSelectedItemPanel" style="display:none;width:100%" />
</asp:Panel>
