<%@ Page EnableEventValidation="false" Trace="false" Language="C#" AutoEventWireup="true" CodeBehind="DsiPage.aspx.cs" Inherits="Spotted.Master.DsiPage" %>
<%@ Register TagPrefix="Navigation" TagName="Admin" Src="~/Controls/Navigation/Admin.ascx" %>
<%@ Register TagPrefix="Navigation" TagName="Login" Src="~/Controls/Login.ascx" %>
<%@ Register TagPrefix="Navigation" TagName="WhosOnline" Src="~/Controls/Navigation/WhosOnline.ascx" %>
<%@ Register TagPrefix="Navigation" TagName="SpotterRank" Src="~/Controls/Navigation/SpotterRank.ascx" %>
<%@ Register TagPrefix="Navigation" TagName="RecentDonators" Src="~/Controls/Navigation/RecentDonators.ascx" %>
<%@ Register TagPrefix="Navigation" TagName="Menu" Src="~/Controls/Navigation/NewMenu.ascx" %>
<%@ Register TagPrefix="Banners" TagName="Generator" Src="~/Controls/Banners/Generator.ascx" %>
<%@ Register TagPrefix="Promoters" TagName="AccountsWarning" Src="~/Controls/Promoters/AccountsWarning.ascx" %>
<%@ Register TagPrefix="Navigation" TagName="ChatClient" Src="~/Controls/ChatClient.ascx" %>
<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml" xml:lang="en" lang="en" style="overflow-y: scroll; overflow-x: scroll;" runat="server" id="HtmlTag">
<head runat="server">
	<title runat="server" id="PageTitleTag" />
	<meta property="fb:app_id" content="<%= Bobs.Vars.DevEnv ? "197126327107" : "148416032543" %>"/>
	<meta http-equiv="X-UA-Compatible" content="chrome=1">
	<meta http-equiv="content-type" content="text/html;charset=utf-8" />
	<meta http-equiv="Content-Style-Type" content="text/css" />
	<meta name="google-site-verification" content="rY6ZGN8UTSVYR6neyF9DtYJ8cZNyX8i6Bz3i7F7KDoI" />
	<asp:PlaceHolder runat="server" ID="GoogleAnalyticsPlaceholder">
		<script type="text/javascript">
			var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
			document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
		</script>
		<script type="text/javascript">
			try
			{
				var pageTracker = _gat._getTracker("UA-260957-1");
				pageTracker._trackPageview();
			}
			catch (e)
			{
				try // again
				{
					var pageTracker = _gat._getTracker("UA-260957-1");
					pageTracker._trackPageview();
				}
				catch (e)
				{
				}
			}
		</script>
	</asp:PlaceHolder>
	<asp:PlaceHolder runat="server" id="TemplateMetaPlaceHolder" />
	<meta name="resource-type" content="document" />
	<meta name="distribution" content="global" />
	<meta name="author" content="DontStayIn" />
	<meta name="copyright" content="Copyright (c) by DontStayIn" />
	<meta name="keywords" content="clubbing, photos, bars, pics, photo" />
	
	<meta name="title" content="" runat="server" id="MetaTitle" />
	<meta name="description" content="The world's biggest dance music and clubbing website." runat="server" id="MetaDescription" />
	<meta name="medium" content="" runat="server" id="MetaMedium" />
	<link rel="image_src" href="" runat="server" id="LinkImageSrc" />
	<link rel="video_src" href="" runat="server" id="LinkVideoSrc" />
	<meta name="video_height" content="" runat="server" id="MetaVideoHeight" />
	<meta name="video_width" content="" runat="server" id="MetaVideoWidth" />
	<meta name="video_type" content="" runat="server" id="MetaVideoType" />

	<meta name="robots" content="index, follow">
	<meta name="revisit-after" content="1 days">
	<meta name="rating" content="general">
	<%= Bobs.Storage.PathJavascriptFunction() %>
	<link rel="stylesheet" type="text/css" href="/support/style.css?a=6"/>
	<link rel="stylesheet" type="text/css" href="/misc/jquery/css/jquery-ui-1.8.1.custom.css"/>
	<script src="/misc/json-min.js" type="text/javascript"></script>
	<script src="/misc/jquery/jquery-1.4.2.modified-get-function.min.js" type="text/javascript"></script>
	<script src="/misc/jquery/jquery.ba-bbq.js" type="text/javascript"></script>
	<script language="javascript" src="/Misc/DbChat.js?a=41<%= Bobs.Vars.DevEnv?Cambro.Misc.Utility.GenRandomText(5):"" %>"></script>
	<script language="javascript" src="/Misc/Flash.js"></SCRIPT>
	<%= Spotted.Main15Script.Register %>
	<script language="javascript" src="/Misc/Utilities.js"></SCRIPT>
	<link rel="stylesheet" type="text/css" href="/Misc/MenuStyle.css?a=3"/>
	<link rel="stylesheet" href="/Misc/thickbox.css" type="text/css" media="screen" />
	<asp:Panel Runat="server" Visible="false">
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name=vs_defaultClientScript content="JavaScript">
		<meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
	</asp:Panel>
	
	<%= Vars.IE && Request.Browser.MajorVersion <= 7 ? "<style>a:link, a:visited {color:#444444;}</style>" : "" %>
	<%= Vars.FacebookCanvasMode ? "<style> div.ContentBorder { border:0px; padding: 3px 10px 3px 10px; } div.Pad { padding: 0px 10px 0px 10px; } div.ContentBorder.Padding { padding: 10px; } </style>" : ""%>
	
