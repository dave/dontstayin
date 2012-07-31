<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Photos.ascx.cs" Inherits="Spotted.Pages.Events.Photos" %>
<%@ Register src="../../Controls/PhotoBrowser.ascx" tagname="PhotoBrowser" tagprefix="uc1" %>
<%@ Register src="../../Controls/PhotoControl.ascx" tagname="PhotoControl" tagprefix="uc2" %>
<%@ Register src="../../Controls/LatestChat.ascx" tagname="LatestChat" tagprefix="uc3" %>
<%@ Register src="../../Controls/ThreadControl.ascx" tagname="ThreadControl" tagprefix="uc4" %>

<div runat="server" id="GalleryHasNoPhotos" visible="false">
	<dsi:h1 runat="server">Still processing...</dsi:h1>
	<div class="ContentBorder">
		<p>
			Looks like this gallery is still processing...
		</p>
		<p>
			<a runat="server" id="NoPhotosRetryLink" href="/">Retry this gallery</a>
		</p>
		<p>
			<a runat="server" id="NoPhotosEventLink" href="/">Back to the event page</a>
		</p>
		<p>
			<a runat="server" id="NoPhotosGalleryEditLink" href="/">Edit this gallery</a>
		</p>
	</div>
</div>

<div runat="server" id="GalleryHasPhotos">

	<a name="ContentStart"></a>
	<dsi:h1 runat="server" id="uiTitle">Event photos</dsi:h1>
	<div class="ContentBorder" style="text-align:center">
		<p style="font-size:14px; font-weight:bold; margin-top:12px; margin-bottom:10px; text-align:center;">
			<span ID="uiEventInfoSpan" runat="server"><%# EventInfoHtml %></span>
		</p>
		<p>
			Choose a gallery to view: 
			<asp:DropDownList ID="uiCurrentGallery" runat="server">
			</asp:DropDownList>	
		</p>
	</div>

	<dsi:h1 runat="server">Photo browser</dsi:h1>
	<div class="ContentBorder">
		<uc1:PhotoBrowser ID="uiPhotoBrowser" runat="server" PageSize="<%# Common.Properties.PhotoBrowser.IconsPerPage %>" RowLength="<%# Common.Properties.PhotoBrowser.IconsPerRow %>"/>
		<uc2:PhotoControl ID="uiPhotoControl" runat="server" />
	</div>
	<asp:UpdatePanel runat="server" ID="uiUpdatePanel">
		<ContentTemplate>
			<uc3:LatestChat ID="uiLatestChat" runat="server" ParentObjectType="Photo" Items="200" ShowHolder="true" />
			<uc4:ThreadControl ID="uiThreadControl" runat="server" ParentObjectType="Photo" />
		</ContentTemplate>
	</asp:UpdatePanel>

	<input runat="server" id="uiGalleryK" type="hidden" />
	<input runat="server" id="uiEventK" type="hidden" />
</div>
