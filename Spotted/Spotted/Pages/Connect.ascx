<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Connect.ascx.cs" Inherits="Spotted.Pages.Connect" %>




<dsi:h1 runat="server">Title</dsi:h1>
<div class="ContentBorder">
	<p>
		<a href="/pages/connect">Reset</a>
	</p>
	
	<div id="FacebookLoggedInP">
		<p>
			Your facebook name: <fb:name uid="loggedinuser" useyou="false" linked="true" class=""></fb:profile-pic>
		</p>
		<p>
			<fb:profile-pic uid="loggedinuser" size="square" facebook-logo="true"></fb:profile-pic>
		</p>
		<p>
			<a href="/" onclick="FacebookLogoutClick();return false;">Log out</a>
		</p>
	</div>
	<p id="FacebookLoginButtonP" style="display:none;">
		<fb:login-button onlogin="FacebookLoginButtonSetState();"></fb:login-button>
	</p>
	
	<script>
		function FacebookLoginButtonSetState()
		{
			var status = FB.Connect.get_status == FB.ConnectState;
			var api = FB.Facebook.apiClient;
			var session = api.get_session();
			document.getElementById("FacebookLoginButtonP").style.display = session == null ? "" : "none";
			document.getElementById("FacebookLoggedInP").style.display = session == null ? "none" : "";
		}
		function FacebookLogoutClick()
		{
			FB.Connect.logout(function() { FacebookLoginButtonSetState(); });
		}
		FacebookReadyFunctions[FacebookReadyFunctions.length] = function()
		{
			FacebookLoginButtonSetState();
		}
	
	</script>
	
</div>
