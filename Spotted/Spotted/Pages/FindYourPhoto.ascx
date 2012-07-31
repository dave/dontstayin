<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindYourPhoto.ascx.cs" Inherits="Spotted.Pages.FindYourPhoto" %>
<%@ Register TagPrefix="DsiControls" TagName="Picker" Src="/Controls/Picker.ascx" %>
<%@ Register TagPrefix="DsiControls" TagName="GalleriesBySpotter" Src="/Controls/GalleriesBySpotter.ascx" %>
<%@ Register TagPrefix="DsiControls" TagName="GalleriesByEvent" Src="/Controls/GalleriesByEvent.ascx" %>
<%@ Register TagPrefix="DsiControls" TagName="OptionsList" Src="/Controls/OptionsList.ascx" %>
<%@ Register TagPrefix="DsiControls" TagName="NewUserWizardOptions" Src="/Controls/NewUserWizardOptions.ascx" %>

<DsiControls:NewUserWizardOptions runat="server"></DsiControls:NewUserWizardOptions>

<dsi:h1 runat="server">Find your photo</dsi:h1>
<div class="ContentBorder">
	
	<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;margin-bottom:0px;">
		<img src="/gfx/icon-calendar.png" border="0" runat="server" width="26" height="21" id="TopIcon">
	</p>
	<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
		You can find your photos by using one of the following options:
	</p>
	
	<DsiControls:Picker 
		runat="server" 
		ID="Picker" 
		OptionSpotter="true"
	/>
	
	<div style="position:relative;" runat="server" id="ResultOuter">
		<div runat="server" id="LoadingOverlay" style="display:none; position:absolute; top:0px; left:0px; width:100%; padding-top:5px; height:100px; text-align:center; background-image:url(/gfx/transparent-white-75.png); background-repeat:repeat; font-weight:bold; text-align:center; padding:10px;">Loading...</div>
		<div runat="server" id="Result" />
	</div>

	
</div>
