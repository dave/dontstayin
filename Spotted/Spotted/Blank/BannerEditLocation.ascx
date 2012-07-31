<%@ Import Namespace="Spotted"%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerEditLocation.ascx.cs"
	Inherits="Spotted.Blank.BannerEditLocation" %>
<%@ Register TagPrefix="uc1" TagName="PlacesChooser" Src="~/Controls/PlacesChooser.ascx" %>
<link rel="stylesheet" type="text/css" href="/support/style.css" />

<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<div id="TipLayer" style="visibility:hidden;position:absolute;z-index:1000;"></div>
<script>window.focus();</script>
<%= Main15Script.Register %>
<div class="Content">
	<dsi:h1 runat="server" ID="H12" NAME="H11">
		Banner Locality Targetting</dsi:h1>
	<div class="ContentBorder">
		<h2>
			Targetted towns
		</h2>
		
		<uc1:PlacesChooser ID="uiPlacesChooser" runat="server" />
		<p>
			<input type="button" value="Cancel" causesvalidation="False" id="CancelButton" onclick="Cancel()" />
			<input type="button" value="Save" causesvalidation="False" id="SaveButton" onclick="Save()" />
		</p>
	</div>
</div><script>
function CloseWindow()
{
	window.close();
	opener.focus();
}
function Cancel()
{
	CloseWindow();
}
function Save()
{
	CloseWindow();
	try
	{
		var placeKs = '';
		var values = <%= uiPlacesChooser.ClientID %>Controller.view.get_uiPlacesMultiSelector().getSelections().toArray();
		for (var i=0;i<values.length;i++)
		{
			var pair = values[i];
			placeKs += pair[1] + ',';
		}
		placeKs = placeKs.substring(0, placeKs.length - 1);
		opener.SetPlaceTargettingString(escape(placeKs));
	}
	catch(ex){alert(ex.message);}
}

</script>