</head>
<script>
	var RunAtEndOfPage = "";
	var RunAtEndOfPageFunctions = new Array();
	var FacebookReadyFunctions = new Array();
</script>
<body runat=server topmargin=0 bottommargin=0 leftmargin=0 rightmargin=0 id="Body" bgcolor="ffffff">
	<div id="fb-root"></div>
	<script>
		var DoneFbAsyncInit = false;
		window.fbAsyncInit = function () {
			FB.init({
				appId: '<%= Facebook.FacebookCommon.Common(Facebook.Apps.Dsi).AppId %>',
				status: true, // check login status
				cookie: true, // enable cookies to allow the server to access the session
				xfbml: true,  // parse XFBML
				channelURL: 'http://<%= Vars.DomainName %>/channel.html', // channel.html file
				oauth: true
			});
			DoneFbAsyncInit = true;
			try
			{
				LoginFacebookReady();
			}
			catch (ex) { }
		};

		(function()
		{
			var e = document.createElement('script');
			e.src = document.location.protocol + '//connect.facebook.net/en_US/all.js';
			e.async = true;
			document.getElementById('fb-root').appendChild(e);
		}());
	</script>
	
	<a name="TopAnchor"></a>
	<script type="text/javascript"> var ie = false; </script>
	<!--[if IE]>
	<script type="text/javascript"> ie = true; </script>
	<![endif]-->

	<form id="TemplateForm" method="post" runat="server">

		<!-- Welcome to DontStayIn  --> <% // DON'T DELETE THIS - IT'S NEEDED FOR THE TESTS TO PASS %>

		
		<asp:HiddenField runat="server" ID="AuthCookieHasError" />
		<asp:HiddenField runat="server" ID="UsrKAtInit" />
		<asp:HiddenField runat="server" ID="UsrK" />
		<asp:HiddenField runat="server" ID="UsrNickname" />
		<asp:HiddenField runat="server" ID="UsrLink" />
		<asp:HiddenField runat="server" ID="CountryKFromIp" />

		<div runat="server" id="ChatClientPopupAreaHolder" class="ChatClientPopupArea" style="position:fixed; bottom:0px; right:0px; height:0px; width:250px; z-index: 960; overflow: visible;"></div>
		<asp:ScriptManager ID="Script" runat="server" EnableHistory="true" EnablePageMethods="true" />
		<script>
			PageMethods.set_path("/pagemethods/genericpage.aspx");
		</script>
		
		<asp:PlaceHolder runat="server" id="HeaderScriptPlaceHolder"></asp:PlaceHolder>
		<div id="TipLayer" style="visibility:hidden; position:absolute; z-index:1000;" class="TipLayerDiv"></div>	
		<asp:Button Runat="server" ID="MasterButton" Visible="False"></asp:Button>

		

		<div id="BodyMainBackgroundOuter" runat="server" style="margin:0px 0px; padding:0px; text-align:center;">
		<div id="BodyMainBackgroundInner" runat="server" style="position:relative; height:100%; margin:0px auto; width:983px; text-align:left;">
		
		<asp:Panel runat="server" ID="N3BottomBox" />
			
		<Navigation:Menu runat="Server" ID="Menu"/>
	
		<div style="position:absolute;width:634px;left:0px;top:130px;z-index:100;" id="ContentDiv" runat="server">
			<asp:Panel runat="server" ID="TopContentHolder">
			
				<asp:Panel runat="server" ID="SalesCallPanel"></asp:Panel>
				
				<asp:Panel runat="server" ID="SalesUsrAlarmPanel" Visible="false">
					<dsi:h1 id="SalesUsrAlarmPanelHeader" runat="server">You have an alarm</dsi:h1>
					<div class="ContentBorder" runat="server" id="SalesUsrAlarmPanelInner">
						<asp:GridView ID="SalesUsrAlarmGridView" runat="server" AllowPaging="False" ShowFooter="true" AutoGenerateColumns="False" CssClass="dataGrid" width="100%" OnRowDeleting="SalesUsrAlarmGridView_RowDeleting" OnRowCommand="SalesUsrAlarmGridView_RowCommand"
							AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader" OnRowDataBound="SalesUsrAlarmGridView_RowDataBound" FooterStyle-CssClass="dataGridFooter"
							SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
						   <Columns>
							   <asp:TemplateField HeaderText="" SortExpression="" Visible="False">
									<ItemTemplate>
										<asp:Label ID="PromoterKLabel" runat="server" Text='<%# ((Promoter)Container.DataItem).K %>'></asp:Label>
									</ItemTemplate>
							   </asp:TemplateField>
							   <asp:TemplateField HeaderText="Promoter">
									<ItemStyle HorizontalAlign="Left" />
									<ItemTemplate>
										<nobr><a href="<%#((Promoter)Container.DataItem).Url() %>"><%# ((Promoter)Container.DataItem).Name %></a></nobr>
									</ItemTemplate>
									<HeaderStyle HorizontalAlign="Left" />
							   </asp:TemplateField>
							   <asp:TemplateField HeaderText="<nobr>Next call</nobr>">
									<ItemStyle HorizontalAlign="Left" />
									<ItemTemplate>
										<b><%#Cambro.Misc.Utility.FriendlyDate(((Promoter)Container.DataItem).SalesNextCall) %>&nbsp;<%#Cambro.Misc.Utility.FriendlyTime(((Promoter)Container.DataItem).SalesNextCall) %></b>
									</ItemTemplate>
									<FooterStyle HorizontalAlign="Right" />
									<FooterTemplate>
										<b>ALL</b>
									</FooterTemplate>
									<HeaderStyle HorizontalAlign="Left" />
							   </asp:TemplateField>
							   <asp:TemplateField HeaderText="Snooze">
									<ItemStyle HorizontalAlign="Left" />
									<ItemTemplate>
										<asp:DropDownList runat="server" ID="SnoozeDropDownList" AutoPostBack="true" OnSelectedIndexChanged="SnoozeDropDownList_SelectedIndexChanged">
											<asp:ListItem Value=""></asp:ListItem>
											<asp:ListItem Value="5">5 mins</asp:ListItem>
											<asp:ListItem Value="30">30 mins</asp:ListItem>
											<asp:ListItem Value="60">1 hour</asp:ListItem>
											<asp:ListItem Value="120">2 hours</asp:ListItem>
											<asp:ListItem Value="240">4 hours</asp:ListItem>
											<asp:ListItem Value="1440">1 day</asp:ListItem>
											<asp:ListItem Value="2880">2 days</asp:ListItem>
											<asp:ListItem Value="4320">3 days</asp:ListItem>
											<asp:ListItem Value="10080">1 week</asp:ListItem>
											<asp:ListItem Value="20160">2 weeks</asp:ListItem></asp:DropDownList>
									</ItemTemplate>
									<FooterTemplate><asp:DropDownList runat="server" ID="SnoozeAllDropDownList" AutoPostBack="true" OnSelectedIndexChanged="SnoozeAllDropDownList_SelectedIndexChanged">
											<asp:ListItem Value=""></asp:ListItem>
											<asp:ListItem Value="5">5 mins</asp:ListItem>
											<asp:ListItem Value="30">30 mins</asp:ListItem>
											<asp:ListItem Value="60">1 hour</asp:ListItem>
											<asp:ListItem Value="120">2 hours</asp:ListItem>
											<asp:ListItem Value="240">4 hours</asp:ListItem>
											<asp:ListItem Value="1440">1 day</asp:ListItem>
											<asp:ListItem Value="2880">2 days</asp:ListItem>
											<asp:ListItem Value="4320">3 days</asp:ListItem>
											<asp:ListItem Value="10080">1 week</asp:ListItem>
											<asp:ListItem Value="20160">2 weeks</asp:ListItem></asp:DropDownList>
									</FooterTemplate>
									<HeaderStyle HorizontalAlign="Left" />
							   </asp:TemplateField>
							   <asp:TemplateField HeaderText="<nobr>Alarm off</nobr>">
									<ItemTemplate>
										<asp:LinkButton ID="DeleteLinkButton" CommandName="Delete" runat="server" CausesValidation="False"><asp:Image ID="Image2" runat="server" ImageUrl="~/Gfx/button-delete.gif" AlternateText="Alarm off" /></asp:LinkButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:LinkButton ID="DeleteAllLinkButton" CommandName="DeleteAll" runat="server" CausesValidation="False"><asp:Image ID="Image3" runat="server" ImageUrl="~/Gfx/button-delete.gif" AlternateText="All alarms off" /></asp:LinkButton>
									</FooterTemplate>
							   </asp:TemplateField>
						   </Columns>
						</asp:GridView>
					</div>
				</asp:Panel>
				
				<asp:Panel Runat=server ID="PendingAbusePanel" Visible="False" EnableViewState="False">
					<dsi:h1 id="H2" runat="server">Photo abuse</dsi:h1>
					<div class="ContentBorder">
						<p>
							<a href="/pages/abusereport">Resolve here</a>
						</p>
					</div>
				</asp:Panel>
				
				<Promoters:AccountsWarning runat="server" ID="PromoterAccountsWarningControl" EnableViewState="False"/>
				
				<asp:Panel Runat="server" ID="PromoterAccountsOutstandingPanel" Visible="false">
					<dsi:h1 id="PromoterAccountsOutstandingHeader" runat="server">Your promoter account balance is outstanding</dsi:h1>
					<div class="ContentBorder">
						<h2 id="PromoterAccountsOutstandingMessage" runat="server" visible="false"></h2>
						<table id="PromoterAccountsOutstandingTable" runat="server" cellpadding="3" cellspacing="0"></table>
					</div>
				</asp:Panel>
				
				<asp:Panel Runat="server" ID="ActivitiesPanel" EnableViewState="false">
					<dsi:h1 id="H3" runat="server">Things to do before you log off</dsi:h1>
					<div class="ContentBorder">
						<p>
							<ul style="margin-top:2px;">
								<li runat="server" id="ActivityEmailVerify"><a href="/pages/emailverify"><b>Your email isn't verified</b> - we can't send you email until you fix it</a></li>
								<li runat="server" id="ActivityBrokenEmail"><a href="/pages/emailbroken"><b>Your email is broken</b> - we can't send you email until you fix it</a></li>
								<li runat="server" id="ActivityPicture"><a href="/pages/mypicture">Create your picture</a></li>
								<li runat="server" id="ActivityPlaces"><a href="/pages/placesivisit">Tell us which places you visit</a></li>
								<li runat="server" id="ActivityMusic"><a href="/pages/mymusic">Tell us what music you like</a></li>
								<li runat="server" id="ActivityDetails"><a href="/pages/mydetails">Complete your details</a></li>
								<li runat="server" id="ActivityDj"><a href="/pages/areyoudj">Are you a DJ? Click here to get your DJ icon!</a></li>
								<li runat="server" id="ActivityTicketFeedback"></li>
								<!--<li runat="server" id="ActivityMixmag"><a href="/popup/mixmag?go=1" target="_blank">Get a FREE subscription to Mixmag Online!</a></li>-->
								<!--<li runat="server" id="ActivityBuddyImporter"><a href="/pages/friendinviter">Invite your friends to DontStayIn</a></li>-->
								<!--<li runat="server" id="ActivityExDirectoryOption"><a href="/pages/exdirectoryprivacyoption">Confirm your privacy settings</a></li>-->
							</ul>
						</p>
					</div>
				</asp:Panel>

				<asp:Panel Runat="server" ID="PromoterStuckPanel">
					<dsi:h1 id="H4" runat="server">We're happy to help</dsi:h1>
					<div class="ContentBorder">
						<p>
							As a promoter you're important to us. If you're having problems with the site or simply don't understand something, <b>just give us a call on 0207 835 5599</b>.
						</p>
						<p align="right">
							<asp:LinkButton Runat="server" OnClick="PromoterStuckPanel_Hide" CausesValidation="False" ID="Linkbutton1" NAME="Linkbutton1">Hide this box</asp:LinkButton>
						</p>
					</div>
				</asp:Panel>
				
			</asp:Panel>
			<a name="ContentAnchor"></a>
			<!-- google_ad_section_start -->
			<asp:PlaceHolder Runat="server" ID="MainContentPlaceHolder"/>
			<!-- google_ad_section_end -->
			<div class="ContentFooter">
				<center>
					<small id="TermsAndConditionsText" runat="server">
						Copyright Development Hell Limited 2003 - <%= DateTime.Now.Year.ToString() %> | 
						<a id="TermsAndConditionsLink" runat="server" href="/pages/legal">Terms and conditions</a> | 
						<a href="/pages/contact">Contact us</a> | 
						<asp:LinkButton runat="server" OnClick="Mobile" id="MobileLinkButton" CausesValidation="false">Mobile site</asp:LinkButton>
					</small>
				</center>
			</div>
			<div class="ContentFooter">
				<center>
					<asp:LinkButton runat="server" OnClick="ToggleAdmin" id="ToggleAdminLinkButton" CausesValidation="false">[toggle admin]</asp:LinkButton>
					<asp:LinkButton runat="server" OnClick="ToggleBanners" id="ToggleBannersLinkButton" CausesValidation="false">[toggle banners]</asp:LinkButton>
					<asp:LinkButton runat="server" OnClick="ToggleChat" id="ToggleChatLinkButton" CausesValidation="false">[toggle chat]</asp:LinkButton>
					<asp:LinkButton runat="server" OnClick="ToggleCanvas" id="ToggleCanvasLinkButton" CausesValidation="false">[toggle canvas]</asp:LinkButton>
				</center>
			</div>
			<input type="text" style="width:1px;height:1px;" class="TextBoxFix" />
		</div>
		
		<div runat="server" id="ChatClientHolder" style="position:absolute; left:649px; width:334px; height:500px; z-index:150;">
			<asp:Panel runat="server" ID="SkyscraperHolder">
				<div class="ContentBorder Padding" style="position:relative; margin-top:35px; text-align:center;" runat="server" id="SkyscraperOuterDiv" enableviewstate="false">
					<!-- //XMAS -->
					<div runat="server" id="SkyscraperInnerDiv" style="border:0px; width:300px; height:250px; overflow:hidden; margin-left:auto; margin-right:auto;">
						<Banners:Generator runat="server" Position="Skyscraper" ID="Skyscraper" ShowClickHelper="True" ClickHelperTopOffset="15" ClickHelperLeftOffset="15" /></div>
				</div>
			</asp:Panel>
			<asp:PlaceHolder runat="server" ID="AboveChatHtmlPh" />
			<Navigation:ChatClient runat="Server" ID="ChatClient" Visible="false" />
			<asp:Panel runat="server" ID="UnrulyBottomBox">
				<dsi:h1 ID="H1" runat="server">Bored? Watch this!</dsi:h1>
				<div class="ContentBorder">
					<script type="text/javascript" src="http://video.unrulymedia.com/wildfire_1836999.js"></script>
				</div>
			</asp:Panel>

			<H1><span class="Inner">Join us on:</span></H1> 
			<div class="ContentBorder">
				<p class="ClearAfter">
					<iframe src="http://www.facebook.com/plugins/likebox.php?id=95813938222&amp;width=292&amp;connections=10&amp;stream=true&amp;header=false&amp;height=555" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:292px; height:555px;" allowTransparency="true"></iframe>
					<a href="http://www.dontstayin.com/article-11684" target="_blank">
						<img src="http://pix-cdn.dontstayin.com/657b9d01-69ab-44d7-a5fe-dcbae5872c0a.png" width="150" height="115" class="Block" style="float:left;">
					</a>
					<a href="http://twitter.com/DontStayIn_com" target="_blank">
						<img src="http://pix-cdn.dontstayin.com/42243229-ed7f-40fa-8963-623f10aafb18.png" width="150" height="115" class="Block" style="float:right;">
					</a>
					<center>
						<a href="http://www.dontstayin.com/popup/bannerclick/bannerk-14074">
							<img src="http://pix-cdn.dontstayin.com/325a88b4-69a5-4471-bdad-acbaf8b888f2.jpg" width="300" height="57" class="Block" style="float:center;">
						</a>
					</center>
					<a class="mixcloud-follow-widget" href="http://www.mixcloud.com/dontstayin/" data-h="160" data-w="292" data-faces="on">
						Follow Don't Stay In on Mixcloud
					</a>
					<script src="http://widget.mixcloud.com/media/js/follow_embed.js"></script>
				</p>
			</div>

			<!-- w00t! Quantcast Tag --> 
			<script type="text/javascript">
				var _qevents = _qevents || [];
				(function () {
					var elem = document.createElement('script');
					elem.src = (document.location.protocol == "https:" ? "https://secure" : "http://edge") + ".quantserve.com/quant.js";
					elem.async = true;
					elem.type = "text/javascript";
					var scpt = document.getElementsByTagName('script')[0];
					scpt.parentNode.insertBefore(elem, scpt);
				})();
				_qevents.push({
					qacct: "p-70RhgKmunzjBs",
					labels: "Music,Nightlife,Urban Lifestyle"
				});
			</script> 
			<noscript>
				<div style="display:none;">
					<img src="//pixel.quantserve.com/pixel/p-70RhgKmunzjBs.gif?labels=Music,Nightlife,Urban%20Lifestyle" border="0" height="1" width="1" alt="Quantcast"/>
				</div>
			</noscript> 
			<!-- End of w00t! Quantcast tag -->

			
			<div id="SidebarMaxTopDiv"></div>
		
		</div>
		
		<div runat="server" id="LeaderboardOuterDiv" style="position:absolute; top:20px; left:0px; z-index:500; width:949px; height:90px;" class="ContentBorder Padding">
			<div runat="server" id="LogoOuterDiv1" style="position:absolute;height:90px;width:219px;">
				<a href="/" class="NoStyle" runat="server" id="LogoAnchor"><img src="/gfx/logo-200-90.jpg" style="display:block;" width="200" height="90" /></a><asp:PlaceHolder runat="server" ID="LogoTakeoverPh" />
			</div>
			<div runat="server" id="LeaderboardInnerDiv" style="position:absolute;left:236px;" class="NoStyle">
				<Banners:Generator runat="Server" ID="Leaderboard" Position="Leaderboard" ShowClickHelper="True" ClickHelperTopOffset="0" ClickHelperLeftOffset="0" EnableViewState="False"/>
			</div>
		</div>
		
		<div runat="server" id="BigLeaderboardOuterDiv" style="position:absolute; top:20px; left:0px; z-index:500; width:952px; height:250px; padding-left:13px; padding-right:14px;" class="ContentBorder Padding">
			<div runat="server" id="BigLeaderboardInnerDiv" style="position:absolute;" class="NoStyle"></div>
		</div>

		<div runat="server" id="FacebookCanvasNavOuterDiv" visible="false" style="position:absolute; left:640px;">
			<dsi:h1 runat="server">Header</dsi:h1>
			<div runat="server" id="FacebookCanvasNavInnerDiv" style="width:90px;" class="ContentBorder">
				Facebook nav
			</div>
			<div runat="server" id="FacebookCanvasNavBannerOuterDiv" style="width:120px; background-color:#cccccc; color:#ffffff; text-align:center; height:600px; margin-top:10px;">
				<div style="padding-top:290px;">
					<b>banner</b>
				</div>
			</div>
		</div>

		<asp:Panel runat="server" id="TallMpuOuterDiv" Visible="false" style="position:absolute;left:649px;top:20px;width:300px;z-index:300;" class="ContentBorder Padding HeaderSpace" enableviewstate="false">
			<asp:Panel runat="server" id="TallMpuDiv">
				<div runat="server" id="TallMpuInnerDiv" style="border:0px;width:300px;height:600px;" class="NoStyle">
				</div>
			</asp:Panel>
		</asp:Panel>
		
		<asp:Panel runat="server" id="HotboxOuterDiv" style="position:absolute;left:649px;top:20px;width:300px;z-index:300;" class="ContentBorder Padding HeaderSpace" enableviewstate="false">
			<asp:Panel runat="server" id="HotboxDiv">
				<div runat="server" id="HotboxInnerDiv" style="border:0px;width:300px;height:250px;" class="NoStyle">
					<Banners:Generator runat="Server" Position="Hotbox" EnableViewState="False" ID="Hotbox" ShowClickHelper="True" ClickHelperTopOffset="15" ClickHelperLeftOffset="15"/></div>
			</asp:Panel>
		</asp:Panel>
		
		<div id="NavLoginDiv" style="position:absolute;left:20px;top:20px;height:21px;width:600px; z-index:100;" runat="server">
			<Navigation:Login runat="Server" ID="NavLogin" visible="true"/>
		</div>
		
		<script>
			
			function ShowWaitingCursor()
			{
				document.getElementById("WaitCursorImg").style.width = "983px";
				document.getElementById("WaitCursorImg").style.height = jQuery(document).height() + "px";
				document.getElementById("WaitCursorImg").style.display = "";
				document.getElementById("WaitCursorImg").style.cursor = "wait";
			}
			function HideWaitingCursor()
			{
				document.getElementById("WaitCursorImg").style.cursor = "default";
				document.getElementById("WaitCursorImg").style.display = "none";
			}
			
			var prm = Sys.WebForms.PageRequestManager.getInstance();
			
			prm.add_beginRequest
			(
				function ()
				{
					prm._scrollPosition = null;
					ShowWaitingCursor();
				}
			);
			prm.add_pageLoading
			(
				function ()
				{
					HideWaitingCursor();
				}
			);

		</script>
		
		<img id="WaitCursorImg" src="/gfx/1pix.gif" style="display:none; cursor:default; position:absolute; z-index:1100; top:0; left:0; width:100px; height:100px;" onclick="/*Sys.WebForms.PageRequestManager.getInstance().abortPostBack();*/this.style.cursor = 'default';this.style.display = 'none';" />

		<asp:Panel Runat="server" ID="AnchorSkipJs" EnableViewState="False" Visible="False">
			<script>
				document.location="#<asp:PlaceHolder Runat="server" ID="AnchorSkipName"></asp:PlaceHolder>";
			</script>
		</asp:Panel>
		
		

		

		</div>
		</div>

		
		
		<div runat="server" ID="uiFooterRightDiv"
			style="position:fixed; bottom:0px; right:0px; text-align:right; z-index:-10; height:100px; width:100px; overflow:visible;" 
			Visible="false">
			This is the right footer
		</div>

		<div runat="server" ID="uiFooterLeftDiv" 
			style="position:fixed; bottom:0px; left:0px; z-index:-10; height:100px; width:100px; overflow:visible;" 
			Visible="false">
			This is the left footer
		</div>

		<div runat="server" ID="uiLinksDiv" 
			style="display:none; position:absolute; height:100px; width:100px; overflow:visible;" 
			Visible="false">
			This is the links section
		</div>
		
	</form>
	<div id="FB_HiddenIFrameContainer" style="display:none; position:absolute; left:-100px; top:-100px; width:0px; height: 0px;"></div>
	<script type="text/javascript">
