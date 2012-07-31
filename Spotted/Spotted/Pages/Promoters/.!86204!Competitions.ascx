<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Competitions.ascx.cs" Inherits="Spotted.Pages.Promoters.Competitions" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<%@ Register TagPrefix="Controls" TagName="Pic" Src="/Controls/Pic.ascx" %>

<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Competitions">
	<asp:Panel Runat="server" ID="InfoPanel">
		<h2>It's FREE to add a competition</h2>
		<p>
			Just click the button below and complete the form.
		</p>
		<p>
			When you've finished editing your competition, and added a picture, you must click the Publish 
			button. We will then check it and make it live on the site. If you don't click Publish, it won't
			be enabled!
		</p>
		<p>
			<a href="<%= CurrentPromoter.UrlApp("competitions","mode","add") %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
				align="absmiddle" style="margin-right:3px;">add a competition</a>
		</p>
	</asp:Panel>
</dsi:PromoterIntro>

<asp:Panel Runat="server" ID="PanelEdit">
	<dsi:h1 runat="server" ID="H14">Add a competition</dsi:h1>
	<div class="ContentBorder">
		<asp:ValidationSummary Runat="server" ShowSummary="True" HeaderText="You've made some mistakes" CssClass="ValidationSummaryDiv" Font-Bold="True" DisplayMode="BulletList" ID="Validationsummary1" NAME="Validationsummary1"/>
		<h2>Instructions</h2>
		<p>
			You just complete the details below. Enter a question, three answers, the prizes and we do the rest. 
			We'll draw your competition automatically at midday on the date you choose, and we'll invite you to 
			a private message with each of the winners.
		</p>
		<p>
