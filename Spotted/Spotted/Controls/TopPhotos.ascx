<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopPhotos.ascx.cs" Inherits="Spotted.Controls.TopPhotos" %>
<div class="p" align="center">
	<asp:DataList Runat="server" 
		ID="PhotoOfWeekAllDataList" RepeatColumns="4" RepeatLayout="Table" 
		RepeatDirection="Horizontal" Width="96%" ItemStyle-Width="24%" 
		ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"/>
</div>
<p align="center">
	<b><a href="/pages/top/all" style="font-size:12px;">Archive</a></b> | <a href="/groups/front-page-suggestions" style="font-size:12px;">suggest a top photo / video</a>
</p>
