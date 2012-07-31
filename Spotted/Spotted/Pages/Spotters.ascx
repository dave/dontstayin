<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Spotters.ascx.cs" Inherits="Spotted.Pages.Spotters" %>
<%@ Register src="/Controls/Picker.ascx" tagname="Picker" tagprefix="uc1" %>
<%@ Register TagPrefix="Controls" TagName="SpottersChecklist" Src="/Controls/SpottersChecklist.ascx" %>



<asp:Panel Runat="server" ID="PanelIntro1">
	<dsi:h1 runat="server" ID="H19" NAME="H11">Spotters</dsi:h1>
	<div class="ContentBorder">
		<p align="center">
			<img src="/gfx/spotting.jpg" class="BorderBlack All" width="450" height="110">
		</p>
		<p>
			'Spotters' are our photographers. You've probably had your photo taken by 
			a spotter at an event. That's why you're here!
		</p>
		<p>
			Being a spotter is great fun - and as a spotter we'll try to get 
			you a press pass. That means you (and usually one or two of your 
			mates) get on the guest list, and access to the VIP area (if there 
			is one).
		</p>
		<p>
			What you'll have to do as a spotter:
		</p>
		<ul>
			<li>Take photos with a digital camera*</li>
			<li>Give out DontStayIn cards&dagger;</li>
			<li>Tell people how cool DontStayIn is</li>
			<li>Write a short review of the night</li>
			<li>Upload your photos using the simple upload page</li>
		</ul>
		<p align="center">
			<asp:Button ID="Button1" Runat="server" OnClick="PanelIntro1Click" Text="Sign up as a spotter" OnClientClick="try { return WhenLoggedInButton(this); } catch(ex) { return false; }"></asp:Button>
		</p>
		<p>
			<small>
				* You'll need to own a digital camera for this (obviously!)<br>
				&dagger; We'll send you DontStayIn cards to give out when you sign up
			</small>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelNoPhoto">
	<dsi:h1 runat="server" ID="H2" NAME="H11">Spotters</dsi:h1>
	<div class="ContentBorder">
		<h2>Can't sign up as a spotter</h2>
		<p>
			This event or venue has a no-photo policy.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelSignUpForm">
	<dsi:h1 runat="server" ID="H3" NAME="H11">Spotters</dsi:h1>
	<div class="ContentBorder">
		<p>
			To become a spotter, please fill in this form below. We will send 
			you you a pack of cards to give out when you take photos.
		</p>
		<p>
			<table cellpadding="2" cellspacing="2">
				<tr>
					<th valign=top>First name</th>
					<td valign=top>
						<asp:TextBox Runat="server" ID="FirstName" Columns="20"/>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="Dynamic" ControlToValidate="FirstName" ErrorMessage="<p>Please enter a first name.</p>"/>
					</td>
				</tr>
				<tr>
					<th valign=top>Last name</th>
					<td valign=top>
						<asp:TextBox Runat="server" ID="LastName" Columns="20"/>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Runat="server" Display="Dynamic" ControlToValidate="LastName" ErrorMessage="<p>Please enter a last name.</p>"/>
					</td>
				</tr>
				<TR>
					<TH valign="top">
						Mobile number:</TH>
					<TD valign="top">
						<table cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td><small>Country&nbsp;code:</small>&nbsp;</td>
								<td width="100%">
									<asp:DropDownList Runat="server" ID="DialingCodeDropDown">
										<asp:ListItem Value="44">UK (44)</asp:ListItem>
										<asp:ListItem Value="61">Australia (61)</asp:ListItem>
										<asp:ListItem Value="33">France (33)</asp:ListItem>
										<asp:ListItem Value="49">Germany (49)</asp:ListItem>
										<asp:ListItem Value="353">Ireland (353)</asp:ListItem>
										<asp:ListItem Value="39">Italy (39)</asp:ListItem>
										<asp:ListItem Value="34">Spain (34)</asp:ListItem>
										<asp:ListItem Value="1">USA (1)</asp:ListItem>
										<asp:ListItem Value="0">Other...</asp:ListItem>
									</asp:DropDownList>
									<span runat="server" id="DialingCodeOtherSpan"><small>If other:</small>
									<asp:TextBox Runat="server" ID="DialingCodeOther" Columns="3" MaxLength="3"/></span>
								</td>
							</tr>
							<tr>
								<td>
									<small>Number:&nbsp;</small>
								</td>
								<td width="100%">
									<asp:TextBox Runat="server" ID="MobileNumber" Columns="15" MaxLength="15"/>
								</td>
							</tr>
							<tr>
								<td colspan="2">
									<small>(if your number starts with a zero, please leave this out)</small>
								</td>
							</tr>
						</table>
						<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ControlToValidate="MobileNumber" ErrorMessage="<p>Please enter your mobile number.</p>" ID="Requiredfieldvalidator3"/>
					</TD>
				</TR>
				<tr>
					<th>
						My date of birth is : 
					</th>
					<td>
						day:<asp:TextBox Runat="server" ID="DateOfBirthDay" Columns="2"/>
						month:<asp:TextBox Runat="server" ID="DateOfBirthMonth" Columns="2"/>
						year:<asp:TextBox Runat="server" ID="DateOfBirthYear" Columns="4"/>
					</td>
					<asp:CustomValidator ID="CustomValidator1" Runat="server" EnableClientScript="False" OnServerValidate="DateOfBirthVal" Display="Dynamic" ErrorMessage="<td class=dataGridDateError style=border-left-width:0px>That isn't a valid date. Please enter the full date, including day, month and full, 4-digit year.</td>"/>
					<asp:CustomValidator ID="CustomValidator2" Runat="server" EnableClientScript="False" OnServerValidate="DateOfBirthAdultVal" Display="Dynamic" ErrorMessage="<td class=dataGridDateError style=border-left-width:0px>You must be at least 18 years old to be a spotter.</td>"/>
				</tr>
				<tr>
					<th valign=top>Address</th>
					<td valign=top>
						<table cellpadding="0" cellspacing="0" border="0"
							<tr><td><asp:TextBox Runat="server" ID="AddressStreet" Columns="40"/> (street)*</td></tr>
							<tr><td><asp:TextBox Runat="server" ID="AddressArea" Columns="40"/> (area)</td></tr>
							<tr><td><asp:TextBox Runat="server" ID="AddressTown" Columns="40"/> (town / city)*</td></tr>
							<tr><td><asp:TextBox Runat="server" ID="AddressCounty" Columns="20"/> (county / state)*</td></tr>
							<tr><td><asp:TextBox Runat="server" ID="AddressPostcode" Columns="10"/> (postcode / zipcode)*</td></tr>
							<tr><td><asp:DropDownList Runat="server" ID="AddressCountry"/> (country)*</td></tr>
							<tr><td><asp:RequiredFieldValidator ID="RequiredFieldValidator4" Runat="server" Display="Dynamic" ControlToValidate="AddressStreet" ErrorMessage="<br>Please enter an address (street)."/>
							<tr><td>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Runat="server" Display="Dynamic" ControlToValidate="AddressTown" ErrorMessage="<p>Please enter an address (town / city).</p>"/>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Runat="server" Display="Dynamic" ControlToValidate="AddressCounty" ErrorMessage="<p>Please enter an address (county / state).</p>"/>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator7" Runat="server" Display="Dynamic" ControlToValidate="AddressPostcode" ErrorMessage="<p>Please enter an address (postcode / zipcode).</p>"/>
								<asp:CustomValidator ID="CustomValidator3" Runat="server" Display="Dynamic" ControlToValidate="AddressPostcode" ErrorMessage="<p>Please enter your full UK postcode.</p>" EnableClientScript="False" OnServerValidate="AddressPostcodeVal" />
								<p><small>Please enter all fields marked with the *</small></p>
							</td></tr>
						</table>
					</td>
				</tr>
				<tr>
					<th valign=top>Photo permission</th>
					<td valign=top>
						<p>
							DontStayIn is now owned by the company that publishes Mixmag and The Word magazines. We now have lots more requests to use your photos in print. We've got some options that will help us streamline these requests:
						</p>
						<p>
							<asp:RadioButton GroupName="PhotoUsage" runat="server" ID="PhotoUsageUse" Text="I am happy for my photos to be used in print, credited with my name. Send me an email if my photo is used." /><br />
							<asp:RadioButton GroupName="PhotoUsage" runat="server" ID="PhotoUsageContact" Text="Please contact me by email, and don't use my photo until I respond." /><br />
							<asp:RadioButton GroupName="PhotoUsage" runat="server" ID="PhotoUsageDoNotUse" Text="Please DO NOT use my photos." />
						</p>
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>
						<asp:Button Runat="server" OnClick="PanelSignUpFormClick" Text="Save" ID="SaveSpotterButton"/>
						<asp:Label Runat="server" ForeColor="#0000ff" ID="SpotterSavedLabel" Visible="False">Details saved</asp:Label>
					</td>
				</tr>
			</table>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelChecklist">
	<dsi:h1 runat="server" ID="H4" NAME="H11">Spotters</dsi:h1>
	<div class="ContentBorder">
		<p>
			We've put together a quick list of the things you need to know to be a 
			spotter. Please read all the points below, and tick each one after 
			you've read it.
		</p>
		<asp:CustomValidator ID="CustomValidator4" Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="CheklistVal" ErrorMessage="<p>Please read all the points above, and tick each one.</p>"></asp:CustomValidator>
		<Controls:SpottersChecklist runat="server" ID="Checklist"/>
		<asp:CustomValidator ID="CustomValidator5" Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="CheklistVal" ErrorMessage="<p>Please read all the points above, and tick each one.</p>"></asp:CustomValidator>
		<p>
			<asp:Button ID="Button4" Runat="server" onclick="SaveSpotter" Text="Done"/>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelEnabled">
	<asp:Panel Runat="server" ID="GuestlistPanel">
		<dsi:h1 runat="server" ID="H11">Free Guestlist</dsi:h1>
		<div class="ContentBorder">
			<img src="/gfx/new-user-freeguestlist.png" width="69" height="43" border="0" align="left" style="margin:10px 5px 15px 5px;" />
			<p>
				Our spotters get free entry at lots of events. Call the promoter to organise your 
				exclusive Free Guestlist - you'll find the number on the event page.
			</p>
			<p>
				<b>You will be refused free entry if you don't have Don't Stay In cards to give out.</b>
				Click the <i>Request more cards</i> button below and we'll post you some for free.
			</p>
			<p class="BigCenter">
				<a href="/pages/freeguestlist">Click here to find Free Guestlist events</a>
			</p>
		</div>
	</asp:Panel>
	
	<dsi:h1 runat="server" ID="H1">Your spotter code</dsi:h1>
	<div class="ContentBorder ClearAfter">
		<div style="float:right; margin-top:10px; margin-left:5px; margin-right:10px;">
			<center>
				<a href="/support/buystamp.aspx" target="_blank"><img src="<%= Bobs.Storage.Path(new Guid("20f08473-0977-4804-8088-6019f2c05f66"), "gif") %>" width="54" height="80" border="0" class="Block" />Click to buy<br />a stamp!</a>
			</center>
		</div>
		<p class="BigCenter">Your spotter code is: <%= CurrentUsr.SpotterCode %></p>
		<p>
			<b>Write this on the back of your spotter cards so people you spot can find your photos.</b>
		</p>
		<p>
			You can order a little ink stamp for a few pounds by <a href="/support/buystamp.aspx" target="_blank">clicking here</a>. The nice people at Stamps Direct will send it out right away, customised with your spotter code. 
		</p>
		
	</div>
	
	<asp:Panel Runat="server" ID="RequestCardsPanel">
		<a name="RequestCards"></a>
		<dsi:h1 runat="server" ID="H15">Need more cards?</dsi:h1>
		<div class="ContentBorder">
			<p>
				Have you run out of cards? Do you not have enough to give out at your next event?
				Don't worry - we can send you more cards... just click below:
			</p>
			<p>
				<asp:Button ID="Button7" Runat="server" Text="Request more cards" OnClick="RequestCards" CausesValidation="False"/>
			</p>
			<asp:Panel Runat="server" id="RequestCardsError2" Visible="False">
				<p style="color:red;">
					You've only just signed up as a spotter. We will send you a pack of 
					cards with your welcome pack.
				</p>
			</asp:Panel>
			<asp:Panel Runat="server" id="RequestCardsError3" Visible="False">
				<p style="color:red;">
					Your cards are in the post. If you don't receive them within 
					three days, they may have been lost in the post. First check the 
					address at the top of the page is correct, then click 
					"I've received my cards", then "Request more cards" again.
				</p>
			</asp:Panel>
			<asp:Panel Runat="server" id="RequestCardsError4" Visible="False">
				<p style="color:red;">
					We know you need cards, and we'll post them as soon as we can.
				</p>
			</asp:Panel>
			<p>
				Your current cards status: <asp:Label Runat="server" ID="RequestCardsStatusLabel"></asp:Label>
			</p>
			<asp:Panel Runat="server" id="PanelStatusCardsInPost" Visible="False">
				<p>
					Your cards are in the post. When you receive them, click 
					<b><asp:LinkButton ID="LinkButton1" Runat="server" OnClick="ResetCards" CausesValidation="False">I've received my cards</asp:LinkButton></b>.
				</p>
			</asp:Panel>
		</div>
		<asp:Panel Runat="server" ID="PanelStatusNoCardsInPost">
			<dsi:h1 runat="server" ID="H18" NAME="H12">Cards - IMPORTANT INFORMATION</dsi:h1>
			<div class="ContentBorder" style="background-color:#ff9999;">	
				<p>
					We've run out of cards, and don't have the money to buy more. We have 
					NOT sent you more cards, but you CAN still cover events. Simply download the 
					template below, and print onto A4. Cut out the cards, and hand them out. We're
					really sorry about this, and we're working hard on ways to get more cards. When 
					we have more cards, we'll send you some.
				</p>
				<p>
					<a href="/pages/icons">Why not buy an icon?</a>
				</p>
			</div>
		</asp:Panel>
	</asp:Panel>
	
	
	<dsi:h1 runat="server">Spotter details</dsi:h1>
	<div class="ContentBorder">	
		<p>
			Your spotter details are listed below. 
		</p>
		<p>
			<table cellpadding="2" cellspacing="2" border="0">
				<tr><th>Name</th>					<td><%= CurrentUsr.FirstName %> <%= CurrentUsr.LastName %></td></tr>
				<tr><th>Email</th>					<td><%= CurrentUsr.Email %></td></tr>
				<tr><th>Mobile</th>					<td>+<%= CurrentUsr.Mobile %></td></tr>
				<tr><th valign="top">Address</th>	<td><%= CurrentUsr.AddressStreet %><br>
														<%= CurrentUsr.AddressArea %><br>
														<%= CurrentUsr.AddressTown %><br>
														<%= CurrentUsr.AddressCounty %><br>
														<%= CurrentUsr.AddressPostcode %><br>
														<asp:Label Runat="server" ID="SpotterAddressCountryName"></asp:Label></td></tr>
				<tr><th>Permissions</th>			<td><%= CurrentUsr.PhotoUsageString %></td></tr>
			</table>
		</p>
		<p>
			<asp:Button ID="Button5" Runat="server" onclick="ChangeSpotterDetails" Text="Change these details" CausesValidation="False"/>
		</p>
	</div>
	
	<asp:Panel Runat="server" ID="OptionsPanel" Visible="False">
		<dsi:h1 runat="server">Sign-up to cover an event</dsi:h1>
		<div class="ContentBorder">
			<p>
				Want to cover an event? Select the event below:
			</p>
			<p>
				<uc1:Picker ID="uiEventPicker" runat="server" />
				<p>
					<asp:Button ID="Button6" Runat="server" OnClick="PanelEnabledSignUpClick" 
						Text="Cover this event" />
				</p>
				<asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="uiEventPicker"
					ErrorMessage="<p>Please select an event!</p>" />
				
				<h2>
					Want to cover an event that isn't on the site yet?
				</h2>
				<p>
					<a href="/pages/events/edit/signup-1">Add it and sign-up</a>.
				</p>
			</p>
		</div>
		
	</asp:Panel>
	<asp:Panel Runat="server" ID="EventsPanel">
		<dsi:h1 runat="server" ID="H13">Events you're signed up to cover</dsi:h1>
		<div class="ContentBorder">
			<p>
				When you're ready to upload your photos, click the <b>Options</b> link in 
				the table below.
			</p>
			<p>
				<asp:DataGrid Runat="server" ID="EventsDataGrid" 
					GridLines="None" AutoGenerateColumns="False"
					BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					AlternatingItemStyle-CssClass="dataGridAltItem"
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Top" AllowPaging="True" 
					PageSize="20" PagerStyle-Mode="NumericPages"
					OnPageIndexChanged="EventsDataGridChangePage">
					<Columns>
						<asp:TemplateColumn HeaderText="Name">
							<ItemTemplate>
								<a href="<%#((Bobs.Event)(Container.DataItem)).Url()%>"><%#((Bobs.Event)(Container.DataItem)).Name%></a> <small>@ <a href="<%#((Bobs.Event)(Container.DataItem)).Venue.Url()%>"><%#((Bobs.Event)(Container.DataItem)).Venue.Name%></a></small>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Date">
							<ItemTemplate>
								<nobr><%#((Bobs.Event)(Container.DataItem)).FriendlyDate(true)%></nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Photos">
							<ItemTemplate>
								<nobr><%#((Bobs.Event)(Container.DataItem)).TotalPhotos%> (<%#((Bobs.Event)(Container.DataItem)).LivePhotos%> live)</nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Photos">
							<ItemTemplate>
								<%#PhotosHtml(((Bobs.Event)(Container.DataItem)))%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Review">
							<ItemTemplate>
								<%#ReviewHtml(((Bobs.Event)(Container.DataItem)))%>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
		</div>
	</asp:Panel>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelSignedUp">
	<dsi:h1 runat="server" ID="H5" NAME="H11">Spotters</dsi:h1>
	<div class="ContentBorder">
		<p>
			You're now signed up to cover <b><a href="" Runat="server" ID="PanelSignedUpEventLink"/></b>.
		</p>
		<p>
			You can find a list of events you're signed up for on the 
			<a href="/pages/spotters">Spotters page</a>.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelAlreadySignedUp">
	<dsi:h1 runat="server" ID="H6" NAME="H11">Spotters</dsi:h1>
	<div class="ContentBorder">
		<h2>Already signed-up</h2>
		<p>
			You've already signed up to cover <b><asp:Label Runat="server" ID="PanelAlreadySignedUpEventLabel"></asp:Label></b>. 
			Details are available on the <a href="" runat="server" id="PanelAlreadySignedUpEventLink">event page</a>.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelPastEventConfirm">
	<dsi:h1 runat="server" ID="H7" NAME="H11">Spotters</dsi:h1>
	<div class="ContentBorder">
		<h2>Event has already happened</h2>
		<p>
			<b><asp:Label Runat="server" ID="PanelPastEventConfirmLabel"/></b> has already 
			happened. Please confirm you'd like to sign up to cover this event by clicking below:
		</p>
		<p>
			<asp:Button ID="Button8" Runat="server" onclick="PanelPastEventConfirmClick" Text="Yes, sign up to cover this event"></asp:Button>
		</p>
		<p>
			<asp:Button ID="Button9" Runat="server" onclick="PanelPastEventBackClick" Text="Oops, I selected the wrong event"></asp:Button>
		</p>
	</div>
</asp:Panel>
