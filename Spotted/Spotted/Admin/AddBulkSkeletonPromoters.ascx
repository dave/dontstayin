<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBulkSkeletonPromoters.ascx.cs" Inherits="Spotted.Admin.AddBulkSkeletonPromoters" %>
<div class="ContentBorder">
	<p>
		Paste in a list of new promoters. Format should be:
	</p>
	<p>
		<b>
			Name, Number[, Notes]
		</b>
	</p>
	<p>
		(notes is optional)
	</p>
	<p>
		They will be added to your NEW list.
	</p>
	<p>
		Sector: <asp:DropDownList runat="server" ID="Sector" />
	</p>
	<p>
		Campaign: <asp:DropDownList runat="server" ID="SalesCampaignDropDown" />
	</p>
	<p>
		Promoters:<br />
		<asp:TextBox runat="server" ID="Csv" TextMode="MultiLine" Columns="80" Rows="20"></asp:TextBox>
	</p>
	<p>
		<asp:Button runat="server" Text="Add now" OnClick="Add" />
	</p>
	<p runat="server" id="Error"></p>
</div>
