<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Questionairre.ascx.cs" Inherits="Spotted.Blank.Questionairre" %>
<html>
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
	<asp:Panel Runat="server" ID="DoneQuestionairrePanel">
		<div style="width:500px;">
			<img src="/gfx/welcome-top-2.jpg" class="WelcomeHeaderTop"><div class="WelcomeBox">
				<p style="font-size:12px;">
					You've already completed the questionnaire - no need to do it again!
				</p>
				<p style="font-size:12px;" align="center">
					<a href="/">Back to DontStayIn</a>
				</p>
			</div>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="QuestionairrePanel">
		<div style="width:500px;">
			<img src="/gfx/welcome-top-2.jpg" class="WelcomeHeaderTop"><div class="WelcomeBox">
				<p style="font-size:12px;">
					We all hate questionnaires - but the more information we can get from 
					our visitors, the better we can make DontStayIn. It's confidential - 
					we'll never reveal your answers to anyone*
				</p>
				<p style="font-size:12px;">
					<b>To say thanks, we'll give you a special icon on your profile page:</b>
				</p>
				<p align="center" style="font-size:12px; margin:20px;">
					<img src="/gfx/icon-thumbs-up.png" width="26" height="21" align="absmiddle" style="margin-right:3px;"><b>Thanks!</b>
				</p>
				<p style="font-size:12px;">
					If you don't want to complete it, just click the "Skip" button. You can come back and fill it in later to get your icon!
				</p>
				<p align="center">
					<button class="LargeButton" ID="Button2" onclick="document.getElementById('<%= QuestionairreDiv.ClientID %>').style.visibility='visible';return false;">I want my icon!</button> 
					&nbsp;or&nbsp;&nbsp;
					<button class="LargeButton" runat="server" ID="Button1" causesvalidation="false" onserverclick="Skip">Skip this rubbish!</button>
				</p>
				<p style="font-size:12px;">
					* Our evil corporate sponsors just get general statistics
					- stuff like "75% of our members drink alcopops". We also use this info to 
					make sure you don't see ads that aren't relevant.
				</p>
				<p>You can read <a href="/popup/legalinformationpolicy" target="_blank">our privacy policy here</a>.</p>
			</div>
		</div>
		<asp:ValidationSummary 
			Runat="server" 
			EnableClientScript="False" 
			ShowSummary="True" 
			HeaderText="There were some questions you missed:" 
			CssClass="ValidationSummary"
			
			Width="300" 
			Font-Bold="True" 
			DisplayMode="BulletList" 
			ID="Validationsummary2" 
			NAME="Validationsummary1"/>
		
			
		<div id="QuestionairreDiv" class="WelcomeBox" style="width:600px;border-top:3px solid #000000;padding-top:10px;padding-bottom:10px;visibility:hidden;" runat="server">
			<style>
			.ValidationSummary{
					border:2px solid #ff0000;
					margin:5px;
					padding:5px;
					font-size:12px;			
					text-align:left;
					
				}
			</style>

			
			<style>
				h2{
				margin-top:20px;
				font-size:16px;
				}
			</style>
			<h2>When you go out, what do you drink?</h2>
			<p>
				<table cellpadding="0" cellspacing="0">
					<tr>
						<td width="120"><asp:CheckBox Runat="server" ID="DrinkWater" Text="Water"></asp:CheckBox></td>
						<td width="120"><asp:CheckBox Runat="server" ID="DrinkSoft" Text="Soft drinks"></asp:CheckBox></td>
						<td width="120"><asp:CheckBox Runat="server" ID="DrinkEnergy" Text="Energy drinks"></asp:CheckBox></td>
					</tr>
					<tr>
						<td width="120"><asp:CheckBox Runat="server" ID="DrinkDraftBeer" Text="Beer in a glass"></asp:CheckBox></td>
						<td width="120"><asp:CheckBox Runat="server" ID="DrinkBottledBeer" Text="Bottled beer"></asp:CheckBox></td>
						<td width="120"><asp:CheckBox Runat="server" ID="DrinkCider" Text="Cider"></asp:CheckBox></td>
					</tr>
					<tr>
						<td width="120"><asp:CheckBox Runat="server" ID="DrinkSpirits" Text="Spirits"></asp:CheckBox></td>
						<td width="120"><asp:CheckBox Runat="server" ID="DrinkWine" Text="Wine"></asp:CheckBox></td>
						<td width="120"><asp:CheckBox Runat="server" ID="DrinkAlcopops" Text="Alcopops"></asp:CheckBox></td>
					</tr>
				</table>
			</p>
			
			<h2>Do you smoke?</h2>
			<p>
				<asp:RadioButton Runat="server" GroupName="Smoke" ID="Smoke1" Text="Yes"/>
				<asp:RadioButton Runat="server" GroupName="Smoke" ID="Smoke2" Text="No"/>
				<asp:RadioButton Runat="server" GroupName="Smoke" ID="Smoke3" Text="Only when I go out"/>
			</p>
			<asp:CustomValidator Runat="server" Display="None" OnServerValidate="SmokeVal" ErrorMessage="Do you smoke?" ID="Customvalidator1"></asp:CustomValidator>
			
			<h2>How do you spend your evenings?</h2>
			<p>
				<table cellpadding="0" cellspacing="0">
					<tr>
						<td>All night clubbing</td>
						<td style="padding:2px;">
							<asp:DropDownList Runat="server" ID="EveningAllNight" onchange="FixEvenings(1);">
								<asp:ListItem Value="0">Never</asp:ListItem>
								<asp:ListItem Value="0.5">Less than once a week</asp:ListItem>
								<asp:ListItem Value="1">Once a week</asp:ListItem>
								<asp:ListItem Value="2">Twice a week</asp:ListItem>
								<asp:ListItem Value="3">3 times per week</asp:ListItem>
								<asp:ListItem Value="4">4 times per week</asp:ListItem>
								<asp:ListItem Value="5">5 times per week</asp:ListItem>
								<asp:ListItem Value="6">6 times per week</asp:ListItem>
								<asp:ListItem Value="7">Every night</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td>
							Late night at a pub/club (in bed by 3am)
						</td>
						<td style="padding:2px;">
							<asp:DropDownList Runat="server" ID="EveningLateNight" onchange="FixEvenings(2);">
								<asp:ListItem Value="0">Never</asp:ListItem>
								<asp:ListItem Value="0.5">Less than once a week</asp:ListItem>
								<asp:ListItem Value="1">Once a week</asp:ListItem>
								<asp:ListItem Value="2">Twice a week</asp:ListItem>
								<asp:ListItem Value="3">3 times per week</asp:ListItem>
								<asp:ListItem Value="4">4 times per week</asp:ListItem>
								<asp:ListItem Value="5">5 times per week</asp:ListItem>
								<asp:ListItem Value="6">6 times per week</asp:ListItem>
								<asp:ListItem Value="7">Every night</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td>Couple of drinks in a bar (in bed by midnight)</td>
						<td style="padding:2px;">
							<asp:DropDownList Runat="server" ID="EveningCoupleDrinks" onchange="FixEvenings(3);">
								<asp:ListItem Value="0">Never</asp:ListItem>
								<asp:ListItem Value="0.5">Less than once a week</asp:ListItem>
								<asp:ListItem Value="1">Once a week</asp:ListItem>
								<asp:ListItem Value="2">Twice a week</asp:ListItem>
								<asp:ListItem Value="3">3 times per week</asp:ListItem>
								<asp:ListItem Value="4">4 times per week</asp:ListItem>
								<asp:ListItem Value="5">5 times per week</asp:ListItem>
								<asp:ListItem Value="6">6 times per week</asp:ListItem>
								<asp:ListItem Value="7">Every night</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td>Other social event (e.g. cinema, restaurant etc.)</td>
						<td style="padding:2px;">
							<asp:DropDownList Runat="server" ID="EveningOther" onchange="FixEvenings(4);">
								<asp:ListItem Value="0">Never</asp:ListItem>
								<asp:ListItem Value="0.5">Less than once a week</asp:ListItem>
								<asp:ListItem Value="1">Once a week</asp:ListItem>
								<asp:ListItem Value="2">Twice a week</asp:ListItem>
								<asp:ListItem Value="3">3 times per week</asp:ListItem>
								<asp:ListItem Value="4">4 times per week</asp:ListItem>
								<asp:ListItem Value="5">5 times per week</asp:ListItem>
								<asp:ListItem Value="6">6 times per week</asp:ListItem>
								<asp:ListItem Value="7">Every night</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td>Stay in / work</td>
						<td style="padding:2px;">
							<asp:DropDownList Runat="server" ID="EveningStayIn" onchange="FixEvenings(5);">
								<asp:ListItem Value="0">Never</asp:ListItem>
								<asp:ListItem Value="0.5">Less than once a week</asp:ListItem>
								<asp:ListItem Value="1">Once a week</asp:ListItem>
								<asp:ListItem Value="2">Twice a week</asp:ListItem>
								<asp:ListItem Value="3">3 times per week</asp:ListItem>
								<asp:ListItem Value="4">4 times per week</asp:ListItem>
								<asp:ListItem Value="5">5 times per week</asp:ListItem>
								<asp:ListItem Value="6">6 times per week</asp:ListItem>
								<asp:ListItem Value="7" Selected="True">Every night</asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
				</table>
				<script>
					function FixEvenings(Skip)
					{
						var AllNight = document.getElementById("<%= EveningAllNight.ClientID %>");
						var LateNight = document.getElementById("<%= EveningLateNight.ClientID %>");
						var CoupleDrinks = document.getElementById("<%= EveningCoupleDrinks.ClientID %>");
						var Other = document.getElementById("<%= EveningOther.ClientID %>");
						var StayIn = document.getElementById("<%= EveningStayIn.ClientID %>");
						
						var total = GetEvening(AllNight) + GetEvening(LateNight) + GetEvening(CoupleDrinks) + GetEvening(Other) + GetEvening(StayIn) - 7;
						if (total>0)
						{
							if (Skip!=5) total = total - FixEvening(StayIn, total);
							if (Skip!=4) total = total - FixEvening(Other, total);
							if (Skip!=3) total = total - FixEvening(CoupleDrinks, total);
							if (Skip!=2) total = total - FixEvening(LateNight, total);
							if (Skip!=1) total = total - FixEvening(AllNight, total);
						}
						else
						{
							SetEvening(StayIn, GetEvening(StayIn) - total);
						}
					}
					function GetEvening(Obj)
					{
						var num = parseFloat(Obj[Obj.selectedIndex].value);
						if (num<1)
							return 0;
						else
							return num;
					}
					function SetEvening(Obj, Num)
					{
						if (Num==0)
						{
							if (Obj.selectedIndex>0)
								Obj.selectedIndex = Num+1;
						}
						else
							Obj.selectedIndex = Num+1;
					}
					function FixEvening(Obj, Total)
					{
						var Remove = Total;
						var CanRemove = GetEvening(Obj);
						if (Total>CanRemove)
						{
							Remove = CanRemove;
						}
						SetEvening(Obj,CanRemove-Remove);
						return Remove;
					}
				</script>
			</p>
			<asp:CustomValidator Runat="server" Display="None" OnServerValidate="EveningsVal" ErrorMessage="How do you spend your evenings?" ID="Customvalidator2" NAME="Customvalidator1"></asp:CustomValidator>
			
			<h2>What's your employment status?</h2>
			<p>
				<asp:RadioButton Runat="server" GroupName="Employment" ID="Employment1" Text="Full-time"/>
				<asp:RadioButton Runat="server" GroupName="Employment" ID="Employment2" Text="Part-time"/>
				<asp:RadioButton Runat="server" GroupName="Employment" ID="Employment3" Text="Currently unemployed"/>
				<asp:RadioButton Runat="server" GroupName="Employment" ID="Employment4" Text="Student"/>
			</p>
			<asp:CustomValidator Runat="server" Display="None" OnServerValidate="EmployVal" ErrorMessage="What's your employment status?" ID="Customvalidator3" NAME="Customvalidator1"></asp:CustomValidator>
			
			<h2>How much do you earn per year?</h2>
			<p>
