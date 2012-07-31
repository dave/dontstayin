<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LegalInformationPolicy.ascx.cs" Inherits="Spotted.Blank.LegalInformationPolicy" %>
<%@ Register TagPrefix="Spotted" TagName="InformationPolicy" Src="/Controls/LegalInformationPolicy.ascx" %>

<script>window.focus();</script>
<link rel="stylesheet" type="text/css" href="/support/style.css"/>
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<table cellpadding=0 cellspacing=0 border=0><tr><td>
<div class="Content">
<dsi:h1 runat="server" ID="H12" NAME="H11">Information policy</dsi:h1>
<div class="ContentBorder">
<Spotted:InformationPolicy runat="server" UsePopups="true"></Spotted:InformationPolicy>
</div>
</div>
</td></tr></table>
<script>
function openPopup(url)
{
	var popUp = window.open(url, popUp, 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=400');
}
</script>
