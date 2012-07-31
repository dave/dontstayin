<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LegalTermsPromoterAgree.ascx.cs" Inherits="Spotted.Blank.LegalTermsPromoterAgree" %>
<%@ Register TagPrefix="Spotted" TagName="Terms" Src="/Controls/LegalTermsPromoter.ascx" %>
<html>
<style>
	* {font-family:Verdana,Arial,Helvetica,sans-serif;} 

	div.WelcomeBox{
		padding:3px 13px 3px 13px;
		border:3px solid #000000;
		background-color:FECA26;
		margin-bottom:10px;	
		text-align:left;
	}
	.ValidationSummary{
		padding:9px 13px 13px 13px;
		border:3px solid #000000;
		background-color:FECA26;
		margin-bottom:10px;	
		text-align:left;
	}
	div{
		font-size:12px;
	}
	td, th{
		font-size:10px;
	}
	td.Large{
		font-size:12px;
	}
	th{
		text-align:left;
		font-weight:normal;
	}

	button, input, select, textarea
	{
		font-size:10px;
	}
	button.LargeButton, input.LargeButton
	{
		font-size:12px;
	}
	p, li
	{
		margin-top:6px;
		margin-bottom:10px;
	}
	td p
	{
		margin-top:1px;
		margin-bottom:3px;
	}
	td div
	{
		font-size:10px;
	}
	small{
		color:#A58319;
	}
	a:link, 
	a:visited         { color:#000000; }
	a:hover           { color:#FF0000; }

	small a:link, 
	small a:visited   { color:#A58319; }
	small a:hover     { color:#FF0000; }	
	
</style>
<head>
	<title>Welcome to DontStayIn!</title>
</head>
<body>
<center>
	<div style="width:500px; padding-top:50px;">
		<div class="WelcomeBox">
			<p>
				<b>These terms and conditions are for PROMOTERS ONLY. They are different 
				to the terms and conditions for membership, so please read them carefully!</b>
			</p>
			<p>
				We've recently changed our terms and conditions for DontStayIn promoter 
				accounts. We've tried hard to make it easy to read and understand - 
				we've used as little legal jargon as possible! Have a read through - 
				you must agree to them before using the site.
			</p>
		</div>
		<asp:ValidationSummary Runat="server" 
			HeaderText="<b>You've made some mistakes:</b>" DisplayMode="SingleParagraph" ShowSummary="True" CssClass="ValidationSummary" ID="Validationsummary1"/>
		
		<div class="WelcomeBox" style="xheight:200px; xoverflow:auto;">
			<Spotted:Terms runat="server" UsePopups="true"></Spotted:Terms>
		</div>
		
		<div class="WelcomeBox">
			<p>
				<asp:CheckBox Runat="server" ID="TermsCheckbox" Text="I have read and agree to be bound by the terms for promoters"></asp:CheckBox>
			</p>
			<asp:CustomValidator Runat="server" Display="None" EnableClientScript="False" 
				OnServerValidate="TermsVal" ErrorMessage="<br>You must agree to the terms for promoters" ID="Customvalidator7"/>
			<p>
				<asp:Button id="PrefsUpdateButton" onclick="PrefsUpdateClick" Runat="server" 
					Text="Continue" CssClass="LargeButton"></asp:Button>
			</p>
		</div>
		
	</div>
</center>
<script>
function openPopup(url)
{
	var popUp = window.open(url, popUp, 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=400');
}
</script>
</body>
</html>
