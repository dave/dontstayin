<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Guestlists.ascx.cs" Inherits="Spotted.Pages.Promoters.Guestlists" %>

<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>



<asp:Panel Runat="server" ID="PanelBuy" Visible="false">
	<dsi:h1 runat="server" ID="H17">Top-up your guestlist account with credits</dsi:h1>
	<div class="ContentBorder">
		<p>
			You can buy extra guestlist credits here. Guestlist credits cost 
			<%= CurrentPromoter.GuestlistCharge.ToString("c") %> each.
		</p>
		<p>
			Enter a number below and click purchase. Your account will be 
			credited when we receive confirmation of your transaction.
		</p>
		<p>
			Buy <asp:TextBox Runat="server" ID="BuyCredits" Columns="5">100</asp:TextBox> credits <small>(minimum 100 credits)</small>
		</p>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="Dynamic" ControlToValidate="BuyCredits" 
			ErrorMessage="<p>Please enter a number above</p>"/>
		<asp:CompareValidator ID="CompareValidator1" Runat="server" Display="Dynamic" ControlToValidate="BuyCredits" Type="Integer" Operator="GreaterThanEqual" ValueToCompare="100"
			ErrorMessage="<p>The minimum number of credits you can buy is 100.</p>"/>
		<p>
			<button Runat="server" onserverclick="Buy_Cancel" causesvalidation="false" ID="Button1">&lt;- Cancel</button>
			<asp:Button ID="Button2" Runat="server" Text="Buy now -&gt;" OnClick="Buy_Click"></asp:Button>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelPay"  Visible="false">
	<dsi:h1 runat="server" ID="H18">Top-up your guestlist account with credits</dsi:h1>
	<div class="ContentBorder">
		<p>
			<Controls:Payment id="Payment" Runat="server" 
				OnPaymentDone="PaymentReceived" />
		</p>
		<p>
			<button Runat="server" onserverclick="Pay_Cancel" causesvalidation="false" ID="Button3">&lt;- Cancel</button>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelPayDone" Visible="false">
	<dsi:h1 runat="server" ID="H19">Top-up complete</dsi:h1>
	<div class="ContentBorder">
		<p>
			Thanks for topping up your account. <a href="<%= CurrentPromoter.UrlApp("guestlists") %>">Back to guestlist options</a>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelEdit" Visible="false">
	<dsi:h1 runat="server" ID="H14">Add a guestlist</dsi:h1>
	<div class="ContentBorder">
		<table cellpadding="5" cellspacing="2">
			<tr runat="server" id="EditEventTr">
				<td colspan="3">
					Select the event:
				</td>
			</tr>
			<tr runat="server" id="EditEventTr1">
				<td colspan="3">
					<asp:DropDownList Runat="server" ID="EditEventDropDown"/>
					<asp:CustomValidator ID="CustomValidator1" Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="EditEvent_Val" ErrorMessage="<br>This event already has a guestlist. Please choose another."/>
				</td>
			</tr>
			<tr>
				<td>Guestlist entry price (£)</td>
				<td>
					<asp:TextBox Runat="server" ID="EditPriceTextBox" Columns="5"></asp:TextBox>
				</td>
				<td>
					<small>This is the reduced entry price for people on this guestlist</small>
					<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ControlToValidate="EditPriceTextBox" ErrorMessage="<br>Please enter a price here" ID="Requiredfieldvalidator2" NAME="Requiredfieldvalidator1"/>
					<asp:CompareValidator Runat="server" Display="Dynamic" ControlToValidate="EditPriceTextBox" ErrorMessage="<br>Please only numbers here" Operator="DataTypeCheck" Type="Currency" ID="Comparevalidator2" NAME="Comparevalidator1"/>
				</td>
			</tr>
			
			<tr>
				<td>Normal entry price (£)</td>
				<td>
					<asp:TextBox Runat="server" ID="EditRegularPriceTextBox" Columns="5"></asp:TextBox>
				</td>
				<td>
					<small>This is the normal price for people NOT on the guestlist</small>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Runat="server" Display="Dynamic" ControlToValidate="EditRegularPriceTextBox" ErrorMessage="<br>Please enter a price here"/>
					<asp:CompareValidator ID="CompareValidator3" Runat="server" Display="Dynamic" ControlToValidate="EditRegularPriceTextBox" ErrorMessage="<br>Please only numbers here" Operator="DataTypeCheck" Type="Currency"/>
					<asp:CompareValidator ID="CompareValidator4" Runat="server" Display="Dynamic" ControlToCompare="EditPriceTextBox" ControlToValidate="EditRegularPriceTextBox" ErrorMessage="<br>The normal entry price should be GREATER than OR EAQUAL to the guestlist price." Operator="GreaterThanEqual" Type="Currency"/>
				</td>
			</tr>
			
			<tr>
				<td colspan="3">
					<small>If you're not offering a discount on the entry price, enter the same value in both the boxes above.</small>
				</td>
			</tr>
			
			<tr>
				<td>Additional details</td>
				<td colspan="2">
					<asp:TextBox Runat="server" ID="EditDetails" Columns="70"></asp:TextBox>
				</td>
			</tr>
			
			<tr>
				<td colspan="3">
					<small>Enter any other relevant details here - e.g. list closure time, queueing benefits etc.</small>
				</td>
			</tr>
			
			<tr>
				<td>Guestlist spaces</td>
				<td>
					<asp:TextBox Runat="server" ID="EditLimit" Columns="5"></asp:TextBox>
				</td>
				<td>
					<small>
						This is the maximum number of people that will go on your list. You must have enough 
						<i>guestlist-credits</i> available to cover this number. You can buy additional credits 
						<a href="<%= CurrentPromoter.UrlApp("guestlists","mode","buy") %>">by clicking here</a>.
					</small>
					<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ControlToValidate="EditLimit" ErrorMessage="<br>Please enter a price here" ID="Requiredfieldvalidator4" NAME="Requiredfieldvalidator2"/>
					<asp:CompareValidator ID="CompareValidator5" Runat="server" Display="Dynamic" ControlToValidate="EditLimit" ErrorMessage="<br>Please only numbers here" Operator="GreaterThan" ValueToCompare="0" Type="Integer"/>
					<asp:CustomValidator Runat="server" Display="Dynamic" OnServerValidate="EditLimit_CreditVal" EnableClientScript="False" ErrorMessage="<br>You must have enough <i>guestlist-credits</i> available to cover this number. You can buy additional credits by clicking the link above." ID="Customvalidator2" NAME="Customvalidator1"></asp:CustomValidator>
					<asp:CustomValidator Runat="server" Display="Dynamic" OnServerValidate="EditLimit_CountVal" EnableClientScript="False" ErrorMessage="<br>You can't change the limit to less than the number of people currently on the list!" ID="Customvalidator3" NAME="Customvalidator2"></asp:CustomValidator>
				</td>
			</tr>
			
		</table>

		<p>
			<button id="Button4" Runat="server" onserverclick="PanelEdit_Cancel" causesvalidation="false">&lt;- Cancel</button>
			<asp:Button ID="Button5" Runat="server" OnClick="PanelEdit_Save" Text="Save this guestlist -&gt;"></asp:Button>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelAddError" Visible="false">
	<dsi:h1 runat="server" ID="H13">Can't add a guestlist!</dsi:h1>
	<div class="ContentBorder">
		<p class="BigCenter">
			Add an event first!	
		</p>
		<p>
			You can't add a guestlist because you don't have any upcoming events. <a href="/pages/events/edit">Click here to add an event</a>.
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelAddCreditsError" Visible="false">
	<dsi:h1 runat="server" ID="H16">Can't add a guestlist!</dsi:h1>
	<div class="ContentBorder">
		<p class="BigCenter">
			Top-up your credits first!
		</p>
		<p>
			You can't add a guestlist because you don't have enough guestlist credits. You can buy additional credits 
			<a href="<%= CurrentPromoter.UrlApp("guestlists","mode","buy") %>">by clicking here</a>.
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelClose" Visible="false">
	<dsi:h1 runat="server" ID="H12">Closing your guestlist</dsi:h1>
	<div class="ContentBorder">
		<p>
			This page will close your guestlist. This guestlist is for 
			<asp:Label Runat="server" ID="PanelCloseEventLabel"></asp:Label>.
			<asp:Label Runat="server" ID="PanelCloseCountLabel"></asp:Label>
		</p>
		<p>
			Once you've closed the list, the number of names on it is deducted 
			from your guestlist credits.
			The list will be permanently closed - you can't re-open it.
		</p>
		<p>
			Once the list is closed, you're free to download and print the list 
			of names.
		</p>
		<p>
			<button id="Button6" Runat="server" onserverclick="PanelClose_Cancel" causesvalidation="false">&lt;- Cancel</button>
			<asp:Button ID="Button7" Runat="server" OnClick="PanelClose_Close" Text="Close this list -&gt;"></asp:Button>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelList" Visible="false">
	<dsi:h1 runat="server" ID="H11">Your guestlists</dsi:h1>
	<div class="ContentBorder">
		<p>
			Below are listed your guestlists:
		</p>
		<p>
			<asp:Label Runat="server" ID="NoGuestlistsLabel" Visible="False">You don't have any guestlists set up.</asp:Label>
			<asp:DataGrid Runat="server" ID="EventsDataGrid" 
				GridLines="None" AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				AlternatingItemStyle-CssClass="dataGridAltItem"
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="EventsDataGridChangePage"
				PageSize="20" PagerStyle-Mode="NumericPages">
				<Columns>
					<asp:TemplateColumn HeaderText="Event">
						<ItemTemplate>
							<a href="<%#((Bobs.Event)Container.DataItem).Url()%>"><%#((Bobs.Event)(Container.DataItem)).Name%></a><br>
							<nobr><%#((Bobs.Event)Container.DataItem).FriendlyDate(true)%></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status">
						<ItemTemplate>
							<%#((Bobs.Event)Container.DataItem).GuestlistOpen?"Open":(((Bobs.Event)(Container.DataItem)).GuestlistFinished?"Closed":"Paused")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="On list /<br>Limit /<br>Spaces">
						<ItemTemplate>
							<nobr><%#((Bobs.Event)Container.DataItem).GuestlistCount%> / 
							<%#((Bobs.Event)Container.DataItem).GuestlistLimit%> / 
							<%#((Bobs.Event)Container.DataItem).GuestlistLimit - ((Bobs.Event)(Container.DataItem)).GuestlistCount%></nobr>
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
