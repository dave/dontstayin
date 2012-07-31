<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyPicture.ascx.cs" Inherits="Spotted.Pages.MyPicture" %>

<%@ Register TagPrefix="Controls" TagName="Cropper" Src="/Controls/Cropper.ascx" %>

<dsi:h1 runat="server" ID="H11" NAME="H11">Create your profile picture</dsi:h1>
<asp:Panel Runat="server" ID="PanelNoPhotosMe">
	<div class="ContentBorder">
		<p>
			You don't have any photos on Don't Stay In, so we've taken your picture from Facebook.
			
			Why not go to one of our events and get your photo taken, or <a href="/pages/uploadphotos">upload a photo of your own</a>.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelImages">
	<div class="ContentBorder">
		<p>
			You can create a picture of yourself by cropping a photo with you in it
		</p>
		<p>
			<b>RULES: This photo must include your face. If it doesn't, it will be deleted.</b>
		</p>
	</div>
	<asp:Panel Runat="server" ID="PanelUsrPic">
		<dsi:h1 runat="server">
			Your current picture
		</dsi:h1>
		<div class="ContentBorder">
			<p align="center">
				<a href="" runat="server" id="PicAnchor"><img src="" runat="server" id="PicImg" border="0" class="BorderBlack All" width="100" height="100"></a>
			</p>
			<p align="center">
				<asp:Button Runat="server" onclick="Delete_Click" Text="Delete this picture" ID="Button1"/>&nbsp;
				<asp:Button Runat="server" onclick="ReCrop_Click" Text="Re-crop picture" ID="ReCropButton"/>
			</p>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="PanelChatPic" Visible="true">
		<dsi:h1 ID="H1" runat="server">
			Your current chat picture
		</dsi:h1>
		<div class="ContentBorder">
			<p>
				This picture appears at the top of the live chat box when you post a message.
			</p>
			<p align="center">
				<a href="" runat="server" id="ChatPicAnchor"><img src="" runat="server" id="ChatPicImg" border="0" class="BorderBlack All" width="300" height="100"></a>
			</p>
			<p align="center">
				<asp:Button Runat="server" onclick="ChatReCrop_Click" Text="Re-crop picture" ID="ChatReCropButton"/>
			</p>
		</div>
	</asp:Panel>
	<dsi:h1 runat="server">Choose a photo</dsi:h1>
	<div class="ContentBorder">
		<p>Choose a photo of you below to crop:</P>
		<p runat="server" id="AllPhotosPageP1" align="center">
			<asp:HyperLink runat="server" id="AllPhotosPrevPage1">&lt;- Previous page</asp:HyperLink>&nbsp;|&nbsp;<asp:HyperLink runat="server" id="AllPhotosNextPage1">Next page -&gt;</asp:HyperLink>
		</p>
		<P align="center">
			<asp:DataList ID="ImagesDataList" Runat="server" CellSpacing="10" RepeatColumns="3" RepeatDirection="Horizontal"
				RepeatLayout="Table" Width="100%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" EnableViewState="False">
				<ItemTemplate>
					<a href="<%#ContainerPage.Url.CurrentUrl("type", "pic", "k",((Bobs.Photo)Container.DataItem).K)%>">
						<img src="<%#((Bobs.Photo)Container.DataItem).ThumbPath%>" border="0" class="BorderBlack All" Width="<%#((Bobs.Photo)Container.DataItem).ThumbWidth%>" Height="<%#((Bobs.Photo)Container.DataItem).ThumbHeight%>">
					</a>
				</ItemTemplate>
			</asp:DataList>
		</P>
		<p runat="server" id="AllPhotosPageP2" align="center">
			<asp:HyperLink runat="server" id="AllPhotosPrevPage2">&lt;- Previous page</asp:HyperLink>&nbsp;|&nbsp;<asp:HyperLink runat="server" id="AllPhotosNextPage2">Next page -&gt;</asp:HyperLink>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelCrop">
	<div class="ContentBorder" style="padding:0px;">
		<Controls:Cropper Runat="server" ID="Cropper" ShowTextHelpers="false"/>
		<p align="center">
			<asp:Button Runat="server" onclick="Cancel_Click" Text="Cancel" ID="CancelButton"/>&nbsp;
			<asp:Button Runat="server" onclick="Save_Click" Text="Save this picture" ID="Button3"/>
		</p>
		<p align="center">
			Zoom in or out by dragging the blue square.<br>Drag the image around so your face is in view.
		</p>
	</div>
</asp:Panel>
