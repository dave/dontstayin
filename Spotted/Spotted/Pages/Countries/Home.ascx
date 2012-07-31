<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Pages.Countries.Home" %>
<%@ Register TagPrefix="Spotted" TagName="HomeContentTop" Src="HomeContentTop.ascx" %>
<%@ Register TagPrefix="Spotted" TagName="Latest" Src="/Controls/Latest.ascx" %>
<asp:PlaceHolder Runat="server" ID="ContentPlaceHolder" EnableViewState="False"/>
<Spotted:HomeContentTop Runat="server" ID="HomeContentTopUc" EnableViewState="False"/>
<Spotted:Latest runat="server" ID="Latest" ParentObjectType="Country" Items="5" />
