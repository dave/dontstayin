<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Guestlist.ascx.cs" Inherits="Spotted.Blank.Guestlist" %>

<p align="center">
	<img src="/gfx/dsi-1-126.gif" border="0" align="bottom">
</p>
<p style="font-family:Verdana,Arial,Helvetica,sans-serif;font-weight:bold;" align="center">
	DontStayIn guestlist for 
	<asp:Label Runat="server" ID="EventLabel"></asp:Label>
</p>
<p style="font-family:Verdana,Arial,Helvetica,sans-serif;font-weight:bold;" align="center">
	Call our promoter hotline on 0207 835 5599 for more info about the DontStayIn promoter system.
</p>

<p style="font-family:Verdana,Arial,Helvetica,sans-serif;font-weight:bold;" align="center">
	<asp:Label Runat="server" ID="PriceLabel"></asp:Label>
</p>

<p>
	<asp:DataList Runat="server" RepeatColumns="1" ID="GuestlistDataList" EnableViewState="False"></asp:DataList>
</p>
