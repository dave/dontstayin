<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Welcome.ascx.cs" Inherits="Spotted.Blank.Welcome" %>
<%@ Register TagPrefix="Spotted" TagName="Terms" Src="/Controls/LegalTermsUser.ascx" %>
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
		background-color:#FECA26;
		margin-bottom:10px;	
		text-align:left;
	}
	div.TermsBox{
		position:relative;
		left:36px;
		padding:3px 13px 3px 13px;
		border-left:3px solid #000000;
		border-right:3px solid #000000;
		border-bottom:3px solid #000000;
		background-color:#FECA26;
		margin-bottom:10px;
		text-align:left;
	}
	.ValidationSummary{
		position:relative;
		left:35px;
		padding:9px 13px 13px 13px;
		border:3px solid #000000;
		background-color:#FECA26;
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
		font-size:10px;
	}
	a:link, 
	a:visited         { color:#000000; }
	a:hover           { color:#FF0000; }

	small a:link, 
	small a:visited   { color:#A58319; }
	small a:hover     { color:#FF0000; }	
	
	tr.dataGridItem td { background-color:#FECA26; }
	tr.dataGridAltItem td {	background-color:#FED551; }

</style>
<head>
	<title>Welcome to DontStayIn!</title>
</head>
<body>
<center>
	<br />
	<div style="width:500px;">
		<img src="/gfx/welcome-top-2.jpg" class="WelcomeHeaderTop"><div class="WelcomeBox">
			<asp:Panel Runat="server" ID="WelcomeHeaderInvite">
				<p>
					Someone invited you to DontStayIn, so we've created you a free
					account. Before you use the site we'd like to know a little bit more 
					about you. You've got three options:
				</p>
			</asp:Panel>
			<asp:Panel Runat="server" ID="WelcomeHeaderSignUp">
				<p>
					There's just a few more things to complete before you can start using 
					DontStayIn... Just complete the details below:
				</p>
			</asp:Panel>
		</div>
		
		<asp:Panel Runat="server" ID="WelcomePart1Header">
			<img src="/gfx/welcome-1.jpg" class="WelcomeHeader"><div class="WelcomeBox">
				<p>
					Complete the following details - you'll then be fully signed up to 
					DontStayIn, and you'll be able to use the site. <b>It's all FREE!</b>
				</p>
				<p>
					All your personal details are <b>kept private</b>, and not shown 
					publicly on the site.
				</p>
			</div>
		</asp:Panel>
		<asp:ValidationSummary Runat="server" 
			HeaderText="<b>You've made some mistakes:</b>" DisplayMode="SingleParagraph" ShowSummary="True" CssClass="ValidationSummary" ID="Validationsummary1"/>

		<div class="WelcomeBox" runat="server" id="uiAddedByUsrsDiv">
			<p>
				You were invited to DontStayIn by <asp:Label runat="server" id="uiAddedByUsrLabel"/>.
			</p>
			<asp:GridView ID="uiAddedByUsrsGridView" runat="server" DataKeyNames="K"
				AutoGenerateColumns="false" Width="100%"
				CssClass="dataGrid" BorderWidth="0" CellPadding="3"
				AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top" ShowHeader="false"
				OnRowDataBound="uiAddedByUsrsGridView_RowDataBound">
				<Columns>
					<asp:TemplateField ItemStyle-Width="50">
						<ItemTemplate>
							<img style="border:1px solid #000000;" width="50" height="50" src="<%# ((Bobs.Usr)Container.DataItem).PicPath %>" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField>
						<ItemTemplate>
							<asp:CheckBox runat="server" ID="uiCheckBox" />
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</div>

		<div class="WelcomeBox" runat="server" id="uiAddedByGroupsDiv">
			<p>
				You've been invited to the <asp:Label runat="server" id="uiAddedByGroupLabel"/>.
			</p>
			<asp:GridView ID="uiAddedByGroupsGridView" runat="server" DataKeyNames="K"
				AutoGenerateColumns="false" Width="100%" 
				CssClass="dataGrid" BorderWidth="0" CellPadding="3"
				AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top" ShowHeader="false"
				OnRowDataBound="uiAddedByGroupsGridView_RowDataBound">
				<Columns>
					<asp:TemplateField ItemStyle-Width="50">
						<ItemTemplate>
							<img style="border:1px solid #000000;" width="50" height="50" src="<%# ((Bobs.Group)Container.DataItem).PicPath %>" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField>
						<ItemTemplate>
							<asp:CheckBox runat="server" ID="uiCheckBox" />
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</div>
		<div class="WelcomeBox" style="border-top:3px solid #000000;padding:4px 7px 7px 7px;">
			<table cellSpacing="6" cellPadding="0" border="0">
				<tr>
					<th>
						Nickname:</th>
					<td>
						<asp:TextBox id="NickName" Runat="server" Columns="20" MaxLength="20"></asp:TextBox><small> this is your name that's displayed the site</small>
						<asp:RequiredFieldValidator 
							Runat="server" Display="Dynamic" ControlToValidate="NickName" 
							ErrorMessage="<br>Please enter a nickname" ID="Requiredfieldvalidator1"/>
						<asp:RegularExpressionValidator id="Regularexpressionvalidator99" Runat="server" 
							ControlToValidate="NickName" 
							ErrorMessage="<br>Your nickname must be between 2 and 20 characters, and must only include letters, numbers and the dash character (-). It must start with a letter, and must not end with a dash." Display="Dynamic" 
							ValidationExpression="^[A-Za-z][A-Za-z0-9 -]{0,18}[A-Za-z0-9]$"/>
						<asp:CustomValidator Runat="server" Display="Dynamic" EnableClientScript="False" 
							OnServerValidate="NickNameDuplicateVal"
							ErrorMessage="<br>The nickname you entered is already used by someone on DontStayIn. Please choose another." ID="Customvalidator1"></asp:CustomValidator>
					</td>
				</tr>
				<tr>
					<th>
						First name:</th>
					<td>
						<asp:TextBox id="FirstName" Runat="server" Columns="20"></asp:TextBox>
						<asp:RequiredFieldValidator 
							Runat="server" Display="Dynamic" ControlToValidate="FirstName" 
							ErrorMessage="<br>Please enter a first name" ID="Requiredfieldvalidator2"/>
						
					</td>
				</tr>
				<tr>
					<th>
						Last name:</th>
					<td>
						<asp:TextBox id="LastName" Runat="server" Columns="20"></asp:TextBox>
						<asp:RequiredFieldValidator
							Runat="server" Display="Dynamic" ControlToValidate="LastName" 
							ErrorMessage="<br>Please enter a last name" ID="Requiredfieldvalidator3"/>
						
					</td>
				</tr>
				<tr>
					<th>
						Email:</th>
					<td>
						<asp:TextBox id="Email" Runat="server" Columns="35"></asp:TextBox>
						<asp:RequiredFieldValidator 
							id="Requiredfieldvalidator4" Runat="server" ControlToValidate="Email" 
							ErrorMessage="<br>Please enter an email address" Display="Dynamic"/>
						<asp:RegularExpressionValidator 
							id="EmailRegex" Runat="server" ControlToValidate="Email"
							ErrorMessage="<br>Please enter your real email address" Display="Dynamic"/>
						<asp:CustomValidator 
							id="EmailDuplicateValidator" Runat="server" ControlToValidate="Email" 
							ErrorMessage="<br>Email address already in our database, please try again." 
							Display="Dynamic" EnableClientScript="False"/>
					</td>
				</tr>
				<tr runat="server" id="PasswordTr">
					<th>
						Choose a password:</th>
					<td>
						<asp:TextBox id="Password1" Runat="server" Columns="15" TextMode="Password"></asp:TextBox><small> enter it again to check: </small>
						<asp:TextBox id="Password2" Runat="server" Columns="15" TextMode="Password"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
							Display="Dynamic" ControlToValidate="Password2" ErrorMessage="<br>Please enter a password"/>
						<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
							Display="dynamic" ControlToValidate="Password2" ValidationExpression="^.{4,20}$" 
							ErrorMessage="<br>Please enter between 4 and 20 characters for your password" />
						<asp:CompareValidator ID="CompareValidator1" runat="server" 
							Display="dynamic" ControlToCompare="Password1" ControlToValidate="Password2" 
							Type="String" Operator="Equal" ErrorMessage="<br>The passwords you entered don't match. Please try again." />
					</td>
				</tr>
				<tr>
					<th valign="top">
						Mobile number (optional):</th>
					<td>
						<asp:DropDownList Runat="server" ID="DialingCodeDropDown">
							<asp:ListItem Value="44">UK (44)</asp:ListItem>
							<asp:ListItem Value="61">Australia (61)</asp:ListItem>
							<asp:ListItem Value="33">France (33)</asp:ListItem>
							<asp:ListItem Value="49">Germany (49)</asp:ListItem>
							<asp:ListItem Value="353">Ireland (353)</asp:ListItem>
							<asp:ListItem Value="39">Italy (39)</asp:ListItem>
							<asp:ListItem Value="34">Spain (34)</asp:ListItem>
							<asp:ListItem Value="1">USA (1)</asp:ListItem>
							<asp:ListItem Value="0">Other...</asp:ListItem>
						</asp:DropDownList>
						<small>country code </small><span runat="server" id="DialingCodeOtherSpan"><small>other:</small> <asp:TextBox Runat="server" ID="DialingCodeOther" Columns="3" MaxLength="3"/></span>
						<br>
						<asp:TextBox Runat="server" ID="MobileNumber" Columns="15" MaxLength="15"/><small> phone number</small>
					</td>
				</tr>
				<tr>
					<th>
						Sex:</th>
					<td>
						<asp:RadioButton Runat="server" ID="SexMale" GroupName="Sex" Text="Boy"/>
						<asp:RadioButton Runat="server" ID="SexFemale" GroupName="Sex" Text="Girl"/><asp:CustomValidator Runat="server"
						Display="Dynamic" EnableClientScript="False" OnServerValidate="SexVal" ErrorMessage="<br>Please select your sex" ID="Customvalidator2" NAME="Customvalidator1"/>
					</td>
				</tr>
				<tr>
					<th>
						Date of birth: 
					</th>
					<td>
						day:<asp:TextBox Runat="server" ID="DateOfBirthDay" Columns="2"/>
						month:<asp:TextBox Runat="server" ID="DateOfBirthMonth" Columns="2"/>
						year:<asp:TextBox Runat="server" ID="DateOfBirthYear" Columns="4"/>
						<asp:CustomValidator Runat="server" EnableClientScript="False" OnServerValidate="DateOfBirthVal" Display="Dynamic" ErrorMessage="<br>Your date of birth isn't a valid date. Please enter the full date, including day, month and full, 4-digit year." ID="Customvalidator8" NAME="Customvalidator3"/>
						<asp:CustomValidator Runat="server" EnableClientScript="False" OnServerValidate="DateOfBirthRangeVal" Display="Dynamic" ErrorMessage="<br>Please check your date of birth. You must be over 13 years old to use DontStayIn." ID="Customvalidator9" NAME="Customvalidator3"/>
					</td>
				</tr>
				<tr>
					<th>
						Favourite music:</th>
					<td>
						<asp:DropDownList Runat="server" ID="FavouriteMusicDropDownList"></asp:DropDownList><asp:CustomValidator Runat="server"
						Display="Dynamic" EnableClientScript="False" OnServerValidate="FavouriteMusicVal" ErrorMessage="<br>Please select your favourite music. You can give us more details on your music preferences after you sign up (on the <i>Music I listen to</i> page)" ID="Customvalidator6" NAME="Customvalidator2"/>
					</td>
				</tr>
				<tr>
					<th>
						Are you a DJ?</th>
					<td>
						<asp:RadioButton runat="server" GroupName="IsDj" ID="IsDjYes" Text="Yes" />
						<asp:RadioButton runat="server" GroupName="IsDj" ID="IsDjNo" Text="No" /><asp:CustomValidator ID="CustomValidatorIsDj" Runat="server"
							Display="Dynamic" EnableClientScript="False" OnServerValidate="IsDjVal" ErrorMessage="<br>Please select if you're a DJ or not" />
					</td>
				</tr>
				<tr>
					<th>
						Home town:</th>
					<td>
						<asp:DropDownList Runat="server" ID="HomeTownDropDownList"></asp:DropDownList> <small><a href="/pages/placemissing" onclick="openPopup('/popup/placemissing');return false;">my home town is missing!</a></small><asp:CustomValidator Runat="server"
						Display="Dynamic" EnableClientScript="False" OnServerValidate="HomeTownVal" ErrorMessage="<br>Please select your home town. If it's not listed, please select the nearest large town." ID="Customvalidator4" NAME="Customvalidator2"/>
					</td>
				</tr>
				<tr>
					<th>&nbsp;</th>
					<td>
						<asp:CheckBox id="SendSpottedEmails" Runat="server" 
							Text=" Send me the weekly email"></asp:CheckBox>
						<div style="margin-left:24px;">
							<small>
								Tick the box above to receive our weekly email, containing 
								quick details of parties in your area playing your favourite music.
							</small>
						</div>
					</td>
				</tr>
				<tr>
					<th>&nbsp;</th>
					<td>
						<asp:CheckBox id="SendInvites" Runat="server" 
							Text=" Send me party invites"></asp:CheckBox><br>
						<div style="margin-left:24px;">
							<small>
								Tick the box above to let us send you party invites and 
								guestlist offers.
							</small>
						</div>
					</td>
				</tr>
				<tr>
					<th>&nbsp;</th>
					<td>
						<asp:CheckBox id="SendFlyers" Runat="server" 
							Text=" Send me e-flyers"></asp:CheckBox>
						<div style="margin-left:24px;">
							<small>
								Tick the box above to let us send e-flyers to you by email.
							</small>
						</div>
					</td>
				</tr>
				<tr>
					<th>&nbsp;</th>
					<td>
						<asp:CheckBox id="SendSpottedTexts" Runat="server" 
							Text=" Send texts to my mobile"></asp:CheckBox>
						<div style="margin-left:24px;">
							<small>
								Tick the box above to let us send texts to your mobile 
								about parties and other cool stuff.
							</small>
						</div>
					</td>
				</tr>
				<tr>
					<th valign="top" style="padding-top:2px;">Verification:</th>
					<td>
						<table cellpadding="2" cellspacing="0">
							<tr>
								<td valign="top">
									You should see five uppercase letters to the right. Enter them below to continue:<br />
									<asp:TextBox runat="server" ID="HipChallengeTextBox" MaxLength="5"></asp:TextBox>
								</td>
								<td>
									<img src="/support/hipimage.aspx" runat="server" id="HipImage" style="border:1px solid #000000;" width="150" height="50" />
								</td>
							</tr>
						</table>
						<asp:CustomValidator Runat="server" Display="Dynamic" EnableClientScript="False" 
							OnServerValidate="HipVal" 
							ErrorMessage="<br>You got the verification letters wrong... You should see 5 upper-case letters. Try half-closing your eyes..." 
							ID="Customvalidator10"/>
					</td>
				</tr>
			</table>
		</div>
		
		<div class="TermsBox" style="border-top:3px solid #000000; height:200px; overflow:auto;">
			<Spotted:Terms runat="server" UsePopups="true"></Spotted:Terms>
		</div>
		
		<div class="WelcomeBox" style="border-top:3px solid #000000;">
			<p>
				<asp:CheckBox Runat="server" ID="TermsCheckbox" Text="I have read and agree to be bound by the terms of use"></asp:CheckBox>
			</p>
			<asp:CustomValidator Runat="server" Display="None" EnableClientScript="False" 
				OnServerValidate="TermsVal" ErrorMessage="<br>You must agree to the terms of use" ID="Customvalidator7"/>
			<p>
				<asp:Button id="PrefsUpdateButton" onclick="PrefsUpdateClick" Runat="server" 
					Text="Sign up and log on" CssClass="LargeButton"></asp:Button>
			</p>
			
		</div>
		
		<asp:Panel Runat="server" ID="WelcomePart2And3">
			<div><img src="/gfx/welcome-or.gif" style="position:relative;left:35px;"></div>
			
			<img src="/gfx/welcome-2.jpg" class="WelcomeHeader"><div class="WelcomeBox">
				<p>
					You'll be logged off, so you can check out the DontStayIn site before 
					entering your details. <b>You won't be able to view any of your private 
					messages, post comments or chat</b>.
				</p>
				<p>
					<asp:Button Runat="server" id="LogOffButton" OnClick="LogOff" Text="Log off" 
						CssClass="LargeButton" CausesValidation="False" />
				</p>
				<p>
					Tip: You can log back on by following the link in the email that brought 
					you here.
				</p>
			</div>
		
			<div><img src="/gfx/welcome-or.gif" style="position:relative;left:35px;"></div>
		
			<img src="/gfx/welcome-3a.jpg" class="WelcomeHeader"><div class="WelcomeBox">
				<p>
					If you wish to stop DontStayIn from sending you any further invitations from your friends,
					you can use the button below to unsubscribe. You won't be able to use DontStayIn while you're unsubscribed.
				</p>
				<p>
					<asp:Button Runat="server" ID="UnsubscribeButton" OnClick="Unsubscribe" Text="Unsubscribe" 
						CssClass="LargeButton" CausesValidation="False"/>
				</p>
			</div>
		</asp:Panel>
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

