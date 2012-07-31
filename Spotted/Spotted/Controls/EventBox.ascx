<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventBox.ascx.cs" Inherits="Spotted.Controls.EventBox" EnableViewState="false" %>
<%@ Register TagPrefix="Control" TagName="MusicDropDown" Src="/Controls/MusicDropDown.ascx" %>
<h1 class="TabHolder">
	<a href="" class="TabbedHeading Selected" runat="server" causesvalidation="false" id="FutureEventsTab">Future events</a>
	<a href="" class="TabbedHeading" runat="server" causesvalidation="false" id="PastEventsTab">Past events</a>
	<a href="" class="TabbedHeading" runat="server" causesvalidation="false" id="TicketsTab">Tickets</a>
</h1>
<div class="ContentBorder">
	<input type="hidden" runat="server" id="InitEnableEffects" />
	<input type="hidden" runat="server" id="InitClientID" />
	<input type="hidden" runat="server" id="InitFirstPage" />
	<div class="ClearAfter">
		<div runat="server" id="TitleHolder" class="p" style="float:left; padding-top:3px;">
			This event box shows the most popular events on DontStayIn.
		</div>
		<div runat="server" id="MusicDropDownHolder" class="p" style="float:right; vertical-align:top;">
			<img src="/gfx/icon-music.png" border="0" width="26" height="21" align="top" style="padding-right:8px;"><Control:MusicDropDown runat="server" id="MusicDropDownControl" />
		</div>
	</div>
	<div class="p ClearAfter EventBoxIconsAndNavigationHolder">
		<div class="EventBoxIconsHolderClip"><div runat="server" id="EventIconsHolder" class="EventBoxIconsHolderOuter" /></div>
		<div runat="server" id="EventIconsNavigationBackHolder" class="Rollover EventBoxIconsNavigationHolder Back"><img src="/gfx/icon-back-12.png" align="top" width="12" height="21" /></div>
		<div runat="server" id="EventIconsNavigationForwardHolder" class="Rollover EventBoxIconsNavigationHolder Forward"><img src="/gfx/icon-forward-12.png" align="top" width="12" height="21" /></div>
	</div>
	<div runat="server" id="EventInfoHolderOuter" class="All EventBoxInfoHolderOuter ClearAfter" style="margin-bottom:7px;" />
	<div runat="server" id="BottomNavigationTitle" class="p" visible="false">
		Find more events in these countries:
	</div>
	<div runat="server" id="BottomNavigationHolder" class="ClearAfter" visible="false">
		<div class="EventBoxCountryHolder Rollover">
			<a href="/uk"><img src="/gfx/flags1/tn_gb.gif" width="30" height="18" align="top" border="0" style="margin-bottom:3px;" class="BorderBlack All"><br /> 
			UK</a>
		</div>
		
		<div class="EventBoxCountryHolder Rollover">
			<a href="/spain"><img src="/gfx/flags1/tn_es.gif" width="30" height="18" align="top" border="0" style="margin-bottom:3px;" class="BorderBlack All"><br /> 
			Spain</a> 
		</div>
		
		<div class="EventBoxCountryHolder Rollover">
			<a href="/usa"><img src="/gfx/flags1/tn_us.gif" width="30" height="18" align="top" border="0" style="margin-bottom:3px;" class="BorderBlack All"><br /> 
			USA</a> 
		</div>
		
		<div class="EventBoxCountryHolder Rollover">
			<a href="/ireland"><img src="/gfx/flags1/tn_ie.gif" width="30" height="18" align="top" border="0" style="margin-bottom:3px;" class="BorderBlack All"><br /> 
			Ireland</a> 
		</div>
		
		<div class="EventBoxCountryHolder Rollover">
			<a href="/australia"><img src="/gfx/flags1/tn_au.gif" width="30" height="18" align="top" border="0" style="margin-bottom:3px;" class="BorderBlack All"><br /> 
			Australia</a> 
		</div>
		
		<div class="EventBoxCountryHolder Rollover">
			<a href="/france"><img src="/gfx/flags1/tn_fr.gif" width="30" height="18" align="top" border="0" style="margin-bottom:3px;" class="BorderBlack All"><br /> 
			France</a> 
		</div>
		
		<div class="EventBoxCountryHolder Rollover">
			<a href="/uk"><img src="/gfx/flags1/tn_gb.gif" width="30" height="18" align="top" border="0" style="margin-bottom:3px;" class="BorderBlack All"><br /> 
			UK</a>
		</div>
		
		<div class="EventBoxCountryHolder Rollover">
			<a href="/spain"><img src="/gfx/flags1/tn_es.gif" width="30" height="18" align="top" border="0" style="margin-bottom:3px;" class="BorderBlack All"><br /> 
			Spain</a> 
		</div>
		
		<div class="EventBoxCountryHolder Rollover">
			<a href="/usa"><img src="/gfx/flags1/tn_us.gif" width="30" height="18" align="top" border="0" style="margin-bottom:3px;" class="BorderBlack All"><br /> 
			USA</a> 
		</div>
		
		<div class="EventBoxCountryHolder Rollover">
			<a href="/ireland"><img src="/gfx/flags1/tn_ie.gif" width="30" height="18" align="top" border="0" style="margin-bottom:3px;" class="BorderBlack All"><br /> 
			Ireland</a> 
		</div>
		
	</div>
</div>
<div id="ErrorBox">

</div>
