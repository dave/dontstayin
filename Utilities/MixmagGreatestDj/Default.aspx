<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MixmagGreatest._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Mixmag greatest DJ of all time</title>
</head>
<body style="background-color:#b5b5b5;">
	<style>
			body{
				font-family: Calibri, Arial, Helvetica, sans-serif;
			}
			a.NoHilight:hover
			{
				text-decoration:none!important;
			}
	</style>
	<script src="/misc/jquery-1.2.6.min.js" type="text/javascript"></script>
	<Xscript src="http://static.ak.connect.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php/en_US" type="text/javascript"></script>
	<script>
		var FacebookReadyFunctions = new Array();
	</script>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="Script" runat="server" EnableHistory="true" EnablePageMethods="true" />

		<div style="position:relative; background-color:#ffffff; margin:25px 20px 30px 20px; padding:20px; width:665px;">
		
			<a href="/<%= Request["canvas"]==null ? "" : "default.aspx?canvas=1" %>" class="NoHilight"><img src="/gfx/mixmag-logo-300.png" width="300" height="67" style="margin-top:10px; margin-left:5px;" border="0" /> <span style="position:relative; bottom:24px; left:-10px; font-weight:bold; font-size:20px;">greatest DJ of all time</span></a>
			
			<span id="LabelSpan" style="xdisplay:none; top:10px; right:10px; position:absolute;">Loading...</span>
			
			<style>
					.GreatestBox
					{
						margin:15px;
						padding-left:15px;
						padding-right:15px;
						border:2px solid #cccccc;
						background-color: #f8f8f8;
					}
					h2
					{
						font-size:30px;
					}
			</style>

			<fb:like href="http://www.facebook.com/MixmagMagazine" width="600"></fb:like>

			<fb:visible-to-connection>
				Welcome, fans!
				<fb:else>You're not a fan.</fb:else>
			
			</fb:visible-to-connection>

			<div class="GreatestBox" id="PanelDjList" runat="server">
				<p style="display:none;<%= Request["canvas"]==null ? "" : "display:none;" %>">
					[Your Facebook name: <fb:name uid="loggedinuser" useyou="false" linked="true" class=""></fb:name>.
					If this isn't you, you can <a href="/" onclick="FacebookLogoutClick();return false;">log out</a>]
				</p>
				<p>
					Click below to vote for the greatest DJ of all time:
				</p>
				<asp:PlaceHolder runat="server" ID="DjsPh" />
			</div>

			

			<div class="GreatestBox" id="PanelVoteConnect" runat="server">
				<p>
					To vote for [DJ NAME], click the "connect" button below:
				</p>
				<p>
					[CANCEL BUTTON] [CONNECT BUTTON]
				</p>
			</div>

			<div class="GreatestBox" id="PanelThanks" runat="server">
				<p style="<%= Request["canvas"]==null ? "" : "display:none;" %>">
					Your Facebook name: <fb:name uid="loggedinuser" useyou="false" linked="true" class=""></fb:name>.
					If this isn't you, you can <a href="/" onclick="FacebookLogoutClick();return false;">log out</a>
				</p>
				<p>
					Thanks for your vote. You voted for [DJ NAME].
				</p>
			</div>

			<div class="GreatestBox" id="PanelVoted" runat="server">
				<p style="<%= Request["canvas"]==null ? "" : "display:none;" %>">
					Your Facebook name: <fb:name uid="loggedinuser" useyou="false" linked="true" class=""></fb:name>.
					If this isn't you, you can <a href="/" onclick="FacebookLogoutClick();return false;">log out</a>
				</p>
				<p>
					<!-- Already logged in, already voted -->
					You voted for [DJ NAME].
				</p>
			</div>

			<div class="GreatestBox" id="PanelErrorAlreadyVoted" runat="server">
				<p style="<%= Request["canvas"]==null ? "" : "display:none;" %>">
					Your Facebook name: <fb:name uid="loggedinuser" useyou="false" linked="true" class=""></fb:name>.
					If this isn't you, you can <a href="/" onclick="FacebookLogoutClick();return false;">log out</a>
				</p>
				<p>
					<!-- Try to vote twice -->
					Sorry, you can't vote twice. You already voted for [DJ NAME].
				</p>
			</div>




			<div style="margin-left:15px; margin-right:15px;">
				
				<p xstyle="display:none;">
					<textarea id="debug" cols="40" rows="10"></textarea>
				</p>
			</div>
		</div>
		<asp:HiddenField runat="server" ID="RequestCode" />
		<script type="text/javascript">
			PageMethods.set_path("/default.aspx");


			function FacebookReady() {
				//alert("test");
				//var session = FB.Facebook.apiClient.get_session();



				FB.api('/me', function (response) {

					debug(response.uid);
					//alert(response.name);
				});

				FB.Event.subscribe('auth.sessionChange', function (response) {
					// do something with response.session
				});
				
			}

			function ValidateEmail(address)
			{
				var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
				return reg.test(address);
			}
			
			
			
			function ShowLabel(text)
			{
				document.getElementById("LabelSpan").innerHTML = text;
				document.getElementById("LabelSpan").style.display = "";
			}
			function HideLabel()
			{
				document.getElementById("LabelSpan").style.display = "none";
			}
			
			function debug(txt)
			{
				document.getElementById("debug").value = txt + "\n" + document.getElementById("debug").value;
			}
			
			
		
		</script>
		
		
    </form>
	<div id="fb-root"></div>
    <script type="text/javascript">

    	var firedReadyFunction = false;
    	var skippedCallingReadyFunction = false;

    	FacebookReadyFunctions[FacebookReadyFunctions.length] = function()
    	{
			FacebookReady();
    		//FacebookLoginButtonSetState();
    	}


    	window.fbAsyncInit = function () {

    		FB.init({ appId: '<% = Common.Properties.IsDevelopmentEnvironment ? "79645061c9a11a654ac596182c42b489" : "726d8d3a2694bbf1fd9f90ec3d067d89" %>', status: true, cookie: true, xfbml: true });


			if (firedReadyFunction) {
   				CallFacebookReadyFunctions();
   			}
   			else {
   				skippedCallingReadyFunction = true;
   			}


//    		FB.XFBML.Host.get_areElementsReady().waitUntilReady(function () { alert("waitUntilReady"); });
//    		
//			alert("2")

//    		FB.XFBML.Host.get_areElementsReady().waitUntilReady(function () {
//    			alert("waitUntilReady");
//    			if (firedReadyFunction) {
//    				CallFacebookReadyFunctions();
//    			}
//    			else {
//    				skippedCallingReadyFunction = true;
//    			}
//    		});
    	};

		(
			function () {
				var e = document.createElement('script'); 
				e.async = true;
				e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
				document.getElementById('fb-root').appendChild(e);
			}()
		);

    	
    	
//    	FB_RequireFeatures(["XFBML"], function()
//    	{
//    		FB.Facebook.init(
//				"<% = Common.Properties.IsDevelopmentEnvironment ? "79645061c9a11a654ac596182c42b489" : "726d8d3a2694bbf1fd9f90ec3d067d89" %>", 
//				"/xd_receiver.html");

//			
//    		FB.XFBML.Host.get_areElementsReady().waitUntilReady(function()
//    		{
//    			if (firedReadyFunction)
//    			{
//    				CallFacebookReadyFunctions();
//    			}
//    			else
//    				skippedCallingReadyFunction = true;
//    		});
//    	});
		$(document).ready(function()
		{
			firedReadyFunction = true;
			if (skippedCallingReadyFunction)
			{
				CallFacebookReadyFunctions();
			}
		});
		function CallFacebookReadyFunctions()
    	{
    		for (i = 0; i < FacebookReadyFunctions.length; i++)
    		{
    			FacebookReadyFunctions[i].call();
    		}
    	}
	</script>
	<style>
		
		a:link, 
		a:visited   { color:#0000aa; text-decoration:none; }
		a:hover     { color:#2fa600; text-decoration:underline;}
		
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
	</style>
</body>
</html>
