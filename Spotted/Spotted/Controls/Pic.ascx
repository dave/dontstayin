<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pic.ascx.cs" Inherits="Spotted.Controls.Pic" %>

<%@ Register TagPrefix="Spotted" TagName="PicCropper" Src="/Controls/PicCropper.ascx" %>
<asp:Panel Runat="server" ID="NoPicPanel">
	<p>
		<b>No picture loaded.</b>
		<ul>
			<li>Click "Add Picture -&gt;" to upload a picture. </li>
			<li>Click "No Picture" to save with no picture.</li>
		</ul>
	</p>
	<p>
		<asp:Button ID="Button1" Runat="server" OnClick="NoImageClick" CausesValidation="False" Text="No Picture"/>&nbsp;
		<asp:Button ID="Button2" Runat="server" OnClick="AddImageClick" CausesValidation="False" Text="Add Picture -&gt;"/>
	</p>
</asp:Panel>
<asp:Panel Runat="server" ID="PicPanel">
	<p>
		<b>Current picture.</b>
		<ul>
			<li>Click "Upload/crop" to upload a new picture, or to re-crop the current one.</li>
			<li>Click "Delete" to delete this picture. </li>
			<li>Click "Save -&gt;" to save this picture.</li>
		</ul>
	</p>
	<p>
		<img src="" runat="server" id="PicImg" width="100" height="100" style="margin-left:25px;" class="BorderBlack All"/>
	</p>
	<p>
		<asp:Button ID="Button3" Runat="server" OnClick="AddImageClick" CausesValidation="False" Text="Upload / crop"/>&nbsp;
		<asp:Button ID="Button4" Runat="server" OnClick="DeleteImageClick" CausesValidation="False" Text="Delete"/>&nbsp;
		<asp:Button ID="Button5" Runat="server" OnClick="SaveImageClick" CausesValidation="False" Text="Save -&gt;"/>
	</p>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelCropper">
	<p>
		<b>Picture upload/editor.</b> 
		<ul>
			<li>Click "Browse" to select a picture from your computer, then click "Upload" to load the selected picture.</li>
			<li>The zoom slider will allow you to zoom in or out, and you can drag the image around to get it in the right place.</li>
			<li>Finally click "Save -&gt;" to save your picture.</li>
			<li>Click "&lt;- Back" to cancel your changes.</li>
		</ul>
	</p>
	<P>
		<Spotted:PicCropper id="PicCropper" runat="server" OnBackClick="CancelCropperClick" OnSaved="OkClick"/>
	</P>
</asp:Panel>
