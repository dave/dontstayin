<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StyledPage.aspx.cs" Inherits="Spotted.Master.StyledPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head runat="server">
		<title runat="server" id="PageTitleTag"></title>
		<%= Bobs.Storage.PathJavascriptFunction() %>
		<link rel="stylesheet" href="/Misc/thickbox.css" type="text/css" media="screen" />
		<link rel="stylesheet" type="text/css" runat="server" id="StyleTag" />
		<link id="CustomStyleSheet" href='' rel="stylesheet" type="text/css" />
	</head>
	<body>
		<form id="form1" runat="server">
			<div class="OuterDiv">
				<a href='<%= StyledObject.UrlStyled() %>'><div class="MainImage"></div></a>
				<div class="WelcomeDiv">Welcome <asp:Label ID="UserName" runat="server"></asp:Label> (<asp:LinkButton ID="LogOnOffLinkButton" runat="server" CssClass="WelcomeDivLink" Text="log off" OnClick="LogOnOffLinkButton_Click"></asp:LinkButton>)</div>
				<!-- google_ad_section_start -->
				<asp:PlaceHolder Runat="server" ID="MainContentPlaceHolder"/>
				<!-- google_ad_section_end -->
				<div class="FooterDiv">
					<hr>
					<div class="FooterLinks"><a href='<%= StyledObject.UrlStyled() %>' class="Link">home</a>&nbsp;|&nbsp;<a href='<%= StyledObject.UrlStyledCalendar(Common.Time.Now.Year, Common.Time.Now.Month) %>' class="Link">calendar of events</a>&nbsp;|&nbsp;<a href='<%= StyledObject.UrlStyledApp("mytickets") %>' class="Link">my tickets</a></div>
					<div class="PoweredByDSI"><a href="http://www.dontstayin.com/">Powered by <img src="/gfx/dsi-2-126.gif" border="0" width="62" height="26" align="absmiddle"/></a></div>
					<div style="font-size:8pt; text-align:center; margin-top:4px;"><a href='<%= this.StyledObject.UrlStyledApp("legal")%>' class="Link">Terms and conditions</a> | All content Copyright © Development Hell Limited 2003 - <%= DateTime.Now.Year.ToString() %> | All rights reserved</div>
					
				</div>
			</div>
		</form>
		<asp:Panel Runat="server" ID="AnchorSkipJs" EnableViewState="False" Visible="False">
			<script>document.location="#<asp:PlaceHolder Runat="server" ID="AnchorSkipName" />";</script>
		</asp:Panel>
	</body>
</html>