//		FB_RequireFeatures(["XFBML"], function()
//		{
//			FB.Facebook.init("<% = Common.Properties.IsDevelopmentEnvironment ? "bfa8eee21e5571480f66888debf50534" : "1268a0d0435face20e77bf22465eb438" %>", "/misc/xd_receiver.html"); /*TEST*/

//			FB.XFBML.Host.get_areElementsReady().waitUntilReady(function()
//			{
//				for (i = 0; i < FacebookReadyFunctions.length; i++)
//				{
//					FacebookReadyFunctions[i].call();
//				}
//			});
//		});

//		FB_RequireFeatures(["CanvasUtil"], function()
//		{
//			FB.Facebook.init("<% = Common.Properties.IsDevelopmentEnvironment ? "bfa8eee21e5571480f66888debf50534" : "1268a0d0435face20e77bf22465eb438" %>", "/misc/xd_receiver.html"); /*TEST*/

//			var contentHeight = document.getElementById("ContentDiv").offsetHeight;
//			if (contentHeight < 600)
//				FB.CanvasClient.setCanvasHeight("1000px");
//			else
//				FB.CanvasClient.setCanvasHeight((contentHeight + 400) + "px");

//		});
		
	</script>
	<script>
		//document.getElementById("Body").className = "BodyClass BackgroundColorLight";
		eval(RunAtEndOfPage);
		for (i = 0; i < RunAtEndOfPageFunctions.length; i++)
		{
			RunAtEndOfPageFunctions[i].call();
		}
	</script>
	</body>
	
</html>
