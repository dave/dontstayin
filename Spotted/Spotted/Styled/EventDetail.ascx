<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventDetail.ascx.cs" Inherits="Spotted.Styled.EventDetail" %>
<h2 class="EventText" id="EventHeader" runat="server"></h2>
<hr />
<div class="InnerDiv">
	<p>
		<div class="EventDetails">
			<img ID="EventPic" runat="server" class="EventPic" src="gfx/frantic-event.jpg" align="left"/>
			<div style="float:left;">
				<asp:Repeater ID="RunningTicketRunsForPromoterRepeater" runat="server">
					<ItemTemplate>
						<div class="TicketText" style="text-align:left;"><asp:TextBox ID="TicketRunKTextBox" runat="server" Text="<%#((Bobs.TicketRun)(Container.DataItem)).K%>" Visible="false">
							</asp:TextBox><select id="NumberOfTicketsDropDownList" runat="server" class="TicketsDropDownList">
													<option value="0">0</option>
													<option value="1">1</option>								
													<option value="2">2</option>
													<option value="3">3</option>
													<option value="4">4</option>
													<option value="5">5</option>
													<option value="6">6</option>
													<option value="7">7</option>
													<option value="8">8</option>
													<option value="9">9</option>
													<option value="10">10</option>
												</select><asp:Label ID="TicketRunLabel" runat="server" class="TicketRunInfo" Text='<%# " x " + ((Bobs.TicketRun)Container.DataItem).Price.ToString("c") + " " + ((Bobs.TicketRun)Container.DataItem).Name %>'></asp:Label><br /><small><%# ((Bobs.TicketRun)Container.DataItem).Description %></small></div>	
					</ItemTemplate>
					<FooterTemplate><button class="BuyButton" id="BuyButton" runat="server" onserverclick="BuyButton_Click">Buy</button><div class="BringCardDiv"><br /><%= Ticket.ETICKET_CARD_REMINDER_PLURAL %><br />&nbsp;</div></FooterTemplate>
				</asp:Repeater>
			</div>
			<div style="height:100px;" id="NoTicketsAvailableDiv" runat="server">No tickets currently available for this event.</div>
			<p id="TicketNoteP" runat="server"><asp:Label ID="TicketNoteLabel" runat="server" Font-Bold="true"></asp:Label></p>
			<asp:CustomValidator ID="ProcessingVal" Runat="server" Display="None" ValidationGroup="BuyTicketsValidation" EnableClientScript="False" ErrorMessage="Error processing tickets. Please try again."/>
			<asp:ValidationSummary ID="TicketValidationSummary" BorderWidth="2" Runat="server" EnableClientScript="False" ShowSummary="True" HeaderText="&nbsp;There were some errors:" CssClass="PaymentValidationSummary" Width="100%" Font-Bold="True" DisplayMode="BulletList" ValidationGroup="BuyTicketsValidation"/>	
		</div>
	</p>

	<p>
		<div id="EventShortDescription" runat="server" class="EventShortDescription" style="width:100%"></div>
	</p>
</div>
