<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesCalls.ascx.cs" Inherits="Spotted.Admin.SalesCalls" %>
<%@ Register TagPrefix="Controls" TagName="Cal" Src="/Controls/Cal.ascx" %>

<h1>Sales calls</h1>
<div class="ContentBorder">
	<p>
		Sales user: <asp:PlaceHolder runat="server" ID="UsersPh" />
	</p>
</div>

<Controls:Cal Runat="server" ID="Cal"/>

<h1>Sales calls for <%= CurrentUsr != null ? CurrentUsr.NickName : "" %></h1>

<div class="ContentBorder" style="padding:0px;">
	<asp:PlaceHolder runat="server" ID="SalesCallsPh" />
</div>
