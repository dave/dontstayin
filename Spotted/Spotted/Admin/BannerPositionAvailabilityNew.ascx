<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerPositionAvailabilityNew.ascx.cs" Inherits="Spotted.Admin.BannerPositionAvailabilityNew" %>
<%@ Register TagPrefix="Controls" TagName="Av" Src="/Controls/Admin/BannerAvailabilityNew.ascx" %>
<div class="ContentBorder">
	<p><small>Notes: These numbers are approximate; they do not take into account the individual targetting of banners. However, when a particular position is over 100% full you know this is a Bad Thing. Also for a while after exceeding capacity, the server will still be trying to catch up from the previous underserving, and this extra demand is not factored into the figures below.</small></p>
	<h2>
		Hotbox
	</h2>
	<p>
		<Controls:Av Runat="Server" Position="HotBox" ID="Slots0"/>
	</p>
	<h2>
		Leaderboard
	</h2>
	<p>
		<Controls:Av Runat="Server" Position="Leaderboard" ID="Slots1"/>
	</p>
	<h2>
		Skyscraper
	</h2>
	<p>
		<Controls:Av Runat="Server" Position="Skyscraper" ID="Slots4"/>
	</p>
	<h2>
		Photo banner
	</h2>
	<p>
		<Controls:Av Runat="Server" Position="PhotoBanner" ID="Slots3"/>
	</p>
	<h2>
		Email banner
	</h2>
	<p>
		<Controls:Av Runat="Server" Position="EmailBanner" ID="Slots2"/>
	</p>
</div>

