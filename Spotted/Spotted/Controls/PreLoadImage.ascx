<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreLoadImage.ascx.cs" Inherits="Spotted.Controls.PreLoadImage" %>

<img id="<%= this.ClientID %>UiImage" style="display:none" height="1" width="1" />
<script>
function PreLoadImage<%= this.ClientID %>(imageSrc)
{
	document.getElementById('<%= this.ClientID %>UiImage').src = imageSrc;
}
</script>

<dsi:InlineScript runat="server">
	<script>
		PreLoadImage<%= this.ClientID %>('<%= ImageSrc %>');
	</script>
</dsi:InlineScript>
