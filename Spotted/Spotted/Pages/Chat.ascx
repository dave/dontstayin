<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Chat.ascx.cs" Inherits="Spotted.Pages.Chat" %>
<%@ Register TagPrefix="AddThread" TagName="Thread" Src="~/Controls/AddThread.ascx" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="~/Controls/Html.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultiBuddyChooser" Src="~/Controls/MultiBuddyChooser.ascx" %>
<%@ Register TagPrefix="Controls" TagName="Article" Src="/Pages/Articles/HomeContent.ascx" %>

<asp:Panel Runat="server" ID="PanelForum">
	<h1 class="TabHolder">
		<a href="/chat" class="TabbedHeading Selected">Chat<!--<img src="/gfx/icon-discuss.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/inbox" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Inbox<!--<img src="/gfx/icon-inbox-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/favourites" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Favourites<!--<img src="/gfx/icon-star-26-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/watching" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Watching<!--<img src="/gfx/icon-eye-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="<%= Bobs.Usr.Current != null ? Bobs.Usr.Current.UrlMyComments():"/pages/mycomments" %>" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">My&nbsp;comments<!--<img src="/gfx/icon-me-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
	</h1>
	<div class="ContentBorder">
		<asp:Panel Runat="server" ID="PanelThreadDescTypeNone" EnableViewState="False">
			<p>
				<img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Showing all topics worldwide.
				<a href="" runat="server" id="ThreadDescWorldwideHomeCountryLink">We can restrict this to ???</a>.
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelThreadDescTypeCountry" EnableViewState="False">
			<p>
				<img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Showing all <a href="" runat="server" id="ThreadDescCountryLink"><asp:Label Runat="server" ID="ThreadDescCountryLabel"></asp:Label></a> topics.
				We can show <a href="/chat">topics worldwide</a>.
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelThreadDescTypeEvent" EnableViewState="False">
			<p>
				<img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Showing topics about
				<b><a href="" runat="server" id="ThreadDescEventEventLink"/></b> 
				@ <a href="" runat="server" id="ThreadDescEventVenueLink"/> 
				in <a href="" runat="server" id="ThreadDescEventPlaceLink"/>, 
				<asp:Label Runat="server" id="ThreadDescEventDateLabel"/>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelThreadDescTypeVenue" EnableViewState="False">
			<p>
				<img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Showing topics about
				<b><a href="" runat="server" id="ThreadDescVenueVenueLink"/></b> 
				in <a href="" runat="server" id="ThreadDescVenuePlaceLink"/>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelThreadDescTypePlace" EnableViewState="False">
			<p>
				<img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Showing topics about
				<b><a href="" runat="server" id="ThreadDescPlacePlaceLink"/></b>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelThreadDescTypeArticle" EnableViewState="False">
			<p>
				<img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Showing topics about
				<b><a href="" runat="server" id="ThreadDescArticleArticleLink"/></b>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelThreadDescTypeBrand" EnableViewState="False">
			<p>
				<img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Showing 
				all topics from <b><a href="" runat="server" id="ThreadDescBrandBrandLink"/></b> events
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelThreadDescTypeGroup" EnableViewState="False">
			<p>
				<img src="/gfx/icon-group.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Showing 
				all topics in the <b><a href="" runat="server" id="ThreadDescGroupGroupLink"/></b>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelThreadDescGroupBrandPanel" EnableViewState="False">
			<p>
				<img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Check 
				out the <b><a href="" runat="server" id="PanelThreadDescGroupBrandAnchor"></a></b> - 
				<asp:Label Runat="server" ID="PanelThreadDescGroupBrandCommentsLabel"></asp:Label> - 
				shows topics from events and photos
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelThreadDescBrandPanel" EnableViewState="False">
			<p>
				<img src="/gfx/icon-group.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Check 
				out the <b><a href="" runat="server" id="PanelThreadDescBrandGroupChatAnchor"></a></b> - 
				<asp:Label Runat="server" ID="PanelThreadDescBrandGroupChatCommentsLabel"></asp:Label> - 
				only shows topics posted to the regulars group
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="CommentAlertPanel">
			<p>
				<script>
					DbButton(
						"/gfx/icon-eye-up.png",
						"/gfx/icon-eye-dn.png",
						"","",
						"Ignore new topics in this <%= CommentAlertButtonGroupForumString %>",
						"Watch all new topics in this <%= CommentAlertButtonGroupForumString %>",
						"",
						"cursor:pointer;margin-right:3px;",
						"absmiddle",
						26,21,
						"CommentAlert",
						"<%= CommentAlertButtonArgs %>",
						<%= CommentAlertButtonState %>,
						"CommentAlertButton",
						"",
						"",
						"");
				</script>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="FavouriteGroupPanel">
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
						"<%= FavouriteGroupButtonGroupK %>",
						<%= FavouriteGroupButtonState %>,
						"InfoFavouriteGroupButton",
						"",
						"",
						"");
				</script>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelThreadDescRelatedPanel" EnableViewState="False">
			<p>
				We can show you topics from related events - check out <asp:PlaceHolder Runat="server" ID="PanelThreadDescRelatedPh"></asp:PlaceHolder>.
			</p>
		</asp:Panel>
		
	</div>
	
	<asp:Panel Runat="server" ID="NoThreadsPanel" EnableViewState="False">
		<dsi:h1 runat="server">No topics in here...</dsi:h1>
		<div class="ContentBorder">
			<p>Be the first to add one - see below!</p>
		</div>
	</asp:Panel>
	
	
	<asp:Panel Runat="server" ID="ThreadsPanel" EnableViewState="False">
		<a name="Threads"></a>
		<dsi:h1 runat="server">Topics</dsi:h1>
		<div class="ContentBorder">
			<p runat="server" id="ThreadsPageP" align="right">
				<asp:HyperLink runat="server" id="ThreadsPrevPageLink"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... <asp:HyperLink runat="server" id="ThreadsNextPageLink">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
			</p>
			<p>
				<dsi:InlineScript ID="InlineScript3" runat="server">
					<script>
						var i1="/gfx/icon-eye-up.png";
						var i2="/gfx/icon-eye-dn.png";
						var a1="Ignore this topic";
						var a2="Watch this topic";
						var l1="left";
						var s1="cursor:pointer;";
						var f1="WatchTopic";
						
						var i3="/gfx/icon-star-22-up.png";
						var i4="/gfx/icon-star-22-dn.png";
						var a3="Remove this topic from my favourites";
						var a4="Add this topic to my favourites";
						var l2="left";
						var s2="cursor:pointer;";
						var f2="FavouriteTopic";
					</script>
				</dsi:InlineScript>
				<asp:DataGrid Runat="server" ID="ThreadsDataGrid" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" ItemStyle-VerticalAlign=Top 
					width="100%">
					<Columns>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitlesTight">
							<ItemTemplate>
								<%#((Bobs.Thread)Container.DataItem).WatchingHtml("", ThreadsDataGrid)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitlesTight">
							<ItemTemplate>
								<%#((Bobs.Thread)Container.DataItem).FavouriteHtml(ThreadsDataGrid)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridTightImg" Visible=False>
							<ItemTemplate>
								<a href="<%#ContainerPage.Url.CurrentUrl("k",((Bobs.Thread)Container.DataItem).K.ToString())%>"><img src="<%#((Bobs.Thread)Container.DataItem).SimpleIconPath%>" align="top" border="0" class="LatestChatImage" hspace="0" width="30" height="30"></a></ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitles" ItemStyle-Width="100%">
							<ItemTemplate>
								<%#((Bobs.Thread)Container.DataItem).IconsHtml(ContainerPage.Url)%><%#((Bobs.Thread)Container.DataItem).CommentHtmlStart%><a href="<%#ContainerPage.Url.CurrentUrl("k",((Bobs.Thread)Container.DataItem).K.ToString())%>" <%#((Bobs.Thread)Container.DataItem).Rollover%>><%#HttpUtility.HtmlEncode(((Bobs.Thread)Container.DataItem).SubjectSafe)%></a><%#((Bobs.Thread)Container.DataItem).CommentHtmlEnd%><%#((Bobs.Thread)Container.DataItem).PagesHtml(ContainerPage.Url, new Bobs.Thread.GetUrlDelegate(GetThreadUrl), "k",((Bobs.Thread)Container.DataItem).K.ToString())%>
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
					</Columns>
				</asp:DataGrid>
			</p>
			<p runat="server" id="ThreadsPageP1" align="right">
				<asp:HyperLink runat="server" id="ThreadsPrevPageLink1"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... <asp:HyperLink runat="server" id="ThreadsNextPageLink1">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
			</p>
		</div>
	</asp:Panel>
	<dsi:h1 runat="server" ID="H13" NAME="H11">Post a new topic</dsi:h1>
	<div class="ContentBorder">
		<AddThread:Thread runat="server" ID="AddThread"/>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelForumPrivate" EnableViewState="False">
	<dsi:h1 runat="server" ID="H114">Private chat</dsi:h1>
	<div class="ContentBorder">
		<h2>Error</h2>
		<p>You don't have permission to read this forum. Only group members may read it.</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelThread">
	<h1 class="TabHolder">
		<a href="/chat" class="TabbedHeading Selected">Chat<!--<img src="/gfx/icon-discuss.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/inbox" class="TabbedHeading">Inbox<!--<img src="/gfx/icon-inbox-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/favourites" class="TabbedHeading">Favourites<!--<img src="/gfx/icon-star-26-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/watching" class="TabbedHeading">Watching<!--<img src="/gfx/icon-eye-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="<%= Bobs.Usr.Current!=null?Bobs.Usr.Current.UrlMyComments():"/pages/login?er=Log+in+first&url=/chat/inbox" %>" class="TabbedHeading">My&nbsp;comments<!--<img src="/gfx/icon-me-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
	</h1>
	<dsi:h1 runat="server" Visible="false">
		Chat
	</dsi:h1>
	<div class="ContentBorder" runat="server" visible="false">
		<asp:Panel ID="Panel1" Runat="server" EnableViewState="False">
			<p>
				<center>
					<table>
						<tr>
							<td align="center" style="width:70px;"><small><a href="/pages/inbox"><img src="/gfx/icon-inbox-up.png" style="margin-bottom:2px;" border="0" height="21" width="26"><br>Inbox</a></small></td>
							<td align="center" style="width:70px;"><small><a href="/pages/favourites"><img src="/gfx/icon-star-26-up.png" style="margin-bottom:2px;" border="0" height="21" width="26"><br>Favourites</a></small></td>
							<td align="center" style="width:70px;"><small><a href="/pages/watching"><img src="/gfx/icon-eye-up.png" style="margin-bottom:2px;" border="0" height="21" width="26"><br>Watching</a></small></td>
							<td align="center" style="width:70px;"><small><a href="<%= Bobs.Usr.Current!=null?Bobs.Usr.Current.UrlMyComments():"/pages/login?er=Log+in+first&url=/pages/inbox" %>"><img src="/gfx/icon-me-up.png" style="margin-bottom:2px;" border="0" height="21" width="26"><br>My&nbsp;comments</a></small></td>
							<td align="center" style="width:70px;"><small><a href="/chat"><img src="/gfx/icon-discuss.png" style="margin-bottom:2px;" border="0" height="21" width="26"><br>All&nbsp;chat</a></small></td>
							<td align="center" style="width:70px;"><small><a href="/pages/mygroups"><img src="/gfx/icon-group.png" style="margin-bottom:2px;" border="0" height="21" width="26"><br>My groups</a></small></td>
						</tr>
					</table>
				</center>
			</p>
		</asp:Panel>
	</div>
	
	<dsi:h1 runat="server" id="H112" Visible="false">Options</dsi:h1>
	<div class="ContentBorder">
		<p runat=server id=PanelCommentsDescriptionP></p>
		<p runat="server" id="ThreadDetailAdvancedOptionsP">
			<a href="" runat="server" id="ThreadDetailAdvancedOptionsAnchor"><img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">Advanced options</a>
		</p>
		
		<p>
			<script>
				DbButton(
					"/gfx/icon-eye-up.png",
					"/gfx/icon-eye-dn.png",
					"","",
					"Ignore this topic",
					"Watch this topic",
					"",
					"margin-right:3px;",
					"absmiddle",
					26,21,
					"WatchTopic",
					<%= ThreadButtonK %>,
					<%= ThreadWatchingButtonState %>,
					"WatchTop",
					"WatchReturnTop",
					"",
					"");
				function WatchReturnTop(id,oldState,newState)
				{
					DbButtonSetState("WatchBottom",newState);
					if (!newState && <%= HasInbox %>)
					{
						DbButtonSetState("InboxTop",false);
						DbButtonSetState("InboxBottom",false);
					}
				}
			</script>
		</p>
		<p>
			<script>
				DbButton(
					"/gfx/icon-star-26-up.png",
					"/gfx/icon-star-26-dn.png",
					"","",
					"Remove this topic from my favourites",
					"Add this topic to my favourites",
					"",
					"margin-right:3px;",
					"absmiddle",
					26,21,
					"FavouriteTopic",
					<%= ThreadButtonK %>,
					<%= ThreadFavouriteButtonState %>,
					"FavouriteTop",
					"FavouriteReturnTop",
					"",
					"");
				function FavouriteReturnTop(id,oldState,newState)
				{
					DbButtonSetState("FavouriteBottom",newState);
				}
			</script>
		</p>
		<asp:Panel runat="server" ID="ThreadDetailInboxPanel">
			<p>
				<script>
					DbButton(
						"/gfx/icon-inbox-up.png",
						"/gfx/icon-inbox-dn.png",
						"","",
						"Remove this topic from my inbox",
						"Put this topic back in my inbox",
						"",
						"margin-right:3px;",
						"absmiddle",
						26,21,
						"InboxTopic",
						<%= ThreadButtonK %>,
						<%= ThreadInboxButtonState %>,
						"InboxTop",
						"InboxReturnTop",
						"",
						"");
					function InboxReturnTop(id,oldState,newState)
					{
						DbButtonSetState("InboxBottom",newState);
						if (newState)
						{
							DbButtonSetState("WatchTop",true);
							DbButtonSetState("WatchBottom",true);
							DbButtonSpamSetState(true);
						}
					}
				</script>
				<asp:PlaceHolder Runat="server" ID="ThreadDetailInboxPh"></asp:PlaceHolder>
			</p>
		</asp:Panel>
		<asp:Panel ID="ThreadDetailsSpamPanel" runat="server">
			<p>
				<script>
				DbButton(
						"/gfx/icon-spam-dn.png",
						"/gfx/icon-spam-up.png",
						"","",
						"This is SPAM!",
						"This is not spam",
						"",
						"margin-right:3px;",
						"absmiddle",
						26,21,
						"WatchTopic",
						<%= ThreadButtonK %>,
						true,
						"SpamTop",
						"SpamReturnTop",
						"",
						"");
					function DbButtonSpamSetState(state)
					{
						DbButtonSetState("SpamTop",state);
						document.getElementById('<%= ThreadSpamOptionsPanel.ClientID %>').style.display = state ? "none" : "";
						document.getElementById('<%= SpamSourceLabel.ClientID %>').style.display = state ? "none" : "";
					}
					function SpamReturnTop(id,oldState,newState)
					{
						document.getElementById('<%= ThreadSpamOptionsPanel.ClientID %>').style.display = newState ? "none" : "";
						document.getElementById('<%= SpamSourceLabel.ClientID %>').style.display = newState ? "none" : "";
						if(!newState)
						{
							DbButtonSetState("WatchTop",false);
							DbButtonSetState("WatchBottom",false);
							
							if (<%= HasInbox %>)
							{
								DbButtonSetState("InboxTop",false);
								DbButtonSetState("InboxBottom",false);
							}
						}
						else
						{
							DbButtonSetState("WatchTop",true);
							DbButtonSetState("WatchBottom",true);
						}
					}
				</script>
				<asp:Label ID="SpamSourceLabel" runat="server" style="display:none;"></asp:Label>
				<asp:Panel id="ThreadSpamOptionsPanel" runat="server" style="display:none; margin-left:24px;">
					<p>
						<asp:RadioButton ID="ThreadSpamOption1RadioButton" runat="server" Text="Option 1" GroupName="ThreadSpamRadioGroup" style="margin-right:3px; font-weight:bold;"/>
						<asp:RadioButton ID="ThreadSpamOption2RadioButton" runat="server" Text="Option 2" GroupName="ThreadSpamRadioGroup" style="margin-right:3px; font-weight:bold;"/>
						<asp:RadioButton ID="ThreadSpamOption3RadioButton" runat="server" Text="Option 3" GroupName="ThreadSpamRadioGroup" style="margin-right:3px; font-weight:bold;"/>
						<button id="ThreadSpamUpdateButton" runat="server" validationgroup="ThreadSpamValidationGroup" style="margin-left:6px;" onserverclick="ThreadSpamUpdateButton_Click">Update</button>
					</p>
				</asp:Panel>
			</p>
		</asp:Panel>
	</div>
	
	
	
	<asp:Panel Runat="server" ID="PanelParticipants" EnableViewState="False">
		<dsi:h1 runat="server" ID="H111">Participants</dsi:h1>
		<div class="ContentBorder">
			<asp:Panel Runat="server" ID="ParticipantsHiddenPanel" Visible="False">
				<p>
					<img src="/gfx/icon-key.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">This is a private topic - there <asp:Label Runat="server" ID="ParticipantsLabel"/>.
					<asp:LinkButton Runat="server" OnClick="ParticipantsShowAll_Click" CausesValidation="False" ID="Linkbutton1" NAME="Linkbutton1">Click here to show all the participants</asp:LinkButton>.
				</p>
			</asp:Panel>
			<asp:Panel Runat="server" ID="ParticipantsListPanel">
				<p>
					<img src="/gfx/icon-key.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21">This is a private topic - the following people can see it (your buddies are <b>bold</b>):
				</p>
				<p class="CleanLinks">
					<asp:DataList Runat="server" ID="ParticipantsDataList" 
						RepeatColumns="2" RepeatLayout="Table" 
						Width="100%" ItemStyle-VerticalAlign="Top" />
				</p>
			</asp:Panel>
			<p>
				<small>
					If you invite people to a private message, this panel can take a few seconds to update. If there
					are people missing, try <a href="" runat="server" id="ParticipantsRefreshAnchor">clicking here</a>.
				</small>
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="PanelThreadSubject" EnableViewState="False">
		<dsi:h1 runat="server" ID="H18">Subject</dsi:h1>
		<div class="ContentBorder">
			<p runat="server" id="PanelThreadSubjectHeadP" align="center" style="font-size:165%;font-weight:bold;margin-top:12px;margin-bottom:12px;"/>
			<p align="center" runat="server" id="PanelThreadSubjectSubHeadP" style="margin-top:-9px;" visible="false"/>
			<p align="center" runat="server" id="PanelThreadSubjectPhotoP" visible="false">
				<a href="" runat="server" id="ThreadSubjectPhotoAnchor" class="NoStyle"><img runat="server" id="ThreadSubjectPhotoImg" class="BorderBlack All" border="0"></a>
			</p>
			<asp:Panel Runat="server" ID="PanelThreadSubjectPhotoMePanel" visible="false">
				<p align="center" style="font-size:125%;font-weight:bold;margin-top:8px;margin-bottom:8px;">
					This is <asp:PlaceHolder Runat="server" ID="PanelThreadSubjectPhotoMePh"></asp:PlaceHolder>
				</p>
			</asp:Panel>
			<p align="center" runat="server" id="PanelThreadSubjectArticleP" visible="false" style="font-size:12px;" />
			<p align="center" runat="server" id="PanelThreadSubjectArticleMoreP" visible="false" style="font-size:12px;font-weight:bold;padding:3px;"/>
		</div>
	</asp:Panel>
	
	<Controls:Article runat="server" id="PanelThreadSubjectArticleExtended" visible="false" />
	
	<asp:Panel Runat="server" ID="InitialCommentPanel">
		<dsi:h1 runat="server" id="InitialCommentH1">First comment</dsi:h1>
		<div class="ContentBorder">
			<p align="center"><a href="#Comments">Skip to replies</a></p>
			<asp:Panel Runat="server" ID="SubjectPanel1">
				<table cellpadding="0" cellspacing="0" border="0" width="582" style="margin-top:10px;margin-bottom:10px;">
					<tr>
						<td width="110" style="width:110px;" valign="top" align="right">
							<div style="font-size:12px; margin-right:8px; width:102px; text-align:right; overflow:hidden; padding-top:3px; padding-bottom:3px;">
								Subject</div>
						</td>
						<td valign="top" width="471" style="width:471px;">
							<div style="font-size:12px; font-weight:bold; padding-left:7px; width:471px; overflow:hidden; padding-top:3px; padding-bottom:3px;">
								<asp:Label Runat="server" ID="ThreadSubject1"/>
							</div>
						</td>
					</tr>
				</table>
			</asp:Panel>
			<asp:DataList Runat="server" ID="InitialCommentDataList" RepeatDirection="Horizontal" RepeatLayout="Flow" />
		</div>
	</asp:Panel>
	
	<asp:UpdatePanel runat="server" ChildrenAsTriggers="true">
		<ContentTemplate>
		
			<a name="Comments"></a>
			<dsi:h1 runat="server" id="CommentsSubjectH1">Comments</dsi:h1>
			<div class="ContentBorder">
				<script>
					function QuoteNow(usrK)
					{
						try
						{
							QuoteGeneric(document.getElementById('<%=  AddCommentHtml.TextBoxClientID  %>'), usrK);
						}
						catch(ex){}
					}
					function FocusNow()
					{
						try
						{
							FocusGeneric(document.getElementById('<%=  AddCommentHtml.TextBoxClientID  %>'));
						}
						catch(ex){}
					}
				</script>
				<p id="P1" runat="server" EnableViewState="False">
					<table width="100%" cellpadding="0" cellspacing="0">
						<tr>
							<td valign="center" style="padding:1px 0px 1px 0px;">
								<a href="" runat="server" id="ThreadDetailBackLink1"><img src="/gfx/icon-back-arrow.png" border="0" align="absmiddle" style="margin-right:3px;" height="21" width="26"><asp:Label Runat="server" ID="ThreadDetailBackLinkLabel1"></asp:Label></a>
							</td>
							<td valign="center" align="right" style="padding:1px 0px 1px 0px;">
								<div runat="server" id="CommentsPageTopHolder">
									<asp:HyperLink runat="server" id="CommentsPrevPageLink1"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... <asp:HyperLink runat="server" id="CommentsNextPageLink1">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
								</div>
							</td>
						</tr>
					</table>
				</p>
				<p runat="server" id="CommentsPageP1" align="right" style="padding-right:15px;" EnableViewState="False"/>
				<asp:Panel Runat="server" ID="SubjectPanel2" EnableViewState="False">
					<table cellpadding="0" cellspacing="0" border="0" width="582" style="margin-top:10px;margin-bottom:10px;">
						<tr>
							<td width="110" style="width:110px;" valign="top" align="right">
								<div style="font-size:12px; margin-right:8px; width:102px; text-align:right; overflow:hidden; padding-top:3px; padding-bottom:3px;">
									Subject</div>
							</td>
							<td valign="top" width="471" style="width:471px;">
								<div style="font-size:12px; font-weight:bold; padding-left:7px; width:471px; overflow:hidden; padding-top:3px; padding-bottom:3px;">
									<asp:Label Runat="server" ID="ThreadSubject2"/>
								</div>
							</td>
						</tr>
					</table>
				</asp:Panel>
				<asp:DataList Runat="server" ID="CommentsDataList" RepeatDirection="Horizontal" RepeatLayout="Flow"/>
				<p runat="server" id="CommentsPageP2" align="right" style="padding-right:15px;" EnableViewState="False"/>
				<p id="P2" runat="server" EnableViewState="False">
					<table width="100%" cellpadding="0" cellspacing="0">
						<tr>
							<td valign="center" style="padding:1px 0px 1px 0px;" width="33%">
								<nobr><a href="" runat="server" id="ThreadDetailBackLink2"><img src="/gfx/icon-back-arrow.png" border="0" align="absmiddle" style="margin-right:3px;" height="21" width="26"><asp:Label Runat="server" ID="ThreadDetailBackLinkLabel2"></asp:Label></a></nobr>
							</td>
							<td align="center" width="33%">
								<span runat="server" ID="ThreadDetailInboxBottomSpan">
									<span id="InboxBottomButtonHolder" runat="server" style="width:26px;height:21px;" />
									<dsi:InlineScript runat="server">
										<script>
											DbButtonFull(
												"/gfx/icon-inbox-up.png",
												"/gfx/icon-inbox-dn.png",
												"Remove this topic from my inbox","Put this topic back in my inbox",
												"",
												"",
												"",
												"margin-right:0px;",
												"absmiddle",
												26,21,
												"InboxTopic",
												<%= ThreadButtonK %>,
												<%= ThreadInboxButtonState %>,
												"InboxBottom",
												"InboxReturnBot",
												"",
												"",
												"<%= InboxBottomButtonHolder.ClientID %>");
											function InboxReturnBot(id,oldState,newState)
											{
												DbButtonSetState("InboxTop",newState);
												if (newState)
												{
													DbButtonSetState("WatchTop",true);
													DbButtonSetState("WatchBottom",true);
													DbButtonSpamSetState(true);
												}
											}
										</script>
									</dsi:InlineScript>
								</span>
								<span id="WatchBottomButtonHolder" runat="server" style="width:26px;height:21px;" />
								<dsi:InlineScript ID="InlineScript1" runat="server">
									<script>
										DbButtonFull(
											"/gfx/icon-eye-up.png",
											"/gfx/icon-eye-dn.png",
											"Ignore this topic","Watch this topic",
											"",
											"",
											"",
											"margin-right:0px;",
											"absmiddle",
											26,21,
											"WatchTopic",
											<%= ThreadButtonK %>,
											<%= ThreadWatchingButtonState %>,
											"WatchBottom",
											"WatchReturnBot",
											"",
											"",
											"<%= WatchBottomButtonHolder.ClientID %>");
										function WatchReturnBot(id,oldState,newState)
										{
											DbButtonSetState("WatchTop",newState);
											if (!newState && <%= HasInbox %>)
											{
												DbButtonSetState("InboxTop",false);
												DbButtonSetState("InboxBottom",false);
											}
										}
									</script>
								</dsi:InlineScript>
								<span id="FavouriteBottomButtonHolder" runat="server" style="width:26px;height:21px;" />
								<dsi:InlineScript ID="InlineScript2" runat="server">
									<script>
										DbButtonFull(
											"/gfx/icon-star-26-up.png",
											"/gfx/icon-star-26-dn.png",
											"Remove this topic from my favourites",
											"Add this topic to my favourites",
											"",
											"",
											"",
											"margin-right:0px;",
											"absmiddle",
											26,21,
											"FavouriteTopic",
											<%= ThreadButtonK %>,
											<%= ThreadFavouriteButtonState %>,
											"FavouriteBottom",
											"FavouriteReturnBot",
											"",
											"",
											"<%= FavouriteBottomButtonHolder.ClientID %>");
										function FavouriteReturnBot(id,oldState,newState)
										{
											DbButtonSetState("FavouriteTop",newState);
										}
									</script>
								</dsi:InlineScript>
							</td>
							<td valign="center" align="right" style="padding:1px 0px 1px 0px;" width="33%">
								<div runat="server" id="CommentsPageBottomHolder">
									<nobr><asp:HyperLink runat="server" id="CommentsPrevPageLink2"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... <asp:HyperLink runat="server" id="CommentsNextPageLink2">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink></nobr>
								</div>
							</td>
						</tr>
					</table>
				</p>
			</div>
			
			<asp:Panel runat="server" ID="CaptionEntryPanel">
				<a name="PostComment"></a>
				<dsi:h1 runat="server" ID="H19">Post another caption</dsi:h1>
				<div class="ContentBorder">
					<p align="center" style="font-size:12px;font-weight:bold;margin-top:15px;">
						You can win a Sony Ericsson W890i Walkman ® Phone - just add a funny caption below!
					</p>
					<table align="center">
						<tr>
							<td valign="top">
								<img src="/gfx/caption-start.gif" width="26" height="20" />
							</td>
							<td valign="center">
								<asp:TextBox ID="CaptionTextBox" runat="server" style="font-family: verdana, sans-serif; font-weight:bold; font-size:15px; width:450px;" MaxLength="200"></asp:TextBox>
							</td>
							<td valign="top">
								<img src="/gfx/caption-end.gif" width="26" height="20" />
							</td>
						</tr>
					</table>
					<p align="center" style="font-size:12px;font-weight:bold;" runat="server" id="CaptionButtonP">
						<asp:Button ID="Button2" runat="server" OnClick="Caption_Click" Text="Add my caption" CausesValidation="false"/>
					</p>
					<asp:Panel Runat="server" ID="CaptionLoginPanel">
						<p align="center">
							To post a caption you must first log on - use the links below to log on 
							or create a free account.
						</p>
						<p align="center">
							<table width="400" cellspacing="15">
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
					</asp:Panel>
					<p align="center" style="font-size:12px; font-weight:bold; color:Red;" runat="server" id="CaptionErrorP">
					</p>
				</div>
			</asp:Panel>
			
			<asp:Panel Runat="server" ID="PanelAddCommentClosed">
				<a name="PostComment"></a>
				<dsi:h1 runat="server" ID="H19dfs">Post a reply</dsi:h1>
				<div class="ContentBorder">
					<p class="BigCenter" align="center">
						This topic is closed - posting is disabled.
					</p>
				</div>
			</asp:Panel>
			<asp:Panel Runat="server" ID="PanelAddComment">
				<a name="PostComment"></a>
				<dsi:h1 runat="server">Post a reply</dsi:h1>
				<div class="ContentBorder">
					<asp:Panel Runat="server" ID="AddCommentGroupMemberPanel">
						<p>
							This topic was posted in a group forum. Only group members may post here. Becoming a 
							member is easy, just click the link below:
						</p>
						<p class="BigCenter">
							<a href="" runat="server" id="AddCommentGroupMemberAnchor" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">join this group</a>
						</p>
					</asp:Panel>
					<asp:Panel Runat="server" ID="AddCommentLoginPanel">
						<p>
							To post a comment you must first log on - use the links below to log on 
							or create a free account.
						</p>
						<p align="center">
							<table width="600" cellspacing="15">
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
					</asp:Panel>
					<asp:Panel Runat="server" ID="AddCommentEmailVerifyPanel">
						<p>
							To post a comment you must <a href="/pages/emailverify">first verify your email address</a>.
						</p>
					</asp:Panel>
					<asp:RequiredFieldValidator Runat="server" ControlToValidate="AddCommentHtml" Display="Dynamic" ErrorMessage="<p>Please enter a message</p>" ID="Requiredfieldvalidator3"/>
					<p>
						<dsi:Html runat="server" id="AddCommentHtml" PreviewType="Comment" DisableContainer="true" SaveButtonText="Post comment" OnSave="ThreadReply_Click" TabIndexBase="100" />
					</p>
				</div>
			</asp:Panel>
		</ContentTemplate>
	</asp:UpdatePanel>
	<asp:Panel Runat="server" ID="PanelInviteBuddiesSealed">
		<a name="InviteBuddies"></a>
		<dsi:h1 runat="server" ID="H110" NAME="H12">Invite your buddies to this topic</dsi:h1>
		<div class="ContentBorder">
			<p class="BigCenter" align="center">
				This topic is sealed - inviting is disabled.
			</p>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="PanelInviteBuddies">
		<a name="InviteBuddies"></a>
		<dsi:h1 runat="server" ID="H12" NAME="H12">Invite your buddies to this topic</dsi:h1>
		<div class="ContentBorder">
			<p>
				<uc1:MultiBuddyChooser runat="server" ID="uiMultiBuddyChooser" TabIndexBase="120" />
			</p>
			<p>
				<button id="Button1" runat="server" onserverclick="ThreadInvite_Click" causesvalidation="false" tabindex="140">Invite buddies</button>
			</p>
			<p style="color:red;" runat="server" id="PanelInviteErrorP" EnableViewState="False" visible="false">
				You haven't selected any buddies!
			</p>
			<p runat="server" id="PanelInviteDoneP" EnableViewState="False" visible="false"/>
		</div>
	</asp:Panel>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelThreadDiasbled" EnableViewState="False">
	<dsi:h1 runat="server" ID="H16">Discussions</dsi:h1>
	<div class="ContentBorder">
		<h2>Topic disabled</h2>
		<p>This topic is disabled.</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelPrivateThread" EnableViewState="False">
	<dsi:h1 runat="server" ID="H17">Discussions</dsi:h1>
	<div class="ContentBorder">
		<h2>Error</h2>
		<p>You don't have permission to view this topic.</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelGroupPrivateWaiting" EnableViewState="False">
	<dsi:h1 runat="server" ID="H118">Discussions</dsi:h1>
	<div class="ContentBorder">
		<h2>Can't view this topic until you're a group member</h2>
		<p>
			You can't view this topic yet because you're not yet a member 
			of the <a href="" runat="server" id="PanelGroupPrivateWaitingAnchor1"/> group. 
			Your membership application is waiting for a group moderator to review it. 
			You'll get an email soon.
		</p>
		<p class="BigCenter">
			<a href="" runat="server" id="PanelGroupPrivateWaitingAnchor2"/>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelGroupPrivateCanJoin" EnableViewState="False">
	<dsi:h1 runat="server" ID="H115" NAME="H115">Discussions</dsi:h1>
	<div class="ContentBorder">
		<h2>Can't view this topic until you're a group member</h2>
		<p>
			You can't view this topic yet because you're not yet a member 
			of the <a href="" runat="server" id="PanelGroupPrivateCanJoinGroupAnchor1"/> group. 
			You can join the group by clicking the button on the group page:
		</p>
		<p class="BigCenter">
			<a href="" runat="server" id="PanelGroupPrivateCanJoinGroupAnchor2"/>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelGroupPrivateRecommend" EnableViewState="False">
	<dsi:h1 runat="server" ID="H116" NAME="H115">Discussions</dsi:h1>
	<div class="ContentBorder">
		<h2>Error</h2>
		<p>
			You can't view this topic yet because you're not a member 
			of the <span Runat="server" ID="PanelGroupPrivateRecommendGroupSpan"/> 
			group. <span Runat="server" ID="PanelGroupPrivateRecommendInvitingUsrSpan"/> 
			has recommended you as a member, and group moderators are
			reviewing your membership. If you are invited to join the group you'll 
			receive an email shortly.
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelGroupPrivateRecommendRejected" EnableViewState="False">
	<dsi:h1 runat="server" ID="H117" NAME="H115">Discussions</dsi:h1>
	<div class="ContentBorder">
		<h2>Error</h2>
		<p>
			You can't view this topic yet because you're not a member 
			of the <span Runat="server" ID="PanelGroupPrivateRecommendRejectedGroupSpan"/> 
			group. You've been recommended as a possible member, but the group 
			moderators have rejected the request.
		</p>
	</div>
</asp:Panel>














