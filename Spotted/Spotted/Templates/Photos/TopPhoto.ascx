<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopPhoto.ascx.cs" Inherits="Spotted.Templates.Photos.TopPhoto" %>
<asp:Panel runat="server" ID="NormalPanel">
	<table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px;margin-top:5px;">
		<tr>
			<td align="center">
				<a href="<%#CurrentPhoto.Url()%>" class="NoStyle"><img src="<%#CurrentPhoto.IconPath%>" border="0" class="BorderBlack All" style="margin-bottom:2px;" height="100" width="100"></a>
			</td>
		</tr>
		<tr>
			<td align="center">
				<%#CurrentPhoto.PhotoOfWeekCaption%>
			</td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel runat="server" ID="SonyPanel">
	<div style="width:<%#Bobs.Vars.IE?"116":"114"%>px; height:<%#Bobs.Vars.IE?"116":"114"%>px;" class="BorderBlack All">
		<a href="<%#CurrentPhoto.Url()%>" class="NoStyle"><img src="<%#CurrentPhoto.IconPath%>" border="0" class="BorderBlack All" style="margin:4px;" height="100" width="100"></a>
	</div>
	<img src="/gfx/sony-shot-with-a-phone-4.gif" width="136" height="48" style="position:relative; left:-9px; top:-<%#Bobs.Vars.IE?"28":"37"%>px; margin-bottom:-30px;" />
</asp:Panel>
