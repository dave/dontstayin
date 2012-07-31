<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Invoices.ascx.cs" Inherits="Spotted.Pages.Promoters.Invoices" %>
<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>
<%@ Register TagPrefix="ControlsSetup" TagName="SetupPayment" Src="/Controls/SetupPayment.ascx" %>

<!--
<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Invoices">
		<p>
		Below are listed your invoices and credits.<br />To view a specific date range, please select the month and year and click "View Summary".
	</p>
</dsi:PromoterIntro>
<dsi:h1 runat="server" ID="H11">Your invoices and credits</dsi:h1>
-->
<script>
function InvoicesTogglePlusMinus(subRowIDs, id)
{
	var elem;
	var img;
	
	var arry = subRowIDs.split(','); 
	img = document.getElementById('showHideImage' + id);

	for(var i=0; i<arry.length; i++)
	{
		elem = document.getElementById(arry[i]);
		if(elem)
		{			
			elem.style.display = elem.style.display == 'none' ? '' : 'none';
			img.src = elem.style.display == 'none' ? "/gfx/plus.gif" : "/gfx/minus.gif";
		}
	}
}
</script>
<dsi:PromoterIntro runat="server" ID="PromoterIntro1" Header="Account">
    <p>Below are listed your account details:</p>
</dsi:PromoterIntro>
	
<asp:Panel ID="MainPanel" runat="server">
	<dsi:h1 runat="server" ID="H1Header">Your account details</dsi:h1>
	<div class="ContentBorder">    
		<table cellpadding="3">
			<tr>
				<td style="width: 80px"><asp:Label ID="BalanceLabel" runat="server" Text="Current&nbsp;balance"></asp:Label>&nbsp;</td>
