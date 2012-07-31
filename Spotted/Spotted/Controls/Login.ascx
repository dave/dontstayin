<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="Spotted.Controls.Login" %>

<style>
	/*.BigButton { height:30px; padding-left:10px; padding-right:10px; padding: 0 .3em; overflow: visible; }
	.SmallButton { height:20px; padding-left:5px; padding-right:5px; padding: 0 .3em; overflow: visible; }*/
	.BigButton { height:30px; padding-left:10px; padding-right:10px; overflow: visible; margin-right:10px; }
	.SmallButton { height:20px; padding-left:5px; padding-right:5px; overflow: visible; margin-right:10px; }
	.LoginPanel { height: 220px; width:480px; padding-bottom:6px; margin-top:7px; }
	.LoginPanelInner { height:195px; }
	.LoginPanelTitle { font-size:20px; }
	.ColumnWithNoParaAbove { margin-top:-7px; }
	.ui-corner-all { margin-left:0px; margin-left:0px; }
</style>

<div runat="server" id="ConnectDialog" style="display:none; height: 206px; width:480px;" title="Connect..." enableviewstate="false">
<div runat="server" id="Connect_Inner" visible="false">
	<div runat="server" id="Connect_Note" visible="false">
		<!--

		******************
		NOTE TO DEVELOPERS
		HTML insise this div is ALL DUPLICATED in the controller javascript file. 
		If you update anything here, make sure you update it in the controller too.
		******************

		-->
	</div>

	<div runat="server" id="Connect_LoadingLabel"></div>

	<div runat="server" id="Connect_LoadingPanel">
		<button runat="server" id="Connect_Loading_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_ConnectingPanel">
		<button runat="server" id="Connect_Connecting_PopupRetry"></button>
		<button runat="server" id="Connect_Connecting_BackButton"></button>
		<button runat="server" id="Connect_Connecting_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_ErrorPanel">
		<p runat="server" id="Connect_Error_ErrorDescription"></p>
		<button runat="server" id="Connect_Error_TryAgainButton"></button>
		<button runat="server" id="Connect_Error_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_LoggedOutPanel">
		<button runat="server" id="Connect_LoggedOut_ConnectButton"></button>
		<button runat="server" id="Connect_LoggedOut_CancelButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebookButton"></button>
	</div>

	<div runat="server" id="Connect_LoggedOut_NoFacebook_ChoosePanel">
		<button runat="server" id="Connect_LoggedOut_NoFacebook_Choose_LoginButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_Choose_SignupButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_Choose_BackButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_Choose_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_LoggedOut_NoFacebook_LoginPanel">
		<input runat="server" id="Connect_LoggedOut_NoFacebook_Login_UsernameTextbox" type="text" />
		<input runat="server" id="Connect_LoggedOut_NoFacebook_Login_PasswordTextbox" type="password" />
		<p runat="server" id="Connect_LoggedOut_NoFacebook_Login_Error"></p>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_Login_LoginButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_Login_NoPasswordButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_Login_BackButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_Login_CancelButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton"></button>
	</div>

	<div runat="server" id="Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel">
		<button runat="server" id="Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_LoggedOut_NoFacebook_PasswordResetPanel">
		<p runat="server" id="Connect_LoggedOut_NoFacebook_PasswordReset_Title"></p>
		<input runat="server" id="Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox" type="text" />
		<button runat="server" id="Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton"></button>
		<span runat="server" id="Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel"></span>
		<span runat="server" id="Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel"></span>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_PasswordReset_BackButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_LoggedOut_NoFacebook_SignUp1Panel">
		<p runat="server" id="Connect_LoggedOut_NoFacebook_SignUp1_EmailPara">
			<input runat="server" id="Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox" type="text" />
		</p>
		<input runat="server" id="Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox" type="password" />
		<input runat="server" id="Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox" type="password" />
		<button runat="server" id="Connect_LoggedOut_NoFacebook_SignUp1_SaveButton"></button>
		<span runat="server" id="Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel" />
		<button runat="server" id="Connect_LoggedOut_NoFacebook_SignUp1_BackButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_SignUp1_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2Panel">
		<input runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox" type="text" />
		<input runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox" type="text" />
		<input runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox" type="text" />
		<select runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown" />
		<select runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown" />
		<select runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown" />
		<input runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio" type="radio" />
		<input runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio" type="radio" />
		<button runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_SaveButton"></button>
		<span runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel" />
		<button runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_BackButton"></button>
		<button runat="server" id="Connect_LoggedOut_NoFacebook_SignUp2_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_NewAccount_ConfirmFacebookPanel">
		<img runat="server" id="Connect_NewAccount_ConfirmFacebook_Image" />
		<a runat="server" id="Connect_NewAccount_ConfirmFacebook_Link"></a>
		<button runat="server" id="Connect_NewAccount_ConfirmFacebook_YesButton"></button>
		<button runat="server" id="Connect_NewAccount_ConfirmFacebook_NoButton"></button>
		<button runat="server" id="Connect_NewAccount_ConfirmFacebook_BackButton"></button>
		<button runat="server" id="Connect_NewAccount_ConfirmFacebook_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_NewAccount_NoEmailMatchPanel">
		<button runat="server" id="Connect_NewAccount_NoEmailMatch_NewAccountButton"></button>
		<button runat="server" id="Connect_NewAccount_NoEmailMatch_ChooseAccountButton"></button>
		<button runat="server" id="Connect_NewAccount_NoEmailMatch_CancelButton"></button>
		<button runat="server" id="Connect_NewAccount_NoEmailMatch_FacebookLogoutButton"></button>
		<button runat="server" id="Connect_NewAccount_NoEmailMatch_BackButton"></button>
	</div>

	<div runat="server" id="Connect_NewAccount_EmailMatchPanel">
		<span runat="server" id="Connect_NewAccount_EmailMatch_UserLink1"></span>
		<button runat="server" id="Connect_NewAccount_EmailMatch_AutoConnectButton"></button>
		<button runat="server" id="Connect_NewAccount_EmailMatch_ChooseAccountButton"></button>
		<button runat="server" id="Connect_NewAccount_EmailMatch_CancelButton"></button>
		<button runat="server" id="Connect_NewAccount_EmailMatch_FacebookLogoutButton"></button>
		<button runat="server" id="Connect_NewAccount_EmailMatch_BackButton"></button>
	</div>

	<div runat="server" id="Connect_NewAccount_ChooseAccountPanel">
		<input runat="server" id="Connect_NewAccount_ChooseAccount_UsernameTextbox" />
		<input runat="server" id="Connect_NewAccount_ChooseAccount_PasswordTextbox" type="password" />
		<button runat="server" id="Connect_NewAccount_ChooseAccount_LinkAccountButton"></button>
		<span runat="server" id="Connect_NewAccount_ChooseAccount_ErrorLabel"></span>
		<button runat="server" id="Connect_NewAccount_ChooseAccount_CancelButton"></button>
		<button runat="server" id="Connect_NewAccount_ChooseAccount_FacebookLogoutButton"></button>
		<button runat="server" id="Connect_NewAccount_ChooseAccount_BackButton"></button>
		<button runat="server" id="Connect_NewAccount_ChooseAccount_ForgottonPasswordButton"></button>
	</div>

	<div runat="server" id="Connect_NewAccount_ForgotPasswordPanel">
		<input runat="server" id="Connect_NewAccount_ForgotPassword_UsernameTextbox" type="text" />
		<button runat="server" id="Connect_NewAccount_ForgotPassword_SendLinkButton"></button>
		<span runat="server" id="Connect_NewAccount_ForgotPassword_MessageLabel"></span>
		<span runat="server" id="Connect_NewAccount_ForgotPassword_ErrorLabel"></span>
		<button runat="server" id="Connect_NewAccount_ForgotPassword_CancelButton"></button>
		<button runat="server" id="Connect_NewAccount_ForgotPassword_FacebookLogoutButton"></button>
		<button runat="server" id="Connect_NewAccount_ForgotPassword_BackButton"></button>
	</div>

	<div runat="server" id="Connect_LoggedInPanel">
		<span runat="server" id="Connect_LoggedIn_LoggedInUsrLink"></span>
		<button runat="server" id="Connect_LoggedIn_CloseButton"></button>
		<button runat="server" id="Connect_LoggedIn_LogoutButton"></button>
		<p runat="server" id="Connect_LoggedIn_DisconnectLinkOuter">
			<a runat="server" id="Connect_LoggedIn_DisconnectButtonShowLink"></a>
		</p>
		<p runat="server" id="Connect_LoggedIn_DisconnectButtonOuter">
			<button runat="server" id="Connect_LoggedIn_DisconnectButton"></button>
		</p>
		<button runat="server" id="Connect_LoggedIn_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_AutoLoginMismatchPanel">
		<span runat="server" id="Connect_AutoLoginMismatch_AutoLoginUsrLink"></span>
		<button runat="server" id="Connect_AutoLoginMismatch_RetryButton"></button>
		<button runat="server" id="Connect_AutoLoginMismatch_ContinueButton"></button>
		<p runat="server" id="Connect_AutoLoginMismatch_SwitchAccountsPara">
			<span runat="server" id="Connect_AutoLoginMismatch_AutoLoginUsrLink2"></span>
			<a runat="server" id="Connect_AutoLoginMismatch_SwitchAccountsShowLink"></a>
		</p>
		<p runat="server" id="Connect_AutoLoginMismatch_SwitchAccountsOuter">
			<button runat="server" id="Connect_AutoLoginMismatch_SwitchButton"></button>
		</p>
		<button runat="server" id="Connect_AutoLoginMismatch_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_CreatePasswordPanel">
		<input runat="server" id="Connect_CreatePassword_Password1Textbox" type="password" />
		<input runat="server" id="Connect_CreatePassword_Password2Textbox" type="password" />
		<button runat="server" id="Connect_CreatePassword_DisconnectButton"></button>
		<span runat="server" id="Connect_CreatePassword_ErrorSpan"></span>
		<button runat="server" id="Connect_CreatePassword_CancelButton"></button>
		<button runat="server" id="Connect_CreatePassword_BackButton"></button>
	</div>

	<div runat="server" id="Connect_LikeButtonPanel">
		<button runat="server" id="Connect_LikeButton_CancelButton"></button>
	</div>

	<div runat="server" id="Connect_DetailsPanel">
		<select runat="server" id="Connect_Details_MusicDropDown"></select>
		<select runat="server" id="Connect_Details_CountryDropDown"></select>
		<select runat="server" id="Connect_Details_PlaceDropDown"></select>
		<span runat="server" id="Connect_Details_PlaceDefaultOuterSpan">
			<span runat="server" id="Connect_Details_PlaceDefaultSpan"></span>
			<a runat="server" id="Connect_Details_PlaceChangeLink"></a>
		</span>
		<p runat="server" id="Connect_Details_FacebookInfoPanel"></p>
		<p runat="server" id="Connect_Details_WeeklyEmailInfoPanel"></p>
		<p runat="server" id="Connect_Details_PartyInvitesInfoPanel"></p>
		<input runat="server" id="Connect_Details_FacebookCheck" type="checkbox" />
		<label runat="server" id="Connect_Details_FacebookCheckLabel"></label>
		<a runat="server" id="Connect_Details_FacebookInfoAnchor"></a>
		<input runat="server" id="Connect_Details_WeeklyEmailCheck" type="checkbox" />
		<label runat="server" id="Connect_Details_WeeklyEmailCheckLabel"></label>
		<a runat="server" id="Connect_Details_WeeklyEmailInfoAnchor"></a>
		<input runat="server" id="Connect_Details_PartyInvitesCheck" type="checkbox" />
		<label runat="server" id="Connect_Details_PartyInvitesCheckLabel"></label>
		<a runat="server" id="Connect_Details_PartyInvitesInfoAnchor"></a>
		<button runat="server" id="Connect_Details_CancelButton"></button>
		<button runat="server" id="Connect_Details_BackButton"></button>
		<button runat="server" id="Connect_Details_SaveButton"></button>
		<span runat="server" id="Connect_Details_PlaceErrorSpan"></span>
	</div>

	<div runat="server" id="Connect_CaptchaPanel">
		<img runat="server" id="Connect_Captcha_Img" />
		<input runat="server" id="Connect_Captcha_Textbox" />
		<button runat="server" id="Connect_Captcha_SaveButton"></button>
		<p runat="server" id="Connect_Captcha_Error"></p>
		<button runat="server" id="Connect_Captcha_BackButton"></button>
		<button runat="server" id="Connect_Captcha_CancelButton"></button>
	</div>

	<p runat="server" id="Connect_DebugPanel">
		<textarea runat="server" id="Connect_Debug_Output"></textarea>
		<button runat="server" id="Connect_Debug_LogoutButton"></button>
		<button runat="server" id="Connect_Debug_DisconnectButton"></button>
		<button runat="server" id="Connect_Debug_AuthButton"></button>
	</p>
	
</div>
</div>
