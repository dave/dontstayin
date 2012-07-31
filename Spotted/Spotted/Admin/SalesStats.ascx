<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesStats.ascx.cs" Inherits="Spotted.Admin.SalesStats" %>
<div class="ContentBorder">
	<h2>Calls:</h2>
	<p>
		<asp:DataGrid runat="server" ID="CallsDataGrid" GridLines="None"
			BorderWidth=0 CellPadding=3 CssClass=dataGrid 
			AlternatingItemStyle-CssClass="dataGridAltItem"
			HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
			ItemStyle-VerticalAlign="Top"></asp:DataGrid>
	</p>
	<h2>Sales by day:</h2>
	<p>
		<asp:DataGrid runat="server" ID="DailySalesDataGrid" GridLines="None"
			BorderWidth=0 CellPadding=3 CssClass=dataGrid 
			AlternatingItemStyle-CssClass="dataGridAltItem"
			HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
			ItemStyle-VerticalAlign="Top"></asp:DataGrid>
	</p>
	<h2>Sales by month:</h2>
	<p>
		<asp:DataGrid runat="server" ID="MonthlySalesDataGrid" GridLines="None"
			BorderWidth=0 CellPadding=3 CssClass=dataGrid 
			AlternatingItemStyle-CssClass="dataGridAltItem"
			HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
			ItemStyle-VerticalAlign="Top"></asp:DataGrid>
	</p>
</div>
