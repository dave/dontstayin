<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarTicketsContent.ascx.cs" Inherits="Spotted.Pages.CalendarTicketsContent" %>
<!--%@ OutputCache Duration="600" VaryByCustom="Country;PageName;MusicPref;" VaryByParam="None" %-->
<%@ Register TagPrefix="Spotted" TagName="Calendar" Src="/Controls/Calendar.ascx" %>
<Spotted:Calendar Runat="server" ID="Calendar" Personalise="false" Tickets="true"/>
