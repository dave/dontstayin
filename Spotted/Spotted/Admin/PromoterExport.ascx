<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PromoterExport.ascx.cs" Inherits="Spotted.Admin.PromoterExport" %>
<h1>Promoter export...</h1>
<div class="ContentBorder">
	<p>
		First click the button to generate the XML. It's stored on the server until you download it using the link at the bottom. Make sure you right-click > Save as.
	</p>
	<p>
		<asp:Button Runat="server" Text="Create promoter xml" OnClick="Process_Click" ID="Button1"></asp:Button>
	</p>
	<p>
		<a href="/popup/promotersxml">Promoters export</a>
	</p>
</div>
