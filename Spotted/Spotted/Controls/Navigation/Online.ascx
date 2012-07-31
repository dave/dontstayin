<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Online.ascx.cs" Inherits="Spotted.Controls.Navigation.Online" %>
<iframe name="usrsOnlineIframe" src="/Support/Online.aspx?v=1&online=<%= Usr.Current == null ? "0" : "1" %>" style="display:none" onload="extractUsrsOnline()"></iframe>
<div id="usrsOnlineSpan"></div>
