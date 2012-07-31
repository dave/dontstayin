<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventsWithNoSpendPromoters.ascx.cs" Inherits="Spotted.Admin.EventsWithNoSpendPromoters" %>
<h1>Events</h1>
<div class="ContentBorder">
	<p>
		Upcoming events that have promoter accounts, but haven't spent money:
	</p>
	<asp:PlaceHolder runat="server" ID="Ph"></asp:PlaceHolder>
</div>
