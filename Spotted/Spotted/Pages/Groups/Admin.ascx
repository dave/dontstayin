<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin.ascx.cs" Inherits="Spotted.Pages.Groups.Admin" %>
<%@ Register TagPrefix="uc1" TagName="MultiBuddyChooser" Src="~/Controls/MultiBuddyChooser.ascx" %>
<%@ Register src="/Controls/Picker.ascx" tagname="Picker" tagprefix="uc1" %>
<asp:Panel Runat="server" ID="PanelOptions">
	<dsi:GroupIntro runat="server" ID="GroupIntro" Header="Group options">
		<p>
			<img src="/gfx/icon-group.png" border="0" align="absmiddle" style="margin-right:3px;">Your 
			member options for the <a href="" runat="server" id="OptionsGroupAnchor"></a> group:
		</p>
		<p runat="server" id="OptionsMenuP"/>
		<p runat="server" id="EditOptionsP"/>
		<p runat="server" id="OptionsInviteP" EnableViewState="false"/>
	</dsi:GroupIntro>
	<asp:Panel Runat="server" ID="BuddiesPanel">
		<dsi:h1 runat="server" ID="H112">Invite my buddies</dsi:h1>
		<div class="ContentBorder">
			<p>
				You can invite your buddies to this group by selecting them below. Please write a few words of introduction in the box below:
			</p>
			<p runat="server" id="BuddiesOutputP" EnableViewState="false"></p>
			<h2>Intro message:</h2>	
			<p>
				<asp:TextBox Runat="server" ID="BuddiesIntroTextBox" Columns="80" Rows="5" TextMode="MultiLine"></asp:TextBox>
			</p>
			<asp:CustomValidator Display="Dynamic" Runat="server" ID="BuddiesIntroVal" EnableClientScript="False" OnServerValidate="BuddiesIntro_Val"
				ErrorMessage="<p>Please enter an intro message here - enter between 10 and 500 characters, and please don't use HTML code.</p>"/>
			<h2>Buddies:</h2>
			<p>
				<uc1:MultiBuddyChooser runat="server" ID="uiMultiBuddyChooser" />
			</p>
			<p>
				<asp:Button ID="Button1" Runat="server" OnClick="Buddies_Click" Text="Invite your buddies"/>
			</p>
			
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="EmailPanel">
		<dsi:h1 runat="server" ID="H111">Invite people by email address</dsi:h1>
		<div class="ContentBorder">
			<p>
				Enter email addresses of people you want to invite to your group in the box 
				below. Make sure there's one email address per line.
			</p>
			<p>
				We'll send them all an invitation to your group. Please write a few words of introduction in the box below:
			</p>
			
			<h2>Intro message:</h2>	
			<p runat="server" id="EmailOutputP" EnableViewState="false"></p>
			<p>
				<asp:TextBox Runat="server" ID="EmailIntroTextBox" Columns="80" Rows="5" TextMode="MultiLine"></asp:TextBox>
			</p>
			<asp:CustomValidator Display="Dynamic" Runat="server" ID="EmailIntroVal" EnableClientScript="False" OnServerValidate="EmailIntro_Val"
				ErrorMessage="<p>Please enter an intro message here - enter between 10 and 500 characters, and please don't use HTML code.</p>"/>
			<h2>Emails:</h2>
			<p>
				<b>
					This page is only designed for small batches of email addresses. If you have more than 
					1,000 emails, please split them into batches of about 500.
				</b>
			</p>
			<p>
				<asp:TextBox Runat="server" ID="EmailTextBox" Columns="50" Rows="30" TextMode="MultiLine"></asp:TextBox>
			</p>
			<p>
				<asp:Button Runat="server" OnClick="Email_Click" Text="Invite now" ID="Button3" NAME="Button3"/>
			</p>
			<p>
				If you have more than 10,000 addresses, we can send them for you. Send an email 
				to <img src="/gfx/dave-email.gif" align="absbottom" width="120" height="10">, and be sure 
				to include:
			</p>
			<p>
				1) Link to the group you're inviting to<br>
				2) Link to the profile page of the user doing the invite<br>
				3) Intro message<br>
				4) Email addresses in an attachment
			</p>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="OwnerPanel">
		<dsi:h1 runat="server" ID="H16">Moderator options - owner admin</dsi:h1>
		<div class="ContentBorder">
			<h2>Group details</h2>
			<p>
				Click the link below to edit the group details - e.g. name, description, picture, location etc.
			</p>
			<p class="BigCenter">
				<a href="<%= CurrentGroup.UrlApp("edit") %>">edit group details</a>
			</p>
			<h2>Adjust moderator permissions</h2>
			<p>
				<asp:DataGrid Runat="server" ID="OwnerModeratorsDataGrid" 
					GridLines="None" AutoGenerateColumns="False"
					BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					AlternatingItemStyle-CssClass="dataGridAltItem"
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Middle" AllowPaging="True" OnPageIndexChanged="OwnerModeratorsDataGridChangePage"
					PageSize="20" PagerStyle-Mode="NumericPages" PagerStyle-CssClass="dataGridFooterGrey">
					<Columns>
						<asp:TemplateColumn HeaderText="Nickname">
							<ItemTemplate>
								<a href="<%#((Bobs.GroupUsr)(Container.DataItem)).Usr.Url()%>" <%#((Bobs.GroupUsr)(Container.DataItem)).Usr.Rollover%>><%#((Bobs.GroupUsr)(Container.DataItem)).Usr.NickName%></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Moderator" ItemStyle-Width="60px">
							<ItemTemplate>
								<asp:CheckBox ID="CheckBox1" Runat="server"
									OnDataBinding="OwnerModeratorsDataGridCheckBoxDataBindMod"/>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="News" ItemStyle-Width="60px">
							<ItemTemplate>
								<asp:CheckBox ID="CheckBox2" Runat="server"
									OnDataBinding="OwnerModeratorsDataGridCheckBoxDataBindNews"/>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Members" ItemStyle-Width="60px">
							<ItemTemplate>
								<asp:CheckBox ID="CheckBox3" Runat="server"
									OnDataBinding="OwnerModeratorsDataGridCheckBoxDataBindMember"/>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Owner" ItemStyle-Width="60px">
							<ItemTemplate>
								<asp:CheckBox ID="CheckBox4" Runat="server"
									OnDataBinding="OwnerModeratorsDataGridCheckBoxDataBindOwner"/>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
			<p runat="server" id="OnwerModeratorError" style="color:red;font-weight:bold;" visible="false" enableviewstate="false">
				Update failed! You must have at least one owner selected!
			</p>
			<p runat="server" id="OnwerModeratorDone" style="color:blue;font-weight:bold;" visible="false" enableviewstate="false">
				Update done!
			</p>
			<p>
				<small>
					<b>Moderator</b><br>
					These people can moderate the group chat board - they can delete off-topic posts. 
					They can also select photos for the "top-photos" section.
					They can also select recommended events that are displayed on the group home-page (non party-brand groups only)<br>
				
					<b>News</b><br>
					These people can post news to the group. Each news post is sent by email to all members.<br>
					<b>Members</b><br>
					These people can ban people from the group, and reject or accept new membership applications.<br>
					<b>Owner</b><br>
					These people can add or delete moderators, and adjust the permissions.
				</small>
			</p>
			<p>
				<button id="Button2" runat="server" onserverclick="OwnerModeratorsUpdate">Update permissions</button>
			</p>
			<h2>Add a new moderator</h2>
			<p>
				To add a new moderator, select the user from the drop-down below and click "Add moderator"
			</p>
			<p>
				<js:HtmlAutoComplete ID="uiOwnerModeratorsAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetGroupMembers" Width="580px"/>
			</p>
			<p>
				<small>To use this special drop-down, type the first few letters of the nickname of the member in the box, 
				and a list of results should appear. This drop-down only shows members of this group. To add someone else, 
				you must first invite them to join the group by using the members admin section below.</small>
			</p>
			<p>
				<button runat="server" onserverclick="OwnerModeratorsAdd" ID="Button4">Add moderator</button>
			</p>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="MemberAdminPanel">
		<dsi:h1 runat="server" ID="H114">Email notifications</dsi:h1>
		<div class="ContentBorder">
			<p>
				<asp:CheckBox Runat="server" ID="MemberAdminNewUserEmailsCheckBox" Text="Send me an email when someone joins the group"
					OnCheckedChanged="MemberAdminNewUserEmailsCheckBox_Change" AutoPostBack="True"/>
			</p>
			<p runat="server" id="MemberAdminNewUserEmailsCheckBoxStatusPanel" style="color:blue;" EnableViewState="false" visible="false">
				Done.
			</p>
		</div>
		<dsi:h1 runat="server" ID="H15">Moderator options - members admin</dsi:h1>
		<div class="ContentBorder">
			<h2>Invite someone to this group</h2>
			<p>
				To invite someone to be a member of this group, select them from the 
				drop-down below and click "Invite"
			</p>
			<p>
				<js:HtmlAutoComplete runat="server" ID="uiMemberAdminInviteAutoComplete" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetUsrsPublic" Width="580px" />
			</p>
			<p>
				<small>To use this special drop-down, type the first few letters of the nickname of the member in the box, 
				and a list of results should appear.</small>
			</p>
			<p>
				<button id="Button5" runat="server" onserverclick="MemberInvite">Invite</button> 
				<asp:Label Runat="server" ID="MemberInviteLabel"/>
			</p>
		</div>
		<a name="MemberAdminRequests"></a>
		<dsi:h1 runat="server" ID="H18">New membership requests</dsi:h1>
		<div class="ContentBorder">
			<asp:Panel  runat="server" id="MemberAdminRequestsDataGridPanel">
				<p>
					<asp:DataGrid Runat="server" 
						ID="MemberAdminRequestsDataGrid" 
						GridLines="None" 
						AutoGenerateColumns="False"
						BorderWidth=0 
						CellPadding=3 
						CssClass=dataGrid 
						AlternatingItemStyle-CssClass="dataGridAltItem"
						HeaderStyle-CssClass="dataGridHeader" 
						SelectedItemStyle-CssClass="dataGridSelectedItem" 
						ItemStyle-VerticalAlign="Middle" 
						AllowPaging="True" 
						OnPageIndexChanged="MemberAdminRequestsDataGridChangePage"
						PageSize="40" 
						PagerStyle-Mode="NumericPages" 
						PagerStyle-CssClass="dataGridFooterGrey">
						<Columns>
							<asp:TemplateColumn HeaderText="Nickname">
								<ItemTemplate>
									<a href="<%#((Bobs.GroupUsr)(Container.DataItem)).Usr.Url()%>" <%#((Bobs.GroupUsr)(Container.DataItem)).Usr.Rollover%>><%#((Bobs.GroupUsr)(Container.DataItem)).Usr.NickName%></a>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Status">
								<ItemTemplate>
									<%#((Bobs.GroupUsr)(Container.DataItem)).MembershipAdminStatusHtml%>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Accept" ItemStyle-Width="60px">
								<ItemTemplate>
									<asp:CheckBox ID="CheckBox5" Runat="server"
										OnDataBinding="MemberAdminRequestsDataGridCheckBoxDataBindAccept"
										/><img src="/gfx/icon-tick-up.png" border="0" align="absmiddle" style="margin-right:3px;">
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Reject" ItemStyle-Width="60px">
								<ItemTemplate>
									<asp:CheckBox ID="CheckBox6" Runat="server"
										OnDataBinding="MemberAdminRequestsDataGridCheckBoxDataBindReject"
										/><img src="/gfx/icon-cross-up.png" border="0" align="absmiddle" style="margin-right:3px;">
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
				</p>
				<p>
					<button runat="server" onserverclick="MemberAdminRequestsUpdate_Click" ID="Button6">Update</button>
				</p>
			</asp:Panel>
			<p runat="server" id="MemberAdminRequestsNoResultsP">
				No results - nobody is waiting to be enabled
			</p>
			<p runat="server" id="MemberAdminRequestsUpdateResultsP"/>
		</div>
		<a name="MemberOptions"></a>
		<dsi:h1 runat="server" ID="H19">Member options</dsi:h1>
		<div class="ContentBorder">
			<h2>Search the group:</h2>
			<table cellpadding=0 cellspacing=0 border=0>
				<tr>
					<td valign=top style="padding-top:3px;padding-right:3px;">
						<p>
							Search for:
						</p>
					</td>
					<td>
						<p>
							<asp:CheckBox Runat="server" ID="MemberAdminOptionsMembersCheckBox" Text=" Members - <small>people who are now members of the group</small><br>"/>
							<asp:CheckBox Runat="server" ID="MemberAdminOptionsRequestCheckBox" Text=" Requested - <small>people that have requested membership / been recommended by a member</small><br>"/>
							<asp:CheckBox Runat="server" ID="MemberAdminOptionsInvitedCheckBox" Text=" Invited - <small>people who have been invited to join the group</small><br>"/>
							<asp:CheckBox Runat="server" ID="MemberAdminOptionsRejectedCheckBox" Text=" Rejected - <small>people who have had their membership request rejected</small><br>"/>
							<asp:CheckBox Runat="server" ID="MemberAdminOptionsExitedCheckBox" Text=" Exited - <small>people who have exited the group</small><br>"/>
							<asp:CheckBox Runat="server" ID="MemberAdminOptionsBarredCheckBox" Text=" Barred - <small>people who have been barred from the group</small>"/>
						</p>
					</td>
				</tr>
				<tr>
					<td>
						<p style="padding-right:3px;">
							Nickname:
						</p>
					</td>
					<td>
						<p>
							<asp:TextBox Runat="server" ID="MemberAdminOptionsSearchTextBox" Columns="20"/> <small>leave this blank to search all</small>
						</p>
					</td>
				</tr>
			</table>
			
			<p>
				<button id="Button7" runat="server" onserverclick="MemberAdminOptionsSearch_Click">Search</button>
				<asp:Label Runat="server" ID="MemberAdminOptionsSearchNoResults" Visible="False">No results</asp:Label>
				<asp:Label Runat="server" ID="MemberAdminOptionsSearchNoneSelected" Visible="False">Tick one or more of the status-level boxes above to search</asp:Label>
			</p>
			<p runat="server" id="MemberAdminOptionsDataGridP">
				<asp:DataGrid Runat="server" 
					ID="MemberAdminOptionsDataGrid" 
					GridLines="None" 
					AutoGenerateColumns="False"
					BorderWidth=0 
					CellPadding=3 
					CssClass=dataGrid 
					AlternatingItemStyle-CssClass="dataGridAltItem"
					HeaderStyle-CssClass="dataGridHeader" 
					SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Middle" 
					AllowPaging="True" 
					OnPageIndexChanged="MemberAdminOptionsDataGridChangePage"
					PageSize="20" 
					PagerStyle-Mode="NumericPages" 
					PagerStyle-CssClass="dataGridFooterGrey"
					OnItemCommand="MemberAdminOptionsDataGridItemCommand"
					OnItemDataBound="MemberAdminOptionsDataGridItemDataBound">
					<Columns>
						<asp:TemplateColumn HeaderText="Nickname">
							<ItemTemplate>
								<a href="<%#((Bobs.GroupUsr)(Container.DataItem)).Usr.Url()%>" <%#((Bobs.GroupUsr)(Container.DataItem)).Usr.Rollover%>><%#((Bobs.GroupUsr)(Container.DataItem)).Usr.NickName%></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Status">
							<ItemTemplate>
								<%#((Bobs.GroupUsr)(Container.DataItem)).MembershipAdminStatusHtml%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Options"/>
					</Columns>
				</asp:DataGrid>
			</p>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="NewsAdminPanel">
		<dsi:h1 runat="server" ID="H14">How to post news</dsi:h1>
		<div class="ContentBorder">
			<p>
				To post news, simply post a new topic in your group chat, and tick the "News" box in the advanced options panel.
			</p>
		</div>
		<a name="NewsDataGrid"></a>
		<dsi:h1 runat="server" ID="H17">News suggested by users</dsi:h1>
		<div class="ContentBorder">
		
			<p>
				Below is listed news that has been suggested by your group members. It's not news 
				yet, and will only become news if you enable it. Remember, if you enable something 
				as news, ALL of your group members will receive an email about it.
			</p>
			<p>
				If there are too many items for you to handle, you can disable all items by 
				<asp:LinkButton Runat="server" OnClick="NewsDisableAll" ID="NewsDisableAllLinkButton">clicking here</asp:LinkButton>.
			</p>
			<p>
				<asp:DataGrid Runat="server" ID="NewsDataGrid" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" ItemStyle-VerticalAlign=Top 
					width="100%">
					<Columns>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridTightImg" Visible=False>
							<ItemTemplate>
								<a href="<%#ContainerPage.Url.CurrentUrl("k",((Bobs.Thread)Container.DataItem).K.ToString())%>"><img src="<%#((Bobs.Thread)Container.DataItem).SimpleIconPath%>" align="top" border="0" class="LatestChatImage" hspace="0" width="30" height="30"></a></ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitles" ItemStyle-Width="100%">
							<ItemTemplate>
								<%#((Bobs.Thread)Container.DataItem).IconsHtml(ContainerPage.Url)%><%#((Bobs.Thread)Container.DataItem).CommentHtmlStart%><a href="<%#ContainerPage.Url.CurrentUrl("threadk",((Bobs.Thread)Container.DataItem).K.ToString())%>#NewsDataGrid"><%#HttpUtility.HtmlEncode(((Bobs.Thread)Container.DataItem).SubjectSafe)%></a><%#((Bobs.Thread)Container.DataItem).CommentHtmlEnd%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Author" ItemStyle-CssClass="dataGridThread">
							<ItemTemplate>
								<small>
									<%#((Bobs.Thread)Container.DataItem).AuthorHtml%>
								</small>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Replies&nbsp;/&nbsp;last" ItemStyle-CssClass="dataGridThread">
							<ItemTemplate>
								<small>
									<%#((Bobs.Thread)Container.DataItem).RepliesHtml%>
								</small>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Quick<br>disable" ItemStyle-CssClass="dataGridThread">
							<ItemTemplate>
								<script>
									DbButton(
										"/gfx/icon-cross-up.png",
										"/gfx/icon-cross-dn.png",
										"Disable","Disable",
										"",
										"",
										"",
										"margin-right:0px;",
										"absmiddle",
										26,21,
										"DisableGroupNews",
										"<%#((Bobs.Thread)Container.DataItem).K%>",
										"0",
										"TopDisableNews<%#((Bobs.Thread)Container.DataItem).K%>",
										"",
										"",
										"");
								</script>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
			<p runat="server" id="NewsPageP"/>
		</div>
		<asp:Panel Runat="server" ID="NewsThreadPanel">
			<a name="NewsThread"></a>
			<dsi:h1 runat="server" ID="H110">News suggested by users</dsi:h1>
			<div class="ContentBorder">
				<p>
					<asp:Repeater Runat="server" ID="NewsThreadRepeater"/>
				</p>
			</div>
		</asp:Panel>
	</asp:Panel>
	<asp:Panel Runat="server" ID="ModeratorPanel">
		<dsi:h1 runat="server" ID="H12">Moderator options</dsi:h1>
		<div class="ContentBorder">
			<p>
				You're a moderator of this group. This means you can delete comments that 
				don't comply to the posting rules. Just click the "Delete" link at the bottom-right
				of a comment to delete it. Take care when excersising this power, because 
				censorship is frowned upon by the DontStayIn community.
			</p>
			<p>
				You can change various properties of a topic by using the "Advanced options" page. 
				There's a link on the topic page in the "Options" panel. Using this page, you can 
				move a topic into a different forum - that will create a big link to the forum 
				object (e.g. event, venue or town) at the top of the topic page.
			</p>
		</div>
		<dsi:h1 runat="server" ID="H11">Top photos section</dsi:h1>
		<div class="ContentBorder">
			<P>
				You can choose the top photos, and add a caption of your choice. They 
				appear on the group home-page, just like on the home page of the site. 
			</P>
			<P>
				Go to a photo page, and click <I>Add this to group top photos</I> (under the 
				photo).
			</P>
		</div>
		<asp:Panel Runat="server" ID="ModeratorRecommendedPanel">
			<dsi:h1 runat="server" ID="H113">Recommended events</dsi:h1>
			<div class="ContentBorder">
				<h2>Add a recommended event</h2>
				<p>
					Recommended events show on the group home page. You'll also see a drop-down of these events in 
					the "Advanced" panel when you post a comment. Make sure you select the event if you're posting 
					about it.
				</p>
				<uc1:Picker id="uiEventPicker" runat="server" />
				<p>
					<asp:Button Runat="server" OnClick="ModeratorRecommendedAdd_Click" Text="Add" ID="Button8"></asp:Button>
				</p>
				<p runat="server" id="ModeratorRecommendedAddErrorP" EnableViewState="False" Visible="False" style="color:red;">
					Error - make sure you select an event from the drop-down above!
				</p>
				<p runat="server" id="ModeratorRecommendedAddDoneP" EnableViewState="False" Visible="False" style="color:blue;">
					Done - Your recommended event will appear on your group front-page.
				</p>
				<h2>Remove</h2>
				<p>
					If you've added an event in error, you can remove it here.
				</p>
				<p>
					<uc1:Picker id="uiRemoveEventPicker" runat="server" />
				</p>
				<p>
					<asp:Button Runat="server" OnClick="ModeratorRecommendedRemove_Click" Text="Remove" ID="Button9"></asp:Button>
				</p>
				<p runat="server" id="ModeratorRecommendedRemoveErrorP" EnableViewState="False" Visible="False" style="color:red;">
					Error - make sure you select an event from the drop-down above!
				</p>
				<p runat="server" id="ModeratorRecommendedRemoveDoneP" EnableViewState="False" Visible="False" style="color:blue;">
					Done - This event will no longer appear on your group front-page.
				</p>
				
			</div>
		</asp:Panel>
	</asp:Panel>
	<asp:Panel Runat="server" ID="MemberPanel">
		<dsi:h1 runat="server" ID="H13">Member options</dsi:h1>
		<div class="ContentBorder">
			<p>
				<asp:CheckBox Runat="server" ID="MemberFavouriteCheckBox" Text="This is one of my favourite groups"
					OnCheckedChanged="MemberFavouriteCheckBox_Change" AutoPostBack="True"/>
			</p>
		</div>
	</asp:Panel>
</asp:Panel>
