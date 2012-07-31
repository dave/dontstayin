<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Edit.ascx.cs" Inherits="Spotted.Pages.Events.Edit" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<%@ Register TagPrefix="Controls" TagName="MusicTypes" Src="/Controls/MusicTypes.ascx" %>
<%@ Register TagPrefix="Controls" TagName="Pic" Src="/Controls/Pic.ascx" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<%@ Register TagPrefix="dsi" TagName="Picker" Src="/Controls/Picker.ascx" %>

<%@ Register TagPrefix="Banners" TagName="Preview" Src="/Controls/Banners/Preview.ascx" %>
<dsi:h1 runat="server" id="HeaderH1"></dsi:h1>
<asp:Panel Runat="server" ID="PanelSelectionError">
	<div class="ContentBorder">
		<p>
			To add an event, we must have details about the venue it's at. Select a venue below:
		</p>
		<p> 
			<dsi:Picker
				runat="server" 
				id="PanelSelectionErrorPicker"
				OptionBrand="false"
				OptionEvent="false"
				OptionDate="false"
				ValidationType="venue" />
		</p>
		<p>
			<small>Can't find the venue? <a href="/pages/venues/edit" target="_blank">Add a new venue</a>.</small>
		</p>
		<p>
			<asp:Button Runat="server" OnClick="PanelSelectionErrorNext" Text="Next -&gt;" ID="Button1" EnableViewState="False"/>
		</p>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="Dynamic" ControlToValidate="PanelSelectionErrorPicker" 
			ErrorMessage="<p>Please select a venue above.</p>"/>
	</div>
	
	<dsi:h1 runat="server">Add a new event by copying details from another event</dsi:h1>
	<div class="ContentBorder">
		<p>
			If you're about to add an event with similar details to an event that's currently on the 
			site, you can now add it easily without re-typing all the details.
		</p>
		<p>
			<a href="/pages/events/copy">Click here to copy an event</a>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelEditError">
	<div class="ContentBorder">
		<h2>Error</h2>
		<p>
			You can't edit this event.
		</p>
		<p>
			If you need to change the details of this event, please 
			ask an event moderator. You can find a list on the <a href="/pages/contact">contact page</a>.	
		</p>
		<p>
			If you're the promoter of this event, you should be able to edit it. 
			<a href="/pages/promoters">Sign up as a promoter</a>, or call us if you 
			already have a promoter account.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelDate">
	<div class="ContentBorder">
		<h2>Date</h2>
		<p id="PanelDateLine1P" runat="server">
			Please select the date that your event will be on. If it's a multi-day event, choose the first day.
		</p>
		<p id="PanelDateCalendarP" runat="server">
			<dsi:Cal runat="server" id="PanelDateCal" />
		</p>
		<asp:Panel Runat="server" ID="PanelDateFutureConfirmPanel" Visible="False">
			<p style="color:red;">
				You've selected a date in the past. Are you sure you want to add an event that's already happened?
			</p>
			<asp:CheckBox Runat="server" ID="PanelDateFutureConfirmCheckBox" Text="Yes, I really want to add an event that's already happened!"/>
			<p>
				If not, please change the date above.
			</p>
		</asp:Panel>
		<asp:Label ID="PanelDateMessage" runat="server" Visible="false"></asp:Label>
		<asp:Panel Runat="server" ID="PanelDateNonSelectedError" Visible="False">
			<p style="color:red;">
				Please select a date.
			</p>
		</asp:Panel>
		<p runat="server" id="PanelDateNextButtonP">
			<asp:Button ID="Button2" Runat="server" OnClick="PanelDateNext" Text="Next -&gt;" EnableViewState="False"/>
		</p>
		<p runat="server" id="PanelDateSaveButtonP">
			<asp:Button Runat="server" OnClick="PanelDateSave" Text="Save and exit" EnableViewState="False" ID="Button4"/>
		</p>
	</div>
</asp:Panel>


