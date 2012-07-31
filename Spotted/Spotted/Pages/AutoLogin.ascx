<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoLogin.ascx.cs" Inherits="Spotted.Pages.AutoLogin" %>
<asp:HiddenField runat="server" ID="AutoLogin_Value" Value="true" />
<asp:HiddenField runat="server" ID="AutoLogin_RedirectUrl" />
<asp:HiddenField runat="server" ID="AutoLogin_UsrK" />
<asp:HiddenField runat="server" ID="AutoLogin_String" />
<asp:HiddenField runat="server" ID="AutoLogin_LogOutFirst" />
<asp:HiddenField runat="server" ID="AutoLogin_UsrEmail" />
<asp:HiddenField runat="server" ID="AutoLogin_UsrIsSkeleton" />
<asp:HiddenField runat="server" ID="AutoLogin_UsrIsEnhancedSecurity" />
<asp:HiddenField runat="server" ID="AutoLogin_UsrIsFacebookNotConfirmed" />
<asp:HiddenField runat="server" ID="AutoLogin_UsrNeedsCaptcha" />
<asp:HiddenField runat="server" ID="AutoLogin_UsrCaptchaEncrypted" />

<asp:HiddenField runat="server" ID="AutoLogin_HomePlaceName" />
<asp:HiddenField runat="server" ID="AutoLogin_HomeCountryName" />
<asp:HiddenField runat="server" ID="AutoLogin_HomePlaceK" />
<asp:HiddenField runat="server" ID="AutoLogin_HomeCountryK" />
<asp:HiddenField runat="server" ID="AutoLogin_HomeGoodMatch" />
<asp:HiddenField runat="server" ID="AutoLogin_FavouriteMusicK" />
<asp:HiddenField runat="server" ID="AutoLogin_SendSpottedEmails" />
<asp:HiddenField runat="server" ID="AutoLogin_SendEflyers" />

<dsi:h1 runat="server">
	Login...
</dsi:h1>
<div class="ContentBorder">
	<p>
		You'll now be logged in to Don't Stay In.
	</p>
</div>
