<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindEvents.ascx.cs" Inherits="Spotted.Pages.FindEvents" %>
<%@ Register TagPrefix="DsiControls" TagName="Picker" Src="/Controls/Picker.ascx" %>
<%@ Register TagPrefix="DsiControls" TagName="NewUserWizardOptions" Src="/Controls/NewUserWizardOptions.ascx" %>

<DsiControls:NewUserWizardOptions runat="server" id="NewUserWizardOptions"></DsiControls:NewUserWizardOptions>

<dsi:h1 runat="server" id="FindEventsHeader">Find events</dsi:h1>
<dsi:h1 runat="server" ID="CalendarTabHolder" ClassAttribute="TabHolder">
	<a href="/pages/findevents/mode-calendar" class="TabbedHeading Selected" runat="server" id="EventFinderTab">Event finder</a>
	<a href="/pages/mycalendar" class="TabbedHeading" runat="server" id="MyCalendarTab" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Suggested events</a>
	<a href="/pages/mycalendar/type-buddy" class="TabbedHeading" runat="server" id="BuddyCalendarTab" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Where my buddies are going</a>
</dsi:h1>

<div class="ContentBorder" style="padding-left:0px; padding-right:0px;">
	
	<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;margin-bottom:0px;">
		<img src="/gfx/icon-calendar.png" border="0" runat="server" width="26" height="21" id="TopIcon">
	</p>
	<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
		This is the event finder. Use the controls below to search for an event.
	</p>
	
	<asp:TextBox runat="server" ID="Debug" TextMode="MultiLine" Columns="96" Rows="10" style="display:none;" />
	
	<div class="Pad">
		<DsiControls:Picker runat="server" id="Picker" 
			OptionEvent="false" 
			OptionMusic="true" 
			OptionDateDay="true" 
			OptionDateDayIncrement="7" />
	</div>

	<div runat="server" id="CalendarHolderOuter" style="display:none;">
		<div class="ClearAfter" style="position:relative;">
			<div style="padding:5px; text-align:left; width:180px; float:left;">
				<a href="/" runat="server" id="BackLink"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">Older events</a>
			</div>
			<div runat="server" id="MonthLabel" style="padding-top:5px; text-align:center; position:absolute; top:0px; left:180px; width:240px; font-weight:bold;" />
			<div runat="server" id="LoadingLabel" style="padding-top:5px; text-align:center; position:absolute; top:0px; left:180px; width:240px; font-weight:bold; display:none;">Loading...</div>
			<div style="padding:5px; text-align:right; width:180px; float:right;">
				<a href="/" runat="server" id="ForwardLink">Newer events<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></a>
			</div>
		</div>
		<div style="position:relative;">
			<div runat="server" id="CalendarLoadingOverlay" style="display:none; position:absolute; top:0px; left:0px; width:100%; padding-top:5px; height:100px; text-align:center; background-image:url(/gfx/transparent-white-75.png); background-repeat:repeat; font-weight:bold;" />
			<div runat="server" id="CalendarHolder" />
		</div>
	</div>
	
</div>
