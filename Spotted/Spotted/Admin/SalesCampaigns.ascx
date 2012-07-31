<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesCampaigns.ascx.cs" Inherits="Spotted.Admin.SalesCampaigns" %>
<div class="ContentBorder">
	<p>
		Campaigns:
	</p>
	<p>
		<asp:DataGrid runat="server" ID="CampaignsDataGrid" AutoGenerateColumns="false" CellPadding="5">
			<Columns>
				<asp:BoundColumn DataField="K"></asp:BoundColumn>
				<asp:BoundColumn DataField="Name"></asp:BoundColumn>
				<asp:BoundColumn DataField="DateStart" DataFormatString="{0:d}"></asp:BoundColumn>
				<asp:BoundColumn DataField="NumberOfPromoters" HeaderText="New leads"></asp:BoundColumn>
				<asp:BoundColumn DataField="TotalRevenue" HeaderText="Revenue" DataFormatString="{0:c}"></asp:BoundColumn>
				<asp:EditCommandColumn></asp:EditCommandColumn>
			</Columns>
		</asp:DataGrid>
	</p>
	<p>
		Add campaign
	</p>
	<p>
		Name:<br />
		<asp:TextBox runat="server" ID="AddName" Columns="50"></asp:TextBox>
	</p>
	<p>
		Description:<br />
		<asp:TextBox runat="server" ID="AddDescription" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
	</p>
	<p>
		Start date (approx):<br />
		<dsi:Cal runat="server" ID="AddStartDate" />
	</p>
	<p>
		End date (approx):<br />
		<dsi:Cal runat="server" ID="AddEndDate" />
	</p>
	<p>
		<asp:Button runat="server" Text="Add" OnClick="Add" />
	</p>
</div>
