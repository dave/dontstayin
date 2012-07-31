<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArchiveContent.ascx.cs" Inherits="Spotted.Pages.ArchiveContent" %>
<!--%@ OutputCache Duration="1200" VaryByCustom="Country;PageName" VaryByParam="None" %-->
<%@ Register TagPrefix="Controls" Namespace="Spotted.Controls" Assembly="Spotted" %>
<dsi:h1 runat="server" ID="Header">Archive</dsi:h1>
<div class="ContentBorder" style="padding-right:0px;padding-left:0px;padding-bottom:0px;">	
	<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;margin-bottom:0px;">
		<img src="/gfx/icon-calendar.png" border="0" width="26" height="21">
	</p>
	
	<p align="center">
		This is the <span Runat="server" ID="TitleSpan"></span>
	</p>
	
	<p runat="server" id="ItemsHiddenP" align="center">
		<b>
			There are too many things this month to show them all on the calendar.<br>
			Click on a day, and we'll display the items for that day at the top of the page.
		</b>
	</p>

	<p runat="server" id="DayItemsP" align="center" class="ClearAfter">
		<asp:Repeater Runat="server" ID="DayRepeater">
			<ItemTemplate>
				<%#((Bobs.IArchive)(Container.DataItem)).ArchiveHtml(ShowCountry,ShowPlace,ShowVenue,ShowEvent,84)%>
			</ItemTemplate>
		</asp:Repeater>
	</p>
	
	<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
		<a href="" runat="server" id="BackLink"></a> - 
		<asp:Label Runat="server" ID="MonthNameLabel"></asp:Label> - 
		<a href="" runat="server" id="NextLink"></a>
	</p>

	<Controls:Archive runat="server" ID="Arch"/>
	
	<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
		<a href="" runat="server" id="BackLink1"></a> - 
		<asp:Label Runat="server" ID="MonthNameLabel1"></asp:Label> - 
		<a href="" runat="server" id="NextLink1"></a>
	</p>

</div>
