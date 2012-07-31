var ^ButtonState;
var ^DbChatSetLanguage;
var ^DbChatDoneInit = false;
function DbButtonInit($language)
{
	if (!^DbChatDoneInit)
	{
		^DbChatDoneInit = true;
		^DbChatSetLanguage = $language;
		^ButtonState = [];
	}
}
function DbButton(
	$imageFileNameTrue,
	$imageFileNameFalse,
	$altTrue,
	$altFalse,
	$textTrue,
	$textFalse,
	$class,
	$style,
	$align,
	$imageWidth,
	$imageHeight,
	$functionName,
	$functionArgs,
	$initialStateString,
	$id,
	$onReturn,
	$confirmStringTrue,
	$confirmStringFalse)
{
	DbButtonFull(
		$imageFileNameTrue,
		$imageFileNameFalse,
		$altTrue,
		$altFalse,
		$textTrue,
		$textFalse,
		$class,
		$style,
		$align,
		$imageWidth,
		$imageHeight,
		$functionName,
		$functionArgs,
		$initialStateString,
		$id,
		$onReturn,
		$confirmStringTrue,
		$confirmStringFalse,
		"");
}
function DbButtonFull(
	$imageFileNameTrue,
	$imageFileNameFalse,
	$altTrue,
	$altFalse,
	$textTrue,
	$textFalse,
	$class,
	$style,
	$align,
	$imageWidth,
	$imageHeight,
	$functionName,
	$functionArgs,
	$initialStateString,
	$id,
	$onReturn,
	$confirmStringTrue,
	$confirmStringFalse,
	$writeToElementId)
{
	
	try
	{
		var $initialiseHtml = $writeToElementId != "-";
		var $initialState = $initialStateString == "1" ? true : false;
		^ButtonState[$id+"-1"] = $initialState;
		^ButtonState[$id+"-4"] = $altTrue;
		^ButtonState[$id+"-5"] = $altFalse;
		^ButtonState[$id+"-6"] = $textTrue;
		^ButtonState[$id+"-7"] = $textFalse;
		^ButtonState[$id+"-8"] = $functionName;
		^ButtonState[$id+"-9"] = $functionArgs;
		^ButtonState[$id+"-10"] = $onReturn;
		
		if ($initialiseHtml)
		{
			var $initialAlt   = $initialState ? $altTrue : $altFalse;
			var $initialText  = $initialState ? $textTrue : $textFalse;
			var $alignAttribute = $align==""?"":"align=\""+$align+"\"";
			var $styleAttribute = $style==""?"":"style=\""+$style+"\"";
			var $classAttribute = $class==""?"":"class=\""+$class+"\"";
			var $mouseOverAttribute = $initialAlt==""?"":"onmouseover=\"stt('"+$initialAlt.replace(/'/,"\\'")+"');\" onmouseout=\"htm();\"";
			var $textSpan = $initialText==""?"":"<span id=\""+$id+"-span\">"+$initialText+"</span>";
			
			var $buttonHtml = "<span id=\""+$id+"\" "+$mouseOverAttribute+"><a href=\"\" "+$styleAttribute+" "+$classAttribute+" onclick=\"DbButtonClick('"+$id+"');return false;\"><img src=\""+$imageFileNameTrue+"\" border=\"0\" "+$alignAttribute+" "+$styleAttribute+" "+$classAttribute+" height=\""+$imageHeight+"\" width=\""+$imageWidth+"\" id=\""+$id+"-true\"><img src=\""+$imageFileNameFalse+"\" border=\"0\" "+$alignAttribute+" "+$styleAttribute+" "+$classAttribute+" height=\""+$imageHeight+"\" width=\""+$imageWidth+"\" id=\""+$id+"-false\">"+$textSpan+"</a></span>";
			if ($writeToElementId != "-")
			{
				if ($writeToElementId == "")
					document.write($buttonHtml);
				else
					document.getElementById($writeToElementId).innerHTML = $buttonHtml;
			}
		}
		var $imgTrue = document.getElementById($id+"-true");
		var $imgFalse = document.getElementById($id+"-false");
		if ($initialiseHtml)
		{
			$imgTrue.style.display = $initialState?"":"none";
			$imgFalse.style.display = ! $initialState?"":"none";
		}
		^ButtonState[$id+"-11"] = $imgTrue;
		^ButtonState[$id+"-12"] = $imgFalse;
		^ButtonState[$id+"-13"] = false;
		^ButtonState[$id+"-14"] = $confirmStringTrue;
		^ButtonState[$id+"-15"] = $confirmStringFalse;
	}
	catch(ex){}
}
function DbButtonClick($id)
{
	WhenLoggedIn(
		function()
		{
			var $state = ^ButtonState[$id+"-1"];
	
			var $newState = ! $state;
	
			var $confirmString;
			if ($newState)
				$confirmString = ^ButtonState[$id+"-14"];
			else
				$confirmString = ^ButtonState[$id+"-15"];
	
			if ($confirmString!="")
			{
				if (!confirm($confirmString))
					return;
			}
	
			DbButtonSetState($id, $newState);
	
			var $altTrue = ^ButtonState[$id+"-4"];
			var $altFalse = ^ButtonState[$id+"-5"];
			var $textTrue = ^ButtonState[$id+"-6"];
			var $textFalse = ^ButtonState[$id+"-7"];
			var $functionName = ^ButtonState[$id+"-8"];
			var $functionArgs = ^ButtonState[$id+"-9"];

			var $xmlDom = ^CreateXmlDom();
			var $docNode = ^XmlInit($xmlDom);
	
			$docNode.setAttribute("s",$newState?"1":"0");
			$docNode.setAttribute("f",$functionName);
			$docNode.setAttribute("a",$functionArgs);
			$docNode.setAttribute("p",location.href);
	
			if (!^ButtonState[$id+"-13"])
				^ButtonState[$id+"-13"] = ^CreateXmlHttp();
			var $xmlhttp = ^ButtonState[$id+"-13"];
			try
			{
					$xmlhttp.open("POST","/support/DbButtonServer.aspx",true);
					$xmlhttp.setRequestHeader("Content-Type","text/xml");
					if (^DbChatSetLanguage)
						$xmlhttp.setRequestHeader("Accept-Language","en/us");
					$xmlhttp.onreadystatechange = function(){^ButtonReturn($id)};
					$xmlhttp.send(^GetXML($xmlDom));
			}
			catch(ex){}
		}
	);
}
function DbButtonSetState($id,$state)
{
	if($id.split("_").length > 1)
	{
		var idArray = $id.split("_");
		for(var i=0; i<idArray.length; i++)
		{
			DbButtonSetState(idArray[i], $state);
		}
	}
	var $altTrue = ^ButtonState[$id+"-4"];
	var $altFalse = ^ButtonState[$id+"-5"];
	var $textTrue = ^ButtonState[$id+"-6"];
	var $textFalse = ^ButtonState[$id+"-7"];
	var $imgTrue = ^ButtonState[$id+"-11"];
	var $imgFalse = ^ButtonState[$id+"-12"];
	
	^ButtonState[$id+"-1"] = $state;
	$imgTrue.style.display = $state?"":"none";
	$imgFalse.style.display = ! $state?"":"none";
	
	var $altNow = $state?$altTrue:$altFalse;
	if ($altNow=="")
		document.getElementById($id).onmouseover = function(){};
	else
		document.getElementById($id).onmouseover = function(){stt($altNow);};
		
	var $textNow = $state?$textTrue:$textFalse;
	if ($textNow!="")
	{
		document.getElementById($id+"-span").innerHTML=$textNow;
	}
}
function DbButtonSetArgs($id,$args)
{
	^ButtonState[$id+"-9"] = $args;
}
function ^ButtonReturn($id)
{
	if (!^ButtonState[$id+"-13"])
		return;
		
	if (^ButtonState[$id+"-13"]==null)
		return;
		
	if (^ButtonState[$id+"-13"].readyState==4)
	{
		try
		{
			if (^ButtonState[$id+"-13"].status==200)
			{
				htm();
				
				var $oldState = ^ButtonState[$id+"-1"];
				var $onReturn = ^ButtonState[$id+"-10"];
				var $doc = ^ButtonState[$id+"-13"].responseXML.documentElement;

				if ($doc.getAttribute("s")!=null && $doc.getAttribute("s").length>0)
				{
					var $newState = ($doc.getAttribute("s") == "1"?true:false);

					DbButtonSetState($id,$newState);					

					if ($onReturn!="")
					{
						eval($onReturn+"('"+$id+"',"+$oldState+","+$newState+");");
					}
				}
				else if ($doc.getAttribute("l")!=null && $doc.getAttribute("l").length>0)
				{
					document.location="/pages/login?er=You+must+log+on+before+using+this+button&url="+escape(document.location);
				}
				else if ($doc.getAttribute("r")!=null && $doc.getAttribute("r").length>0)
				{
					document.location=$doc.getAttribute("r");
				}
				
				if ($doc.getAttribute("e")!=null && $doc.getAttribute("e").length>0)
				{
					eval($doc.getAttribute("e"));
				}
				try
				{
					if($doc.getAttribute("ex")!=null && $doc.getAttribute("ex").length>0)
					{

						DbChatDebug("[Button error (1): "+$doc.getAttribute("ex")+"]");
						
					}
				}
				catch(ex)
				{
				}
			}
		}
		catch(ex)
		{
			DbChatDebug("[Button error (2): "+ex.message+"]");
		}
	}
}
