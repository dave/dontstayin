<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncomeEarningDate.ascx.cs" Inherits="Spotted.Admin.IncomeEarningDate" %>
<h2>
	When was the revenue EARNED? (<%= BuyerType.ToString() %>)
</h2>
<p>
	<a href="/admin/incomeearningdate/BuyerType-1">Agency</a> | 
	<a href="/admin/incomeearningdate/BuyerType-2">Promoter</a> |
	<a href="/admin/incomeearningdate/BuyerType-3">Tickets</a> |
	<a href="/admin/incomeearningdate/BuyerType-4">Users</a>
</p>
<p>
	<asp:PlaceHolder runat="server" ID="Stats" />
</p>
