<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickBrowserPhotoListContent.ascx.cs" Inherits="Spotted.Blank.QuickBrowserPhotoListContent" %>
<!--%@ OutputCache Duration="600" VaryByCustom="PageName" VaryByParam="None" %-->
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
<%= Spotted.Main15Script.Register %>
<DIV id="TipLayer" style="visibility:hidden;position:absolute;z-index:1000;top:0px;left:0px;"></DIV>
<script>
function SwitchPhoto(k, pic, video, comments, views, picWidth, picHeight, overlay)
{
	var UrlFragment = "/<%= CurrentGallery.UrlFragment %>";
	
	//try
	//{
	//	document.getElementById("LoadingSpan").style.display="";
	//	document.getElementById("LoadingSpan").style.left=document.getElementById("P"+k).offsetParent.offsetLeft - 1;
	//	document.getElementById("LoadingSpan").style.top=-1;
	//}
	//catch(err){}

	try
	{	
		var left = document.getElementById("P"+k).offsetParent.offsetLeft - document.body.clientWidth/2 + 50;
		window.scroll(left,0);
	}
	catch(err){}
	
	try
	{
		window.parent.SwitchPhotoStart(k, pic, video, comments, views, picWidth, picHeight, UrlFragment, overlay);
	}
	catch(err){}
	
	try
	{
		window.parent.NextBanner();
	}
	catch(err){}
	
	//should really wait for load (but we can't now with videos...)
	
	//try
	//{
	//	document.getElementById("LoadingSpan").style.display="none";
	//}
	//catch(err){}
	
	//try
	//{
	//	parent.document.getElementById("AnchorAbovePhoto").focus();
	//	document.getElementById("P"+k).focus();
	//}
	//catch(err2){}
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
