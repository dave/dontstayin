<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendar.ascx.cs" Inherits="Spotted.Styled.Calendar" %>
<h2>Calendar of events</h2>
<p class="CalendarFilterP">
	<a href="<%= StyledObject.UrlStyledCalendar(ContainerPage.Url.DateFilter.AddMonths(-1).Year, ContainerPage.Url.DateFilter.AddMonths(-1).Month)%>" class="Link">&lt; <%= ContainerPage.Url.DateFilter.AddMonths(-1).ToString("MMM yyyy") %></a><span class="CalendarMonthSpacer">&nbsp;:&nbsp;</span><span class="CalendarCurrentMonth"><%= ContainerPage.Url.DateFilter.ToString("MMM yyyy") %></span><span class="CalendarMonthSpacer">&nbsp;:&nbsp;</span><a href="<%= StyledObject.UrlStyledCalendar(ContainerPage.Url.DateFilter.AddMonths(1).Year, ContainerPage.Url.DateFilter.AddMonths(1).Month)%>" class="Link"><%= ContainerPage.Url.DateFilter.AddMonths(1).ToString("MMM yyyy") %> &gt;</a>
	<hr />		
</p>
<div class="InnerDiv">
	<p class="Link">
		<asp:Repeater ID="EventLinkRepeater" runat="server">
			<ItemTemplate>
				<%# StyledObject.UrlStyledEventLink((Event)Container.DataItem) %>
			</ItemTemplate>
		</asp:Repeater>
		<asp:Label id="NoEventsLabel" runat="server" Text="No events for this month." Visible="false"></asp:Label>
	</p>
</div>
