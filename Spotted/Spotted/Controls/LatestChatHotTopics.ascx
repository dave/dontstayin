<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestChatHotTopics.ascx.cs" Inherits="Spotted.Controls.LatestChatHotTopics" %>
<!--%@ OutputCache Duration="600" VaryByCustom="Country;PageName;Usr" VaryByParam="*" %-->
<%@ Register TagPrefix="Spotted" TagName="LatestChat" Src="/Controls/LatestChat.ascx" %>
<Spotted:LatestChat Runat="server" ID="LatestChatUc" EnableViewState="False" HotTopicsOnly="true" DbButtonPrefix="b" />
