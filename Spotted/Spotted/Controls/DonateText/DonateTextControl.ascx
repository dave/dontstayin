<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DonateTextControl.ascx.cs" Inherits="Spotted.Controls.DonateText.DonateTextControl" %>
<%@ Register TagPrefix="DonateText" TagName="Basic"  Src="~/Controls/DonateText/Basic.ascx" %>
<%@ Register TagPrefix="DonateText" TagName="Default"  Src="~/Controls/DonateText/Default.ascx" %>
<%@ Register TagPrefix="DonateText" TagName="Monkey"  Src="~/Controls/DonateText/Monkey.ascx" %>

<DonateText:Basic runat="server" ID="uiBasic" Visible="false" />
<DonateText:Default runat="server" ID="uiDefault" Visible="false" />
<DonateText:Monkey runat="server" ID="uiMonkey" Visible="false" />
