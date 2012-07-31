<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PicCropper.ascx.cs" Inherits="Spotted.Controls.PicCropper" %>
<%@ Register TagPrefix="Spotted" TagName="Cropper" Src="Cropper.ascx" %>
<p>
	<input type="file" runat="server" id="InputFile" style="height:20px;" size="30"/><button id="Button1" runat="server" onserverclick="Upload" style="height:20px;">Upload</button>
</p>
<asp:Panel Runat="server" ID="JpgErrorPanel" Visible="False">
	<p style="font-weight:bold;color:#ff0000;">
		<b>Please select a valid file. This upload control only supports .jpg, .gif and .png files.</b>
	</p>
</asp:Panel>
<Spotted:Cropper Runat="server" ID="Cropper" ShowTextHelpers="false"/>
<p>
	<button id="Button2" runat="server" onserverclick="Back_Click" style="height:20px;">&lt;- Back</button>
	<asp:Button Runat="server" OnClick="Save_Click" style="height:20px;" Text="Save -&gt;" ID="SaveButton" />
</p>
