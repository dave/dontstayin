<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewMenu.ascx.cs" Inherits="Spotted.Controls.Navigation.NewMenu" %>
<%@ Register TagPrefix="Navigation" TagName="Admin" Src="~/Controls/Navigation/Admin.ascx" %>
<div 
	runat="server" 
	id="OuterDiv" 
	style="position:absolute; top:0px; left:0px; width:978px;" 
	class="BackgroundBlack ClearAfter TopNavigationBarOuter" 
	xonmouseover="ShowMenuAfterDelay();" 
	xonmouseout="HideMenuAfterDelay();"
	>
	<!--<a href="/" class="HomeLink" style="margin-left:0px;" onclick="window.location = '/';"><img src="/gfx/logo-white-100-26-long.png" width="122" height="26" border="0" style="position:absolute; top:4px; left:5px;" xonclick="DsiPageHideLoginNew();" /></a>-->
	<div 
		style="width:<%= Vars.FacebookCanvasMode ? "300px" : "400px" %>;" 
		class="TopNavigationBar CleanLinks"
		>
		<div class="TopNavigationBarInner1">
			<div class="TopNavigationBarInner2" style="padding-left:2px;">
				<a href="/" runat="server" id="A2">Home</a>
				<a href="/pages/inbox" runat="server" id="TopBarInboxLink">Inbox</a>
				<span id="CalendarSpan"><a href="<% = Usr.Current == null ? "/pages/findevents/mode-calendar" : "/pages/mycalendar" %>">Calendar</a></span>
				<span id="FaqSpan"><a href="/pages/faq">FAQ</a></span>
				<a href="/" onclick="this.style.display='none';<%= Vars.FacebookCanvasMode ? "document.getElementById('CalendarSpan').style.display='none';document.getElementById('FaqSpan').style.display='none';" : ""%>document.getElementById('NewMenuSearchHolder').style.display='';document.getElementById('<% = SearchTextBox.ClientID %>').focus();return false;" id="NewMenuSearchLink">Search</a>
				<span id="NewMenuSearchHolder" style="display:none; padding-top:-5px; position:relative; margin-left:7px;">
					<asp:TextBox onkeydown="return newMenuSearchKeyDown(event);" runat="server" ID="SearchTextBox" style="height:16px; font-size:10px; margin:0px; padding:0px; position:relative; top:-2px; font-family:Verdana Arial Sans-Serif;" CssClass="ForegroundBlack"></asp:TextBox>
					<button onclick="newMenuSearchNow();return false;" runat="server" style="font-size:10px; height:20px; margin:0px; padding:0px; position:relative;top:-1px;" class="ForegroundBlack">Search</button>
					<script>
						function newMenuSearchKeyDown(event)
						{
							if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13))
							{
								newMenuSearchNow();
								return false;
							}
							else
								return true; 
						}
						function newMenuSearchNow()
						{
							var searchString = document.getElementById("<% = SearchTextBox.ClientID %>").value;
							searchString = encodeURI(searchString).replace(/'/g, "\\'");
							
							window.location = 'http://www.google.co.uk/search?q=site:dontstayin.com+' + searchString;
						}
						if (document.getElementById("<% = SearchTextBox.ClientID %>").value != "")
						{
							document.getElementById('NewMenuSearchLink').style.display = 'none';
							document.getElementById('NewMenuSearchHolder').style.display = '';
							document.getElementById('<% = SearchTextBox.ClientID %>').focus();
						}
						
					</script>
				</span>
			</div>
		</div>
	</div>
	
	<div 
		runat="server" 
		id="QuickLinksLinkHolder" 
		style="left:385px; width:120px;"
		class="TopNavigationBar CleanLinks"
		>
		<div class="TopNavigationBarInner1">
			<div class="TopNavigationBarInner2">
				
			</div>
		</div>
	</div>

	<div 
		runat="server" 
		id="Div3" 
		style="right:0px; width:600px;" 
		class="TopNavigationBar CleanLinks"
		>
		<div class="TopNavigationBarInner1" style="text-align:right;">
			<div class="TopNavigationBarInner2" style="text-align:right; padding-right:12px; right:0px;">
				<a href="/" runat="server" id="MenuLink" onclick="ToggleMenuNow();return false;" onmouseover="document.getElementById('MenuArrow').src='/gfx/menu-arrow-black.png';" onmouseout="document.getElementById('MenuArrow').src='/gfx/menu-arrow.png';" xstyle="padding-right:3px;">
					<img src="/gfx/menu-arrow.png" border="0" width="9" height="6" style="margin-bottom:2px; margin-right:2px; margin-left:3px;" id="MenuArrow" />MENU
				</a>
				<a href="/pages/spotters">Spotters</a>
				<a href="/pages/promoters/home">Promoters</a>
			
				<span runat="server" id="TopBarMyAccountLinkHolder"><a href="/pages/myaccount" runat="server" id="TopBarMyAccountLink" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">My account</a></span>
				<span runat="server" id="TobBarProfileLinkHolder"><a href="" runat="server" id="TobBarProfileLink"></a></span>

				<a runat="server" id="LogInLink" href="/pages/login" onclick="try { ConnectButtonClick(); } catch(ex) { } return false;">Log in</a>
				<a runat="server" id="SignUpLink" href="/pages/login" onclick="try { ConnectButtonClick(); } catch(ex) { } return false;">Sign up FREE</a>
				<a runat="server" id="LogOutLink" href="/pages/login" onclick="try { ConnectButtonClick(); } catch(ex) { } return false;">Log out</a>

			</div>
		</div>
	</div>
	
	<div style="height:33px;">&nbsp;</div>
	
	<div style="margin:0px 20px 15px 20px; width:904px; display:none; position:relative;" 
		 runat="server" id="BigMenu" class="ClearAfter">
		<div style="width:440px; float:left;" runat="server" id="BigMenuDiv1">
			<h2>
				Messages
			</h2>
			<p>
				<a href="/pages/inbox">Inbox</a> - 
				<a href="/pages/favourites">Favoutite topics</a> - 
				<a href="/" runat="server" id="MyCommentsLink">My comments</a>
			</p>
			<p>
				<a href="/pages/clearmyinbox">Clear my inbox</a> - <a href="/pages/inbox">Send a private message</a>
			</p>
			
			<h2>
				Calendars
			</h2>
			<p>
				<a href="/" runat="server" id="MyCalendarLink">My calendar</a> - suggests events based on your area / favourite music.
			</p>
			<p>
				<a href="/" runat="server" id="BuddyCalendarLink">Buddy calendar</a> - shows where you buddies are going.
			</p>
			
			<asp:Panel runat="server" Visible="false">
			<h2>
				Favourite photos
			</h2>
			<p>
				<asp:PlaceHolder runat="server" ID="FavouritePhotosPlaceholder" />
			</p>
			<p>
				<a href="/" runat="server" id="FavouritePhotosLink">Show all my favourite photos</a>
			</p>			
			</asp:Panel>
			
			<h2>
				Places I visit
			</h2>
			<p runat="server" id="PlacesHolder">
				<asp:PlaceHolder runat="server" ID="PlacesPlaceholder" />
			</p>
			<p>
				<a href="/pages/placesivisit">Change which places I visit</a> - <a href="/pages/countries/places">List all places in <asp:Label runat="server" ID="HomeCountryLabel">UK</asp:Label></a>
			</p>
			<p>
				My home country is <a href="/uk" runat="server" id="HomeCountryLink">UK</a> - <a href="/pages/countries/list">List all countiries</a>
			</p>
			
			<h2>
				Contribute
			</h2>
			<p>
				<a href="/pages/uploadphotos">Add a gallery</a> - <a href="/pages/mygalleries">My galleries</a>
			</p>
			<p>
				<a href="/pages/myarticles/mode-add">Add an article</a> - <a href="/pages/myarticles/mode-list">My articles</a>
			</p>
			<p>
				<a href="/pages/events/edit">Add an event</a> - <a href="/pages/myevents">Events I've added</a>
			</p>
			<p>
				<a href="/pages/venues/edit">Add a venue</a> - <a href="/pages/myvenues">Venues I've added</a>
			</p>

		</div>
		<div style="width:440px; float:left;" runat="server" id="BigMenuDiv2">
			<h2>
				Online buddies
			</h2>
			<p>
				<asp:PlaceHolder runat="server" ID="OnlineBuddiesPlaceholder" />
			</p>
			<p>
				<a href="/pages/online">Show a list of all <asp:Label runat="server" ID="NumberOnlineLabel" /> online members</a>
			</p>
			
			<h2>
				Favourite groups
			</h2>
			<p runat="server" id="FavouriteGroupsHolder">
				<asp:PlaceHolder runat="server" ID="FavouriteGroupsPlaceholder" />
			</p>
			<p runat="server" id="NoFavouriteGroupsHolder">
				You don't have any favourite groups right now... Click the star icon on your favourite groups to list them here for easy access.
			</p>
			<p>
				<a href="/pages/mygroups">My groups</a> - <a href="/groups">List of all groups</a> - <a href="/pages/groups/edit">Create a group</a>
			</p>
			
			<h2>
				Tickets
			</h2>
			<p>
				<a href="" runat="server" id="MyTicketsLink">My tickets</a> - <!--<a href="/pages/findtickets">Find tickets</a> ---> <a href="/hottickets">Hot tickets</a> - <a href="/pages/ticketscalendar#Day<%= DateTime.Now.ToString("yyyyMMdd") %>">Suggested tickets</a>
			</p>
			
			<h2>Other</h2>
			<p>
				<a href="/pages/contact">Contact Don't Stay In</a> - <a href="/pages/prospotters">Pro spotters</a> - <a href="/pages/top/photos">Front page photos</a>
			</p>
		</div>
		
		<Navigation:Admin runat="Server" ID="Admin" EnableViewState="False"/>
		
		<script>
			var prevState = new Object();
			var showMenuId = 0;
			var hideMenuId = 0;
			function ToggleMenuNow()
			{
				if (document.getElementById("<%= BigMenu.ClientID %>").style.display == "")
				{

					showMenuId++;
					HideMenuNow(hideMenuId);
				}
				else
				{
					hideMenuId++;
					ShowMenuNow(showMenuId);
				}
			}
			function ShowMenuAfterDelay()
			{
				hideMenuId++;
				setTimeout("ShowMenuNow(" + showMenuId + ")", 250);
			}
			function ShowMenuNow(showMenuIdFromRequest)
			{
				if (showMenuIdFromRequest == showMenuId && document.getElementById("<%= BigMenu.ClientID %>").style.display == "none")
				{
					document.getElementById("<%= BigMenu.ClientID %>").style.display = "";
					showHideNewMenu("ChatClientHolder", true);
					showHideNewMenu("ContentDiv", true);
					showHideNewMenu("NavAdminOuter", true);
					showHideNewMenu("LeaderboardOuterDiv", true);
					showHideNewMenu("BigLeaderboardOuterDiv", true);
					showHideNewMenu("TallMpuOuterDiv", true);
					showHideNewMenu("HotboxOuterDiv", true);
					showHideNewMenu("NavLoginDiv", true);
					<%= Vars.FacebookCanvasMode ? "showHideNewMenu('FacebookCanvasNavOuterDiv', true);" : ""%>
				}
			}
			function HideMenuAfterDelay()
			{
				showMenuId++;
				setTimeout("HideMenuNow(" + hideMenuId + ")", 500);
			}
			function HideMenuNow(hideMenuIdFromRequest)
			{
				if (hideMenuIdFromRequest == hideMenuId && document.getElementById("<%= BigMenu.ClientID %>").style.display == "")
				{
					document.getElementById("<%= BigMenu.ClientID %>").style.display = "none";
					showHideNewMenu("ChatClientHolder", false);
					showHideNewMenu("ContentDiv", false);
					showHideNewMenu("NavAdminOuter", false);
					showHideNewMenu("LeaderboardOuterDiv", false);
					showHideNewMenu("BigLeaderboardOuterDiv", false);
					showHideNewMenu("TallMpuOuterDiv", false);
					showHideNewMenu("HotboxOuterDiv", false);
					showHideNewMenu("NavLoginDiv", false);
					<%= Vars.FacebookCanvasMode ? "showHideNewMenu('FacebookCanvasNavOuterDiv', false);" : ""%>
				}
			}
			function showHideNewMenu(name, hide)
			{
				if (document.getElementById(name) != null)
				{
					var menuHeight = document.getElementById("<%= BigMenu.ClientID %>").offsetHeight + 20;
					
					if (hide)
					{
						//prevState[name] = document.getElementById(name).style.display;
						//document.getElementById(name).style.display = "none";
						prevState[name] = document.getElementById(name).style.top;

						var top = document.getElementById(name).style.top;
						top = top.substr(0, top.length - 2);
						top = parseInt(top) + menuHeight;
						document.getElementById(name).style.top = top + "px";
					}
					else
					{
						//if (prevState[name] != null)
						//	document.getElementById(name).style.display = prevState[name];
						//else
						//	document.getElementById(name).style.display = "";

						if (prevState[name] != null)
							document.getElementById(name).style.top = prevState[name];
					}
				}
			}
			//setTimeout("ToggleMenuNow();", 1000);
		</script>
	</div>
	
</div>
