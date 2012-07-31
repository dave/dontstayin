<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="Spotted.Styled.Login" %>
<%@ Register TagPrefix="Spotted" TagName="Terms" Src="/Controls/LegalTermsUser.ascx" %>
<script>
	function HideLoginRegister()
	{
		document.getElementById("LoginDiv").style.display = "none";
		document.getElementById("RegisterDiv").style.display = "none";
	}
	function ShowLogin()
	{
		document.getElementById("LoginDiv").style.display = "block";
		document.getElementById("RegisterDiv").style.display = "none";
	}
	function ShowRegister()
	{
		document.getElementById("LoginDiv").style.display = "none";
		document.getElementById("RegisterDiv").style.display = "block";
	}
</script>
<h2>Login or register new user</h2>
<hr />
<div class="InnerDiv" style="text-align:left;" onload="HideLoginRegister();">
	<span id="AlreadySignedInP" runat="server">	
		<b>Already signed in</b>
		<br />
		<label style="font-size:80%;">You are currently logged in as <asp:Label ID="UserFirstNameLabel" runat="server"></asp:Label>.</label><button ID="ContinueButton" runat="server" style="margin-left:10px;" onserverclick="ContinueButton_Click" causesvalidation="false">Continue</button> or
	</span><input type="button" causesvalidation="false" onclick="ShowLogin();" value="Login" /> or&nbsp;<input type="button" causesvalidation="false" onclick="ShowRegister();" value="Register" />
	
	<div id="LoginDiv">
		<p class="Divider">	
			<br />
			<b>Login</b>
			<br />
			<label class="loginLabel">Email address</label>
			<asp:TextBox class="loginTextbox" id="LoginEmailTextBox" runat="server" MaxLength="100"></asp:TextBox>
			<asp:RequiredFieldValidator ID="LoginEmailRequiredFieldValidator" runat="server" ControlToValidate="LoginEmailTextBox" ErrorMessage="Enter email address" Display="None" ValidationGroup="LoginGroup"></asp:RequiredFieldValidator>
			<br />
			<label class="loginLabel" for="password">Password</label>
			<asp:TextBox class="loginTextbox" id="LoginPasswordTextBox" MaxLength="20" runat="server" TextMode="Password"></asp:TextBox>
			<asp:RequiredFieldValidator ID="LoginPasswordRequiredFieldValidator" runat="server" ControlToValidate="LoginPasswordTextBox" ErrorMessage="Enter password" Display="None" ValidationGroup="LoginGroup"></asp:RequiredFieldValidator>
			&nbsp;
			<input type="button" class="proceed" ID="LoginButton" validationgroup="LoginGroup" runat="server" onserverclick="LoginButton_Click" value="Login" />
			<br />
			<small>If you have a DontStayIn account, use those details to login. Enter your DontStayIn nickname or email address and your password.</small>
			<asp:CustomValidator ID="LoginCustomValidator" runat="server" Display="None" ValidationGroup="LoginGroup"></asp:CustomValidator>
			<asp:ValidationSummary ID="LoginValidationSummary" runat="server" ValidationGroup="LoginGroup" CssClass="ValidationSummaryDiv" DisplayMode="BulletList" />		
		</p>
	</div>
	<div id="RegisterDiv">
		<p class="Divider" id="RegisterP">	
			<br />
			<b>Register free account</b>
			<br />
			<label class="loginLabel">First name</label>
			<asp:TextBox class="loginTextbox"  id="RegistrationFirstNameTextBox" runat="server" MaxLength="100" ></asp:TextBox>
			<asp:RequiredFieldValidator ID="RegistrationFirstNameRequiredFieldValidator" runat="server" ControlToValidate="RegistrationFirstNameTextBox" ErrorMessage="Enter first name" Display="None" ValidationGroup="RegistrationGroup"></asp:RequiredFieldValidator>
			<br />
			<label class="loginLabel">Last name</label>
			<asp:TextBox class="loginTextbox"  id="RegistrationLastNameTextBox" runat="server" MaxLength="100"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RegistrationLastNameRequiredFieldValidator" runat="server" ControlToValidate="RegistrationLastNameTextBox" ErrorMessage="Enter last name" Display="None" ValidationGroup="RegistrationGroup"></asp:RequiredFieldValidator>
			<br />
			<label class="loginLabel">Email address</label>
			<asp:TextBox class="loginTextbox"  id="RegistrationEmailTextBox" runat="server" MaxLength="100"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RegistrationEmailRequiredFieldValidator" runat="server" ControlToValidate="RegistrationEmailTextBox" ErrorMessage="Enter email address" Display="None" ValidationGroup="RegistrationGroup"></asp:RequiredFieldValidator>
			<asp:RegularExpressionValidator id="RegistrationEmailRegularExpressionValidator" Runat="server" ControlToValidate="RegistrationEmailTextBox" ErrorMessage="Invalid email" Display="None" ValidationGroup="RegistrationGroup"></asp:RegularExpressionValidator>
			<br />
			<label class="loginLabel">Password</label>
			<asp:TextBox class="loginTextbox"  TextMode="Password" id="RegistrationPasswordTextBox" runat="server" MaxLength="20" ></asp:TextBox>
			<asp:RequiredFieldValidator ID="RegistrationPasswordRequiredFieldValidator" runat="server" ControlToValidate="RegistrationPasswordTextBox" ErrorMessage="Enter password" Display="None" ValidationGroup="RegistrationGroup"></asp:RequiredFieldValidator>
			<br />
			<label class="loginLabel"><nobr>Confirm password</nobr></label>
			<asp:TextBox class="loginTextbox"  TextMode="Password" id="RegistrationConfirmPasswordTextBox" runat="server" MaxLength="20" ></asp:TextBox>
			<asp:RequiredFieldValidator ID="RegistrationConfirmPasswordRequiredFieldValidator" runat="server" ControlToValidate="RegistrationConfirmPasswordTextBox" ErrorMessage="Confirm password" Display="None" ValidationGroup="RegistrationGroup"></asp:RequiredFieldValidator>
			<asp:CompareValidator ID="RegistrationComparePasswordsValidator" runat="server" ControlToValidate="RegistrationPasswordTextBox" ControlToCompare="RegistrationConfirmPasswordTextBox" ErrorMessage="Passwords do not match" Display="None" ValidationGroup="RegistrationGroup"></asp:CompareValidator>
			<p>
				<div class="TermsBox" style="height:160px; width:400px; border:solid 1px #888888; overflow:auto;">
					<Spotted:Terms runat="server" UsePopups="true"></Spotted:Terms>
				</div>
			</p>
			<p>
				<asp:CheckBox Runat="server" ID="TermsCheckBox" Text="I have read and agree to be bound by the terms of use"></asp:CheckBox>
				<asp:CustomValidator Runat="server" Display="None" EnableClientScript="False" OnServerValidate="TermsVal" ErrorMessage="You must agree to the terms of use" ID="TermsCheckBoxCustomValidator" ValidationGroup="RegistrationGroup"/>
			</p>
			<input type="button" class="proceed" ID="RegistrationButton" runat="server" validationgroup="RegistrationGroup" onserverclick="RegistrationButton_Click" value="Register" />
			<asp:CustomValidator ID="RegistrationCustomValidator" runat="server" Display="None" ValidationGroup="RegistrationGroup"></asp:CustomValidator>
			<asp:ValidationSummary ID="RegistrationValidationSummary" runat="server" ValidationGroup="RegistrationGroup" CssClass="ValidationSummaryDiv" DisplayMode="BulletList" />
		</p>
	</div>
</div>
<asp:Label ID="JavascriptLabel" runat="server"></asp:Label>
