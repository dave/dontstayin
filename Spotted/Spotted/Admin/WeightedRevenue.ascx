<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeightedRevenue.ascx.cs" Inherits="Spotted.Admin.WeightedRevenue" %>
<h2>
	Weighted revenue stats (<%= BuyerType.ToString() %>)
</h2>
<p>
	<a href="/admin/weightedrevenue/BuyerType-1">Agency</a> | 
	<a href="/admin/weightedrevenue/BuyerType-2">Promoter</a> |
	<a href="/admin/weightedrevenue/BuyerType-3">Tickets</a> |
	<a href="/admin/weightedrevenue/BuyerType-4">Users</a>
</p>
<p>
	<asp:PlaceHolder runat="server" ID="Stats" />
</p>
<p>
	* Addvantage income only included for December 2006 and on.
</p>
