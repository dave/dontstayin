<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Pages.Articles.Home" %>
<%@ Register TagPrefix="Controls" TagName="Thread" Src="/Controls/ThreadControl.ascx" %>
<%@ Register TagPrefix="Controls" TagName="LatestChat" Src="/Controls/LatestChat.ascx" %>
<%@ Register TagPrefix="Controls" TagName="HomeContent" Src="/Pages/Articles/HomeContent.ascx" %>

<Controls:HomeContent runat="server" id="HomeContent" />

<Controls:LatestChat runat="server" ID="LatestChat" ParentObjectType="Article" Items="200" ShowHolder="true" />
	
<a name="Comments"></a>
<Controls:Thread runat="server" ID="ThreadControl" />

<input runat="server" id="uiThreadK" type="hidden" />
<input runat="server" id="uiArticleK" type="hidden" />
<input runat="server" id="uiPageNumber" type="hidden" />
