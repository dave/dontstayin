<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Terms.ascx.cs" Inherits="Spotted.Blank.Terms" %>
<%@ Register TagPrefix="Spotted" TagName="TermsUser" Src="/Controls/LegalTermsUser.ascx" %>

<script>window.focus();</script>
<link rel="stylesheet" type="text/css" href="/support/style.css"/>
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<table cellpadding=0 cellspacing=0 border=0><tr><td>
<div class="Content">
<dsi:h1 runat="server" ID="H12" NAME="H11">DontStayIn Terms & Conditions</dsi:h1>
<div class="ContentBorder">
<Spotted:TermsUser runat="server"></Spotted:TermsUser>
</div>
</div>
</td></tr></table>
