<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncomePaymentDate.ascx.cs" Inherits="Spotted.Admin.IncomePaymentDate" %>
<h2>
	When was the revenue PAID? (<%= BuyerType.ToString() %>)
</h2>
<p>
	<a href="/admin/incomepaymentdate/BuyerType-1">Agency</a> | 
	<a href="/admin/incomepaymentdate/BuyerType-2">Promoter</a> |
	<a href="/admin/incomepaymentdate/BuyerType-3">Tickets</a> |
	<a href="/admin/incomepaymentdate/BuyerType-4">Users</a>
</p>
<p>
	<asp:PlaceHolder runat="server" ID="Stats" />
</p>
