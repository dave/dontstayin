<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentDonators.ascx.cs" Inherits="Spotted.Controls.Navigation.RecentDonators" %>
<!--%@ OutputCache Duration="7200" VaryByParam="None" %-->
<dsi:h1 runat="server" id="H1" EnableViewState="False" Type="Nav">Recent donators</dsi:h1>
<div class="NavRightBorder" style="padding:0px;">
	<p style="margin:4px 6px 7px 6px;">
		Our users help us with the costs of running the site by <a href="/pages/icons">donating</a>. 
		Our top ten most recent donators are:
	</p>
	<div style="width:158px; overflow:hidden;" class="CleanLinks">
		<asp:Repeater Runat="server" ID="uiRepeater"/>
	</div>
</div>
