<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Pages.Groups.Home" %>
<%@ Register TagPrefix="Spotted" TagName="Latest" Src="/Controls/Latest.ascx" %>
<%@ Register TagPrefix="Spotted" TagName="EventList" Src="/Controls/EventList.ascx" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>

<asp:Panel Runat="server" ID="PanelPrivate">
	<dsi:h1 runat="server" ID="H13">Private</dsi:h1>
	<div class="ContentBorder">
		<p>
			This group is private. You must be invited to join before you can view the home-page.
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelGroup">
	<asp:Panel Runat="server" ID="MiscInfoPanel">
		<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td valign="top">
					<dsi:h1 ID="H1" runat="server"><asp:Label  runat="server" id="GroupName"/></dsi:h1>
					<div class="ContentBorder">
						<table cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td valign="top" style="padding-right:7px;" runat="server" id="GroupPicCell">
									<p><img src="" runat="server" id="GroupPicImg" class="BorderBlack All" width="100" height="100" /></p>
								</td>
								<td width="100%" valign="top">
									<p runat="server" id="PublicChatP">
										<a runat="server" id="PublicChatLink"><img src="/gfx/icon-discuss.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="PublicChatLinkLabel"></asp:Label></a>
									</p>
									<p runat="server" id="GroupChatP">
										<a runat="server" id="GroupChatLink"><img src="/gfx/icon-group.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="GroupChatLinkLabel"></asp:Label></a>
									</p>
									<p runat="server" id="CalendarP">
										<a href="" runat="server" id="CalendarLink"><img src="/gfx/icon-calendar.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="CalendarLinkLabel"></asp:Label> calendar</a>
									</p>
									<p runat="server" id="HotTicketsP" visible="false">
										<a href="" runat="server" id="HotTicketsLink"><img src="/gfx/icon-hottickets.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">Hot <asp:Label Runat="server" id="HotTicketsLinkLabel"></asp:Label> tickets</a>
									</p>
									<p runat="server" id="TicketsP" visible="false">
										<a href="" runat="server" id="TicketsLink"><img src="/gfx/icon-tickets.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="TicketsLinkLabel"></asp:Label> tickets calendar</a>
									</p>
									<p runat="server" id="FreeGuestlistP" visible="false">
										<a href="" runat="server" id="FreeGuestlistLink"><img src="/gfx/icon-freeguestlist.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="FreeGuestlistLinkLabel"></asp:Label> Free Guestlist calendar</a></a>
									</p>
									
									<asp:Panel Runat="server" ID="CommentAlertButtonPanel">
										<p>
											<script>
												DbButton(
													"/gfx/icon-eye-up.png",
													"/gfx/icon-eye-dn.png",
													"","",
													"Ignore new topics in <%= CommentAlertButtonDesc %>",
													"Watch all new topics in <%= CommentAlertButtonDesc %>",
													"",
													"cursor:pointer;margin-right:3px;",
													"absmiddle",
													26,21,
													"CommentAlert",
													"<%= CurrentGroup.K %>,15",
													<%= CommentAlertButtonState %>,
													"CommentAlertButton",
													"",
													"",
													"");
											</script>
										</p>
									</asp:Panel>
									
									<asp:Panel Runat="server" ID="InfoFavouriteGroupButtonPanel">
										<p>
											<script>
												DbButton(
													"/gfx/icon-star-26-up.png",
													"/gfx/icon-star-26-dn.png",
													"","",
													"Remove this group from your favourites",
													"Add this group to your favourites",
													"",
													"cursor:pointer;margin-right:3px;",
													"absmiddle",
													26,21,
													"FavouriteGroup",
													"<%= CurrentGroup.K %>",
													<%= InfoFavouriteGroupButtonState %>,
													"InfoFavouriteGroupButton",
													"",
													"",
													"");
											</script>
										</p>
									</asp:Panel>
									
									<p>
										<asp:Label Runat="server" ID="InfoNameLabel" /> has <asp:PlaceHolder Runat="server" ID="InfoMembersLinkPh"/>. 
										<span runat="server" id="PrivacySpan"/>
									</p>
									
									<asp:PlaceHolder Runat="server" ID="InfoModsPh"/>
									
									<p runat="server" id="InfoInviteP"/>
									
									<p runat="server" id="InfoMemberStatusP" style="font-weight:bold;"/>
									
									<p runat="server" id="InfoJoinLoggedOutP">
										<button onclick="document.getElementById('JoinGroupLoginTable').style.display='';">Join this group</button>
										
										<table width="300" cellspacing="15" id="JoinGroupLoginTable" style="display:none;">
											<tr>
												<td align="center" valign="top" width="50%">
													<p style="font-size:18px;font-weight:bold;">
														<a href="" onclick="DsiPageShowLoginNew();return false;">Log in</a>
													</p>
													<p>If you've already signed-up</p>
												</td>
												<td align="center" valign="top" width="50%">
													<p style="font-size:18px;font-weight:bold;">
														<a href="" onclick="DsiPageShowLoginNew();return false;">Sign up FREE!</a>
													</p>
													<p>If you've not used the site before</p>
												</td>
											</tr>
										</table>
									</p>
									
									<p runat="server" id="InfoJoinP">
										<button Runat="server" onserverclick="InfoJoinClick" ID="InfoJoinButton" CausesValidation="False" onclick="try { return WhenLoggedInHtmlButton(this); } catch(ex) { return false; }">Join this group</button>
										<button Runat="server" onserverclick="InfoInviteRejectClick" id="InfoInviteRejectButton" CausesValidation="False">Decline the invite</button>
									</p>
									<p runat="server" id="InfoLeaveP">
										<button Runat="server" onserverclick="InfoLeaveClick" id="InfoLeaveButton" CausesValidation="False">Exit this group</button>
									</p>
								</td>
							</tr>
						</table>
					</div>
				</td>
				<td runat="server" id="NextEventCell" style="padding-left:10px;" valign="top" width="150">
					<dsi:h1 runat="server" ID="H19">Next event...</dsi:h1>
					<div class="ContentBorder" align="center">
						<asp:DataList Runat="server" ID="NextEventDataList" CellPadding="0" CellSpacing="0" ItemStyle-HorizontalAlign="Center"/>
					</div>
				</td>
			</tr>
		</table>
	</asp:Panel>
	
	<asp:PlaceHolder Runat="server" ID="PlainHtmlPlaceHolder" EnableViewState="False"></asp:PlaceHolder>
		
	<asp:Panel Runat="server" ID="HtmlPanel">
		<dsi:h1 runat="server" ID="H11">What is <asp:Label runat="server" id="GroupName1"/>?</dsi:h1>
		<div class="ContentBorder" style="width:600px;overflow:hidden;">
			<asp:PlaceHolder Runat="server" ID="HtmlPlaceHolder" EnableViewState="False"></asp:PlaceHolder>
		</div>
	</asp:Panel>
	
	<div runat="server" ID="uiCompetitionPanel1" style="padding-bottom:10px" Visible="false"/>
		
	<asp:Panel Runat="server" ID="CaptionCompetitionPanel" Visible="false">
		<a name="comp1photos"></a>
		<dsi:h1 ID="H2" runat="server">Caption competition photos</dsi:h1>
		<div class="ContentBorder">
			<p align="center">
				<asp:DataList Runat="server" 
					ID="CaptionCompetitionPhotoDataList" RepeatColumns="4" RepeatLayout="Table" 
					RepeatDirection="Horizontal" Width="100%" ItemStyle-Width="25%" 
					ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" />
			</p>
		</div>
	</asp:Panel>
	
	<div runat="server" ID="uiCompetitionPanel2" style="padding-bottom:10px" Visible="false"/>

	<asp:Panel Runat="server" ID="GroupPhotoPanel">
		<a name="topphotos"></a>
		<dsi:h1 runat="server" ID="uiTopPhotosHeader">Top photos</dsi:h1>
		<div runat="server" class="ContentBorder" id="uiTopPhotosDiv">
			<p align="center">
				<asp:DataList Runat="server" 
					ID="GroupPhotoDataList" RepeatColumns="4" RepeatLayout="Table" 
					RepeatDirection="Horizontal" Width="100%" ItemStyle-Width="25%" 
					ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"/>
			</p>
			<p align="center" runat="server" id="GroupPhotoArchiveLinkP">
				<b><a href="<%= CurrentGroup.UrlGroupPhotos() %>" style="font-size:12px;">Top photos archive</a></b><br>
			</p>
		</div>
	</asp:Panel>

	<div runat="server" ID="uiCompetitionPanel3" style="padding-bottom:10px" Visible="false"/>
	
	<asp:PlaceHolder Runat="server" ID="CaptionHtmlPlaceHolder" EnableViewState="False"></asp:PlaceHolder>
	
	<asp:Panel Runat="server" ID="GroupPhotoModPanelPanel" Visible="False">
		<dsi:h1 ID="H3" runat="server">Did you know?</dsi:h1>
		<div class="ContentBorder">
			<p>
				You're the moderator of this group - so you can now choose the top 
				photos, and add a caption of your choice. They appear right here in 
				this box, just like on the home page of the site.
			</p>
			<p>
				Go to a photo page, and click <i>Add this to group top photos</i> 
				(under the photo).
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="CompetitionPhotoPanel" Visible="false">
		<a name="comp2photos"></a>
		<dsi:h1 ID="uiCompetitionPhotosHeader" runat="server">Latest 'ROCK OUT' photos</dsi:h1>
		<div class="ContentBorder">
			<p align="center">
				<asp:DataList Runat="server" 
					ID="CompetitionPhotosDataGrid" RepeatColumns="4" RepeatLayout="Table" 
					RepeatDirection="Horizontal" Width="100%" ItemStyle-Width="25%" 
					ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"/>
			</p>
			<p align="center">
				<b><a href="<%= CurrentGroup.Url() %>/photos" style="font-size:12px;">Browse all entries</a></b><br>
			</p>
		</div>
	</asp:Panel>
	
	<Spotted:Latest runat="server" ID="Latest" ParentObjectType="Group" Items="5" />
	
	
</asp:Panel>
