<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LegalTermsPromoter.ascx.cs" Inherits="Spotted.Blank.LegalTermsPromoter" %>
<%@ Register TagPrefix="Spotted" TagName="Terms" Src="/Controls/LegalTermsPromoter.ascx" %>

<script>window.focus();</script>
<link rel="stylesheet" type="text/css" href="/support/style.css"/>
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<table cellpadding=0 cellspacing=0 border=0><tr><td>
<div class="Content">
<dsi:h1 runat="server" ID="H12" NAME="H11">Terms for promoter account</dsi:h1>
<div class="ContentBorder">
<Spotted:Terms runat="server" UsePopups="true"></Spotted:Terms>
</div>
</div>
</td></tr></table>
<script>
function openPopup(url)
{
	var popUp = window.open(url, popUp, 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=500,height=400');
}
</script>