<asp:Panel Runat="server" ID="PanelConflict">
	<div class="ContentBorder">
		<h2>Venue / date conflict</h2>
		<p>
			Your event seems to conflict with another event at this venue on the same day. Please check 
			the clashing event(s) below and if necessary click "&lt;- Back" to change the date.
		</p>
		<p>
			If there is no clash - for example a very large venue or two different events on the same day, 
			tick the box below and click "Next -&gt;".
		</p>
		<p>
			<asp:DataGrid Runat="server" GridLines="None" ID="PanelConflictDataGrid" 
				AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign=Top>
				<Columns>
					<asp:BoundColumn DataField="Name" HeaderText="Name"/>
					<asp:TemplateColumn HeaderText="Venue">
						<ItemTemplate>
							<%#((Bobs.Event)(Container.DataItem)).Venue.Name%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Date">
						<ItemTemplate>
							<%#((Bobs.Event)(Container.DataItem)).DateTime.ToShortDateString()%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<a href="<%#((Bobs.Event)(Container.DataItem)).Url()%>" onclick="openPopupFocus('<%#((Bobs.Event)(Container.DataItem)).Url()%>');return false;">Details</a>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
		
		<p>
			<asp:CheckBox Runat="server" ID="PanelConflictCheckbox" 
				Text="I have checked the events above, and I'm not adding the same event twice."/>
		</p>
		<p>
			<input id="Button3" type="button" Runat="server" onserverclick="PanelConflictBack" Value="&lt;- Back" CausesValidation="False" EnableViewState="False"/>
			<asp:Button ID="Button5" Runat="server" OnClick="PanelConflictNext" Text="Next -&gt;" EnableViewState="False"/>
		</p>
		<asp:CustomValidator ID="CustomValidator1" Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="PanelConflictVal" ErrorMessage="<p>You must click the checkbox above to continue</p>"/>
	</div>
</asp:Panel>


