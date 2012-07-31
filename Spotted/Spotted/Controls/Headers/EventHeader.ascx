<%@ Import Namespace="System.Diagnostics"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventHeader.ascx.cs" Inherits="Spotted.Controls.Headers.EventHeader" %>

<dsi:h1 runat="Server"><asp:Label runat="server" id="EventName"/></dsi:h1>
<div class="ContentBorder">
	<table cellpadding="0" cellspacing="0" border="0" width="100%">
		<tr>
			<td valign="top" style="padding-right:7px;" runat="server" id="EventPicCell">
				<p>
					<img src="" runat="server" id="EventPicImg" class="BorderBlack All" width="100" height="100"  />
					<a href="" runat="server" id="EventPicAnchor" class="NoStyle" />
				</p>
			</td>
			<td width="100%" valign="top">
				<p>
					<small>
						<asp:Label  runat="server" id="EventDate"/>
						<asp:Label  runat="server" id="EventStartTime"/>
						@ <a href="" runat="server" id="EventVenueLink"></a> 
						in <a href="" runat="server" id="EventPlaceLink"></a>
					</small>
				</p>
				<p runat="server" id="EventSelectedPanelAddedByP">
					<small>This event was added by <a href="" runat="server" id="EventSelectedPanelAddedByLink"></a></small>
				</p>
				
				<p>
					<span Runat="server" ID="MapSpan">
						<a href="" runat="server" id="MapLink" onclick="mapOpen('');return false;"><img src="/gfx/icon-map.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">Google map of the venue</a> and <a href="/" runat="server" id="DirectionsLink" onclick="alert('To show directions, you first need to enter your home postcode on the \'My details\' page.');return false;">directions</a>
						<script>
							function mapOpen(url)
							{
								var popUp = window.open(url, popUp, 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=890,height=600');
								popUp.focus();
							}
							function overrideMapOpen(url)
							{
								var popUp = window.open(url, popUp, 'toolbar=1,scrollbars=1,location=0,statusbar=1,menubar=1,resizable=1,width=600,height=500');
								popUp.focus();
							}
						</script>
					</span>
				</p>
				<p>
					<span runat="server" id="uiHotelLinkSpan"><asp:HyperLink id="uiHotelLink" runat="server" Target="_blank"><img src="/gfx/icon-hotel.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;" ><asp:Label ID="uiHotelLinkText" runat="server"></asp:Label></asp:HyperLink></span>
				</p>
				<p>
					<a runat="server" id="DiscussionLink"><img src="/gfx/icon-discuss.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">Chat about this event<asp:Label Runat="server" ID="DiscussionLinkCommentsLabel"/></a>
				</p>
				<asp:Panel Runat="server" ID="ReviewsPanel">
					<p>
						<a href="/" runat="server" id="AddReviewLink"><img src="/gfx/icon-chatter.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">Add / edit your review</a>
					</p>
				</asp:Panel>
				<p>
					<dsi:DbButton 
						runat="server"
						ID="CommentAlertButton"	
						ImageFileNameTrue="/gfx/icon-eye-up.png"
						ImageFileNameFalse="/gfx/icon-eye-dn.png"
						AltTrue=""
						AltFalse=""
						TextTrue="Ignore new topics about this event"
						TextFalse="Watch all new topics about this event"
						CssClass=""
						CssStyle="cursor:pointer;margin-right:3px;"
						Align="absmiddle"
						ImageWidth="26"
						ImageHeight="21"
						FunctionName="CommentAlert"
						DbButtonId="CommentAlertButton"
						OnReturn=""
						ConfirmStringTrue=""
						ConfirmStringFalse="" />
				</p>
				<asp:Panel Runat="server" ID="BrandsPanel">
					<p style="font-size:14px;font-weight:bold;" runat="server" id="BrandsHolder">
						<img src="/gfx/icon-group.png" border="0" align="absmiddle" style="margin-right:3px;" width="26" height="21"><asp:PlaceHolder Runat="server" ID="EventSelectedPanelBrandsPlaceHolder"></asp:PlaceHolder>
					</p>
				</asp:Panel>
				<asp:PlaceHolder runat="server" ID="Content"/>
			</td>
		</tr>
	</table>
</div>
<asp:PlaceHolder runat="server" id="ControlEnd" />
