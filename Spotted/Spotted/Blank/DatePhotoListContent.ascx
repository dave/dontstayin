<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePhotoListContent.ascx.cs" Inherits="Spotted.Blank.DatePhotoListContent" %>
<!--%@ OutputCache Duration="3600" VaryByCustom="PageName" VaryByParam="None" %-->
<style>
body{
background-color:#000000;
}
td
{
vertical-align:top;
text-align:center;
font-family: Verdana, sans-serif;
font-size:10px;
}
img
{
border-right:1px solid #000000;
}
small
{
color:#A58319;
}
</style>
<script>
function showDatePic(k,filename)
{
	try
	{
		parent.document.getElementById("Content_FindDateProfile_WebImg").src=filename;
	}
	catch(err){}
	
	try
	{
		parent.document.getElementById("Content_FindDateEditProfile_WebImg").src=filename;
	}
	catch(err){}
	
	var left = document.getElementById("P"+k).offsetParent.offsetLeft - document.body.clientWidth/2 + 50;
	window.scroll(left,0);
}
function JumpPhoto()
{
	if (document.location.hash.length>0)
	{
		try
		{
			var left = document.getElementById(document.location.hash.replace("#Photo","P")).offsetParent.offsetLeft - document.body.clientWidth/2 + 50;
			window.scroll(left,0);
		}
		catch(err){}
	}	
}
</script>
<table cellpadding="0" cellspacing="0" style="border-bottom:1px solid #000000;"><tr><asp:Repeater Runat="server" ID="PhotosDataList"/></tr></table>
<script>
JumpPhoto();
</script>
