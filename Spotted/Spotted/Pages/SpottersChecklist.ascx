<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpottersChecklist.ascx.cs" Inherits="Spotted.Pages.SpottersChecklist" %>
<%@ Register TagPrefix="Controls" TagName="SpottersChecklist" Src="/Controls/SpottersChecklist.ascx" %>

<dsi:h1 runat="server" ID="H19" NAME="H11">Spotters checklist</dsi:h1>
<div class="ArticleBorder">
	<p>
		We've put together a quick list of the things you need to know to be a 
		spotter. Please take the time to read it in advance of covering an event.
	</p>
	<Controls:SpottersChecklist runat="server"/>
</div>
