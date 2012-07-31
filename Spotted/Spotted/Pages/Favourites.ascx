<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Favourites.ascx.cs" Inherits="Spotted.Pages.Favourites" %>

<asp:Panel Runat="server" ID="PanelFavourites">
	<h1 class="TabHolder">
		<a href="/chat" class="TabbedHeading">Chat<!--<img src="/gfx/icon-discuss.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/inbox" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Inbox<!--<img src="/gfx/icon-inbox-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/favourites" class="TabbedHeading Selected" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Favourites<!--<img src="/gfx/icon-star-26-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/watching" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Watching<!--<img src="/gfx/icon-eye-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="<%= Bobs.Usr.Current != null ? Bobs.Usr.Current.UrlMyComments() : "/pages/mycomments" %>" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">My&nbsp;comments<!--<img src="/gfx/icon-me-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
	</h1>
	<div class="ContentBorder">
		<p>
			This is your favourite chats page. You can store any topic on 
			this page by clicking the favourite button <nobr>(<img src="/gfx/icon-star-22-up.png" border="0" align="absmiddle" height="21" width="22"> / <img src="/gfx/icon-star-22-dn.png" border="0" align="absmiddle" height="21" width="22">).</nobr> 
			You should be able to find it easily later if it's here!
		</p>
	</div>
	<asp:Panel Runat="server" ID="NoThreadsPanel" EnableViewState="False">
		<a name="Threads"></a>
		<dsi:h1 runat="server" ID="H12" NAME="H13">Topics</dsi:h1>
		<div class="ContentBorder">
			<p align="center">
				There are no topics in your favourites list yet. Click the favourite button <nobr>(<img src="/gfx/icon-star-22-up.png" border="0" align="absmiddle" height="21" width="22"> / <img src="/gfx/icon-star-22-dn.png" border="0" align="absmiddle" height="21" width="22">)</nobr> to add one.
			</p>
			<p align="center">
				<button onclick="history.go(0);">Refresh</button>
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="ThreadsPanel" EnableViewState="False">
		<a name="Threads"></a>
		<dsi:h1 runat="server" ID="H13" NAME="H13">Topics</dsi:h1>
		<div class="ContentBorder">
			<p runat="server" id="ThreadsPageLinksP" align="right"></p>
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
								<%#((Bobs.Thread)Container.DataItem).WatchingHtml("", ThreadsDataGrid)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitlesTight" HeaderStyle-CssClass="dataGridHeaderThreadTitlesTight">
							<HeaderTemplate>
								<asp:Label ID="FavouriteAllLabel" runat="server" Visible="false"></asp:Label>
							</HeaderTemplate>
							<ItemTemplate>
								<%#((Bobs.Thread)Container.DataItem).FavouriteHtml(ThreadsDataGrid)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitles" ItemStyle-Width="100%">
							<ItemTemplate>
								<%#((Bobs.Thread)Container.DataItem).IconsHtml(ContainerPage.Url)%><%#((Bobs.Thread)Container.DataItem).CommentHtmlStart%><a href="<%#Bobs.UrlInfo.MakeUrl("","chat","f",ThreadPage.ToString(),"k",((Bobs.Thread)Container.DataItem).K.ToString())%>" <%#((Bobs.Thread)Container.DataItem).Rollover%>><%#HttpUtility.HtmlEncode(((Bobs.Thread)Container.DataItem).SubjectSafe)%></a><%#((Bobs.Thread)Container.DataItem).CommentHtmlEnd%><%#((Bobs.Thread)Container.DataItem).PagesHtml(ContainerPage.Url, new Bobs.Thread.GetUrlDelegate(GetThreadUrl),"f",ThreadPage.ToString(),"k",((Bobs.Thread)Container.DataItem).K.ToString())%>
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
				<button onclick="history.go(0);">Refresh</button>
			</p>
			<p runat="server" id="ThreadsPageP1" align="right">
				<asp:HyperLink runat="server" id="ThreadsPrevPageLink1"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... <asp:HyperLink runat="server" id="ThreadsNextPageLink1">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
			</p>
			<p runat="server" id="ThreadsPageLinksP1" align="right"></p>
		</div>
	</asp:Panel>
</asp:Panel>
