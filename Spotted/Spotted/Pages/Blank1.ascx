<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Blank1.ascx.cs" Inherits="Spotted.Pages.Blank1" %>
<%@ Register TagPrefix="Controls" TagName="Picker" Src="~/Controls/Picker.ascx" %>

<dsi:h1 runat="server">Blank1</dsi:h1>
<div class="ContentBorder">

	<fb:like href="http://www.facebook.com/armaniexchange" layout="box_count" font="verdana" width="200px"></fb:like>

	<button runat="server" id="Button1">Button1</button> <button runat="server" id="Button2">Button2</button>

	<h2>Output:</h2>

	<pre runat="server" id="Output"></pre>

</div>
