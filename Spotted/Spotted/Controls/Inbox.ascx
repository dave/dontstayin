<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inbox.ascx.cs" Inherits="Spotted.Controls.Inbox" %>
<%@ Register TagPrefix="AddThread" TagName="Thread" Src="/Controls/AddThread.ascx" %>
<asp:Panel ID="InboxFilterPanel" runat="server" Visible="false">
	<p align="center" id="InboxFilterP" runat="server"></p>
</asp:Panel>
<asp:Panel ID="FilterPanel" runat="server" Visible="false">
	<table cellpadding=8 cellspacing=0 border=0>
		<tr>
			<td><strong>Filter</strong></td>
			<td><asp:CheckBox ID="BuddyPostCheckBox" runat="server" AutoPostBack="true" Text="Buddy posts" />&nbsp;<asp:DropDownList ID="BuddyDropDownList" runat="server" AutoPostBack="true" Width="150"></asp:DropDownList></td>
			<td><asp:CheckBox ID="GroupPostCheckBox" runat="server" AutoPostBack="true" Text="Group posts" />&nbsp;<asp:DropDownList ID="GroupDropDownList" runat="server" AutoPostBack="true" Width="150"></asp:DropDownList></td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel Runat="server" ID="NoThreadsPanel" EnableViewState="False">
	<p align="center">
		There are no topics in your inbox. We put topics in your inbox when they have new comments posted.
	</p>
	<p align="center">
		<button ID="NoThreadsRefreshButton" runat="server" onserverclick="RefreshButton_Click" causesvalidation="false">Refresh</button>
	</p>
</asp:Panel>
<asp:Panel Runat="server" ID="ThreadsPanel" EnableViewState="False">
	<p runat="server" id="ThreadsPageLinksP" align="right"></p>
	<p runat="server" id="ThreadsPageP" align="right">
		<asp:HyperLink runat="server" id="ThreadsPrevPageLink"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... 
		<asp:HyperLink runat="server" id="ThreadsNextPageLink">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
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
				function wR(id,oldState,newState)
				{
					if (!newState)
						DbButtonSetState(id.replace(/w/g, "i"),false);
				}
				
				var i3="/gfx/icon-star-22-up.png";
				var i4="/gfx/icon-star-22-dn.png";
				var a3="Remove this topic from my favourites";
				var a4="Add this topic to my favourites";
				var l2="left";
				var s2="cursor:pointer;";
				var f2="FavouriteTopic";
				
				var i5="/gfx/icon-inbox-up.png";
				var i6="/gfx/icon-inbox-dn.png";
				var a5="Remove this topic from my inbox";
				var a6="Put this topic back in my inbox";
				var l3="left";
				var s3="cursor:pointer;";
				var f3="InboxTopic";
				function iT(id,oldState,newState)
				{
					if (newState)
						DbButtonSetState(id.replace(/i/g, "w"),true);
				}
			</script>
		</dsi:InlineScript>
		<asp:DataGrid Runat="server" ID="ThreadsDataGrid" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass=dataGrid OnItemDataBound="ThreadsDataGrid_ItemDataBound"
			HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" ItemStyle-VerticalAlign=Top width="100%">
			<Columns>
				<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitlesTight" HeaderStyle-CssClass="dataGridHeaderThreadTitlesTight">
					<ItemTemplate>
						<%#((Bobs.Thread)Container.DataItem).InboxButtonHtml(ThreadsDataGrid)%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridThreadTitlesTight" HeaderStyle-CssClass="dataGridHeaderThreadTitlesTight">
					<ItemTemplate>
						<%#((Bobs.Thread)Container.DataItem).WatchingHtml("wR", ThreadsDataGrid)%>
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
						<%#((Bobs.Thread)Container.DataItem).IconsHtml(ContainerPage.Url)%><%#((Bobs.Thread)Container.DataItem).CommentHtmlStart%><a href="<%#Bobs.UrlInfo.MakeUrl("","chat","i",ThreadPage.ToString(),"k",((Bobs.Thread)Container.DataItem).K.ToString())%>" <%#((Bobs.Thread)Container.DataItem).Rollover%>><%#HttpUtility.HtmlEncode(((Bobs.Thread)Container.DataItem).SubjectSafe)%></a><%#((Bobs.Thread)Container.DataItem).CommentHtmlEnd%><%#((Bobs.Thread)Container.DataItem).PagesHtml(ContainerPage.Url, new Bobs.Thread.GetUrlDelegate(GetThreadUrl),"i",ThreadPage.ToString(),"k",((Bobs.Thread)Container.DataItem).K.ToString())%>
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
		<button ID="RefreshButton" runat="server" onserverclick="RefreshButton_Click" causesvalidation="false">Refresh</button>
	</p>
	<p runat="server" id="ThreadsPageP1" align="right">
		<asp:HyperLink runat="server" id="ThreadsPrevPageLink1"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... <asp:HyperLink runat="server" id="ThreadsNextPageLink1">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
	</p>
	<p runat="server" id="ThreadsPageLinksP1" align="right"></p>
</asp:Panel>
