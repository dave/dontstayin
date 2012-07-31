<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InviteFriends.ascx.cs" Inherits="Spotted.Blank.InviteFriends" %>
<%@ Register TagPrefix="DsiControls" TagName="BuddyImporter" Src="/Controls/BuddyImporter.ascx" %>
<%= Spotted.Main15Script.Register %>

<style>
	* {font-family:Verdana,Arial,Helvetica,sans-serif;} 
	img.WelcomeHeaderTop
	{
		position:relative;
		left:36px;
	}
	div.WelcomeBox{
		position:relative;
		left:36px;
		padding:3px 13px 3px 13px;
		border-left:3px solid #000000;
		border-right:3px solid #000000;
		border-bottom:3px solid #000000;
		background-color:FECA26;
		margin-bottom:10px;	
		text-align:left;
	}
	.ValidationSummary{
		position:relative;
		left:35px;
		padding:9px 13px 13px 13px;
		border:3px solid #000000;
		background-color:FECA26;
		margin-bottom:10px;	
		text-align:left;
	}
	div{
		font-size:10px;
	}
	td, th{
		font-size:10px;
	}
	td.Large{
		font-size:10px;
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
	p
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
		font-size:10px;
	}
	a:link, 
	a:visited         { color:#000000; }
	a:hover           { color:#FF0000; }

	small a:link, 
	small a:visited   { color:#A58319; }
	small a:hover     { color:#FF0000; }	
	
</style>
<center>
	<br />
	<div style="width:500px;">
		<img src="/gfx/welcome-top-2.jpg" class="WelcomeHeaderTop"><div class="WelcomeBox">
			<p style="font-size:12px;">
				One last thing! We can find all your contacts from your email account, and then you can choose which of your friends to invite to DontStayIn.
			</p>
			<p align="center">
				<button class="LargeButton" onclick="document.getElementById('<%= uiBuddyImporterDiv.ClientID %>').style.visibility='visible';return false;">Find my friends!</button> 
				&nbsp;or&nbsp;&nbsp;
				<button class="LargeButton" runat="server" id="uiSkipButton" causesvalidation="false" onserverclick="Skip">continue to Don't Stay In</button>
				<button class="LargeButton" runat="server" id="uiGoToSiteButton" causesvalidation="false" visible="false" onserverclick="GoToSite">continue to Don't Stay In</button>
			</p>
		</div>
	</div>
	<div id="uiBuddyImporterDiv" runat="server" style="width:600px;border-top:3px solid #000000;padding-top:10px;padding-bottom:10px;" class="WelcomeBox">
		<DsiControls:BuddyImporter runat="server" ID="uiBuddyImporter"></DsiControls:BuddyImporter>
		<asp:Panel runat="server" ID="uiFinishedPanel" Visible="false">
			<a href="/"><b>Go to DontStayIn!</b></a>
		</asp:Panel>
	</div>
</center>
<DIV id="TipLayer" style="visibility:hidden;position:absolute;z-index:1000;top:-100px"></DIV>
