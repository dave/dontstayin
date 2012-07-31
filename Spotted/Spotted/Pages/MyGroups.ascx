<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyGroups.ascx.cs" Inherits="Spotted.Pages.MyGroups" %>

<%@ Register TagPrefix="AddThread" TagName="Thread" Src="/Controls/AddThread.ascx" %>
<asp:Panel Runat="server" ID="GroupsPanel">
	<asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
		<ContentTemplate>
			<asp:Panel Runat="server" ID="PanelInvites" EnableViewState="False">
				<a name="PanelInvites"></a>
				<dsi:h1 runat="server" ID="H12">Invites</dsi:h1>
				<div class="ContentBorder">
					<p>
						You've been invited to the groups below:
					</p>
					<p>
						<asp:DataGrid Runat="server" ID="InvitesDataGrid" 
							GridLines="None" AutoGenerateColumns="False"
							BorderWidth=0 CellPadding=3 CssClass=dataGrid 
							AlternatingItemStyle-CssClass="dataGridAltItem"
							HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
							ItemStyle-VerticalAlign="Middle" AllowPaging="True" OnPageIndexChanged="InvitesDataGridChangePage"
							PageSize="20" PagerStyle-Mode="NumericPages" PagerStyle-CssClass="dataGridFooterGrey"
							OnItemCommand="InvitesDataGridItemCommand"
							OnItemDataBound="InvitesDataGridItemDataBound">
							<Columns>
								<asp:TemplateColumn HeaderText="Invite">
									<ItemTemplate>
										<%#((Bobs.GroupUsr)(Container.DataItem)).InviteHtml()%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Options" ItemStyle-VerticalAlign="Top">
									<ItemTemplate>
										<nobr>
											<a href="<%#((Bobs.GroupUsr)(Container.DataItem)).Group.UrlApp("join")%>"><img src="/gfx/icon-tick-up.png" border="0" align="absmiddle" style="margin-right:3px;">Accept</a>
											<asp:LinkButton Runat="server" CommandName="reject" CommandArgument="<%#((Bobs.GroupUsr)(Container.DataItem)).GroupK%>" ID="Linkbutton1" CausesValidation="False"><img src="/gfx/icon-cross-up.png" border="0" align="absmiddle" style="margin-right:0px;">Decline</asp:LinkButton>
											&nbsp;
										</nobr>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</p>
				</div>
			</asp:Panel>
		</ContentTemplate>
	</asp:UpdatePanel>
	<dsi:h1 runat="server" ID="H11">Groups</dsi:h1>
	<div class="ContentBorder">
	
		<p class="BigCenter">
			<a href="/pages/groups/edit">Add a new group of your own</a>
		</p>
		
		<script>
			function ShowNewThread()
			{
				document.forms[0]["<%= AddThreadStatusHidden.ClientID %>"].value='1';
				document.getElementById('<%= AddThreadLinkP.ClientID %>').style.display='none';
				document.getElementById('<%= AddThreadPanel.ClientID %>').style.display='';
			}
		</script>
		<input type="hidden" runat="server" id="AddThreadStatusHidden" NAME="AddThreadStatusHidden"/>
		
		<p class="BigCenter" runat="server" id="AddThreadLinkP">
			<a href="/" onclick="ShowNewThread();return false;">Send a message to a group</a>
		</p>
		<asp:Panel Runat="server" ID="AddThreadPanel" style="display:none;">
			<AddThread:Thread runat="server" ID="AddThread"/>
		</asp:Panel>
		
		<asp:Panel Runat="server" ID="PanelNoGroups">
			<p align="center">
				You're not a member of any groups yet.
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelGroupsList" EnableViewState="False">
			<p class="CleanLinks">
				<dsi:InlineScript ID="InlineScript3" runat="server">
					<script>
						var i1="/gfx/icon-eye-up.png";
						var i2="/gfx/icon-eye-dn.png";
						var a1="Ignore new topics in this group";
						var a2="Watch all new topics in this group";
						var l1="left";
						var s1="cursor:pointer;";
						var f1="CommentAlert";
						
						var i3="/gfx/icon-star-22-up.png";
						var i4="/gfx/icon-star-22-dn.png";
						var a3="Remove this group from your favourites";
						var a4="Add this group to your favourites";
						var l2="left";
						var s2="cursor:pointer;";
						var f2="FavouriteGroup";
					</script>
				</dsi:InlineScript>
				<asp:DataGrid Runat="server" ID="GroupsDataGrid"
					GridLines="None" AutoGenerateColumns="False"
					BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Middle" AllowPaging="True" OnPageIndexChanged="GroupsDataGridChangePage"
					PageSize="20" PagerStyle-Mode="NumericPages" PagerStyle-CssClass="dataGridFooterGrey" Width="100%">
					<Columns>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridPlainCell dataGridTight">
							<ItemTemplate>
								<%#((Bobs.GroupUsr)(Container.DataItem)).WatchingHtml(GroupsDataGrid)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridPlainCell dataGridTight">
							<ItemTemplate>
								<%#((Bobs.GroupUsr)(Container.DataItem)).FavouriteHtml(GroupsDataGrid)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="" ItemStyle-CssClass="dataGridPlainCell" ItemStyle-Width="100%">
							<ItemTemplate>
								<b><a href="<%#((Bobs.GroupUsr)(Container.DataItem)).Group.Url()%>"><%#((Bobs.GroupUsr)(Container.DataItem)).Group.FriendlyName%></a></b>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn ItemStyle-CssClass="dataGridPlainCell">
							<ItemTemplate>
								<%#((Bobs.GroupUsr)(Container.DataItem)).ModeratorHtml%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Stats">
							<ItemTemplate>
								<small>
									<nobr><a href="<%#((Bobs.GroupUsr)(Container.DataItem)).Group.UrlApp("members")%>"><%#((Bobs.GroupUsr)(Container.DataItem)).Group.TotalMembers.ToString("###,##0")%> member<%#((Bobs.GroupUsr)(Container.DataItem)).Group.TotalMembers==1?"":"s"%></a></nobr><br>
									<nobr><a href="<%#((Bobs.GroupUsr)(Container.DataItem)).Group.UrlDiscussion()%>"><%#((Bobs.GroupUsr)(Container.DataItem)).Group.TotalComments.ToString("###,##0")%> comment<%#((Bobs.GroupUsr)(Container.DataItem)).Group.TotalComments==1?"":"s"%></a></nobr>
								</small>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
		</asp:Panel>
	</div>
</asp:Panel>
