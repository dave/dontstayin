function UnreadString($firstUnreadPage)
{
	return "Skip to new comments on page " + $firstUnreadPage;
}
function UnreadNewString($type, $objectType)
{
	if ($type == 0)
	{
		return "You have been invited to this topic by a buddy";
	}
	else if ($type == 4)
	{
		var $objectTypeString = "in a forum";
		
		if ($objectType == 1)
			$objectTypeString = "on a photo";
		else if ($objectType == 2)
			$objectTypeString = "in an event";
		else if ($objectType == 3)
			$objectTypeString = "in a venue";
		else if ($objectType == 4)
			$objectTypeString = "in a place";
		else if ($objectType == 6)
			$objectTypeString = "in a topic";
		else if ($objectType == 7)
			$objectTypeString = "in a country";
		else if ($objectType == 8)
			$objectTypeString = "on an article";
		else if ($objectType == 10)
			$objectTypeString = "in a brand";
		else if ($objectType == 13)
			$objectTypeString = "in a region";
		else if ($objectType == 14)
			$objectTypeString = "in a gallery";
		else if ($objectType == 15)
			$objectTypeString = "in a group";
        
		return "This is a new topic " + $objectTypeString + " you're watching";
	}
	else if ($type == 5)
	{
		return "This is new group news topic";
	}
	else
	{
		return "n/a";
	}
}
function QuoteGeneric($textBox, $usrK)
{
	var $txt = "";
	if (window.getSelection)
		$txt = window.getSelection()+"";
	else if (document.getSelection)
		$txt = document.getSelection()+"";
	else if (document.selection)
		$txt = document.selection.createRange().text+"";
	
	$txt = $txt.replace(/<(.|\n)*?>/g, "");
	$txt = $txt.replace(/^\s+|\s+$/g, "");
	
	if ($txt.length > 500)
		$txt = $txt.substring(0, 499) + "...";
	
	if ($txt.length==0)
	{
		alert("First select some text to quote");
		return;
	}
	
	$textBox.value += "<dsi:quote ref=\"" + $usrK + "\">" + $txt + "</dsi:quote>\n\n";
}
function FocusGeneric($textBox)
{
	var $isIE = false;
	if (document.all && window.ActiveXObject && navigator.userAgent.toLowerCase().indexOf("msie") > -1  && navigator.userAgent.toLowerCase().indexOf("opera") == -1)
		$isIE = true;
					
	if ($isIE) {
		var $range = $textBox.createTextRange();
		$range.moveStart("character", $textBox.value.length);
		$range.moveEnd("character", $textBox.value.length);
		$range.collapse(true);
		$textBox.focus();
		$range.select();
	} else {
		$textBox.focus();
		$textBox.setSelectionRange($textBox.value.length, $textBox.value.length);
	}
}
