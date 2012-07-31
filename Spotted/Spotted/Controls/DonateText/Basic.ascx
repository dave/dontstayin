<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Basic.ascx.cs" Inherits="Spotted.Controls.DonateText.Basic" %>

<dsi:h1 runat="server">Get a <%=DonationIcon.IconName%>!</dsi:h1>
	<div class="ContentBorder">
		<p class="BigCenter">
			Get your very own <%=DonationIcon.IconName%> on your profile!
		</p>
		<center>
			<p><img src="<%= DonationIcon.IconPath %>" border="0" align="absMiddle" style="margin-right:3px;margin-left:3px"><b><%= DonationIcon.IconText%></b></p>
			<p>Your icon will stay on your profile!</p>
		</center>
		<p>
			Thanks,
		</p>
		<p>
			- The DSI team.
		</p>
