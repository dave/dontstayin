<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Events.ascx.cs" Inherits="Spotted.Pages.Promoters.Events" %>

<asp:Panel Runat="server" ID="PanelEventsList">
	<dsi:PromoterIntro runat="server" ID="Promoterintro2" Header="Events">
		<p>
			All the events in your promoter account are listed below. If there are events on DontStayIn
			that are missing from this list, please call our promoter hotline on 0207 835 5599.
		</p>
		<p style="font-weight:bold; font-size:14px;">
			<a href="/pages/events/edit">Click here to add a new event</a>
		</p>
	</dsi:PromoterIntro>
	<dsi:h1 runat="server" ID="H12">Events</dsi:h1>
	<div class="ContentBorder">
		<p>
			<asp:GridView Runat="server" ID="EventsGridView" GridLines="None" AutoGenerateColumns="False" BorderWidth=0 CellPadding=3 CssClass="dataGrid" 
						AlternatingRowStyle-CssClass="dataGridAltItem" HeaderStyle-CssClass="dataGridHeader" SelectedRowStyle-CssClass="dataGridSelectedItem"
					 	RowStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanging="EventsGridViewChangePage" PageSize="10" PagerSettings-Mode="Numeric">
				<Columns>
					<asp:TemplateField HeaderText="Name">
						<ItemTemplate>
							<a href="<%#CurrentPromoter.UrlEventOptions((Bobs.Event)(Container.DataItem))%>"><%#((Bobs.Event)(Container.DataItem)).Name%></a><br>
							<small><%#((Bobs.Event)(Container.DataItem)).FriendlyDate(true)%></small>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Options" HeaderStyle-CssClass="Vert">
						<ItemTemplate>
							<%#Utilities.Link(CurrentPromoter.UrlEventOptions((Bobs.Event)(Container.DataItem)), Utilities.IconHtml(Utilities.Icon.Edit, "Options", ""))%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Tickets" HeaderStyle-CssClass="Vert">
						<ItemTemplate>
							<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlTickets(CurrentPromoter)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Banners" HeaderStyle-CssClass="Vert">
						<ItemTemplate>
							<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlBanner(CurrentPromoter)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Highlight" HeaderStyle-CssClass="Vert">
						<ItemTemplate>
							<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlEventDonate(CurrentPromoter)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Group<br>news">
						<ItemTemplate>
							<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlNews(CurrentPromoter)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Article">
						<ItemTemplate>
							<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlArticle(CurrentPromoter)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Comp-<br>etition">
						<ItemTemplate>
							<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlCompetition(CurrentPromoter)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Guest-<br>list">
						<ItemTemplate>
							<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlGuestlist(CurrentPromoter)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Spotter<br>invite">
						<ItemTemplate>
							<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlSpotterInvite(CurrentPromoter)%>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</p>
	</div>
</asp:Panel>
