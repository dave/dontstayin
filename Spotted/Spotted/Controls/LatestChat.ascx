<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestChat.ascx.cs" Inherits="Spotted.Controls.LatestChat" %>
<asp:Panel runat="server" id="Holder">
	<dsi:h1 runat="server" ID="Header" />
	<div runat="server" id="InnerHolder">
		<asp:Panel Runat="server" ID="ThreadsNoPermissionPanel" EnableViewState="False">
			<p>
				This group has a private forum. Only group members may read the posts. Becoming a 
				member is easy, just click the link below:
			</p>
			<p class="BigCenter">
				<a href="" runat="server" id="ThreadsNoPermissionJoinAnchor">Join this group</a>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="ThreadsPanel" EnableViewState="False">
			<p runat="server" id="BrandChatControlsP" align="center">
				<b runat="server" id="ShowGroupChatEnabled">regulars chat</b>
				<asp:LinkButton runat="server" ID="ShowGroupChatLinkButton" OnClick="ShowGroupChat_Click" Text="regulars chat" CausesValidation="false"/> | 
				<asp:LinkButton runat="server" ID="ShowBrandChatLinkButton" OnClick="ShowBrandChat_Click" Text="public chat from events" CausesValidation="false"/>
				<b runat="server" id="ShowBrandChatEnabled">public chat from events</b>
			</p>
			<dsi:InlineScript ID="InlineScript1" runat="server" Type="StartOfPage">
				<script>
					var i1 = "/gfx/icon-eye-up.png";
					var i2 = "/gfx/icon-eye-dn.png";
					var a1 = "Ignore this topic";
					var a2 = "Watch this topic";
					var l1 = "left";
					var s1 = "cursor:pointer;";
					var f1 = "WatchTopic";

					var i3 = "/gfx/icon-star-22-up.png";
					var i4 = "/gfx/icon-star-22-dn.png";
					var a3 = "Remove this topic from my favourites";
					var a4 = "Add this topic to my favourites";
					var l2 = "left";
					var s2 = "cursor:pointer;";
					var f2 = "FavouriteTopic";
				</script>
			</dsi:InlineScript>
			<p>
				<asp:DataGrid Runat="server" ID="ThreadsDataGrid" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" ItemStyle-VerticalAlign=Top width="100%">
					<Columns>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitlesTight">
							<ItemTemplate>
								<%#((Bobs.Thread)Container.DataItem).WatchingHtml("", ThreadsDataGrid, DbButtonPrefix)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitlesTight">
							<ItemTemplate>
								<%#((Bobs.Thread)Container.DataItem).FavouriteHtml(ThreadsDataGrid, DbButtonPrefix)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridTightImg" Visible=False>
							<ItemTemplate>
								<a href="<%#((Bobs.Thread)Container.DataItem).GetThreadUrlSimple(Discussable)%>"><img src="<%#((Bobs.Thread)Container.DataItem).SimpleIconPath%>" align="top" border="0" class="LatestChatImage" hspace="0" width="30" height="30"></a></ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitles" ItemStyle-Width="100%">
							<ItemTemplate>
								<%#((Bobs.Thread)Container.DataItem).IconsHtml(((Spotted.Master.DsiPage)Page).Url)%><%#((Bobs.Thread)Container.DataItem).CommentHtmlStart%><a href="<%#((Bobs.Thread)Container.DataItem).GetThreadUrlSimple(Discussable)%>" <%#((Bobs.Thread)Container.DataItem).Rollover%>><%#HttpUtility.HtmlEncode(((Bobs.Thread)Container.DataItem).SubjectSafe)%></a><%#((Bobs.Thread)Container.DataItem).CommentHtmlEnd%><%#((Bobs.Thread)Container.DataItem).PagesHtml(Discussable, new Bobs.Thread.GetUrlByDiscussableDelegate(((Bobs.Thread)Container.DataItem).GetThreadUrl), "k",((Bobs.Thread)Container.DataItem).K.ToString())%>
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
			<p runat="server" id="CommentsFooter" align="center">
				<small>
					<a href="" runat="server" id="MoreThreadsAnchor">Read more chat<asp:Label Runat="server" ID="MoreThreadsCountLabel"></asp:Label></a>
				</small>
			</p>
		</asp:Panel>
	</div>
	<input runat="server" type="hidden" id="uiObjectType" />
	<input runat="server" type="hidden" id="uiObjectK" />
	<input runat="server" type="hidden" id="uiThreadsCount" />
	<input runat="server" type="hidden" id="uiHasGroupObjectFilter" />
</asp:Panel>
