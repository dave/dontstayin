<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompetitionPhotoIcon.ascx.cs" Inherits="Spotted.Templates.GroupPhotos.CompetitionPhotoIcon" %>
<table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:5px;margin-top:5px;">
	<tr>
		<td align="center">
			<a href="/pages/captioncompetition/k-<%#CurrentPhoto.K%>"><img src="<%#CurrentPhoto.IconPath%>" border="0" class="BorderBlack All" style="margin-bottom:2px;" height="100" width="100"></a>
		</td>
	</tr>
	<tr>
		<td align="center">
			<%#HttpUtility.HtmlEncode(CurrentPhoto.JoinedGroupPhoto.Caption)%>
		</td>
	</tr>
</table>
