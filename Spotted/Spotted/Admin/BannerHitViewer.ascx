<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerHitViewer.ascx.cs"
	Inherits="Spotted.Admin.BannerHitViewer" %>
<style>
	.bad
	{
		color: Red;
	}
</style>
<table>
	<tr valign="top">
		<td>
			Show timeslot data between
			<br />
			<asp:DropDownList ID="TimeslotStart" runat="server" AutoPostBack="True" />
			and
			<br />
			<asp:DropDownList ID="TimeslotEnd" runat="server" AutoPostBack="True" />
			<table style="border-collapse: collapse;" cellspacing="0" rules="all" border="1">
				<thead>
					<tr>
						<td>
							Timeslot
						</td>
						<td>
							Required
						</td>
						<td>
							Actual
						</td>
						<td>
							Not shown
						</td>
						<td>
							Timeouts
						</td>
						<td>
							BannerServer calls
						</td>
					</tr>
				</thead>
				<tbody>
					<asp:Repeater ID="TimeslotInfoRepeater" runat="server">
						<ItemTemplate>
							<tr>
								<td>
									<%# ((TimeslotInfo) Container.DataItem).TimeslotStart.ToString()%>
								</td>
								<td>
									<%# ((TimeslotInfo) Container.DataItem).Required%>
								</td>
								<td>
									<%# ((TimeslotInfo) Container.DataItem).Actual %>
								</td>
								<td>
									<%# ((TimeslotInfo) Container.DataItem).NotShown %>
								</td>
								<td>
									<%# ((TimeslotInfo) Container.DataItem).Timeouts %>
								</td>
								<td>
									<%# ((TimeslotInfo) Container.DataItem).CallsToBannerServer %>
								</td>
							</tr>
						</ItemTemplate>
					</asp:Repeater>
				</tbody>
			</table>
		</td>
		<td>
			<table style="border-collapse: collapse;" cellspacing="0" rules="all" border="1">
				<thead>
					<tr valign="bottom">
						<td scope="col" style="width: 300px">
							Banner
						</td>
						<td scope="col">
							K
						</td>
						<td scope="col" style="width: 100px">
							Position
						</td>
						<td scope="col">
							Place Targetted
						</td>
						<td scope="col">
							Music Targetted
						</td>
						<td scope="col">
							Hits required
						</td>
						<td scope="col">
							Hits so far
						</td>
						<td scope="col">
							Hits so far db
						</td>
						<td scope="col">
							Unique hits so far
						</td>
						<td scope="col">
							Clicks so far
						</td>
						<td scope="col">
							Clicks so far db
						</td>
						<td scope="col">
							Credits per click
						</td>
						<td scope="col">
							Lifespan (days)
						</td>
						<td scope="col">
							Elapsed lifespan (days)
						</td>
						<td scope="col">
							Elapsed lifespan
						</td>
						<td scope="col">
							Elapsed hits
						</td>
						<td scope="col">
							Hits required previous timeslot
						</td>
						<td scope="col">
							Hits desired previous timeslot
						</td>
						<td scope="col">
							Hits actual previous timeslot
						</td>
						<td scope="col">
							Times considered prev
						</td>
						<td scope="col">
							Rate prev
						</td>
						<td scope="col">
							Rem secs
						</td>
						<td scope="col">
							Hits required current timeslot
						</td>
						<td scope="col">
							Hits desired current timeslot
						</td>
						<td scope="col">
							Hits actual current timeslot
						</td>
						<td scope="col">
							Times considered
						</td>
						<td scope="col">
							Rate
						</td>
						
					</tr>
				</thead>
				<asp:Repeater ID="BannerInfoRepeater" runat="server">
					<ItemTemplate>
						<tbody>
							<tr style="text-align: right;">
								<td style="text-align: left">
									<asp:HyperLink runat="server" ID="link" Text='<%#((BannerInfo) Container.DataItem).Name %>'
										NavigateUrl='<%#((BannerInfo) Container.DataItem).Url %>'></asp:HyperLink>
								</td>
								<td style="text-align: left">
									<%#((BannerInfo) Container.DataItem).K %>
								</td>
								<td style="text-align: left">
									<%#((BannerInfo) Container.DataItem).Position %>
								</td>
								<td>
									<%#(((BannerInfo)Container.DataItem).IsPlaceTargetted ? "Y" : "")%>
								</td>
								<td>
									<%#(((BannerInfo)Container.DataItem).IsMusicTargetted ? "Y" : "")%>
								</td>
								<td>
									<%#((BannerInfo)Container.DataItem).HitsRequired.ToString("n0")%>
								</td>
								<td>
									<span title='<%# ((float) ((BannerInfo)Container.DataItem).HitsSoFar / (float) ((BannerInfo)Container.DataItem).HitsRequired).ToString("p1") %> of required hits'>
										<%# ((BannerInfo)Container.DataItem).HitsSoFar.ToString("n0")%></span>
								</td>
								<td>
									<span title='<%# ((float)((BannerInfo)Container.DataItem).HitsSoFarDb / (float)((BannerInfo)Container.DataItem).HitsRequired).ToString("p1")%> of required hits'>
										<%# ((BannerInfo)Container.DataItem).HitsSoFarDb.ToString("n0")%></span>
								</td>
								<td>
									<%# ((BannerInfo)Container.DataItem).UniqueHitsSoFar.ToString("n0")%>
								</td>
								<td>
									<%# ((BannerInfo)Container.DataItem).ClicksSoFar.ToString("n0")%>
								</td>
								<td>
									<%# ((BannerInfo)Container.DataItem).ClicksSoFarDb.ToString("n0")%>
								</td>
								<td>
									<nobr><%#((BannerInfo)Container.DataItem).CreditsPerClick.ToString("0.00")%></nobr>
								</td>
								<td>
									<%# ((BannerInfo)Container.DataItem).Lifespan.ToString("n0")%>
								</td>
								<td>
									<%# ((BannerInfo)Container.DataItem).ElapsedLifespan.ToString("n0")%>
								</td>
								<td>
									<%#(((BannerInfo)Container.DataItem).IsTotalHitRateBad ? "<font class=\"bad\">" : "") + 
									"<nobr>" + (((BannerInfo) Container.DataItem).ElapsedLifespan / ((BannerInfo) Container.DataItem).Lifespan).ToString("p1") + "</nobr>" +
									(((BannerInfo)Container.DataItem).IsTotalHitRateBad ? "</font>" : "")%>
								</td>
								<td>
									<%#(((BannerInfo)Container.DataItem).IsTotalHitRateBad ? "<font class=\"bad\">" : "") + 
									"<nobr>" + ((float) ((BannerInfo)Container.DataItem).HitsSoFar / (float) ((BannerInfo)Container.DataItem).HitsRequired).ToString("p1") + "</nobr>" +
									(((BannerInfo)Container.DataItem).IsTotalHitRateBad ? "</font>" : "")%>
								</td>
								<td>
									<%#(((BannerInfo)Container.DataItem).IsPreviousTimeslotBad ? "<font class=\"bad\">" : "") + 
									((BannerInfo)Container.DataItem).HitsRequiredPreviousTimeslot.ToString("n0") +
									(((BannerInfo)Container.DataItem).IsPreviousTimeslotBad ? "</font>" : "") %>
								</td>
								<td>
									<%#(((BannerInfo)Container.DataItem).IsPreviousTimeslotBad ? "<font class=\"bad\">" : "") + 
									((BannerInfo)Container.DataItem).HitsDesiredPreviousTimeslot.ToString("n0") +
									(((BannerInfo)Container.DataItem).IsPreviousTimeslotBad ? "</font>" : "") %>
								</td>
								<td>
									<%#(((BannerInfo)Container.DataItem).IsPreviousTimeslotBad ? "<font class=\"bad\">" : "") + 
									((BannerInfo)Container.DataItem).ActualHitsPreviousTimeslot.ToString("n0") +
									(((BannerInfo)Container.DataItem).IsPreviousTimeslotBad ? "</font>" : "") %>
								</td>
								<td>
									<%#	(((BannerInfo)Container.DataItem).TimesConsideredPrev == 0 ? "<font class=\"bad\">" : "") +
									((BannerInfo)Container.DataItem).TimesConsideredPrev.ToString("n0") +
									(((BannerInfo)Container.DataItem).TimesConsideredPrev == 0 ? "</font>" : "")%>
								</td>
								<td>
									<nobr><%#((BannerInfo)Container.DataItem).RatePrev.ToString("p") %></nobr>
								</td>
								<td>
									<nobr><%#((BannerInfo)Container.DataItem).RemainingSecondsWhenPreviousBannerCompleted.ToString("n0") %></nobr>
								</td>
								<td>
									<%#(((BannerInfo)Container.DataItem).IsCurrentTimeslotBad ? "<font class=\"bad\">" : "") + 
									((BannerInfo)Container.DataItem).HitsRequiredCurrentTimeslot.ToString("n0") +
									(((BannerInfo)Container.DataItem).IsCurrentTimeslotBad ? "</font>" : "")%>
								</td>
								<td>
									<%#(((BannerInfo)Container.DataItem).IsCurrentTimeslotBad ? "<font class=\"bad\">" : "") + 
									((BannerInfo)Container.DataItem).HitsDesiredCurrentTimeslot.ToString("n0") +
									(((BannerInfo)Container.DataItem).IsCurrentTimeslotBad ? "</font>" : "")%>
								</td>
								<td>
									<%#(((BannerInfo)Container.DataItem).IsCurrentTimeslotBad ? "<font class=\"bad\">" : "") + 
									((BannerInfo)Container.DataItem).ActualHitsCurrentTimeslot.ToString("n0") +
									(((BannerInfo)Container.DataItem).IsCurrentTimeslotBad ? "</font>" : "")%>
								</td>
								<td>
									<%#(((BannerInfo)Container.DataItem).TimesConsidered == 0 ? "<font class=\"bad\">" : "") + 
									((BannerInfo)Container.DataItem).TimesConsidered.ToString("n0") +
									(((BannerInfo)Container.DataItem).TimesConsidered == 0 ? "</font>" : "") %>
								</td>
								<td>
									<nobr><%#((BannerInfo)Container.DataItem).Rate.ToString("p") %></nobr>
								</td>
								
							</tr>
						</tbody>
					</ItemTemplate>
				</asp:Repeater>
				<tfoot>
					<tr>
						<td>
						</td>
						<!--Banner-->
						<td>
						</td>
						<!--Position-->
						<td>
						</td>
						<!--Hits required-->
						<td>
						</td>
						<!--Hits so far-->
						<td>
						</td>
						<!--Hits so far db-->
						<td>
						</td>
						<!--Unique hits so far-->
						<td>
						</td>
						<!--Clicks so far-->
						<td>
						</td>
						<!--Clicks so far db-->
						<td>
						</td>
						<!--Lifespan (days)-->
						<td>
						</td>
						<!--Elapsed lifespan (days)-->
						<td>
						</td>
						<!--Elapsed lifespan-->
						<td>
						</td>
						<!--Elapsed hits-->
						<td>
						</td>
						<!--Hits required previous timeslot-->
						<td>
						</td>
						<!--Hits actual previous timeslot-->
						<td>
						</td>
						<!--Times considered prev-->
						<td>
						</td>
						<!--Hits required current timeslot-->
						<td>
						</td>
						<!--Hits actual current timeslot-->
						<td>
						</td>
						<!--Times considered-->
					</tr>
				</tfoot>
			</table>
		</td>
	</tr>
</table>

