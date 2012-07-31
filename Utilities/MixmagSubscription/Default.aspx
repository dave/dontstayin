<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MixmagSubscription._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Mixmag online</title>
</head>
<body style="background-color:#b5b5b5;">
	<style>
			body{
				font-family: Calibri, Arial, Helvetica, sans-serif;
			}
	</style>
	<script src="/misc/jquery-1.2.6.min.js" type="text/javascript"></script>
	<script src="http://static.ak.connect.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php/en_US" type="text/javascript"></script>
	<script>
		var FacebookReadyFunctions = new Array();
	</script>
    <form id="form1" runat="server">
		<asp:ScriptManager ID="Script" runat="server" EnableHistory="true" EnablePageMethods="true" />

		<div style="position:relative; background-color:#ffffff; margin:25px 20px 30px 20px; padding:20px; width:665px;">
		
			<a href="/<%= Request["canvas"]==null ? "" : "default.aspx?canvas=1" %>"><img src="/gfx/mixmag-logo-300.png" width="300" height="67" style="margin-top:10px; margin-left:5px;" border="0" /></a>
			
			<span id="LabelSpan" style="xdisplay:none; top:10px; right:10px; position:absolute;">Loading...</span>
			
			<div style="margin-left:15px; margin-right:15px;">
				<div id="FacebookLoggedInP" style="display:none;">
					<p>
						<input type="checkbox" id="FacebookCheckboxSubscribe" onclick="FacebookCheckboxSubscribeClick();" />
						<label for="FacebookCheckboxSubscribe">Send me my FREE copy of Mixmag each month</label>
					</p>
					<p>
						<input type="checkbox" id="FacebookCheckboxStream" onclick="FacebookCheckboxStreamClick();" />
						<label for="FacebookCheckboxStream">Post a message to my Facebook wall when I read each issue</label>
					</p>
					
					<p style="<%= Request["canvas"]==null ? "" : "display:none;" %>">
						Your Facebook name: <fb:name uid="loggedinuser" useyou="false" linked="true" class=""></fb:name>.
						
						If this isn't you, you can <a href="/" onclick="FacebookLogoutClick();return false;">log out</a>
					</p>
					
					<p id="EmailAddressPanel" style="display:none;">
						Your email: <span ID="EmailSpan"></span>. If this isn't correct, you can <a href="/" onclick="document.getElementById('ChangeEmailAddressPanel').style.display='';return false;">change it</a>
					</p>
					
					<div style="margin:10px; padding-left:15px; padding-right:15px; border:1px solid #666666; background-color:#eeeeee; display:none;" id="ChangeEmailAddressPanel">
						<p style="position:relative;">
							Enter your new email: <asp:TextBox runat="server" ID="ChangeEmailTextBox" style="position:absolute; left:180px; width:220px;"></asp:TextBox>
						</p>
						<p id="ChangeEmailFacebook" style="padding-left:180px;">
							Tip: we got this email address from your Facebook account. <span id="ChangeEmailFacebookProxy">It's an 
							anonymous email address, which means it'll get redirected to your main Facebook email address.</span> If you'd 
							prefer to have your updates delivered to a different address, you can update it above.
						</p>
						<p id="ChangeEmailManual" style="padding-left:180px;">
							Tip: you entered this email address manually. We can update this address with the email from your Facebook 
							account. <a href="/" onclick="UpdateEmailFromFacebook();return false;">Update from Facebook</a>.
						</p>
						<p>
							<button onclick="ChangeEmail();return false;">Save</button>
						</p>
					</div>
					
					<p style="display:none; position:absolute; bottom:-50px; left:10px;">
						<a href="/" onclick="FacebookDisconnectClick();return false;">Disconnect</a>
					</p>
					<p style="display:none;">
						<a href="/" onclick="FacebookLoginButtonSetState();return false;">Refresh</a>
					</p>
					
					<div runat="server" id="AddressHolder" style="display:none;">
						<h2>One more thing...</h2>
						<p>
							We need your name and address to include your free copy in our audited circulation figures.
							Don't worry, we'll never send you anything or share your details with anyone.
						</p>
						<div id="AddressEmailHolder">
							<p style="position:relative;">
								Email address: <asp:TextBox runat="server" ID="EmailTextBox" style="position:absolute; left:180px; width:220px;" />
							</p>
						</div>
						<p style="position:relative;">
							First name: <asp:TextBox runat="server" ID="FirstNameTextBox" style="position:absolute; left:180px; width:130px;" />
						</p>
						<p style="position:relative;">
							Last name: <asp:TextBox runat="server" ID="LastNameTextBox" style="position:absolute; left:180px; width:130px;" />
						</p>
						<p style="position:relative;">
							Address (first line): <asp:TextBox runat="server" ID="AddressFirstLineTextBox" style="position:absolute; left:180px; width:220px;" />
						</p>
						<p style="position:relative;">
							Postal code: <asp:TextBox runat="server" ID="PostalCodeTextBox" style="position:absolute; left:180px; width:220px;" />
						</p>
						<p style="position:relative;">
							Country: <asp:DropDownList runat="server" ID="CountryDropDownList" style="position:absolute; left:180px; width:224px;"><asp:ListItem Text="UK" Value="224" /></asp:DropDownList>
						</p>
						<p>
							<asp:Button runat="server" Text="Save" OnClientClick="SaveAddress();return false;" />
						</p>
					</div>
					
					<div runat="server" id="BrokenEmailHolder" style="display:none;">
						<h2>There's a problem with your email...</h2>
						<p>
							We sent you an email but it bounced. Maybe your email address has changed? You can update it below. 
							If it's the right address and you've fixed the problem, click the "try again" button.
						</p>
						<p style="position:relative;">
							Email address: <asp:TextBox runat="server" ID="EmailTextBox3" style="position:absolute; left:180px; width:220px;" /> 
						</p>
						<p style="padding-left:180px;" id="BrokenEmailManual">
							Tip: you entered this email address manually. We can update this address with the email from your Facebook 
							account. <a href="/" onclick="UpdateEmailFromFacebook();return false;">Update from Facebook</a>.
						</p>
						<p>
							<asp:Button ID="Button3" runat="server" Text="Change email" OnClientClick="SaveEmailBroken();return false;" /> <asp:Button ID="Button4" runat="server" Text="Try again with this email" OnClientClick="FixedBroken();return false;" />
						</p>
					</div>
					
					<div runat="server" id="VerifyEmailHolder" style="display:none;">
						<h2>One more thing...</h2>
						<p>
							We need to verify your email address. We've sent you an email with a link. As soon as you click the link you'll be able to access the magazine. 
							If you've entered the wrong email address, change it and click "Save". To send another verification link, click "Send link again".
						</p>
						<p style="position:relative;">
							Email address: <asp:TextBox runat="server" ID="EmailTextBox2" style="position:absolute; left:180px; width:220px;" />
						</p>
						<p style="padding-left:180px;">
							Tip: you entered this email address manually. We can update this address with the email from your Facebook 
							account. If you update your email from Facebook - it's already verified, so you can skip this step.
							<a href="/" onclick="UpdateEmailFromFacebook();return false;">Update from Facebook</a>.
						</p>
						<p>
							<asp:Button ID="Button1" runat="server" Text="Save" OnClientClick="SaveEmail();return false;" /> <asp:Button ID="Button2" runat="server" Text="Send link again" OnClientClick="SendLink();return false;" />
						</p>
					</div>
					
					<div runat="server" id="IssueHolder" style="display:none;">
						<div runat="server" id="SelectedIssueHolder" visible="false">
							<h2 runat="server" id="SelectedIssueHeader">Click below to load the magazine</h2>
							<asp:PlaceHolder runat="server" ID="SelectedIssuePh" />
						</div>
						
						<div runat="server" id="BackIssuesHolder" visible="false">
							<h2>Back issues</h2>
							<asp:PlaceHolder runat="server" ID="BackIssuesPh" />
						</div>
					</div>
					
				</div>
				<div id="FacebookLoginButtonP" style="display:none;">
					<p runat="server" id="LoginButtonIntroTextP">
						To get a FREE copy of Mixmag by email each month, click the button below:
					</p>
					<p>
						<a href="#" onclick="FacebookLogin(); return false;" class="fbconnect_login_button FBConnectButton FBConnectButton_Small">
							<span id="RES_ID_fb_login_text" class="FBConnectButton_Text">Connect with Facebook and add a bookmark</span>
						</a>
					</p>
				</div>
				<p style="display:none;">
					<textarea id="debug" cols="40" rows="10"></textarea>
				</p>
			</div>
		</div>
		<asp:HiddenField runat="server" ID="RequestCode" />
		<script type="text/javascript">
			PageMethods.set_path("/default.aspx");

			function ValidateEmail(address)
			{
				var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
				return reg.test(address);
			}
			
			function UpdateEmailFromFacebook()
			{
				ShowLabel("Updating...");
				var session = FB.Facebook.apiClient.get_session();

			//	FB.Facebook.apiClient.users_hasAppPermission("email",
			//		function(result) { if (result == 0 || result == null) FB.Connect.showPermissionDialog("email", function() { FacebookSaveStateFromPermissions(true, false, true); FacebookLoginButtonSetState(); }); else FacebookSaveState(true); }
			//	);
									

				PageMethods.RevokeEmailPermission(
						session.uid,
						session.session_key,
						session.secret,
						session.expires,
						session.base_domain,
						function(result)
						{
							debug("Success (SendLink): " + result); /*Success*/

							//updateForm(result);
							//alert(result);

							FB.Facebook.apiClient.users_hasAppPermission("email",
								function(result) { if (result == 0 || result == null) FB.Connect.showPermissionDialog("email", function() { FacebookSaveStateFromPermissions(true, false, true); FacebookLoginButtonSetState(); }); else FacebookSaveState(true); }
							);

						},
						function(result) { debug("Fail (SendLink):" + result.get_message()); HideLabel(); /*Fail*/ },
						null
						);

				
				
			}

			function FixedBroken()
			{
				ShowLabel("Sending...");
				var session = FB.Facebook.apiClient.get_session();
				PageMethods.SendLink(
						session.uid,
						session.session_key,
						session.secret,
						session.expires,
						session.base_domain,
						document.getElementById("RequestCode").value,
						function(result)
						{
							debug("Success (SendLink): " + result); /*Success*/
							HideLabel();

							updateForm(result);
							alert("We've send you an email to test this address. You should receive it shortly.");

						},
						function(result) { debug("Fail (SendLink):" + result.get_message()); HideLabel(); /*Fail*/ },
						null
						);
			}
			function SendLink()
			{
				ShowLabel("Sending...");
				var session = FB.Facebook.apiClient.get_session();
				PageMethods.SendLink(
						session.uid,
						session.session_key,
						session.secret,
						session.expires,
						session.base_domain,
						document.getElementById("RequestCode").value,
						function(result)
						{
							debug("Success (SendLink): " + result); /*Success*/
							HideLabel();

							updateForm(result);
							alert("We've sent you the link again. You should receive it shortly.");

						},
						function(result) { debug("Fail (SendLink):" + result.get_message()); HideLabel(); /*Fail*/ },
						null
						);
			}

			function SaveEmailBroken()
			{
				var email = document.getElementById("EmailTextBox3").value;

				SaveEmailGeneric(email);
			}
			
			function SaveEmail()
			{
				var email = document.getElementById("EmailTextBox2").value;

				SaveEmailGeneric(email);
			}

			function ChangeEmail()
			{
				var email = document.getElementById("ChangeEmailTextBox").value;

				SaveEmailGeneric(email);
			}

			function SaveEmailGeneric(email)
			{
				debug("SaveEmailGeneric - " + email);
				
				if (email.length > 0 && ValidateEmail(email))
				{
					ShowLabel("Saving...");
					var session = FB.Facebook.apiClient.get_session();
					PageMethods.SaveEmail(
						session.uid,
						session.session_key,
						session.secret,
						session.expires,
						session.base_domain,
						document.getElementById("RequestCode").value,
						email,
						function(result)
						{
							debug("Success (SaveEmail): " + result); /*Success*/
							HideLabel();

							updateForm(result);

						},
						function(result) { debug("Fail (SaveEmail):" + result.get_message()); HideLabel(); /*Fail*/ },
						null
						);
				}
				else
				{
					alert("Please check your email address.");
				}
			}

			function SaveAddress()
			{
				var firstName = document.getElementById("FirstNameTextBox").value;
				var lastName = document.getElementById("LastNameTextBox").value;
				
				var addressFirstLine = document.getElementById("AddressFirstLineTextBox").value;
				var postalCode = document.getElementById("PostalCodeTextBox").value;
				var country = document.getElementById("CountryDropDownList")[document.getElementById("CountryDropDownList").selectedIndex].value;
				
				var email = document.getElementById("EmailTextBox").value;
				var emailDisabled = document.getElementById("AddressEmailHolder").style.display == "none";

				if (firstName.length > 0 && lastName.length > 0 && addressFirstLine.length > 0 && (emailDisabled || (email.length > 0 && ValidateEmail(email))))
				{
					ShowLabel("Saving...");
					var session = FB.Facebook.apiClient.get_session();
					PageMethods.SaveAddress(
						session.uid,
						session.session_key,
						session.secret,
						session.expires,
						session.base_domain,
						document.getElementById("RequestCode").value,
						email,
						firstName,
						lastName,
						addressFirstLine,
						postalCode,
						country,
						function(result)
						{
							debug("Success (SaveAddress): " + result); /*Success*/
							HideLabel();

							updateForm(result);

						},
						function(result) { debug("Fail (SaveAddress):" + result.get_message()); HideLabel(); /*Fail*/ },
						null
						);
				}
				else
				{
					alert("Please complete your " + (emailDisabled ? "" : "email, ") + "name and address.");
				}
				
				
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
			function FacebookCheckboxSubscribeClick()
			{
				ShowLabel("Saving...");
				if (document.getElementById("FacebookCheckboxSubscribe").checked)
				{
					FB.Facebook.apiClient.users_hasAppPermission("email",
						function(result) { if (result == 0 || result == null) FB.Connect.showPermissionDialog("email", function() { FacebookSaveStateFromPermissions(true, false, false); FacebookLoginButtonSetState(); }); else FacebookSaveState(false); }
					);
				}
				else
				{
					FacebookSaveState(false);
				}
			}
			function FacebookCheckboxStreamClick()
			{
				ShowLabel("Saving...");
				if (document.getElementById("FacebookCheckboxStream").checked)
				{
					FB.Facebook.apiClient.users_hasAppPermission("publish_stream",
						function(result) { if (result == 0 || result == null) FB.Connect.showPermissionDialog("publish_stream", function() { FacebookSaveStateFromPermissions(false, true, false); FacebookLoginButtonSetState(); }); else FacebookSaveState(false); }
					);
				}
				else
				{
					FacebookSaveState(false);
				}
			}
			function FacebookLogin()
			{
				ShowLabel("Loading...");
				debug("FacebookLogin");

//				var session = FB.Facebook.apiClient.get_session();

//				FB.login(
//					function (response) {
//						if (response.session && response.perms) {

//							debug("FacebookLogin requireSession start");
//							var session = FB.Facebook.apiClient.get_session();

//							//debug("FacebookLogin uid=" + session.uid + ", session_key=" + session.session_key);

//							PageMethods.QueryDetails(
//								session.uid,
//								session.session_key,
//								session.secret,
//								session.expires,
//								session.base_domain,
//								function (result) {
//									/*Success*/
//									debug("Success (FacebookLogin):" + result);
//									if (result == "XXX" || (result.substring(0, 1) == "0" && result.substring(1, 2) == "0")) {
//										FB.Connect.showBookmarkDialog(function () { AfterPermissionDialog(); });
//									}
//									else {
//										updateForm(result);
//										HideLabel();
//									}
//								},
//								function (result) { /*Fail*/ debug("Fail (FacebookLogin):" + result.get_message()); HideLabel(); },
//								null
//							);

//							debug("FacebookLogin requireSession end");

//						}
//						else if (response.session) {
//							// no permissions
//							alert("Oops, you have to grant permissions to connect...");
//						}
//						else {
//							// no session
//							alert("Oops, you have to connect to facebook...");
//						}
//					},
//					{ perms: 'publish_stream,email' }
//				);
//				
//				debug("FacebookLogin done");


				FB.Connect.pollLoginStatus(2000, 100, onConnectStatus);
				FB.Connect.requireSession(
					function () {
						//onConnectStatus();
					}
				);
				debug("FacebookLogin done");
			}

			function onConnectStatus() {
				debug("FacebookLogin onConnectStatus start");
				var session = FB.Facebook.apiClient.get_session();

				PageMethods.QueryDetails(
					session.uid,
					session.session_key,
					session.secret,
					session.expires,
					session.base_domain,
					function (result) {
						debug("Success (FacebookLogin):" + result); /*Success*/
						if (result == "XXX" || (result.substring(0, 1) == "0" && result.substring(1, 2) == "0")) {
							FB.Connect.showPermissionDialog("email,publish_stream", function () {
								FB.Connect.showBookmarkDialog(function () { AfterPermissionDialog(); });
							});

						}
						else {
							updateForm(result);
							HideLabel();
						}
					},
					function (result) { debug("Fail (FacebookLogin):" + result.get_message()); HideLabel(); /*Fail*/ },
					null
				);

					debug("FacebookLogin onConnectStatus end");
			}

			function updateForm(result)
			{
				var subscribe = result.substring(0, 1) == "1";
				var stream = result.substring(1, 2) == "1";
				var address = result.substring(2, 3) == "1";
				var email = result.substring(3, 4) == "1";
				var verified = result.substring(4, 5) == "1";
				var broken = result.substring(5, 6) == "1";
				var emailFromFacebook = result.substring(6, 7) == "1";
				var emailAddress = result.substring(7);

				if (address && email && verified && !broken && document.getElementById("RequestCode").value.length > 0)
				{
					window.location.reload();
					return;
				}
				
				document.getElementById("FacebookLoginButtonP").style.display = "none";
				document.getElementById("FacebookLoggedInP").style.display = "";

				document.getElementById("FacebookCheckboxSubscribe").checked = subscribe;
				document.getElementById("FacebookCheckboxStream").checked = stream;

				document.getElementById("AddressHolder").style.display = address ? "none" : "";
				document.getElementById("AddressEmailHolder").style.display = email ? "none" : "";

				document.getElementById("BrokenEmailHolder").style.display = address && broken ? "" : "none";
				document.getElementById("VerifyEmailHolder").style.display = address && (!email || !verified) && !broken ? "" : "none";

				document.getElementById("IssueHolder").style.display = address && email && verified && !broken ? "" : "none";

				document.getElementById("EmailAddressPanel").style.display = email && verified && !broken ? "" : "none";
				if (!(email && verified && !broken))
					document.getElementById("ChangeEmailAddressPanel").style.display = "none";

				document.getElementById("EmailTextBox").value = emailAddress;
				document.getElementById("EmailTextBox2").value = emailAddress;
				document.getElementById("EmailTextBox3").value = emailAddress;
				document.getElementById("EmailSpan").innerHTML = emailAddress;
				document.getElementById("ChangeEmailTextBox").value = emailAddress;

				document.getElementById("ChangeEmailFacebook").style.display = emailAddress.length > 0 && emailFromFacebook ? "" : "none";
				document.getElementById("ChangeEmailFacebookProxy").style.display = emailAddress.indexOf("@proxymail.facebook.com") > -1 ? "" : "none";
				document.getElementById("ChangeEmailManual").style.display = emailAddress.length > 0 && !emailFromFacebook ? "" : "none";

				document.getElementById("BrokenEmailManual").style.display = emailFromFacebook ? "" : "none";
				

			}
			
			function AfterPermissionDialog()
			{
				FacebookSaveStateFromPermissions(true, true, false);
				FacebookLoginButtonSetState(); 
			}
//			var gatherResultsEmail = -1;
//			var gatherResultsPublish = -1;
//			function gatherResults(type, result)
//			{
//				if (type == "email")
//				{
//					gatherResultsEmail = (result == 0 || result == null) ? 0 : 1;
//				}
//				else if (type == "publish_stream")
//				{
//					gatherResultsPublish = (result == 0 || result == null) ? 0 : 1;
//				}

//				if (gatherResultsEmail > -1 && gatherResultsPublish > -1)
//				{
//					if (gatherResultsEmail == 0 || gatherResultsPublish == 0)
//					{
//						var permissions = gatherResultsEmail == 0 ? "email" : "";
//						permissions += (permissions == "" ? "" : ",") + (gatherResultsPublish == 0 ? "publish_stream" : "");
//						FB.Connect.showPermissionDialog(permissions, function() { FacebookSaveStateFromPermissions(gatherResultsEmail == 0, gatherResultsPublish == 0); FacebookLoginButtonSetState(); });
//					}
//					else
//					{
//						FacebookLoginButtonSetState();
//					}
//				}
//			}
			function debug(txt)
			{
				document.getElementById("debug").value = txt + "\n" + document.getElementById("debug").value;
			}
			function FacebookLoginButtonSetState()
			{
				debug("FacebookLoginButtonSetState");
				var session = FB.Facebook.apiClient.get_session();
				document.getElementById("FacebookLoginButtonP").style.display = session == null ? "" : "none";
				document.getElementById("FacebookLoggedInP").style.display = session == null ? "none" : "";

//				if (session != null)
//				{
//					FB.Facebook.apiClient.users_hasAppPermission("email",
//						function(result) { document.getElementById("FacebookCheckboxSubscribe").checked = !(result == 0 || result == null); }
//					);

//					FB.Facebook.apiClient.users_hasAppPermission("publish_stream",
//						function(result) { document.getElementById("FacebookCheckboxStream").checked = !(result == 0 || result == null); }
//					);
//				}
				if (session != null)
				{

					//debug("FacebookLoginButtonSetState uid=" + session.uid + ", session_key=" + session.session_key);

					PageMethods.QueryDetails(
						session.uid,
						session.session_key,
						session.secret,
						session.expires,
						session.base_domain,
						function(result)
						{
							debug("Success (FacebookLoginButtonSetState): " + result); /*Success*/
							updateForm(result);
							HideLabel();
						},
						function(result) { debug("Fail (FacebookLoginButtonSetState):" + result.get_message()); HideLabel(); /*Fail*/ },
						null);
				}
				else
					HideLabel();
				
//				if (session != null)
//				{
//					SaveDetailsClient(session.uid, session.session_key, session.secret, session.expires, session.base_domain);
//				}
				debug("FacebookLoginButtonSetState done");
			}
			function FacebookSaveStateFromPermissions(saveEmail, savePublish, revertEmailToFacebook)
			{
				debug("FacebookSaveStateFromPermissions saveEmail:" + saveEmail + ", savePublish: " + savePublish);
				var session = FB.Facebook.apiClient.get_session();
				if (session != null)
				{
					PageMethods.SaveDetailsFromPermissions(
						session.uid,
						session.session_key,
						session.secret,
						session.expires,
						session.base_domain,
						saveEmail,
						savePublish,
						revertEmailToFacebook,
						function(result)
						{
							debug("Success (FacebookSaveStateFromPermissions): " + result); /*Success*/
							updateForm(result);
							HideLabel();
							debug("Success (FacebookSaveStateFromPermissions) - DONE");
						},
						function(result) { debug("Fail (FacebookSaveStateFromPermissions):" + result.get_message()); HideLabel(); /*Fail*/ },
						null);
					debug("FacebookSaveStateFromPermissions done");
				}
			}
			function FacebookSaveState(revertEmailToFacebook)
			{
				var session = FB.Facebook.apiClient.get_session();
				if (session != null)
				{
					PageMethods.SaveDetails(
						session.uid,
						session.session_key,
						session.secret,
						session.expires,
						session.base_domain,
						document.getElementById("FacebookCheckboxSubscribe").checked,
						document.getElementById("FacebookCheckboxStream").checked,
						revertEmailToFacebook,
						function(result)
						{
							debug("Success (FacebookSaveState): " + result); /*Success*/
							updateForm(result);
							HideLabel();
						},
						function(result) { debug("Fail (FacebookSaveState):" + result.get_message()); HideLabel(); /*Fail*/ },
						null);
					
				}
			}
			function FacebookLogoutClick()
			{
				FB.Connect.logout(function() { FacebookLoginButtonSetState(); });
			}
			function FacebookDisconnectClick()
			{
				FB.Facebook.apiClient.revokeAuthorization(null, function() { FB.Connect.logout(function() { FacebookLoginButtonSetState(); }); });
			}
			
		
		</script>
		
		
    </form>
    <script type="text/javascript">

    	FacebookReadyFunctions[FacebookReadyFunctions.length] = function()
    	{
    		FacebookLoginButtonSetState();
    	}
    	
    	var firedReadyFunction = false;
    	var skippedCallingReadyFunction = false;
    	FB_RequireFeatures(["XFBML"], function()
    	{
    		FB.Facebook.init("<% = Common.Properties.IsDevelopmentEnvironment ? "97fd68d377367f415b68d76ab52ad668" : "623844c11d22fd9e79a5cb9f3f02a9b7" %>", "/xd_receiver.html");
    		FB.XFBML.Host.get_areElementsReady().waitUntilReady(function()
    		{
    			if (firedReadyFunction)
    			{
    				CallFacebookReadyFunctions();
    			}
    			else
    				skippedCallingReadyFunction = true;
    		});
    	});
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
