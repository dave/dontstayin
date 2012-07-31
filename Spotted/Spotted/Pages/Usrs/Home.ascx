<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Home.ascx.cs" Inherits="Spotted.Pages.Usrs.Home" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<%@ Register TagPrefix="DsiControl" TagName="GalleriesBySpotter" Src="/Controls/GalleriesBySpotter.ascx" %>

<asp:Panel Runat="server" ID="PanelNoDetails">
	<dsi:h1 runat="server">No Details</dsi:h1>
	<div class="ContentBorder">
		<p>
			This user has been added to the site, but hasn't filled in 
			all their details yet. 
			You'll be able to see their profile as soon as they've 
			visited the site to complete their registration.
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelDetails">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td valign="top" width="100%">
				<dsi:h1 runat="server" ID="H11" NAME="H11">Hi, I'm <%= HttpUtility.HtmlEncode(ThisUsr.NickName) %></dsi:h1>
				<div class="ContentBorder">
					<table cellpadding="0" cellspacing="0" border="0" width="100%">
						<tr>
							<td valign="top" style="padding-right:7px;padding-top:5px;padding-bottom:5px;" runat="server" id="PicCell">
								<a href="" runat="server" id="PicAnchor"><img src="" runat="server" id="PicImg" border="0" width="100" height="100" class="BorderBlack All"></a>
							</td>
							<td width="100%" valign="top">
								
								<p runat="server" id="NewUserIconPanel">
									<img src="/gfx/icon-newuser.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">I'm a new user - 
									I've been signed up less than 2 months
								</p>
								
								<p runat="server" id="AdminIconPanel">
									<img src="/gfx/icon-admin.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">I'm an admin
								</p>
								
								<p runat="server" id="EventModeratorIconPanel">
									<img src="/gfx/icon-admin.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">I'm an event moderator
								</p>
								
								<p runat="server" id="ModeratorIconPanel">
									<img src="/gfx/icon-admin.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">I'm a photo moderator
								</p>
																
								<p runat="server" id="DiscussionModeratorIconPanel">
									<img src="/gfx/icon-admin.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">I'm a chat moderator
								</p>
								
								<p runat="server" id="SuperIconPanel">
									<img src="/gfx/icon-super.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">I'm a superuser!
								</p>

								<p runat="server" id="DjIconPanel">
									<img src="/gfx/icon-deck.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">I'm a DJ
								</p>
								
								<p runat="server" id="SpotterIconPanel">
									<a runat="server" id="SpotterLink"><img src="/gfx/icon-spotter.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21" runat="server" id="SpotterIcon"><asp:Label runat="server" ID="SpotterLabel"></asp:Label></a>
									- I've spotted <asp:Label Runat="server" ID="SpotterSpottingsLabel"/>.
								</p>
								
								<p runat="server" id="ExtraIconPanel">
									<a href="/popup/questionairre"><img src="/gfx/icon-thumbs-up.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">I've helped DontStayIn by completing the questionnaire!</a>
								</p>
								
								<p runat="server" id="TicketIconPanel">
									<a href="/uk/london/clapham-common/2006/aug/26/event-46716"><img src="/gfx/icon-tickets.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">I've got tickets for South West Four 2006!</a>
								</p>
								
								<p runat="server" id="ChatterboxIconPanel">
									<img src="/gfx/icon-chatter.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">I'm a chatterbox!
								</p>
								
								<p runat="server" id="DsiRegularIconPanel">
									<img src="/gfx/icon-regular.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">I'm a DontStayIn regular!
								</p>
								
								<p runat="server" id="DonationIconTopP" />
								
								<p runat="server" id="DonationIconLegendP" visible="false">
									<img src="/gfx/don/legend.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21" />LEGEND!
								</p>
								
								<asp:Panel Runat=server ID=OtherUsrPanel>
									<p>
										<script>
											DbButton(
												"/gfx/icon-star-26-up.png",
												"/gfx/icon-star-26-dn.png",
												"<%= ThisUsr.NickName %> has been added to your buddy list",
												"<%= ThisUsr.NickName %> is not on your buddy list",
												"Remove <%= ThisUsr.NickName %> from my buddy list",
												"Add <%= ThisUsr.NickName %> to my buddy list",
												"",
												"cursor:pointer;margin-right:3px;",
												"absmiddle",
												26,21,
												"Buddy",
												"<%= ThisUsr.K %>",
												<%= BuddyButtonState %>,
												"BuddyButton",
												"BuddyButtonReturn",
												"",
												"");
											function BuddyButtonReturn(id,oldState,newState)
											{
												DbButtonSetState("BuddyInviteButton",newState);
											}
										</script>
									</p>
									<p>
										<script>
											DbButton(
												"/gfx/icon-inbox-up.png",
												"/gfx/icon-inbox-dn.png",
												"<%= ThisUsr.NickName %> can invite you to chat topics",
												"You have stopped <%= ThisUsr.NickName %> inviting you to chat topics",
												"Stop <%= ThisUsr.NickName %> inviting me in bulk to chat topics",
												"Allow <%= ThisUsr.NickName %> to invite me in bulk to chat topics",
												"",
												"cursor:pointer;margin-right:3px;",
												"absmiddle",
												26,21,
												"BuddyChatInvite",
												"<%= ThisUsr.K %>",
												<%= BuddyInviteButtonState %>,
												"BuddyInviteButton",
												"BuddyInviteButtonReturn",
												"",
												"");
											function BuddyInviteButtonReturn(id,oldState,newState)
											{
												if (newState)
													DbButtonSetState("BuddyButton",true);
											}
										</script>
									</p>
									<p>
										<a href="" runat="server" id="GroupInviteAnchor"><img src="/gfx/icon-group.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Invite xxx to join a group</a>
									</p>
									<p runat="server" id="UsrChatArchiveAnchorP">
										<a href="" runat="server" id="UsrChatArchiveAnchor"><img src="/gfx/icon-me-up.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">xxx</a>
									</p>
									<p>
										<a href="<%= ThisUsr.UrlBuddies() %>"><img src="/gfx/icon-me-up.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">View <%= HttpUtility.HtmlEncode(ThisUsr.NickName) %>'s buddies</a>
									</p>
								</asp:Panel>
							</td>
						</tr>
					</table>
				</div>
			</td>
			<td style="padding-left:10px;" runat="server" id="ChatCell" valign="top" width="178">
				<dsi:h1 runat="server" ID="H15" NAME="H15">I'm <%= ThisUsr.ChattingNow ? "chatting now" : "online" %></dsi:h1>
				<div class="ContentBorder" style="width:150px;">
					<p>
						<a href="" onclick="chatClientPinRoom('<%= new Bobs.Chat.RoomSpec(Usr.Current == null ? 0 : Usr.Current.K, ThisUsr.K).Guid.Pack() %>', '#<%= ChatCell.ClientID %>', true);return false;"><img src="<%= ThisUsr.ChattingNow ? "/gfx/icon-chatting.png" : "/gfx/icon-me-up.png" %>" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;" />I'm <%= ThisUsr.ChattingNow ? "chatting now" : "online" %></a>
					</p>
					<p>
						You can private chat to your buddies in the chat box on the right.
					</p>
					<p>
						<a href="" onclick="chatClientPinRoom('<%= new Bobs.Chat.RoomSpec(Usr.Current == null ? 0 : Usr.Current.K, ThisUsr.K).Guid.Pack() %>', '#<%= ChatCell.ClientID %>', true);return false;">Click to live chat with <%= ThisUsr.NickName %><img src="/gfx/chatclient-pinup-selected-onyellow.gif" style="margin-left:3px;" border="0" width="9" height="8" /></a>
					</p>
				</div>
			</td>
		</tr>
	</table>
	
	<div runat="server" id="uiDonationIconsPanel">
		<dsi:h1 runat="server" ID="H1">My donations...</dsi:h1>
		<div class="ContentBorder">
			<p>
				<asp:Literal runat="server" ID="uiDonationIconsHtml"></asp:Literal>
			</p>
			<p runat="server" id="DonationRolloverP">
				<img src="" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21" runat="server" id="DonationRolloverImage">
				<asp:DropDownList runat="server" ID="DonationRolloverDrop" AutoPostBack="true" CausesValidation="false" OnSelectedIndexChanged="DonationRolloverDrop_Change"></asp:DropDownList>
				- show this icon when people mouse-over my name.
			</p>					
		</div>
	</div>
	
	<asp:UpdatePanel runat="server" ID="PersonalStatementUpdatePanel">
		<ContentTemplate>
			<asp:Panel runat="server" id="EditPersonalStatementPanel">
				<dsi:h1 runat="server" ID="H14">Tell us a bit about yourself...</dsi:h1>
				<div class="ContentBorder">
					<p>
						<dsi:Html runat="server" id="PersonalStatementHtml" OnSave="PersonalStatementSave"></dsi:Html>
					</p>
				</div>
			</asp:Panel>
			
			<asp:PlaceHolder runat="server" ID="PersonalStatementPlainPh"/>
			
			<asp:Panel runat="server" id="PersonalStatementPanel">
				<dsi:h1 runat="server" ID="H16" NAME="H16">A bit about <%= HttpUtility.HtmlEncode(ThisUsr.NickName) %></dsi:h1>
				<div class="ContentBorder" style="width:600px;overflow:hidden;">
					<asp:PlaceHolder runat="server" id="PersonalStatementPh"></asp:PlaceHolder>
					
					<p>
						I've posted <asp:Label Runat="server" ID="UsrCommentsLabel"/>, <asp:Label Runat="server" ID="UsrChatLabel"/><asp:Label Runat="server" ID="UsrPhotoLabel"/>
					</p>
					
					<asp:Panel Runat="server" ID="UsrMusicTypesPanel" EnableViewState="False">
						<p>
							Music I listen to: <asp:Label Runat="server" ID="UsrMusicTypesLabel"></asp:Label>
						</p>
					</asp:Panel>

					<asp:Panel Runat="server" ID="UsrPlaceVisitPanel" EnableViewState="False">
						<p>
							Places I visit: <asp:Label Runat="server" ID="UsrPlaceVisitLabel"></asp:Label>
						</p>
					</asp:Panel>

				</div>
			</asp:Panel>
		</ContentTemplate>
	</asp:UpdatePanel>
	
	<asp:Panel Runat="server" ID="FavouriteGroupsPanel">
		<dsi:h1 runat="server">
			My favourite groups
		</dsi:h1>
		<div class="ContentBorder">
			<asp:DataList runat="server" ID="FavouriteGroupsDataList" ItemStyle-Width="50%" ItemStyle-VerticalAlign="Top" RepeatLayout="Table" RepeatColumns="2" RepeatDirection="Horizontal"/>
		</div>
	</asp:Panel>
	
	<a name="PrivateMessage" />
	<asp:Panel Runat="server" ID="SendMessagePanel">
		<dsi:h1 runat="server" ID="H17" NAME="H17">Send <%= HttpUtility.HtmlEncode(ThisUsr.NickName) %> a private message</dsi:h1>
		<div class="ContentBorder">
			<asp:RequiredFieldValidator Runat="server" ControlToValidate="AddThreadSubjectTextBox" Display="Dynamic" ErrorMessage="<p>Please enter a subject for your message</p>" ID="Requiredfieldvalidator1" NAME="Requiredfieldvalidator1"/>
			<asp:RequiredFieldValidator Runat="server" ControlToValidate="AddThreadCommentHtml" Display="Dynamic" ErrorMessage="<p>Please enter a message</p>" ID="Requiredfieldvalidator2"/>
			<p>
				Subject:<br>
				<asp:TextBox Runat="server" ID="AddThreadSubjectTextBox" AutoComplete="Off" style="border-width:1px;" class="BorderKeyline" TabIndex="100" />
			</p>
			<p>
				<dsi:Html runat="server" id="AddThreadCommentHtml" OnSave="AddThreadPostClick" PreviewType="Comment" DisableContainer="true" SaveButtonText="Send" TabIndexBase="101" Width="580" />
			</p>
			<asp:Panel Runat="server" ID="AddThreadLoginPanel">
				<p>
					To send a message you must first log on - use the links at the 
					top of the page to log on or create a free account.
				</p>
			</asp:Panel>
			<asp:Panel Runat="server" ID="AddThreadEmailVerifyPanel">
				<p>
					To post a comment you must <a href="/pages/emailverify">first verify your email address</a>.
				</p>
			</asp:Panel>
			<p runat="server" id="AddThreadAddBuddyP">
				<asp:CheckBox Runat="server" ID="AddThreadAddBuddy" TabIndex="122"></asp:CheckBox>
			</p>
			
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="PhotosMePanel" EnableViewState="False">
		<a name="PhotosMe"></a>
		<dsi:h1 runat="server" ID="H112" NAME="H112">Recent photos of me</dsi:h1>
		<div class="ContentBorder">
			<p class="CleanLinks">
				<asp:DataList runat="server" ID="PhotosMeDataList" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" CellPadding="5" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Top" Width="100%" ItemStyle-Width="33%" />
			</p>
			<asp:Panel Runat="server" ID="PhotosMeShowAllLinkPanel">
				<p class="BigCenter">
					<a href="<%Response.Write(ThisUsr.UrlMyPhotos());%>">Show all photos of <%Response.Write(HttpUtility.HtmlEncode(ThisUsr.NickName));%></a>
				</p>
			</asp:Panel>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="FavouritePhotoPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H12">My favourite photos</dsi:h1>
		<div class="ContentBorder">
			<p class="CleanLinks">
				<asp:DataList runat="server" ID="FavouritePhotoDataList" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" CellPadding="5" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Top" Width="100%"  ItemStyle-Width="33%"/>
			</p>
			<asp:Panel Runat="server" ID="FavouritePhotosShowAllLinkPanel">
				<p class="BigCenter">
					<a href="<%Response.Write(ThisUsr.UrlFavouritePhotos());%>">Show all <%Response.Write(HttpUtility.HtmlEncode(ThisUsr.NickName));%>'s favourite photos</a>
				</p>
			</asp:Panel>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="EventsAttendFuturePanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H113" NAME="H113">I'll be at these events</dsi:h1>
		<div class="ContentBorder">
			<p class="CleanLinks">
				<asp:DataList Runat="server" ID="EventsAttendFutureDataList" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="2" Width="100%" CellPadding="0" CellSpacing="0" ItemStyle-VerticalAlign="Top" ItemStyle-Width="50%" />
			</p>
		</div>
	</asp:Panel>

	<asp:Panel Runat="server" ID="EventsAttendedPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H114" NAME="H114">I was at these events</dsi:h1>
		<div class="ContentBorder">
			<p class="CleanLinks">
				<asp:DataList Runat="server" ID="EventsAttendedDataList" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="2" Width="100%" CellPadding="0" CellSpacing="0" ItemStyle-VerticalAlign="Top" ItemStyle-Width="50%" />
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="GalleriesPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H111">Recent galleries</dsi:h1>
		<div class="ContentBorder">
			<DsiControl:GalleriesBySpotter runat="server" ID="uiRecentGalleries"></DsiControl:GalleriesBySpotter>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" Visible="false" ID="UsrsSpottedPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H18">Recent spottings</dsi:h1>
		<div class="ContentBorder">
			<p runat="server" id="UsrsSpottedStatsP" align="center"/>
			<p class="CleanLinks">
				<style>
					.ForceNarrow
					{
						overflow:hidden;
						width:83px;
					}
				</style>
				<asp:DataList 
					Runat="server" 
					RepeatLayout="Table" 
					RepeatColumns="6"
					ID="UsrsSpottedDataList" 
					RepeatDirection="Horizontal"
					ItemStyle-HorizontalAlign="Center"
					ItemStyle-VerticalAlign="top"
					CellSpacing="10"
				/>
			</p>
			<asp:Panel Runat="server" ID="UsrsSpottedShowAllLinkPanel">
				<p class="BigCenter">
					<a href="<%= ThisUsr.UrlSpottings() %>">Show everyone <%= HttpUtility.HtmlEncode(ThisUsr.NickName) %> has spotted</a>
				</p>
			</asp:Panel>
			<p runat="server" id="UsrsSpottedFootnoteP" align="center"/>
		</div>
	</asp:Panel>
	

	
</asp:Panel>
<asp:Panel Runat="server" ID="PanelBan">
	<dsi:h1 runat="server" ID="H19" NAME="H18">Ban this user</dsi:h1>
	<div class="ContentBorder">
		<p>
			As a chat moderator, you can ban people that abuse the DontStayIn 
			chat forums. Be careful when banning someone - we fully investigate 
			each time someone is banned. If you don't have a valid reason for
			banning them, we will permanently remove your moderator status.
		</p>
		<p>
			BEFORE you ban anyone, please read the current banning rules
			<a href="/chat/k-60331">in this PM</a>. If you're 
			unsure, ask first.
		</p>
		<p>
			If you want to ban this person, please enter a short description 
			of why they are being banned in the box below, and click "Ban".
		</p>
		<p>
			<asp:TextBox Runat="server" ID="BanReasonTextBox" MaxLength="50" Columns="50"></asp:TextBox>
			<asp:Button Runat="server" ID="BanButton" OnClick="Ban_Click" Text="Ban" CausesValidation="False"></asp:Button>
		</p>
	</div>
</asp:Panel>
