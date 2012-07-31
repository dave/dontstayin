<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="Spotted.Pages.Venues.Edit" %>
<%@ Register TagPrefix="Controls" TagName="Pic" Src="/Controls/Pic.ascx" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<%@ Register TagPrefix="dsi" TagName="Picker" Src="/Controls/Picker.ascx" %>
<dsi:h1 runat="server" id="HeaderH1"></dsi:h1>
<asp:Panel Runat="server" ID="PanelEditError">
	<div class="ContentBorder">
		<h2>Error</h2>
		<p>
			You can't edit this venue.
		</p>
		<p>
			If you need to change the details of this venue, please 
			ask an event moderator. You can find a list on the <a href="/pages/contact">contact page</a>.
		</p>
		<p>
			If you're the promoter of this venue, you should be able to edit it. 
			<a href="/pages/promoters">Sign up as a promoter</a>, or call us if you 
			already have a promoter account.
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelDetails">
	<div class="ContentBorder">
		<div style="border:solid 1px #ff0000; padding:2px 8px 4px 10px; margin-left:0px; margin-right:5px;">
			<p style="font-size:165%;">
				<b>IMPORTANT</b>
			</p>
			<p>
				This page is only for details of the VENUE. If you're entering details about an 
				EVENT, don't do it here. 
			</p>
			<p>
				VENUE = club, EVENT = club night. Get it?
			</p>
			<p>
				This page is just for the VENUE, so tell us about the building, the sound system, 
				the lights, the surrounding area - that sort of thing. Don't tell us about the 
				night that's going on next weekend - that's for the EVENT page.
			</p>
			<p>
				When you've finished adding the details of the VENUE, you can use the "Add an Event" 
				button to add the EVENT.
			</p>
		</div>
		<div runat="server" id="PanelDetailsPlaceDiv">
			<h2>Town</h2>
			<p>
				<dsi:Picker 
					runat="server"
					id="PanelDetailsPlacePicker" 
					OptionEvent="false"
					OptionDate="false"
					OptionBrand="false"
					OptionVenue="false"
					ValidationType="place" />
			</p>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Runat="server" Display="Dynamic" ControlToValidate="PanelDetailsPlacePicker" ErrorMessage="<p>Please choose a town</p>"/>
			<p>
				<small>
					If your location isn't listed, choose the nearest large town.
				</small>
			</p>
		</div>
		<div runat="server" id="PanelDetailsPlaceLockedDiv">
			<h2>Town</h2>
			<p>
				<asp:Label Runat="server" ID="PanelDetailsPlaceLockedLabel"/><img src="/gfx/icon-lock.png" align="absmiddle" style="margin-right:3px;margin-left:3px;"><small>This is locked - to change it, contact one of our <a href="/pages/contact">event moderators</a>.</small>
			</p>
		</div>
		<div runat="server" id="PanelDetailsPostcodeDiv">
			<h2>Postcode</h2>
			<p><asp:TextBox Runat="server" ID="PanelDetailsPostcodeTextBox"></asp:TextBox></p>
			<asp:CustomValidator ID="CustomValidator2" Runat="server" Display="Dynamic" OnServerValidate="PostcodeVal" EnableClientScript="False" ErrorMessage="<p>Please enter a postcode</p>"/>
			<p>
				<small>
					If this venue is not in the UK, leave this blank.
				</small>
			</p>
		</div>
		<div runat="server" id="PanelDetailsNameDiv">
			<h2>Venue name</h2>
			<p>
				<asp:TextBox Runat="server" ID="PanelDetailsVenueName" Columns="30" MaxLength="50"/>
			</p>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="Dynamic" ControlToValidate="PanelDetailsVenueName" ErrorMessage="<p>Please enter a name</p>"/>
		</div>
		<h2>Events</h2>
		<p>
			How often are events held at this venue?
		</p>
		<p>
			<asp:RadioButton Runat="server" GroupName="RegularEvents" ID="PanelDetailsVenueRegularEventsYes" Text="Once a month or more <small>(e.g. monthly, weekly etc.)</small>"></asp:RadioButton><br>
			<asp:RadioButton Runat="server" GroupName="RegularEvents" ID="PanelDetailsVenueRegularEventsNo" Text="Less than once a month <small>(e.g. yearly, one-off etc.)</small>"></asp:RadioButton>
		</p>
		<asp:CustomValidator ID="CustomValidator1" Runat="server" Display="Dynamic" OnServerValidate="RegularEventsVal" EnableClientScript="False" ErrorMessage="<p>Please specify how often this venue hosts events</p>"/>
		
		<h2>Capacity</h2>
		<p>
			<asp:TextBox Runat="server" ID="PanelDetailsVenueCapacity" Columns="10"/>
		</p>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Runat="server" Display="Dynamic" ControlToValidate="PanelDetailsVenueCapacity" ErrorMessage="<p>Please enter the capacity</p>"/>
		<asp:CompareValidator ID="CompareValidator1" Runat="server" Display="Dynamic" ControlToValidate="PanelDetailsVenueCapacity" ValueToCompare="0" Type="Integer" Operator="GreaterThan" ErrorMessage="<p>Please enter a number for the capacity</p>"/>
		<p>
			<small>
				This is the maximum number of people the venue can hold. If you can't find out, make an estimate.
			</small>
		</p>
		
		<h2>Details</h2>
		<p>
			<dsi:Html runat="server" id="PanelDetailsVenueDetailsHtml" DisableSaveButton="true" />
		</p>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Runat="server" Display="Dynamic" ControlToValidate="PanelDetailsVenueDetailsHtml" ErrorMessage="<p>Please enter the venue details</p>"/>

		<a name="SubmitButton"></a>
		<p>
			<asp:Button ID="Button1" Runat="server" OnClick="PanelDetailsNext" Text="Next -&gt;" EnableViewState="False"/>
		</p>
		

		
		
		
		
		
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelPostcodeCheck">
	<div class="ContentBorder">
		<h2>Similar venue found</h2>
		<p>
			There is a venue with a similar name or postcode in our database. Please check 
			the list below, and make sure you're not adding a duplicate venue. If you want to update 
			the details on one of the venues listed below, please contact one of our 
			<a href="/pages/contact">event moderators</a> with the details and they'll change it 
			for you. Be sure to include a link to the venue you want to change.
		</p>
		<p>
			<asp:DataGrid Runat="server" ID="PanelPostcodeCheckDataGrid" 
				GridLines="None" 
				AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign=Top>
				<Columns>
					<asp:BoundColumn DataField="Name" HeaderText="Name"/>
					<asp:TemplateColumn HeaderText="Place">
						<ItemTemplate>
							<%#((Bobs.Venue)(Container.DataItem)).Place.Name%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<a href="<%#((Bobs.Venue)(Container.DataItem)).Url()%>" onclick="openPopupFocus('<%#((Bobs.Venue)(Container.DataItem)).Url()%>');return false;">Details</a>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
		<p>
			If the venue you are adding is definitely not in the list below, tick the box and click the 
			Next button. If you entered the wrong postcode, click the Back button.
		</p>
		<p>
			<asp:CheckBox Runat="server" ID="PanelPostcodeCheckNewCheckBox" Text="My venue is definitely not on the list above"/>
		</p>
		<p class="BigCenter">
			Please don't add duplicates!
		</p>
		<p>
			<input type="button" Runat="server" onserverclick="PanelPostcodeCheckBack" Value="&lt;- Back" CausesValidation="False" EnableViewState="False" ID="Button2" NAME="Button1"/>
			<asp:Button Runat="server" OnClick="PanelPostcodeCheckNext" Text="Next -&gt;" EnableViewState="False" ID="Button3"/>
		</p>
		<asp:CustomValidator Runat="server" Display="Dynamic" OnServerValidate="PanelPostcodeCheckNewCheckBoxVal" EnableClientScript="False" ErrorMessage="<p>To continue to the new venue page you must tick the 'My venue is definitely not on the list above' box.</p>" ID="Customvalidator3"/>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelPic">
	<div class="ContentBorder">
		<h2>Upload a picture</h2>
		<Controls:Pic Runat="server" ID="PicUc" OnActionSaved="PanelPicSaved" OnActionNoPic="PanelPicNoPic"/>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelSaved">
	<div class="ContentBorder">
		<h2>Venue saved</h2>
		<p>
			This venue will now appear on the site.
		</p>
		<h2>What do you want to do now?</h2>
		<p>
			<a href="" runat="server" id="PanelSavedAddEventLink">Add an event at this venue</a>.
		</p>
		<p>
			Check out the venue page: <a href="" runat="server" id="PanelSavedVenueLink"></a>
		</p>
		<p>
			You can find this venue in <a href="/pages/myvenues">Venues I've added</a>.
		</p>
	</div>
</asp:Panel>
