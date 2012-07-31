<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindYourPhotoContent.aspx.cs" Inherits="Spotted.Pages.FindYourPhotoContent" %>
<%@ Register TagPrefix="DsiControls" TagName="GalleriesBySpotter" Src="/Controls/GalleriesBySpotter.ascx" %>
<%@ Register TagPrefix="DsiControls" TagName="GalleriesByEvent" Src="/Controls/GalleriesByEvent.ascx" %>
<div id="SpotterCodePanel" runat="server">
	<p style="text-align:center;">
		<center>
			<a runat="server" id="SpotterLink1"><img runat="server" id="SpotterImg" class="BorderBlack All Block" border="0" width="100" height="100" /></a>
		</center>
	</p>
	<p style="text-align:center;">
		You have been spotted by <a runat="server" id="SpotterLink2" style="font-weight:bold;" />
	</p>
	<DsiControls:GalleriesBySpotter runat="server" ID="uiGalleriesBySpotter" />
</div>

<div id="EventSearchPanel" runat="server">
	<DsiControls:GalleriesByEvent runat="server" ID="uiGalleriesByEvent" />
</div>
