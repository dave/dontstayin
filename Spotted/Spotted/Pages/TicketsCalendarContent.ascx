<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketsCalendarContent.ascx.cs" Inherits="Spotted.Pages.TicketsCalendarContent" %>
<%@ Register TagPrefix="Spotted" TagName="Calendar" Src="/Controls/Calendar.ascx" %>
<Spotted:Calendar Runat="server" ID="Calendar" Personalise="true" Tickets="true"/>
