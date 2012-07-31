<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin.ascx.cs" Inherits="Spotted.Templates.Photos.Admin" %>
<a name="Photo<%#CurrentPhoto.K%>"></a>
<table id="ucEditGalleryPhotoTableK<%#CurrentPhoto.K.ToString()%>" cellspacing="0" cellpadding="5"><tr><td style="padding:5px; margin:10px;">
<table cellpadding="0" cellspacing="2" width="100%" border="0">
	<tr>
		<td width="100%" align="left" valign=top>
			<img style="cursor:pointer;" src="<%#CurrentPhoto.IconPath%>" class="BorderBlack All" onmouseover="<%#CurrentPhoto.AdminMouseOver%>" onmouseout="htm();" height="100" width="100" onclick="document.getElementById('ucEditGalleryPhotoSelectK<%#CurrentPhoto.K.ToString()%>').click();document.getElementById('ucEditGalleryPhotoTableK<%#CurrentPhoto.K.ToString()%>').style.backgroundColor=document.getElementById('ucEditGalleryPhotoSelectK<%#CurrentPhoto.K.ToString()%>').checked?'#fff3cc':'transparent';">
		</td>
	</tr>
	<!--<tr runat="server" id="DeleteRow">
		<td width="100%" valign=top align="left">
			<asp:LinkButton Runat="server" OnCommand="Command" CommandName="Delete" CommandArgument="<%#CurrentPhoto.K%>" ID="DeleteLinkButton">Delete</asp:LinkButton>
		</td>
	</tr>-->
	<tr runat="server" id="GalleryTitleRow" visible="false">
		<td width="100%" valign=top align="left">
			<b>Gallery icon</b>
		</td>
	</tr>
	<tr runat="server" id="GalleryTitleLinkRow">
		<td width="100%" valign=top align="left">
			<asp:LinkButton Runat="server" OnCommand="Command" CommandName="MakeMain" CommandArgument="<%#CurrentPhoto.K%>" ID="Linkbutton1">Gallery icon</asp:LinkButton>
		</td>
	</tr>
	<tr>
		<td width="100%" valign=top align="left">
			<input type="checkbox" name="ucEditGalleryPhotoSelectK<%#CurrentPhoto.K.ToString()%>" id="ucEditGalleryPhotoSelectK<%#CurrentPhoto.K.ToString()%>" value="1" onclick="document.getElementById('ucEditGalleryPhotoTableK<%#CurrentPhoto.K.ToString()%>').style.backgroundColor=this.checked?'#fff3cc':'transparent';"><label for="ucEditGalleryPhotoSelectK<%#CurrentPhoto.K.ToString()%>">Select</label>
		</td>
	</tr>
</table>
</td></tr></table>
