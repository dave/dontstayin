<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarContent.ascx.cs" Inherits="Spotted.Pages.CalendarContent" %>
<!--%@ OutputCache Duration="600" VaryByCustom="Country;PageName;MusicPref;" VaryByParam="None" %-->
<%@ Register TagPrefix="Spotted" TagName="Calendar" Src="/Controls/Calendar.ascx" %>
<Spotted:Calendar Runat="server" ID="CalendarUc" Personalise="false"/>
