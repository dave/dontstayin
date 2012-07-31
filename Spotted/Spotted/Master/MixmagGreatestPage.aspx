<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="MixmagGreatestPage.aspx.cs" Inherits="Spotted.Master.MixmagGreatestPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
	<head runat="server">
		<title>Who is the greatest dance act of all time? Vote now in the Mixmag poll.</title>
		<link rel="shortcut icon" href="/favicon-mix.ico" />
		<link rel="stylesheet" type="text/css" href="/misc/jquery/css/jquery-ui-1.8.1.custom.css"/>
		<%= Bobs.Storage.PathJavascriptFunction() %>
		<script src="http://platform.twitter.com/widgets.js" type="text/javascript"></script>
		<script src="/misc/json.js" type="text/javascript"></script>
	</head>
	<!-- Welcome to DontStayIn -->
	<style>
		body{
			font-family: Calibri, Arial, Helvetica, sans-serif;
		}
		
		@font-face {
			font-family: 'ProximaNovaExtrabold';
			src: url('/misc/fonts/proximanova-extrabld-webfont.eot');
			src: url('/misc/fonts/proximanova-extrabld-webfont.eot?#iefix') format('embedded-opentype'),
				 url('/misc/fonts/proximanova-extrabld-webfont.woff') format('woff'),
				 url('/misc/fonts/proximanova-extrabld-webfont.ttf') format('truetype'),
				 url('/misc/fonts/proximanova-extrabld-webfont.svg#ProximaNovaExtrabold') format('svg');
			font-weight: normal;
			font-style: normal;
		}
		
		@font-face {
			font-family: 'ProximaNovaBold';
			src: url('/misc/fonts/proximanova-bold-webfont.eot');
			src: url('/misc/fonts/proximanova-bold-webfont.eot?#iefix') format('embedded-opentype'),
				 url('/misc/fonts/proximanova-bold-webfont.woff') format('woff'),
				 url('/misc/fonts/proximanova-bold-webfont.ttf') format('truetype'),
				 url('/misc/fonts/proximanova-bold-webfont.svg#ProximaNovaBold') format('svg');
			font-weight: normal;
			font-style: normal;
		}
		
		.rounded-corners {
		  -moz-border-radius: 10px; /* Firefox */
		  -webkit-border-radius: 10px; /* Safari, Chrome */
		  border-radius: 10px; /* CSS3 */
		}

		.GreatestBox
		{
			/*margin:15px;
			padding-left:15px;
			padding-right:15px;*/
			border:2px solid #cccccc;
			background-color: #f8f8f8;
		}
		h1
		{
			font-family:'ProximaNovaExtrabold',Helvetica, Arial, sans-serif;
			font-size:30px;
			/*text-transform:uppercase;*/
			
		}
		h2
		{
			font-family:'ProximaNovaExtrabold',Helvetica, Arial, sans-serif;
			font-size:20px;
			/*text-transform:uppercase;*/
		}
		
		a.NoStyle:link, 
		a.NoStyle:visited   { text-decoration:none!important; color:#000000!important; background-color:transparent!important; }
		a.NoStyle:hover     { text-decoration:none!important; color:#000000!important; background-color:transparent!important; }
		
		
					
		a:link, 
		a:visited   { text-decoration:underline; color:#000000; background-color:transparent; }
		a:hover     { text-decoration:underline; color:#FFFFFF; background-color:#000000; }
		
		.ClearAfter:after 
		{
			display: block;
			height: 0;
			clear: both;
			visibility: hidden;
		}
		/* Hides from IE-mac \*/
		* html .ClearAfter {height: 1%;}
		/* End hide from IE-mac */
		
		div.Spacer
		{
			width:500px;
			height:12px;
			background-color:#000000;
			
			margin-top:15px;
			margin-bottom:15px;
		}
		div.SpacerDotted
		{
			width:500px;
			height:1px;
			
			border-top-width:1px;
			border-top-style:dashed;
			border-bottom-color:#000000;
			
			margin-top:15px;
			margin-bottom:15px;
		}
		p.Header
		{
			font-family:Arial Black;
			font-size:25px;
			font-weight:bold;
			margin-top:-10px;
			margin-bottom:-10px;
			padding-top:0px;
			padding-bottom:0px;
		}
		p.Text, div.Text
		{
			font-family:Arial, Sans-Serif;
			font-size:13px;
			font-weight:bold;
		}
		
		.TextQuote
		{
			font-family:Arial, Sans-Serif;
			font-size:18px;
			min-height:80px;
		}
		.fb_edge_widget_with_comment span.fb_edge_comment_widget iframe.fb_ltr { display: none !important; }
	</style>

	<body runat="server" id="BodyTag" topmargin="0" bottommargin="0" leftmargin="0" rightmargin="0" marginwidth="0" marginheight="0">
		<div id="fb-root"></div>
		<script>
			var DoneFbAsyncInit = false;
			window.fbAsyncInit = function () {
				FB.init({
					appId: '<%= Facebook.FacebookCommon.Common(Facebook.Apps.MixmagGreatest).AppId %>',
					status: true, // check login status
					cookie: true, // enable cookies to allow the server to access the session
					xfbml: true,  // parse XFBML
					channelURL: 'http://<%= Vars.DevEnv ? "dev0.dontstayin.com" : "mixmag-greatest.com" %>/channel.html', // channel.html file
					oauth: true

				});
				DoneFbAsyncInit = true;
				try {
					FacebookReady();
				}
				catch (ex) { }
				//FB.Canvas.setSize();
				FB.Canvas.setAutoGrow();
				FB.Canvas.scrollTo(0, 0);

//				FB.Event.subscribe('auth.login', function (response) {
//					if (navigator.userAgent.toLowerCase().indexOf("safari") > -1) {
//						if (response.session) {
//							if (document.getElementById("Content_DoneRefresh").value == "0") {
//								$('body').prepend("<form id='safariFix'></form>");
//								$('#safariFix')
//								.attr('method', 'POST')
//								.attr('action', location.href)
//								.append('<input type="hidden" name="session" id="safariFix_session" />')
//								.append('<input type="hidden" name="signed_request" id="safariFix_signedRequest" />');

//								$('#safariFix_session').attr('value', JSON.stringify(response.session));
//								$('#safariFix_signedRequest').attr('value', document.getElementById("Content_SignedRequest").value);

//								$('#safariFix').submit();
//							}
//						}
//					} else {
//						//window.location.reload(); // Whenever the user logs in, we refresh the page
//					}
//				});

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
			<div style="width:500px; background-color:#ffffff; margin:10px; clear:both;">
				<asp:PlaceHolder runat="server" id="HeaderScriptPlaceHolder"></asp:PlaceHolder>
				<asp:PlaceHolder Runat="server" ID="MainContentPlaceHolder"/>
			</div>
			
		</form>
	</body>
</html>
