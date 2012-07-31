var ^HitPhotoXmlHttp;

function HitPhoto($k)
{
	var $xmlDom = ^CreateXmlDom();
	var $docNode = ^XmlInit($xmlDom);
	
	$docNode.setAttribute("k",$k);
	
	if (!^HitPhotoXmlHttp)
		^HitPhotoXmlHttp = ^CreateXmlHttp();

	try
	{
		^HitPhotoXmlHttp.open("POST","/support/HitPhoto.aspx",true);
		^HitPhotoXmlHttp.setRequestHeader("Content-Type","text/xml");
		if (^DbChatSetLanguage)
			^HitPhotoXmlHttp.setRequestHeader("Accept-Language","en/us");
		^HitPhotoXmlHttp.onreadystatechange = function(){^HitPhotoReturn($k)};
		^HitPhotoXmlHttp.send(^GetXML($xmlDom));
	}
	catch(ex){}
}
function ^HitPhotoReturn($k)
{
	if (!^HitPhotoXmlHttp)
		return;
		
	if (^HitPhotoXmlHttp==null)
		return;
		
	if (^HitPhotoXmlHttp.readyState==4)
	{
		try
		{
			if (^HitPhotoXmlHttp.status==200)
			{
				var $doc = ^HitPhotoXmlHttp.responseXML.documentElement;
				
				var $photoK = $doc.getAttribute("k");
				var $fav = $doc.getAttribute("fav");
				var $me = $doc.getAttribute("me");
				var $html = ^NodeText($doc);
				
				SwitchPhotoReturn($photoK,$me,$fav,$html);

				try
				{
					if($doc.getAttribute("ex")!=null && $doc.getAttribute("ex").length>0)
					{
						DbChatDebug("[HitPhoto server error: "+$doc.getAttribute("ex")+"]");
					}
				}
				catch(ex)
				{
				}
			}
		}
		catch(ex)
		{
			DbChatDebug("[HitPhoto javascript error: "+ex.message+"]");
		}
	}
}
