<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerImpressionStats.ascx.cs" Inherits="Spotted.Admin.BannerImpressionStats" %>

<p>
	Start date:
	<dsi:Cal runat="server" id="uiFirstDate"></dsi:Cal>
</p>
<p>
	End date:
	<dsi:Cal runat="server" id="uiSecondDate"></dsi:Cal>
</p>
<p>
	<asp:Button ID="uiChangeDateRange" runat="server" Text="Change date range"></asp:Button>
</p>

<asp:GridView runat="server" ID="GridView" AutoGenerateColumns="false" OnRowDataBound="GridView_RowDataBound">
	<Columns>
		<asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:ddd dd MMM yy}" HtmlEncode="false" />
		<asp:BoundField HeaderText="Position" DataField="Position" />
		<asp:BoundField HeaderText="Expected Page Hits" DataField="ExpectedPageHits" DataFormatString="{0:N0}" HtmlEncode="false" />
		<asp:BoundField HeaderText="Actual Page Hits" DataField="ActualPageHits" DataFormatString="{0:N0}" HtmlEncode="false" />
		<asp:BoundField HeaderText="Required Impressions" DataField="RequiredImpressions" DataFormatString="{0:N0}" HtmlEncode="false" />
		<asp:BoundField HeaderText="Actual Impressions" DataField="ActualImpressions" DataFormatString="{0:N0}" HtmlEncode="false" />
	</Columns>
</asp:GridView>

