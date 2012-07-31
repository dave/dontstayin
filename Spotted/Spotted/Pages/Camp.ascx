<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Camp.ascx.cs" Inherits="Spotted.Pages.Camp" %>

<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>

<dsi:h1 runat="Server">Camp DSI 2006</dsi:h1>
	<div class="ContentBorder">
					<table cellpadding="0" cellspacing="0" border="0" width="100%">
						<tr>
							<td id="Content_EventPicCell" valign="top" style="padding-right:7px;">
								<p><a href="/uk/barnstaple/a-secret-location/2006/jun/17/event-29398"><img src="/gfx/camp-dsi.jpg" id="Content_EventPicImg" class="BorderBlack All" width="100" height="100" /></a></p>
							</td>
		
							<td width="100%" valign="top">
								<p>
									<small>
										<span id="Content_EventDate">Sat 17 Jun</span>
										<span id="Content_EventStartTime"></span>
										@ <a href="/uk/barnstaple/little-roadway-farm-campsite" id="Content_EventVenueLink">Little Roadway Farm Campsite</a> 
										in <a href="/uk/barnstaple" id="Content_EventPlaceLink">Barnstaple &amp; North Devon</a>
									</small>
								</p>
								
								<p>
									<span id="Content_MapSpan">
										<a href="http://maps.google.com/maps?q=EX34+7HL&spn=0.098633,0.118808&hl=en" id="Content_MapLink" onclick="overrideMapOpen('http://maps.google.com/maps?q=EX34+7HL&amp;spn=0.098633,0.118808&amp;hl=en');return false;"><img src="/gfx/icon-map.png" border="0" align="absmiddle" style="margin-right:3px;">Google map of the venue</a> and <a href="http://maps.google.co.uk/maps?saddr=SW66NU(DaveB-DSI's+house)&daddr=EX337HL(Camp+DSI+2006+%40+Little+Roadway+Farm+Campsite)" id="Content_DirectionsLink" onclick="mapOpen('http://maps.google.co.uk/maps?saddr=SW66NU(DaveB-DSI\'s+house)&amp;daddr=EX337HL(Camp+DSI+2006+%40+Little+Roadway+Farm+Campsite)');return false;">directions</a>
										<script>
											function mapOpen(url)
											{
												var popUp = window.open(url, popUp, 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=890,height=600');
												popUp.focus();
											}
											function overrideMapOpen(url)
											{
												var popUp = window.open(url, popUp, 'toolbar=1,scrollbars=1,location=0,statusbar=1,menubar=1,resizable=1,width=600,height=500');
												popUp.focus();
											}
										</script>
									</span>
								</p>
								<p>
									<a href="/uk/barnstaple/little-roadway-farm-campsite/2006/jun/17/event-29398/chat" id="Content_DiscussionLink"><img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;">Chat about this event<span id="Content_DiscussionLinkCommentsLabel"> - 1,402 comments</span></a>
								</p>
								<div id="Content_BrandsPanel">
			
									<p style="font-size:14px;font-weight:bold;">
										<img src="/gfx/icon-group.png" border="0" align="absmiddle" style="margin-right:3px;"><a href="/parties/party-liaison">PARTY LIAISON</a>
									</p>
								
		</div>
								
							</td>
						</tr>
					</table>
</div>

<asp:Panel Runat="server" ID="QuantityPanel">
	<dsi:h1 runat="Server">Camp DSI 2006 payment page</dsi:h1>
	<div class="ContentBorder">
	
		<p>
			That's right campers! It's that time of year again! Camp DSI will be taking place on Friday 
			16th to Sunday the 18th June. This year we will have our own field, themed tents, sports day, 
			marquee and sound system! This is one time in the year where EVERYONE who loves DSI can get 
			together in one place, so please make the effort and come.
		</p>

		<h2>Your name</h2> 
		<p>
			Your ticket will be in the name "<asp:Label runat="server" ID="NameLabel"/>". If this isn't 
			correct, please <a href="/pages/mydetails">change it now</a>. You may need to provide ID 
			showing this name to get in.
		</p>

		<h2>Money</h2>
		<p>
			Admission for this weekend event is £20 per person. You can buy up to 5 tickets for you 
			and your mates.
		</p>
		
		<p runat="server" id="AlreadyHaveTicketsP"></p>
		
		<p>
			Number of <asp:Label runat="server" ID="AlreadyHaveTicketsLabel"/> tickets: 
			<asp:TextBox runat="server" ID="TicketsQuantityTextBox" Columns="5" Text="1" />
		</p>
		
		<p>
			<asp:Button ID="Button1" runat="server" OnClick="QuantityNext_Click" Text="Next -&gt;" />
		</p>
		
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="QuantityErrorPanel">
	<dsi:h1 runat="Server">Error</dsi:h1>
	<div class="ContentBorder">
		<p>
			You can only have a maximum of 5 tickets.
		</p>
		<p runat="server" id="QuantityErrorP"/>
		<p>
			<asp:Button ID="Button2" runat="server" OnClick="PayCancel_Click" Text="&lt;- Back" />
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PayPanel">
	<dsi:h1 runat="Server">Camp DSI payment page</dsi:h1>
	<div class="ContentBorder">
		<p>
			<Controls:Payment runat="server" id="TicketPayment" OnPaymentDone="PaymentReceived"/>
		</p>
		<p>
			<asp:Button runat="server" OnClick="PayCancel_Click" Text="&lt;- Cancel" />
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="DonePanel">
	<dsi:h1 runat="Server">Camp DSI payment page - thanks!</dsi:h1>
	<div class="ContentBorder">
		<p>
			Congratulations - you have bought <asp:Label runat="server" ID="DoneQuantityLabel" /> to Camp DSI 2006.
		</p>
		<p>
			Please regularly check the Camp DSI event listing for further information and updates.
		</p>
		<p class="BigCenter">
			<a href="/uk/barnstaple/a-secret-location/2006/jun/17/event-29398">Camp DSI 2006 event page</a>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="SoldOutPanel">
	<dsi:h1 runat="Server">Camp DSI 2006 SOLD OUT!</dsi:h1>
	<div class="ContentBorder">
		<p>
			Sorry, we've sold out of tickets.
		</p>
		<p class="BigCenter">
			<a href="/uk/barnstaple/a-secret-location/2006/jun/17/event-29398">Camp DSI 2006 event page</a>
		</p>
	</div>
</asp:Panel>