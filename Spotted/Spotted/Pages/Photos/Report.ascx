<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Report.ascx.cs" Inherits="Spotted.Pages.Photos.Report" %>

<asp:Panel Runat="server" ID="ReportPanel">
	<dsi:h1 runat="server" ID="H11">Report this photo to a moderator</dsi:h1>
	<div class="ContentBorder">
		<p>
			<a href="" runat="server" id="PhotoAnchor"><img src="" runat="server" id="PhotoImg" class="BorderBlack All" border="0"></a>
		</p>
		<p>
			DontStayIn treats abuse very seriously. If you believe this photo breaks any of the rules below, please 
			complete this form. Our photo moderators will be contacted and will investigate. Please DO NOT use this
			form unless the photo breaks one of the rules below. There is no need to report several photos in the 
			same gallery - our moderators will investigate the whole gallery.
		</p>
		<h2>Rules for photos</h2>
		<p>
			Photos should not:
		</p>
		<ul>
			<li>be abusive, vulgar, hateful or harassing.</li>
			<li>be obscene, profane or sexually explicit.</li>
			<li>be threatening, invasive of a person's privacy, or otherwise violate any law.</li>
			<li>have a non-dsi logo on, or be copyrighted photos already used elsewhere.</li>
			<li>depict illegal or semi-legal drugs (e.g. poppers, mushrooms or gas balloons) or drug abuse in any form.</li>
		</ul>
		<p>
			Please explain why the photo breaks the rules above:
		</p>
		<p>
			<asp:TextBox Runat="server" TextMode="MultiLine" Rows="5" Columns="80" ID="ReportMessageTextBox" EnableViewState="False"/>
		</p>
		<a name="DoneAnchor"></a>
		<p>
			<asp:Button Runat="server" onclick="Report_Click" Text="Report this photo" CausesValidation="False" ID="Button1" EnableViewState="False"/>
		</p>
		
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="DonePanel" Visible="false">
	<dsi:h1 runat="server" ID="H12">Report this photo to a moderator</dsi:h1>
	<div class="ContentBorder">
		<p Runat="server" ID="ReportP"/>
	</div>
</asp:Panel>
