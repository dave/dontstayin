<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="Spotted.Pages.Promoters.Edit" %>
<%@ Register TagPrefix="Spotted" TagName="Terms" Src="/Controls/LegalTermsPromoter.ascx" %>
<%@ Register TagPrefix="Controls" TagName="Pic" Src="/Controls/Pic.ascx" %>
<asp:Panel Runat="server" ID="PanelSignUpForm">
	<dsi:h1 ID="PanelSignUpFormHeading" runat="server">
		Promoter account application</dsi:h1>
	<div class="ContentBorder">
		<p runat="server" id="DuplicateAccountP">
			<div style="margin:5px; padding:10px; border:2px solid #ff0000;">
				If you run your promotion with a friend, first ask them if they've 
				signed up a DontStayIn promoter account already. If so, don't fill 
				in this form - just ring us in 0207 835 5599 and we'll add you 
				to their account.
			</div>
		</p>
		<p>
			To become a registered promoter just fill in this form below:
		</p>
		<asp:ValidationSummary Runat="server" ShowSummary="True" HeaderText="You've made some mistakes" CssClass="ValidationSummaryDiv" Font-Bold="True" DisplayMode="BulletList" ID="Validationsummary1" NAME="Validationsummary1"/>
		<h2>Contact details</h2>
		<p>
			<table cellpadding="2" cellspacing="2">
				<tr>
					<th valign=top width="100">Contact name</th>
					<td valign=top>
						<asp:TextBox Runat="server" ID="ContactName" Columns="40"/> <small>(full name of the primary contact)</small>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="Dynamic" ControlToValidate="ContactName" ErrorMessage="<p>Please enter a contact name.</p>"/>
					</td>
				</tr>
				<tr>
					<th valign=top>Organisation name</th>
					<td valign=top>
						<asp:TextBox Runat="server" ID="Name" Columns="40"/> <small>(the company or brand name)</small>
						<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ControlToValidate="Name" ErrorMessage="<p>Please enter a name.</p>" ID="Requiredfieldvalidator2" NAME="Requiredfieldvalidator1"/>
					</td>
				</tr>
				<TR>
					<TH valign="top">
						Phone number</TH>
					<TD valign="top">
						<asp:TextBox Runat="server" ID="PhoneNumber" Columns="30"/>
						<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ControlToValidate="PhoneNumber" ErrorMessage="<p>Please enter a contact phone number.</p>" ID="Requiredfieldvalidator5" NAME="Requiredfieldvalidator1"/>
					</TD>
				</TR>
				<tr>
					<th valign=top>Address</th>
					<td valign=top>
						<table cellpadding="0" cellspacing="0" border="0">
							<tr><td><asp:TextBox Runat="server" ID="AddressStreet" Columns="40"/> (street)*</td></tr>
							<tr><td><asp:TextBox Runat="server" ID="AddressArea" Columns="40"/> (area)</td></tr>
							<tr><td><asp:TextBox Runat="server" ID="AddressTown" Columns="40"/> (town / city)*</td></tr>
							<tr><td><asp:TextBox Runat="server" ID="AddressCounty" Columns="20"/> (county / state)</td></tr>
							<tr><td><asp:TextBox Runat="server" ID="AddressPostcode" Columns="10"/> (postcode / zipcode)*</td></tr>
							<tr><td><asp:DropDownList Runat="server" ID="AddressCountry"/> (country)*</td></tr>
							<tr>
								<td>
									<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ControlToValidate="AddressStreet" ErrorMessage="<p>Please enter an address (street).</p>" ID="Requiredfieldvalidator6" NAME="Requiredfieldvalidator5"/>
									<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ControlToValidate="AddressTown" ErrorMessage="<p>Please enter an address (town / city).</p>" ID="Requiredfieldvalidator7" NAME="Requiredfieldvalidator6"/>
									<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ControlToValidate="AddressCounty" Enabled="false" ErrorMessage="<p>Please enter an address (county / state).</p>" ID="Requiredfieldvalidator8" NAME="Requiredfieldvalidator7"/>
									<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ControlToValidate="AddressPostcode" ErrorMessage="<p>Please enter an address (postcode / zipcode).</p>" ID="Requiredfieldvalidator9" NAME="Requiredfieldvalidator8"/>
									<asp:CustomValidator Runat="server" Display="Dynamic" ControlToValidate="AddressPostcode" ErrorMessage="<p>Please enter your full UK postcode.</p>" EnableClientScript="False" OnServerValidate="AddressPostcodeVal" ID="Customvalidator1" NAME="Customvalidator1"/>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TH valign="top">
						VAT registration</TH>
					<TD valign="top">
						<table cellpadding="0" cellspacing="0" border="0">
							<tr><td><asp:DropDownList Runat="server" ID="VatStatusDropDownList"/> (VAT registration status)*</td></tr>
							<tr><td><asp:TextBox Runat="server" ID="VatNumberTextBox" Columns="20"/> (VAT number)</td></tr>
							<tr><td><asp:DropDownList Runat="server" ID="VatCountryDropDownList"/> (VAT registration country)</td></tr>
							<tr>
								<td>
									<asp:RequiredFieldValidator Runat="server" Enabled="false" Display="Dynamic" ControlToValidate="VatStatusDropDownList" InitialValue="0" ErrorMessage="<p>Please select your VAT registration status.</p>" ID="VatStatusRequiredFieldValidator"/>
									<asp:CustomValidator Runat="server" Display="Dynamic" ControlToValidate="VatStatusDropDownList" ErrorMessage="" EnableClientScript="False" OnServerValidate="VatStatusVal" ID="VatStatusCustomValidator"/>
									<p><small>Please enter all fields marked with the *</small></p>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</table>
		</p>
		<asp:Panel runat="server" ID="ExtraContactDetailsPanel" Visible="false">
			<h2>Extra contact details (admin only)</h2>
			<p>
				<table cellpadding="2" cellspacing="2">
					<tr>
						<th valign=top width="100"><nobr>Main contact job title</nobr></th>
						<td valign=top>
							<asp:TextBox Runat="server" ID="JobTitle" Columns="40"/>
						</td>
					</tr>
					<tr>
						<th valign=top width="100"><nobr>Main contact personal title</nobr></th>
						<td valign=top>
							<asp:TextBox Runat="server" ID="PersonalTitle" Columns="40"/>
						</td>
					</tr>					
					<tr>
						<th valign=top width="100"><nobr>Phone number 2</nobr></th>
						<td valign=top>
							<asp:TextBox Runat="server" ID="PhoneNumber2" Columns="30"/>
						</td>
					</tr>
					<tr>
						<th valign=top width="100"><nobr>Accounts contact name</nobr></th>
						<td valign=top>
							<asp:TextBox Runat="server" ID="AccountsName" Columns="40"/>
						</td>
					</tr>					
					<tr>
						<th valign=top width="100"><nobr>Accounts contact email</nobr></th>
						<td valign=top>
							<asp:TextBox Runat="server" ID="AccountsEmail" Columns="40"/>
							<asp:CustomValidator Runat="server" Display="Dynamic" ControlToValidate="AccountsEmail" ErrorMessage="Invalid email" EnableClientScript="False" OnServerValidate="EmailCustVal" ID="AccountsEmailCustomValidator"/>
						</td>
					</tr>
					<tr>
						<th valign=top width="100"><nobr>Accounts contact phone</nobr></th>
						<td valign=top>
							<asp:TextBox Runat="server" ID="AccountsPhone" Columns="40"/>
						</td>
					</tr>
					<tr>
						<th valign=top width="100"><nobr>Web address</nobr></th>
						<td valign=top>
							<asp:TextBox Runat="server" ID="WebAddress" Columns="40"/>
						</td>
					</tr>
					<tr>
						<th valign=top width="100">Sector</th>
						<td valign=top>
							<asp:DropDownList runat="server" ID="Sector"></asp:DropDownList>
						</td>
					</tr>
					<tr>
						<th valign=top width="100">Is an agency</th>
						<td valign=top>
							<asp:CheckBox runat="server" ID="uiAgency" />
						</td>
					</tr>
					<tr>
						<th valign=top width="100">Sales campaign</th>
						<td valign=top>
							<asp:DropDownList runat="server" ID="SalesCampaignDropDown"></asp:DropDownList>
						</td>
					</tr>
				</table>
			</p>
		</asp:Panel>
		<asp:Panel runat="server" ID="BankAccountDetailsPanel" Visible="false">
			<h2>Bank account details (admin only)</h2>
			<p>
				<table cellpadding="2" cellspacing="2">
					<tr>
						<th valign=top width="100"><nobr>Bank name</nobr></th>
						<td valign=top>
							<asp:TextBox Runat="server" ID="BankNameTextBox" Columns="40" MaxLength="100"/>
						</td>
					</tr>
					<tr>
						<th valign=top width="100"><nobr>Bank account name</nobr></th>
						<td valign=top>
							<asp:TextBox Runat="server" ID="BankAccountNameTextBox" Columns="40" MaxLength="100"/>
						</td>
					</tr>					
					<tr>
						<th valign=top width="100"><nobr>Bank account number</nobr></th>
						<td valign=top>
							<asp:TextBox Runat="server" ID="BankAccountNumberTextBox" Columns="30" MaxLength="50"/>
						</td>
					</tr>
					<tr>
						<th valign=top width="100"><nobr>Bank account sort code</nobr></th>
						<td valign=top>
							<asp:TextBox Runat="server" ID="BankAccountSortCodeTextBox" Columns="30" MaxLength="50"/>
						</td>
					</tr>
				</table>
			</p>
		</asp:Panel>
		<h2>Events</h2>
		<p>
			<table cellpadding="2" cellspacing="2">
				<tr>
					<th valign=top width="100">Are you an event promoter?</th>
					<td valign=top>
						<a name="AccountType"></a>
						<p>
							If you are an event organiser or promoter, we can offer extra services 
							such as guestlists. If not, you can still sign up for a promoter 
							account to book banners or run competitions.
						</p>
						
						<p>
							<asp:RadioButton Runat="server" ID="AccountTypeRadioEvents" AutoPostBack="True" 
								GroupName="AccountTypeRadio" OnCheckedChanged="AccountTypeRadioChanged" 
								Text=" I promote events" Checked="True"/>
						</p>
						<p>
							<asp:RadioButton Runat="server" ID="AccountTypeRadioNoEvents" AutoPostBack="True" 
								GroupName="AccountTypeRadio" OnCheckedChanged="AccountTypeRadioChanged" 
								Text=" I do NOT promote events"/>
						</p>
					</td>
				</tr>
				<tr runat="server" id="BrandsTr">
					<th valign=top>Brands / promotions</th>
					<td valign=top>
						<p>
							Please choose the promotions/brands that your organisation owns using 
							the special multi-selector below. If you can't find the 
							promotion/brand you're looking for, click <b>New promotion/brand</b>.
						</p>
						<p>
							<js:MultiSelector runat="server" ID="uiBrandMultiSelector" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetBrands" Width="500px" Height="100px" />
						</p>
						<asp:CustomValidator Runat="server" Display="Dynamic" EnableClientScript="false"
							ErrorMessage="<p>Please select a promotion / brand. If you do NOT promote events, click 'I do NOT promote events'. If you do promote events and your promotion isn't listed in the drop-down, click 'New promotion/brand'.</p>" 
							OnServerValidate="BrandMultiVal"
							ID="Customfieldvalidator3" NAME="Customfieldvalidator3"/>
						<p>
							<small>
								To use this special multi-selector, type the first few letters of the 
								promoter / brand in the bottom box, and a list of results should appear. 
							</small>
						</p>
						<p>
							<small>
								When you've found the right one in the drop-down, select it and click <b>Add</b>. 
								Repeat this until you've added all the promoters / brands that you need.
							</small>
						</p>
						<p>
							<small>
								If you make a mistake or want to remove a brand you've entered, select 
								it and click <b>Remove</b>.
							</small>
						</p>
						<p>
							<input type="button" Value="New promotion/brand" onclick="openPopupFocusSize('/popup/newbrand',500,500);"/>
						</p>
						<script>
							function NewBrand(K,Name)
							{
								<%= uiBrandMultiSelector.ClientID %>Behaviour.addItem(unescape(Name), K);
							}
						</script>
					</td>
				</tr>
			</table>
		</p>
		<h2>Venues</h2>
		<p>
			<table cellpadding="2" cellspacing="2">
				<tr>
					<th valign=top width="100">Do you own or manage a venue?</th>
					<td valign=top>
						<a name="Venues"></a>
						<p>
							If you own or manage a venue, we can give you full control of all your events.
						</p>
						
						<p>
							<asp:RadioButton Runat="server" ID="VenuesRadioYes" AutoPostBack="True" 
								GroupName="VenuesRadio" OnCheckedChanged="VenuesRadioChanged" 
								Text=" I manage a venue"/>
						</p>
						<p>
							<asp:RadioButton Runat="server" ID="VenuesRadioNo" AutoPostBack="True" 
								GroupName="VenuesRadio" OnCheckedChanged="VenuesRadioChanged" 
								Text=" I do NOT manage a venue" Checked="True"/>
						</p>
					</td>
				</tr>
				<tr runat="server" id="VenuesTr" visible="false">
					<th valign=top>Venues</th>
					<td valign=top>
						<p>
							Please choose the venue(s) that you manage or own using 
							the special multi-selector below. If you can't find the 
							promotion/brand you're looking for, click <b>New venue</b>.
						</p>
						<p>
							<js:MultiSelector runat="server" ID="uiVenuesMultiSelector" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetVenues" Width="500px" Height="100px" />
							
						</p>
						<asp:CustomValidator Runat="server" Display="Dynamic" EnableClientScript="false"
							ErrorMessage="<p>Please select a venue. If you do NOT manage a venue, click 'I do NOT manage a venue'. If you do manage a venue, but it's not listed in the drop-down, click 'New venue'.</p>" 
							OnServerValidate="VenuesMultiVal"
							ID="CustomValidator2" NAME="Customfieldvalidator3"/>
						<p>
							<small>
								To use this special multi-selector, type the first few letters of the 
								promoter / brand in the box, and a list of results should appear. 
							</small>
						</p>

						<p>
							<input type="button" Value="New venue" onclick="openPopupFocusSize('/pages/venues/edit',800,600);"/>
						</p>
						<script>
							function NewVenue(K,Name)
							{
								<%= uiVenuesMultiSelector.ClientID %>Behaviour.addItem(unescape(Name), K);
							}
						</script>
					</td>
				</tr>
			</table>
		</p>
		<h2>Your account</h2>
		<p runat="server" id="NoAccessP">
			Several people can have access to this promoter account. However, only 
			the account owner <span runat="server" ID="PrimaryUsrSpan" /> may change 
			who has access.
		</p>
		<p runat="server" id="AccessP">
			<table cellpadding="2" cellspacing="2">
				<tr>
					<th valign=top width="100">People with access</th>
					<td valign=top>
						<a name="PeopleWithAccess"></a>
						<p>
							You can nominate several colleagues to manage your promoter 
							account with you. They will have full access to the account, 
							for example they will be able to place adverts or send e-flyers.
							Make sure they understand there may be a charge for promoter 
							services.
						</p>
						<p>
							Who would you like to have access to your promoter account?
						</p>
						<p runat="server" id="SingleAccountUser">
							<asp:RadioButton Runat="server" ID="AccessJustMeRadio" AutoPostBack="True" GroupName="AccessRadio"
								OnCheckedChanged="AccessRadioChanged" Text=" Just me" Checked="True"/>
						</p>
						<p runat="server" id="MultiAccountUsers">
							<asp:RadioButton Runat="server" ID="AccessMultiRadio" AutoPostBack="True" GroupName="AccessRadio"
								OnCheckedChanged="AccessRadioChanged" Text=" Me and colleagues"/>
						</p>
						<p runat="server" id="NoAccountUsers">
							<asp:RadioButton Runat="server" ID="AccessNoAccountUsersRadio" AutoPostBack="True" GroupName="AccessRadio"
								OnCheckedChanged="AccessRadioChanged" Text=" No account users <small>(admin only option)</small>"/>
						</p>
						<asp:Panel Runat="server" ID="MultiAccess" Visible="False">
							<p>
								Please choose the buddies you want to be able to access 
								your account using the special multi-selector below.
							</p>
							<p>
								<js:MultiSelector runat="server" ID="uiAccessUsersMultiSelector" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetBuddies" Width="500px" Height="100px" />
					
							</p>
							<p>
								Primary user: <asp:DropDownList runat="server" ID="uiPrimaryUserDropDown" ></asp:DropDownList>
							</p>
							<script>
								var list = $get('<%= uiPrimaryUserDropDown.ClientID%>');
								var behaviour = <%= uiAccessUsersMultiSelector.ClientID %>Behaviour;
								behaviour.itemRemoved = function(text, value){
									for(var i=0;i<list.length;i++){
										if (list[i].value == value){
											list.options.remove(i);
											break;
										}
									}
								};
								behaviour.itemAdded = function(text, value){
									var opt = document.createElement("option");
									opt.text = text;
									opt.value = value;
									list.options.add(opt);        
								};
									
							</script>
							<p>
								<small>
									To use this special multi-selector, type the first few letters of the 
									nickname of one of your buddies in the box, and a list of 
									results should appear. 
								</small>
							</p>
					
						</asp:Panel>
					</td>
				</tr>
			</table>
		</p>
	</div>
	<asp:Panel runat="server" ID="TermsPanel">
		<dsi:h1 ID="H11" runat="server">
			Promoter terms and conditions</dsi:h1>
		<div class="ContentBorder">
			<Spotted:Terms runat="server"></Spotted:Terms>
			<p>
				<asp:CheckBox Runat="server" ID="TermsCheckbox" Text="I have read and agree to be bound by the terms for promoters"></asp:CheckBox>
			</p>
			<asp:CustomValidator Runat="server" Display="None" EnableClientScript="False" 
				OnServerValidate="TermsVal" ErrorMessage="<p>You must agree to the terms for promoters</p>" ID="Customvalidator7"/>
		</div>
	</asp:Panel>
			<dsi:h1 ID="H1dsf1" runat="server">
				Finished?</dsi:h1>
	<div class="ContentBorder">
		<p>
			When you've finished entering information about your promoter account, click the button below:
		</p>
		<p>
			<asp:Button Runat="server" OnClick="PanelSignUpFormClick" Text="Save promoter" ID="SavePromoterButton"/>
			<asp:Label Runat="server" ForeColor="#0000ff" ID="PromoterSavedLabel" Visible="False">Details saved</asp:Label>
		</p>
		<asp:ValidationSummary Runat="server" ShowSummary="True" HeaderText="You've made some mistakes" CssClass="ValidationSummaryDiv" Font-Bold="True" DisplayMode="BulletList" ID="Validationsummary2" NAME="Validationsummary1"/>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelBrandVenueError">
	<dsi:h1 runat="server" ID="H12">Brand/venue error</dsi:h1>
	<div class="ContentBorder">
		<p>
			I looks like you selected a party brand or venue that is currently controlled by 
			another promoter account. See below for a list of the brands / venues that 
			are clashing:
		</p>
		<asp:Panel runat="server" ID="BrandErrorPanel">
			<p class="MedLeft">
				<asp:Label runat="server" ID="BrandErrorLabel"></asp:Label>
			</p>
		</asp:Panel>
		
		<asp:Panel runat="server" ID="VenueErrorPanel">
			<p class="MedLeft">
				<asp:Label runat="server" ID="VenueErrorLabel"></asp:Label>
			</p>
		</asp:Panel>
		
		<p>
			These party brands / venues have NOT been added to your account. 
			<b>To fix this situation, please give us a ring on 0207 835 5599.</b>
		</p>
		<p>
			<asp:Button runat="server" OnClick="PanelBrandErrorNext_Click" Text="Click here to continue" />
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelAlreadyPromoter">
	<dsi:h1 runat="server" ID="H12cxvvcx">Add a new promoter account?</dsi:h1>
	<div class="ContentBorder">
		<p>
			You already have access to at least one promoter. You can apply for another promoter account,
			but this is just a warning to remind you not to add a duplicate.
		</p>
		<p>
			You can see which promoter accounts you have access to on the 
			<a href="/pages/promoters">Promoters page</a>.
		</p>
		<p>
			<a href="/pages/promoters/edit/mode-add">Click here to apply for a new promoter account</a>.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelPic">
	<dsi:h1 ID="H15" runat="server">
		Add a picture</dsi:h1>
	<div class="ContentBorder">
		<p>
			If you like, you can upload a logo or picture for your organisation.
		</p>
		<Controls:Pic Runat="server" ID="Pic" OnActionSaved="PicSaved" OnActionNoPic="PicNoPic"/>
	</div>
</asp:Panel>
	
<asp:Panel Runat="server" ID="PanelAddDone">
	<dsi:h1 runat="server" ID="H13">Promoter account added</dsi:h1>
	<div class="ContentBorder">
		<p>
			Thanks for registering as a promoter. We've sent you a private message with firther details.
			You must call our promoter hotline on 0207 835 5599 to enable your account.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelEditDone">
	<dsi:PromoterIntro runat="server" ID="Promoterintro1" Header="Promoter account updated">
		<p>
			Your changes are saved.
		</p>
	</dsi:PromoterIntro>
</asp:Panel>
