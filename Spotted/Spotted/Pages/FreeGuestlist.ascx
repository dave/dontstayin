<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FreeGuestlist.ascx.cs" Inherits="Spotted.Pages.FreeGuestlist" %>
<%@ Register TagPrefix="DsiControls" TagName="Picker" Src="/Controls/Picker.ascx" %>
<%@ Register TagPrefix="DsiControls" TagName="NewUserWizardOptions" Src="/Controls/NewUserWizardOptions.ascx" %>

<DsiControls:NewUserWizardOptions runat="server" id="NewUserWizardOptions" />

<dsi:h1 runat="server" id="FindEventsHeader">Free Guestlist</dsi:h1>

<div class="ContentBorder" style="padding-left:0px; padding-right:0px;">

	<p align="center" style="font-weight:bold;margin-right:15px;margin-left:15px;margin-bottom:0px;">
		<img src="/gfx/new-user-freeguestlist.png" width="69" height="43" border="0" />
	</p>
	
	<p class="BigCenter" style="margin-right:15px;margin-left:15px;">
		Exclusive Free Guestlist offers for our members!
	</p>
	
	<p style="margin-right:15px;margin-left:15px;">
		Take photos and hand out Don't Stay In cards at these event for free entry!
		Just call the promoter to organise your exclusive Free Guestlist - you'll 
		find the number on the event page.
	</p>
	<asp:Panel runat="server" ID="SpotterRequestNonSpotter">
		<p style="margin-right:15px;margin-left:15px;">
			<b>You will be refused free entry if you don't have Don't Stay In cards to give out.</b>
			<a href="/pages/spotters">Fill in this form</a> and we'll post you some for free.
		</p>
	</asp:Panel>
	<asp:Panel runat="server" ID="SpotterRequestSpotter">
		<p style="margin-right:15px;margin-left:15px;">
			<b>You will be refused free entry if you don't have Don't Stay In cards to give out.</b>
			It's free to get more - just click the button on the <a href="/pages/spotters">spotters page</a>.
		</p>
	</asp:Panel>
	
	
	
	<p style="margin-right:15px;margin-left:15px;">
		Use the controls below to search for Free Guestlist events, or check out the <a href="/<%= DateTime.Now.Year %>/<%= DateTime.Now.ToString("MMM").ToLower() %>/free">Free Guestlist calendar</a>.
	</p>
	
	<asp:TextBox runat="server" ID="Debug" TextMode="MultiLine" Columns="96" Rows="10" style="display:none;" />
	
	<div class="Pad">
		<DsiControls:Picker runat="server" id="Picker" 
			OptionEvent="false" 
			OptionMusic="false" 
			OptionVenue="false" 
			OptionBrand="false" 
			OptionDate="true"
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
