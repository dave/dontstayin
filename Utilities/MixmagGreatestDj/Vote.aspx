<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vote.aspx.cs" Inherits="MixmagGreatest.Vote" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="overflow-y: scroll;">

<head runat="server">
	<title><% = CurrentMixmagGreatestDj.Name %> - greatest DJ of all time? </title>
</head>
<style>
	body
	{
		font-family: Calibri, Arial, Helvetica, sans-serif;
	}
	
	a.NoStyle:link, 
	a.NoStyle:visited   { background-color:transparent!important; }
	a.NoStyle:hover     { background-color:transparent!important; }
	
	a:link, 
	a:visited   { text-decoration:none!important; color:#000000; background-color:#fecd07; }
	a:hover     { text-decoration:none!important; color:#fecd07; background-color:#000000; }
		
	.GreatestBox
	{
		margin:15px;
		padding-left:15px;
		padding-right:15px;
		border:2px solid #cccccc;
		background-color: #f8f8f8;
	}
	h1
	{
		font-size:30px;
	}
	h2
	{
		font-size:20px;
	}
	
	
	div.Spacer
		{
			width:520px;
			height:12px;
			background-color:#000000;
			
			margin-top:15px;
			margin-bottom:15px;
		}
		div.SpacerDotted
		{
			width:520px;
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
		p.Text
		{
			font-family:Arial, Sans-Serif;
			font-size:13px;
			font-weight:bold;
			
		}
</style>

<body style="background-image:url(/gfx/background.gif); background-repeat:repeat;">
    <form id="form1" runat="server">
		<asp:ScriptManager ID="Script" runat="server" EnableHistory="true" EnablePageMethods="true" />
		<div id="fb-root"></div>
		<script>
			PageMethods.set_path("/vote.aspx");
			var djname="<% = CurrentMixmagGreatestDj.Name.ToUpper() %>";
			window.fbAsyncInit = function () {
				FB.init({ appId: '<% = Common.Properties.IsDevelopmentEnvironment ? "79645061c9a11a654ac596182c42b489" : "726d8d3a2694bbf1fd9f90ec3d067d89" %>', status: true, cookie: true,
					xfbml: true
				});
			};
			(function () {
				var e = document.createElement('script'); e.async = true;
				e.src = document.location.protocol +
			  '//connect.facebook.net/en_US/all.js';
				document.getElementById('fb-root').appendChild(e);
			} ());
			function VoteNow() {
				document.getElementById("VotingSpan").style.display = "";
				document.getElementById("VoteButton").style.display = "none";
				//document.getElementById("VoteButton").innerHTML = "VOTING...";
				FB.getLoginStatus(function(response) {
					if (response.session) {
						VoteNowGo(response);
					} else {
						FB.login(function (response) {
							if (response.session) {
								if (response.perms) {

									VoteNowGo(response);

								} else {
									document.getElementById("VotingSpan").style.display = "none";
									document.getElementById("VoteButton").style.display = "";
									//document.getElementById("VoteButton").innerHTML = "VOTE FOR " + djname;
									alert("Oops, you got to grant permissions to vote...");
								}
							} else {
								document.getElementById("VotingSpan").style.display = "none";
								document.getElementById("VoteButton").style.display = "";
								//document.getElementById("VoteButton").innerHTML = "VOTE FOR " + djname;
								alert("Oops, you got to connect to facebook to vote...");
							}
						}, { perms: 'publish_stream,email' });
					}
				});
			}
			function VoteNowGo(response) {
				PageMethods.VoteNow(
					response.session.uid,
					response.session.session_key,
					response.session.secret,
					response.session.expires,
					response.session.base_domain,
					<% = CurrentMixmagGreatestDj.K %>,
					"<% = CurrentMixmagGreatestDj.UrlName %>",
					function (result) {
						//alert("Voting done!");
						document.getElementById("DoneForm").style.display = "";
						document.getElementById("VoteForm").style.display = "none";
					},
					function (result) {
						document.getElementById("VotingSpan").style.display = "none";
						document.getElementById("VoteButton").style.display = "";
						alert(result.get_message());
					},
					null
				);
			}
			function FacebookLogoutClick()
			{
				FB.getLoginStatus(function (response) {
					if (response.session) {
						FB.logout(function() { alert("Done."); });
					}
					else
						alert("Logout failed - no session!");
				});
			}
			function FacebookDisconnectClick()
			{
				FB.getLoginStatus(function (response) {
					if (response.session) {
						PageMethods.DisconnectNow(
							response.session.uid,
							response.session.session_key,
							response.session.secret,
							response.session.expires,
							response.session.base_domain,
							function (result) { alert("Done."); },
							function (result) { alert("Disconnect failed!"); },
							null
						);
					}
					else
						alert("Disconnect failed - no session!");
				});
			}
		</script>

		<div style="width:520px; padding:15px; background-color:#ffffff; margin-left:auto; margin-right:auto; margin-top:15px; margin-bottom:15px;">
		
			<div style="margin-bottom:15px;">
				<a href="/" class="NoStyle"><img src="http://greatest.dj/gfx/mm_greatest_dj_withmm_logo.jpg" width="520" height="299" border="0" /></a>
			</div>

			<div style="height:50px;">
				<div style="height:50px; float:left;">
					<a href="/<% = PrevDj.UrlName %>">
						<div style="height:25px; float:right; background-color: #b3b6bb; padding: 25px 10px 0px 10px; min-width:100px; float:left; text-align:right; cursor:pointer;">
							<span style="font-size:12px; font-family:Arial Black, Sans-Serif; font-weight:bold; margin:0px; padding:0px;"><% = PrevDj.Name.ToUpper() %></span>
						</div>
						<img src="<% = PrevDj.Image100Url %>" width="50" height="50" style="margin-right:0px; float:left;" border="0" />
						<img src="/gfx/arrow-prev.gif" width="23" height="50" style="margin-right:0px; float:left;" border="0" />
					</a>
				</div>
				<div style="height:50px; float:right;">
					<a href="/<% = NextDj.UrlName %>">
						<div style="height:25px; float:right; background-color: #b3b6bb; padding: 25px 10px 0px 10px; min-width:100px; cursor:pointer;">
							<span style="font-size:12px; font-family:Arial Black, Sans-Serif; font-weight:bold;"><% = NextDj.Name.ToUpper() %></span>
						</div>
						<img src="<% = NextDj.Image100Url %>" width="50" height="50" style="margin-left:0px; float:right;" border="0" />
						<img src="/gfx/arrow-next.gif" width="23" height="50" style="margin-left:0px; float:right;" border="0" />
					</a>
				</div>
			</div>

			<div class="Spacer" style="clear:both;"></div>

			
			<p class="Header">
				<% = CurrentMixmagGreatestDj.Name.ToUpper() %>
			</p>
			<div class="SpacerDotted"></div>

			<div style="padding-left:215px; position:relative; min-height:200px;">
				<img src="<% = CurrentMixmagGreatestDj.Image200Url %>" width="200" height="200" style="position:absolute; left:0px;" />
				<p class="Text">
					<i><% = CurrentMixmagGreatestDj.ShortDescription %></i>
				</p>
				<p class="Text">
					<% = CurrentMixmagGreatestDj.Description %>
				</p>
			</div>

			<!--<div class="Spacer" style="clear:both;"></div>-->

			<div runat="server" id="DoneForm" style="display:none;" visible="false">
				<div style="height:33px; xborder:1px solid #ff0000;">
					<span style="height:35px; font-size: 25px; font-weight: bold; margin:0px; float:left;" runat="server" id="MessageSpan">
						Thankyou for voting
					</span>
					<button class="BigButton" style="float:right;" onclick="window.close();/*location.href='/go/';*/ return false;" xonclick="window.close();return false;" onmouseover="this.style.backgroundColor='#000000';this.style.color='#fecd07';" onmouseout="this.style.backgroundColor='#fecd07';this.style.color='#000000';">
						CLOSE
					</button>
				</div>
			</div>
			<div runat="server" id="VoteForm" xstyle="display:none;" visible="false">
				<div style="height:33px;">
					<span style="height:35px; font-size: 25px; font-weight: bold; margin:0px; display:none; float:left;" id="VotingSpan">
						Voting...
					</span>
					<button class="BigButton" style="float:left;" onclick="VoteNow(); return false;" id="VoteButton" onmouseover="this.style.backgroundColor='#000000';this.style.color='#fecd07';" onmouseout="this.style.backgroundColor='#fecd07';this.style.color='#000000';">
						VOTE FOR <% = CurrentMixmagGreatestDj.Name.ToUpper() %>
					</button>
					<button class="BigButton" style="float:right;" onclick="window.close();/*location.href='/go/';*/ return false;" xonclick="window.close();return false;" onmouseover="this.style.backgroundColor='#000000';this.style.color='#fecd07';" onmouseout="this.style.backgroundColor='#fecd07';this.style.color='#000000';">
						CANCEL
					</button>
				</div>
			</div>
			
			<style>
				.BigButton
				{
					cursor:pointer;
					border: 1px solid #000000;
					height: 33px;
					font-size: 20px;
					font-weight: bold;
					margin: 0px;
					background-color: #fecd07;
					padding-left:10px;
					padding-right:10px;
					overflow: visible; 
				}
			</style>

			<div class="Spacer" style="clear:both;"></div>

			<p style="clear:both;" runat="server" id="VideoPanel">
				<object width="520" height="300">
					<param name="movie" value="http://www.youtube.com/v/<% = CurrentMixmagGreatestDj.YoutubeId %>?fs=1&amp;hl=en_GB"></param>
					<param name="allowFullScreen" value="true"></param>
					<param name="allowscriptaccess" value="always"></param>
					<embed 
						src="http://www.youtube.com/v/<% = CurrentMixmagGreatestDj.YoutubeId %>?fs=1&amp;hl=en_GB" 
						type="application/x-shockwave-flash" 
						allowscriptaccess="always" 
						allowfullscreen="true" 
						width="520" 
						height="300">
					</embed>
				</object>
			</p>
			<p class="Text" style="margin-bottom:25px; font-weight:normal;">
				<img src="<% = CurrentMixmagGreatestDj.Image50Url %>" width="50" height="50" align="left" style="margin-top:3px; margin-right:12px; margin-bottom:5px;" />
				<% = CurrentMixmagGreatestDj.LongDescription %>
			</p>
			<!--<p style="border:1px solid #cecfce; padding:5px; margin:5px 9px 5px 0px; background-color:#f7f7f7; font-family:Verdana; font-size:12px; line-height:15px;">
				Looks like Facebook comments are broken today, but we've found a work-around... To post a comment, click into the box, then click out of it, then click into it again. It should work!
			</p>-->
			<p>
				<fb:comments xid="main<% = CurrentMixmagGreatestDj.K %>" width="520px"></fb:comments>
			</p>

			<div style="height:50px;">
				<div style="height:50px; float:left;">
					<a href="/<% = PrevDj.UrlName %>">
						<div style="height:25px; float:right; background-color: #b3b6bb; padding: 25px 10px 0px 10px; min-width:100px; float:left; text-align:right; cursor:pointer;">
							<span style="font-size:12px; font-family:Arial Black, Sans-Serif; font-weight:bold; margin:0px; padding:0px;"><% = PrevDj.Name.ToUpper() %></span>
						</div>
						<img src="<% = PrevDj.Image100Url %>" width="50" height="50" style="margin-right:0px; float:left;" border="0" />
						<img src="/gfx/arrow-prev.gif" width="23" height="50" style="margin-right:0px; float:left;" border="0" />
					</a>
				</div>
				<div style="height:50px; float:right;">
					<a href="/<% = NextDj.UrlName %>">
						<div style="height:25px; float:right; background-color: #b3b6bb; padding: 25px 10px 0px 10px; min-width:100px; cursor:pointer;">
							<span style="font-size:12px; font-family:Arial Black, Sans-Serif; font-weight:bold;"><% = NextDj.Name.ToUpper() %></span>
						</div>
						<img src="<% = NextDj.Image100Url %>" width="50" height="50" style="margin-left:0px; float:right;" border="0" />
						<img src="/gfx/arrow-next.gif" width="23" height="50" style="margin-left:0px; float:right;" border="0" />
					</a>
				</div>
			</div>

			<p style="display:<%=Bobs.Vars.DevEnv?"":"none"%>;">
				<a href="/" onclick="FacebookDisconnectClick();return false;">Disconnect</a> | 
				<a href="/" onclick="FacebookLogoutClick();return false;">log out</a>
			</p>

		</div>
    </form>
</body>
</html>
