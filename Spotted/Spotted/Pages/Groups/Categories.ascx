<%@ Import Namespace="System.Linq"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categories.ascx.cs" Inherits="Spotted.Pages.Groups.Categories" %>
<%@ Register Src="~/Controls/GroupsListedByHeading.ascx" TagPrefix="uc1" TagName="GroupsListedByHeading" %>
<div>
	<dsi:h1 runat="server">Groups by category</dsi:h1>
	<div class="ContentBorder">
		<uc1:GroupsListedByHeading runat="server" id="uiList"></uc1:GroupsListedByHeading>
	</div>
</div>
