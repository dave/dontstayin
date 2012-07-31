<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="MixmagVotePage.aspx.cs" Inherits="Spotted.Master.MixmagVotePage" %>
<html>
	<head id="Head1" runat="server">
		<link rel="shortcut icon" href="/favicon-mix.ico" />
		<link rel="stylesheet" type="text/css" href="/misc/jquery/css/jquery-ui-1.8.1.custom.css"/>
		<%= Bobs.Storage.PathJavascriptFunction() %>
	</head>
	<!-- Welcome to DontStayIn -->
	<body topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" runat="server" id="BodyTag">
		<div id="fb-root"></div>
		<script>
			var DoneFbAsyncInit = false;
			window.fbAsyncInit = function () {
				FB.init({
					appId: '<%= Facebook.FacebookCommon.Common(Facebook.Apps.MixmagVote).AppId %>',
					status: true, // check login status
					cookie: true, // enable cookies to allow the server to access the session
					xfbml: true,  // parse XFBML
					channelURL: 'http://<%= Vars.DevEnv ? "dev0.dontstayin.com" : "mixmag-vote.com" %>/channel.html', // channel.html file
					oauth: true
				});
				DoneFbAsyncInit = true;
				try {
					FacebookReady();
				}
				catch (ex) { }
			};

			(function () {
				var e = document.createElement('script');
				e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
				e.async = true;
				document.getElementById('fb-root').appendChild(e);
			} ());
		</script>
		<form id="TemplateForm" method="post" runat="server">
			<asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="true" EnablePageMethods="true" />
			<script>
				PageMethods.set_path("/pagemethods/genericpage.aspx");
			</script>
			<asp:PlaceHolder runat="server" id="HeaderScriptPlaceHolder"></asp:PlaceHolder>
			<asp:PlaceHolder Runat="server" ID="MainContentPlaceHolder"/>
		</form>
	</body>
</html>
