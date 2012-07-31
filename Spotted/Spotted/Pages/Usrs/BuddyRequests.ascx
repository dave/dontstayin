<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuddyRequests.ascx.cs" Inherits="Spotted.Pages.Usrs.BuddyRequests" %>
<dsi:h1 runat="server" ID="H18">My buddy requests</dsi:h1>
<div class="ContentBorder">
	<asp:Panel runat="server" ID="uiNoBuddyRequestsPanel">
		<p>You currently have no buddy requests.</p>
	</asp:Panel>
	<asp:Panel runat="server" ID="uiBuddyRequestsPanel">
		<p>These people have invited you to their buddy lists. You need to add them to your list to accept the invite. If you deny a buddy request, you can still add them later from their profile page. They won't receive any notification that you denied their request.</p>
		<asp:Panel runat="server" ID="uiMultiButtonsPanel">
			<p>
				<script>
					DbButton(
						"/gfx/icon-star-26-up.png",
						"/gfx/icon-star-26-dn.png",
						"",
						"",
						"Remove all from my buddy list",
						"Add all to my buddy list",
						"",
						"cursor:pointer;margin-right:3px;",
						"absmiddle",
						26,21,
						"MultiBuddy",
						"<%= UsrKsList %>",
						"0",
						"MultiBuddyButton",
						"MultiBuddyButtonReturn",
						"",
						"");
					function MultiBuddyButtonReturn(id,oldState,newState)
					{
						for (i=0; i<<%= UsrKsCount %>; i++)
						{
							DbButtonSetState("BuddyButton" + i,newState);
							DbButtonSetState("BuddyInviteButton" + i,newState);
							DbButtonSetState("BuddyDenyButton" + i,false);
						}
						DbButtonSetState("MultiBuddyInviteButton",newState);
						DbButtonSetState("MultiBuddyDenyButton",false);
					}
				</script>
			</p>
			<p>
				<script>
					DbButton(
						"/gfx/icon-inbox-up.png",
						"/gfx/icon-inbox-dn.png",
						"",
						"",
						"Stop all inviting me in bulk to chat topics",
						"Allow all to invite me in bulk to chat topics",
						"",
						"cursor:pointer;margin-right:3px;",
						"absmiddle",
						26,21,
						"MultiBuddyChatInvite",
						"<%= UsrKsList %>",
						"0",
						"MultiBuddyInviteButton",
						"MultiBuddyInviteButtonReturn",
						"",
						"");
					function MultiBuddyInviteButtonReturn(id,oldState,newState)
					{
						if (newState)
						{
							for (i=0; i<<%= UsrKsCount %>; i++)
							{
								DbButtonSetState("BuddyButton" + i,true);
							}
							DbButtonSetState("MultiBuddyButton",true);
						}
						for (i=0; i<<%= UsrKsCount %>; i++)
						{
							DbButtonSetState("BuddyInviteButton" + i,newState);
							DbButtonSetState("BuddyDenyButton" + i,false);
						}
						DbButtonSetState("MultiBuddyDenyButton",false);
					}
				</script>
			</p>
			<p>
				<script>
					DbButton(
						"/gfx/icon-cross-up.png",
						"/gfx/icon-cross-dn.png",
						"",
						"",
						"Leave all buddy requests for later",
						"Deny all and remove all buddy requests from list",
						"",
						"cursor:pointer;margin-right:3px;",
						"absmiddle",
						26,21,
						"MultiBuddyDeny",
						"<%= UsrKsList %>",
						"0",
						"MultiBuddyDenyButton",
						"MultiBuddyDenyButtonReturn",
						"",
						"");
					function MultiBuddyDenyButtonReturn(id,oldState,newState)
					{
						if (newState)
						{
							for (i=0; i<<%= UsrKsCount %>; i++)
							{
								DbButtonSetState("BuddyButton" + i,false);
								DbButtonSetState("BuddyInviteButton" + i,false);
							}

							DbButtonSetState("MultiBuddyButton",false);
							DbButtonSetState("MultiBuddyInviteButton",false);
						}
						for (i=0; i<<%= UsrKsCount %>; i++)
						{
							DbButtonSetState("BuddyDenyButton" + i,newState);
						}
					}
				</script>
			</p>
		</asp:Panel>
		<asp:GridView Runat="server" 
			ID="uiBuddiesRequested" 
			DataKeyNames="K"
			GridLines="None" 
			AutoGenerateColumns="False"
			ShowHeader="False"
			BorderWidth="0" 
			CellPadding="3" 
			CssClass="dataGrid" 
			RowStyle-VerticalAlign="Middle" 
			AllowPaging="True" 
			OnPageIndexChanging="ChangePage"
			PageSize="20" 
			PagerStyle-Mode="NumericPages" 
			PagerStyle-CssClass="dataGridFooterGrey"
			OnRowDataBound="uiBuddiesRequestedRowDataBound">
			<Columns>
				<asp:TemplateField>
					<ItemTemplate>
							<a href="<%#((Bobs.Usr)(Container.DataItem)).Url()%>"><img src="<%#((Bobs.Usr)(Container.DataItem)).AnyPicPath%>" class="BorderBlack All" border="0" width="100" height="100" /></a>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<ItemTemplate>
						<a href="<%#((Bobs.Usr)(Container.DataItem)).Url()%>" <%#((Bobs.Usr)(Container.DataItem)).Rollover%>><%#((Bobs.Usr)(Container.DataItem)).NickName%></a>
						<asp:Literal runat="server" ID="uiDbButtonScripts"></asp:Literal>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<button runat="server" onserverclick="Refresh">Refresh</button>
	</asp:Panel>
	<p><a href="<%= Usr.Current.UrlBuddyRequestsIveSent() %>">View buddy requests I've sent</a></p>
</div>
