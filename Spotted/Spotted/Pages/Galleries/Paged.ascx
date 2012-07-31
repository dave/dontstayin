<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Paged.ascx.cs" Inherits="Spotted.Pages.Galleries.Paged" %>

<asp:Panel Runat="server" ID="MiscInfoPanel">
	<dsi:h1 runat="server" ID="Header">Photos</dsi:h1>
	<div class="ContentBorder">
		<table cellpadding="0" cellspacing="0" border="0" width="100%">
			<tr>
				<td valign="top" style="padding-right:7px;" runat="server" id="PicCell">
					<p><img src="" runat="server" id="GalleryPicImg" class="BorderBlack All" width="100" height="100" /></p>
				</td>
				<td width="100%" valign="top">
					<p runat="server" id="EventLinkP">
						<small>
							Photos from 
							<a href="" runat="server" id="EventLink"/>
							@ <a href="" runat="server" id="EventVenueLink"></a> 
							in <a href="" runat="server" id="EventPlaceLink"></a>, 
							<asp:Label  runat="server" id="EventDate"/>
						</small>
					</p>
					<p runat="server" id="ArticleLinkP">
						<small>
							Photos from 
							<a href="" runat="server" id="ArticleLink"/>
						</small>
					</p>
					
					<p>
						<small>This gallery was added by <a href="" runat="server" id="OwnerLink"></a></small>
					</p>
					
					<p>
						<small>You can also view this gallery in the <a href="" runat="server" id="QuickBrowserLink">quick browser</a></small>
					</p>
					
					<p>
						<a runat="server" id="DiscussionLink"><img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;">Chat about this <asp:Label Runat="server" ID="DiscussionLinkTargetLabel"/><asp:Label Runat="server" ID="DiscussionLinkCommentsLabel"/></a>
					</p>
				</td>
			</tr>
		</table>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="NoPhotosPanel">
	<dsi:h1 runat="server" ID="H11">No photos</dsi:h1>
	<div class="ContentBorder">
		<p>
			There are no live photos in this gallery.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PhotosPanel">
	<a name="Photos"></a>
	<dsi:h1 runat="server">Photos</dsi:h1>
	<div class="ContentBorder" align="center">
		<p align="center" style="font-size:12px;font-weight:bold;padding:5px;">
			<a runat="server" id="LinkBack">Back to ??? page</a>
		</p>
		<p runat="server" id="PhotoPageLinksP" style="horizontal-align:center;"/>
		<asp:DataList runat="server" ID="PhotosDataList" ItemStyle-Width="200px" Width="100%" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" CellPadding="10" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" />
		<p runat="server" id="PhotoPageLinksP1" style="horizontal-align:center;"/>
	</div>
</asp:Panel>
