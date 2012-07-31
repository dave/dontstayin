<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ParaPhotoList.ascx.cs" Inherits="Spotted.Blank.ParaPhotoList" %>
<style>
body{
background-color:#<%= BgCol %>;
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
function SwitchPhoto(k,icon,thumb,web)
{
	try
	{
		window.parent.UpdateSelectedPhoto(k);
		
		var left = document.getElementById("P"+k).offsetParent.offsetLeft - document.body.clientWidth/2 + 50;
		window.scroll(left,0);
	}
	catch(err)
	{
		alert(err);
	}
}
function UrlDecode(psEncodeString) 
{
  var lsRegExp = /\+/g;
  return unescape(String(psEncodeString).replace(lsRegExp," "));
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
<table cellpadding="0" cellspacing="0" style="border-bottom:1px solid #000000;"><tr><asp:Repeater 
Runat="server" ID="PhotosDataList"/></tr></table>
<div runat="server" id="NoPhotosDiv" style="padding:8px;font-family: Verdana, sans-serif;font-size:12px;">No photos uploaded. <a href="/pages/galleries/add/articlek-<%= CurrentArticle!=null?CurrentArticle.K.ToString():"" %>" target="_parent">Click here to upload some</a>.</div>
<script>
JumpPhoto();
</script>
