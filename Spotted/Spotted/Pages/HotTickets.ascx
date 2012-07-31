<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotTickets.ascx.cs" Inherits="Spotted.Pages.HotTickets" %>
<%@ Register TagPrefix="Spotted" TagName="Calendar" Src="/Controls/Calendar.ascx" %>
<Spotted:Calendar Runat="server" ID="Calendar" HotTickets="true"/>
