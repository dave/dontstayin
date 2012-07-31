<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestEventList.ascx.cs" Inherits="Spotted.Controls.LatestEventList" %>
<%@ Register TagPrefix="Spotted" TagName="EventList" Src="/Controls/EventList.ascx" %>
<a name="Events"></a>
<div class="ClearAfter" runat="server" id="EventsPanel" visible="true">
	<div style="width:309px; padding-left:8px; position:relative; float:right;">
		<dsi:h1 ID="H1" runat="server">
			Past events
		</dsi:h1>
		<div class="ContentBorder">
			<Spotted:EventList runat="server" ID="PastEventList" />
		</div>
	</div>
	<div style="width:309px; padding-right:8px; position:relative; float:left;">
		<dsi:h1 ID="H2" runat="server">
			Next events
		</dsi:h1>
		<div class="ContentBorder">
			<Spotted:EventList runat="server" ID="NextEventList" />
		</div>
	</div>
</div>
