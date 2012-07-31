<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventOptions.ascx.cs" Inherits="Spotted.Pages.Promoters.EventOptions" %>

<%@ Register TagPrefix="Controls" TagName="AddThread" Src="/Controls/AddThread.ascx" %>
<asp:Panel Runat="server" ID="PanelEvent">
	<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Promoter event page">
		<p>
			This is the promoter event page. It lists all the promotional items connected to your event:
		</p>
		<p runat="server" id="EventLinksP" style="font-weight:bold;font-size:14px;"/>
	</dsi:PromoterIntro>
	
	<dsi:h1 runat="server" ID="Header" NAME="H18">Event details</dsi:h1>
	<div class="ContentBorder" style="padding:0px;">
		<table cellspacing="0" cellpadding="5" border="0" width="100%" class="padding5">
			<tr>
				<td valign="top"><small>Date</small></td>
				<td valign="top"><%= CurrentEvent.FriendlyDate(true) %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-date">Change</a></small></td>
			</tr>
			<tr class="dataGridAltItem">
				<td valign="top"><small>Name</small></td>
				<td valign="top"><%= CurrentEvent.Name %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr>
				<td valign="top"><small>Venue</small></td>
				<td valign="top"><%= CurrentEvent.Venue.FriendlyHtml(true,false) %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr class="dataGridAltItem">
				<td valign="top"><small>Start&nbsp;time</small></td>
				<td valign="top"><%= CurrentEvent.StartTime %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr>
				<td valign="top"><small>Short&nbsp;details</small></td>
				<td valign="top"><%= CurrentEvent.ShortDetailsHtmlRender.Replace("<p>","").Replace("</p>","") %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr class="dataGridAltItem">
				<td valign="top"><small>Long&nbsp;details</small></td>
				<td valign="top"><small>[long details not shown here]</small></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr>
				<td valign="top"><small>Capacity</small></td>
				<td valign="top"><%= CurrentEvent.Capacity.ToString("#,##0") %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr class="dataGridAltItem">
				<td valign="top"><small>Party&nbsp;brands</small></td>
				<td valign="top"><%= CurrentEvent.BrandsHtml %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-brand">Change</a></small></td>
			</tr>
			<tr>
				<td valign="top"><small>Music&nbsp;types</small></td>
				<td valign="top"><%= CurrentEvent.MusicTypesString %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-musictype">Change</a></small></td>
			</tr>
			<tr class="dataGridAltItem">
				<td valign="top"><small>Picture</small></td>
				<td valign="top" runat="server" id="EventDetailsPicCell"></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-pic">Change</a></small></td>
			</tr>
		</table>
	</div>
	
	
	<asp:Panel Runat="server" ID="SpotterRequestYesPanel">
		<a name="SpotterRequestPanel"/>
		<dsi:h1 runat="server" ID="H1" NAME="H18a">Spotter invite</dsi:h1>
		<div class="ContentBorder">
			<p>
				<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="left" style="margin-right:3px;">
				You've invited spotters to contact you to arrange a guestlist. 
				The contact details are: <asp:Label runat="server" ID="SpotterRequestDetails" />.
			</p>
			<p>
				To change your spotter invite options, <a href="" runat="server" id="SpotterRequestYesLink">click here</a>
			</p>
		</div>
	</asp:Panel>
		
	<asp:Panel Runat="server" ID="SpotterRequestNoPanel">
		<a name="SpotterRequestPanel"/>
		<dsi:h1 runat="server" ID="H2" NAME="H18b">No spotter invites</dsi:h1>
		<div class="ContentBorder">
			<p>
				<img src="/gfx/icon-warning.png" border="0" width="26" height="21" align="absmiddle" style="margin-right:3px;"><b>Warning!</b> You haven't invited any spotters!
			</p>
			<p class="MedCenter">
				<a href="" runat="server" id="SpotterRequestNoLink"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">invite a spotter</a>
			</p>
		</div>
	</asp:Panel>
	
	
	
	<asp:Panel Runat="server" ID="TicketRunPanel">
		<a name="TicketRunPanel"/>
		<dsi:h1 runat="server" ID="H10" NAME="H18">Tickets</dsi:h1>
		<div class="ContentBorder" style="padding-left:0px;padding-right:0px;">
			<p>
				<asp:GridView ID="TicketRunsGridView" runat="server" AllowPaging="False" AutoGenerateColumns="False" 
					OnRowCommand="TicketRunsGridView_RowCommand" CssClass="dataGrid" EnableViewState="true"
					AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader"
					SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top" Width="100%">
					<Columns>
						<asp:TemplateField HeaderText="Tickets"  ItemStyle-BorderWidth="0">
							<ItemTemplate><asp:TextBox ID="TicketRunKTextBox" runat="server" Visible="false" Text='<%#((Bobs.TicketRun)(Container.DataItem)).K%>'></asp:TextBox>
								<%#((Bobs.TicketRun)(Container.DataItem)).LinkPriceBrandName%>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right" Visible="false">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).Price.ToString("c")%>
							</ItemTemplate>
						</asp:TemplateField>				
						<asp:TemplateField HeaderText="Tickets<br>start" Visible="false">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).StartDateTime.ToString("dd/MM/yy HH:mm")%> 
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Tickets<br>end" Visible="false">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).EndDateTime.ToString("dd/MM/yy HH:mm")%> 
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Sold" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).SoldTickets.ToString()%><%# ((Bobs.TicketRun)(Container.DataItem)).CancelledTicketQuantity > 0 ? "(" + ((Bobs.TicketRun)(Container.DataItem)).CancelledTicketQuantity.ToString() + ")" : "" %>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Max" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).MaxTickets.ToString()%>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Status">
							<ItemTemplate>
								<%#Utilities.CamelCaseToString(((Bobs.TicketRun)(Container.DataItem)).Status.ToString())%>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Live" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="26">
							<ItemTemplate>
								<%#Utilities.TickCrossHtml(((Bobs.TicketRun)(Container.DataItem)).Status.Equals(Bobs.TicketRun.TicketRunStatus.Running))%>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="26">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).LinkEditIconHtml %>
							</ItemTemplate>
						</asp:TemplateField>				
						<asp:TemplateField HeaderText="Door<br>list" Visible="false">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).Event.DoorlistIconLinkHtml %>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Actions">
							<ItemTemplate>
								<nobr><asp:LinkButton ID="PauseResumeTicketRunButton" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="PauseResumeTicketRun" CausesValidation="false"><%#((Bobs.TicketRun)(Container.DataItem)).PauseResumeIconHtml%></asp:LinkButton>
								<asp:LinkButton ID="StopTicketRunButton" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="StopTicketRun" CausesValidation="false" OnClientClick="return confirm('Are you sure you wish to stop this ticket run? Once it is stopped it cannot be restarted.')" Visible='<%#((Bobs.TicketRun)(Container.DataItem)).IsUpdateable %>'><%#((Bobs.TicketRun)(Container.DataItem)).StopIconHtml %></asp:LinkButton></nobr>
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>	
				</asp:GridView>
			</p>
			<p style="padding-left:8px;padding-right:8px;" id="NoTicketRunsP" runat="server" visible="false">
				<img src="/gfx/icon-warning.png" border="0" width="26" height="21" align="absmiddle" style="margin-right:3px;"><b>Warning!</b> You are not selling tickets for this event! It's FREE to sell tickets!
			</p>
			<p class="MedCenter" id="SellTicketsP" runat="server">
				<a href="" runat="server" id="SellTicketsLink"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">sell tickets</a>
			</p>
			<p class="MedCenter" id="NoSellTicketsP" runat="server">
				You can no longer sell tickets for this event.
			</p>
			
			<p class="MedCenter" id="DoorlistP" runat="server">
				<a href="" runat="server" id="DoorlistLink"><img src="/gfx/icon-print.png" border="0"  width="26" height="21"
					align="absmiddle" style="margin-right:3px;">print door list</a>
			</p>
			<p style="padding-left:8px;padding-right:8px;">
				<center>Special offer! Sell tickets on DontStayIn, and get a FREE event hilight - worth up to £200!</center>
			</p>
			<asp:Panel ID="AdminLinksPanel" runat="server" Visible="false">
				<p style="padding-left:8px;padding-right:8px;">
					<small><a href="/admin/ticketfundsrelease">[Ticket funds release]</a> <a href="/admin/ticketsearch">[ticket search]</a></small>
				</p>
			</asp:Panel>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="BannersPanel">
		<a name="BannersPanel"/>
		<dsi:h1 runat="server" ID="H11" NAME="H18">Banners</dsi:h1>
		<div class="ContentBorder" style="padding-left:0px;padding-right:0px;">
			<p>
				<asp:DataGrid Runat="server" ID="BannerDataGrid" 
					GridLines="None" AutoGenerateColumns="False"
					BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					AlternatingItemStyle-CssClass="dataGridAltItem"
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="BannerDataGridChangePage"
					PageSize="20" PagerStyle-Mode="NumericPages" Width="100%">
					<Columns>
						<asp:TemplateColumn HeaderText="Our<br>ref" ItemStyle-BorderWidth="0">
							<ItemTemplate>
								<%#((Bobs.Banner)(Container.DataItem)).K%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Your<br>ref">
							<ItemTemplate>
								<%#((Bobs.Banner)(Container.DataItem)).Link()%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Details">
							<ItemTemplate>
								<nobr><%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Banner)(Container.DataItem)).FirstDay,true,true)%>
								-&gt; <%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Banner)(Container.DataItem)).LastDay,false,true)%></nobr><br>
								<nobr><%#((Bobs.Banner)(Container.DataItem)).SummaryString(true)%>,
								<%#((Bobs.Banner)(Container.DataItem)).TotalRequiredImpressions%> impressions</nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Slots<br>booked">
							<ItemTemplate>
								<img src="<%#((Bobs.Banner)(Container.DataItem)).StatusBooked?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Banner<br>ready">
							<ItemTemplate>
								<img src="<%#((Bobs.Banner)(Container.DataItem)).IsReady?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Live<br>now">
							<ItemTemplate>
								<img src="<%#((Bobs.Banner)(Container.DataItem)).IsLive?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Price" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<%#((Bobs.Banner)(Container.DataItem)).PriceString%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Options">
							<ItemTemplate>
								<nobr>
									<a href="<%#((Bobs.Banner)(Container.DataItem)).Promoter.UrlApp("banneroptions","mode","delete","bannerk",((Bobs.Banner)(Container.DataItem)).K.ToString(),"eventk",CurrentEvent.K.ToString())%>" onclick="return confirm('Are you sure?');">Delete</a>
								</nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
			<p class="MedCenter">
				<a href="" runat="server" id="BannerAddLink"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add a banner</a>
			</p>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="NoBannersPanel">
		<dsi:h1 runat="server" ID="H17" NAME="H18">No banners!</dsi:h1>
		<div class="ContentBorder">
			<p>
				<img src="/gfx/icon-warning.png" border="0" width="26" height="21" align="absmiddle" style="margin-right:3px;"><b>Warning!</b> You have no banners for this event!
			</p>
			<p class="MedCenter">
				<a href="" runat="server" id="BannerAddLink1"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add a banner</a>
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="EventHighlightPanel">
		<a name="EventHighlightPanel"/>
		<dsi:h1 runat="server" ID="H12a" NAME="H18a">Event highlighted</dsi:h1>
		<div class="ContentBorder">
			<p>
				<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="left" style="margin-right:3px;">This event has been highlighted! It will appear in a highlighted box at the top of the calendar, event lists, and the update email.
			</p>
		</div>
	</asp:Panel>
		
	<asp:Panel Runat="server" ID="NoEventHighlightPanel">
		<a name="NoEventHighlightPanel"/>
		<dsi:h1 runat="server" ID="H12b" NAME="H18b">No event highlight</dsi:h1>
		<div class="ContentBorder">
			<p>
				<img src="/gfx/icon-warning.png" border="0" width="26" height="21" align="absmiddle" style="margin-right:3px;"><b>Warning!</b> You have no event highlight for this event!
			</p>
			<p class="MedCenter">
				<a href="" runat="server" id="EventHighlightLink"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add event highlight</a>
			</p>
		</div>
	</asp:Panel>
	
	
	<a name="NewsPanel"/>
	<dsi:h1 runat="server" ID="H12" NAME="H18">Group news</dsi:h1>
	<div class="ContentBorder" style="padding-left:0px;padding-right:0px;">
		<asp:Panel Runat="server" ID="NewsPanel">
			<p>
				<asp:DataGrid Runat="server" ID="NewsDataGrid" 
					GridLines="None" AutoGenerateColumns="False"
					BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					AlternatingItemStyle-CssClass="dataGridAltItem"
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="NewsDataGridChangePage"
					PageSize="20" PagerStyle-Mode="NumericPages" Width="100%">
					<Columns>
						<asp:TemplateColumn HeaderText="Subject" ItemStyle-BorderWidth="0">
							<ItemTemplate>
								<a href="<%#((Bobs.Thread)(Container.DataItem)).Url()%>"><%#((Bobs.Thread)(Container.DataItem)).Subject%></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Posted">
							<ItemTemplate>
								<%#Cambro.Misc.Utility.FriendlyTime(((Bobs.Thread)(Container.DataItem)).DateTime, true)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Part-<br>icipants">
							<ItemTemplate>
								<%#((Bobs.Thread)(Container.DataItem)).TotalWatching%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Com-<br>ments">
							<ItemTemplate>
								<%#((Bobs.Thread)(Container.DataItem)).TotalComments%>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
		</asp:Panel>
		
		<asp:Panel Runat="server" ID="NewsPostPanel">
		
			<asp:Panel Runat="server" ID="NoNewsPanel">
				<p style="padding-left:8px;padding-right:8px;">
					<img src="/gfx/icon-warning.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;"><b>Warning!</b> You haven't posted any news about this event! It's FREE to post news!
				</p>
			</asp:Panel>
			
			<script>
				function NewsShowNewThread()
				{
					document.forms[0]["<%= NewsAddThreadStatusHidden.ClientID %>"].value='1';
					document.getElementById('<%= NewsAddThreadLinkP.ClientID %>').style.display='none';
					document.getElementById('<%= NewsAddThreadPanel.ClientID %>').style.display='';
				}
			</script>
			
			<p class="MedCenter" runat="server" id="NewsAddThreadLinkP">
				<a href="/" onclick="NewsShowNewThread();return false;"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add new group news</a>
			</p>
			
			<asp:Panel Runat="server" ID="NewsAddThreadPanel" style="display:none;padding-left:8px;padding-right:8px;">
				<input type="hidden" runat="server" id="NewsAddThreadStatusHidden" NAME="NewsAddThreadStatusHidden"/>
				<Controls:AddThread runat="server" ID="NewsAddThread"/>
			</asp:Panel>
		</asp:Panel>
		
		<asp:Panel Runat="server" ID="NoNewsPostPanel">
			<p style="padding-left:8px;padding-right:8px;">
				<img src="/gfx/icon-warning.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;"><b>You can't post news because you don't have a group.</b> Call 0207 835 5599 for more info.
			</p>
		</asp:Panel>
		
	</div>
		
	
	
	
	
	<asp:Panel Runat="server" ID="ArticlePanel">
		<a name="ArticlePanel"/>
		<dsi:h1 runat="server" ID="H110" NAME="H18">Articles</dsi:h1>
		<div class="ContentBorder" style="padding-left:0px;padding-right:0px;">
			<p>
				<asp:DataGrid Runat="server" ID="ArticleDataGrid" 
					GridLines="None" AutoGenerateColumns="False"
					BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					AlternatingItemStyle-CssClass="dataGridAltItem"
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="ArticleDataGridChangePage"
					PageSize="20" PagerStyle-Mode="NumericPages" Width="100%">
					<Columns>
						<asp:TemplateColumn HeaderText="Pic" ItemStyle-BorderWidth="0">
							<ItemTemplate>
								<%#Bobs.Promoter.PicHtml((Bobs.Article)(Container.DataItem))%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Subject">
							<ItemTemplate>
								<a href="<%#((Bobs.Article)(Container.DataItem)).Url()%>"><%#((Bobs.Article)(Container.DataItem)).Title%></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Author">
							<ItemTemplate>
								<%#((Bobs.Article)(Container.DataItem)).Owner.Link()%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Pub-<br>lished">
							<ItemTemplate>
								<img src="<%#(((Bobs.Article)(Container.DataItem)).Status.Equals(Bobs.Article.StatusEnum.Edit) || ((Bobs.Article)(Container.DataItem)).Status.Equals(Bobs.Article.StatusEnum.Enabled))?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Ena-<br>bled">
							<ItemTemplate>
								<img src="<%#((Bobs.Article)(Container.DataItem)).Status.Equals(Bobs.Article.StatusEnum.Enabled)?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Com-<br>ments">
							<ItemTemplate>
								<%#((Bobs.Article)(Container.DataItem)).TotalComments%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Options">
							<ItemTemplate>
								<nobr>
									<a href="/pages/myarticles/mode-edit/k-<%#((Bobs.Article)(Container.DataItem)).K%>">Edit</a>
								</nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
			<p class="MedCenter">
				<a href="" runat="server" id="ArticleAddLink"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add new article</a>
			</p>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="NoArticlePanel">
		<dsi:h1 runat="server" ID="H111" NAME="H18">No articles!</dsi:h1>
		<div class="ContentBorder">
			<p>
				<img src="/gfx/icon-warning.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;"><b>Warning!</b> You haven't posted any articles about this event! It's FREE to post articles!
			</p>
			<p class="MedCenter">
				<a href="" runat="server" id="ArticleAddLink1"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add new article</a>
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="CompPanel">
		<a name="CompPanel"/>
		<dsi:h1 runat="server" ID="H13" NAME="H18">Competitions</dsi:h1>
		<div class="ContentBorder" style="padding-left:0px;padding-right:0px;">
			<p>
				<asp:DataGrid Runat="server" ID="CompDataGrid" 
					GridLines="None" AutoGenerateColumns="False"
					BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					AlternatingItemStyle-CssClass="dataGridAltItem"
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="CompDataGridChangePage"
					PageSize="20" PagerStyle-Mode="NumericPages" Width="100%">
					<Columns>
						<asp:TemplateColumn HeaderText="Pic" ItemStyle-BorderWidth="0">
							<ItemTemplate>
								<%#Bobs.Promoter.PicHtml((Bobs.Comp)(Container.DataItem))%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Prize">
							<ItemTemplate>
								<a href="<%#((Bobs.Comp)(Container.DataItem)).Url()%>"><%#((Bobs.Comp)(Container.DataItem)).Name%></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Start">
							<ItemTemplate>
								<%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Comp)(Container.DataItem)).DateTimeStart, true)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="End">
							<ItemTemplate>
								<%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Comp)(Container.DataItem)).DateTimeClose, true)%> @ midday
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Pub-<br>lished">
							<ItemTemplate>
								<img src="<%#(((Bobs.Comp)(Container.DataItem)).Status.Equals(Bobs.Comp.StatusEnum.Published) || ((Bobs.Comp)(Container.DataItem)).Status.Equals(Bobs.Comp.StatusEnum.Enabled))?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Ena-<br>bled">
							<ItemTemplate>
								<img src="<%#((Bobs.Comp)(Container.DataItem)).Status.Equals(Bobs.Comp.StatusEnum.Enabled)?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Winners<br>picked">
							<ItemTemplate>
								<img src="<%#((Bobs.Comp)(Container.DataItem)).WinnersPicked?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Entries">
							<ItemTemplate>
								<%#((Bobs.Comp)(Container.DataItem)).Entries.ToString("#,##0")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Options">
							<ItemTemplate>
								<nobr><%#((Bobs.Comp)Container.DataItem).OptionsHtmlEvent(CurrentEvent.K)%></nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
			<p class="MedCenter">
				<a href="" runat="server" id="CompAddLink"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add new competition</a>
			</p>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="NoCompPanel">
		<dsi:h1 runat="server" ID="H19" NAME="H18">No competitions!</dsi:h1>
		<div class="ContentBorder">
			<p>
				<img src="/gfx/icon-warning.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;"><b>Warning!</b> You haven't posted any competition related to this event! It's FREE to post competition!
			</p>
			<p class="MedCenter">
				<a href="" runat="server" id="CompAddLink1"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add new competition</a>
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="GuestlistPanel">
		<a name="GuestlistPanel"/>
		<dsi:h1 runat="server" ID="H14" NAME="H18">Guestlist</dsi:h1>
		<div class="ContentBorder">
			<p>
				<asp:DataGrid Runat="server" ID="GuestlistDataGrid" 
					GridLines="None" AutoGenerateColumns="False"
					BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					AlternatingItemStyle-CssClass="dataGridAltItem"
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Top" AllowPaging="False"
					PageSize="20" PagerStyle-Mode="NumericPages">
					<Columns>
						<asp:TemplateColumn HeaderText="Status">
							<ItemTemplate>
								<%#((Bobs.Event)Container.DataItem).GuestlistOpen?"Open":(((Bobs.Event)(Container.DataItem)).GuestlistFinished?"Closed":"Paused")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="On list" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<%#((Bobs.Event)Container.DataItem).GuestlistCount%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Limit" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<%#((Bobs.Event)Container.DataItem).GuestlistLimit%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Spaces" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<%#((Bobs.Event)Container.DataItem).GuestlistLimit - ((Bobs.Event)(Container.DataItem)).GuestlistCount%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Regular<br>price" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<%#((Bobs.Event)Container.DataItem).GuestlistRegularPrice.ToString("£0.##")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Guestlist<br>price" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<%#((Bobs.Event)Container.DataItem).GuestlistPrice.ToString("£0.##")%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Options">
							<ItemTemplate>
								<%#((Bobs.Event)Container.DataItem).GuestlistOptionsHtml%>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="NoGuestlistPanel">
		<dsi:h1 runat="server" ID="H112" NAME="H18">No guestlist!</dsi:h1>
		<div class="ContentBorder">
			<p>
				<img src="/gfx/icon-warning.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;"><b>Warning!</b> You haven't added a guestlist! It's FREE to add a guestlist, and you just pay per name.
			</p>
			<p class="MedCenter">
				<a href="" runat="server" id="GuestlistAddLink"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add a guestlist</a>
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="SpotterInvitePanel" Visible="False">
		<dsi:h1 runat="server" ID="H16" NAME="H18">Spotter invite</dsi:h1>
		<div class="ContentBorder">
			<p>
				Shows spotter invite details
			</p>
		</div>
	</asp:Panel>
</asp:Panel>