<asp:Panel Runat="server" ID="PanelDetails">
	<div class="ContentBorder">
		<p>
			Complete the details below to add your event:
		</p>
	</div>
	
	
	<div runat="server" id="DateLockedDiv" visible="false">
		<dsi:h1 runat="server">Date</dsi:h1>
		<div class="ContentBorder">
			<p>
				<asp:Label Runat="server" ID="DetailsDateLockedLabel"/><img src="/gfx/icon-lock.png" align="absmiddle" style="margin-right:3px;margin-left:3px;"><asp:Label Runat="server" ID="DetailsDateLockedLabel2"/>
			</p>
		</div>
	</div>
	
		
		
	<div runat="server" id="PanelDetailsVenueDiv">
		<dsi:h1 ID="H1" runat="server">Venue</dsi:h1>
		<div class="ContentBorder">
			<p>
				To change the venue, select a venue below:
			</p>
			<p>
				<dsi:Picker
					runat="server" 
					id="PanelDetailsVenuePicker"
					OptionBrand="false"
					OptionEvent="false"
					OptionDate="false"
					ValidationType="venue" />
			</p>
			<p>
				<small>Can't find the venue? <a href="/pages/venues/edit" target="_blank">Add a new venue</a>.</small>
			</p>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Runat="server" Display="Dynamic" ControlToValidate="PanelDetailsVenuePicker" 
				ErrorMessage="<p>Please select a venue above.</p>"/>
			<p ID="PanelDetailsVenueWarningP" runat="server" visible="false"></p>
		</div>
	</div>
		
		
		
	<div runat="server" id="PanelDetailsVenueLockedDiv">
		<dsi:h1 ID="H2" runat="server">Venue</dsi:h1>
		<div class="ContentBorder">
			<p>
				<asp:Label Runat="server" ID="DetailsVenueLockedLabel"/><img src="/gfx/icon-lock.png" align="absmiddle" style="margin-right:3px;margin-left:3px;"><asp:Label Runat="server" ID="DetailsVenueLockedLabel2"/>
			</p>
		</div>
	</div>
		
		
		
	<dsi:h1 ID="H3" runat="server">Name of the event</dsi:h1>
	<div class="ContentBorder">
		<p>
			<asp:TextBox Runat="server" ID="EventName" Columns="60" MaxLength="200" TabIndex="101"/>
		</p>
		<p>
			<small>Try to keep this below 50 characters. </small>
		</p>
		<p>
			<small>
				There's no need to include the date or venue in the name - the site will add this automatically.
			</small>
		</p>
	</div>
	
	
	<dsi:h1 ID="H4" runat="server">Event start time</dsi:h1>
	<div class="ContentBorder">
		<p>
			<asp:RadioButton Runat="server" ID="StartTimeEvening" GroupName="StartTime" Text="Regular evening event (starts between 6pm and 2am)" Checked="True" TabIndex="102"></asp:RadioButton><br>
			<asp:RadioButton Runat="server" ID="StartTimeMorning" GroupName="StartTime" Text="Morning party (e.g. an after-party - starts between 2am and 10am)" TabIndex="103"></asp:RadioButton><br>
			<asp:RadioButton Runat="server" ID="StartTimeDaytime" GroupName="StartTime" Text="Daytime party (starts between 10am and 6pm)" TabIndex="104"></asp:RadioButton><br>
		</p>
	</div>		
	
	
	<dsi:h1 ID="H5" runat="server">Short details</dsi:h1>
	<div class="ContentBorder">
		<p>
			<asp:TextBox Runat="server" ID="EventShortDetailsHtml" TextMode="MultiLine" Rows="5" Width="594px" TabIndex="105"/>
		</p>
		<p>
			<small>
				A short paragraph about the event - it's displayed in the "Latest" box. No html tags.
			</small>
		</p>
		<p>
			<small>
				The short details is not displayed on the event page. Make sure you include all the info
				from the short details in the long details.
			</small>
		</p>
	</div>

	<dsi:h1 ID="H6" runat="server">Long details</dsi:h1>
	<div class="ContentBorder">
		<p>
			<dsi:Html ID="EventLongDetailsHtml" runat="server" DisableSaveButton="true" TabIndexBase="110" />
		</p>
		<p>
			<small>
				This is the main description, shown on the event page.
			</small>
		</p>
	</div>
	
	<dsi:h1 ID="H7" runat="server">Capacity</dsi:h1>
	<div class="ContentBorder">
		<p>
			<asp:TextBox ID="EventCapacity" Runat="server" Columns="10" TabIndex="130" />
		</p>
		<p>
			<small>If the capacity of the event is different from the venue capacity, you 
			can enter it here. </small>
		</p>
	</div>
		
	<dsi:h1 ID="H8" runat="server">Promoters / party brands</dsi:h1>
	<div class="ContentBorder">
		<p>
			<small>Events on DontStayIn are organised by promoter or 'party-brand'.</small>
		</p>
		
		<p>
			<asp:RadioButton ID="uiNoBrandsRadioButton" runat="server" GroupName="HasBrands" onclick="ClearBrands();" TabIndex="131"/>This event has no promoters / party brands
		</p>
		<p>
			<asp:RadioButton ID="uiHasBrandsRadioButton" runat="server"  GroupName="HasBrands" TabIndex="132"/>This event is associated with the following promoters / party brands:
		</p>
		<p>
			<js:MultiSelector ID="uiBrandsMultiSelector" runat="server" 
				Watermark="Start typing the name of the brand here" 
				WebServiceMethod="GetBrands" WebServiceUrl="/WebServices/AutoComplete.asmx" TextBoxTabIndex="133" />
		</p>
		<p>
			If you can't find the brand click <a href="" onclick="openPopupFocusSize('/popup/newbrand',500,500);return false;" tabindex="134">new party-brand</a>.
		</p>
		<script>
			function EnsureRadioButtonIsChecked(source, clientside_arguments)
			{         
				clientside_arguments.IsValid = $get('<%= uiHasBrandsRadioButton.ClientID %>').checked || $get('<%= uiNoBrandsRadioButton.ClientID %>').checked;
			}

			function ClearBrands(){
				<%= uiBrandsMultiSelector.ClientID %>Behaviour.clear();
			}
			function NewBrand(k, name){
				<%= uiBrandsMultiSelector.ClientID %>Behaviour.addItem(unescape(name), k);
			}
			<%= uiBrandsMultiSelector.ClientID %>Behaviour.itemAdded = function() {
				$get('<%= uiHasBrandsRadioButton.ClientID %>').checked = true;
			};
			<%= uiBrandsMultiSelector.ClientID %>Behaviour.itemRemoved = function() {
				if (<%= uiBrandsMultiSelector.ClientID %>Behaviour.getSelections().get_count() == 0){
					$get('<%= uiNoBrandsRadioButton.ClientID %>').checked = true;
				}
			};
			
		</script>
	</div>
	
	
	<asp:Panel runat="server" ID="SpotterRequestPanel">
		<a name="SpotterRequest" />
		<dsi:h1 ID="H9" runat="server">Spotter request</dsi:h1>
		<div class="ContentBorder">
			<p>
				Can you offer free guestlist to Don't Stay In photographers at this event?
			</p>
			<p>
				<asp:RadioButton runat="server" ID="SpotterRequestYes" GroupName="SpotterRequest" Text="Yes" />
			</p>
			<p>
				<asp:RadioButton runat="server" ID="SpotterRequestNo" GroupName="SpotterRequest" Text="No" />
			</p>
			
			<p>
				Who should the photographer contact to arrange their guestlist?
			</p>
			
			<p>
				<div style="width:100px; float:left;">Name:</div><asp:TextBox runat="server" ID="SpotterRequestName" Columns="30"/>
			</p>
			<p>
				<div style="width:100px; float:left;">Phone number:</div><asp:TextBox runat="server" ID="SpotterRequestNumber" Columns="20"/>
			</p>
			
			<p>
				<small>
					If you're the promoter of this event, you can request that 
					one or more of our photgraphers ('spotters') cover the event.
					Our spotters take photos of the clubbers and give them Don't 
					Stay In cards. This is a great way to build a community around 
					your brand.
				</small>
			</p>
			<p>
				<small>
					You need offer our spotters free entry to the event. We will 
					give them your phone number, and you can arrange it with them.
				</small>
			</p>
			<p>
				<small>
					Remember our spotters range in experience from professional 
					photographers to complete beginners, so we encourage you to ask 
					for samples of their previous work before booking them.
				</small>
			</p>
		</div>
	</asp:Panel>
	
	<dsi:h1 ID="H10" runat="server">Finished?</dsi:h1>
	<div class="ContentBorder">
		<a name="SaveButton"></a>
		<p ID="PanelDetailsSaveP" runat="server">
			<asp:Button ID="Button8" Runat="server" EnableViewState="False" 
				OnClick="PanelDetailsSave" Text="Save and exit" TabIndex="135" />
		</p>
		<p>
			<input ID="Button6" Runat="server" CausesValidation="False" 
				EnableViewState="False" onserverclick="PanelDetailsBack" type="button" 
				Value="&lt;- Back" tabindex="136" />
			<asp:Button ID="Button7" Runat="server" EnableViewState="False" 
				OnClick="PanelDetailsNext" Text="Next -&gt;" TabIndex="137" />
		</p>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Runat="server" 
			ControlToValidate="EventName" Display="Dynamic" 
			ErrorMessage="&lt;p&gt;Please enter a name.&lt;/p&gt;" />
		<asp:RegularExpressionValidator ID="RegularExpressionValidator2" Runat="server" 
			ControlToValidate="EventName" Display="Dynamic" 
			ErrorMessage="&lt;p&gt;Please enter no more than 60 characters for the event name&lt;/p&gt;" 
			ValidationExpression="^(.|\n){0,60}$" />
		<asp:CustomValidator Visible="false" ID="CustomValidator11" runat="server" Display="Dynamic" ControlToValidate="EventName" OnServerValidate="EventNameCapsVal"
			ErrorMessage="<p>You've used lots of capitals in the event name. Please use normal sentance capitalisation. We've made everything lower-case to help you.</p>" />
		<asp:CustomValidator Visible="false" ID="CustomValidator12" runat="server" Display="Dynamic" ControlToValidate="EventName" OnServerValidate="EventNamePunctuationVal"
			ErrorMessage="<p>You've used lots of punctuation in the event name. Please don't decorate your text with punctuation characters.</p>" />
			
		<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Runat="server" 
			ControlToValidate="EventShortDetailsHtml" Display="Dynamic" 
			ErrorMessage="&lt;p&gt;Please enter a short description.&lt;/p&gt;" />
		<asp:RegularExpressionValidator ID="RegularExpressionValidator1" Runat="server" 
			ControlToValidate="EventShortDetailsHtml" Display="Dynamic" 
			ErrorMessage="&lt;p&gt;Please enter no more than 1000 characters for the short details&lt;/p&gt;" 
			ValidationExpression="^(.|\n){0,1000}$" />
		<asp:CustomValidator Visible="false" ID="CustomValidator2" runat="server" Display="Dynamic" ControlToValidate="EventShortDetailsHtml" OnServerValidate="EventShortDetailsCapsVal"
			ErrorMessage="<p>You've used lots of capitals in the short description. Please use normal sentance capitalisation. We've made everything lower-case to help you.</p>" />
		<asp:CustomValidator Visible="false" ID="CustomValidator4" runat="server" Display="Dynamic" ControlToValidate="EventShortDetailsHtml" OnServerValidate="EventShortDetailsPunctuationVal"
			ErrorMessage="<p>You've used lots of punctuation in the short description. Please don't decorate your text with punctuation characters.</p>" />
			
		<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Runat="server" 
			ControlToValidate="EventLongDetailsHtml" Display="Dynamic" 
			ErrorMessage="&lt;p&gt;Please enter a long description.&lt;/p&gt;" />
		<asp:CompareValidator ID="CompareValidator1" Runat="server" 
			ControlToValidate="EventCapacity" Display="Dynamic" 
			ErrorMessage="&lt;p&gt;Please either leave the capacity box blank, or enter a number.&lt;/p&gt;" 
			Operator="GreaterThan" Type="Integer" ValueToCompare="0" />
		
		<asp:CustomValidator ID="BrandCustomValidator" runat="server" Display="Dynamic" ClientValidationFunction="EnsureRadioButtonIsChecked" Text="&lt;p&gt;Please complete the 'Promoters / party brands' section.&lt;/p&gt;"></asp:CustomValidator>
		<asp:CustomValidator ID="SpotterRequestCustomValidator" runat="server" Display="Dynamic" OnServerValidate="SpotterRequestVal" Text="<p>Please choose an option in the 'Spotter request' section. If you choose 'Yes', you must specify a contact name and phone number.</p>"></asp:CustomValidator>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelMusicTypes">
	<div class="ContentBorder">
		<h2>Music at this event</h2>
		<p>
			Tick the music types that will be played at this event. If you know exactly what will 
			be played, select the sub-types - e.g. "US House". If you don't know what will be played, 
			or there will be a range played, tick the main category - e.g. "All House".
		</p>
		<p>
			If you think we should include another music type in this list, please post a message 
			in the <a href="/groups/dontstayin-music-types">music types group</a>.
		</p>
		<p>
			<Controls:MusicTypes Runat="server" ID="MusicTypesUc" />
		</p>
		<p>
			<input id="Button12" type="button" Runat="server" onserverclick="PanelMusicTypesBack" Value="&lt;- Back" CausesValidation="False" EnableViewState="False"/>
			<asp:Button ID="Button13" Runat="server" OnClick="PanelMusicTypesNext" Text="Next -&gt;" EnableViewState="False"/>
		</p>
		<p runat="server" id="PanelMusicTypesSaveP">
			<asp:Button Runat="server" OnClick="PanelMusicTypesSave" Text="Save and exit" EnableViewState="False" ID="Button14"/>
		</p>
		<a name="PanelMusicTypeValError"></a>
		<asp:CustomValidator Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="PanelMusicTypeVal"
			ErrorMessage="<p>Please choose at least one type of music</p>" ID="Customvalidator3"/>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelPic">
	<div class="ContentBorder">
		<asp:Panel Runat="server" ID="PicUploadDefaultPanel">
			<h2>Use a default picture</h2>
			<p>
				If you would like to use one of the pictures below instead 
				of uploading a new one, please click the picture below:
			</p>
			<asp:DataList Runat="server" ID="PicUploadDefaultDataList" RepeatColumns="3" CellPadding="5" OnItemCommand="PicUploadDefaultSelect">
				<ItemTemplate>
					<asp:LinkButton Runat="server" CommandName="Select" CommandArgument='<%#((Bobs.Brand)(Container.DataItem)).K%>' ID="Linkbutton1"><img src="<%#((Bobs.Brand)(Container.DataItem)).PicPath%>" width="100" height="100" border="0" class="BorderBlack All"></asp:LinkButton>
				</ItemTemplate>
			</asp:DataList>
		</asp:Panel>
		<h2>Upload a picture</h2>
		<p>
			To upload a picture for this event, use the controls below:
		</p>
		<asp:Panel Runat="server" ID="PicUploadPanel">
			<Controls:Pic Runat="server" ID="PicUcEvent" OnActionSaved="PanelPicEventSaved" OnActionNoPic="PanelPicEventNoPic"/>
		</asp:Panel>
	</div>
</asp:Panel>


<br /><br /><br /><br /><br /><br /><br /><br />
