<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditPara.ascx.cs" Inherits="Spotted.Templates.Articles.EditPara" %>
<a name="ArticlePara<%#CurrentPara.K%>"></a>
<asp:PlaceHolder Runat="server" ID="ParaPh"></asp:PlaceHolder>
<div align="right" style="margin-bottom:8px;padding:3px;">
	<asp:Button ID="Button1" Runat="server" Text="Edit" OnClick="EditClick"/>
	<asp:Button ID="Button2" Runat="server" Text="Photo" OnClick="PhotoClick"/>
	<asp:Button ID="Button3" Runat="server" Text="Up" OnClick="UpClick"/>
	<asp:Button ID="Button4" Runat="server" Text="Down" OnClick="DownClick"/>
	<asp:Button Runat="server" Text="Delete" OnClick="DeleteClick" ID="DeleteButton"/>
	<asp:Button ID="Button5" Runat="server" Text="New" OnClick="NewClick"/>
</div>
