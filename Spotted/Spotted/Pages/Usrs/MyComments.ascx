<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyComments.ascx.cs" Inherits="Spotted.Pages.Usrs.MyComments" %>

<%@ Register TagPrefix="Spotted" TagName="Cal" Src="/Controls/Cal.ascx" %>
<asp:Panel Runat="server" ID="PanelMyComments">
	<asp:Panel runat="server" id="MyChatLinksPanel">
		<h1 class="TabHolder">
			<a href="/chat" class="TabbedHeading">Chat<!--<img src="/gfx/icon-discuss.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
			<a href="/pages/inbox" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Inbox<!--<img src="/gfx/icon-inbox-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
			<a href="/pages/favourites" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Favourites<!--<img src="/gfx/icon-star-26-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
			<a href="/pages/watching" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Watching<!--<img src="/gfx/icon-eye-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
			<a href="<%= Bobs.Usr.Current != null ? Bobs.Usr.Current.UrlMyComments() : "/pages/mycomments" %>" class="TabbedHeading Selected" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">My&nbsp;comments<!--<img src="/gfx/icon-me-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		</h1>
		<div class="ContentBorder">
			<p>
				This page shows all of <a href="<%= ThisUsr.Url() %>" <%= ThisUsr.Rollover %>><%= ThisUsr.NickName %>'s</a> comments. It's different 
				to the other chat pages because it lists a seperate item for each comment.
				You can look for comments posted on a certain day by using the calendar:
			</p>
			<p>
				<a href="<%= ThisUsr.UrlMyCommentsMonth(DateTime.Today) %>"><img src="/gfx/icon-calendar.png" style="margin-right:3px;" border="0" height="21" width="26" align="absmiddle">Calendar</a>
			</p>
		</div>
	</asp:Panel>
	
	<dsi:UsrIntro runat="server" ID="UsrIntro">
		<p>
			This page shows all of <a href="<%= ThisUsr.Url() %>" <%= ThisUsr.Rollover %>><%= ThisUsr.NickName %>'s</a> comments. It's different 
			to the other chat pages because it lists a seperate item for each comment.
			You can look for comments posted on a certain day by using the calendar:
		</p>
		<p>
			<a href="<%= ThisUsr.UrlMyCommentsMonth(DateTime.Today) %>"><img src="/gfx/icon-calendar.png" style="margin-right:3px;" border="0" height="21" width="26" align="absmiddle">Calendar</a>
		</p>
	</dsi:UsrIntro>
	
	<Spotted:Cal Runat="server" ID="Cal"/>
	
	<asp:Panel Runat="server" ID="NoThreadsPanel" EnableViewState="False">
		<a name="Threads"></a>
		<dsi:h1 runat="server" ID="H12" NAME="H13">Topics</dsi:h1>
		<div class="ContentBorder">
			<p align="center">
				<a href="<%= ThisUsr.Url() %>" <%= ThisUsr.Rollover %>><%= ThisUsr.NickName %></a> hasn't posted any comments.
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="ThreadsPanel" EnableViewState="False">
		<a name="Threads"></a>
		<dsi:h1 runat="server" ID="H13" NAME="H13">Topics</dsi:h1>
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
				<asp:DataGrid Runat="server" ID="ThreadsDataGrid" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass=dataGrid OnItemDataBound="ThreadsDataGrid_ItemDataBound" 
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" ItemStyle-VerticalAlign=Top width="100%">
					<Columns>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitlesTight" HeaderStyle-CssClass="dataGridHeaderThreadTitlesTight">
							<HeaderTemplate>
								<asp:Label ID="WatchingAllLabel" runat="server" Visible="false"></asp:Label>
							</HeaderTemplate>
							<ItemTemplate>
								<%#((Bobs.Comment)Container.DataItem).WatchingHtml("", ThreadsDataGrid)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitlesTight" HeaderStyle-CssClass="dataGridHeaderThreadTitlesTight">
							<HeaderTemplate>
								<asp:Label ID="FavouriteAllLabel" runat="server" Visible="false"></asp:Label>
							</HeaderTemplate>
							<ItemTemplate>
								<%#((Bobs.Comment)Container.DataItem).FavouriteHtml(ThreadsDataGrid)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitles" ItemStyle-Width="100%">
							<ItemTemplate>
								<%#((Bobs.Comment)Container.DataItem).Thread.IconsHtml(ContainerPage.Url)%>
								<a href="<%#CommentUrl((Bobs.Comment)Container.DataItem)%>" <%#((Bobs.Comment)Container.DataItem).Thread.Rollover%>>
									<%#((Bobs.Comment)Container.DataItem).TextSnip(60)%>
								</a>
								<br>
								<small>
									Topic: 
									<a href="<%#Bobs.UrlInfo.MakeUrl("","chat","k",((Bobs.Comment)Container.DataItem).ThreadK.ToString())%>" <%#((Bobs.Comment)Container.DataItem).Thread.Rollover%>>
										<%#HttpUtility.HtmlEncode(((Bobs.Comment)Container.DataItem).Thread.SubjectSnip(50))%>
									</a>
								</small>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Posted / To" ItemStyle-CssClass="dataGridThreadTitles" ItemStyle-Width="100%">
							<ItemTemplate>
								<nobr><small><%#DateString((Bobs.Comment)Container.DataItem)%></small></nobr><br>
								<small><%#((Bobs.Comment)Container.DataItem).Thread.MyCommentsToHtml(ThisUsr.K)%></small>
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
</asp:Panel>
