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
				<asp:RadioButton Runat="server" GroupName="Salary" ID="Salary1" Text="less than £15k"/>
				<asp:RadioButton Runat="server" GroupName="Salary" ID="Salary2" Text="£15 - £19"/>
				<asp:RadioButton Runat="server" GroupName="Salary" ID="Salary3" Text="£20 - £24"/>
				<asp:RadioButton Runat="server" GroupName="Salary" ID="Salary4" Text="£25 - £29"/>
				<asp:RadioButton Runat="server" GroupName="Salary" ID="Salary5" Text="£30 - £39"/>
				<asp:RadioButton Runat="server" GroupName="Salary" ID="Salary6" Text="£40 - £49"/>
				<asp:RadioButton Runat="server" GroupName="Salary" ID="Salary7" Text="£50k+"/>
			</p>
			<asp:CustomValidator Runat="server" Display="None" OnServerValidate="SalaryVal" ErrorMessage="How much do you earn per year?" ID="Customvalidator4" NAME="Customvalidator1"></asp:CustomValidator>
			
			
			<h2>Money</h2>
			<p>
				<table cellpadding="0" cellspacing="0">
					<tr>
						<td>
							Do you have a credit card?
						</td>
						<td>
							<asp:RadioButton Runat="server" GroupName="CreditCard" ID="CreditCardYes" Text="Yes"/>
							<asp:RadioButton Runat="server" GroupName="CreditCard" ID="CreditCardNo" Text="No"/>
						</td>
					</tr>
					<tr>
						<td>
							Do you have a personal loan?
						</td>
						<td>
							<asp:RadioButton Runat="server" GroupName="Loan" ID="LoanYes" Text="Yes"/>
							<asp:RadioButton Runat="server" GroupName="Loan" ID="LoanNo" Text="No"/>
						</td>
					</tr>
					<tr>
						<td>
							Do you have a mortgage?
						</td>
						<td>
							<asp:RadioButton Runat="server" GroupName="Mortgage" ID="MortgageYes" Text="Yes"/>
							<asp:RadioButton Runat="server" GroupName="Mortgage" ID="MortgageNo" Text="No"/>
						</td>
					</tr>
				</table>
			</p>
			<asp:CustomValidator Runat="server" Display="None" OnServerValidate="CreditCardVal" ErrorMessage="Do you have a credit card?" ID="Customvalidator5" NAME="Customvalidator1"></asp:CustomValidator>
			<asp:CustomValidator Runat="server" Display="None" OnServerValidate="LoanVal" ErrorMessage="Do you have a personal loan?" ID="Customvalidator6" NAME="Customvalidator1"></asp:CustomValidator>
			<asp:CustomValidator Runat="server" Display="None" OnServerValidate="MortgageVal" ErrorMessage="Do you have a mortgage?" ID="Customvalidator7" NAME="Customvalidator1"></asp:CustomValidator>
			
			<h2>Your stuff</h2>
			<p>
				What stuff do you own, and what stuff might you buy or replace in the next six months?
			</p>
			<p>
				<table cellpadding="0" cellspacing="0">
					<tr>
						<td>Car / motorbike</td>
						<td align="center"><asp:CheckBox Runat="server" ID="OwnCar" Text="Own now"></asp:CheckBox></td>
						<td>&nbsp;&nbsp;</td>
						<td align="center"><asp:CheckBox Runat="server" ID="BuyCar" Text="Might buy soon"></asp:CheckBox></td>
					</tr>
					<tr>
						<td>Pedal bike</td>
						<td align="center"><asp:CheckBox Runat="server" ID="OwnBike" Text="Own now"></asp:CheckBox></td>
						<td>&nbsp;&nbsp;</td>
						<td align="center"><asp:CheckBox Runat="server" ID="BuyBike" Text="Might buy soon"></asp:CheckBox></td>
					</tr>
					<tr>
						<td>MP3 player</td>
						<td align="center"><asp:CheckBox Runat="server" ID="OwnMp3" Text="Own now"></asp:CheckBox></td>
						<td>&nbsp;&nbsp;</td>
						<td align="center"><asp:CheckBox Runat="server" ID="BuyMp3" Text="Might buy soon"></asp:CheckBox></td>
					</tr>
					<tr>
						<td>PC</td>
						<td align="center"><asp:CheckBox Runat="server" ID="OwnPc" Text="Own now"></asp:CheckBox></td>
						<td>&nbsp;&nbsp;</td>
						<td align="center"><asp:CheckBox Runat="server" ID="BuyPc" Text="Might buy soon"></asp:CheckBox></td>
					</tr>
					<tr>
						<td>Laptop</td>
						<td align="center"><asp:CheckBox Runat="server" ID="OwnLaptop" Text="Own now"></asp:CheckBox></td>
						<td>&nbsp;&nbsp;</td>
						<td align="center"><asp:CheckBox Runat="server" ID="BuyLaptop" Text="Might buy soon"></asp:CheckBox></td>
					</tr>
					<tr>
						<td>Mac</td>
						<td align="center"><asp:CheckBox Runat="server" ID="OwnMac" Text="Own now"></asp:CheckBox></td>
						<td>&nbsp;&nbsp;</td>
						<td align="center"><asp:CheckBox Runat="server" ID="BuyMac" Text="Might buy soon"></asp:CheckBox></td>
					</tr>
					<tr>
						<td>Broadband internet</td>
						<td align="center"><asp:CheckBox Runat="server" ID="OwnBroadband" Text="Own now"></asp:CheckBox></td>
						<td>&nbsp;&nbsp;</td>
						<td align="center"><asp:CheckBox Runat="server" ID="BuyBroadband" Text="Might buy soon"></asp:CheckBox></td>
					</tr>
					<tr>
						<td>Games console</td>
						<td align="center"><asp:CheckBox Runat="server" ID="OwnConsole" Text="Own now"></asp:CheckBox></td>
						<td>&nbsp;&nbsp;</td>
						<td align="center"><asp:CheckBox Runat="server" ID="BuyConsole" Text="Might buy soon"></asp:CheckBox></td>
					</tr>
					<tr>
						<td>Digital camera</td>
						<td align="center"><asp:CheckBox Runat="server" ID="OwnCamera" Text="Own now"></asp:CheckBox></td>
						<td>&nbsp;&nbsp;</td>
						<td align="center"><asp:CheckBox Runat="server" ID="BuyCamera" Text="Might buy soon"></asp:CheckBox></td>
					</tr>
					<tr>
						<td>DVD player</td>
						<td align="center"><asp:CheckBox Runat="server" ID="OwnDvd" Text="Own now"></asp:CheckBox></td>
						<td>&nbsp;&nbsp;</td>
						<td align="center"><asp:CheckBox Runat="server" ID="BuyDvd" Text="Might buy soon"></asp:CheckBox></td>
					</tr>
					<tr>
						<td>DVD recorder</td>
						<td align="center"><asp:CheckBox Runat="server" ID="OwnDvdRec" Text="Own now"></asp:CheckBox></td>
						<td>&nbsp;&nbsp;</td>
						<td align="center"><asp:CheckBox Runat="server" ID="BuyDvdRec" Text="Might buy soon"></asp:CheckBox></td>
					</tr>
				</table>
			</p>
			
			
			<h2>Shopping</h2>
			<p>
				How much do you spend on average per month on:
			</p>
			<p>
				<table cellpadding="0" cellspacing="0">
					<tr><td>Designer / branded clothes</td>
						<td style="padding:2px;"><asp:DropDownList Runat="server" ID="SpendDesignerClothes"><asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem><asp:ListItem Value="1">Nothing</asp:ListItem><asp:ListItem Value="2">Less than £10</asp:ListItem><asp:ListItem Value="3">£10 - £20</asp:ListItem><asp:ListItem Value="4">£20 - £50</asp:ListItem><asp:ListItem Value="5">£50 - £100</asp:ListItem><asp:ListItem Value="6">£100 - £200</asp:ListItem><asp:ListItem Value="7">£200+</asp:ListItem></asp:DropDownList> per month</td></tr>
					<tr><td>High street / non-branded clothes</td>
						<td style="padding:2px;"><asp:DropDownList Runat="server" ID="SpendHighStreetClothes"><asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem><asp:ListItem Value="1">Nothing</asp:ListItem><asp:ListItem Value="2">Less than £10</asp:ListItem><asp:ListItem Value="3">£10 - £20</asp:ListItem><asp:ListItem Value="4">£20 - £50</asp:ListItem><asp:ListItem Value="5">£50 - £100</asp:ListItem><asp:ListItem Value="6">£100 - £200</asp:ListItem><asp:ListItem Value="7">£200+</asp:ListItem></asp:DropDownList> per month</td></tr>
					<tr><td>Music on CD</td>
						<td style="padding:2px;"><asp:DropDownList Runat="server" ID="SpendMusicCd"><asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem><asp:ListItem Value="1">Nothing</asp:ListItem><asp:ListItem Value="2">Less than £10</asp:ListItem><asp:ListItem Value="3">£10 - £20</asp:ListItem><asp:ListItem Value="4">£20 - £50</asp:ListItem><asp:ListItem Value="5">£50 - £100</asp:ListItem><asp:ListItem Value="6">£100 - £200</asp:ListItem><asp:ListItem Value="7">£200+</asp:ListItem></asp:DropDownList> per month</td></tr>
					<tr><td>Music on vinyl</td>
						<td style="padding:2px;"><asp:DropDownList Runat="server" ID="SpendMusicVinyl"><asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem><asp:ListItem Value="1">Nothing</asp:ListItem><asp:ListItem Value="2">Less than £10</asp:ListItem><asp:ListItem Value="3">£10 - £20</asp:ListItem><asp:ListItem Value="4">£20 - £50</asp:ListItem><asp:ListItem Value="5">£50 - £100</asp:ListItem><asp:ListItem Value="6">£100 - £200</asp:ListItem><asp:ListItem Value="7">£200+</asp:ListItem></asp:DropDownList> per month</td></tr>
					<tr><td>Music downloads</td>
						<td style="padding:2px;"><asp:DropDownList Runat="server" ID="SpendMusicDownload"><asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem><asp:ListItem Value="1">Nothing</asp:ListItem><asp:ListItem Value="2">Less than £10</asp:ListItem><asp:ListItem Value="3">£10 - £20</asp:ListItem><asp:ListItem Value="4">£20 - £50</asp:ListItem><asp:ListItem Value="5">£50 - £100</asp:ListItem><asp:ListItem Value="6">£100 - £200</asp:ListItem><asp:ListItem Value="7">£200+</asp:ListItem></asp:DropDownList> per month</td></tr>
					<tr><td>DVDs</td>
						<td style="padding:2px;"><asp:DropDownList Runat="server" ID="SpendDvd"><asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem><asp:ListItem Value="1">Nothing</asp:ListItem><asp:ListItem Value="2">Less than £10</asp:ListItem><asp:ListItem Value="3">£10 - £20</asp:ListItem><asp:ListItem Value="4">£20 - £50</asp:ListItem><asp:ListItem Value="5">£50 - £100</asp:ListItem><asp:ListItem Value="6">£100 - £200</asp:ListItem><asp:ListItem Value="7">£200+</asp:ListItem></asp:DropDownList> per month</td></tr>
					<tr><td>Computer/video games</td>
						<td style="padding:2px;"><asp:DropDownList Runat="server" ID="SpendGames"><asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem><asp:ListItem Value="1">Nothing</asp:ListItem><asp:ListItem Value="2">Less than £10</asp:ListItem><asp:ListItem Value="3">£10 - £20</asp:ListItem><asp:ListItem Value="4">£20 - £50</asp:ListItem><asp:ListItem Value="5">£50 - £100</asp:ListItem><asp:ListItem Value="6">£100 - £200</asp:ListItem><asp:ListItem Value="7">£200+</asp:ListItem></asp:DropDownList> per month</td></tr>
					<tr><td>Mobile phone calls</td>
						<td style="padding:2px;"><asp:DropDownList Runat="server" ID="SpendMobile"><asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem><asp:ListItem Value="1">Nothing</asp:ListItem><asp:ListItem Value="2">Less than £10</asp:ListItem><asp:ListItem Value="3">£10 - £20</asp:ListItem><asp:ListItem Value="4">£20 - £50</asp:ListItem><asp:ListItem Value="5">£50 - £100</asp:ListItem><asp:ListItem Value="6">£100 - £200</asp:ListItem><asp:ListItem Value="7">£200+</asp:ListItem></asp:DropDownList> per month</td></tr>
					<tr><td>Ringtones / text voting etc.</td>
						<td style="padding:2px;"><asp:DropDownList Runat="server" ID="SpendSms"><asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem><asp:ListItem Value="1">Nothing</asp:ListItem><asp:ListItem Value="2">Less than £10</asp:ListItem><asp:ListItem Value="3">£10 - £20</asp:ListItem><asp:ListItem Value="4">£20 - £50</asp:ListItem><asp:ListItem Value="5">£50 - £100</asp:ListItem><asp:ListItem Value="6">£100 - £200</asp:ListItem><asp:ListItem Value="7">£200+</asp:ListItem></asp:DropDownList> per month</td></tr>
					<tr><td>Car / motorbike</td>
						<td style="padding:2px;"><asp:DropDownList Runat="server" ID="SpendCar"><asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem><asp:ListItem Value="1">Nothing</asp:ListItem><asp:ListItem Value="2">Less than £10</asp:ListItem><asp:ListItem Value="3">£10 - £20</asp:ListItem><asp:ListItem Value="4">£20 - £50</asp:ListItem><asp:ListItem Value="5">£50 - £100</asp:ListItem><asp:ListItem Value="6">£100 - £200</asp:ListItem><asp:ListItem Value="7">£200+</asp:ListItem></asp:DropDownList> per month</td></tr>
					<tr><td>Other travel / public transport</td>
						<td style="padding:2px;"><asp:DropDownList Runat="server" ID="SpendTravel"><asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem><asp:ListItem Value="1">Nothing</asp:ListItem><asp:ListItem Value="2">Less than £10</asp:ListItem><asp:ListItem Value="3">£10 - £20</asp:ListItem><asp:ListItem Value="4">£20 - £50</asp:ListItem><asp:ListItem Value="5">£50 - £100</asp:ListItem><asp:ListItem Value="6">£100 - £200</asp:ListItem><asp:ListItem Value="7">£200+</asp:ListItem></asp:DropDownList> per month</td></tr>
				</table>
			</p>
			<asp:CustomValidator Runat="server" Display="None" OnServerValidate="ShoppingVal" ErrorMessage="Shopping - please answer each question." ID="Customvalidator8" NAME="Customvalidator1"></asp:CustomValidator>
			
			<h2>How often do you go abroad on holiday?</h2>
			<p>
				<asp:DropDownList Runat="server" ID="Holidays">
					<asp:ListItem Value="0" Text="&nbsp;">&nbsp;</asp:ListItem>
					<asp:ListItem Value="1">Never</asp:ListItem>
					<asp:ListItem Value="2">Less than once a year</asp:ListItem>
					<asp:ListItem Value="3">Once a year</asp:ListItem>
					<asp:ListItem Value="4">Twice a year</asp:ListItem>
					<asp:ListItem Value="5">3 times per year</asp:ListItem>
					<asp:ListItem Value="6">4 times per year</asp:ListItem>
					<asp:ListItem Value="7">5+ times per year</asp:ListItem>
				</asp:DropDownList>
			</p>
			<asp:CustomValidator Runat="server" Display="None" OnServerValidate="HolidayVal" ErrorMessage="How often do you go abroad on holiday?" ID="Customvalidator9" NAME="Customvalidator1"></asp:CustomValidator>
			
			<asp:Panel runat="server" Visible="false">
				<h2>Phones</h2>
				<p>
					When you think of <b>mobile phone imaging technology</b>, which mobile phone manufacturer comes to mind first?
				</p>
				<p>
					<asp:TextBox runat="server" ID="ImagingManufacturer"></asp:TextBox>
				</p>
				<asp:RequiredFieldValidator runat="server" Display="none" EnableClientScript="false"
					ControlToValidate="ImagingManufacturer"
					ErrorMessage="When you think of mobile phone imaging technology, which mobile phone manufacturer comes to mind first?"/>
					
				<p>
					On a scale of 1-5, how important is <b>imaging functionality</b> in a mobile phone when considering which handset to get? (1 = not very important, 5 = very important)
				</p>
				<p>
					<img src="/gfx/icon-thumbs-down.png" width="26" height="21" align="absmiddle" style="margin-right:3px;">
					<asp:RadioButton Runat="server" GroupName="ImagingImportant" ID="ImagingImportant1" Text="1"/>
					<asp:RadioButton Runat="server" GroupName="ImagingImportant" ID="ImagingImportant2" Text="2"/>
					<asp:RadioButton Runat="server" GroupName="ImagingImportant" ID="ImagingImportant3" Text="3"/>
					<asp:RadioButton Runat="server" GroupName="ImagingImportant" ID="ImagingImportant4" Text="4"/>
					<asp:RadioButton Runat="server" GroupName="ImagingImportant" ID="ImagingImportant5" Text="5"/>
					<img src="/gfx/icon-thumbs-up.png" width="26" height="21" align="absmiddle" style="margin-left:3px;">
				</p>
				<asp:CustomValidator Runat="server" Display="None" OnServerValidate="ImagingImportantVal" 
					ErrorMessage="How important is <b>imaging functionality</b> in a mobile phone when considering which handset to get?" ID="Customvalidator10" NAME="Customvalidator1"></asp:CustomValidator>
				
				<p>
					How would you describe your <b>overall opinion</b> about the following mobile phone manufacturers? (1 = not good, 5 = very good)
				</p>
				<p>
					<table>
						<tr>
							<td>&nbsp;</td>
							<td>
								<img src="/gfx/icon-thumbs-down.png" width="26" height="21">
							</td>
							<td align="right">
								<img src="/gfx/icon-thumbs-up.png" width="26" height="21">
							</td>
						</tr>
						<tr>
							<td>
								Sony Ericsson
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSony" ID="ImagingOpinionSony1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSony" ID="ImagingOpinionSony2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSony" ID="ImagingOpinionSony3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSony" ID="ImagingOpinionSony4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSony" ID="ImagingOpinionSony5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								Nokia
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionNokia" ID="ImagingOpinionNokia1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionNokia" ID="ImagingOpinionNokia2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionNokia" ID="ImagingOpinionNokia3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionNokia" ID="ImagingOpinionNokia4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionNokia" ID="ImagingOpinionNokia5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								Motorola
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionMotorola" ID="ImagingOpinionMotorola1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionMotorola" ID="ImagingOpinionMotorola2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionMotorola" ID="ImagingOpinionMotorola3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionMotorola" ID="ImagingOpinionMotorola4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionMotorola" ID="ImagingOpinionMotorola5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								BenQ/Siemens
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSiemens" ID="ImagingOpinionSiemens1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSiemens" ID="ImagingOpinionSiemens2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSiemens" ID="ImagingOpinionSiemens3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSiemens" ID="ImagingOpinionSiemens4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSiemens" ID="ImagingOpinionSiemens5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								LG
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionLg" ID="ImagingOpinionLg1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionLg" ID="ImagingOpinionLg2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionLg" ID="ImagingOpinionLg3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionLg" ID="ImagingOpinionLg4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionLg" ID="ImagingOpinionLg5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								Samsung
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSamsung" ID="ImagingOpinionSamsung1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSamsung" ID="ImagingOpinionSamsung2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSamsung" ID="ImagingOpinionSamsung3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSamsung" ID="ImagingOpinionSamsung4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingOpinionSamsung" ID="ImagingOpinionSamsung5" Text="5"/>
							</td>
						</tr>
					</table>
				</p>
				<asp:CustomValidator Runat="server" Display="None" OnServerValidate="ImagingOpinionVal" 
					ErrorMessage="How would you describe your <b>overall opinion</b> about the following mobile phone manufacturers?" ID="Customvalidator12" NAME="Customvalidator1"></asp:CustomValidator>
				
				<p>
					Thinking of mobile phone <b>imaging capabilities</b>, how would you rate each of the following manufacturers? (1=does not provide imaging technology, 5=provides advanced imaging technology)
				</p>
				<p>
					<table>
						<tr>
							<td>&nbsp;</td>
							<td>
								<img src="/gfx/icon-thumbs-down.png" width="26" height="21">
							</td>
							<td align="right">
								<img src="/gfx/icon-thumbs-up.png" width="26" height="21">
							</td>
						</tr>
						<tr>
							<td>
								Sony Ericsson
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySony" ID="ImagingCapabilitySony1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySony" ID="ImagingCapabilitySony2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySony" ID="ImagingCapabilitySony3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySony" ID="ImagingCapabilitySony4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySony" ID="ImagingCapabilitySony5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								Nokia
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityNokia" ID="ImagingCapabilityNokia1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityNokia" ID="ImagingCapabilityNokia2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityNokia" ID="ImagingCapabilityNokia3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityNokia" ID="ImagingCapabilityNokia4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityNokia" ID="ImagingCapabilityNokia5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								Motorola
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityMotorola" ID="ImagingCapabilityMotorola1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityMotorola" ID="ImagingCapabilityMotorola2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityMotorola" ID="ImagingCapabilityMotorola3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityMotorola" ID="ImagingCapabilityMotorola4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityMotorola" ID="ImagingCapabilityMotorola5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								BenQ/Siemens
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySiemens" ID="ImagingCapabilitySiemens1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySiemens" ID="ImagingCapabilitySiemens2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySiemens" ID="ImagingCapabilitySiemens3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySiemens" ID="ImagingCapabilitySiemens4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySiemens" ID="ImagingCapabilitySiemens5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								LG
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityLg" ID="ImagingCapabilityLg1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityLg" ID="ImagingCapabilityLg2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityLg" ID="ImagingCapabilityLg3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityLg" ID="ImagingCapabilityLg4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilityLg" ID="ImagingCapabilityLg5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								Samsung
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySamsung" ID="ImagingCapabilitySamsung1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySamsung" ID="ImagingCapabilitySamsung2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySamsung" ID="ImagingCapabilitySamsung3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySamsung" ID="ImagingCapabilitySamsung4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingCapabilitySamsung" ID="ImagingCapabilitySamsung5" Text="5"/>
							</td>
						</tr>
					</table>
				</p>
				<asp:CustomValidator Runat="server" Display="None" OnServerValidate="ImagingCapabilityVal" 
					ErrorMessage="Thinking of mobile phone imaging capabilities, how would you rate each of the following manufacturers?" ID="Customvalidator13" NAME="Customvalidator1"></asp:CustomValidator>
				
				
				<p>
					If you were to <b>buy a new mobile phone</b>, how likely would you be to consider the following manufacturers? (1=very unlikely, 5 = very likely)
				</p>
				<p>
					<table>
						<tr>
							<td>&nbsp;</td>
							<td>
								<img src="/gfx/icon-thumbs-down.png" width="26" height="21">
							</td>
							<td align="right">
								<img src="/gfx/icon-thumbs-up.png" width="26" height="21">
							</td>
						</tr>
						<tr>
							<td>
								Sony Ericsson
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingBuySony" ID="ImagingBuySony1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySony" ID="ImagingBuySony2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySony" ID="ImagingBuySony3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySony" ID="ImagingBuySony4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySony" ID="ImagingBuySony5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								Nokia
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingBuyNokia" ID="ImagingBuyNokia1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyNokia" ID="ImagingBuyNokia2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyNokia" ID="ImagingBuyNokia3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyNokia" ID="ImagingBuyNokia4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyNokia" ID="ImagingBuyNokia5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								Motorola
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingBuyMotorola" ID="ImagingBuyMotorola1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyMotorola" ID="ImagingBuyMotorola2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyMotorola" ID="ImagingBuyMotorola3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyMotorola" ID="ImagingBuyMotorola4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyMotorola" ID="ImagingBuyMotorola5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								BenQ/Siemens
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingBuySiemens" ID="ImagingBuySiemens1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySiemens" ID="ImagingBuySiemens2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySiemens" ID="ImagingBuySiemens3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySiemens" ID="ImagingBuySiemens4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySiemens" ID="ImagingBuySiemens5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								LG
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingBuyLg" ID="ImagingBuyLg1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyLg" ID="ImagingBuyLg2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyLg" ID="ImagingBuyLg3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyLg" ID="ImagingBuyLg4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuyLg" ID="ImagingBuyLg5" Text="5"/>
							</td>
						</tr>
						<tr>
							<td>
								Samsung
							</td>
							<td colspan="2">
								<asp:RadioButton Runat="server" GroupName="ImagingBuySamsung" ID="ImagingBuySamsung1" Text="1"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySamsung" ID="ImagingBuySamsung2" Text="2"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySamsung" ID="ImagingBuySamsung3" Text="3"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySamsung" ID="ImagingBuySamsung4" Text="4"/>
								<asp:RadioButton Runat="server" GroupName="ImagingBuySamsung" ID="ImagingBuySamsung5" Text="5"/>
							</td>
						</tr>
					</table>
				</p>
				<asp:CustomValidator Runat="server" Display="None" OnServerValidate="ImagingBuyVal" 
					ErrorMessage="If you were to buy a new mobile phone, how likely would you be to consider the following manufacturers?" ID="Customvalidator11" NAME="Customvalidator1"></asp:CustomValidator>
				
			</asp:Panel>
			
			
			<h2>That's it!</h2>
			<p>
				Click the button below to get your new icon!
			</p>
			<p>
				<asp:Button CssClass="LargeButton" Runat="server" OnClick="Finish" Text="Finish" ID="Button3"></asp:Button>
			</p>
		</div>
	</asp:Panel>
	<br>&nbsp;<br>&nbsp;<br>
</center>
<script>
function openPopup(url)
{
	var popUp = window.open(url, popUp, 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=400');
}
</script>
</body>
</html>
