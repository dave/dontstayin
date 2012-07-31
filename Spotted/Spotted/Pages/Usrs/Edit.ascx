<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="Spotted.Pages.Usrs.Edit" %>
<%@ Register TagPrefix="dsi" TagName="Picker" Src="/Controls/Picker.ascx" %>
<style>
	.ErrorSpan
	{
		margin-top:5px;
		margin-bottom:5px;
	}
</style>
<dsi:h1 runat="server" ID="H11" NAME="H11">My details</dsi:h1>
<asp:Panel Runat="server" ID="PrefsUpdatePanel">
	<div class="ContentBorder">
		

		<asp:ValidationSummary id="ValidationSummary" runat="server" Font-Bold="True"
					CssClass="PaymentValidationSummary" HeaderText="There were some errors:"
					EnableClientScript="False" ShowSummary="True" DisplayMode="BulletList"></asp:ValidationSummary>

		<div id="SuccessDiv" Runat="server" class="ForegroundAttentionBlue BorderAttentionBlue" style="font-weight:bold; border-width:2px; margin:3px; padding:5px; border-style:solid;" visible="false">
			Details updated
		</div>
		
		<p>You may modify the details below to update your profile:</p>

		<h2>
			Name
		</h2>
		<p>
			<div style="width:150px; display:inline-block;"><small>First name:</small></div>
			<div style="width:150px; display:inline-block;"><small>Last name:</small></div><br />

			<div style="width:150px; display:inline-block;"><asp:TextBox id="FirstName" Runat="server" Columns="15"></asp:TextBox></div>
			<div style="width:150px; display:inline-block;"><asp:TextBox id="LastName" Runat="server" Columns="15"></asp:TextBox></div>
			
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
				Runat="server" Display="Dynamic" ControlToValidate="FirstName" 
				ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Please enter a first name</span>"/>
			<asp:RequiredFieldValidator
				Runat="server" Display="Dynamic" ControlToValidate="LastName" 
				ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Please enter a last name</span>" ID="Requiredfieldvalidator3"/>
		</p>
		<h2>
			Nickname
		</h2>
		<p>
			<asp:TextBox id="NickName" Runat="server" Columns="30" MaxLength="20"></asp:TextBox>

			<asp:RequiredFieldValidator 
				Runat="server" ID="Requiredfieldvalidator49999"
				Display="Dynamic" ControlToValidate="NickName" 
				ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Please enter a nickname</span>"></asp:RequiredFieldValidator>

			<asp:RegularExpressionValidator 
				Runat="server" id="Regularexpressionvalidator99" ControlToValidate="NickName" 
				ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Your nickname must be between 2 and 20 characters, and must only include letters, numbers and the dash character (-). It must start with a letter, and must not end with a dash.</span>"
				Display="Dynamic" ValidationExpression="^[A-Za-z][A-Za-z0-9 -]{0,18}[A-Za-z0-9]$"></asp:RegularExpressionValidator>

			<asp:CustomValidator 
				Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="NickNameDuplicateVal"
				ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>The nickname you entered is already used by someone on DontStayIn. Please choose another.</span>"
				ID="Customvalidator5" NAME="Customvalidator5"></asp:CustomValidator>
		</p>

		<h2>
			Email
		</h2>
		<p>
			<asp:TextBox id="Email" Runat="server" Columns="30"></asp:TextBox><asp:RequiredFieldValidator 
				id="Requiredfieldvalidator2" Runat="server" ControlToValidate="Email" 
				ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Please enter an email address</span>" Display="Dynamic"
			/><asp:RegularExpressionValidator 
				id="EmailRegex" Runat="server" ControlToValidate="Email" 
				ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Please enter your real email address</span>" Display="Dynamic"
			/><asp:CustomValidator 
				id="emailDuplicateValidator" Runat="server" ControlToValidate="Email" 
				ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Email address already in our database, please try again.</span>" 
				Display="Dynamic" EnableClientScript="False"/>
		</p>

		<h2>
			Mobile number
		</h2>
		<p>
			<div style="width:150px; display:inline-block;"><small>Country&nbsp;code:</small></div>
			<div style="width:150px; display:inline-block;"><small>Number:</small></div><br />

			<div style="width:150px; display:inline-block;">
				<asp:DropDownList Runat="server" ID="DialingCodeDropDown" onchange="">
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
			</div>
			<div style="width:150px; display:inline-block;">
				<asp:TextBox Runat="server" ID="MobileNumber" Columns="15" MaxLength="15"/>
			</div>

			<span runat="server" id="DialingCodeOtherSpan"><br /><small>If other:</small> <asp:TextBox Runat="server" ID="DialingCodeOther" Columns="3" MaxLength="3"/></span>
		</p>

		<h2>
			Sex
		</h2>
		<p>
			<asp:RadioButton Runat="server" ID="SexMale" GroupName="Sex" Text="Boy"/>
			<asp:RadioButton Runat="server" ID="SexFemale" GroupName="Sex" Text="Girl"/><asp:CustomValidator ID="CustomValidator1" Runat="server"
				Display="Dynamic" EnableClientScript="False" OnServerValidate="SexVal" ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Please select your sex</span>"/>
		</p>

		<h2>
			Date of birth
		</h2>

		<p>
			<div style="width:60px; display:inline-block;"><small>Year:</small></div>
			<div style="width:50px; display:inline-block;"><small>Month:</small></div>
			<div style="width:50px; display:inline-block;"><small>Day:</small></div><br />

			<div style="width:60px; display:inline-block;"><asp:TextBox Runat="server" ID="DateOfBirthYear" Columns="4"/></div>
			<div style="width:50px; display:inline-block;"><asp:TextBox Runat="server" ID="DateOfBirthMonth" Columns="2"/></div>
			<div style="width:50px; display:inline-block;"><asp:TextBox Runat="server" ID="DateOfBirthDay" Columns="2"/></div>
			
			<asp:CustomValidator Runat="server" 
				EnableClientScript="False" OnServerValidate="DateOfBirthVal" Display="Dynamic" 
				ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Your date of birth isn't a valid date. Please enter the full date, including day, month and full, 4-digit year.</span>" 
				ID="Customvalidator8" NAME="Customvalidator3"/><asp:CustomValidator Runat="server" EnableClientScript="False" 
				OnServerValidate="DateOfBirthRangeVal" Display="Dynamic" 
				ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Please check your date of birth. You must be over 13 years old to use DontStayIn.</span>" 
				ID="Customvalidator9" NAME="Customvalidator3"/>

		</p>

		<h2>
			Favourite music
		</h2>
		<p>
			<asp:DropDownList Runat="server" ID="FavouriteMusicDropDownList"></asp:DropDownList><asp:CustomValidator Runat="server"
				Display="Dynamic" EnableClientScript="False" OnServerValidate="FavouriteMusicVal" ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Please select your favourite music. You can give us more details on your music preferences on the <b>Music I listen to</b> page</span>" ID="Customvalidator4" NAME="Customvalidator2"/>
		</p>

		<h2>
			Home town
		</h2>
		<p>
			<dsi:Picker 
				runat="server"
				id="HomeTownPlacePicker" 
				OptionEvent="false"
				OptionDate="false"
				OptionBrand="false"
				OptionVenue="false"
				ValidationType="place" />

			<asp:RequiredFieldValidator Runat="server"
				Display="Dynamic" EnableClientScript="False" ControlToValidate="HomeTownPlacePicker" ErrorMessage="Please select a town." />

			<small>
				If we're missing your home town, just choose the nearest large town.
			</small>
		</p>

		<h2>
			Address
		</h2>

		<p>
			<table cellpadding="0" cellspacing="0" border="0">
				<tr><td><asp:TextBox Runat="server" ID="AddressStreetTextBox" Columns="40"/> (street)</td></tr>
				<tr><td><asp:TextBox Runat="server" ID="AddressAreaTextBox" Columns="40"/> (area)</td></tr>
				<tr><td><asp:TextBox Runat="server" ID="AddressTownTextBox" Columns="40"/> (town / city)</td></tr>
				<tr><td><asp:TextBox Runat="server" ID="AddressCountyTextBox" Columns="20"/> (county / state)</td></tr>
				<tr><td><asp:TextBox Runat="server" ID="AddressPostcodeTextBox" Columns="10"/> (postcode / zipcode)</td></tr>
				<tr><td><asp:DropDownList Runat="server" ID="AddressCountryDropDownList"/> (country)</td></tr>
			</table>
		</p>

<h2>Are you a DJ?</h2>
					<p>
						<asp:RadioButton runat="server" GroupName="IsDj" ID="IsDjYes" Text="Yes" />
						<asp:RadioButton runat="server" GroupName="IsDj" ID="IsDjNo" Text="No" /><asp:CustomValidator ID="CustomValidatorIsDj" Runat="server"
							Display="Dynamic" EnableClientScript="False" OnServerValidate="IsDjVal" ErrorMessage="<span class='ErrorSpan ForegroundAttentionRed' style='font-weight:bold;display:block;'>Please select if you're a DJ or not</span>" />
					</p>


	</div>
	

	<a name="SaveBox"></a>
	<dsi:h1 runat="server">Save changes</dsi:h1>
	<div class="ContentBorder">
		<p>When you have finished, click the button below to save your changes.</p>
		<p>
			<asp:Button id="PrefsUpdateButton" onclick="PrefsUpdateClick" Runat="server" Text="Update"></asp:Button>
		</p>
	</div>
</asp:Panel>
