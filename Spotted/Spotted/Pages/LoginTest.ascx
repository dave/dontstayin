<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginTest.ascx.cs" Inherits="Spotted.Pages.LoginTest" %>
<script>
	jQuery(document).ready(function () {
		if (!IsLoggedIn())
			LogInTransfer("<%= Request.QueryString["url"] == "" ? "/" : Request.QueryString["url"] %>");
	});
</script>
<dsi:h1 runat="server">Log in</dsi:h1>
<div class="ContentBorder">
	<p runat="server" id="ErrorP">
		Looks like you have to be logged in to use this page. 
	</p>
	<script>
		function loginButtonClick()
		{
		
			if (IsLoggedIn())
			{
				LogOutAndDoAction(function(){tryToLoginNow();});
			}
			else
			{
				tryToLoginNow();
			}
		}
		function tryToLoginNow() {
			LogInTransfer("<%= Request.QueryString["url"] == "" ? "/" : Request.QueryString["url"] %>");
		}
	</script>
	<p>
		<a href="/" onclick="loginButtonClick();return false;">Click here to log in</a>, or you can <a href="/" onclick="history.go(-1);return false;">go back to the previous page</a>.
	</p>
</div>
