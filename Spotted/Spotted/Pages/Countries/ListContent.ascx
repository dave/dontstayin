<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListContent.ascx.cs" Inherits="Spotted.Pages.Countries.ListContent" %>
<!--%@ OutputCache Duration="3600" VaryByParam="None" %-->

<dsi:h1 runat="server" ID="H12" NAME="H11">Countries</dsi:h1>
<div class="ContentBorder">
	<p>Countries with events:</p>
	<p class="CleanLinks">
		<asp:DataList Runat="server" ID="CountriesDataList" RepeatColumns="3" 
			RepeatDirection="Horizontal" RepeatLayout="Table" ItemStyle-Width="33%"></asp:DataList>
	</p>
	<p>All countries:</p>
	<p class="CleanLinks">
		<asp:DataList Runat="server" ID="OtherCountriesDataList" RepeatColumns="3" 
			RepeatDirection="Horizontal" RepeatLayout="Table" ItemStyle-Width="33%"></asp:DataList>
	</p>
</div>
