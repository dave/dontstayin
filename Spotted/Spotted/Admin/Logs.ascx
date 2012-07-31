<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Logs.ascx.cs" Inherits="Spotted.Admin.Logs" %>
<h1>Spotters</h1>
<div class="ContentBorder">
	<p>
		Logs:
	</p>
	<p>
		<asp:Calendar Runat="server" ID="Cal" OnSelectionChanged="ReBind"></asp:Calendar>
	</p>
	<p>
		<asp:DataGrid Runat="server" ID="Times" AutoGenerateColumns="False">
			<Columns>
				<asp:BoundColumn HeaderText="Log" DataField="ItemType" />
				<asp:BoundColumn HeaderText="Count" DataField="Count" DataFormatString="{0:#,###}" ItemStyle-HorizontalAlign="Right" />
			</Columns>
		</asp:DataGrid>
	</p>
</div>
