<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlacesChooser.ascx.cs" Inherits="Spotted.Controls.PlacesChooser" %>
<%@ Register TagPrefix="uc1" TagName="MapControl" Src="~/Controls/MapControl.ascx" %>
<p>
	<js:MultiSelector runat="server" ID="uiPlacesMultiSelector" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPlacesEnabled" Watermark="To add a place, start typing it's name" Height="150px"/>
</p>
<p>
	Click on a marker on the map below to add it to the box above. We only show the most populated places on this map, so try zooming in if you can't find the place you are looking for. You can zoom using the control, your mouse wheel or a double click. If you can't see a place on the map, remember to try typing its name in the box above, as we don't have map coordinates for everywhere yet.
</p>
<p>
	<uc1:MapControl id="uiMap" runat="server" style="width:580pc;height:300px;border solid 1px black" />
</p>
<asp:Panel ID="uiPlacesRadius" runat="server" DefaultButton="uiAddRadiusButton">
<p style="text-align:center">
	Add nearest 
	<asp:DropDownList ID="uiNumberOfSurroundingTownsDropDown" runat="server">
		<asp:ListItem Text="3" Value="3"></asp:ListItem>
		<asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
		<asp:ListItem Text="10" Value="10"></asp:ListItem>
		<asp:ListItem Text="15" Value="15"></asp:ListItem>
		<asp:ListItem Text="20" Value="20"></asp:ListItem>
		<asp:ListItem Text="30" Value="30"></asp:ListItem>
		<asp:ListItem Text="50" Value="50"></asp:ListItem>
		<asp:ListItem Text="100" Value="100"></asp:ListItem>
	</asp:DropDownList>
	&nbsp;places to&nbsp; 
	<js:HtmlAutoComplete ID="uiRadiusPlaceAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPlacesEnabled" Width="300px"/>
	&nbsp;
	<asp:Button runat="server" ID="uiAddRadiusButton" Text="Add" />
</p>
</asp:Panel>
