<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FrontPageCrop.ascx.cs" Inherits="Spotted.Pages.Photos.FrontPageCrop" %>
<%@ Register TagPrefix="Controls" TagName="Cropper" Src="/Controls/Cropper.ascx" %>
<dsi:h1 runat="server">Event / article</dsi:h1>
<div class="ContentBorder">
	<p>
		<asp:Label runat="server" ID="ParentName" />
	</p>
</div>

<dsi:h1 runat="server">Current image</dsi:h1>
<div class="ContentBorder">
	<p>
		<img runat="server" id="CurrentImage" width="600" height="250" />
	</p>
</div>
<dsi:h1 runat="server">Crop new image</dsi:h1>
<div class="ContentBorder">
	
	<p>
		<Controls:Cropper Runat="server" ID="Cropper" ShowTextHelpers="false" ControlHeight="600"/>
	</p>
	
	
	<p>
		<asp:CheckBox runat="server" ID="CheckBox" Text="Photo of the week?" />
	</p>
	<p>
		Added date: <dsi:Cal runat="server" ID="Date"></dsi:Cal> (change this to keep the photo on the front page for longer)
	</p>
	<p>
		Caption: <asp:TextBox runat="server" ID="Caption" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
	</p>
	<p>
		Colour:<br />
		<asp:RadioButton ID="ColourBlackOnWhite" runat="server" GroupName="Colour" Text="Black text on a white background" /><br />
		<asp:RadioButton ID="ColourWhiteOnBlack" runat="server" GroupName="Colour" Text="White text on a black background" />
	</p>
	<p>
		Corner:<br />
		<asp:RadioButton ID="CornerTopLeft" runat="server" GroupName="Corner" Text="Top left" />
		<asp:RadioButton ID="CornerTopRight" runat="server" GroupName="Corner" Text="Top right" /><br />
		<asp:RadioButton ID="CornerBottomLeft" runat="server" GroupName="Corner" Text="Bottom left" />
		<asp:RadioButton ID="CornerBottomRight" runat="server" GroupName="Corner" Text="Bottom right" />
	</p>
	
	<p>
		<asp:Button Runat="server" onclick="Save_Click" Text="Save this picture" ID="Button3"/>
	</p>
</div>
