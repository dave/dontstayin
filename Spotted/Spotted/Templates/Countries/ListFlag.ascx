<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListFlag.ascx.cs" Inherits="Spotted.Templates.Countries.ListFlag" %>
<table cellspacing="5" cellpadding="0" width="100%">
	<tr>
		<td valign="top" align="right" style="padding-top:1px; padding-right:7px;" rowspan="2" class="BorderKeyline Right">
			<a href="<%#CurrentCountry.Url()%>"><img src="<%#CurrentCountry.FlagUrl()%>" width="60" height="36" align="top" border="0" class="BorderBlack All"></a>
		</td>
		<td valign="top" style="padding-left:2px;padding-right:10px;" width="100%">
			<a href="<%#CurrentCountry.Url()%>"><%#CurrentCountry.Name%></a>
			<div runat="server" id="EventsSpan">
				<small><%#CurrentCountry.TotalEvents.ToString("#,##0")+" event"+(CurrentCountry.TotalEvents==1?"":"s")%></small>
			</div>
		</td>
	</tr>
</table>
