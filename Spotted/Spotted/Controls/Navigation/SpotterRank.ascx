<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpotterRank.ascx.cs" Inherits="Spotted.Controls.Navigation.SpotterRank" %>
<!--%@ OutputCache Duration="7200" VaryByParam="None" %-->
<dsi:h1 runat="server" id="H1" EnableViewState="False" Type="Nav">Top spotters</dsi:h1>
<div class="NavRightBorder" style="padding:0px;">
	<p style="margin:4px 6px 7px 6px;">
		DontStayIn just wouldn't work without our army of <a href="/pages/spotters">spotters</a>. 
		Our top spotters in the last month are:
	</p>
	<div style="width:158px; overflow:hidden;" class="CleanLinks">
		<asp:Repeater Runat="server" ID="SpotterRankRepeater"/>
	</div>
	<p style="margin:5px 6px 7px 6px;">
		<small>
			The number in brackets is how many people they've spotted in the last month.
		</small>
	</p>
</div>
