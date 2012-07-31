<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Copy.ascx.cs" Inherits="Spotted.Pages.Events.Copy" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo"  %>
<%@ Register TagPrefix="Controls" TagName="BannerPreview" Src="/Controls/Banners/Preview.ascx" %>
<%@ Register TagPrefix="dsi" TagName="Picker" Src="/Controls/Picker.ascx" %>

<asp:Panel Runat="server" ID="PanelStart">
	<dsi:h1 runat="server" ID="H11">Add a new event by copying details from another event</dsi:h1>
	<div class="ContentBorder">
		<p>
			If you're about to add an event with similar details to an event that's currently on the 
			site, you can add it easily without re-typing all the details. If you have to change some of the
			details, you can edit the new event.
		</p>
		<h2>
			Select event to copy from
		</h2>
		<p>
			First select the event to copy the details from:
		</p>
		<p> 
			<dsi:Picker runat="server" id="uiEventPicker" />
		</p>
		<h2>
			Select date for new event
		</h2>
		<p>
			<dsi:Cal runat="server" id="NewDateCalendar" />
		</p>
			
		<p>
			<asp:Button Runat="server" onclick="PanelStartAddButtonClick" Text="Add new event" ID="Button1"/>
		</p>
		
		<asp:CustomValidator ID="CustomValidator1" Runat="server" EnableClientScript="False" OnServerValidate="SameDateVal"
			Display="Dynamic" ErrorMessage="<p>Oops - you've selected the same date as the original event. Please choose another date.</p>"/>
		<asp:CustomValidator Runat="server" EnableClientScript="False" OnServerValidate="SelectDateVal"
			Display="Dynamic" ErrorMessage="<p>Please select a date for the new event.</p>" ID="Customvalidator2"/>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" ControlToValidate="uiEventPicker"
			Display="Dynamic" ErrorMessage="<p>Please select an event to copy the details from.</p>"/>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelConflict">
	<dsi:h1 runat="Server">Warning: venue / date conflict</dsi:h1>
	<div class="ContentBorder">
		<p>
			Your event seems to conflict with another event at this venue on the same day. Please check 
			the clashing event(s) below and if necessary click "&lt;- Back" to change the date, or the 
			event you're copying from.
		</p>
		<p>
			If there is no clash - for example a very large venue or two different events on the same day, 
			tick the box below and click "Add new event".
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
			<input type="button" Runat="server" onserverclick="PanelConflictBack" Value="&lt;- Back" CausesValidation="False" EnableViewState="False" ID="Button2" NAME="Button2"/>
			<asp:Button Runat="server" OnClick="PanelConflictNext" Text="Add new event" EnableViewState="False" ID="Button3"/>
		</p>
		<asp:CustomValidator Runat="server" Display="Dynamic" 
			EnableClientScript="False" OnServerValidate="PanelConflictVal" 
			ErrorMessage="<p>You must click the checkbox above to continue</p>" ID="Customvalidator3"/>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelReview">
	<dsi:h1 runat="Server" ID="H12">Make changes?</dsi:h1>
	<div class="ContentBorder">
		<p>
			Please double check the details below, you can edit them by clicking the link below.
		</p>
		<p>
			<table cellpadding="5" cellspacing="0">
				<tr>
					<th valign="top"><p>Venue</p></th>
					<td valign="top">
						<p><asp:Label Runat="server" ID="ReviewVenue"/></p>
					</td>
				</tr>
				<tr>
					<th valign="top"><p>Date</p></th>
					<td valign="top">
						<p><asp:Label Runat="server" ID="ReviewDate"/></p>
					</td>
				</tr>
				<tr>
					<th valign="top"><p>Name</p></th>
					<td valign="top">
						<p><asp:Label Runat="server" ID="ReviewName"/></p>
					</td>
				</tr>
				<tr>
					<th valign="top"><p>Start time</p></th>
					<td valign="top">
						<p><asp:Label Runat="server" ID="ReviewStartTime"/></p>
					</td>
				</tr>
				<tr>
					<th valign="top"><p>Short details</p></th>
					<td valign="top">
						<asp:Label Runat="server" ID="ReviewShortDetailsHtml"/>
					</td>
				</tr>
				<tr>
					<th valign="top"><p>Long details</p></th>
					<td valign="top">
						<asp:Label Runat="server" ID="ReviewLongDetailsHtml"/>
					</td>
				</tr>
				<tr>
					<th valign="top"><p>Capacity</p></th>
					<td valign="top">
						<p><asp:Label Runat="server" ID="ReviewCapacity"/></p>
					</td>
				</tr>
				<tr runat="server" id="ReviewBrandTr">
					<th valign="top"><p>Promoters / brands</p></th>
					<td valign="top">
						<p><asp:Label Runat="server" ID="ReviewBrand"/></p>
					</td>
				</tr>
				<tr>
					<th valign="top"><p>Music</p></th>
					<td valign="top">
						<p><asp:Label Runat="server" ID="ReviewMusicTypes"/></p>
					</td>
				</tr>
				<tr>
					<th valign="top"><p>Picture</p></th>
					<td valign="top" runat="server" id="ReviewNoPicTd">
						<p>No picture uploaded</p>
					</td>
					<td valign="top" runat="server" id="ReviewPicTd">
						<p><img runat="server" id="ReviewPicImg"/></p>
					</td>
				</tr>
			</table>
		</p>
		<p align="center" style="font-size:small;font-weight:bold;">
			<a href="" runat="server" id="ReviewEditAnchor">Edit these details</a> | <asp:LinkButton ID="LinkButton1" Runat="server" onclick="ReviewNextClick">No changes needed</asp:LinkButton>
		</p>
	</div>
</asp:Panel>


<asp:Panel Runat="server" ID="PanelSaved">
	<dsi:h1 runat="Server" ID="H14">Event saved</dsi:h1>
	<div class="ContentBorder">
		<p>
			This event will now appear on the site.
		</p>
	</div>

	<Controls:BannerPreview Runat="server" ID="ucBannerPreview"/>

	<dsi:h1 runat="server" ID="H13" NAME="H11">What do you want to do now?</dsi:h1>
	<div class="ContentBorder">
		<p>
			Check out the event page: <a href="" runat="server" id="PanelSavedEventLink"></a>
		</p>
		<p>
			Sign up to <a href="" runat="server" id="PanelSavedSignUpLink">take photos at this event</a>?
		</p>
		<p>
			You can see this event in <a href="/pages/myevents">Events I've added</a>.
		</p>
		<p>
			<asp:LinkButton ID="LinkButton2" Runat="server" OnClick="PanelSavedAnotherClick">Add another event by copying a current event</asp:LinkButton>
		</p>
	</div>
</asp:Panel>
