<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="Spotted.Controls.DonateText.Default" %>

<dsi:h1 runat="server"><%=DonationIcon.DonatePageHeader%></dsi:h1>
	<div class="ContentBorder">
		<p class="BigCenter">
			<%= DonationIcon.DonatePageCenterText%>
		</p>
		<p>
			<%= DonationIcon.DonatePageLine1Text%>
		</p>
		<center>
			<p><img src="<%= DonationIcon.IconPath %>" width="26" height="21" border="0" align="absMiddle" style="margin-right:3px;margin-left:3px"><b><%= DonationIcon.IconText%></b></p>
			<p><b><%= DonationIcon.DonatePageLine2Text%></b></p>
		</center>
		<p>
			- The DSI team.
		</p>
